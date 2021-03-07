using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReproductorSonidoPiesJugador : MonoBehaviour
{
    [SerializeField] private AudioClip[] sonidosDePasos; // Asignado en Unity
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ReproducirSonidoPaso1()
    {
        audioSource.clip = sonidosDePasos[0];
        audioSource.PlayOneShot(audioSource.clip);
    }

    public void ReproducirSonidoPaso2()
    {
        audioSource.clip = sonidosDePasos[1];
        audioSource.PlayOneShot(audioSource.clip);
    }

    public void ReproducirSonidoCorrer()
    {
        audioSource.clip = sonidosDePasos[2];
        audioSource.PlayOneShot(audioSource.clip);
    }
}
