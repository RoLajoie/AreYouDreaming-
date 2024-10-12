using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BluePickup : MonoBehaviour
{
    public AudioSource blueSound;
    public ParticleSystem blueEffect;

    public void playSound(Vector3 position)
    {
       
        Debug.Log("Sound Playing");
        blueSound.Play();
        blueEffect.transform.position = position;
        blueEffect.Play();

    }
}

