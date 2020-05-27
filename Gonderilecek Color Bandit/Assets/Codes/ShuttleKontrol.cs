using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
public class ShuttleKontrol : MonoBehaviour
{
    Rigidbody fizik;

    Vector3 vec;
    Vector3 kameraSonPos;
    Vector3 kameraIlkPos;


    float yatay;
    float dikey;
    float yatayDonus = 0;
    float dikeyDonus = 0;


    public GameObject renkdegistirici;
    public GameObject sayaccollider;
    GameObject uzaymekigi;
    GameObject kamera;
    
    

    Color32[] cemberrenkleri = new Color32[5];
    

    public Text puantext;

    int puan = 0;
    int enYuksekPuan = 0;
    int sayac = 0;

    public static bool oyunBittiTemas = true;
    public static bool oyunBittiBosGecis = true;

    ShuttleKontrol araba;



    void Start()
    {
        
        enYuksekPuan = PlayerPrefs.GetInt("enYuksekPuanKayit");
        
        cemberrenkleri[0] = new Color32((byte)255, (byte)243,(byte)0,(byte) 255);
        cemberrenkleri[1] = new Color32((byte)255, (byte)18, (byte)34, (byte)255);
        cemberrenkleri[2] = new Color32((byte)97, (byte)255, (byte)45, (byte)255);
        cemberrenkleri[3] = new Color32((byte)59, (byte)192, (byte)255, (byte)255);
        cemberrenkleri[4] = new Color32((byte)217, (byte)35, (byte)222, (byte)255);

        uzaymekigi = transform.GetChild(0).gameObject;

        fizik = GetComponent<Rigidbody>();

        araba = GameObject.FindGameObjectWithTag("yenishuttle").GetComponent<ShuttleKontrol>();

        kamera = GameObject.FindGameObjectWithTag("MainCamera");

        kameraIlkPos = kamera.transform.position - transform.position;
    }

    //Hareket ve mekik rotasyon kodları:
    void FixedUpdate()
    {
        // Hareket kodu oluşturuldu. GetAxisRaw yerine getaxis kullanıldı. Rotasyonu verirken bir anda değil de kademe kademe rotasyon olması için.
        float yatay = Input.GetAxis("Horizontal");
        float dikey = Input.GetAxis("Vertical");
        Vector3 vec = new Vector3(yatay, dikey, 0);
        fizik.velocity = vec;
        Rotasyon();

    }
   

   

    //Kamera olayları:
    void LateUpdate()
    {
        //if (transform.position.y < 1.2)
        {
            kameraKontrol();
            Debug.Log(kamera.transform.position + "Kamera transform position");
            Debug.Log(kameraIlkPos + "Ilk pos");
            Debug.Log(kameraSonPos + "Kamera son pos.");
        }
    }


    void OnTriggerExit(Collider other)
    {
        //Renk degistirici icinden gecince mekigin rengini degistirme kodu:
        if (other.gameObject.tag == "yellowText")
        {
            uzaymekigi.GetComponent<Renderer>().material.color = new Color32((byte)255, (byte)243, (byte)0, (byte)255);
            uzaymekigi.tag = "yellow";
        }
        if (other.gameObject.tag == "redText")
        {
            uzaymekigi.GetComponent<Renderer>().material.color = new Color32((byte)255, (byte)18, (byte)34, (byte)255);
            uzaymekigi.tag = "red";
        }
        if (other.gameObject.tag == "greenText")
        {
            uzaymekigi.GetComponent<Renderer>().material.color = new Color32((byte)97, (byte)255, (byte)45, (byte)255);
            uzaymekigi.tag = "green";
        }
        if (other.gameObject.tag == "blueText")
        {
            uzaymekigi.GetComponent<Renderer>().material.color = new Color32((byte)59, (byte)192, (byte)255, (byte)255);
            uzaymekigi.tag = "blue";
        }
        if (other.gameObject.tag == "purpleText")
        {
            uzaymekigi.GetComponent<Renderer>().material.color = new Color32((byte)217, (byte)35, (byte)222, (byte)255);
            uzaymekigi.tag = "magenta";
        }
    }
    //Renk değişimi puan artması en yüksek skor kaydedilmesi ve boş geçiş bitişi.
    void OnTriggerEnter(Collider other)
    {
        //Gelen ilk renk değiştirici random renk atar ve o rengin tagını atar.
        if (other.gameObject.tag == "renkdegistirici")
        {
            // BURASI DA BELİRLENEN RENKLERE GÖRE DEGİSTİRİLECEK.
            uzaymekigi.GetComponent<Renderer>().material.color = cemberrenkleri[Random.Range(0, cemberrenkleri.Length)];


            if (uzaymekigi.GetComponent<Renderer>().material.color == new Color32((byte)255, (byte)243, (byte)0, (byte)255))
            {
                uzaymekigi.tag = "yellow";
                Debug.Log("TAG DEGİSTİRİLDİ");
            }
            if (uzaymekigi.GetComponent<Renderer>().material.color == new Color32((byte)255, (byte)18, (byte)34, (byte)255))
            {
                uzaymekigi.tag = "red";
                Debug.Log("TAG DEGİSTİRİLDİ");
            }
            if (uzaymekigi.GetComponent<Renderer>().material.color == new Color32((byte)97, (byte)255, (byte)45, (byte)255))
            {
                uzaymekigi.tag = "green";
                Debug.Log("TAG DEGİSTİRİLDİ");
            }
            if (uzaymekigi.GetComponent<Renderer>().material.color == new Color32((byte)217, (byte)35, (byte)222, (byte)255))
            {
                uzaymekigi.tag = "magenta";
                Debug.Log("TAG DEGİSTİRİLDİ");
            }
            if (uzaymekigi.GetComponent<Renderer>().material.color == new Color32((byte)59, (byte)192, (byte)255, (byte)255))
            {
                uzaymekigi.tag = "blue";
                Debug.Log("TAG DEGİSTİRİLDİ");
            }
        }


        //Aynı renkli(aynı taglı) cember icinden gecince puan kazan.
        if (other.gameObject.tag == uzaymekigi.tag)
        {
            puan++;
            puantext.text = "Score= " + puan;
        }

        //Sayaccollider triggerlandığında skor ve trigger sayısı eşit değilse oyunu bitir.
        if (other.gameObject.tag == "sayaccollider")
        {
            sayac++;
            //Debug.Log(sayac);

            if (sayac > puan)
            {
                oyunBittiBosGecis = false;
                //Debug.Log("oyunhicbircemberecarpmamaktandolayıbitti");
                if (puan > enYuksekPuan)
                {
                    enYuksekPuan = puan;
                    PlayerPrefs.SetInt("enYuksekPuanKayit", enYuksekPuan);
                }


            }
        }

        //İçinden gecilen collider tagı renk degistirici veya aynı renkte olunan çember değilse oyun bitti bool false yap.

        if (other.gameObject.tag == uzaymekigi.tag || other.gameObject.tag == "renkdegistirici" || other.gameObject.tag == "sayaccollider" || other.gameObject.tag == "yellowText" || other.gameObject.tag == "redText" || other.gameObject.tag == "greenText" || other.gameObject.tag == "blueText" || other.gameObject.tag == "purpleText")
        {
        }

        //Oyunun bitme durumu:
        else
        {
            oyunBittiBosGecis = false;
            //Debug.Log("oyunbitti");

            if (puan > enYuksekPuan)
            {
                enYuksekPuan = puan;
                PlayerPrefs.SetInt("enYuksekPuanKayit", enYuksekPuan);
            }

        }



    }




    //Cembere temasta oyunu bitir. oyunBittiTemas ı false yap kodu kapat:
    void OnCollisionEnter(Collision collision)
    {
        //Cember yüzeylerine temasta kodu kapat oyunu bitir.
        if (collision.gameObject.tag == "Orta" || collision.gameObject.tag == "Üst" || collision.gameObject.tag == "Sol" || collision.gameObject.tag == "Sağ" || collision.gameObject.tag == "Alt")
        {
            //Oyun bitiminde diğer geçişe dönmesin diye puan ekliyoruz nasıl olsa yazdırılmıyor kod kapandığı.
            puan++;
            fizik.constraints = RigidbodyConstraints.None;
            //Araba = shuttlekontrol kodu
            araba.enabled = false;
            //Debug.Log("oyuncemberecarpmaktandolayıbitti");
            oyunBittiTemas = false;
             
        }
    }


    void kameraKontrol()

    {
        kameraSonPos = new Vector3
            (
            (kameraIlkPos.x + transform.position.x),            //Kameranın gideceği son pozisyonun x'i
            Mathf.Clamp((kameraIlkPos.y + transform.position.y), -10, 
            1.3f),                                                   //Kameranın gideceği son pozisyonunun sınırlandırılmış y'si
            (kameraIlkPos.z + transform.position.z)             //Kameranın gideceği son pozisyonun z'si
            );

        kamera.transform.position = Vector3.Lerp(kamera.transform.position, kameraSonPos, 1f);

    }
    void Rotasyon()
    {


        //Zıt tuşa basıldığında önce rotasyon 0 a gelmeli daha sonra diğer yöne dönmeli. (SAĞLANDI)
        // Herhangi bir tuşa basılmadığında 0 rotasyonuna dönmesi isteniyor.
        //SORUN!!! -40 rotasyonunda 0.5 saniyelik bir bekleme yaptıktan sonra başlıyor 0 rotasyonu dönüşe.Çünkü hız -1 den 0 a yavaş yavaş geliyor tuşa basmayı bıraktığımızda.
        //SORUN Mobil entegrasyon kısmında yeniden gözden geçirilecek.
        if (fizik.velocity.x == 0)
        {
            //YatayDonus degerimiz ekside kaldıysa 3 eklenecek artıda kaldıysa 3 çıkartılacak ve eski konuma dönüş sağlanacak.
            if (yatayDonus < 0)
            {
                transform.rotation = Quaternion.Euler(-dikeyDonus, 0, -yatayDonus);
                yatayDonus = yatayDonus + 3;
            }
            if (yatayDonus > 0)
            {
                transform.rotation = Quaternion.Euler(-dikeyDonus, 0, -yatayDonus);
                yatayDonus = yatayDonus - 3;
            }
        }
        //Eğer cisim sola doğru gidiyorsa yatayDonus degiskenine -3 ekle ve -40 a ulaştığında sınırla.
        if (fizik.velocity.x < 0)
        {
            transform.rotation = Quaternion.Euler(-dikeyDonus, 0, -yatayDonus);
            yatayDonus = yatayDonus - 3;
            if (yatayDonus < -40)
            {
                yatayDonus = -40;
            }
        }
        //Cisim sağa gitmeye başladıysa yatayDonuse 3 er 3 er ekle. Sola yatık olan mekik 0 rotasyonuna ışınlanmadan önce 0 a sonra 40 a gidebilsin böylece.
        if (fizik.velocity.x > 0)
        {
            transform.rotation = Quaternion.Euler(-dikeyDonus, 0, -yatayDonus);
            yatayDonus = yatayDonus + 3;
            if (yatayDonus > 40)
            {
                yatayDonus = 40;
            }

        }
        //---------------------- Şimdi aynılarını dikey harekey için
        if (fizik.velocity.y == 0)
        {
            //YatayDonus degerimiz ekside kaldıysa 3 eklenecek artıda kaldıysa 3 çıkartılacak ve eski konuma dönüş sağlanacak.
            if (dikeyDonus < 0)
            {
                transform.rotation = Quaternion.Euler(-dikeyDonus, 0, -yatayDonus);
                dikeyDonus = dikeyDonus + 3;
            }
            if (dikeyDonus > 0)
            {
                transform.rotation = Quaternion.Euler(-dikeyDonus, 0, -yatayDonus);
                dikeyDonus = dikeyDonus - 3;
            }
        }
        //Eğer cisim sola doğru gidiyorsa yatayDonus degiskenine -3 ekle ve -40 a ulaştığında sınırla.
        if (fizik.velocity.y < 0)
        {
            transform.rotation = Quaternion.Euler(-dikeyDonus, 0, -yatayDonus);
            dikeyDonus = dikeyDonus - 3;
            if (dikeyDonus < -30)
            {
                dikeyDonus = -30;
            }
        }
        //Cisim sağa gitmeye başladıysa yatayDonuse 3 er 3 er ekle. Sola yatık olan mekik 0 rotasyonuna ışınlanmadan önce 0 a sonra 40 a gidebilsin böylece.
        if (fizik.velocity.y > 0)
        {
            transform.rotation = Quaternion.Euler(-dikeyDonus, 0, -yatayDonus);
            dikeyDonus = dikeyDonus + 3;
            if (dikeyDonus > 30)
            {
                dikeyDonus = 30;
            }

        }
    }




    
    


}