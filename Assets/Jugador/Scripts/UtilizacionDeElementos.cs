using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilizacionDeElementos : MonoBehaviour
{
    private bool colisionando = false;

    private AudioSource audioSource; // Asignado en Unity

    private void Start()
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
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interactuar") && colisionando)
        {
            if (gameObject.CompareTag("Proyector") &&
            FindObjectOfType<InventarioJugador>().BombillaEnElInventario)
            {
                FindObjectOfType<InventarioJugador>().
                    BombillaEnElInventario = false;
                EstablecerComoUtilizado();
            }    
        }
    }

    private void EstablecerComoUtilizado()
    {
        audioSource.Play();
        gameObject.tag = "Utilizado";
        DesactivarScript();
    }

    private void DesactivarScript()
    {
        enabled = false;
    }
}
