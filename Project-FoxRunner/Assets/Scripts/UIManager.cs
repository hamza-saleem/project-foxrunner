using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    int score = 0;
    [SerializeField] private Transform player;

    private void OnGUI()
    {
        scoreText.text = "Score : " + (score + (int)player.transform.position.x).ToString();
    }
}
