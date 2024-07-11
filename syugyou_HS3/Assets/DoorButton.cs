using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DoorButton : MonoBehaviour
{
    public RectTransform door;
    public Button doorButton;

    private bool isDoorVisible = false;
    public Vector2 StratPosition;
    public Vector2 EndPosition;

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
            door.DOAnchorPos(StratPosition, 0.5f);
        }
        else
        {
            // 扉を閉める
            door.DOAnchorPos(EndPosition, 0.5f);
        }
        isDoorVisible = !isDoorVisible;
    }
}