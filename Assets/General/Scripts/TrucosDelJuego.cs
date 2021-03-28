using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrucosDelJuego : MonoBehaviour
{
    [SerializeField] bool llavePeonEnElInventarioDelJugador = false;

    private void Update()
    {
        if (llavePeonEnElInventarioDelJugador)
            FindObjectOfType<InventarioJugador>().LlavePeonEnElInventario = true;
        else
            FindObjectOfType<InventarioJugador>().LlavePeonEnElInventario = false;
    }
}
