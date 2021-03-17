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

    private void DesactivarScript()
    {
        enabled = false;
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
                EncenderProyectorSalaDeReuniones();
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

    private void EncenderProyectorSalaDeReuniones()
    {
        Light luzDelProyector = gameObject.GetComponentInChildren<Light>();
        luzDelProyector.enabled = true;
        GameObject imagenDelProyector = 
            GameObject.FindGameObjectWithTag("ImagenDeProyeccion");
        imagenDelProyector.GetComponent<MeshRenderer>().enabled = true;
    }
}
