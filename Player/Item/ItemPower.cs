using UnityEngine;

public class ItemPower : MonoBehaviour
{
    [SerializeField] private int powerUp = 1;
    private int itemMaxPower = 9;

    [SerializeField] private AudioClip itemGetClip;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerAttack player = other.GetComponent<PlayerAttack>();
            if (player.power < itemMaxPower)
            {
                player.power += powerUp;
                PlayerSoundManager.Instance.PlaySE(itemGetClip);
            }

            Destroy(gameObject);
        }
    }
}
