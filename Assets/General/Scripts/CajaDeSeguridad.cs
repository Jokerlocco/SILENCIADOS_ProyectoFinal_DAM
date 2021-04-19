using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CajaDeSeguridad : MonoBehaviour
{
    [SerializeField] GameObject panelNumerico; // Asignado en Unity
    [SerializeField] GameObject codigoPanel; // Asignado en Unity
    private TMP_Text textoCodigoPanel;
    private Animator animacionCodigo;

    private Animator animacionTapaderaDeLaCaja;

    private AudioSource audioSource;
    [SerializeField] AudioClip sonidoBoton; // Asignado en Unity
    [SerializeField] AudioClip sonidoBotonNoFuncional; // Asignado en Unity
    [SerializeField] AudioClip sonidoCodigoCajaDeSeguridadCorrecto; // Asignado en Unity
    [SerializeField] AudioClip sonidoCodigoCajaDeSeguridadIncorrecto; // Asignado en Unity

    private string codigoCajaDeSeguridadSecretaria = "6427";
    private string codigoCajaDeSeguridadSObservacion = "0634";
    private string codigoCajaDeSeguridadDMJ = "1625";
    private string codigoCajaDeSeguridadSMaquinas = "1306";

    private void Start()
    {
        textoCodigoPanel = codigoPanel.GetComponent<TMP_Text>();
        animacionCodigo = codigoPanel.GetComponent<Animator>();

        animacionTapaderaDeLaCaja = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cerrar") && panelNumerico.activeSelf)
            CerrarPanelNumerico();
    }

    private void AbrirPanelNumerico() // Es llamado en la clase "InspeccionDeCajaDeSeguridad"
    {
        FindObjectOfType<ControlDelJugador>().PuedeMoverse = false;
        panelNumerico.SetActive(true);
    }

    private void CerrarPanelNumerico()
    {
        FindObjectOfType<ControlDelJugador>().PuedeMoverse = true;
        textoCodigoPanel.text = "";
        animacionCodigo.SetBool("sinAnimacion", true);
        panelNumerico.SetActive(false);
    }

    private void DesactivarScript()
    {
        enabled = false;
    }

    public void SeHaPulsadoUnBoton(string botonPulsado) // Es llamado en la clase "Boton"
    {
        if (botonPulsado == "btnOk")
        {
            if (gameObject.CompareTag("CajaDeSeguridadSecretaria") && 
                textoCodigoPanel.text == codigoCajaDeSeguridadSecretaria)
            {
                AbrirCajaDeSeguridadPorCodigoCorrecto();
                FindObjectOfType<EstadoDelJuego>().
                    CajaDeSeguridadSecretariaAbierta = true;
            }
            else if (gameObject.CompareTag("CajaDeSeguridadSObservacion") &&
                textoCodigoPanel.text == codigoCajaDeSeguridadSObservacion)
            {
                AbrirCajaDeSeguridadPorCodigoCorrecto();
                FindObjectOfType<EstadoDelJuego>().
                    CajaDeSeguridadSObservacionAbierta = true;
            }
            else if (gameObject.CompareTag("CajaDeSeguridadDMJ") &&
                textoCodigoPanel.text == codigoCajaDeSeguridadDMJ)
            {
                AbrirCajaDeSeguridadPorCodigoCorrecto();
                FindObjectOfType<EstadoDelJuego>().
                    CajaDeSeguridadDMJAbierta = true;
            }
            else if (gameObject.CompareTag("CajaDeSeguridadSMaquinas") &&
                textoCodigoPanel.text == codigoCajaDeSeguridadSMaquinas)
            {
                AbrirCajaDeSeguridadPorCodigoCorrecto();
                FindObjectOfType<EstadoDelJuego>().
                    CajaDeSeguridadSMaquinasAbierta = true;
            }
            else
            {
                ReproducirSonidoCodigoCajaDeSeguridadIncorrecto();
                EstablecerAnimacionCodigoIncorrecto();
                StartCoroutine(EsperarAntesDeCerrar());
            }
        }
        else if (botonPulsado == "btnBorrar")
        {
            ReproducirSonidoBoton();
            textoCodigoPanel.text = "";
        }
        else
        {
            if (textoCodigoPanel.text.Length <= 3)
            {
                ReproducirSonidoBoton();
                textoCodigoPanel.text += botonPulsado;
            }
            else
                ReproducirSonidoBotonNoFuncional();
        }
    }

    private IEnumerator EsperarAntesDeCerrar()
    {
        yield return new WaitForSecondsRealtime(1);
        CerrarPanelNumerico();
        DesactivarScript();
    }

    private void ReproducirSonidoBoton()
    {
        audioSource.clip = sonidoBoton;
        audioSource.PlayOneShot(audioSource.clip);
    }

    private void ReproducirSonidoBotonNoFuncional()
    {
        audioSource.clip = sonidoBotonNoFuncional;
        audioSource.PlayOneShot(audioSource.clip);
    }

    private void ReproducirSonidoCodigoCajaDeSeguridadCorrecto()
    {
        audioSource.clip = sonidoCodigoCajaDeSeguridadCorrecto;
        audioSource.PlayOneShot(audioSource.clip);
    }

    private void ReproducirSonidoCodigoCajaDeSeguridadIncorrecto()
    {
        audioSource.clip = sonidoCodigoCajaDeSeguridadIncorrecto;
        audioSource.PlayOneShot(audioSource.clip);
    }

    private void EstablecerAnimacionCodigoCorrecto()
    {
        animacionCodigo.SetBool("sinAnimacion", false);
        animacionCodigo.SetBool("codigoCorrecto", true);
        animacionCodigo.SetBool("seHaPulsadoOk", true);
    }

    private void EstablecerAnimacionCodigoIncorrecto()
    {
        animacionCodigo.SetBool("sinAnimacion", false);
        animacionCodigo.SetBool("codigoCorrecto", false);
        animacionCodigo.SetBool("seHaPulsadoOk", true);
    }

    private void EstablecerAnimacionAbrirCaja()
    {
        animacionTapaderaDeLaCaja.SetBool(
            "seHaIntroducidoElCodigoCorrecto", true);
    }

    private void AbrirCajaDeSeguridadPorCodigoCorrecto()
    {
        ReproducirSonidoCodigoCajaDeSeguridadCorrecto();
        EstablecerAnimacionCodigoCorrecto();
        EstablecerAnimacionAbrirCaja();
        gameObject.tag = "Utilizado";
        StartCoroutine(EsperarAntesDeCerrar());
    }
}
