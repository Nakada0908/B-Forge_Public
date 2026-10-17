using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RankingUI : MonoBehaviour
{
    [SerializeField] private GameObject rankingPanel;
    [SerializeField] private TMP_Text rankingText;

    public void ShowRanking()
    {
        rankingPanel.SetActive(true);

        List<int> scores = SaveScoreManager.Instance.GetHighScores();

        rankingText.text = "\nHIGH SCORE\n\n";

        for (int i = 0; i < scores.Count; i++)
        {
            rankingText.text += (i + 1) + ". " + scores[i] + "\n";
        }
    }

    public void CloseRanking()
    {
        rankingPanel.SetActive(false);
    }
}
