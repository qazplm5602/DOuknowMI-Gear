using System;
using System.Collections;
using UnityEngine;

public abstract class Agent : MonoBehaviour
{
    public Animator AnimatorCompo {get; protected set;}
    public AgentMovement MovementCompo {get; protected set;}
    public Rigidbody2D RigidCompo {get; protected set;}
    public bool CanStateChangeable { get; protected set; } = true;
    

    protected virtual void Awake()
    {
        AnimatorCompo = GetComponent<Animator>();
        MovementCompo = GetComponent<AgentMovement>();
        RigidCompo = GetComponent<Rigidbody2D>();
        MovementCompo.Initialize(this);
    }
        
    #region Delay callback coroutine
    public Coroutine StartDelayCallback(float delayTime, Action Callback)
    {
        return StartCoroutine(DelayCoroutine(delayTime, Callback));
    }

    protected IEnumerator DelayCoroutine(float delayTime, Action Callback)
    {
        yield return new WaitForSeconds(delayTime);
        Callback?.Invoke();
    }
    #endregion

}
