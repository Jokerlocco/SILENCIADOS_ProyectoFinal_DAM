using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mensajero : MonoBehaviour
{
    [SerializeField] TMP_Text mensajeDeInteraccion; // Asignado en Unity
    [SerializeField] GameObject fondoOscuroTraslucidoMensajes; // Asignado en Unity
    private bool mensajeActivo = false;

    private void ActivarInterfazMensaje()
    {
        fondoOscuroTraslucidoMensajes.SetActive(true);
        mensajeActivo = true;
    }

    private void QuitarInterfazMensaje()
    {
        mensajeActivo = false;
        mensajeDeInteraccion.text = "";
        fondoOscuroTraslucidoMensajes.SetActive(false);
    }

    public void InformarSobreIntentoDeDesbloqueoDePuertas(string[] datos) // Es llamado desde "InteraccionPuerta"
    {
        string tipoDeCerradura = datos[0];
        string estadoDePuerta = datos[1];

        if (estadoDePuerta == "PuertaBloqueada")
        {
            mensajeDeInteraccion.text = "La puerta está cerrada. " +
                "En la cerradura hay grabada una pieza de ajedrez: ";

            if (tipoDeCerradura != "torre")
                mensajeDeInteraccion.text += "Un " + tipoDeCerradura + ".";
            else
                mensajeDeInteraccion.text += "Una " + tipoDeCerradura + ".";
        }
        else
        {
            mensajeDeInteraccion.text =
                "Has desbloqueado la puerta con la llave " + tipoDeCerradura;
        }

        ActivarInterfazMensaje();
    }
}
