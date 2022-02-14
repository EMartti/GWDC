using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public float jumpSpeed = 10f;
    public float gravity = 20.0f;
    public bool VRMode = false;

    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 turnDirection = Vector3.zero;
    private Vector3 lookDirection = Vector3.zero;
    public float baseSpeed = 1f;

    public Camera cam;
    public GameObject leftController;
    public GameObject rightController;

    private PlayerInputActions playerInputActions;
    private InputAction movement;
    private InputAction turning;
    public Animator animator;
    private AudioSource audioSource;
    [SerializeField] private AudioClip footstepSound;
    [SerializeField] private AudioClip WindupSound;

    private Ray ray;
    private RaycastHit hit;
    private Vector3 targetPointRight;
    private Vector3 targetPointLeft;
    [SerializeField] private int weaponRangeRay;
    private LineRenderer lineRenderer;

    private Transform rightArmFiringPoint;
    private Transform leftArmFiringPoint;

    private AnimationEvent animEvent;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void UpdateLength(GameObject controller)
    {
        lineRenderer.SetPosition(0, controller.transform.position);
        lineRenderer.SetPosition(1, CalculateEnd(controller));
    }

    private Vector3 CalculateEnd(GameObject controller)
    {
        RaycastHit hit = CreateForwardRaycast(controller);
        Vector3 endPosition = DefaultEnd(weaponRangeRay, controller);

        if (hit.collider) endPosition = hit.point;

        return endPosition;
    }

    private Vector3 DefaultEnd(float length, GameObject controller)
    {
        return controller.transform.position + (-controller.transform.up * length);
    }

    private RaycastHit CreateForwardRaycast(GameObject controller)
    {
        RaycastHit hit;
        Ray ray = new Ray(controller.transform.position, -controller.transform.up);

        Physics.Raycast(ray, out hit, weaponRangeRay);
        return hit;
    }

    private void OnEnable()
    {
        movement = playerInputActions.Player.Move;
        movement.Enable();

        turning = playerInputActions.Player.Look;
        turning.Enable();

        playerInputActions.Player.Jump.performed += DoJump;
        playerInputActions.Player.Jump.Enable();
    }

    private void DoJump(InputAction.CallbackContext obj)
    {
        Debug.Log("JUMP");
    }

    private void OnDisable()
    {
        movement.Disable();
        turning.Disable();
        playerInputActions.Player.Jump.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayFootstep()
    {
        audioSource.PlayOneShot(footstepSound);
    }

    public void PlayWindup()
    {
        audioSource.PlayOneShot(WindupSound);
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = new Vector3(movement.ReadValue<Vector2>().x, 0f, movement.ReadValue<Vector2>().y);
        //moveDirection = transform.TransformDirection(moveDirection);
        //turnDirection = new Vector3(0f, turning.ReadValue<Vector2>().x, 0f);


        transform.Rotate(turnDirection * Time.deltaTime);
        controller.Move(moveDirection * baseSpeed * Time.deltaTime);
        float overallSpeed = controller.velocity.magnitude;

        animator.SetFloat("speed", controller.velocity.magnitude);
    }
}