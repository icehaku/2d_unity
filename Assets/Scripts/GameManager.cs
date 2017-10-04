using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;

    public int p1_hp;
    public int p2_hp;

    public GameObject p1_wins;
    public GameObject p2_wins;

    public GameObject[] p1_lifes;
    public GameObject[] p2_lifes;

    public AudioSource hurt_snd;

    public string main_menu;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (p1_hp <= 0)
        {
            player1.SetActive(false);
            p2_wins.SetActive(true);
        }

        if (p2_hp <= 0)
        {
            player2.SetActive(false);
            p1_wins.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(main_menu);
        }
    }

    public void damage_p1()
    {
        //hp damage system p1
        p1_hp -= 1;    
        for (int i = 0; i < p1_lifes.Length; i++)
        {
            if (p1_hp > i)
            {
                p1_lifes[i].SetActive(true);
            }
            else {
                p1_lifes[i].SetActive(false);
            }
        }
        hurt_snd.Play();
    }

    public void damage_p2()
    {
        //hp damage system p2
        p2_hp -= 1;
        for (int i = 0; i < p2_lifes.Length; i++)
        {
            if (p2_hp > i)
            {
                p2_lifes[i].SetActive(true);
            }
            else
            {
                p2_lifes[i].SetActive(false);
            }
        }
        hurt_snd.Play();
    }

}
