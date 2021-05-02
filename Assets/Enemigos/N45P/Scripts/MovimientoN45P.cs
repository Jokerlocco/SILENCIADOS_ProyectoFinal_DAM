using UnityEngine;
using UnityEngine.AI;

public class MovimientoN45P : MonoBehaviour
{
    [SerializeField] Transform objetivoAAlcanzar = null; // Asignado en Unity
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
        agenteDeMovimiento.SetDestination(objetivoAAlcanzar.position);
    }
}
