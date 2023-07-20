using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterMovement : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private float speed;
    [SerializeField] private float JumpPower;

    public static float JumpPowerOfSkill = 0;
    private float horizontalInput;
    Animator anim;
    Rigidbody2D myBody;

    [Header("Layer")]
    [SerializeField] private LayerMask groundLayer;

    [Header("Multiple Jumps")]
    [SerializeField]private int extraJump;
    private int jumpCounter;

    private Collider2D collider;
    private float coyoteCounter; // How much time passed since the player ran off the egde
    [SerializeField] private float coyoteTime; // How much time the player can hang in the air before jumping
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Awake()
    {
        anim = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if(horizontalInput < -0.01f || Input.GetKey(KeyCode.A))
        {
            transform.localScale = new Vector3(-1, 1, 1);
            //myBody.AddForce(new Vector2(-speed, 0));
            //myBody.velocity = new Vector3(-speed, 0, 0);
        }else if (horizontalInput > 0.01f || Input.GetKey(KeyCode.D))
        {
            transform.localScale = Vector3.one;
            //myBody.AddForce(new Vector2(speed, 0));
            //myBody.velocity = new Vector3(speed, 0, 0);
        }
        myBody.velocity = new Vector2(horizontalInput*speed, myBody.velocity.y);
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }
        //if ( Input.GetKeyUp(KeyCode.W) || myBody.velocity.y > 0.000f)
        //{
        //    myBody.velocity = new Vector2(myBody.velocity.x, myBody.velocity.y / 2);
        //}
        //if (Input.GetKey(KeyCode.Space))
        //{
        //    Crouch();
        //    anim.SetBool("Crouch", true);

        //}
        anim.SetBool("Grounded", IsGrounded());
        anim.SetBool("Run", horizontalInput != 0);
        
        if (IsGrounded())
        {
            coyoteCounter = coyoteTime;
            jumpCounter = extraJump; // Reset jump counter to extra jump value
        }else
        {
            coyoteCounter -= Time.deltaTime; // Start decreasing coyote conter when not on the ground;
        }
    }
    private void Crouch()
    {
        if (IsGrounded())
        {
            myBody.velocity = new Vector2(speed, myBody.velocity.y);
        }
    }
    private void Jump()
    {
        if (IsGrounded())
        {
            if (JumpPower > JumpPowerOfSkill)
            {
                JumpPowerOfSkill = JumpPower;
            }
            myBody.velocity = new Vector2(myBody.velocity.x, JumpPowerOfSkill);
        }
        coyoteCounter = 0; // reset coyote to 0 avoid double jumps
    }
    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return (raycastHit.collider != null);
    }


    public bool CanAttack()
    {
        return horizontalInput == 0 && IsGrounded();
    }

}
