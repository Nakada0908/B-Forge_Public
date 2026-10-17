using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SetRanking : MonoBehaviour
{
    [SerializeField] private Text rankingText;
    private List<int> scores;

    void Start()
    {
        scores = SaveScoreManager.Instance.GetHighScores();

        string rankingTexts = "";
        for (int i = 0; i < scores.Count; i++)
        {
            rankingTexts += "Rank " + (i + 1) + ": " + scores[i] + "\n";
        }
        rankingText.text = "HighScoresRankings" + "\n" + rankingTexts;
    }
}
