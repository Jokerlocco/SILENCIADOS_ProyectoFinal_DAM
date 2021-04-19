using UnityEngine;

public class MenuOpciones : MonoBehaviour
{
    [SerializeField] GameObject fondoOscuroDelMenu = null; // Asignado en Unity
    [SerializeField] GameObject btnVolverAlMenuPrincipal = null; // Asignado en Unity
    [SerializeField] GameObject btnSalirDelJuego = null; // Asignado en Unity

    private bool menuOpcionesAbierto = false;

    private void Start()
    {
        if (menuOpcionesAbierto)
            CerrarMenuOpciones();
    }

    private void Update()
    {
        if (Input.GetButtonDown("BotonDeOpciones") && !menuOpcionesAbierto)
        {
            MostrarMenuOpciones();
        }

        if (Input.GetButtonDown("Cerrar") && menuOpcionesAbierto)
        {
            CerrarMenuOpciones();
        }
    }

    private void MostrarMenuOpciones()
    {
        FindObjectOfType<ControlDelJugador>().PuedeMoverse = false;

        fondoOscuroDelMenu.SetActive(true);
        btnVolverAlMenuPrincipal.SetActive(true);
        btnSalirDelJuego.SetActive(true);

        menuOpcionesAbierto = true;
    }

    private void CerrarMenuOpciones()
    {
        FindObjectOfType<ControlDelJugador>().PuedeMoverse = true;

        fondoOscuroDelMenu.SetActive(false);
        btnVolverAlMenuPrincipal.SetActive(false);
        btnSalirDelJuego.SetActive(false);

        menuOpcionesAbierto = false;
    }
}
