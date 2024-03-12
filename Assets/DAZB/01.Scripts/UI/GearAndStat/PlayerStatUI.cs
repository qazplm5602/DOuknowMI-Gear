using UnityEngine;
using TMPro;

public class PlayerStatUI : MonoBehaviour
{
    [SerializeField] private TMP_Text[] texts;
    [SerializeField] private InputReader inputReader;
    private PlayerStats stats;

    private void Awake() {
        inputReader.StatOpenEvent += UpdateStatUI;
    }

    private void Start() {
        stats = PlayerManager.instance.stats;
        UpdateStatUI();
    }

    public void UpdateStatUI() {
        texts[0].text = "HEALTH POINT: " + stats.healthPoint;
        texts[1].text = "ATTACK SPEED: " + stats.AttackSpeed;
        texts[2].text = "MOVE SPEED: " + stats.MoveSpeed;
        texts[3].text = "ATK: " + stats.ATK;
        texts[4].text = "DEFENSE: " + stats.Defense;
        texts[5].text = "JUMP POWER: " + stats.JumpPower;
        texts[6].text = "CRITICAL CHANCE: " + stats.CriticalChance + "%";
        texts[7].text = "MAX DASH COUNT: " + stats.maxDashCount;
    }
}
