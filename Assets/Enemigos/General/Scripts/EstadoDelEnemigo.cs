using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EstadoDelEnemigo : MonoBehaviour
{
    /* Peones: 2 balas - 100f
     * Alfiles: 3 balas - 200f
     * Caballos: 4 balas - 300f
     * Torres: 5 balas - 400f
     * N45P: 30 balas - 5000f
     */

    private float dañoPorBala = 100f;

    private bool enemigoVivo = true;
    [SerializeField] float vidaMaxima = 0f; // Asignado en Unity
    [SerializeField] float vidaActual = 0f; // Asignado en Unity

    [SerializeField] Image barraDeVidaDeN45P = null; // Asignado en Unity

    private AudioSource audioSource;
    [SerializeField] AudioClip sonidoDaño = null; // Asignado en Unity
    [SerializeField] AudioClip sonidoMuerte = null; // Asignado en Unity 

    private GameObject particulasDeDañoRecibidoN45P;
    private GameObject particulasDeDañoRecibido;

    private void Awake()
    {
        InicializarParticulas();
    }

    private void Start()
    {
        if (gameObject.CompareTag("N45P"))
        {
            audioSource = gameObject.transform.GetChild(2)
                .GetComponent<AudioSource>();
        }
        else
        {
            audioSource = // Sonido Auxiliar
                gameObject.transform.GetChild(0).GetComponent<AudioSource>();
        }
    }

    private void Update()
    {
        ActualizarBarraDeVidaDeN45P();

        if (vidaActual <= 0f && enemigoVivo)
        {
            Morir();
        }
    }

    private void ReproducirSonidoDeDaño()
    {
        if (audioSource != null)
            audioSource.PlayOneShot(sonidoDaño); 
    }

    private void ReproducirSonidoDeMuerte()
    {
        if (audioSource != null)
            audioSource.PlayOneShot(sonidoMuerte);
    }

    public void RecibirDaño()
    {
        vidaActual -= dañoPorBala;
        ReproducirSonidoDeDaño();
        if (gameObject.CompareTag("N45P"))
            StartCoroutine(MostrarParticulasDeDañoRecibidoDeN45P(0.6f));
        else
            StartCoroutine(MostrarParticulasDeDañoRecibido(0.6f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador"))
        {
            FindObjectOfType<EstadoDelJugador>().RecibirDaño();
            Morir();
        }
    }

    private void Morir()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Muerte");
        gameObject.GetComponent<NavMeshAgent>().speed = 0f;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        enemigoVivo = false;

        if (!enemigoVivo)
        {
            ReproducirSonidoDeMuerte();
            FindObjectOfType<SpawnerEnemigos>()
                .ApuntarEnemigoDespawneado(gameObject.tag);
            if (!gameObject.CompareTag("N45P"))
            {
                Destroy(gameObject, 2f);
            }
            else
            {
                CargadorDeEscenas.CargarEscenaDirectamente("MenuPrincipal");
            }

        }
    }

    private void ActualizarBarraDeVidaDeN45P()
    {
        if (gameObject.CompareTag("N45P"))
            barraDeVidaDeN45P.fillAmount =
                vidaActual / vidaMaxima;
    }

    private IEnumerator MostrarParticulasDeDañoRecibidoDeN45P(float segundosAEsperar)
    {
        particulasDeDañoRecibidoN45P.transform.GetChild(0)
            .GetComponent<ParticleSystem>().Play();
        particulasDeDañoRecibidoN45P.transform.GetChild(1)
            .GetComponent<ParticleSystem>().Play();
        particulasDeDañoRecibidoN45P.transform.GetChild(2)
            .GetComponent<ParticleSystem>().Play();

        yield return new WaitForSecondsRealtime(segundosAEsperar);

        particulasDeDañoRecibidoN45P.transform.GetChild(0)
            .GetComponent<ParticleSystem>().Stop();
        particulasDeDañoRecibidoN45P.transform.GetChild(1)
            .GetComponent<ParticleSystem>().Stop();
        particulasDeDañoRecibidoN45P.transform.GetChild(2)
            .GetComponent<ParticleSystem>().Stop();
    }

    private IEnumerator MostrarParticulasDeDañoRecibido(float segundosAEsperar)
    {
        particulasDeDañoRecibido.GetComponent<ParticleSystem>().Play();
        particulasDeDañoRecibido.transform.GetChild(2)
            .GetComponent<ParticleSystem>().Play();

        yield return new WaitForSecondsRealtime(segundosAEsperar);

        particulasDeDañoRecibido.GetComponent<ParticleSystem>().Stop();
        particulasDeDañoRecibido.transform.GetChild(2)
            .GetComponent<ParticleSystem>().Stop();
    }

    private void InicializarParticulas()
    {
        if (gameObject.CompareTag("N45P"))
        {
            particulasDeDañoRecibidoN45P =
                gameObject.transform.GetChild(3).gameObject;
            particulasDeDañoRecibidoN45P.transform.GetChild(0)
                .GetComponent<ParticleSystem>().Stop();
            particulasDeDañoRecibidoN45P.transform.GetChild(1)
                .GetComponent<ParticleSystem>().Stop();
            particulasDeDañoRecibidoN45P.transform.GetChild(2)
                .GetComponent<ParticleSystem>().Stop();
        }
        else
        {
            particulasDeDañoRecibido = gameObject.transform.GetChild(1)
                .gameObject.transform.GetChild(0).gameObject;
            particulasDeDañoRecibido.GetComponent<ParticleSystem>().Stop();
            particulasDeDañoRecibido.transform.GetChild(2)
                .GetComponent<ParticleSystem>().Stop();
        }
    }
}
