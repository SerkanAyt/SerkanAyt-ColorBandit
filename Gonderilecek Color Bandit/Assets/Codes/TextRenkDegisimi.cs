using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UI;

public class TextRenkDegisimi : MonoBehaviour
{
    Text Baslik;
    Color randomRenk;
    int x, y, z;
    float sayi = 0;

    public Color32 textColor32;


    void Start()
    {
        Baslik = GetComponent<Text>();
        
    }

    void RandomizeTextColor()
    {
        // Randomly set each values of textColor32 by using Random.Range.
        // Call Random.Range and convert the random int value to byte.
        textColor32 = new Color32(
            (byte)Random.Range(240, 255),     // R
            (byte)Random.Range(0, 100),     // G
            (byte)Random.Range(0, 100),     // B
            (byte)Random.Range(250, 255));   // A

        // Set the color of [textObject] to [textColor32]
        Baslik.color = textColor32;
    }
    void Update()
    {
        
        sayi += Time.deltaTime;
        if (sayi > 0.5f)
        {
            RandomizeTextColor();

            sayi = 0;
        }
    }

}
