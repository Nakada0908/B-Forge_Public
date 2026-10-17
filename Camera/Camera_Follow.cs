using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    public Transform target;
    public float smoothing = 7.0f;//遅れる度合い
    private Vector3 offset;

    [Header("カメラを引く制御")]
    private new Camera camera;
    private Vector2 inputVector;
    [SerializeField] private float cheageCameraSize;
    private float defaultCameraSize;



    void Start()
    {
        offset = transform.position - target.position;
        camera = GetComponent<Camera>();
        defaultCameraSize = camera.orthographicSize;
    }

    void FixedUpdate()
    {
        if (target == null)
        {
            return;
        }

        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(
            transform.position,
            targetCamPos,
            Time.deltaTime * smoothing
        );

        inputVector = target.GetComponent<PlayerMove>().inputVector;
        if (inputVector != Vector2.zero)
        {
            //カメラを引く
            camera.orthographicSize = Mathf.Lerp(
                camera.orthographicSize,
                cheageCameraSize,
                Time.deltaTime * smoothing
            );
        }
        else
        {
            camera.orthographicSize = Mathf.Lerp(
                camera.orthographicSize,
                defaultCameraSize,
                Time.deltaTime * smoothing);
        }
    }
}

