using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerNado : MonoBehaviour
{
    Transform tr;
    Rigidbody rb;
    public float fuerzaDeNado;
    float lerp =0;
    float angulo;

    GameControllerNado gm;
    float record = 0;

    webS w;

    private void Awake()
    {
        gm = FindObjectOfType<GameControllerNado>();
        w = FindObjectOfType<webS>();


        tr= transform;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gm.GetEnJuego())
        {
           if(rb.velocity.y<0)
            {
                //LERPEAR ANGULO
                if (angulo <= 35)
                { lerp += Time.deltaTime; }
            
                angulo = lerp * 100;

                tr.eulerAngles= new Vector3(angulo,0,180);
            }
            if (rb.velocity.y > 0)
            {
                tr.eulerAngles = new Vector3(-35, 0, 180);
                lerp = 0;
                angulo = -35;
            }
            if (rb.velocity.y == 0)
            {
                tr.eulerAngles = new Vector3(0, 0, 0);
            }
        }
    }


    public void Nadar()
    {
        if (gm.GetEnJuego())
        {
            rb.velocity = new Vector3(0, fuerzaDeNado, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gm.GetEnJuego())
        {
            if (other.gameObject.tag == "Enemigo" || other.gameObject.tag == "MuerteAgua")
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