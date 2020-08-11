using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerController : MonoBehaviour {
   
    AudioSource aS;

    public void SetAudioClip (AudioClip ac)
    {
        aS = GetComponent<AudioSource>();
        if (ac)
        {
            aS.clip = ac;     // le asignamos el clip que le pasamos
            aS.Play();          // reproducimos el clip de sonido
        } else
        {
            Debug.LogError("WARNING :: No se ha cargado ningun sonido en el altavoz");
        }
    }
}
