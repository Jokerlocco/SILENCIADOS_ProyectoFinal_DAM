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
                if (accionDelBoton == "CargarEscenaAsilo")
                    //SceneManager.LoadScene("Asilo");
                    CargadorDeEscenas.CargarEscena("Asilo");
                else if (accionDelBoton == "SalirDelJuego")
                    Application.Quit();
            }

            if (transform.parent.CompareTag("BotonesPanelNumerico"))
            {
                if (accionDelBoton == "Numero1")
                    FindObjectOfType<CajaDeSeguridad>().
                        SendMessage("SeHaPulsadoUnBoton", "1");
                if (accionDelBoton == "Numero2")
                    FindObjectOfType<CajaDeSeguridad>().
                        SendMessage("SeHaPulsadoUnBoton", "2");
                if (accionDelBoton == "Numero3")
                    FindObjectOfType<CajaDeSeguridad>().
                        SendMessage("SeHaPulsadoUnBoton", "3");
                if (accionDelBoton == "btnOk")
                    FindObjectOfType<CajaDeSeguridad>().
                        SendMessage("SeHaPulsadoUnBoton", "btnOk");
                if (accionDelBoton == "Numero4")
                    FindObjectOfType<CajaDeSeguridad>().
                        SendMessage("SeHaPulsadoUnBoton", "4");
                if (accionDelBoton == "Numero5")
                    FindObjectOfType<CajaDeSeguridad>().
                        SendMessage("SeHaPulsadoUnBoton", "5");
                if (accionDelBoton == "Numero6")
                    FindObjectOfType<CajaDeSeguridad>().
                        SendMessage("SeHaPulsadoUnBoton", "6");
                if (accionDelBoton == "Numero0")
                    FindObjectOfType<CajaDeSeguridad>().
                        SendMessage("SeHaPulsadoUnBoton", "0");
                if (accionDelBoton == "Numero7")
                    FindObjectOfType<CajaDeSeguridad>().
                        SendMessage("SeHaPulsadoUnBoton", "7");
                if (accionDelBoton == "Numero8")
                    FindObjectOfType<CajaDeSeguridad>().
                        SendMessage("SeHaPulsadoUnBoton", "8");
                if (accionDelBoton == "Numero9")
                    FindObjectOfType<CajaDeSeguridad>().
                        SendMessage("SeHaPulsadoUnBoton", "9");
                if (accionDelBoton == "btnBorrar")
                    FindObjectOfType<CajaDeSeguridad>().
                        SendMessage("SeHaPulsadoUnBoton", "btnBorrar");
            }
        }
    }
}
