using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int nowScore;

    [SerializeField] private Text scoreText;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        nowScore = 0;
        if (scoreText != null)
        {
            scoreText.text = "Score:" + nowScore;
        }
    }

    public void AddScore(int s)
    {
        nowScore += s;
        scoreText.text= "Score:" + nowScore;
    }

    public void Reset()
    {
        nowScore = 0;
        var s = GameObject.Find("ScoreText");
        if (s != null)
        {
            scoreText = s.GetComponent<Text>();
        }
        scoreText.text= "Score:" + nowScore;
    }
}