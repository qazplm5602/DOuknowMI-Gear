using System;
using TMPro;
using UnityEngine;

public enum PlayerStatEnum {
    Atk, Health, Speed, Defense, Money, Exp
}

public class PlayerStat : MonoBehaviour
{
    public int statPoint;
    
    #region Stats

    [Header("default")]
    public int defaultAtk;
    public int defaultHealth;
    public int defaultSpeed;
    public int defaultDefense;

    [Header("point per increas value")]
    public float atkPerPointInc;
    public float healthPerPointInc;
    public float speedPerPointInc;
    public float defensePerPointInc;

    public Action OnUpdateStat;

    private int _atk;
    private int _health;
    private int _defense;
    private int _speed;
    private int _money;
    private int _exp;

    // current stat
    private float currentHealth;
    private float currentSpeed;
    private float currentDefense;
    private float currentAtk;

    [HideInInspector] public int Atk {
        get => _atk;
        set {
            _atk = value;
            _atkText.text = _atk.ToString();
        }
    }
    [HideInInspector] public int Health {
        get => _health;
        set {
            _health = value;
            _healthText.text = _health.ToString();
        }
    }
    [HideInInspector] public int Defense {
        get => _defense;
        set {
            _defense = value;
            _defenseText.text = _defense.ToString();
        }
    }
        [HideInInspector] public int Speed {
        get => _speed;
        set {
            _speed = value;
            _speedText.text = _defense.ToString();
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
    public TextMeshProUGUI _defenseText;
    public TextMeshProUGUI _speedText;
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
            case PlayerStatEnum.Money:
                ++Money;
                break;
            case PlayerStatEnum.Defense:
                ++Defense;
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
            case PlayerStatEnum.Defense:
                if(Defense > 0) --Defense;
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
        statPoint += Defense;
        statPoint += Money;
        statPoint += Exp;
        
        _statPointText.text = $"StatPoint: {statPoint}";

        Atk = 0;
        Health = 0;
        Speed = 0;
        Defense = 0;
        Money = 0;
        Exp = 0;
    }
}
