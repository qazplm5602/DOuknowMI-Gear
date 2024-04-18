using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public PlayerStat stats;
    public Transform playerTrm;
    public Player player;
    public SpriteRenderer playerSpriteRenderer;

    private void Awake() {
        instance = this;
    }
}
