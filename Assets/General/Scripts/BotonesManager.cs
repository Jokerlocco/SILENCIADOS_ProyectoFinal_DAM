﻿using UnityEngine;

public class BotonesManager : MonoBehaviour
{
    [SerializeField] Boton[] botones; // Asignado en Unity
    private int posicion;
    private string tipoDeMovimiento;
    private int posicionPrimerBoton;
    private int posicionUltimoBoton;

    private AudioSource audioSource;
    [SerializeField] AudioClip sonidoSeleccion; // Asignado en Unity

    private void Start()
    {
        posicionPrimerBoton = 0;
        posicionUltimoBoton = botones.Length - 1;

        audioSource = GetComponent<AudioSource>();

        // Primera selección (primer botón)
        posicion = posicionPrimerBoton;
        botones[posicion].Seleccionado = true;
    }

    private void Update()
    {
        if(Input.GetButtonDown("FlechaDerecha"))
        {
            tipoDeMovimiento = "derecha";
            Desplazarse(tipoDeMovimiento);
        }
        else if (Input.GetButtonDown("FlechaIzquierda"))
        {
            tipoDeMovimiento = "izquierda";
            Desplazarse(tipoDeMovimiento);
        }
        else if (Input.GetButtonDown("FlechaArriba"))
        {
            tipoDeMovimiento = "arriba";
            Desplazarse(tipoDeMovimiento);
        }
        else if (Input.GetButtonDown("FlechaAbajo"))
        {
            tipoDeMovimiento = "abajo";
            Desplazarse(tipoDeMovimiento);
        }
    }

    private void Desplazarse(string tipoDeMovimiento)
    {
        botones[posicion].Seleccionado = false; // Deseleccionamos el que estaba seleccionado

        if (tipoDeMovimiento == "derecha" || tipoDeMovimiento == "abajo")
            posicion++;
        else if (tipoDeMovimiento == "izquierda" || tipoDeMovimiento == "arriba")
            posicion--;

        // Limites
        if (posicion < posicionPrimerBoton)
            posicion = posicionUltimoBoton;
        else if (posicion > posicionUltimoBoton)
            posicion = posicionPrimerBoton;

        botones[posicion].Seleccionado = true; // Seleccionamos el que está seleccionado

        ReproducirSonido();
    }

    private void ReproducirSonido()
    {
        audioSource.clip = sonidoSeleccion;
        audioSource.PlayOneShot(audioSource.clip);
    }
}
