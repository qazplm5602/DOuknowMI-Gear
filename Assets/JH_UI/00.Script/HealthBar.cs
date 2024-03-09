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
    public void Init(int maxHealth)
    {
        _slider.maxValue = maxHealth;
        _slider.value = maxHealth;
    }
    public void SetHealth(int health)
    {
        _slider.value = health;
        SetGradientValue();
    }
    public void DecreaseHealth(int amount)
    {
        _slider.value -= amount;
        SetGradientValue();
    }

    public void IncreaseHealth(int amount)
    {
        _slider.value += amount;
        SetGradientValue();
    }
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

