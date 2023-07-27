using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{

    public bool gameStarted = false;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        gameStarted = false;
        Time.timeScale = 0.0f;
    }
    private void Update()
    {

        if(Input.GetMouseButton(0) && !gameStarted)
        {
            Time.timeScale = 1.0f;
            gameStarted = true;
            UIManager.Instance.ShowUI();
        }
    }
}
