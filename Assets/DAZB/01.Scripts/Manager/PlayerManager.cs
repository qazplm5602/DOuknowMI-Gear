using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public PlayerStat stats;
    public Transform playerTrm;

    private void Awake() {
        instance = this;
    }
}
