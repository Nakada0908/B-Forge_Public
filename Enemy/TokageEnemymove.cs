using UnityEngine;
using static DamageInterface;

public class TokageEnemymove : MonoBehaviour
{
    AudioSource tokagewalk;
    public AudioClip se;

    Transform targetpos;
    private Rigidbody2D rb;
    private Animator Anim;
    private TokageEnemyparametor tokageenemyparametor;
    public float speed = 8f;

    
    private bool tokageDead = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tokagewalk = GetComponent<AudioSource>();
        Anim = GetComponent<Animator>();
        tokageenemyparametor = GetComponent<TokageEnemyparametor>();
        targetpos = GameObject.Find("Player").transform;
        tokagewalk.Play();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!tokageDead && !tokagewalk.isPlaying)
        {
            Debug.Log("トカゲの歩行音が止まってる！");
            tokagewalk.Play();
        }
        if (targetpos == null)
        {
            return;
        }

        if (tokageDead == false)
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

        if (tokageDead)
        {
            return;
        }

        if (tokageenemyparametor.ToDead == true)
        {
            tokageDead = true;
        }
    }
    void FixedUpdate()
    {
        // 死んでいたら移動停止
        if (tokageDead)
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
