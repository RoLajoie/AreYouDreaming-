using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowPickup : MonoBehaviour
{
    public AudioSource yellowSound;
    public ParticleSystem yellowEffect;

    public void yellowInteraction(Vector3 position)
    {
        Debug.Log("Sound Playing");
        yellowSound.Play();
        
        yellowEffect.transform.position = position;
        yellowEffect.Play();

        GameManager gameManager = GameManager.Instance;
        gameManager.IncrementScore(); 
    }
}
