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
                "6523789J, Anton Oliveira\n\n" +
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

        else if (gameObject.CompareTag("D_CorreccionConductaAnton"))
        {
            paginasDelDocumento.Add("Anton es el paciente más problemático " +
                "que tenemos actualmente. Su triste condición, sumada a " +
                "su terrible actitud y educación, le hacen una persona " +
                "terriblemente conflictiva.\n\n" +
                "Es deleznable que manoseé a las enfermeras " +
                "(sobre todo a Sasha) y les robe sus pertenencias para luego " +
                "esconderlas. Y no solo coge los bienes de nuestras amadas " +
                "enfermeras, sino también elementos indispensables del asilo, " +
                "como podría ser una llave de alta seguridad...\n\n");

            paginasDelDocumento.Add(
                "Los tranquilizantes pueden calmarlo temporalmente, pero no" +
                " son suficientes para modular su conducta. Así que, a modo de " +
                "castigo correctivo, sugiero que el señor Oliveira pase " +
                "un tiempo en la habitación de reclusión B. Mientras esté ahí," +
                " sólo se le darán los cuidados básicos, y no se le " +
                "permitirá bajo ningún concepto salir de la habitación " +
                "hasta el tiempo estipulado por el que decida " +
                "el doctor Karlheinz.\n\n" +
                "Espero que esto ayude a corregir su conducta. " +
                "Cualquier consulta, háganmela saber.\n\n" +
                "Enfermera A.Wilkes");
        }

        else if (gameObject.CompareTag("D_ResultadosProyectoN45P"))
        {
            paginasDelDocumento.Add("RESULTADOS DE LA PRUEBA DEL " +
                "PROYECTO N45P\n" +
                "--------------------------");

            paginasDelDocumento.Add(
                "Hombres:\n\n" +
                "C.García - Apto\n" +
                "A.Oliveira - Apto\n" +
                "L. Hidalgo - Fallecido\n" +
                "B. Robinson - Fallecido\n" +
                "M. Coen - Fallecido\n" +
                "M. Smith - Fallecido\n" +
                "T. Winston - Mutado\n");

            paginasDelDocumento.Add(
                "Mujeres:\n\n" +
                "A.Thompson - Fallecida\n" +
                "C. Martin - Fallecida\n" +
                "B. Clark - Fallecida\n" +
                "N. White - Fallecida\n" +
                "J. Allen - Fallecida\n" +
                "S. López - Fallecida\n" +
                "F. Ramírez - Fallecida\n");

            paginasDelDocumento.Add(
                "Resultados desastrosos. Ninguna mujer ha sido apta, " +
                "solo dos hombres (los más jóvenes). Aunque hemos " +
                "obtenido un resultado interesante: Un hombre ha mutado. " +
                "Por lo que investigaremos sus alteraciones genéticas " +
                "antes de eliminarlo.\n" +
                "Solicito su permiso para ello.\n\n" +
                "P.D: Debido a estos resultados, en las futuras " +
                "pruebas deberíamos de usar un rango de edad menor...");
        }

        else if (gameObject.CompareTag("D_AvisoCodigoCajaDeSeguridadSMaquinas"))
        {
            paginasDelDocumento.Add("Hace unos días, el equipo del " +
                "laboratorio trajo unas cuantas cajas de seguridad. " +
                "Alfred se encargó de una, y la guardó en la sala de " +
                "máquinas (que es su zona de trabajo).\n" +
                "Como está desaparecido y es posible que la señora Wilkes " +
                "quiera acceder a ella en algún momento espontáneo, " +
                "Alfred nos dijo que la contraseña para abrirla era su " +
                "día y mes de nacimiento (en formato: ddmm), " +
                "cosa que ya deberíamos saber...\n\n" +
                "Así que, si Wilkes os pregunta por dicha caja, " +
                "ya sabéis como responder.\n\n" +
                "Morgan Sanderson");
        }

        else if (gameObject.CompareTag("D_DiarioDeSharonParte1"))
        {
            paginasDelDocumento.Add("DIARIO DE SHARON (PARTE 1)\n" +
                "--------------------------");

            paginasDelDocumento.Add("13, Junio\n\n" +
                "¡Hoy hemos dado una sorpresa a Alfred! " +
                "¡Cumple 49 años! Es un gran trabajador y " +
                "aún más importante, una excelente persona. " +
                "Le hemos regalado entre todos un reloj dorado de Maren. " +
                "Le ha encantado, es un fanático de esa marca.\n" +
                "Por gente como él, sigo aquí. Alfred, Morgan, Sasha, " +
                "y el resto, son buenos compañeros... El problema es " +
                "la señorita Wilkes y el doctor Karlheinz... " +
                "Cuando alguno de los dos aparece por aquí, el buen rollo " +
                "se esfuma, y el ambiente se vuelve frío y negativo...\n");

            paginasDelDocumento.Add("Los pacientes estaban perfectamente " +
                "controlados aunque " +
                "estuviésemos celebrando el cumpleaños de Alfred, " +
                "sin embargo, ha llegado la señorita Wilkes y ha pensado " +
                "lo contrario, echándolo todo a perder... Lo peor es que " +
                "ha caído toda su ira contra el pobre Alfred, cuando el " +
                "hombre ni si quiera sabía que le habíamos " +
                "preparado nada...\n" + "Aunque tengo la sensación de que " +
                "la señorita Wilkes ha cargado contra él, no por el " +
                "cumpleaños, sino por algo anterior. No sé que hizo " +
                "Alfred, pero llevo notando " +
                "que Wilkes le tiene cierto rencor desde hace un tiempo...");

            paginasDelDocumento.Add("14, Junio\n\n" +
                "Aparte de los jefazos sin emociones, es la propia empresa " +
                "(Zesimov) la que me inquieta... No es bueno hablar mal de " +
                "la empresa para la que trabajas, pero estoy segura de " +
                "que Zesimov utiliza a los pacientes del asilo para " +
                "cosas malas... muy malas... " +
                "Me preocupa todo lo que estoy viendo...\n\n" +
                "17, Junio\n\n" +
                "Alfred ha desaparecido. Ha desaparecido y sé que Zesimov " +
                "tiene algo que ver.\n\n");

            paginasDelDocumento.Add("23, Junio\n\n" +
                "He tomado la decisión de que voy a meterme dónde " +
                "no debo para conseguir pruebas (con los problemas " +
                "que eso pueda conllevar). Cuando las tenga, se las " +
                "entregaré a mi hermana Rose, para que use su poder " +
                "mediático, y revele la verdad sobre Zesimov.\n" +
                "La cosa se va a poner fea...");
        }

        else if (gameObject.CompareTag("D_LaMancha"))
        {
            paginasDelDocumento.Add("¡LA MANCHA DE LA PARED! " +
                "¡AHÍ LO HE OCULTADO! ¡SÍ! ¡SÍ! ¡SÍ! " +
                "¡ME ENCANTA ESCONDER COSAS!");
        }

        else if (gameObject.CompareTag("D_NotaVictoriaAnton"))
        {
            paginasDelDocumento.Add("¡ME HE ENFRENTADO A LA LUZ! " +
                "¡SÍ, SÍ, SÍ, SÍ!\n" +
                "¡HE SOBREVIVIDO, Y EL RESTO NO!\n" +
                "¡YO EL GRAN ANTON HE DERROTADO A LA LUZ QUE NOS ACECHABA!\n" +
                "¡ERA REAL!\n\n" +
                "HE APARECIDO EN SU MUNDO DE AJEDREZ CON UNA PIPA.\n" +
                "LAS FICHAS DE AJEDREZ ME ATACARON, PERO PUDE CON ELLAS " +
                "USANDO LA PIPA, Y TAMBIÉN PUDE CON LA LUZ.\n\n" +
                "¡YO EL GRAN ANTON, PUDE CON LA LUZ! " +
                "¡Y EL RESTO NO!\n" +
                "¡JAJAJAJA!");
        }

        else if (gameObject.CompareTag("D_NotaDelDoctorKarlheinz"))
        {
            paginasDelDocumento.Add("Apolo: Dios de la muerte súbita, " +
                "de las plagas, y de las enfermedades... pero también, " +
                "el dios de la curación, y de la protección contra las " +
                "fuerzas malignas.\n" +
                "Patrón de la salud, la belleza, " +
                "la música, y las Bellas Artes.\n" +
                "Presidía las leyes de " +
                "la religión, y las constituciones de las ciudades.\n\n" +
                "Hacía a las personas conscientes de sus pecados, y " +
                "era el agente de su purificación...\n\n" +
                "Era temido por " +
                "los otros dioses... Su padre era el único que podía " +
                "contenerlo.\n\n" +
                "Apolo siempre iba cabalgando con sus grifos " +
                "por los cielos.\n" +
                "Y cada día, visitaba a una de sus hermosas musas.\n" +
                "Su favorita: Urania, musa de la Astronomía y la Astrología.");

            paginasDelDocumento.Add("Apolo y sus alrededores son mi inspiración" +
                " como creador de la nueva vida y purificación de los " +
                "pecados de la Tierra, y además, no se aleja de los " +
                "intereses de Zesimov.\n\n" +
                "La estatua de Urania y los grifos, " +
                "fueron costosos, pero hermosos. Útiles para la entrada al " +
                "nuevo mundo.\n\n" +
                "El ajedrez también es importante en la " +
                "construcción del nuevo génesis. Amo su estrategia, " +
                "y su jerarquía de poder.\n\n" +
                "Pronto empezaremos a crear el nuevo mundo. Zesimov espera " +
                "mucho de mí, y no decepcionaré. Nunca lo hago.\n\n" +
                "Dr. Raphael Karlheinz");
        }
    }
}
