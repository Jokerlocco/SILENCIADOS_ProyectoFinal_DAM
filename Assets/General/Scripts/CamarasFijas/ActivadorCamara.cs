using System.Collections;
using UnityEngine;

public class ActivadorCamara : MonoBehaviour
{
    private GestorDeCamaras gestorDeCamaras;
    [SerializeField] private GameObject camaraAActivar;

    private void Start()
    {
        gestorDeCamaras = FindObjectOfType<GestorDeCamaras>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador"))
            gestorDeCamaras.CambiarCamara(camaraAActivar);
    }
}
