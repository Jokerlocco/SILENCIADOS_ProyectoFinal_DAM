using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteraccionPuerta : MonoBehaviour
{
    private Animator animacion;

    [SerializeField] private bool interactuandoConLaPuerta = false;
    private bool puertaAbierta = false;

    void Start()
    {
        animacion = GetComponent<Animator>();
    }

    void Update()
    {
        TocarElPomoDeLaPuerta();
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

    private void TocarElPomoDeLaPuerta()
    {
        if (Input.GetButtonDown("Interactuar") &&
            interactuandoConLaPuerta)
        {
            // Alternamos el booleano en cada entrada
            puertaAbierta = !puertaAbierta;
        }

        if(puertaAbierta)
        {
            animacion.SetBool("abierta", true);
        }
        else
        {
            animacion.SetBool("abierta", false);
        }
    }
}
