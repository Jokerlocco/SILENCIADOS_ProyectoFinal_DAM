using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementoInteractivo : MonoBehaviour
{
    [SerializeField] RawImage imagenIndicadorInteraccion = null; // Asignado en Unity

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador"))
        {
            imagenIndicadorInteraccion.enabled = true;
            Vector3 posicionAColocar = gameObject.transform.position;
            imagenIndicadorInteraccion.
                rectTransform.anchoredPosition = posicionAColocar;
            imagenIndicadorInteraccion.rectTransform.sizeDelta = 
                new Vector2(100f, 100f);
        }      
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador"))
        {
            imagenIndicadorInteraccion.enabled = false;
        }
    }
}
