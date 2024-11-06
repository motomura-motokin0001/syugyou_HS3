using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameController : MonoBehaviour
{
    [Header("ゲージ関連")]
        public RectTransform gaugeContainer; // ゲージ全体のコンテナ
        public RectTransform judgementLine; // 判定線
        public RectTransform perfectZone; // パーフェクトゾーン
        public RectTransform greatZone; // グレイトゾーン
        [SerializeField] private float judgementSpeed = 100f; // 判定線の速度
        [SerializeField] private Vector2 initialJudgementPosition = Vector2.zero; // 判定線の初期座標
        [SerializeField] private float perfectZoneRange = 50f; // ピクセル単位で調整
        [SerializeField] private float greatZoneRange = 100f; // ピクセル単位で調整
        private Vector2 judgementDirection = Vector2.right; // 判定線の進行方向


    [Header("表示関連")]
        public TextMeshProUGUI countdownText;
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI lifeText;
        public TextMeshProUGUI ResultScoreText;
        public Transform StartText;
        public Transform StopText;
        public Button ResultButton;
        public Canvas GAMECanvas;
        


    [Header("ゲーム制御")]
        public int initialLife = 3;
        public bool GAME = false;
        private int count = 3;


    private int score = 0;
    private int life;
    public Button StartButton;
    public AudioSource Slice;
    public AudioSource countSE;


    void Start()
    {
        Debug.Log("Startは機能してるよ!!");
        StartText.gameObject.SetActive(false);
        StopText.gameObject.SetActive(false);
        ResultButton.gameObject.SetActive(false);
        life = initialLife;
        UpdateUI();
        RandomizeZones();
        ResetJudgementLine(); // 初期位置に設定
        
    }
    public void Button_Start()
    {
        Start();
        StartButton.interactable = false;
        Debug.Log("Button_Start called"); // デバッグログ
        StartCoroutine(Load());
        
    }

    void Update()
    {
        if(GAME == true)
        {

            MoveJudgementLine();//判定線の動き
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Slice.Play();
                CheckJudgement();//判定メゾット
                ResetJudgementLine();//判定線のリセット
                RandomizeZones();//判定のランダム化
                UpdateUI();//スコアとライフの更新
            }
        }
        //Debug.Log("Updateは機能してるよ!!");
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
                GameStop();
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
        ResultScoreText.text = "SCORE   " + score;
    }

    void GameStop()

    {
        GAME = false;
        StopText.gameObject.SetActive(true);
        ResultButton.gameObject.SetActive(true);
    }
    IEnumerator Load()
    {
        yield return new WaitForSeconds(3);
        countdownText.gameObject.SetActive(true);
        StartCoroutine(CountdownCoroutine());
    }
    IEnumerator CountdownCoroutine()
    {   
        Debug.Log("カウントダウン はじめ"); // デバッグログ
        for (count = 3; count > 0; count--)
        {
            countdownText.text = count.ToString();
            Debug.Log("Count: " + count);  // 現在のカウント数を表示
            countSE.Play();
            yield return new WaitForSeconds(1);
        }
        
        if(count == 0)
        {
            countdownText.gameObject.SetActive(false);
            StartText.gameObject.SetActive(true);
            Debug.Log("Displaying 'GO!'");  // デバッグログ
            yield return new WaitForSeconds(1);
            
            StartText.gameObject.SetActive(false);
            GAME = true;
            Debug.Log("Game started");  // デバッグログ
                    StartButton.interactable = true;

        }
    }
}
