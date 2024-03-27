using System;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    [SerializeField] private int _level = 1;
    [SerializeField] private int _currentExp = 0;
    [SerializeField] private int _needExp = 1;

    [SerializeField] private PlayerStat _playerStat;

    public event Action LevelUpEvent;

    public void GetExp(int value) {
        _currentExp += value;

        CheckExp();
    }

    private void CheckExp() {
        if(_currentExp > _needExp) {
            _currentExp -= _needExp;
            ++_level;
            _playerStat.statPoint += 3;
            
            LevelUpEvent?.Invoke();

            CheckExp();
        }
    }
}
