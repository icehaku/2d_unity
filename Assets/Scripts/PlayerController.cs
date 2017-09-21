
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

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
    public bool dashReady = true;
    public string[] last_taps;

    // Use this for initialization
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        lastTapTime = 0;
    }


    // Update is called once per framee
    void Update()
    {

        //controles
        if (Input.GetKey(right))
        {
            theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);
        }

        if (Input.GetKey(left))
        {
            theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
        }

        //removendo a desaceleracao do player e parando instantaneo;
        //Caso queria que o boneco pare aos poucos, remover o bloco abaixo
        if (Input.GetKeyUp(left) || Input.GetKeyUp(right))
        {
            theRB.velocity = new Vector2(0, theRB.velocity.y);

        }

        //MY DASH--------------------------------------------------------------
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
                //Dash que ta mais pra blink
                //theRB.AddForce(transform.right * 5000); //KINDA WORK
                theRB.velocity = new Vector2(moveSpeed * 2, theRB.velocity.y);
                dashReady = false;
                StartCoroutine(dashReloadRoutine());
            }
            lastTapTime = Time.time;
        }


        //left_dash
        if (Input.GetKeyDown(left))
        {
            if (dashReady && (Time.time - lastTapTime) < tapReload && (last_taps[0] == last_taps[1]))
            {
                anim.SetTrigger("dashing");
                trow_snd.Play();
                //Dash que ta mais pra blink
                //theRB.AddForce(-transform.right * 5000); //KINDA WORK                    
                dashReady = false;
                StartCoroutine(dashReloadRoutine());

            }
            lastTapTime = Time.time;
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
        if (theRB.velocity.x < 0)
        {
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
    }//----------------------------------------------------------------FIM UPDATE


    IEnumerator dashReloadRoutine()
    {
        if (dashReady == false)
        {
            for (float f = 0f; f < 3.0f; f += 1.0f)
            {
                Debug.Log("Dash Reload Time: " + f);
                yield return new WaitForSeconds(1.0f);
            }
            dashReady = true;
        }

    }

}



/*


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
    public bool dashReady = true;
    public string[] last_taps;

    //dash by video
    public int leftTotal = 0;
    public float leftTimeDelay = 0;
    public int rightTotal = 0;
    public float rightTimeDelay = 0;
    
    public int xVel = 0;


    // Use this for initialization
    void Start () {
        theRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        lastTapTime = 0;
    }

	
	// Update is called once per framee
	void Update () {
        
        //controles by video dash
        if (Input.GetKey(left) && (isGrounded == true))
        {
            theRB.velocity = new Vector3(-2, 0, 0);
        }
        
        if (Input.GetKeyDown(left) && (isGrounded == true))
        {
            leftTotal += 1;
        }
        
        if (Input.GetKeyUp(left))
        {
            xVel = 0;
            theRB.velocity = new Vector3(0, 0, 0);
        }
        
        if ((leftTotal == 1) && (leftTimeDelay < 0.5))
        {
            leftTimeDelay += Time.deltaTime;
        }
        
        if ((leftTotal == 1) && (leftTimeDelay >= 0.5))
        {
            leftTimeDelay = 0;
            leftTotal = 0;
        }
        
        if ((leftTotal == 2) && (leftTimeDelay < 0.5))
        {
            leftTotal = 0;
            theRB.velocity = new Vector3(-40, 30, 0);
            anim.SetTrigger("dashing");
            trow_snd.Play();
        }
        
        if ((leftTotal == 2) && (leftTimeDelay >= 0.5))
        {
            leftTotal = 0;
            leftTimeDelay = 0;
        }
        //////////////////////////////////////////////////////////////////////////////////////////
        if ((rightTotal == 2) && (rightTimeDelay >= 0.5))
        {
            rightTotal = 0;
            rightTimeDelay = 0;
        }
        
        if (Input.GetKey(right))
        {
            theRB.velocity = new Vector3(2 + xVel, 0, 0);
        }
        
        if (Input.GetKeyDown(right))
        {
            rightTotal += 1;
        }
        
        if (Input.GetKeyUp(right))
        {
            xVel = 0;
            theRB.velocity = new Vector3(0, 0, 0);
        }
        
        if ((rightTotal == 1) && (rightTimeDelay < 0.5))
        {
            rightTimeDelay += Time.deltaTime;
        }
        
        if ((rightTotal == 1) && (rightTimeDelay >= 0.5))
        {
            rightTimeDelay = 0;
            rightTotal = 0;
        }
        
        if ((rightTotal == 2) && (rightTimeDelay < 0.5))
        {
            xVel = 2;
            rightTotal = 0;
            anim.SetTrigger("dashing");
            trow_snd.Play();
        }
        
        if ((rightTotal == 2) && (rightTimeDelay >= 0.5))
        {
            xVel = 0;
            rightTotal = 0;
            rightTimeDelay = 0;
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
    }//----------------------------------------------------------------FIM UPDATE


    IEnumerator dashReloadRoutine()
    {
        if(dashReady == false) { 
            for (float f = 0f; f < 3.0f; f += 1.0f)
            {
                Debug.Log("Dash Reload Time: " + f);
                yield return new WaitForSeconds(1.0f);
            }
            dashReady = true;
        }

    }

}


 */
