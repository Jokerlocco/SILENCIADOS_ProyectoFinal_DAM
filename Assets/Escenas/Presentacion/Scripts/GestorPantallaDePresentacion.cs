using System.Collections;
using UnityEngine;

public class GestorPantallaDePresentacion : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(IrAlMenuPrincipal());
    }

    private IEnumerator IrAlMenuPrincipal()
    {
        yield return new WaitForSecondsRealtime(53f);
        CargadorDeEscenas.CargarEscenaDirectamente("MenuPrincipal");
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cerrar"))
        {
            CargadorDeEscenas.CargarEscena("MenuPrincipal");
        }
    }
}
