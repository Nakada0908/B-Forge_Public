using UnityEngine;
using static DamageInterface;

public class MiddleEnemyparametor : MonoBehaviour,IEnemyDamage
{
    AudioSource gorembomb;
    public AudioClip gdse;

    public int MEhp = 80;
    public int MEpower = 5;

    public bool meDead = false;
    private Animator Ganim;

    private bool isDead;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gorembomb = GetComponent<AudioSource>();
        Ganim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) { return; }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(isDead) { return; }
        if (col.TryGetComponent(out IPlayerDamage target))
        {
            target.TakeDamage(MEpower);
        }
    }

    public void TakeDamage(int damege)
    {
        if (isDead) 
        {
            return; 
        }
         MEhp -= damege;

        if (MEhp <= 0)
        {
            Dead();
        }
        else
        {
            Ganim.SetBool("Gbomb", false);
        }
    }

    private void Dead()
    {
        isDead = true;
        meDead = true;

        //Ž€–SƒAƒjƒ[ƒVƒ‡ƒ“
        Ganim.SetBool("Gbomb", true);

        //Ž€–S‰¹
        if (gdse != null)
        {
            gorembomb.PlayOneShot(gdse);
        }

        ScoreManager.Instance.AddScore(60);

        //“–‚½‚è”»’è–³Œø
        Collider2D gorem = GetComponent<Collider2D>();

        if (gorem != null)
        {
            gorem.enabled = false;
        }

        Destroy(gameObject, 2f);
    }
}
