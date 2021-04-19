using UnityEngine;

public class InspeccionDeElementos : MonoBehaviour
{
    private bool colisionando = false;

    private AudioSource audioSource;

    void Start()
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
            if (FindObjectOfType<Mensajero>().InterfazMensajeActiva)
                FindObjectOfType<Mensajero>().OcultarInterfazMensaje();
        }
    }

    private void ReproducirSonidoElemento()
    {
        if (audioSource != null)
            audioSource.Play();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interactuar") &&
            colisionando && 
            !FindObjectOfType<Mensajero>().InterfazMensajeActiva)
        {
            EstablecerMensajeOTerminar();
        }
    }

    private void EstablecerMensajeOTerminar()
    {
        bool mostrarMensaje = false;

        if (gameObject.CompareTag("InspeccionPuertaPrincipal"))
        {
            FindObjectOfType<Mensajero>().Mensaje = 
                "La puerta principal está bloqueada. " +
                "Debo buscar otra forma de salir.";
            ReproducirSonidoElemento();
            mostrarMensaje = true;
        }

        if (gameObject.CompareTag("InspeccionAscensor"))
        {
            FindObjectOfType<Mensajero>().Mensaje =
                "El ascensor está destrozado. No se puede utilizar.";
            mostrarMensaje = true;
        }

        if (gameObject.CompareTag("InspeccionEstatua") && (
            !FindObjectOfType<InventarioJugador>().GrifoMaderaEnElInventario ||
            !FindObjectOfType<InventarioJugador>().GrifoMarmolEnElInventario ||
            !FindObjectOfType<InventarioJugador>().GrifoBronceEnElInventario))
        {
            FindObjectOfType<Mensajero>().Mensaje =
                "La estatua tiene tres orificios vacíos. " +
                "Es cómo si se tuviera que insertar algo.";
            mostrarMensaje = true;
        }
        
        if (gameObject.CompareTag("Proyector") && 
            !FindObjectOfType<InventarioJugador>().BombillaEnElInventario)
        {
            FindObjectOfType<Mensajero>().Mensaje =
                "El proyector parece funcionar, pero le falta la bombilla.";
            mostrarMensaje = true;
        }
            
        if (gameObject.CompareTag("FuegoEnLaCocina") && 
            !FindObjectOfType<InventarioJugador>().ExtintorEnElInventario)
        {
            FindObjectOfType<Mensajero>().Mensaje = "Hay un escape... " + 
                "Será mejor no acercarse más.";
            mostrarMensaje = true;
        }
        
        if (gameObject.CompareTag("GasEnSalaDeMaquinas") &&
            !FindObjectOfType<InventarioJugador>().TuboCurvoConValvulaEnElInventario)
        {
            FindObjectOfType<Mensajero>().Mensaje =
                "Hay un escape de gas y no me deja pasar.";
            mostrarMensaje = true;
        }

        if (gameObject.CompareTag("CompartimientoDelExtintor") &&
            !FindObjectOfType<InventarioJugador>().GanzuaEnElInventario)
        {
            FindObjectOfType<Mensajero>().Mensaje =
                "Es un extintor, pero el compartimiento está cerrado. " + 
                "Si tuviera la herramienta adecuada creo que podría abrirlo.";
            mostrarMensaje = true;
        }
        
        if (gameObject.CompareTag("ContenedorBiologico") &&
            (!FindObjectOfType<InventarioJugador>().AcetonaEnElInventario ||
            !FindObjectOfType<InventarioJugador>().EterEnElInventario ||
            !FindObjectOfType<InventarioJugador>().VinagreEnElInventario))
        {
            FindObjectOfType<Mensajero>().Mensaje =
                "Si tuviese los componentes necesarios podría " + 
                "crear un potente disolvente.";
            mostrarMensaje = true;
        }
        
        if (gameObject.CompareTag("SiliconaLlaveAlfil") &&
            !FindObjectOfType<InventarioJugador>().DisolventeDeSiliconaEnElInventario)
        {
            FindObjectOfType<Mensajero>().Mensaje = 
                "Es una llave en forma de alfil, pero no puedo cogerla porque " + 
                "está pegada con silicona...";
            mostrarMensaje = true;
        }
        
        if (gameObject.CompareTag("MotorHidraulico") &&
            !FindObjectOfType<InventarioJugador>().LlaveInglesaEnElInventario)
        {
            FindObjectOfType<Mensajero>().Mensaje = 
                "Es el motor hidraúlico del asilo. " + 
                "Creo que si lo arreglo, los grifos de los lavabos funcionarán, " + 
                "pero necesito una llave inglesa.";
            mostrarMensaje = true;
        }
        
        if (gameObject.CompareTag("GrifoDeLavabo") &&
            !FindObjectOfType<InventarioJugador>().JarronEnElInventario &&
            !FindObjectOfType<EstadoDelJuego>().MotorHidraulicoArreglado)
        {
            FindObjectOfType<Mensajero>().Mensaje = "El grifo no da agua...";
            mostrarMensaje = true;
        }
        if (gameObject.CompareTag("GrifoDeLavabo") &&
            FindObjectOfType<InventarioJugador>().JarronEnElInventario &&
            !FindObjectOfType<EstadoDelJuego>().MotorHidraulicoArreglado)
        {
            FindObjectOfType<Mensajero>().Mensaje = 
                "Podría llenar el jarrón con agua, pero el grifo no da. " + 
                "Debo solucionarlo...";
            mostrarMensaje = true;
        }
        
        if (gameObject.CompareTag("CodigoSalaObservacionEnHRA") &&
            !FindObjectOfType<InventarioJugador>().JarronConAguaEnElInventario)
        {
            FindObjectOfType<Mensajero>().Mensaje =
                "Hay una mancha en la pared, parece que está ocultando algo. " +
                "Su textura es muy extraña, pero creo que si arrojase agua," +
                " podría eliminarla.";
            mostrarMensaje = true;
        }
        
        if (gameObject.CompareTag("CajaDeSeguridadIdentificacion"))
        {
            FindObjectOfType<Mensajero>().Mensaje = 
                "Es una caja de seguridad que require una tarjeta de " +
                "identificación específica.";
            mostrarMensaje = true;
        }


        if (mostrarMensaje)
            MostrarMensaje();
        else
            DesactivarScript();
    }

    private void MostrarMensaje()
    {
        FindObjectOfType<Mensajero>().MostrarInterfazMensaje();
    }

    private void DesactivarScript()
    {
        enabled = false;
    }
}
