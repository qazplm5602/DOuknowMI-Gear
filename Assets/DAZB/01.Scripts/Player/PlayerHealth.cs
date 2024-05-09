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
/*         if (Input.GetKeyDown(KeyCode.T)) {
            ApplyDamage(1, null);
        } */
    }

    public void Healing(int value) {
        currentHealth = Mathf.Clamp(value, 0, maxHealth);
        if ((float)currentHealth / maxHealth > 0.4) {
            CameraManager.Instance.ResetChromaticAberration();
        }
    }

    public void ApplyDamage(int damage, Transform dealer) {
        if(owner.isDead || owner.isInvincibility) return;
        print(damage);
        if (Mathf.RoundToInt(damage - PlayerManager.instance.player.stat.defense.GetValue() * 0.5f) <= 1) {
            damage = 1;
        }
        else {
            damage = Mathf.RoundToInt(damage - PlayerManager.instance.player.stat.defense.GetValue() * 0.5f);
            //print(damage);
        }
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        IngameUIControl.Instance.SetHealthBar(currentHealth, maxHealth);
        if ((float)currentHealth / maxHealth <= 0.4f) {
            CameraManager.Instance.SetChromaticAberration(currentHealth, maxHealth);
        }
        CameraManager.Instance.HitMethod(damage / 15);
        float perlinAmplitude  = Mathf.Clamp(damage * 1.25f, 6, 20);
        CameraManager.Instance.ShakeCamera(perlinAmplitude, 10f, 0.1f);
        owner.StateMachine.ChangeState(PlayerStateEnum.Hurt);
    }
}
