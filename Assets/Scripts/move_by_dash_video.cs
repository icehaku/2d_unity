using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_by_dash_video : MonoBehaviour {

    ////dash by video
    //public int leftTotal = 0;
    //public float leftTimeDelay = 0;
    //public int rightTotal = 0;
    //public float rightTimeDelay = 0;
    //
    //public int xVel = 0;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //
        ////controles by video dash
        //if (Input.GetKey(left) && (isGrounded == true))
        //{
        //    theRB.velocity = new Vector3(-2, 0, 0);
        //}
        //
        //if (Input.GetKeyDown(left) && (isGrounded == true))
        //{
        //    leftTotal += 1;
        //}
        //
        //if (Input.GetKeyUp(left))
        //{
        //    xVel = 0;
        //    theRB.velocity = new Vector3(0, 0, 0);
        //}
        //
        //if ((leftTotal == 1) && (leftTimeDelay < 0.5))
        //{
        //    leftTimeDelay += Time.deltaTime;
        //}
        //
        //if ((leftTotal == 1) && (leftTimeDelay >= 0.5))
        //{
        //    leftTimeDelay = 0;
        //    leftTotal = 0;
        //}
        //
        //if ((leftTotal == 2) && (leftTimeDelay < 0.5))
        //{
        //    leftTotal = 0;
        //    theRB.velocity = new Vector3(-40, 30, 0);
        //    anim.SetTrigger("dashing");
        //    trow_snd.Play();
        //}
        //
        //if ((leftTotal == 2) && (leftTimeDelay >= 0.5))
        //{
        //    leftTotal = 0;
        //    leftTimeDelay = 0;
        //}
        ////////////////////////////////////////////////////////////////////////////////////////////
        //if ((rightTotal == 2) && (rightTimeDelay >= 0.5))
        //{
        //    rightTotal = 0;
        //    rightTimeDelay = 0;
        //}
        //
        //if (Input.GetKey(right))
        //{
        //    theRB.velocity = new Vector3(2 + xVel, 0, 0);
        //}
        //
        //if (Input.GetKeyDown(right))
        //{
        //    rightTotal += 1;
        //}
        //
        //if (Input.GetKeyUp(right))
        //{
        //    xVel = 0;
        //    theRB.velocity = new Vector3(0, 0, 0);
        //}
        //
        //if ((rightTotal == 1) && (rightTimeDelay < 0.5))
        //{
        //    rightTimeDelay += Time.deltaTime;
        //}
        //
        //if ((rightTotal == 1) && (rightTimeDelay >= 0.5))
        //{
        //    rightTimeDelay = 0;
        //    rightTotal = 0;
        //}
        //
        //if ((rightTotal == 2) && (rightTimeDelay < 0.5))
        //{
        //    xVel = 2;
        //    rightTotal = 0;
        //    anim.SetTrigger("dashing");
        //    trow_snd.Play();
        //}
        //
        //if ((rightTotal == 2) && (rightTimeDelay >= 0.5))
        //{
        //    xVel = 0;
        //    rightTotal = 0;
        //    rightTimeDelay = 0;
        //}

    }
}
