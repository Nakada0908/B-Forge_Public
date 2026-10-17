using UnityEngine;

public class KoumoriMove : MonoBehaviour
{
    AudioSource suraimuwalk;
    public AudioClip se;
    Transform targetpos;
    private Rigidbody2D rb;
    private Animator Anim;
    private KoumoriParametor enemyparametor;
    private float speed = 10f;


    private bool suraDead = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        suraimuwalk = GetComponent<AudioSource>();
        Anim = GetComponent<Animator>();
        enemyparametor = GetComponent<KoumoriParametor>();
        targetpos = GameObject.Find("Player").transform;
        suraimuwalk.Play();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetpos == null)
        {
            return;
        }

        if (suraDead == false)
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


        if (suraDead)
        {
            return;
        }

        if (enemyparametor.sDead == true)
        {
            suraDead = true;
        }
    }
    void FixedUpdate()
    {
        // 死んでいたら移動停止
        if (suraDead)
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
