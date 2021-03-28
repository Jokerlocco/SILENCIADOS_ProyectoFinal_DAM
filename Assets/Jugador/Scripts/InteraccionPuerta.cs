using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteraccionPuerta : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] AudioClip sonidoPuerta; // Asignado en Unity o null
    [SerializeField] AudioClip sonidoPuertaCerrada; // Asignado en Unity o null

    private Animator animacion;

    [SerializeField] private bool interactuandoConLaPuerta = false;
    private bool puertaAbierta = false;

    [SerializeField] bool puertaConLlave;
    [SerializeField] bool puertaDesbloqueada;
    [SerializeField] bool puedeAbrirOCerrarPuerta;

    private bool haUsadoLlave = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animacion = GetComponent<Animator>();

        if (gameObject.CompareTag("PuertaPeon"))
        {
            puertaConLlave = true;
            puertaDesbloqueada = false;
            puedeAbrirOCerrarPuerta = false;
            audioSource.clip = sonidoPuertaCerrada;
        }
        else
        {
            puertaConLlave = false;
            puedeAbrirOCerrarPuerta = true;
            audioSource.clip = sonidoPuerta;
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Interactuar") && interactuandoConLaPuerta)
        {
            if (!puertaDesbloqueada)
                ComprobarSiPuedeDesbloquear();

            AbrirOCerrarPuertas();
        }
    }

    private void ComprobarSiPuedeDesbloquear()
    {
        if (puertaConLlave && !puertaDesbloqueada)
        {
            string[] datos = new string[2];

            if (gameObject.CompareTag("PuertaPeon") && 
                FindObjectOfType<InventarioJugador>().LlavePeonEnElInventario)
            {
                DesbloquearPuerta();
                FindObjectOfType<InventarioJugador>().NumUsosLlavePeon++;
                datos[0] = "peón";
                datos[1] = "PuertaDesbloqueada";
            }
            else
            {
                datos[1] = "PuertaBloqueada";
            }

            FindObjectOfType<InspeccionDeElementos>().
                SendMessage("InformarSobreIntentoDeDesbloqueoDePuertas", datos);

            ReproducirSonidoPuerta();
        }
    }

    private void DesbloquearPuerta()
    {
        puertaDesbloqueada = true;
        puedeAbrirOCerrarPuerta = true;
        audioSource.clip = sonidoPuerta;
    }

    private void AbrirOCerrarPuertas() // abrir o cerrar puertas
    {
        if (puedeAbrirOCerrarPuerta)
        {
            // Alternamos el booleano en cada entrada
            puertaAbierta = !puertaAbierta;
        }

        if(puertaAbierta)
            animacion.SetBool("abierta", true);
        else
            animacion.SetBool("abierta", false);

        ReproducirSonidoPuerta();
    }

    private void ReproducirSonidoPuerta()
    {
        if (audioSource != null)
            audioSource.Play();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador"))
            interactuandoConLaPuerta = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador"))
            interactuandoConLaPuerta = false;
    }
}
