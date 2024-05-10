using UnityEngine;
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public PlayerStat stats;
    public Transform playerTrm;
    public Player player;
    public PlayerExperience playerExperience;
    public PlayerPart playerPart;
    public SpriteRenderer playerSpriteRenderer;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        if(player != null) {
            playerExperience = player.GetComponent<PlayerExperience>();
            playerPart = player.GetComponent<PlayerPart>();
        }
    }
}
