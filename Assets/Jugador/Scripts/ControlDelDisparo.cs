using UnityEngine;

public class ControlDelDisparo : MonoBehaviour
{
    [SerializeField] GameObject bala;
    [SerializeField] Transform spawnPoint;

    [SerializeField] float fuerzaDelDisparo = 1500;
    [SerializeField] float cooldownDelDisparo = 0.5f;
    private float cooldownDelDisparoTiempo = 0;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interactuar"))
        {
            if (Time.time > cooldownDelDisparoTiempo)
            {
                GameObject nuevaBala;
                nuevaBala = Instantiate(
                    bala, spawnPoint.position, spawnPoint.rotation);
                nuevaBala.GetComponent<Rigidbody>()
                    .AddForce(spawnPoint.forward * fuerzaDelDisparo);

                cooldownDelDisparoTiempo = Time.time + cooldownDelDisparo;

                //Destroy(nuevaBala, 2);
            }
        }
    }
}
