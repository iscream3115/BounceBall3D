using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    public static Movement instance;

    [SerializeField] Transform orientation;
    [SerializeField] Transform player;
    [SerializeField] Transform PlayerObj;
    [SerializeField] Rigidbody rb;
    [SerializeField] private float moveForce = 10f;
    [SerializeField] float airControlMultiplier = 0.5f;
    [SerializeField] float jumpForce = 5.0f;
    [SerializeField] Material MatWhenDash;
    [SerializeField] AudioClip JumpSound;
    [SerializeField] AudioClip DashItemSound;
    [SerializeField] AudioClip DashingSound;
    Vector3 moveDir;
    Vector3 viewDir;
    Vector3 inputDir;

    bool isLaunched = false;

    bool isDashReady = false;
    float DashPowPlayer = 0.0f;
    public float inputCancelThreshold = 0.1f;

    private MeshRenderer MRender;

    private AudioSource AS;
    private Material[] mats;
    private Material TempMat;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        rb.freezeRotation = true;
        MRender = GetComponentInChildren<MeshRenderer>();
        AS = GetComponent<AudioSource>();
        mats = MRender.materials;
    }

    void Update()
    {
        CameraAndControl();


    }

    //카메라 위치에 따른 방향 변화 및 이동 입력
    private void CameraAndControl()
    {
        float horizonInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0;
        right.y = 0;

        forward = forward.normalized;
        right = right.normalized;
        Vector3 moveDirection = (forward * verticalInput + right * horizonInput).normalized;

        rb.AddForce(moveDirection * moveForce * airControlMultiplier, ForceMode.Acceleration);

        if (Input.GetKeyDown(KeyCode.Space) && isDashReady)
        {
            rb.AddForce(moveDirection * DashPowPlayer, ForceMode.Impulse);
            isDashReady = false;
            mats[0] = TempMat;
            MRender.materials = mats;

            AS.PlayOneShot(DashingSound);
        }

        if (isLaunched)
        {
            if (Mathf.Abs(horizonInput) > inputCancelThreshold || Mathf.Abs(verticalInput) > inputCancelThreshold)
            {
                StopLaunch();
            }
        }

    }

    public void StartLaunch(Vector3 force)
    {
        isLaunched = true;
        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero; // 이전 힘 제거
        rb.AddForce(force, ForceMode.VelocityChange);
    }

    public void StopLaunch()
    {
        isLaunched = false;
        rb.useGravity = true;
        rb.linearVelocity = Vector3.zero; // 활강 중이던 힘 제거
    }

    public void ActiveDash(float powerValue)
    {
        isDashReady = true;
        DashPowPlayer = powerValue;
        Debug.Log("공중 대쉬 이용 가능");

        TempMat = mats[0];
        mats[0] = MatWhenDash;
        MRender.materials = mats;

        AS.PlayOneShot(DashItemSound);

    }

    public void ActiveJumpPad(Vector3 Dir, float Pow)
    {
        rb.AddForce(Dir * Pow, ForceMode.Impulse);

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            AS.PlayOneShot(JumpSound);
        }
            
    }

}
