using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : Singleton<UIManager>
{
    [Header("In-Game UI")]
    public TextMeshProUGUI startText;
    public TextMeshProUGUI scoreText;
    public Button pause;
    public Button reset;
    public GameObject HighScore;

    [Header("Panels")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI myScorePause;
    public TextMeshProUGUI myScoreGameOver;
    [SerializeField] private GameObject highScorePanel;
    [SerializeField] private TextMeshProUGUI newHighScore;
    [SerializeField] private TextMeshProUGUI currentHighScore;

    [Header("Misc. References")]
    [SerializeField] private Transform player;
    [SerializeField] private Image tutorialArrow;
    GameManager gameManager;
    private int score = 0;
    private int totalScore;
    private int tutorialArrowCount = 0;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        currentHighScore.text = PlayerPrefs.GetInt("HighScore").ToString();
    }


    private void Update()
    {
        if (gameManager.gameStarted)
            ShowUI();

        UpdateScoreText();

    }

    public void TogglePause()
    {
        if (Time.timeScale == 0)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
    }

    private void ShowUI()
    {
        startText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        pause.gameObject.SetActive(true);
        reset.gameObject.SetActive(true);
        HighScore.SetActive(true);

    }

    public void OnDeath()
    {
        if (totalScore > PlayerPrefs.GetInt("HighScore"))
        {
            highScorePanel.SetActive(true);
            newHighScore.text = scoreText.text;
            PlayerPrefs.SetInt("HighScore", totalScore);
        }
        else
        {
            gameOverPanel.SetActive(true);
            myScoreGameOver.text = scoreText.text;
        }
    }

    public void ShowTutorialArrow(bool value) 
    {
        tutorialArrow.gameObject.SetActive(value);
        tutorialArrowCount++;

    }

    public int GetTutorialArrowCount() => tutorialArrowCount;

    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void OnBerryCollect()
    {
        score += 20;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        totalScore = score + (int)player.transform.position.x;
        scoreText.text = totalScore.ToString();

        if (!gameManager.GameOver() && totalScore > PlayerPrefs.GetInt("HighScore"))
            currentHighScore.text = totalScore.ToString();

        myScoreGameOver.text = totalScore.ToString();
        myScorePause.text = totalScore.ToString();
    }
}

