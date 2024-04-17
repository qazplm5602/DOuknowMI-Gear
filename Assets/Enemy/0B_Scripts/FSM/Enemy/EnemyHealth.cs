using System;
using UnityEngine;
using UnityEngine.UI;
using FSM;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public event Action OnDead;

    [SerializeField] private int _currentHealth;
    private int _maxHealth;

    [SerializeField] private Material _whiteMat;
    private Material _originMat;

    public Image healthFilled;

    private Transform _playerTrm;
    private Enemy _owner;

    private void Start() {
        _playerTrm = PlayerManager.instance.playerTrm;
    }

    public void SetOwner(Enemy owner) {
        _owner = owner;

        _maxHealth = owner.Stat.maxHealth.GetValue();
        _currentHealth = _maxHealth;
        _originMat = _owner.SpriteRendererCompo.material;
    }

    public void ApplyDamage(int damage, Transform dealer) {
        if(_owner.isDead) return;

        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maxHealth);
        healthFilled.fillAmount = (float)_currentHealth / _maxHealth;

        ShowDamageText(damage);

        if(_currentHealth == 0) {
            _owner.isDead = true;

            dealer.GetComponent<PlayerExperience>().GetExp(_owner.dropTable.experience);
            OnDead?.Invoke();
        }
        else Blink();
    }

    private void ShowDamageText(int damage) {
        DamageText damageText = PoolManager.Instance.Pop(PoolingType.DamageText) as DamageText;
        damageText.transform.position = new Vector3(transform.position.x, transform.position.y, -1f);
        damageText.Init(damage);
    }

    private void Blink() {
        _owner.SpriteRendererCompo.material = _whiteMat;
        healthFilled.material = _whiteMat;
        _owner.StartDelayCallback(0.1f, () => {
            _owner.SpriteRendererCompo.material = _originMat;
            healthFilled.material = null;
        });
    }

    //test
    private void Update() {
        if(_playerTrm != null && Input.GetKeyDown(KeyCode.P)) {
            _owner.HealthCompo.ApplyDamage(10, _playerTrm);
        }
    }
}
