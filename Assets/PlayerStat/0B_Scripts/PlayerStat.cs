using System;
using TMPro;
using UnityEngine;

public enum PlayerStatEnum {
    Atk, Health, Speed, Defence, CriticalChance
}

public class PlayerStat : MonoSingleton<PlayerStat>
{
    public int statPoint;
    
    #region Stats

    [Header("point per increas value")]
    public int atkPerPointInc;
    public int healthPerPointInc;
    public float attackCoolPerPointInc;
    public float moveSpeedPerPointInc;
    public int defencePerPointInc;
    public float criticalChancePerPointInc;

    public Action OnUpdateStat;

    private int _atk;
    private int _health;
    private int _defence;
    private int _speed;
    private int _criticalChance;

    private Player player;

    private void Start() {
        player = PlayerManager.instance.player;
    }

    [HideInInspector] public int Atk {
        get => _atk;
        set {
            _atk = value;
            player.stat.attack.SetDefaultValue((int)(player.stat.attack.GetDefaultValue() + value * atkPerPointInc));
            _atkText.text = _atk.ToString();
            OnUpdateStat?.Invoke();
        }
    }
    [HideInInspector] public int Health {
        get => _health;
        set {
            _health = value;
            player.stat.maxHealth.SetDefaultValue((int)(player.stat.maxHealth.GetDefaultValue() + value * healthPerPointInc));
            _healthText.text = _health.ToString();
            OnUpdateStat?.Invoke();
        }
    }
    [HideInInspector] public int Speed {
        get => _speed;
        set {
            _speed = value;
            player.stat.moveSpeed.SetDefaultValue((int)(player.stat.moveSpeed.GetDefaultValue() + value * moveSpeedPerPointInc  * 1000));
            player.stat.attackSpeed.SetDefaultValue((int)(player.stat.attackSpeed.GetDefaultValue() - value * attackCoolPerPointInc * 1000));
            _speedText.text = _speed.ToString();
            OnUpdateStat?.Invoke();
        }
    }

    [HideInInspector] public int Defence {
        get => _defence;
        set {
            _defence = value;
            player.stat.defense.SetDefaultValue((int)(player.stat.defense.GetDefaultValue() + value * defencePerPointInc));
            _defenceText.text = _defence.ToString();
            OnUpdateStat?.Invoke();
        }
    }

    [HideInInspector] public int CriticalChance {
        get => _criticalChance;
        set {
            _criticalChance = value;
            player.stat.criticalChance.SetDefaultValue((int)
                (player.stat.criticalChance.GetDefaultValue() + value * criticalChancePerPointInc * 1000) * 1000);
            _criticalChanceText.text = _criticalChance.ToString();
            OnUpdateStat?.Invoke();
        }
    }

    #endregion

    [Header("Stat Text")]
    public TextMeshProUGUI _atkText;
    public TextMeshProUGUI _healthText;
    public TextMeshProUGUI _speedText;
    public TextMeshProUGUI _defenceText;
    public TextMeshProUGUI _criticalChanceText;

    public TextMeshProUGUI _statPointText;

    public void IncreaseStat(int stat) {
        if(statPoint <= 0) return;

        --statPoint;
        _statPointText.text = $"StatPoint: {statPoint}";

        switch((PlayerStatEnum)stat) {
            case PlayerStatEnum.Atk:
                ++Atk;
                break;
            case PlayerStatEnum.Health:
                ++Health;
                break;
            case PlayerStatEnum.Speed:
                ++Speed;
                break;
            case PlayerStatEnum.CriticalChance:
                ++CriticalChance;
                break;
            case PlayerStatEnum.Defence:
                ++Defence;
                break;
        }
    }

    public void DecreaseStat(int stat) {
        switch((PlayerStatEnum)stat) {
            case PlayerStatEnum.Atk:
                if(Atk > 0) --Atk;
                else return;
                break;
            case PlayerStatEnum.Health:
                if(Health > 0) --Health;
                else return;
                break;
            case PlayerStatEnum.Speed:
                if(Speed > 0) --Speed;
                else return;
                break;
        }
        
        ++statPoint;
        _statPointText.text = $"StatPoint: {statPoint}";
    }

    public void ResetStat() {
        statPoint += Atk;
        statPoint += Health;
        statPoint += Speed;
        statPoint += Defence;
        statPoint += CriticalChance;
        
        _statPointText.text = $"StatPoint: {statPoint}";

        Atk = 0;
        Health = 0;
        Speed = 0;
        Defence = 0;
        CriticalChance = 0;
    }
}
