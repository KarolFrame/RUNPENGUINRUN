using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generador : MonoBehaviour
{
    Transform tr;

    float timer = 2.515f;

    public GameObject[] sueloPrefabs;
    int sueloRandom;
    GameObject nuevoSuelo;

    private void Awake()
    {
        tr = transform;
    }
    private void Start()
    {
        //StartCoroutine(GenerarSuelo());
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void ElegirSuelo()
    {
        sueloRandom = Random.Range(0, sueloPrefabs.Length);
        nuevoSuelo = Instantiate(sueloPrefabs[sueloRandom].gameObject);
        nuevoSuelo.transform.position = tr.position;
    }

    IEnumerator GenerarSuelo()
    {
        for (int i = 0; i < 100; i++)
        {
            //Esperar los segundos necesarios, cada ronda son menos segundos
            yield return new WaitForSeconds(timer);
            //Generar el proyectil en su spawn
            ElegirSuelo();
            //Establecer velocidad a los proyectiles creados, cada vez son más rápidos

            //Bajamos el valor del timer y subimos la velocida de los proyectiles de la siguiente ronda
        }
    }
    private void OnTriggerExit(Collider other)
    {
        print(other.name);
        if(other.name == "collder1 (1)" || other.name == "default")
        {
            ElegirSuelo();
        }
    }
}
