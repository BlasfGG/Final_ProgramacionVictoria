using UnityEngine;

public class PLAYERMOVEMENT : MonoBehaviour
{
    
    [SerializeField] private float jumpForce = 5.3f;
  
    [SerializeField] private float crouchSpeed = 4;
    [SerializeField] private float walkspeed = 6;
    [SerializeField] private float runSpeed = 8;

    //private Respawn respawn;

    private Rigidbody rb; 





    private void Start()
    {
        //respawn = FindObjectOfType<Respawn>();

        rb = rb == null ? GetComponent<Rigidbody>() : rb;
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        Debug.Log(HorizontalAxis());
        Debug.Log(Speed());
        Move();
        Jump();
    }

  
    private void Move() 
    {


        rb.velocity = transform.rotation * new Vector3(HorizontalAxis(), rb.velocity.y, VerticalAxis()) * Speed(); 
    }

 
    public float Speed()
    {
        if (RunInputPressed())
        {
            return runSpeed;
        }
        else if (CrouchInputPressed())
        {
            return crouchSpeed;
        }

        return RunInputPressed() ? runSpeed : CrouchInputPressed() ? crouchSpeed : walkspeed;
    }

    private void Jump()
    {
        if (JumpInputPressed())
        {
            Debug.Log("Saltando");
            rb.AddForce(Vector3.up * jumpForce);
        }
    }

    private float HorizontalAxis()
    {
        return Input.GetAxis("Horizontal"); 
    }

    private float VerticalAxis()
    {
        return Input.GetAxis("Vertical"); 
    }

    public bool JumpInputPressed()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    public bool RunInputPressed()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }

    public bool CrouchInputPressed()
    {
        return Input.GetKey(KeyCode.LeftControl);
    }

        

        






}
