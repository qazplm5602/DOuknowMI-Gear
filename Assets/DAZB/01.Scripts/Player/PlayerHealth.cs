using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private Transform test;
    
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

    private void Update() {
        if (Keyboard.current.kKey.wasPressedThisFrame) {
            ApplyDamage(1, test);
        }
    }

    public void ApplyDamage(int damage, Transform dealer) {
        if(owner.isDead || owner.isInvincibility) return;
        owner.StateMachine.ChangeState(PlayerStateEnum.Hurt);
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        owner.MovementCompo.Knockback(dealer, 10f);
        if(currentHealth == 0) {
            owner.isDead = true;
            OnDead?.Invoke();
        }
    }
}
