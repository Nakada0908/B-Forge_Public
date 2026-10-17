using UnityEngine;

public class ItemSpeed : MonoBehaviour
{
    [SerializeField] private int speedUp = 1;

    [SerializeField] private AudioClip itemGetClip;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMove target = other.GetComponent<PlayerMove>();
        if (target != null)
        {
            if (target.speed < 10)
            {
                target.speed += speedUp;
                PlayerSoundManager.Instance.PlaySE(itemGetClip);
            }

            Destroy(gameObject);
        }
    }
}
