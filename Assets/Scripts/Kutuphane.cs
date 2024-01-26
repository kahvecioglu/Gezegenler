using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.IO.LowLevel.Unsafe;
using System.IO;
using GoogleMobileAds.Api;

namespace Bugra


{

    [Serializable]
    public class ItemBilgileri
    {
        public int itemIndex;
        public string item_ad;
        public int para;
        public bool satinAlmaDurumu;

    }

    public class VeriYonetimi
    {

        public void Save(List<ItemBilgileri> _ItemBilgileri)
        {

            BinaryFormatter bf=new BinaryFormatter();
            FileStream file = File.OpenWrite(Application.persistentDataPath + "/ItemVerileri3.gd");
            bf.Serialize(file, _ItemBilgileri);
            file.Close();



        }

        public void IlkKurulum(List<ItemBilgileri> _ItemBilgileri)
        {
            if(!File.Exists(Application.persistentDataPath + "/ItemVerileri3.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/ItemVerileri3.gd");
                bf.Serialize(file, _ItemBilgileri);
                file.Close();
            }
           
        }


        List<ItemBilgileri> _ItemIcliste;
        public void Load()
        {

            if (File.Exists(Application.persistentDataPath + "/ItemVerileri3.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/ItemVerileri3.gd", FileMode.Open);
                _ItemIcliste = (List<ItemBilgileri>)bf.Deserialize(file);
                file.Close();

            }

        }

        public List<ItemBilgileri> ListeyiAktar()
        {
            return _ItemIcliste;
        }




    }

    public class ReklamYonetimi
    {
        private InterstitialAd interstitialAd;
        private RewardedAd rewardedAd;

        // Geçiþ Reklamý

        public void RequestInterstitial()
        {
            string AdUnitId;
#if UNITY_ANDROID 
            AdUnitId= "ca-app-pub-2857887652652750/8242004443";
#elif UNITY_IPHONE
             AdUnitId="ca-app-pub-3940256099942544/4411468910";
#else
                AdUnitId="unexpected_platform";
#endif



            interstitialAd=new InterstitialAd(AdUnitId);
            AdRequest adRequest=new AdRequest.Builder().Build();
            interstitialAd.LoadAd(adRequest);
            
            interstitialAd.OnAdClosed += GecisReklamiKapatildi;

        }

        private void GecisReklamiKapatildi(object sender, EventArgs e)
        {
            interstitialAd.Destroy();
            RequestInterstitial();
        }
        public void GecisReklamiGoster()
        {
            if(PlayerPrefs.GetInt("GecisReklamiSayisi")==2)
            {
                if(interstitialAd.IsLoaded())
                {
                    PlayerPrefs.SetInt("GecisReklamiSayisi", 0);
                    interstitialAd.Show();

                }
                else
                {

                    interstitialAd.Destroy();
                    RequestInterstitial();



                }


            }
            else
            {
                PlayerPrefs.SetInt("GecisReklamiSayisi", PlayerPrefs.GetInt("GecisReklamiSayisi") + 1);


            }



        }

        //--------ÖDÜLLÜ REKLAM

        public void RequestRewarded()
        {

            string AdUnitId;

#if    UNITY_ANDROID
            AdUnitId = "ca-app-pub-2857887652652750/4401217382";
#elif  UNITY_IPHONE
            AdUnitId="ca-app-pub-3940256099942544/1712485313";
#else
            AdUnitId="unexpected_platform";
#endif


            rewardedAd=new RewardedAd(AdUnitId);
            AdRequest adRequest=new AdRequest.Builder().Build();
            rewardedAd.LoadAd(adRequest);

            rewardedAd.OnUserEarnedReward += OdulluReklamTamamlandi;
            rewardedAd.OnAdClosed += OdulluReklamKapatildi;
            rewardedAd.OnAdLoaded += OdulluReklamYuklendi;






        }

        private void OdulluReklamTamamlandi(object sender, Reward e)
        {
            PlayerPrefs.SetInt("Toplamelmas", PlayerPrefs.GetInt("Toplamelmas") + 30 );

        }

        private void OdulluReklamKapatildi(object sender, EventArgs e)
        {
            Debug.Log("reklam kapatýldý");
            RequestRewarded();
        }

        private void OdulluReklamYuklendi(object sender, EventArgs e)
        {
            Debug.Log("reklam yüklendi");
        }
        public void OdulluReklamGoster()
        {
            if (rewardedAd.IsLoaded())
            {

                rewardedAd.Show();

            }

        }
    }



}
