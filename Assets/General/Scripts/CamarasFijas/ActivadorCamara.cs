using UnityEngine;

public class ActivadorCamara : MonoBehaviour
{
    private GestorDeCamaras gestorDeCamaras;
    [SerializeField] private GameObject camaraAActivar;

    private void Start()
    {
        gestorDeCamaras = FindObjectOfType<GestorDeCamaras>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Jugador"))
        {
            gestorDeCamaras.DesactivarTodasLasCamaras(); // Otra forma de hacer una especie de "SendMessage"
            camaraAActivar.SetActive(true);
        }
    }
}
