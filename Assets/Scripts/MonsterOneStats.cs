using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterOneStats : MonoBehaviour
{
    //string monsterName = "Dat Boi";
    
    [SerializeField] GameObject player;
    [SerializeField] GameObject monsterTwoStats;
    [SerializeField] GameObject gameManager;

    public int monsterOneSpeed = 300;
    public static int monsterOneAccuracy = 400;
    
    public float hitChance;
    public bool moveHits = false;
    public bool moveCrits;
    public bool playerOneIsCountering = false;


    void Start()
    {
        moveHits = false;
        moveCrits = false;
    }

    void Update()
    {
      
    }

    public void SetPlayerOneHitChance()
    {
        float rankModifier = player.GetComponent<PlayerOne>().rankModifier;
        int monsterTwoSpeed = monsterTwoStats.GetComponent<MonsterTwoStats>().monsterTwoSpeed;
        int adjustedMonsterOneAccuracy;

        adjustedMonsterOneAccuracy = ((monsterOneAccuracy / 10) / 4);
        hitChance = ((monsterTwoSpeed / 10) + rankModifier + adjustedMonsterOneAccuracy);
        Debug.Log("Hit Chance is " + hitChance);
        RollForMiss();
    }

    public void RollForMiss()
    {
        int RandomInt = Random.Range(1, 100);
        Debug.Log("The Roll for Miss was " + RandomInt);

        if (RandomInt <= hitChance)
        {
            moveHits = true;
            
            Debug.Log("Move Hits!");
        }
        else if (RandomInt > hitChance)
        {
            moveHits = false;
            
            Debug.Log("Move Misses!");
        }
    }



}
