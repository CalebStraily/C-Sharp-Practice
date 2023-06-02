using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTwoHealth : MonoBehaviour
{
    
    public Slider playerTwoSlider;
    [SerializeField] Gradient gradient;
    [SerializeField] Image fill;
    [SerializeField] int maxHealth = 100;
    public int p2CurrentHealth;

    private void Start()
    {
        playerTwoSlider.value = maxHealth;
        p2CurrentHealth = maxHealth;
    }

    public void SetPlayerTwoMaxHealth(int health)
    {
        playerTwoSlider.maxValue = health;
        playerTwoSlider.value = health;

        gradient.Evaluate(1f);
    }

    public void SetPlayerTwoHealth(int health)
    {
        playerTwoSlider.value = health;

        fill.color = gradient.Evaluate(playerTwoSlider.normalizedValue);
    }
}
