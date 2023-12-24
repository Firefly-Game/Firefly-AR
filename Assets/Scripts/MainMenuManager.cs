using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void OpenTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void GameStart()
    {
        SceneManager.LoadScene("ARGame");
    }

    public void ExitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false; // Uncomment if testing in the Unity Editor
    }
}
