using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Vector2 groundCheckSize;
    [SerializeField] private Vector3 offset;
    [SerializeField] private LayerMask groundLayer;
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
        if (facingDirection.x < 0) {
            agent.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (facingDirection.x > 0) {
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
        if (agent.isDash || agent.isDead || agent.ishurt) return;
        agent.RigidCompo.velocity = new Vector2(velocity.x, agent.RigidCompo.velocity.y);
    }

/*     private void HandleMovementEvent(Vector2 movement) {
        movementDir = movement;
    } */



    private bool IsGround() {
        if (Physics2D.OverlapBox(transform.position + offset, groundCheckSize, 0, groundLayer)) {
            return true;
        }
        else {
            return false;
        }
    }

    public bool CanUnderJump() {
        Collider2D hit = Physics2D.OverlapBox(transform.position  + offset, groundCheckSize, 0, groundLayer);
        if (hit != null) {
            print(hit.gameObject.layer);
            if (hit.gameObject.layer == LayerMask.NameToLayer("Platform")) {
                print("아래점프 가느,ㅇ");
                return true;
            }
            else {
                print("아래점프 bool가느,ㅇ");
                return false;
            }
        }
        return false;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + offset, groundCheckSize);
    }
}
