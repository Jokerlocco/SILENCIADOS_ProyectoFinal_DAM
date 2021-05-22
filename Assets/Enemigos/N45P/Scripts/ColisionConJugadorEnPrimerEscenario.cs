using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ColisionConJugadorEnPrimerEscenario : MonoBehaviour
{
    private bool colisionando = false;
    private bool ejecutandoFuncion = false;

    private void Update()
    {
        if (colisionando && !ejecutandoFuncion)
        {
            ejecutandoFuncion = true;
            StartCoroutine(TerminarPrimerEscenario());
        }
    }

    private IEnumerator TerminarPrimerEscenario()
    {
        GameObject.FindGameObjectWithTag("ImagenEnNegro").gameObject
                .GetComponent<Image>().enabled = true;

        if (PararTodoElAudioDeLaEscena())
        {
            GameObject.FindGameObjectWithTag("SonidoFinal").gameObject
                .GetComponent<AudioSource>().Play();
        }

        yield return new WaitForSecondsRealtime(7);
        CargadorDeEscenas.CargarEscena("Asilo");
    }

    private bool PararTodoElAudioDeLaEscena()
    {
        AudioSource[] audioSources = 
            FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.Stop();
        }

        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador"))
        {
            colisionando = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador"))
        {
            colisionando = false;
        }
    }
}
