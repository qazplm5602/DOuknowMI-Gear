using FSM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EliteMobStage : BaseStage
{
    [SerializeField] private Transform[] _spawnPoints;
    [Header("FUCKING ENEMIES WEIGHT")]
    public Enemy[] Enemies;
    [Header("무게, 스폰 확률인.")]
    public int[] Weights;
    [Header("무게, 방의 총 적들의 크기를 알기 위함 (약한 몹은 가볍고 높은 몹은 높다)")]
    public int[] WeightsFromPower;
    public int TotalWeight = 0;
    public int MaxWeight;
    public int HighestWeight = 0;
    [Header("FUCKING ENEMIES WEIGHT")]
    public Enemy[] EliteEnemies;
    //public int TotalWeight = 0;

   


    public override void Init()
    {
        base.Init();
        HighestWeight = Weights.Max();
        MaxWave = 4;
    }
    //대충 보상

    public override void Enter()
    {
        base.Enter();
    }

    [ContextMenu("Start Elite ASS")]
    public void StartEliteStage()
    {
        Cleared = false;
        SpawnEnemy();
    }

    public override void NextWave()
    {
        base.NextWave();
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
        StartCoroutine(SpawnEliteEnemeis());
    }

    private IEnumerator SpawnEnemeis()
    {
        int curr = 0, amountSpawnPoint = _spawnPoints.Length;
        var spawnDelay = new WaitForSeconds(.2f);
        Enemy[] _enemies = GetRandomEnemies();
        foreach (var _enemy in _enemies)
        {
            Transform spawnPoint = _spawnPoints[curr];

            Enemy enemy = Instantiate(_enemy, Arena.transform);
            enemy.transform.position = spawnPoint.position;

            ++curr;
            if (curr > amountSpawnPoint) curr = 0;
            yield return spawnDelay;
        }
    }

    private IEnumerator SpawnEliteEnemeis()
    {
        int i = 0;
        Transform spawnPoint = _spawnPoints[i];

        Enemy _enemy = EliteEnemies[Random.Range(0, EliteEnemies.Length)];

        Enemy enemy = Instantiate(_enemy, transform);
        enemy.transform.position = spawnPoint.position;

        if (CurrentWave == MaxWave)
        {
            ++i;
            if (i > _spawnPoints.Length) i = 0;
            yield return new WaitForSeconds(.2f);

            spawnPoint = _spawnPoints[i];
            _enemy = EliteEnemies[Random.Range(0, EliteEnemies.Length)];

            enemy = Instantiate(_enemy, transform);
            enemy.transform.position = spawnPoint.position;
        }
        yield return null;
    }

    private void OnTransformChildrenChanged()
    {
        if (Cleared) return;
        var enemy = GetComponentInChildren<Enemy>();
        if (enemy == null)
        {
            NextWave();
        }
    }
}
