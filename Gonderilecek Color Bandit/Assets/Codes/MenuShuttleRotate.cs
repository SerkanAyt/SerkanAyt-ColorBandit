using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuShuttleRotate : MonoBehaviour
{
    public float DonmeHizi;
    
    float x, y, z;
    void Start()
    {
        
    }

    
    void LateUpdate()
    {
        x = Random.Range(10f, 30f);
        y = Random.Range(20f, 50f);
        z = Random.Range(35f, 65f);
        transform.Rotate(new Vector3(x, y, z)*Time.deltaTime*0.4f);
        

    }
}
