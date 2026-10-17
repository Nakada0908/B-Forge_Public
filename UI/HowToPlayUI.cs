using UnityEngine;

public class HowToPlayUI : MonoBehaviour
{
    [SerializeField] private GameObject howToPlayPanel;

    public void ShowHowToPlay()
    {
        howToPlayPanel.SetActive(true);
    }

    public void CloseHowToPlay()
    {
        howToPlayPanel.SetActive(false);
    }
}
