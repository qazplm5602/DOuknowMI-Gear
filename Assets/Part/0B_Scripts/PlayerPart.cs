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

    #if UNITY_EDITOR
        [ContextMenu("50개 추가")]
        void Plus50() => IncreasePart(50);
        [ContextMenu("500개 추가")]
        void Plus500() => IncreasePart(500);
    #endif
}
