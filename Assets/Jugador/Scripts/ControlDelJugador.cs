using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private string tipoDeControl;

    private GameObject pistolaEstandoQuieto;
    private GameObject pistolaMoviendose;

    void Start()
    {
        animacion = GetComponent<Animator>();
        velocidadCaminar = velocidadMovimiento;
        PuedeMoverse = true;
        reproductorSonidoPies =
            GetComponentInChildren<ReproductorSonidoPiesJugador>();

        EstablecerTipoDeControl();
        if (tipoDeControl == "Armado")
        {
            pistolaEstandoQuieto = GameObject.FindGameObjectWithTag(
                "PistolaEstandoQuieto").gameObject;
            pistolaMoviendose = GameObject.FindGameObjectWithTag(
                "PistolaMoviendose").gameObject;
        }
    }

    void Update()
    {
        if (PuedeMoverse)
        {
            if (tipoDeControl == "Normal") // Si esta "Armado" ya da la sensación de que corre, y no es necesario esto
                Correr();
            MoverPersonaje();
        }
        else
            EstablecerAnimacionDeEstarQuieto();
    }

    private void EstablecerTipoDeControl()
    {
        if (SceneManager.GetActiveScene().name != "ElOtroMundo")
            tipoDeControl = "Normal";
        else
            tipoDeControl = "Armado";
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

        if (tipoDeControl == "Armado")
            EstablecerArmaSegunQuietoOMoviendose();
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

    private void EstablecerArmaSegunQuietoOMoviendose()
    {
        if ((x == 0 && y == 0) || (y == -1)) // Si esta quieto o va hacia atrás...
        {
            pistolaMoviendose.transform.GetChild(0).
                GetComponent<SkinnedMeshRenderer>().enabled = false;

            pistolaEstandoQuieto.transform.GetChild(0).
                GetComponent<SkinnedMeshRenderer>().enabled = true;
        }
        else
        {
            pistolaEstandoQuieto.transform.GetChild(0).
                GetComponent<SkinnedMeshRenderer>().enabled = false;

            pistolaMoviendose.transform.GetChild(0).
                GetComponent<SkinnedMeshRenderer>().enabled = true;
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
