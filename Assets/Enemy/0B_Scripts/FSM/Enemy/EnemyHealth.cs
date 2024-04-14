using System;
using UnityEngine;
using FSM;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public event Action OnDead;

    [SerializeField] private int _currentHealth;
    private int _maxHealth;

    private Transform _playerTrm;
    private Enemy _owner;

    private void Start() {
        _playerTrm = PlayerManager.instance.playerTrm;
    }

    public void SetOwner(Enemy owner) {
        _owner = owner;

        _maxHealth = owner.Stat.maxHealth.GetValue();
        _currentHealth = _maxHealth;
    }

    public void ApplyDamage(int damage, Transform dealer) {
        if(_owner.isDead) return;

        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maxHealth);

        if(_currentHealth == 0) {
            _owner.isDead = true;

            dealer.GetComponent<PlayerExperience>().GetExp(_owner.dropTable.experience);
            OnDead?.Invoke();
        }
    }

    //test
    private void Update() {
        if(_playerTrm != null && Input.GetKeyDown(KeyCode.P)) {
            _owner.HealthCompo.ApplyDamage(10, _playerTrm);
        }
    }
}
