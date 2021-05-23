using System.Collections;
using UnityEngine;

public class GestorDerrotaDeN45P : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(IrAlLaboratorio());
    }

    private IEnumerator IrAlLaboratorio()
    {
        yield return new WaitForSecondsRealtime(18f);
        CargadorDeEscenas.CargarEscena("Laboratorio");
    }
}
