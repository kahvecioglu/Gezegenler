using System.Collections;
using System.Collections.Generic;
using Bugra;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AnaMenu_Manager : MonoBehaviour
{
    public TextMeshProUGUI enyuksekText;
    public TextMeshProUGUI sonskortext;
    public TextMeshProUGUI toplamelmastext;
    public AudioSource butonses;


    ReklamYonetimi reklamYonetimi = new ReklamYonetimi();



    private void Start()
    {
        butonses.volume = PlayerPrefs.GetFloat("ButtonSound");

        reklamYonetimi.RequestRewarded();

        if (PlayerPrefs.HasKey("Enyuksekskor"))
        {
            enyuksekText.text = PlayerPrefs.GetInt("Enyuksekskor").ToString();
            sonskortext.text = PlayerPrefs.GetInt("Sonskor").ToString();




        }
        else
        {
            PlayerPrefs.SetInt("Aktifindex", 0);
            PlayerPrefs.SetInt("Enyuksekskor", 0);
            PlayerPrefs.SetInt("sonskor", 0);
            PlayerPrefs.SetInt("Toplamelmas",0);
            PlayerPrefs.SetInt("Sonskor", 0);

            PlayerPrefs.SetFloat("GameSound", 0.1f);
            PlayerPrefs.SetFloat("ButtonSound", 0.6f);
            PlayerPrefs.SetFloat("PlayerSound", 0.6f);
            PlayerPrefs.SetFloat("OtherSound", 0.5f);


            enyuksekText.text = 0.ToString();
            sonskortext.text = 0.ToString();
            toplamelmastext.text = 0.ToString();

        }

     



    }

    private void Update()
    {
        toplamelmastext.text = PlayerPrefs.GetInt("Toplamelmas").ToString();

    }
    public void OyunSahnesineGec()
    {
        butonses.Play();
        SceneManager.LoadScene(1);

    }

    public void AlýsveriseGit()
    {
        butonses.Play();
        SceneManager.LoadScene(2);



    }
    public void AyarlaraGit()
    {
        butonses.Play();
        SceneManager.LoadScene(3);


    }

    public void CikisYap()
    {
        Application.Quit();



    }

    public void OdulluReklamGoster()
    {

        reklamYonetimi.OdulluReklamGoster();

    }

}
