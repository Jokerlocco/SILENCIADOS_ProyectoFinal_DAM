using System.Collections;
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
        if (InterfazMensajeActiva) // Si hay un mensaje anterior abierto, lo quitamos.
        {
            StopCoroutine("EsperarEnLaInterfazAntesDeCerrar");
        }

        mensajeDeInteraccion.text = Mensaje;
        fondoOscuroTraslucidoMensajes.SetActive(true);
        InterfazMensajeActiva = true;
        StartCoroutine("EsperarEnLaInterfazAntesDeCerrar");
    }

    private IEnumerator EsperarEnLaInterfazAntesDeCerrar()
    {
        yield return new WaitForSecondsRealtime(4);

        if (InterfazMensajeActiva)
            OcultarInterfazMensaje();
    }

    // En la inspección de elementos y puertas, si el jugador sale del rango, también quitamos el mensaje
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
