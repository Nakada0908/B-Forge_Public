using NUnit.Framework.Internal;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static DamageInterface;

public class TokageEnemyparametor : MonoBehaviour,IEnemyDamage
{
    AudioSource tokagebomb;
    public AudioClip tose;

    public int TOhp = 150;
    public int TOpower = 10;

    public bool ToDead = false;
    private Animator Tanim;

    private bool isDead = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tokagebomb = GetComponent<AudioSource>();
        Tanim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) { return; }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (isDead) { return; }
        if (col.TryGetComponent(out IPlayerDamage target))
        {
            target.TakeDamage(TOpower);
        }
    }

    public void TakeDamage(int damege)
    {
        if (isDead)
        {
            return;
        }
        TOhp -= damege;

        if (TOhp <= 0)
        {
            Dead();
        }
        else
        {
            Tanim.SetBool("Def", false);
        }
    }

    private void Dead()
    {
        isDead = true;
        ToDead = true;

        //Ž€–SƒAƒjƒ[ƒVƒ‡ƒ“
        Tanim.SetBool("Def", true);

        //Ž€–S‰¹
        if (tose != null)
        {
            tokagebomb.PlayOneShot(tose);
        }

        ScoreManager.Instance.AddScore(60);

        //“–‚½‚è”»’è–³Œø
        Collider2D tokage = GetComponent<Collider2D>();

        if (tokage != null)
        {
            tokage.enabled = false;
        }

        Destroy(gameObject, 2f);
    }
}
