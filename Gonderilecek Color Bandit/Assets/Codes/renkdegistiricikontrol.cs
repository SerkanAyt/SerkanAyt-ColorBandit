using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class renkdegistiricikontrol : MonoBehaviour
{
    public GameObject renkdegistirici;
    public float renkdegistiricihiz;

    public int kactanerenkdegistirici;
    GameObject[] renkdegistiricidizisi;

    float olusmazaman = 0;
    int sayac;

    bool oyunbitisi = true;



    void Start()
    {
        renkdegistiricidizisi = new GameObject[kactanerenkdegistirici];
        for (int i = 0; i < renkdegistiricidizisi.Length; i++)
        {
            renkdegistiricidizisi[i] = Instantiate(renkdegistirici, new Vector3(-20, 0, 20), Quaternion.identity);



            renkdegistiricidizisi[i].transform.position = new Vector3(0.92f, -0.216f, 20);
            //Dizinin sırayla elemanlarına erişip elemanlardan rigidboddy componentini çekip o komponente hiz atadik.
            renkdegistiricidizisi[i].GetComponent<Rigidbody>().velocity = new Vector3(0, 0, renkdegistiricihiz);

            //Rigidbody renkdegistiricifizik = renkdegistiricidizisi[i].AddComponent<Rigidbody>();



            //fizik.velocity = new Vector3(0, 0, renkdegistiricihiz);
        }
    }

    void Update()
    {
        //if (oyunbitisi)
        //{
        //    //İçerisindeki oluşma zamanı= 0 ı silerek renk değiştiricinin sadece bir kez gelmesini sağladım.
        //    olusmazaman += Time.deltaTime;
        //    if (olusmazaman > 3f)
        //    {
                
        //        //Debug.Log("olusmazamansıfırlandı");

        //        renkdegistiricidizisi[sayac].transform.position = new Vector3(0.92f, -0.216f, 10);
        //        sayac++;
        //        //Debug.Log("sayacarttırıldı");

        //        if (sayac >= renkdegistiricidizisi.Length)
        //        {
        //            //Debug.Log("sayacsıfırlandı");

        //            sayac = 0;
        //        }
        //        //Debug.Log(sayac);

        //    }

        //}
        //else
        //{

        //}
    }

}
