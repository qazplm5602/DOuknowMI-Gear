using System;
using UnityEngine;
using FSM;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private Transform Player;

    public int maxHealth;
    [SerializeField] private int _currentHealth;
    
    public event Action OnDead;

    private Enemy _owner;

    private void Start() {
        _currentHealth = maxHealth;
    }

    public void SetOwner(Enemy owner) {
        _owner = owner;

        _currentHealth = maxHealth;
    }

    public void ApplyDamage(int damage, Transform dealer) {
        if(_owner.isDead) return;

        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, maxHealth);

        if(_currentHealth == 0) {
            _owner.isDead = true;

            dealer.GetComponent<PlayerExperience>().GetExp(_owner.dropTable.experience);
            OnDead?.Invoke();
        }
    }

    //test
    private void Update() {
        if(Player != null && Input.GetKeyDown(KeyCode.P)) {
            ApplyDamage(10, Player);
        }
    }
}
