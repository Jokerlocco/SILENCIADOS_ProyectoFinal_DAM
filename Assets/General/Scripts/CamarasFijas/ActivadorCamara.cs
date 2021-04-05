using UnityEngine;

public class ActivadorCamara : MonoBehaviour
{
    private GestorDeCamaras gestorDeCamaras;
    [SerializeField] private GameObject camaraAActivar;

    [SerializeField] private bool colisionando;
    [SerializeField] private bool camaraActivada;

    private void Start()
    {
        gestorDeCamaras = FindObjectOfType<GestorDeCamaras>();
        camaraActivada = false;
    }

    private void Update()
    {
        if (colisionando && !camaraActivada)
            ActivarCamara();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador"))
            colisionando = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador"))
        {
            colisionando = false;
            camaraActivada = false;
        }
    }

    private void ActivarCamara()
    {
        gestorDeCamaras.DesactivarTodasLasCamaras(); // Otra forma de hacer una especie de "SendMessage"
        camaraAActivar.SetActive(true);
        camaraActivada = true;
    }
}
