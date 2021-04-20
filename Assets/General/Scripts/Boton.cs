using UnityEngine;
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
                {
                    FindObjectOfType<InventarioJugador>().
                        InventarioJugadorDisponible = false;
                    CargadorDeEscenas.CargarEscena("Asilo");
                }
                else if (accionDelBoton == "SalirDelJuego")
                    Application.Quit();
            }

            if (transform.parent.CompareTag("BotonesMenuOpciones"))
            {
                if (accionDelBoton == "CargarEscenaMenuPrincipal")
                {
                    FindObjectOfType<InventarioJugador>().
                        InventarioJugadorDisponible = false;
                    CargadorDeEscenas.CargarEscena("MenuPrincipal");
                }
                else if (accionDelBoton == "SalirDelJuego")
                    Application.Quit();
            }

            if (transform.parent.CompareTag("BotonesPanelNumerico"))
            {
                CajaDeSeguridad cajaDeSeguridadActiva =
                    DeterminarCajaDeSeguridadActiva();

                if (cajaDeSeguridadActiva != null)
                {
                    if (accionDelBoton == "Numero1")
                        cajaDeSeguridadActiva.SeHaPulsadoUnBoton("1");
                    if (accionDelBoton == "Numero2")
                        cajaDeSeguridadActiva.SeHaPulsadoUnBoton("2");
                    if (accionDelBoton == "Numero3")
                        cajaDeSeguridadActiva.SeHaPulsadoUnBoton("3");
                    if (accionDelBoton == "btnOk")
                        cajaDeSeguridadActiva.SeHaPulsadoUnBoton("btnOk");
                    if (accionDelBoton == "Numero4")
                        cajaDeSeguridadActiva.SeHaPulsadoUnBoton("4");
                    if (accionDelBoton == "Numero5")
                        cajaDeSeguridadActiva.SeHaPulsadoUnBoton("5");
                    if (accionDelBoton == "Numero6")
                        cajaDeSeguridadActiva.SeHaPulsadoUnBoton("6");
                    if (accionDelBoton == "Numero0")
                        cajaDeSeguridadActiva.SeHaPulsadoUnBoton("0");
                    if (accionDelBoton == "Numero7")
                        cajaDeSeguridadActiva.SeHaPulsadoUnBoton("7");
                    if (accionDelBoton == "Numero8")
                        cajaDeSeguridadActiva.SeHaPulsadoUnBoton("8");
                    if (accionDelBoton == "Numero9")
                        cajaDeSeguridadActiva.SeHaPulsadoUnBoton("9");
                    if (accionDelBoton == "btnBorrar")
                        cajaDeSeguridadActiva.SeHaPulsadoUnBoton("btnBorrar");
                }
            }
        }
    }

    private CajaDeSeguridad DeterminarCajaDeSeguridadActiva()
    {
        CajaDeSeguridad cajaDeSeguridadActiva = null;

        CajaDeSeguridad[] cajasDeSeguridad = 
            FindObjectsOfType<CajaDeSeguridad>();

        foreach (CajaDeSeguridad caja in cajasDeSeguridad)
        {
            if (caja.isActiveAndEnabled)
            {
                cajaDeSeguridadActiva = caja;
                break;
            }   
        }

        return cajaDeSeguridadActiva;
    }
}
