using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class Player : MonoBehaviour
{ 
    //COMPONENTES
    Rigidbody rb;
    Animator anim;
    Transform tr;
    public Transform checkSuelo;

    public float fuerzaSalto;
    bool puedeSaltar, puedeDeslizar = true, estaSaltando = false;

    int saltos = 1;
    float tiempoagachado;

    //PARTICULAS
    public ParticleSystem polvoPies;
    ParticleSystem.EmissionModule emisionPolvoPies;

    SoundManager sm;

    GameController gm;
    float record = 0;

    web w;

    private void Awake()
    {
        sm = FindObjectOfType<SoundManager>();
        gm = FindObjectOfType<GameController>();
        w = FindObjectOfType<web>();

        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        tr = transform;
        record = 0;

        emisionPolvoPies = polvoPies.emission;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dwn = checkSuelo.TransformDirection(Vector3.down);
        RaycastHit hit;


        if (Physics.Raycast(checkSuelo.position, dwn, out hit, 0.2f) && hit.collider.CompareTag("Suelo") && saltos == 1)
            puedeSaltar = true;
        else
            puedeSaltar = false;

    }
    public void Salto()
    {
        if(gm.GetEnJuego())
        {
            if (puedeSaltar) 
            {
                //Si estaba deslizandose corrigimos cositas
                anim.SetBool("Desplazarse", false);
                Collider collider = GetComponent<Collider>();
                //collider.enabled = true;
                rb.isKinematic = false;

                sm.SeleccionAudio(1);

                estaSaltando = true;
                puedeDeslizar= false;
                saltos--;

                anim.SetBool("Salta", true);
                emisionPolvoPies.enabled = false;

                rb.velocity = new Vector3(0, fuerzaSalto, 0); 
            }

            //rb.velocity = new Vector3(0, 0, 0);
        }
        //SALTAR

    }

    public void Desplazarse()
    {
        if (gm.GetEnJuego())
        {   
            
            tiempoagachado = 2;
            //DESLIZARSE CON LA PANZA
            if (puedeDeslizar && !estaSaltando)
            {
                puedeSaltar = true;
                anim.SetBool("Desplazarse", true);

                Collider collider = GetComponent<Collider>();
                Rigidbody rb = GetComponent<Rigidbody>();
                //collider.enabled = false;
                rb.isKinematic = true;

                StartCoroutine(ActivarCollider(collider, rb));


            }

            //CAER EN PICADO
            if(estaSaltando)
            {
                rb.velocity = new Vector3(0, -fuerzaSalto*1.5f, 0);
            }
        }

    }

    IEnumerator ActivarCollider(Collider col, Rigidbody rb)
    {
        yield return new WaitForSeconds(tiempoagachado);
        //col.enabled = true;
        rb.isKinematic = false;
        anim.SetBool("Desplazarse", false);
        puedeSaltar = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gm.GetEnJuego())
        {
            if (collision.gameObject.tag =="Suelo" && estaSaltando)
            {
                anim.SetBool("Salta", false);
                estaSaltando = false;
                saltos = 1;
                puedeDeslizar = true;
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (gm.GetEnJuego())
        {
            if (other.gameObject.tag == "Pajaro" && !anim.GetBool("Desplazarse"))
            {
                record = gm.GetTiempo();
                StopAllCoroutines();
                gm.StopAllCoroutines();

                //Time.timeScale = 0;

                w.ProcesoInicialLectura();

                gm.SetEnJuego(false);
            }
            if (other.gameObject.tag == "Leopardo")
            {
                record = gm.GetTiempo();
                StopAllCoroutines();
                gm.StopAllCoroutines();

                //Time.timeScale = 0;

                w.ProcesoInicialLectura();

                gm.SetEnJuego(false);
            }
        }

    }

    public float GetRecord()
    { return record; }

}
