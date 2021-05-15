using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class MenuOpciones : MonoBehaviour
{
    [SerializeField] GameObject fondoOscuroDelMenu = null; // Asignado en Unity
    [SerializeField] GameObject botonesManager = null; // Asignado en Unity
    [SerializeField] GameObject btnVolverAlMenuPrincipal = null; // Asignado en Unity
    [SerializeField] GameObject btnSalirDelJuego = null; // Asignado en Unity

    private bool menuOpcionesAbierto = false;

    // Para control alternativo
    private float walkSpeedInicial = 0f;
    private float runSpeedInicial = 0f;

    private void Start()
    {
        if (!FindObjectOfType<ControlDelJugador>())
        {
            walkSpeedInicial = 
                FindObjectOfType<FirstPersonController>().GetWalkSpeed();
            runSpeedInicial =
                FindObjectOfType<FirstPersonController>().GetRunSpeed();
        }
        
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
        if (FindObjectOfType<ControlDelJugador>())
        {
            FindObjectOfType<ControlDelJugador>().PuedeMoverse = false;
        }
        else
        {
            FindObjectOfType<FirstPersonController>().SetWalkSpeed(0f);
            FindObjectOfType<FirstPersonController>().SetRunSpeed(0f);
        }

        fondoOscuroDelMenu.SetActive(true);
        botonesManager.SetActive(true);
        btnVolverAlMenuPrincipal.SetActive(true);
        btnSalirDelJuego.SetActive(true);

        menuOpcionesAbierto = true;

        PausarJuego();
    }

    private void CerrarMenuOpciones()
    {
        if (FindObjectOfType<ControlDelJugador>())
        {
            FindObjectOfType<ControlDelJugador>().PuedeMoverse = true;
        }
        else
        {
            FindObjectOfType<FirstPersonController>()
                .SetWalkSpeed(walkSpeedInicial);
            FindObjectOfType<FirstPersonController>()
                .SetRunSpeed(runSpeedInicial);
        }

        fondoOscuroDelMenu.SetActive(false);
        botonesManager.SetActive(false);
        btnVolverAlMenuPrincipal.SetActive(false);
        btnSalirDelJuego.SetActive(false);

        menuOpcionesAbierto = false;

        ReanudarJuego();
    }

    void PausarJuego()
    {
        Time.timeScale = 0;
    }

    void ReanudarJuego()
    {
        Time.timeScale = 1;
    }
}
