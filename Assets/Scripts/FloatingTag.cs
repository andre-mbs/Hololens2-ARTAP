using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTag : MonoBehaviour
{
    public GameObject head;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.parent.LookAt(head.transform);
        gameObject.transform.rotation = Quaternion.Euler(0, gameObject.transform.rotation.eulerAngles.y, 0);
    }
}
