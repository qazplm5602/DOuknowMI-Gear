using UnityEngine;

public class PlayerPart : MonoBehaviour
{
    public uint Part { get; private set; }

    public void IncreasePart(uint value) {
        Part += value;
    }
}
