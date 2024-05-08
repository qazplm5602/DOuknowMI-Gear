using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public int maxHealth;
    [SerializeField] private int currentHealth;
    
    public event Action OnDead;

    private Player owner;

    private void Awake() {
        owner = GetComponent<Player>();
    }

    private void Start() {
        maxHealth = (int)owner.stat.maxHealth.GetValue();
        currentHealth = maxHealth;
        IngameUIControl.Instance.SetHealthBar(currentHealth, maxHealth);
    }

    private void Update() {
        if(currentHealth == 0 && !owner.isDead) {
            owner.isDead = true;
            OnDead?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.T)) {
            ApplyDamage(100, null);
        }
    }

    public void ApplyDamage(int damage, Transform dealer) {
        if(owner.isDead || owner.isInvincibility) return;
        if (Mathf.RoundToInt(damage * PlayerManager.instance.player.stat.defense.GetValue() * 0.5f) >= 1) {
            damage = 1;
        }
        else {
            damage = Mathf.RoundToInt(damage * PlayerManager.instance.player.stat.defense.GetValue() * 0.5f);
        }
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        IngameUIControl.Instance.SetHealthBar(currentHealth, maxHealth);
        owner.StateMachine.ChangeState(PlayerStateEnum.Hurt);
    }
}
