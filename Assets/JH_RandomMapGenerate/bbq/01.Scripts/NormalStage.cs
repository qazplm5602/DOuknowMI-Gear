using FSM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class NormalStage : BaseStage
{
    [Header("FUCKING ENEMIES WEIGHT")]
    public Enemy[] Enemies;
    [Header("무게, 스폰 확률인. 적배열의 크기와 같아야한다.")]
    public int[] Weights;
    [Header("무게, 방의 총 적들의 크기를 알기 위함 (약한 몹은 가볍고 높은 몹은 높다). 적배열의 크기와 같아야한다.")]
    public int[] WeightsFromPower;
    [Header("스폰 포인트들인.")]
    [SerializeField] private Transform[] _spawnPoints;
    [Header("방상태 ㅅㅂ")]
    public int TotalWeight = 0;
    public int MaxWeight;
    public int HighestWeight = 0;

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

    private void OnTransformChildrenChanged()
    {
        var enemy = GetComponentInChildren<Enemy>();
        if (enemy == null)
        {
            NextWave();
        }
    }
}
