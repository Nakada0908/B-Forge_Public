using NUnit.Framework.Internal;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static DamageInterface;

public class SinigamiEnemyparametor : MonoBehaviour,IEnemyDamage
{
    AudioSource sinigamibomb;
    public AudioClip sinise;
    private Animator sinianim;

    public int Enemyhp = 200;
    public int power = 30;
    public bool SiDead = false;

    private bool isDead = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sinigamibomb = GetComponent<AudioSource>();
        sinianim = GetComponent<Animator>();
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
            sinianim.SetBool("Def", false);
        }
    }

    private void Dead()
    {
        SiDead = true;
        isDead = true;

        //アニメーション再生
        sinianim.SetBool("Def", true);

        //死亡音
        if (sinise != null)
        {
            sinigamibomb.PlayOneShot(sinise);
        }
        ScoreManager.Instance.AddScore(60);

        //当たり判定をなくす
        Collider2D sini =
            GetComponent<Collider2D>();

        if (sini != null)
        {
            sini.enabled = false;
        }

        Destroy(gameObject, 2f);
    }
}
