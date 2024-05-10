using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    public static StatManager Instance;
    public int Attack;
    public int health;
    public int defence;
    public int speed;
    public int criticalChance;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else {
            Destroy(gameObject);
        }
    }

    public void SaveStat(PlayerStatEnum type, int value) {
        switch (type) {
            case PlayerStatEnum.Atk:
            Attack = value;
            break;
            case PlayerStatEnum.Health:
            health = value;
            break;
            case PlayerStatEnum.Speed:
            speed = value;
            break;
            case PlayerStatEnum.Defence:
            defence = value;
            break;
            case PlayerStatEnum.CriticalChance:
            criticalChance = value;
            break;
        }
    }
}
