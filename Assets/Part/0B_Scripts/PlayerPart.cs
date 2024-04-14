using UnityEngine;

public class PlayerPart : MonoBehaviour
{
    public uint Part { get; private set; }

    public void IncreasePart(uint value) {
        Part += value;
        IngameUIControl.Instance.SetCoin((int)Part);
    }

    public bool TryPayPart(uint value) {
        if (!CheckPart(value)) return false;
        
        Part -= value;
        return true;
    }

    // 결제 ㄱㄴ?
    public bool CheckPart(uint value) {
        return Part >= value;
    }
}
