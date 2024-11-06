using UnityEngine;
using UnityEngine.EventSystems;

public class PlaySoundOnHover : MonoBehaviour, IPointerEnterHandler
{
    public AudioSource audioSource;

    // マウスがボタンに触れたときに呼び出されるメソッド
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
