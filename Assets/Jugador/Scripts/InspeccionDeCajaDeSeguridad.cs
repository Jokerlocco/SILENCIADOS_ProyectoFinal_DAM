using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspeccionDeCajaDeSeguridad : MonoBehaviour
{
    private bool colisionando = false;

    private void Update()
    {
        if (Input.GetButtonDown("Interactuar") && colisionando && 
            gameObject.CompareTag("CajaDeSeguridadSecretaria") &&
            !FindObjectOfType<EstadoDelJuego>().
                CajaDeSeguridadSecretariaAbierta)
        {
            ActivarScriptParaLaCajaEspecifica();
            MostrarPanelNumericoDeLaCajaDeSeguridad();
        }
        else if (Input.GetButtonDown("Interactuar") && colisionando &&
            gameObject.CompareTag("CajaDeSeguridadSObservacion") &&
            !FindObjectOfType<EstadoDelJuego>().
                CajaDeSeguridadSObservacionAbierta)
        {
            ActivarScriptParaLaCajaEspecifica();
            MostrarPanelNumericoDeLaCajaDeSeguridad();
        }
        else if (Input.GetButtonDown("Interactuar") && colisionando &&
            gameObject.CompareTag("CajaDeSeguridadDMJ") &&
            !FindObjectOfType<EstadoDelJuego>().
                CajaDeSeguridadDMJAbierta)
        {
            ActivarScriptParaLaCajaEspecifica();
            MostrarPanelNumericoDeLaCajaDeSeguridad();
        }
        else if (Input.GetButtonDown("Interactuar") && colisionando &&
            gameObject.CompareTag("CajaDeSeguridadSMaquinas") &&
            !FindObjectOfType<EstadoDelJuego>().
                CajaDeSeguridadSMaquinasAbierta)
        {
            ActivarScriptParaLaCajaEspecifica();
            MostrarPanelNumericoDeLaCajaDeSeguridad();
        }
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

    private void ActivarScriptParaLaCajaEspecifica()
    {
        gameObject.GetComponent<CajaDeSeguridad>().enabled = true;
    }

    private void MostrarPanelNumericoDeLaCajaDeSeguridad()
    {
            FindObjectOfType<CajaDeSeguridad>().
                        SendMessage("AbrirPanelNumerico");
    }
}
