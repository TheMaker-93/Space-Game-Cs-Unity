using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour {

    public GameObject powerUp;

	// Use this for initialization
	void Start () {
		
        if (powerUp)
        {
            InvokeRepeating("InstantiatePowerUp", 1, Random.Range(25,41));
        }

	}
	
	// Update is called once per frame
	void Update () {
        Destroy();
    }

    private void InstantiatePowerUp()
    {
        Debug.Log("Power Instanciado");
        Instantiate(powerUp,this.transform.position,Quaternion.identity);
    }

    private void Destroy ()
    {
        Vector2 screenPositioon = Camera.main.WorldToScreenPoint(transform.position);

        if (screenPositioon.y < -4.5f)
        {
            Destroy(this.gameObject);
        }
    }
}
