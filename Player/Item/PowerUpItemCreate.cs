using UnityEngine;

public class PowerUpItemCreate : MonoBehaviour
{
    [SerializeField] private PlayerAttack playerAtk;
    [SerializeField] private GameObject item;
    [SerializeField] private Transform tpos;
    [SerializeField] private float span = 8f;
    [SerializeField] private int sponRange = 40;
    private float Cnt = 0f;

    [SerializeField] private Transform parent;
    [SerializeField] private GameObject[] defaultItems;
    private bool isDeleteItem;


    private void Update()
    {
        if (playerAtk.power >= 9)
        {
            if (isDeleteItem)
            {
                return;
            }
            else
            {
                Destroy(gameObject);
                for (int i = 0; i < defaultItems.Length; i++)
                {
                    Destroy(defaultItems[i]);
                }
                isDeleteItem = true;

                return;
            }
        }

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
