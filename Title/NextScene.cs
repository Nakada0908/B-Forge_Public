using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextScene : MonoBehaviour
{
    [SerializeField] string nextSceneName;

    public void GoNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
