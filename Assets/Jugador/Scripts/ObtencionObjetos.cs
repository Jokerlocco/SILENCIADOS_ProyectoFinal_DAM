using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObtencionObjetos : MonoBehaviour
{
    private bool colisionando = false;

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
        {
            colisionando = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador"))
        {
            colisionando = false;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interactuar") && colisionando)
        {
            RecogerObjeto();
        }
    }

    private void RecogerObjeto()
    {
        if (gameObject.CompareTag("GrifoDeBronce"))
        {
            FindObjectOfType<InventarioJugador>().
                GrifoBronceEnElInventario = true;
            textoDelMensaje = "Grifo de bronce recogido";
        }
        else if (gameObject.CompareTag("GrifoDeMarmol"))
        {
            FindObjectOfType<InventarioJugador>().
                GrifoMarmolEnElInventario = true;
            textoDelMensaje = "Grifo de mármol recogido";
        }
        else if (gameObject.CompareTag("GrifoDeMadera"))
        {
            FindObjectOfType<InventarioJugador>().
                GrifoMaderaEnElInventario = true;
            textoDelMensaje = "Grifo de madera recogido";
        }
        else if (gameObject.CompareTag("LlaveTorre"))
        {
            FindObjectOfType<InventarioJugador>().
                LlaveTorreEnElInventario = true;
            textoDelMensaje = "Llave torre recogida";
        }
        else if (gameObject.CompareTag("Bombilla"))
        {
            FindObjectOfType<InventarioJugador>().
                BombillaEnElInventario = true;
            textoDelMensaje = "Bombilla funcional recogida";
        }

        audioSource.Play();
        OcultarObjetoDelEscenario();
        StartCoroutine(InformarSobreObjetoRecogido());
    }

    private void OcultarObjetoDelEscenario()
    {
        Renderer renderDelObjeto = gameObject.GetComponent<Renderer>();
        renderDelObjeto.enabled = false;
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
}
