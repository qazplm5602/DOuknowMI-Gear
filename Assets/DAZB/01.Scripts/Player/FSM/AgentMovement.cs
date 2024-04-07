using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private float groundCheckRadius;
    private Player agent;

    private Vector2 velocity;
    public Vector2 Velocity => velocity;
    public bool isGround => IsGround();
    private Vector2 movementDir;

    public void Initialize(Agent agent) {
        this.agent = (Player)agent;
/*         inputReader.MovementEvent += HandleMovementEvent; */
    }

    private void FixedUpdate() {
        Move();
    }

    public void SetMovement(Vector3 movement, bool isFlip = false)
    {
        velocity = movement;
        if (isFlip) {
            Flip(movement);
        }
    }

    public void StopImmediately()
    {
        velocity = Vector2.zero;
    }

    public void Flip(Vector2 facingDirection, bool isForce = false, bool isRight = false) {
        if (isForce) {
            if (isRight) {
                agent.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (!isRight) {
                agent.transform.eulerAngles = new Vector3(0, 180, 0);
            }
            return;
        }
        else if (facingDirection.x < 0) {
            agent.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (facingDirection.x > 1) {
            agent.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    public void Knockback(Transform dealer,float power) {
        if (dealer.position.x > agent.transform.position.x) {
            Flip(Vector2.zero, true, true);
            agent.RigidCompo.AddForce(Vector2.left * power, ForceMode2D.Impulse);
            print("ddsdssdsdsdsd1");
        }
        else {
            Flip(Vector2.zero, true, false);
            agent.RigidCompo.AddForce(Vector2.right * power, ForceMode2D.Impulse);
            print("ddsdssdsdsdsd2");
        }
    }

    private void Move() {
        //rigid.velocity = new Vector2(velocity.x, rigid.velocity.y);
        if (agent.isDash) return;
        agent.RigidCompo.velocity = new Vector2(velocity.x, agent.RigidCompo.velocity.y);
    }

/*     private void HandleMovementEvent(Vector2 movement) {
        movementDir = movement;
    } */



    private bool IsGround() {
        if (Physics2D.Raycast(transform.position, Vector2.down, groundCheckRadius, LayerMask.GetMask("Ground"))) {
            return true;
        }
        else {
            return false;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * groundCheckRadius);
    }
}
