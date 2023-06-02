using UnityEngine;
using UnityEngine.UI;

public class AttackAndDamage : MonoBehaviour
{
    [SerializeField] GameObject playerOneHealthBarSlider;
    [SerializeField] GameObject playerTwoHealthBarSlider;

    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;

    void Start()
    {
        
    }

    void Update()
    {
       
    }

    public void TakePlayerOneDamage(int damage)
    {
        playerOneHealthBarSlider.GetComponent<Slider>().value -= damage;
        playerOneHealthBarSlider.GetComponent<PlayerOneHealth>().p1CurrentHealth -= damage;
    }

    public void TakePlayerTwoDamage(int damage)
    {
        playerTwoHealthBarSlider.GetComponent<Slider>().value -= damage;
        playerTwoHealthBarSlider.GetComponent<PlayerTwoHealth>().p2CurrentHealth -= damage;
    }
}
