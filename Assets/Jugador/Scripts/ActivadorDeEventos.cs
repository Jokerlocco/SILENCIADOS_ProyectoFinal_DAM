using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActivadorDeEventos : MonoBehaviour
{
    private bool colisionando = false;
    private AudioSource audioSource;
    private bool eventoEnProceso = false;

    [SerializeField] AudioClip musicaACambiar = null; // Asignado en unity

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
        // Primer escenario
        if (!eventoEnProceso)
        {
            if (gameObject.CompareTag("ActivadorAlguienCaminando"))
                StartCoroutine(EstablecerAlguienCaminando(3f));

            if (gameObject.CompareTag("ActivadorApagarLucesYChirrido"))
                StartCoroutine(EstablecerApagarLucesYChirrido(2f));
        }

        // Asilo
        if (gameObject.CompareTag("ActivadorSustoHReclusionBB"))
            StartCoroutine(EstablecerAsustoHReclusionB(0.8f));

        // Laboratorio
        if (gameObject.CompareTag("ActivadorFinalizar"))
            StartCoroutine(FinalizarJuego(2f));
    }

    private IEnumerator EstablecerAlguienCaminando(float segundosAEsperar)
    {
        eventoEnProceso = true;

        FindObjectOfType<ControlDelJugador>().PuedeMoverse = false;

        AudioSource sonidoAlguienCaminando = 
            GameObject.FindGameObjectWithTag("AlguienCaminando")
            .GetComponent<AudioSource>();
        sonidoAlguienCaminando.Play();

        yield return new WaitForSecondsRealtime(segundosAEsperar);

        sonidoAlguienCaminando.Stop();
        FindObjectOfType<ControlDelJugador>().PuedeMoverse = true;

        eventoEnProceso = false;
        DesactivarScript();
    }

    private IEnumerator EstablecerApagarLucesYChirrido(float segundosAEsperar)
    {
        eventoEnProceso = true;
        FindObjectOfType<ControlDelJugador>().PuedeMoverse = false;

        // sonido chirrido
        gameObject.transform.GetChild(1).gameObject
            .GetComponent<AudioSource>().Play();

        // sonido luz
        gameObject.transform.GetChild(0).gameObject
            .GetComponent<AudioSource>().Play();

        GameObject[] lucesAApagar = 
            GameObject.FindGameObjectsWithTag("LuzAApagar");
        foreach (GameObject luz in lucesAApagar)
        {
            luz.GetComponent<Light>().enabled = false;
        }

        // Cambiar la música de fondo:
        AudioSource reproductorDeMusica =
            GameObject.FindGameObjectWithTag("ReproductorDeMusica").gameObject
            .GetComponent<AudioSource>();
        reproductorDeMusica.clip = musicaACambiar;
        reproductorDeMusica.Play();

        yield return new WaitForSecondsRealtime(2f);

        // Linterna del jugador
        GameObject linternaJugador = 
            GameObject.FindGameObjectWithTag("Jugador")
            .transform.GetChild(8).gameObject;
        linternaJugador.GetComponent<Light>().enabled = true;
        linternaJugador.GetComponent<AudioSource>().Play();

        yield return new WaitForSecondsRealtime(1f);

        FindObjectOfType<ControlDelJugador>().PuedeMoverse = true;

        eventoEnProceso = false;
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

        DesactivarScript();
    }

    private IEnumerator FinalizarJuego(float segundosDeLaAnimacion)
    {
        EstablecerAnimacionPantallaNegra(segundosDeLaAnimacion);
        yield return new WaitForSecondsRealtime(segundosDeLaAnimacion);

        GameObject continuara = 
            GameObject.FindGameObjectWithTag("Continuara").gameObject;
        continuara.transform.GetChild(0).gameObject.
            GetComponent<Image>().enabled = true;
        continuara.transform.GetChild(1).gameObject.
            GetComponent<TMP_Text>().enabled = true;

        yield return new WaitForSecondsRealtime(5f);
        CargadorDeEscenas.CargarEscena("MenuPrincipal");

        DesactivarScript();
    }
}
