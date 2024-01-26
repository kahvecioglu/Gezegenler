using System.Collections.Generic;
using Bugra;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject circleObject; // Dönme yarýçapýný deðiþtireceðimiz dairesel nesne
    public float yaricapArtis = 0.5f; // Yarýçap artýþ miktarý
    int aktif_orbital=1;
    public static int panelcikissayisi = 0;

    public Button arti_buton;
    public Button eksi_buton;
    public GameObject[] elmaslar;
    public static bool oyunBittimi=false;
    public static int skor = 0;
    public static bool ekranadokunuldu=false;
    public GameObject dokunmaPaneli;
    public GameObject yeniSkorPaneli;
    public TextMeshProUGUI skortext;

    public Sprite[] spriteler;
    SpriteRenderer spriteRenderer;
    public GameObject oyuncu;

    public AudioSource butonses;
    public TextMeshProUGUI paratexti;
    public AudioSource baslamases;
    public AudioSource orbitalatlamasesi;






    public List<int> oncekiRastgeleler = new List<int>();

  
    private void Start()
    {
        butonses.volume= PlayerPrefs.GetFloat("ButtonSound");
        baslamases.volume = PlayerPrefs.GetFloat("OtherSound");

        orbitalatlamasesi.volume = PlayerPrefs.GetFloat("PlayerSound");
        paratexti.text = PlayerPrefs.GetInt("Toplamelmas").ToString();
        eksi_buton.interactable = false;
        butonses.Pause();
        skortext.text = 0.ToString();

        spriteRenderer = oyuncu.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spriteler[PlayerPrefs.GetInt("Aktifindex")];




    }

    public void RastegelSayiOlustur()
    {
        int rastgele;

        do
        {
            rastgele = Random.Range(0, 24);
        } while (oncekiRastgeleler.Contains(rastgele));

        oncekiRastgeleler.Add(rastgele);
        elmaslar[rastgele].SetActive(true);

        if (oncekiRastgeleler.Count == 24)
        {
            oncekiRastgeleler.Clear();

        }
    }


    private void Update()
    {
        skortext.text = skor.ToString();


        if (skor > PlayerPrefs.GetInt("Enyuksekskor"))
        {                                 
            PlayerPrefs.SetInt("Enyuksekskor", skor);
            if (panelcikissayisi == 0)
            {
                yeniSkorPaneli.SetActive(true);
            }
            Invoke("PaneliKapat", 5f);
            panelcikissayisi = 1;
        }

        


    }

    public void PaneliKapat()
    {

        yeniSkorPaneli.SetActive(false);


    }

    public void YaricapArttýr(int x)
    {
        
            orbitalatlamasesi.Play();
        
      
        if(x==0)
        {
            aktif_orbital++;
            // Dönme yarýçapýný artýr
            circleObject.transform.localScale += new Vector3(yaricapArtis, yaricapArtis, yaricapArtis);          
        }
        else
        {
             aktif_orbital--;
            // Dönme yarýçapýný azalt
            circleObject.transform.localScale += new Vector3(-yaricapArtis, -yaricapArtis, -yaricapArtis);
        }

        if(aktif_orbital==3)
        {
            arti_buton.interactable = false;
            Oyuncu.donmehizi = 110;
            
        }
        else
        {
            arti_buton.interactable = true;
        }


        if (aktif_orbital == 1)
        {
            eksi_buton.interactable = false;
            Oyuncu.donmehizi = 150;


        }
        else
        {
            eksi_buton.interactable = true;
        }

        if (aktif_orbital == 2)
        {

            Oyuncu.donmehizi = 130;

        }

        

    }

    public void Dokunuldu()
    {
        baslamases.Play();
        ekranadokunuldu = true;
        dokunmaPaneli.SetActive(false);
        RastegelSayiOlustur();


    }
   
    public void AnaMenuyeDon()
    {
        butonses.Play();
        ekranadokunuldu = false;
        oyunBittimi = false;
        SceneManager.LoadScene(0);
        skor = 0;

    }

    public void AyarlaraDon()
    {
        butonses.Play();
        ekranadokunuldu = false;
        oyunBittimi = false;
        SceneManager.LoadScene(3);
        skor = 0;

    }

    public void TekrarOyna()
    {
        butonses.Play();
        ekranadokunuldu = false;
        oyunBittimi = false;
        SceneManager.LoadScene(1);
        skor = 0;




    }

}
