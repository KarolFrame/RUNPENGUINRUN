using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peces : MonoBehaviour
{
    Transform tr;
    float velocidadPez;
    Animator anim;
    private void Awake()
    {
        tr = transform;
        anim = GetComponent<Animator>();
        velocidadPez = Random.Range(1.25f, 2.5f);

        anim.SetFloat("Multiplicador", velocidadPez);
    }

    void Update()
    {
        tr.Translate(-tr.forward * velocidadPez * Time.deltaTime);
    }
    void TPPez()
    {
        tr.position = new Vector3(tr.position.x, tr.position.y, 6f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Eliminador")
            TPPez();
    }
    public void SetVelocidadPez(float suma)
    {
        velocidadPez += suma;
    }
}
