using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour {

    public KeyCode grab;
    public bool grabbing;
    RaycastHit2D hit;
    RaycastHit2D hit2;
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
                Physics2D.queriesStartInColliders = false; //negar o retorno do raycast quando for algo que ele ta dentro;
                hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);                

                if (hit.collider != null && hit.collider.tag == "pegavel")
                {
                    hit.collider.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                    hit.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                    grabbing = true;
                    pick.Play();
                }

            }
            else
            {
                //jogar                           
                if (hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
                {
                    //TROCAR pegar outro se tiver perto
                    hit.collider.gameObject.SetActive(false);
                    hit2 = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);
                    hit.collider.gameObject.SetActive(true);
                    hit.collider.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
                    //=================================
                    hit.collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 2) * trowForce;
                    hit.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                    grabbing = false;
                    toss.Play();

                    //TROCAR pegar outro se tiver perto
                    if (hit2.collider != null && hit2.collider.tag == "pegavel")
                    {
                        hit = hit2;
                        hit.collider.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                        hit.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                        grabbing = true;
                        trade.Play();
                    }
                    //=================================
                }
            }

        }//END getkeyDOWN

        //move o pegavel pro player
        if (grabbing)
        {
            hit.collider.gameObject.transform.position = holdPoint.position;
        }

	}//END UPDATE

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
    }
}
