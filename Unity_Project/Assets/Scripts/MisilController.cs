using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisilController : MonoBehaviour {

    public byte father;
    public ParticleSystem explosion;
    public ParticleSystem explosion2;


    public AudioClip explosionSound;
    public GameObject speaker;

    // Use this for initialization
    void Start () {
        Debug.Log(father);
        tag = "missile" + father;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 2));
        Destroy();      // destruimos el misil si se sale de los limites
    }

    void Destroy()
    {
        if (transform.position.y > 6)
        {
            Destroy(this.gameObject);
        }
    }

    // unicoos          canal de mates

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "asteroid")
        {
            coll.gameObject.GetComponent<AsteroidController>().lifes--;
            coll.gameObject.GetComponent<AsteroidController>().SetScale();

            explosion.GetComponent<ParticleSystem>().startSize = 0.2f;
            ParticleSystem exp1;
            exp1 = Instantiate(explosion, transform.position, Quaternion.identity);

            // reprucimos sonido
            // primero instanciamos el objeto y lo guardamos
            GameObject speakerContainer = Instantiate(speaker, this.transform.position, Quaternion.identity);
            // despues accedemos a su script y lanzamos el metodo dandole el sonido que produciremos
            speakerContainer.GetComponent<SpeakerController>().SetAudioClip(explosionSound);

            // la destruiremos en cuanto su tiempo de duracion
            Destroy(exp1, exp1.GetComponent<ParticleSystem>().main.duration);


            explosion2.GetComponent<ParticleSystem>().startSize = 0.3f + coll.gameObject.GetComponent<AsteroidController>().startingLifes / 3;

            ParticleSystem exp2;
            exp2 = Instantiate(explosion2, coll.transform.position, Quaternion.identity);

            Destroy(exp2, exp2.GetComponent<ParticleSystem>().main.duration);


            if (coll.gameObject.GetComponent<AsteroidController>().lifes <= 0)
            {
                // HudController.GivePoint(father);
                GameObject.FindGameObjectWithTag("manager").GetComponent<HudController>().GivePoint(father);

             
                Destroy(coll.gameObject);
            }

            Destroy(gameObject);
        }
        if (coll.gameObject.tag == "Player")
        {

            Destroy(gameObject);
        }
    }

    
 
    }


