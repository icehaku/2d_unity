//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class sprint : MonoBehaviour {
//
//	// Use this for initialization
//	void Start () {
//		
//	}
//	
//	// Update is called once per frame
//        void Update () {
//        //controles by video dash
//        if (Input.GetKey(right))
//        {
//            theRB.velocity = new Vector3(2 + xVel, 0, 0);
//        }
//
//        if (Input.GetKeyDown(right))
//        {
//            rightTotal += 1;
//        }
//
//        if (Input.GetKeyUp(right))
//        {
//            xVel = 0;
//            theRB.velocity = new Vector3(0, 0, 0);
//        }
//
//        if ((rightTotal == 1) && (rightTimeDelay < 0.5))
//        {
//            rightTimeDelay += Time.deltaTime;
//        }
//
//        if ((rightTotal == 1) && (rightTimeDelay >= 0.5))
//        {
//            rightTimeDelay = 0;
//            rightTotal = 0;
//        }
//
//        if ((rightTotal == 2) && (rightTimeDelay < 0.5))
//        {
//            xVel = 2;
//            rightTotal = 0;
//            anim.SetTrigger("dashing");
//            trow_snd.Play();
//        }
//
//        if ((rightTotal == 2) && (rightTimeDelay >= 0.5))
//        {
//            xVel = 0;
//            rightTotal = 0;
//            rightTimeDelay = 0;
//        }
//    }
//
//