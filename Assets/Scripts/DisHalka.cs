using UnityEngine;

public class DisHalka : MonoBehaviour
{
    public float donmeHizi = 100f; // D�nme h�z� (derece/saniye cinsinden)

    void Update()
    {
        if(GameManager.oyunBittimi==false && GameManager.ekranadokunuldu==true) 
            
        // GameObject'i Z ekseni etraf�nda d�nd�rme
        transform.Rotate(Vector3.forward * donmeHizi * Time.deltaTime);
    }

  



}
