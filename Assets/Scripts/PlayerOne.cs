using System;
using System.Collections;
using System.Net;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerOne : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private float slamDistance;
    [SerializeField] private float slamTravelTime;
    [SerializeField] private float slamDelay;

    [SerializeField] private float quickStepDelay;
    [SerializeField] private float quickStepDistance;

    [SerializeField] private GameObject player2;

    [SerializeField] GameObject gameManager;
    [SerializeField] GameObject playerOneGutsBar;

    [SerializeField] GameObject slamImage;

    private int playerOneGuts;

    public float rankModifier;
    public int playerOneCritChance;
    public int playerOneLastPressed;
    public int damageValue;

    public bool isP1InputEnabled = true;
    private bool isCollidingPlayer2 = true;

    private PlayerControls p1Controls;

    public bool playerOneInitiatedCounter = false;

    void Awake()
    {
        p1Controls = new PlayerControls();
    }

    void Start()
    {
        
    }

    void Update()
    {
        float distance = gameManager.GetComponent<DistanceKeeper>().distance;
        playerOneGuts = playerOneGutsBar.GetComponent<PlayerOneGutsBar>().p1CurrentGuts;

        if (Input.GetKeyDown(KeyCode.Alpha1) && distance >= 1.5 && distance <= 6.33 && playerOneGuts >= 20)
        {
            playerOneLastPressed = 1;
            rankModifier = 10; //C Rank Modifier
            playerOneCritChance = 50;
            damageValue = 20;

            playerOneGutsBar.GetComponent<PlayerOneGutsBar>().p1TakeGutsDamage(damageValue);
            
            playerOneInitiatedCounter = true;

            if (player2.GetComponent<PlayerTwo>().playerTwoInitiatedCounter == false)
            {
                Debug.Log("Player One initiated counter!");
                gameManager.GetComponent<MonsterTwoStats>().playerTwoIsCountering = true;
                gameManager.GetComponent<CounterSystem>().toggleScriptOn();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && distance >= 6.34 && distance <= 10.66 && playerOneGuts >= 40)
        {
            playerOneLastPressed = 2;
            rankModifier = 20; //B Rank Modifier
            playerOneCritChance = 50;
            damageValue = 40;

            playerOneGutsBar.GetComponent<PlayerOneGutsBar>().p1TakeGutsDamage(damageValue);

            playerOneInitiatedCounter = true;

            if (player2.GetComponent<PlayerTwo>().playerTwoInitiatedCounter == false)
            {
                Debug.Log("Player One initiated counter!");
                gameManager.GetComponent<MonsterTwoStats>().playerTwoIsCountering = true;
                gameManager.GetComponent<CounterSystem>().toggleScriptOn();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3) && distance >= 10.67 && distance <= 15 && playerOneGuts >= 40)
        {
            playerOneLastPressed = 3;
            rankModifier = 30; //A Rank Modifier
            playerOneCritChance = 50;
            damageValue = 60;

            playerOneGutsBar.GetComponent<PlayerOneGutsBar>().p1TakeGutsDamage(damageValue);

            playerOneInitiatedCounter = true;

            if (player2.GetComponent<PlayerTwo>().playerTwoInitiatedCounter == false)
            {
                Debug.Log("Player One initiated counter!");
                gameManager.GetComponent<MonsterTwoStats>().playerTwoIsCountering = true;
                gameManager.GetComponent<CounterSystem>().toggleScriptOn();
            }
        }
    }

    private void OnEnable()
    {
        p1Controls.CombatP1.Enable();
    }

    private void OnDisable()
    {
        p1Controls.CombatP1.Disable();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player2")
        {
            isCollidingPlayer2 = true;
            slamImage.SetActive(true);
        }
        else
        {
            isCollidingPlayer2 = false;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        isCollidingPlayer2 = false;
        slamImage.SetActive(false);
    }
    
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A) && isP1InputEnabled)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D) && isP1InputEnabled)
        {
            transform.Translate(-Vector3.forward * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S) && isP1InputEnabled && isCollidingPlayer2)
        {
            SlamCalculationsPlayer1();
            player2.GetComponent<PlayerTwo>().SlamCalculationsPlayer2();
        }

        if (Input.GetKeyDown(KeyCode.Q) && isP1InputEnabled)
        {
            QuickStepForward();
            StartCoroutine(QuickStepDelay());
            playerOneGutsBar.GetComponent<PlayerOneGutsBar>().p1TakeGutsDamage(10);
        }

        if (Input.GetKeyDown(KeyCode.E) && isP1InputEnabled)
        {
            QuickStepBack();
            StartCoroutine(QuickStepDelay());
            playerOneGutsBar.GetComponent<PlayerOneGutsBar>().p1TakeGutsDamage(10);
        }
    }

        //Functions used in Quickstep System
         void QuickStepBack()
         {
            transform.Translate(Vector3.back * quickStepDistance * Time.deltaTime);
         }    
        
    
        void QuickStepForward()
        {
            transform.Translate(Vector3.forward * quickStepDistance * Time.deltaTime);
        }

        IEnumerator QuickStepDelay()
        {
            yield return new WaitForSeconds(quickStepDelay);
            isP1InputEnabled = true;
        }
        
        //Functions used in Slam System
        IEnumerator SlamDelay()
        {
            yield return new WaitForSeconds(slamDelay);
            isP1InputEnabled = true;
        }
        public void SlamCalculationsPlayer1()
        {
            isP1InputEnabled = false;
            Vector3 newPosition = transform.position;
            newPosition.z += slamDistance;
            transform.position = newPosition;
            StartCoroutine(SlamDelay());
            Vector3.Lerp(transform.position, newPosition, slamTravelTime / Time.deltaTime);
        }

        public void dodgeChanceCalculation()
        {
            
        }
}
