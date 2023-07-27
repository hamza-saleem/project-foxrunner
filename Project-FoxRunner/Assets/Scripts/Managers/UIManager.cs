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
    public Button pause;
    public Button reset;
    int score = 0;
    [SerializeField] private Transform player;



    public void Pause()
    {
        if(Time.timeScale == 0.0f)
            Time.timeScale = 1.0f;

        else
            Time.timeScale = 0.0f;
    }

    public void ShowUI()
    {
        if(GameManager.Instance.gameStarted)
        {
            startText.gameObject.SetActive(false);
            scoreText.gameObject.SetActive(true);
            pause.gameObject.SetActive(true);
            reset.gameObject.SetActive(true);
        }
    }

    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }



    private void OnGUI()
    {
        scoreText.text = "Score : " + (score + (int)player.transform.position.x).ToString();
    }
}
