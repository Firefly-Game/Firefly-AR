using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenTutorial()
    {
        SceneManager.LoadScene("SplashScreen");
    }
    public void GameStart()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void ExitGame()
    {
        
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false; // Uncomment if testing in the Unity Editor

    }


}
