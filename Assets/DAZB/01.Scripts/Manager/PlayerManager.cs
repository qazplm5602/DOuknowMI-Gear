using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public PlayerStats stats;

    private void Awake() {
        instance = this;
    }
}
