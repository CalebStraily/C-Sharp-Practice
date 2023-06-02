using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterSystemV2 : MonoBehaviour
{
    public float timeLimit = 3f;    // The time limit for button presses

    private float currentTime;      // Current time remaining
    private bool isCounting;        // Flag to track if counting is active
    private KeyCode player1Button;  // Button for Player 1 to initiate counting
    private KeyCode player2Button;  // Button for Player 2 to counter

    private void Start()
    {
        // Example: Start counting when Player 1 presses Alpha1 and require Player 2 to press Alpha8
        SetButtons(KeyCode.Alpha1, KeyCode.Alpha8);
        StartCounting();
    }

    private void Update()
    {
        if (isCounting)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0f)
            {
                StopCounting();
                Debug.Log("Failed! You didn't press the correct button in time.");
                // Add your failure logic here
                RestartCounting();
            }
        }
    }

    private void SetButtons(KeyCode p1Button, KeyCode p2Button)
    {
        player1Button = p1Button;
        player2Button = p2Button;
    }

    private void StartCounting()
    {
        currentTime = timeLimit;
        isCounting = true;
        Debug.Log("Counting started!");

        // Start waiting for the correct button press from Player 2
        StartCoroutine(WaitForButtonPress());
    }

    private void StopCounting()
    {
        isCounting = false;
        Debug.Log("Counting stopped!");
    }

    private void RestartCounting()
    {
        StartCoroutine(WaitForButtonPress());
    }

    private IEnumerator WaitForButtonPress()
    {
        while (true)
        {
            // Check if Player 2 presses the correct button
            if (Input.GetKeyDown(player2Button))
            {
                Debug.Log("Success! Player 2 pressed the correct button: " + player2Button);
                // Add your success logic here
                StopCounting();
                yield break;
            }

            yield return null;
        }

        // Wait for Player 1 to initiate counting again
        while (true)
        {
            // Check if Player 1 presses the button to initiate counting
            if (Input.GetKeyDown(player1Button))
            {
                Debug.Log("Player 1 initiated counting with button: " + player1Button);
                StartCounting();
                yield break;
            }

            yield return null;
        }
    }
}
