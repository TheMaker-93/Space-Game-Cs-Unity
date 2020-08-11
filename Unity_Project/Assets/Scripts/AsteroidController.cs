using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidController : MonoBehaviour {

    // propias
    public int startingLifes;
    public int lifes;

    int minSpeed;
    int speedMultipier;

    Vector3 rotSpeed;

    // referncias
    GameObject gameManager;

    // AUdio
    public GameObject multiporposeSpeaker;
    public AudioClip collisionSFX;
    public AudioClip collisionWithPlayerSFX;

    // impact particles
    public ParticleSystem impactParticles;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager");

        // si jugamos en facil nos aseguramos que no rote por nada en el mundo y sino le dejamos libre de hacer lo que quiera
        switch (MainMenuController.difficulty)
        {
            case 0: gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;    break;
            case 1: gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;               break;
        }

    }

    // Use this for initialization
    void Start() {

        if (lifes == 0)
        {
            startingLifes = Random.Range(1, 4);
            lifes = startingLifes;
        }

        switch (startingLifes)
        {
            case 1: speedMultipier = 15;    break;
            case 2: speedMultipier = 10;    break;
            case 3: speedMultipier = 5;     break;
            default:
                Debug.LogError("Has roto algo en el seteo de la salud");
                break;
        }

        rotSpeed = new Vector3(Random.Range(-1, 2), Random.Range(-1, 2), Random.Range(-1, 2));

        // seteamos la escala dependiendo de la salud inicial del asteroide
        SetScale();
        SetRotation();

        ApplyInitialForce();

    }

    // Update is called once per frame
    void Update() {

        Destroy();
        SetRotativeMovement();
    }

    // este metodo seteara el tamaño del asteroide dependiendo de la salud que tenga
    public void SetScale()
    {
        transform.localScale = new Vector3(lifes * 100, lifes * 100, lifes * 100);
    }
    void SetRotation()
    {
        transform.localEulerAngles = new Vector3(transform.rotation.x, Random.Range(0, 361), transform.rotation.z);
    }


    void ApplyInitialForce()
    {

        // si nos salimos por encima
        if (this.transform.position.y < GameObject.Find("MeteorSpawner").transform.position.y)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1));
        }

        // dependiendo de la dificultad seleccionda y aplicada a la variable ESTATICA de la clase MAINmENU determinaremos el comportamiento
        // del spawneo incial de enemigos
        switch (MainMenuController.difficulty)
        {
            case (0):   // easy
                Debug.Log("seteado easy");      // en este caso caen verticalmente
                GetComponent<Rigidbody2D>().AddForce(new Vector3(0, -Random.Range(30, 61) * speedMultipier, 0));
                break;
            case (1):   // HARD
                Debug.Log("seteado hard");
                GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-80, 81) * speedMultipier * 1.5f, -Random.Range(50, 91) * speedMultipier * 1.5f, 0));
                break;
            default:
                Debug.LogError("No hay una dificultad seleccionada, SETEANDO EASY");
                MainMenuController.difficulty = 0;          // corregimos el posible fallo al setear la dificultad
                ApplyInitialForce();                        // rehacemso el proceso para determinar las velocidades adecuadas
                break;

        }
    }

    // en caso de superar una posicion concreta el objeto sera borrado 
    void Destroy()
    {
        if (transform.position.y < -5)
        {
            Destroy(this.gameObject);
        }
    }

    // hacemos rotar al objeto si la dificultad es dificil
    void SetRotativeMovement()
    {
        if (MainMenuController.difficulty == 1)
        {
            transform.Rotate(rotSpeed.x, rotSpeed.y, rotSpeed.z);
        }
    }

    void OnCollisionEnter2D (Collision2D other)
    {
        if (other.transform.tag == "Player")
        {
            GameObject multiporposeSpeakerContainer = Instantiate(multiporposeSpeaker, this.transform.position, Quaternion.identity);
            multiporposeSpeaker.GetComponent<SpeakerController>().SetAudioClip(collisionWithPlayerSFX);

            ParticleSystem exp3;
            exp3 = Instantiate(impactParticles, this.transform.position, Quaternion.identity);

            Destroy(exp3, exp3.GetComponent<ParticleSystem>().main.duration);

            Destroy(gameObject);
        }
    }

    







}
    


