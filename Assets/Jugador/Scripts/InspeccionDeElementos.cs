using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InspeccionDeElementos : MonoBehaviour
{
    [SerializeField] TMP_Text mensajeDeInteraccion; // Asignado en Unity
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador") && 
            Input.GetButtonDown("Interactuar"))
        {
            if (gameObject.CompareTag("InspeccionPuertaPrincipal"))
            {
                mensajeDeInteraccion.text =
                    "La puerta principal está bloqueada. " +
                    "Debo buscar otra forma de salir.";
                audioSource.Play();
            }

            else if (gameObject.CompareTag("InspeccionAscensor"))
                mensajeDeInteraccion.text = 
                    "El ascensor está destrozado. No se puede utilizar.";

            else if (gameObject.CompareTag("InspeccionEstatua"))
                mensajeDeInteraccion.text =
                    "La estatua tiene tres orificios vacíos. " +
                    "Es cómo si se tuviera que insertar algo.";
        } 
    }

    void OnTriggerExit(Collider other)
    {
        mensajeDeInteraccion.text = "";
    }
}
