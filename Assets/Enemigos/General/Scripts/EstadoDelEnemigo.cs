using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EstadoDelEnemigo : MonoBehaviour
{
    /* Peones: 3 balas - 300f
     * N45P: 30 balas - 3000f
     */

    private float dañoPorBala = 100f;

    private bool enemigoVivo = true;
    [SerializeField] float vidaMaxima = 300f; // Asignado en Unity
    [SerializeField] float vidaActual = 300f; // Asignado en Unity

    [SerializeField] Image barraDeVidaDeN45P = null; // Asignado en Unity

    private AudioSource audioSource;
    [SerializeField] AudioClip sonidoDaño = null; // Asignado en Unity
    [SerializeField] AudioClip sonidoMuerte = null; // Asignado en Unity 

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
            FindObjectOfType<SpawnerEnemigos>().NumEnemigosExistentes -= 1;
            Destroy(gameObject, 2f);
        }
    }

    private void ActualizarBarraDeVidaDeN45P()
    {
        if (gameObject.CompareTag("N45P"))
            barraDeVidaDeN45P.fillAmount =
                vidaActual / vidaMaxima;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador"))
        {
            FindObjectOfType<EstadoDelJugador>().RecibirDaño();
            Morir();
        }
    }
}
