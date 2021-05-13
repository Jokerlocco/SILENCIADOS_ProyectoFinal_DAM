using UnityEngine;

public class EstadoDelJugador : MonoBehaviour
{
    public bool JugadorVivo { get; set; } = true;

    private int numeroDeGolpesNecesariasParaMorir = 3;
    private int numeroDeGolpesRecibidos = 0;

    private AudioSource audioSource;
    [SerializeField] AudioClip sonidoDaño = null; // Asignado en Unity
    [SerializeField] AudioClip sonidoMuerte = null; // Asignado en Unity

    private void Start()
    {
        audioSource = 
            gameObject.transform.GetChild(1).GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (numeroDeGolpesRecibidos == numeroDeGolpesNecesariasParaMorir
            && JugadorVivo)
            Morir();
    }

    public void RecibirDaño()
    {
        numeroDeGolpesRecibidos++;
        ReproducirSonidoDeDaño();
        FindObjectOfType<SangreEnPantalla>().MostrarSangreEnPantalla();
    }

    private void Morir()
    {
        Debug.Log("MUERTE");
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
}
