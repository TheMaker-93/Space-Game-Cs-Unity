using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HudController : MonoBehaviour {

    public float gameTime;
    bool bTwoPlayers;

    // puntuacion del jugador en formato numerico
    int player1Score;
    int player2Score;

    // puntuacion d elos jugadores en formato de texto
    Text player1Sc;
    Text player2Sc;

    // SALUD DEL PLAYER
    Text player1He;
    Text player2He;

    // counter de tiempo transcurrido
    float timeSinceStart;
    public Text timeText;

    // losecanvas
    public Canvas loseCanvas;

    void Awake ()
    {
        //TODO
        //Variable para mostrar o no objetos segun players (no implementado)
        bTwoPlayers = true;
        if (MainMenuController.players < 2)
        {
            bTwoPlayers = false;
        }

        Time.timeScale = 1f;
        // guardamos la referencia del texto
        player1Sc = GameObject.Find("P1Score").GetComponent<Text>();
        player2Sc = GameObject.Find("P2Score").GetComponent<Text>();
        
        // guardamos la referencia del texto de salud restante
        player1He = GameObject.Find("p1RemainingHealth").GetComponent<Text>();
        player2He = GameObject.Find("p2RemainingHealth").GetComponent<Text>();
        
    }

    // Use this for initialization
    void Start () {
        // seteamos los scores a 0
        player1Score = player2Score = 0;
		
	}

    void Update ()
    {
        SetElapsedTime();
    }
	
    // añadimos puntuacion al player que toque dependiendo de playerNumber
    public void GivePoint (byte playerNumber)
    {
        if (playerNumber == 1)
        {
            player1Score++;                             // aumentamos la puntuacion numerica
            player1Sc.text = player1Score.ToString();   // se la aplicamos a la interfaz
        }
        else if (playerNumber == 2)
        {
            player2Score++;
            player2Sc.text = player2Score.ToString();
        }
        else
        {
            Debug.LogWarning("Not a valid player");     // por si rompeis algo
        }

    }

    public void UpdateLifesHud (byte playerNumber, int currentLifes)
    {
        if (playerNumber == 1)
        {
            player1He.text = currentLifes.ToString();

            switch (currentLifes )
            {
                case (2):
                player1He.color = new Color(255, 153, 0);
                    break;
                case (1):
                    player1He.color = Color.red;
                break;
            }
        } else if (playerNumber == 2)
        {
            player2He.text = currentLifes.ToString();

            switch (currentLifes)
            {
                case (2):
                    player2He.color = new Color(255, 153, 0);
                    break;
                case (1):
                    player2He.color = Color.red;
                    break;
            }
        }
    }






    // metodo para la carga usual de escenas
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void SetElapsedTime ()
    {
        timeSinceStart +=  Time.deltaTime;
        timeText.text = "Time: " + ((int)timeSinceStart).ToString();

        if (timeSinceStart >= gameTime)
        {
            loseCanvas.gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("P1-EndScore").GetComponent<Text>().text = GameObject.FindGameObjectWithTag("P1-Score").GetComponent<Text>().text;
            GameObject.FindGameObjectWithTag("P2-EndScore").GetComponent<Text>().text = GameObject.FindGameObjectWithTag("P2-Score").GetComponent<Text>().text;
            Time.timeScale = 0f;
        }
    }


}
