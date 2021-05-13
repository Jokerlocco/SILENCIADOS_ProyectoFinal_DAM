using UnityEngine;
using UnityEngine.AI;

public class EstadoDelEnemigo : MonoBehaviour
{
    private bool enemigoVivo = true;

    [SerializeField] int numeroDeBalasNecesariasParaMorir = 3; // Asignado en Unity
    public int NumeroDeBalasRecibidas { get; set; } = 0;

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
        if (NumeroDeBalasRecibidas == numeroDeBalasNecesariasParaMorir 
            && enemigoVivo)
            Morir();
    }

    public void ReproducirSonidoDeDaño()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(sonidoDaño);
        }     
    }

    private void ReproducirSonidoDeMuerte()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(sonidoMuerte);
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
            Destroy(gameObject, 2f);
        }
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
