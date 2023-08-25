using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AddManager : MonoBehaviour
{
    public Vidas vida;
    InterstitialAd intersticial;
    RewardedAd recompensado;
    GameContrrollerMenu controller;

    private void Start()
    {
        MobileAds.Initialize(initStatus => { });
        CargarIntersticial();
        CargaRecompensado();
        controller = FindObjectOfType<GameContrrollerMenu>();
    }

    void CargarIntersticial()
    {
        string idTest = "ca-app-pub-9230024696687921/5047417084";//test
        this.intersticial = new InterstitialAd(idTest);
        

        this.intersticial.OnAdLoaded += HandleOnAdLoaded;
        this.intersticial.OnAdOpening += HandleOnAdOpening;
        this.intersticial.OnAdClosed += HandleOnAdClosed;

        
        AdRequest request = new AdRequest.Builder().Build();
        this.intersticial.LoadAd(request);
    }

    void CargaRecompensado()
    {
        string idTest = "ca-app-pub-9230024696687921/7329260251";//test
        this.recompensado = new RewardedAd(idTest);


        this.recompensado.OnAdLoaded += HandleRewardedAdLoaded;
        this.recompensado.OnAdOpening += HandleRewardedAdOpening;
        this.recompensado.OnUserEarnedReward += HandleUserEarnedReward;
        this.recompensado.OnAdClosed += HandleRewardedAdClosed;



        AdRequest request = new AdRequest.Builder().Build();
        this.recompensado.LoadAd(request);

    }

    public void LlamarIntersticial()
    {
        if(this.intersticial.IsLoaded())
        {
            this.intersticial.Show();
        }
    }
    public void LlamarRecompensado()
    {
        if (this.recompensado.IsLoaded())
        {
            this.recompensado.Show();
        }
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        print("secargo");
    }
    public void HandleOnAdOpening(object sender, EventArgs args)
    {
        //Desacrivar la musica
    }
    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        CargarIntersticial();
    }

    //RECOMPENSADO
    public void HandleRewardedAdLoaded(object sender, EventArgs args) 
    {
        print("cargarecompensa");
    }
    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        //quitar musica
    }
    public void HandleUserEarnedReward(object sender, EventArgs args)
    {
        vida.vidasRestantes++;
        if(controller != null) 
        {
            controller.RefrescarVidas();
        }
    }
    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        CargaRecompensado();
    }

}
