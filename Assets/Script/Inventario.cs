using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour {
    public GameObject descripcion, listaObjetos, tipoObjeto, imagenObjeto, flecha;

    GameObject manager;
    BaseDatos baseDeDatos;
    MusicaManager musica;
    Mapa mapa;

    int categoria; //tipo de menu en el que nos encontramos 0 --> consumibles, 1 --> equipo, 2 --> ataques, 3 --> obj. clave
    int pos;
    int pagina;
    int numeroPaginas;
    int numeroPosicionesPorPagina;
    int numeroElementosTotales;
    int posConf, posSelec, posAtq;
    int miembrosEquipo;

    public bool activo;
    public bool realizaAccion;
    bool confirmacionActiva, seleccionActiva, seleccionApAtqActiva, muestraAtaques;
    bool combate, textoActivo, textoCombate, quitaTexto;
    bool[] puedeAprender;

    bool pulsado;
    bool objetoClave;

    float digitalX;
    float digitalY;

    void Start ()
    {
        manager = GameObject.Find("GameManager");
        musica = GameObject.Find("EfectosSonido").GetComponent<MusicaManager>();
        baseDeDatos = manager.GetComponent<BaseDatos>();
        mapa = GameObject.Find("MensajesEnPantalla").transform.GetChild(12).GetComponent<Mapa>();
        activo = false;
        confirmacionActiva = seleccionActiva = seleccionApAtqActiva = false;

        posConf = posSelec = 0;
        pulsado = false;
        
        this.gameObject.SetActive(activo);
        DesactivaSeleccion();
        DesactivaSeleccionAtq();
        DesactivarConfirmacion();
        DesactivaTexto();
        OcultaAtaques();

        puedeAprender = new bool[3];
    }



    void Update ()
    {
        if (activo)
        {
            if (!objetoClave)
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
                    this.transform.GetChild(8).GetChild(0).GetComponent<Image>().sprite = baseDeDatos.seleccionXBOX[0];
                    this.transform.GetChild(8).GetChild(2).GetComponent<Image>().sprite = baseDeDatos.volverXBOX[0];
                    this.transform.GetChild(8).GetChild(4).GetComponent<Image>().sprite = baseDeDatos.moverXBOX[0];
                }
                else
                {
                    this.transform.GetChild(8).GetChild(0).GetComponent<Image>().sprite = baseDeDatos.seleccionPC[0];
                    this.transform.GetChild(8).GetChild(2).GetComponent<Image>().sprite = baseDeDatos.volverPC[0];
                    this.transform.GetChild(8).GetChild(4).GetComponent<Image>().sprite = baseDeDatos.moverPC[0];
                }

                if (textoActivo)
                {
                    if (Input.GetKeyDown(KeyCode.N) || Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("A") || Input.GetButtonUp("B"))
                    {
                        if (quitaTexto)
                        {
                            DesactivaTexto();
                        }
                    }
                }
                else if (textoCombate)
                {
                    if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A") || Input.GetButtonUp("B"))
                    {
                        musica.ProduceEfecto(10);
                        if (quitaTexto)
                        {
                            DesactivaTextoCombate();
                            CierraMenu();
                        }
                    }
                }
                else if (confirmacionActiva)
                {
                    if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                    {
                        musica.ProduceEfecto(10);

                        if (posConf == 0)
                        {
                            if (categoria == 0)
                            {
                                ActivaSeleccion();
                            }
                            else if (categoria == 2)
                            {
                                ActivaSeleccionAtq();
                            }
                        }
                        else
                        {
                            DesactivarConfirmacion();
                        }

                    }
                    else if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("B"))
                    {
                        musica.ProduceEfecto(12);
                        DesactivarConfirmacion();
                    }
                    else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || (!pulsado && digitalY != 0))
                    {
                        pulsado = true;
                        musica.ProduceEfecto(11);
                        if (posConf == 0)
                        {
                            posConf = 1;
                            this.transform.GetChild(5).transform.GetChild(4).transform.position = this.transform.GetChild(5).transform.GetChild(3).transform.position;
                        }
                        else
                        {
                            posConf = 0;
                            this.transform.GetChild(5).transform.GetChild(4).transform.position = this.transform.GetChild(5).transform.GetChild(2).transform.position;
                        }
                    }
                }
                else if (seleccionActiva)
                {
                    if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                    {
                        musica.ProduceEfecto(10);
                        if (combate)
                        {
                            AplicaObjetoCombate();
                        }
                        else
                        {
                            AplicaObjeto();
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("B"))
                    {
                        musica.ProduceEfecto(12);
                        DesactivaSeleccion();
                    }
                    else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || (!pulsado && digitalY < 0))
                    {
                        pulsado = true;
                        musica.ProduceEfecto(11);
                        if (posSelec == 0)
                        {
                            if (miembrosEquipo > 1)
                            {
                                posSelec = 1;
                            }
                        }
                        else if (posSelec == 1)
                        {
                            if (miembrosEquipo > 2)
                            {
                                posSelec = 2;
                            }
                            else
                            {
                                posSelec = 0;
                            }
                        }
                        else
                        {
                            posSelec = 0;
                        }

                        this.transform.GetChild(6).transform.GetChild(3).transform.position = this.transform.GetChild(6).transform.GetChild(posSelec).transform.GetChild(5).transform.position;
                    }
                    else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || (!pulsado && digitalY > 0))
                    {
                        pulsado = true;
                        musica.ProduceEfecto(11);
                        if (posSelec == 0)
                        {
                            if (miembrosEquipo > 2)
                            {
                                posSelec = 2;
                            }
                            else if (miembrosEquipo > 1)
                            {
                                posSelec = 1;
                            }
                        }
                        else if (posSelec == 1)
                        {
                            posSelec = 0;
                        }
                        else
                        {
                            posSelec = 1;
                        }

                        this.transform.GetChild(6).transform.GetChild(3).transform.position = this.transform.GetChild(6).transform.GetChild(posSelec).transform.GetChild(5).transform.position;
                    }
                }
                else if (muestraAtaques)
                {
                    if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                    {
                        musica.ProduceEfecto(10);
                        if (!combate)
                        {
                            AprendeAtaque(posAtq);
                            OcultaAtaques();
                            DesactivaSeleccionAtq();
                        }
                        ActivaTextoCombate();
                    }
                    else if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("B"))
                    {
                        musica.ProduceEfecto(12);
                        OcultaAtaques();
                        ActivaSeleccionAtq();
                    }
                    else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || (!pulsado && digitalY < 0))
                    {
                        pulsado = true;
                        musica.ProduceEfecto(11);
                        posAtq++;

                        if (posAtq == 4)
                        {
                            posAtq = 0;
                        }

                        this.transform.GetChild(7).transform.GetChild(4).transform.GetChild(4).transform.position = this.transform.GetChild(7).transform.GetChild(4).transform.GetChild(posAtq).transform.GetChild(7).transform.position;
                    }
                    else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || (!pulsado && digitalY > 0))
                    {
                        pulsado = true;
                        musica.ProduceEfecto(11);
                        posAtq--;

                        if (posAtq == -1)
                        {
                            posAtq = 3;
                        }

                        this.transform.GetChild(7).transform.GetChild(4).transform.GetChild(4).transform.position = this.transform.GetChild(7).transform.GetChild(4).transform.GetChild(posAtq).transform.GetChild(7).transform.position;
                    }
                }
                else if (seleccionApAtqActiva)
                {
                    if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                    {
                        musica.ProduceEfecto(10);
                        if (!combate)
                        {
                            AplicaObjeto();
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("B"))
                    {
                        musica.ProduceEfecto(12);
                        DesactivaSeleccionAtq();
                    }
                    else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || (!pulsado && digitalY < 0))
                    {
                        pulsado = true;
                        musica.ProduceEfecto(11);
                        if (posSelec == 0)
                        {
                            if (miembrosEquipo > 1)
                            {
                                posSelec = 1;
                            }
                        }
                        else if (posSelec == 1)
                        {
                            if (miembrosEquipo > 2)
                            {
                                posSelec = 2;
                            }
                            else
                            {
                                posSelec = 0;
                            }
                        }
                        else
                        {
                            posSelec = 0;
                        }

                        this.transform.GetChild(7).transform.GetChild(3).transform.position = this.transform.GetChild(7).transform.GetChild(posSelec).transform.GetChild(7).transform.position;
                    }
                    else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || (!pulsado && digitalY > 0))
                    {
                        pulsado = true;
                        musica.ProduceEfecto(11);
                        if (posSelec == 0)
                        {
                            if (miembrosEquipo > 2)
                            {
                                posSelec = 2;
                            }
                            else if (miembrosEquipo > 1)
                            {
                                posSelec = 1;
                            }
                        }
                        else if (posSelec == 1)
                        {
                            posSelec = 0;
                        }
                        else
                        {
                            posSelec = 1;
                        }

                        this.transform.GetChild(7).transform.GetChild(3).transform.position = this.transform.GetChild(7).transform.GetChild(posSelec).transform.GetChild(7).transform.position;
                    }
                }
                else
                {
                    if (numeroPosicionesPorPagina != 0)
                    {
                        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || (!pulsado && digitalY < 0))
                        {
                            pulsado = true;

                            musica.ProduceEfecto(11);

                            if (pos != (numeroPosicionesPorPagina - 1))
                            {
                                pos++;
                            }
                            else
                            {
                                if (numeroPaginas != 0)
                                {
                                    pagina++;

                                    if (pagina == numeroPaginas)
                                    {
                                        pagina = 0;
                                    }

                                    CalculaPosicionesPagina(pagina);
                                    ActualizaListaObjetos();
                                    ActualizaDescripcionObjeto();
                                }

                                pos = 0;
                            }

                            MueveFlecha();
                        }
                        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || (!pulsado && digitalY > 0))
                        {
                            pulsado = true;

                            musica.ProduceEfecto(11);

                            if (pos != 0)
                            {
                                pos--;
                            }
                            else
                            {
                                if (numeroPaginas != 0)
                                {
                                    if (pagina == 0)
                                    {
                                        pagina = numeroPaginas - 1;
                                    }
                                    else
                                    {
                                        pagina--;
                                    }
                                }

                                CalculaPosicionesPagina(pagina);
                                ActualizaListaObjetos();
                                ActualizaDescripcionObjeto();
                                pos = numeroPosicionesPorPagina - 1;

                            }

                            MueveFlecha();
                        }
                        else if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                        {
                            musica.ProduceEfecto(10);
                            int posicionActual = pos + pagina * 10;

                            if (categoria == 0)
                            {
                                if (baseDeDatos.cantidadesObjetosConsumibles[pos] != 0)
                                {
                                    IniciarConfirmacion();
                                }
                            }
                            else if (categoria == 2)
                            {
                                if (baseDeDatos.cantidadesObjetosAtaques[pos] != 0)
                                {
                                    IniciarConfirmacion();
                                }
                            }
                            else if (categoria == 3)
                            {
                                if (baseDeDatos.listaObjetosClave[pos].indiceTipo == 3)
                                {
                                    objetoClave = true;
                                    mapa.ActivaMenu(false);
                                }
                            }
                        }
                    }

                    if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || (!pulsado && digitalX < 0))
                    {
                        pulsado = true;

                        musica.ProduceEfecto(11);
                        categoria--;

                        if (categoria < 0)
                        {
                            categoria = 3;
                        }

                        MueveCategoria();
                    }
                    else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || (!pulsado && digitalX > 0))
                    {
                        pulsado = true;
                        musica.ProduceEfecto(11);
                        categoria++;

                        if (categoria > 3)
                        {
                            categoria = 0;
                        }

                        MueveCategoria();
                    }
                    else if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("B"))
                    {
                        musica.ProduceEfecto(12);
                        CierraMenu();
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("B"))
                {
                    objetoClave = false;
                }
            }
        }
    }



    void MueveFlecha()
    {
        flecha.transform.position = listaObjetos.transform.GetChild(2).transform.GetChild(pos).transform.position;
        ActualizaDescripcionObjeto();
    }



    void MueveCategoria()
    {
        switch (categoria)
        {
            case 0:
                numeroElementosTotales = baseDeDatos.numeroObjetosConsumibles;
                numeroPaginas = numeroElementosTotales / 10;
                numeroPaginas++;

                if(baseDeDatos.idioma == 1)
                {
                    tipoObjeto.transform.GetChild(0).GetComponent<Text>().text = "< Consumables >";
                }
                else
                {
                    tipoObjeto.transform.GetChild(0).GetComponent<Text>().text = "< Consumibles >";
                }

                tipoObjeto.transform.GetChild(1).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                tipoObjeto.transform.GetChild(2).GetComponent<Image>().color = new Color32(100, 100, 100, 255);
                tipoObjeto.transform.GetChild(3).GetComponent<Image>().color = new Color32(100, 100, 100, 255);
                tipoObjeto.transform.GetChild(4).GetComponent<Image>().color = new Color32(100, 100, 100, 255);
                break;
            case 1:
                numeroElementosTotales = baseDeDatos.numeroObjetosEquipo;
                numeroPaginas = numeroElementosTotales / 10;
                numeroPaginas++;

                if (baseDeDatos.idioma == 1)
                {
                    tipoObjeto.transform.GetChild(0).GetComponent<Text>().text = "< Equipment >";
                }
                else
                {
                    tipoObjeto.transform.GetChild(0).GetComponent<Text>().text = "< Equipables >";
                }

                tipoObjeto.transform.GetChild(2).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                tipoObjeto.transform.GetChild(1).GetComponent<Image>().color = new Color32(100, 100, 100, 255);
                tipoObjeto.transform.GetChild(3).GetComponent<Image>().color = new Color32(100, 100, 100, 255);
                tipoObjeto.transform.GetChild(4).GetComponent<Image>().color = new Color32(100, 100, 100, 255);
                break;
            case 2:
                numeroElementosTotales = baseDeDatos.numeroObjetosAtaques;
                numeroPaginas = numeroElementosTotales / 10;
                numeroPaginas++;

                if (baseDeDatos.idioma == 1)
                {
                    tipoObjeto.transform.GetChild(0).GetComponent<Text>().text = "< Attacks >";
                }
                else
                {
                    tipoObjeto.transform.GetChild(0).GetComponent<Text>().text = "< Ataques >";
                }

                tipoObjeto.transform.GetChild(3).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                tipoObjeto.transform.GetChild(2).GetComponent<Image>().color = new Color32(100, 100, 100, 255);
                tipoObjeto.transform.GetChild(1).GetComponent<Image>().color = new Color32(100, 100, 100, 255);
                tipoObjeto.transform.GetChild(4).GetComponent<Image>().color = new Color32(100, 100, 100, 255);
                break;
            case 3:
                numeroElementosTotales = baseDeDatos.numeroObjetosClave;
                numeroPaginas = numeroElementosTotales / 10;
                numeroPaginas++;

                if (baseDeDatos.idioma == 1)
                {
                    tipoObjeto.transform.GetChild(0).GetComponent<Text>().text = "< Key >";
                }
                else
                {
                    tipoObjeto.transform.GetChild(0).GetComponent<Text>().text = "< Clave >";
                }

                tipoObjeto.transform.GetChild(4).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                tipoObjeto.transform.GetChild(2).GetComponent<Image>().color = new Color32(100, 100, 100, 255);
                tipoObjeto.transform.GetChild(3).GetComponent<Image>().color = new Color32(100, 100, 100, 255);
                tipoObjeto.transform.GetChild(1).GetComponent<Image>().color = new Color32(100, 100, 100, 255);
                break;
        }

        pos = 0;
        pagina = 0;
        CalculaPosicionesPagina(pagina);
        MueveFlecha();
        ActualizaDescripcionObjeto();
        ActualizaListaObjetos();
    }



    void CalculaPosicionesPagina(int pagina)
    {
        if(categoria == 0)
        {
            if(baseDeDatos.numeroObjetosConsumibles <= 10)
            {
                numeroPosicionesPorPagina = baseDeDatos.numeroObjetosConsumibles;
            }
            else
            {
                if(pagina == 0)
                {
                    numeroPosicionesPorPagina = 10;
                }
                else
                {
                    numeroPosicionesPorPagina = baseDeDatos.numeroObjetosConsumibles % (pagina * 10);
                }
            }
        }
        else if (categoria == 1)
        {
            if(baseDeDatos.numeroObjetosEquipo <= 10)
            {
                numeroPosicionesPorPagina = baseDeDatos.numeroObjetosEquipo;
            }
            else
            {
                if(pagina == 0)
                {
                    numeroPosicionesPorPagina = 10;
                }
                else
                {
                    numeroPosicionesPorPagina = baseDeDatos.numeroObjetosEquipo % (pagina * 10);
                }
            }

        }
        else if (categoria == 2)
        {
            if (baseDeDatos.numeroObjetosAtaques <= 10)
            {
                numeroPosicionesPorPagina = baseDeDatos.numeroObjetosAtaques;
            }
            else
            {
                if (pagina == 0)
                {
                    numeroPosicionesPorPagina = 10;
                }
                else
                {
                    numeroPosicionesPorPagina = baseDeDatos.numeroObjetosAtaques % (pagina * 10);
                }
            }
        }
        else
        {
            if (baseDeDatos.numeroObjetosClave <= 10)
            {
                numeroPosicionesPorPagina = baseDeDatos.numeroObjetosClave;
            }
            else
            {
                if (pagina == 0)
                {
                    numeroPosicionesPorPagina = 10;
                }
                else
                {
                    numeroPosicionesPorPagina = baseDeDatos.numeroObjetosClave % (pagina * 10);
                }
            }
        }
    }



    public void IniciarMenu(bool enCombate)
    {
        DesactivaSeleccion();
        DesactivaSeleccionAtq();
        DesactivarConfirmacion();
        DesactivaTexto();
        OcultaAtaques();

        if(baseDeDatos.idioma == 1)
        {
            this.transform.GetChild(8).GetChild(1).GetComponent<Text>().text = "Select";
            this.transform.GetChild(8).GetChild(3).GetComponent<Text>().text = "Back";
            this.transform.GetChild(8).GetChild(5).GetComponent<Text>().text = "Move";
        }
        else
        {
            this.transform.GetChild(8).GetChild(1).GetComponent<Text>().text = "Seleccionar";
            this.transform.GetChild(8).GetChild(3).GetComponent<Text>().text = "Volver";
            this.transform.GetChild(8).GetChild(5).GetComponent<Text>().text = "Mover";
        }

        if (baseDeDatos.mandoActivo)
        {
            this.transform.GetChild(8).GetChild(0).GetComponent<Image>().sprite = baseDeDatos.seleccionXBOX[0];
            this.transform.GetChild(8).GetChild(2).GetComponent<Image>().sprite = baseDeDatos.volverXBOX[0];
            this.transform.GetChild(8).GetChild(4).GetComponent<Image>().sprite = baseDeDatos.moverXBOX[0];
        }
        else
        {
            this.transform.GetChild(8).GetChild(0).GetComponent<Image>().sprite = baseDeDatos.seleccionPC[0];
            this.transform.GetChild(8).GetChild(2).GetComponent<Image>().sprite = baseDeDatos.volverPC[0];
            this.transform.GetChild(8).GetChild(4).GetComponent<Image>().sprite = baseDeDatos.moverPC[0];
        }

        activo = true;
        realizaAccion = false;
        combate = enCombate;
        this.gameObject.SetActive(true);
        categoria = 0;
        pos = 0;
        MueveCategoria();
        MueveFlecha();
        ActualizaListaObjetos();
        
        CalculaPosicionesPagina(0);

        objetoClave = false;
    }



    void CierraMenu()
    {
        activo = false;
        this.gameObject.SetActive(activo);
    }



    void ActualizaDescripcionObjeto()
    {
        int posicionActual = pos + pagina * 10;

        if (categoria == 0)
        {
            if(baseDeDatos.cantidadesObjetosConsumibles[posicionActual] == 0)
            {
                descripcion.transform.GetChild(0).GetComponent<Text>().text = "";
                imagenObjeto.transform.GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                imagenObjeto.transform.GetChild(0).gameObject.SetActive(true);

                if(baseDeDatos.idioma == 0)
                {
                    descripcion.transform.GetChild(0).GetComponent<Text>().text = baseDeDatos.objetosConsumibles[posicionActual].descripcion;
                }
                else if(baseDeDatos.idioma == 1)
                {
                    descripcion.transform.GetChild(0).GetComponent<Text>().text = baseDeDatos.objetosConsumibles[posicionActual].descripcionIngles;
                }
                
                imagenObjeto.transform.GetChild(0).GetComponent<Image>().sprite = baseDeDatos.objetosConsumibles[posicionActual].imagen;
            }
        }
        else if (categoria == 1)
        {
            if (baseDeDatos.cantidadesObjetosEquipo[posicionActual] == 0)
            {
                descripcion.transform.GetChild(0).GetComponent<Text>().text = "";
                imagenObjeto.transform.GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                imagenObjeto.transform.GetChild(0).gameObject.SetActive(true);

                if (baseDeDatos.idioma == 0)
                {
                    descripcion.transform.GetChild(0).GetComponent<Text>().text = baseDeDatos.objetosEquipo[posicionActual].descripcion;
                }
                else if (baseDeDatos.idioma == 1)
                {
                    descripcion.transform.GetChild(0).GetComponent<Text>().text = baseDeDatos.objetosEquipo[posicionActual].descripcionIngles;
                }

                imagenObjeto.transform.GetChild(0).GetComponent<Image>().sprite = baseDeDatos.objetosEquipo[posicionActual].imagen;
            }
            
        }
        else if (categoria == 2)
        {
            if (baseDeDatos.cantidadesObjetosAtaques[posicionActual] == 0)
            {
                descripcion.transform.GetChild(0).GetComponent<Text>().text = "";
                imagenObjeto.transform.GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                imagenObjeto.transform.GetChild(0).gameObject.SetActive(true);

                if (baseDeDatos.idioma == 0)
                {
                    if(baseDeDatos.listaAtaques[baseDeDatos.objetosAtaques[posicionActual].indiceAtq].tipo == Ataque.tipoAtaque.FISICO || baseDeDatos.listaAtaques[baseDeDatos.objetosAtaques[posicionActual].indiceAtq].tipo == Ataque.tipoAtaque.MAGICO)
                    {
                        descripcion.transform.GetChild(0).GetComponent<Text>().text = baseDeDatos.listaAtaques[baseDeDatos.objetosAtaques[posicionActual].indiceAtq].nombre + ": " + baseDeDatos.listaAtaques[baseDeDatos.objetosAtaques[posicionActual].indiceAtq].descripcion + ". \n\n Potencia: " + baseDeDatos.listaAtaques[baseDeDatos.objetosAtaques[posicionActual].indiceAtq].potencia + "   Precision: " + baseDeDatos.listaAtaques[baseDeDatos.objetosAtaques[posicionActual].indiceAtq].precision + "\n Elemento: " + baseDeDatos.listaAtaques[baseDeDatos.objetosAtaques[posicionActual].indiceAtq].elemento + "  " + baseDeDatos.listaAtaques[baseDeDatos.objetosAtaques[posicionActual].indiceAtq].tipo;
                    }
                    else
                    {
                        descripcion.transform.GetChild(0).GetComponent<Text>().text = baseDeDatos.listaAtaques[baseDeDatos.objetosAtaques[posicionActual].indiceAtq].nombre + ": " + baseDeDatos.listaAtaques[baseDeDatos.objetosAtaques[posicionActual].indiceAtq].descripcion + "\n\n APOYO";
                    }
                }
                else if (baseDeDatos.idioma == 1)
                {
                    if (baseDeDatos.listaAtaques[baseDeDatos.objetosAtaques[posicionActual].indiceAtq].tipo == Ataque.tipoAtaque.FISICO || baseDeDatos.listaAtaques[baseDeDatos.objetosAtaques[posicionActual].indiceAtq].tipo == Ataque.tipoAtaque.MAGICO)
                    {
                        descripcion.transform.GetChild(0).GetComponent<Text>().text = baseDeDatos.listaAtaques[baseDeDatos.objetosAtaques[posicionActual].indiceAtq].nombreIngles + ": " + baseDeDatos.listaAtaques[baseDeDatos.objetosAtaques[posicionActual].indiceAtq].descripcionIngles + ". \n\n Power: " + baseDeDatos.listaAtaques[baseDeDatos.objetosAtaques[posicionActual].indiceAtq].potencia + "   Precision: " + baseDeDatos.listaAtaques[baseDeDatos.objetosAtaques[posicionActual].indiceAtq].precision + "\n Element: " + baseDeDatos.listaAtaques[baseDeDatos.objetosAtaques[posicionActual].indiceAtq].elementoIngles + "  " + baseDeDatos.listaAtaques[baseDeDatos.objetosAtaques[posicionActual].indiceAtq].tipoIngles;
                    }
                    else
                    {
                        descripcion.transform.GetChild(0).GetComponent<Text>().text = baseDeDatos.listaAtaques[baseDeDatos.objetosAtaques[posicionActual].indiceAtq].nombreIngles + ": " + baseDeDatos.listaAtaques[baseDeDatos.objetosAtaques[posicionActual].indiceAtq].descripcionIngles + "\n\n SUPPORT";
                    }
                }

                imagenObjeto.transform.GetChild(0).GetComponent<Image>().sprite = baseDeDatos.objetosAtaques[posicionActual].imagen;
            }
            
        }
        else
        {
            if (baseDeDatos.cantidadObjetosClave[posicionActual] == 0)
            {
                descripcion.transform.GetChild(0).GetComponent<Text>().text = "";
                imagenObjeto.transform.GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                imagenObjeto.transform.GetChild(0).gameObject.SetActive(true);

                if (baseDeDatos.idioma == 0)
                {
                    descripcion.transform.GetChild(0).GetComponent<Text>().text = baseDeDatos.listaObjetosClave[posicionActual].descripcion;
                }
                else if (baseDeDatos.idioma == 1)
                {
                    descripcion.transform.GetChild(0).GetComponent<Text>().text = baseDeDatos.listaObjetosClave[posicionActual].descripcionIngles;
                }

                imagenObjeto.transform.GetChild(0).GetComponent<Image>().sprite = baseDeDatos.listaObjetosClave[posicionActual].imagen;
            }
        }
    }



    void ActualizaListaObjetos()
    {
        int auxPos = 0;

        if (categoria == 0)
        {
            if(baseDeDatos.numeroObjetosConsumibles != 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    int j = (pagina * 10) + i;

                    if (j < baseDeDatos.numeroObjetosConsumibles)
                    {
                        if(baseDeDatos.idioma == 1)
                        {
                            listaObjetos.transform.GetChild(0).transform.GetChild(auxPos).GetComponent<Text>().text = baseDeDatos.objetosConsumibles[j].nombreIngles;
                        }
                        else
                        {
                            listaObjetos.transform.GetChild(0).transform.GetChild(auxPos).GetComponent<Text>().text = baseDeDatos.objetosConsumibles[j].nombre;
                        }

                        listaObjetos.transform.GetChild(1).transform.GetChild(auxPos).GetComponent<Text>().text = "" + baseDeDatos.cantidadesObjetosConsumibles[j];
                    }
                    else
                    {
                        listaObjetos.transform.GetChild(0).transform.GetChild(auxPos).GetComponent<Text>().text = "------------";
                        listaObjetos.transform.GetChild(1).transform.GetChild(auxPos).GetComponent<Text>().text = "";
                    }


                    auxPos++;
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    listaObjetos.transform.GetChild(0).transform.GetChild(auxPos).GetComponent<Text>().text = "------------";
                    listaObjetos.transform.GetChild(1).transform.GetChild(auxPos).GetComponent<Text>().text = "";
                    auxPos++;
                }
            }
        }
        else if (categoria == 1)
        {
            if (baseDeDatos.numeroObjetosEquipo != 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    int j = (pagina * 10) + i;

                    if (j < baseDeDatos.numeroObjetosEquipo)
                    {
                        if (baseDeDatos.idioma == 1)
                        {
                            listaObjetos.transform.GetChild(0).transform.GetChild(auxPos).GetComponent<Text>().text = baseDeDatos.objetosEquipo[j].nombreIngles;
                        }
                        else
                        {
                            listaObjetos.transform.GetChild(0).transform.GetChild(auxPos).GetComponent<Text>().text = baseDeDatos.objetosEquipo[j].nombre;
                        }
                        
                        listaObjetos.transform.GetChild(1).transform.GetChild(auxPos).GetComponent<Text>().text = "" + baseDeDatos.cantidadesObjetosEquipo[j];
                    }
                    else
                    {
                        listaObjetos.transform.GetChild(0).transform.GetChild(auxPos).GetComponent<Text>().text = "------------";
                        listaObjetos.transform.GetChild(1).transform.GetChild(auxPos).GetComponent<Text>().text = "";
                    }

                    auxPos++;
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    listaObjetos.transform.GetChild(0).transform.GetChild(auxPos).GetComponent<Text>().text = "------------";
                    listaObjetos.transform.GetChild(1).transform.GetChild(auxPos).GetComponent<Text>().text = "";
                    auxPos++;
                }
            }
        }
        else if (categoria == 2)
        {
            if (baseDeDatos.numeroObjetosAtaques != 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    int j = (pagina * 10) + i;

                    if (j < baseDeDatos.numeroObjetosAtaques)
                    {
                        if (baseDeDatos.idioma == 1)
                        {
                            listaObjetos.transform.GetChild(0).transform.GetChild(auxPos).GetComponent<Text>().text = baseDeDatos.objetosAtaques[i].nombreIngles;
                        }
                        else
                        {
                            listaObjetos.transform.GetChild(0).transform.GetChild(auxPos).GetComponent<Text>().text = baseDeDatos.objetosAtaques[i].nombre;
                        }
                        
                        listaObjetos.transform.GetChild(1).transform.GetChild(auxPos).GetComponent<Text>().text = "" + baseDeDatos.cantidadesObjetosAtaques[i];
                    }
                    else
                    {
                        listaObjetos.transform.GetChild(0).transform.GetChild(auxPos).GetComponent<Text>().text = "------------";
                        listaObjetos.transform.GetChild(1).transform.GetChild(auxPos).GetComponent<Text>().text = "";
                    }

                    auxPos++;
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    listaObjetos.transform.GetChild(0).transform.GetChild(auxPos).GetComponent<Text>().text = "------------";
                    listaObjetos.transform.GetChild(1).transform.GetChild(auxPos).GetComponent<Text>().text = "";
                    auxPos++;
                }
            }
        }
        else if (categoria == 3)
        {
            if (baseDeDatos.numeroObjetosClave != 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    int j = (pagina * 10) + i;

                    if (j < baseDeDatos.numeroObjetosClave)
                    {
                        if (baseDeDatos.idioma == 1)
                        {
                            listaObjetos.transform.GetChild(0).transform.GetChild(auxPos).GetComponent<Text>().text = baseDeDatos.listaObjetosClave[i].nombreIngles;
                        }
                        else
                        {
                            listaObjetos.transform.GetChild(0).transform.GetChild(auxPos).GetComponent<Text>().text = baseDeDatos.listaObjetosClave[i].nombre;
                        }
                        
                        listaObjetos.transform.GetChild(1).transform.GetChild(auxPos).GetComponent<Text>().text = "" + baseDeDatos.cantidadObjetosClave[i];
                    }
                    else
                    {
                        listaObjetos.transform.GetChild(0).transform.GetChild(auxPos).GetComponent<Text>().text = "------------";
                        listaObjetos.transform.GetChild(1).transform.GetChild(auxPos).GetComponent<Text>().text = "";
                    }

                    auxPos++;
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    listaObjetos.transform.GetChild(0).transform.GetChild(auxPos).GetComponent<Text>().text = "------------";
                    listaObjetos.transform.GetChild(1).transform.GetChild(auxPos).GetComponent<Text>().text = "";
                    auxPos++;
                }
            }
        }
    }



    void IniciarConfirmacion()
    {
        this.transform.GetChild(5).gameObject.SetActive(true);

        if(baseDeDatos.idioma == 1)
        {
            this.transform.GetChild(5).GetChild(0).GetComponent<Text>().text = "Use";
            this.transform.GetChild(5).GetChild(1).GetComponent<Text>().text = "Back";
        }
        else
        {
            this.transform.GetChild(5).GetChild(0).GetComponent<Text>().text = "Usar";
            this.transform.GetChild(5).GetChild(1).GetComponent<Text>().text = "Volver";
        }

        confirmacionActiva = true;
        posConf = 0;
        this.transform.GetChild(5).transform.GetChild(4).transform.position = this.transform.GetChild(5).transform.GetChild(2).transform.position;
    }



    void DesactivarConfirmacion()
    {
        this.transform.GetChild(5).gameObject.SetActive(false);
        confirmacionActiva = false;
    }



    void ActivaSeleccion()
    {
        miembrosEquipo = baseDeDatos.numeroIntegrantesEquipo;

        this.transform.GetChild(6).gameObject.SetActive(true);
        seleccionActiva = true;
        DesactivarConfirmacion();

        for(int i = 0; i < 3; i++)
        {
            if(i < miembrosEquipo)
            {
                this.transform.GetChild(6).transform.GetChild(i).gameObject.SetActive(true);
                this.transform.GetChild(6).transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().sprite = baseDeDatos.equipoAliado[i].imagen;

                if (baseDeDatos.idioma == 0)
                {
                    this.transform.GetChild(6).transform.GetChild(i).transform.GetChild(1).GetComponent<Text>().text = baseDeDatos.equipoAliado[i].nombreIngles;

                    this.transform.GetChild(6).transform.GetChild(i).transform.GetChild(2).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[i].estado;
                    this.transform.GetChild(6).transform.GetChild(i).transform.GetChild(3).GetComponent<Text>().text = "LP: " + baseDeDatos.equipoAliado[i].vidaActual + "/" + baseDeDatos.equipoAliado[i].vidaModificada;
                }
                else if(baseDeDatos.idioma == 1)
                {
                    this.transform.GetChild(6).transform.GetChild(i).transform.GetChild(1).GetComponent<Text>().text = baseDeDatos.equipoAliado[i].nombre;

                    this.transform.GetChild(6).transform.GetChild(i).transform.GetChild(2).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[i].estadoIngles;
                    this.transform.GetChild(6).transform.GetChild(i).transform.GetChild(3).GetComponent<Text>().text = "PS: " + baseDeDatos.equipoAliado[i].vidaActual + "/" + baseDeDatos.equipoAliado[i].vidaModificada;
                }
            }
            else
            {
                this.transform.GetChild(6).transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        posSelec = 0;
        this.transform.GetChild(6).transform.GetChild(3).transform.position = this.transform.GetChild(6).transform.GetChild(posSelec).transform.GetChild(5).transform.position;
    }



    void DesactivaSeleccion()
    {
        this.transform.GetChild(6).gameObject.SetActive(false);
        seleccionActiva = false;
    }



    void ActivaSeleccionAtq()
    {
        miembrosEquipo = baseDeDatos.numeroIntegrantesEquipo;

        this.transform.GetChild(7).gameObject.SetActive(true);
        seleccionApAtqActiva = true;
        DesactivarConfirmacion();
        OcultaAtaques();

        for (int i = 0; i < 3; i++)
        {
            if (i < miembrosEquipo)
            {
                this.transform.GetChild(7).transform.GetChild(i).gameObject.SetActive(true);
                this.transform.GetChild(7).transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().sprite = baseDeDatos.equipoAliado[i].imagen;

                if(baseDeDatos.idioma == 1)
                {
                    this.transform.GetChild(7).transform.GetChild(i).transform.GetChild(1).GetComponent<Text>().text = baseDeDatos.equipoAliado[i].nombreIngles;
                    this.transform.GetChild(7).transform.GetChild(i).transform.GetChild(2).GetComponent<Text>().text = "Phy. Atck.: " + baseDeDatos.equipoAliado[i].ataqueFisicoModificado;
                    this.transform.GetChild(7).transform.GetChild(i).transform.GetChild(3).GetComponent<Text>().text = "Mag. Atck.: " + baseDeDatos.equipoAliado[i].ataqueMagicoModificado;
                    this.transform.GetChild(7).transform.GetChild(i).transform.GetChild(4).GetComponent<Text>().text = "Phy. Def.: " + baseDeDatos.equipoAliado[i].defensaFisicaModificada;
                    this.transform.GetChild(7).transform.GetChild(i).transform.GetChild(5).GetComponent<Text>().text = "Mag. Def.: " + baseDeDatos.equipoAliado[i].defensaMagicaModificada;
                }
                else
                {
                    this.transform.GetChild(7).transform.GetChild(i).transform.GetChild(1).GetComponent<Text>().text = baseDeDatos.equipoAliado[i].nombre;
                    this.transform.GetChild(7).transform.GetChild(i).transform.GetChild(2).GetComponent<Text>().text = "Atq. Fis.: " + baseDeDatos.equipoAliado[i].ataqueFisicoModificado;
                    this.transform.GetChild(7).transform.GetChild(i).transform.GetChild(3).GetComponent<Text>().text = "Atq. Mag.: " + baseDeDatos.equipoAliado[i].ataqueMagicoModificado;
                    this.transform.GetChild(7).transform.GetChild(i).transform.GetChild(4).GetComponent<Text>().text = "Def. Fis.: " + baseDeDatos.equipoAliado[i].defensaFisicaModificada;
                    this.transform.GetChild(7).transform.GetChild(i).transform.GetChild(5).GetComponent<Text>().text = "Def. Mag.: " + baseDeDatos.equipoAliado[i].defensaMagicaModificada;
                }

                int posicionActual = pos + pagina * 10;
                int indiceAtaque = baseDeDatos.objetosAtaques[posicionActual].indiceAtq;
                bool tieneAtaque = false;

                for (int j = 0; j < baseDeDatos.equipoAliado[i].numeroAtaques; j++)
                {
                    if (baseDeDatos.equipoAliado[i].indicesAtaque[j] == indiceAtaque)
                    {
                        tieneAtaque = true;
                    }
                }

                if (!tieneAtaque)
                {
                    if (baseDeDatos.equipoAliado[i].elemento == Personajes.elementoPersonaje.NEUTRO)
                    {
                        if(baseDeDatos.idioma == 1)
                        {
                            this.transform.GetChild(7).transform.GetChild(i).transform.GetChild(8).GetComponent<Text>().text = "Can";
                        }
                        else
                        {
                            this.transform.GetChild(7).transform.GetChild(i).transform.GetChild(8).GetComponent<Text>().text = "Apto";
                        }
                        
                        puedeAprender[i] = true;
                    }
                    else 
                    {
                        if (baseDeDatos.listaAtaques[indiceAtaque].elemento == Ataque.elementoAtaque.APOYO)
                        {
                            if (baseDeDatos.idioma == 1)
                            {
                                this.transform.GetChild(7).transform.GetChild(i).transform.GetChild(8).GetComponent<Text>().text = "Can";
                            }
                            else
                            {
                                this.transform.GetChild(7).transform.GetChild(i).transform.GetChild(8).GetComponent<Text>().text = "Apto";
                            }
                            
                            puedeAprender[i] = true;
                        }
                        else if (baseDeDatos.listaAtaques[indiceAtaque].elemento == Ataque.elementoAtaque.DORMILON)
                        {
                            if(baseDeDatos.equipoAliado[i].elemento == Personajes.elementoPersonaje.DORMILON)
                            {
                                if (baseDeDatos.idioma == 1)
                                {
                                    this.transform.GetChild(7).transform.GetChild(i).transform.GetChild(8).GetComponent<Text>().text = "Can";
                                }
                                else
                                {
                                    this.transform.GetChild(7).transform.GetChild(i).transform.GetChild(8).GetComponent<Text>().text = "Apto";
                                }
                               
                                puedeAprender[i] = true;
                            }
                            else
                            {
                                if (baseDeDatos.idioma == 1)
                                {
                                    this.transform.GetChild(7).transform.GetChild(i).transform.GetChild(8).GetComponent<Text>().text = "Can't";
                                }
                                else
                                {
                                    this.transform.GetChild(7).transform.GetChild(i).transform.GetChild(8).GetComponent<Text>().text = "No Apto";
                                }
                               
                                puedeAprender[i] = false;
                            }
                        }
                        else if (baseDeDatos.listaAtaques[indiceAtaque].elemento == Ataque.elementoAtaque.FIESTERO)
                        {
                            if (baseDeDatos.equipoAliado[i].elemento == Personajes.elementoPersonaje.FIESTERO)
                            {
                                if (baseDeDatos.idioma == 1)
                                {
                                    this.transform.GetChild(7).transform.GetChild(i).transform.GetChild(8).GetComponent<Text>().text = "Can";
                                }
                                else
                                {
                                    this.transform.GetChild(7).transform.GetChild(i).transform.GetChild(8).GetComponent<Text>().text = "Apto";
                                }
                                
                                puedeAprender[i] = true;
                            }
                            else
                            {
                                if (baseDeDatos.idioma == 1)
                                {
                                    this.transform.GetChild(7).transform.GetChild(i).transform.GetChild(8).GetComponent<Text>().text = "Can't";
                                }
                                else
                                {
                                    this.transform.GetChild(7).transform.GetChild(i).transform.GetChild(8).GetComponent<Text>().text = "No Apto";
                                }
                                
                                puedeAprender[i] = false;
                            }
                        }
                        else if (baseDeDatos.listaAtaques[indiceAtaque].elemento == Ataque.elementoAtaque.FRIKI)
                        {
                            if (baseDeDatos.equipoAliado[i].elemento == Personajes.elementoPersonaje.FRIKI)
                            {
                                if (baseDeDatos.idioma == 1)
                                {
                                    this.transform.GetChild(7).transform.GetChild(i).transform.GetChild(8).GetComponent<Text>().text = "Can";
                                }
                                else
                                {
                                    this.transform.GetChild(7).transform.GetChild(i).transform.GetChild(8).GetComponent<Text>().text = "Apto";
                                }
                                
                                puedeAprender[i] = true;
                            }
                            else
                            {
                                if (baseDeDatos.idioma == 1)
                                {
                                    this.transform.GetChild(7).transform.GetChild(i).transform.GetChild(8).GetComponent<Text>().text = "Can't";
                                }
                                else
                                {
                                    this.transform.GetChild(7).transform.GetChild(i).transform.GetChild(8).GetComponent<Text>().text = "No Apto";
                                }
                                
                                puedeAprender[i] = false;
                            }
                        }
                        else if (baseDeDatos.listaAtaques[indiceAtaque].elemento == Ataque.elementoAtaque.RESPONSABLE)
                        {
                            if (baseDeDatos.equipoAliado[i].elemento == Personajes.elementoPersonaje.RESPONSABLE)
                            {
                                if (baseDeDatos.idioma == 1)
                                {
                                    this.transform.GetChild(7).transform.GetChild(i).transform.GetChild(8).GetComponent<Text>().text = "Can";
                                }
                                else
                                {
                                    this.transform.GetChild(7).transform.GetChild(i).transform.GetChild(8).GetComponent<Text>().text = "Apto";
                                }
                                
                                puedeAprender[i] = true;
                            }
                            else
                            {
                                if (baseDeDatos.idioma == 1)
                                {
                                    this.transform.GetChild(7).transform.GetChild(i).transform.GetChild(8).GetComponent<Text>().text = "Can't";
                                }
                                else
                                {
                                    this.transform.GetChild(7).transform.GetChild(i).transform.GetChild(8).GetComponent<Text>().text = "No Apto";
                                }
                               
                                puedeAprender[i] = false;
                            }
                        }
                        else if (baseDeDatos.listaAtaques[indiceAtaque].elemento == Ataque.elementoAtaque.TIRANO)
                        {
                            if (baseDeDatos.equipoAliado[i].elemento == Personajes.elementoPersonaje.TIRANO)
                            {
                                if (baseDeDatos.idioma == 1)
                                {
                                    this.transform.GetChild(7).transform.GetChild(i).transform.GetChild(8).GetComponent<Text>().text = "Can";
                                }
                                else
                                {
                                    this.transform.GetChild(7).transform.GetChild(i).transform.GetChild(8).GetComponent<Text>().text = "Apto";
                                }
                                
                                puedeAprender[i] = true;
                            }
                            else
                            {
                                if (baseDeDatos.idioma == 1)
                                {
                                    this.transform.GetChild(7).transform.GetChild(i).transform.GetChild(8).GetComponent<Text>().text = "Can't";
                                }
                                else
                                {
                                    this.transform.GetChild(7).transform.GetChild(i).transform.GetChild(8).GetComponent<Text>().text = "No Apto";
                                }
                                
                                puedeAprender[i] = false;
                            }
                        }
                        else if (baseDeDatos.listaAtaques[indiceAtaque].elemento == Ataque.elementoAtaque.NEUTRO)
                        {
                            if (baseDeDatos.idioma == 1)
                            {
                                this.transform.GetChild(7).transform.GetChild(i).transform.GetChild(8).GetComponent<Text>().text = "Can";
                            }
                            else
                            {
                                this.transform.GetChild(7).transform.GetChild(i).transform.GetChild(8).GetComponent<Text>().text = "Apto";
                            }

                            puedeAprender[i] = true;
                        }
                    }
                }
                else
                {
                    if (baseDeDatos.idioma == 1)
                    {
                        this.transform.GetChild(7).transform.GetChild(i).transform.GetChild(8).GetComponent<Text>().text = "Can't";
                    }
                    else
                    {
                        this.transform.GetChild(7).transform.GetChild(i).transform.GetChild(8).GetComponent<Text>().text = "No Apto";
                    }
                    
                    puedeAprender[i] = false;
                }
            }
            else
            {
                this.transform.GetChild(7).transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        this.transform.GetChild(7).GetChild(3).transform.position = this.transform.GetChild(7).GetChild(0).GetChild(7).transform.position;
        posSelec = 0;
    }



    void DesactivaSeleccionAtq()
    {
        this.transform.GetChild(7).gameObject.SetActive(false);
        seleccionApAtqActiva = false;
    }



    public void AplicaObjetoCombate()
    {
        int posicionActual = pos + pagina * 10;

        if (categoria == 0)
        {
            if (baseDeDatos.objetosConsumibles[posicionActual].tipo == Objeto.tipoObjeto.CURACION)
            {
                if (baseDeDatos.equipoAliado[posSelec].vidaActual != baseDeDatos.equipoAliado[posSelec].vidaModificada && baseDeDatos.equipoAliado[posSelec].vidaActual != 0)
                {
                    baseDeDatos.QuitarDeInventario(posicionActual, 1, 0);

                    baseDeDatos.equipoAliado[posSelec].vidaActual += baseDeDatos.objetosConsumibles[posicionActual].aumentoVida;

                    if (baseDeDatos.equipoAliado[posSelec].vidaActual > baseDeDatos.equipoAliado[posSelec].vidaModificada)
                    {
                        baseDeDatos.equipoAliado[posSelec].vidaActual = baseDeDatos.equipoAliado[posSelec].vidaModificada;
                    }

                    if(baseDeDatos.idioma == 1)
                    {
                        this.transform.GetChild(6).transform.GetChild(posSelec).transform.GetChild(3).GetComponent<Text>().text = "LP: " + baseDeDatos.equipoAliado[posSelec].vidaActual + "/" + baseDeDatos.equipoAliado[posSelec].vidaModificada;
                    }
                    else
                    {
                        this.transform.GetChild(6).transform.GetChild(posSelec).transform.GetChild(3).GetComponent<Text>().text = "PS: " + baseDeDatos.equipoAliado[posSelec].vidaActual + "/" + baseDeDatos.equipoAliado[posSelec].vidaModificada;
                    }

                    ActivaTextoCombate();
                }
                else
                {
                    ActivaTexto();
                }
            }
            else if (baseDeDatos.objetosConsumibles[posicionActual].tipo == Objeto.tipoObjeto.REVIVIR)
            {
                /*
                if (baseDeDatos.equipoAliado[posSelec].vidaActual == 0)
                {
                    baseDeDatos.QuitarDeInventario(baseDeDatos.objetosConsumibles[posicionActual].indice, 1, 0);

                    if (baseDeDatos.objetosConsumibles[posicionActual].aumentoVida == 1)
                    {
                        baseDeDatos.equipoAliado[posSelec].vidaActual += (int)(baseDeDatos.equipoAliado[posSelec].vidaModificada / 2);
                        baseDeDatos.equipoAliado[posSelec].estado = Personajes.estadoPersonaje.SANO;
                    }

                    if(baseDeDatos.idioma == 1)
                    {
                        this.transform.GetChild(6).transform.GetChild(posSelec).transform.GetChild(3).GetComponent<Text>().text = "LP: " + baseDeDatos.equipoAliado[posSelec].vidaActual + "/" + baseDeDatos.equipoAliado[posSelec].vidaModificada;
                        this.transform.GetChild(6).transform.GetChild(posSelec).transform.GetChild(2).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posSelec].estadoIngles;
                    }
                    else
                    {
                        this.transform.GetChild(6).transform.GetChild(posSelec).transform.GetChild(3).GetComponent<Text>().text = "PS: " + baseDeDatos.equipoAliado[posSelec].vidaActual + "/" + baseDeDatos.equipoAliado[posSelec].vidaModificada;
                        this.transform.GetChild(6).transform.GetChild(posSelec).transform.GetChild(2).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posSelec].estado;
                    }

                    ActivaTextoCombate();
                }
                else
                {
                    ActivaTexto();
                }
                */
                ActivaTexto();
            }
        }
        else
        {
            ActivaTexto();
        }
    }



    void AplicaObjeto()
    {
        int posicionActual = pos + pagina * 10;

        if (categoria == 0)
        {
            if (baseDeDatos.objetosConsumibles[posicionActual].tipo == Objeto.tipoObjeto.CURACION)
            {
                if(baseDeDatos.equipoAliado[posSelec].vidaActual != baseDeDatos.equipoAliado[posSelec].vidaModificada && baseDeDatos.equipoAliado[posSelec].vidaActual != 0)
                {
                    baseDeDatos.QuitarDeInventario(posicionActual, 1, 0);

                    baseDeDatos.equipoAliado[posSelec].vidaActual += baseDeDatos.objetosConsumibles[posicionActual].aumentoVida;

                    if (baseDeDatos.equipoAliado[posSelec].vidaActual > baseDeDatos.equipoAliado[posSelec].vidaModificada)
                    {
                        baseDeDatos.equipoAliado[posSelec].vidaActual = baseDeDatos.equipoAliado[posSelec].vidaModificada;
                    }

                    if(baseDeDatos.idioma == 1)
                    {
                        this.transform.GetChild(6).transform.GetChild(posSelec).transform.GetChild(3).GetComponent<Text>().text = "LP: " + baseDeDatos.equipoAliado[posSelec].vidaActual + "/" + baseDeDatos.equipoAliado[posSelec].vidaModificada;
                    }
                    else
                    {
                        this.transform.GetChild(6).transform.GetChild(posSelec).transform.GetChild(3).GetComponent<Text>().text = "PS: " + baseDeDatos.equipoAliado[posSelec].vidaActual + "/" + baseDeDatos.equipoAliado[posSelec].vidaModificada;
                    }

                    ActualizaListaObjetos();
                }
                else
                {
                    ActivaTexto();
                }
            }
            else if(baseDeDatos.objetosConsumibles[posicionActual].tipo == Objeto.tipoObjeto.REVIVIR)
            {
                if(baseDeDatos.equipoAliado[posSelec].vidaActual == 0)
                {
                    baseDeDatos.QuitarDeInventario(posicionActual, 1, 0);

                    if(baseDeDatos.objetosConsumibles[posicionActual].aumentoVida == 1)
                    {
                        baseDeDatos.equipoAliado[posSelec].vidaActual += (int)(baseDeDatos.equipoAliado[posSelec].vidaModificada / 2);
                        baseDeDatos.equipoAliado[posSelec].estado = Personajes.estadoPersonaje.SANO;
                    }

                    if(baseDeDatos.idioma == 1)
                    {
                        this.transform.GetChild(6).transform.GetChild(posSelec).transform.GetChild(3).GetComponent<Text>().text = "LP: " + baseDeDatos.equipoAliado[posSelec].vidaActual + "/" + baseDeDatos.equipoAliado[posSelec].vidaModificada;
                        this.transform.GetChild(6).transform.GetChild(posSelec).transform.GetChild(2).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posSelec].estadoIngles;
                    }
                    else
                    {
                        this.transform.GetChild(6).transform.GetChild(posSelec).transform.GetChild(3).GetComponent<Text>().text = "PS: " + baseDeDatos.equipoAliado[posSelec].vidaActual + "/" + baseDeDatos.equipoAliado[posSelec].vidaModificada;
                        this.transform.GetChild(6).transform.GetChild(posSelec).transform.GetChild(2).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posSelec].estado;
                    }

                    ActualizaListaObjetos();
                }
                else
                {
                    ActivaTexto();
                }
            }
        }
        else
        {
            if (puedeAprender[posSelec])
            {
                if (baseDeDatos.equipoAliado[posSelec].numeroAtaques != 4)
                {
                    AprendeAtaque(baseDeDatos.equipoAliado[posSelec].numeroAtaques);
                    DesactivaSeleccionAtq();
                    ActivaTextoCombate();
                }
                else
                {
                    MuestraAtaques();
                }
            }
            else
            {
                ActivaTexto();
            }
        }
    }



    void ActivaTexto()
    {
        textoActivo = true;
        quitaTexto = false;
        this.transform.GetChild(9).gameObject.SetActive(textoActivo);

        if(baseDeDatos.idioma == 0)
        {
            this.transform.GetChild(9).transform.GetChild(0).GetComponent<Text>().text = "Este objeto no se puede usar ahora.";
        }
        else if(baseDeDatos.idioma == 1)
        {
            this.transform.GetChild(9).transform.GetChild(0).GetComponent<Text>().text = "This item cannot be used now.";
        }

        StartCoroutine(EsperaTexto());
    }



    void DesactivaTexto()
    {
        textoActivo = false;
        this.transform.GetChild(9).gameObject.SetActive(textoActivo);
    }



    void ActivaTextoCombate()
    {
        textoCombate = true;
        realizaAccion = true;
        quitaTexto = false;

        this.transform.GetChild(9).gameObject.SetActive(textoCombate);

        int posicionActual = pos + pagina * 10;

        if(baseDeDatos.idioma == 1)
        {
            if (categoria == 0)
            {
                this.transform.GetChild(9).transform.GetChild(0).GetComponent<Text>().text = "You used " + baseDeDatos.objetosConsumibles[posicionActual].nombreIngles + ".";
            }
            else if (categoria == 2)
            {
                textoCombate = false;
                realizaAccion = false;
                textoActivo = true;
                quitaTexto = false;

                this.transform.GetChild(9).transform.GetChild(0).GetComponent<Text>().text = "You learned " + baseDeDatos.objetosAtaques[posicionActual].nombreIngles + ".";
            }
        }
        else
        {
            if (categoria == 0)
            {
                this.transform.GetChild(9).transform.GetChild(0).GetComponent<Text>().text = "Has usado " + baseDeDatos.objetosConsumibles[posicionActual].nombre + ".";
            }
            else if (categoria == 2)
            {
                textoCombate = false;
                realizaAccion = false;
                textoActivo = true;
                quitaTexto = false;

                this.transform.GetChild(9).transform.GetChild(0).GetComponent<Text>().text = "Ha aprendido " + baseDeDatos.objetosAtaques[posicionActual].nombre + ".";
            }
        }

        StartCoroutine(EsperaTexto());
    }



    void DesactivaTextoCombate()
    {
        textoCombate = false;
        this.transform.GetChild(9).gameObject.SetActive(textoCombate);
    }



    IEnumerator EsperaTexto()
    {
        yield return new WaitForSeconds(0.3f);

        quitaTexto = true;
    }



    void MuestraAtaques()
    {
        muestraAtaques = true;
        this.transform.GetChild(7).transform.GetChild(4).gameObject.SetActive(muestraAtaques);
        this.transform.GetChild(7).transform.GetChild(4).transform.GetChild(4).transform.position = this.transform.GetChild(7).transform.GetChild(4).transform.GetChild(0).transform.GetChild(7).transform.position;

        if (baseDeDatos.idioma == 0)
        {
            this.transform.GetChild(7).transform.GetChild(4).transform.GetChild(5).transform.GetChild(0).GetComponent<Text>().text = "¿Qué ataque quieres sustituir?";
        }
        else if(baseDeDatos.idioma == 1)
        {
            this.transform.GetChild(7).transform.GetChild(4).transform.GetChild(5).transform.GetChild(0).GetComponent<Text>().text = "What attack do you want to replace?";
        }

        for (int i = 0; i < 4; i++)
        {
            this.transform.GetChild(7).transform.GetChild(4).transform.GetChild(i).gameObject.SetActive(true);

            if (baseDeDatos.idioma == 0)
            {
                this.transform.GetChild(7).transform.GetChild(4).transform.GetChild(i).transform.GetChild(0).GetComponent<Text>().text = baseDeDatos.listaAtaques[baseDeDatos.equipoAliado[posSelec].indicesAtaque[i]].nombre;
                this.transform.GetChild(7).transform.GetChild(4).transform.GetChild(i).transform.GetChild(1).GetComponent<Text>().text = "Potencia: " + baseDeDatos.listaAtaques[baseDeDatos.equipoAliado[posSelec].indicesAtaque[i]].potencia;
                this.transform.GetChild(7).transform.GetChild(4).transform.GetChild(i).transform.GetChild(2).GetComponent<Text>().text = "Precisión: " + baseDeDatos.listaAtaques[baseDeDatos.equipoAliado[posSelec].indicesAtaque[i]].precision;
                this.transform.GetChild(7).transform.GetChild(4).transform.GetChild(i).transform.GetChild(3).GetComponent<Text>().text = "" + baseDeDatos.listaAtaques[baseDeDatos.equipoAliado[posSelec].indicesAtaque[i]].elemento;
                this.transform.GetChild(7).transform.GetChild(4).transform.GetChild(i).transform.GetChild(4).GetComponent<Text>().text = "ER: " + baseDeDatos.listaAtaques[baseDeDatos.equipoAliado[posSelec].indicesAtaque[i]].energia;

                if (baseDeDatos.listaAtaques[baseDeDatos.equipoAliado[posSelec].indicesAtaque[i]].tipo == Ataque.tipoAtaque.APOYO_MIXTO || baseDeDatos.listaAtaques[baseDeDatos.equipoAliado[posSelec].indicesAtaque[i]].tipo == Ataque.tipoAtaque.APOYO_NEGATIVO || baseDeDatos.listaAtaques[baseDeDatos.equipoAliado[posSelec].indicesAtaque[i]].tipo == Ataque.tipoAtaque.APOYO_POSITIVO)
                {
                    this.transform.GetChild(7).transform.GetChild(4).transform.GetChild(i).transform.GetChild(5).GetComponent<Text>().text = "Apoyo";
                }
                else if (baseDeDatos.listaAtaques[baseDeDatos.equipoAliado[posSelec].indicesAtaque[i]].tipo == Ataque.tipoAtaque.FISICO)
                {
                    this.transform.GetChild(7).transform.GetChild(4).transform.GetChild(i).transform.GetChild(5).GetComponent<Text>().text = "Físico";
                }
                else if (baseDeDatos.listaAtaques[baseDeDatos.equipoAliado[posSelec].indicesAtaque[i]].tipo == Ataque.tipoAtaque.FISICO)
                {
                    this.transform.GetChild(7).transform.GetChild(4).transform.GetChild(i).transform.GetChild(5).GetComponent<Text>().text = "Mágico";
                }
            }
            else if (baseDeDatos.idioma == 1)
            {
                this.transform.GetChild(7).transform.GetChild(4).transform.GetChild(i).transform.GetChild(0).GetComponent<Text>().text = baseDeDatos.listaAtaques[baseDeDatos.equipoAliado[posSelec].indicesAtaque[i]].nombreIngles;
                this.transform.GetChild(7).transform.GetChild(4).transform.GetChild(i).transform.GetChild(1).GetComponent<Text>().text = "Power: " + baseDeDatos.listaAtaques[baseDeDatos.equipoAliado[posSelec].indicesAtaque[i]].potencia;
                this.transform.GetChild(7).transform.GetChild(4).transform.GetChild(i).transform.GetChild(2).GetComponent<Text>().text = "Precision: " + baseDeDatos.listaAtaques[baseDeDatos.equipoAliado[posSelec].indicesAtaque[i]].precision;
                this.transform.GetChild(7).transform.GetChild(4).transform.GetChild(i).transform.GetChild(3).GetComponent<Text>().text = "" + baseDeDatos.listaAtaques[baseDeDatos.equipoAliado[posSelec].indicesAtaque[i]].elementoIngles;
                this.transform.GetChild(7).transform.GetChild(4).transform.GetChild(i).transform.GetChild(4).GetComponent<Text>().text = "ER: " + baseDeDatos.listaAtaques[baseDeDatos.equipoAliado[posSelec].indicesAtaque[i]].energia;

                if (baseDeDatos.listaAtaques[baseDeDatos.equipoAliado[posSelec].indicesAtaque[i]].tipo == Ataque.tipoAtaque.APOYO_MIXTO || baseDeDatos.listaAtaques[baseDeDatos.equipoAliado[posSelec].indicesAtaque[i]].tipo == Ataque.tipoAtaque.APOYO_NEGATIVO || baseDeDatos.listaAtaques[baseDeDatos.equipoAliado[posSelec].indicesAtaque[i]].tipo == Ataque.tipoAtaque.APOYO_POSITIVO)
                {
                    this.transform.GetChild(7).transform.GetChild(4).transform.GetChild(i).transform.GetChild(5).GetComponent<Text>().text = "Support";
                }
                else if (baseDeDatos.listaAtaques[baseDeDatos.equipoAliado[posSelec].indicesAtaque[i]].tipo == Ataque.tipoAtaque.FISICO)
                {
                    this.transform.GetChild(7).transform.GetChild(4).transform.GetChild(i).transform.GetChild(5).GetComponent<Text>().text = "Physical";
                }
                else if (baseDeDatos.listaAtaques[baseDeDatos.equipoAliado[posSelec].indicesAtaque[i]].tipo == Ataque.tipoAtaque.FISICO)
                {
                    this.transform.GetChild(7).transform.GetChild(4).transform.GetChild(i).transform.GetChild(5).GetComponent<Text>().text = "Magical";
                }
            }
        }

        posAtq = 0;
    }



    void OcultaAtaques()
    {
        muestraAtaques = false;
        this.transform.GetChild(7).transform.GetChild(4).gameObject.SetActive(muestraAtaques);
    }



    void AprendeAtaque(int valor)
    {
        int posicionActual = pos + pagina * 10;
        int indiceAtaque = baseDeDatos.objetosAtaques[posicionActual].indiceAtq;

        baseDeDatos.equipoAliado[posSelec].indicesAtaque[valor] = indiceAtaque;

        if (baseDeDatos.equipoAliado[posSelec].numeroAtaques != 4)
        {
            baseDeDatos.equipoAliado[posSelec].numeroAtaques++;
        }

        baseDeDatos.equipoAliado[posSelec].habilidades = new Ataque[baseDeDatos.equipoAliado[posSelec].numeroAtaques];

        for (int i = 0; i < baseDeDatos.equipoAliado[posSelec].numeroAtaques; i++)
        {
            Ataque ataque = new Ataque();
            baseDeDatos.equipoAliado[posSelec].habilidades[i] = ataque.BuscaAtaque(baseDeDatos.equipoAliado[posSelec].indicesAtaque[i]);
        }
    }
}
