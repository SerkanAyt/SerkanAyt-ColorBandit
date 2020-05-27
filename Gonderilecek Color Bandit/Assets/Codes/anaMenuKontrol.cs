using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class anaMenuKontrol : MonoBehaviour
{
    public Text puanText;
    public static Text LevelName;


    public void Start()
    {




    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenuEng")
        {
            int enYuksekPuan = PlayerPrefs.GetInt("enYuksekPuanKayit");

            puanText.text = "Highest Score: " + enYuksekPuan;



        }
        if (SceneManager.GetActiveScene().name == "MainMenuTur")
        {
            int enYuksekPuan = PlayerPrefs.GetInt("enYuksekPuanKayit");
            puanText.text = "En Yüksek Puan: " + enYuksekPuan;
        }
    }

    public void oyunaGitTur()
    {
        SceneManager.LoadScene("GameTur");

    }

    public void LanguagesEng()
    {
        SceneManager.LoadScene("LanguagesEng");

    }

    public void AnaMenuTurkce()
    {
        SceneManager.LoadScene("MainMenuTur");



    }

    public void AnaMenuEnglish()
    {
        SceneManager.LoadScene("MainMenuEng");


    }

    public void LanguagesTur()
    {
        SceneManager.LoadScene("LanguagesTur");

    }
    public void oyunaGitEng()
    {
        SceneManager.LoadScene("GameEng");

    }
}