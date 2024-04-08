using System;
using TMPro;
using UnityEngine;

public enum PlayerStatEnum {
    Atk, Health, Speed, CriticalChance, Defence, Money, Exp
}

public class PlayerStat : MonoBehaviour
{
    public int statPoint;
    
    #region Stats

    [Header("default")]
    public int defaultAtk;
    public int defaultHealth;
    public float defaultAttackCool;
    public float defaultMoveSpeed;
    public float defaultDefence;
    public float defaultCriticalChance;

    [Header("point per increas value")]
    public float atkPerPointInc;
    public float healthPerPointInc;
    public float attackCoolPerPointInc;
    public float moveSpeedPerPointInc;
    public float defencePerPointInc;
    public float ciriticalChancePerPointInc;

    public Action OnUpdateStat;

    private int _atk;
    private int _health;
    private int _defence;
    private int _speed;
    private int _criticalChance;
    private int _money;
    private int _exp;

    #region current stat
    public int currentAtk {get; private set;}
    public int currentHealth {get; private set;}
    public int currentMoveSpeed {get; private set;}
    public int currentDefence {get; private set;}
    public float currentCriticalChance {get; private set;}
    public float currentAttckCool {get; private set;}
    #endregion

    [HideInInspector] public int Atk {
        get => _atk;
        set {
            _atk = value;
            _atkText.text = _atk.ToString();
            currentAtk = (int)(defaultAtk + _atk * atkPerPointInc);
            OnUpdateStat?.Invoke();
        }
    }
    [HideInInspector] public int Health {
        get => _health;
        set {
            _health = value;
            _healthText.text = _health.ToString();
            currentHealth = (int)(defaultHealth + _health * healthPerPointInc);
            OnUpdateStat?.Invoke();
        }
    }
    [HideInInspector] public int Speed {
        get => _speed;
        set {
            _speed = value;
            _speedText.text = _speed.ToString();
            currentAttckCool = defaultAttackCool - _speed * attackCoolPerPointInc;
            currentMoveSpeed = (int)(defaultMoveSpeed + _speed * moveSpeedPerPointInc);
            OnUpdateStat?.Invoke();
        }
    }

    [HideInInspector] public int Defence {
        get => _defence;
        set {
            _defence = value;
            _defenceText.text = _defence.ToString();
            currentDefence = (int)(defaultDefence + _defence * defencePerPointInc);
            OnUpdateStat?.Invoke();
        }
    }

    [HideInInspector] public int CriticalChance {
        get => _criticalChance;
        set {
            _criticalChance = value;
            _criticalChanceText.text = _criticalChance.ToString();
            currentCriticalChance = defaultCriticalChance + _criticalChance * ciriticalChancePerPointInc;
            OnUpdateStat?.Invoke();
        }
    }

    [HideInInspector] public int Money {
        get => _money;
        set {
            _money = value;
            _moneyText.text = _money.ToString();
        }
    }
    [HideInInspector] public int Exp {
        get => _exp;
        set {
            _exp = value;
            _expText.text = _exp.ToString();
        }
    }

    #endregion

    [Header("Stat Text")]
    public TextMeshProUGUI _atkText;
    public TextMeshProUGUI _healthText;
    public TextMeshProUGUI _speedText;
    public TextMeshProUGUI _criticalChanceText;
    public TextMeshProUGUI _defenceText;
    public TextMeshProUGUI _moneyText;
    public TextMeshProUGUI _expText;

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
            case PlayerStatEnum.Money:
                ++Money;
                break;
            case PlayerStatEnum.Exp:
                ++Exp;
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
            case PlayerStatEnum.Money:
                if(Money > 0) --Money;
                else return;
                break;
            case PlayerStatEnum.Exp:
                if(Exp > 0) --Exp;
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
        statPoint += Money;
        statPoint += Defence;
        statPoint += CriticalChance;
        statPoint += Exp;
        
        _statPointText.text = $"StatPoint: {statPoint}";

        Atk = 0;
        Health = 0;
        Speed = 0;
        Defence = 0;
        CriticalChance = 0;
        Money = 0;
        Exp = 0;
    }
}
