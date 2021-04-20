using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventarioJugador : MonoBehaviour
{
    public bool InventarioJugadorDisponible { get; set; } = false;

    // Animación inventario
    //[SerializeField] GameObject interfazInventario; // Asignado en Unity
    //[SerializeField] GameObject fondoOscuroTraslucidoInventario; // Asignado en Unity
    private GameObject interfazInventario;
    private Animator animacionDeLaInterfaz;
    private bool inventarioAbierto;

    //[SerializeField] TMP_Text contenidoInventario; // Asignado en Unity
    private TMP_Text contenidoInventario;


    // Objetos (ordenados alfabéticamente)
    private string nombreAcetona = "Acetona";
    public bool AcetonaEnElInventario { get; set; } = false;

    private string nombreBombilla = "Bombilla funcional";
    public bool BombillaEnElInventario { get; set; } = false;

    private string nombreDisolventeDeSilicona = "Disolvente de silicona";
    public bool DisolventeDeSiliconaEnElInventario { get; set; } = false;

    private string nombreExtintor = "Extintor";
    public bool ExtintorEnElInventario { get; set; } = false;

    private string nombreGanzua = "Ganzúa";
    public bool GanzuaEnElInventario { get; set; } = false;

    private string nombreGrifoBronce = "Grifo de bronce";
    public bool GrifoBronceEnElInventario { get; set; } = false;

    private string nombreGrifoMadera = "Grifo de madera";
    public bool GrifoMaderaEnElInventario { get; set; } = false;

    private string nombreGrifoMarmol = "Grifo de mármol";
    public bool GrifoMarmolEnElInventario { get; set; } = false;

    private string nombreJarron = "Jarrón";
    public bool JarronEnElInventario { get; set; } = false;

    private string nombreJarronConAgua = "Jarrón con agua";
    public bool JarronConAguaEnElInventario { get; set; } = false;

    private string nombreLlaveAlfil = "Llave alfil";
    public bool LlaveAlfilEnElInventario { get; set; } = false;

    private string nombreLlaveCaballo = "Llave caballo";
    public bool LlaveCaballoEnElInventario { get; set; } = false;

    private string nombreLlaveInglesa = "Llave inglesa";
    public bool LlaveInglesaEnElInventario { get; set; } = false;

    private string nombreLlavePeon = "Llave peón";
    public bool LlavePeonEnElInventario { get; set; } = false;

    private string nombreLlaveRey = "Llave rey";
    public bool LlaveReyEnElInventario { get; set; } = false;

    private string nombreLlaveTorre = "Llave torre";
    public bool LlaveTorreEnElInventario { get; set; } = false;

    private string nombreTarjetaDeIdentificacionAlynS = "Tjt.Id: Alyn Spencer";
    public bool TarjetaDeIdentificacionAlynSEnElInventario { get; set; } = false;

    private string nombreTarjetaDeIdentificacionMorganS = "Tjt.Id: Morgan Sanderson";
    public bool TarjetaDeIdentificacionMorganSEnElInventario { get; set; } = false;

    private string nombreTarjetaDeIdentificacionRKarlheinz = "Tjt.Id: Raphael Karlheinz";
    public bool TarjetaDeIdentificacionRKarlheinzEnElInventario { get; set; } = false;

    private string nombreTuboCurvoConValvula = "Tubo curvo con válvula";
    public bool TuboCurvoConValvulaEnElInventario { get; set; } = false;

    private string nombreVinagre = "Vinagre";
    public bool VinagreEnElInventario { get; set; } = false;

    private string nombreEter = "Éter etílico";
    public bool EterEnElInventario { get; set; } = false;


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
        {
            Destroy(gameObject);
            InventarioJugadorDisponible = false;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            InventarioJugadorDisponible = true;
        }
    }

    private void InicializarComponentes()
    {
        if (GameObject.FindGameObjectWithTag("InterfazInventarioJugador"))
        {
            interfazInventario = GameObject.
                FindGameObjectWithTag("InterfazInventarioJugador").gameObject;
            contenidoInventario = GameObject.
                FindGameObjectWithTag("ContenidoInventario").gameObject.
                GetComponent<TMP_Text>();
            contenidoInventario.text = "";

            animacionDeLaInterfaz = interfazInventario.GetComponent<Animator>();

            inventarioAbierto = false;
        }
    }

    private void Awake()
    {
        ImplementarPatronSingleton();
    }

    private void Update()
    {
        if (interfazInventario == null)
        {
            InicializarComponentes();
        } 
        else
        {
            if (InventarioJugadorDisponible &&
                FindObjectOfType<ControlDelJugador>())
            {
                if (FindObjectOfType<ControlDelJugador>().PuedeMoverse)
                {
                    AbrirOCerrarAnimacionDelInventario();
                    ComprobarObjetosEspecificos();
                    MostrarObjetosQueEstanEnElInventario();
                }
            }
        }  
    }

    private void AbrirOCerrarAnimacionDelInventario()
    {
        if (Input.GetButtonDown("BotonInventario"))
            inventarioAbierto = !inventarioAbierto;

        if (animacionDeLaInterfaz != null)
        {
            if (inventarioAbierto)
                animacionDeLaInterfaz.SetBool("abierto", true);
            else
                animacionDeLaInterfaz.SetBool("abierto", false);
        }
    }

    private void ComprobarObjetosEspecificos()
    {
        DescartarLlavesPorUsos();
    }
    
    private void DescartarLlavesPorUsos()
    {
        if (LlaveAlfilEnElInventario &&
            NumUsosLlaveAlfil == cantidadDePuertasParaLlaveAlfil)
            LlaveAlfilEnElInventario = false;

        if (LlaveCaballoEnElInventario &&
            NumUsosLlaveCaballo == cantidadDePuertasParaLlaveCaballo)
            LlaveCaballoEnElInventario = false;

        if (LlavePeonEnElInventario && 
            NumUsosLlavePeon == cantidadDePuertasParaLlavePeon)
            LlavePeonEnElInventario = false;

        if (LlaveReyEnElInventario &&
            NumUsosLlaveRey == cantidadDePuertasParaLlaveRey)
            LlaveReyEnElInventario = false;

        if (LlaveTorreEnElInventario &&
            NumUsosLlaveTorre == cantidadDePuertasParaLlaveTorre)
            LlaveTorreEnElInventario = false;
    }

    private void MostrarObjetosQueEstanEnElInventario()
    {
        contenidoInventario.text = "";

        if (AcetonaEnElInventario &&
            !contenidoInventario.text.Contains(nombreAcetona))
            contenidoInventario.text += nombreAcetona + "\n";

        if (BombillaEnElInventario &&
            !contenidoInventario.text.Contains(nombreBombilla))
            contenidoInventario.text += nombreBombilla + "\n";

        if (DisolventeDeSiliconaEnElInventario &&
            !contenidoInventario.text.Contains(nombreDisolventeDeSilicona))
            contenidoInventario.text += nombreDisolventeDeSilicona + "\n";

        if (ExtintorEnElInventario &&
            !contenidoInventario.text.Contains(nombreExtintor))
            contenidoInventario.text += nombreExtintor + "\n";

        if (GanzuaEnElInventario &&
            !contenidoInventario.text.Contains(nombreGanzua))
            contenidoInventario.text += nombreGanzua + "\n";

        if (GrifoBronceEnElInventario &&
            !contenidoInventario.text.Contains(nombreGrifoBronce))
            contenidoInventario.text += nombreGrifoBronce + "\n";

        if (GrifoMaderaEnElInventario &&
            !contenidoInventario.text.Contains(nombreGrifoMadera))
            contenidoInventario.text += nombreGrifoMadera + "\n";

        if (GrifoMarmolEnElInventario &&
            !contenidoInventario.text.Contains(nombreGrifoMarmol))
            contenidoInventario.text += nombreGrifoMarmol + "\n";

        if (JarronEnElInventario &&
            !contenidoInventario.text.Contains(nombreJarron))
            contenidoInventario.text += nombreJarron + "\n";

        if (JarronConAguaEnElInventario &&
            !contenidoInventario.text.Contains(nombreJarronConAgua))
            contenidoInventario.text += nombreJarronConAgua + "\n";

        if (LlaveAlfilEnElInventario &&
            !contenidoInventario.text.Contains(nombreLlaveAlfil))
            contenidoInventario.text += nombreLlaveAlfil + "\n";

        if (LlaveCaballoEnElInventario &&
            !contenidoInventario.text.Contains(nombreLlaveCaballo))
            contenidoInventario.text += nombreLlaveCaballo + "\n";

        if (LlaveInglesaEnElInventario &&
            !contenidoInventario.text.Contains(nombreLlaveInglesa))
            contenidoInventario.text += nombreLlaveInglesa + "\n";

        if (LlavePeonEnElInventario && 
            !contenidoInventario.text.Contains(nombreLlavePeon))
            contenidoInventario.text += nombreLlavePeon + "\n";

        if (LlaveReyEnElInventario &&
            !contenidoInventario.text.Contains(nombreLlaveRey))
            contenidoInventario.text += nombreLlaveRey + "\n";

        if (LlaveTorreEnElInventario &&
            !contenidoInventario.text.Contains(nombreLlaveTorre))
            contenidoInventario.text += nombreLlaveTorre + "\n";

        if (TarjetaDeIdentificacionAlynSEnElInventario &&
            !contenidoInventario.text.Contains(nombreTarjetaDeIdentificacionAlynS))
            contenidoInventario.text += nombreTarjetaDeIdentificacionAlynS + "\n";

        if (TarjetaDeIdentificacionMorganSEnElInventario &&
            !contenidoInventario.text.Contains(nombreTarjetaDeIdentificacionMorganS))
            contenidoInventario.text += nombreTarjetaDeIdentificacionMorganS + "\n";

        if (TarjetaDeIdentificacionRKarlheinzEnElInventario &&
            !contenidoInventario.text.Contains(nombreTarjetaDeIdentificacionRKarlheinz))
            contenidoInventario.text += nombreTarjetaDeIdentificacionRKarlheinz + "\n";

        if (TuboCurvoConValvulaEnElInventario &&
            !contenidoInventario.text.Contains(nombreTuboCurvoConValvula))
            contenidoInventario.text += nombreTuboCurvoConValvula + "\n";

        if (VinagreEnElInventario &&
            !contenidoInventario.text.Contains(nombreVinagre))
            contenidoInventario.text += nombreVinagre + "\n";

        if (EterEnElInventario &&
            !contenidoInventario.text.Contains(nombreEter))
            contenidoInventario.text += nombreEter + "\n";
    }
}
