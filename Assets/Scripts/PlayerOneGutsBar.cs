using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class PlayerOneGutsBar : MonoBehaviour
{
    [SerializeField] float delayAmount = 0.5f;
    [SerializeField] int maxGuts = 100;
    [SerializeField] public int p1CurrentGuts;
   
    [SerializeField] GameObject gameManager;
    [SerializeField] GameObject pOneGutsBar;

    public Slider p1GutsSlider;
    public Gradient gradient;
    public Image fill;

    private void Start()
    {
        p1CurrentGuts = 0;
    }

    public void SetPlayerOneMinGuts (int guts)
    {
        p1GutsSlider.minValue = guts;
        p1GutsSlider.value = guts; 
    }

    public void SetPlayerOneGuts (int guts)
    {
        p1GutsSlider.value = guts;
    }

    protected float Timer;

    private static float distance;

    void Update()
    {
        Timer += Time.deltaTime;

        if (Timer >= delayAmount && p1GutsSlider.value < 100)
        {
            Timer = 0f;
            p1GutsSlider.value++;
            p1CurrentGuts++;
        }
    }
    public void p1TakeGutsDamage(int damage)
    {
        p1GutsSlider.value -= damage;
        p1CurrentGuts -= damage;
    }
}

