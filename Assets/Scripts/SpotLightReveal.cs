using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SpotLightReveal : MonoBehaviour
{
    public GameObject spotLight; 
    public float timerForSpotLight = 5f;
    public float offTheLights = 20f; 
    // Start is called before the first frame update
    void Start()
    {
        spotLight.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        timerForSpotLight -= Time.deltaTime;  
        offTheLights -= Time.deltaTime;
        if (timerForSpotLight <= 0)
        {
            spotLight.SetActive(true);
            timerForSpotLight = 10f;
        }

        if (offTheLights <= 0) {
            spotLight.SetActive(false);
        }

    }
}
