using System.Collections;
using UnityEngine;

public class PantallaNegra : MonoBehaviour
{
    private Animator animacion;

    private void Start()
    {
        animacion = gameObject.GetComponent<Animator>();
        animacion.enabled = false;
    }

    public void ActivarAnimacionPantallaNegra()
    {
        animacion.enabled = true;
        FindObjectOfType<ControlDelJugador>().PuedeMoverse = false;
    }

    public void QuitarPantallaNegra(float segundosAEsperar)
    {
        StartCoroutine(EsperarAntesDeQuitar(segundosAEsperar));
    }

    private IEnumerator EsperarAntesDeQuitar(float segundosAEsperar)
    {
        yield return new WaitForSecondsRealtime(segundosAEsperar);
        animacion.SetBool("QuitarPantallaNegra", true);

        yield return new WaitForSecondsRealtime(1);
        FindObjectOfType<ControlDelJugador>().PuedeMoverse = true;
    }
}
