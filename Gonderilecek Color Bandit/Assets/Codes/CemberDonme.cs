using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CemberDonme : MonoBehaviour
{
   

    void Start()
    {
        
    }


    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 15)*Time.deltaTime);
        //transform.rotation = Quaternion.identity;
        //transform.RotateAround(transform.position + new Vector3(cemberGrubuDizisi[sayac].GetWidth() / 2f, cemberGrubuDizisi[sayac].GetHeight() / 2f, 0f), Vector3.forward, 15);
        //    Invoke("donme", 5);
    }

    //void donme()
    //{
    //    transform.Rotate(new Vector3(0, 0, 15) * Time.deltaTime);
    //}
}
