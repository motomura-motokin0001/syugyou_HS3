using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.Mathematics;

public class LodChange : MonoBehaviour
{

    [Header("各キャンバス")]
        public Canvas TitleCanvas;
        public Canvas GAMECanvas;
        public Canvas RESULTCanvas;

    [Header("右の扉")]
        public RectTransform RightDoor;
        public Vector3 S_Position_Right;
        public Vector3 E_Position_Right;

    [Header("左の扉")]
        public RectTransform LeftDoor;
        public Vector3 S_Position_Left;
        public Vector3 E_Position_Left;
        
    [Header("各待機時間")]
        public float DoorSped = 0.5f;
        public float StopTime = 1f;
    [Header("SE関連")]
        public AudioSource DoorClose;
        public AudioSource DoorOpen;
        void Start()
    {
        //==========キャンバスの初期設定=============
        TitleCanvas.gameObject.SetActive(true);
        GAMECanvas.gameObject.SetActive(false);
        RESULTCanvas.gameObject.SetActive(false);
        //=========================================
        
        RightDoor.anchoredPosition = S_Position_Right;
        LeftDoor.anchoredPosition = S_Position_Left;

    }
    void FeedImage()
    {
        DoorClose.Play();
        RightDoor.DOAnchorPos(E_Position_Right, DoorSped);
        LeftDoor.DOAnchorPos(E_Position_Left, DoorSped);
    }

    public void GAME_START()
    {
        // メニューを隠す
        FeedImage();
        // 3秒待機してからメニューを表示
        StartCoroutine(Time_GAME());
    }
    IEnumerator Time_GAME()
    {
        yield return new WaitForSeconds(StopTime);
        TitleCanvas.gameObject.SetActive(false);
        DoorOpen.Play();
        RightDoor.DOAnchorPos(S_Position_Right, DoorSped);
        LeftDoor.DOAnchorPos(S_Position_Left, DoorSped);
        GAMECanvas.gameObject.SetActive(true);
    }

    public void Result_back_Title()
    {
        // メニューを隠す
        FeedImage();
        StartCoroutine(Time_S_B());// 3秒待機してからメニューを表示
        
    }
    IEnumerator Time_S_B()
    {
        yield return new WaitForSeconds(StopTime);
        RESULTCanvas.gameObject.SetActive(false);
        DoorOpen.Play();
        RightDoor.DOAnchorPos(S_Position_Right, DoorSped);
        LeftDoor.DOAnchorPos(S_Position_Left, DoorSped);
        TitleCanvas.gameObject.SetActive(true);
    }

    public void GAME_back_RESULT()
    {
        // メニューを隠す
        FeedImage();
        StartCoroutine(Time_G_B());// 3秒待機してからメニューを表示
    }
    IEnumerator Time_G_B()
    {
        yield return new WaitForSeconds(StopTime);
        GAMECanvas.gameObject.SetActive(false);
        DoorOpen.Play();
        RightDoor.DOAnchorPos(S_Position_Right, DoorSped);
        LeftDoor.DOAnchorPos(S_Position_Left, DoorSped);
        RESULTCanvas.gameObject.SetActive(true);
    }

}

