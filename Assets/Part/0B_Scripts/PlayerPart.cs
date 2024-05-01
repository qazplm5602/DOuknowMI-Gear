using UnityEngine;

public class PlayerPart : MonoBehaviour
{
    public int Part { get; private set; }

    public void InitPart(int value) {
        Part = value;
        IngameUIControl.Instance.SetCoin(Part);
    }

    public void IncreasePart(int value) {
        Part += value;
        IngameUIControl.Instance.SetCoin(Part);
    }

    public bool TryPayPart(int value) {
        if (!CheckPart(value)) return false;
        
        Part -= value;
        IngameUIControl.Instance.SetCoin(Part);
        return true;
    }

    // 결제 ㄱㄴ?
    public bool CheckPart(int value) {
        return Part >= value;
    }

    #if UNITY_EDITOR
        [ContextMenu("50개 추가")]
        void Plus50() => IncreasePart(50);
        [ContextMenu("500개 추가")]
        void Plus500() => IncreasePart(500);
    #endif
}
