using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject MiddleEnemy;
    public GameObject TokageEnemy;
    public GameObject SinigamiEnemy;
    public GameObject Koumori;
    public GameObject DragonEnemy;
    public Transform tpos;

    private bool Enemystrengthenphase1 = false;
    private bool Enemystrengthenphase2 = false;
    private bool Enemystrengthenphase3 = false;
    private bool Enemystrengthenphase4 = false;
    private bool Enemystrengthenphase5 = false;

    private Enemyparametor enemypar;
    private MiddleEnemyparametor midpar;
    private TokageEnemyparametor tokapar;
    private SinigamiEnemyparametor sinipar;
    private KoumoriParametor koupar;
    private DragonEnemyparametor drapar;

    float span =  3f;
    float productionCnt = 0f;
    float Cnt = 0f;

    float couspan = 0.5f;
    float couCnt = 0f;

    float Mspan = 10.0f;
    float Mcnt = 0f;

    float Tspan = 10.0f;
    float Tcnt = 0f;

    float Sispan = 5.0f;
    float Sicnt = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemypar = Enemy.GetComponent<Enemyparametor>();
        midpar = MiddleEnemy.GetComponent<MiddleEnemyparametor>();
        tokapar = TokageEnemy.GetComponent<TokageEnemyparametor>();
        sinipar = SinigamiEnemy.GetComponent<SinigamiEnemyparametor>();
        koupar = Koumori.GetComponent<KoumoriParametor>();
        drapar = DragonEnemy.GetComponent<DragonEnemyparametor>();
    }

    // Update is called once per frame
    void Update()
    {
        Cnt += Time.deltaTime;
        Mcnt += Time.deltaTime;
        Tcnt += Time.deltaTime;
        Sicnt += Time.deltaTime;
        couCnt += Time.deltaTime;
        productionCnt += Time.deltaTime;

        //フェーズ移行
        if (productionCnt >= 60.0f && !Enemystrengthenphase1)
        {
            Enemyphase1();
            Debug.Log("一度だけ呼ばれたよ");
            Enemystrengthenphase1 = true;
        }

        if (productionCnt >= 120.0f && !Enemystrengthenphase2)
        {
            Enemyphase2();
            Debug.Log("一度だけ呼ばれたよ2フェーズ");
            Enemystrengthenphase2 = true;
        }

        if (productionCnt >= 180.0f && !Enemystrengthenphase3)
        {
            Enemyphase3();
            Debug.Log("一度だけ呼ばれたよ3フェーズ");
            Enemystrengthenphase3 = true;
        }

        if (productionCnt >= 240.0f && !Enemystrengthenphase4)
        {
            Enemyphase4();
            Debug.Log("一度だけ呼ばれたよ4フェーズ");
            Enemystrengthenphase4 = true;
        }

        if (productionCnt >= 300.0f && !Enemystrengthenphase5)
        {
            Enemyphase5();
            Debug.Log("一度だけ呼ばれたよ5フェーズ");
            Enemystrengthenphase5 = true;
        }

        //スライム生産
        if (Cnt > span)
        {
            Cnt = 0f;
            Instantiate(Enemy,
                new Vector2(tpos.transform.position.x + Random.Range(-50, 50),
                tpos.transform.position.y + Random.Range(-50, 50)),
                Quaternion.identity);
        }
        //コウモリ生産
        if (couCnt > couspan && productionCnt >= 60f)
        {
            couCnt = 0f;
            Instantiate(Koumori,
                new Vector2(tpos.transform.position.x + Random.Range(-50, 50),
                tpos.transform.position.y + Random.Range(-50, 50)),
                Quaternion.identity);
        }
        //ゴーレム生産
        if (Mcnt > Mspan && productionCnt >= 60f )
        {
            Mcnt = 0f;
            Instantiate(MiddleEnemy,
                new Vector2(tpos.transform.position.x + Random.Range(-50, 50),
                tpos.transform.position.y + Random.Range(-50, 50)),
                Quaternion.identity);
        }
        //トカゲ竜生産
        if (Tcnt > Tspan && productionCnt >= 120f)
        {
            Tcnt = 0f;
            Instantiate(TokageEnemy,
                new Vector2(tpos.transform.position.x + Random.Range(-50, 50),
                tpos.transform.position.y + Random.Range(-50, 50)),
                Quaternion.identity);
        }
        //死神生産
        if (Sicnt > Sispan && productionCnt >= 240.0f)
        {
            Sicnt = 0f;
            Instantiate(SinigamiEnemy,
                new Vector2(tpos.transform.position.x + Random.Range(-50, 50),
                tpos.transform.position.y + Random.Range(-50, 50)),
                Quaternion.identity);
        }

        //デバッグ用操作　フェーズ移行
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            productionCnt = 60f;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            productionCnt = 120f;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            productionCnt = 180f;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            productionCnt = 240f;
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            productionCnt = 300f;
        }
    }

    private void Enemyphase1()
    {
        span = 1.5f;

        //このフェーズでゴーレムが出現し始め、スライムが強化される
        //このフェーズでのみコウモリが出てくる
        enemypar.Enemyhp = 4;
        enemypar.power = 3;
    }

    private void Enemyphase2()
    {
        span = 1f;
        Mspan = 5.0f;

        //このフェーズでトカゲ竜が出現し始め、スライムとゴーレムが強化される
        enemypar.Enemyhp = 6;
        enemypar.power = 4;
        midpar.MEhp = 100;
        midpar.MEpower = 10;
    }

    private void Enemyphase3()
    {
        Mspan = 2.5f;
        Tspan = 2.5f;

        //このフェーズでは三体すべてが強化される
        enemypar.Enemyhp = 8;
        enemypar.power = 6;
        midpar.MEhp = 120;
        midpar.MEpower = 15;
        tokapar.TOhp = 200;
        tokapar.TOpower = 20;
    }

    private void Enemyphase4()
    {
        //このフェーズで死神とドラゴン出現
        //トカゲとゴーレム強化
        midpar.MEhp = 150;
        tokapar.TOpower = 25;

        Instantiate(DragonEnemy,
                 new Vector2(tpos.transform.position.x + Random.Range(-50, 50),
                 tpos.transform.position.y + Random.Range(-50, 50)),
                 Quaternion.identity);
    }

    private void Enemyphase5()
    {
        //このフェーズでは基本的に生きるのが不可能に近い
        //死神出現頻度とトカゲ出現頻度が上昇
        Sispan = 2f;
        Tspan = 1f;

        sinipar.Enemyhp = 350;
        sinipar.power = 15;
        tokapar.TOhp = 250;
        drapar.Dragonhp = 10000;
        

        Instantiate(DragonEnemy,
                 new Vector2(tpos.transform.position.x + Random.Range(-50, 50),
                 tpos.transform.position.y + Random.Range(-50, 50)),
                 Quaternion.identity);
    }
}
