using System.Collections;
using UnityEngine;

public class ActivadorDeEventos : MonoBehaviour
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
        }
    }

    private void ReproducirSonidoElemento()
    {
        if (audioSource != null)
            audioSource.Play();
    }

    private void DesactivarScript()
    {
        enabled = false;
    }

    private void EstablecerAnimacionPantallaNegra(float segundosDeLaAnimacion)
    {
        FindObjectOfType<PantallaNegra>().ActivarAnimacionPantallaNegra();
        FindObjectOfType<PantallaNegra>()
            .QuitarPantallaNegra(segundosDeLaAnimacion);
    }

    private void Update()
    {
        if (colisionando)
        {
            EstablecerEventoOTerminar();
        }
    }

    private void EstablecerEventoOTerminar()
    {
        bool eventoActivado = false;

        if (gameObject.CompareTag("ActivadorSustoHReclusionBB"))
        {
            StartCoroutine(EstablecerAsustoHReclusionB(0.8f));

            eventoActivado = true;
        }


        if (eventoActivado)
            DesactivarScript();
    }

    private IEnumerator EstablecerAsustoHReclusionB(float segundosDeLaAnimacion)
    {
        FindObjectOfType<Mensajero>().Mensaje =
            "???";
        FindObjectOfType<Mensajero>().MostrarInterfazMensaje();

        ReproducirSonidoElemento();
        FindObjectOfType<ControlDelJugador>().PuedeMoverse = false;

        yield return new WaitForSecondsRealtime(2.8f); // Esperamos los segundos concretos para cuadrar el golpetazo con la aparición de la pantalla negra
        GameObject grietaEnLaPared = 
            GameObject.FindGameObjectWithTag("GrietaHReclusionB").gameObject;
        grietaEnLaPared.GetComponent<MeshRenderer>().enabled = true;

        yield return new WaitForSecondsRealtime(1f);
        FindObjectOfType<Mensajero>().Mensaje = 
            "¡¿Qué demonios?! ¡Debo encontrar a Sharon cuanto antes! " +
            "Espero que no haya sido ella...";
        FindObjectOfType<Mensajero>().MostrarInterfazMensaje();

        yield return new WaitForSecondsRealtime(2.5f);
        FindObjectOfType<ControlDelJugador>().PuedeMoverse = true;
    }
}
