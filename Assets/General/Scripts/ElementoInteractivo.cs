using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementoInteractivo : MonoBehaviour
{
    [SerializeField] RawImage imagenIndicador = null; // Asignado en Unity

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador"))
        {
            imagenIndicador.enabled = true;
            Vector3 posicionAColocar = gameObject.transform.position;
            //imagenIndicador.transform.position = posicionAColocar;
            imagenIndicador.rectTransform.anchoredPosition = posicionAColocar;
            imagenIndicador.rectTransform.sizeDelta = new Vector2(100f, 100f);
        }      
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador"))
        {
            imagenIndicador.enabled = false;
        }
    }
}
