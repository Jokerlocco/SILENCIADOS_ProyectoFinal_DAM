using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InspeccionDeElementos : MonoBehaviour
{
    private bool colisionando = false;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
            FindObjectOfType<Mensajero>().OcultarInterfazMensaje();
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
            colisionando && 
            !FindObjectOfType<Mensajero>().InterfazMensajeActiva)
        {
            EstablcerMensajeOTerminar();
        }
    }

    private void EstablcerMensajeOTerminar()
    {
        bool mostrarMensaje = true;

        if (gameObject.CompareTag("InspeccionPuertaPrincipal"))
        {
            FindObjectOfType<Mensajero>().Mensaje = 
                "La puerta principal está bloqueada. " +
                "Debo buscar otra forma de salir.";
            ReproducirSonidoElemento();
        }

        else if (gameObject.CompareTag("InspeccionAscensor"))
            FindObjectOfType<Mensajero>().Mensaje =
                "El ascensor está destrozado. No se puede utilizar.";

        else if (gameObject.CompareTag("InspeccionEstatua"))
            FindObjectOfType<Mensajero>().Mensaje =
                "La estatua tiene tres orificios vacíos. " +
                "Es cómo si se tuviera que insertar algo.";

        else if (gameObject.CompareTag("Proyector") && 
            !FindObjectOfType<InventarioJugador>().BombillaEnElInventario)
            FindObjectOfType<Mensajero>().Mensaje = 
                "El proyector parece funcionar, pero le falta la bombilla.";

        else if (gameObject.CompareTag("FuegoEnLaCocina") || 
            gameObject.CompareTag("HumoEnSMaquinas"))
            FindObjectOfType<Mensajero>().Mensaje = "Hay un escape... " +
                "Será mejor no acercarse más.";

        else if (gameObject.CompareTag("CompartimientoDelExtintor") &&
            !FindObjectOfType<InventarioJugador>().GanzuaEnElInventario)
            FindObjectOfType<Mensajero>().Mensaje = 
                "Es un extintor, pero el compartimiento está cerrado. " +
                "Si tuviera la herramienta adecuada creo que podría abrirlo.";

        else // Si no ha entrado a ningún if, terminamos con el script.
        {
            mostrarMensaje = false;
            DesactivarScript();
        }

        if (mostrarMensaje)
            MostrarMensaje();
    }

    private void MostrarMensaje()
    {
        FindObjectOfType<Mensajero>().MostrarInterfazMensaje();
    }

    private void DesactivarScript()
    {
        enabled = false;
    }
}
