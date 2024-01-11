using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreLabel : MonoBehaviour
{
    public Text label;

    private Dictionary<FireflyBehaviour.FireflyType, int> typeValues = new Dictionary<FireflyBehaviour.FireflyType, int> {
        { FireflyBehaviour.FireflyType.Common,    1 },
        { FireflyBehaviour.FireflyType.Rare,      3 },
        { FireflyBehaviour.FireflyType.Epic,      8 },
        { FireflyBehaviour.FireflyType.Legendary, 25 },
        { FireflyBehaviour.FireflyType.Moth, 0 }
    };

    public int score { get; private set; } = 0;

    void Update()
    {
        label.text = score.ToString();
    }

    // Call this when a firefly is caught
    public void OnCatch(FireflyBehaviour firefly)
    {
        
        // If we caught moth, end game
        if(firefly.tag.Equals("moth"))
        {
            StartCoroutine(GameOverSequence());
        }

        Debug.Log(firefly.tag);

        score += typeValues[firefly.Type];
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    private IEnumerator GameOverSequence()
    {
        PauseGame();

        yield return new WaitForSecondsRealtime(2);

        ResumeGame();

        SceneManager.LoadScene("LoseScreen"); 

    }

    private void EndGame()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
    }
}
