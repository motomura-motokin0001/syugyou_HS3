using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Start()
    {
        int finalScore = PlayerPrefs.GetInt("Score", 0);
        scoreText.text = "ScoreResult " + finalScore;
    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
