using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementoInteractivo : MonoBehaviour
{
    private bool colisionando = false;

    [SerializeField] RawImage imagenIndicadorInteraccion = null; // Asignado en Unity

    private void Update()
    {
        if (gameObject.CompareTag("Recogido"))
        {
            OcultarIndicadorDeInteraccion();
            DesactivarFuncionUpdate();
        }
        else
        {
            if (colisionando)
                MostrarIndicadorDeInteraccion();
        }     
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador"))
        {
            colisionando = true;
        }      
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador"))
        {
            colisionando = false;
            OcultarIndicadorDeInteraccion();
        }
    }

    private void MostrarIndicadorDeInteraccion()
    {
        imagenIndicadorInteraccion.enabled = true;
        Vector3 posicionAColocar = gameObject.transform.position;
        imagenIndicadorInteraccion.
            rectTransform.anchoredPosition = posicionAColocar;
        imagenIndicadorInteraccion.rectTransform.sizeDelta =
            new Vector2(100f, 100f);
    }

    private void OcultarIndicadorDeInteraccion()
    {
        imagenIndicadorInteraccion.enabled = false;
    }

    private void DesactivarFuncionUpdate()
    {
        /* Cómo al coger un objeto, el gameobject siempre está en "RECOGIDO", 
         * desactivamos la función UPDATE de este objeto para evitar que
         * oculte el icono de interacción (para el resto de elementos). */
        enabled = false;
    }
}
