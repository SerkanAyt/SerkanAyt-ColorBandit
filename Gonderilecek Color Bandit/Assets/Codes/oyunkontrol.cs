using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class oyunkontrol : MonoBehaviour
{
    
    public GameObject altigenplatform1;
    public GameObject altigenplatform2;
    public GameObject cemberGrubu;
    public GameObject textGrubu;
    GameObject[] cemberGrubuDizisi;
    GameObject[] textGrubuDizisi;
    GameObject[] cocuklar;
    GameObject[] textCocuklar;
    

    Rigidbody fizikbir;
    Rigidbody fizikiki;
    Rigidbody fizikCember;

    

    bool sahnebitisi = true;
    bool DonguKontrol = true;

    public float CemberHareketHizi;
    public float TextHareketHizi;
    float SahnedegismeZamani = 0;
    float uzunluk;
    float CemberOlusmaZamani=0f;
    




    //Vektör değerleri belirlenirken prefab üzerinde çember parentı 0 a alındı ve çocuklarının konumu yazıldı. Fakat çocukların konumu prefab sahneye geldiğinde konumlarda açıklık oluyor.
    //Açıklık miktarı ve istenilen ölçünün oranı alındı ve bu oran ilk atanan vektör değerleriyle çarpılarak son vektörler tanımlandı.
    Vector3 Orta = new Vector3(0.357f, 0.357f, -300f);
    Vector3 Ust = new Vector3(0.357f, 0.856f, -300f);
    Vector3 Sol = new Vector3(0.856f, 0.357f, -300f);
    Vector3 Sag = new Vector3(-0.167f, 0.357f, -300f);
    Vector3 Alt = new Vector3(0.357f, -0.167f, -300f);

    Vector3 textOrta = new Vector3(0.257f, 0.231f, -300f);
    Vector3 textUst = new Vector3(0.257f, 0.717f, -300f);
    Vector3 textSol = new Vector3(-0.267f, 0.231f, -300f);
    Vector3 textSag = new Vector3(0.756f, 0.231f, -300f);
    Vector3 textAlt = new Vector3(0.257f, -0.31f, -300f);



    Vector3[] vektorDizisi;
    Vector3[] textVektorDizisi;


    List<int> sayiListesi = new List<int>() {0,1,2,3,4};

    public int kacAdetCemberGrubu;
    public int kacAdetTextGrubu;
    int randomDegerimiz;
    int randomDegerimiz2;
    int sayac = 0;
    int sayac2 = 0;


    bool oyunBitisi = true;

    public Image olumarkaplan;
    float olumarkaplansayaci = 0;


    CemberDonme[] kod;

    





    void Start()
    {
        //Instantiate(blueText, new Vector3(0.856f, 0.357f, -300f), Quaternion.Euler(0,180,0));
       // Vektör listesi oluşturuldu ve her biri belirlenen konumlara atandı.
        vektorDizisi = new Vector3[5];
        textVektorDizisi = new Vector3[5];
        
        vektorDizisi[0] = Alt;
        vektorDizisi[1] = Sag;
        vektorDizisi[2] = Ust;
        vektorDizisi[3] = Sol;
        vektorDizisi[4] = Orta;

        textVektorDizisi[0] = textAlt;
        textVektorDizisi[1] = textSag;
        textVektorDizisi[2] = textUst;
        textVektorDizisi[3] = textSol;
        textVektorDizisi[4] = textOrta;





        fizikbir = altigenplatform1.GetComponent<Rigidbody>();
        fizikiki = altigenplatform2.GetComponent<Rigidbody>();
        //Cember grubunun alt nesnesi olarak 5 çember var. Ben her çembere rigidbody vermek yerini ana objeye yani ÇemberGrubuna rigidbody verdim ve buna kod üzerinde eriştim.
        fizikCember = cemberGrubu.GetComponent<Rigidbody>();

        uzunluk = 100;

        //Çember grubu dediğimiz şey 5 adet çemberden oluşmuş çember toplulugu.
        //ÇemberGrubuDizisi çember gruplarından oluşan bir dizi.
        //kaçAdetCemberGrubu ise bu diziye atanacak toplam çember grubu sayısı.
        cemberGrubuDizisi = new GameObject[kacAdetCemberGrubu];
        textGrubuDizisi = new GameObject[kacAdetTextGrubu];


        //Childlar için gameobject tanımladık.
        cocuklar = new GameObject[5];
        textCocuklar = new GameObject[5];


        //Çember grupları sahnede oluşturuldu ve oluşan çember gruplarının alt nesneleri random konumlandırıldı.Her bir çember grubuna hiz verildi.
        for(int i=0; i<cemberGrubuDizisi.Length; i++)
        {
            cemberGrubuDizisi[i] = Instantiate(cemberGrubu, new Vector3(0.1f, 0.126f, 0), Quaternion.identity);
            //Cember yerlerini random olusturma: 
            
            for (int z = 0; z < 5; z++)
            {                
                cocuklar[z] = cemberGrubuDizisi[i].transform.GetChild(z).gameObject;
            
                //0 ile liste elaman sayısı arasında random bir sayı belirliyoruz:
                randomDegerimiz = Random.Range(0, sayiListesi.Count);

                //Bir sayi tanımlıyoruz ve bu sayı listeden seçilmiş olan random bir ögeye eşit oluyor. 
                int sayi = sayiListesi.ElementAt(randomDegerimiz);
                
                //ÇemberGrubunun sırayla çocuklarını alıp daha önceden tanımlanmış olan vektörDizisinin random bir elemanına pozisyonunu eşitliyoruz.
                cocuklar[z].transform.position = vektorDizisi[sayi];

                //Elde ettiğimiz sayıyı siliyoruz ki yeniden o sayının karşılık geldiği konuma çember gönderilmesin.
                sayiListesi.RemoveAt(randomDegerimiz);
                
                
                //HAHAHAHAH YAPTIK!!!

            }
            //Yok ettiğimiz listeyi yeniden oluşturuyoruz. 2. Çember grubu için yeniden konumlandırma yapabilmesi için.
            sayiListesi = new List<int>() { 0, 1, 2, 3, 4 };


            //Dizinin sırayla elemanlarına erişip elemanlardan rigidboddy componentini çekip o komponente hiz atadik.
             
        }

        //Üst kısımda çember grupları için yapılan random oluşturma ışınlama ve hız verme işlemi şimdi textler için yapılıyor:
        for (int i = 0; i < textGrubuDizisi.Length; i++)
        {
            textGrubuDizisi[i] = Instantiate(textGrubu, new Vector3(0.1f, 0.126f, 0), Quaternion.identity);

            for (int z = 0; z < 5; z++)
            {
                textCocuklar[z] = textGrubuDizisi[i].transform.GetChild(z).gameObject;

                //0 ile liste elaman sayısı arasında random bir sayı belirliyoruz:
                randomDegerimiz2 = Random.Range(0, sayiListesi.Count);

                //Bir sayi tanımlıyoruz ve bu sayı listeden seçilmiş olan random bir ögeye eşit oluyor. 
                int sayi2 = sayiListesi.ElementAt(randomDegerimiz2);

                //ÇemberGrubunun sırayla çocuklarını alıp daha önceden tanımlanmış olan vektörDizisinin random bir elemanına pozisyonunu eşitliyoruz.
                textCocuklar[z].transform.position = vektorDizisi[sayi2];

                //Elde ettiğimiz sayıyı siliyoruz ki yeniden o sayının karşılık geldiği konuma çember gönderilmesin.
                sayiListesi.RemoveAt(randomDegerimiz2);


                //HAHAHAHAH YAPTIK!!!

            }
            //Yok ettiğimiz listeyi yeniden oluşturuyoruz. 2. Çember grubu için yeniden konumlandırma yapabilmesi için.
            sayiListesi = new List<int>() { 0, 1, 2, 3, 4 };

           

        }
        

    }

    //Altıgenlerin hareketi ve konumlandırılması ve çemberlere hız verilmesi.
    void FixedUpdate()
    {

        for (int i = 0; i < cemberGrubuDizisi.Length; i++)
        {
            cemberGrubuDizisi[i].GetComponent<Rigidbody>().velocity = new Vector3(0, 0, CemberHareketHizi);
        }

        for (int i = 0; i < textGrubuDizisi.Length; i++)
        {
            textGrubuDizisi[i].GetComponent<Rigidbody>().velocity = new Vector3(0, 0, CemberHareketHizi);

        }

        SahnedegismeZamani += Time.deltaTime; //Olay sıklığı.
        
        if (SahnedegismeZamani > 0.02)
        {
            
            SahnedegismeZamani = 0;
            fizikbir.transform.position = new Vector3(0.97f, -0.28f, (fizikbir.transform.position.z -0.3f));
            fizikiki.transform.position = new Vector3(0.97f, -0.28f, (fizikiki.transform.position.z -0.3f));

            
        }
    }


    //Cemberlerin olusma sikligini belirliyoruz. 
    //Uzunluga gore arkadaki sahnenin one eklenisi.
    //Textlerin oluşumu alt nesne yapılması ve sahneye gönderilmesi
    ////OYUN BİTİMİ SAHNE DEGİSİMİ FONKSİYONLARI
    void Update()
    {
        //Uzunluga gore arkadaki sahnenin one eklenisi.
        if (sahnebitisi)
        {
            if (altigenplatform1.transform.position.z <= -uzunluk)
            {
                altigenplatform1.transform.position += new Vector3(0, 0, uzunluk * 2);
            }
              
            if (altigenplatform2.transform.position.z <= -uzunluk)
            {
                altigenplatform2.transform.position += new Vector3(0, 0, uzunluk*2 );
                
            }
        }


        //oyunBitisisiBosGecis = ShuttleKontrol.oyunBittiBosGecis;
        //oyunBitisisiTemas = ShuttleKontrol.oyunBittiTemas;
       

        
        //Cemberlerin olusma sikligini belirliyoruz.
        //TEXTLERİ DE BURADA GÖNDERİYORUZ AYNI KODU BİDAHA YAZMAMAK İÇİN !!!!!!!!!!!!!! ÖNEMLİ !!!!!!!!!!!
        //Text grubu ve Çember grubu eş zamanlı olarak altıgenin içerisine ışınlanıyor ve textler çemberin alt nesnesi yapılıyor. Bu anda çemberin dönme kodu aktif ediliyor.
        CemberOlusmaZamani += Time.deltaTime;
        if (CemberOlusmaZamani > 3)
        {
            CemberOlusmaZamani = 0;

            cemberGrubuDizisi[sayac].transform.position = new Vector3(0f, 0.59f, 330);

            //For döngüsünün bir kere çalışmasını istiyorum.
            if (sayac2<5)
            {
                //Textler bir kere ışınlandıktan sonra birdaha ışınlanmayacaklar çünkü artık çemberlerin alt nesnesi oldular. Çemberlerin ışınlanması yeterli.
                for (int i = 0; i < textGrubuDizisi.Length; i++)
                {

                    textGrubuDizisi[sayac].transform.position = new Vector3(-0.4f, 0.3f, 330.03f);
                    textGrubuDizisi[sayac].transform.parent = cemberGrubuDizisi[sayac].transform;
                    cemberGrubuDizisi[sayac].GetComponent<CemberDonme>().enabled = true;
                    


                }
                

            }
            sayac2++;
            sayac++;
            if (sayac >= cemberGrubuDizisi.Length)
            {
                sayac = 0;
            }
            
        }



        //OYUN BİTİMİ SAHNE DEGİSİMİ:


        //Temas edilerek mekiğin savrulduğu geçiş:
        //OYUN BİTİMİ SAHNE DEGİSİMİ:
        oyunBitisi = ShuttleKontrol.oyunBittiTemas;

        //Temas edilerek mekiğin savrulduğu geçiş:
        if (ShuttleKontrol.oyunBittiTemas == false)
        {


            Invoke("TemasliBitis", 2);


        }


        //Yanlıs cemberden veya bosluktan temazsız geçişteki bitiş: ANİMASYON EKLENECEK PANEL GELECEK TAMAM VEYA DEVAM:
        if (ShuttleKontrol.oyunBittiBosGecis == false)
        {

            //Şimdilik oyunun bittiğini görelim diye menüye gönderiyorum. Bos gecis icin animasyon ve panel gelişi ayarlanacak.
            // BosGecisBitis()
            Time.timeScale = 0.3f;

            olumarkaplansayaci += 0.02f;
            olumarkaplan.color = new Color(0, 0, 0, olumarkaplansayaci);






            Invoke("BosGecisBitis", 0.5f);

        }
    }


    void TemasliBitis()
    {
        if (SceneManager.GetActiveScene().name == "GameEng")
        {
            SceneManager.LoadScene("reklamladevametmeeng");


        }
        if (SceneManager.GetActiveScene().name == "GameTur")
        {
            SceneManager.LoadScene("MainMenuTur");
        }

    }

    void BosGecisBitis()
    {
        if (SceneManager.GetActiveScene().name == "GameEng")
        {
            SceneManager.LoadScene("reklamladevametmeeng");


        }
        if (SceneManager.GetActiveScene().name == "GameTur")
        {
            SceneManager.LoadScene("MainMenuTur");
        }

    }

}
