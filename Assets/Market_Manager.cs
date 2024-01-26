using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Bugra;
using TMPro;

public class Market_Manager : MonoBehaviour
{
    public GameObject panelIki;
    public GameObject panelBir;
    
    public Button sagbuton;
    public Button solbuton;

    public Button satinAlbuton;
    public Button kusanbuton;
    public GameObject butontext;
    public GameObject noMoneyYazisi;



    public TextMeshProUGUI toplamelmastext;  
    public TextMeshProUGUI ortaisim;
    public TextMeshProUGUI ortapara;

    public static int aktifindex = 0;
    public AudioSource butonses;
    public AudioSource coinses;
    public AudioSource kusanses;

    VeriYonetimi _veriYonetimi =new VeriYonetimi();
    public List<ItemBilgileri> _varsayilanItemBilgileri=new List<ItemBilgileri>();
    ReklamYonetimi reklamYonetimi = new ReklamYonetimi();

    public Animator satinAlmaAnimatoru;
    public Animator kusanmaAnimatoru;
    public GameObject kusanildiResmi;

    public Image resminImagesi;
    public Sprite[] newSprite; // Yeni Texture2D

    void Start()
    {
        reklamYonetimi.RequestRewarded();
        butonses.volume = PlayerPrefs.GetFloat("ButtonSound");
        coinses.volume= PlayerPrefs.GetFloat("ButtonSound");
        kusanses.volume = PlayerPrefs.GetFloat("ButtonSound");

        aktifindex = PlayerPrefs.GetInt("Aktifindex");


        if (PlayerPrefs.GetInt("Satinalindimi")==0) 
        {

            ortaisim.text = _varsayilanItemBilgileri[0].item_ad;
            ortapara.text = _varsayilanItemBilgileri[0].para.ToString();
            satinAlbuton.interactable = false;
            kusanbuton.interactable = true;
            panelBir.SetActive(true);
            panelIki.SetActive(false);
            solbuton.interactable = false;
            sagbuton.interactable = true;


        }
        else
        {
            if (PlayerPrefs.GetInt("Aktifindex")>8)
            {
                panelBir.SetActive(false);
                panelIki.SetActive(true);
                solbuton.interactable = true;
                sagbuton.interactable = false;

            }
            else
            {
                panelBir.SetActive(true);
                panelIki.SetActive(false);
                solbuton.interactable = false;
                sagbuton.interactable = true;
            }

            ortaisim.text = _varsayilanItemBilgileri[PlayerPrefs.GetInt("Aktifindex")].item_ad;
            ortapara.text = _varsayilanItemBilgileri[PlayerPrefs.GetInt("Aktifindex")].para.ToString();
            if (_varsayilanItemBilgileri[PlayerPrefs.GetInt("Aktifindex")].satinAlmaDurumu == true)
            {
                satinAlbuton.interactable = false;
                kusanbuton.interactable = true;

            }
            else
            {
                satinAlbuton.interactable = true;
                kusanbuton.interactable = false;
            }

        }
        
     
        toplamelmastext.text = PlayerPrefs.GetInt("Toplamelmas").ToString();
        _veriYonetimi.IlkKurulum(_varsayilanItemBilgileri);
        _veriYonetimi.Load();
       _varsayilanItemBilgileri= _veriYonetimi.ListeyiAktar();           
       

    }

  
    // Update is called once per frame
    void Update()
    {


        toplamelmastext.text = PlayerPrefs.GetInt("Toplamelmas").ToString();

        if (_varsayilanItemBilgileri[aktifindex].satinAlmaDurumu == true)
        {
            satinAlbuton.interactable = false;
            kusanbuton.interactable= true;
            butontext.SetActive(false);



        }
        else
        {

            if(PlayerPrefs.GetInt("Toplamelmas") < _varsayilanItemBilgileri[aktifindex].para)
            {
                satinAlbuton.interactable = false;
                kusanbuton.interactable = false;
                
                butontext.SetActive(true);



            }
            else
            {
                butontext.SetActive(false);

                if (aktifindex != 0)
                {
                    satinAlbuton.interactable = true;
                    kusanbuton.interactable = false;
                }
               

            }

            

        }

        if (aktifindex == 0)
        {
            satinAlbuton.interactable = false;
            kusanbuton.interactable = true;

        }
        

    }

    public void AnamenuyeDon()
    {
        butonses.Play();
        SceneManager.LoadScene(0);
        _veriYonetimi.Save(_varsayilanItemBilgileri);
        

    }

    public void SagaGel()
    {

        butonses.Play();
        panelBir.SetActive(false);
        panelIki.SetActive(true);
        solbuton.interactable = true;
        sagbuton.interactable = false;

    }

    public void SolaGel()
    {

        butonses.Play();
        panelBir.SetActive(true);
        panelIki.SetActive(false);
        solbuton.interactable = false;
        sagbuton.interactable = true;

    }



    public void SecilenTop(int index)
    {
        butonses.Play();
        aktifindex = index;
        ortaisim.text = _varsayilanItemBilgileri[aktifindex].item_ad;
        ortapara.text = _varsayilanItemBilgileri[aktifindex].para.ToString();
        

    }

    public void SatinAl()
    {
       

        satinAlmaAnimatoru.SetBool("SatinAlindi", true);
        coinses.Play();
        _varsayilanItemBilgileri[aktifindex].satinAlmaDurumu = true;
        satinAlbuton.interactable = false;
        kusanbuton.interactable = true;

        PlayerPrefs.SetInt("Toplamelmas", PlayerPrefs.GetInt("Toplamelmas") - _varsayilanItemBilgileri[aktifindex].para);
        PlayerPrefs.SetInt("Satinalindimi", 1);


    }

    public void Kusan()
    {
        resminImagesi.sprite = newSprite[aktifindex];

        kusanildiResmi.SetActive(true);
        kusanmaAnimatoru.SetBool("Kusanildi", true);
        Invoke("KusanildiIptal", 1);
       kusanses.Play();
        PlayerPrefs.SetInt("Aktifindex", aktifindex);
        satinAlbuton.interactable = false;
       


    }

    public void KusanildiIptal()
    {
        kusanildiResmi.SetActive(false);

    }

    public void ButonTexti()
    {
        noMoneyYazisi.SetActive(true);
        Invoke("ButonTextiIptal", 1.5f);


    }

    public void ButonTextiIptal()
    {
        noMoneyYazisi.SetActive(false);



    }

    public void OdulluReklamGosterMarket()
    {

        reklamYonetimi.OdulluReklamGoster();

    }

}
