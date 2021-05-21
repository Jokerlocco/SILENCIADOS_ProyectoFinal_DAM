using UnityEngine;
using UnityEngine.UI;

public class EstadoDelJugador : MonoBehaviour
{
    public bool JugadorVivo { get; set; } = true;

    private float dañoPorGolpeRecibido = 100f;

    private float vidaMaxima = 500f; // Asignado en Unity
    private float vidaActual = 500f; // Asignado en Unity

    [SerializeField] Image barraDeVida = null; // Asignado en Unity

    private AudioSource audioSource;
    [SerializeField] AudioClip sonidoDaño = null; // Asignado en Unity

    private void Start()
    {
        audioSource = 
            gameObject.transform.GetChild(1).GetComponent<AudioSource>();
    }

    private void Update()
    {
        ActualizarBarraDeVida();

        if (vidaActual <= 0f && JugadorVivo)
            Morir();
    }

    public void RecibirDaño()
    {
        vidaActual -= dañoPorGolpeRecibido;
        ReproducirSonidoDeDaño();
        FindObjectOfType<SangreEnPantalla>().MostrarSangreEnPantalla();
    }

    private void Morir()
    {
        CargadorDeEscenas.CargarEscenaDirectamente("PantallaDeMuerte");
        JugadorVivo = true; // Para asegurar que no la cargue más veces
    }

    private void ActualizarBarraDeVida()
    {
        barraDeVida.fillAmount = vidaActual / vidaMaxima;
    }

    public void ReproducirSonidoDeDaño()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(sonidoDaño);
        }
    }
}
