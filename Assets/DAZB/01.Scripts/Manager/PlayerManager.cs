using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public PlayerStats stats;
    public Transform playerTrm;

    private void Awake() {
        instance = this;
    }
}
