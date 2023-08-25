using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlatformMovement : MonoBehaviour
{
    public enum TipoEnemigo {leonM, pajaro}
    public TipoEnemigo tipo;

    bool enMov;

    //COMPONENTES   
    Transform tr;

    float velocidadPlatform = 5;

    public Transform[] tps;

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

    /*IEnumerator EsperarATP()
    {
        float random = Random.Range(0,5f);

        yield return new WaitForSeconds(random);

        if(tipo == TipoEnemigo.leonM)
            tr.position = tps[0].position;
        else
            tr.position = tps[1].position;

        enMov= true;

    }*/

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Eliminador")
        {
            enMov= false;
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
}
