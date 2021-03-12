using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventarioJugador : MonoBehaviour
{
    [SerializeField] TMP_Text contenidoInventario; // Asignado en Unity

    // Animación inventario
    [SerializeField] GameObject interfazInventario; // Asignado en Unity
    [SerializeField] GameObject fondoOscuroTraslucidoInventario; // Asignado en Unity
    private Animator animacionDeLaInterfaz;
    private bool inventarioAbierto = false;

    // Objetos
    private string nombreGrifoBronce = "Grifo de bronce";
    public bool GrifoBronceEnElInventario { get; set; }
    private string nombreGrifoMarmol = "Grifo de mármol";
    public bool GrifoMarmolEnElInventario { get; set; }
    private string nombreGrifoMadera = "Grifo de madera";
    public bool GrifoMaderaEnElInventario { get; set; }

    private void ImplementarPatronSingleton()
    {
        int numeroDeInventariosInstanciados =
            FindObjectsOfType<InventarioJugador>().Length;

        if (numeroDeInventariosInstanciados > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }

    private void Awake()
    {
        ImplementarPatronSingleton();
    }

    private void Start()
    {
        animacionDeLaInterfaz = interfazInventario.GetComponent<Animator>();

        contenidoInventario.text = "";
        GrifoBronceEnElInventario = false;
        GrifoMarmolEnElInventario = false;
    }

    private void Update()
    {
        MostrarObjetosDelInventario();
        AbrirOCerrarInventario();
    }

    private void AbrirOCerrarInventario()
    {
        if (Input.GetButtonDown("BotonInventario"))
        {
            inventarioAbierto = !inventarioAbierto;
        }

        if (inventarioAbierto)
        {
            animacionDeLaInterfaz.SetBool("abierto", true);
        }
        else
        {
            animacionDeLaInterfaz.SetBool("abierto", false);
        }
    }

    private void MostrarObjetosDelInventario()
    {
        if (GrifoBronceEnElInventario && 
            !contenidoInventario.text.Contains(nombreGrifoBronce))
            contenidoInventario.text += nombreGrifoBronce + "\n";

        if (GrifoMarmolEnElInventario &&
            !contenidoInventario.text.Contains(nombreGrifoMarmol))
            contenidoInventario.text += nombreGrifoMarmol + "\n";

        if (GrifoMaderaEnElInventario &&
            !contenidoInventario.text.Contains(nombreGrifoMadera))
            contenidoInventario.text += nombreGrifoMadera + "\n";

    }
}
