using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : Singleton<GameManager>
{

    public bool gameStarted { get; set; }
    [SerializeField] private int berryCount = 0;

    [SerializeField] private Transform player;
    [SerializeField] private float deathHeight = -10f;
    [SerializeField] private SideScrollingCamera scrollCamera;
    [SerializeField] private BackgroundScroller bgScroll;

    private bool isGameOver = false;
    private void Awake()
    {
        CheckPlayerData();
        Debug.Log( "First Time : " + PlayerPrefs.GetString("FirstPlay"));
        gameStarted = false;
        scrollCamera.enabled = false;
        bgScroll.enabled = false;
    }

    private void Update()
    {

        if (Input.GetMouseButton(0) && !gameStarted)
        {
            InputManager.Instance.enabled = true;
            gameStarted = true;
            scrollCamera.enabled = true;
            bgScroll.enabled = true;
        }

        if (FallToDeath() || DeathBySpike())
        {
            isGameOver = true;
            scrollCamera.enabled = false;
            bgScroll.enabled = false;
        }

    }


    private bool DeathBySpike() => TrapDeath.touchedSpike;
    private bool FallToDeath() => gameStarted && player.position.y < deathHeight;
    public bool GameOver() => isGameOver;
    public void CollectItem() => UIManager.Instance.OnBerryCollect();
    public int GetBerryCount() => berryCount;

    public void CheckPlayerData()
    {
        if (!PlayerPrefs.HasKey("FirstPlay"))
            PlayerPrefs.SetString("FirstPlay", "Yes");


        if (!PlayerPrefs.HasKey("HighScore"))
            PlayerPrefs.SetInt("HighScore", 0);
    }

}