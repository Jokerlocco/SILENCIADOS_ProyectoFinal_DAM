using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ActivadorDeEventos : MonoBehaviour
{
    private bool colisionando = false;
    private AudioSource audioSource;
    private bool eventoEnProceso = false;

    private AudioSource reproductorDeMusica;

    [SerializeField] AudioClip musicaACambiar = null; // Asignado en unity

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (GameObject.FindGameObjectWithTag("ReproductorDeMusica"))
        {
            reproductorDeMusica =
                GameObject.FindGameObjectWithTag("ReproductorDeMusica").gameObject
                .GetComponent<AudioSource>();
        }
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
        if (!eventoEnProceso)
        {
            // Primer escenario
            if (gameObject.CompareTag("ActivadorAlguienCaminando"))
                StartCoroutine(EstablecerAlguienCaminando(3f));

            if (gameObject.CompareTag("ActivadorInspeccionSangre"))
                StartCoroutine(EstablecerInspeccionSangre(2f));

            if (gameObject.CompareTag("ActivadorApagarLucesYChirrido"))
                StartCoroutine(EstablecerApagarLucesYChirrido(2f));

            if (gameObject.CompareTag("ActivadorAparicionN45P"))
                StartCoroutine(EstablecerAparicionN45P(2f));

            // Asilo
            if (gameObject.CompareTag("ActivadorPensamientoSobreElPabellon"))
                StartCoroutine(MostrarPensamientoSobreElPabellon());

            if (gameObject.CompareTag("ActivadorMusicaAsilo"))
                CambiarMusicaDeFondo();

            if (gameObject.CompareTag("ActivadorMusicaJukeBox"))
                CambiarMusicaDeFondo();

            // Laboratorio
            if (gameObject.CompareTag("ActivadorFinalizar"))
                StartCoroutine(FinalizarJuego(2f));
        }
    }

    private IEnumerator EstablecerAlguienCaminando(float segundosAEsperar)
    {
        eventoEnProceso = true;

        FindObjectOfType<ControlDelJugador>().PuedeMoverse = false;

        AudioSource sonidoAlguienCaminando = 
            GameObject.FindGameObjectWithTag("AlguienCaminando")
            .GetComponent<AudioSource>();
        sonidoAlguienCaminando.Play();

        FindObjectOfType<Mensajero>().Mensaje =
            "Pasos...";
        FindObjectOfType<Mensajero>().MostrarInterfazMensaje();

        yield return new WaitForSecondsRealtime(segundosAEsperar);

        sonidoAlguienCaminando.Stop();
        FindObjectOfType<ControlDelJugador>().PuedeMoverse = true;

        eventoEnProceso = false;
        DesactivarScript();
    }

    private IEnumerator EstablecerInspeccionSangre(float segundosAEsperar)
    {
        eventoEnProceso = true;

        FindObjectOfType<ControlDelJugador>().PuedeMoverse = false;

        FindObjectOfType<Mensajero>().Mensaje =
            "¡¿Y este charco de sangre?! ¡Dios! Sharon... espero que estés bien...";
        FindObjectOfType<Mensajero>().MostrarInterfazMensaje();

        yield return new WaitForSecondsRealtime(1f);

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

        CambiarMusicaDeFondo();

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

    private IEnumerator EstablecerAparicionN45P(float segundosAEsperar)
    {
        eventoEnProceso = true;

        FindObjectOfType<ControlDelJugador>().PuedeMoverse = false;

        reproductorDeMusica.Stop();

        // Sonido destrucción y tensión
        gameObject.transform.GetChild(0).transform
            .GetComponent<AudioSource>().Play();
        gameObject.transform.GetChild(1).transform
            .GetComponent<AudioSource>().Play();

        yield return new WaitForSecondsRealtime(12f);

        GameObject n45p = 
            GameObject.FindGameObjectWithTag("N45PGeneral").gameObject;
        n45p.transform.GetChild(0).gameObject.SetActive(true);
        n45p.transform.GetChild(1).gameObject.SetActive(true);
        n45p.transform.GetChild(2).gameObject.SetActive(true);
        n45p.transform.GetChild(3).gameObject.SetActive(true);
        n45p.gameObject.GetComponent<MovimientoEnemigos>().enabled = true;
        n45p.gameObject.GetComponent<NavMeshAgent>().enabled = true;

        yield return new WaitForSecondsRealtime(1f);

        CambiarMusicaDeFondo();

        yield return new WaitForSecondsRealtime(0.8f);

        GameObject indicadorDeEsprintar = 
            GameObject.FindGameObjectWithTag("IndicadorDeEsprintar").gameObject;
        indicadorDeEsprintar.transform.GetChild(0).gameObject
            .GetComponent<TMP_Text>().enabled = true;
        indicadorDeEsprintar.transform.GetChild(1).gameObject
            .GetComponent<RawImage>().enabled = true;
        indicadorDeEsprintar.transform.GetChild(2).gameObject
            .GetComponent<TMP_Text>().enabled = true;

        FindObjectOfType<ControlDelJugador>().PuedeMoverse = true;
        FindObjectOfType<ControlDelJugador>().PuedeCorrer = true;

        yield return new WaitForSecondsRealtime(3f);

        indicadorDeEsprintar.transform.GetChild(0).gameObject
            .GetComponent<TMP_Text>().enabled = false;
        indicadorDeEsprintar.transform.GetChild(1).gameObject
            .GetComponent<RawImage>().enabled = false;
        indicadorDeEsprintar.transform.GetChild(2).gameObject
            .GetComponent<TMP_Text>().enabled = false;

        eventoEnProceso = false;
        DesactivarScript();
    }

    private IEnumerator MostrarPensamientoSobreElPabellon()
    {
        eventoEnProceso = true;

        FindObjectOfType<ControlDelJugador>().PuedeMoverse = false;

        FindObjectOfType<Mensajero>().Mensaje =
            "Debo de estar en uno de los pabellones del asilo... " +
            "¿Pero como he acabado aquí?";
        FindObjectOfType<Mensajero>().MostrarInterfazMensaje();

        yield return new WaitForSecondsRealtime(2f);

        FindObjectOfType<ControlDelJugador>().PuedeMoverse = true;

        eventoEnProceso = false;
        DesactivarScript();
    }

    private IEnumerator EstablecerSustoHReclusionB(float segundosDeLaAnimacion)
    {
        eventoEnProceso = true;

        FindObjectOfType<Mensajero>().Mensaje =
            "???";
        FindObjectOfType<Mensajero>().MostrarInterfazMensaje();

        ReproducirSonidoElemento();
        FindObjectOfType<ControlDelJugador>().PuedeMoverse = false;

        yield return new WaitForSecondsRealtime(2.8f); // Esperamos los segundos concretos para cuadrar el golpetazo con la aparición de la pantalla negra
        
        GameObject grietaEnLaPared =
            GameObject.FindGameObjectWithTag("GrietaHReclusionB").gameObject;
        grietaEnLaPared.GetComponent<MeshRenderer>().enabled = true;

        FindObjectOfType<Mensajero>().OcultarInterfazMensaje();

        yield return new WaitForSecondsRealtime(1f);
        FindObjectOfType<Mensajero>().Mensaje =
            "¡¿Qué demonios?! ¡Debo encontrar a Sharon cuanto antes! " +
            "Espero que no haya sido ella...";
        FindObjectOfType<Mensajero>().MostrarInterfazMensaje();

        yield return new WaitForSecondsRealtime(2.5f);
        FindObjectOfType<ControlDelJugador>().PuedeMoverse = true;

        eventoEnProceso = false;
        DesactivarScript();
    }

    private IEnumerator FinalizarJuego(float segundosDeLaAnimacion)
    {
        eventoEnProceso = true;

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

        eventoEnProceso = false;
        DesactivarScript();
    }

    private void CambiarMusicaDeFondo()
    {
        reproductorDeMusica.clip = musicaACambiar;
        reproductorDeMusica.Play();
    }
}
