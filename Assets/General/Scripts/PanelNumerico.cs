using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PanelNumerico : MonoBehaviour
{
    [SerializeField] GameObject codigoPanel; // Asignado en Unity
    private TMP_Text textoCodigoPanel;
    private Animator animacionCodigo;

    private AudioSource audioSource;
    [SerializeField] AudioClip sonidoBoton; // Asignado en Unity
    [SerializeField] AudioClip sonidoBotonNoFuncional; // Asignado en Unity
    [SerializeField] AudioClip sonidoCodigoCajaDeSeguridadCorrecto; // Asignado en Unity
    [SerializeField] AudioClip sonidoCodigoCajaDeSeguridadIncorrecto; // Asignado en Unity

    private string codigoCajaDeSeguridadSecretaria = "6427";

    private void Start()
    {
        textoCodigoPanel = codigoPanel.GetComponent<TMP_Text>();

        animacionCodigo = codigoPanel.GetComponent<Animator>();
        animacionCodigo.SetBool("sinAnimacion", true);

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (gameObject.activeSelf)
            FindObjectOfType<Jugador>().PuedeMoverse = false;

        if (Input.GetButtonDown("Cerrar"))
            CerrarPanelNumerico();
    }

    private void CerrarPanelNumerico()
    {
        FindObjectOfType<Jugador>().PuedeMoverse = true;
        textoCodigoPanel.text = "";
        animacionCodigo.SetBool("sinAnimacion", true);
        gameObject.SetActive(false);
    }

    private void DesactivarScript()
    {
        enabled = false;
    }

    private void SeHaPulsadoUnBoton(string botonPulsado) // Es llamado en la clase "Boton"
    {
        if (botonPulsado == "btnOk")
        {
            if (textoCodigoPanel.text == codigoCajaDeSeguridadSecretaria)
            {
                ReproducirSonidoCodigoCajaDeSeguridadCorrecto();
                EstablecerAnimacionCodigoCorrecto();
                StartCoroutine(EsperarAntesDeCerrar());
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
}
