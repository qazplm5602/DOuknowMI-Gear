using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    [SerializeField] private int _currentExp = 0;

    public void GetExp(int value) {
        _currentExp += value;
    }
}
