using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObtencionObjetos : MonoBehaviour
{
    private bool colisionando = false;
    private bool objetoRecogido = false;

    [SerializeField] TMP_Text mensajeObtencionDeObjeto; // Asignado en Unity
    [SerializeField] GameObject fondoOscuroTraslucidoMensajes; // Asignado en Unity

    private string textoDelMensaje = "";

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
            FindObjectOfType<InventarioJugador>().
                BombillaEnElInventario = true;
            textoDelMensaje = "Bombilla funcional recogida";
        }

        if (gameObject.CompareTag("GrifoDeBronce"))
        {
            FindObjectOfType<InventarioJugador>().
                GrifoBronceEnElInventario = true;
            textoDelMensaje = "Grifo de bronce recogido";
        }

        if (gameObject.CompareTag("GrifoDeMadera"))
        {
            FindObjectOfType<InventarioJugador>().
                GrifoMaderaEnElInventario = true;
            textoDelMensaje = "Grifo de madera recogido";
        }

        if (gameObject.CompareTag("GrifoDeMarmol"))
        {
            FindObjectOfType<InventarioJugador>().
                GrifoMarmolEnElInventario = true;
            textoDelMensaje = "Grifo de mármol recogido";
        }

        if (gameObject.CompareTag("LlavePeon"))
        {
            FindObjectOfType<InventarioJugador>().
                LlavePeonEnElInventario = true;
            textoDelMensaje = "Llave peón recogida";
        }

        if (gameObject.CompareTag("LlaveTorre"))
        {
            FindObjectOfType<InventarioJugador>().
                LlaveTorreEnElInventario = true;
            textoDelMensaje = "Llave torre recogida";
        }

        objetoRecogido = true;
        audioSource.Play();
        OcultarObjetoDelEscenario();
        StartCoroutine(InformarSobreObjetoRecogido());
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

    private void ActivarInterfazMensaje()
    {
        fondoOscuroTraslucidoMensajes.SetActive(true);
    }

    private void QuitarInterfazMensaje()
    {
        mensajeObtencionDeObjeto.text = "";
        fondoOscuroTraslucidoMensajes.SetActive(false);
    }

    private IEnumerator InformarSobreObjetoRecogido()
    {
        ActivarInterfazMensaje();
        mensajeObtencionDeObjeto.text = textoDelMensaje;
        yield return new WaitForSecondsRealtime(3);
        QuitarInterfazMensaje();
    }

    private void DesactivarScript()
    {
        enabled = false;
    }
}
