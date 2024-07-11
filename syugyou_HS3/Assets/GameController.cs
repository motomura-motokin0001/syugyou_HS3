using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public RectTransform gaugeContainer; // ゲージ全体のコンテナ
    public RectTransform judgementLine; // 判定線
    public RectTransform perfectZone; // パーフェクトゾーン
    public RectTransform greatZone; // グレイトゾーン
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI lifeText;
    public int initialLife = 3;
    [SerializeField] private float judgementSpeed = 100f; // 判定線の速度
    [SerializeField] private Vector2 initialJudgementPosition = Vector2.zero; // 判定線の初期座標
    private bool waitStart = true;

    private int score = 0;
    private int life;
    [SerializeField] private float perfectZoneRange = 50f; // ピクセル単位で調整
    [SerializeField] private float greatZoneRange = 100f; // ピクセル単位で調整
    private Vector2 judgementDirection = Vector2.right; // 判定線の進行方向

    void Start()
    {
        if(waitStart)
        {
            life = initialLife;
            UpdateUI();
            RandomizeZones();
            ResetJudgementLine(); // 初期位置に設定
        
        }
        
    }

    void Update()
    {
        if(!GameManager.instance.IsStart()) { return; }
        {
            MoveJudgementLine();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CheckJudgement();
                ResetJudgementLine();
                RandomizeZones();
                UpdateUI();
            }
        }
    }

    void MoveJudgementLine()
    {
        judgementLine.anchoredPosition += judgementDirection * judgementSpeed * Time.deltaTime;
        if (judgementLine.anchoredPosition.x >= gaugeContainer.rect.width / 2 || judgementLine.anchoredPosition.x <= -gaugeContainer.rect.width / 2)
        {
            judgementDirection = -judgementDirection;
        }
    }

    void CheckJudgement()
    {
        float distanceToPerfect = Mathf.Abs(judgementLine.anchoredPosition.x - perfectZone.anchoredPosition.x);
        float distanceToGreat = Mathf.Abs(judgementLine.anchoredPosition.x - greatZone.anchoredPosition.x);

        if (distanceToPerfect <= perfectZoneRange / 2)
        {
            score += 100;
        }
        else if (distanceToGreat <= greatZoneRange / 2)
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

    void ResetJudgementLine()
    {
        judgementLine.anchoredPosition = initialJudgementPosition;
        judgementDirection = Vector2.right;
    }

    void RandomizeZones()
    {
        float randomX = Random.Range(-gaugeContainer.rect.width / 2 + greatZoneRange / 2, gaugeContainer.rect.width / 2 - greatZoneRange / 2);
        greatZone.anchoredPosition = new Vector2(randomX, greatZone.anchoredPosition.y);
        perfectZone.anchoredPosition = new Vector2(randomX, perfectZone.anchoredPosition.y);
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
