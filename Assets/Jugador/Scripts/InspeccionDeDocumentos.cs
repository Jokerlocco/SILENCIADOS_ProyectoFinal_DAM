using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InspeccionDeDocumentos : MonoBehaviour
{
    [SerializeField] GameObject documentos; // Asignado en Unity
    [SerializeField] TMP_Text textoDelDocumento; // Asignado en Unity
    private bool documentoActivo = false;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetButtonDown("Interactuar") && 
            other.gameObject.CompareTag("Jugador") && !documentoActivo)
        {
            documentoActivo = true;
            audioSource.Play();
            documentos.SetActive(true);

            // Documentos
            if (gameObject.CompareTag("InspeccionDocumentoPrueba"))
            {
                textoDelDocumento.text = "El Doctor Isaacs ha informado " +
                    "del escape del paciente 0234567L. Debe de ser " +
                    "encontrado de inmediato.";
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        documentoActivo = false;
        documentos.SetActive(false);
        textoDelDocumento.text = "";
    }
}
