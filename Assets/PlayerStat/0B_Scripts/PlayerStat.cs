using TMPro;
using UnityEngine;

public enum PlayerStatEnum {
    Atk, Health, AttackSpeed, MoveSpeed, Money, Exp
}

public class PlayerStat : MonoBehaviour
{
    public int statPoint;
    
    #region Stats

    private int _atk;
    private int _health;
    private int _attackSpeed;
    private int _moveSpeed;
    private int _money;
    private int _exp;

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
    [HideInInspector] public int AttackSpeed {
        get => _attackSpeed;
        set {
            _attackSpeed = value;
            _attackSpeedText.text = _attackSpeed.ToString();
        }
    }
    [HideInInspector] public int MoveSpeed {
        get => _moveSpeed;
        set {
            _moveSpeed = value;
            _moveSpeedText.text = _moveSpeed.ToString();
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
    [SerializeField] private TextMeshProUGUI _atkText;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _attackSpeedText;
    [SerializeField] private TextMeshProUGUI _moveSpeedText;
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _expText;

    [SerializeField] private TextMeshProUGUI _statPointText;

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
            case PlayerStatEnum.AttackSpeed:
                ++AttackSpeed;
                break;
            case PlayerStatEnum.MoveSpeed:
                ++MoveSpeed;
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
            case PlayerStatEnum.AttackSpeed:
                if(AttackSpeed > 0) --AttackSpeed;
                else return;
                break;
            case PlayerStatEnum.MoveSpeed:
                if(MoveSpeed > 0) --MoveSpeed;
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
        statPoint += AttackSpeed;
        statPoint += MoveSpeed;
        statPoint += Money;
        statPoint += Exp;
        
        _statPointText.text = $"StatPoint: {statPoint}";

        Atk = 0;
        Health = 0;
        AttackSpeed = 0;
        MoveSpeed = 0;
        Money = 0;
        Exp = 0;
    }
}
