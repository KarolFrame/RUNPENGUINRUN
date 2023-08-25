using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource controlAudio;
    [SerializeField ]AudioClip[] audios;

    private void Awake()
    {
        controlAudio= GetComponent<AudioSource>();
    }

    public void SeleccionAudio(int i)
    {
        controlAudio.PlayOneShot(audios[i], 0.25f);
    }

}
