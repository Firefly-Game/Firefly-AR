using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenControl : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
