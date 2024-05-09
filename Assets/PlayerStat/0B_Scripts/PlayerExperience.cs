using System;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    public int level = 1;
    public int currentExp = 0;
    [SerializeField] private int[] _needExp;

    private PlayerStat _playerStat;

    public event Action LevelUpEvent;

    private void Awake() {
        _playerStat = PlayerStat.Instance;
        IngameUIControl.Instance.SetHealthLevel(level);
        IngameUIControl.Instance.SetHealthLevelBar(currentExp, _needExp);
    }

    public void GetExp(int value) {
        currentExp += value;
        IngameUIControl.Instance.SetHealthLevelBar(currentExp, _needExp);
        CheckExp();
    }

    private void CheckExp() {
        if(currentExp >= _needExp[level - 1]) {
            currentExp -= _needExp[level - 1];
            ++level;
            _playerStat.statPoint += 3;
            IngameUIControl.Instance.SetHealthLevelBar(currentExp, _needExp);
            IngameUIControl.Instance.SetHealthLevel(level);
            LevelUpEvent?.Invoke();
            
            CheckExp();
        }
    }
}
