using UnityEngine;

public class ItemShotgunLevelUp : MonoBehaviour
{
    [SerializeField] private AudioClip itemGetClip;
    [SerializeField] private int shotgunAngle = 1;
    [SerializeField] private float addDistance = 0.2f;
    private int itemMaxShotgunAngle = 18;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerAttack player = other.GetComponent<PlayerAttack>();
            if(player.shotgunAngle > itemMaxShotgunAngle)
            {
                Destroy(gameObject);
                return;
            }
            player.shotgunAngle += shotgunAngle;
            player.distance += addDistance;

            PlayerSoundManager.Instance.PlaySE(itemGetClip);
            Destroy(gameObject);
        }
    }
}
