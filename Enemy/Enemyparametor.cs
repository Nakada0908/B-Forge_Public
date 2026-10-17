using NUnit.Framework.Internal;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static DamageInterface;

public class Enemyparametor : MonoBehaviour,IEnemyDamage
{
    AudioSource suraimubomb;
    public AudioClip dse;
    private Animator sanim;

    public int Enemyhp = 2;
    public int power = 2;
    public bool sDead = false;

    private bool isDead = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        suraimubomb = GetComponent<AudioSource>();
        sanim = GetComponent<Animator>();
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
            sanim.SetBool("Bomb", false);
        }
    }

    private void Dead()
    {
        sDead = true;
        isDead = true;

        //アニメーション再生
        sanim.SetBool("Bomb", true);

        //死亡音
        if (dse != null)
        {
            suraimubomb.PlayOneShot(dse);
        }
        ScoreManager.Instance.AddScore(20);

        //当たり判定をなくす
        Collider2D sura =
            GetComponent<Collider2D>();

        if (sura != null)
        {
            sura.enabled = false;
        }

        Destroy(gameObject, 2f);
    }
}
