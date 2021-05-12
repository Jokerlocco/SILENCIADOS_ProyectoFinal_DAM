using UnityEngine;

public class Bala : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemigoPeon"))
            Destroy(other);
    }
}
