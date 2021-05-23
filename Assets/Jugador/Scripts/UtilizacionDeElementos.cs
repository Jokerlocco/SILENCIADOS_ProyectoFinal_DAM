using System.Collections;
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

            if (gameObject.CompareTag("BaldosaSecreta"))
            {
                StartCoroutine(QuitarBaldosaSecretaYMostrarLlaveTorre(2f));
                elementoUtilizado = true;
            }

            if (gameObject.CompareTag("CompartimientoDelExtintor") &&
                FindObjectOfType<InventarioJugador>().GanzuaEnElInventario)
            {
                StartCoroutine(UsarGanzuaEnElCompartimientoDelExtintor(2f));
                elementoUtilizado = true;
            }

            if (gameObject.CompareTag("FuegoEnLaCocina") &&
                 FindObjectOfType<InventarioJugador>().ExtintorEnElInventario)
            {
                StartCoroutine(AgagarFuegoCocinaConExtintor(2f));
                elementoUtilizado = true;
            }

            if (gameObject.CompareTag("ContenedorBiologico") &&
                (FindObjectOfType<InventarioJugador>().AcetonaEnElInventario &&
                FindObjectOfType<InventarioJugador>().EterEnElInventario &&
                FindObjectOfType<InventarioJugador>().VinagreEnElInventario))
            {
                StartCoroutine(
                    CombinarComponentesYCrearDisolventeDeSilicona(2f));
                elementoUtilizado = true;
            }

            if (gameObject.CompareTag("SiliconaLlaveAlfil") &&
                 FindObjectOfType<InventarioJugador>().
                 DisolventeDeSiliconaEnElInventario)
            {
                StartCoroutine(DisolverSiliconaLlaveAlfil(2f));
                elementoUtilizado = true;
            }

            if (gameObject.CompareTag("MotorHidraulico") &&
                 FindObjectOfType<InventarioJugador>().
                 LlaveInglesaEnElInventario)
            {
                StartCoroutine(ArreglarMotorHidraulico(2f));
                elementoUtilizado = true;
            }

            if (gameObject.CompareTag("GrifoDeLavabo") &&
                 FindObjectOfType<InventarioJugador>().JarronEnElInventario &&
                 FindObjectOfType<EstadoDelJuego>().MotorHidraulicoArreglado)
            {
                StartCoroutine(LlenarJarronConAguaDelGrifo(2f));
                elementoUtilizado = true;
            }

            if (gameObject.CompareTag("CodigoSalaObservacionEnHRA") &&
                 FindObjectOfType<InventarioJugador>().JarronConAguaEnElInventario)
            {
                StartCoroutine(EliminarManchaDeLaPared(2f));
                elementoUtilizado = true;
            }

            if (gameObject.CompareTag("CajaDeSeguridadIdentificacion") &&
                !FindObjectOfType<InventarioJugador>().
                    TarjetaDeIdentificacionAlynSEnElInventario &&
                !FindObjectOfType<InventarioJugador>().
                    TarjetaDeIdentificacionMorganSEnElInventario &&
                !FindObjectOfType<InventarioJugador>().
                TarjetaDeIdentificacionRKarlheinzEnElInventario)
            {
                FindObjectOfType<Mensajero>().Mensaje =
                "Es una caja de seguridad que requiere una tarjeta de " +
                "identificación específica.";
                FindObjectOfType<Mensajero>().MostrarInterfazMensaje();
            }
            else if (gameObject.CompareTag("CajaDeSeguridadIdentificacion") &&
                FindObjectOfType<InventarioJugador>().
                    TarjetaDeIdentificacionRKarlheinzEnElInventario)
            {
                UtilizarTarjetaCorrectaEnLaCajaDeSeguridadDeIdentificacion();
                elementoUtilizado = true;
            }
            else if (gameObject.CompareTag("CajaDeSeguridadIdentificacion") &&
                FindObjectOfType<InventarioJugador>().
                    TarjetaDeIdentificacionAlynSEnElInventario)
            {
                FindObjectOfType<Mensajero>().Mensaje =
                "Es una caja de seguridad que requiere una tarjeta de " +
                "identificación específica, y esta no me sirve...";
                FindObjectOfType<Mensajero>().MostrarInterfazMensaje();
                FindObjectOfType<InventarioJugador>().
                    TarjetaDeIdentificacionAlynSEnElInventario = false;
            }
            else if (gameObject.CompareTag("CajaDeSeguridadIdentificacion") &&
                FindObjectOfType<InventarioJugador>().
                    TarjetaDeIdentificacionMorganSEnElInventario)
            {
                FindObjectOfType<Mensajero>().Mensaje =
                "Es una caja de seguridad que requiere una tarjeta de " +
                "identificación específica, y esta no me sirve...";
                FindObjectOfType<Mensajero>().MostrarInterfazMensaje();
                FindObjectOfType<InventarioJugador>().
                    TarjetaDeIdentificacionMorganSEnElInventario = false;
            }

            if (gameObject.CompareTag("GasEnSalaDeMaquinas") &&
                 FindObjectOfType<InventarioJugador>().
                    TuboCurvoConValvulaEnElInventario &&
                 FindObjectOfType<InventarioJugador>().
                    LlaveInglesaEnElInventario)
            {
                StartCoroutine(ArreglarEscapeDeGas(2f));
                elementoUtilizado = true;
            }

            if (gameObject.CompareTag("InspeccionEstatua") &&
                 FindObjectOfType<InventarioJugador>().
                    GrifoBronceEnElInventario &&
                 FindObjectOfType<InventarioJugador>().
                    GrifoMaderaEnElInventario &&
                FindObjectOfType<InventarioJugador>().
                    GrifoMarmolEnElInventario)
            {
                StartCoroutine(InsertarGrifosEnLaEstatua(9f));
                elementoUtilizado = true;
            }

            if (gameObject.CompareTag("EscalerasAlLaboratorio") && 
                FindObjectOfType<EstadoDelJuego>().EstatuaMovida)
            {
                StartCoroutine(BajarEscalerasAlLaboratorio(8f));
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

        if (gameObject.CompareTag("GrifoDeLavabo")) // Si hay que establecer varios objetos en el mapa (grifos de lavabos por ejemplo)...
        {
            GameObject[] grifos =
                GameObject.FindGameObjectsWithTag("GrifoDeLavabo");

            foreach (GameObject grifo in grifos)
            {
                grifo.gameObject.tag = "Utilizado";
            }
        }
        else
        {
            gameObject.tag = "Utilizado";
        }

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
        llaveTorre.GetComponent<BoxCollider>().enabled = true;
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

    private IEnumerator CombinarComponentesYCrearDisolventeDeSilicona(
    float segundosDeLaAnimacion)
    {
        EstablecerAnimacionPantallaNegra(segundosDeLaAnimacion);
        yield return new WaitForSecondsRealtime(segundosDeLaAnimacion);

        FindObjectOfType<InventarioJugador>().VinagreEnElInventario = false;
        FindObjectOfType<InventarioJugador>().EterEnElInventario = false;
        FindObjectOfType<InventarioJugador>().AcetonaEnElInventario = false;
        FindObjectOfType<InventarioJugador>()
            .DisolventeDeSiliconaEnElInventario = true;

        FindObjectOfType<Mensajero>().Mensaje =
            "Usando la acetona, el éter, y el vinagre en esta cosa, " +
            "he podido crear un disolvente para la silicona.";
        FindObjectOfType<Mensajero>().MostrarInterfazMensaje();
    }

    private IEnumerator DisolverSiliconaLlaveAlfil(float segundosDeLaAnimacion)
    {
        EstablecerAnimacionPantallaNegra(segundosDeLaAnimacion);
        yield return new WaitForSecondsRealtime(segundosDeLaAnimacion);
        audioSource.Stop();

        MeshRenderer renderSiliconaLlaveAlfil =
            gameObject.GetComponent<MeshRenderer>();
        renderSiliconaLlaveAlfil.enabled = false;

        FindObjectOfType<InventarioJugador>()
            .DisolventeDeSiliconaEnElInventario = false;

        FindObjectOfType<Mensajero>().Mensaje =
            "He usado el disolvente en la silicona y " +
            "se ha disuelto bastante rápido.";
        FindObjectOfType<Mensajero>().MostrarInterfazMensaje();
    }

    private IEnumerator ArreglarMotorHidraulico(float segundosDeLaAnimacion)
    {
        EstablecerAnimacionPantallaNegra(segundosDeLaAnimacion);
        yield return new WaitForSecondsRealtime(segundosDeLaAnimacion);

        FindObjectOfType<EstadoDelJuego>().MotorHidraulicoArreglado = true;

        FindObjectOfType<Mensajero>().Mensaje =
            "He arreglado el motor hidraúlico con la llave inglesa. " +
            "Ahora los grifos de los lavabos deberían funcionar.";
        FindObjectOfType<Mensajero>().MostrarInterfazMensaje();
    }

    private IEnumerator LlenarJarronConAguaDelGrifo(float segundosDeLaAnimacion)
    {
        EstablecerAnimacionPantallaNegra(segundosDeLaAnimacion);
        yield return new WaitForSecondsRealtime(segundosDeLaAnimacion);

        FindObjectOfType<InventarioJugador>()
            .JarronEnElInventario = false;
        FindObjectOfType<InventarioJugador>()
            .JarronConAguaEnElInventario = true;

        FindObjectOfType<Mensajero>().Mensaje =
            "He llenado el jarrón con agua del grifo.";
        FindObjectOfType<Mensajero>().MostrarInterfazMensaje();
    }

    private IEnumerator EliminarManchaDeLaPared(float segundosDeLaAnimacion)
    {
        EstablecerAnimacionPantallaNegra(segundosDeLaAnimacion);
        yield return new WaitForSecondsRealtime(segundosDeLaAnimacion);

        // Desactivamos las manchas
        Renderer mancha1Renderer =
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        mancha1Renderer.enabled = false;
        Renderer mancha2Renderer =
            gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>();
        mancha2Renderer.enabled = false;

        FindObjectOfType<InventarioJugador>()
            .JarronConAguaEnElInventario = false;

        FindObjectOfType<Mensajero>().Mensaje =
            "He eliminado la mancha con el jarrón con agua. " +
            "Hay una especie de código grabado en la pared.";
        FindObjectOfType<Mensajero>().MostrarInterfazMensaje();
    }

    private void UtilizarTarjetaCorrectaEnLaCajaDeSeguridadDeIdentificacion()
    {
        Animator animacionTapaderaDeLaCaja =
            GetComponent<Animator>();
        animacionTapaderaDeLaCaja.SetBool(
            "seHaIntroducidoElCodigoCorrecto", true);

        FindObjectOfType<Mensajero>().Mensaje =
            "He utilizado la tarjeta de identificación del " + 
            "doctor Karlheinz en la caja de seguridad.";
        FindObjectOfType<Mensajero>().MostrarInterfazMensaje();

        FindObjectOfType<InventarioJugador>().
            TarjetaDeIdentificacionAlynSEnElInventario = false;
        FindObjectOfType<InventarioJugador>().
            TarjetaDeIdentificacionMorganSEnElInventario = false;
        FindObjectOfType<InventarioJugador>().
            TarjetaDeIdentificacionRKarlheinzEnElInventario = false;

        FindObjectOfType<EstadoDelJuego>().
            CajaDeSeguridadDeIdentificacionAbierta = true;
    }

    private IEnumerator ArreglarEscapeDeGas(float segundosDeLaAnimacion)
    {
        EstablecerAnimacionPantallaNegra(segundosDeLaAnimacion);
        yield return new WaitForSecondsRealtime(segundosDeLaAnimacion);

        // Mostramos el tubo 
        GameObject tubo = GameObject.
            FindGameObjectWithTag("TuboCurvoConValvulaEnSMaquinas").gameObject;
        tubo.GetComponent<MeshRenderer>().enabled = true;
        // Y la válvula
        tubo.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;

        // Ocultamos el gas:
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        // Desactivamos el sonido del gas:
        gameObject.transform.GetChild(1).gameObject.
            GetComponent<AudioSource>().Stop();
        // Desactivamos el bloqueo de paso:
        gameObject.transform.GetChild(2).gameObject.
            GetComponent<MeshCollider>().enabled = false;

        FindObjectOfType<InventarioJugador>()
            .TuboCurvoConValvulaEnElInventario = false;
        FindObjectOfType<InventarioJugador>()
            .LlaveInglesaEnElInventario = false;

        FindObjectOfType<Mensajero>().Mensaje =
            "He arreglado el escape de gas usando la llave inglesa y" +
            " el tubo con válvula.";
        FindObjectOfType<Mensajero>().MostrarInterfazMensaje();
    }

    private IEnumerator InsertarGrifosEnLaEstatua(float segundosDeLaAnimacion)
    {
        FindObjectOfType<ControlDelJugador>().PuedeMoverse = false;

        EstablecerAnimacionPantallaNegra(segundosDeLaAnimacion);

        // Sonidos de colocación de grifos
        yield return new WaitForSecondsRealtime(1f);
        gameObject.transform.GetChild(0).gameObject.
            GetComponent<AudioSource>().Play();
        yield return new WaitForSecondsRealtime(1f);
        gameObject.transform.GetChild(0).gameObject.
            GetComponent<AudioSource>().Play();
        yield return new WaitForSecondsRealtime(1f);
        gameObject.transform.GetChild(0).gameObject.
            GetComponent<AudioSource>().Play();
        yield return new WaitForSecondsRealtime(1f);

        // Sonido de estatua moviendose
        gameObject.transform.GetChild(1).gameObject.
            GetComponent<AudioSource>().Play();

        // Ocultamos la estatua
        gameObject.transform.parent.
            GetComponent<MeshRenderer>().enabled = false;
        gameObject.transform.parent.
            GetComponent<MeshCollider>().enabled = false;

        // Mostramos la estatua movida
        GameObject estatuaMovida = 
            GameObject.FindGameObjectWithTag("EstatuaMovida").gameObject;
        estatuaMovida.GetComponent<MeshRenderer>().enabled = true;
        estatuaMovida.GetComponent<MeshCollider>().enabled = true;

        FindObjectOfType<InventarioJugador>()
            .GrifoBronceEnElInventario = false;
        FindObjectOfType<InventarioJugador>()
            .GrifoMaderaEnElInventario = false;
        FindObjectOfType<InventarioJugador>()
            .GrifoMarmolEnElInventario = false;

        FindObjectOfType<EstadoDelJuego>().EstatuaMovida = true;

        yield return new WaitForSecondsRealtime(4.5f);

        FindObjectOfType<Mensajero>().Mensaje =
            "He insertado los tres grifos, y la estatua se ha movido, " +
            "revelando un agujero con unas escaleras secretas...";
        FindObjectOfType<Mensajero>().MostrarInterfazMensaje();

        yield return new WaitForSecondsRealtime(6f);
        FindObjectOfType<ControlDelJugador>().PuedeMoverse = true;
    }

    private IEnumerator BajarEscalerasAlLaboratorio(float segundosDeLaAnimacion)
    {
        EstablecerAnimacionPantallaNegra(segundosDeLaAnimacion);
        yield return new WaitForSecondsRealtime(segundosDeLaAnimacion);

        CargadorDeEscenas.CargarEscenaDirectamente("TransicionAlOtroMundo");
    }

}