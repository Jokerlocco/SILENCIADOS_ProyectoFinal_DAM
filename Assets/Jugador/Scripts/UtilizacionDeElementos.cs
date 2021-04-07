using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilizacionDeElementos : MonoBehaviour
{
    private bool colisionando = false;

    private AudioSource audioSource; // Asignado en Unity

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
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

    private void DesactivarScript()
    {
        enabled = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interactuar") && colisionando)
        {
            bool elementoUtilizado = false;

            if (gameObject.CompareTag("Proyector") &&
                FindObjectOfType<InventarioJugador>().BombillaEnElInventario)
            {
                FindObjectOfType<InventarioJugador>().
                    BombillaEnElInventario = false;
                EncenderProyectorSalaDeReuniones();
                elementoUtilizado = true;
            }

            else if (gameObject.CompareTag("BaldosaSecreta"))
            {
                StartCoroutine(QuitarBaldosaSecretaYMostrarLlaveTorre(2f));
                elementoUtilizado = true;
            }

            else if (gameObject.CompareTag("CompartimientoDelExtintor") &&
                FindObjectOfType<InventarioJugador>().GanzuaEnElInventario)
            {
                StartCoroutine(UsarGanzuaEnElCompartimientoDelExtintor(2f));
                elementoUtilizado = true;
            }

            else if (gameObject.CompareTag("FuegoEnLaCocina") &&
                 FindObjectOfType<InventarioJugador>().ExtintorEnElInventario)
            {
                StartCoroutine(AgagarFuegoCocinaConExtintor(2f));
                elementoUtilizado = true;
            }


            if (elementoUtilizado)
                EstablecerComoUtilizado();
        }
    }

    private void EstablecerComoUtilizado()
    {
        if (audioSource != null)
            audioSource.Play();

        gameObject.tag = "Utilizado";

        DesactivarScript();
    }

    private void EstablecerAnimacionPantallaNegra(float segundosDeLaAnimacion)
    {
        FindObjectOfType<PantallaNegra>().ActivarAnimacionPantallaNegra();
        FindObjectOfType<PantallaNegra>()
            .QuitarPantallaNegra(segundosDeLaAnimacion);
    }

    private void EncenderProyectorSalaDeReuniones()
    {
        Light luzDelProyector = gameObject.GetComponentInChildren<Light>();
        luzDelProyector.enabled = true;
        GameObject imagenDelProyector =
            GameObject.FindGameObjectWithTag("ImagenDeProyeccion");
        imagenDelProyector.GetComponent<MeshRenderer>().enabled = true;

        FindObjectOfType<Mensajero>().Mensaje =
            "He utilizado la bombilla funcional en el proyector.";
        FindObjectOfType<Mensajero>().MostrarInterfazMensaje();
    }

    private IEnumerator QuitarBaldosaSecretaYMostrarLlaveTorre(
        float segundosDeLaAnimacion)
    {
        EstablecerAnimacionPantallaNegra(segundosDeLaAnimacion);
        yield return new WaitForSecondsRealtime(segundosDeLaAnimacion); // Esperar a que la animación termine antes de continuar

        // Mostrarmos la baldosa levantada:
        GameObject baldosaLevantada1 = 
            gameObject.transform.GetChild(0).gameObject;
        baldosaLevantada1.SetActive(true);
        GameObject baldosaLevantada2 =
            gameObject.transform.GetChild(1).gameObject;
        baldosaLevantada2.SetActive(true);

        // Y la llave
        GameObject llaveTorre =
            GameObject.FindGameObjectWithTag("LlaveTorre").gameObject;
        llaveTorre.GetComponent<Renderer>().enabled = true;
        llaveTorre.transform.GetChild(0).GetComponent<Renderer>().enabled = true;

        FindObjectOfType<Mensajero>().Mensaje =
            "Había una llave bajo la baldosa.";
        FindObjectOfType<Mensajero>().MostrarInterfazMensaje();
    }

    private IEnumerator UsarGanzuaEnElCompartimientoDelExtintor(
        float segundosDeLaAnimacion)
    {
        EstablecerAnimacionPantallaNegra(segundosDeLaAnimacion);
        yield return new WaitForSecondsRealtime(segundosDeLaAnimacion);

        // Cambiamos el cristal del compartimiento
        Renderer cCristalCerradoRender =
            gameObject.transform.GetChild(0).GetComponent<MeshRenderer>();
        cCristalCerradoRender.enabled = false;
        Renderer cCristalAbiertoRender =
            gameObject.transform.GetChild(1).GetComponent<MeshRenderer>();
        cCristalAbiertoRender.enabled = true;

        FindObjectOfType<Mensajero>().Mensaje =
            "He abierto el compartimiento del extintor con la ganzúa.";
        FindObjectOfType<Mensajero>().MostrarInterfazMensaje();
    }

    private IEnumerator AgagarFuegoCocinaConExtintor(
        float segundosDeLaAnimacion)
    {
        GameObject fuegoEnLaCocina =
            GameObject.FindGameObjectWithTag("FuegoEnLaCocina").gameObject;

        EstablecerAnimacionPantallaNegra(segundosDeLaAnimacion);
        yield return new WaitForSecondsRealtime(segundosDeLaAnimacion);

        fuegoEnLaCocina.SetActive(false);
        FindObjectOfType<InventarioJugador>().ExtintorEnElInventario = false;

        FindObjectOfType<Mensajero>().Mensaje =
            "He apagado el fuego con el extintor.";
        FindObjectOfType<Mensajero>().MostrarInterfazMensaje();
    }
}
