using System.Collections;
using TMPro;
using UnityEngine;

public class Mensajero : MonoBehaviour
{
    [SerializeField] TMP_Text mensajeDeInteraccion; // Asignado en Unity
    [SerializeField] GameObject fondoOscuroTraslucidoMensajes; // Asignado en Unity
    public bool InterfazMensajeActiva { get; set; } = false;
    public string Mensaje { get; set; }
    private float segundosAEsperarAntesDeCerrarInterfaz = 4;
    private bool mostrandoInterfaz = false;

    public void MostrarInterfazMensaje()
    {
        if (mostrandoInterfaz)
        {
            StopCoroutine("MostrarMensajeYCerrar");
            mostrandoInterfaz = false;
        }
            
        StartCoroutine("MostrarMensajeYCerrar");
    }

    private IEnumerator MostrarMensajeYCerrar()
    {
        mostrandoInterfaz = true;

        mensajeDeInteraccion.text = Mensaje;
        fondoOscuroTraslucidoMensajes.SetActive(true);
        InterfazMensajeActiva = true;

        yield return new WaitForSecondsRealtime(
            segundosAEsperarAntesDeCerrarInterfaz);

        if (InterfazMensajeActiva)
            OcultarInterfazMensaje();

        mostrandoInterfaz = false;
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
