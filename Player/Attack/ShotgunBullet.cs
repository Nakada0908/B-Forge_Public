using UnityEngine;
using static DamageInterface;

public class ShotgunBullet : MonoBehaviour
{
    [Header("弾の設定")]
    [HideInInspector] public int power = 0;
    [SerializeField] private int defaultPower=1;
    [SerializeField] private float maxTime = 0.5f;

    private float timer = 0f;

    [Header("弾の見た目変更")]
    [SerializeField] private RuntimeAnimatorController changeAnime;
    private int changePower = 9;
    private bool isChanged = false;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= maxTime)
        {
            Destroy(this.gameObject);
        }

        if (isChanged)
        {
            return;
        }

        //一定以上になったら見た目を変える
        if (power >= changePower && !isChanged)
        {
            Animator animator = GetComponent<Animator>();
            animator.runtimeAnimatorController = changeAnime;
            isChanged = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out IEnemyDamage target))
        {
            target.TakeDamage(defaultPower+power);
        }
    }
}