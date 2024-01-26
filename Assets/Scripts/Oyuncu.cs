using System.Collections;
using System.Collections.Generic;
using Bugra;
using TMPro;
using UnityEngine;

public class Oyuncu : MonoBehaviour
{
    public static float donmehizi = 150f;
    public GameObject gameoverPaneli;
    public GameManager gameManager;
    public TextMeshProUGUI Gskortext;
    public TextMeshProUGUI Genyuksekskortext;
    public TextMeshProUGUI GodulText;
    int carpan = 2;
    public AudioSource toplamases;
    public AudioSource gameoverses;

    ReklamYonetimi reklamYonetimi = new ReklamYonetimi();


    private void Start()
    {
        reklamYonetimi.RequestInterstitial();
        gameoverses.volume = PlayerPrefs.GetFloat("OtherSound");
        toplamases.volume = PlayerPrefs.GetFloat("OtherSound");



    }

    private void Update()
    {
        if (GameManager.oyunBittimi == false && GameManager.ekranadokunuldu==true)
        {
            transform.Rotate(Vector3.forward, donmehizi * Time.deltaTime);
            
        }
       


    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("dusman"))
        {
           
            reklamYonetimi.GecisReklamiGoster();
            gameoverses.Play();
            GameManager.oyunBittimi = true;
            gameoverPaneli.SetActive(true);
            PlayerPrefs.SetInt("Sonskor", GameManager.skor);
            Gskortext.text = GameManager.skor.ToString();
            Genyuksekskortext.text = PlayerPrefs.GetInt("Enyuksekskor").ToString();
            GodulText.text=(carpan * GameManager.skor).ToString();
            int odul = carpan * GameManager.skor;
            PlayerPrefs.SetInt("Toplamelmas", PlayerPrefs.GetInt("Toplamelmas") + odul);

            GameManager.panelcikissayisi = 0;


        }else if (collision.CompareTag("elmas"))
        {
            toplamases.Play();
            collision.gameObject.SetActive(false);
            GameManager.skor++;
            gameManager.RastegelSayiOlustur();
            



        }




    }

   

}
