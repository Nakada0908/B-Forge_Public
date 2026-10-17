using UnityEngine;
using static DamageInterface;

public class KoumoriParametor : MonoBehaviour,IEnemyDamage
{
    AudioSource koumoribomb;
    public AudioClip dse;
    private Animator koanim;

    private int Enemyhp = 2;
    private int power = 2;
    public bool sDead = false;

    private bool isDead = false;

    void Start()
    {
        koumoribomb = GetComponent<AudioSource>();
        koanim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead) { return; }
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (isDead) { return; }
        if (col.TryGetComponent(out IPlayerDamage target))
        {
            target.TakeDamage(power);
        }
    }

    public void TakeDamage(int damege)
    {
        if (isDead)
        {
            return;
        }
        Enemyhp -= damege;

        if (Enemyhp <= 0)
        {
            Dead();
        }
        else
        {
            koanim.SetBool("Bomb", false);
        }
    }

    private void Dead()
    {
        sDead = true;
        isDead = true;

        //アニメーション再生
        koanim.SetBool("Bomb", true);

        //死亡音
        if (dse != null)
        {
            koumoribomb.PlayOneShot(dse);
        }
        ScoreManager.Instance.AddScore(1);

        //当たり判定をなくす
        Collider2D kou =
            GetComponent<Collider2D>();

        if (kou != null)
        {
            kou.enabled = false;
        }

        Destroy(gameObject, 2f);
    }
}
