using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    public float baseSpeed = 5f;
    [SerializeField] private float speedMult = 2f;
    private float speed;

    private InputAction movement;
    private InputAction sprint;
    private Animator animator;

    private AudioSource audioSource;
    [SerializeField] private AudioClip footstepSound;

    private PlayerInputActions playerInputActions;

    private FollowTarget mainCamera;

    public bool dashing;

    void Start()
    {
        playerInputActions = PlayerInputs.Instance.playerInputActions;

        controller = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();

        movement = playerInputActions.Player.Move;
        movement.Enable();

        sprint = playerInputActions.Player.Sprint;
        sprint.Enable();
        

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowTarget>();

        animator = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        movement.Disable();
        sprint.Disable();
    }

    public void PlayFootstep()
    {
        audioSource.PlayOneShot(footstepSound);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection;

        float moveX = movement.ReadValue<Vector2>().x;
        float moveZ = movement.ReadValue<Vector2>().y;

        moveDirection = new Vector3(moveX, 0, moveZ);

        moveDirection = Quaternion.Euler(0, mainCamera.yaw, 0) * moveDirection;

        //Vector3 targetDirection = Vector3.Lerp(moveDirection , moveDirection + transform.position, 0.01f);

        Vector3 targetDirection = moveDirection + transform.position;

        //if angle is 180 snap turn
        float angle = Vector3.Angle(transform.forward, moveDirection);
        if (angle > 90)
        {
            transform.LookAt(targetDirection);
        }
        else
        {
            transform.LookAt(Vector3.Lerp(transform.forward + transform.position, targetDirection, 10f * Time.deltaTime));
        }

        if (playerInputActions.Player.Sprint.IsPressed())
        {
            speed = baseSpeed * speedMult;
        }
        else
        {
            speed = baseSpeed;
        }

        moveDirection += Physics.gravity * 0.1f;

        if(dashing)
            controller.Move(transform.forward * 30 * Time.deltaTime);
        else
            controller.Move(moveDirection * speed * Time.deltaTime);


        animator.SetFloat("Speed", controller.velocity.magnitude);

        if (transform.position.y <= -10)
        {
            transform.position = Vector3.zero;
        }
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