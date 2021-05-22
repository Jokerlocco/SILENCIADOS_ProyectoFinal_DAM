using System.Collections;
using UnityEngine;

public class IniciadorAsilo : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DarMensajeInicial());
    }

    private IEnumerator DarMensajeInicial()
    {
        yield return new WaitForSecondsRealtime(0.5f);

        FindObjectOfType<ControlDelJugador>().PuedeMoverse = false;

        FindObjectOfType<Mensajero>().Mensaje =
            "Joder... ¿Qué ha pasado? ¿Qué era esa cosa? ¿Dónde estoy? " +
            "¿Cuánto tiempo he estado inconsciente? " +
            "No importa, debo seguir buscando...";
        FindObjectOfType<Mensajero>().MostrarInterfazMensaje();

        yield return new WaitForSecondsRealtime(4.5f);

        FindObjectOfType<ControlDelJugador>().PuedeMoverse = true;
    }
}
