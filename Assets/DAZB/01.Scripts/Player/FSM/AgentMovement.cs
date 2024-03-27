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

    public void SetMovement(Vector3 movement)
    {
        velocity = movement;
    }

    public void StopImmediately()
    {
        velocity = Vector2.zero;
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
