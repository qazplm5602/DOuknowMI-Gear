using System;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    public int level = 1;
    public int currentExp = 0;
    [SerializeField] private int[] _needExp;

    [SerializeField] private PlayerStat _playerStat;

    public event Action LevelUpEvent;

    public void GetExp(int value) {
        currentExp += value;

        CheckExp();
    }

    private void CheckExp() {
        if(currentExp >= _needExp[level - 1]) {
            currentExp -= _needExp[level - 1];
            ++level;
            _playerStat.statPoint += 3;
            
            LevelUpEvent?.Invoke();

            CheckExp();
        }
    }
}
