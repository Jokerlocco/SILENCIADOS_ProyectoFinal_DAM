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
                    FindObjectOfType<PanelNumerico>().
                        SendMessage("SeHaPulsadoUnBoton", "1");
                if (accionDelBoton == "Numero2")
                    FindObjectOfType<PanelNumerico>().
                        SendMessage("SeHaPulsadoUnBoton", "2");
                if (accionDelBoton == "Numero3")
                    FindObjectOfType<PanelNumerico>().
                        SendMessage("SeHaPulsadoUnBoton", "3");
                if (accionDelBoton == "btnOk")
                    FindObjectOfType<PanelNumerico>().
                        SendMessage("SeHaPulsadoUnBoton", "btnOk");
                if (accionDelBoton == "Numero4")
                    FindObjectOfType<PanelNumerico>().
                        SendMessage("SeHaPulsadoUnBoton", "4");
                if (accionDelBoton == "Numero5")
                    FindObjectOfType<PanelNumerico>().
                        SendMessage("SeHaPulsadoUnBoton", "5");
                if (accionDelBoton == "Numero6")
                    FindObjectOfType<PanelNumerico>().
                        SendMessage("SeHaPulsadoUnBoton", "6");
                if (accionDelBoton == "Numero0")
                    FindObjectOfType<PanelNumerico>().
                        SendMessage("SeHaPulsadoUnBoton", "0");
                if (accionDelBoton == "Numero7")
                    FindObjectOfType<PanelNumerico>().
                        SendMessage("SeHaPulsadoUnBoton", "7");
                if (accionDelBoton == "Numero8")
                    FindObjectOfType<PanelNumerico>().
                        SendMessage("SeHaPulsadoUnBoton", "8");
                if (accionDelBoton == "Numero9")
                    FindObjectOfType<PanelNumerico>().
                        SendMessage("SeHaPulsadoUnBoton", "9");
                if (accionDelBoton == "btnBorrar")
                    FindObjectOfType<PanelNumerico>().
                        SendMessage("SeHaPulsadoUnBoton", "btnBorrar");
            }
        }
    }
}
