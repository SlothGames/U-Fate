using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ajedrez : MonoBehaviour
{
    GameObject seleccion;
    GameObject posiciones;
    GameObject fichasBlancas, fichasNegras;
    GameObject interfazJugador, interfazEnemigo;
    GameObject interfazCombate;
    GameObject ayuda;
    GameObject menuPromocion;

    int[,] matrizTablero;
    int[,] matrizIndicesFichas;
    int indicePromocion;
    int dificultad;
    int posX, posY;
    int posMenu;
    int numeroPeonesBlancos, numeroPeonesNegros;
    int numeroTorresBlancas, numeroTorresNegras;
    int numeroAlfilesBlancos, numeroAlfilessNegros;
    int numeroCaballosBlancos, numeroCaballosNegros;
    int numeroReinasBlancas, numeroReinassNegras;
    int numeroPosicionesMostradas = 0;
    int[] posicionesAux;
    int indiceFichaSeleccionada, indiceFichaObjetivo;

    bool activo;
    bool menuActivo;
    bool mover;
    bool huir;
    bool atacar;
    bool opcionElegida;
    bool enemigoDecideJugada;
    bool resuelveJugadaJugador, resuelveJugadaEnemigo;
    bool turnoJugador;
    bool promociona;
    bool mueve;

    bool pulsado;
    float digitalX;
    float digitalY;

    public FichasAjedrez[] fichasBlan;
    public FichasAjedrez[] fichasNegr;

    BaseDatos baseDeDatos;
    Sprite[] imagenFichas; //0 Peon negro, 1 torre negra, 2 caballo negro, 3 alfil negro, 4 rey negro, 5 reina negre, 6 peon blanco, 7 torre blanca, 8 caballo blanco, 9 alfil blanco, 10 rey blanco, 11 reina blanca
    public Sprite[] imagenesPromocion;//0 activo, 1 desactivado
    public Sprite[] imagenMenu; //0 activo, 1 desactivado
    Sprite[] imagenAliado, imagenEnemigo;

    MusicaManager musica;


    void Start()
    {
        seleccion = this.transform.GetChild(3).gameObject;
        posiciones = this.transform.GetChild(2).gameObject;
        fichasBlancas = this.transform.GetChild(5).gameObject;
        fichasNegras = this.transform.GetChild(4).gameObject;
        interfazJugador = this.transform.GetChild(6).gameObject;
        interfazEnemigo = this.transform.GetChild(7).gameObject;
        interfazCombate = this.transform.GetChild(8).gameObject;
        ayuda = this.transform.GetChild(9).gameObject;
        menuPromocion = this.transform.GetChild(10).gameObject;

        matrizTablero = new int[8, 8]; //0 nada, 1 blanca, 2 negra
        matrizIndicesFichas = new int[8, 8]; //-1 nada, 0-7 peon, 8-9 torre, 12-13 alfil, 10-11 caballo, 14 reina, 15 rey
        baseDeDatos = GameObject.Find("GameManager").GetComponent<BaseDatos>();
        
        fichasBlan = new FichasAjedrez[16];
        fichasNegr = new FichasAjedrez[16];

        imagenFichas = new Sprite[12];
        imagenFichas = Resources.LoadAll<Sprite>("Sprites/Interfaz/Piezas/AjedrezMagico");
        musica = GameObject.Find("EfectosSonido").GetComponent<MusicaManager>();

        ActivaJuego(0);
        CierraJuego();
    }

    

    void Update()
    {
        if (activo)
        {
            digitalX = Input.GetAxis("D-Horizontal");
            digitalY = Input.GetAxis("D-Vertical");

            if (baseDeDatos.mandoActivo)
            {
                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.N) || Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.Escape) ||
                    Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.V) || Input.GetKeyDown(KeyCode.B))
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

            if (pulsado)
            {
                if (digitalY == 0 && digitalX == 0)
                {
                    pulsado = false;
                }
            }

            if (menuActivo)
            {
                if (mueve)
                {
                    mueve = false;
                    interfazCombate.transform.GetChild(posMenu).GetComponent<Image>().sprite = imagenMenu[0];
                }

                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || (!pulsado && digitalY > 0))
                {
                    interfazCombate.transform.GetChild(posMenu).GetComponent<Image>().sprite = imagenMenu[1];

                    pulsado = true;
                    mueve = true;
                    musica.ProduceEfecto(11);

                    posMenu--;

                    if (posMenu < 1)
                    {
                        posMenu = 2;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || (!pulsado && digitalY < 0))
                {
                    pulsado = true;
                    mueve = true;
                    musica.ProduceEfecto(11);
                    interfazCombate.transform.GetChild(posMenu).GetComponent<Image>().sprite = imagenMenu[1];

                    posMenu++;

                    if (posMenu > 2)
                    {
                        posMenu = 1;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                {
                    musica.ProduceEfecto(10);

                    if (posMenu == 1)
                    {
                        mover = true;

                        indiceFichaSeleccionada = matrizIndicesFichas[posX, posY];

                        if (matrizIndicesFichas[posX, posY] >= 0 && matrizIndicesFichas[posX, posY] < 8)
                        {
                            MuestraCasillasPeon();
                        }
                        else if (matrizIndicesFichas[posX, posY] > 7 && matrizIndicesFichas[posX, posY] < 10)
                        {
                            MuestraCasillasTorre();
                        }
                        else if (matrizIndicesFichas[posX, posY] > 9 && matrizIndicesFichas[posX, posY] < 12)
                        {
                            MuestraCasillasCaballo();
                        }
                        else if (matrizIndicesFichas[posX, posY] > 11 && matrizIndicesFichas[posX, posY] < 14)
                        {
                            MuestraCasillasAlfil();
                        }
                        else if (matrizIndicesFichas[posX, posY] == 14)
                        {
                            MuestraCasillaReina();
                        }
                        else
                        {
                            MuestraCasillasRey();
                        }
                    }
                    else
                    {
                        huir = true;
                    }

                    ActivaMenu(false);
                }
                else if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("B"))
                {
                    musica.ProduceEfecto(10);

                    ActivaMenu(false);
                }
            }
            else if (promociona)
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || (!pulsado && digitalY > 0))
                {
                    pulsado = true;
                    
                }
                else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || (!pulsado && digitalY < 0))
                {
                    pulsado = true;
                }
                else if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                {
                    PromocionaFicha();
                    promociona = false;
                }
            }
            else
            {
                if (turnoJugador)
                {
                    if (mover)
                    {
                        if (mueve)
                        {
                            int aux = posY * 8 + posX;
                            seleccion.transform.position = posiciones.transform.GetChild(aux).transform.position;
                            mueve = false;

                            if (matrizTablero[posX, posY] == 1)
                            {
                                fichasBlancas.transform.GetChild(matrizIndicesFichas[posX, posY]).GetChild(0).gameObject.SetActive(true);
                                fichasBlancas.transform.GetChild(matrizIndicesFichas[posX, posY]).GetChild(1).gameObject.SetActive(true);
                            }
                            else if (matrizTablero[posX, posY] == 2)
                            {
                                fichasNegras.transform.GetChild(matrizIndicesFichas[posX, posY]).GetChild(0).gameObject.SetActive(true);
                                fichasNegras.transform.GetChild(matrizIndicesFichas[posX, posY]).GetChild(1).gameObject.SetActive(true);
                            }
                        }

                        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || (!pulsado && digitalY > 0))
                        {
                            pulsado = true;
                            mueve = true;
                            musica.ProduceEfecto(11);

                            if (matrizTablero[posX, posY] == 1)
                            {
                                fichasBlancas.transform.GetChild(matrizIndicesFichas[posX, posY]).GetChild(0).gameObject.SetActive(false);
                                fichasBlancas.transform.GetChild(matrizIndicesFichas[posX, posY]).GetChild(1).gameObject.SetActive(false);
                            }
                            else if (matrizTablero[posX, posY] == 2)
                            {
                                fichasNegras.transform.GetChild(matrizIndicesFichas[posX, posY]).GetChild(0).gameObject.SetActive(false);
                                fichasNegras.transform.GetChild(matrizIndicesFichas[posX, posY]).GetChild(1).gameObject.SetActive(false);
                            }

                            posY++;

                            if (posY > 7)
                            {
                                posY = 0;
                            }
                        }
                        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || (!pulsado && digitalY < 0))
                        {
                            pulsado = true;
                            mueve = true;
                            musica.ProduceEfecto(11);

                            if (matrizTablero[posX, posY] == 1)
                            {
                                fichasBlancas.transform.GetChild(matrizIndicesFichas[posX, posY]).GetChild(0).gameObject.SetActive(false);
                                fichasBlancas.transform.GetChild(matrizIndicesFichas[posX, posY]).GetChild(1).gameObject.SetActive(false);
                            }
                            else if (matrizTablero[posX, posY] == 2)
                            {
                                fichasNegras.transform.GetChild(matrizIndicesFichas[posX, posY]).GetChild(0).gameObject.SetActive(false);
                                fichasNegras.transform.GetChild(matrizIndicesFichas[posX, posY]).GetChild(1).gameObject.SetActive(false);
                            }

                            posY--;

                            if (posY < 0)
                            {
                                posY = 7;
                            }
                        }
                        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || (!pulsado && digitalX < 0))
                        {
                            pulsado = true;
                            mueve = true;
                            musica.ProduceEfecto(11);

                            if (matrizTablero[posX, posY] == 1)
                            {
                                fichasBlancas.transform.GetChild(matrizIndicesFichas[posX, posY]).GetChild(0).gameObject.SetActive(false);
                                fichasBlancas.transform.GetChild(matrizIndicesFichas[posX, posY]).GetChild(1).gameObject.SetActive(false);
                            }
                            else if (matrizTablero[posX, posY] == 2)
                            {
                                fichasNegras.transform.GetChild(matrizIndicesFichas[posX, posY]).GetChild(0).gameObject.SetActive(false);
                                fichasNegras.transform.GetChild(matrizIndicesFichas[posX, posY]).GetChild(1).gameObject.SetActive(false);
                            }

                            posX--;

                            if (posX < 0)
                            {
                                posX = 7;
                            }
                        }
                        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || (!pulsado && digitalX > 0))
                        {
                            pulsado = true;
                            mueve = true;
                            musica.ProduceEfecto(11);

                            if (matrizTablero[posX, posY] == 1)
                            {
                                fichasBlancas.transform.GetChild(matrizIndicesFichas[posX, posY]).GetChild(0).gameObject.SetActive(false);
                                fichasBlancas.transform.GetChild(matrizIndicesFichas[posX, posY]).GetChild(1).gameObject.SetActive(false);
                            }
                            else if (matrizTablero[posX, posY] == 2)
                            {
                                fichasNegras.transform.GetChild(matrizIndicesFichas[posX, posY]).GetChild(0).gameObject.SetActive(false);
                                fichasNegras.transform.GetChild(matrizIndicesFichas[posX, posY]).GetChild(1).gameObject.SetActive(false);
                            }

                            posX++;

                            if (posX > 7)
                            {
                                posX = 0;
                            }
                        }
                        else if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                        {
                            musica.ProduceEfecto(10);
                            MueveAliado();
                        }
                        else if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("B"))
                        {
                            musica.ProduceEfecto(10);
                            DesactivaCasillasMostradas();
                            mover = false;
                        }
                    }
                    else if (huir)
                    {

                    }
                    else
                    {
                        if (mueve)
                        {
                            int aux = posY * 8 + posX;
                            seleccion.transform.position = posiciones.transform.GetChild(aux).transform.position;
                            mueve = false;

                            if (matrizTablero[posX, posY] == 1)
                            {
                                fichasBlancas.transform.GetChild(matrizIndicesFichas[posX, posY]).GetChild(0).gameObject.SetActive(true);
                                fichasBlancas.transform.GetChild(matrizIndicesFichas[posX, posY]).GetChild(1).gameObject.SetActive(true);
                            }
                            else if (matrizTablero[posX, posY] == 2)
                            {
                                fichasNegras.transform.GetChild(matrizIndicesFichas[posX, posY]).GetChild(0).gameObject.SetActive(true);
                                fichasNegras.transform.GetChild(matrizIndicesFichas[posX, posY]).GetChild(1).gameObject.SetActive(true);
                            }
                        }

                        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || (!pulsado && digitalY > 0))
                        {
                            pulsado = true;
                            mueve = true;
                            musica.ProduceEfecto(11);

                            if (matrizTablero[posX, posY] == 1)
                            {
                                fichasBlancas.transform.GetChild(matrizIndicesFichas[posX, posY]).GetChild(0).gameObject.SetActive(false);
                                fichasBlancas.transform.GetChild(matrizIndicesFichas[posX, posY]).GetChild(1).gameObject.SetActive(false);
                            }
                            else if (matrizTablero[posX, posY] == 2)
                            {
                                fichasNegras.transform.GetChild(matrizIndicesFichas[posX, posY]).GetChild(0).gameObject.SetActive(false);
                                fichasNegras.transform.GetChild(matrizIndicesFichas[posX, posY]).GetChild(1).gameObject.SetActive(false);
                            }

                            posY++;

                            if (posY > 7)
                            {
                                posY = 0;
                            }
                        }
                        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || (!pulsado && digitalY < 0))
                        {
                            pulsado = true;
                            mueve = true;
                            musica.ProduceEfecto(11);

                            if (matrizTablero[posX, posY] == 1)
                            {
                                fichasBlancas.transform.GetChild(matrizIndicesFichas[posX, posY]).GetChild(0).gameObject.SetActive(false);
                                fichasBlancas.transform.GetChild(matrizIndicesFichas[posX, posY]).GetChild(1).gameObject.SetActive(false);
                            }
                            else if (matrizTablero[posX, posY] == 2)
                            {
                                fichasNegras.transform.GetChild(matrizIndicesFichas[posX, posY]).GetChild(0).gameObject.SetActive(false);
                                fichasNegras.transform.GetChild(matrizIndicesFichas[posX, posY]).GetChild(1).gameObject.SetActive(false);
                            }

                            posY--;

                            if (posY < 0)
                            {
                                posY = 7;
                            }
                        }
                        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || (!pulsado && digitalX < 0))
                        {
                            pulsado = true;
                            mueve = true;
                            musica.ProduceEfecto(11);

                            if (matrizTablero[posX, posY] == 1)
                            {
                                fichasBlancas.transform.GetChild(matrizIndicesFichas[posX, posY]).GetChild(0).gameObject.SetActive(false);
                                fichasBlancas.transform.GetChild(matrizIndicesFichas[posX, posY]).GetChild(1).gameObject.SetActive(false);
                            }
                            else if (matrizTablero[posX, posY] == 2)
                            {
                                fichasNegras.transform.GetChild(matrizIndicesFichas[posX, posY]).GetChild(0).gameObject.SetActive(false);
                                fichasNegras.transform.GetChild(matrizIndicesFichas[posX, posY]).GetChild(1).gameObject.SetActive(false);
                            }

                            posX--;

                            if (posX < 0)
                            {
                                posX = 7;
                            }
                        }
                        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || (!pulsado && digitalX > 0))
                        {
                            pulsado = true;
                            mueve = true;
                            musica.ProduceEfecto(11);

                            if (matrizTablero[posX, posY] == 1)
                            {
                                fichasBlancas.transform.GetChild(matrizIndicesFichas[posX, posY]).GetChild(0).gameObject.SetActive(false);
                                fichasBlancas.transform.GetChild(matrizIndicesFichas[posX, posY]).GetChild(1).gameObject.SetActive(false);
                            }
                            else if (matrizTablero[posX, posY] == 2)
                            {
                                fichasNegras.transform.GetChild(matrizIndicesFichas[posX, posY]).GetChild(0).gameObject.SetActive(false);
                                fichasNegras.transform.GetChild(matrizIndicesFichas[posX, posY]).GetChild(1).gameObject.SetActive(false);
                            }

                            posX++;

                            if (posX > 7)
                            {
                                posX = 0;
                            }
                        }
                        else if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                        {
                            musica.ProduceEfecto(10);

                            if(matrizTablero[posX, posY] == 1)
                            {
                                ActivaMenu(true);
                            }
                        }
                    }
                }
                else
                {
                    EnemigoDecideAccion();
                }
            }
        }
    }



    void ActivaJuego(int indice)
    {
        atacar = false;
        mueve = false;
        posX = posY = 0;

        seleccion.SetActive(true);
        seleccion.transform.position = posiciones.transform.GetChild(0).transform.position;

        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                matrizTablero[i, j] = 0;
                matrizIndicesFichas[i, j] = -1;
            }
        }

        for (int i = 0; i < 16; i++)
        {
            FichasAjedrez aux = new FichasAjedrez();

            if (i < 8) //Peones
            {
                fichasBlan[i] = aux.IniciaFicha(0);

                matrizTablero[i, 1] = 1;
                matrizIndicesFichas[i, 1] = i;
                fichasBlan[i].posX = i;
                fichasBlan[i].posY = 1;
            }
            else if(i < 10) //Torres
            {
                fichasBlan[i] = aux.IniciaFicha(1);

                if(i == 8)
                {
                    matrizTablero[0, 0] = 1;
                    matrizIndicesFichas[0, 0] = 8;
                    fichasBlan[i].posX = 0;
                    fichasBlan[i].posY = 0;
                }
                else
                {
                    matrizTablero[7, 0] = 1;
                    matrizIndicesFichas[7, 0] = 9;
                    fichasBlan[i].posX = 7;
                    fichasBlan[i].posY = 0;
                }
            }
            else if (i < 12) //Caballos
            {
                fichasBlan [i] = aux.IniciaFicha(3);

                if (i == 10)
                {
                    matrizTablero[1, 0] = 1;
                    matrizIndicesFichas[1, 0] = 10;
                    fichasBlan[i].posX = 1;
                    fichasBlan[i].posY = 0;
                }
                else
                {
                    matrizTablero[6, 0] = 1;
                    matrizIndicesFichas[6, 0] = 11;
                    fichasBlan[i].posX = 6;
                    fichasBlan[i].posY = 0;
                }
            }
            else if (i < 14) //Alfiles
            {
                fichasBlan[i] = aux.IniciaFicha(2);

                if (i == 12)
                {
                    matrizTablero[2, 0] = 1;
                    matrizIndicesFichas[2, 0] = 12;
                    fichasBlan[i].posX = 2;
                    fichasBlan[i].posY = 0;
                }
                else
                {
                    matrizTablero[5, 0] = 1;
                    matrizIndicesFichas[5, 0] = 13;
                    fichasBlan[i].posX = 5;
                    fichasBlan[i].posY = 0;
                }
            }
            else if (i == 14)
            {
                fichasBlan[i] = aux.IniciaFicha(4);

                matrizTablero[3, 0] = 1;
                matrizIndicesFichas[3, 0] = 14;
                fichasBlan[i].posX = 3;
                fichasBlan[i].posY = 0;
            }
            else if (i == 15)
            {
                fichasBlan[i] = aux.IniciaFicha(5);
                matrizTablero[4, 0] = 1;
                matrizIndicesFichas[4, 0] = 15;
                fichasBlan[i].posX = 4;
                fichasBlan[i].posY = 0;
            }
        } //Inicio fichas blancas

        for (int i = 0; i < 16; i++) 
        {
            FichasAjedrez aux = new FichasAjedrez();

            if (i < 8) //Peones
            {
                fichasNegr[i] = aux.IniciaFicha(0);
                matrizTablero[i, 6] = 2;
                matrizIndicesFichas[i, 6] = i;
                fichasNegr[i].posX = i;
                fichasNegr[i].posY = 6;
            }
            else if (i < 10) //Torres
            {
                fichasNegr[i] = aux.IniciaFicha(1);

                if (i == 8)
                {
                    matrizTablero[0, 7] = 2;
                    matrizIndicesFichas[0, 7] = 8;
                    fichasNegr[i].posX = 0;
                    fichasNegr[i].posY = 7;
                }
                else
                {
                    matrizTablero[7, 7] = 2;
                    matrizIndicesFichas[7, 7] = 9;
                    fichasNegr[i].posX = 7;
                    fichasNegr[i].posY = 7;
                }
            }
            else if (i < 12) //Caballos
            {
                fichasNegr[i] = aux.IniciaFicha(3);

                if (i == 10)
                {
                    matrizTablero[1, 7] = 2;
                    matrizIndicesFichas[1, 7] = 10;
                    fichasNegr[i].posX = 1;
                    fichasNegr[i].posY = 7;
                }
                else
                {
                    matrizTablero[6, 7] = 2;
                    matrizIndicesFichas[6, 7] = 11;
                    fichasNegr[i].posX = 6;
                    fichasNegr[i].posY = 7;
                }
            }
            else if (i < 14) //Alfiles
            {
                fichasNegr[i] = aux.IniciaFicha(2);

                if (i == 12)
                {
                    matrizTablero[2, 7] = 2;
                    matrizIndicesFichas[2, 7] = 12;
                    fichasNegr[i].posX = 2;
                    fichasNegr[i].posY = 7;
                }
                else
                {
                    matrizTablero[5, 7] = 2;
                    matrizIndicesFichas[5, 7] = 13;
                    fichasNegr[i].posX = 5;
                    fichasNegr[i].posY = 7;
                }
            }
            else if (i == 14)
            {
                fichasNegr[i] = aux.IniciaFicha(4);

                matrizTablero[3, 7] = 2;
                matrizIndicesFichas[3, 7] = 14;
                fichasNegr[i].posX = 3;
                fichasNegr[i].posY = 7;
            }
            else if (i == 15)
            {
                fichasNegr[i] = aux.IniciaFicha(5);
                matrizTablero[4, 7] = 2;
                matrizIndicesFichas[4, 7] = 15;
                fichasNegr[i].posX = 4;
                fichasNegr[i].posY = 7;
            }
        } //Inicio fichas negras

        int k = 0;

        for (int i = 0; i < 16; i++)
        {
            int aux;
            int aux2;

            if (i < 8)
            {
                aux = 8 + i;
                aux2 = 48 + i;
            }
            else
            {
                if(i % 2 == 0)
                {
                    aux = i - 8 - k;
                    aux2 = 56 + k;
                    k++;
                }
                else
                {
                    aux = i - k - 2*k + 1;
                    aux2 = 64 - k;
                }
            }

            fichasBlancas.transform.GetChild(i).transform.position = posiciones.transform.GetChild(aux).transform.position;
            fichasBlancas.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
            fichasBlancas.transform.GetChild(i).GetChild(1).gameObject.SetActive(false);
            fichasNegras.transform.GetChild(i).transform.position = posiciones.transform.GetChild(aux2).transform.position;
            fichasNegras.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
            fichasNegras.transform.GetChild(i).GetChild(1).gameObject.SetActive(false);
        }

        fichasBlancas.transform.GetChild(8).GetChild(0).gameObject.SetActive(true);
        fichasBlancas.transform.GetChild(8).GetChild(1).gameObject.SetActive(true);

        numeroPeonesBlancos = numeroPeonesNegros = 8;
        numeroTorresBlancas = numeroTorresNegras = 2;
        numeroAlfilesBlancos = numeroAlfilessNegros = 2;
        numeroCaballosBlancos = numeroCaballosNegros = 2;
        numeroReinasBlancas = numeroReinassNegras = 1;

        interfazJugador.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "x8";
        interfazJugador.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = "x2";
        interfazJugador.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = "x2";
        interfazJugador.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = "x2";
        interfazJugador.transform.GetChild(5).GetChild(0).GetComponent<Text>().text = "x1";
        interfazJugador.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = baseDeDatos.equipoAliado[0].nombre;
        
        interfazEnemigo.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "x8";
        interfazEnemigo.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = "x2";
        interfazEnemigo.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = "x2";
        interfazEnemigo.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = "x2";
        interfazEnemigo.transform.GetChild(5).GetChild(0).GetComponent<Text>().text = "x1";

        //0 -- golpista 1 -- imperio 2 -- regente 3 -- Resistencia 4 -- R.Asalto 5 -- R.Especiales 6 -- R.Investigacion 7 -- Cupula 8 -- Nada
        if (baseDeDatos.faccion == 0)
        {
            imagenAliado = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/TrajesProta/Emperador");
        }
        else if (baseDeDatos.faccion == 1)
        {
            imagenAliado = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/TrajesProta/GuardiaImperial");
        }
        else if (baseDeDatos.faccion == 2)
        {
            imagenAliado = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/TrajesProta/GuardiaReal");
        }
        else if (baseDeDatos.faccion == 3)
        {
            imagenAliado = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/TrajesProta/Resistencia");
        }
        else if (baseDeDatos.faccion == 4)
        {
            imagenAliado = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/TrajesProta/FuerzasAsalto");
        }
        else if (baseDeDatos.faccion == 5)
        {
            imagenAliado = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/TrajesProta/EquipoFuerzasEspeciales");
        }
        else if (baseDeDatos.faccion == 6)
        {
            imagenAliado = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/TrajesProta/EquipoInteligencia");
        }
        else if (baseDeDatos.faccion == 7)
        {
            imagenAliado = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/TrajesProta/Cupula");
        }
        else
        {
            imagenAliado = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/Prota");
        }

        string nombreEnemigo = "";

        switch (indice)
        {
            case 0:
                dificultad = 0;
                nombreEnemigo = "Falta";
                imagenEnemigo = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Lionetta");
                break;
            case 1:
                dificultad = 4;
                nombreEnemigo = "Falta";
                imagenEnemigo = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Lionetta");
                break;

        }

        interfazJugador.transform.GetChild(0).GetComponent<Image>().sprite = imagenAliado[1];
        interfazEnemigo.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = nombreEnemigo;
        interfazEnemigo.transform.GetChild(0).GetComponent<Image>().sprite = imagenEnemigo[1];

        this.gameObject.SetActive(true);

        activo = true;

        ActivaMenu(false);
        ActivaPromocion(false);
        ActivaMovimiento(true);
    }



    void CierraJuego()
    {

    }



    void MuestraCasillasAlfil()
    {
        int aux = 0;
        numeroPosicionesMostradas = 0;
        posicionesAux = new int[14];

        if (posX == 0 && posY == 0) //Esquina inferior izquierda
        {
            bool obstaculo = false;
            int i = 1;

            while (!obstaculo && i < 8) //diagonal superior derecha
            {
                if (matrizTablero[i, 0] != 0)
                {
                    obstaculo = true;

                    if (matrizTablero[posX + i, posY + 1] == 2)
                    {
                        aux = posX + i + (posY + 1) * 8;
                        posicionesAux[numeroPosicionesMostradas] = aux;
                        numeroPosicionesMostradas++;

                        posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
                    }
                }
                else
                {
                    aux = posX + i + (posY + 1) * 8;
                    posicionesAux[numeroPosicionesMostradas] = aux;
                    numeroPosicionesMostradas++;

                    posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 255f / 255f);
                }

                i++;
            }
        }
        else if (posX == 7 && posY == 0) //Esquina inferior derecha
        {
            bool obstaculo = false;
            int i = 1;

            while (!obstaculo && i < 8)
            {
                if (matrizTablero[posX - i, (posY + i)] != 0)
                {
                    obstaculo = true;

                    if (matrizTablero[posX - i, (posY + i)] == 2)
                    {
                        aux = posX - i + (posY + i) * 8;
                        posicionesAux[numeroPosicionesMostradas] = aux;
                        numeroPosicionesMostradas++;

                        posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
                    }
                }
                else
                {
                    aux = posX - i + (posY + i) * 8;
                    posicionesAux[numeroPosicionesMostradas] = aux;
                    numeroPosicionesMostradas++;

                    posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 255f / 255f);
                }

                i--;
            }
        }
        else if (posX == 0 && posY == 7) //Esquina superior izquierda
        {
            bool obstaculo = false;
            int i = 1;

            while (!obstaculo && i < 8)
            {
                if (matrizTablero[posX + i, posY - i] != 0)
                {
                    obstaculo = true;

                    if (matrizTablero[i, 0] == 2)
                    {
                        aux = posX + i + (posY - i) * 8;
                        posicionesAux[numeroPosicionesMostradas] = aux;
                        numeroPosicionesMostradas++;

                        posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
                    }
                }
                else
                {
                    aux = posX + i + (posY - i) * 8;
                    posicionesAux[numeroPosicionesMostradas] = aux;
                    numeroPosicionesMostradas++;

                    posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 255f / 255f);
                }

                i++;
            }
        }
        else if (posX == 7 && posY == 7) //Esquina superior derecha
        {
            bool obstaculo = false;
            int i = 1;

            while (!obstaculo && i < 8)
            {
                if (matrizTablero[posX - i, posY - i] != 0)
                {
                    obstaculo = true;

                    if (matrizTablero[posX - i, posY - i] == 2)
                    {
                        aux = posX - i + (posY - i) * 8;
                        posicionesAux[numeroPosicionesMostradas] = aux;
                        numeroPosicionesMostradas++;

                        posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
                    }
                }
                else
                {
                    aux = posX - i + (posY - i) * 8;
                    posicionesAux[numeroPosicionesMostradas] = aux;
                    numeroPosicionesMostradas++;

                    posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 255f / 255f);
                }

                i++;
            }
        }
        else
        {
            bool miraIzq = false;
            bool miraDer = false;
            bool miraArr = false;
            bool miraAb = false;

            if(posX != 0)
            {
                miraIzq = true;
            }

            if(posX != 7)
            {
                miraDer = true;
            }

            if(posY != 0)
            {
                miraAb = true;
            }

            if(posY != 7)
            {
                miraArr = true;
            }

            if (miraIzq)
            {
                if (miraArr)
                {
                    bool obstaculo = false;
                    int i = 1;
                    int limite = posX;
                    int limiteY = posY;

                    while (!obstaculo && i < 8 && limite != 0 && limiteY != 7)
                    {
                        if (matrizTablero[posX - i, (posY + i)] != 0)
                        {
                            obstaculo = true;

                            if (matrizTablero[posX - i, (posY + i)] == 2)
                            {
                                aux = posX - i + (posY + i) * 8;
                                posicionesAux[numeroPosicionesMostradas] = aux;
                                numeroPosicionesMostradas++;

                                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
                            }
                        }
                        else
                        {
                            aux = posX - i + (posY + i) * 8;
                            posicionesAux[numeroPosicionesMostradas] = aux;
                            numeroPosicionesMostradas++;

                            posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 255f / 255f);
                        }

                        limite = posX - i;
                        limiteY = posY + i;
                        i++;
                    }
                }

                if (miraAb)
                {
                    bool obstaculo = false;
                    int i = 1;
                    int limite = posX;
                    int limiteY = posY;

                    while (!obstaculo && i < 8 && limite != 0 && limiteY != 0)
                    {
                        if (matrizTablero[posX - i, posY - i] != 0)
                        {
                            obstaculo = true;

                            if (matrizTablero[posX - i, posY - i] == 2)
                            {
                                aux = posX - i + (posY - i) * 8;
                                posicionesAux[numeroPosicionesMostradas] = aux;
                                numeroPosicionesMostradas++;

                                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
                            }
                        }
                        else
                        {
                            aux = posX - i + (posY - i) * 8;
                            posicionesAux[numeroPosicionesMostradas] = aux;
                            numeroPosicionesMostradas++;

                            posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 255f / 255f);
                        }

                        limite = posX - i;
                        limiteY = posY - i;
                        i++;
                    }
                }
            }

            if (miraDer)
            {
                if (miraArr)
                {
                    bool obstaculo = false;
                    int i = 1;
                    int limite = posX;
                    int limiteY = posY;

                    while (!obstaculo && i < 8 && limite != 7 && limiteY != 7) //diagonal superior derecha
                    {
                        if (matrizTablero[posX + i, posY + i] != 0)
                        {
                            obstaculo = true;

                            if (matrizTablero[posX + i, posY + i] == 2)
                            {
                                aux = posX + i + (posY + i) * 8;
                                posicionesAux[numeroPosicionesMostradas] = aux;
                                numeroPosicionesMostradas++;

                                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
                            }
                        }
                        else
                        {
                            aux = posX + i + (posY + i) * 8;
                            posicionesAux[numeroPosicionesMostradas] = aux;
                            numeroPosicionesMostradas++;

                            posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 255f / 255f);
                        }

                        limite = posX + i;
                        limiteY = posY + i;
                        i++;
                    }
                }

                if (miraAb)
                {
                    bool obstaculo = false;
                    int i = 1;
                    int limite = posX;
                    int limiteY = posY;

                    while (!obstaculo && i < 8 && limite != 7 && limiteY != 0)
                    {
                        if (matrizTablero[posX + i, posY - i] != 0)
                        {
                            obstaculo = true;

                            if (matrizTablero[posX + i, posY - i] == 2)
                            {
                                aux = posX + i + (posY - i) * 8;
                                posicionesAux[numeroPosicionesMostradas] = aux;
                                numeroPosicionesMostradas++;

                                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
                            }
                        }
                        else
                        {
                            aux = posX + i + (posY - i) * 8;
                            posicionesAux[numeroPosicionesMostradas] = aux;
                            numeroPosicionesMostradas++;

                            posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 255f / 255f);
                        }

                        limite = posX + i;
                        limiteY = posY - i;
                        i++;
                    }
                }
            }
        }
    }



    void MuestraCasillasPeon()
    {
        int aux = 0;
        numeroPosicionesMostradas = 0;
        posicionesAux = new int [4];

        if(posX == 0)
        {
            if(matrizTablero[posX, (posY+1)] == 2) //posicion una casilla delante enemigo
            {
                aux = (posY + 1) * 8;
                posicionesAux[numeroPosicionesMostradas] = aux;
                numeroPosicionesMostradas++;

                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            }
            else if (matrizTablero[posX, (posY + 1)] == 0)
            {
                aux = (posY + 1) * 8;
                posicionesAux[numeroPosicionesMostradas] = aux;
                numeroPosicionesMostradas++;

                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 255f / 255f);
            }

            if(posY == 1) //Si el peon aun no se ha movido puede mirar dos casillas adelante
            {
                if (matrizTablero[posX, (posY + 2)] == 2) //posicion una casilla delante enemigo
                {
                    aux = (posY + 2) * 8;
                    posicionesAux[numeroPosicionesMostradas] = aux;
                    numeroPosicionesMostradas++;

                    posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
                }
                else if (matrizTablero[posX, (posY + 2)] == 0)
                {
                    aux = (posY + 2) * 8;
                    posicionesAux[numeroPosicionesMostradas] = aux;
                    numeroPosicionesMostradas++;

                    posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 255f / 255f);
                }
            }

            if (matrizTablero[(posX+1), (posY + 1)] == 2) //posicion una casilla diagonal derecha enemigo
            {
                aux = (posY + 1) * 8 + 1;
                posicionesAux[numeroPosicionesMostradas] = aux;
                numeroPosicionesMostradas++;

                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            }
        }
        else if(posX > 0 && posX < 7)
        {
            if (matrizTablero[posX, (posY + 1)] == 2) //posicion una casilla delante enemigo
            {
                aux = (posY + 1) * 8 + posX;
                posicionesAux[numeroPosicionesMostradas] = aux;
                numeroPosicionesMostradas++;

                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            }
            else if (matrizTablero[posX, (posY + 1)] == 0)
            {
                aux = (posY + 1) * 8 + posX;
                posicionesAux[numeroPosicionesMostradas] = aux;
                numeroPosicionesMostradas++;

                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 255f / 255f);
            }
            
            if (posY == 1) //Si el peon aun no se ha movido puede mirar dos casillas adelante
            {
                if (matrizTablero[posX, (posY + 2)] == 2) //posicion una casilla delante enemigo
                {
                    aux = (posY + 2) * 8 + posX;
                    posicionesAux[numeroPosicionesMostradas] = aux;
                    numeroPosicionesMostradas++;

                    posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
                }
                else if (matrizTablero[posX, (posY + 2)] == 0)
                {
                    aux = (posY + 2) * 8 + posX;
                    posicionesAux[numeroPosicionesMostradas] = aux;
                    numeroPosicionesMostradas++;

                    posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 255f / 255f);
                }
            }

            if (matrizTablero[(posX + 1), (posY + 1)] == 2) //posicion una casilla diagonal derecha enemigo
            {
                aux = (posY + 1) * 8 + (posX + 1);
                posicionesAux[numeroPosicionesMostradas] = aux;
                numeroPosicionesMostradas++;

                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            }

            if (matrizTablero[(posX - 1), (posY + 1)] == 2) //posicion una casilla diagonal izquierda enemigo
            {
                aux = (posY + 1) * 8 + (posX - 1);
                posicionesAux[numeroPosicionesMostradas] = aux;
                numeroPosicionesMostradas++;

                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            }
        }
        else
        {
            if (matrizTablero[posX, (posY + 1)] == 2) //posicion una casilla delante enemigo
            {
                aux = (posY + 1) * 8 + posX;
                posicionesAux[numeroPosicionesMostradas] = aux;
                numeroPosicionesMostradas++;

                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            }
            else if (matrizTablero[posX, (posY + 1)] == 0)
            {
                aux = (posY + 1) * 8 + posX;
                posicionesAux[numeroPosicionesMostradas] = aux;
                numeroPosicionesMostradas++;

                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 255f / 255f);
            }

            if (posY == 1) //Si el peon aun no se ha movido puede mirar dos casillas adelante
            {
                if (matrizTablero[posX, (posY + 2)] == 2) //posicion una casilla delante enemigo
                {
                    aux = (posY + 2) * 8 + posX;
                    posicionesAux[numeroPosicionesMostradas] = aux;
                    numeroPosicionesMostradas++;

                    posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
                }
                else if (matrizTablero[posX, (posY + 2)] == 0)
                {
                    aux = (posY + 2) * 8 + posX;
                    posicionesAux[numeroPosicionesMostradas] = aux;
                    numeroPosicionesMostradas++;

                    posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 255f / 255f);
                }
            }

            if (matrizTablero[(posX - 1), (posY + 1)] == 2) //posicion una casilla diagonal izquierda enemigo
            {
                aux = (posY + 1) * 8 + (posX - 1);
                posicionesAux[numeroPosicionesMostradas] = aux;
                numeroPosicionesMostradas++;

                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            }
        }
    }



    void MuestraCasillasTorre()
    {
        int aux = 0;
        numeroPosicionesMostradas = 0;
        posicionesAux = new int[14];

        if (posX == 0 && posY == 0) //Esquina inferior izquierda
        {
            bool obstaculo = false;
            int i = 1;

            while (!obstaculo && i < 8) //Posiciones X
            {
                if(matrizTablero[i, 0] != 0)
                {
                    obstaculo = true;

                    if (matrizTablero[i, posY] == 2)
                    {
                        aux = i;
                        posicionesAux[numeroPosicionesMostradas] = aux;
                        numeroPosicionesMostradas++;

                        posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
                    }
                }
                else
                {
                    aux = i;
                    posicionesAux[numeroPosicionesMostradas] = aux;
                    numeroPosicionesMostradas++;

                    posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 255f / 255f);
                }

                i++;
            }

            i = 1;
            obstaculo = false;

            while (!obstaculo && i < 8) //Posiciones Y
            {
                if (matrizTablero[0, i] != 0)
                {
                    obstaculo = true;

                    if (matrizTablero[0, i] == 2)
                    {
                        aux = 8 * i;
                        posicionesAux[numeroPosicionesMostradas] = aux;
                        numeroPosicionesMostradas++;

                        posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
                    }
                }
                else
                {
                    aux = 8 * i;
                    posicionesAux[numeroPosicionesMostradas] = aux;
                    numeroPosicionesMostradas++;

                    posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 255f / 255f);
                }

                i++;
            }
        }
        else if (posX == 7 && posY == 0) //Esquina inferior derecha
        {
            bool obstaculo = false;
            int i = 6;

            while (!obstaculo && i >= 0) //Posiciones X
            {
                if (matrizTablero[i, 0] != 0)
                {
                    obstaculo = true;

                    if (matrizTablero[i, 0] == 2)
                    {
                        aux = i + posY*8;
                        posicionesAux[numeroPosicionesMostradas] = aux;
                        numeroPosicionesMostradas++;

                        posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
                    }
                }
                else
                {
                    aux = i + posY * 8;
                    posicionesAux[numeroPosicionesMostradas] = aux;
                    numeroPosicionesMostradas++;

                    posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 255f / 255f);
                }

                i--;
            }

            i = 1;
            obstaculo = false;

            while (!obstaculo && i < 8) //Posiciones Y
            {
                if (matrizTablero[7, i] != 0)
                {
                    obstaculo = true;

                    if (matrizTablero[0, i] == 2)
                    {
                        aux = 8 * i + posX;
                        posicionesAux[numeroPosicionesMostradas] = aux;
                        numeroPosicionesMostradas++;

                        posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
                    }
                }
                else
                {
                    aux = 8 * i + posX;
                    posicionesAux[numeroPosicionesMostradas] = aux;
                    numeroPosicionesMostradas++;

                    posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 255f / 255f);
                }

                i++;
            }
        }
        else if (posX == 0 && posY == 7) //Esquina superior izquierda
        {
            bool obstaculo = false;
            int i = 1;

            while (!obstaculo && i < 8) //Posiciones X
            {
                if (matrizTablero[i, 7] != 0)
                {
                    obstaculo = true;

                    if (matrizTablero[i, 0] == 2)
                    {
                        aux = i + posY * 8;
                        posicionesAux[numeroPosicionesMostradas] = aux;
                        numeroPosicionesMostradas++;

                        posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
                    }
                }
                else
                {
                    aux = i + posY * 8;
                    posicionesAux[numeroPosicionesMostradas] = aux;
                    numeroPosicionesMostradas++;

                    posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 255f / 255f);
                }

                i++;
            }

            i = 6;
            obstaculo = false;

            while (!obstaculo && i >= 0) //Posiciones Y
            {
                if (matrizTablero[0, i] != 0)
                {
                    obstaculo = true;

                    if (matrizTablero[0, i] == 2)
                    {
                        aux = 8 * i + posX;
                        posicionesAux[numeroPosicionesMostradas] = aux;
                        numeroPosicionesMostradas++;

                        posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
                    }
                }
                else
                {
                    aux = 8 * i + posX;
                    posicionesAux[numeroPosicionesMostradas] = aux;
                    numeroPosicionesMostradas++;

                    posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 255f / 255f);
                }

                i--;
            }
        }
        else if (posX == 7 && posY == 7) //Esquina superior derecha
        {
            bool obstaculo = false;
            int i = 6;

            while (!obstaculo && i >= 0) //Posiciones X
            {
                if (matrizTablero[i, 7] != 0)
                {
                    obstaculo = true;

                    if (matrizTablero[i, 0] == 2)
                    {
                        aux = i + posY * 8;
                        posicionesAux[numeroPosicionesMostradas] = aux;
                        numeroPosicionesMostradas++;

                        posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
                    }
                }
                else
                {
                    aux = i + posY * 8;
                    posicionesAux[numeroPosicionesMostradas] = aux;
                    numeroPosicionesMostradas++;

                    posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 255f / 255f);
                }

                i--;
            }

            i = 6;
            obstaculo = false;

            while (!obstaculo && i >= 0) //Posiciones Y
            {
                if (matrizTablero[7, i] != 0)
                {
                    obstaculo = true;

                    if (matrizTablero[0, i] == 2)
                    {
                        aux = 8 * i;
                        posicionesAux[numeroPosicionesMostradas] = aux;
                        numeroPosicionesMostradas++;

                        posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
                    }
                }
                else
                {
                    aux = 8 * i;
                    posicionesAux[numeroPosicionesMostradas] = aux;
                    numeroPosicionesMostradas++;

                    posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 255f / 255f);
                }

                i++;
            }
        }
        else
        {
            bool obstaculo = false;
            int i = posX - 1;

            if(posX != 0)
            {
                while (!obstaculo && i >= 0) //Posiciones X izquierda
                {
                    if (matrizTablero[i, posY] != 0)
                    {
                        obstaculo = true;

                        if (matrizTablero[i, posY] == 2)
                        {
                            aux = i + posY * 8;
                            posicionesAux[numeroPosicionesMostradas] = aux;
                            numeroPosicionesMostradas++;

                            posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
                        }
                    }
                    else
                    {
                        aux = i + posY * 8;
                        posicionesAux[numeroPosicionesMostradas] = aux;
                        numeroPosicionesMostradas++;

                        posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 255f / 255f);
                    }

                    i--;
                }

            }

            if(posX != 7)
            {
                obstaculo = false;
                i = posX + 1;

                while (!obstaculo && i < 8) //Posiciones X derecha
                {
                    if (matrizTablero[i, posY] != 0)
                    {
                        obstaculo = true;

                        if (matrizTablero[i, posY] == 2)
                        {
                            aux = i + posY * 8;
                            posicionesAux[numeroPosicionesMostradas] = aux;
                            numeroPosicionesMostradas++;

                            posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
                        }
                    }
                    else
                    {
                        aux = i + posY * 8;
                        posicionesAux[numeroPosicionesMostradas] = aux;
                        numeroPosicionesMostradas++;

                        posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 255f / 255f);
                    }

                    i++;
                }
            }
            
            if(posY != 0)
            {
                i = posY - 1;
                obstaculo = false;

                while (!obstaculo && i >= 0) //Posiciones Y abajo
                {
                    if (matrizTablero[posX, i] != 0)
                    {
                        obstaculo = true;

                        if (matrizTablero[posX, i] == 2)
                        {
                            aux = 8 * i + posX;
                            posicionesAux[numeroPosicionesMostradas] = aux;
                            numeroPosicionesMostradas++;

                            posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
                        }
                    }
                    else
                    {
                        aux = 8 * i + posX;
                        posicionesAux[numeroPosicionesMostradas] = aux;
                        numeroPosicionesMostradas++;

                        posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 255f / 255f);
                    }

                    i--;
                }
            }
            
            if(posY != 7)
            {
                i = posY + 1;
                obstaculo = false;

                while (!obstaculo && i < 8) //Posiciones Y superiores
                {
                    if (matrizTablero[posX, i] != 0)
                    {
                        obstaculo = true;

                        if (matrizTablero[posX, i] == 2)
                        {
                            aux = 8 * i + posX;
                            posicionesAux[numeroPosicionesMostradas] = aux;
                            numeroPosicionesMostradas++;

                            posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
                        }
                    }
                    else
                    {
                        aux = 8 * i + posX;
                        posicionesAux[numeroPosicionesMostradas] = aux;
                        numeroPosicionesMostradas++;

                        posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 255f / 255f);
                    }

                    i++;
                }
            }
           
        }
    }



    void MuestraCasillasCaballo()
    {
        int aux = 0;
        numeroPosicionesMostradas = 0;
        posicionesAux = new int[8];

        if (posX + 2 <= 7 && posY + 1 <= 7)
        {
            aux = (posY + 1) * 8 + posX + 2;
            int aux2 = matrizTablero[posX + 2, posY + 1];

            if (aux2 == 0)
            {
                posicionesAux[numeroPosicionesMostradas] = aux;
                numeroPosicionesMostradas++;

                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 255f / 255f);
            }
            else if (aux2 == 2)
            {
                posicionesAux[numeroPosicionesMostradas] = aux;
                numeroPosicionesMostradas++;

                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            }
        }

        if (posX + 2 <= 7 && posY - 1 >= 0)
        {
            aux = (posY - 1) * 8 + posX + 2;
            int aux2 = matrizTablero[posX + 2, posY - 1];

            if (aux2 == 0)
            {
                posicionesAux[numeroPosicionesMostradas] = aux;
                numeroPosicionesMostradas++;

                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 255f / 255f);
            }
            else if (aux2 == 2)
            {
                posicionesAux[numeroPosicionesMostradas] = aux;
                numeroPosicionesMostradas++;

                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            }
        }

        if (posX - 2 >= 0 && posY - 1 >= 0)
        {
            aux = (posY - 1) * 8 + posX - 2;
            int aux2 = matrizTablero[posX - 2, posY - 1];

            if (aux2 == 0)
            {
                posicionesAux[numeroPosicionesMostradas] = aux;
                numeroPosicionesMostradas++;

                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 255f / 255f);
            }
            else if (aux2 == 2)
            {
                posicionesAux[numeroPosicionesMostradas] = aux;
                numeroPosicionesMostradas++;

                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            }
        }

        if (posX - 2 >= 0 && posY + 1 <= 7)
        {
            aux = (posY + 1) * 8 + posX - 2;
            int aux2 = matrizTablero[posX - 2, posY + 1];

            if (aux2 == 0)
            {
                posicionesAux[numeroPosicionesMostradas] = aux;
                numeroPosicionesMostradas++;

                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 255f / 255f);
            }
            else if (aux2 == 2)
            {
                posicionesAux[numeroPosicionesMostradas] = aux;
                numeroPosicionesMostradas++;

                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            }
        }

        if (posX - 1 >= 0 && posY - 2 >= 0)
        {
            aux = (posY - 2) * 8 + posX - 1;
            int aux2 = matrizTablero[posX - 1, posY - 2];

            if (aux2 == 0)
            {
                posicionesAux[numeroPosicionesMostradas] = aux;
                numeroPosicionesMostradas++;

                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 255f / 255f);
            }
            else if (aux2 == 2)
            {
                posicionesAux[numeroPosicionesMostradas] = aux;
                numeroPosicionesMostradas++;

                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            }
        }

        if (posX + 1 <= 7 && posY - 2 >= 0)
        {
            aux = (posY - 2) * 8 + posX + 1;
            int aux2 = matrizTablero[posX + 1, posY - 2];

            if (aux2 == 0)
            {
                posicionesAux[numeroPosicionesMostradas] = aux;
                numeroPosicionesMostradas++;

                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 255f / 255f);
            }
            else if (aux2 == 2)
            {
                posicionesAux[numeroPosicionesMostradas] = aux;
                numeroPosicionesMostradas++;

                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            }
        }

        if (posX + 1 <= 7 && posY + 2 <= 7)
        {
            aux = (posY + 2) * 8 + posX + 1;
            int aux2 = matrizTablero[posX + 1, posY + 2];

            if (aux2 == 0)
            {
                posicionesAux[numeroPosicionesMostradas] = aux;
                numeroPosicionesMostradas++;

                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 255f / 255f);
            }
            else if (aux2 == 2)
            {
                posicionesAux[numeroPosicionesMostradas] = aux;
                numeroPosicionesMostradas++;

                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            }
        }

        if (posX - 1 >= 0 && posY + 2 <= 7)
        {
            aux = (posY + 2) * 8 + posX - 1;
            int aux2 = matrizTablero[posX - 1, posY + 2];

            if (aux2 == 0)
            {
                posicionesAux[numeroPosicionesMostradas] = aux;
                numeroPosicionesMostradas++;

                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 255f / 255f);
            }
            else if (aux2 == 2)
            {
                posicionesAux[numeroPosicionesMostradas] = aux;
                numeroPosicionesMostradas++;

                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            }
        }
    }



    void MuestraCasillasRey()
    {
        int aux = 0;
        numeroPosicionesMostradas = 0;
        posicionesAux = new int[8];

        if (posY + 1 <= 7)
        {
            aux = (posY + 1) * 8 + posX;
            int aux2 = matrizTablero[posX, posY + 1];

            if (aux2 == 0)
            {
                posicionesAux[numeroPosicionesMostradas] = aux;
                numeroPosicionesMostradas++;

                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 255f / 255f);
            }
            else if (aux2 == 2)
            {
                posicionesAux[numeroPosicionesMostradas] = aux;
                numeroPosicionesMostradas++;

                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            }
        }

        if (posY - 1 >= 0)
        {
            aux = (posY - 1) * 8 + posX;
            int aux2 = matrizTablero[posX, posY - 1];

            if (aux2 == 0)
            {
                posicionesAux[numeroPosicionesMostradas] = aux;
                numeroPosicionesMostradas++;

                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 255f / 255f);
            }
            else if (aux2 == 2)
            {
                posicionesAux[numeroPosicionesMostradas] = aux;
                numeroPosicionesMostradas++;

                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            }
        }

        if (posX - 1 >= 0 && posY - 1 >= 0)
        {
            aux = (posY - 1) * 8 + posX - 1;
            int aux2 = matrizTablero[posX - 1, posY - 1];

            if (aux2 == 0)
            {
                posicionesAux[numeroPosicionesMostradas] = aux;
                numeroPosicionesMostradas++;

                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 255f / 255f);
            }
            else if (aux2 == 2)
            {
                posicionesAux[numeroPosicionesMostradas] = aux;
                numeroPosicionesMostradas++;

                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            }
        }

        if (posX - 1 >= 0 && posY + 1 <= 7)
        {
            aux = (posY + 1) * 8 + posX - 1;
            int aux2 = matrizTablero[posX - 1, posY + 1];

            if (aux2 == 0)
            {
                posicionesAux[numeroPosicionesMostradas] = aux;
                numeroPosicionesMostradas++;

                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 255f / 255f);
            }
            else if (aux2 == 2)
            {
                posicionesAux[numeroPosicionesMostradas] = aux;
                numeroPosicionesMostradas++;

                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            }
        }

        if (posX - 1 >= 0)
        {
            aux = posY * 8 + posX - 1;
            int aux2 = matrizTablero[posX - 1, posY];

            if (aux2 == 0)
            {
                posicionesAux[numeroPosicionesMostradas] = aux;
                numeroPosicionesMostradas++;

                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 255f / 255f);
            }
            else if (aux2 == 2)
            {
                posicionesAux[numeroPosicionesMostradas] = aux;
                numeroPosicionesMostradas++;

                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            }
        }

        if (posX + 1 <= 7)
        {
            aux = posY * 8 + posX + 1;
            int aux2 = matrizTablero[posX + 1, posY];

            if (aux2 == 0)
            {
                posicionesAux[numeroPosicionesMostradas] = aux;
                numeroPosicionesMostradas++;

                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 255f / 255f);
            }
            else if (aux2 == 2)
            {
                posicionesAux[numeroPosicionesMostradas] = aux;
                numeroPosicionesMostradas++;

                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            }
        }

        if (posX + 1 <= 7 && posY - 1 >= 0)
        {
            aux = (posY - 1) * 8 + posX + 1;
            int aux2 = matrizTablero[posX + 1, posY - 1];

            if (aux2 == 0)
            {
                posicionesAux[numeroPosicionesMostradas] = aux;
                numeroPosicionesMostradas++;

                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 255f / 255f);
            }
            else if (aux2 == 2)
            {
                posicionesAux[numeroPosicionesMostradas] = aux;
                numeroPosicionesMostradas++;

                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            }
        }

        if (posX + 1 <= 7 && posY + 1 <= 7)
        {
            aux = (posY + 1) * 8 + posX + 1;
            int aux2 = matrizTablero[posX + 1, posY + 1];

            if (aux2 == 0)
            {
                posicionesAux[numeroPosicionesMostradas] = aux;
                numeroPosicionesMostradas++;

                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 255f / 255f);
            }
            else if (aux2 == 2)
            {
                posicionesAux[numeroPosicionesMostradas] = aux;
                numeroPosicionesMostradas++;

                posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(221f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            }
        }
    }



    void MuestraCasillaReina()
    {
        int[] posicionesReina = new int[28];
        int numeroPosicionesReina = 0;
        MuestraCasillasAlfil();
        numeroPosicionesReina = numeroPosicionesMostradas;
        
        for (int i = 0; i < numeroPosicionesReina; i++)
        {
            posicionesReina[i] = posicionesAux[i];
        }

        MuestraCasillasTorre();

        int aux = numeroPosicionesReina;
        numeroPosicionesReina += numeroPosicionesMostradas;
        
        for (int i = aux; i < numeroPosicionesReina; i++)
        {
            int aux2 = i - aux;
            posicionesReina[i] = posicionesAux[aux2];
        }

        posicionesAux = new int[numeroPosicionesReina];
        numeroPosicionesMostradas = numeroPosicionesReina;

        for(int i = 0; i < numeroPosicionesReina; i++)
        {
            posicionesAux[i] = posicionesReina[i];
        }
    }



    void EnemigoDecideAccion()
    {

    }



    void MueveAliado()
    {
        bool puedeMover = false;
        int nuevaPos = posY * 8 + posX;

        if (indiceFichaSeleccionada < 8) //peon
        {
            if (posX == fichasBlan[indiceFichaSeleccionada].posX)
            {
                if (posY == fichasBlan[indiceFichaSeleccionada].posY + 1)
                {
                    if (matrizTablero[posX, posY] == 0)
                    {
                        puedeMover = true;
                    }
                    else if (matrizTablero[posX, posY] == 1)
                    {
                        atacar = true;
                    }
                }
            }
            if (fichasBlan[indiceFichaSeleccionada].posY == 1)
            {
                if (posX == fichasBlan[indiceFichaSeleccionada].posX)
                {
                    if (posY == fichasBlan[indiceFichaSeleccionada].posY + 2)
                    {
                        if (matrizTablero[posX, posY] == 0)
                        {
                            puedeMover = true;
                        }
                        else if (matrizTablero[posX, posY] == 1)
                        {
                            atacar = true;
                        }
                    }
                }
            }
        }
        else if (indiceFichaSeleccionada < 10) //torre
        {
            if (posX == fichasBlan[indiceFichaSeleccionada].posX)
            {
                if (matrizTablero[posX, posY] == 0)
                {
                    puedeMover = true;
                }
                else if (matrizTablero[posX, posY] == 1)
                {
                    atacar = true;
                }
            }
            else if (posY == fichasBlan[indiceFichaSeleccionada].posY)
            {
                if (matrizTablero[posX, posY] == 0)
                {
                    puedeMover = true;
                }
                else if (matrizTablero[posX, posY] == 1)
                {
                    atacar = true;
                }
            }
        }
        else if (indiceFichaSeleccionada < 12) //caballo
        {
            if (posX == fichasBlan[indiceFichaSeleccionada].posX + 2 && posY == fichasBlan[indiceFichaSeleccionada].posY + 1)
            {
                if (matrizTablero[posX, posY] == 0)
                {
                    puedeMover = true;
                }
                else if (matrizTablero[posX, posY] == 1)
                {
                    atacar = true;
                }
            }
            else if (posX == fichasBlan[indiceFichaSeleccionada].posX + 2 && posY == fichasBlan[indiceFichaSeleccionada].posY - 1)
            {
                if (matrizTablero[posX, posY] == 0)
                {
                    puedeMover = true;
                }
                else if (matrizTablero[posX, posY] == 1)
                {
                    atacar = true;
                }
            }
            else if (posX == fichasBlan[indiceFichaSeleccionada].posX - 2 && posY == fichasBlan[indiceFichaSeleccionada].posY - 1)
            {
                if (matrizTablero[posX, posY] == 0)
                {
                    puedeMover = true;
                }
                else if (matrizTablero[posX, posY] == 1)
                {
                    atacar = true;
                }
            }
            else if (posX == fichasBlan[indiceFichaSeleccionada].posX - 2 && posY == fichasBlan[indiceFichaSeleccionada].posY + 1)
            {
                if (matrizTablero[posX, posY] == 0)
                {
                    puedeMover = true;
                }
                else if (matrizTablero[posX, posY] == 1)
                {
                    atacar = true;
                }
            }
            else if (posX == fichasBlan[indiceFichaSeleccionada].posX - 1 && posY == fichasBlan[indiceFichaSeleccionada].posY + 2)
            {
                if (matrizTablero[posX, posY] == 0)
                {
                    puedeMover = true;
                }
                else if (matrizTablero[posX, posY] == 1)
                {
                    atacar = true;
                }
            }
            else if (posX == fichasBlan[indiceFichaSeleccionada].posX + 1 && posY == fichasBlan[indiceFichaSeleccionada].posY + 2)
            {
                if (matrizTablero[posX, posY] == 0)
                {
                    puedeMover = true;
                }
                else if (matrizTablero[posX, posY] == 1)
                {
                    atacar = true;
                }
            }
            else if (posX == fichasBlan[indiceFichaSeleccionada].posX - 1 && posY == fichasBlan[indiceFichaSeleccionada].posY - 2)
            {
                if (matrizTablero[posX, posY] == 0)
                {
                    puedeMover = true;
                }
                else if (matrizTablero[posX, posY] == 1)
                {
                    atacar = true;
                }
            }
            else if (posX == fichasBlan[indiceFichaSeleccionada].posX + 1 && posY == fichasBlan[indiceFichaSeleccionada].posY - 2)
            {
                if (matrizTablero[posX, posY] == 0)
                {
                    puedeMover = true;
                }
                else if (matrizTablero[posX, posY] == 1)
                {
                    atacar = true;
                }
            }
        }
        else if (indiceFichaSeleccionada < 14) //alfil
        {
            int diferenciaX = Mathf.Abs(posX - fichasBlan[indiceFichaSeleccionada].posX);
            int diferenciaY = Mathf.Abs(posY - fichasBlan[indiceFichaSeleccionada].posY);

            if (diferenciaX == diferenciaY)
            {
                if (matrizTablero[posX, posY] == 0)
                {
                    puedeMover = true;
                }
                else if (matrizTablero[posX, posY] == 1)
                {
                    atacar = true;
                }
            }
        }
        else if (indiceFichaSeleccionada == 14) //Reina
        {
            int diferenciaX = Mathf.Abs(posX - fichasBlan[indiceFichaSeleccionada].posX);
            int diferenciaY = Mathf.Abs(posY - fichasBlan[indiceFichaSeleccionada].posY);

            if (diferenciaX == diferenciaY)
            {
                if (matrizTablero[posX, posY] == 0)
                {
                    puedeMover = true;
                }
                else if (matrizTablero[posX, posY] == 1)
                {
                    atacar = true;
                }
            }
            else if (posX == fichasBlan[indiceFichaSeleccionada].posX)
            {
                if (matrizTablero[posX, posY] == 0)
                {
                    puedeMover = true;
                }
                else if (matrizTablero[posX, posY] == 1)
                {
                    atacar = true;
                }
            }
            else if (posY == fichasBlan[indiceFichaSeleccionada].posY)
            {
                if (matrizTablero[posX, posY] == 0)
                {
                    puedeMover = true;
                }
                else if (matrizTablero[posX, posY] == 1)
                {
                    atacar = true;
                }
            }
        }
        else if (indiceFichaSeleccionada == 15) //Rey
        {
            if (posX == fichasBlan[indiceFichaSeleccionada].posX && posY == fichasBlan[indiceFichaSeleccionada].posY + 1)
            {
                if (matrizTablero[posX, posY] == 0)
                {
                    puedeMover = true;
                }
                else if (matrizTablero[posX, posY] == 1)
                {
                    atacar = true;
                }
            }
            else if (posX == fichasBlan[indiceFichaSeleccionada].posX && posY == fichasBlan[indiceFichaSeleccionada].posY - 1)
            {
                if (matrizTablero[posX, posY] == 0)
                {
                    puedeMover = true;
                }
                else if (matrizTablero[posX, posY] == 1)
                {
                    atacar = true;
                }
            }
            else if (posX == fichasBlan[indiceFichaSeleccionada].posX + 1 && posY == fichasBlan[indiceFichaSeleccionada].posY - 1)
            {
                if (matrizTablero[posX, posY] == 0)
                {
                    puedeMover = true;
                }
                else if (matrizTablero[posX, posY] == 1)
                {
                    atacar = true;
                }
            }
            else if (posX == fichasBlan[indiceFichaSeleccionada].posX + 1 && posY == fichasBlan[indiceFichaSeleccionada].posY + 1)
            {
                if (matrizTablero[posX, posY] == 0)
                {
                    puedeMover = true;
                }
                else if (matrizTablero[posX, posY] == 1)
                {
                    atacar = true;
                }
            }
            else if (posX == fichasBlan[indiceFichaSeleccionada].posX - 1 && posY == fichasBlan[indiceFichaSeleccionada].posY + 1)
            {
                if (matrizTablero[posX, posY] == 0)
                {
                    puedeMover = true;
                }
                else if (matrizTablero[posX, posY] == 1)
                {
                    atacar = true;
                }
            }
            else if (posX == fichasBlan[indiceFichaSeleccionada].posX - 1 && posY == fichasBlan[indiceFichaSeleccionada].posY - 1)
            {
                if (matrizTablero[posX, posY] == 0)
                {
                    puedeMover = true;
                }
                else if (matrizTablero[posX, posY] == 1)
                {
                    atacar = true;
                }
            }
            else if (posX == fichasBlan[indiceFichaSeleccionada].posX - 1 && posY == fichasBlan[indiceFichaSeleccionada].posY)
            {
                if (matrizTablero[posX, posY] == 0)
                {
                    puedeMover = true;
                }
                else if (matrizTablero[posX, posY] == 1)
                {
                    atacar = true;
                }
            }
            else if (posX == fichasBlan[indiceFichaSeleccionada].posX + 1 && posY == fichasBlan[indiceFichaSeleccionada].posY)
            {
                if (matrizTablero[posX, posY] == 0)
                {
                    puedeMover = true;
                }
                else if (matrizTablero[posX, posY] == 1)
                {
                    atacar = true;
                }
            }
        }

        if (puedeMover)
        {
            fichasBlancas.transform.GetChild(indiceFichaSeleccionada).transform.position = posiciones.transform.GetChild(nuevaPos).transform.position;
            DesactivaCasillasMostradas();
            mover = false;

            int posAuxX = fichasBlan[indiceFichaSeleccionada].posX;
            int posAuxY = fichasBlan[indiceFichaSeleccionada].posY;

            matrizIndicesFichas[posAuxX, posAuxY] = -1;
            matrizTablero[posAuxX, posAuxY] = 0;

            fichasBlan[indiceFichaSeleccionada].posX = posX;
            fichasBlan[indiceFichaSeleccionada].posY = posY;

            matrizIndicesFichas[posX, posY] = indiceFichaSeleccionada;
            matrizTablero[posX, posY] = 1;

            //print(matrizTablero[0, 1] + " " + matrizTablero[1, 1] + " " + matrizTablero[2, 1] + " " + matrizTablero[3, 1] + " " + matrizTablero[4, 1] + " " + matrizTablero[5, 1] + " " + matrizTablero[6, 1] + " " + matrizTablero[7, 1] + " ");
            //print(matrizTablero[0, 0] + " " + matrizTablero[1, 0] + " " + matrizTablero[2, 0] + " " + matrizTablero[3, 0] + " " + matrizTablero[4, 0] + " " + matrizTablero[5, 0] + " " + matrizTablero[6, 0] + " " + matrizTablero[7, 0] + " ");
        }
        else if (atacar)
        {
            CalculaDanio();
        }
    }



    void CalculaDanio()
    {
        int nuevaPos = posY * 8 + posX;
        bool victoria = false;

        if (indiceFichaSeleccionada < 8)
        {
            if (fichasNegr[matrizIndicesFichas[posX, posY]].escudo)
            {
                fichasNegr[matrizIndicesFichas[posX, posY]].escudo = false;
            }
            else
            {
                fichasNegr[matrizIndicesFichas[posX, posY]].vidas -= 1;
            }

            if (fichasNegr[matrizIndicesFichas[posX, posY]].vidas == 0)
            {
                victoria = true;
            }
        }
        else if (indiceFichaSeleccionada < 10)
        {
            if (matrizIndicesFichas[posX, posY] < 8)
            {
                if (fichasNegr[matrizIndicesFichas[posX, posY]].escudo)
                {
                    fichasNegr[matrizIndicesFichas[posX, posY]].escudo = false;
                }
                else
                {
                    fichasNegr[matrizIndicesFichas[posX, posY]].vidas -= 1;
                }

                if (fichasNegr[matrizIndicesFichas[posX, posY]].vidas == 0)
                {
                    victoria = true;
                }
            }
            else if (matrizIndicesFichas[posX, posY] < 10)
            {
                if (fichasNegr[matrizIndicesFichas[posX, posY]].escudo)
                {
                    fichasNegr[matrizIndicesFichas[posX, posY]].escudo = false;
                }
                else
                {
                    fichasNegr[matrizIndicesFichas[posX, posY]].vidas -= 1;
                }

                if (fichasNegr[matrizIndicesFichas[posX, posY]].vidas == 0)
                {
                    victoria = true;
                }
            }
            else if (matrizIndicesFichas[posX, posY] < 12)
            {
                if (fichasNegr[matrizIndicesFichas[posX, posY]].escudo)
                {
                    fichasNegr[matrizIndicesFichas[posX, posY]].escudo = false;
                }
                else
                {
                    fichasNegr[matrizIndicesFichas[posX, posY]].vidas -= 1;
                }

                if (fichasNegr[matrizIndicesFichas[posX, posY]].vidas == 0)
                {
                    victoria = true;
                }
            }
            else if (matrizIndicesFichas[posX, posY] < 14)
            {
                if (fichasNegr[matrizIndicesFichas[posX, posY]].escudo)
                {
                    fichasNegr[matrizIndicesFichas[posX, posY]].escudo = false;
                }
                else
                {
                    fichasNegr[matrizIndicesFichas[posX, posY]].vidas -= 2;
                }

                if (fichasNegr[matrizIndicesFichas[posX, posY]].vidas == 0)
                {
                    victoria = true;
                }
            }
            else if (matrizIndicesFichas[posX, posY] == 14)
            {
                if (fichasNegr[matrizIndicesFichas[posX, posY]].escudo)
                {
                    fichasNegr[matrizIndicesFichas[posX, posY]].escudo = false;
                }
                else
                {
                    fichasNegr[matrizIndicesFichas[posX, posY]].vidas -= 1;
                }

                if (fichasNegr[matrizIndicesFichas[posX, posY]].vidas == 0)
                {
                    victoria = true;
                }
            }
            else if (matrizIndicesFichas[posX, posY] == 15)
            {
                if (fichasNegr[matrizIndicesFichas[posX, posY]].escudo)
                {
                    fichasNegr[matrizIndicesFichas[posX, posY]].escudo = false;
                }
                else
                {
                    fichasNegr[matrizIndicesFichas[posX, posY]].vidas -= 1;
                }

                if (fichasNegr[matrizIndicesFichas[posX, posY]].vidas == 0)
                {
                    victoria = true;
                }
            }
        }
        else if (indiceFichaSeleccionada < 12)
        {
            if (matrizIndicesFichas[posX, posY] < 8)
            {
                if (fichasNegr[matrizIndicesFichas[posX, posY]].escudo)
                {
                    fichasNegr[matrizIndicesFichas[posX, posY]].escudo = false;
                }
                else
                {
                    fichasNegr[matrizIndicesFichas[posX, posY]].vidas -= 1;
                }

                if (fichasNegr[matrizIndicesFichas[posX, posY]].vidas == 0)
                {
                    victoria = true;
                }
            }
            else if (matrizIndicesFichas[posX, posY] < 10)
            {
                if (fichasNegr[matrizIndicesFichas[posX, posY]].escudo)
                {
                    fichasNegr[matrizIndicesFichas[posX, posY]].escudo = false;
                }
                else
                {
                    fichasNegr[matrizIndicesFichas[posX, posY]].vidas -= 2;
                }

                if (fichasNegr[matrizIndicesFichas[posX, posY]].vidas == 0)
                {
                    victoria = true;
                }
            }
            else if (matrizIndicesFichas[posX, posY] < 12)
            {
                if (fichasNegr[matrizIndicesFichas[posX, posY]].escudo)
                {
                    fichasNegr[matrizIndicesFichas[posX, posY]].escudo = false;
                }
                else
                {
                    fichasNegr[matrizIndicesFichas[posX, posY]].vidas -= 1;
                }

                if (fichasNegr[matrizIndicesFichas[posX, posY]].vidas == 0)
                {
                    victoria = true;
                }
            }
            else if (matrizIndicesFichas[posX, posY] < 14)
            {
                if (fichasNegr[matrizIndicesFichas[posX, posY]].escudo)
                {
                    fichasNegr[matrizIndicesFichas[posX, posY]].escudo = false;
                }
                else
                {
                    fichasNegr[matrizIndicesFichas[posX, posY]].vidas -= 2;
                }

                if (fichasNegr[matrizIndicesFichas[posX, posY]].vidas == 0)
                {
                    victoria = true;
                }
            }
            else if (matrizIndicesFichas[posX, posY] == 14)
            {
                if (fichasNegr[matrizIndicesFichas[posX, posY]].escudo)
                {
                    fichasNegr[matrizIndicesFichas[posX, posY]].escudo = false;
                }
                else
                {
                    fichasNegr[matrizIndicesFichas[posX, posY]].vidas -= 1;
                }

                if (fichasNegr[matrizIndicesFichas[posX, posY]].vidas == 0)
                {
                    victoria = true;
                }
            }
            else if (matrizIndicesFichas[posX, posY] == 15)
            {
                if (fichasNegr[matrizIndicesFichas[posX, posY]].escudo)
                {
                    fichasNegr[matrizIndicesFichas[posX, posY]].escudo = false;
                }
                else
                {
                    fichasNegr[matrizIndicesFichas[posX, posY]].vidas -= 1;
                }

                if (fichasNegr[matrizIndicesFichas[posX, posY]].vidas == 0)
                {
                    victoria = true;
                }
            }
        }
        else if (indiceFichaSeleccionada < 14)
        {
            if (matrizIndicesFichas[posX, posY] < 8)
            {

            }
            else if (matrizIndicesFichas[posX, posY] < 10)
            {

            }
            else if (matrizIndicesFichas[posX, posY] < 12)
            {

            }
            else if (matrizIndicesFichas[posX, posY] < 14)
            {

            }
            else if (matrizIndicesFichas[posX, posY] == 15)
            {

            }
        }
        else if (indiceFichaSeleccionada == 14)
        {
            if (matrizIndicesFichas[posX, posY] < 8)
            {

            }
            else if (matrizIndicesFichas[posX, posY] < 10)
            {

            }
            else if (matrizIndicesFichas[posX, posY] < 12)
            {

            }
            else if (matrizIndicesFichas[posX, posY] < 14)
            {

            }
            else if (matrizIndicesFichas[posX, posY] == 15)
            {

            }
        }
        else if (indiceFichaSeleccionada == 15)
        {
            if (matrizIndicesFichas[posX, posY] < 8)
            {

            }
            else if (matrizIndicesFichas[posX, posY] < 10)
            {

            }
            else if (matrizIndicesFichas[posX, posY] < 12)
            {

            }
            else if (matrizIndicesFichas[posX, posY] < 14)
            {

            }
            else if (matrizIndicesFichas[posX, posY] == 15)
            {

            }
        }

        if (victoria)
        {
            fichasBlancas.transform.GetChild(indiceFichaSeleccionada).transform.position = posiciones.transform.GetChild(nuevaPos).transform.position;
            fichasNegras.transform.GetChild(indiceFichaSeleccionada).gameObject.SetActive(false);

            int posAuxX = fichasBlan[indiceFichaSeleccionada].posX;
            int posAuxY = fichasBlan[indiceFichaSeleccionada].posY;

            matrizIndicesFichas[posAuxX, posAuxY] = -1;
            matrizTablero[posAuxX, posAuxY] = 0;

            fichasBlan[indiceFichaSeleccionada].posX = posX;
            fichasBlan[indiceFichaSeleccionada].posY = posY;

            matrizIndicesFichas[posX, posY] = indiceFichaSeleccionada;
            matrizTablero[posX, posY] = 1;
        }
    }



    void CalculaDanioEnemigo()
    {

    }



    void SituaFicha()
    {

    }



    void Rendirse()
    {

    }



    void CambiaControl()
    {
        if (!baseDeDatos.mandoActivo)
        {
            baseDeDatos.mandoActivo = true;

            ayuda.transform.GetChild(0).GetComponent<Image>().sprite = baseDeDatos.seleccionXBOX[0];
            ayuda.transform.GetChild(1).GetComponent<Image>().sprite = baseDeDatos.volverXBOX[0];
            ayuda.transform.GetChild(2).GetComponent<Image>().sprite = baseDeDatos.moverXBOX[0];
        }
        else
        {
            baseDeDatos.mandoActivo = false;

            ayuda.transform.GetChild(0).GetComponent<Image>().sprite = baseDeDatos.seleccionPC[0];
            ayuda.transform.GetChild(1).GetComponent<Image>().sprite = baseDeDatos.volverPC[0];
            ayuda.transform.GetChild(2).GetComponent<Image>().sprite = baseDeDatos.moverPC[0];
        }
    }



    void PromocionaFicha()
    {
        if(indicePromocion == 0)//Torre
        {

        }
        else if (indicePromocion == 1) //Alfil
        {

        }
        else if (indicePromocion == 2) //Caballo
        {

        }
        else
        {

        }
    }



    void ActivaMenu(bool activa)
    {
        if (activa)
        {
            posMenu = 1;
            interfazCombate.transform.GetChild(0).GetComponent<Image>().sprite = imagenMenu[1];
            interfazCombate.transform.GetChild(0).GetComponent<Image>().color = new Color(40f / 255f, 40f / 255f, 40f / 255f);
            interfazCombate.transform.GetChild(posMenu).GetComponent<Image>().sprite = imagenMenu[0];
            interfazCombate.transform.GetChild(2).GetComponent<Image>().sprite = imagenMenu[1];
        }

        menuActivo = activa;
        interfazCombate.SetActive(activa);
    }



    void ActivaPromocion(bool activa)
    {
        if (activa)
        {
            indicePromocion = 0;

            menuPromocion.transform.GetChild(0).GetComponent<Image>().sprite = imagenesPromocion[0];
            menuPromocion.transform.GetChild(1).GetComponent<Image>().sprite = imagenesPromocion[1];
            menuPromocion.transform.GetChild(2).GetComponent<Image>().sprite = imagenesPromocion[2];
            menuPromocion.transform.GetChild(3).GetComponent<Image>().sprite = imagenesPromocion[3];
        }

        promociona = activa;
        menuPromocion.SetActive(activa);
    }



    void ActivaMovimiento(bool activa)
    {
        if (activa)
        {
            atacar = false;
            mover = false;
            huir = false;
            opcionElegida = false;
            enemigoDecideJugada = false;
            resuelveJugadaJugador = resuelveJugadaEnemigo = false;
            turnoJugador = true;
            promociona = false;
        }

        seleccion.SetActive(activa);
    }



    void DesactivaCasillasMostradas()
    {
        for(int i = 0; i < numeroPosicionesMostradas; i++)
        {
            int aux = posicionesAux[i];
            posiciones.transform.GetChild(aux).GetChild(0).GetComponent<Image>().color = new Color(0f / 255f, 221f / 255f, 0f / 255f, 0f / 255f);
        }
    }
}
