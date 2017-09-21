using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public string main_game_scene;

    public void StartGame()
    {
        SceneManager.LoadScene(main_game_scene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // Use this for initialization
    void Start () {		
	}
	
	// Update is called once per frame
	void Update () {		
	}
}
