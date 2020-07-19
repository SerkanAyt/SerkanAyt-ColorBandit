using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class reklam : MonoBehaviour
{
    InterstitialAd interstitial;

    static reklam reklamKontrol;
    void Start()

    {
        if(reklamKontrol==null)

        {
            DontDestroyOnLoad(gameObject);
            reklamKontrol = this;


            //1. aşama----------------------------------
#if UNITY_ANDROID
            string appID = "ca-app-pub-5881142327850120~9950362933";
#elif UNITY_IPHONE
        string appID="";
#else
        string appID="unexpected_platform";
#endif
                MobileAds.Initialize(appID);

            //2. aşama ------------------------------
#if UNITY_ANDROID
            string adUnitID = "ca-app-pub-39402560999425544/1033173712";
#elif UNITY_IPHONE
        string adunitID="";
#else
        string adunitID="unexpected_platform";
#endif

            interstitial = new InterstitialAd(adUnitID);

            //3.aşama---------------------------------

            AdRequest request = new AdRequest.Builder().AddTestDevice(AdRequest.TestDeviceSimulator).AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build();
            interstitial.LoadAd(request);

            //4.aşama-----------------------------------


        }
        else
        {
            Destroy(gameObject);
        }



    }

    public void reklamiGoster()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }

    }
}
