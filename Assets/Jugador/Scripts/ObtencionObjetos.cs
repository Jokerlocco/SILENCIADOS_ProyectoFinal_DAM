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
            if (Input.GetButtonDown("Interactuar") && colisionando &&
                FindObjectOfType<ControlDelJugador>().PuedeMoverse)
                RecogerObjeto();
        }
    }

    private void RecogerObjeto()
    {
        if (gameObject.CompareTag("Acetona"))
        {
            objetoRecogido = true;
            FindObjectOfType<InventarioJugador>().
                AcetonaEnElInventario = true;
            FindObjectOfType<Mensajero>().Mensaje =
                "Acetona recogida";
        }

        if (gameObject.CompareTag("Bombilla"))
        {
            objetoRecogido = true;
            FindObjectOfType<InventarioJugador>().
                BombillaEnElInventario = true;
            FindObjectOfType<Mensajero>().Mensaje =
                "Bombilla funcional recogida";
        }

        if (gameObject.CompareTag("Extintor") && 
            !GameObject.FindGameObjectWithTag("CompartimientoDelExtintor"))
        {
            objetoRecogido = true;
            FindObjectOfType<InventarioJugador>().
                ExtintorEnElInventario = true;
            FindObjectOfType<Mensajero>().Mensaje =
                "Extintor recogido";
        }

        if (gameObject.CompareTag("Ganzua"))
        {
            objetoRecogido = true;
            FindObjectOfType<InventarioJugador>().
                GanzuaEnElInventario = true;
            FindObjectOfType<Mensajero>().Mensaje =
                "Ganzúa recogida";
        }

        if (gameObject.CompareTag("GrifoDeBronce") &&
            FindObjectOfType<EstadoDelJuego>().CajaDeSeguridadSMaquinasAbierta)
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

        if (gameObject.CompareTag("GrifoDeMarmol") && 
            FindObjectOfType<EstadoDelJuego>().CajaDeSeguridadDMJAbierta)
        {
            objetoRecogido = true;
            FindObjectOfType<InventarioJugador>().
                GrifoMarmolEnElInventario = true;
            FindObjectOfType<Mensajero>().Mensaje = 
                "Grifo de mármol recogido";
        }

        if (gameObject.CompareTag("Jarron"))
        {
            objetoRecogido = true;
            FindObjectOfType<InventarioJugador>().
                JarronEnElInventario = true;
            FindObjectOfType<Mensajero>().Mensaje =
                "Jarrón de gran acabado recogido";
        }

        if (gameObject.CompareTag("LlaveCaballo"))
        {
            objetoRecogido = true;
            FindObjectOfType<InventarioJugador>().
                LlaveCaballoEnElInventario = true;
            FindObjectOfType<Mensajero>().Mensaje = 
                "Llave caballo recogida";
        }

        if (gameObject.CompareTag("LlaveInglesa"))
        {
            objetoRecogido = true;
            FindObjectOfType<InventarioJugador>().
                LlaveInglesaEnElInventario = true;
            FindObjectOfType<Mensajero>().Mensaje =
                "Llave inglesa recogida";
        }

        if (gameObject.CompareTag("LlavePeon") &&
            FindObjectOfType<EstadoDelJuego>().
                    CajaDeSeguridadSecretariaAbierta)
        {
            objetoRecogido = true;
            FindObjectOfType<InventarioJugador>().
                LlavePeonEnElInventario = true;
            FindObjectOfType<Mensajero>().Mensaje =
                "Llave peón recogida";
        }

        if (gameObject.CompareTag("LlaveRey") &&
            FindObjectOfType<EstadoDelJuego>().
                    CajaDeSeguridadSObservacionAbierta)
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

        if (gameObject.CompareTag("LlaveAlfil"))
        {
            MeshRenderer renderSiliconaLlaveAlfil = 
                gameObject.transform.GetChild(1).gameObject.
                GetComponent<MeshRenderer>();

            // Si se ha quitado la silicona...
            if (!renderSiliconaLlaveAlfil.enabled)
            {
                objetoRecogido = true;
                FindObjectOfType<InventarioJugador>().
                    LlaveAlfilEnElInventario = true;
                FindObjectOfType<Mensajero>().Mensaje =
                    "Llave alfil recogida";
            }
        }

        if (gameObject.CompareTag("Vinagre"))
        {
            objetoRecogido = true;
            FindObjectOfType<InventarioJugador>().
                VinagreEnElInventario = true;
            FindObjectOfType<Mensajero>().Mensaje =
                "Vinagre recogido";
        }

        if (gameObject.CompareTag("Eter"))
        {
            objetoRecogido = true;
            FindObjectOfType<InventarioJugador>().
                EterEnElInventario = true;
            FindObjectOfType<Mensajero>().Mensaje =
                "Éter etílico recogido";
        }


        // Si se ha recogido un objeto...
        if (objetoRecogido)
        {
            if (audioSource != null)
                audioSource.Play();
            OcultarObjetoDelEscenario();
            FindObjectOfType<Mensajero>().MostrarInterfazMensaje();
        }
    }

    private void OcultarObjetoDelEscenario()
    {
        gameObject.tag = "Recogido";

        // Objeto
        if (gameObject.GetComponent<Renderer>())
            gameObject.GetComponent<Renderer>().enabled = false;
        if (gameObject.GetComponent<MeshCollider>())
            gameObject.GetComponent<MeshCollider>().enabled = false;
        if (gameObject.GetComponent<Light>())
            gameObject.GetComponent<Light>().enabled = false;

        /* Posibles elementos hijos del objeto (Por ejemplo, 
         * las llaves tienen dos partes: "La cabeza" y el "cifrado". 
         * Pues con esto, eliminamos el cifrado que  es la parte hija 
         * de la llave. */
        foreach (Transform hijo in gameObject.transform)
        {
            if (hijo.GetComponent<Renderer>())
                hijo.GetComponent<Renderer>().enabled = false;
            if (hijo.GetComponent<MeshCollider>())
                hijo.GetComponent<MeshCollider>().enabled = false;
            if (hijo.GetComponent<Light>())
                hijo.GetComponent<Light>().enabled = false;
        }
    }

    private void DesactivarScript()
    {
        enabled = false;
    }
}
