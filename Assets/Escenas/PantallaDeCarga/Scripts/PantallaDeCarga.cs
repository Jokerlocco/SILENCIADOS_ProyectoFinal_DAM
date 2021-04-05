using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PantallaDeCarga : MonoBehaviour
{
    private void Start()
    {
        string escenaACargar = CargadorDeEscenas.escenaACargar;
        StartCoroutine(CargarEscena(escenaACargar)); 
    }

    private IEnumerator CargarEscena(string escenaACargar)
    {
        AsyncOperation carga = SceneManager.LoadSceneAsync(escenaACargar); // Cargamos la escena de forma asíncrona

        while (carga.isDone == false)
        {
            yield return null;
        }

        yield return new WaitForSecondsRealtime(0.5f);
    }
}
