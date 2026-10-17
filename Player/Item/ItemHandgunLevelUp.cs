using UnityEngine;

public class ItemHandgunLevelUp : MonoBehaviour
{
    [SerializeField] private AudioClip itemGetClip;
    private int itemMaxLevel = 11;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerAttack player = other.GetComponent<PlayerAttack>();
            if(player.rapidFire > itemMaxLevel)
            {
                Destroy(gameObject);
                return;
            }
            player.rapidFire += 1;

            PlayerSoundManager.Instance.PlaySE(itemGetClip);
            Destroy(gameObject);
        }
    }
}
