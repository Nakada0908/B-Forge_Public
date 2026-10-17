using UnityEngine;
using static DamageInterface;

public class DragonEnemymove : MonoBehaviour
{
    AudioSource dragonwalk;
    public AudioClip drase;
    Transform targetpos;
    private Rigidbody2D rb;
    private Animator Anim;
    private DragonEnemyparametor dragonenemyparametor;
    public float speed = 5f;


    private bool doraDead = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dragonwalk = GetComponent<AudioSource>();
        Anim = GetComponent<Animator>();
        dragonenemyparametor = GetComponent<DragonEnemyparametor>();
        targetpos = GameObject.Find("Player").transform;
        dragonwalk.Play();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetpos == null)
        {
            return;
        }

        if (doraDead == false)
        {
            //アニメーション制御
            if (targetpos.position.x < transform.position.x)
            {
                Anim.SetBool("WalkL", true);
                Anim.SetBool("WalkR", false);
            }
            else
            {
                Anim.SetBool("WalkR", true);
                Anim.SetBool("WalkL", false);
            }
        }
        else
        {
            Anim.SetBool("WalkL", false);
            Anim.SetBool("WalkR", false);
        }


        if (doraDead)
        {
            return;
        }

        if (dragonenemyparametor.doraDead == true)
        {
            doraDead = true;
        }
    }
    void FixedUpdate()
    {
        // 死んでいたら移動停止
        if (doraDead)
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
