﻿using UnityEngine;
using UnityEngine.UI;

public class InspeccionDeMapas : MonoBehaviour
{
    private bool colisionando = false;

    [SerializeField] RawImage imagenMapaPlanta1; // Asignado en Unity
    [SerializeField] RawImage imagenMapaPlanta2; // Asignado en Unity
    private bool mapaActivo = false;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador"))
            colisionando = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador"))
            colisionando = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interactuar") && colisionando && !mapaActivo)
        {
            MostrarMapa();
            MostrarImagenDelMapa();
        }

        if (Input.GetButtonDown("Cerrar") && mapaActivo)
            QuitarMapa();
    }

    private void MostrarMapa()
    {
        mapaActivo = true;
        FindObjectOfType<ControlDelJugador>().PuedeMoverse = false;
        audioSource.Play();
    }

    private void QuitarMapa()
    {
        mapaActivo = false;
        FindObjectOfType<ControlDelJugador>().PuedeMoverse = true;
        imagenMapaPlanta1.enabled = false;
        imagenMapaPlanta2.enabled = false;
    }

    private void MostrarImagenDelMapa()
    {
        if (gameObject.CompareTag("MapaPlanta1"))
            imagenMapaPlanta1.enabled = true;
        else if (gameObject.CompareTag("MapaPlanta2"))
            imagenMapaPlanta2.enabled = true;
    }
}