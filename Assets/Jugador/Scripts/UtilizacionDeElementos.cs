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
            if (gameObject.CompareTag("Proyector") &&
                FindObjectOfType<InventarioJugador>().BombillaEnElInventario)
            {
                FindObjectOfType<InventarioJugador>().
                    BombillaEnElInventario = false;
                EncenderProyectorSalaDeReuniones();
                EstablecerComoUtilizado();
            }

            else if (gameObject.CompareTag("BaldosaSecreta"))
            {
                StartCoroutine(QuitarBaldosaSecretaYMostrarLlaveTorre(2f));
                EstablecerComoUtilizado();
            }
        }
    }

    private void EstablecerComoUtilizado()
    {
        if (audioSource != null)
            audioSource.Play();
        gameObject.tag = "Utilizado";
        DesactivarScript();
    }

    private void ActivarAnimacionPantallaNegra(float segundosDeLaAnimacion)
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
    }

    private IEnumerator QuitarBaldosaSecretaYMostrarLlaveTorre(
        float segundosDeLaAnimacion)
    {
        ActivarAnimacionPantallaNegra(segundosDeLaAnimacion);
        yield return new WaitForSecondsRealtime(segundosDeLaAnimacion); // Esperar a que la animación termine antes de continuar

        GameObject llaveTorre =
            GameObject.FindGameObjectWithTag("ContenedorLlaveTorre").
            gameObject.transform.GetChild(0).gameObject;
        llaveTorre.SetActive(true);

        FindObjectOfType<Mensajero>().Mensaje =
            "Había una llave bajo la baldosa.";
        FindObjectOfType<Mensajero>().MostrarInterfazMensaje();
    }
}
