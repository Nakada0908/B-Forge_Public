using UnityEngine;

public class HandgunLevelUpCreate : MonoBehaviour
{
    [SerializeField] private PlayerAttack playerAttack;
    private int maxRapidFire = 12;
    [SerializeField] private GameObject item;
    [SerializeField] private Transform tpos;
    [SerializeField] private float span = 8f;
    [SerializeField] private int sponRange = 40;
    private float Cnt = 0f;

    [SerializeField] private Transform parent;
    private bool isDeleteItem;

    private void Update()
    {
        if(playerAttack.rapidFire >= maxRapidFire)
        {
            if (isDeleteItem)
            {
                return;
            }
            else
            {
                Destroy(gameObject);
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
