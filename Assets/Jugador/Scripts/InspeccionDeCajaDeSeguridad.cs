using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspeccionDeCajaDeSeguridad : MonoBehaviour
{
    private bool colisionando = false;

    private void Update()
    {
        if (Input.GetButtonDown("Interactuar") && colisionando && 
            !FindObjectOfType<CajaDeSeguridad>().CajaAbierta)
            MostrarPanelNumericoDeLaCajaDeSeguridad();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador"))
            colisionando = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador"))
            colisionando = false;
    }

    private void MostrarPanelNumericoDeLaCajaDeSeguridad()
    {
        FindObjectOfType<CajaDeSeguridad>().
                        SendMessage("AbrirPanelNumerico");
    }
}
