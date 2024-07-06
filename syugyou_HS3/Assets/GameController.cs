using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Slider gauge;
    public RectTransform judgementLine;
    public RectTransform perfectZone;
    public RectTransform greatZone;
    public Text scoreText;
    public Text lifeText;
    public int initialLife = 3;

    private int score = 0;
    private int life;
    private float perfectZoneRange = 0.05f;
    private float greatZoneRange = 0.1f;

    void Start()
    {
        life = initialLife;
        UpdateUI();
        RandomizeJudgementLine();
    }

    void Update()
    {
        // ゲージの更新と判定
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckJudgement();
            RandomizeJudgementLine();
            UpdateUI();
        }
    }

    void CheckJudgement()
    {
        float distance = Mathf.Abs(judgementLine.anchoredPosition.x - gauge.value);
        if (distance <= perfectZoneRange)
        {
            score += 100;
        }
        else if (distance <= greatZoneRange)
        {
            score += 50;
        }
        else
        {
            life -= 1;
            if (life <= 0)
            {
                GameOver();
            }
        }
    }

    void RandomizeJudgementLine()
    {
        float randomX = Random.Range(-gauge.GetComponent<RectTransform>().rect.width / 2, gauge.GetComponent<RectTransform>().rect.width / 2);
        judgementLine.anchoredPosition = new Vector2(randomX, judgementLine.anchoredPosition.y);
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        lifeText.text = "Life: " + life;
    }

    void GameOver()
    {
        PlayerPrefs.SetInt("Score", score);
        SceneManager.LoadScene("GameOver");
    }
}
