using System.Collections;
using UnityEngine;

public class GestorPantallaDeMuerte : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(VolverAlOtroMundo());
    }

    private IEnumerator VolverAlOtroMundo()
    {
        yield return new WaitForSecondsRealtime(18.8f);
        CargadorDeEscenas.CargarEscena("ElOtroMundo");
    }
}
