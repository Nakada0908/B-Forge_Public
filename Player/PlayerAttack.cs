using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    private InputSystem_Actions input;

    private GameObject HandgunPrefab;
    private GameObject ShotgunPrefab;
    private Vector2 handgunDefaultScale;
    private Vector2 shotgunDefaultScale;
    [SerializeField] private AudioClip handgunClip;
    [SerializeField] private AudioClip shotgunClip;

    private Animator anime;

    [SerializeField] private GameObject aimImage;

    [HideInInspector] public bool isDead=false;

    //ハンドガンのAutoショット
    [SerializeField] private float autoShotTime = 0.2f;
    private float reShotTime = 0f;

    //アイテムによって変化するパラメータ
    [HideInInspector] public int power;
    [HideInInspector] public int rapidFire;
    [HideInInspector] public int shotgunAngle;
    [HideInInspector] public float distance;

    void Start()
    {
        input = InputManager.Instance.input;
        HandgunPrefab = Resources.Load<GameObject>("PlayerAttack/HandgunPrefab");
        ShotgunPrefab = Resources.Load<GameObject>("PlayerAttack/ShotgunPrefab");
        handgunDefaultScale = HandgunPrefab.transform.localScale;
        shotgunDefaultScale = ShotgunPrefab.transform.localScale;
        anime =GetComponentInChildren<Animator>();
        power = 0;
        rapidFire = 1;
        shotgunAngle = 0;
        distance = 1f;
    }

    void Update()
    {
        if (isDead) { return; }

        DrawAim();

        reShotTime += Time.deltaTime;
        if (autoShotTime <= reShotTime)
        {
            HandgunShot();
            anime.SetTrigger("Shot");
            PlayerSoundManager.Instance.PlaySE(handgunClip);
            reShotTime = 0f;
        }

        if (input.Player.Shotgun.WasPressedThisFrame())
        {
            ShotgunShot();
            anime.SetTrigger("Shot");
            PlayerSoundManager.Instance.PlaySE(shotgunClip);
        }
    }

    private void HandgunShot()
    {
        Quaternion farstAngle = GetRotation();
        float angel = 360 / rapidFire;

        for (int i = 0; i < rapidFire; i++)
        {
            GameObject bullet = Instantiate(HandgunPrefab, transform.position,
                farstAngle * Quaternion.Euler(0, 0, angel * i),this.gameObject.transform);
            bullet.GetComponent<HandgunBullet>().power = power;
        }
    }

    private void ShotgunShot()
    {
        Vector3 direction = GetRotation() * Vector3.right;
        Vector3 spawnPosition = transform.position + direction * distance;

        GameObject bullet = Instantiate(ShotgunPrefab, spawnPosition, GetRotation(), this.gameObject.transform);
        bullet.transform.localScale = 
            new Vector3(shotgunDefaultScale.x + shotgunAngle, shotgunDefaultScale.y + shotgunAngle, 1f);
        bullet.GetComponent<ShotgunBullet>().power = power;
    }

    private Quaternion GetRotation()
    {
        //一旦コントローラーでの入力を優先する
        Vector3 houkou = input.Player.Aim.ReadValue<Vector2>();

        //コントローラーの入力がない場合はマウスの位置を取得する
        if (houkou.sqrMagnitude <= 0.001f&&Mouse.current!=null)
        {
            houkou = Camera.main.ScreenToWorldPoint
                (Mouse.current.position.ReadValue())-transform.position;
        }

        float kakudo = Mathf.Atan2(houkou.y, houkou.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, kakudo);

        return rotation;
    }

    private void DrawAim()
    {
        aimImage.transform.position = transform.position + GetRotation() * Vector3.right * 1f;
        aimImage.transform.rotation = GetRotation();
    }
}