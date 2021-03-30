using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventarioJugador : MonoBehaviour
{
    // Animación inventario
    [SerializeField] GameObject interfazInventario; // Asignado en Unity
    [SerializeField] GameObject fondoOscuroTraslucidoInventario; // Asignado en Unity
    private Animator animacionDeLaInterfaz;
    private bool inventarioAbierto;

    [SerializeField] TMP_Text contenidoInventario; // Asignado en Unity


    // Objetos (ordenados alfabéticamente)
    private string nombreBombilla = "Bombilla funcional";
    public bool BombillaEnElInventario { get; set; } = false;

    private string nombreGrifoBronce = "Grifo de bronce";
    public bool GrifoBronceEnElInventario { get; set; } = false;

    private string nombreGrifoMadera = "Grifo de madera";
    public bool GrifoMaderaEnElInventario { get; set; } = false;

    private string nombreGrifoMarmol = "Grifo de mármol";
    public bool GrifoMarmolEnElInventario { get; set; } = false;

    private string nombreLlaveAlfil = "Llave alfil";
    public bool LlaveAlfilEnElInventario { get; set; } = false;

    private string nombreLlaveCaballo = "Llave caballo";
    public bool LlaveCaballoEnElInventario { get; set; } = false;

    private string nombreLlavePeon = "Llave peón";
    public bool LlavePeonEnElInventario { get; set; } = false;

    private string nombreLlaveRey = "Llave rey";
    public bool LlaveReyEnElInventario { get; set; } = false;

    private string nombreLlaveTorre = "Llave torre";
    public bool LlaveTorreEnElInventario { get; set; } = false;

    // Usos de las llaves
    public int NumUsosLlaveAlfil { get; set; } = 0;
    private int cantidadDePuertasParaLlaveAlfil = 3;

    public int NumUsosLlaveCaballo { get; set; } = 0;
    private int cantidadDePuertasParaLlaveCaballo = 3;

    public int NumUsosLlavePeon { get; set; } = 0;
    private int cantidadDePuertasParaLlavePeon = 3;

    public int NumUsosLlaveRey { get; set; } = 0;
    private int cantidadDePuertasParaLlaveRey = 1;

    public int NumUsosLlaveTorre { get; set; } = 0;
    private int cantidadDePuertasParaLlaveTorre = 2;


    private void ImplementarPatronSingleton()
    {
        int numeroDeInventariosInstanciados =
            FindObjectsOfType<InventarioJugador>().Length;

        if (numeroDeInventariosInstanciados > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }

    private void Awake()
    {
        ImplementarPatronSingleton();
    }

    private void Start()
    {
        animacionDeLaInterfaz = interfazInventario.GetComponent<Animator>();
        inventarioAbierto = false;

        contenidoInventario.text = "";
    }

    private void Update()
    {
        if (FindObjectOfType<ControlDelJugador>().PuedeMoverse)
        {
            AbrirOCerrarAnimacionDelInventario();
            ComprobarObjetosEspecificos();
            MostrarObjetosQueEstanEnElInventario();
        }
    }

    private void AbrirOCerrarAnimacionDelInventario()
    {
        if (Input.GetButtonDown("BotonInventario"))
            inventarioAbierto = !inventarioAbierto;

        if (inventarioAbierto)
            animacionDeLaInterfaz.SetBool("abierto", true);
        else
            animacionDeLaInterfaz.SetBool("abierto", false);
    }

    private void ComprobarObjetosEspecificos()
    {
        DescartarLlavesPorUsos();
    }
    
    private void DescartarLlavesPorUsos()
    {
        if (LlaveAlfilEnElInventario &&
            NumUsosLlaveAlfil == cantidadDePuertasParaLlaveAlfil)
        {
            LlaveAlfilEnElInventario = false;
        }

        if (LlaveCaballoEnElInventario &&
            NumUsosLlaveCaballo == cantidadDePuertasParaLlaveCaballo)
        {
            LlaveCaballoEnElInventario = false;
        }

        if (LlavePeonEnElInventario && 
            NumUsosLlavePeon == cantidadDePuertasParaLlavePeon)
        {
            LlavePeonEnElInventario = false;
        }

        if (LlaveReyEnElInventario &&
            NumUsosLlaveRey == cantidadDePuertasParaLlaveRey)
        {
            LlaveReyEnElInventario = false;
        }

        if (LlaveTorreEnElInventario &&
            NumUsosLlaveTorre == cantidadDePuertasParaLlaveTorre)
        {
            LlaveTorreEnElInventario = false;
        }
    }

    private void MostrarObjetosQueEstanEnElInventario()
    {
        contenidoInventario.text = "";

        if (BombillaEnElInventario &&
            !contenidoInventario.text.Contains(nombreBombilla))
            contenidoInventario.text += nombreBombilla + "\n";

        if (GrifoBronceEnElInventario &&
            !contenidoInventario.text.Contains(nombreGrifoBronce))
            contenidoInventario.text += nombreGrifoBronce + "\n";

        if (GrifoMaderaEnElInventario &&
            !contenidoInventario.text.Contains(nombreGrifoMadera))
            contenidoInventario.text += nombreGrifoMadera + "\n";

        if (GrifoMarmolEnElInventario &&
            !contenidoInventario.text.Contains(nombreGrifoMarmol))
            contenidoInventario.text += nombreGrifoMarmol + "\n";

        if (LlaveAlfilEnElInventario &&
            !contenidoInventario.text.Contains(nombreLlaveAlfil))
            contenidoInventario.text += nombreLlaveAlfil + "\n";

        if (LlaveCaballoEnElInventario &&
            !contenidoInventario.text.Contains(nombreLlaveCaballo))
            contenidoInventario.text += nombreLlaveCaballo + "\n";

        if (LlavePeonEnElInventario && 
            !contenidoInventario.text.Contains(nombreLlavePeon))
            contenidoInventario.text += nombreLlavePeon + "\n";

        if (LlaveReyEnElInventario &&
            !contenidoInventario.text.Contains(nombreLlaveRey))
            contenidoInventario.text += nombreLlaveRey + "\n";

        if (LlaveTorreEnElInventario &&
            !contenidoInventario.text.Contains(nombreLlaveTorre))
            contenidoInventario.text += nombreLlaveTorre + "\n";
    }
}
