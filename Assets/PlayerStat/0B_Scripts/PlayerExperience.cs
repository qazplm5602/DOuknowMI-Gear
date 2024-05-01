using System;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    public int level = 1;
    public int currentExp = 0;
    [SerializeField] private int _needExp = 1;

    [SerializeField] private PlayerStat _playerStat;

    public event Action LevelUpEvent;

    public void GetExp(int value) {
        currentExp += value;

        CheckExp();
    }

    private void CheckExp() {
        if(currentExp >= _needExp) {
            currentExp -= _needExp;
            ++level;
            _playerStat.statPoint += 3;
            
            LevelUpEvent?.Invoke();

            CheckExp();
        }
    }
}
