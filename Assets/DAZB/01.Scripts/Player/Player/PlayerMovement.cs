using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius;
    private Rigidbody2D rigid;
    private Vector2 moveDir;
    private bool isGround;
    private bool isDash;
    private PlayerStats stats;

    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        inputReader.MovementEvent += MovementHandle;
        inputReader.JumpEvent += JumpHandle;
        inputReader.DashEvent += DashHandle;
    }
    
    private void Start() {
        stats = PlayerManager.instance.stats;
    }

    private void FixedUpdate() {
        Move();
        GroundCheck();
    }

    private void Move() {
        if (isDash) return;
        rigid.velocity = new Vector2(moveDir.x * stats.PlayerSpeed, rigid.velocity.y);
    }

    private void MovementHandle(Vector2 value) {
        moveDir = value;
    }

    private void JumpHandle() {
        if (isGround) {
            rigid.AddForce(Vector2.up * stats.JumpPower, ForceMode2D.Impulse);
        }
    }

    private void DashHandle() {
        StartCoroutine(DashRoutine());
    }

    private IEnumerator DashRoutine() {
        if (isDash) yield break;
        isDash = true;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector2 dashDirection = (mousePosition - transform.position).normalized;
        rigid.AddForce(dashDirection * 40, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.05f);
        isDash = false;
    }

    private void GroundCheck() {
        isGround = Physics2D.Raycast(transform.position, Vector2.down, groundCheckRadius, groundLayer);
    }

    private void OnDrawGizmos() {
        Debug.DrawRay(transform.position, Vector2.down * groundCheckRadius, Color.red);
    }
}
