using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventarioJugador : MonoBehaviour
{
    [SerializeField] TMP_Text contenidoInventario; // Asignado en Unity
    [SerializeField] GameObject fondoOscuroTraslucidoInventario; // Asignado en Unity

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
        contenidoInventario.text = "";
        GlifoBronceEnElInventario = false;
        GlifoMarmolEnElInventario = false;
    }

    private void Update()
    {
        MostrarObjetosDelInventario();
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
