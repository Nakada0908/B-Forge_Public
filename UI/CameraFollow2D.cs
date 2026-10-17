using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [SerializeField] private Transform player;

    private void Start()
    {
        FollowPlayer();
    }

    private void LateUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (player == null)
        {
            return;
        }

        transform.position = new Vector3(
            player.position.x,
            player.position.y,
            -10f
        );
    }
}
