using UnityEngine;

public class ItemBaria : MonoBehaviour
{
    [SerializeField] private AudioClip itemGetClip;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStates playerStates = other.GetComponent<PlayerStates>();
            playerStates.isItemUnHit = true;

            PlayerSoundManager.Instance.PlaySE(itemGetClip);
            Destroy(gameObject);
        }
    }
}
