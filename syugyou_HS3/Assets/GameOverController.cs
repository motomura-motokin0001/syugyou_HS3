using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public Text scoreText;

    void Start()
    {
        int finalScore = PlayerPrefs.GetInt("Score", 0);
        scoreText.text = "Score: " + finalScore;
    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
