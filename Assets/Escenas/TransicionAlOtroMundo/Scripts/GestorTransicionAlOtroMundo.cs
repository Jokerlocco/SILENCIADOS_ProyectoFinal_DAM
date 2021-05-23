using System.Collections;
using UnityEngine;

public class GestorTransicionAlOtroMundo : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(IrAlOtroMundo());
    }

    private IEnumerator IrAlOtroMundo()
    {
        yield return new WaitForSecondsRealtime(31.8f);
        CargadorDeEscenas.CargarEscenaDirectamente("ElOtroMundo");
    }
}
