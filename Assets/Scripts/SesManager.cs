using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SesManager : MonoBehaviour
{
    
    private static GameObject instance;
    public AudioSource ses;
    private void Start()
    {
        ses.volume = PlayerPrefs.GetFloat("GameSound");
        DontDestroyOnLoad(gameObject);

        if(instance == null)
        {
            instance = gameObject;
        }
        else
        {

            Destroy(gameObject);
        }

    }

    private void Update()
    {
        ses.volume = PlayerPrefs.GetFloat("GameSound");

    }


}
