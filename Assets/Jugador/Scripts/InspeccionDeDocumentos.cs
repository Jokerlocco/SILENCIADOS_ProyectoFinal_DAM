using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InspeccionDeDocumentos : MonoBehaviour
{
    private AudioSource audioSource;
    private string tagOriginal;

    private bool colisionando = false;

    [SerializeField] GameObject documentos; // Asignado en Unity
    [SerializeField] TMP_Text textoDelDocumento; // Asignado en Unity
    [SerializeField] RawImage flechaDerecha; // Asignado en Unity
    [SerializeField] RawImage flechaIzquierda; // Asignado en Unity
    [SerializeField] TMP_Text indicadorNumPagina; // Asignado en Unity
    private bool documentoActivo = false;

    private List<string> paginasDelDocumento = new List<string>();
    private int numPaginaActual;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        tagOriginal = gameObject.tag;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interactuar") && colisionando
            && !documentoActivo)
        {
            AbrirInterfaz();
        }

        if (documentoActivo)
        {
            MostrarIndicadoresDePaginas();
            MostrarIndicadorNumPagina();

            if (Input.GetButtonDown("Cerrar"))
            {
                CerrarInterfaz();
            }
            else if (Input.GetButtonDown("FlechaDerecha"))
            {
                AvanzarPagina();
            }
            else if (Input.GetButtonDown("FlechaIzquierda"))
            {
                RetrocederPagina();
            }
        }
    }

    private void AbrirInterfaz()
    {
        FindObjectOfType<ControlDelJugador>().PuedeMoverse = false;

        documentos.SetActive(true);
        documentoActivo = true;

        AsignarTextoDelDocumento();
        numPaginaActual = 0; // Mostramos la primera página cuando se abre
        MostrarPagina(numPaginaActual);

        gameObject.tag = "InterfazAbierta"; // Para quitar el icono de interacción en la vista de documentos
    }

    private void MostrarIndicadoresDePaginas()
    {
        if (paginasDelDocumento.Count > 1 && 
            numPaginaActual < (paginasDelDocumento.Count - 1))
            flechaDerecha.enabled = true;
        else
            flechaDerecha.enabled = false;

        if (paginasDelDocumento.Count > 1 &&
            numPaginaActual < paginasDelDocumento.Count &&
            numPaginaActual > 0)
            flechaIzquierda.enabled = true;
        else
            flechaIzquierda.enabled = false;
    }

    private void MostrarIndicadorNumPagina()
    {
        indicadorNumPagina.text = 
            "Página " + (numPaginaActual + 1) + " de " + 
            paginasDelDocumento.Count;
    }

    private void CerrarInterfaz()
    {
        textoDelDocumento.text = "";
        documentos.SetActive(false);
        documentoActivo = false;
        FindObjectOfType<ControlDelJugador>().PuedeMoverse = true;
        gameObject.tag = tagOriginal;
    }

    private void MostrarPagina(int numPaginaActual)
    {
        if (audioSource != null)
            audioSource.Play();
        textoDelDocumento.text = paginasDelDocumento[numPaginaActual];
    }

    private void AvanzarPagina()
    {
        if (paginasDelDocumento.Count > 1 && 
            numPaginaActual < (paginasDelDocumento.Count - 1) )
        {
            numPaginaActual++;
            MostrarPagina(numPaginaActual);
        }
    }

    private void RetrocederPagina()
    {
        if (paginasDelDocumento.Count > 1 &&
            (numPaginaActual - 1) >= 0)
        {
            numPaginaActual--;
            MostrarPagina(numPaginaActual);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador"))
            colisionando = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador"))
            colisionando = false;
    }

    private void AsignarTextoDelDocumento()
    {
        paginasDelDocumento.Clear();

        if (gameObject.CompareTag("D_DiarioDelSecretario"))
        {
            paginasDelDocumento.Add("DIARIO DEL SECRETARIO\n" +
                "---------------------------");

            paginasDelDocumento.Add(
                "02, Junio \n" +
                "Bueno, como me aburro en este antro, voy a escribir " +
                "mi propio diario. Pensaba que eso era cosas de chicas " +
                "adolescentes, pero al menos, me puedo entretener. " +
                "Vamos a ello: \n\n" +
                "Zesimov Inc, me contrató para trabajar como " +
                "secretario/conserje del asilo Karlheinz, cuyo apellido " +
                "pertenece al médico jefe del recinto (aunque creo que el " +
                "fundador fue su padre). Es un tipo estirado, viejo, y con " +
                "cara de pocos amigos. Lo realmente importante es que es un " +
                "gilipollas. Tranqui, él no va a leer esto porque guardo este " +
                "documento bajo llave.\n");

            paginasDelDocumento.Add("Desde que llegué hace unos meses, me " +
                "tratan como a una farola. Mi única función es atender a la " +
                "gente que entra por esa puerta de enfrente. Es decir: nadie " +
                "(sin contar a mis compañeros enfermeros y enfermeras, claro)." +
                " Y los desconocidos que entran, trabajan para Zesimov, " +
                "teniendo un cargo mayor que el mío, ni si quiera me miran. " +
                "Su identificación es suficientemente importante para que ni " +
                "les pueda preguntar a dónde se dirigen o que van a hacer en " +
                "el asilo... \n" +
                "Si hasta tienen una llave especial para el " +
                "ascensor que yo no tengo. Creo que van a una planta inferior," +
                " una que está por debajo del propio sótano. " +
                "No sé que chingados harán ahí abajo, pero a veces... " +
                "a veces se llevan enfermos ahí, y no regresan... \n" +
                "Y no quiero sonar conspiranoico, pero para ser un asilo " +
                "relativamente pequeño, la morgue es muy transitada... " +
                "Mejor será no irme de la lengua con Zesimov o acabaré " +
                "ahí, je, je.\n");

            paginasDelDocumento.Add("07, Junio \n" +
                "Me encontraba limpiando la estatua de la musa Urania, " +
                "y me percaté de que los orificios que tiene en su soporte " +
                "sirven para insertar algo. La Sra. Wilkes (enfermera jefe), " +
                "me pilló fisgoneándolos y me echó la bronca por no estar en " +
                "la oficina. No soy un pendejo, sé que quería que me alejará " +
                "de ahí, que no metiera mis zarpas dónde no debía, como " +
                "siempre... Ella y el doctor Karlheinz son la máxima " +
                "autoridad del asilo y saben (o incluso son responsables) " +
                "de todos los secretos de Zesimov en él... Cuanto más tiempo" +
                " paso en este antro, más cosas turbias encuentro... " +
                "Si no fuera por la gran cantidad de plata que me dan, " +
                "me hubiera largado de aquí, pero me gusta el dinero.\n");

            paginasDelDocumento.Add("11, Junio \n" +
                "Hoy me han traído a la oficina una caja de seguridad con " +
                "un código. Es muy futurística, la verdad. Me han dicho que " +
                "no la toque, que la dejé ahí por si el doctor o Wilkes la " +
                "necesitan en caso de emergencia. Me pregunto que " +
                "contendrá...\n\n" +
                "21, Junio \n" +
                "La gente teme al cementerio... " +
                "Teme que se les aparezca algún fantasma... Oh, no, no, no... " +
                "El cementerio es un lugar seguro. Ahí no hay fantasmas. " +
                "Los espíritus perdidos se quedan en el lugar de fallecimiento," +
                " y es aquí, cuando realmente se tiene que tener miedo... " +
                "Lugares como las carreteras, los hospitales, o...\n" +
                "los asilos...");

            paginasDelDocumento.Add("01, Julio \n" +
                "No soporto más este lugar. Pensaba que serían fantasmas, " +
                "pero no, es algo más... No sabría explicar qué carajos es " +
                "lo que estoy viendo últimamente. Voy a informar a mis " +
                "responsables de que hoy es mi último día acá.\n\n" +
                "¡Chingada madre! Legalmente no puedo largarme tan rápido. " +
                "Oh Dios mío, quiero irme a casa...\n");

            paginasDelDocumento.Add("(Justo debajo hay un texto escrito a " +
                "prisas, sin fecha):\n¡La Luz! ¡Esa luz intermitente viene " +
                "a por mí! ¡Huí de ella mientras se alimentaba de " +
                "Morgan! Me he encerrado en la oficina. Voy a morir, voy a " +
                "morir acá... Si alguien encuentra esto, debe saber que " +
                "Zesimov es el respo... (el texto termina aquí).\n");
        }

        else if (gameObject.CompareTag("D_PistaDeUnLoco1"))
        {
            paginasDelDocumento.Add("¡HE ENCONTRADO UNA LLAVE! ¡SÍ! ¡SÍ! " +
                "¡SÍ! ¡SÍ! ¡Y SERÁ SÓLO MÍA! ¡MÍA! ¡MÍA! ¡MÍA! ¡SÍ!\n " +
                "¡NADIE LA ENCONTRARÁ! ¡NI SI QUIERA LA ENFERMERA WILKES!");
        }

        else if (gameObject.CompareTag("D_PistaDeUnLoco2"))
        {
            paginasDelDocumento.Add("¡ME ENCANTA ENCONTRAR Y ESCONDER COSAS! " +
                "¡HACE POCO COGÍ LA LLAVE EN FORMA DE TORRE DE AJEDREZ Y " +
                "LA ESCONDÍ!\n" +
                "¡JA, JA, JA!\n" +
                "¡MORGAN Y SHARON HAN PERDIDO LA CABEZA BUSCÁNDOLA!\n" +
                "La he escondido en otro de mis " +
                "lugares secretos: Debajo de una baldosa de los dormitorios, " +
                "cerca de la cama donde duerme el tipo con medio cuerpo " +
                "quemado, je, je.");
        }

        else if (gameObject.CompareTag("D_RecordatorioACristobal"))
        {
            paginasDelDocumento.Add("Cristóbal, acuérdese de que mañana se " +
                "le realizará un chequeo en la sala médica B.\n" +
                "Aunque las enfermeras estarán pendientes de usted, " +
                "por favor, no beba ni coma nada. " +
                "Sea responsable.\n\nEnfermera A.Wilkes.");
        }

        else if (gameObject.CompareTag("D_CombinacionesQuimicos"))
        {
            paginasDelDocumento.Add("(Entre multiples combinaciones, " +
                "me interesa la siguiente):\n\n" +
                "Disolvente de silicona:\n" +
                "Combinar acetona y éter etílico diluidos en partes iguales. " +
                "Unas gotas de vinagre ayudará a acelerar el " +
                "proceso de disolución.");
        }

        else if (gameObject.CompareTag("D_PistaDeLaPizarra"))
        {
            paginasDelDocumento.Add("Resuelve la operación de la pizarra" +
                " para obtener el código de la caja de seguridad LMC, " +
                "número: '6352'.\n" +
                "Debe de ser transportada de mi despacho " +
                "al laboratorio en cuanto leas esto.\n\n" +
                "Dr. Karlheinz");
        }

        else if (gameObject.CompareTag("D_NecesidadesPMarvin"))
        {
            paginasDelDocumento.Add("Necesidades del paciente:\n\n" +
                "8756567L, Marvin Smith\n\n" +
                "- Atención constante\n" +
                "- Suministrar una dosis de Femeh cada 5 horas, 2ml\n" +
                "- Suministrar una dosis de Albanético cada 7 horas, 4ml\n" +
                "- Inyectar Apotima cada 24 horas\n" +
                "- Hacer que ande cada 4 horas");
        }

        else if (gameObject.CompareTag("D_NecesidadesPTom"))
        {
            paginasDelDocumento.Add("Necesidades del paciente:\n\n" +
                "0742567K, Tom Winston\n\n" +
                "- Acostarlo de lado (debido a sus quemaduras)\n" +
                "- Suministrar una dosis de Femeh cada 5 horas, 2ml\n" +
                "- Curar quemaduras cada 3 horas\n" +
                "- Realizar un estudio de sus quemaduras cada 24 horas (llamar al Dr. Karlheinz)\n" +
                "- Inyectar Leemo cada 24 horas\n" +
                "- Inyectar tranquilizante tipo C cada 7 horas\n" +
                "- Inyectar tranquilizante tipo B cada 9 horas\n");
        }

        else if (gameObject.CompareTag("D_NecesidadesPMarcus"))
        {
            paginasDelDocumento.Add("Necesidades del paciente:\n\n" +
                "9870985K, Marcus Coen\n\n" +
                "- Inyectar tranquilizante de 4ml en caso de autolesión\n" +
                "- Inyectar Leemo cada 24 horas");
        }

        else if (gameObject.CompareTag("D_NecesidadesPLuis"))
        {
            paginasDelDocumento.Add("Necesidades del paciente:\n\n" +
                "5432786N, Luis Hidalgo\n\n" +
                "- Cambiar pañal cada hora\n" +
                "- Mojarle los labios y darle agua si el paciente así lo desea\n" +
                "- Inyectar tranquilizante tipo C cada 7 horas\n" +
                "- Inyectar tranquilizante tipo B cada 9 horas\n");
        }

        else if (gameObject.CompareTag("D_NecesidadesPRobinson"))
        {
            paginasDelDocumento.Add("Necesidades del paciente:\n\n" +
                "96753497M, Ben Robinson\n\n" +
                "- Inyectar Leemo cada 24 horas\n" +
                "- Realizar un estudio de su torso cada 24 horas (llamar al Dr. Karlheinz)\n" +
                "- Atención costante (para evitar peleas con los otros pacientes)\n" +
                "- Suministrar una dosis de Femeh cada 7 horas, 1ml\n");
        }

        else if (gameObject.CompareTag("D_NecesidadesPAnton"))
        {
            paginasDelDocumento.Add("Necesidades del paciente:\n\n" +
                "6523789J, Ánton Oliveira\n\n" +
                "- Inyectar Leemo cada 24 horas\n" +
                "- Atención costante (para evitar peleas con los otros pacientes)\n" +
                "- Precaución al tener contacto con él (sobre todo las mujeres)\n" +
                "- Inyectar 3ml de Menova cada 6 horas\n" +
                "- Asegurarse de que no coja ningún utensilio que " +
                    "pueda esconder o utilizar contra otros pacientes\n" +
                "- Inyectar tranquilizante de 4ml si es necesario\n");
        }

        else if (gameObject.CompareTag("D_NecesidadesPCristobal"))
        {
            paginasDelDocumento.Add("Necesidades del paciente:\n\n" +
                "7548976A, Cristóbal García \n\n" +
                "- Suministrar una dosis de Femeh cada 5 horas, 2ml\n" +
                "- Inyectar tranquilizante tipo C cada 7 horas\n" +
                "- Curar hematomas cada 5 horas\n");
        }
    }
}
