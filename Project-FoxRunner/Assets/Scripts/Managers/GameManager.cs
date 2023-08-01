using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : Singleton<GameManager>
{

    public bool gameStarted = false;
    [SerializeField] private int berryCount = 0;
    private int score = 0;


    [SerializeField] private Transform player;
    [SerializeField] private float deathHeight = -10f;
    [SerializeField] private SideScrollingCamera scrollCamera;
    [SerializeField] private BackgroundScroller bgScroll;

    private bool isGameOver = false;


    private void Awake()
    {
        Application.targetFrameRate = 60;
        gameStarted = false;
        scrollCamera.enabled = false;
        bgScroll.enabled = false;
    }
    private void Update()
    {

        if(Input.GetMouseButton(0) && !gameStarted)
        {
            gameStarted = true;
            UIManager.Instance.ShowUI();
            scrollCamera.enabled = true;
            bgScroll.enabled = true;
        }

        if (gameStarted && player.position.y < deathHeight)
        {
            gameStarted=false;
            isGameOver = true;
            scrollCamera.enabled = false;
            bgScroll.enabled = false;
        }
    }

    public bool GameStart() => gameStarted;
    public bool GameOver() => isGameOver;

    public void CollectItem()
    {
        berryCount++;
    }

    public int GetBerryCount()
    {
        return berryCount;
    }
}
