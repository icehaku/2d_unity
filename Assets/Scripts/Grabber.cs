using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour {

    public KeyCode grab;
    public bool grabbing;
    public RaycastHit2D hit;
    public RaycastHit2D hit2;
    public float distance = 1;
    public Transform holdPoint;
    public float trowForce = 10;
    public AudioSource pick;
    public AudioSource trade;
    public AudioSource toss;

    // Use this for initialization
    void Start () {		
	}

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(grab))
        {
            if (!grabbing)
            {
                //pegar
                Physics2D.queriesStartInColliders = false;
                hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);

                if (hit.collider != null && hit.collider.tag == "pegavel")
                {
                    pick.Play();
                    grabbing = true;
                }

            }
            else
            {
                //jogar                           
                if (hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
                {
                    //sem fisica no item
                    //hit.collider.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
                    hit.collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 2) * trowForce;
                    grabbing = false;
                    toss.Play();

                    //pegar outro se tiver perto                    
                    //hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);
                    //if (hit.collider != null && hit.collider.tag == "pegavel")
                    //{
                    //    trade.Play();
                    //    grabbing = true;
                    //}
                }
            }

        }//END getkeyDOWN

        //move o pegavel pro player
        if (grabbing)
        {   
            //sem fisica no item         
            //hit.collider.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            hit.collider.gameObject.transform.position = holdPoint.position;
        }

	}//END UPDATE

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
    }
}
