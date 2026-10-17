using UnityEngine;
using static DamageInterface;

public class MIddleEnemymove : MonoBehaviour
{
    AudioSource goremwalk;
    public AudioClip gse;
    Transform targetpos;
    private Rigidbody2D rb;
    private Animator Anim;
    private MiddleEnemyparametor middle;
    public float speed = 5f;


    private bool goremDead = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        goremwalk = GetComponent<AudioSource>();
        Anim = GetComponent<Animator>();
        middle = GetComponent<MiddleEnemyparametor>();
        targetpos = GameObject.Find("Player").transform;
        goremwalk.Play();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetpos == null)
        {
            return;
        }

        if (goremDead == false)
        { 
            //アニメーション制御
            if (targetpos.position.x < transform.position.x)
            {
                Anim.SetBool("WalkL", false);
                Anim.SetBool("WalkR", true);
            }
            else
            {
                Anim.SetBool("WalkR", false);
                Anim.SetBool("WalkL", true);
            }
        }
        else
        {
            Anim.SetBool("WalkL", false);
            Anim.SetBool("WalkR", false);
        }

        if (goremDead)
        {
            return;
        }

        if (middle.meDead == true)
        {
            goremDead = true;
        }
    }
    void FixedUpdate()
    {
        // 死んでいたら移動停止
        if (goremDead)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (targetpos == null)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 direction =
            ((Vector2)targetpos.position - rb.position).normalized;

        rb.linearVelocity = direction * speed;
    }
}
