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

    private void OnTriggerEnter(Collider other)
    {
        colisionando = true;
    }

    private void OnTriggerExit(Collider other)
    {
        colisionando = false;

        fondoOscuroTraslucidoMensajes.SetActive(false);
        mensajeActivo = false;
        mensajeDeInteraccion.text = "";
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interactuar") &&
            colisionando && !mensajeActivo)
        {
            fondoOscuroTraslucidoMensajes.SetActive(true);
            mensajeActivo = true;

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

    private void ReproducirSonidoElemento()
    {
        if (audioSource != null)
            audioSource.Play();
    }
}
