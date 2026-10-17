using UnityEngine;
using UnityEngine.InputSystem;

public class DebugItems : MonoBehaviour
{
    private InputSystem_Actions input;

    [SerializeField] private GameObject secretItemPrefabs;
    [SerializeField] private GameObject debugItemPrefabs;
    [SerializeField] private Transform playerPosition;

    private bool isSecretItemActive = false;

    private void Start()
    {
        input = InputManager.Instance.input;
        secretItemPrefabs.SetActive(false);
        debugItemPrefabs.SetActive(false);
    }

    private void Update()
    {
        if(isSecretItemActive)
        {
            return;
        }

        if (input.Player.Secret.WasPressedThisFrame())
        {
            //半分は隠し機能としてアイテム一部出現
            secretItemPrefabs.SetActive(true);
            secretItemPrefabs.transform.position = playerPosition.position;

#if UNITY_EDITOR || DEVELOPMENT_BUILD
            //残りは開発者専用
            debugItemPrefabs.SetActive(true);
            debugItemPrefabs.transform.position = playerPosition.position;
#endif
            isSecretItemActive = true;
        }
    }
}
