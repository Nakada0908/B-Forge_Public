using UnityEngine;
using UnityEngine.UI;

public class DrawScore : MonoBehaviour
{
    [SerializeField] private Text scoreText;

    void Start()
    {
        scoreText.text = ScoreManager.Instance.nowScore+"";
    }
}
