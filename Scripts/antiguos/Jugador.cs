using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    //TRANSFORM DEL JUGADOR
    public Transform mallaJugador;
    //TRANSFORMS DE LOS PUNTOS EN LOS QUE SE PUEDE ENCONTRAR EL JUGADOR
    public Transform[] puntos;
    int puntoActual;
    float tempo = 0.3f;
    //SALTO
    Rigidbody rB;
    Animator anim;
    public float saltoMax;
    float salto;

    float tiempoInicio;
    float tiempo;


    bool semueve = false;

    Transform tr;
    float velocidad = 3.5f;

    void Awake()
    {
        puntoActual = 1;
        mallaJugador.position = puntos[puntoActual].position;
        rB = GetComponentInChildren<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        tr = transform;
    }

    void Update()
    {
        //IR HACIA ALANTE
        tr.Translate(Vector3.forward * velocidad * Time.deltaTime);

        //CAMBIAR DE POSICION EL JUGADOR ENTRE LOS TRES PUNTOS POSIBLES
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (puntoActual == 0 || puntoActual == 1)
                puntoActual++;
            tempo = 0.3f;
            semueve = true;
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (puntoActual == 2 || puntoActual == 1)
                puntoActual--;
            tempo = 0.15f;
            semueve = true;
        }
        //SALTO

        /*if (Input.GetKey(KeyCode.UpArrow))
            tiempo += Time.deltaTime;
        else if(Input.GetKeyUp(KeyCode.UpArrow))
        {
            Vector3 dwn = mallaJugador.TransformDirection(Vector3.down);
            RaycastHit hit;
            if (Physics.Raycast(mallaJugador.position, dwn, out hit, 0.3f) && hit.collider.CompareTag("Suelo"))
            {
                if (tiempo >= 1.5f)
                    salto = saltoMax;
                else
                    salto = tiempo * 5;

                rB.velocity = new Vector3(0f, salto, 0f);

                anim.SetTrigger("Salta");

            }
        }
                    else
                salto = 0;
         */

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Vector3 dwn = mallaJugador.TransformDirection(Vector3.down);
            RaycastHit hit;
            if (Physics.Raycast(mallaJugador.position, dwn, out hit, 0.3f) && hit.collider.CompareTag("Suelo"))
            {
                rB.velocity = new Vector3(0f, saltoMax, 0f);

                anim.SetTrigger("Salta");
            }
           
        }

        //EL JUGADOR SE MUEVE A LA SIGUIENTE POSICION POR UN LERPEO
        if (semueve)
        {
            mallaJugador.position = Vector3.Lerp(mallaJugador.position, puntos[puntoActual].position, Time.deltaTime * 30);

            tempo -= Time.deltaTime;
            if(tempo <= 0)
            {
                semueve = false;
            }
        }
    }

 


}
