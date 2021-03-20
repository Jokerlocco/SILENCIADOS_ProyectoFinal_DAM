using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InspeccionDeDocumentos : MonoBehaviour
{
    private bool colisionando = false;

    [SerializeField] GameObject documentos; // Asignado en Unity
    [SerializeField] TMP_Text textoDelDocumento; // Asignado en Unity
    private bool documentoActivo = false;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador"))
            colisionando = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador"))
            colisionando = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interactuar") && colisionando 
            && !documentoActivo)
        {
            MostrarDocumento();
            MostrarTextoDelDocumento();
        }

        if (Input.GetButtonDown("Cerrar"))
            QuitarDocumento();
    }

    private void MostrarDocumento()
    {
        documentos.SetActive(true);
        documentoActivo = true;
        audioSource.Play();
        FindObjectOfType<Jugador>().PuedeMoverse = false;
    }

    private void QuitarDocumento()
    {
        documentos.SetActive(false);
        documentoActivo = false;
        textoDelDocumento.text = "";
        FindObjectOfType<Jugador>().PuedeMoverse = true;
    }

    private void MostrarTextoDelDocumento()
    {
        if (gameObject.CompareTag("InspeccionDocumentoPrueba"))
        {
            textoDelDocumento.text = "El Doctor Isaacs ha informado " +
                "del escape del paciente 0234567L. Debe de ser " +
                "encontrado de inmediato.";
        }
    }
}
