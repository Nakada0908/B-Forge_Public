using UnityEngine;
using static DamageInterface;

public class ItemRecovery : MonoBehaviour
{
    [SerializeField] private int recoveryHp = 5;

    [SerializeField] private AudioClip itemGetClip;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerStates target = other.GetComponent<PlayerStates>();
        if (target != null)
        {
            PlayerSoundManager.Instance.PlaySE(itemGetClip);

            //HPが最大なら回復しない
            if (target.hp >= target.maxHp)
            {
                Destroy(gameObject);
                return;
            }

            target.hp += recoveryHp;

            //最大HPを超えないようにする
            if (target.hp > target.maxHp)
            {
                target.hp = target.maxHp;
            }

            //HPバーの更新を-1ダメージでさせる
            if (other.TryGetComponent(out IPlayerDamage retarget))
            {
                retarget.TakeDamage(-1);
            }

            Destroy(gameObject);
        }
    }
}
