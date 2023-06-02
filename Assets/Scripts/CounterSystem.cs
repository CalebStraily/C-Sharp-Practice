using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CounterSystem : MonoBehaviour
{

    public float timeValue;
    public Slider slider;

    public int playerOneInput;
    public int playerTwoInput;

    
    public bool playerOneMoveHits;
    public bool playerTwoMoveHits;
    public bool moveCountered;

    [SerializeField] GameObject counterTimer;
    [SerializeField] GameObject gameManager;
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;

    // Start is called before the first frame update
    void Start()
    {
        counterTimer.SetActive(false);

        playerOneMoveHits = gameManager.GetComponent<MonsterOneStats>().moveHits;
        playerTwoMoveHits = gameManager.GetComponent<MonsterTwoStats>().moveHits;
        moveCountered = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Cheat button to start counter system.
        if(Input.GetKeyDown(KeyCode.Z))
        {
            toggleScriptOn();
        }

        if (timeValue > 0)
        {
            timeValue -= Time.deltaTime;
            slider.value -= Time.deltaTime;
            
            if (gameManager.GetComponent<MonsterOneStats>().playerOneIsCountering == true && (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3)))
            {
                Debug.Log("Player one is attempting to counter!");
                PauseTime();

                if(gameManager.GetComponent<MonsterOneStats>().playerOneIsCountering == true)
                {
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        Debug.Log("P1 tried to counter with 1 key!");
                        player1.GetComponent<PlayerOne>().damageValue = 20;
                        playerOneInput = 8;
                        playerOneCounterCalculations();
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        Debug.Log("P1 tried to counter with 2 key!");
                        player1.GetComponent<PlayerOne>().damageValue = 40;
                        playerOneInput = 9;
                        playerOneCounterCalculations();
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        Debug.Log("P1 tried to counter with 3 key!");
                        player1.GetComponent<PlayerOne>().damageValue = 60;
                        playerOneInput = 0;
                        playerOneCounterCalculations();
                    }
                }

                gameManager.GetComponent<MonsterOneStats>().playerOneIsCountering = false;
                player1.GetComponent<PlayerOne>().playerOneInitiatedCounter = false;
                
                ResumeTime();
                toggleScriptOff();
                return;
            }
            else if (gameManager.GetComponent<MonsterTwoStats>().playerTwoIsCountering == true && (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Alpha0)))
            {
                Debug.Log("Player two is attempting to counter!");
                PauseTime();

                if (gameManager.GetComponent<MonsterTwoStats>().playerTwoIsCountering == true)
                {
                    if (Input.GetKeyDown(KeyCode.Alpha8))
                    {
                        Debug.Log("P2 tried to counter with 8 key!");
                        player2.GetComponent<PlayerTwo>().damageValue = 30;
                        playerTwoInput = 1;
                        playerTwoCounterCalculations();
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha9))
                    {
                        Debug.Log("P2 tried to counter with 9 key!");
                        player2.GetComponent<PlayerTwo>().damageValue = 50;
                        playerTwoInput = 2;
                        playerTwoCounterCalculations();
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha0))
                    {
                        Debug.Log("P2 tried to counter with 0 key!");
                        player2.GetComponent<PlayerTwo>().damageValue = 60;
                        playerTwoInput = 3;
                        playerTwoCounterCalculations();
                    }
                }

                gameManager.GetComponent<MonsterTwoStats>().playerTwoIsCountering = false;
                player1.GetComponent<PlayerOne>().playerOneInitiatedCounter = false;

                ResumeTime();
                toggleScriptOff();

                return;
            }
        }
        else if(timeValue <= 0)
        {
            timeValue = 0;

            TimeoutCalculation();

            gameManager.GetComponent<MonsterOneStats>().playerOneIsCountering = false;
            gameManager.GetComponent<MonsterTwoStats>().playerTwoIsCountering = false;
            player1.GetComponent<PlayerOne>().playerOneInitiatedCounter = false;
            player2.GetComponent<PlayerTwo>().playerTwoInitiatedCounter = false;

            toggleScriptOff();
        }
        
    }

    public void toggleScriptOff()
    {   
        counterTimer.SetActive(false);
        return;
    }

    public void toggleScriptOn()
    {
        counterTimer.SetActive(true);
        timeValue = 0.5f;
        slider.value = 0.5f;
        return;
    }

    public void PauseTime()
    {
        //player1.GetComponent<Player>().isP1InputEnabled = false;
        //player2.GetComponent<Enemy>().isP2InputEnabled = false;

        Debug.Log("Physics should be paused.");

        Time.timeScale = 0f;
        return;
    }

    public void ResumeTime()
    {
        //player1.GetComponent<Player>().isP1InputEnabled = true;
        //player2.GetComponent<Enemy>().isP2InputEnabled = true;

        Debug.Log("Physics should be resumed.");

        Time.timeScale = 1f;
        return;
    }

    public void playerOneCounterCalculations()
    {
        if (playerOneInput == player2.GetComponent<PlayerTwo>().playerTwoLastPressed)
        {
            playerOneMoveHits = true;
            playerTwoMoveHits = false;
            moveCountered = playerOneMoveHits;

            Debug.Log("Player One Counter Success!");
        }
        else
        {
            playerOneMoveHits = false;
            playerTwoMoveHits = true;
            moveCountered = playerOneMoveHits;

            Debug.Log("Player One Counter Failed!");
        }

        if (moveCountered == false)
        {
            Debug.Log("Player One failed to counter and is taking damage.");

            int randInt = Random.Range(1, 100);

            Debug.Log("P1 Failed to Counter (P2's roll for crit: " + randInt + ".");

            gameManager.GetComponent<MonsterTwoStats>().SetPlayerTwoHitChance();

            if (gameManager.GetComponent<MonsterTwoStats>().moveHits == true)
            {
                if (randInt < player2.GetComponent<PlayerTwo>().playerTwoCritChance)
                {
                    int critApply = (player2.GetComponent<PlayerTwo>().damageValue + (100 / player2.GetComponent<PlayerTwo>().playerTwoCritChance));
                    gameManager.GetComponent<AttackAndDamage>().TakePlayerOneDamage(critApply);
                    Debug.Log("Critical Hit for " + critApply + ".");
                }
                else
                {
                    gameManager.GetComponent<AttackAndDamage>().TakePlayerOneDamage(player2.GetComponent<PlayerTwo>().damageValue);
                    Debug.Log("Did not Crit. Did " + player2.GetComponent<PlayerTwo>().damageValue + " Damage.");
                }
            }
            else if (gameManager.GetComponent<MonsterTwoStats>().moveHits == false)
            {
                gameManager.GetComponent<AttackAndDamage>().TakePlayerOneDamage(0);
            }
        }
        else if (moveCountered == true)
        {
            Debug.Log("Player Two's Move got Countered and Dealt 0 Damage.");

            gameManager.GetComponent<AttackAndDamage>().TakePlayerOneDamage(0);
            //Add animation and/or ui feedback here for miss.

            int randInt = Random.Range(1, 100);

            Debug.Log("P1 Managed to Counter (P1's roll for crit: " + randInt + ".");

            gameManager.GetComponent<MonsterOneStats>().SetPlayerOneHitChance();

            if (gameManager.GetComponent<MonsterOneStats>().moveHits == true)
            {
                if (randInt < player1.GetComponent<PlayerOne>().playerOneCritChance)
                {
                    int critApply = (player1.GetComponent<PlayerOne>().damageValue + (100 / player1.GetComponent<PlayerOne>().playerOneCritChance));
                    gameManager.GetComponent<AttackAndDamage>().TakePlayerTwoDamage(critApply);
                    Debug.Log("Critical Hit for " + critApply + ".");
                }
                else
                {
                    gameManager.GetComponent<AttackAndDamage>().TakePlayerTwoDamage(player1.GetComponent<PlayerOne>().damageValue);
                    Debug.Log("Did not Crit. Did " + player1.GetComponent<PlayerOne>().damageValue + " Damage.");
                }
            }
            else if  (gameManager.GetComponent<MonsterOneStats>().moveHits == false)
            {
                gameManager.GetComponent<AttackAndDamage>().TakePlayerTwoDamage(0);
            }
        }

        moveCountered = false;
        playerOneMoveHits = false;
        playerTwoMoveHits = false;

        return;
    }

    public void TimeoutCalculation()
    {
        if (gameManager.GetComponent<MonsterTwoStats>().playerTwoIsCountering == true)
        {
            Debug.Log("Player Two failed to counter and is taking damage.");

            int randInt = Random.Range(1, 100);

            Debug.Log("P2 Failed to Counter (P1's roll for crit: " + randInt + ".");

            gameManager.GetComponent<MonsterOneStats>().SetPlayerOneHitChance();

            if (gameManager.GetComponent<MonsterOneStats>().moveHits == true)
            {
                if (randInt < player1.GetComponent<PlayerOne>().playerOneCritChance)
                {
                    int critApply = (player1.GetComponent<PlayerOne>().damageValue + (100 / player1.GetComponent<PlayerOne>().playerOneCritChance));
                    gameManager.GetComponent<AttackAndDamage>().TakePlayerTwoDamage(critApply);

                    Debug.Log("Critical Hit for " + critApply + ".");
                }
                else
                {
                    gameManager.GetComponent<AttackAndDamage>().TakePlayerTwoDamage(player1.GetComponent<PlayerOne>().damageValue);
                    Debug.Log("Did not Crit. Did " + player1.GetComponent<PlayerOne>().damageValue + " Damage.");
                }
            }
            else if (gameManager.GetComponent<MonsterOneStats>().moveHits == false)
            {
                gameManager.GetComponent<AttackAndDamage>().TakePlayerTwoDamage(0);
            }
        }
        else if (gameManager.GetComponent<MonsterOneStats>().playerOneIsCountering == true)
        {
            Debug.Log("Player One failed to counter and is taking damage.");

            int randInt = Random.Range(1, 100);

            Debug.Log("P1 Failed to Counter (P2's roll for crit: " + randInt + ".");

            gameManager.GetComponent<MonsterTwoStats>().SetPlayerTwoHitChance();

            if (gameManager.GetComponent<MonsterTwoStats>().moveHits == true)
            {
                if (randInt < player2.GetComponent<PlayerTwo>().playerTwoCritChance)
                {
                    int critApply = (player2.GetComponent<PlayerTwo>().damageValue + (100 / player2.GetComponent<PlayerTwo>().playerTwoCritChance));
                    gameManager.GetComponent<AttackAndDamage>().TakePlayerOneDamage(critApply);
                    Debug.Log("Critical Hit for " + critApply + ".");
                }
                else
                {
                    gameManager.GetComponent<AttackAndDamage>().TakePlayerOneDamage(player2.GetComponent<PlayerTwo>().damageValue);
                    Debug.Log("Did not Crit. Did " + player2.GetComponent<PlayerTwo>().damageValue + " Damage.");
                }
            }
            else if (gameManager.GetComponent<MonsterTwoStats>().moveHits == false)
            {
                gameManager.GetComponent<AttackAndDamage>().TakePlayerOneDamage(0);
            }
        }
    }

    public void playerTwoCounterCalculations()
    { 
        if (playerTwoInput == player1.GetComponent<PlayerOne>().playerOneLastPressed)
        {
            playerTwoMoveHits = true;
            playerOneMoveHits = false;
            moveCountered = playerTwoMoveHits;

            Debug.Log("Player Two Counter Success!");
        }
        else
        {
            playerOneMoveHits = true;
            playerTwoMoveHits = false;
            moveCountered = playerTwoMoveHits;

            Debug.Log("Player Two Counter Failed!");
        }

        if (moveCountered == false)
        {
            Debug.Log("Player Two failed to counter and is taking damage.");

            int randInt = Random.Range(1, 100);

            Debug.Log("P2 Failed to Counter (P1's roll for crit: " + randInt + ".).");

            gameManager.GetComponent<MonsterOneStats>().SetPlayerOneHitChance();

            if (gameManager.GetComponent<MonsterOneStats>().moveHits == true)
            {
                if (randInt < player1.GetComponent<PlayerOne>().playerOneCritChance)
                {
                    int critApply = (player1.GetComponent<PlayerOne>().damageValue + (100 / player1.GetComponent<PlayerOne>().playerOneCritChance));
                    gameManager.GetComponent<AttackAndDamage>().TakePlayerTwoDamage(critApply);
                    Debug.Log("Critical Hit for " + critApply + ".");
                }
                else
                {
                    gameManager.GetComponent<AttackAndDamage>().TakePlayerTwoDamage(player1.GetComponent<PlayerOne>().damageValue);
                    Debug.Log("Did not Crit. Did " + player1.GetComponent<PlayerOne>().damageValue + " Damage.");
                }
            }
            else if (gameManager.GetComponent<MonsterOneStats>().moveHits == false)
            {
                gameManager.GetComponent<AttackAndDamage>().TakePlayerTwoDamage(0);
            }
        }
        else if (moveCountered == true)
        {
            Debug.Log("Player One's's Move got Countered and Dealt 0 Damage.");
            
            gameManager.GetComponent<AttackAndDamage>().TakePlayerTwoDamage(0);
            //Add animation and/or ui feedback here for miss.
            
            int randInt = Random.Range(1, 100);

            Debug.Log("P2 Managed to Counter (P2's roll for crit: " + randInt + ".).");

            gameManager.GetComponent<MonsterTwoStats>().SetPlayerTwoHitChance();

            if (gameManager.GetComponent<MonsterTwoStats>().moveHits == true)
            {
                if (randInt < player2.GetComponent<PlayerTwo>().playerTwoCritChance)
                {
                    int critApply = (player2.GetComponent<PlayerTwo>().damageValue + (100 / player2.GetComponent<PlayerTwo>().playerTwoCritChance));
                    gameManager.GetComponent<AttackAndDamage>().TakePlayerOneDamage(critApply);
                    Debug.Log("Critical Hit for " + critApply + ".");
                }
                else
                {
                    gameManager.GetComponent<AttackAndDamage>().TakePlayerOneDamage(player2.GetComponent<PlayerTwo>().damageValue);
                    Debug.Log("Did not Crit. Did " + player2.GetComponent<PlayerTwo>().damageValue + " Damage.");
                }
            }
            else if (gameManager.GetComponent<MonsterTwoStats>().moveHits == false)
            {
                gameManager.GetComponent<AttackAndDamage>().TakePlayerOneDamage(0);
            }
        }

        moveCountered = false;
        playerOneMoveHits = false;
        playerTwoMoveHits = false;

        return;
    }
}
