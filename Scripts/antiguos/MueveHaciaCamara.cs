using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MueveHaciaCamara : MonoBehaviour
{
    Transform tr;
    float velocidad;
    Jugador jugador;
    void Start()
    {
        tr = transform;
        jugador = FindObjectOfType<Jugador>();
        velocidad = 3.5f;
    }

    // Update is called once per frame
    void Update()
    {

        if (tr.transform.position.z < jugador.transform.position.z - 5)
            Destroy(gameObject);
    }

    public void SetVelocidad(float fl)
    {
        velocidad = fl;
    }

}
