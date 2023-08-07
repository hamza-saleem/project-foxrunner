using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    public TextMeshProUGUI startText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI berryText;
    public TextMeshProUGUI myScorePause;
    public TextMeshProUGUI myScoreGameOver;

    public Button pause;
    public Button reset;
    public GameObject GameOver;
    int score = 0;
    [SerializeField] private Transform player;


    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;

    }

    public void Pause()
    {
        if(Time.timeScale == 0.0f)
            Time.timeScale = 1.0f;

        else
            Time.timeScale = 0.0f;
    }

    public void ShowUI()
    {
        if(gameManager.gameStarted)
        {
            startText.gameObject.SetActive(false);
            scoreText.gameObject.SetActive(true);
            pause.gameObject.SetActive(true);
            reset.gameObject.SetActive(true);
        }
    }

    public void OnDeath()
    {
        GameOver.SetActive(true);
    }

    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void OnBerryCollect()
    {
        scoreText.text = (20 + score + (int)player.transform.position.x).ToString() ;
    }

    private void OnGUI()
    {
        scoreText.text = (score + (int)player.transform.position.x).ToString();
        myScoreGameOver.text = scoreText.text;
        myScorePause.text = scoreText.text;
    }
}
