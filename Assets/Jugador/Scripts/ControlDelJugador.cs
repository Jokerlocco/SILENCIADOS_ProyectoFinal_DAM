using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDelJugador : MonoBehaviour
{
    [SerializeField] float velocidadMovimiento; // Asignado en Unity
    [SerializeField] float velocidadRotacion; // Asignado en Unity

    private Animator animacion;
    [SerializeField] private float x;
    [SerializeField] private float y;

    [SerializeField] private float velocidadCorrer; // Asignado en Unity
    private float velocidadCaminar;

    private ReproductorSonidoPiesJugador reproductorSonidoPies;

    public bool PuedeMoverse { get; set; }

    void Start()
    {
        animacion = GetComponent<Animator>();
        velocidadCaminar = velocidadMovimiento;
        PuedeMoverse = true;
        reproductorSonidoPies =
            GetComponentInChildren<ReproductorSonidoPiesJugador>();
    }

    void Update()
    {
        if (PuedeMoverse)
        {
            Correr();
            MoverPersonaje();
        }
        else
            EstablecerAnimacionDeEstarQuieto();
    }

    private void Correr()
    {
        if (Input.GetButton("Esprintar")) // Si se pulsa el botón de correr...
        {
            if (y >= 1) // y si también está avanzando (y no retrocediendo), puede correr
                EstablecerAnimacionDeCorrer();
            else
                EstablecerAnimacionDeCaminar();
        }
        else
            EstablecerAnimacionDeCaminar();
    }

    private void MoverPersonaje()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        transform.Rotate(0, x * Time.deltaTime * velocidadRotacion, 0);
        transform.Translate(0, 0, y * Time.deltaTime * velocidadMovimiento);

        animacion.SetFloat("velocidadX", x);
        animacion.SetFloat("velocidadY", y);
    }

    private void EstablecerAnimacionDeCorrer()
    {
        velocidadMovimiento = velocidadCorrer;
        animacion.SetBool("correr", true);
    }

    private void EstablecerAnimacionDeCaminar()
    {
        velocidadMovimiento = velocidadCaminar;
        animacion.SetBool("correr", false);
    }

    private void EstablecerAnimacionDeEstarQuieto()
    {
        animacion.SetFloat("velocidadX", 0.0f);
        animacion.SetFloat("velocidadY", 0.0f);
        animacion.SetBool("correr", false);
    }

    private void ReproducirSonidoPaso1() // Utilizado en las animaciones
    {
        reproductorSonidoPies.ReproducirSonidoPaso1();
    }

    private void ReproducirSonidoPaso2() // Utilizado en las animaciones
    {
        reproductorSonidoPies.ReproducirSonidoPaso2();
    }

    private void ReproducirSonidoCorrer() // Utilizado en las animaciones
    {
        reproductorSonidoPies.ReproducirSonidoCorrer();
    }
}
