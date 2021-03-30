using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrucosDelJuego : MonoBehaviour
{
    [SerializeField] bool llaveAlfilEnElInventarioDelJugador = false;
    [SerializeField] bool llaveCaballoEnElInventarioDelJugador = false;
    [SerializeField] bool llavePeonEnElInventarioDelJugador = false;
    [SerializeField] bool llaveReyEnElInventarioDelJugador = false;
    [SerializeField] bool llaveTorreEnElInventarioDelJugador = false;

    private void Update()
    {
        if (llaveAlfilEnElInventarioDelJugador)
            FindObjectOfType<InventarioJugador>().
                LlaveAlfilEnElInventario = true;
        else
            FindObjectOfType<InventarioJugador>().
                LlaveAlfilEnElInventario = false;

        if (llaveCaballoEnElInventarioDelJugador)
            FindObjectOfType<InventarioJugador>().
                LlaveCaballoEnElInventario = true;
        else
            FindObjectOfType<InventarioJugador>().
                LlaveCaballoEnElInventario = false;

        if (llavePeonEnElInventarioDelJugador)
            FindObjectOfType<InventarioJugador>().
                LlavePeonEnElInventario = true;
        else
            FindObjectOfType<InventarioJugador>().
                LlavePeonEnElInventario = false;

        if (llaveReyEnElInventarioDelJugador)
            FindObjectOfType<InventarioJugador>().
                LlaveReyEnElInventario = true;
        else
            FindObjectOfType<InventarioJugador>().
                LlaveReyEnElInventario = false;

        if (llaveTorreEnElInventarioDelJugador)
            FindObjectOfType<InventarioJugador>().
                LlaveTorreEnElInventario = true;
        else
            FindObjectOfType<InventarioJugador>().
                LlaveTorreEnElInventario = false;
    }
}
