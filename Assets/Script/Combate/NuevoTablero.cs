using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NuevoTablero : MonoBehaviour
{
    struct ObjetivoAtaque
    {
        public int lanzador;
        public int receptor;
        public int indiceAtaque;
        public Ataque ataqueAUsar;
    }

    struct ObjetivoObjeto
    {
        public int lanzador;
        public int receptor;
        public Objeto objetoAUsar;
    }

    BaseDatos baseDeDatos;

    Inventario inventario;

    GameObject manager;
    public GameObject aliado1, aliado2, aliado3, enemigo1, enemigo2, enemigo3, enemigoGrande1, enemigoGrande2, enemigoGigante;
    public GameObject fondoCombate;
    public GameObject flechaSeleccionPersonajeEnemigo;
    public GameObject controlDanio;
    public GameObject controlEfecto1, controlEfecto2, controlEfecto3, controlEfecto4, controlEfecto5, controlEfecto6;
    public GameObject[] listaObjetosRecompensa;
    public GameObject escenaFin;
    public GameObject flechaSeleccionPersonaje;
    public GameObject menuDeCombate;
    public GameObject menuDeHabilidades;
    public GameObject menuEstadisticas;
    public GameObject infoEnemigos;

    //public Sprite[] imagenesFondo;
    public Sprite flechaMas, flechaMenos, flechaDobleMas, flechaDobleMenos, simboloIgual;
    Sprite[] elementosInterfaz;

    public Image menuRecompensa;
    public Image premioDinero;
    public Image premioExp;
    public Image[] iconosObjetos;
    public Image[] cajasRecompensaPersonaje;
    public Image[] flechaEstadisticasMejora;
    public Image[] flechaEstadisticasEmpeoro;
    
    /*
     *  0 --> aliado1
     *  1 --> aliado2
     *  2 --> aliado3
     *  3 --> enemigoP1
     *  4 --> enemigoP2
     *  5 --> enemigoP3
     *  6 --> enemigoG1
     *  7 --> enemigoG2
     *  8 --> enemigoGig
    */

    Animator animadorAl1, animadorAl2, animadorAl3, animadorEn1, animadorEn2, animadorEn3;
    Animator controlDeDanio;
    Animator controlEfectoDanioAl1, controlEfectoDanioAl2, controlEfectoDanioAl3, controlEfectoDanioEn1, controlEfectoDanioEn2, controlEfectoDanioEn3;

    MusicaManager musica;

    public Personajes[] datosDelPersonaje = new Personajes[6];
    /*
        0 --> Prota  
        1 --> Aliado2
        2 --> Aliado3
        3 --> Enemigo1
        4 --> Enemigo2
        5 --> Enemigo3
    */
    ControlJugador controlJugador;
    Camara camara;
    
    GameOver finJuego;

    Ataque[] listaDeAtaques;
    ObjetivoAtaque[] listaDeObjetivos;

    //int[] vectorOrdenado;
    //int[] ordenVelocidad;
    int[] listaAcciones;
    /*
     * 0 --> Muerto
     * 1 --> Ataca
     * 2 --> Objeto
    */
    int[] listaDeFlechas;
    /*
     * -2 --> Muy negativo
     * -1 --> Negativo
     * 0 --> Igual
     * 1 --> Positivo
     * 2 --> Muy Positivo
    */

    public bool combateActivo = false;
    bool turnoJugador = false;
    bool turnoEnemigo = false;
    bool elegidoObjetivo;
    bool apuntadoIniciado = false;
    bool ataqueGuardado = false;
    bool posicionado = false;
    bool combateGanado = false;
    bool combatePerdido = false;
    bool animacionEnCurso = false;
    //bool[] listaDeAtacantes;
    bool pasandoAtaque = false;
    bool flechaEstadisticaActiva = false;
    bool aumenta = false;
    bool reduce = false;
    bool aplicaApoyo = false;
    bool pasandoTexto = false;
    bool huir = false;
    bool esperaFin = false;
    bool fallo = false;
    bool invocacionCargada;
    public bool iniciado;
    public bool historiaActiva;
    public bool recienSalido;
    bool fase1Activa;

    bool menuCombateActivo;
    bool menuHabilidadesActivo;
    bool usarObjeto, usarAtaque, usarPoder;
    bool retrocede;


    float separacionFlechaX;
    int indiceCombate;
    int nivelMin;
    int movimientoAliados, movimientoAliadoGastado, movimientoEnemigo, movimientoEnemigoGastado;
    int experienciaTotal, oroTotal;
    int posicionFlechaEnemiga;
    int proximaCamaraHistoria;
    int posicionFlechaAliado;
    int ataqueLanzado;
    int indiceFlechaMejora;
    int indiceVectorFlechas;
    int numeroMejoras;
    int indiceJefeFinal;
    int dificultad;
    /* 
     * 0 --> Facil     
     * 1 --> Intermedio
     * 2 --> Dificil
     * 3 --> Titán
    */

    bool pulsado;

    float digitalX;
    float digitalY;


    string[] mensajesMejora;

    public Text danioAliado1, danioAliado2, danioAliado3, danioEnemigoPeq1, danioEnemigoPeq2, danioEnemigoPeq3, danioEnemigoGran1, danioEnemigoGran2, danioEnemigoGig;
    public Text textoCombate;
    public Text dineroRecompensa, expRecompensa;
    public Text[] nombresPersonajes;
    public Text[] experienciaPersonajes;
    public Text[] nivelPersonajes;
    public Text[] nivelMas;
    public Text[] nombreObjetos;
    public Text[] cantidadesObjetos;

    ControlObjetos controlSecundarias;

    Vector3 posAl1, posAl2, posAl3, posEnP1, posEnP2, posEnP3, posEnG1, posEnG2, posEnGG;

    int filaHabilidad;
    int columnaHabilidad;


    void Awake()
    {
        iniciado = false;
        movimientoAliados = 0;
        movimientoEnemigo = 0;
    }



    void Start()
    {
        inventario = GameObject.Find("Inventario").transform.GetChild(0).GetComponent<Inventario>();
        manager = GameObject.Find("GameManager");
        musica = GameObject.Find("EfectosSonido").GetComponent<MusicaManager>();
        controlSecundarias = manager.GetComponent<ControlObjetos>();

        baseDeDatos = manager.GetComponent<BaseDatos>();

        for (int i = 0; i < 4; i++)
        {
            listaObjetosRecompensa[i].SetActive(false);
        }

        for(int i = 1; i < 3; i++)
        {
            cajasRecompensaPersonaje[i].gameObject.SetActive(false);
        }

        menuRecompensa.gameObject.SetActive(false);

        controlJugador = GameObject.Find("Player").GetComponent<ControlJugador>();
        camara = GameObject.Find("Main Camera").GetComponent<Camara>();
        

        //menuTexto.gameObject.SetActive(false);

        separacionFlechaX = 1;
        recienSalido = false;
        
        listaDeAtaques = new Ataque[6];
        listaDeObjetivos = new ObjetivoAtaque[6];
        //listaDeAtacantes = new bool[6];
        listaAcciones = new int[6];
        listaDeFlechas = new int[7];
        mensajesMejora = new string[7];
        //ordenVelocidad = new int[6];

        elementosInterfaz = new Sprite[19];
        elementosInterfaz = Resources.LoadAll<Sprite>("Sprites/Interfaz/Combate/ElementosInterfazCombate");

        this.TerminaCombate(false, true);

        controlEfecto1.SetActive(false);
        controlEfecto2.SetActive(false);
        controlEfecto3.SetActive(false);
        controlEfecto4.SetActive(false);
        controlEfecto5.SetActive(false);
        controlEfecto6.SetActive(false);

        posAl1 = aliado1.transform.position;
        posAl2 = aliado2.transform.position;
        posAl3 = aliado3.transform.position;

        posEnP1 = enemigo1.transform.position;
        posEnP2 = enemigo2.transform.position;
        posEnP3 = enemigo3.transform.position;

        posEnG1 = enemigoGrande1.transform.position;
        posEnG2 = enemigoGrande2.transform.position;

        posEnGG = enemigoGigante.transform.position;

        iniciado = true;

        finJuego = escenaFin.transform.GetComponent<GameOver>();

        DesactivarMenuCombate();
        DesactivarMenuHabilidades();
        DesactivaInfoEnemigo();
        usarObjeto = usarAtaque = usarPoder = false;
    }



    public void IniciarCombate(Personajes personajeEne1, Personajes personajeEne2, Personajes personajeEne3, int numeroEnemigos, int nivelMinE, bool historia, int proxHistoria, int indiceJefe)
    {
        if(baseDeDatos.idioma == 1)
        {
            menuDeCombate.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Invocation";
            menuDeCombate.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "Attack";
            menuDeCombate.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = "Inventory";
            menuDeCombate.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = "Run";
        }
        else
        {
            menuDeCombate.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Invocación";
            menuDeCombate.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Atacar";
            menuDeCombate.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Objetos";
            menuDeCombate.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Huir";
        }

        retrocede = false;
        indiceJefeFinal = indiceJefe;
        historiaActiva = historia;
        proximaCamaraHistoria = proxHistoria;
        aliado1.transform.position = posAl1;
        aliado2.transform.position = posAl2;
        aliado3.transform.position = posAl3;

        finJuego.espera = false;

        enemigo1.transform.position = posEnP1;
        enemigo2.transform.position = posEnP2;
        enemigo3.transform.position = posEnP3;

        enemigoGrande1.transform.position = posEnG1;
        enemigoGrande2.transform.position = posEnG2;

        enemigoGigante.transform.position = posEnGG;

        nivelMin = nivelMinE;
        dificultad = controlJugador.GetDificultad();

        camara.combate = true;
        flechaSeleccionPersonajeEnemigo.SetActive(false);
        fondoCombate.gameObject.SetActive(true);
       

        aplicaApoyo = false;
        pasandoTexto = false;

        controlDeDanio = controlDanio.GetComponent<Animator>();

        movimientoAliados = baseDeDatos.numeroIntegrantesEquipo;
        movimientoAliadoGastado = 0;
        ataqueLanzado = 0;
        indiceVectorFlechas = 0;
        numeroMejoras = 0;
        oroTotal = 0;

        IniciarAliados();

        //flechaSeleccionPersonaje.transform.position = new Vector3(aliado1.transform.position.x - separacionFlechaX, aliado1.transform.position.y, aliado1.transform.position.z);
        movimientoEnemigo = numeroEnemigos;
        movimientoEnemigoGastado = 0;

        IniciaEnemigos(personajeEne1, personajeEne2, personajeEne3);

        combateActivo = true;
        turnoJugador = true;
        elegidoObjetivo = false;
        combateGanado = false;
        combatePerdido = false;
        animacionEnCurso = false;
        turnoEnemigo = false;
        pasandoAtaque = false;

        posicionFlechaEnemiga = 0;
        posicionFlechaAliado = 0;

        animadorAl1 = aliado1.GetComponent<Animator>();
        animadorAl1.runtimeAnimatorController = datosDelPersonaje[0].animacion;
        controlEfectoDanioAl1 = controlEfecto1.GetComponent<Animator>();


        if (movimientoAliados > 1)
        {
            animadorAl2 = aliado2.GetComponent<Animator>();
            animadorAl2.runtimeAnimatorController = datosDelPersonaje[1].animacion;
            controlEfectoDanioAl2 = controlEfecto2.GetComponent<Animator>();
        }

        if (movimientoAliados > 2)
        {
            animadorAl3 = aliado3.GetComponent<Animator>();
            animadorAl3.runtimeAnimatorController = datosDelPersonaje[2].animacion;
            controlEfectoDanioAl3 = controlEfecto3.GetComponent<Animator>();
        }

        animadorEn1 = enemigo1.GetComponent<Animator>();
        animadorEn1.runtimeAnimatorController = datosDelPersonaje[3].animacion;
        controlEfectoDanioEn1 = controlEfecto4.GetComponent<Animator>();


        if (movimientoEnemigo > 1)
        {
            animadorEn2 = enemigo2.GetComponent<Animator>();
            animadorEn2.runtimeAnimatorController = datosDelPersonaje[4].animacion;
            controlEfectoDanioEn2 = controlEfecto5.GetComponent<Animator>();
        }
        if (movimientoEnemigo > 2)
        {
            animadorEn3 = enemigo3.GetComponent<Animator>();
            animadorEn3.runtimeAnimatorController = datosDelPersonaje[5].animacion;
            controlEfectoDanioEn3 = controlEfecto6.GetComponent<Animator>();
        }

        ActivarMenuCombate();
        DesactivarMenuHabilidades();
        StartCoroutine(EsperaInicio());
    }



    void IniciarAliados()
    {
        Personajes protagonista, personaje2, personaje3;
        personaje2 = null;
        personaje3 = null;

        baseDeDatos.equipoAliado[0].ataqueFisicoActual = baseDeDatos.equipoAliado[0].ataqueFisicoModificado;
        baseDeDatos.equipoAliado[0].defensaFisicaActual = baseDeDatos.equipoAliado[0].defensaFisicaModificada;
        baseDeDatos.equipoAliado[0].ataqueMagicoActual = baseDeDatos.equipoAliado[0].ataqueMagicoModificado;
        baseDeDatos.equipoAliado[0].defensaMagicaActual = baseDeDatos.equipoAliado[0].defensaMagicaModificada;
        baseDeDatos.equipoAliado[0].velocidadActual = baseDeDatos.equipoAliado[0].velocidadModificada;

        if(movimientoAliados > 1)
        {
            baseDeDatos.equipoAliado[1].ataqueFisicoActual = baseDeDatos.equipoAliado[1].ataqueFisicoModificado;
            baseDeDatos.equipoAliado[1].defensaFisicaActual = baseDeDatos.equipoAliado[1].defensaFisicaModificada;
            baseDeDatos.equipoAliado[1].ataqueMagicoActual = baseDeDatos.equipoAliado[1].ataqueMagicoModificado;
            baseDeDatos.equipoAliado[1].defensaMagicaActual = baseDeDatos.equipoAliado[1].defensaMagicaModificada;
            baseDeDatos.equipoAliado[1].velocidadActual = baseDeDatos.equipoAliado[1].velocidadModificada;
        }

        if(movimientoAliados > 2)
        {
            baseDeDatos.equipoAliado[2].ataqueFisicoActual = baseDeDatos.equipoAliado[2].ataqueFisicoModificado;
            baseDeDatos.equipoAliado[2].defensaFisicaActual = baseDeDatos.equipoAliado[2].defensaFisicaModificada;
            baseDeDatos.equipoAliado[2].ataqueMagicoActual = baseDeDatos.equipoAliado[2].ataqueMagicoModificado;
            baseDeDatos.equipoAliado[2].defensaMagicaActual = baseDeDatos.equipoAliado[2].defensaMagicaModificada;
            baseDeDatos.equipoAliado[2].velocidadActual = baseDeDatos.equipoAliado[2].velocidadModificada;
        }

        if (movimientoAliados == 1)
        {
            protagonista = baseDeDatos.equipoAliado[0];
            personaje2 = null;
            personaje3 = null;
        }
        else if (movimientoAliados == 2)
        {
            if (baseDeDatos.equipoAliado[0].vidaActual == 0)
            {
                protagonista = baseDeDatos.equipoAliado[1];
                movimientoAliados--;
                personaje2 = null;
            }
            else
            {
                protagonista = baseDeDatos.equipoAliado[0];

                if (baseDeDatos.equipoAliado[1].vidaActual == 0)
                {
                    personaje2 = null;
                    movimientoAliados--;
                }
                else
                {
                    personaje2 = baseDeDatos.equipoAliado[1];
                }
            }

            personaje3 = null;
        }
        else
        {
            if (baseDeDatos.equipoAliado[0].vidaActual == 0)
            {
                movimientoAliados--;

                if(baseDeDatos.equipoAliado[1].vidaActual == 0)
                {
                    protagonista = baseDeDatos.equipoAliado[2];
                    movimientoAliados--;
                    personaje2 = null;
                    personaje3 = null;
                }
                else
                {
                    protagonista = baseDeDatos.equipoAliado[1];

                    if (baseDeDatos.equipoAliado[2].vidaActual == 0)
                    {
                        personaje2 = null;
                        personaje3 = null;
                        movimientoAliados--;
                    }
                    else
                    {
                        personaje2 = baseDeDatos.equipoAliado[2];
                        personaje3 = null;
                    }
                }
            }
            else
            {
                if (baseDeDatos.equipoAliado[1].vidaActual == 0)
                {
                    protagonista = baseDeDatos.equipoAliado[0];

                    movimientoAliados--;

                    if (baseDeDatos.equipoAliado[2].vidaActual == 0)
                    {
                        personaje2 = null;
                        personaje3 = null;
                        movimientoAliados--;
                    }
                    else
                    {
                        personaje2 = baseDeDatos.equipoAliado[2];
                        personaje3 = null;
                    }
                }
                else
                {
                    protagonista = baseDeDatos.equipoAliado[0];
                    personaje2 = baseDeDatos.equipoAliado[1];

                    if (baseDeDatos.equipoAliado[2].vidaActual == 0)
                    {
                        personaje3 = null;
                        movimientoAliados--;
                    }
                    else
                    {
                        personaje3 = baseDeDatos.equipoAliado[2];
                    }
                }
            }
        }
        
        controlJugador.setCombateActivo(true);

        Sprite imagen;
        Sprite[] imagenes;

        if (baseDeDatos.faccion == 0) //golpista
        {
            imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/TrajesProta/Emperador");
            imagen = imagenes[1];
            aliado1.GetComponent<SpriteRenderer>().sprite = imagen;
        }
        else if (baseDeDatos.faccion == 1) //imperio
        {
            imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/TrajesProta/GuardiaImperial");
            imagen = imagenes[1];
            aliado1.GetComponent<SpriteRenderer>().sprite = imagen;
        }
        else if (baseDeDatos.faccion == 2) //regente
        {
            imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/TrajesProta/GuardiaReal");
            imagen = imagenes[1];
            aliado1.GetComponent<SpriteRenderer>().sprite = imagen;
        }
        else if (baseDeDatos.faccion == 3) //resistencia
        {
            imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/TrajesProta/Resistencia");
            imagen = imagenes[1];
            aliado1.GetComponent<SpriteRenderer>().sprite = imagen;
        }
        else if (baseDeDatos.faccion == 4) //R.Asalto
        {
            imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/TrajesProta/FuerzasAsalto");
            imagen = imagenes[1];
            aliado1.GetComponent<SpriteRenderer>().sprite = imagen;
        }
        else if (baseDeDatos.faccion == 5) //R.Especiales
        {
            imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/TrajesProta/EquipoFuerzasEspeciales");
            imagen = imagenes[1];
            aliado1.GetComponent<SpriteRenderer>().sprite = imagen;
        }
        else if (baseDeDatos.faccion == 6) //R.Investigación
        {
            imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/TrajesProta/EquipoInteligencia");
            imagen = imagenes[1];
            aliado1.GetComponent<SpriteRenderer>().sprite = imagen;
        }
        else if (baseDeDatos.faccion == 7) //Cúpula
        {
            imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/TrajesProta/Cupula");
            imagen = imagenes[1];
            aliado1.GetComponent<SpriteRenderer>().sprite = imagen;
        }
        else if (baseDeDatos.faccion == 8) //Ninguna
        {
            aliado1.GetComponent<SpriteRenderer>().sprite = protagonista.imagen;
        }
        else if (baseDeDatos.faccion == 9) //anarquista
        {
            imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/TrajesProta/Anarquista");
            imagen = imagenes[1];
            aliado1.GetComponent<SpriteRenderer>().sprite = imagen;
        }
        
        aliado1.GetComponent<Animator>().runtimeAnimatorController = protagonista.animacion;
        datosDelPersonaje[0] = protagonista;
        aliado1.SetActive(true);

        if (movimientoAliados > 1)
        {
            aliado2.GetComponent<SpriteRenderer>().sprite = personaje2.imagen;
            aliado2.GetComponent<Animator>().runtimeAnimatorController = personaje2.animacion;
            datosDelPersonaje[1] = personaje2;
            aliado2.SetActive(true);
        }


        if (movimientoAliados == 3)
        {
            aliado3.GetComponent<SpriteRenderer>().sprite = personaje3.imagen;
            aliado3.GetComponent<Animator>().runtimeAnimatorController = personaje3.animacion;
            datosDelPersonaje[2] = personaje3;
            aliado3.SetActive(true);
        }
    }



    void IniciaEnemigos(Personajes personajeEne1, Personajes personajeEne2, Personajes personajeEne3) //De haber un solo personaje grande este siempre irá en PersonajeEne1
    {
        if(movimientoEnemigo == 1)
        {
            if (personajeEne1.tipo == Personajes.tipoPersonaje.GIGANTE)
            {
                enemigoGigante.GetComponent<SpriteRenderer>().sprite = personajeEne1.imagen;
                enemigoGigante.SetActive(true);
            }
            else if (personajeEne1.tipo == Personajes.tipoPersonaje.GRANDE)
            {
                enemigoGrande1.GetComponent<SpriteRenderer>().sprite = personajeEne1.imagen;
                enemigoGrande1.SetActive(true);
            }
            else
            {
                enemigo1.GetComponent<SpriteRenderer>().sprite = personajeEne1.imagen;
                enemigo1.SetActive(true);
            }

            datosDelPersonaje[3] = personajeEne1;
            CalculaEstadisticas(3);
        }
        else if (movimientoEnemigo == 2)
        {
            if (personajeEne1.tipo == Personajes.tipoPersonaje.GRANDE)
            {
                enemigoGrande1.GetComponent<SpriteRenderer>().sprite = personajeEne1.imagen;
                enemigoGrande1.SetActive(true);

                if (personajeEne2.tipo == Personajes.tipoPersonaje.GRANDE)
                {
                    enemigoGrande2.GetComponent<SpriteRenderer>().sprite = personajeEne2.imagen;
                    enemigoGrande2.SetActive(true);
                }
                else
                {
                    enemigo3.GetComponent<SpriteRenderer>().sprite = personajeEne2.imagen;
                    enemigo3.SetActive(true);
                }
            }
            else
            {
                enemigo1.GetComponent<SpriteRenderer>().sprite = personajeEne1.imagen;
                enemigo1.SetActive(true);

                enemigo2.GetComponent<SpriteRenderer>().sprite = personajeEne2.imagen;
                enemigo2.SetActive(true);
            }

            datosDelPersonaje[3] = personajeEne1;
            CalculaEstadisticas(3);
            datosDelPersonaje[4] = personajeEne2;
            CalculaEstadisticas(4);

        }
        else
        {
            enemigo1.GetComponent<SpriteRenderer>().sprite = personajeEne1.imagen;
            enemigo1.SetActive(true);

            enemigo2.GetComponent<SpriteRenderer>().sprite = personajeEne2.imagen;
            enemigo2.SetActive(true);

            enemigo3.GetComponent<SpriteRenderer>().sprite = personajeEne3.imagen;
            enemigo3.SetActive(true);

            datosDelPersonaje[3] = personajeEne1;
            CalculaEstadisticas(3);
            datosDelPersonaje[4] = personajeEne2;
            CalculaEstadisticas(4);
            datosDelPersonaje[5] = personajeEne3;
            CalculaEstadisticas(5);
        }
    }



    void TerminaCombate(bool victoria, bool huir)
    {
        if (victoria)
        {
            if (controlSecundarias.misionPedroActiva)
            {
                for (int i = 0; i < movimientoEnemigo; i++)
                {
                    if (controlSecundarias.contadorMonstruosPedro != 10)
                    {
                        if (datosDelPersonaje[3 + i].indicePersonaje == 18)
                        {
                            if (datosDelPersonaje[3 + i].vidaActual == 0)
                            {
                                controlSecundarias.contadorMonstruosPedro++;
                            }
                        }
                    }
                }

                if (baseDeDatos.idioma == 1)
                {
                    baseDeDatos.listaMisionesReclutamiento[controlSecundarias.posicionMisionPedro].estadoActual[0] = "- Enemies defeated " + controlSecundarias.contadorMonstruosPedro + "/10.";
                    baseDeDatos.listaMisionesActivas[controlSecundarias.posicionGeneralMisionPedro].estadoActual[0] = "- Enemies defeated " + controlSecundarias.contadorMonstruosPedro + "/10.";
                }
                else
                {
                    baseDeDatos.listaMisionesReclutamiento[controlSecundarias.posicionMisionPedro].estadoActual[0] = "- Enemigos derrotados " + controlSecundarias.contadorMonstruosPedro + "/10.";
                    baseDeDatos.listaMisionesActivas[controlSecundarias.posicionGeneralMisionPedro].estadoActual[0] = "- Enemigos derrotados " + controlSecundarias.contadorMonstruosPedro + "/10.";
                }
            }

            if (controlSecundarias.misionGamezActiva)
            {
                if (victoria && indiceJefeFinal == 1)
                {
                    controlSecundarias.Ladron.SetActive(false);
                    controlJugador.SetConversacion(false);
                }
            }
        }

        if (historiaActiva)
        {
            if (victoria)
            {
                GanaCombate();
            }
            else
            {
                StartCoroutine(EsperaGameOver());
            }
        }
        else
        {
            if (victoria)
            {
                GanaCombate();
            }
            else if (huir)
            {
                StartCoroutine(EsperaFinCombate());
            }
            else
            {
                StartCoroutine(EsperaGameOver());
            }
        }

        TextBox.OcultaTextoFinCombate();
    }

   

    void Update()
    {
        if (finJuego.espera)
        {
            StartCoroutine(EsperaFinCombate());
        }
        else
        {
            if (combateActivo)
            {
                if (movimientoAliados == 1 && datosDelPersonaje[0].vidaActual == 0)
                {
                    combatePerdido = true;
                }
                else if (movimientoAliados == 2 && datosDelPersonaje[0].vidaActual == 0 && datosDelPersonaje[1].vidaActual == 0)
                {
                    combatePerdido = true;
                }
                else if (movimientoAliados == 3 && datosDelPersonaje[0].vidaActual == 0 && datosDelPersonaje[1].vidaActual == 0 && datosDelPersonaje[2].vidaActual == 0)
                {
                    combatePerdido = true;
                }

                if (!esperaFin)
                {
                    if (!animacionEnCurso && !pasandoTexto)
                    {
                        if (!combateGanado && !combatePerdido)
                        {
                            if (turnoJugador)
                            {
                                Fase1();
                            }
                            else if (turnoEnemigo)
                            {
                                Fase2();
                            }
                            else if (!turnoEnemigo && !turnoJugador && !pasandoAtaque)
                            {
                                Fase3();
                            }
                        }
                        else if (combatePerdido)
                        {
                            TerminaCombate(false, false);
                        }

                        if (TextBox.ocultar)
                        {
                            if (combateGanado)
                            {
                                //StartCoroutine(EsperaFinCombate(true));
                                TerminaCombate(true, false);
                            }
                            else if (combatePerdido)
                            {
                                TerminaCombate(false, false);
                                //StartCoroutine(EsperaFinCombate(false));
                            }
                        }
                    }
                    else
                    {
                        if (pasandoTexto)
                        {
                            menuEstadisticas.gameObject.SetActive(false);
                        }

                        if (flechaEstadisticaActiva)
                        {
                            if (aumenta)
                            {
                                if (flechaEstadisticasMejora[indiceFlechaMejora].gameObject.transform.localScale.y <= 1)
                                {
                                    flechaEstadisticasMejora[indiceFlechaMejora].gameObject.transform.localScale += new Vector3(0, 0.05f, 0);
                                }
                            }
                            else if (reduce)
                            {
                                if (flechaEstadisticasEmpeoro[indiceFlechaMejora].gameObject.transform.localScale.y <= 1)
                                {
                                    flechaEstadisticasEmpeoro[indiceFlechaMejora].gameObject.transform.localScale += new Vector3(0, 0.05f, 0);
                                }
                            }
                            else
                            {
                                if (flechaEstadisticasMejora[indiceFlechaMejora].gameObject.transform.localScale.y <= 1)
                                {
                                    flechaEstadisticasMejora[indiceFlechaMejora].gameObject.transform.localScale += new Vector3(0, 0.05f, 0);
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                    {
                        StartCoroutine(EsperaFinCombate());
                    }
                }
            }
        }
    }



    void GanaCombate()
    {
        if(indiceJefeFinal != -1)
        {
            switch (indiceJefeFinal)
            {
                case 0: //Luis El Paso
                    baseDeDatos.CumpleMision(9);
                    break;
                case 1: //Ladrón Gamez
                    baseDeDatos.ApagaPersonajes(0);
                    break;
                case 2: //Orco Nani
                    baseDeDatos.ApagaPersonajes(3);
                    controlSecundarias.orcoDerrotado = true;
                    break;
                case 3: //Guardia Nani
                    baseDeDatos.ApagaPersonajes(4);
                    controlSecundarias.guardiaDerrotado = true;
                    break;
            }
        }

        CalculaRecompensa();
        MusicaManager cambio = GameObject.Find("Musica").GetComponent<MusicaManager>();
        cambio.CambiaCancion(14);
        StartCoroutine(EsperaMenuRecompensa());
    }



    void CalculaRecompensa()
    {
        int recompensas = 0;
        int aux = -1;
        int numeroAleatorio = Random.Range(0,101);
        int cantidad;
        experienciaTotal = 0;

        for(int i = 0; i < movimientoEnemigo; i++)
        {
            experienciaTotal += (int)((datosDelPersonaje[3 + i].experiencia * datosDelPersonaje[3+i].nivel * 1.5f) / 7.0f);
            oroTotal += datosDelPersonaje[3 + i].dinero;
            cantidad = 0;

            if (datosDelPersonaje[3 + i].objetoRecompensa != -1)
            {
                if (baseDeDatos.listaObjetos[datosDelPersonaje[3 + i].objetoRecompensa].tipoRareza == Objeto.rareza.COMUN)
                {
                    if ((numeroAleatorio) > 60)
                    {
                        listaObjetosRecompensa[recompensas].SetActive(true);
                        iconosObjetos[recompensas].sprite = baseDeDatos.listaObjetos[datosDelPersonaje[3 + i].objetoRecompensa].icono;

                        if(baseDeDatos.idioma == 1)
                        {
                            nombreObjetos[recompensas].text = baseDeDatos.listaObjetos[datosDelPersonaje[3 + i].objetoRecompensa].nombreIngles;
                        }
                        else
                        {
                            nombreObjetos[recompensas].text = baseDeDatos.listaObjetos[datosDelPersonaje[3 + i].objetoRecompensa].nombre;
                        }

                        numeroAleatorio = Random.Range(0, 101);

                        if (numeroAleatorio < 35)
                        {
                            cantidad = 1;
                        }
                        else if (numeroAleatorio >= 35 && numeroAleatorio < 60)
                        {
                            cantidad = 2;
                        }
                        else if (numeroAleatorio >= 60 && numeroAleatorio < 80)
                        {
                            cantidad = 3;
                        }
                        else if (numeroAleatorio >= 80 && numeroAleatorio < 95)
                        {
                            cantidad = 4;
                        }

                        cantidadesObjetos[recompensas].text = "" + cantidad;
                        recompensas++;
                    }
                }
                else if (baseDeDatos.listaObjetos[datosDelPersonaje[3 + i].objetoRecompensa].tipoRareza == Objeto.rareza.RARO)
                {
                    if ((numeroAleatorio) > 70)
                    {
                        listaObjetosRecompensa[recompensas].SetActive(true);
                        iconosObjetos[recompensas].sprite = baseDeDatos.listaObjetos[datosDelPersonaje[3 + i].objetoRecompensa].icono;

                        if(baseDeDatos.idioma == 1)
                        {
                            nombreObjetos[recompensas].text = baseDeDatos.listaObjetos[datosDelPersonaje[3 + i].objetoRecompensa].nombreIngles;
                        }
                        else
                        {
                            nombreObjetos[recompensas].text = baseDeDatos.listaObjetos[datosDelPersonaje[3 + i].objetoRecompensa].nombre;
                        }

                        numeroAleatorio = Random.Range(0, 101);

                        if (numeroAleatorio < 45)
                        {
                            cantidad = 1;
                        }
                        else if (numeroAleatorio >= 45 && numeroAleatorio < 75)
                        {
                            cantidad = 2;
                        }
                        else if (numeroAleatorio >= 75 && numeroAleatorio < 90)
                        {
                            cantidad = 3;
                        }
                        

                        cantidadesObjetos[recompensas].text = "" + cantidad;
                        recompensas++;
                    }
                }
                else if (baseDeDatos.listaObjetos[datosDelPersonaje[3 + i].objetoRecompensa].tipoRareza == Objeto.rareza.MITICO)
                {
                    if ((numeroAleatorio) > 80)
                    {
                        listaObjetosRecompensa[recompensas].SetActive(true);
                        iconosObjetos[recompensas].sprite = baseDeDatos.listaObjetos[datosDelPersonaje[3 + i].objetoRecompensa].icono;

                        if(baseDeDatos.idioma == 1)
                        {
                            nombreObjetos[recompensas].text = baseDeDatos.listaObjetos[datosDelPersonaje[3 + i].objetoRecompensa].nombreIngles;
                        }
                        else
                        {
                            nombreObjetos[recompensas].text = baseDeDatos.listaObjetos[datosDelPersonaje[3 + i].objetoRecompensa].nombre;
                        }

                        numeroAleatorio = Random.Range(0, 101);

                        if (numeroAleatorio < 45)
                        {
                            cantidad = 1;
                        }
                        else if (numeroAleatorio >= 45 && numeroAleatorio < 75)
                        {
                            cantidad = 2;
                        }

                        cantidadesObjetos[recompensas].text = "" + cantidad;
                        recompensas++;
                    }
                }
                else if (baseDeDatos.listaObjetos[datosDelPersonaje[3 + i].objetoRecompensa].tipoRareza == Objeto.rareza.LEGENDARIO)
                {
                    if ((numeroAleatorio) > 10)
                    {
                        listaObjetosRecompensa[recompensas].SetActive(true);
                        iconosObjetos[recompensas].sprite = baseDeDatos.listaObjetos[datosDelPersonaje[3 + i].objetoRecompensa].icono;

                        if(baseDeDatos.idioma == 1)
                        {
                            nombreObjetos[recompensas].text = baseDeDatos.listaObjetos[datosDelPersonaje[3 + i].objetoRecompensa].nombreIngles;
                        }
                        else
                        {
                            nombreObjetos[recompensas].text = baseDeDatos.listaObjetos[datosDelPersonaje[3 + i].objetoRecompensa].nombre;
                        }

                        cantidad = 1;

                        cantidadesObjetos[recompensas].text = "" + cantidad;
                        recompensas++;
                    }
                }

                if (recompensas > aux)
                {
                    baseDeDatos.IncluirEnInventario(datosDelPersonaje[3 + i].objetoRecompensa, cantidad);
                    aux = recompensas;
                }
            }

        }
        

        for (int j = 0; j < movimientoAliados; j++)
        {
            if(datosDelPersonaje[j].vidaActual > 0)
            {
                datosDelPersonaje[j].experiencia += experienciaTotal;
            }

            bool subeNivel = false;

            if (datosDelPersonaje[j].experiencia >= datosDelPersonaje[j].proximoNivel)
            {
                nivelMas[j].gameObject.SetActive(true);

                while(datosDelPersonaje[j].experiencia >= datosDelPersonaje[j].proximoNivel)
                {
                    baseDeDatos.SubirNivelAliado(j);
                    subeNivel = true;
                }

                if (subeNivel)
                {
                    musica.ProduceEfecto(5);
                }
            }

            nombresPersonajes[j].text = "" + datosDelPersonaje[j].nombre;
            experienciaPersonajes[j].text = "" + datosDelPersonaje[j].experiencia + "/" + datosDelPersonaje[j].proximoNivel;
            nivelPersonajes[j].text = "" + datosDelPersonaje[j].nivel;
            cajasRecompensaPersonaje[j].gameObject.SetActive(true);
        }

        controlJugador.dinero += oroTotal;

        dineroRecompensa.text = "" + oroTotal;
        expRecompensa.text = "" + experienciaTotal;

    }



    bool PuedeAtacar(int indiceLanzador)//Falta incluir problemas de estado y otros ataques que impidan atacar
    {
        bool puedeAtacar = false;
        int indiceEscape = listaDeObjetivos[indiceLanzador].lanzador;

        if(indiceEscape != -1)
        {
            if (indiceLanzador == 0)
            {
                if (datosDelPersonaje[0].vidaActual != 0)
                {
                    puedeAtacar = true;
                }
            }
            else if (indiceLanzador == 1)
            {
                if (movimientoAliados > 1)
                {
                    if (datosDelPersonaje[1].vidaActual != 0)
                    {
                        puedeAtacar = true;
                    }
                }
            }
            else if (indiceLanzador == 2)
            {
                if (movimientoAliados > 2)
                {
                    if (datosDelPersonaje[2].vidaActual != 0)
                    {
                        puedeAtacar = true;
                    }
                }
            }
            else if (indiceLanzador == 3)
            {
                if (datosDelPersonaje[3].vidaActual != 0)
                {
                    puedeAtacar = true;
                }
            }
            else if (indiceLanzador == 4)
            {
                if (movimientoEnemigo > 1)
                {
                    if (datosDelPersonaje[4].vidaActual != 0)
                    {
                        puedeAtacar = true;
                    }
                }
            }
            else if (indiceLanzador == 5)
            {
                if (movimientoEnemigo > 2)
                {
                    if (datosDelPersonaje[5].vidaActual != 0)
                    {
                        puedeAtacar = true;
                    }
                }
            }

            int indiceReceptor = listaDeObjetivos[indiceLanzador].receptor;

            if (datosDelPersonaje[indiceReceptor].vidaActual == 0)
            {
                puedeAtacar = false;
            }
        }

        return puedeAtacar;
    }


    
    void SeleccionarAliadoAtacante()
    {
        if (movimientoAliadoGastado == 0)
        {
            if (datosDelPersonaje[0].vidaActual != 0)
            {
                ActivarMenuCombate();
                posicionado = true;
            }
            else
            {
                movimientoAliadoGastado++;
                menuEstadisticas.transform.GetChild(0).GetComponent<Image>().sprite = elementosInterfaz[17];
            }
        }
        else if (movimientoAliados > 1 && movimientoAliadoGastado == 1)
        {
            if (datosDelPersonaje[1].vidaActual != 0)
            {
                ActivarMenuCombate();

                posicionado = true;
            }
            else
            {
                movimientoAliadoGastado++;
                menuEstadisticas.transform.GetChild(1).GetComponent<Image>().sprite = elementosInterfaz[17];
            }
        }
        else if (movimientoAliados == 3 && movimientoAliadoGastado == 2)
        {
            if (datosDelPersonaje[2].vidaActual != 0)
            {
                ActivarMenuCombate();

                posicionado = true;
            }
            else
            {
                movimientoAliadoGastado++;
                menuEstadisticas.transform.GetChild(2).GetComponent<Image>().sprite = elementosInterfaz[17];
            }
        }


        if (movimientoAliadoGastado == movimientoAliados)
        {
            TerminaTurnoAliado();
        }
    }



    void RealizaAccion()
    {
        if (usarAtaque)
        {
            int ataqueAUsar;

            if (!ataqueGuardado)
            {
                if (filaHabilidad == 1 && columnaHabilidad == 1)
                {
                    ataqueAUsar = 0;
                }
                else if (filaHabilidad == 1 && columnaHabilidad == 2)
                {
                    ataqueAUsar = 1;
                }
                else if (filaHabilidad == 2 && columnaHabilidad == 1)
                {
                    ataqueAUsar = 2;
                }
                else
                {
                    ataqueAUsar = 3;
                }

                listaDeObjetivos[movimientoAliadoGastado].indiceAtaque = ataqueAUsar;
                listaDeAtaques[movimientoAliadoGastado] = datosDelPersonaje[movimientoAliadoGastado].habilidades[ataqueAUsar];
                ataqueGuardado = true;
            }
            
            if (!elegidoObjetivo && ataqueGuardado)
            {
                digitalX = Input.GetAxis("D-Horizontal");
                digitalY = Input.GetAxis("D-Vertical");

                if (pulsado)
                {
                    if (digitalY == 0 && digitalX == 0)
                    {
                        pulsado = false;
                    }
                }

                if (!apuntadoIniciado)
                {
                    if (listaDeAtaques[movimientoAliadoGastado].tipo == Ataque.tipoAtaque.FISICO || listaDeAtaques[movimientoAliadoGastado].tipo == Ataque.tipoAtaque.MAGICO || listaDeAtaques[movimientoAliadoGastado].tipo == Ataque.tipoAtaque.APOYO_NEGATIVO)
                    {
                        IniciarApuntado(false, movimientoAliadoGastado);
                    }
                    else
                    {
                        IniciarApuntado(true, movimientoAliadoGastado);
                    }
                }
                else if (!menuCombateActivo && !menuHabilidadesActivo)
                {
                    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || (!pulsado && digitalY > 0))
                    {
                        pulsado = true;
                        musica.ProduceEfecto(11);
                        if (listaDeAtaques[movimientoAliadoGastado].tipo == Ataque.tipoAtaque.APOYO_POSITIVO)
                        {
                            MueveFlecha(true, -1);
                        }
                        else
                        {
                            MueveFlecha(false, -1);
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || (!pulsado && digitalY < 0))
                    {
                        pulsado = true;
                        musica.ProduceEfecto(11);

                        if (listaDeAtaques[movimientoAliadoGastado].tipo == Ataque.tipoAtaque.APOYO_POSITIVO)
                        {
                            MueveFlecha(true, 1);
                        }
                        else
                        {
                            MueveFlecha(false, 1);
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                    {
                        musica.ProduceEfecto(10);

                        if (listaDeAtaques[movimientoAliadoGastado].tipo == Ataque.tipoAtaque.APOYO_POSITIVO || listaDeAtaques[movimientoAliadoGastado].tipo == Ataque.tipoAtaque.APOYO_MIXTO)
                        {
                            AsignaObjetivoAliado(true);
                        }
                        else
                        {
                            AsignaObjetivoAliado(false);
                        }
                        DesactivaInfoEnemigo();
                        elegidoObjetivo = true;
                    }
                    else if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("B"))
                    {
                        musica.ProduceEfecto(12);
                        ataqueGuardado = false;
                        apuntadoIniciado = false;
                        elegidoObjetivo = false;
                        huir = false;

                        flechaSeleccionPersonajeEnemigo.SetActive(false);
                        flechaSeleccionPersonaje.SetActive(false);

                        DesactivaInfoEnemigo();
                        ActivarMenuHabilidades();
                        retrocede = true;
                    }
                }
            }
            else
            {
                usarAtaque = false;
                ataqueGuardado = false;
                ReiniciaAccion();
            }
        }
        else if (usarObjeto)
        {
            if (!inventario.activo)
            {
                usarObjeto = false;

                if (inventario.realizaAccion)
                {
                    listaDeObjetivos[movimientoAliadoGastado].lanzador = -1;
                    ReiniciaAccion();
                }
                else
                {
                    ActivarMenuCombate();
                }
            }
        }
        else if (huir)
        {
            string mensaje = "";

            if (historiaActiva)
            {
                if(baseDeDatos.idioma == 1)
                {
                    mensaje = "You mustn't run from this fight";
                }
                else
                {
                    mensaje = "No puedes huir de este combate";
                }
                
                StartCoroutine(EsperaTextoGeneral(mensaje, false));
                listaDeObjetivos[movimientoAliadoGastado].lanzador = -1;
                huir = false;
                ReiniciaAccion();
            }
            else
            {
                int F = 0;
                int A = datosDelPersonaje[movimientoAliadoGastado].velocidadActual;
                int B = datosDelPersonaje[3].velocidadActual;
                int C = Random.Range(0, 4);

                F = ((A * 128 / B) + 30 * C) % 256;

                C = Random.Range(0, 256);

                if (C <= F)
                {
                    if (baseDeDatos.idioma == 1)
                    {
                        mensaje = "You run away";
                    }
                    else
                    {
                        mensaje = "Has logrado huir";
                    }
                    
                    StartCoroutine(EsperaTextoGeneral(mensaje, true));
                }
                else
                {
                    if (baseDeDatos.idioma == 1)
                    {
                        mensaje = "You could not escape";
                    }
                    else
                    {
                        mensaje = "No has podido huir";
                    }
                    
                    StartCoroutine(EsperaTextoGeneral(mensaje, false));
                    listaDeObjetivos[movimientoAliadoGastado].lanzador = -1;
                    huir = true;
                    ReiniciaAccion();
                }
            }
            
        }
    }



    void TerminaTurnoAliado()
    {
        DesactivarMenuCombate();
        turnoJugador = false;
        turnoEnemigo = true;
        flechaSeleccionPersonajeEnemigo.SetActive(false);
        MueveFlecha(true, 1);
        flechaSeleccionPersonaje.SetActive(false);
        movimientoEnemigoGastado = 0;
    }



    void TerminaTurnoEnemigo()
    {
        TextBox.OcultaTextoFinCombate();
        ActivarMenuCombate();
        menuEstadisticas.gameObject.SetActive(true);
        turnoJugador = true;
        movimientoAliadoGastado = -1;
        ReiniciaAccion();
        ataqueLanzado = 0;
    }



    void IniciarApuntado(bool aliado, int lanzador)
    {
        apuntadoIniciado = true;

        if (!aliado)
        {
            flechaSeleccionPersonajeEnemigo.SetActive(true);

            if (movimientoEnemigo == 3)
            {
                if (datosDelPersonaje[3].vidaActual != 0)
                {
                    posicionFlechaEnemiga = 1;
                }
                else if (datosDelPersonaje[4].vidaActual != 0)
                {
                    posicionFlechaEnemiga = 2;
                }
                else
                {
                    posicionFlechaEnemiga = 3;
                }
            }
            else if (movimientoEnemigo == 2)
            {
                if (datosDelPersonaje[3].vidaActual != 0)
                {
                    posicionFlechaEnemiga = 1;
                }
                else if (datosDelPersonaje[4].vidaActual != 0)
                {
                    posicionFlechaEnemiga = 2;
                }
            }
            else
            {
                posicionFlechaEnemiga = 1;
            }

            MueveFlecha(false, 0);
        }
        else
        {
            flechaSeleccionPersonaje.SetActive(true);

            if (movimientoAliados == 3)
            {
                if (datosDelPersonaje[0].vidaActual != 0)
                {
                    posicionFlechaAliado = 1;
                }
                else if (datosDelPersonaje[1].vidaActual != 0)
                {
                    posicionFlechaAliado = 2;
                }
                else
                {
                    posicionFlechaAliado = 3;
                }
            }
            else if (movimientoAliados == 2)
            {
                if (datosDelPersonaje[0].vidaActual != 0)
                {
                    posicionFlechaAliado = 1;
                }
                else if (datosDelPersonaje[1].vidaActual != 0)
                {
                    posicionFlechaAliado = 2;
                }
            }
            else
            {
                posicionFlechaAliado = 1;
            }

            MueveFlecha(true, 0);
        }
    }



    void MueveFlecha(bool aliado, int movimiento)
    {
        int posicionFlechaOriginal;

        if (!aliado)
        {
            posicionFlechaOriginal = posicionFlechaEnemiga;

            posicionFlechaEnemiga += movimiento;

            if (movimientoEnemigo == 3)
            {
                if (posicionFlechaEnemiga < 1)
                {
                    posicionFlechaEnemiga = 3;
                }
                else if (posicionFlechaEnemiga > 3)
                {
                    posicionFlechaEnemiga = 1;
                }

                if (posicionFlechaEnemiga == 1)
                {
                    if (datosDelPersonaje[3].vidaActual != 0)
                    {
                        flechaSeleccionPersonajeEnemigo.transform.position = new Vector3(enemigo1.transform.position.x + separacionFlechaX, enemigo1.transform.position.y, enemigo1.transform.position.z);
                    }
                    else
                    {
                        if (posicionFlechaOriginal == 3)
                        {
                            if (datosDelPersonaje[4].vidaActual != 0)
                            {
                                posicionFlechaEnemiga = 2;
                            }
                            else
                            {
                                posicionFlechaEnemiga = 3;
                            }
                        }
                        else if (posicionFlechaOriginal == 2)
                        {
                            if (datosDelPersonaje[5].vidaActual != 0)
                            {
                                posicionFlechaEnemiga = 3;
                            }
                            else
                            {
                                posicionFlechaEnemiga = 2;
                            }
                        }
                    }
                }

                if (posicionFlechaEnemiga == 2)
                {
                    if (datosDelPersonaje[4].vidaActual != 0)
                    {
                        flechaSeleccionPersonajeEnemigo.transform.position = new Vector3(enemigo2.transform.position.x + separacionFlechaX, enemigo2.transform.position.y, enemigo2.transform.position.z);
                    }
                    else
                    {
                        if (posicionFlechaOriginal == 1)
                        {
                            if (datosDelPersonaje[5].vidaActual != 0)
                            {
                                posicionFlechaEnemiga = 3;
                            }
                            else
                            {
                                posicionFlechaEnemiga = 1;
                            }
                        }
                        else if (posicionFlechaOriginal == 3)
                        {
                            if (datosDelPersonaje[3].vidaActual != 0)
                            {
                                posicionFlechaEnemiga = 1;
                                flechaSeleccionPersonajeEnemigo.transform.position = new Vector3(enemigo1.transform.position.x + separacionFlechaX, enemigo1.transform.position.y, enemigo1.transform.position.z);
                            }
                            else
                            {
                                posicionFlechaEnemiga = 3;
                            }
                        }
                    }
                }

                if (posicionFlechaEnemiga == 3)
                {
                    if (datosDelPersonaje[5].vidaActual != 0)
                    {
                        flechaSeleccionPersonajeEnemigo.transform.position = new Vector3(enemigo3.transform.position.x + separacionFlechaX, enemigo3.transform.position.y, enemigo3.transform.position.z);
                    }
                    else
                    {
                        if (posicionFlechaOriginal == 1)
                        {
                            if (datosDelPersonaje[4].vidaActual != 0)
                            {
                                posicionFlechaEnemiga = 2;
                                flechaSeleccionPersonajeEnemigo.transform.position = new Vector3(enemigo2.transform.position.x + separacionFlechaX, enemigo2.transform.position.y, enemigo2.transform.position.z);
                            }
                            else if (posicionFlechaOriginal == 2)
                            {
                                posicionFlechaEnemiga = 1;
                            }
                        }
                        else if (posicionFlechaOriginal == 2)
                        {
                            if (datosDelPersonaje[3].vidaActual != 0)
                            {
                                posicionFlechaEnemiga = 1;
                                flechaSeleccionPersonajeEnemigo.transform.position = new Vector3(enemigo1.transform.position.x + separacionFlechaX, enemigo1.transform.position.y, enemigo1.transform.position.z);
                            }
                            else if (posicionFlechaOriginal == 2)
                            {
                                posicionFlechaEnemiga = 2;
                            }
                        }
                    }
                }
            }
            else if (movimientoEnemigo == 2)
            {
                if (posicionFlechaEnemiga < 1)
                {
                    posicionFlechaEnemiga = 2;
                }
                else if (posicionFlechaEnemiga > 2)
                {
                    posicionFlechaEnemiga = 1;
                }

                if (posicionFlechaEnemiga == 1)
                {
                    if (datosDelPersonaje[3].vidaActual != 0)
                    {
                        if (datosDelPersonaje[3].tipo == Personajes.tipoPersonaje.GRANDE)
                        {
                            flechaSeleccionPersonajeEnemigo.transform.position = new Vector3(enemigoGrande1.transform.position.x + separacionFlechaX, enemigoGrande1.transform.position.y, enemigoGrande1.transform.position.z);
                        }
                        else
                        {
                            flechaSeleccionPersonajeEnemigo.transform.position = new Vector3(enemigo1.transform.position.x + separacionFlechaX, enemigo1.transform.position.y, enemigo1.transform.position.z);
                        }
                    }
                    else
                    {
                        posicionFlechaEnemiga++;
                    }
                }

                if (posicionFlechaEnemiga == 2)
                {
                    if (datosDelPersonaje[4].vidaActual != 0)
                    {
                        if (datosDelPersonaje[4].tipo == Personajes.tipoPersonaje.GRANDE)
                        {
                            flechaSeleccionPersonajeEnemigo.transform.position = new Vector3(enemigoGrande2.transform.position.x + separacionFlechaX, enemigoGrande1.transform.position.y, enemigoGrande1.transform.position.z);
                        }
                        else
                        {
                            if (datosDelPersonaje[3].tipo == Personajes.tipoPersonaje.GRANDE)
                            {
                                flechaSeleccionPersonajeEnemigo.transform.position = new Vector3(enemigo3.transform.position.x + separacionFlechaX, enemigo3.transform.position.y, enemigo3.transform.position.z);
                            }
                            else
                            {
                                flechaSeleccionPersonajeEnemigo.transform.position = new Vector3(enemigo2.transform.position.x + separacionFlechaX, enemigo2.transform.position.y, enemigo2.transform.position.z);
                            }
                        }
                    }
                    else
                    {
                        posicionFlechaEnemiga = 1;

                        if (datosDelPersonaje[3].tipo == Personajes.tipoPersonaje.GRANDE)
                        {
                            flechaSeleccionPersonajeEnemigo.transform.position = new Vector3(enemigoGrande1.transform.position.x + separacionFlechaX, enemigoGrande1.transform.position.y, enemigoGrande1.transform.position.z);
                        }
                        else
                        {
                            flechaSeleccionPersonajeEnemigo.transform.position = new Vector3(enemigo1.transform.position.x + separacionFlechaX, enemigo1.transform.position.y, enemigo1.transform.position.z);
                        }
                    }
                }
            }
            else
            {
                posicionFlechaEnemiga = 1;

                if (datosDelPersonaje[3].tipo == Personajes.tipoPersonaje.GRANDE)
                {
                    flechaSeleccionPersonajeEnemigo.transform.position = new Vector3(enemigoGrande1.transform.position.x + separacionFlechaX, enemigoGrande1.transform.position.y, enemigoGrande1.transform.position.z);
                }
                else
                {
                    flechaSeleccionPersonajeEnemigo.transform.position = new Vector3(enemigo1.transform.position.x + separacionFlechaX, enemigo1.transform.position.y, enemigo1.transform.position.z);
                }
            }

            int aux = posicionFlechaEnemiga - 1;
            ActivaInforEnemigos(aux);
        }
        else
        {
            posicionFlechaOriginal = posicionFlechaAliado;
            posicionFlechaAliado += movimiento;

            if (movimientoAliados == 3)
            {
                if (posicionFlechaAliado < 1)
                {
                    posicionFlechaAliado = 3;
                }
                else if (posicionFlechaAliado > 3)
                {
                    posicionFlechaAliado = 1;
                }

                if (posicionFlechaAliado == 1)
                {
                    if (datosDelPersonaje[0].vidaActual != 0)
                    {
                        flechaSeleccionPersonaje.transform.position = new Vector3(aliado1.transform.position.x - separacionFlechaX, aliado1.transform.position.y, aliado1.transform.position.z);
                    }
                    else
                    {
                        if (posicionFlechaOriginal == 3)
                        {
                            if (datosDelPersonaje[1].vidaActual != 0)
                            {
                                posicionFlechaAliado = 2;
                            }
                            else
                            {
                                posicionFlechaAliado = 3;
                            }
                        }
                        else if (posicionFlechaOriginal == 2)
                        {
                            if (datosDelPersonaje[2].vidaActual != 0)
                            {
                                posicionFlechaAliado = 3;
                            }
                            else
                            {
                                posicionFlechaAliado = 2;
                            }
                        }
                    }
                }

                if (posicionFlechaAliado == 2)
                {
                    if (datosDelPersonaje[1].vidaActual != 0)
                    {
                        flechaSeleccionPersonaje.transform.position = new Vector3(aliado2.transform.position.x - separacionFlechaX, aliado2.transform.position.y, aliado2.transform.position.z);
                    }
                    else
                    {
                        if (posicionFlechaOriginal == 1)
                        {
                            if (datosDelPersonaje[2].vidaActual != 0)
                            {
                                posicionFlechaAliado = 3;
                            }
                            else
                            {
                                posicionFlechaAliado = 1;
                            }
                        }
                        else if (posicionFlechaOriginal == 3)
                        {
                            if (datosDelPersonaje[0].vidaActual != 0)
                            {
                                posicionFlechaAliado = 1;
                                flechaSeleccionPersonajeEnemigo.transform.position = new Vector3(aliado1.transform.position.x - separacionFlechaX, aliado1.transform.position.y, aliado1.transform.position.z);
                            }
                            else
                            {
                                posicionFlechaAliado = 3;
                            }
                        }
                    }
                }

                if (posicionFlechaAliado == 3)
                {
                    if (datosDelPersonaje[2].vidaActual != 0)
                    {
                        flechaSeleccionPersonaje.transform.position = new Vector3(aliado3.transform.position.x - separacionFlechaX, aliado3.transform.position.y, aliado3.transform.position.z);
                    }
                    else
                    {
                        if (posicionFlechaOriginal == 1)
                        {
                            if (datosDelPersonaje[1].vidaActual != 0)
                            {
                                posicionFlechaAliado = 2;
                                flechaSeleccionPersonajeEnemigo.transform.position = new Vector3(aliado2.transform.position.x - separacionFlechaX, aliado2.transform.position.y, aliado2.transform.position.z);
                            }
                            else if (posicionFlechaOriginal == 2)
                            {
                                posicionFlechaAliado = 1;
                            }
                        }
                        else if (posicionFlechaOriginal == 2)
                        {
                            if (datosDelPersonaje[3].vidaActual != 0)
                            {
                                posicionFlechaAliado = 1;
                                flechaSeleccionPersonajeEnemigo.transform.position = new Vector3(aliado1.transform.position.x - separacionFlechaX, enemigo1.transform.position.y, enemigo1.transform.position.z);
                            }
                            else if (posicionFlechaOriginal == 2)
                            {
                                posicionFlechaAliado = 2;
                            }
                        }
                    }
                }
            }
            else if (movimientoAliados == 2)
            {
                if (posicionFlechaAliado < 1)
                {
                    posicionFlechaAliado = 2;
                }
                else if (posicionFlechaAliado > 2)
                {
                    posicionFlechaAliado = 1;
                }


                if (posicionFlechaAliado == 1)
                {
                    if (datosDelPersonaje[0].vidaActual != 0)
                    {
                        flechaSeleccionPersonaje.transform.position = new Vector3(aliado1.transform.position.x - separacionFlechaX, aliado1.transform.position.y, aliado1.transform.position.z);
                    }
                    else
                    {
                        posicionFlechaAliado++;
                    }
                }

                if (posicionFlechaAliado == 2)
                {
                    if (datosDelPersonaje[1].vidaActual != 0)
                    {
                        flechaSeleccionPersonaje.transform.position = new Vector3(aliado2.transform.position.x - separacionFlechaX, aliado2.transform.position.y, aliado2.transform.position.z);
                    }
                    else
                    {
                        posicionFlechaAliado = 1;
                    }
                }
            }
            else
            {
                posicionFlechaAliado = 1;
            }
        }
    }



    void AsignaObjetivoAliado(bool apoyo)
    {
        listaDeObjetivos[movimientoAliadoGastado].lanzador = movimientoAliadoGastado;

        if (apoyo)
        {
            listaDeObjetivos[movimientoAliadoGastado].receptor = posicionFlechaAliado - 1;
        }
        else
        {
            listaDeObjetivos[movimientoAliadoGastado].receptor = posicionFlechaEnemiga + 2;
        }

        listaDeObjetivos[movimientoAliadoGastado].ataqueAUsar = listaDeAtaques[movimientoAliadoGastado];
        listaAcciones[movimientoAliadoGastado] = 1;

        if (flechaSeleccionPersonajeEnemigo.activeSelf)
        {
            flechaSeleccionPersonajeEnemigo.SetActive(false);
            //flechaSeleccionPersonaje.SetActive(true);
        }
    }



    void ReiniciaAccion()
    {
        posicionado = false;
        //menuEstadisticas.gameObject.SetActive(true);
        elegidoObjetivo = false;
        apuntadoIniciado = false;
        ataqueGuardado = false;

        if(movimientoAliadoGastado >= 0)
        {
            menuEstadisticas.transform.GetChild(movimientoAliadoGastado).GetComponent<Image>().sprite = elementosInterfaz[17];
        }
        movimientoAliadoGastado++;

        if (flechaSeleccionPersonajeEnemigo.activeSelf)
        {
            flechaSeleccionPersonajeEnemigo.SetActive(false);
            //flechaSeleccionPersonaje.SetActive(true);
        }

        if (movimientoAliadoGastado == movimientoAliados)
        {
            TerminaTurnoAliado();
        }
    }



    void AsignarObjetivoEnemigo(int lanzadorDeAtaque, int receptorDeAtaque) //Al lanzador y al receptor hay que sumarle 3 al valor que tenga el indice
    {
        listaDeObjetivos[3 + movimientoEnemigoGastado].lanzador = lanzadorDeAtaque;
        listaDeObjetivos[3 + movimientoEnemigoGastado].receptor = receptorDeAtaque;
        listaDeObjetivos[3 + movimientoEnemigoGastado].ataqueAUsar = listaDeAtaques[3 + movimientoEnemigoGastado];
    }



    void RealizaAtaque(int indiceLanzador)
    {
        int indiceAtaque = listaDeObjetivos[indiceLanzador].indiceAtaque;

        if(indiceLanzador < 3)
        {
            datosDelPersonaje[indiceLanzador].habilidades[indiceAtaque].energiaActual--;
        }

        if (listaDeObjetivos[indiceLanzador].ataqueAUsar.tipo == Ataque.tipoAtaque.FISICO || listaDeObjetivos[indiceLanzador].ataqueAUsar.tipo == Ataque.tipoAtaque.MAGICO)
        {
            if (listaDeObjetivos[indiceLanzador].ataqueAUsar.tipo == Ataque.tipoAtaque.FISICO)
            {
                CalculaDanio(indiceLanzador, true);
            }
            else
            {
                CalculaDanio(indiceLanzador, false);
            }
        }
        else
        {
            AplicaApoyo(indiceLanzador);
        }
        
        pasandoAtaque = false;
    }



    void CalculaDanio(int indiceLanzador, bool fisico)
    {
        if (!combateGanado && !combatePerdido)
        {
            int indiceReceptor = listaDeObjetivos[indiceLanzador].receptor;
            int nivelAtacante = datosDelPersonaje[indiceLanzador].nivel;
            int ataqueLanzador, defensaReceptor, potenciaAtaque, variacion;
            int numeroGolpes = 1;

            float danio = 0;
            float efectividad = 1;
            float bonificacion = 1;

            fallo = true;
            float acierto = listaDeObjetivos[indiceLanzador].ataqueAUsar.precision * (1 / datosDelPersonaje[indiceReceptor].evasion);

            int diferenciaVel = datosDelPersonaje[indiceLanzador].velocidadActual - datosDelPersonaje[indiceReceptor].velocidadActual;

            if (diferenciaVel <= 10)
            {
                numeroGolpes = 1;
            }
            else if (diferenciaVel >= 15 && diferenciaVel <= 30)
            {
                numeroGolpes = 2;
            }
            else
            {
                numeroGolpes = 2;
            }

            int golpesFinales = numeroGolpes;

            for (int i = 0; i < numeroGolpes; i++)
            {
                variacion = Random.Range(0, 100);

                if (variacion >= acierto)
                {
                    golpesFinales--;
                }
            }

            numeroGolpes = golpesFinales;

            if (numeroGolpes != 0)
            {
                fallo = false;
            }
            int valorDanio = 0;

            if (!fallo)
            {
                efectividad = CalculaEficiencia(indiceLanzador, indiceReceptor);
                bonificacion = CalculaBonificacion(indiceLanzador);

                variacion = Random.Range(85, 101);

                if (fisico)
                {
                    ataqueLanzador = datosDelPersonaje[indiceLanzador].ataqueFisicoActual;
                    defensaReceptor = datosDelPersonaje[indiceReceptor].defensaFisicaActual;
                }
                else
                {
                    ataqueLanzador = datosDelPersonaje[indiceLanzador].ataqueMagicoActual;
                    defensaReceptor = datosDelPersonaje[indiceReceptor].defensaMagicaActual;
                }

                potenciaAtaque = listaDeObjetivos[indiceLanzador].ataqueAUsar.potencia;
                danio = 0.01f * bonificacion * efectividad * variacion * ((((0.2f * nivelAtacante + 1) * ataqueLanzador * potenciaAtaque) / (25 * defensaReceptor)) + 2);
                
                if (danio < 1 && danio > 0)
                {
                    danio = 1;
                }

                valorDanio = (int)danio;
                
                if (numeroGolpes == 2)
                {
                    danio = (int)valorDanio * 1.5f;
                }
                else if (numeroGolpes == 3)
                {
                    danio = (int)valorDanio * 2;
                }
                else
                {
                    danio = valorDanio;
                }

                datosDelPersonaje[indiceReceptor].vidaActual -= (int)danio;
            }

            string mensajeDanio = "";

            if (numeroGolpes == 2)
            {
                mensajeDanio = "-" + valorDanio + " x1.5";
            }
            else if (numeroGolpes == 3)
            {
                mensajeDanio = "-" + valorDanio + " x2";
            }
            else
            {
                mensajeDanio = "-" + valorDanio;
            }

            switch (indiceReceptor)
            {
                case 0:
                    if (efectividad == 2)
                    {
                        danioAliado1.color = new Color(203.0f / 255.0f, 64.0f / 255.0f, 64.0f / 255.0f);
                    }
                    else if (efectividad == 0.5f)
                    {
                        danioAliado1.color = new Color(64.0f / 255.0f, 88.0f / 255.0f, 202.0f / 255.0f);
                    }
                    else
                    {
                        danioAliado1.color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
                    }

                    danioAliado1.text = mensajeDanio;
                    danioAliado1.gameObject.SetActive(true);

                    if(datosDelPersonaje[0].vidaActual < 0)
                    {
                        datosDelPersonaje[0].vidaActual = 0;
                    }

                    menuEstadisticas.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>().text = datosDelPersonaje[0].vidaActual + "/" + datosDelPersonaje[0].vidaModificada;

                    animadorAl1.SetBool("danio", true);
                    controlEfecto1.SetActive(true);
                    controlEfectoDanioAl1.SetBool("activo", true);
                    break;
                case 1:
                    if (efectividad == 2)
                    {
                        danioAliado2.color = new Color(203.0f / 255.0f, 64.0f / 255.0f, 64.0f / 255.0f);
                    }
                    else if (efectividad == 0.5f)
                    {
                        danioAliado2.color = new Color(64.0f / 255.0f, 88.0f / 255.0f, 202.0f / 255.0f);
                    }
                    else
                    {
                        danioAliado2.color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
                    }

                    danioAliado2.text = mensajeDanio;
                    danioAliado2.gameObject.SetActive(true);

                    if (datosDelPersonaje[1].vidaActual < 0)
                    {
                        datosDelPersonaje[1].vidaActual = 0;
                    }

                    menuEstadisticas.transform.GetChild(1).GetChild(3).GetChild(0).GetComponent<Text>().text = datosDelPersonaje[1].vidaActual + "/" + datosDelPersonaje[1].vidaModificada;

                    animadorAl2.SetBool("danio", true);
                    controlEfecto2.SetActive(true);
                    controlEfectoDanioAl2.SetBool("activo", true);
                    break;
                case 2:
                    if (efectividad == 2)
                    {
                        danioAliado3.color = new Color(203.0f / 255.0f, 64.0f / 255.0f, 64.0f / 255.0f);
                    }
                    else if (efectividad == 0.5f)
                    {
                        danioAliado3.color = new Color(64.0f / 255.0f, 88.0f / 255.0f, 202.0f / 255.0f);
                    }
                    else
                    {
                        danioAliado3.color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
                    }

                    danioAliado3.text = mensajeDanio;
                    danioAliado3.gameObject.SetActive(true);

                    if (datosDelPersonaje[2].vidaActual < 0)
                    {
                        datosDelPersonaje[2].vidaActual = 0;
                    }

                    menuEstadisticas.transform.GetChild(2).GetChild(3).GetChild(0).GetComponent<Text>().text = datosDelPersonaje[2].vidaActual + "/" + datosDelPersonaje[2].vidaModificada;

                    animadorAl3.SetBool("danio", true);
                    controlEfecto3.SetActive(true);
                    controlEfectoDanioAl3.SetBool("activo", true);
                    break;
                case 3:
                    if (datosDelPersonaje[3].tipo == Personajes.tipoPersonaje.PEQUENIO)
                    {
                        if (efectividad == 2)
                        {
                            danioEnemigoPeq1.color = new Color(203.0f / 255.0f, 64.0f / 255.0f, 64.0f / 255.0f);
                        }
                        else if (efectividad == 0.5f)
                        {
                            danioEnemigoPeq1.color = new Color(64.0f / 255.0f, 88.0f / 255.0f, 202.0f / 255.0f);
                        }
                        else
                        {
                            danioEnemigoPeq1.color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
                        }

                        danioEnemigoPeq1.text = mensajeDanio;
                        danioEnemigoPeq1.gameObject.SetActive(true);
                    }
                    else if (datosDelPersonaje[3].tipo == Personajes.tipoPersonaje.GRANDE)
                    {
                        if (efectividad == 2)
                        {
                            danioEnemigoGran1.color = new Color(203.0f / 255.0f, 64.0f / 255.0f, 64.0f / 255.0f);
                        }
                        else if (efectividad == 0.5f)
                        {
                            danioEnemigoGran1.color = new Color(64.0f / 255.0f, 88.0f / 255.0f, 202.0f / 255.0f);
                        }
                        else
                        {
                            danioEnemigoGran1.color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
                        }

                        danioEnemigoGran1.text = mensajeDanio;
                        danioEnemigoGran1.gameObject.SetActive(true);

                    }
                    else
                    {
                        if (efectividad == 2)
                        {
                            danioEnemigoGig.color = new Color(203.0f / 255.0f, 64.0f / 255.0f, 64.0f / 255.0f);
                        }
                        else if (efectividad == 0.5f)
                        {
                            danioEnemigoGig.color = new Color(64.0f / 255.0f, 88.0f / 255.0f, 202.0f / 255.0f);
                        }
                        else
                        {
                            danioEnemigoGig.color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
                        }

                        danioEnemigoGig.text = mensajeDanio;
                        danioEnemigoGig.gameObject.SetActive(true);
                    }

                    animadorEn1.SetBool("danio", true);
                    controlEfecto4.SetActive(true);
                    controlEfectoDanioEn1.SetBool("activo", true);
                    break;
                case 4:
                    if (datosDelPersonaje[4].tipo == Personajes.tipoPersonaje.PEQUENIO)
                    {
                        if (efectividad == 2)
                        {
                            danioEnemigoPeq2.color = new Color(203.0f / 255.0f, 64.0f / 255.0f, 64.0f / 255.0f);
                        }
                        else if (efectividad == 0.5f)
                        {
                            danioEnemigoPeq2.color = new Color(64.0f / 255.0f, 88.0f / 255.0f, 202.0f / 255.0f);
                        }
                        else
                        {
                            danioEnemigoPeq2.color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
                        }

                        danioEnemigoPeq2.text = mensajeDanio;
                        danioEnemigoPeq2.gameObject.SetActive(true);
                    }
                    else
                    {
                        if (efectividad == 2)
                        {
                            danioEnemigoGran2.color = new Color(203.0f / 255.0f, 64.0f / 255.0f, 64.0f / 255.0f);
                        }
                        else if (efectividad == 0.5f)
                        {
                            danioEnemigoGran2.color = new Color(64.0f / 255.0f, 88.0f / 255.0f, 202.0f / 255.0f);
                        }
                        else
                        {
                            danioEnemigoGran2.color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
                        }

                        danioEnemigoGran2.text = mensajeDanio;
                        danioEnemigoGran2.gameObject.SetActive(true);
                    }

                    animadorEn2.SetBool("danio", true);
                    controlEfecto5.SetActive(true);
                    controlEfectoDanioEn2.SetBool("activo", true);
                    break;
                case 5:
                    if (efectividad == 2)
                    {
                        danioEnemigoPeq3.color = new Color(203.0f / 255.0f, 64.0f / 255.0f, 64.0f / 255.0f);
                    }
                    else if (efectividad == 0.5f)
                    {
                        danioEnemigoPeq3.color = new Color(64.0f / 255.0f, 88.0f / 255.0f, 202.0f / 255.0f);
                    }
                    else
                    {
                        danioEnemigoPeq3.color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
                    }

                    danioEnemigoPeq3.text = mensajeDanio;
                    danioEnemigoPeq3.gameObject.SetActive(true);

                    animadorEn3.SetBool("danio", true);
                    controlEfecto6.SetActive(true);
                    controlEfectoDanioEn3.SetBool("activo", true);
                    break;
            }

            if (fisico)
            {
                musica.ProduceEfecto(2);
            }
            else
            {
                musica.ProduceEfecto(3);
            }

            StartCoroutine(EsperarTextoCombate(1.2f, indiceReceptor, indiceLanzador, efectividad));

            if (datosDelPersonaje[indiceReceptor].vidaActual <= 0)
            {
                datosDelPersonaje[indiceReceptor].vidaActual = 0;

                if (indiceReceptor < 3)
                {
                    datosDelPersonaje[indiceReceptor].estado = Personajes.estadoPersonaje.DERROTADO;
                    datosDelPersonaje[indiceReceptor].estadoIngles = Personajes.estadoPersonajeIngles.DEFEATED;

                    if (movimientoAliados == 1 && datosDelPersonaje[0].vidaActual == 0)
                    {
                        combatePerdido = true;
                    }
                    else if (movimientoAliados == 2 && datosDelPersonaje[0].vidaActual == 0 && datosDelPersonaje[1].vidaActual == 0)
                    {
                        combatePerdido = true;
                    }
                    else if (movimientoAliados == 3 && datosDelPersonaje[0].vidaActual == 0 && datosDelPersonaje[1].vidaActual == 0 && datosDelPersonaje[2].vidaActual == 0)
                    {
                        combatePerdido = true;
                    }
                }
                else
                {
                    if (movimientoEnemigo == 1 && datosDelPersonaje[3].vidaActual == 0)
                    {
                        combateGanado = true;
                    }
                    else if (movimientoEnemigo == 2 && datosDelPersonaje[3].vidaActual == 0 && datosDelPersonaje[4].vidaActual == 0)
                    {
                        combateGanado = true;
                    }
                    else if (movimientoEnemigo == 3 && datosDelPersonaje[3].vidaActual == 0 && datosDelPersonaje[4].vidaActual == 0 && datosDelPersonaje[5].vidaActual == 0)
                    {
                        combateGanado = true;
                    }
                }
            }
        }
    }



    float CalculaEficiencia(int indiceLanzador, int indiceReceptor)
    {
        float efectividad = 1;

        if (datosDelPersonaje[indiceReceptor].elemento != Personajes.elementoPersonaje.NEUTRO)
        {
            if (listaDeObjetivos[indiceLanzador].ataqueAUsar.elemento == Ataque.elementoAtaque.DORMILON)
            {
                if (datosDelPersonaje[indiceReceptor].elemento == Personajes.elementoPersonaje.DORMILON)
                {
                    efectividad = 0.5f;
                }
                else if (datosDelPersonaje[indiceReceptor].elemento == Personajes.elementoPersonaje.FIESTERO)
                {
                    efectividad = 2;
                }
                else if (datosDelPersonaje[indiceReceptor].elemento == Personajes.elementoPersonaje.FRIKI)
                {
                    efectividad = 1;
                }
                else if (datosDelPersonaje[indiceReceptor].elemento == Personajes.elementoPersonaje.RESPONSABLE)
                {
                    efectividad = 1;
                }
                else
                {
                    efectividad = 2;
                }
            }
            else if (listaDeObjetivos[indiceLanzador].ataqueAUsar.elemento == Ataque.elementoAtaque.FIESTERO)
            {
                if (datosDelPersonaje[indiceReceptor].elemento == Personajes.elementoPersonaje.DORMILON)
                {
                    efectividad = 1;
                }
                else if (datosDelPersonaje[indiceReceptor].elemento == Personajes.elementoPersonaje.FIESTERO)
                {
                    efectividad = 0.5f;
                }
                else if (datosDelPersonaje[indiceReceptor].elemento == Personajes.elementoPersonaje.FRIKI)
                {
                    efectividad = 1;
                }
                else if (datosDelPersonaje[indiceReceptor].elemento == Personajes.elementoPersonaje.RESPONSABLE)
                {
                    efectividad = 2;
                }
                else
                {
                    efectividad = 2;
                }
            }
            else if (listaDeObjetivos[indiceLanzador].ataqueAUsar.elemento == Ataque.elementoAtaque.FRIKI)
            {
                if (datosDelPersonaje[indiceReceptor].elemento == Personajes.elementoPersonaje.DORMILON)
                {
                    efectividad = 2;
                }
                else if (datosDelPersonaje[indiceReceptor].elemento == Personajes.elementoPersonaje.FIESTERO)
                {
                    efectividad = 2;
                }
                else if (datosDelPersonaje[indiceReceptor].elemento == Personajes.elementoPersonaje.FRIKI)
                {
                    efectividad = 0.5f;
                }
                else if (datosDelPersonaje[indiceReceptor].elemento == Personajes.elementoPersonaje.RESPONSABLE)
                {
                    efectividad = 1;
                }
                else
                {
                    efectividad = 1;
                }
            }
            else if (listaDeObjetivos[indiceLanzador].ataqueAUsar.elemento == Ataque.elementoAtaque.RESPONSABLE)
            {
                if (datosDelPersonaje[indiceReceptor].elemento == Personajes.elementoPersonaje.DORMILON)
                {
                    efectividad = 2;
                }
                else if (datosDelPersonaje[indiceReceptor].elemento == Personajes.elementoPersonaje.FIESTERO)
                {
                    efectividad = 1;
                }
                else if (datosDelPersonaje[indiceReceptor].elemento == Personajes.elementoPersonaje.FRIKI)
                {
                    efectividad = 2;
                }
                else if (datosDelPersonaje[indiceReceptor].elemento == Personajes.elementoPersonaje.RESPONSABLE)
                {
                    efectividad = 0.5f;
                }
                else
                {
                    efectividad = 1;
                }
            }
            else if (listaDeObjetivos[indiceLanzador].ataqueAUsar.elemento == Ataque.elementoAtaque.NEUTRO)
            {
                efectividad = 1;
            }
            else
            {
                if (datosDelPersonaje[indiceReceptor].elemento == Personajes.elementoPersonaje.DORMILON)
                {
                    efectividad = 1;
                }
                else if (datosDelPersonaje[indiceReceptor].elemento == Personajes.elementoPersonaje.FIESTERO)
                {
                    efectividad = 1;
                }
                else if (datosDelPersonaje[indiceReceptor].elemento == Personajes.elementoPersonaje.FRIKI)
                {
                    efectividad = 2;
                }
                else if (datosDelPersonaje[indiceReceptor].elemento == Personajes.elementoPersonaje.RESPONSABLE)
                {
                    efectividad = 2;
                }
                else
                {
                    efectividad = 0.5f;
                }
            }
        }
        else
        {
            if (listaDeObjetivos[indiceLanzador].ataqueAUsar.elemento == Ataque.elementoAtaque.TIRANO)
            {
                efectividad = 2;
            }
        }
        

        return efectividad;
    }



    float CalculaBonificacion(int indiceLanzador)
    {
        float bonificacion = 1;

        if (listaDeObjetivos[indiceLanzador].ataqueAUsar.elemento == Ataque.elementoAtaque.DORMILON)
        {
            if (datosDelPersonaje[indiceLanzador].elemento == Personajes.elementoPersonaje.DORMILON)
            {
                bonificacion = 1.5f;
            }
            else
            {
                bonificacion = 1;
            }
        }
        else if (listaDeObjetivos[indiceLanzador].ataqueAUsar.elemento == Ataque.elementoAtaque.FIESTERO)
        {
            if (datosDelPersonaje[indiceLanzador].elemento == Personajes.elementoPersonaje.FIESTERO)
            {
                bonificacion = 1.5f;
            }
            else
            {
                bonificacion = 1;
            }
        }
        else if (listaDeObjetivos[indiceLanzador].ataqueAUsar.elemento == Ataque.elementoAtaque.FRIKI)
        {
            if (datosDelPersonaje[indiceLanzador].elemento == Personajes.elementoPersonaje.FRIKI)
            {
                bonificacion = 1.5f;
            }
            else
            {
                bonificacion = 1;
            }
        }
        else if (listaDeObjetivos[indiceLanzador].ataqueAUsar.elemento == Ataque.elementoAtaque.RESPONSABLE)
        {
            if (datosDelPersonaje[indiceLanzador].elemento == Personajes.elementoPersonaje.RESPONSABLE)
            {
                bonificacion = 1.5f;
            }
            else
            {
                bonificacion = 1;
            }
        }
        else if (listaDeObjetivos[indiceLanzador].ataqueAUsar.elemento == Ataque.elementoAtaque.NEUTRO)
        {
            if (datosDelPersonaje[indiceLanzador].elemento == Personajes.elementoPersonaje.NEUTRO)
            {
                bonificacion = 1.5f;
            }
            else
            {
                bonificacion = 1;
            }
        }
        else
        {
            if (datosDelPersonaje[indiceLanzador].elemento == Personajes.elementoPersonaje.TIRANO)
            {
                bonificacion = 1.5f;
            }
            else
            {
                bonificacion = 1;
            }
        }

        return bonificacion;
    }



    int DeterminarObjetivo(int indiceLanzador)
    {
        int objetivo = 0;
        bool tieneApoyo = false;

        for(int i = 0; i < datosDelPersonaje[indiceLanzador].numeroAtaques; i++)
        {
            if (!tieneApoyo)
            {
                if (datosDelPersonaje[indiceLanzador].habilidades[i].tipo == Ataque.tipoAtaque.APOYO_POSITIVO || datosDelPersonaje[indiceLanzador].habilidades[i].tipo == Ataque.tipoAtaque.APOYO_MIXTO)
                {
                    tieneApoyo = true;
                }
            }
        }

        objetivo = DecidirObjetivo(indiceLanzador, tieneApoyo);

        return objetivo;
    }



    int DecidirObjetivo(int indiceLanzador, bool tieneApoyo)
    {
        int aleatorio = Random.Range(0, 101);
        int indice = 0;
        int prob1, prob2;
        int elementoPersonaje;
        int[] prior1, prior2, prior3;
        int ind1, ind2, ind3;
        ind1 = ind2 = ind3 = 0;

        prior1 = new int[3];
        prior2 = new int[3];
        prior3 = new int[3];

        if (datosDelPersonaje[indiceLanzador].elemento == Personajes.elementoPersonaje.DORMILON)
        {
            elementoPersonaje = 1;
        }
        else if (datosDelPersonaje[indiceLanzador].elemento == Personajes.elementoPersonaje.FIESTERO)
        {
            elementoPersonaje = 2;
        }
        else if (datosDelPersonaje[indiceLanzador].elemento == Personajes.elementoPersonaje.FRIKI)
        {
            elementoPersonaje = 3;
        }
        else if (datosDelPersonaje[indiceLanzador].elemento == Personajes.elementoPersonaje.RESPONSABLE)
        {
            elementoPersonaje = 4;
        }
        else
        {
            elementoPersonaje = 5;
        }

        if (!tieneApoyo || aleatorio < 70)
        {
            if (movimientoAliados == 1)
            {
                indice = 0;
            }
            else
            {
                for (int i = 0; i < movimientoAliados; i++)
                {
                    if(datosDelPersonaje[i].vidaActual != 0)
                    {
                        if (elementoPersonaje == 1)
                        {
                            if (datosDelPersonaje[i].elemento == Personajes.elementoPersonaje.DORMILON)
                            {
                                prior3[ind3] = i;
                                ind3++;
                            }
                            else if (datosDelPersonaje[i].elemento == Personajes.elementoPersonaje.FIESTERO)
                            {
                                prior1[ind1] = i;
                                ind1++;
                            }
                            else if (datosDelPersonaje[i].elemento == Personajes.elementoPersonaje.FRIKI)
                            {
                                prior2[ind2] = i;
                                ind2++;
                            }
                            else if (datosDelPersonaje[i].elemento == Personajes.elementoPersonaje.RESPONSABLE)
                            {
                                prior2[ind2] = i;
                                ind2++;
                            }
                            else if (datosDelPersonaje[i].elemento == Personajes.elementoPersonaje.TIRANO)
                            {
                                prior1[ind1] = i;
                                ind1++;
                            }
                            else
                            {
                                prior2[ind2] = i;
                                ind2++;
                            }
                        }
                        else if (elementoPersonaje == 2)
                        {
                            if (datosDelPersonaje[i].elemento == Personajes.elementoPersonaje.DORMILON)
                            {
                                prior2[ind2] = i;
                                ind2++;
                            }
                            else if (datosDelPersonaje[i].elemento == Personajes.elementoPersonaje.FIESTERO)
                            {
                                prior3[ind3] = i;
                                ind3++;
                            }
                            else if (datosDelPersonaje[i].elemento == Personajes.elementoPersonaje.FRIKI)
                            {
                                prior2[ind2] = i;
                                ind2++;
                            }
                            else if (datosDelPersonaje[i].elemento == Personajes.elementoPersonaje.RESPONSABLE)
                            {
                                prior1[ind1] = i;
                                ind1++;
                            }
                            else if (datosDelPersonaje[i].elemento == Personajes.elementoPersonaje.TIRANO)
                            {
                                prior1[ind1] = i;
                                ind1++;
                            }
                            else
                            {
                                prior2[ind2] = i;
                                ind2++;
                            }
                        }
                        else if (elementoPersonaje == 3)
                        {
                            if (datosDelPersonaje[i].elemento == Personajes.elementoPersonaje.DORMILON)
                            {
                                prior1[ind1] = i;
                                ind1++;
                            }
                            else if (datosDelPersonaje[i].elemento == Personajes.elementoPersonaje.FIESTERO)
                            {
                                prior1[ind1] = i;
                                ind1++;
                            }
                            else if (datosDelPersonaje[i].elemento == Personajes.elementoPersonaje.FRIKI)
                            {
                                prior3[ind3] = i;
                                ind3++;
                            }
                            else if (datosDelPersonaje[i].elemento == Personajes.elementoPersonaje.RESPONSABLE)
                            {
                                prior2[ind2] = i;
                                ind2++;
                            }
                            else if (datosDelPersonaje[i].elemento == Personajes.elementoPersonaje.TIRANO)
                            {
                                prior2[ind2] = i;
                                ind2++;
                            }
                            else
                            {
                                prior2[ind2] = i;
                                ind2++;
                            }
                        }
                        else if (elementoPersonaje == 4)
                        {
                            if (datosDelPersonaje[i].elemento == Personajes.elementoPersonaje.DORMILON)
                            {
                                prior1[ind1] = i;
                                ind1++;
                            }
                            else if (datosDelPersonaje[i].elemento == Personajes.elementoPersonaje.FIESTERO)
                            {
                                prior2[ind2] = i;
                                ind2++;
                            }
                            else if (datosDelPersonaje[i].elemento == Personajes.elementoPersonaje.FRIKI)
                            {
                                prior1[ind1] = i;
                                ind1++;
                            }
                            else if (datosDelPersonaje[i].elemento == Personajes.elementoPersonaje.RESPONSABLE)
                            {
                                prior3[ind3] = i;
                                ind3++;
                            }
                            else if (datosDelPersonaje[i].elemento == Personajes.elementoPersonaje.TIRANO)
                            {
                                prior2[ind2] = i;
                                ind2++;
                            }
                            else
                            {
                                prior2[ind2] = i;
                                ind2++;
                            }
                        }
                        else
                        {
                            if (datosDelPersonaje[i].elemento == Personajes.elementoPersonaje.DORMILON)
                            {
                                prior2[ind2] = i;
                                ind2++;
                            }
                            else if (datosDelPersonaje[i].elemento == Personajes.elementoPersonaje.FIESTERO)
                            {
                                prior2[ind2] = i;
                                ind2++;
                            }
                            else if (datosDelPersonaje[i].elemento == Personajes.elementoPersonaje.FRIKI)
                            {
                                prior1[ind1] = i;
                                ind1++;
                            }
                            else if (datosDelPersonaje[i].elemento == Personajes.elementoPersonaje.RESPONSABLE)
                            {
                                prior1[ind1] = i;
                                ind1++;
                            }
                            else if (datosDelPersonaje[i].elemento == Personajes.elementoPersonaje.TIRANO)
                            {
                                prior3[ind3] = i;
                                ind3++;
                            }
                            else
                            {
                                prior2[ind2] = i;
                                ind2++;
                            }
                        }
                    }
                }

                aleatorio = Random.Range(0, 101);

                if (dificultad == 0)
                {
                    prob1 = 25;
                    prob2 = prob1 + 35;
                }
                else if (dificultad == 1)
                {
                    prob1 = 40;
                    prob2 = prob1 + 40;
                }
                else if (dificultad == 2)
                {
                    prob1 = 60;
                    prob2 = prob1 + 30;
                }
                else
                {
                    prob1 = 80;
                    prob2 = prob1 + 10;
                }
                

                if (aleatorio <= prob1)
                {
                    if (ind1 == 0)
                    {
                        if (ind2 != 0)
                        {
                            indice = prior2[Random.Range(0, ind2)];
                        }
                        else
                        {
                            indice = prior3[Random.Range(0, ind3)];
                        }
                    }
                    else
                    {
                        indice = prior1[Random.Range(0, ind1)];
                    }
                }
                else if (aleatorio > prob1 && aleatorio <= prob2)
                {
                    if (ind2 == 0)
                    {
                        if (ind1 != 0)
                        {
                            indice = prior1[Random.Range(0, ind1)];
                        }
                        else
                        {
                            indice = prior3[Random.Range(0, ind3)];
                        }
                    }
                    else
                    {
                        indice = prior2[Random.Range(0, ind2)];
                    }
                }
                else
                {
                    if (ind3 == 0)
                    {
                        if (ind2 != 0)
                        {
                            indice = prior2[Random.Range(0, ind2)];
                        }
                        else
                        {
                            indice = prior1[Random.Range(0, ind1)];
                        }
                    }
                    else
                    {
                        indice = prior3[Random.Range(0, ind3)];
                    }
                }
            }
        }
        else
        {
            if (movimientoEnemigo == 1)
            {
                indice = 3;
            }
            else if (movimientoEnemigo == 2)
            {
                aleatorio = Random.Range(3, 5);
                indice = aleatorio;
            }
            else
            {
                aleatorio = Random.Range(3, 6);
                indice = aleatorio;
            }
        }

        return indice;
    }



    int DeterminaPrioridadAtaqueAEnemigo(int indiceLanzador, int indiceReceptor)
    {
        int indice = 0;
        int elementoPersonaje;
        int prob1, prob2;
        int[] prior1, prior2, prior3;
        int[] apoyoEnemigo;
        int ind1, ind2, ind3;
        int apoyoEnemigoIndice;

        ind1 = ind2 = ind3 = apoyoEnemigoIndice = 0;
        indiceLanzador += 3;

        prior1 = new int[4];
        prior2 = new int[4];
        prior3 = new int[4];
        apoyoEnemigo = new int[4];

        if (datosDelPersonaje[indiceReceptor].elemento == Personajes.elementoPersonaje.DORMILON)
        {
            elementoPersonaje = 1;
        }
        else if (datosDelPersonaje[indiceReceptor].elemento == Personajes.elementoPersonaje.FIESTERO)
        {
            elementoPersonaje = 2;
        }
        else if (datosDelPersonaje[indiceReceptor].elemento == Personajes.elementoPersonaje.FRIKI)
        {
            elementoPersonaje = 3;
        }
        else if (datosDelPersonaje[indiceReceptor].elemento == Personajes.elementoPersonaje.RESPONSABLE)
        {
            elementoPersonaje = 4;
        }
        else
        {
            elementoPersonaje = 5;
        }

        for (int i = 0; i < 4; i++)
        {
            if (indiceReceptor < 3)
            {
                if (datosDelPersonaje[indiceLanzador].habilidades[i].elemento != Ataque.elementoAtaque.APOYO)
                {
                    if (elementoPersonaje == 1)
                    {
                        if (datosDelPersonaje[indiceLanzador].habilidades[i].elemento == Ataque.elementoAtaque.DORMILON)
                        {
                            prior3[ind3] = i;
                            ind3++;
                        }
                        else if (datosDelPersonaje[indiceLanzador].habilidades[i].elemento == Ataque.elementoAtaque.FIESTERO)
                        {
                            prior1[ind1] = i;
                            ind1++;
                        }
                        else if (datosDelPersonaje[indiceLanzador].habilidades[i].elemento == Ataque.elementoAtaque.FRIKI)
                        {
                            prior2[ind2] = i;
                            ind2++;
                        }
                        else if (datosDelPersonaje[indiceLanzador].habilidades[i].elemento == Ataque.elementoAtaque.RESPONSABLE)
                        {
                            prior2[ind2] = i;
                            ind2++;
                        }
                        else if (datosDelPersonaje[indiceLanzador].habilidades[i].elemento == Ataque.elementoAtaque.TIRANO)
                        {
                            prior1[ind1] = i;
                            ind1++;
                        }
                    }
                    else if (elementoPersonaje == 2)
                    {
                        if (datosDelPersonaje[indiceLanzador].habilidades[i].elemento == Ataque.elementoAtaque.DORMILON)
                        {
                            prior2[ind2] = i;
                            ind2++;
                        }
                        else if (datosDelPersonaje[indiceLanzador].habilidades[i].elemento == Ataque.elementoAtaque.FIESTERO)
                        {
                            prior3[ind3] = i;
                            ind3++;
                        }
                        else if (datosDelPersonaje[indiceLanzador].habilidades[i].elemento == Ataque.elementoAtaque.FRIKI)
                        {
                            prior2[ind2] = i;
                            ind2++;
                        }
                        else if (datosDelPersonaje[indiceLanzador].habilidades[i].elemento == Ataque.elementoAtaque.RESPONSABLE)
                        {
                            prior1[ind1] = i;
                            ind1++;
                        }
                        else
                        {
                            prior1[ind1] = i;
                            ind1++;
                        }
                    }
                    else if (elementoPersonaje == 3)
                    {
                        if (datosDelPersonaje[indiceLanzador].habilidades[i].elemento == Ataque.elementoAtaque.DORMILON)
                        {
                            prior1[ind1] = i;
                            ind1++;
                        }
                        else if (datosDelPersonaje[indiceLanzador].habilidades[i].elemento == Ataque.elementoAtaque.FIESTERO)
                        {
                            prior1[ind1] = i;
                            ind1++;
                        }
                        else if (datosDelPersonaje[indiceLanzador].habilidades[i].elemento == Ataque.elementoAtaque.FRIKI)
                        {
                            prior3[ind3] = i;
                            ind3++;
                        }
                        else if (datosDelPersonaje[indiceLanzador].habilidades[i].elemento == Ataque.elementoAtaque.RESPONSABLE)
                        {
                            prior2[ind2] = i;
                            ind2++;
                        }
                        else
                        {
                            prior2[ind2] = i;
                            ind2++;
                        }
                    }
                    else if (elementoPersonaje == 4)
                    {
                        if (datosDelPersonaje[indiceLanzador].habilidades[i].elemento == Ataque.elementoAtaque.DORMILON)
                        {
                            prior1[ind1] = i;
                            ind1++;
                        }
                        else if (datosDelPersonaje[indiceLanzador].habilidades[i].elemento == Ataque.elementoAtaque.FIESTERO)
                        {
                            prior2[ind2] = i;
                            ind2++;
                        }
                        else if (datosDelPersonaje[indiceLanzador].habilidades[i].elemento == Ataque.elementoAtaque.FRIKI)
                        {
                            prior1[ind1] = i;
                            ind1++;
                        }
                        else if (datosDelPersonaje[indiceLanzador].habilidades[i].elemento == Ataque.elementoAtaque.RESPONSABLE)
                        {
                            prior3[ind3] = i;
                            ind3++;
                        }
                        else
                        {
                            prior2[ind2] = i;
                            ind2++;
                        }
                    }
                    else
                    {
                        if (datosDelPersonaje[indiceLanzador].habilidades[i].elemento == Ataque.elementoAtaque.DORMILON)
                        {
                            prior2[ind2] = i;
                            ind2++;
                        }
                        else if (datosDelPersonaje[indiceLanzador].habilidades[i].elemento == Ataque.elementoAtaque.FIESTERO)
                        {
                            prior2[ind2] = i;
                            ind2++;
                        }
                        else if (datosDelPersonaje[indiceLanzador].habilidades[i].elemento == Ataque.elementoAtaque.FRIKI)
                        {
                            prior1[ind1] = i;
                            ind1++;
                        }
                        else if (datosDelPersonaje[indiceLanzador].habilidades[i].elemento == Ataque.elementoAtaque.RESPONSABLE)
                        {
                            prior1[ind1] = i;
                            ind1++;
                        }
                        else
                        {
                            prior3[ind3] = i;
                            ind3++;
                        }
                    }
                }
                else
                {
                    if (datosDelPersonaje[indiceLanzador].habilidades[i].tipo == Ataque.tipoAtaque.APOYO_NEGATIVO)
                    {
                        prior2[ind2] = i;
                        ind2++;
                    }
                }
            }
            else
            {
                if (datosDelPersonaje[indiceLanzador].habilidades[i].tipo == Ataque.tipoAtaque.APOYO_POSITIVO || datosDelPersonaje[indiceLanzador].habilidades[i].tipo == Ataque.tipoAtaque.APOYO_MIXTO)
                {
                    apoyoEnemigo[apoyoEnemigoIndice] = i;
                    apoyoEnemigoIndice++;
                }
            }
        }

        if(indiceReceptor < 3)
        {
            if (dificultad == 0)
            {
                prob1 = 30;
                prob2 = prob1 + 35;
            }
            else if (dificultad == 1)
            {
                prob1 = 50;
                prob2 = prob1 + 35;
            }
            else if (dificultad == 2)
            {
                prob1 = 70;
                prob2 = prob1 + 20;
            }
            else
            {
                prob1 = 80;
                prob2 = prob1 + 15;
            }
            

            
            int aleatorio = Random.Range(0, 100);
            int elementoElegido;

            if (aleatorio <= prob1)
            {
                if (ind1 == 0)
                {
                    if (ind2 != 0)
                    {
                        elementoElegido = Random.Range(0, ind2);
                        indice = prior2[elementoElegido];
                    }
                    else
                    {
                        elementoElegido = Random.Range(0, ind3);
                        indice = prior3[elementoElegido];
                    }
                }
                else
                {
                    elementoElegido = Random.Range(0, ind1);
                    indice = prior1[elementoElegido];
                }
            }
            else if (aleatorio > prob1 && aleatorio <= prob2)
            {
                if (ind2 == 0)
                {
                    if (ind1 != 0)
                    {
                        elementoElegido = Random.Range(0, ind1);
                        indice = prior1[elementoElegido];
                    }
                    else
                    {
                        elementoElegido = Random.Range(0, ind3);
                        indice = prior3[elementoElegido];
                    }
                }
                else
                {
                    elementoElegido = Random.Range(0, ind2);
                    indice = prior2[elementoElegido];
                }
            }
            else
            {
                if (ind3 == 0)
                {
                    if (ind2 != 0)
                    {
                        elementoElegido = Random.Range(0, ind2);
                        indice = prior2[elementoElegido];
                    }
                    else
                    {
                        elementoElegido = Random.Range(0, ind1);
                        indice = prior1[elementoElegido];
                    }
                }
                else
                {
                    elementoElegido = Random.Range(0, ind3);
                    indice = prior3[elementoElegido];
                }
            }
        }
        else
        {
            indice = Random.Range(0, apoyoEnemigoIndice);
            indice = apoyoEnemigo[indice];
        }

        return indice;
    }



    void AplicaApoyo(int lanzador) //hay que corregir la vida y añadir los problemas de estado en esta fase
    {
        float modificador;
        int indiceReceptor = listaDeObjetivos[lanzador].receptor;
        int indiceLanzador = listaDeObjetivos[lanzador].lanzador;
        int objetivo = listaDeObjetivos[lanzador].receptor;
        string mensaje = "";
        indiceFlechaMejora = indiceReceptor;

        indiceVectorFlechas = 0;
        numeroMejoras = 0;

        
        if (listaDeObjetivos[lanzador].ataqueAUsar.tipo == Ataque.tipoAtaque.APOYO_POSITIVO)
        {
            if (listaDeObjetivos[lanzador].ataqueAUsar.aumentaAtaqueF)
            {
                if (datosDelPersonaje[objetivo].aumentoAtaqueFisico <= 5)
                {
                    listaDeFlechas[indiceVectorFlechas] = 1;
                    datosDelPersonaje[objetivo].aumentoAtaqueFisico++;
                    
                    if(baseDeDatos.idioma == 1)
                    {
                        mensaje = "The physical attack of " + datosDelPersonaje[objetivo].nombreIngles + " has increased a little.";
                    }
                    else
                    {
                        mensaje = "El ataque físico de " + datosDelPersonaje[objetivo].nombre + " ha aumentado un poco.";
                    }
                }
                else
                {
                    listaDeFlechas[indiceVectorFlechas] = 0;

                    if (baseDeDatos.idioma == 1)
                    {
                        mensaje = "The physical attack of " + datosDelPersonaje[objetivo].nombreIngles + " can not increase more.";
                    }
                    else
                    {
                        mensaje = "El ataque físico de " + datosDelPersonaje[objetivo].nombre + " no puede aumentar más.";
                    }
                }

                mensajesMejora[indiceVectorFlechas] = mensaje;
                indiceVectorFlechas++;

                if(datosDelPersonaje[objetivo].aumentoAtaqueFisico >= 0)
                {
                    modificador = (2.0f + datosDelPersonaje[objetivo].aumentoAtaqueFisico) / 2.0f;
                }
                else
                {
                    modificador = 2.0f / (2.0f - datosDelPersonaje[objetivo].aumentoAtaqueFisico);
                }

                datosDelPersonaje[indiceReceptor].ataqueFisicoActual = (int)(datosDelPersonaje[indiceReceptor].ataqueFisicoModificado * modificador);
            }

            if (listaDeObjetivos[lanzador].ataqueAUsar.aumentaAtaqueM)
            {
                if (datosDelPersonaje[objetivo].aumentoAtaqueMagico <= 5)
                {
                    listaDeFlechas[indiceVectorFlechas] = 1;
                    datosDelPersonaje[objetivo].aumentoAtaqueMagico++;

                    if(baseDeDatos.idioma == 1)
                    {
                        mensaje = "The magic attack of " + datosDelPersonaje[objetivo].nombreIngles + " has increased a little.";
                    }
                    else
                    {
                        mensaje = "El ataque mágico de " + datosDelPersonaje[objetivo].nombre + " ha aumentado un poco.";
                    }
                }
                else
                {
                    listaDeFlechas[indiceVectorFlechas] = 0;
                   
                    if (baseDeDatos.idioma == 1)
                    {
                        mensaje = "The magic attack of " + datosDelPersonaje[objetivo].nombreIngles + " can not increase more.";
                    }
                    else
                    {
                        mensaje = "El ataque mágico de " + datosDelPersonaje[objetivo].nombre + " no puede aumentar más.";
                    }
                }

                mensajesMejora[indiceVectorFlechas] = mensaje;
                indiceVectorFlechas++;

                if (datosDelPersonaje[objetivo].aumentoAtaqueMagico >= 0)
                {
                    modificador = (2.0f + datosDelPersonaje[objetivo].aumentoAtaqueMagico) / 2.0f;
                }
                else
                {
                    modificador = 2.0f / (2.0f - datosDelPersonaje[objetivo].aumentoAtaqueMagico);
                }

                datosDelPersonaje[indiceReceptor].ataqueMagicoActual = (int)(datosDelPersonaje[indiceReceptor].ataqueMagicoModificado * modificador);
            }

            if (listaDeObjetivos[lanzador].ataqueAUsar.aumentaDefensaF)
            {
                if (datosDelPersonaje[objetivo].aumentoDefensaFisica <= 5)
                {
                    listaDeFlechas[indiceVectorFlechas] = 1;
                    datosDelPersonaje[objetivo].aumentoDefensaFisica++;

                    if (baseDeDatos.idioma == 1)
                    {
                        mensaje = "The physical defense of " + datosDelPersonaje[objetivo].nombreIngles + " has increased a little.";
                    }
                    else
                    {
                        mensaje = "La defensa física de " + datosDelPersonaje[objetivo].nombre + " ha aumentado un poco.";
                    }
                }
                else
                {
                    listaDeFlechas[indiceVectorFlechas] = 0;

                    if (baseDeDatos.idioma == 1)
                    {
                        mensaje = "The physical defense of " + datosDelPersonaje[objetivo].nombreIngles + " can not increase more.";
                    }
                    else
                    {
                        mensaje = "La defensa física de " + datosDelPersonaje[objetivo].nombre + " no puede aumentar más.";
                    }
                }

                mensajesMejora[indiceVectorFlechas] = mensaje;
                indiceVectorFlechas++;

                if (datosDelPersonaje[objetivo].aumentoDefensaFisica >= 0)
                {
                    modificador = (2.0f + datosDelPersonaje[objetivo].aumentoDefensaFisica) / 2.0f;
                }
                else
                {
                    modificador = 2.0f / (2.0f - datosDelPersonaje[objetivo].aumentoDefensaFisica);
                }

                datosDelPersonaje[indiceReceptor].defensaFisicaActual = (int)(datosDelPersonaje[indiceReceptor].defensaFisicaModificada * modificador);
            }

            if (listaDeObjetivos[lanzador].ataqueAUsar.aumentaDefensaM)
            {
                if (datosDelPersonaje[objetivo].aumentoDefensaMagica <= 5)
                {
                    listaDeFlechas[indiceVectorFlechas] = 1;
                    datosDelPersonaje[objetivo].aumentoDefensaMagica++;

                    if (baseDeDatos.idioma == 1)
                    {
                        mensaje = "The magical defense of " + datosDelPersonaje[objetivo].nombreIngles + " has increased a little.";
                    }
                    else
                    {
                        mensaje = "La defensa mágica de " + datosDelPersonaje[objetivo].nombre + " ha aumentado un poco.";
                    }
                }
                else
                {
                    listaDeFlechas[indiceVectorFlechas] = 0;

                    if (baseDeDatos.idioma == 1)
                    {
                        mensaje = "The magical defense of " + datosDelPersonaje[objetivo].nombreIngles + " can not increase more.";
                    }
                    else
                    {
                        mensaje = "La defensa mágica de " + datosDelPersonaje[objetivo].nombre + " no puede aumentar más.";
                    }
                }

                mensajesMejora[indiceVectorFlechas] = mensaje;
                indiceVectorFlechas++;

                if (datosDelPersonaje[objetivo].aumentoDefensaMagica >= 0)
                {
                    modificador = (2.0f + datosDelPersonaje[objetivo].aumentoDefensaMagica) / 2.0f;
                }
                else
                {
                    modificador = 2.0f / (2.0f - datosDelPersonaje[objetivo].aumentoDefensaMagica);
                }

                datosDelPersonaje[indiceReceptor].defensaMagicaActual = (int)(datosDelPersonaje[indiceReceptor].defensaMagicaModificada * modificador);
            }

            if (listaDeObjetivos[lanzador].ataqueAUsar.aumentaEva)
            {
                if (datosDelPersonaje[objetivo].aumentoEvasion <= 5)
                {
                    listaDeFlechas[indiceVectorFlechas] = 1;
                    datosDelPersonaje[objetivo].aumentoEvasion++;

                    if (baseDeDatos.idioma == 1)
                    {
                        mensaje = "The evasion of " + datosDelPersonaje[objetivo].nombreIngles + " has increased a little.";
                    }
                    else
                    {
                        mensaje = "La evasión de " + datosDelPersonaje[objetivo].nombre + " ha aumentado un poco.";
                    }
                }
                else
                {
                    listaDeFlechas[indiceVectorFlechas] = 0;

                    if (baseDeDatos.idioma == 1)
                    {
                        mensaje = "The evasion of " + datosDelPersonaje[objetivo].nombreIngles + " can not increase more.";
                    }
                    else
                    {
                        mensaje = "La evasión de " + datosDelPersonaje[objetivo].nombre + " no puede aumentar más.";
                    }
                }

                mensajesMejora[indiceVectorFlechas] = mensaje;
                indiceVectorFlechas++;

                if (datosDelPersonaje[objetivo].aumentoEvasion >= 0)
                {
                    modificador = (3.0f + datosDelPersonaje[objetivo].aumentoEvasion) / 3.0f;
                }
                else
                {
                    modificador = 3.0f / (3.0f - datosDelPersonaje[objetivo].aumentoEvasion);
                }

                datosDelPersonaje[indiceReceptor].evasionActual = modificador;
            }

            if (listaDeObjetivos[lanzador].ataqueAUsar.aumentaVel)
            {
                if (datosDelPersonaje[objetivo].aumentoVelocidad <= 5)
                {
                    listaDeFlechas[indiceVectorFlechas] = 1;
                    datosDelPersonaje[objetivo].aumentoVelocidad++;

                    if (baseDeDatos.idioma == 1)
                    {
                        mensaje = "The speed of " + datosDelPersonaje[objetivo].nombreIngles + " has increased a little.";
                    }
                    else
                    {
                        mensaje = "La velocidad de " + datosDelPersonaje[objetivo].nombre + " ha aumentado un poco.";
                    }
                }
                else
                {
                    listaDeFlechas[indiceVectorFlechas] = 0;

                    if (baseDeDatos.idioma == 1)
                    {
                        mensaje = "The speed of " + datosDelPersonaje[objetivo].nombreIngles + " can not increase more.";
                    }
                    else
                    {
                        mensaje = "La velocidad de " + datosDelPersonaje[objetivo].nombre + " no puede aumentar más.";
                    }
                }

                mensajesMejora[indiceVectorFlechas] = mensaje;
                indiceVectorFlechas++;

                if (datosDelPersonaje[objetivo].aumentoVelocidad >= 0)
                {
                    modificador = (2.0f + datosDelPersonaje[objetivo].aumentoVelocidad) / 2.0f;
                }
                else
                {
                    modificador = 2.0f / (2.0f - datosDelPersonaje[objetivo].aumentoVelocidad);
                }

                datosDelPersonaje[indiceReceptor].velocidadActual = (int)(datosDelPersonaje[indiceReceptor].velocidadModificada * modificador);
            }

            if (listaDeObjetivos[lanzador].ataqueAUsar.aumentaVid) //Falta revisar
            {
                if (datosDelPersonaje[objetivo].vidaActual < datosDelPersonaje[objetivo].vidaModificada)
                {
                    listaDeFlechas[indiceVectorFlechas] = 1;

                    if (datosDelPersonaje[objetivo].vidaActual + listaDeObjetivos[lanzador].ataqueAUsar.mejoraVid >= datosDelPersonaje[objetivo].vida)
                    {
                        int aumentoVida = datosDelPersonaje[objetivo].vidaModificada - datosDelPersonaje[objetivo].vidaActual;
                        datosDelPersonaje[objetivo].vidaActual = datosDelPersonaje[objetivo].vidaModificada;

                        if (baseDeDatos.idioma == 1)
                        {
                            mensaje = "The life of " + datosDelPersonaje[objetivo].nombreIngles + " has increased " + aumentoVida + " life points.";
                        }
                        else
                        {
                            mensaje = "La vida de " + datosDelPersonaje[objetivo].nombre + " ha aumentado " + aumentoVida + " puntos de vida.";
                        }
                    }
                    else
                    {
                        datosDelPersonaje[objetivo].vidaActual += listaDeObjetivos[lanzador].ataqueAUsar.mejoraVid;

                        if (baseDeDatos.idioma == 1)
                        {
                            mensaje = "The life of " + datosDelPersonaje[objetivo].nombreIngles + " has increased " + listaDeObjetivos[lanzador].ataqueAUsar.mejoraVid + " life points.";
                        }
                        else
                        {
                            mensaje = "La vida de " + datosDelPersonaje[objetivo].nombre + " ha aumentado " + listaDeObjetivos[lanzador].ataqueAUsar.mejoraVid + " puntos de vida.";
                        }
                    }
                }
                else
                {
                    listaDeFlechas[indiceVectorFlechas] = 0;

                    if (baseDeDatos.idioma == 1)
                    {
                        mensaje = "The life of " + datosDelPersonaje[objetivo].nombreIngles + " can not increase more.";
                    }
                    else
                    {
                        mensaje = "La vida de " + datosDelPersonaje[objetivo].nombre + " está ya al máximo.";
                    }
                }

                mensajesMejora[indiceVectorFlechas] = mensaje;
                indiceVectorFlechas++;
            }

            musica.ProduceEfecto(4);
        }
        else if (listaDeObjetivos[lanzador].ataqueAUsar.tipo == Ataque.tipoAtaque.APOYO_NEGATIVO)
        {
            if (listaDeObjetivos[lanzador].ataqueAUsar.aumentaAtaqueF)
            {
                if (datosDelPersonaje[objetivo].aumentoAtaqueFisico >= -5)
                {
                    listaDeFlechas[indiceVectorFlechas] = -1;
                    datosDelPersonaje[objetivo].aumentoAtaqueFisico--;

                    if (baseDeDatos.idioma == 1)
                    {
                        mensaje = "The physical attack of " + datosDelPersonaje[objetivo].nombre + " has been reduced a little.";
                    }
                    else
                    {
                        mensaje = "El ataque físico de " + datosDelPersonaje[objetivo].nombre + " se ha reducido un poco.";
                    }
                    
                }
                else
                {
                    listaDeFlechas[indiceVectorFlechas] = 0;

                    if (baseDeDatos.idioma == 1)
                    {
                        mensaje = "The physical attack of " + datosDelPersonaje[objetivo].nombre + " can't be reduced anymore.";
                    }
                    else
                    {
                        mensaje = "El ataque físico de " + datosDelPersonaje[objetivo].nombre + " no puede reducirse más.";
                    }
                }

                mensajesMejora[indiceVectorFlechas] = mensaje;
                indiceVectorFlechas++;

                if (datosDelPersonaje[objetivo].aumentoAtaqueFisico >= 0)
                {
                    modificador = (2.0f + datosDelPersonaje[objetivo].aumentoAtaqueFisico) / 2.0f;
                }
                else
                {
                    modificador = 2.0f / (2.0f - datosDelPersonaje[objetivo].aumentoAtaqueFisico);
                }

                datosDelPersonaje[indiceReceptor].ataqueFisicoActual = (int)(datosDelPersonaje[indiceReceptor].ataqueMagicoActual * modificador);
            }

            if (listaDeObjetivos[lanzador].ataqueAUsar.aumentaAtaqueM)
            {
                if (datosDelPersonaje[objetivo].aumentoAtaqueMagico >= -5)
                {
                    listaDeFlechas[indiceVectorFlechas] = -1;
                    datosDelPersonaje[objetivo].aumentoAtaqueMagico--;

                    if (baseDeDatos.idioma == 1)
                    {
                        mensaje = "El ataque mágico de " + datosDelPersonaje[objetivo].nombre + " has been reduced a little.";
                    }
                    else
                    {
                        mensaje = "El ataque mágico de " + datosDelPersonaje[objetivo].nombre + " se ha reducido un poco.";
                    }
                }
                else
                {
                    listaDeFlechas[indiceVectorFlechas] = 0;

                    if (baseDeDatos.idioma == 1)
                    {
                        mensaje = "El ataque mágico de " + datosDelPersonaje[objetivo].nombre + " can't be reduced anymore.";
                    }
                    else
                    {
                        mensaje = "El ataque mágico de " + datosDelPersonaje[objetivo].nombre + " no puede reducirse más.";
                    }
                }

                mensajesMejora[indiceVectorFlechas] = mensaje;
                indiceVectorFlechas++;

                if (datosDelPersonaje[objetivo].aumentoAtaqueMagico >= 0)
                {
                    modificador = (2.0f + datosDelPersonaje[objetivo].aumentoAtaqueMagico) / 2.0f;
                }
                else
                {
                    modificador = 2.0f / (2.0f - datosDelPersonaje[objetivo].aumentoAtaqueMagico);
                }

                datosDelPersonaje[indiceReceptor].ataqueMagicoActual = (int)(datosDelPersonaje[indiceReceptor].ataqueMagicoModificado * modificador);
            }

            if (listaDeObjetivos[lanzador].ataqueAUsar.aumentaDefensaF)
            {
                if (datosDelPersonaje[objetivo].aumentoDefensaFisica >= -5)
                {
                    listaDeFlechas[indiceVectorFlechas] = -1;
                    datosDelPersonaje[objetivo].aumentoDefensaFisica--;

                    if (baseDeDatos.idioma == 1)
                    {
                        mensaje = "The physical defense of " + datosDelPersonaje[objetivo].nombre + " has been reduced a little.";
                    }
                    else
                    {
                        mensaje = "La defensa física de " + datosDelPersonaje[objetivo].nombre + " se ha reducido un poco.";
                    }
                }
                else
                {
                    listaDeFlechas[indiceVectorFlechas] = 0;

                    if(baseDeDatos.idioma == 1)
                    {
                        mensaje = "The physical defense of " + datosDelPersonaje[objetivo].nombre + " can't be reduced anymore.";
                    }
                    else
                    {
                        mensaje = "La defensa física de " + datosDelPersonaje[objetivo].nombre + " no puede reducirse más.";
                    }
                }

                mensajesMejora[indiceVectorFlechas] = mensaje;
                indiceVectorFlechas++;

                if (datosDelPersonaje[objetivo].aumentoDefensaFisica >= 0)
                {
                    modificador = (2.0f + datosDelPersonaje[objetivo].aumentoDefensaFisica) / 2.0f;
                }
                else
                {
                    modificador = (2.0f / (2.0f - datosDelPersonaje[objetivo].aumentoDefensaFisica));
                }

                datosDelPersonaje[indiceReceptor].defensaFisicaActual = (int)(datosDelPersonaje[indiceReceptor].defensaFisicaModificada * modificador);
            }

            if (listaDeObjetivos[lanzador].ataqueAUsar.aumentaDefensaM)
            {
                if (datosDelPersonaje[objetivo].aumentoDefensaMagica >= -5)
                {
                    listaDeFlechas[indiceVectorFlechas] = -1;
                    datosDelPersonaje[objetivo].aumentoDefensaMagica--;

                    if (baseDeDatos.idioma == 1)
                    {
                        mensaje = "The magical defense of " + datosDelPersonaje[objetivo].nombre + " has been reduced a little.";
                    }
                    else
                    {
                        mensaje = "La defensa mágica de " + datosDelPersonaje[objetivo].nombre + " se ha reducido un poco.";
                    }
                }
                else
                {
                    listaDeFlechas[indiceVectorFlechas] = 0;

                    if (baseDeDatos.idioma == 1)
                    {
                        mensaje = "The magical defense of " + datosDelPersonaje[objetivo].nombre + " can't be reduced anymore.";
                    }
                    else
                    {
                        mensaje = "La defensa mágica de " + datosDelPersonaje[objetivo].nombre + " no puede reducirse más.";
                    }
                   
                }

                mensajesMejora[indiceVectorFlechas] = mensaje;
                indiceVectorFlechas++;

                if (datosDelPersonaje[objetivo].aumentoDefensaMagica >= 0)
                {
                    modificador = (2.0f + datosDelPersonaje[objetivo].aumentoDefensaMagica) / 2.0f;
                }
                else
                {
                    modificador = 2.0f / (2.0f - datosDelPersonaje[objetivo].aumentoDefensaMagica);
                }

                datosDelPersonaje[indiceReceptor].defensaMagicaActual = (int)(datosDelPersonaje[indiceReceptor].defensaMagicaModificada * modificador);
            }

            if (listaDeObjetivos[lanzador].ataqueAUsar.aumentaEva)
            {
                if (datosDelPersonaje[objetivo].aumentoEvasion >= -5)
                {
                    listaDeFlechas[indiceVectorFlechas] = -1;
                    datosDelPersonaje[objetivo].aumentoEvasion--;

                    if (baseDeDatos.idioma == 1)
                    {
                        mensaje = "The evasion of " + datosDelPersonaje[objetivo].nombre + " has been reduced a little.";
                    }
                    else
                    {
                        mensaje = "La evasión de " + datosDelPersonaje[objetivo].nombre + " se ha reducido un poco.";
                    }
                }
                else
                {
                    listaDeFlechas[indiceVectorFlechas] = 0;

                    if (baseDeDatos.idioma == 1)
                    {
                        mensaje = "The evasion of " + datosDelPersonaje[objetivo].nombre + " can't be reduced anymore.";
                    }
                    else
                    {
                        mensaje = "La evasión de " + datosDelPersonaje[objetivo].nombre + " no puede reducirse más.";
                    }
                }

                mensajesMejora[indiceVectorFlechas] = mensaje;
                indiceVectorFlechas++;

                if (datosDelPersonaje[objetivo].aumentoEvasion >= 0)
                {
                    modificador = (3.0f + datosDelPersonaje[objetivo].aumentoEvasion) / 3.0f;
                }
                else
                {
                    modificador = 3.0f / (3.0f - datosDelPersonaje[objetivo].aumentoEvasion);
                }

                datosDelPersonaje[indiceReceptor].evasionActual = modificador;
            }

            if (listaDeObjetivos[lanzador].ataqueAUsar.aumentaVel)
            {
                if (datosDelPersonaje[objetivo].aumentoVelocidad <= 5)
                {
                    listaDeFlechas[indiceVectorFlechas] = -1;
                    datosDelPersonaje[objetivo].aumentoVelocidad--;

                    if (baseDeDatos.idioma == 1)
                    {
                        mensaje = "The speed of " + datosDelPersonaje[objetivo].nombre + " has been reduced a little.";
                    }
                    else
                    {
                        mensaje = "La velocidad de " + datosDelPersonaje[objetivo].nombre + " se ha reducido un poco.";
                    }
                }
                else
                {
                    listaDeFlechas[indiceVectorFlechas] = 0;

                    if (baseDeDatos.idioma == 1)
                    {
                        mensaje = "The speed of " + datosDelPersonaje[objetivo].nombre + " can't be reduced anymore.";
                    }
                    else
                    {
                        mensaje = "La velocidad de " + datosDelPersonaje[objetivo].nombre + " no puede reducirse más.";
                    }
                }

                mensajesMejora[indiceVectorFlechas] = mensaje;
                indiceVectorFlechas++;

                if (datosDelPersonaje[objetivo].aumentoVelocidad >= 0)
                {
                    modificador = (2.0f + datosDelPersonaje[objetivo].aumentoVelocidad) / 2.0f;
                }
                else
                {
                    modificador = 2.0f / (2.0f - datosDelPersonaje[objetivo].aumentoVelocidad);
                }

                datosDelPersonaje[indiceReceptor].velocidadActual = (int)(datosDelPersonaje[indiceReceptor].velocidadModificada * modificador);
            }

            if (listaDeObjetivos[lanzador].ataqueAUsar.aumentaVid) //Falta revisar
            {
                listaDeFlechas[indiceVectorFlechas] = -1;

                if (datosDelPersonaje[objetivo].vidaActual - listaDeObjetivos[lanzador].ataqueAUsar.mejoraVid >= 0)
                {
                    datosDelPersonaje[objetivo].vidaActual -= listaDeObjetivos[lanzador].ataqueAUsar.mejoraVid;

                    if (baseDeDatos.idioma == 1)
                    {
                        mensaje = "The life of " + datosDelPersonaje[objetivo].nombre + " has been reduced " + listaDeObjetivos[lanzador].ataqueAUsar.mejoraVid + " life points.";
                    }
                    else
                    {
                        mensaje = "La vida de " + datosDelPersonaje[objetivo].nombre + " se ha reducido " + listaDeObjetivos[lanzador].ataqueAUsar.mejoraVid + " puntos de vida.";
                    }
                }
                else
                {
                    int diferencia = listaDeObjetivos[lanzador].ataqueAUsar.mejoraVid - datosDelPersonaje[objetivo].vidaActual;

                    datosDelPersonaje[objetivo].vidaActual = 0;

                    if (baseDeDatos.idioma == 1)
                    {
                        mensaje = "The life of " + datosDelPersonaje[objetivo].nombre + " has been reduced " + diferencia + " life points.";
                    }
                    else
                    {
                        mensaje = "La vida de " + datosDelPersonaje[objetivo].nombre + " se ha reducido " + diferencia + " puntos de vida.";
                    }
                }

                mensajesMejora[indiceVectorFlechas] = mensaje;
                indiceVectorFlechas++;
            }

            musica.ProduceEfecto(6);
        }
        else
        {
            if (listaDeObjetivos[lanzador].ataqueAUsar.aumentaAtaqueF)
            {
                if (listaDeObjetivos[indiceLanzador].ataqueAUsar.mejoraAtaqueF > 0)
                {
                    if (datosDelPersonaje[objetivo].aumentoAtaqueFisico <= 5)
                    {
                        listaDeFlechas[indiceVectorFlechas] = 1;
                        datosDelPersonaje[objetivo].aumentoAtaqueFisico++;

                        if (baseDeDatos.idioma == 1)
                        {
                            mensaje = "The physical attack of " + datosDelPersonaje[objetivo].nombre + " has increased a little.";
                        }
                        else
                        {
                            mensaje = "El ataque físico de " + datosDelPersonaje[objetivo].nombre + " ha aumentado un poco.";
                        }
                    }
                    else
                    {
                        listaDeFlechas[indiceVectorFlechas] = 0;

                        if (baseDeDatos.idioma == 1)
                        {
                            mensaje = "The physical attack of " + datosDelPersonaje[objetivo].nombre + " can not increase more.";
                        }
                        else
                        {
                            mensaje = "El ataque físico de " + datosDelPersonaje[objetivo].nombre + " no puede aumentar más.";
                        }
                    }
                }
                else
                {
                    if (datosDelPersonaje[objetivo].aumentoAtaqueFisico >= -5)
                    {
                        listaDeFlechas[indiceVectorFlechas] = -1;
                        datosDelPersonaje[objetivo].aumentoAtaqueFisico--;

                        if (baseDeDatos.idioma == 1)
                        {
                            mensaje = "The physical attack of " + datosDelPersonaje[objetivo].nombre + " has been reduced a little.";
                        }
                        else
                        {
                            mensaje = "El ataque físico de " + datosDelPersonaje[objetivo].nombre + " se ha reducido un poco.";
                        }
                    }
                    else
                    {
                        listaDeFlechas[indiceVectorFlechas] = 0;

                        if (baseDeDatos.idioma == 1)
                        {
                            mensaje = "The physical attack of " + datosDelPersonaje[objetivo].nombre + " can not reduce more.";
                        }
                        else
                        {
                            mensaje = "El ataque físico de " + datosDelPersonaje[objetivo].nombre + " no puede reducirse más.";
                        }
                    }
                }

                mensajesMejora[indiceVectorFlechas] = mensaje;
                indiceVectorFlechas++;

                if (datosDelPersonaje[objetivo].aumentoAtaqueFisico >= 0)
                {
                    modificador = (2.0f + datosDelPersonaje[objetivo].aumentoAtaqueFisico) / 2.0f;
                }
                else
                {
                    modificador = 2.0f / (2.0f - datosDelPersonaje[objetivo].aumentoAtaqueFisico);
                }

                datosDelPersonaje[indiceReceptor].ataqueFisicoActual = (int)(datosDelPersonaje[indiceReceptor].ataqueMagicoActual * modificador);
            }

            if (listaDeObjetivos[lanzador].ataqueAUsar.aumentaAtaqueM)
            {
                if (listaDeObjetivos[indiceLanzador].ataqueAUsar.mejoraAtaqueM > 0)
                {
                    if (datosDelPersonaje[objetivo].aumentoAtaqueMagico <= 5)
                    {
                        listaDeFlechas[indiceVectorFlechas] = 1;
                        datosDelPersonaje[objetivo].aumentoAtaqueMagico++;

                        if (baseDeDatos.idioma == 1)
                        {
                            mensaje = "The magical attack of " + datosDelPersonaje[objetivo].nombre + " has increased a little.";
                        }
                        else
                        {
                            mensaje = "El ataque mágico de " + datosDelPersonaje[objetivo].nombre + " ha aumentado un poco.";
                        }
                    }
                    else
                    {
                        listaDeFlechas[indiceVectorFlechas] = 0;

                        if (baseDeDatos.idioma == 1)
                        {
                            mensaje = "The magical attack of " + datosDelPersonaje[objetivo].nombre + " can not increase more.";
                        }
                        else
                        {
                            mensaje = "El ataque mágico de " + datosDelPersonaje[objetivo].nombre + " no puede aumentar más.";
                        }
                    }
                }
                else
                {
                    if (datosDelPersonaje[objetivo].aumentoAtaqueMagico >= -5)
                    {
                        listaDeFlechas[indiceVectorFlechas] = -1;
                        datosDelPersonaje[objetivo].aumentoAtaqueMagico--;

                        if (baseDeDatos.idioma == 1)
                        {
                            mensaje = "The magical attack of " + datosDelPersonaje[objetivo].nombre + " has been reduced a little.";
                        }
                        else
                        {
                            mensaje = "El ataque mágico de " + datosDelPersonaje[objetivo].nombre + " se ha reducido un poco.";
                        }
                    }
                    else
                    {
                        listaDeFlechas[indiceVectorFlechas] = 0;

                        if (baseDeDatos.idioma == 1)
                        {
                            mensaje = "The magical attack of " + datosDelPersonaje[objetivo].nombre + " can not reduce more.";
                        }
                        else
                        {
                            mensaje = "El ataque mágico de " + datosDelPersonaje[objetivo].nombre + " no puede reducirse más.";
                        }
                    }
                }

                if (datosDelPersonaje[objetivo].aumentoAtaqueMagico >= 0)
                {
                    modificador = (2.0f + datosDelPersonaje[objetivo].aumentoAtaqueMagico) / 2.0f;
                }
                else
                {
                    modificador = 2.0f / (2.0f - datosDelPersonaje[objetivo].aumentoAtaqueMagico);
                }

                datosDelPersonaje[indiceReceptor].ataqueMagicoActual = (int)(datosDelPersonaje[indiceReceptor].ataqueMagicoModificado * modificador);
            }

            if (listaDeObjetivos[lanzador].ataqueAUsar.aumentaDefensaF)
            {
                if (listaDeObjetivos[indiceLanzador].ataqueAUsar.mejoraDefensaF > 0)
                {
                    if (datosDelPersonaje[objetivo].aumentoDefensaFisica <= 5)
                    {
                        listaDeFlechas[indiceVectorFlechas] = 1;
                        datosDelPersonaje[objetivo].aumentoDefensaFisica++;

                        if (baseDeDatos.idioma == 1)
                        {
                            mensaje = "The physical defense of " + datosDelPersonaje[objetivo].nombre + " has increased a little.";
                        }
                        else
                        {
                            mensaje = "La defensa física de " + datosDelPersonaje[objetivo].nombre + " ha aumentado un poco.";
                        }
                    }
                    else
                    {
                        listaDeFlechas[indiceVectorFlechas] = 0;

                        if (baseDeDatos.idioma == 1)
                        {
                            mensaje = "The physical defense of " + datosDelPersonaje[objetivo].nombre + " can not increase more.";
                        }
                        else
                        {
                            mensaje = "La defensa física de " + datosDelPersonaje[objetivo].nombre + " no puede aumentar más.";
                        }
                    }
                }
                else
                {
                    if (datosDelPersonaje[objetivo].aumentoDefensaFisica >= -5)
                    {
                        listaDeFlechas[indiceVectorFlechas] = -1;
                        datosDelPersonaje[objetivo].aumentoDefensaFisica--;

                        if (baseDeDatos.idioma == 1)
                        {
                            mensaje = "The physical defense of " + datosDelPersonaje[objetivo].nombre + " has been reduced a little.";
                        }
                        else
                        {
                            mensaje = "La defensa física de " + datosDelPersonaje[objetivo].nombre + " se ha reducido un poco.";
                        }
                    }
                    else
                    {
                        listaDeFlechas[indiceVectorFlechas] = 0;

                        if (baseDeDatos.idioma == 1)
                        {
                            mensaje = "The physical defense of " + datosDelPersonaje[objetivo].nombre + " can not reduce more.";
                        }
                        else
                        {
                            mensaje = "La defensa física de " + datosDelPersonaje[objetivo].nombre + " no puede reducirse más.";
                        }
                    }
                }

                mensajesMejora[indiceVectorFlechas] = mensaje;
                indiceVectorFlechas++;

                if (datosDelPersonaje[objetivo].aumentoDefensaFisica >= 0)
                {
                    modificador = (2.0f + datosDelPersonaje[objetivo].aumentoDefensaFisica) / 2.0f;
                }
                else
                {
                    modificador = 2.0f / (2.0f - datosDelPersonaje[objetivo].aumentoDefensaFisica);
                }

                datosDelPersonaje[indiceReceptor].defensaFisicaActual = (int)(datosDelPersonaje[indiceReceptor].defensaFisicaModificada * modificador);
            }

            if (listaDeObjetivos[lanzador].ataqueAUsar.aumentaDefensaM)
            {
                if (listaDeObjetivos[indiceLanzador].ataqueAUsar.mejoraDefensaM > 0)
                {
                    if (datosDelPersonaje[objetivo].aumentoDefensaMagica <= 5)
                    {
                        listaDeFlechas[indiceVectorFlechas] = 1;
                        datosDelPersonaje[objetivo].aumentoDefensaMagica++;

                        if (baseDeDatos.idioma == 1)
                        {
                            mensaje = "The magical defense of " + datosDelPersonaje[objetivo].nombre + " has increased a little.";
                        }
                        else
                        {
                            mensaje = "La defensa mágica de " + datosDelPersonaje[objetivo].nombre + " ha aumentado un poco.";
                        }
                    }
                    else
                    {
                        listaDeFlechas[indiceVectorFlechas] = 0;

                        if (baseDeDatos.idioma == 1)
                        {
                            mensaje = "The magical defense of " + datosDelPersonaje[objetivo].nombre + " can not increase more.";
                        }
                        else
                        {
                            mensaje = "La defensa mágica de " + datosDelPersonaje[objetivo].nombre + " no puede aumentar más.";
                        }
                    }
                }
                else
                {
                    if (datosDelPersonaje[objetivo].aumentoDefensaMagica >= -5)
                    {
                        listaDeFlechas[indiceVectorFlechas] = -1;
                        datosDelPersonaje[objetivo].aumentoDefensaMagica--;

                        if (baseDeDatos.idioma == 1)
                        {
                            mensaje = "The magical defense of " + datosDelPersonaje[objetivo].nombre + " has been reduced a little.";
                        }
                        else
                        {
                            mensaje = "La defensa mágica de " + datosDelPersonaje[objetivo].nombre + " se ha reducido un poco.";
                        }
                    }
                    else
                    {
                        listaDeFlechas[indiceVectorFlechas] = 0;

                        if(baseDeDatos.idioma == 1)
                        {
                            mensaje = "The magical defense of " + datosDelPersonaje[objetivo].nombre + " can not reduce more.";
                        }
                        else
                        {
                            mensaje = "La defensa mágica de " + datosDelPersonaje[objetivo].nombre + " no puede reducirse más.";
                        }
                    }
                }

                mensajesMejora[indiceVectorFlechas] = mensaje;
                indiceVectorFlechas++;

                if (datosDelPersonaje[objetivo].aumentoDefensaMagica >= 0)
                {
                    modificador = (2.0f + datosDelPersonaje[objetivo].aumentoDefensaMagica) / 2.0f;
                }
                else
                {
                    modificador = 2.0f / (2.0f - datosDelPersonaje[objetivo].aumentoDefensaMagica);
                }

                datosDelPersonaje[indiceReceptor].defensaMagicaActual = (int)(datosDelPersonaje[indiceReceptor].defensaMagicaModificada * modificador);
            }

            if (listaDeObjetivos[lanzador].ataqueAUsar.aumentaEva)
            {
                if (listaDeObjetivos[indiceLanzador].ataqueAUsar.mejoraEva > 0)
                {
                    if (datosDelPersonaje[objetivo].aumentoEvasion <= 5)
                    {
                        listaDeFlechas[indiceVectorFlechas] = 1;
                        datosDelPersonaje[objetivo].aumentoEvasion++;

                        if (baseDeDatos.idioma == 1)
                        {
                            mensaje = "The evasion of " + datosDelPersonaje[objetivo].nombre + " has increased a little.";
                        }
                        else
                        {
                            mensaje = "La evasión de " + datosDelPersonaje[objetivo].nombre + " ha aumentado un poco.";
                        }
                    }
                    else
                    {
                        listaDeFlechas[indiceVectorFlechas] = 0;

                        if (baseDeDatos.idioma == 1)
                        {
                            mensaje = "The evasion of " + datosDelPersonaje[objetivo].nombre + " can not increase more.";
                        }
                        else
                        {
                            mensaje = "La evasión de " + datosDelPersonaje[objetivo].nombre + " no puede aumentar más.";
                        }
                    }
                }
                else
                {
                    if (datosDelPersonaje[objetivo].aumentoEvasion >= -5)
                    {
                        listaDeFlechas[indiceVectorFlechas] = -1;
                        datosDelPersonaje[objetivo].aumentoEvasion--;

                        if (baseDeDatos.idioma == 1)
                        {
                            mensaje = "The evasion of " + datosDelPersonaje[objetivo].nombre + " has been reduced a little.";
                        }
                        else
                        {
                            mensaje = "La evasión de " + datosDelPersonaje[objetivo].nombre + " se ha reducido un poco.";
                        }
                    }
                    else
                    {
                        listaDeFlechas[indiceVectorFlechas] = 0;

                        if (baseDeDatos.idioma == 1)
                        {
                            mensaje = "The evasion of " + datosDelPersonaje[objetivo].nombre + " can not reduce more.";
                        }
                        else
                        {
                            mensaje = "La evasión de " + datosDelPersonaje[objetivo].nombre + " no puede reducirse más.";
                        }
                    }
                }

                mensajesMejora[indiceVectorFlechas] = mensaje;
                indiceVectorFlechas++;

                if (datosDelPersonaje[objetivo].aumentoEvasion >= 0)
                {
                    modificador = (3.0f + datosDelPersonaje[objetivo].aumentoEvasion) / 3.0f;
                }
                else
                {
                    modificador = 3.0f / (3.0f - datosDelPersonaje[objetivo].aumentoEvasion);
                }

                datosDelPersonaje[indiceReceptor].evasionActual = modificador;
            }

            if (listaDeObjetivos[lanzador].ataqueAUsar.aumentaVel)
            {
                if (listaDeObjetivos[indiceLanzador].ataqueAUsar.mejoraVel > 0)
                {
                    if (datosDelPersonaje[objetivo].aumentoVelocidad <= 5)
                    {
                        listaDeFlechas[indiceVectorFlechas] = 1;
                        datosDelPersonaje[objetivo].aumentoVelocidad++;

                        if (baseDeDatos.idioma == 1)
                        {
                            mensaje = "The speed of " + datosDelPersonaje[objetivo].nombre + " has increased a little.";
                        }
                        else
                        {
                            mensaje = "La velocidad de " + datosDelPersonaje[objetivo].nombre + " ha aumentado un poco.";
                        }
                    }
                    else
                    {
                        listaDeFlechas[indiceVectorFlechas] = 0;

                        if (baseDeDatos.idioma == 1)
                        {
                            mensaje = "The speed of " + datosDelPersonaje[objetivo].nombre + " can not increase more.";
                        }
                        else
                        {
                            mensaje = "La velocidad de " + datosDelPersonaje[objetivo].nombre + " no puede aumentar más.";
                        }
                    }
                }
                else
                {
                    if (datosDelPersonaje[objetivo].aumentoVelocidad >= -5)
                    {
                        listaDeFlechas[indiceVectorFlechas] = -1;
                        datosDelPersonaje[objetivo].aumentoVelocidad--;

                        if (baseDeDatos.idioma == 1)
                        {
                            mensaje = "The speed of " + datosDelPersonaje[objetivo].nombre + " has been reduced a little.";
                        }
                        else
                        {
                            mensaje = "La velocidad de " + datosDelPersonaje[objetivo].nombre + " se ha reducido un poco.";
                        }
                    }
                    else
                    {
                        listaDeFlechas[indiceVectorFlechas] = 0;

                        if (baseDeDatos.idioma == 1)
                        {
                            mensaje = "The speed of " + datosDelPersonaje[objetivo].nombre + " can not reduce more.";
                        }
                        else
                        {
                            mensaje = "La velocidad de " + datosDelPersonaje[objetivo].nombre + " no puede reducirse más.";
                        }
                    }
                }

                mensajesMejora[indiceVectorFlechas] = mensaje;
                indiceVectorFlechas++;

                if (datosDelPersonaje[objetivo].aumentoVelocidad >= 0)
                {
                    modificador = (2.0f + datosDelPersonaje[objetivo].aumentoVelocidad) / 2.0f;
                }
                else
                {
                    modificador = 2.0f / (2.0f - datosDelPersonaje[objetivo].aumentoVelocidad);
                }

                datosDelPersonaje[indiceReceptor].velocidadActual = (int)(datosDelPersonaje[indiceReceptor].velocidadModificada * modificador);
            }

            if (listaDeObjetivos[lanzador].ataqueAUsar.aumentaVid)
            {
                if (listaDeObjetivos[indiceLanzador].ataqueAUsar.mejoraVid > 0)
                {
                    if (datosDelPersonaje[objetivo].vidaActual < datosDelPersonaje[objetivo].vida)
                    {
                        listaDeFlechas[indiceVectorFlechas] = 1;

                        if (datosDelPersonaje[objetivo].vidaActual + listaDeObjetivos[lanzador].ataqueAUsar.mejoraVid >= datosDelPersonaje[objetivo].vida)
                        {
                            int aumentoVida = datosDelPersonaje[objetivo].vidaModificada - datosDelPersonaje[objetivo].vidaActual;
                            datosDelPersonaje[objetivo].vidaActual = datosDelPersonaje[objetivo].vida;

                            if (baseDeDatos.idioma == 1)
                            {
                                mensaje = "The life of " + datosDelPersonaje[objetivo].nombreIngles + " has increased " + aumentoVida + " life points.";
                            }
                            else
                            {
                                mensaje = "La vida de " + datosDelPersonaje[objetivo].nombre + " ha aumentado " + aumentoVida + " puntos de vida.";
                            }
                        }
                        else
                        {
                            datosDelPersonaje[objetivo].vidaActual += listaDeObjetivos[lanzador].ataqueAUsar.mejoraVid;

                            if (baseDeDatos.idioma == 1)
                            {
                                mensaje = "The life of " + datosDelPersonaje[objetivo].nombreIngles + " has increased " + listaDeObjetivos[lanzador].ataqueAUsar.mejoraVid + " life points.";
                            }
                            else
                            {
                                mensaje = "La vida de " + datosDelPersonaje[objetivo].nombre + " ha aumentado " + listaDeObjetivos[lanzador].ataqueAUsar.mejoraVid + " puntos de vida.";
                            }
                        }
                    }
                    else
                    {
                        listaDeFlechas[indiceVectorFlechas] = 0;

                        if (baseDeDatos.idioma == 1)
                        {
                            mensaje = "The life of " + datosDelPersonaje[objetivo].nombreIngles + " is already at maximum.";
                        }
                        else
                        {
                            mensaje = "La vida de " + datosDelPersonaje[objetivo].nombre + " está ya al máximo.";
                        }
                    }
                }
                else
                {
                    listaDeFlechas[indiceVectorFlechas] = -1;

                    if (datosDelPersonaje[objetivo].vidaActual - listaDeObjetivos[lanzador].ataqueAUsar.mejoraVid >= 0)
                    {
                        datosDelPersonaje[objetivo].vidaActual -= listaDeObjetivos[lanzador].ataqueAUsar.mejoraVid;

                        if (baseDeDatos.idioma == 1)
                        {
                            mensaje = "The life of " + datosDelPersonaje[objetivo].nombreIngles + " it has been reduced " + listaDeObjetivos[lanzador].ataqueAUsar.mejoraVid + " life points.";
                        }
                        else
                        {
                            mensaje = "La vida de " + datosDelPersonaje[objetivo].nombre + " se ha reducido " + listaDeObjetivos[lanzador].ataqueAUsar.mejoraVid + " puntos de vida.";
                        }
                    }
                    else
                    {
                        int diferencia = listaDeObjetivos[lanzador].ataqueAUsar.mejoraVid - datosDelPersonaje[objetivo].vidaActual;

                        datosDelPersonaje[objetivo].vidaActual = 0;

                        if (baseDeDatos.idioma == 1)
                        {
                            mensaje = "The life of " + datosDelPersonaje[objetivo].nombreIngles + " it has been reduced " + diferencia + " life points.";
                        }
                        else
                        {
                            mensaje = "La vida de " + datosDelPersonaje[objetivo].nombre + " se ha reducido " + diferencia + " puntos de vida.";
                        }
                    }
                }

                mensajesMejora[indiceVectorFlechas] = mensaje;
                indiceVectorFlechas++;
            }

            musica.ProduceEfecto(4);
        }

        numeroMejoras = indiceVectorFlechas;
        indiceVectorFlechas = 0;
        

        StartCoroutine(EsperarTextoApoyo(2, mensajesMejora[indiceVectorFlechas], listaDeObjetivos[lanzador].receptor, listaDeObjetivos[lanzador].lanzador));
    }



    IEnumerator EsperaTextoGeneral (string texto, bool termina)
    {
        if (combateGanado)
        {
            combateGanado = false;
        }

        pasandoTexto = true;

        TextBox.MuestraTexto(texto, true);

        yield return new WaitForSeconds(2);

        if (termina)
        {
            yield return new WaitForSeconds(1);
            TerminaCombate(false, true);
        }
        else
        {
            pasandoTexto = false;
            
            TextBox.OcultaTextoFinCombate();

            if(movimientoAliadoGastado < movimientoAliados)
            {
                ActivarMenuCombate();
            }
        }
    }



    IEnumerator EsperarTextoCombate(float segundosEspera, int indiceReceptor, int indiceLanzador, float efec)
    {
        string mensaje;

        if (efec == 2)
        {
            if(baseDeDatos.idioma == 1)
            {
                mensaje = datosDelPersonaje[indiceLanzador].nombreIngles + " use " + listaDeObjetivos[indiceLanzador].ataqueAUsar.nombreIngles + " against " + datosDelPersonaje[indiceReceptor].nombreIngles + ". It is very effective.";
            }
            else
            {
                mensaje = datosDelPersonaje[indiceLanzador].nombre + " utiliza " + listaDeObjetivos[indiceLanzador].ataqueAUsar.nombre + " contra " + datosDelPersonaje[indiceReceptor].nombre + ". Es muy eficaz.";
            }
        }
        else if(efec == 0.5f)
        {
            if (baseDeDatos.idioma == 1)
            {
                mensaje = datosDelPersonaje[indiceLanzador].nombreIngles + " use " + listaDeObjetivos[indiceLanzador].ataqueAUsar.nombreIngles + " against " + datosDelPersonaje[indiceReceptor].nombreIngles + ". It is not effective.";
            }
            else
            {
                mensaje = datosDelPersonaje[indiceLanzador].nombre + " utiliza " + listaDeObjetivos[indiceLanzador].ataqueAUsar.nombre + " contra " + datosDelPersonaje[indiceReceptor].nombre + ". Es poco eficaz.";
            }
        }
        else 
        {
            if (baseDeDatos.idioma == 1)
            {
                mensaje = datosDelPersonaje[indiceLanzador].nombreIngles + " use " + listaDeObjetivos[indiceLanzador].ataqueAUsar.nombreIngles + " against " + datosDelPersonaje[indiceReceptor].nombreIngles + ".";
            }
            else
            {
                mensaje = datosDelPersonaje[indiceLanzador].nombre + " utiliza " + listaDeObjetivos[indiceLanzador].ataqueAUsar.nombre + " contra " + datosDelPersonaje[indiceReceptor].nombre + ".";
            }
        }

        animacionEnCurso = true;

        TextBox.MuestraTexto(mensaje, true);

        StartCoroutine(EsperaAnimacionDanio(1, indiceReceptor));

        yield return new WaitForSeconds(segundosEspera);

        if (datosDelPersonaje[indiceReceptor].vidaActual == 0)
        {
            switch (indiceReceptor)
            {
                case 0:
                    aliado1.SetActive(false);
                    break;
                case 1:
                    aliado2.SetActive(false);
                    break;
                case 2:
                    aliado3.SetActive(false);
                    break;
                case 3:
                    if (datosDelPersonaje[indiceReceptor].tipo == Personajes.tipoPersonaje.PEQUENIO)
                    {
                        enemigo1.SetActive(false);
                    }
                    else if (datosDelPersonaje[indiceReceptor].tipo == Personajes.tipoPersonaje.GRANDE)
                    {
                        enemigoGrande1.SetActive(false);
                    }
                    else
                    {
                        enemigoGigante.SetActive(false);
                    }
                    break;
                case 4:
                    if (datosDelPersonaje[indiceReceptor].tipo == Personajes.tipoPersonaje.PEQUENIO)
                    {
                        enemigo2.SetActive(false);
                    }
                    else if (datosDelPersonaje[indiceReceptor].tipo == Personajes.tipoPersonaje.GRANDE)
                    {
                        enemigoGrande2.SetActive(false);
                    }
                    break;
                case 5:
                    enemigo3.SetActive(false);
                    break;
            }
        }
        yield return new WaitForSeconds(2);

        
    }



    /*
    IEnumerator EsperaProblemaEstado(bool activa)
    {
        yield return new WaitForSeconds(1);
    }
    */



    IEnumerator EsperaAnimacionDanio(int segundosEspera, int indice)
    {
        controlDeDanio.SetBool("danio", true);

        yield return new WaitForSeconds(0.39f);

        if (indice == 0)
        {
            controlEfectoDanioAl1.SetBool("activo", false);
            controlEfecto1.SetActive(false);
        }
        else if (indice == 1)
        {
            controlEfectoDanioAl2.SetBool("activo", false);
            controlEfecto2.SetActive(false);
        }
        else if (indice == 2)
        {
            controlEfectoDanioAl3.SetBool("activo", false);
            controlEfecto3.SetActive(false);
        }
        else if (indice == 3)
        {
            controlEfectoDanioEn1.SetBool("activo", false);
            controlEfecto4.SetActive(false);
        }
        else if (indice == 4)
        {
            controlEfectoDanioEn2.SetBool("activo", false);
            controlEfecto5.SetActive(false);
        }
        else if (indice == 5)
        {
            controlEfectoDanioEn3.SetBool("activo", false);
            controlEfecto6.SetActive(false);
        }

        yield return new WaitForSeconds(segundosEspera - 0.39f);

        if (indice == 0)
        {
            if (datosDelPersonaje[indice].vidaActual == 0)
            {
                //animadorAl1.SetBool("muerte", true);
                StartCoroutine(EsperarMuerte(indice));
            }

            animadorAl1.SetBool("danio", false);
            danioAliado1.gameObject.SetActive(false);
        }
        else if (indice == 1)
        {
            if (datosDelPersonaje[indice].vidaActual == 0)
            {
                //animadorAl2.SetBool("muerte", true);
                StartCoroutine(EsperarMuerte(indice));
            }

            animadorAl2.SetBool("danio", false);
            danioAliado2.gameObject.SetActive(false);
        }
        else if (indice == 2)
        {
            if (datosDelPersonaje[indice].vidaActual == 0)
            {
                //animadorAl3.SetBool("muerte", true);
                StartCoroutine(EsperarMuerte(indice));
            }

            animadorAl3.SetBool("danio", false);
            danioAliado3.gameObject.SetActive(false);
        }
        else if (indice == 3)
        {
            animadorEn1.SetBool("danio", false);

            if(datosDelPersonaje[indice].vidaActual == 0)
            {
                animadorEn1.SetBool("muerte", true);
                StartCoroutine(EsperarMuerte(indice));
            }

            if (datosDelPersonaje[3].tipo == Personajes.tipoPersonaje.PEQUENIO)
            {
                danioEnemigoPeq1.gameObject.SetActive(false);
            }
            else if (datosDelPersonaje[3].tipo == Personajes.tipoPersonaje.GRANDE)
            {
                danioEnemigoGran1.gameObject.SetActive(false);
            }
            else
            {
                danioEnemigoGig.gameObject.SetActive(false);
            }
        }
        else if (indice == 4)
        {
            animadorEn2.SetBool("danio", false);

            if (datosDelPersonaje[indice].vidaActual == 0)
            {
                animadorEn2.SetBool("muerte", true);
                StartCoroutine(EsperarMuerte(indice));
            }

            if (datosDelPersonaje[4].tipo == Personajes.tipoPersonaje.PEQUENIO)
            {
                danioEnemigoPeq2.gameObject.SetActive(false);
            }
            else
            {
                danioEnemigoGran2.gameObject.SetActive(false);
            }
        }
        else if (indice == 5)
        {
            animadorEn3.SetBool("danio", false);

            if (datosDelPersonaje[indice].vidaActual == 0)
            {
                animadorEn3.SetBool("muerte", true);
                StartCoroutine(EsperarMuerte(indice));
            }

            danioEnemigoPeq3.gameObject.SetActive(false);
        }

        controlDeDanio.SetBool("danio", false);

        yield return new WaitForSeconds(1.5f);

        animacionEnCurso = false;

        if (ataqueLanzado == (2 + movimientoEnemigo))
        {
            TerminaTurnoEnemigo();
        }
        else
        {
            ataqueLanzado++;
        }
    }



    IEnumerator EsperarMuerte(int indice)
    {
        yield return new WaitForSeconds(0.21f);

        if (indice == 0)
        {
            //animadorAl1.SetBool("muerte", false);
            //aliado1.transform.position = new Vector3 (aliado1.transform.position.x, aliado1.transform.position.y, -5);
            aliado1.SetActive(false);
        }
        else if (indice == 1)
        {
            //animadorAl2.SetBool("muerte", false);
            aliado2.SetActive(false);
        }
        else if (indice == 2)
        {
            //animadorAl3.SetBool("muerte", false);
            aliado3.SetActive(false);
        }
        else if (indice == 3)
        {
            //animadorEn1.SetBool("muerte", false);
            Vector3 nuevaPos = new Vector3(enemigo1.transform.position.x, enemigo1.transform.position.y, enemigo1.transform.position.z + 6);
            enemigo1.transform.position = nuevaPos;
        }
        else if (indice == 4)
        {
            //animadorEn2.SetBool("muerte", false);
            Vector3 nuevaPos = new Vector3(enemigo2.transform.position.x, enemigo2.transform.position.y, enemigo2.transform.position.z + 6);
            enemigo2.transform.position = nuevaPos;
        }
        else if (indice == 5)
        {
            //animadorEn3.SetBool("muerte", false);
            Vector3 nuevaPos = new Vector3(enemigo3.transform.position.x, enemigo3.transform.position.y, enemigo3.transform.position.z + 6);
            enemigo3.transform.position = nuevaPos;
        }
    }



    IEnumerator EsperarTextoApoyo(int segundosEspera, string mensaje, int indiceReceptor, int indiceLanzador)
    {
        aplicaApoyo = true;

        string mensaje2;

        if(baseDeDatos.idioma == 1)
        {
            mensaje2 = datosDelPersonaje[indiceLanzador].nombreIngles + " use " + listaDeObjetivos[indiceLanzador].ataqueAUsar.nombreIngles + " with " + datosDelPersonaje[indiceReceptor].nombreIngles + ".";
        }
        else
        {
            mensaje2 = datosDelPersonaje[indiceLanzador].nombre + " utiliza " + listaDeObjetivos[indiceLanzador].ataqueAUsar.nombre + " con " + datosDelPersonaje[indiceReceptor].nombre + ".";
        }

        animacionEnCurso = true;

        TextBox.MuestraTexto(mensaje2, true);

        yield return new WaitForSeconds(segundosEspera);

        StartCoroutine(EsperaTextoMejora(2, mensaje, indiceReceptor));
    }



    IEnumerator EsperaTextoMejora(int segundosEspera, string mensaje, int indiceReceptor)
    {
        TextBox.MuestraTexto(mensaje, true);

        StartCoroutine(EsperaAnimacionApoyo(2, indiceReceptor));
        yield return new WaitForSeconds(segundosEspera);
    }



    IEnumerator EsperaAnimacionApoyo(int segundosEspera, int indice)
    {
        if (listaDeFlechas[indiceVectorFlechas] < 0)
        {
            reduce = true;
            aumenta = false;

            if (listaDeFlechas[indiceVectorFlechas] == -1)
            {
                flechaEstadisticasEmpeoro[indiceFlechaMejora].sprite = flechaMenos;
            }
            else
            {
                flechaEstadisticasEmpeoro[indiceFlechaMejora].sprite = flechaDobleMenos;
            }

            flechaEstadisticaActiva = true;
            flechaEstadisticasEmpeoro[indiceFlechaMejora].gameObject.SetActive(true);
        }
        else 
        {
            aumenta = true;
            reduce = false;

            if (listaDeFlechas[indiceVectorFlechas] == 1)
            {
                flechaEstadisticasMejora[indice].sprite = flechaMas;
            }
            else if (listaDeFlechas[indiceVectorFlechas] == 2)
            {
                flechaEstadisticasMejora[indice].sprite = flechaDobleMas;
            }
            else
            {
                aumenta = false;
                reduce = false;

                flechaEstadisticasMejora[indice].sprite = simboloIgual;
            }

            flechaEstadisticaActiva = true;
            flechaEstadisticasMejora[indice].gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(segundosEspera);

        if (listaDeFlechas[indiceVectorFlechas] < 0)
        {
            flechaEstadisticasEmpeoro[indice].gameObject.SetActive(false);
            flechaEstadisticasEmpeoro[indice].transform.localScale = new Vector3(flechaEstadisticasEmpeoro[indiceFlechaMejora].transform.localScale.x, 0, flechaEstadisticasEmpeoro[indiceFlechaMejora].transform.localScale.z);
        }
        else
        {
            flechaEstadisticasMejora[indice].gameObject.SetActive(false);
            flechaEstadisticasMejora[indice].transform.localScale = new Vector3(flechaEstadisticasMejora[indiceFlechaMejora].transform.localScale.x, 0, flechaEstadisticasMejora[indiceFlechaMejora].transform.localScale.z);
        }

        if (numeroMejoras == indiceVectorFlechas + 1)
        {
            aplicaApoyo = false;
            animacionEnCurso = false;
            flechaEstadisticaActiva = false;

            if (ataqueLanzado == (2 + movimientoEnemigo))
            {
                TerminaTurnoEnemigo();
            }
            else
            {
                ataqueLanzado++;
            }
        }
        else
        {
            indiceVectorFlechas++;

            StartCoroutine(EsperaTextoMejora(2, mensajesMejora[indiceVectorFlechas], listaDeObjetivos[ataqueLanzado].receptor));
        }

        yield return new WaitForSeconds(1.5f);
    }



    IEnumerator EsperaInicio()
    {
        animacionEnCurso = true;
        yield return new WaitForSeconds(0.9f);
        animacionEnCurso = false;
    }



    IEnumerator EsperaMenuRecompensa()
    {
        if(baseDeDatos.idioma == 1)
        {
            menuRecompensa.transform.GetChild(0).GetComponent<Text>().text = "Victory!";
            
            menuRecompensa.transform.GetChild(4).GetChild(4).GetComponent<Text>().text = "Level Up";
            menuRecompensa.transform.GetChild(5).GetChild(4).GetComponent<Text>().text = "Level Up";
            menuRecompensa.transform.GetChild(6).GetChild(4).GetComponent<Text>().text = "Level Up";

            menuRecompensa.transform.GetChild(7).GetChild(0).GetComponent<Text>().text = "N|A -- Continue";
        }
        else
        {
            menuRecompensa.transform.GetChild(0).GetComponent<Text>().text = "¡Victoria!";

            menuRecompensa.transform.GetChild(4).GetChild(4).GetComponent<Text>().text = "Sube Nivel";
            menuRecompensa.transform.GetChild(5).GetChild(4).GetComponent<Text>().text = "Sube Nivel";
            menuRecompensa.transform.GetChild(6).GetChild(4).GetComponent<Text>().text = "Sube Nivel";

            menuRecompensa.transform.GetChild(7).GetChild(0).GetComponent<Text>().text = "N|A -- Continuar";
        }

        yield return new WaitForSeconds(0.4f);
        menuRecompensa.gameObject.SetActive(true);
        esperaFin = true;
    }



    IEnumerator EsperaFinCombate()
    {
        combateActivo = false;
        
        esperaFin = false;
        animacionEnCurso = true;
        AnimacionSalidaCombate.SalirCombate();
        
        //AnimacionCombate.IniciaCombate();

        yield return new WaitForSeconds(0.25f);

        MusicaManager cambio = GameObject.Find("Musica").GetComponent<MusicaManager>();
        cambio.VuelveMusica();

        recienSalido = true;

        for (int i = 0; i < listaObjetosRecompensa.Length; i++)
        {
            listaObjetosRecompensa[i].SetActive(false);
        }

        menuRecompensa.gameObject.SetActive(false);

        for(int i = 0; i < cajasRecompensaPersonaje.Length; i++)
        {
            cajasRecompensaPersonaje[i].gameObject.SetActive(false);
            nivelMas[i].gameObject.SetActive(false);
        }

        flechaEstadisticaActiva = false;
        aumenta = false;
        reduce = false;

        fondoCombate.gameObject.SetActive(false);
        menuEstadisticas.gameObject.SetActive(false);
        flechaSeleccionPersonaje.gameObject.SetActive(false);
        DesactivarMenuCombate();


        aliado1.SetActive(false);
        aliado2.SetActive(false);
        aliado3.SetActive(false);
        enemigo1.SetActive(false);
        enemigo2.SetActive(false);
        enemigo3.SetActive(false);
        enemigoGrande1.SetActive(false);
        enemigoGrande2.SetActive(false);
        enemigoGigante.SetActive(false);


        danioAliado1.gameObject.SetActive(false);
        danioAliado2.gameObject.SetActive(false);
        danioAliado3.gameObject.SetActive(false);
        danioEnemigoPeq1.gameObject.SetActive(false);
        danioEnemigoPeq2.gameObject.SetActive(false);
        danioEnemigoPeq3.gameObject.SetActive(false);
        danioEnemigoGran1.gameObject.SetActive(false);
        danioEnemigoGran2.gameObject.SetActive(false);
        danioEnemigoGig.gameObject.SetActive(false);


        for (int i = 0; i < flechaEstadisticasMejora.Length; i++)
        {
            flechaEstadisticasMejora[i].gameObject.transform.localScale = new Vector3(flechaEstadisticasMejora[i].gameObject.transform.localScale.x, 0, flechaEstadisticasMejora[i].gameObject.transform.localScale.y);
            flechaEstadisticasMejora[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < flechaEstadisticasEmpeoro.Length; i++)
        {
            flechaEstadisticasEmpeoro[i].gameObject.transform.localScale = new Vector3(flechaEstadisticasEmpeoro[i].gameObject.transform.localScale.x, 0, flechaEstadisticasEmpeoro[i].gameObject.transform.localScale.y);
            flechaEstadisticasEmpeoro[i].gameObject.SetActive(false);
        }


        menuEstadisticas.gameObject.SetActive(false);

        if(movimientoAliados > 0)
        {
            datosDelPersonaje[0].aumentoAtaqueFisico = 0;
            datosDelPersonaje[0].aumentoAtaqueMagico = 0;
            datosDelPersonaje[0].aumentoDefensaFisica = 0;
            datosDelPersonaje[0].aumentoDefensaMagica = 0;
            datosDelPersonaje[0].aumentoEvasion = 0;
            datosDelPersonaje[0].aumentoVelocidad = 0;
        }


        if (movimientoAliados > 1)
        {
            datosDelPersonaje[1].aumentoAtaqueFisico = 0;
            datosDelPersonaje[1].aumentoAtaqueMagico = 0;
            datosDelPersonaje[1].aumentoDefensaFisica = 0;
            datosDelPersonaje[1].aumentoDefensaMagica = 0;
            datosDelPersonaje[1].aumentoEvasion = 0;
            datosDelPersonaje[1].aumentoVelocidad = 0;
        }

        if (movimientoAliados > 2)
        {
            datosDelPersonaje[2].aumentoAtaqueFisico = 0;
            datosDelPersonaje[2].aumentoAtaqueMagico = 0;
            datosDelPersonaje[2].aumentoDefensaFisica = 0;
            datosDelPersonaje[2].aumentoDefensaMagica = 0;
            datosDelPersonaje[2].aumentoEvasion = 0;
            datosDelPersonaje[2].aumentoVelocidad = 0;
        }

        camara.combate = false;

        yield return new WaitForSeconds(0.7f);

        controlJugador.setCombateActivo(false);
        animacionEnCurso = false;

        Vector3 nuevaPos = new Vector3(enemigo1.transform.position.x, enemigo1.transform.position.y, enemigo1.transform.position.z - 6);
        enemigo1.transform.position = nuevaPos;

        if(movimientoEnemigo > 1)
        {
            nuevaPos = new Vector3(enemigo2.transform.position.x, enemigo2.transform.position.y, enemigo2.transform.position.z + 6);
            enemigo2.transform.position = nuevaPos;
        }
        
        if(movimientoEnemigo == 3)
        {
            nuevaPos = new Vector3(enemigo3.transform.position.x, enemigo3.transform.position.y, enemigo3.transform.position.z + 6);
            enemigo3.transform.position = nuevaPos;
        }

        finJuego.espera = false;
    }



    IEnumerator EsperaGameOver()
    {
        combateActivo = false;
        esperaFin = false;

        yield return new WaitForSeconds(0.5f);

        finJuego.ActivaMenu();
    }



    void CalculaEstadisticas(int indice)
    {
        int nivelLogrado;

        switch (dificultad)
        {
            case 0:
                nivelLogrado = Random.Range(nivelMin, (nivelMin + 1));
                datosDelPersonaje[indice].nivel = nivelLogrado;

                nivelLogrado--;

                datosDelPersonaje[indice].ataqueFisico = datosDelPersonaje[indice].ataqueFisico + nivelLogrado;
                datosDelPersonaje[indice].ataqueMagico = datosDelPersonaje[indice].ataqueMagico + nivelLogrado;

                datosDelPersonaje[indice].defensaFisica = datosDelPersonaje[indice].defensaFisica + nivelLogrado;
                datosDelPersonaje[indice].defensaMagica = datosDelPersonaje[indice].defensaMagica + nivelLogrado;

                datosDelPersonaje[indice].velocidad = datosDelPersonaje[indice].velocidad + nivelLogrado;
                datosDelPersonaje[indice].vida = datosDelPersonaje[indice].vida + nivelLogrado;

                break;
            case 1:
                nivelLogrado = Random.Range(nivelMin, (nivelMin + 2));
                datosDelPersonaje[indice].nivel = nivelLogrado;

                nivelLogrado--;

                datosDelPersonaje[indice].ataqueFisico = datosDelPersonaje[indice].ataqueFisico + nivelLogrado * 2;
                datosDelPersonaje[indice].ataqueMagico = datosDelPersonaje[indice].ataqueMagico + nivelLogrado * 2;

                datosDelPersonaje[indice].defensaFisica = datosDelPersonaje[indice].defensaFisica + nivelLogrado * 2;
                datosDelPersonaje[indice].defensaMagica = datosDelPersonaje[indice].defensaMagica + nivelLogrado * 2;

                datosDelPersonaje[indice].velocidad = datosDelPersonaje[indice].velocidad + nivelLogrado * 2;
                datosDelPersonaje[indice].vida = datosDelPersonaje[indice].vida + nivelLogrado * 2;

                break;
            case 2:
                nivelLogrado = Random.Range(nivelMin, (nivelMin + 3));
                datosDelPersonaje[indice].nivel = nivelLogrado;

                nivelLogrado--;
                datosDelPersonaje[indice].ataqueFisico = datosDelPersonaje[indice].ataqueFisico + nivelLogrado * 3;
                datosDelPersonaje[indice].ataqueMagico = datosDelPersonaje[indice].ataqueMagico + nivelLogrado * 3;
                
                datosDelPersonaje[indice].defensaFisica = datosDelPersonaje[indice].defensaFisica + nivelLogrado * 3;
                datosDelPersonaje[indice].defensaMagica = datosDelPersonaje[indice].defensaMagica + nivelLogrado * 3;

                datosDelPersonaje[indice].velocidad = datosDelPersonaje[indice].velocidad + nivelLogrado * 3;
                datosDelPersonaje[indice].vida = datosDelPersonaje[indice].vida + nivelLogrado * 3;

                break;
            case 3:
                nivelLogrado = Random.Range(nivelMin, (nivelMin + 4));
                datosDelPersonaje[indice].nivel = nivelLogrado;

                nivelLogrado--;
                datosDelPersonaje[indice].ataqueFisico = datosDelPersonaje[indice].ataqueFisico + nivelLogrado * 4;
                datosDelPersonaje[indice].ataqueMagico = datosDelPersonaje[indice].ataqueMagico + nivelLogrado * 4;

                datosDelPersonaje[indice].defensaFisica = datosDelPersonaje[indice].defensaFisica + nivelLogrado * 4;
                datosDelPersonaje[indice].defensaMagica = datosDelPersonaje[indice].defensaMagica + nivelLogrado * 4;

                datosDelPersonaje[indice].velocidad = datosDelPersonaje[indice].velocidad + nivelLogrado * 4;
                datosDelPersonaje[indice].vida = datosDelPersonaje[indice].vida + nivelLogrado * 4;

                break;
        }

        datosDelPersonaje[indice].ataqueFisicoActual = datosDelPersonaje[indice].ataqueFisico;
        datosDelPersonaje[indice].ataqueFisicoModificado = datosDelPersonaje[indice].ataqueFisico;

        datosDelPersonaje[indice].ataqueMagicoActual = datosDelPersonaje[indice].ataqueMagico;
        datosDelPersonaje[indice].ataqueMagicoModificado = datosDelPersonaje[indice].ataqueMagico;

        datosDelPersonaje[indice].defensaFisicaActual = datosDelPersonaje[indice].defensaFisica;
        datosDelPersonaje[indice].defensaFisicaModificada = datosDelPersonaje[indice].defensaFisica;

        datosDelPersonaje[indice].defensaMagicaActual = datosDelPersonaje[indice].defensaMagica;
        datosDelPersonaje[indice].defensaMagicaModificada = datosDelPersonaje[indice].defensaMagica;

        datosDelPersonaje[indice].velocidadActual = datosDelPersonaje[indice].velocidad;
        datosDelPersonaje[indice].velocidadModificada = datosDelPersonaje[indice].velocidad;

        datosDelPersonaje[indice].vidaActual = datosDelPersonaje[indice].vida;
        datosDelPersonaje[indice].vidaModificada = datosDelPersonaje[indice].vida;
    }



    void Fase1()
    {
        if (movimientoAliadoGastado < movimientoAliados)
        {
            if (!posicionado)
            {
                fase1Activa = true;

                if (!retrocede)
                {
                    SeleccionarAliadoAtacante();
                    ActivarMenuCombate();
                    DesactivarMenuHabilidades();
                }
                else
                {
                    retrocede = false;
                }
            }
        }
        else
        {
            fase1Activa = false;
        }

        if (fase1Activa)
        {
            if (menuCombateActivo)
            {
                digitalX = Input.GetAxis("D-Horizontal");
                digitalY = Input.GetAxis("D-Vertical");

                if (pulsado)
                {
                    if (digitalY == 0 && digitalX == 0)
                    {
                        pulsado = false;
                    }
                }

                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || (!pulsado && digitalY < 0))
                {
                    pulsado = true;
                    musica.ProduceEfecto(11);

                    menuDeCombate.transform.GetChild(indiceCombate).GetComponent<Image>().sprite = elementosInterfaz[4];

                    indiceCombate++;

                    if (indiceCombate == 4)
                    {
                        if (invocacionCargada)
                        {
                            indiceCombate = 0;
                        }
                        else
                        {
                            indiceCombate = 1;
                        }
                    }

                    menuDeCombate.transform.GetChild(indiceCombate).GetComponent<Image>().sprite = elementosInterfaz[8];
                }
                else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || (!pulsado && digitalY > 0))
                {
                    pulsado = true;
                    musica.ProduceEfecto(11);

                    menuDeCombate.transform.GetChild(indiceCombate).GetComponent<Image>().sprite = elementosInterfaz[4];

                    indiceCombate--;

                    if (indiceCombate == 0)
                    {
                        if (invocacionCargada)
                        {
                            indiceCombate = 0;
                        }
                        else
                        {
                            indiceCombate = 3;
                        }
                    }
                    else if (indiceCombate == -1)
                    {
                        indiceCombate = 3;
                    }

                    menuDeCombate.transform.GetChild(indiceCombate).GetComponent<Image>().sprite = elementosInterfaz[8];
                }
                else if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                {
                    musica.ProduceEfecto(10);
                    menuDeCombate.transform.GetChild(indiceCombate).GetComponent<Image>().sprite = elementosInterfaz[4];

                    if (indiceCombate == 0)
                    {
                        ActivarInvocacion();
                    }
                    else if (indiceCombate == 1)
                    {
                        ActivarMenuHabilidades();
                    }
                    else if (indiceCombate == 2)
                    {
                        ActivarInventario();
                    }
                    else if (indiceCombate == 3)
                    {
                        HuirCombate();
                    }

                    DesactivarMenuCombate();
                }
            }
            else if (menuHabilidadesActivo)
            {
                digitalX = Input.GetAxis("D-Horizontal");
                digitalY = Input.GetAxis("D-Vertical");

                if (pulsado)
                {
                    if (digitalY == 0 && digitalX == 0)
                    {
                        pulsado = false;
                    }
                }

                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || (!pulsado && digitalY < 0))
                {
                    pulsado = true;
                    musica.ProduceEfecto(11);

                    if (columnaHabilidad == 1)
                    {
                        if (datosDelPersonaje[movimientoAliadoGastado].numeroAtaques > 2)
                        {
                            filaHabilidad++;

                            if (filaHabilidad == 3)
                            {
                                filaHabilidad = 1;
                                menuDeHabilidades.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = elementosInterfaz[8];
                                menuDeHabilidades.transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = elementosInterfaz[4];

                                menuDeHabilidades.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "ER " + datosDelPersonaje[movimientoAliadoGastado].habilidades[0].energiaActual + " / " + datosDelPersonaje[movimientoAliadoGastado].habilidades[0].energia;
                               
                                if(baseDeDatos.idioma == 1)
                                {
                                    menuDeHabilidades.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Type: " + datosDelPersonaje[movimientoAliadoGastado].habilidades[0].elementoIngles;
                                }
                                else
                                {
                                    menuDeHabilidades.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Tipo: " + datosDelPersonaje[movimientoAliadoGastado].habilidades[0].elemento;
                                }
                            }
                            else
                            {
                                menuDeHabilidades.transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = elementosInterfaz[8];
                                menuDeHabilidades.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = elementosInterfaz[4];

                                menuDeHabilidades.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "ER " + datosDelPersonaje[movimientoAliadoGastado].habilidades[2].energiaActual + " / " + datosDelPersonaje[movimientoAliadoGastado].habilidades[2].energia;
                                
                                if(baseDeDatos.idioma == 1)
                                {
                                    menuDeHabilidades.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Type: " + datosDelPersonaje[movimientoAliadoGastado].habilidades[2].elementoIngles;
                                }
                                else
                                {
                                    menuDeHabilidades.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Tipo: " + datosDelPersonaje[movimientoAliadoGastado].habilidades[2].elemento;
                                }
                            }
                        }
                    }
                    else if (columnaHabilidad == 2)
                    {
                        if (datosDelPersonaje[movimientoAliadoGastado].numeroAtaques == 4)
                        {
                            filaHabilidad++;

                            if (filaHabilidad == 3)
                            {
                                filaHabilidad = 1;
                                menuDeHabilidades.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = elementosInterfaz[8];
                                menuDeHabilidades.transform.GetChild(0).GetChild(3).GetComponent<Image>().sprite = elementosInterfaz[4];

                                menuDeHabilidades.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "ER " + datosDelPersonaje[movimientoAliadoGastado].habilidades[1].energiaActual + " / " + datosDelPersonaje[movimientoAliadoGastado].habilidades[1].energia;

                                if (baseDeDatos.idioma == 1)
                                {
                                    menuDeHabilidades.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Type: " + datosDelPersonaje[movimientoAliadoGastado].habilidades[1].elementoIngles;
                                }
                                else
                                {
                                    menuDeHabilidades.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Tipo: " + datosDelPersonaje[movimientoAliadoGastado].habilidades[1].elemento;
                                }
                            }
                            else
                            {
                                menuDeHabilidades.transform.GetChild(0).GetChild(3).GetComponent<Image>().sprite = elementosInterfaz[8];
                                menuDeHabilidades.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = elementosInterfaz[4];

                                menuDeHabilidades.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "ER " + datosDelPersonaje[movimientoAliadoGastado].habilidades[3].energiaActual + " / " + datosDelPersonaje[movimientoAliadoGastado].habilidades[3].energia;

                                if (baseDeDatos.idioma == 1)
                                {
                                    menuDeHabilidades.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Type: " + datosDelPersonaje[movimientoAliadoGastado].habilidades[3].elementoIngles;
                                }
                                else
                                {
                                    menuDeHabilidades.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Tipo: " + datosDelPersonaje[movimientoAliadoGastado].habilidades[3].elemento;
                                }
                            }
                        }
                    }
                }
                else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || (!pulsado && digitalY > 0))
                {
                    pulsado = true;
                    musica.ProduceEfecto(11);

                    if (columnaHabilidad == 1)
                    {
                        if (datosDelPersonaje[movimientoAliadoGastado].numeroAtaques > 2)
                        {
                            filaHabilidad--;

                            if (filaHabilidad == 0)
                            {
                                filaHabilidad = 2;
                                menuDeHabilidades.transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = elementosInterfaz[8];
                                menuDeHabilidades.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = elementosInterfaz[4];

                                menuDeHabilidades.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "ER " + datosDelPersonaje[movimientoAliadoGastado].habilidades[2].energiaActual + " / " + datosDelPersonaje[movimientoAliadoGastado].habilidades[2].energia;

                                if (baseDeDatos.idioma == 1)
                                {
                                    menuDeHabilidades.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Type: " + datosDelPersonaje[movimientoAliadoGastado].habilidades[2].elementoIngles;
                                }
                                else
                                {
                                    menuDeHabilidades.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Tipo: " + datosDelPersonaje[movimientoAliadoGastado].habilidades[2].elemento;
                                }
                            }
                            else
                            {
                                menuDeHabilidades.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = elementosInterfaz[8];
                                menuDeHabilidades.transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = elementosInterfaz[4];

                                menuDeHabilidades.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "ER " + datosDelPersonaje[movimientoAliadoGastado].habilidades[0].energiaActual + " / " + datosDelPersonaje[movimientoAliadoGastado].habilidades[0].energia;

                                if (baseDeDatos.idioma == 1)
                                {
                                    menuDeHabilidades.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Type: " + datosDelPersonaje[movimientoAliadoGastado].habilidades[0].elementoIngles;
                                }
                                else
                                {
                                    menuDeHabilidades.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Tipo: " + datosDelPersonaje[movimientoAliadoGastado].habilidades[0].elemento;
                                }
                            }
                        }
                    }
                    else if (columnaHabilidad == 2)
                    {
                        if (datosDelPersonaje[movimientoAliadoGastado].numeroAtaques == 4)
                        {
                            filaHabilidad--;

                            if (filaHabilidad == 0)
                            {
                                filaHabilidad = 2;
                                menuDeHabilidades.transform.GetChild(0).GetChild(3).GetComponent<Image>().sprite = elementosInterfaz[8];
                                menuDeHabilidades.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = elementosInterfaz[4];

                                menuDeHabilidades.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "ER " + datosDelPersonaje[movimientoAliadoGastado].habilidades[3].energiaActual + " / " + datosDelPersonaje[movimientoAliadoGastado].habilidades[3].energia;

                                if (baseDeDatos.idioma == 1)
                                {
                                    menuDeHabilidades.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Type: " + datosDelPersonaje[movimientoAliadoGastado].habilidades[3].elementoIngles;
                                }
                                else
                                {
                                    menuDeHabilidades.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Tipo: " + datosDelPersonaje[movimientoAliadoGastado].habilidades[3].elemento;
                                }
                            }
                            else
                            {
                                menuDeHabilidades.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = elementosInterfaz[8];
                                menuDeHabilidades.transform.GetChild(0).GetChild(3).GetComponent<Image>().sprite = elementosInterfaz[4];

                                menuDeHabilidades.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "ER " + datosDelPersonaje[movimientoAliadoGastado].habilidades[1].energiaActual + " / " + datosDelPersonaje[movimientoAliadoGastado].habilidades[1].energia;

                                if (baseDeDatos.idioma == 1)
                                {
                                    menuDeHabilidades.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Type: " + datosDelPersonaje[movimientoAliadoGastado].habilidades[1].elementoIngles;
                                }
                                else
                                {
                                    menuDeHabilidades.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Tipo: " + datosDelPersonaje[movimientoAliadoGastado].habilidades[1].elemento;
                                }
                            }
                        }
                    }
                }
                else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || (digitalX > 0 && !pulsado))
                {
                    pulsado = true;
                    musica.ProduceEfecto(11);

                    if (filaHabilidad == 1)
                    {
                        if (datosDelPersonaje[movimientoAliadoGastado].numeroAtaques > 1)
                        {
                            columnaHabilidad++;

                            if (columnaHabilidad == 3)
                            {
                                columnaHabilidad = 1;
                                menuDeHabilidades.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = elementosInterfaz[8];
                                menuDeHabilidades.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = elementosInterfaz[4];

                                menuDeHabilidades.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "ER " + datosDelPersonaje[movimientoAliadoGastado].habilidades[0].energiaActual + " / " + datosDelPersonaje[movimientoAliadoGastado].habilidades[0].energia;
                            
                                if(baseDeDatos.idioma == 1)
                                {
                                    menuDeHabilidades.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Type: " + datosDelPersonaje[movimientoAliadoGastado].habilidades[0].elementoIngles;
                                }
                                else
                                {
                                    menuDeHabilidades.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Tipo: " + datosDelPersonaje[movimientoAliadoGastado].habilidades[0].elemento;
                                }
                            }
                            else
                            {
                                menuDeHabilidades.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = elementosInterfaz[8];
                                menuDeHabilidades.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = elementosInterfaz[4];

                                menuDeHabilidades.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "ER " + datosDelPersonaje[movimientoAliadoGastado].habilidades[1].energiaActual + " / " + datosDelPersonaje[movimientoAliadoGastado].habilidades[1].energia;
                                

                                if (baseDeDatos.idioma == 1)
                                {
                                    menuDeHabilidades.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Type: " + datosDelPersonaje[movimientoAliadoGastado].habilidades[1].elementoIngles;
                                }
                                else
                                {
                                    menuDeHabilidades.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Tipo: " + datosDelPersonaje[movimientoAliadoGastado].habilidades[1].elemento;
                                }
                            }
                        }
                    }
                    else if (filaHabilidad == 2)
                    {
                        if (datosDelPersonaje[movimientoAliadoGastado].numeroAtaques == 4)
                        {
                            columnaHabilidad++;

                            if (columnaHabilidad == 3)
                            {
                                columnaHabilidad = 1;
                                menuDeHabilidades.transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = elementosInterfaz[8];
                                menuDeHabilidades.transform.GetChild(0).GetChild(3).GetComponent<Image>().sprite = elementosInterfaz[4];

                                menuDeHabilidades.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "ER " + datosDelPersonaje[movimientoAliadoGastado].habilidades[2].energiaActual + " / " + datosDelPersonaje[movimientoAliadoGastado].habilidades[2].energia;

                                if (baseDeDatos.idioma == 1)
                                {
                                    menuDeHabilidades.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Type: " + datosDelPersonaje[movimientoAliadoGastado].habilidades[2].elementoIngles;
                                }
                                else
                                {
                                    menuDeHabilidades.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Tipo: " + datosDelPersonaje[movimientoAliadoGastado].habilidades[2].elemento;
                                }
                            }
                            else
                            {
                                menuDeHabilidades.transform.GetChild(0).GetChild(3).GetComponent<Image>().sprite = elementosInterfaz[8];
                                menuDeHabilidades.transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = elementosInterfaz[4];

                                menuDeHabilidades.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "ER " + datosDelPersonaje[movimientoAliadoGastado].habilidades[3].energiaActual + " / " + datosDelPersonaje[movimientoAliadoGastado].habilidades[3].energia;

                                if (baseDeDatos.idioma == 1)
                                {
                                    menuDeHabilidades.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Type: " + datosDelPersonaje[movimientoAliadoGastado].habilidades[3].elementoIngles;
                                }
                                else
                                {
                                    menuDeHabilidades.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Tipo: " + datosDelPersonaje[movimientoAliadoGastado].habilidades[3].elemento;
                                }
                            }
                        }
                    }
                }
                else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || (digitalX < 0 && !pulsado))
                {
                    pulsado = true;
                    musica.ProduceEfecto(11);

                    if (filaHabilidad == 1)
                    {
                        if (datosDelPersonaje[movimientoAliadoGastado].numeroAtaques > 1)
                        {
                            columnaHabilidad--;

                            if (columnaHabilidad == 0)
                            {
                                columnaHabilidad = 2;
                                menuDeHabilidades.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = elementosInterfaz[8];
                                menuDeHabilidades.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = elementosInterfaz[4];

                                menuDeHabilidades.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "ER " + datosDelPersonaje[movimientoAliadoGastado].habilidades[1].energiaActual + " / " + datosDelPersonaje[movimientoAliadoGastado].habilidades[1].energia;

                                if (baseDeDatos.idioma == 1)
                                {
                                    menuDeHabilidades.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Type: " + datosDelPersonaje[movimientoAliadoGastado].habilidades[1].elementoIngles;
                                }
                                else
                                {
                                    menuDeHabilidades.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Tipo: " + datosDelPersonaje[movimientoAliadoGastado].habilidades[1].elemento;
                                }
                            }
                            else
                            {
                                menuDeHabilidades.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = elementosInterfaz[8];
                                menuDeHabilidades.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = elementosInterfaz[4];

                                menuDeHabilidades.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "ER " + datosDelPersonaje[movimientoAliadoGastado].habilidades[0].energiaActual + " / " + datosDelPersonaje[movimientoAliadoGastado].habilidades[0].energia;

                                if (baseDeDatos.idioma == 1)
                                {
                                    menuDeHabilidades.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Type: " + datosDelPersonaje[movimientoAliadoGastado].habilidades[0].elementoIngles;
                                }
                                else
                                {
                                    menuDeHabilidades.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Tipo: " + datosDelPersonaje[movimientoAliadoGastado].habilidades[0].elemento;
                                }
                            }
                        }
                    }
                    else if (filaHabilidad == 2)
                    {
                        if (datosDelPersonaje[movimientoAliadoGastado].numeroAtaques == 4)
                        {
                            columnaHabilidad--;

                            if (columnaHabilidad == 0)
                            {
                                columnaHabilidad = 2;
                                menuDeHabilidades.transform.GetChild(0).GetChild(3).GetComponent<Image>().sprite = elementosInterfaz[8];
                                menuDeHabilidades.transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = elementosInterfaz[4];

                                menuDeHabilidades.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "ER " + datosDelPersonaje[movimientoAliadoGastado].habilidades[3].energiaActual + " / " + datosDelPersonaje[movimientoAliadoGastado].habilidades[3].energia;

                                if (baseDeDatos.idioma == 1)
                                {
                                    menuDeHabilidades.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Type: " + datosDelPersonaje[movimientoAliadoGastado].habilidades[3].elementoIngles;
                                }
                                else
                                {
                                    menuDeHabilidades.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Tipo: " + datosDelPersonaje[movimientoAliadoGastado].habilidades[3].elemento;
                                }
                            }
                            else
                            {
                                menuDeHabilidades.transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = elementosInterfaz[8];
                                menuDeHabilidades.transform.GetChild(0).GetChild(3).GetComponent<Image>().sprite = elementosInterfaz[4];

                                menuDeHabilidades.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "ER " + datosDelPersonaje[movimientoAliadoGastado].habilidades[2].energiaActual + " / " + datosDelPersonaje[movimientoAliadoGastado].habilidades[2].energia;

                                if (baseDeDatos.idioma == 1)
                                {
                                    menuDeHabilidades.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Type: " + datosDelPersonaje[movimientoAliadoGastado].habilidades[2].elementoIngles;
                                }
                                else
                                {
                                    menuDeHabilidades.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Tipo: " + datosDelPersonaje[movimientoAliadoGastado].habilidades[2].elemento;
                                }
                            }
                        }
                    }
                }
                else if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                {
                    if (filaHabilidad == 1 && columnaHabilidad == 1)
                    {
                        if (datosDelPersonaje[movimientoAliadoGastado].habilidades[0].energiaActual != 0)
                        {
                            menuDeHabilidades.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = elementosInterfaz[4];
                            usarAtaque = true;
                            musica.ProduceEfecto(10);
                        }
                        else
                        {
                            musica.ProduceEfecto(16);
                        }
                    }
                    else if (filaHabilidad == 1 && columnaHabilidad == 2)
                    {
                        if (datosDelPersonaje[movimientoAliadoGastado].habilidades[1].energiaActual != 0)
                        {
                            menuDeHabilidades.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = elementosInterfaz[4];
                            usarAtaque = true;
                            musica.ProduceEfecto(10);
                        }
                        else
                        {
                            musica.ProduceEfecto(16);
                        }
                    }
                    else if (filaHabilidad == 2 && columnaHabilidad == 1)
                    {
                        if (datosDelPersonaje[movimientoAliadoGastado].habilidades[2].energiaActual != 0)
                        {
                            menuDeHabilidades.transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = elementosInterfaz[4];
                            usarAtaque = true;
                            musica.ProduceEfecto(10);
                        }
                        else
                        {
                            musica.ProduceEfecto(16);
                        }
                    }
                    else if (filaHabilidad == 2 && columnaHabilidad == 2)
                    {
                        if (datosDelPersonaje[movimientoAliadoGastado].habilidades[3].energiaActual != 0)
                        {
                            menuDeHabilidades.transform.GetChild(0).GetChild(3).GetComponent<Image>().sprite = elementosInterfaz[4];    
                            usarAtaque = true;
                            musica.ProduceEfecto(10);
                        }
                        else
                        {
                            musica.ProduceEfecto(16);
                        }
                    }

                    if (usarAtaque)
                    {
                        menuDeCombate.transform.GetChild(indiceCombate).GetComponent<Image>().sprite = elementosInterfaz[4];

                        if(filaHabilidad == 1)
                        {
                            if (columnaHabilidad == 1)
                            {
                                menuDeHabilidades.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = elementosInterfaz[4];
                            }
                            else
                            {
                                menuDeHabilidades.transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = elementosInterfaz[4];
                            }
                        }
                        else
                        {
                            if (columnaHabilidad == 1)
                            {
                                menuDeHabilidades.transform.GetChild(0).GetChild(3).GetComponent<Image>().sprite = elementosInterfaz[4];
                            }
                            else
                            {
                                menuDeHabilidades.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = elementosInterfaz[4];
                            }
                        }

                        DesactivarMenuHabilidades();
                    }
                }
                else if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("B"))
                {
                    usarAtaque = false;
                    musica.ProduceEfecto(12);
                    DesactivarMenuHabilidades();
                    ActivarMenuCombate();
                }
            }

            if (!menuCombateActivo && !menuHabilidadesActivo)
            {
                if (usarAtaque || usarObjeto || huir)
                {
                    RealizaAccion();
                }
            }
        }
    }



    void Fase2()
    {
        int prioridad, objetivo;

        objetivo = DeterminarObjetivo(movimientoEnemigoGastado + 3);
        prioridad = DeterminaPrioridadAtaqueAEnemigo(movimientoEnemigoGastado, objetivo);

        listaDeObjetivos[3 + movimientoEnemigoGastado].lanzador = 3 + movimientoEnemigoGastado;
        listaDeObjetivos[3 + movimientoEnemigoGastado].receptor = objetivo;
        listaDeObjetivos[3 + movimientoEnemigoGastado].indiceAtaque = prioridad;
        listaDeObjetivos[3 + movimientoEnemigoGastado].ataqueAUsar = datosDelPersonaje[3 + movimientoEnemigoGastado].habilidades[prioridad];

        if (movimientoEnemigoGastado == movimientoEnemigo - 1)
        {
            turnoEnemigo = false;
        }
        else
        {
            movimientoEnemigoGastado++;
        }
    }



    void Fase3()
    {
        if (!aplicaApoyo)
        {
            int proximoAtaque = ataqueLanzado;
            //int proximoAtaque = vectorOrdenado[ataqueLanzado];
            bool puedeAtacar = PuedeAtacar(proximoAtaque);

            if (puedeAtacar)
            {
                RealizaAtaque(proximoAtaque);
            }
            else
            {
                if (ataqueLanzado == (2 + movimientoEnemigo))
                {
                    TerminaTurnoEnemigo();
                }
                else
                {
                    ataqueLanzado++;
                }
            }
        }
    }



    void ActivarMenuHabilidades()
    {
        menuCombateActivo = false;
        menuHabilidadesActivo = true;

        DesactivarMenuCombate();
        menuDeHabilidades.SetActive(true);

        menuDeHabilidades.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = elementosInterfaz[8];
        menuDeHabilidades.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "ER " + datosDelPersonaje[movimientoAliadoGastado].habilidades[0].energiaActual + " / " + datosDelPersonaje[movimientoAliadoGastado].habilidades[0].energia;

        if (baseDeDatos.idioma == 0)
        {
            if(baseDeDatos.idioma == 1)
            {
                menuDeHabilidades.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Type: " + datosDelPersonaje[movimientoAliadoGastado].habilidades[0].elementoIngles;
            }
            else
            {
                menuDeHabilidades.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Tipo: " + datosDelPersonaje[movimientoAliadoGastado].habilidades[0].elemento;
            }
            
            menuDeHabilidades.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = datosDelPersonaje[movimientoAliadoGastado].habilidades[0].nombre;


            if (datosDelPersonaje[movimientoAliadoGastado].numeroAtaques > 1)
            {
                menuDeHabilidades.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = datosDelPersonaje[movimientoAliadoGastado].habilidades[1].nombre;
            }

            if (datosDelPersonaje[movimientoAliadoGastado].numeroAtaques > 2)
            {
                menuDeHabilidades.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = datosDelPersonaje[movimientoAliadoGastado].habilidades[2].nombre;

            }

            if (datosDelPersonaje[movimientoAliadoGastado].numeroAtaques == 4)
            {
                menuDeHabilidades.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>().text = datosDelPersonaje[movimientoAliadoGastado].habilidades[3].nombre;
            }
        }
        else if (baseDeDatos.idioma == 1)
        {
            menuDeHabilidades.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Type: " + datosDelPersonaje[movimientoAliadoGastado].habilidades[0].elementoIngles;
            menuDeHabilidades.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = datosDelPersonaje[movimientoAliadoGastado].habilidades[0].nombreIngles;

            if (datosDelPersonaje[movimientoAliadoGastado].numeroAtaques > 1)
            {
                menuDeHabilidades.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = datosDelPersonaje[movimientoAliadoGastado].habilidades[1].nombreIngles;
            }

            if (datosDelPersonaje[movimientoAliadoGastado].numeroAtaques > 2)
            {
                menuDeHabilidades.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = datosDelPersonaje[movimientoAliadoGastado].habilidades[2].nombreIngles;

            }

            if (datosDelPersonaje[movimientoAliadoGastado].numeroAtaques == 4)
            {
                menuDeHabilidades.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>().text = datosDelPersonaje[movimientoAliadoGastado].habilidades[3].nombreIngles;
            }
        }

        if (datosDelPersonaje[movimientoAliadoGastado].numeroAtaques == 1)
        {
            menuDeHabilidades.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            menuDeHabilidades.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
            menuDeHabilidades.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
        }

        if (datosDelPersonaje[movimientoAliadoGastado].numeroAtaques >= 2)
        {
            if (datosDelPersonaje[movimientoAliadoGastado].numeroAtaques == 2)
            {
                menuDeHabilidades.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                menuDeHabilidades.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
                menuDeHabilidades.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
            }
        }

        if (datosDelPersonaje[movimientoAliadoGastado].numeroAtaques >= 3)
        {
            if (datosDelPersonaje[movimientoAliadoGastado].numeroAtaques == 3)
            {
                menuDeHabilidades.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                menuDeHabilidades.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
                menuDeHabilidades.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
            }
        }

        if (datosDelPersonaje[movimientoAliadoGastado].numeroAtaques == 4)
        {
            menuDeHabilidades.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            menuDeHabilidades.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
            menuDeHabilidades.transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
        }

        filaHabilidad = 1;
        columnaHabilidad = 1;
    }



    void ActivarMenuCombate()
    {
        if(movimientoAliadoGastado < movimientoAliados)
        {
            menuCombateActivo = true;
            IniciarMenuCombate();
            IniciarMenuEstadisticas();
        }
    }



    void DesactivarMenuHabilidades()
    {
        menuHabilidadesActivo = false;
        menuDeHabilidades.SetActive(false);
    }



    void DesactivarMenuCombate()
    {
        menuCombateActivo = false;
        menuDeCombate.SetActive(false);
        menuEstadisticas.SetActive(false);
    }



    void IniciarMenuEstadisticas()
    {
        menuEstadisticas.gameObject.SetActive(true);
        menuEstadisticas.transform.GetChild(0).GetChild(5).GetComponent<Image>().sprite = datosDelPersonaje[0].imagenElemento;

        if(baseDeDatos.idioma == 1)
        {
            menuEstadisticas.transform.GetChild(0).GetChild(6).GetComponent<Text>().text = datosDelPersonaje[0].elementoIngles + "";
        }
        else
        {
            menuEstadisticas.transform.GetChild(0).GetChild(6).GetComponent<Text>().text = datosDelPersonaje[0].elemento + "";
        }

        if (movimientoAliados > 1)
        {
            menuEstadisticas.transform.GetChild(1).gameObject.SetActive(true);
            menuEstadisticas.transform.GetChild(1).GetChild(5).GetComponent<Image>().sprite = datosDelPersonaje[1].imagenElemento;

            if (baseDeDatos.idioma == 1)
            {
                menuEstadisticas.transform.GetChild(1).GetChild(6).GetComponent<Text>().text = datosDelPersonaje[1].elementoIngles + "";
            }
            else
            {
                menuEstadisticas.transform.GetChild(1).GetChild(6).GetComponent<Text>().text = datosDelPersonaje[1].elemento + "";
            }
        }
        else
        {
            menuEstadisticas.transform.GetChild(1).gameObject.SetActive(false);
            menuEstadisticas.transform.GetChild(2).gameObject.SetActive(false);
        }

        if (movimientoAliados > 2)
        {
            menuEstadisticas.transform.GetChild(2).gameObject.SetActive(true);
            menuEstadisticas.transform.GetChild(2).GetChild(5).GetComponent<Image>().sprite = datosDelPersonaje[2].imagenElemento;

            if (baseDeDatos.idioma == 1)
            {
                menuEstadisticas.transform.GetChild(2).GetChild(6).GetComponent<Text>().text = datosDelPersonaje[2].elementoIngles + "";
            }
            else
            {
                menuEstadisticas.transform.GetChild(2).GetChild(6).GetComponent<Text>().text = datosDelPersonaje[2].elemento + "";
            }
        }
        else
        {
            menuEstadisticas.transform.GetChild(2).gameObject.SetActive(false);
        }


        for (int i = 0; i < movimientoAliados; i++)
        {
            menuEstadisticas.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = datosDelPersonaje[i].imagen;

            if(baseDeDatos.idioma == 1)
            {
                menuEstadisticas.transform.GetChild(i).GetChild(1).GetComponent<Text>().text = datosDelPersonaje[i].nombreIngles;
            }
            else
            {
                menuEstadisticas.transform.GetChild(i).GetChild(1).GetComponent<Text>().text = datosDelPersonaje[i].nombre;
            }
            
            menuEstadisticas.transform.GetChild(i).GetChild(2).GetComponent<Text>().text = "Lv: " + datosDelPersonaje[i].nivel;
            menuEstadisticas.transform.GetChild(i).GetChild(3).GetChild(0).GetComponent<Text>().text = datosDelPersonaje[i].vidaActual + "/" + datosDelPersonaje[i].vidaModificada;

            float porcentajeVida = (float)datosDelPersonaje[i].vidaActual / (float)datosDelPersonaje[i].vidaModificada;
            menuEstadisticas.transform.GetChild(i).GetChild(3).GetChild(2).transform.localScale = new Vector3(porcentajeVida, 1, 1);

            float porcentajeEnergia = (float)datosDelPersonaje[i].energiaInvocacionActual / (float)datosDelPersonaje[i].energiaInvocacion;
            menuEstadisticas.transform.GetChild(i).GetChild(4).GetChild(0).transform.localScale = new Vector3(porcentajeEnergia, 1, 1);

            if (datosDelPersonaje[i].energiaInvocacion == datosDelPersonaje[i].energiaInvocacionActual)
            {
                menuEstadisticas.transform.GetChild(i).GetChild(4).GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                menuEstadisticas.transform.GetChild(i).GetChild(4).GetChild(1).gameObject.SetActive(false);
            }


            if (datosDelPersonaje[i].elemento == Personajes.elementoPersonaje.DORMILON)
            {
                menuEstadisticas.transform.GetChild(i).GetChild(5).GetComponent<Image>().sprite = elementosInterfaz[12];
            }
            else if (datosDelPersonaje[i].elemento == Personajes.elementoPersonaje.FIESTERO)
            {
                menuEstadisticas.transform.GetChild(i).GetChild(5).GetComponent<Image>().sprite = elementosInterfaz[2];
            }
            else if (datosDelPersonaje[i].elemento == Personajes.elementoPersonaje.FRIKI)
            {
                menuEstadisticas.transform.GetChild(i).GetChild(5).GetComponent<Image>().sprite = elementosInterfaz[13];
            }
            else if (datosDelPersonaje[i].elemento == Personajes.elementoPersonaje.NEUTRO)
            {
                menuEstadisticas.transform.GetChild(i).GetChild(5).GetComponent<Image>().sprite = elementosInterfaz[3];
            }
            else if (datosDelPersonaje[i].elemento == Personajes.elementoPersonaje.RESPONSABLE)
            {
                menuEstadisticas.transform.GetChild(i).GetChild(5).GetComponent<Image>().sprite = elementosInterfaz[10];
            }
            else
            {
                menuEstadisticas.transform.GetChild(i).GetChild(5).GetComponent<Image>().sprite = elementosInterfaz[7];
            }
        }
    }



    void IniciarMenuCombate()
    {
        menuDeCombate.SetActive(true);
        menuDeHabilidades.SetActive(false);

        if (datosDelPersonaje[movimientoAliadoGastado].energiaInvocacion == datosDelPersonaje[movimientoAliadoGastado].energiaInvocacionActual)
        {
            menuDeCombate.transform.GetChild(0).gameObject.SetActive(true);
            indiceCombate = 0;
            invocacionCargada = true;
        }
        else
        {
            menuDeCombate.transform.GetChild(0).gameObject.SetActive(false);
            indiceCombate = 1;
            invocacionCargada = false;
        }

        if(movimientoAliadoGastado >= 0)
        {
            menuEstadisticas.transform.GetChild(movimientoAliadoGastado).GetComponent<Image>().sprite = elementosInterfaz[18];
        }

        for (int i = 0; i < 4; i++)
        {
            menuDeCombate.transform.GetChild(indiceCombate).GetComponent<Image>().sprite = elementosInterfaz[4];
        }

        menuDeCombate.transform.GetChild(indiceCombate).GetComponent<Image>().sprite = elementosInterfaz[8];
    }



    void HuirCombate()
    {
        huir = true;
    }



    void ActivarInventario()
    {
        usarObjeto = true;
        DesactivarMenuCombate();
        inventario.IniciarMenu(true);
    }



    void ActivarInvocacion()
    {

    }



    void ActivarHabilidad()
    {

    }



    void DesactivaInfoEnemigo()
    {
        infoEnemigos.SetActive(false);
    }



    void ActivaInforEnemigos(int objetivo)
    {
        infoEnemigos.SetActive(true);
        int aux = objetivo + 3;

        infoEnemigos.transform.GetChild(0).GetComponent<Image>().sprite = datosDelPersonaje[aux].imagen;

        infoEnemigos.transform.GetChild(2).GetComponent<Text>().text = "Lv: " + datosDelPersonaje[aux].nivel;
        infoEnemigos.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = datosDelPersonaje[aux].vidaActual + "/" + datosDelPersonaje[aux].vidaModificada;
        float porcentajeVida = (float)datosDelPersonaje[aux].vidaActual / (float)datosDelPersonaje[aux].vidaModificada;
        infoEnemigos.transform.GetChild(3).GetChild(2).transform.localScale = new Vector3(porcentajeVida, 1, 1);
        infoEnemigos.transform.GetChild(4).GetComponent<Image>().sprite = datosDelPersonaje[aux].imagenElemento;

        if (baseDeDatos.idioma == 1)
        {
            infoEnemigos.transform.GetChild(5).GetComponent<Text>().text = datosDelPersonaje[aux].elementoIngles + "";
            infoEnemigos.transform.GetChild(1).GetComponent<Text>().text = "" + datosDelPersonaje[aux].nombreIngles;
        }
        else
        {
            infoEnemigos.transform.GetChild(5).GetComponent<Text>().text = datosDelPersonaje[aux].elemento + "";
            infoEnemigos.transform.GetChild(1).GetComponent<Text>().text = "" + datosDelPersonaje[aux].nombre;
        }
    }
}
