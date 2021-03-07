using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InspeccionDeElementos : MonoBehaviour
{
    [SerializeField] TMP_Text mensajeDeInteraccion; // Asignado en Unity

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador") && 
            Input.GetButtonDown("Interactuar"))
        {
            if (gameObject.CompareTag("InspeccionPuertaPrincipal"))
                mensajeDeInteraccion.text = 
                    "La puerta principal está bloqueada. " +
                    "Debo buscar otra forma de salir.";

            else if (gameObject.CompareTag("InspeccionAscensor"))
                mensajeDeInteraccion.text = 
                    "El ascensor está destrozado. No se puede utilizar.";
        } 
    }

    void OnTriggerExit(Collider other)
    {
        mensajeDeInteraccion.text = "";
    }
}
