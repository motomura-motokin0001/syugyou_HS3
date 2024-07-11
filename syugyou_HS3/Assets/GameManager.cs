using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehavior<GameManager>
{
    private bool isStart = false;

    public void GameStart()
    {
        isStart = true;
    }

    public bool IsStart() { return isStart; }
}
