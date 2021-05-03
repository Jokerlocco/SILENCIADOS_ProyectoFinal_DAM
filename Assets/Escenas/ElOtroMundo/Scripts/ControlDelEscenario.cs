using System.Collections.Generic;
using UnityEngine;

public class ControlDelEscenario : MonoBehaviour
{
    // Movemos el escenario para hacer parecer que es infinito (efecto parallax).

    [SerializeField] private GameObject inicioDelMapa = null; // Asignado en Unity
    [SerializeField] private GameObject finalDelMapa = null; // Asignado en Unity

    [SerializeField] private GameObject escenario = null; // Asignado en Unity
    private float velocidadMovimientoEscenario = 6f;

    [SerializeField] private List<GameObject> props;


    private void Start()
    {
        
    }

    private void Update()
    {
        ControlDelMovimientoDelEscenario();
        ReiniciarInicioDelEscenario();
    }

    private void ControlDelMovimientoDelEscenario()
    {
        // Aumentaremos la Z del escenario para que se mueva hacia adelante
        escenario.transform.position = new Vector3(
            escenario.transform.position.x,
            escenario.transform.position.y,
            escenario.transform.position.z - velocidadMovimientoEscenario * Time.deltaTime);
    }

    // Si los props llegan al final del mapa, se vuelve a colocar al inicio
    private void ReiniciarInicioDelEscenario()
    {
        foreach (GameObject prop in props)
        {
            if (prop.transform.position.x <= finalDelMapa.transform.position.x)
            {
                prop.transform.position =
                    new Vector3(
                        prop.transform.position.x,
                        prop.transform.position.y,
                        inicioDelMapa.transform.position.z);
            }
        }
    }
}
