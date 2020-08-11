using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    public static byte difficulty;          // 0 - easy ; 1 hard
    public static byte players;
    private bool difficultySet;
    public Button playButton;

    public GameObject introductionCanvas;
    public static bool introShown;
    public GameObject difficultyCanvas;




	// Use this for initialization
	void Start () {

        // si ya se ha mostrado una vez el canvas de "bienvenida" no lo muestres mas 
        if (introShown)
        {
            introductionCanvas.SetActive(false);
            difficultyCanvas.SetActive(true);
        }

        difficultySet = false;              // aun no hemos marcado ninguna dificultady por lo tanto...
        playButton.interactable = false;    // no podemos avanzar al juego
	}
	
	// Update is called once per frame
	void Update () {

        if (difficultySet != true)
        {
            playButton.interactable = false;
        }
        else
        {
            playButton.interactable = true;
        }

	}

    public void SetEasy()
    {
        Debug.LogWarning("Difficuty set to Easy");
        difficultySet = true;
        difficulty = 0;
    }

    public void SetHard()
    {
        Debug.LogWarning("Difficuty set to Hard");
        difficultySet = true;
        difficulty = 1;
    }

    public void SetOnePlayer()
    
    {

        players = 1;

    }

    public void SetTwoPlayer()
    {

        players = 2;

    }

    public void PlayGame()
    {
        Debug.Log("The game is being loaded with dififuclty : " + difficulty );

        // si tengo tiempo me gustaria probar de implementar un slider que marque el progreso de la carga, de ahi que lo carge en una variable
        AsyncOperation loadAsync = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);

    }

    public void ShowDificutlyCanvas()
    {

        introShown = true;
        // hacemos que el padre se haga invisible
        introductionCanvas.SetActive(false);
        // hacemos activos el menu de seleccion de dificultad
        difficultyCanvas.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    
}
