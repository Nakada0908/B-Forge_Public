using UnityEngine;

public class SinigamiEnemymove : MonoBehaviour
{
    AudioSource sinigamiwalk;
    public AudioClip se;
    Transform targetpos;
    private Rigidbody2D rb;
    private Animator Anim;
    private SinigamiEnemyparametor sinigamienemyparametor;
    public float speed = 10f;


    private bool siniDead = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sinigamiwalk = GetComponent<AudioSource>();
        Anim = GetComponent<Animator>();
        sinigamienemyparametor = GetComponent<SinigamiEnemyparametor>();
        targetpos = GameObject.Find("Player").transform;
        sinigamiwalk.Play();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetpos == null)
        {
            return;
        }

        if (siniDead == false)
        {
            //アニメーション制御
            if (targetpos.position.x < transform.position.x)
            {
                Anim.SetBool("WalkR", true);
                Anim.SetBool("WalkL", false);
            }
            else
            {
                Anim.SetBool("WalkL", true);
                Anim.SetBool("WalkR", false);
            }
        }
        else
        {
            Anim.SetBool("WalkL", false);
            Anim.SetBool("WalkR", false);
        }


        if (siniDead)
        {
            return;
        }

        if (sinigamienemyparametor.SiDead == true)
        {
            siniDead = true;
        }
    }
    void FixedUpdate()
    {
        // 死んでいたら移動停止
        if (siniDead)
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
