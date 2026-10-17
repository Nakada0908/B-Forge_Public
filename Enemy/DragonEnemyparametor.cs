using NUnit.Framework.Internal;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static DamageInterface;
public class DragonEnemyparametor : MonoBehaviour,IEnemyDamage
{
    AudioSource dragonbomb;
    public AudioClip dorase;
    private Animator doanim;
    Transform targetpos;

    public int Dragonhp = 4000;
    public int power = 50;
    public bool doraDead = false;

    private bool isDead = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dragonbomb = GetComponent<AudioSource>();
        doanim = GetComponent<Animator>();
        targetpos = GameObject.Find("Player").transform;
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
        Dragonhp -= damege;

        if (Dragonhp <= 0)
        {
            Dead();
        }
        else
        {
            doanim.SetBool("Bomb", false);
        }
    }

    private void Dead()
    {
        doraDead = true;
        isDead = true;

        //アニメーション制御
        if (targetpos.position.x < transform.position.x)
        {
            doanim.SetBool("DefL", true);
            doanim.SetBool("DefR", false);
        }
        else
        {
            doanim.SetBool("DefR", true);
            doanim.SetBool("DefL", false);
        }
        //死亡音
        if (dorase != null)
        {
            dragonbomb.PlayOneShot(dorase);
        }

        ScoreManager.Instance.AddScore(6500);

        //当たり判定をなくす
        Collider2D dora =
            GetComponent<Collider2D>();

        if (dora != null)
        {
            dora.enabled = false;
        }

        Destroy(gameObject, 4f);
    }
}
