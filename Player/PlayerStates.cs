using Unity.VisualScripting;
using UnityEngine;
using static DamageInterface;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStates : MonoBehaviour,IPlayerDamage
{
    [Header("プレイヤーのステータス")]
    public int hp = 15;
    [SerializeField] private float maxUnHitTime = 0.5f;

    //無敵時間
    private float unHitTimer = 0;
    private bool isUnHit = false;

    [Header("HPバーのイメージ")]
    [SerializeField] private Image hpBarImage;
    public int maxHp { get; private set; }

    private Animator anime;

    private bool isDead=false;
    [SerializeField] private AudioClip deadClip;

    //アイテムによる無敵
    private bool _isItemUnHit = false;
    public bool isItemUnHit
    {
        get => _isItemUnHit;
        set
        {
            _isItemUnHit = value;
            baria.SetActive(value);
        }
    }
    [SerializeField] private GameObject baria;
    [SerializeField] private AudioClip BreakBariaClip;

    void Start()
    {
        isItemUnHit = false;
        maxHp = hp;
        anime = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if(isUnHit)
        {
            unHitTimer += Time.deltaTime;

            if(unHitTimer>=maxUnHitTime)
            {
                isUnHit = false;
                unHitTimer = 0;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) {  return; }

        //バリアによる１回無効
        if(isItemUnHit&&damage>=0) 
        {
            PlayerSoundManager.Instance.PlaySE(BreakBariaClip);
            isItemUnHit = false;
            return; 
        }


        if (isUnHit) { return; }

        isUnHit = true;
        Debug.Log("いたい");
        hp -= damage;
        //HPバーの更新
        hpBarImage.fillAmount = (float)hp / maxHp;

        if (hp <= 0)
        {
            PlayerSoundManager.Instance.PlaySE(deadClip);
            isDead = true;
            anime.SetTrigger("Dead");

            //いろんなコンポーネントを無効化する
            var pm = GetComponent<PlayerMove>();
            pm.isDead=true;
            pm.speed = 0;
            var pa=GetComponent< PlayerAttack >();
            pa.isDead=true;
            
            SaveScoreManager.Instance.SaveScore();
            hpBarImage.fillAmount = (float)hp / maxHp;
            Invoke("IsDead", 3f);
        }
    }


    private void IsDead()
    {
        SceneManager.LoadScene("Result");
    }

    public void Baria()
    {
        baria.SetActive(true);
    }
}
