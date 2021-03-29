using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mensajero : MonoBehaviour
{
    [SerializeField] TMP_Text mensajeDeInteraccion; // Asignado en Unity
    [SerializeField] GameObject fondoOscuroTraslucidoMensajes; // Asignado en Unity
    public bool InterfazMensajeActiva { get; set; } = false;
    public string Mensaje { get; set; }

    public void MostrarInterfazMensaje()
    {
        mensajeDeInteraccion.text = Mensaje;
        fondoOscuroTraslucidoMensajes.SetActive(true);
        InterfazMensajeActiva = true;

        StartCoroutine(EsperarEnLaInterfazAntesDeCerrar());
    }

    private IEnumerator EsperarEnLaInterfazAntesDeCerrar()
    {
        yield return new WaitForSecondsRealtime(3);
        OcultarInterfazMensaje();
    }

    public void OcultarInterfazMensaje()
    {
        if (InterfazMensajeActiva)
        {
            InterfazMensajeActiva = false;
            mensajeDeInteraccion.text = "";
            fondoOscuroTraslucidoMensajes.SetActive(false);
        }
    }
}
