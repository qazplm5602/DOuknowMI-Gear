using FSM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class NormalStage : BaseStage
{
    [Header("스폰 포인트들인.")]
    [SerializeField] private Transform[] _spawnPoints;
    [Header("FUCKING ENEMIES WEIGHT")]
    public Enemy[] Enemies;
    [Header("무게, 스폰 확률인. 적배열의 크기와 같아야한다.")]
    public int[] Weights;
    [Header("무게, 방의 총 적들의 크기를 알기 위함 (약한 몹은 가볍고 높은 몹은 높다). 적배열의 크기와 같아야한다.")]
    public int[] WeightsFromPower;
    [Header("방상태 ㅅㅂ")]
    public int TotalWeight = 0;
    public int MaxWeight;
    public int HighestWeight = 0;

    public Transform RewardPosition;
    public InteractiveObject RewardChest;
    private bool isRewarded = false;

    public List<Enemy> CurrentEnemies;

    public override void Init()
    {
        base.Init();
        CurrentEnemies = new();
        for (int i = 0; i < Weights.Length; i++)
        {
            if (Weights[i] > HighestWeight)
            {
                HighestWeight = Weights[i]; 
            }
        }
    }
    //대충 보상

    public override void Enter()
    {
        base.Enter();
        if (CurrentWave <= MaxWave)
        {
            Cleared = false;
            SpawnEnemy();
        }
    }

    public override void NextWave()
    {
        base.NextWave();
        if (CurrentWave > MaxWave)
        {
            return;
        }
        SpawnEnemy();
    }

    public void OnEnable()
    {
        CaculateWeight();
        OnClearChanged += HandleClearEvent;
    }

    public void OnDisable()
    {
        OnClearChanged -= HandleClearEvent;
    }

    private void CaculateWeight()
    {
        foreach (var weight in Weights)
        {
            TotalWeight += weight;
        }
    }

    private int GetRandomEnemyIndex()
    {
        int chance = Random.Range(0, TotalWeight);
        for (int i = 0; i < Weights.Length; ++i)
        {
            chance -= Weights[i];
            if (chance < 0)
            {
                return i;
            }
        }
        return 0;
    }

    private Enemy[] GetRandomEnemies()
    {
        List<Enemy> enemies = new();
        int index = GetRandomEnemyIndex();
        int sum = 0;
        int inversedWeight = WeightsFromPower[index];
        while (sum + inversedWeight < MaxWeight)
        {
            enemies.Add(Enemies[index]);
            index = GetRandomEnemyIndex();
            sum += inversedWeight;
        }        
        return enemies.ToArray();
    }

    private void SpawnEnemy()
    {
        StopAllCoroutines();
        StartCoroutine(SpawnEnemeis());
    }

    private IEnumerator SpawnEnemeis()
    {
        int curr = 0, amountSpawnPoint = _spawnPoints.Length;
        var spawnDelay = new WaitForSeconds(.2f);
        if(!(Enemies.Length <= 0))
        {
            Enemy[] _enemies = GetRandomEnemies();
            print(_enemies.Length);
            foreach (var _enemy in _enemies)
            {
                Transform spawnPoint = _spawnPoints[curr];

                Enemy enemy = Instantiate(_enemy, transform);
                enemy.transform.position = spawnPoint.position;

                CurrentEnemies.Add(enemy);

                ++curr;
                if (curr >= amountSpawnPoint) curr = 0;

                yield return spawnDelay;
            }
        }
        Check엄마가있나없나();
    }

    private void Check엄마가있나없나()
    {
        var enemy = GetComponentInChildren<Enemy>();
        if (enemy == null)
        {
            NextWave();
        }
    }

    private void OnTransformChildrenChanged()
    {
        Check엄마가있나없나();
    }

    private void HandleClearEvent(bool _)
    {
        if (Cleared == true && _)
        {
            SpawnReward();
        }
    }



    public void SpawnReward()
    {
        if (RewardChest == null) return;
        if (isRewarded) return;
        isRewarded = true;
        CurrencyChest dick = Instantiate(RewardChest, transform) as CurrencyChest;
        dick.transform.position = RewardPosition.position;
    }
}
