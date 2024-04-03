using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public int maxHealth;
    [SerializeField] private int currentHealth;
    
    public event Action OnDead;

    private Player owner;
    private PlayerStat stats;

    private void Awake() {
        owner = GetComponent<Player>();
        stats = GetComponent<PlayerStat>();
    }

    private void Start() {
        maxHealth = stats.defaultHealth;
        currentHealth = maxHealth;
    }

    public void ApplyDamage(int damage, Transform dealer) {
        if(owner.isDead) return;
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        if(currentHealth == 0) {
            owner.isDead = true;
            OnDead?.Invoke();
        }
    }
}
