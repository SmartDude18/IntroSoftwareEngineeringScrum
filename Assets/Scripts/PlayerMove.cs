using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{

    private Rigidbody body;
    [SerializeField]
    private InputActionMap actions;
    [SerializeField]
    private GameObject foot,cam;
    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private float groundCheckDistance;



    InputAction moveAction;
    InputAction jumpAction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = transform.GetComponent<Rigidbody>();

        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        

        // 4. Read the "Move" action value, which is a 2D vector
        // and the "Jump" action state, which is a boolean value

        Vector2 moveValue = moveAction.ReadValue<Vector2>();

        // your movement code here
        transform.rotation = Quaternion.Euler(transform.rotation.x, cam.transform.eulerAngles.y, transform.rotation.z);
        body.AddForce(((transform.forward * moveValue.y) + (transform.right * moveValue.x))* moveSpeed, ForceMode.Force);
        //be aware, there is a physics material that is also being used to help make this feel good, using the difference in dynamic and static friction to start fast but also stop fast

        if (jumpAction.IsPressed())
        {
            jump();
        }
    }

    private void jump()
    {
        if(groundCheck())
        {
            body.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
        }
        
    }

    private bool groundCheck()
    {
        RaycastHit hit;
        return (Physics.Raycast(foot.transform.position, Vector3.down, out hit, groundCheckDistance, groundLayer));
        
    }
}
