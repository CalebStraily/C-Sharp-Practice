using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOneHealth : MonoBehaviour
{
    
    public Slider playerOneSlider;
    [SerializeField] Gradient gradient;
    [SerializeField] Image fill;
    [SerializeField] int maxHealth = 100;
    public int p1CurrentHealth;

    private void Start()
    {
        playerOneSlider.value = maxHealth;
        p1CurrentHealth = maxHealth;
    }
    
    public void SetPlayerOneHealth(int health)
    {
        playerOneSlider.value = health;
        p1CurrentHealth = health;
        
        fill.color = gradient.Evaluate(playerOneSlider.normalizedValue);
    }
}
