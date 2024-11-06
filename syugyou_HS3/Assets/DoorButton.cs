using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class DoorButton : MonoBehaviour
{
    public RectTransform door;
    public Button doorButton;

    private bool isDoorVisible = false;
    public Vector2 StratPosition;
    public Vector2 EndPosition;
    public AudioSource DoorClose;
    public AudioSource DoorOpen;

    void Start()
    {
        // 扉の初期位置を設定
        door.anchoredPosition = StratPosition;

        // ボタンにクリックイベントを登録
        doorButton.onClick.AddListener(ToggleMenu);
    }

    void ToggleMenu()
    {
        if (isDoorVisible)
        {
            // 扉を開ける
            DoorClose.Play();
            door.DOAnchorPos(StratPosition, 1.5f);
        }
        else
        {
            // 扉を閉める
            DoorOpen.Play();
            door.DOAnchorPos(EndPosition, 1.5f);
        }
        isDoorVisible = !isDoorVisible;
    }
    public void Reset()
    {
        StartCoroutine(Load());
    }

        IEnumerator Load()
    {
        yield return new WaitForSeconds(1);
        door.DOAnchorPos(StratPosition, 0.5f);
    }
}