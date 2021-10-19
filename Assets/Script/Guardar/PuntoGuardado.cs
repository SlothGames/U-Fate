using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PuntoGuardado : MonoBehaviour
{
    GameObject manager;
    ControlJugador controlJugador;
    ControlObjetos controlObjetos;
    BaseDatos baseDeDatos;
    MusicaManager musica;
    MusicaManager efecto;

    public GameObject menuGuardado;
    public GameObject confirmacion;
    public GameObject pantallaCarga;
    PantallaCarga controladorCarga;

    GameObject partidasGuardadas;
    GameObject fondoNegro;

    public Image flecha;

    public bool activo;
    bool funcional, confirmacionActiva;
    bool mueveFlecha;
    public bool cargado;
    bool guardar;
    bool pgActivo;
    bool borrar;
    bool gameOver;

    int pos, posConf, fichero;

    bool pulsado;

    float digitalX;
    float digitalY;



    void Awake()
    {
        guardar = false;
        cargado = false;
        borrar = false;
        confirmacionActiva = false;
        DesactivarConfirmacion();
        DesactivaTexto();
        DontDestroyOnLoad(gameObject);
    }



    void Start ()
    {
        manager = GameObject.Find("GameManager");
        controlJugador = GameObject.Find("Player").GetComponent<ControlJugador>();
        fondoNegro = GameObject.Find("MensajesEnPantalla").transform.GetChild(5).gameObject;
        musica = GameObject.Find("Musica").GetComponent<MusicaManager>();
        efecto = GameObject.Find("EfectosSonido").GetComponent<MusicaManager>();
        baseDeDatos = manager.GetComponent<BaseDatos>();
        controlObjetos = manager.GetComponent<ControlObjetos>();
        partidasGuardadas = menuGuardado.transform.GetChild(6).gameObject;
        controladorCarga = pantallaCarga.GetComponent<PantallaCarga>();

        pos = 0;
        fichero = 0;
        mueveFlecha = false;
        activo = false;
        gameOver = false;

        menuGuardado.gameObject.SetActive(activo);
    }
	


	void Update ()
    {
        if (activo)
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

            if (baseDeDatos.mandoActivo)
            {
                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.N) || Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.Escape) ||
                    Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    CambiaControl();
                }
            }
            else
            {
                if (Input.GetButtonUp("A") || Input.GetButtonUp("B") || Input.GetButtonUp("X") || Input.GetButtonUp("Y") || Input.GetButtonUp("Start") || Input.GetButtonUp("Select") || (digitalY != 0) || (digitalX != 0))
                {
                    CambiaControl();
                }
            }

            if (!gameOver)
            {
                if (!confirmacionActiva)
                {
                    if (mueveFlecha)
                    {
                        if (pgActivo)
                        {
                            if (fichero > 2)
                            {
                                fichero = 0;
                            }
                            else if (fichero < 0)
                            {
                                fichero = 2;
                            }

                            partidasGuardadas.transform.GetChild(fichero).GetComponent<Image>().sprite = baseDeDatos.interfazPG[1];
                        }
                        else
                        {
                            if (pos > 2)
                            {
                                pos = 0;
                            }
                            else if (pos < 0)
                            {
                                pos = 2;
                            }

                            if (pos == 0)
                            {
                                flecha.transform.position = menuGuardado.transform.GetChild(2).transform.GetChild(0).transform.position;
                            }
                            else if (pos == 1)
                            {
                                flecha.transform.position = menuGuardado.transform.GetChild(3).transform.GetChild(0).transform.position;
                            }
                            else
                            {
                                flecha.transform.position = menuGuardado.transform.GetChild(4).transform.GetChild(0).transform.position;
                            }
                        }
                    }

                    if (funcional)
                    {
                        if (pgActivo)
                        {
                            if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                            {
                                efecto.ProduceEfecto(10);

                                if (guardar || (!guardar && baseDeDatos.partidaDisponible[fichero]))
                                {
                                    IniciaConfirmacion();
                                }
                            }
                            else if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("B"))
                            {
                                efecto.ProduceEfecto(12);
                                ActivarPartidaGuardada(false, false);
                            }
                            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || (!pulsado && digitalY < 0))
                            {
                                pulsado = true;
                                efecto.ProduceEfecto(11);
                                partidasGuardadas.transform.GetChild(fichero).GetComponent<Image>().sprite = baseDeDatos.interfazPG[0];
                                fichero++;
                                mueveFlecha = true;
                            }
                            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || (!pulsado && digitalY > 0))
                            {
                                pulsado = true;
                                efecto.ProduceEfecto(11);
                                partidasGuardadas.transform.GetChild(fichero).GetComponent<Image>().sprite = baseDeDatos.interfazPG[0];
                                fichero--;
                                mueveFlecha = true;
                            }
                            else if (Input.GetKeyDown(KeyCode.B) || Input.GetButtonUp("X"))
                            {
                                efecto.ProduceEfecto(10);

                                if (baseDeDatos.partidaDisponible[fichero])
                                {
                                    borrar = true;
                                    IniciaConfirmacion();
                                }
                            }
                        }
                        else
                        {
                            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || (!pulsado && digitalY < 0))
                            {
                                pulsado = true;
                                efecto.ProduceEfecto(11);
                                pos++;
                                mueveFlecha = true;
                            }
                            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || (!pulsado && digitalY > 0))
                            {
                                pulsado = true;
                                efecto.ProduceEfecto(11);
                                pos--;
                                mueveFlecha = true;
                            }
                            else if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                            {
                                efecto.ProduceEfecto(10);

                                if (pos == 0)
                                {
                                    guardar = true;

                                    ActivarPartidaGuardada(true, false);
                                }
                                else if (pos == 1)
                                {
                                    guardar = false;
                                    ActivarPartidaGuardada(true, false);
                                    //IniciaConfirmacion();
                                }
                                else
                                {
                                    DesactivarMenu();
                                }
                            }
                            else if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("B"))
                            {
                                efecto.ProduceEfecto(12);
                                DesactivarMenu();
                            }
                        }
                    }
                }
                else
                {
                    if (funcional)
                    {
                        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || (!pulsado && digitalX != 0))
                        {
                            pulsado = true;
                            efecto.ProduceEfecto(11);
                            if (posConf == 0)
                            {
                                posConf = 1;
                                confirmacion.transform.GetChild(3).transform.position = confirmacion.transform.GetChild(2).transform.GetChild(1).transform.position;
                            }
                            else
                            {
                                posConf = 0;
                                confirmacion.transform.GetChild(3).transform.position = confirmacion.transform.GetChild(1).transform.GetChild(1).transform.position;
                            }
                        }
                        else if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                        {
                            efecto.ProduceEfecto(10);

                            if (posConf == 0)
                            {
                                if (borrar)
                                {
                                    SistemaGuardado.BorrarFichero(fichero);
                                    baseDeDatos.partidaDisponible[fichero] = false;
                                    borrar = false;
                                    ActualizaInterfazPG(fichero, false);
                                }
                                else if (guardar)
                                {
                                    GuardarPartida();
                                }
                                else
                                {
                                    IntermediarioJuego.idiomaCarga = baseDeDatos.idioma;
                                    IntermediarioJuego.ficheroCarga = fichero;
                                    IntermediarioJuego.cargar = true;
                                    DesactivarMenu();
                                }
                            }

                            DesactivarConfirmacion();
                        }
                        else if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("B"))
                        {
                            efecto.ProduceEfecto(12);
                            DesactivarConfirmacion();
                        }
                    }
                }
            }
            else
            {
                if (!confirmacionActiva)
                {
                    if (mueveFlecha)
                    {
                        if (fichero > 2)
                        {
                            fichero = 0;
                        }
                        else if (fichero < 0)
                        {
                            fichero = 2;
                        }

                        partidasGuardadas.transform.GetChild(fichero).GetComponent<Image>().sprite = baseDeDatos.interfazPG[1];
                    }

                    if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                    {
                        efecto.ProduceEfecto(10);

                        if (guardar || (!guardar && baseDeDatos.partidaDisponible[fichero]))
                        {
                            IniciaConfirmacion();
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || (!pulsado && digitalY < 0))
                    {
                        pulsado = true;
                        efecto.ProduceEfecto(11);
                        partidasGuardadas.transform.GetChild(fichero).GetComponent<Image>().sprite = baseDeDatos.interfazPG[0];
                        fichero++;
                        mueveFlecha = true;
                    }
                    else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || (!pulsado && digitalY > 0))
                    {
                        pulsado = true;
                        efecto.ProduceEfecto(11);
                        partidasGuardadas.transform.GetChild(fichero).GetComponent<Image>().sprite = baseDeDatos.interfazPG[0];
                        fichero--;
                        mueveFlecha = true;
                    }
                    else if (Input.GetKeyDown(KeyCode.B) || Input.GetButtonUp("X"))
                    {
                        efecto.ProduceEfecto(10);

                        if (baseDeDatos.partidaDisponible[fichero])
                        {
                            borrar = true;
                            IniciaConfirmacion();
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("B"))
                    {
                        efecto.ProduceEfecto(12);
                        ActivarPartidaGuardada(false, false);
                    }
                }
                else
                {
                    if (funcional)
                    {
                        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || (!pulsado && digitalX != 0))
                        {
                            pulsado = true;
                            efecto.ProduceEfecto(11);
                            if (posConf == 0)
                            {
                                posConf = 1;
                                confirmacion.transform.GetChild(3).transform.position = confirmacion.transform.GetChild(2).transform.GetChild(1).transform.position;
                            }
                            else
                            {
                                posConf = 0;
                                confirmacion.transform.GetChild(3).transform.position = confirmacion.transform.GetChild(1).transform.GetChild(1).transform.position;
                            }
                        }
                        else if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                        {
                            efecto.ProduceEfecto(10);

                            if (posConf == 0)
                            {
                                if (borrar)
                                {
                                    SistemaGuardado.BorrarFichero(fichero);
                                    baseDeDatos.partidaDisponible[fichero] = false;
                                    borrar = false;
                                    ActualizaInterfazPG(fichero, false);
                                }
                                else if (guardar)
                                {
                                    GuardarPartida();
                                }
                                else
                                {
                                    IntermediarioJuego.idiomaCarga = baseDeDatos.idioma;
                                    IntermediarioJuego.ficheroCarga = fichero;
                                    IntermediarioJuego.cargar = true;
                                    DesactivarMenu();
                                }
                            }

                            DesactivarConfirmacion();
                        }
                        else if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("B"))
                        {
                            efecto.ProduceEfecto(12);
                            DesactivarConfirmacion();
                        }
                    }
                }              
            }
        }
	}



    public void IniciarPuntoGuardado()
    {
        if (!activo)
        {
            menuGuardado.transform.GetChild(5).GetChild(6).gameObject.SetActive(false);
            menuGuardado.transform.GetChild(5).GetChild(7).gameObject.SetActive(false);
            menuGuardado.transform.GetChild(8).gameObject.SetActive(false);

            if (baseDeDatos.mandoActivo)
            {
                menuGuardado.transform.GetChild(5).GetChild(1).GetComponent<Image>().sprite = baseDeDatos.seleccionXBOX[0];
                menuGuardado.transform.GetChild(5).GetChild(3).GetComponent<Image>().sprite = baseDeDatos.volverXBOX[0];
                menuGuardado.transform.GetChild(5).GetChild(5).GetComponent<Image>().sprite = baseDeDatos.moverXBOX[0];
                menuGuardado.transform.GetChild(5).GetChild(7).GetComponent<Image>().sprite = baseDeDatos.xXBOX[0];
            }
            else
            {
                menuGuardado.transform.GetChild(5).GetChild(1).GetComponent<Image>().sprite = baseDeDatos.seleccionPC[0];
                menuGuardado.transform.GetChild(5).GetChild(3).GetComponent<Image>().sprite = baseDeDatos.volverPC[0];
                menuGuardado.transform.GetChild(5).GetChild(5).GetComponent<Image>().sprite = baseDeDatos.moverPC[0];
                menuGuardado.transform.GetChild(5).GetChild(7).GetComponent<Image>().sprite = baseDeDatos.bPC[0];
            }

            activo = true;
            pos = 0;
            mueveFlecha = true;
            controlJugador.setMensajeActivo(true);
            controlJugador.SetInterrogante(false);
            EstableceTextoMenu();
            menuGuardado.gameObject.SetActive(activo);
            ActivarPartidaGuardada(false, false);
            StartCoroutine(EsperaInicio());
        }
    }



    public void IniciaPGGameOver()
    {
        if (baseDeDatos.mandoActivo)
        {
            menuGuardado.transform.GetChild(5).GetChild(1).GetComponent<Image>().sprite = baseDeDatos.seleccionXBOX[0];
            menuGuardado.transform.GetChild(5).GetChild(3).GetComponent<Image>().sprite = baseDeDatos.volverXBOX[0];
            menuGuardado.transform.GetChild(5).GetChild(5).GetComponent<Image>().sprite = baseDeDatos.moverXBOX[0];
            menuGuardado.transform.GetChild(5).GetChild(7).GetComponent<Image>().sprite = baseDeDatos.xXBOX[0];
        }
        else
        {
            menuGuardado.transform.GetChild(5).GetChild(1).GetComponent<Image>().sprite = baseDeDatos.seleccionPC[0];
            menuGuardado.transform.GetChild(5).GetChild(3).GetComponent<Image>().sprite = baseDeDatos.volverPC[0];
            menuGuardado.transform.GetChild(5).GetChild(5).GetComponent<Image>().sprite = baseDeDatos.moverPC[0];
            menuGuardado.transform.GetChild(5).GetChild(7).GetComponent<Image>().sprite = baseDeDatos.bPC[0];
        }

        activo = true;
        guardar = false;
        pos = 0;
        EstableceTextoMenu();
        menuGuardado.gameObject.SetActive(activo);
        ActivarPartidaGuardada(true, true);
        StartCoroutine(EsperaInicio());
    }



    void GuardarPartida()
    {
        EstableceTextoGuardandoPartida();
        ActivaTexto();

        baseDeDatos.partidaDisponible[fichero] = true;

        SistemaGuardado.GuardarJugador(controlJugador, controlObjetos, fichero);

        for(int i = 0; i < baseDeDatos.numeroIntegrantesEquipo; i++)
        {
            baseDeDatos.listaPersonajesAliados[baseDeDatos.equipoAliado[i].indicePersonaje] = baseDeDatos.equipoAliado[i];
        }

        SistemaGuardado.GuardarBD(baseDeDatos, fichero);

        SistemaGuardado.GuardarMusica(musica, fichero);

        StartCoroutine(EsperaGuardado());
    }



    public void CargarPartida(int ficheroCarga)
    {
        controladorCarga.activo = true;

        StartCoroutine(EsperaCarga());
        fichero = ficheroCarga;

        MusicaData musData = SistemaGuardado.CargarMusica(fichero);
        musica.CambiaCancion(musData.ultimoIndice);

        cargado = false;
        PlayerData data = SistemaGuardado.CargarJugador(fichero);
        
        controlJugador.dinero = data.dinero;
        controlJugador.SetDificultad(data.dificultad);

        //////////////Ccontrol misiones secundarias
        controlObjetos.misionPedroActiva = data.misionPedroActiva;
        controlObjetos.contadorMonstruosPedro = data.contadorMonstruosPedro;

        controlObjetos.misionGamezActiva = data.misionGamezActiva;
        controlObjetos.tieneGema = data.tieneGema;
        controlObjetos.rescatePagado = data.rescatePagado;
        controlObjetos.orcoDerrotado = data.orcoDerrotado;
        controlObjetos.guardiaDerrotado = data.guardiaDerrotado;

        controlObjetos.hActiva = new bool[data.hActiva.Length];

        for(int i = 0; i < controlObjetos.hActiva.Length; i++)
        {
            controlObjetos.hActiva[i] = data.hActiva[i];
        }

        controlObjetos.misionHActiva = data.misionHActiva;

        controlObjetos.PersonajeHActivo();

        controlObjetos.misionNaniActiva = data.misionNaniActiva;
        controlObjetos.perdonarVidaOrco = data.perdonarVidaOrco;
        ////////////////////////////////////////////////////////////


        Vector3 position;
        position.x = data.posicion[0];
        position.y = data.posicion[1];
        position.z = data.posicion[2]; 

        controlJugador.transform.position = position;
        controlJugador.teleportMostrado = data.teleportMostrado;

        BDData dataBD = SistemaGuardado.CargarBD(fichero);

        baseDeDatos.nombreProta = dataBD.nombreProta;
        baseDeDatos.faccion = dataBD.faccion;
        controlJugador.EstableceFaccion(baseDeDatos.faccion);

        baseDeDatos.numeroAlmacenado = dataBD.numeroAlmacenado;
        baseDeDatos.teleportActivo = dataBD.teleportActivo;
        
        //Pociones
        for(int i = 0; i < 10; i++)
        {
            baseDeDatos.recetasPocionesDesbloqueadas[i] = dataBD.recetasPocionesDesbloqueadas[i];
        }
        baseDeDatos.puedeHacerPociones = dataBD.puedeHacerPociones;
        
        //Esgrima
        for (int i = 0; i < 10; i++)
        {
            baseDeDatos.retosEsgrimaDesbloqueados[i] = dataBD.retosEsgrimaDesbloqueados[i];
        }

        int numeroCofres = dataBD.numeroCofres;
        baseDeDatos.cofres = new bool[numeroCofres];

        for(int i = 0; i < numeroCofres; i++)
        {
            baseDeDatos.cofres[i] = dataBD.cofres[i];
        }

        if(baseDeDatos.numeroAlmacenado != 0)
        {
            for (int i = 0; i < baseDeDatos.numeroAlmacenado; i++)
            {
                baseDeDatos.personajesAlmacenados[i] = dataBD.personajesAlmacenados[i];
            }
        }

        for (int i = 0; i < baseDeDatos.listaPersonajesAliados.Length; i++)
        {
            baseDeDatos.listaPersonajesAliados[i].ataqueFisico = dataBD.ataqueFisico[i];
            baseDeDatos.listaPersonajesAliados[i].ataqueFisicoActual = dataBD.ataqueFisicoActual[i];
            baseDeDatos.listaPersonajesAliados[i].ataqueFisicoModificado = dataBD.ataqueFisicoModificado[i];
            baseDeDatos.listaPersonajesAliados[i].ataqueMagico = dataBD.ataqueMagico[i];
            baseDeDatos.listaPersonajesAliados[i].ataqueMagicoActual = dataBD.ataqueMagicoActual[i];
            baseDeDatos.listaPersonajesAliados[i].ataqueMagicoModificado = dataBD.ataqueMagicoModificado[i];
            baseDeDatos.listaPersonajesAliados[i].defensaFisica = dataBD.defensaFisica[i];
            baseDeDatos.listaPersonajesAliados[i].defensaFisicaActual = dataBD.defensaFisicaActual[i];
            baseDeDatos.listaPersonajesAliados[i].defensaFisicaModificada = dataBD.defensaFisicaModificada[i];
            baseDeDatos.listaPersonajesAliados[i].defensaMagica = dataBD.defensaMagica[i];
            baseDeDatos.listaPersonajesAliados[i].defensaMagicaActual = dataBD.defensaMagicaActual[i];
            baseDeDatos.listaPersonajesAliados[i].defensaMagicaModificada = dataBD.defensaMagicaModificada[i];
            baseDeDatos.listaPersonajesAliados[i].velocidad = dataBD.velocidad[i];
            baseDeDatos.listaPersonajesAliados[i].velocidadActual = dataBD.velocidadActual[i];
            baseDeDatos.listaPersonajesAliados[i].velocidadModificada = dataBD.velocidadModificada[i];
            baseDeDatos.listaPersonajesAliados[i].vida = dataBD.vida[i];
            baseDeDatos.listaPersonajesAliados[i].vidaActual = dataBD.vidaActual[i];
            baseDeDatos.listaPersonajesAliados[i].vidaModificada = dataBD.vidaModificada[i];
            baseDeDatos.listaPersonajesAliados[i].evasion = dataBD.evasion[i];
            baseDeDatos.listaPersonajesAliados[i].evasionActual = dataBD.evasionActual[i];
            baseDeDatos.listaPersonajesAliados[i].precision = dataBD.precision[i];
            baseDeDatos.listaPersonajesAliados[i].precisionActual = dataBD.precisionActual[i];

            baseDeDatos.listaPersonajesAliados[i].nivel = dataBD.nivel[i];
            baseDeDatos.listaPersonajesAliados[i].experiencia = dataBD.experiencia[i];
            baseDeDatos.listaPersonajesAliados[i].proximoNivel = dataBD.proximoNivel[i];

            baseDeDatos.listaPersonajesAliados[i].numeroAtaques = dataBD.numeroAtaques[i];

            for(int j = 0; j < 4; j++)
            {
                baseDeDatos.listaPersonajesAliados[i].indicesAtaque[j] = dataBD.indicesAtaque[i][j];
            }

            baseDeDatos.listaPersonajesAliados[i].habilidades = new Ataque[dataBD.numeroAtaques[i]];

            for (int z = 0; z < dataBD.numeroAtaques[i]; z++)
            {
                Ataque ataque = new Ataque();
                baseDeDatos.listaPersonajesAliados[i].habilidades[z] = ataque.BuscaAtaque(baseDeDatos.listaPersonajesAliados[i].indicesAtaque[z]);
            }

            baseDeDatos.listaPersonajesAliados[i].llevaCasco = dataBD.llevaCasco[i];
            if (dataBD.llevaCasco[i])
            {
                baseDeDatos.listaPersonajesAliados[i].casco = baseDeDatos.listaObjetos[dataBD.indiceCasco[i]];
            }
            else
            {
                baseDeDatos.listaPersonajesAliados[i].casco = null;
            }

            baseDeDatos.listaPersonajesAliados[i].llevaArmadura = dataBD.llevaArmadura[i];
            if (dataBD.llevaArmadura[i])
            {
                baseDeDatos.listaPersonajesAliados[i].armadura = baseDeDatos.listaObjetos[dataBD.indiceArmadura[i]];
            }
            else
            {
                baseDeDatos.listaPersonajesAliados[i].armadura = null;
            }

            baseDeDatos.listaPersonajesAliados[i].llevaBotas = dataBD.llevaBotas[i];
            if (dataBD.llevaBotas[i])
            {
                baseDeDatos.listaPersonajesAliados[i].botas = baseDeDatos.listaObjetos[dataBD.indiceBotas[i]];
            }
            else
            {
                baseDeDatos.listaPersonajesAliados[i].botas = null;
            }

            baseDeDatos.listaPersonajesAliados[i].llevaArma = dataBD.llevaArma[i];
            if (dataBD.llevaArma[i])
            {
                baseDeDatos.listaPersonajesAliados[i].arma = baseDeDatos.listaObjetos[dataBD.indiceArma[i]];
            }
            else
            {
                baseDeDatos.listaPersonajesAliados[i].arma = null;
            }

            baseDeDatos.listaPersonajesAliados[i].llevaComplemento = dataBD.llevaComplemento[i];
            if (dataBD.llevaComplemento[i])
            {
                baseDeDatos.listaPersonajesAliados[i].complemento = baseDeDatos.listaObjetos[dataBD.indiceComplemento[i]];
            }
            else
            {
                baseDeDatos.listaPersonajesAliados[i].complemento = null;
            }

            baseDeDatos.listaPersonajesAliados[i].llevaEscudo = dataBD.llevaEscudo[i];
            if (dataBD.llevaEscudo[i])
            {
                baseDeDatos.listaPersonajesAliados[i].escudo = baseDeDatos.listaObjetos[dataBD.indiceEscudo[i]];
            }
            else
            {
                baseDeDatos.listaPersonajesAliados[i].escudo = null;
            }
        }


        for (int i = 0; i < baseDeDatos.equipoAliado[0].numeroAtaques; i++)
        {
            baseDeDatos.equipoAliado[0].habilidades[i].energiaActual = dataBD.energiaR1[i];

            if (baseDeDatos.numeroIntegrantesEquipo > 1)
            {
                baseDeDatos.equipoAliado[1].habilidades[i].energiaActual = dataBD.energiaR2[i];
            }

            if (baseDeDatos.numeroIntegrantesEquipo > 2)
            {
                baseDeDatos.equipoAliado[2].habilidades[i].energiaActual = dataBD.energiaR3[i];
            }
        }

        

        for (int i = 0; i < 72; i++)
        {
            baseDeDatos.ataquesDesbloqueados[i] = dataBD.ataquesDesbloqueados[i];
        }

        baseDeDatos.numeroIntegrantesEquipo = dataBD.numeroIntegrantes;

        for (int i = 0; i < baseDeDatos.numeroIntegrantesEquipo; i++)
        {
            baseDeDatos.equipoAliado[i] = baseDeDatos.listaPersonajesAliados[dataBD.indices[i]];
        }

        baseDeDatos.listaPersonajesAliados[0].nombre = dataBD.nombreProta;
        baseDeDatos.listaPersonajesAliados[0].nombreIngles = dataBD.nombreProta;
        baseDeDatos.equipoAliado[0].nombre = dataBD.nombreProta;
        baseDeDatos.equipoAliado[0].nombreIngles = dataBD.nombreProta;

        for (int i = 0; i < dataBD.cantidadConsumiblesTotal; i++)
        {
            baseDeDatos.IncluirEnInventario(dataBD.indicesConsumibles[i], dataBD.cantidadesConsumibles[i]);
        }

        for (int i = 0; i < dataBD.cantidadOAtaqueTotal; i++)
        {
            baseDeDatos.IncluirEnInventario(dataBD.indicesOAtaque[i], dataBD.cantidadesOAtaque[i]);
        }

        for (int i = 0; i < dataBD.cantidadOEquipoTotal; i++)
        {
            baseDeDatos.IncluirEnInventario(dataBD.indicesOEquipo[i], dataBD.cantidadesOEquipo[i]);
        }

        for (int i = 0; i < dataBD.cantidadOClaveTotal; i++)
        {
            baseDeDatos.IncluirEnInventario(dataBD.indicesOClave[i], dataBD.cantidadesOClave[i]);
        }


        baseDeDatos.numeroMisionesActivas = dataBD.numeroMisionesActivas;
        for (int i = 0; i < baseDeDatos.numeroMisionesActivas; i++)
        {
            baseDeDatos.listaMisionesActivas[i] = baseDeDatos.listaMisiones[dataBD.indicesMisionesActivas[i]];
            baseDeDatos.listaMisionesActivas[i].completada = dataBD.misionCompletada[i];
        }

        baseDeDatos.numeroMisionesPrincipales = dataBD.numeroMisionesPrincipales;
        for (int i = 0; i < baseDeDatos.numeroMisionesPrincipales; i++)
        {
            baseDeDatos.listaMisionesPrincipales[i] = baseDeDatos.listaMisiones[dataBD.indicesMisionesPrincipales[i]];
        }

        baseDeDatos.numeroMisionesSecundarias = dataBD.numeroMisionesSecundarias;
        for (int i = 0; i < baseDeDatos.numeroMisionesSecundarias; i++)
        {
            baseDeDatos.listaMisionesSecundarias[i] = baseDeDatos.listaMisiones[dataBD.indicesMisionesSecundarias[i]];
        }

        baseDeDatos.numeroMisionesReclutamiento = dataBD.numeroMisionesReclutamiento;
        for (int i = 0; i < baseDeDatos.numeroMisionesReclutamiento; i++)
        {
            baseDeDatos.listaMisionesReclutamiento[i] = baseDeDatos.listaMisiones[dataBD.indicesMisionesReclutamiento[i]];
        }

        //Tiempo
        baseDeDatos.segundos = dataBD.segundos;
        baseDeDatos.minutos = dataBD.minutos;
        baseDeDatos.horas = dataBD.horas;

        //Mapa
        baseDeDatos.indiceInicial = dataBD.indiceInicial;
        baseDeDatos.indiceObjetivo = dataBD.indiceObjetivo;
        baseDeDatos.controlMapas.IniciaMapas(baseDeDatos.indiceInicial);

        for(int i = 0; i < 30; i++)
        {
            baseDeDatos.zonaVisitada[i] = dataBD.zonaVisitada[i];
        }

        baseDeDatos.zonaCamara = dataBD.zonaCamara;
        baseDeDatos.areaCamara = dataBD.areaCamara;

        for (int i = 0; i < dataBD.personajesDesactivados.Length; i++)
        {
            baseDeDatos.personajesDesactivados[i] = dataBD.personajesDesactivados[i];
        }
    }



    void DesactivarMenu()
    {
        activo = false;
        funcional = false;
        controlJugador.setMensajeActivo(activo);
        menuGuardado.gameObject.SetActive(activo);
    }



    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            controlJugador.SetExclamacion(true);
        }

        yield return null;
    }



    IEnumerator OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            controlJugador.SetExclamacion(false);
        }

        yield return null;
    }



    IEnumerator EsperaInicio()
    {
        yield return new WaitForSeconds(0.001f);
        funcional = true;
    }



    IEnumerator EsperaGuardado()
    {
        activo = false;
        
        yield return new WaitForSeconds(2);

        ActualizaInterfazPG(fichero, true);
        DesactivaTexto();

        activo = true;
    }



    void ActivaTexto()
    {
        menuGuardado.transform.GetChild(8).gameObject.SetActive(true);
    }



    void DesactivaTexto()
    {
        menuGuardado.transform.GetChild(8).gameObject.SetActive(false);
    }



    void IniciaConfirmacion()
    {
        confirmacionActiva = true;
        confirmacion.SetActive(confirmacionActiva);
        posConf = 1;
        confirmacion.transform.GetChild(3).transform.position = confirmacion.transform.GetChild(2).transform.GetChild(1).transform.position;

        if (!borrar)
        {
            if (guardar)
            {
                EstableceTextoGuardado();
            }
            else
            {
                EstableceTextoCarga();
            }
        }
        else
        {
            EstableceTextoBorrado();
        }
    }



    void DesactivarConfirmacion()
    {
        confirmacionActiva = false;
        confirmacion.SetActive(confirmacionActiva);
    }



    void EstableceTextoGuardado()
    {
        if(baseDeDatos.idioma == 1)
        {
            confirmacion.transform.GetChild(0).GetComponent<Text>().text = "Are you sure you want to save the game? \n This will overwrite any previous data";
            confirmacion.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = "Yes";
            confirmacion.transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = "No";
        }
        else
        {
            confirmacion.transform.GetChild(0).GetComponent<Text>().text = "¿Seguro quieres guardar partida? \n Esto sobreescribirá cualquier partida anterior";
            confirmacion.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = "Sí";
            confirmacion.transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = "No";
        }
    }



    void EstableceTextoCarga()
    {
        if (baseDeDatos.idioma == 1)
        {
            confirmacion.transform.GetChild(0).GetComponent<Text>().text = "Are you sure you want to load the game? \n You will lose unsaved data";
            confirmacion.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = "Yes";
            confirmacion.transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = "No";
        }
        else
        {
            confirmacion.transform.GetChild(0).GetComponent<Text>().text = "¿Seguro quieres cargar partida? \n Perderás los datos no guardados";
            confirmacion.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = "Sí";
            confirmacion.transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = "No";
        }
    }



    void EstableceTextoGuardandoPartida()
    {
        if (baseDeDatos.idioma == 1)
        {
            menuGuardado.transform.GetChild(8).transform.GetChild(0).GetComponent<Text>().text = "Saving game, please wait.............................................";
        }
        else
        {
            menuGuardado.transform.GetChild(8).transform.GetChild(0).GetComponent<Text>().text = "Guardando partida, por favor espera.............................................";
        }
    }



    void EstableceTextoMenu()
    {
        if(baseDeDatos.idioma == 1)
        {
            menuGuardado.transform.GetChild(2).transform.GetChild(1).GetComponent<Text>().text = "SAVE";
            menuGuardado.transform.GetChild(3).transform.GetChild(1).GetComponent<Text>().text = "LOAD";
            menuGuardado.transform.GetChild(4).transform.GetChild(1).GetComponent<Text>().text = "EXIT";

            menuGuardado.transform.GetChild(5).transform.GetChild(0).GetComponent<Text>().text = "Select";
            menuGuardado.transform.GetChild(5).transform.GetChild(2).GetComponent<Text>().text = "Back";
            menuGuardado.transform.GetChild(5).transform.GetChild(4).GetComponent<Text>().text = "Move";
            menuGuardado.transform.GetChild(5).transform.GetChild(6).GetComponent<Text>().text = "Delete";
        }
        else
        {
            menuGuardado.transform.GetChild(2).transform.GetChild(1).GetComponent<Text>().text = "GUARDAR";
            menuGuardado.transform.GetChild(3).transform.GetChild(1).GetComponent<Text>().text = "CARGAR";
            menuGuardado.transform.GetChild(4).transform.GetChild(1).GetComponent<Text>().text = "SALIR";

            menuGuardado.transform.GetChild(5).transform.GetChild(0).GetComponent<Text>().text = "Seleccionar";
            menuGuardado.transform.GetChild(5).transform.GetChild(2).GetComponent<Text>().text = "Volver";
            menuGuardado.transform.GetChild(5).transform.GetChild(4).GetComponent<Text>().text = "Mover";
            menuGuardado.transform.GetChild(5).transform.GetChild(6).GetComponent<Text>().text = "Borrar";
        }
    }



    IEnumerator EsperaCarga()
    {
        pantallaCarga.SetActive(true);

        DesactivarConfirmacion();
        DesactivarMenu();

        yield return new WaitForSeconds(3);
        cargado = true;
        yield return new WaitForSeconds(0.5f);

        controladorCarga.DesactivaCarga(false);
        fondoNegro.SetActive(false);
    }



    void CambiaControl()
    {
        if (!baseDeDatos.mandoActivo)
        {
            baseDeDatos.mandoActivo = true;

            menuGuardado.transform.GetChild(5).GetChild(1).GetComponent<Image>().sprite = baseDeDatos.seleccionXBOX[0];
            menuGuardado.transform.GetChild(5).GetChild(3).GetComponent<Image>().sprite = baseDeDatos.volverXBOX[0];
            menuGuardado.transform.GetChild(5).GetChild(5).GetComponent<Image>().sprite = baseDeDatos.moverXBOX[0];
            menuGuardado.transform.GetChild(5).GetChild(7).GetComponent<Image>().sprite = baseDeDatos.xXBOX[0];
        }
        else
        {
            baseDeDatos.mandoActivo = false;

            menuGuardado.transform.GetChild(5).GetChild(1).GetComponent<Image>().sprite = baseDeDatos.seleccionPC[0];
            menuGuardado.transform.GetChild(5).GetChild(3).GetComponent<Image>().sprite = baseDeDatos.volverPC[0];
            menuGuardado.transform.GetChild(5).GetChild(5).GetComponent<Image>().sprite = baseDeDatos.moverPC[0];
            menuGuardado.transform.GetChild(5).GetChild(7).GetComponent<Image>().sprite = baseDeDatos.bPC[0];
        }
    }



    public void ActivarPartidaGuardada(bool valor, bool gameOverActivo)
    {
        if (baseDeDatos.idioma == 1)
        {
            if (guardar)
            {
                partidasGuardadas.transform.GetChild(3).GetComponent<Text>().text = "Save";
            }
            else
            {
                partidasGuardadas.transform.GetChild(3).GetComponent<Text>().text = "Load";
            }
        }
        else
        {
            if (guardar)
            {
                partidasGuardadas.transform.GetChild(3).GetComponent<Text>().text = "Guardar";
            }
            else
            {
                partidasGuardadas.transform.GetChild(3).GetComponent<Text>().text = "Cargar";
            }
        }

        if (!gameOver)
        {
            menuGuardado.transform.GetChild(6).gameObject.SetActive(true);
        }

        for(int i = 0; i < 3; i++)
        {
            ActualizaInterfazPG(i, baseDeDatos.partidaDisponible[i]);
        }

        pgActivo = valor;

        if (pgActivo)
        {
            menuGuardado.transform.GetChild(5).GetChild(6).gameObject.SetActive(true);
            menuGuardado.transform.GetChild(5).GetChild(7).gameObject.SetActive(true);

            partidasGuardadas.transform.GetChild(0).GetComponent<Image>().sprite = baseDeDatos.interfazPG[1];
            partidasGuardadas.transform.GetChild(1).GetComponent<Image>().sprite = baseDeDatos.interfazPG[0];
            partidasGuardadas.transform.GetChild(2).GetComponent<Image>().sprite = baseDeDatos.interfazPG[0];

            fichero = 0;
        }
        else
        {
            menuGuardado.transform.GetChild(5).GetChild(6).gameObject.SetActive(false);
            menuGuardado.transform.GetChild(5).GetChild(7).gameObject.SetActive(false);
        }

        partidasGuardadas.SetActive(pgActivo);

        gameOver = gameOverActivo;
    }



    void ActualizaInterfazPG(int indicePG, bool existe)
    {
        
        if (!existe)
        {
            baseDeDatos.partidaDisponible[indicePG] = false;
            partidasGuardadas.transform.GetChild(indicePG).GetChild(1).gameObject.SetActive(false);
            partidasGuardadas.transform.GetChild(indicePG).GetChild(2).gameObject.SetActive(false);
            partidasGuardadas.transform.GetChild(indicePG).GetChild(3).gameObject.SetActive(false);
            partidasGuardadas.transform.GetChild(indicePG).GetChild(4).gameObject.SetActive(false);
            partidasGuardadas.transform.GetChild(indicePG).GetChild(5).gameObject.SetActive(false);
            partidasGuardadas.transform.GetChild(indicePG).GetChild(6).gameObject.SetActive(false);
            partidasGuardadas.transform.GetChild(indicePG).GetChild(7).gameObject.SetActive(false);
        }
        else
        {
            baseDeDatos.partidaDisponible[indicePG] = true;
            PlayerData data = SistemaGuardado.CargarJugador(indicePG);
            BDData dataBD = SistemaGuardado.CargarBD(indicePG);

            partidasGuardadas.transform.GetChild(indicePG).GetChild(1).gameObject.SetActive(true);
            partidasGuardadas.transform.GetChild(indicePG).GetChild(2).gameObject.SetActive(true);

            int faccion = dataBD.faccion;
            //0 -- golpista 1 -- imperio 2 -- regente 3 -- Resistencia 4 -- R.Asalto 5 -- R.Especiales 6 -- R.Investigacion 7 -- Cupula 8 -- Nada
            partidasGuardadas.transform.GetChild(indicePG).GetChild(2).GetComponent<Image>().sprite = baseDeDatos.prota[faccion];

            if (faccion == 8)
            {
                partidasGuardadas.transform.GetChild(indicePG).GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                partidasGuardadas.transform.GetChild(indicePG).GetChild(1).GetComponent<Image>().sprite = baseDeDatos.banderas[faccion];
            }

            partidasGuardadas.transform.GetChild(indicePG).GetChild(3).gameObject.SetActive(true);
            partidasGuardadas.transform.GetChild(indicePG).GetChild(3).GetComponent<Text>().text = dataBD.nombreProta;

            partidasGuardadas.transform.GetChild(indicePG).GetChild(4).gameObject.SetActive(true);
            partidasGuardadas.transform.GetChild(indicePG).GetChild(4).GetComponent<Text>().text = "LV: " + dataBD.nivel[0];

            partidasGuardadas.transform.GetChild(indicePG).GetChild(5).gameObject.SetActive(true);


            if(baseDeDatos.idioma == 1)
            {
                switch (data.dificultad)
                {
                    case 0:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(5).GetComponent<Text>().text = "Easy";
                        break;
                    case 1:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(5).GetComponent<Text>().text = "Medium";
                        break;
                    case 2:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(5).GetComponent<Text>().text = "Hard";
                        break;
                    case 3:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(5).GetComponent<Text>().text = "Titan";
                        break;
                }

                switch (dataBD.indiceInicial)
                {
                    case 0:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "Origin Town";
                        break;
                    case 1:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "R5";
                        break;
                    case 2:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "El Paso";
                        break;
                    case 3:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "R6";
                        break;
                    case 4:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "Pedrán";
                        break;
                    case 5:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "R7";
                        break;
                    case 6:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "R8";
                        break;
                    case 7:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "Forest Town";
                        break;
                    case 8:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "R9";
                        break;
                    case 9:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "River Town";
                        break;
                    case 10:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "University";
                        break;
                    case 11:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "R10";
                        break;
                    case 12:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "Canda";
                        break;
                    case 13:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "Hope Forest";
                        break;
                    case 14:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "R11";
                        break;
                    case 15:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "New University";
                        break;
                    case 17:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "Refuge Town";
                        break;
                    case 18:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "Big Grotto";
                        break;
                    case 19:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "Sand Town";
                        break;
                    case 20:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "R12";
                        break;
                    case 21:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "Manfa";
                        break;
                    case 22:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "R4";
                        break;
                    case 23:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "Great Temple of Áncia";
                        break;
                    case 24:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "R1";
                        break;
                    case 25:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "Imperial City";
                        break;
                    case 26:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "R2";
                        break;
                    case 27:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "Albay Town";
                        break;
                    case 28:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "R3";
                        break;
                    case 29:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "Porto Bello";
                        break;
                }

            }
            else
            {
                switch (data.dificultad)
                {
                    case 0:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(5).GetComponent<Text>().text = "Facil";
                        break;
                    case 1:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(5).GetComponent<Text>().text = "Intermedio";
                        break;
                    case 2:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(5).GetComponent<Text>().text = "Difícil";
                        break;
                    case 3:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(5).GetComponent<Text>().text = "Titán";
                        break;
                }

                switch (dataBD.indiceInicial)
                {
                    case 0:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "Pueblo Origen";
                        break;
                    case 1:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "R5";
                        break;
                    case 2:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "El Paso";
                        break;
                    case 3:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "R6";
                        break;
                    case 4:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "Pedrán";
                        break;
                    case 5:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "R7";
                        break;
                    case 6:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "R8";
                        break;
                    case 7:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "Pueblo del Bosque";
                        break;
                    case 8:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "R9";
                        break;
                    case 9:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "Pueblo del Rio";
                        break;
                    case 10:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "Universidad";
                        break;
                    case 11:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "R10";
                        break;
                    case 12:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "Pueblo Canda";
                        break;
                    case 13:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "Bosque Esperanza";
                        break;
                    case 14:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "R11";
                        break;
                    case 15:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "Nueva Universidad";
                        break;
                    case 17:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "Pueblo Refugio";
                        break;
                    case 18:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "Gran Gruta";
                        break;
                    case 19:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "Pueblo Arena";
                        break;
                    case 20:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "R12";
                        break;
                    case 21:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "Manfa";
                        break;
                    case 22:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "R4";
                        break;
                    case 23:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "Gran Templo de Ancia";
                        break;
                    case 24:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "R1";
                        break;
                    case 25:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "Ciudad Imperial";
                        break;
                    case 26:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "R2";
                        break;
                    case 27:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "Pueblo Albay";
                        break;
                    case 28:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "R3";
                        break;
                    case 29:
                        partidasGuardadas.transform.GetChild(indicePG).GetChild(6).GetComponent<Text>().text = "Porto Bello";
                        break;
                }
            }

            partidasGuardadas.transform.GetChild(indicePG).GetChild(6).gameObject.SetActive(true);

            partidasGuardadas.transform.GetChild(indicePG).GetChild(7).gameObject.SetActive(true);

            string minutos;

            if (dataBD.minutos < 10)
            {
                minutos = "0" + dataBD.minutos;
            }
            else
            {
                minutos = "" + dataBD.minutos;
            }

            partidasGuardadas.transform.GetChild(indicePG).GetChild(7).GetComponent<Text>().text = dataBD.horas + ":" + minutos;
        }
    }



    void EstableceTextoBorrado()
    {
        if (baseDeDatos.idioma == 0)
        {
            confirmacion.transform.GetChild(0).GetComponent<Text>().text = "¿Seguro quieres borrar la partida? \n Esto eliminará los datos guardados";
            confirmacion.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = "Sí";
            confirmacion.transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = "No";

        }
        else if (baseDeDatos.idioma == 1)
        {
            confirmacion.transform.GetChild(0).GetComponent<Text>().text = "Are you sure you want to delete the game? \n This will delete any previous data";
            confirmacion.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = "Yes";
            confirmacion.transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = "No";
        }
    }
}
