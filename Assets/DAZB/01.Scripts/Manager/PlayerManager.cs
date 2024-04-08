using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public PlayerStat stats;
    public Transform playerTrm;
    public Player player;

    private void Awake() {
        instance = this;
    }
}
