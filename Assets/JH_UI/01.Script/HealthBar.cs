using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Gradient _gradient;
    private Slider _slider;
    private Image _fill;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _fill = GetComponentInChildren<Image>();
        _fill.color = _gradient.Evaluate(1f);
        Init(100);
    }
    /// <summary>
    /// Initialize HealthBar slider value to maxHealth
    /// </summary>
    /// <param name="maxHealth">최대 체력</param>
    public void Init(int maxHealth)
    {
        _slider.maxValue = maxHealth;
        _slider.value = maxHealth;
    }

    /// <summary>
    /// Set healthBar slider value to health
    /// </summary>
    /// <param name="health">체력</param>
    public void SetHealth(int health)
    {
        _slider.value = health;
        SetGradientValue();
    }

    /// <summary>
    /// Decrease healthBar slider value to amount
    /// </summary>
    /// <param name="amount">값</param>
    public void DecreaseHealth(int amount)
    {
        _slider.value -= amount;
        SetGradientValue();
    }

    /// <summary>
    /// Increase healthBar slider value to amount
    /// </summary>
    /// <param name="amount">값</param>
    public void IncreaseHealth(int amount)
    {
        _slider.value += amount;
        SetGradientValue();
    }

    /// <summary>
    /// Set slider's color to the slider's normalizedvalue
    /// </summary>
    public void SetGradientValue()
    {
        _fill.color = _gradient.Evaluate(_slider.normalizedValue);
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) IncreaseHealth(10);
        if (Input.GetKeyDown(KeyCode.R)) DecreaseHealth(10);
    }
#endif
}

