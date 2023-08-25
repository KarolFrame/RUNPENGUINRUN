using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameController : MonoBehaviour
{
    Leopardo[] leopardos;
    Leopardo leopardoEnemigo;
    Pajaro[] pajaros;
    Pajaro pajaroEnemigo;
    MontañaMovement[] montañas;
    Player player;

    float multiAnim = 1;
    Animator playerAnim;
    public Animator[] sueloAnim;

    public Transform[] tps;

    public TMPro.TextMeshProUGUI textoTiempo;
    float tiempo;

    bool enJuego = true;

    public GameObject panelInicio;

    private void Awake()
    {
        leopardos = FindObjectsOfType<Leopardo>();
        pajaros= FindObjectsOfType<Pajaro>();
        montañas = FindObjectsOfType<MontañaMovement>();
        player = FindObjectOfType<Player>();
        playerAnim = player.GetComponent<Animator>();
    }
    void Start()
    {
        StartCoroutine(GenerarEnmigos());
        StartCoroutine(AumentarVelocidades());

        Time.timeScale = 0;
        enJuego = false;
    }

    void Update()
    {
        if (!enJuego)
        {
            Time.timeScale = 0;
        }
        else
            Time.timeScale = 1;


        if (enJuego)
        {
            tiempo += Time.deltaTime;
            textoTiempo.text = "Tiempo: " + Mathf.Round(tiempo).ToString() + " s";
        }

    }

    IEnumerator GenerarEnmigos()
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
                    for (int i = 0; i < leopardos.Length; i++)
                    {
                        if (!leopardos[i].GetMov())
                        {
                            leopardoEnemigo = leopardos[i];

                        }
                    }
                    if(leopardoEnemigo != null)
                    {
                        leopardoEnemigo.transform.position = tps[enemigo].position;
                        leopardoEnemigo.SetMov(true);
                        leopardoEnemigo=null;
                    }
                }

                //pajaro
                else
                {
                    for (int i = 0; i < pajaros.Length; i++)
                    {
                        if (!pajaros[i].GetMov())
                        {
                            pajaroEnemigo = pajaros[i];

                        }
                    }
                    if (pajaroEnemigo != null)
                    {
                        pajaroEnemigo.transform.position = tps[enemigo].position;
                        pajaroEnemigo.SetMov(true);
                        pajaroEnemigo = null;
                    }
                }

            }
            a = 0;
            if (!enJuego)
                a = 20;

        }


    }

    IEnumerator AumentarVelocidades()
    {
        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(5);

            //leopardos
            for (int j = 0; j < leopardos.Length; j++)
            {
                leopardos[j].SetVelocidadEnemigo(0.1f);
            }

            //pajaros
            for (int k = 0; k < pajaros.Length; k++)
            {
                pajaros[k].SetVelocidadEnemigo(0.1f);
            }

            //montañas
            for (int l = 0; l < montañas.Length; l++)
            {
                montañas[l].SetVelocidadMonta(0.1f);
            }


            multiAnim += 0.1f;

            for (int m = 0; m < sueloAnim.Length; m++)
            {
                sueloAnim[m].SetFloat("Multiplicador", multiAnim);
            }
            i = 0;
            if (!enJuego)
                i = 3;
        } 
    }

    public void BotonCargarModos(string level)
    {
        Time.timeScale = 1;
        LevelLoader.LoadLevel(level);
    }

    public void BotonReinicio()
    {
        print("aw");
    }
    public float GetTiempo()
    { return tiempo; }

    public bool GetEnJuego()
    { return enJuego; }
    public void SetEnJuego(bool r)
    { enJuego = r; }

    public void BotonInicio()
    {
        enJuego = true;
        panelInicio.SetActive(false);
    }

}