using System;
using System.Collections;
using UnityEngine;

public abstract class Agent : MonoBehaviour
{
    public Animator AnimatorCompo {get; protected set;}
    public AgentMovement MovementCompo {get; protected set;}
    public Rigidbody2D RigidCompo {get; protected set;}
    public PlayerHealth HealthCompo {get; protected set;}
    public bool CanStateChangeable { get; protected set; } = true;
    [HideInInspector] public bool isDead = false;
    [HideInInspector] public bool ishurt = false;
    [HideInInspector] public bool isInvincibility = false;
    public AgentStat stat;

    protected virtual void Awake()
    {
        AnimatorCompo = GetComponent<Animator>();
        MovementCompo = GetComponent<AgentMovement>();
        RigidCompo = GetComponent<Rigidbody2D>();
        HealthCompo = GetComponent<PlayerHealth>();
        MovementCompo.Initialize(this);
        if (HealthCompo != null)
            HealthCompo.OnDead += HandleDeadEvent;
    }

    private void OnDestroy() {
        HealthCompo.OnDead -= HandleDeadEvent;
    }
        
    #region Delay callback coroutine
    public Coroutine StartDelayCallback(float delayTime, Action Callback)
    {
        return StartCoroutine(DelayCoroutine(delayTime, Callback));
    }

    public abstract void HandleDeadEvent();

    protected IEnumerator DelayCoroutine(float delayTime, Action Callback)
    {
        yield return new WaitForSeconds(delayTime);
        Callback?.Invoke();
    }
    #endregion

}
