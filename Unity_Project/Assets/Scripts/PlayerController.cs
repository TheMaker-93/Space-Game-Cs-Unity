using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public byte playerNumber;
    public int speedMultiplier;

    public GameObject misil;
    public int playerLifes;

    public Canvas LoseGameCanvas;
    public float timeOfPowerUpEffects;

    // referencia al hud de vidas restantes
    private HudController hudController;

    void Awake ()
    {
        hudController = GameObject.FindGameObjectWithTag("manager").GetComponent<HudController>();
        if (MainMenuController.players < 2 && playerNumber == 2)
        {
            gameObject.SetActive(false);
        }
    }

	// Use this for initialization
	void Start () {
        LoseGameCanvas.gameObject.SetActive(false);
        timeOfPowerUpEffects = 5f;      // 5 segundos

    }
	
	// Update is called once per frame
	void Update () {
        Movement();
        Shoot();
    }

    void Movement()
    {
        //-------------------------------------------------------
        // antes de comporvar el player comporovaremos las colisines con LOS BORDES DE LA PANTALLA
        // y si eso despues entre ellos
        // todo HACER COMPROVACION DE LAS COLISIONES
        // ------------------------------------------------------

        if (playerNumber == 1)
        {

            float horizontal = Input.GetAxis("Horizontal2");
            float vertical = Input.GetAxis("Vertical2");
            

            transform.Translate(-vertical * speedMultiplier * Time.deltaTime, -horizontal * speedMultiplier * Time.deltaTime, 0);
        } else if (playerNumber == 2)
        {
            // estaria bien que el segundo player usara otras teclas, todo 

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            transform.Translate(-vertical * speedMultiplier * Time.deltaTime, -horizontal * speedMultiplier * Time.deltaTime, 0);
        }

        Vector2 screenPositioon = Camera.main.WorldToScreenPoint(transform.position);       // guardamos la posicion relativa a la camrara
        // borde superior e inferior

        Vector3 verticalCorrection = new Vector3(0, speedMultiplier * Time.deltaTime);
        if (screenPositioon.y >= Screen.height)
        {
            transform.SetPositionAndRotation(transform.position - verticalCorrection, this.transform.rotation);
        } else if (screenPositioon.y <= 0)
        {
            transform.SetPositionAndRotation(transform.position + verticalCorrection, this.transform.rotation);
        }
        // lateral derecho e izquierdo
        Vector3 horizontalCorrection = new Vector3(speedMultiplier * Time.deltaTime, 0);
        if (screenPositioon.x >= Screen.width)
        {
            transform.SetPositionAndRotation(transform.position - horizontalCorrection,this.transform.rotation);
        } else if (screenPositioon.x <= 0)
        {
            transform.SetPositionAndRotation(transform.position + horizontalCorrection, this.transform.rotation);
        }


    }

    void Shoot()
    {
        // solo podemos disparar si presionamos la tecla espacio y si ademas no hay misiles en pantalla
        if (Input.GetKeyDown(KeyCode.Space) && GameObject.FindGameObjectWithTag("missile1") == false && playerNumber == 1)
        {
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);

            GameObject misile = Instantiate(misil, spawnPosition, Quaternion.Euler(-180, 0, 0));
            misile.GetComponent<MisilController>().father = playerNumber;
        }



        if (Input.GetKeyDown(KeyCode.RightControl) && GameObject.FindGameObjectWithTag("missile2") == false && playerNumber == 2)
        {
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);

            GameObject misile = Instantiate(misil, spawnPosition, Quaternion.Euler(-180, 0, 0));
            misile.GetComponent<MisilController>().father = playerNumber;
        }


    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "asteroid")
        {
            playerLifes--;
            hudController.UpdateLifesHud(playerNumber, playerLifes);
            if (playerLifes <= 0)
            {
                Debug.Log("Has perdido todas tus salvaciones");

                // aqui hacemos visible la pantalla de derrota
                LoseGameCanvas.gameObject.SetActive(true);
                // CARGAMOS los resultados en la pantalla de fin de partida
                GameObject.FindGameObjectWithTag("P1-EndScore").GetComponent<Text>().text = GameObject.FindGameObjectWithTag("P1-Score").GetComponent<Text>().text;
                GameObject.FindGameObjectWithTag("P2-EndScore").GetComponent<Text>().text = GameObject.FindGameObjectWithTag("P2-Score").GetComponent<Text>().text;
                // paramos el tiempo tambien porque mola
                Time.timeScale = 0f;
            }
            
        }
    }

    public void DoubleSpeedMultiplier ( )
    {
        int multiplier = 2;         // multiplicador de la velocidad

        speedMultiplier = speedMultiplier * multiplier;     // multiplicamos la velocidad por 2

        // llamamos a una corrunita que hara de update y se ejecutara al cabo de 5 segundos para dejar el multiplicador tal y como estaba
        // pD me encatntan las corrutinas!
        StartCoroutine(RestroreMultiplier());

    }

    IEnumerator RestroreMultiplier ()
    {
        yield return new WaitForSeconds(timeOfPowerUpEffects);

        speedMultiplier = speedMultiplier / 2;
    }


}
