using UnityEngine;

[System.Obsolete] // ㄹㅇ 이거 똥 코드니 사용중이면 즉시 중단하사길.
public class PlayerPart : MonoBehaviour
{
    public uint Part { get; private set; }

    public void IncreasePart(uint value) {
        Part += value;
    }
}
