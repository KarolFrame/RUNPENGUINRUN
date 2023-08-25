using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leopardo : MonoBehaviour
{
    bool enMov;

    //COMPONENTES   
    Transform tr;

    float velocidadPlatform = 5;


    private void Awake()
    {
        tr = transform;
        enMov = true;
    }

    void Update()
    {
        if (enMov)
        {
            tr.Translate(-tr.forward * velocidadPlatform * Time.deltaTime);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Eliminador")
        {
            enMov = false;
        }
    }

    public void SetVelocidadEnemigo(float suma)
    {
        velocidadPlatform = velocidadPlatform + suma;
    }
    public void SetMov(bool x)
    {
        enMov = x;
    }
    public bool GetMov()
    { return enMov; }
}
