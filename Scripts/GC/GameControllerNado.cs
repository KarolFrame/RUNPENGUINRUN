using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Rendering;

public class GameControllerNado : MonoBehaviour
{
    Leopardo[] pinchhosBajo;
    Leopardo pinchoBajoObj;
    Pajaro[] pinchosAlto;
    Pajaro pinchoAltoObj;
    Peces[] peces;
    Peces pezObj;
    MontañaMovement[] montañas;
    Player player;

    public Transform[] tps;

    public TMPro.TextMeshProUGUI textoTiempo;
    float tiempo;

    bool enJuego = true;

    public GameObject panelInicio;

    private void Awake()
    {
        pinchhosBajo = FindObjectsOfType<Leopardo>();
        pinchosAlto= FindObjectsOfType<Pajaro>();
        montañas = FindObjectsOfType<MontañaMovement>();
        peces = FindObjectsOfType<Peces>();
        player = FindObjectOfType<Player>();
    }
    void Start()
    {
        StartCoroutine(GenerarPinchos());
        StartCoroutine(AumentarVelocidades());

        enJuego = false;
    }

    void Update()
    {
        if(!enJuego) 
        {
            Time.timeScale = 0;
        }
        else
            Time.timeScale = 1;

        tiempo += Time.deltaTime;
        textoTiempo.text = "Tiempo: " + Mathf.Round(tiempo).ToString() + " s";
    }

    IEnumerator GenerarPinchos()
    {
        for (int a = 0; a < 10; a++)
        {
            float time;
            if (tiempo < 100)
                time = 1;
            else
                time = 0.5f;

            yield return new WaitForSeconds(time);

            bool siNo;
            int random = Random.Range(0, 2);

            if(random == 0) siNo = true;
            else siNo = false;

            //SI DECIDE GENERAR UN ENEMIGOS
            if(siNo)
            {
                //Enemigo aleatorio
                int enemigo=Random.Range(0, 2) ;

                //leopardo
                if(enemigo == 0)
                {
                    for (int i = 0; i < pinchhosBajo.Length; i++)
                    {
                        if (!pinchhosBajo[i].GetMov())
                        {
                            pinchoBajoObj = pinchhosBajo[i];

                        }
                    }
                    if(pinchoBajoObj != null)
                    {
                        pinchoBajoObj.transform.position = tps[enemigo].position;
                        pinchoBajoObj.SetMov(true);
                        pinchoBajoObj=null;
                    }
                }

                //pajaro
                else
                {
                    for (int i = 0; i < pinchosAlto.Length; i++)
                    {
                        if (!pinchosAlto[i].GetMov())
                        {
                            pinchoAltoObj = pinchosAlto[i];

                        }
                    }
                    if (pinchoAltoObj != null)
                    {
                        pinchoAltoObj.transform.position = tps[enemigo].position;
                        pinchoAltoObj.SetMov(true);
                        pinchoAltoObj = null;
                    }
                }

            }
            a = 0;
        }


    }

    IEnumerator AumentarVelocidades()
    {
        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(2.5f);

            //leopardos
            for (int j = 0; j < pinchhosBajo.Length; j++)
            {
                pinchhosBajo[j].SetVelocidadEnemigo(0.1f);
            }

            //pajaros
            for (int k = 0; k < pinchosAlto.Length; k++)
            {
                pinchosAlto[k].SetVelocidadEnemigo(0.1f);
            }

            //montañas
            for (int l = 0; l < montañas.Length; l++)
            {
                montañas[l].SetVelocidadMonta(0.1f);
            }

            for (int m = 0; m < peces.Length; m++)
            {
                peces[m].SetVelocidadPez(0.1f);
            }


            i = 0;
        } 
    }

    public void BotonCargarModos(string level)
    {
        Time.timeScale = 1;
        LevelLoader.LoadLevel(level);
    }

    public float GetTiempo()
    { return tiempo; }

    public bool GetEnJuego()
    { return enJuego; }
    public void SetEnJuego(bool r)
    { enJuego = r; }

    public void BotonInicio()
    {
        enJuego= true;
        panelInicio.SetActive(false);
    }

}