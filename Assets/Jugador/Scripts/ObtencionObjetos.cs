using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObtencionObjetos : MonoBehaviour
{
    private bool colisionando = false;
    private bool objetoRecogido = false;

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
        if (objetoRecogido) // Si el objeto ha sido recogido, dejamos de comprobar la obtención de dicho objeto
            DesactivarScript();
        else
        {
            if (Input.GetButtonDown("Interactuar") && colisionando)
                RecogerObjeto();
        }
    }

    private void RecogerObjeto()
    {
        if (gameObject.CompareTag("Bombilla"))
        {
            objetoRecogido = true;
            FindObjectOfType<InventarioJugador>().
                BombillaEnElInventario = true;
            FindObjectOfType<Mensajero>().Mensaje =
                "Bombilla funcional recogida";
        }

        if (gameObject.CompareTag("GrifoDeBronce"))
        {
            objetoRecogido = true;
            FindObjectOfType<InventarioJugador>().
                GrifoBronceEnElInventario = true;
            FindObjectOfType<Mensajero>().Mensaje = 
                "Grifo de bronce recogido";
        }

        if (gameObject.CompareTag("GrifoDeMadera"))
        {
            objetoRecogido = true;
            FindObjectOfType<InventarioJugador>().
                GrifoMaderaEnElInventario = true;
            FindObjectOfType<Mensajero>().Mensaje = 
                "Grifo de madera recogido";
        }

        if (gameObject.CompareTag("GrifoDeMarmol"))
        {
            objetoRecogido = true;
            FindObjectOfType<InventarioJugador>().
                GrifoMarmolEnElInventario = true;
            FindObjectOfType<Mensajero>().Mensaje = 
                "Grifo de mármol recogido";
        }

        if (gameObject.CompareTag("LlaveAlfil"))
        {
            objetoRecogido = true;
            FindObjectOfType<InventarioJugador>().
                LlaveAlfilEnElInventario = true;
            FindObjectOfType<Mensajero>().Mensaje = 
                "Llave alfil recogida";
        }

        if (gameObject.CompareTag("LlaveCaballo"))
        {
            objetoRecogido = true;
            FindObjectOfType<InventarioJugador>().
                LlaveCaballoEnElInventario = true;
            FindObjectOfType<Mensajero>().Mensaje = 
                "Llave caballo recogida";
        }

        if (gameObject.CompareTag("LlavePeon") && 
            FindObjectOfType<CajaDeSeguridad>().CajaAbierta)
        {
            objetoRecogido = true;
            FindObjectOfType<InventarioJugador>().
                LlavePeonEnElInventario = true;
            FindObjectOfType<Mensajero>().Mensaje =
                "Llave peón recogida";
        }

        if (gameObject.CompareTag("LlaveRey"))
        {
            objetoRecogido = true;
            FindObjectOfType<InventarioJugador>().
                LlaveReyEnElInventario = true;
            FindObjectOfType<Mensajero>().Mensaje =
                "Llave rey recogida";
        }

        if (gameObject.CompareTag("LlaveTorre"))
        {
            objetoRecogido = true;
            FindObjectOfType<InventarioJugador>().
                LlaveTorreEnElInventario = true;
            FindObjectOfType<Mensajero>().Mensaje =
                "Llave torre recogida";
        }

        // Si se ha recogido un objeto...
        if (objetoRecogido)
        {
            audioSource.Play();
            OcultarObjetoDelEscenario();
            FindObjectOfType<Mensajero>().MostrarInterfazMensaje();
        }
    }

    private void OcultarObjetoDelEscenario()
    {
        if (gameObject.CompareTag("LlavePeon") || 
            gameObject.CompareTag("LlaveTorre") ||
            gameObject.CompareTag("LlaveCaballo") ||
            gameObject.CompareTag("LlaveAlfil") ||
            gameObject.CompareTag("LlaveRey"))
        {
            /* Las llaves tienen dos partes: "La cabeza" y el "cifrado". 
             * Pues con esto, eliminamos el cifrado que 
             * es la parte hija de la llave. */
            GameObject cifradoLlave = 
                gameObject.transform.GetChild(0).gameObject;
            Renderer renderer = cifradoLlave.GetComponent<Renderer>();
            renderer.enabled = false;
        }

        Renderer rendererDelObjeto = gameObject.GetComponent<Renderer>();
        rendererDelObjeto.enabled = false;
        MeshCollider meshColliderDelObjeto = 
            gameObject.GetComponent<MeshCollider>();
        meshColliderDelObjeto.enabled = false;
        
        gameObject.tag = "Recogido";
    }

    private void DesactivarScript()
    {
        enabled = false;
    }
}
