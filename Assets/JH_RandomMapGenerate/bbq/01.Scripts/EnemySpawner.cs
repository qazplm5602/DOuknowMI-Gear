using FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemy[] EnemyList;
    public Transform SpawnPoints;

    public void EnemySpawn()
    {
        StopAllCoroutines();
        StartCoroutine(StartSpawn());
    }

    private IEnumerator StartSpawn()
    {
        for (int i = 0; i < SpawnPoints.childCount; ++i)
        {
            Transform spawnPoint = SpawnPoints.GetChild(i);

            Enemy go = Instantiate(EnemyList[Random.Range(0, EnemyList.Length)], spawnPoint.position, Quaternion.identity, transform);
            yield return new WaitForSeconds(.4f);
        }
    }
}
