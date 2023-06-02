using UnityEngine;
using UnityEngine.UI;

public class PlayerTwoGutsBar : MonoBehaviour
{
    [SerializeField] float delayAmount = 0.5f;
    [SerializeField] int maxGuts = 100;
    [SerializeField] public int p2CurrentGuts;
    
    [SerializeField] GameObject gameManager;
    [SerializeField] GameObject pTwoGutsBar;
   
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    
    private void Start()
    {
        p2CurrentGuts = 0;
    }

    public void SetPlayerTwoMinGuts (int guts)
    {
        slider.minValue = guts;
        slider.value = guts; 
    }

    public void SetPlayerTwoGuts (int guts)
    {
        slider.value = guts;
    }

    protected float Timer;

    private static float distance;

    void Update()
    {
        Timer += Time.deltaTime;

        if (Timer >= delayAmount && slider.value < 100)
        {
            Timer = 0f;
            slider.value++;
            p2CurrentGuts++;
        }
    }
    public void p2TakeGutsDamage(int damage)
    {
        slider.value -= damage;
        p2CurrentGuts -= damage;
    }
}

