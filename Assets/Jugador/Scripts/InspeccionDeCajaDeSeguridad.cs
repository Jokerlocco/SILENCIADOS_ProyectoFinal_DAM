using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspeccionDeCajaDeSeguridad : MonoBehaviour
{
    private bool colisionando = false;

    [SerializeField] GameObject panelNumerico; // Asignado en Unity

    private void Update()
    {
        if (Input.GetButtonDown("Interactuar") && colisionando)
            MostrarPanelNumerico();
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

    private void MostrarPanelNumerico()
    {
        panelNumerico.SetActive(true);
    }
}
