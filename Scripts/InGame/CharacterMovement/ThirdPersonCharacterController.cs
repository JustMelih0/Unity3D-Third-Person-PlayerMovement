using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class ThirdPersonCharacterController : MonoBehaviour
{
    [Header("Main Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private Transform camRef;
    private CharacterController controller;

    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float turnSpeed = 8f;
    [SerializeField] private float smoothTransitionSpeed = 12f;
    private float horizontalSpeed;

    [Header("Gravity & Jump Settings")]
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float gravityScaler = 2f;  
    [SerializeField] private float jumpHeight = 2f; 
    private float verticalVelocity;  

    [Header("Input")]
    private float moveInputVertical;
    private float moveInputHorizontal;
    private float currentSpeed;

    [Header("Dance")]
    private bool dancing = false;



    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        InputManagement();  
        Anime();
        Movement();  
        ApplyGravity();  
    }

    private void InputManagement()
    {
        moveInputVertical = Input.GetAxis("Vertical");  
        moveInputHorizontal = Input.GetAxis("Horizontal"); 
    

        currentSpeed = (Input.GetKey(KeyCode.LeftShift)) ? runSpeed : walkSpeed;

        if (Input.GetKeyDown(KeyCode.R)) dancing = !dancing;

    }

    private void Movement()
    {
        GroundMovement();
        TurnCharacter();
    }
    private void Anime()
    {
        float targetHorizontalSpeed = Mathf.Abs(moveInputHorizontal) + Mathf.Abs(moveInputVertical);
        targetHorizontalSpeed = Mathf.Clamp(targetHorizontalSpeed, 0, 1);
        targetHorizontalSpeed *= currentSpeed;
        horizontalSpeed = Mathf.MoveTowards(horizontalSpeed, targetHorizontalSpeed, smoothTransitionSpeed * Time.deltaTime);

        if(targetHorizontalSpeed == 0 && horizontalSpeed < 0.1) horizontalSpeed = 0;


        animator.SetFloat("horizontalSpeed", horizontalSpeed);
        animator.SetBool("dance", dancing);
    }

    private void GroundMovement()
    {
        Vector3 move = new (moveInputHorizontal, 0, moveInputVertical);
        move = camRef.TransformDirection(move);  
        move *= currentSpeed;  
        move.y = verticalVelocity;

        controller.Move(move * Time.deltaTime);  
    }

    private void TurnCharacter()
    {
        if (Mathf.Abs(moveInputHorizontal) > 0 || Mathf.Abs(moveInputVertical) > 0)
        {
            Vector3 direction = new(moveInputHorizontal, 0, moveInputVertical);
            direction = camRef.TransformDirection(direction);
            direction.y = 0;  

            if (direction.magnitude > 0)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);  
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
            }
        }
    }

    private void ApplyGravity()
    {
        if (controller.isGrounded)
        {
            verticalVelocity = -2f;  
            if (Input.GetButtonDown("Jump"))
            {
                animator.SetTrigger("jump");
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);  
            }
        }
        else
        {
            verticalVelocity += gravity * gravityScaler * Time.deltaTime;  

            
        }
    }
}
