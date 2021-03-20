using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boton : MonoBehaviour
{
    [SerializeField] string accionDelBoton; // Asignado en Unity
    [SerializeField] Color colorBotonSinSeleccionar; // Asignado en Unity
    [SerializeField] Color colorBotonSeleccionado; // Asignado en Unity
    private Image imagen;
    public bool Seleccionado { get; set; }

    void Start()
    {
        imagen = GetComponent<Image>();
        imagen.color = colorBotonSinSeleccionar; 
    }

    private void Update()
    {
        CambiarColorDelBoton();
        RealizarAccionDelBoton();
    }

    private void CambiarColorDelBoton()
    {
        if (Seleccionado)
            imagen.color = colorBotonSeleccionado;
        else
            imagen.color = colorBotonSinSeleccionar;
    }

    private void RealizarAccionDelBoton()
    {
        if (Input.GetButtonDown("Interactuar") && Seleccionado)
        {
            if (transform.parent.CompareTag("BotonesMenuPrincipal"))
            {
                if (accionDelBoton == "Asylum")
                    SceneManager.LoadScene(accionDelBoton);
                else if (accionDelBoton == "SalirDelJuego")
                    Application.Quit();
            }

            if (transform.parent.CompareTag("BotonesPanelNumerico"))
            {
                if (accionDelBoton == "Numero1")
                    Debug.Log("Se ha pulsado el 1");
                if (accionDelBoton == "Numero2")
                    Debug.Log("Se ha pulsado el 2");
                if (accionDelBoton == "Numero3")
                    Debug.Log("Se ha pulsado el 3");
                if (accionDelBoton == "Numero4")
                    Debug.Log("Se ha pulsado el 4");
            }
        }
    }
}
