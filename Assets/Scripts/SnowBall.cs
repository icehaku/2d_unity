using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBall : MonoBehaviour {

    public float speed;
    private Rigidbody2D theRB;
    public GameObject snowBallEffect;

	// Use this for initialization
	void Start () {
        theRB = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        theRB.velocity = new Vector2(speed * transform.localScale.x, 0);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "player1_tag")
        {
            FindObjectOfType<GameManager>().damage_p1();
        }

        if (other.tag == "player2_tag")
        {
            FindObjectOfType<GameManager>().damage_p2();
        }

        Instantiate(snowBallEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
