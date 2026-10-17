using UnityEngine;

public class RecoveryItemCreate : MonoBehaviour
{
    [SerializeField] private GameObject item;
    [SerializeField] private Transform tpos;
    [SerializeField] private float span = 8f;
    [SerializeField] private int sponRange = 40;
    private float Cnt = 0f;

    [SerializeField] private Transform parent;

    void Update()
    {
        Cnt += Time.deltaTime;

        if (Cnt > span)
        {
            Cnt = 0f;
            Instantiate(item,
                new Vector2(tpos.transform.position.x + Random.Range(-sponRange, sponRange),
                tpos.transform.position.y + Random.Range(-sponRange, sponRange)),
                Quaternion.identity,
                parent);
        }
    }
}
