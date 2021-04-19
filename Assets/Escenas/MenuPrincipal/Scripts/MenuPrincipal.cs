using UnityEngine;

public class MenuPrincipal : MonoBehaviour
{
    private void Awake()
    {
        if (FindObjectOfType<InventarioJugador>())
        {
            FindObjectOfType<InventarioJugador>().
                InventarioJugadorDisponible = false;
            Destroy(FindObjectOfType<InventarioJugador>().gameObject);
        }
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
}
