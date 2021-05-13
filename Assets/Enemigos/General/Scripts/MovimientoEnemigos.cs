using UnityEngine;
using UnityEngine.AI;

public class MovimientoEnemigos : MonoBehaviour
{
    private Transform objetivoAAlcanzar = null;
    private NavMeshAgent agenteDeMovimiento;

    private void Start()
    {
        agenteDeMovimiento = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        EstablecerDestinoDeMovimiento();
    }

    private void EstablecerDestinoDeMovimiento()
    {
        objetivoAAlcanzar = GameObject.
            FindGameObjectWithTag("Jugador").gameObject.transform;
        agenteDeMovimiento.SetDestination(objetivoAAlcanzar.position);
    }
}
