using System.Collections;
using UnityEngine;

public class IniciadorPrimerEscenario : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DarMensajeInicial());
    }

    private IEnumerator DarMensajeInicial()
    {
        yield return new WaitForSecondsRealtime(0.5f);

        FindObjectOfType<ControlDelJugador>().PuedeMoverse = false;

        FindObjectOfType<ControlDelJugador>().PuedeCorrer = false;

        FindObjectOfType<Mensajero>().Mensaje =
            "¡No encuentro a nadie en este antro! ¡Maldita sea!";
        FindObjectOfType<Mensajero>().MostrarInterfazMensaje();

        yield return new WaitForSecondsRealtime(3f);

        FindObjectOfType<ControlDelJugador>().PuedeMoverse = true;
    }
}
