using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour {

    public GameObject meteor;

    // limites spawn
    public Transform leftLimit;
    public Transform rightLimit;
    private float limitOffset;

    // timepo entre spawns de meteoros
    public float secondsBeforeNewMeteor;
    // tiemp hsata el primer meteoro
    public float startingDelay;



    // Use this for initialization
    void Start() {

        // seteamos el offset de spawneo
        limitOffset = 10f;

        // seteamos las variables encargadas del tiempo entre meteoros y del tiempo para antes de la primera llamda de Instanteiatemeteor()
        switch (MainMenuController.difficulty)
        {
            case 0: secondsBeforeNewMeteor = 2f; break;
            case 1: secondsBeforeNewMeteor = 1.5f; break;
        }
        startingDelay = 5f;

        // Debug.Log(secondsBeforeNewMeteor + " " + startingDelay);
        // lanzamos una corrutina que ira spawneando enemigos al llamar a la funcion InstantiateMeteor
        InvokeRepeating("InstantiateMeteor", startingDelay, secondsBeforeNewMeteor);
    }

    void InstantiateMeteor()
    {
        // calculamos la posicion del instanciado
        Vector3 spawnPosition = new Vector3(Random.Range(leftLimit.position.x , rightLimit.position.x ), transform.position.y,transform.position.z);

        // instanciamos el prefab meteor en la poscion ya calculada y con la rotacion ya definida
        Instantiate(meteor, spawnPosition, Quaternion.identity);
    }
}
