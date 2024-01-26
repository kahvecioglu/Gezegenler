using UnityEngine;

public class DisHalka : MonoBehaviour
{
    public float donmeHizi = 100f; // Dönme hýzý (derece/saniye cinsinden)

    void Update()
    {
        if(GameManager.oyunBittimi==false && GameManager.ekranadokunuldu==true) 
            
        // GameObject'i Z ekseni etrafýnda döndürme
        transform.Rotate(Vector3.forward * donmeHizi * Time.deltaTime);
    }

  



}
