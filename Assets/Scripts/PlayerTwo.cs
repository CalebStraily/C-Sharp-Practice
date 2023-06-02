using System.Collections;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerTwo : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private float slamDistance;
    [SerializeField] private float slamTravelTime;
    [SerializeField] private float slamDelay;
    
    [SerializeField] private float quickStepDelay;
    [SerializeField] private float quickStepDistance;
    
    [SerializeField] private GameObject player1;
    
    [SerializeField] GameObject gameManager;
    [SerializeField] GameObject playerTwoGutsBar;

    public float rankModifier;
    public int playerTwoCritChance;
    public int playerTwoLastPressed;
    public int damageValue;

    public bool isP2InputEnabled = true;
    private bool isCollidingPlayer1 = true;

    public bool playerTwoInitiatedCounter = false;

    void Start()
    {

    }

    void Update()
    {
        float distance = gameManager.GetComponent<DistanceKeeper>().distance;
        int playerTwoGuts = playerTwoGutsBar.GetComponent<PlayerTwoGutsBar>().p2CurrentGuts;
        
        if (Input.GetKeyDown(KeyCode.Alpha8) && distance >= 1.5 && distance <= 6.33 && playerTwoGuts >= 20)
        {
            playerTwoLastPressed = 8;
            rankModifier = 10; //C Rank Modifier
            playerTwoCritChance = 50;
            damageValue = 30;

            playerTwoGutsBar.GetComponent<PlayerTwoGutsBar>().p2TakeGutsDamage(damageValue);

            playerTwoInitiatedCounter = true;

            if (player1.GetComponent<PlayerOne>().playerOneInitiatedCounter == false)
            {
                Debug.Log("Player Two initiated counter!");
                gameManager.GetComponent<MonsterOneStats>().playerOneIsCountering = true;
                gameManager.GetComponent<CounterSystem>().toggleScriptOn();
            }  
        }

        if (Input.GetKeyDown(KeyCode.Alpha9) && distance >= 6.34 && distance <= 10.66 && playerTwoGuts >= 40)
        {
            playerTwoLastPressed = 9;
            rankModifier = 20; //B Rank Modifier
            playerTwoCritChance = 50;
            damageValue = 50;

            playerTwoGutsBar.GetComponent<PlayerTwoGutsBar>().p2TakeGutsDamage(damageValue);

            playerTwoInitiatedCounter = true;

            if (player1.GetComponent<PlayerOne>().playerOneInitiatedCounter == false)
            {
                Debug.Log("Player Two initiated counter!");
                gameManager.GetComponent<MonsterOneStats>().playerOneIsCountering = true;
                gameManager.GetComponent<CounterSystem>().toggleScriptOn();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha0) && distance >= 10.67 && distance <= 15 && playerTwoGuts >= 40)
        {
            playerTwoLastPressed = 0;
            rankModifier = 30; //A Rank Modifier
            playerTwoCritChance = 50;
            damageValue = 60;

            playerTwoGutsBar.GetComponent<PlayerTwoGutsBar>().p2TakeGutsDamage(damageValue);

            playerTwoInitiatedCounter = true;

            if (player1.GetComponent<PlayerOne>().playerOneInitiatedCounter == false)
            {
                Debug.Log("Player Two initiated counter!");
                gameManager.GetComponent<MonsterOneStats>().playerOneIsCountering = true;
                gameManager.GetComponent<CounterSystem>().toggleScriptOn();
            }
        }

        gameManager.GetComponent<MonsterTwoStats>().moveHits = false;
        gameManager.GetComponent<MonsterTwoStats>().moveCrits = false;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player1")
        {
            isCollidingPlayer1 = true;
        }
        else
        {
            isCollidingPlayer1 = false;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        isCollidingPlayer1 = false;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && isP2InputEnabled)
        {
            transform.Translate (-Vector3.forward * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow) && isP2InputEnabled)
        {
            transform.Translate (Vector3.forward * speed * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.DownArrow) && isP2InputEnabled && isCollidingPlayer1)
        {
            SlamCalculationsPlayer2();
            player1.GetComponent<PlayerOne>().SlamCalculationsPlayer1();
        }
        
        if (Input.GetKeyDown(KeyCode.Keypad7) && isP2InputEnabled)
        {
            QuickStepForward();
            StartCoroutine(QuickStepDelay());
            playerTwoGutsBar.GetComponent<PlayerTwoGutsBar>().p2TakeGutsDamage(10);
        }

        if (Input.GetKeyDown(KeyCode.Keypad9) && isP2InputEnabled)
        {
            QuickStepBack();
            StartCoroutine(QuickStepDelay());
            playerTwoGutsBar.GetComponent<PlayerTwoGutsBar>().p2TakeGutsDamage(10);
        }
    }
    
    void QuickStepBack()
    {
        transform.Translate(Vector3.forward * quickStepDistance * Time.deltaTime);
    }    
        
    
    void QuickStepForward()
    {
        transform.Translate(Vector3.back * quickStepDistance * Time.deltaTime);
    }
    
    public void SlamCalculationsPlayer2()
    {
        isP2InputEnabled = false;
        Vector3 newPosition = transform.position;
        newPosition.z -= slamDistance;
        transform.position = newPosition;
        StartCoroutine(SlamDelay());
        Vector3.Lerp(transform.position, newPosition, slamTravelTime / Time.deltaTime);
    }
    IEnumerator SlamDelay()
    {
        yield return new WaitForSeconds(slamDelay);
        isP2InputEnabled = true;
    }
    
    IEnumerator QuickStepDelay()
    {
        yield return new WaitForSeconds(quickStepDelay);
        isP2InputEnabled = true;
    }
    public void TakePlayerOneDamage()
    {
        int randInt = Random.Range(1, 100);

        if (randInt < playerTwoCritChance)
        {
            int critApply = (20 + (100 / playerTwoCritChance));
            gameManager.GetComponent<AttackAndDamage>().TakePlayerOneDamage(critApply);
            Debug.Log("Critical Hit for " + critApply);
        }
        else
        {
            gameManager.GetComponent<AttackAndDamage>().TakePlayerOneDamage(20);
            Debug.Log("Did not Crit.");
        }
    }
}
