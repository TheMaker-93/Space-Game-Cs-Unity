using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSpeedPower : MonoBehaviour {


    Vector2 speed = new Vector2(0,-1.5f);

    public GameObject multiporposeSpeaker;
    public AudioClip sFX;


	// Use this for initialization
	void Start () {

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = speed;

	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(new Vector3(0, 2f, 0));
		
	}

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            // sonidos
            GameObject speakerContainer = Instantiate(multiporposeSpeaker, this.transform.position, Quaternion.identity);
            speakerContainer.GetComponent<SpeakerController>().SetAudioClip(sFX);


            other.GetComponent<PlayerController>().DoubleSpeedMultiplier();
            Destroy(this.gameObject);
        }
        
    }

    
}
