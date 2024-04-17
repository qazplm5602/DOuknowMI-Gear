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

    private void Awake() {
        owner = GetComponent<Player>();
        print(owner);
    }

    private void Start() {
        maxHealth = owner.stat.maxHealth.GetValue();
        currentHealth = maxHealth;
    }

    private void Update() {
        if(currentHealth == 0 && !owner.isDead) {
            owner.isDead = true;
            OnDead?.Invoke();
        }
    }

    public void ApplyDamage(int damage, Transform dealer) {
        if(owner.isDead || owner.isInvincibility) return;
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        owner.MovementCompo.Knockback(dealer, 10f);
/*         if(currentHealth == 0) {
            owner.isDead = true;
            OnDead?.Invoke();
        } */
        owner.StateMachine.ChangeState(PlayerStateEnum.Hurt);
    }
}
