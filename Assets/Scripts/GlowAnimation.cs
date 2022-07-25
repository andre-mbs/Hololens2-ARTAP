using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowAnimation : MonoBehaviour
{
    private float range = 0;
    private int f = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(range >= 0.09)
		{
            f = -1;
		}
        if(range <= 0.03)
		{
            f = 1;
		}

        range += 0.001f*f;
        //gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green*intensity);
        gameObject.GetComponent<Light>().range = range;
    }
}
