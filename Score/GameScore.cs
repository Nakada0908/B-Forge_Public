using UnityEngine;

public class GameScore : MonoBehaviour
{
    private void Start()
    {
        ScoreManager.Instance.Reset();
    }
}
