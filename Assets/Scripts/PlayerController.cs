
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //controle movimento
    public float moveSpeed;
    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    public KeyCode crouch;
    public KeyCode trow_snowball;

    //pulo
    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask whatIsGrounded;
    public bool isGrounded;

    public float jumpForce;
    public int jumpLimit = 3;
    public int jumpCount = 1;

    //componentes
    private Rigidbody2D theRB;
    private Animator anim;
    private BoxCollider2D collid;

    //entidades
    public GameObject snowBall;
    public Transform trowPoint;

    //audio
    public AudioSource trow_snd;
    public AudioSource jump_snd;

    //dash
    public float tapReload = 0.5f; //segundos
    private float lastTapTime = 0;
    public float dashSpeed = 1;
    public float dashSpeedMulti;
    public bool dashReady = true;
    public float dashDuration;
    private float dashTime = 0;
    public string[] last_taps;
    List<string> lol = new List<string>();

    // Use this for initialization
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collid = GetComponent<BoxCollider2D>();
        lastTapTime = 0;
    }


    // Update is called once per framee
    void Update()
    {
        //GET KEYS------------
        //Controles
        if (Input.GetKey(right))
        {
            theRB.velocity = new Vector2(moveSpeed * dashSpeed, theRB.velocity.y);
        }

        if (Input.GetKey(left))
        {
            theRB.velocity = new Vector2(-moveSpeed * dashSpeed, theRB.velocity.y);
        }
        
        if (Input.GetKeyDown(crouch))
        {
            anim.SetBool("crouch", true);
            collid.offset = new Vector2(0.03128839f, -0.1578631f);
            collid.size = new Vector2(0.802074f, 0.5678854f);
        }

        //GetKeysDown--------------------------------
        if (Input.GetKeyDown(right) || (Input.GetKeyDown(left)))
        {
            bool pressedRight = Input.GetKeyDown(right);

            //keep dash direction
            last_taps[1] = last_taps[0];
            last_taps[0] = pressedRight ? "right" : "left";

            if (dashReady && (Time.time - lastTapTime) < tapReload && (last_taps[0] == last_taps[1]))
            {
                if (!isGrounded)
                {
                    theRB.gravityScale = 0;
                    theRB.velocity = new Vector2(dashSpeed * moveSpeed, 0);
                }

                trow_snd.Play();
                dashSpeed = dashSpeedMulti;
                dashReady = false;
                StartCoroutine(dashReloadRoutine());
            }
            lastTapTime = Time.time;

            if (pressedRight)
                theRB.velocity = new Vector2(moveSpeed * dashSpeed, theRB.velocity.y);
            else
                theRB.velocity = new Vector2(-moveSpeed * dashSpeed, theRB.velocity.y);
        }

        //duracao dash
        if (dashTime > dashDuration)
        {
            dashSpeed = 1;
            dashTime = 0;
            theRB.gravityScale = 10;
        }
        if (dashSpeed == dashSpeedMulti)
        {
            dashTime += Time.deltaTime;
        }


        //GET KEYS UP--------------------------------
        //removendo a desaceleracao do player e parando instantaneo;
        //Caso queria que o boneco pare aos poucos, remover o bloco abaixo
        if (Input.GetKeyUp(left) || Input.GetKeyUp(right))
        {
            theRB.velocity = new Vector2(0, theRB.velocity.y);
            dashSpeed = 1;
            theRB.gravityScale = 5;
        }


        if (Input.GetKeyUp(crouch))
        {
            collid.offset = new Vector2(0f, -0.03f);
            collid.size = new Vector2(0.63f, 0.82f);
            anim.SetBool("crouch", false);
        }


        //ground_jump_check
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGrounded);
        if (Input.GetKeyDown(jump) && jumpCount < jumpLimit)
        {
            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
            jump_snd.Play();
            jumpCount += 1;
        }
        anim.SetFloat("speed", Mathf.Abs(theRB.velocity.x));
        anim.SetBool("grounded", isGrounded);
        if (isGrounded)
        {
            jumpCount = 1;
        }


        //revert_position
        if (theRB.velocity.x < 0 && transform.localScale.x > -1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (theRB.velocity.x > 0 && transform.localScale.x < 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }


        //trow snow ball
        if (Input.GetKeyDown(trow_snowball))
        {
            Debug.Log("Hello", snowBall);
            GameObject ballClone = (GameObject)Instantiate(snowBall, trowPoint.position, trowPoint.rotation);
            //ballClone = 3;
            ballClone.transform.localScale = transform.localScale;
            anim.SetTrigger("snowball");
            trow_snd.Play();
        }
    }//----------------------------------------------------------------FIM UPDATE


    IEnumerator dashReloadRoutine()
    {
        if (dashReady == false)
        {
            for (float f = 0f; f < 3.0f; f += 1.0f)
            {
                yield return new WaitForSeconds(1.0f);
            }
            dashReady = true;
        }
    }

}
