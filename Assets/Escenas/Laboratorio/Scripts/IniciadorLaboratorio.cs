using System.Collections;
using UnityEngine;

public class IniciadorLaboratorio : MonoBehaviour
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
            "¿¡He derrotado a esa cosa!? ¡Joder! Espero que sí... " +
            "¿¡Y ahora esto que coño es? ¿Un laboratorio debajo del asilo!?";
        FindObjectOfType<Mensajero>().MostrarInterfazMensaje();

        yield return new WaitForSecondsRealtime(4.5f);

        FindObjectOfType<ControlDelJugador>().PuedeMoverse = true;
    }
}
