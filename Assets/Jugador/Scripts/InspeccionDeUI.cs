using UnityEngine;
using UnityEngine.UI;

public class InspeccionDeUI : MonoBehaviour
{
    private bool colisionando = false;

    private AudioSource audioSource;

    private bool uiActivo = false;

    [SerializeField] private GameObject codigoPizarraCanvas = null; // Asignado en Unity

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
            QuitarUI();
        }
    }

    private void ReproducirSonidoElemento()
    {
        if (audioSource != null)
            audioSource.Play();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interactuar") && colisionando)
        {
            ActivarUI();
        }

        if (Input.GetButtonDown("Cerrar") && uiActivo)
            QuitarUI();
    }

    private void ActivarUI()
    {
        uiActivo = true;
        FindObjectOfType<ControlDelJugador>().PuedeMoverse = false;

        if (gameObject.CompareTag("InspeccionPizarra"))
        {
            codigoPizarraCanvas.GetComponent<RawImage>().enabled = true;
        }
    }

    private void QuitarUI()
    {
        uiActivo = false;
        FindObjectOfType<ControlDelJugador>().PuedeMoverse = true;

        codigoPizarraCanvas.GetComponent<RawImage>().enabled = false;
    }
}
