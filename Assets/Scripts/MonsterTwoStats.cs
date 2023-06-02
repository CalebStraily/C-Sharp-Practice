using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTwoStats : MonoBehaviour
{
    //string monsterName = "Ron";

    [SerializeField] GameObject player;
    [SerializeField] GameObject monsterOneStats;
    [SerializeField] GameObject gameManager;

    public int monsterTwoSpeed = 400;
    public int monsterTwoAccuracy = 400;
    
    public float hitChance;
    public bool moveHits;
    public bool moveCrits;
    public bool playerTwoIsCountering = false;

    void Start()
    {
        moveHits = false;
        moveCrits = false;
    }

    void Update()
    {
       
    }
    public void SetPlayerTwoHitChance()
    {
        float rankModifier = player.GetComponent<PlayerTwo>().rankModifier;
        int monsterOneSpeed = monsterOneStats.GetComponent<MonsterOneStats>().monsterOneSpeed;
        int adjustedMonsterTwoAccuracy;


        adjustedMonsterTwoAccuracy = ((monsterTwoAccuracy / 10) / 4);
        hitChance = ((monsterOneSpeed / 10) + rankModifier + adjustedMonsterTwoAccuracy);
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
