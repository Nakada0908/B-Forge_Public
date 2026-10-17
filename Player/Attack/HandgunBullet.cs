using UnityEngine;
using static DamageInterface;

public class HandgunBullet : MonoBehaviour
{
    [Header("弾の設定")]
    [SerializeField] private float speed = 10f;
    //アイテムによって加算されていくパワー
    [HideInInspector] public int power = 0;
    [SerializeField] private int defaultPower = 3;

    [Header("弾の見た目変更")]
    [SerializeField] private Sprite changeSprite;
    private int lookChangePower = 9;
    private bool isChanged = false;

    private void Update()
    {
        if (isChanged)
        {
            return;
        }

        //一定以上になったら見た目を変える
        if (power >= lookChangePower && !isChanged)
        {
            SpriteRenderer mySP= GetComponent<SpriteRenderer>();
            mySP.sprite= changeSprite;
            isChanged = true;
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    //カメラの範囲外（見えなくなったら）自動で呼ばれる
    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out IEnemyDamage target))
        {
            target.TakeDamage(defaultPower+power);
            Destroy(this.gameObject);
        }
    }
}
