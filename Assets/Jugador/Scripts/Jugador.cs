using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    [SerializeField] float velocidadMovimiento;
    [SerializeField] float velocidadRotacion;

    private Animator animacion;
    [SerializeField] private float x;
    [SerializeField] private float y;

    [SerializeField] private float velocidadCorrer;
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
        Correr();
        MoverPersonaje();
    }

    private void Correr()
    {
        if (Input.GetButton("Esprintar"))// Si se pulsa el botón de correr...
        {
            if (y >= 1) // y si también está avanzando (y no retrocediendo), puede correr
            {
                velocidadMovimiento = velocidadCorrer;
                animacion.SetBool("correr", true);
            }
            else
            {
                velocidadMovimiento = velocidadCaminar;
                animacion.SetBool("correr", false);
            }   
        }
        else
        {
            velocidadMovimiento = velocidadCaminar;
            animacion.SetBool("correr", false);
        }
    }

    private void MoverPersonaje()
    {
        if(PuedeMoverse)
        {
            x = Input.GetAxis("Horizontal");
            y = Input.GetAxis("Vertical");

            transform.Rotate(0, x * Time.deltaTime * velocidadRotacion, 0);
            transform.Translate(0, 0, y * Time.deltaTime * velocidadMovimiento);

            animacion.SetFloat("velocidadX", x);
            animacion.SetFloat("velocidadY", y);
        }
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
