using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    [SerializeField]
    GameObject pausePanel;
    static bool isPaused;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }
    public void PauseGame()
    {
        
        if (isPaused == false)
        {
            pausePanel.SetActive(true);
            isPaused = true;
            Time.timeScale = 0f;
        }
        else if (isPaused)
        {
            pausePanel.SetActive(false);
            isPaused = false;
            Time.timeScale = 1f;
        }
    }

    public void ResumeGame()
    {
            pausePanel.SetActive(false);
            isPaused = false;
            Time.timeScale = 1f;
    }
}
