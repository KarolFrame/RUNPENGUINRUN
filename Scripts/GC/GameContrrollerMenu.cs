using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameContrrollerMenu : MonoBehaviour
{
    public Animator[] sueloAnim;
    public GameObject fundidoNegroObject, panelAjustes, panelModos;
    Animator fundidoNegroAnim;

    public GameObject panelInicio;

    public TextMeshProUGUI vidas;

    public Vidas vida;
    public Hora hora;

    int hActual, minActual;

    private void Awake()
    {
        for (int i = 0; i < sueloAnim.Length; i++)
        {
            sueloAnim[i].SetFloat("Multiplicador", 0.3f);
        }

        fundidoNegroAnim = fundidoNegroObject.GetComponent<Animator>();
    }
    void Start()
    {

        fundidoNegroObject.SetActive(false);
    }
    private void Update()
    {
        if(vida.vidasRestantes == 0)
        {
            hActual = System.DateTime.Now.Hour;
            minActual= System.DateTime.Now.Minute;
            if(20 - (((hActual * 60) + minActual)-((hora.hora*60)+hora.min)) < 0)
            {
                vida.vidasRestantes++;
                RefrescarVidas();
            }
            else
            {
                vidas.text = "Quedan "+ (20 - (((hActual * 60) + minActual) - ((hora.hora * 60) + hora.min))) + " minutos";
            }
        }

    }

    public void EnlacesDeBoton(string enlace)
    {
        Application.OpenURL(enlace);
    }

    //BOTON DE INICIO + CONFIGURACION DEL FUNDIDO A NEGRO
    public void BotonInicio()
    {
        fundidoNegroObject.SetActive(true);
        fundidoNegroAnim.SetTrigger("PasaANegro");
        StartCoroutine(VuelveATransparente());
    }
    IEnumerator VuelveATransparente()
    {
        yield return new WaitForSeconds(2);
        fundidoNegroAnim.SetTrigger("PasaATransparente");
        StartCoroutine(DesactivarFundidoObject());
    }
    IEnumerator DesactivarFundidoObject()
    {
        panelInicio.SetActive(false);
        panelModos.SetActive(true);

        vidas.text = "VIDAS RESTANTES: "+ vida.vidasRestantes.ToString();

        yield return new WaitForSeconds(1);
        fundidoNegroObject.SetActive(false);
    }


    //BOTON PARA VOLVER AL MENU DE INICIO
    public void BotonSalirAInicio()
    {
        fundidoNegroObject.SetActive(true);
        fundidoNegroAnim.SetTrigger("PasaANegro");
        StartCoroutine(VuelveATransparenteAInicio());
    }
    IEnumerator VuelveATransparenteAInicio()
    {
        yield return new WaitForSeconds(2);
        fundidoNegroAnim.SetTrigger("PasaATransparente");
        StartCoroutine(DesactivarFundidoObjectAInicio());
    }
    IEnumerator DesactivarFundidoObjectAInicio()
    {
        panelInicio.SetActive(true);
        panelModos.SetActive(false);
        yield return new WaitForSeconds(1);
        fundidoNegroObject.SetActive(false);
    }

    public void RefrescarVidas()
    {
        vidas.text = "VIDAS RESTANTES: " + vida.vidasRestantes.ToString();
    }

    //BOTON PARA ABRIR LOS AJUSTES
    public void BotonAjustes()
    {
        panelAjustes.SetActive(true);
    }
    public void BotonSalirAjustes()
    { panelAjustes.SetActive(false);}

    //BOTONES MODO DE JUEGO
    public void BotonCargarModos(string level)
    {
        if(vida.vidasRestantes > 0)
        {
            Time.timeScale = 1;
            LevelLoader.LoadLevel(level);

            if (vida.vidasRestantes == 1)
            {
                hora.hora = System.DateTime.Now.Hour;
                hora.min= System.DateTime.Now.Minute;
            }
            vida.vidasRestantes--;

        }
    }

    public void BotonSalir()
    {
        Application.Quit();
    }
}
