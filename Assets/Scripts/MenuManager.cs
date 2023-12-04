using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static bool isGamePaused = false;
    public GameObject PauseMenuPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isGamePaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0;
        PauseMenuPanel.SetActive(true);
        // Additional code for pausing the game
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1;
        PauseMenuPanel.SetActive(false);
        // Additional code for resuming the game
    }

    public void QuitGame()
    {
        Application.Quit();
        // UnityEditor.EditorApplication.isPlaying = false; // Uncomment if testing in the Unity Editor
    }
}
