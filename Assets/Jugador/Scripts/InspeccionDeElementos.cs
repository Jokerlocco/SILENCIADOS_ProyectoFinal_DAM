using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InspeccionDeElementos : MonoBehaviour
{
    private bool colisionando = false;

    // Mensajes
    [SerializeField] TMP_Text mensajeDeInteraccion; // Asignado en Unity
    [SerializeField] GameObject fondoOscuroTraslucidoMensajes; // Asignado en Unity
    private bool mensajeActivo = false;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void ActivarInterfazMensaje()
    {
        fondoOscuroTraslucidoMensajes.SetActive(true);
        mensajeActivo = true;
    }

    private void QuitarInterfazMensaje()
    {
        mensajeActivo = false;
        mensajeDeInteraccion.text = "";
        fondoOscuroTraslucidoMensajes.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador"))
        {
            colisionando = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador"))
        {
            colisionando = false;
            QuitarInterfazMensaje();
        }
    }

    private void ReproducirSonidoElemento()
    {
        if (audioSource != null)
            audioSource.Play();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interactuar") &&
            colisionando && !mensajeActivo)
        {
            ActivarInterfazMensaje();
            DarMensajeDelElemento();
        }
    }

    private void DarMensajeDelElemento()
    {
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
}
