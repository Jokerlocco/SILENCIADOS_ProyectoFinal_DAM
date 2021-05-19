using System.Collections;
using UnityEngine;

public class GestorCinematicaInicial : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(IrAlJuego());
    }

    private IEnumerator IrAlJuego()
    {
        yield return new WaitForSecondsRealtime(115f);
        CargadorDeEscenas.CargarEscena("Asilo");
    }
}
