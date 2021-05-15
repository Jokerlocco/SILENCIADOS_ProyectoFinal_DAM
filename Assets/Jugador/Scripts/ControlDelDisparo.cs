using System.Collections;
using UnityEngine;

public class ControlDelDisparo : MonoBehaviour
{
    private GameObject pistola;
    [SerializeField] GameObject bala = null; // Asignado en Unity
    [SerializeField] Camera camara = null; // Asignado en Unity
    [SerializeField] Transform spawnPointBala = null; // Asignado en Unity

    private float fuerzaDelDisparo = 1500;
    private float cooldownDelDisparo = 0.8f;
    private float distanciaMaximaQueRecorreLaBala = 300f;

    private AudioSource audioSource;
    private Animator animacionDisparo;

    private float cooldownDelDisparoActual = 0;

    private void Start()
    {
        pistola = 
            GameObject.FindGameObjectWithTag("Pistola").transform.gameObject;
        animacionDisparo =
            pistola.transform.GetChild(0).GetComponent<Animator>();
        audioSource = pistola.GetComponent<AudioSource>();
    }

    private void Update()
    {
        Disparar();
    }

    private void Disparar()
    {
        if (Input.GetButtonDown("Fire1") || Input.GetButton("Fire1"))
        {
            if (Time.time > cooldownDelDisparoActual) // Para controlar el tiempo entre cada disparo
            {
                LanzarBala();
                Impactar();

                cooldownDelDisparoActual = Time.time + cooldownDelDisparo;
            }
        }
    }

    private void Impactar()
    {
        RaycastHit impacto;
        bool impactado = Physics.Raycast(
            camara.transform.position,
            camara.transform.forward,
            out impacto,
            distanciaMaximaQueRecorreLaBala);

        if (impactado)
        {
            if (impacto.collider.CompareTag("EnemigoPeon") ||
                impacto.collider.CompareTag("EnemigoTorre") ||
                impacto.collider.CompareTag("EnemigoAlfil") ||
                impacto.collider.CompareTag("EnemigoCaballo") ||
                impacto.collider.CompareTag("N45P"))
            {
                EstadoDelEnemigo estadoDelEnemigoImpactado = impacto.collider
                    .gameObject.GetComponent<EstadoDelEnemigo>();
                estadoDelEnemigoImpactado.RecibirDaño();
            }
        }
    }

    private void LanzarBala()
    {
        GameObject nuevaBala;
        nuevaBala = Instantiate(
            bala, spawnPointBala.position, spawnPointBala.rotation);
        nuevaBala.GetComponent<Rigidbody>()
            .AddForce(spawnPointBala.forward * fuerzaDelDisparo);

        Destroy(nuevaBala, 2);

        ReproducirSonido();
        EstablecerAnimacionDeDisparo();
    }

    private void EstablecerAnimacionDeDisparo()
    {
        animacionDisparo.SetTrigger("Fire");
    }

    private void ReproducirSonido()
    {
        if (audioSource != null)
            audioSource.Play();
    }
}
