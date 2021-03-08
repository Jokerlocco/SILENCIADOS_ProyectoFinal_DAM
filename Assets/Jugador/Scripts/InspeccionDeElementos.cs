using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InspeccionDeElementos : MonoBehaviour
{
    private AudioSource audioSource;

    // Mensajes
    [SerializeField] TMP_Text mensajeDeInteraccion; // Asignado en Unity
    [SerializeField] GameObject fondoOscuroTraslucidoMensajes; // Asignado en Unity
    private bool mensajeActivo = false;

    private bool seHaPulsadoInteractuar = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetButtonDown("Interactuar"))
            seHaPulsadoInteractuar = true;

        if (seHaPulsadoInteractuar &&
            other.gameObject.CompareTag("Jugador") && !mensajeActivo)
        {
            fondoOscuroTraslucidoMensajes.SetActive(true);
            mensajeActivo = true;

            if (gameObject.CompareTag("InspeccionPuertaPrincipal"))
            {
                mensajeDeInteraccion.text =
                    "La puerta principal está bloqueada. " +
                    "Debo buscar otra forma de salir.";
                ReproducirSonidoElemento();
            }

            else if (gameObject.CompareTag("InspeccionAscensor"))
                mensajeDeInteraccion.text = 
                    "El ascensor está destrozado. No se puede utilizar.";

            else if (gameObject.CompareTag("InspeccionEstatua"))
                mensajeDeInteraccion.text =
                    "La estatua tiene tres orificios vacíos. " +
                    "Es cómo si se tuviera que insertar algo.";
        }

        seHaPulsadoInteractuar = false;
    }

    void OnTriggerExit(Collider other)
    {
        mensajeActivo = false;
        fondoOscuroTraslucidoMensajes.SetActive(false);
        mensajeDeInteraccion.text = "";
    }

    private void ReproducirSonidoElemento()
    {
        if (audioSource != null)
            audioSource.Play();
    }
}
