using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour

{
    #region Singleton
    public static PlayerController instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion


    Rigidbody2D RB;
    SpriteRenderer Renderer;
    Animator Anim;
    bool facingRight = true;


    public float maxSpeed;

    public bool grounded = false;
    float groundCheckRadius = 0.5f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpPower;
    float xMovement, yMovement = 0;
    public bool onLadder;

    public Transform ceilPoint;
    private bool ceiled;

    private float crouch;
    public bool crouching;

    public AudioSource jump;
    


    //private bool attack;


    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        Renderer = GetComponent<SpriteRenderer>();
        Anim = GetComponent<Animator>();
        
    }

    

    //private void HandleAttacks()
    //{
    //    if(attack)
    //    {
    //        Anim.SetTrigger("true");
    //    }
    //}

    //private void HandleInput()

    //{
    //    if (Input.GetKeyDown(KeyCode.Mouse0))
    //    {
    //        attack = true;
    //    }
    //}
    //// Update is called once per frame
    void Update()
    {
        //{
        //    HandleInput();
        //}

        if (grounded && Input.GetAxis("Jump")>0)
        {
            jump.Play();
            Anim.SetBool("isGrounded", false);
            RB.velocity = new Vector2(RB.velocity.x, 0f);
            RB.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            grounded = false;
        }

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        Anim.SetBool("isGrounded", grounded);

        ceiled = Physics2D.OverlapCircle(ceilPoint.position, groundCheckRadius, groundLayer);
        Anim.SetBool("isGrounded", grounded);

        crouch = Input.GetAxis("CrouchInput");
        CrouchFunction();
        Anim.SetBool("Crouch", crouching);

        float move = Input.GetAxis("Horizontal");

        if (move > 0 && !facingRight)
            Flip();
        else if(move<0&&facingRight)
            Flip();

        RB.velocity = new Vector2(move * maxSpeed, RB.velocity.y);
        Anim.SetFloat("MoveSpeed", Mathf.Abs(move));
    }


    private void FixedUpdate()
    {
        if (yMovement > 0 && grounded && !onLadder)
        {
            RB.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);
            grounded = false;
            Anim.SetBool("Jumping", true);
        }
        else if (yMovement > 0 && onLadder)
        {
            transform.Translate(Vector3.up * yMovement * (maxSpeed / 2) * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //HandleAttacks();
        //ResetValues();

        if (collision.tag == "Ladder")
        {
            onLadder = true;
            RB.gravityScale = 0;
        }
        //HandleAttacks();
        //ResetValues();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ladder")
        {
            onLadder = false;
            RB.gravityScale = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("LevelEnd"))
        {
            SceneManager.LoadScene("Map");
        }
        switch (collision.transform.tag)
        {
            case ("Gun"):
                Anim.SetBool("Armed", true);
                GetComponent<Weapon>().hasWeapon = true;
                Destroy(collision.gameObject);
                break;
            case ("Enemy"):
                Anim.SetBool("Armed", false);
                break;
        }
    }
    

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
       // Renderer.flipX = !Renderer.flipX;
    }
    //private void ResetValues()
    //{
    //    attack = false;
    //}

    void CrouchFunction()
    {
       if(crouch!=0||ceiled==true && grounded == true)
        {
            crouching = true;
        }
       else
        {
            crouching = false;
        }
    }
}
