using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ayarlar_Manager : MonoBehaviour
{
    public Slider gameSound;
    public Slider buttonSound;
    public Slider playerSound;
    public Slider otherSound;

    public AudioSource butonses;



    void Start()
    {
        gameSound.value = PlayerPrefs.GetFloat("GameSound");
        buttonSound.value = PlayerPrefs.GetFloat("ButtonSound");
        playerSound.value = PlayerPrefs.GetFloat("PlayerSound");
        otherSound.value = PlayerPrefs.GetFloat("OtherSound");

        butonses.volume= PlayerPrefs.GetFloat("ButtonSound");


    }

    public void SesiAyarla(string Hangiayar)
    {
        switch (Hangiayar)
        {

            case "gamesound":
                PlayerPrefs.SetFloat("GameSound",gameSound.value);
                break;

            case "buttonsound":
                PlayerPrefs.SetFloat("ButtonSound", buttonSound.value);
                break;

            case "playersound":
                PlayerPrefs.SetFloat("PlayerSound", playerSound.value);
                break;

            case "othersound":
                PlayerPrefs.SetFloat("OtherSound", otherSound.value);
                break;

        }



    }

   public void AnaMenuyeDon()
    {
        butonses.Play();
        SceneManager.LoadScene(0);

    }
}
