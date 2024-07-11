using UnityEngine;
using System.Collections;
using TMPro;


public class CountdownManager : MonoBehaviour
{
public TextMeshProUGUI countdownText;
    public delegate void CountdownFinishedHandler();
    public event CountdownFinishedHandler OnCountdownFinished;

    void Start()
    {
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }

        countdownText.text = "始め!";
        yield return new WaitForSeconds(1); // "GO!"を表示するための1秒待機
        
        countdownText.text = "";
        GameManager.instance.GameStart();

        // カウントダウン終了を通知
        OnCountdownFinished?.Invoke();
    }

    
}