using System.Collections;
using UnityEngine;

public class InteraccionPuerta : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] AudioClip sonidoPuerta; // Asignado en Unity o null
    [SerializeField] AudioClip sonidoPuertaCerrada; // Asignado en Unity o null

    private Animator animacion;

    private bool interactuandoConLaPuerta = false;
    private bool puertaAbierta = false;

    private bool puertaConLlave;
    private bool puertaDesbloqueada;
    private bool puedeAbrirOCerrarPuerta;

    private string tipoDeCerradura;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animacion = GetComponent<Animator>();

        InicializarPuertasYLlaves();
    }

    private void InicializarPuertasYLlaves()
    {
        if (gameObject.CompareTag("PuertaAlfil"))
            InicializarPuertasConCerradura("alfil");
        else if (gameObject.CompareTag("PuertaCaballo"))
            InicializarPuertasConCerradura("caballo");
        else if (gameObject.CompareTag("PuertaPeon"))
            InicializarPuertasConCerradura("peón");
        else if (gameObject.CompareTag("PuertaRey"))
            InicializarPuertasConCerradura("rey");
        else if (gameObject.CompareTag("PuertaTorre"))
            InicializarPuertasConCerradura("torre");
        else
        {
            puertaConLlave = false;
            puedeAbrirOCerrarPuerta = true;
            audioSource.clip = sonidoPuerta;
        }
    }

    private void InicializarPuertasConCerradura(string tipoDeCerradura)
    {
        puertaConLlave = true;
        puertaDesbloqueada = false;
        puedeAbrirOCerrarPuerta = false;
        audioSource.clip = sonidoPuertaCerrada;
        this.tipoDeCerradura = tipoDeCerradura;
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

    private void AbrirOCerrarPuertas()
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
        {
            interactuandoConLaPuerta = false;
            FindObjectOfType<Mensajero>().OcultarInterfazMensaje();
        }
    }


    // ============= DESBLOQUEO Y DESCARTE DE PUERTAS CON LLAVE =============
    private void ComprobarSiPuedeDesbloquear()
    {
        if (puertaConLlave && !puertaDesbloqueada)
        {
            ReproducirSonidoPuerta();

            if (gameObject.CompareTag("PuertaAlfil") &&
                FindObjectOfType<InventarioJugador>().LlaveAlfilEnElInventario)
            {
                DesbloquearPuerta();
                FindObjectOfType<InventarioJugador>().NumUsosLlaveAlfil++;
            }

            if (gameObject.CompareTag("PuertaCaballo") &&
                FindObjectOfType<InventarioJugador>().LlaveCaballoEnElInventario)
            {
                DesbloquearPuerta();
                FindObjectOfType<InventarioJugador>().NumUsosLlaveCaballo++;
            }

            if (gameObject.CompareTag("PuertaPeon") &&
                FindObjectOfType<InventarioJugador>().LlavePeonEnElInventario)
            {
                DesbloquearPuerta();
                FindObjectOfType<InventarioJugador>().NumUsosLlavePeon++;
            }

            if (gameObject.CompareTag("PuertaRey") &&
                FindObjectOfType<InventarioJugador>().LlaveReyEnElInventario)
            {
                DesbloquearPuerta();
                FindObjectOfType<InventarioJugador>().NumUsosLlaveRey++;
            }

            if (gameObject.CompareTag("PuertaTorre") &&
                FindObjectOfType<InventarioJugador>().LlaveTorreEnElInventario)
            {
                DesbloquearPuerta();
                FindObjectOfType<InventarioJugador>().NumUsosLlaveTorre++;
            }

            InformarSobreElDesbloqueoDeLaPuerta();
        }
    }

    private void DesbloquearPuerta()
    {
        puertaDesbloqueada = true;
        puedeAbrirOCerrarPuerta = true;
        audioSource.clip = sonidoPuerta;
    }

    private void InformarSobreElDesbloqueoDeLaPuerta()
    {
        if (!puertaDesbloqueada)
        {
            FindObjectOfType<Mensajero>().Mensaje =
                "La puerta está cerrada. En la cerradura hay grabada" +
                " una pieza de ajedrez: ";
            if (tipoDeCerradura != "torre")
                FindObjectOfType<Mensajero>().Mensaje +=
                    "Un " + tipoDeCerradura + ".";
            else
                FindObjectOfType<Mensajero>().Mensaje +=
                    "Una " + tipoDeCerradura + ".";
        }
        else
        {
            FindObjectOfType<Mensajero>().Mensaje = 
                "Has desbloqueado la puerta con la llave " + tipoDeCerradura;
        }

        FindObjectOfType<Mensajero>().MostrarInterfazMensaje();
    }
}
