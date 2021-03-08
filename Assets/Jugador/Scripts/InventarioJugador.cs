using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventarioJugador : MonoBehaviour
{
    public bool GlifoBronceEnElInventario { get; set; }

    private void Awake()
    {
        ImplementarPatronSingleton();
    }

    private void Start()
    {
        GlifoBronceEnElInventario = false;
    }

    private void ImplementarPatronSingleton()
    {
        int numeroDeInventariosInstanciados =
            FindObjectsOfType<InventarioJugador>().Length;

        if (numeroDeInventariosInstanciados > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }
}
