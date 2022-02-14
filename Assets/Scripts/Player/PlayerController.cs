using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public float jumpSpeed = 10f;
    public float gravity = 20.0f;

    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 turnDirection = Vector3.zero;
    private Vector3 lookDirection = Vector3.zero;
    public float baseSpeed = 10f;

    private InputAction movement;
    private Animator animator;
    private AudioSource audioSource;
    [SerializeField] private AudioClip footstepSound;
    [SerializeField] private AudioClip WindupSound;

    private AnimationEvent animEvent;

    private PlayerInputActions playerInputActions;

    private GameObject camera;

    private void Awake()
    {
        //playerInputActions = PlayerInputs.Instance.playerInputActions;
    }

    void Start()
    {
        playerInputActions = PlayerInputs.Instance.playerInputActions;

        controller = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();

        movement = playerInputActions.Player.Move;
        movement.Enable();

        camera = GameObject.FindGameObjectWithTag("MainCamera");

        animator = GetComponent<Animator>();
    }

    //private void OnEnable()
    //{
    //    movement = playerInputActions.Player.Move;
    //    movement.Enable();
    //}

    private void OnDisable()
    {
        movement.Disable();
    }

    public void PlayFootstep()
    {
        audioSource.PlayOneShot(footstepSound);
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = new Vector3(movement.ReadValue<Vector2>().x, 0f, movement.ReadValue<Vector2>().y);
        //moveDirection = transform.TransformDirection(moveDirection);
        //turnDirection = new Vector3(0f, turning.ReadValue<Vector2>().x, 0f);


        controller.Move(moveDirection * baseSpeed * Time.deltaTime);
        float overallSpeed = controller.velocity.magnitude;

        animator.SetFloat("speed", controller.velocity.magnitude);
    }
    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(false);
    }

    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}