using UnityEngine;

public class IniciadorPrimerEscenario : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<ControlDelJugador>().PuedeCorrer = false;

        FindObjectOfType<Mensajero>().Mensaje =
            "¡No encuentro a nadie en este antro! ¡Maldita sea!";
        FindObjectOfType<Mensajero>().MostrarInterfazMensaje();
    }
}
