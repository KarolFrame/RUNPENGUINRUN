using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MontañaMovement : MonoBehaviour
{

    public enum TipoMontaña {grande, pequeña}
    public TipoMontaña tipoMontaña;
    public Transform[] trMontañas;
    public float velocidadMontaña = 2;
    GameObject nuevaMontaña;
    Transform tr;

    private void Awake()
    {
        tr = transform;
    }

    // Update is called once per frame
    void Update()
    {
        tr.Translate(-tr.forward*velocidadMontaña*Time.deltaTime);
    }

    void TPMontaña()
    {
        int tamañoMontaña;
        if (tipoMontaña == TipoMontaña.grande)
            tamañoMontaña = 1;
        else
            tamañoMontaña = 0;

        tr.transform.position = trMontañas[tamañoMontaña].position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Eliminador")
            TPMontaña();

    }

    public void SetVelocidadMonta(float suma)
    {
        velocidadMontaña+= suma;
    }
}
