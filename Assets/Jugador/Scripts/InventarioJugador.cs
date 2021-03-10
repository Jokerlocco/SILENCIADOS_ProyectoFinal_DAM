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
    private string nombreGlifoBronce = "Glifo de bronce";
    public bool GlifoBronceEnElInventario { get; set; }
    private string nombreGlifoMarmol = "Glifo de mármol";
    public bool GlifoMarmolEnElInventario { get; set; }

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
        GlifoBronceEnElInventario = false;
        GlifoMarmolEnElInventario = false;
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
        if (GlifoBronceEnElInventario && 
            !contenidoInventario.text.Contains(nombreGlifoBronce))
            contenidoInventario.text += nombreGlifoBronce + "\n";

        if (GlifoMarmolEnElInventario &&
            !contenidoInventario.text.Contains(nombreGlifoMarmol))
            contenidoInventario.text += nombreGlifoMarmol + "\n";

    }
}
