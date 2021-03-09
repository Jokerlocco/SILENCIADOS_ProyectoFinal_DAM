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
        if (colisionando)
            MostrarIndicadorDeInteraccion();

        if (gameObject.CompareTag("Recogido"))
            OcultarIndicadorDeInteraccion();
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
}
