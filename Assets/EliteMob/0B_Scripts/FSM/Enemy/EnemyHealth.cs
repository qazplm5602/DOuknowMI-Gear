using UnityEngine;
using FSM;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public int maxHealth;
    [SerializeField] private int _currentHealth;

    private Entity _owner;

    private void Start() {
        _currentHealth = maxHealth;
    }

    public void SetOwner(Entity owner) {
        _owner = owner;

        _currentHealth = maxHealth;
    }

    public void ApplyDamage(int damage) {
        if(_owner.isDead) return;

        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, maxHealth);

        if(_currentHealth == 0) {
            _owner.isDead = true;
        }
    }
}
