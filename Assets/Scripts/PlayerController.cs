using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //controle movimento
    public float moveSpeed;
    public float moveSpeedMulti = 2.0f;
    public float jumpForce;
    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    public KeyCode trow_snowball;    

    //pulo
    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask whatIsGrounded;
    public bool isGrounded;

    //componentes
    private Rigidbody2D theRB;
    private Animator anim;

    //entidades
    public GameObject snowBall;
    public Transform trowPoint;

    //audio
    public AudioSource trow_snd;
    public AudioSource jump_snd;

    //dash
    public float tapReload = 0.5f; //segundos
    private float lastTapTime = 0;
    private float dashReload = 0;
    public bool dashReady = true;
    public string[] last_taps;


    // Use this for initialization
    void Start () {
        theRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        lastTapTime = 0;
    }
	
	// Update is called once per frame
	void Update () {
        //keep dash direction
        if (Input.GetKeyDown(right))
        {
            last_taps[1] = last_taps[0];
            last_taps[0] = "right";
        }
        if (Input.GetKeyDown(left))
        {
            last_taps[1] = last_taps[0];
            last_taps[0] = "left";
        }

        //right_dash
        if (Input.GetKeyDown(right))
        {
            if (dashReady && (Time.time - lastTapTime) < tapReload && (last_taps[0] == last_taps[1]))
            {
                anim.SetTrigger("dashing");
                trow_snd.Play();                
                theRB.AddForce(transform.right * 5000); //KINDA WORK
                dashReady = false;
                StartCoroutine(dashReloadRoutine());                
            }
            lastTapTime = Time.time;
        }

        //leftt_dash
        if (Input.GetKeyDown(left))
        {
            if (dashReady && (Time.time - lastTapTime) < tapReload && (last_taps[0] == last_taps[1]))
            {
                anim.SetTrigger("dashing");
                trow_snd.Play();                
                theRB.AddForce(-transform.right * 5000); //KINDA WORK                    
                dashReady = false;
                StartCoroutine(dashReloadRoutine());
                
            }
            lastTapTime = Time.time;
        }

        //controles
        if (Input.GetKey(left))
        {
            theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
        }
        else if (Input.GetKey(right))
        {
            theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);
        }
        else 
        {
            theRB.velocity = new Vector2(0, theRB.velocity.y);
        }


        //ground_jump_check
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGrounded);
        if (Input.GetKeyDown(jump) && isGrounded)
        {
            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
            jump_snd.Play();
        }
        anim.SetFloat("speed", Mathf.Abs(theRB.velocity.x));
        anim.SetBool("grounded", isGrounded);


        //revert_position
        if (theRB.velocity.x < 0) {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (theRB.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        //trow snow ball
        if (Input.GetKeyDown(trow_snowball))
        {
            GameObject ballClone = (GameObject)Instantiate(snowBall, trowPoint.position, trowPoint.rotation);
            ballClone.transform.localScale = transform.localScale;
            anim.SetTrigger("snowball");
            trow_snd.Play();
        }
    }//---------------------------------FIM UPDATE

    IEnumerator dashReloadRoutine()
    {
        while (dashReady == false)
        {
            Debug.Log("Dash Reload Time: " + (int)dashReload);
            dashReload += 1;
            if (dashReload == 3)
            {
                dashReady = true;
                dashReload = 0;
            }


            yield return new WaitForSeconds(1f);
        }
    }

}
