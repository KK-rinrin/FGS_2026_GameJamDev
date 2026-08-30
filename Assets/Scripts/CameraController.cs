using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CinemachineCamera cinemachineCamera;

    [Header("ƒJƒƒ‰‚Ì“®‚­ŠÔ")]
    [SerializeField] private float kCameraMoveTime = 0.1f;

    private float currentRot;
    private float targetRot;

    private float time = 0.0f;
    private bool isInversing = false;
    public void Inverse()
    {
        currentRot = cinemachineCamera.transform.localEulerAngles.z;
        targetRot += 180.0f;

        time = 0.0f;

        isInversing = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cinemachineCamera = GetComponent<CinemachineCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isInversing)
        {
            return;
        }

        time += Time.deltaTime;

        float rate = Mathf.Clamp01(time / kCameraMoveTime);

        rate = Mathf.SmoothStep(0.0f, 1.0f, rate);

        float rotZ = Mathf.LerpAngle(currentRot, targetRot, rate);

        cinemachineCamera.transform.localEulerAngles = new Vector3(0.0f, 0.0f,rotZ);

        if (time >= kCameraMoveTime)
        {
            isInversing = false;
            currentRot = targetRot;
        }
    }
}
