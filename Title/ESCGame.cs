using UnityEngine;
using UnityEngine.InputSystem;

public class ESCGame : MonoBehaviour
{
    private static ESCGame Instance;

    private void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        //キーボードが存在し、かつESCキーが押されたか確認
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            QuitGame();
        }
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
