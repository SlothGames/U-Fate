using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class Menu : MonoBehaviour 
{
	public GameObject lista, flecha, menuCreditos, menuControles, dificultades, partidasGuardadas, confirmacion, fondo, nombreMenu;
    GameObject manager;
    public GameObject carga;
    Ajustes ajustes;
    //Intermediario inter;
    public Font fuente;

    int indice, indDif, indPG;
    int idioma;

    bool mueveFlecha;
    bool activo, creditos, controles, confirmacionAct, aceptar;
    bool dificultad, partidaGuardada;
    bool pulsado;
    bool[] partidaDisponible;
    bool introduceNombre;

    float digitalX;
    float digitalY;

    bool mandoActivo;
    //bool textoActivo;

    string nombre = "Victor";

    Sprite[] seleccionPC;
    Sprite[] moverPC;
    Sprite[] volverPC;
    Sprite[] bPC;
    Sprite[] xXBOX;
    Sprite[] seleccionXBOX;
    Sprite[] moverXBOX;
    Sprite[] volverXBOX;
    Sprite[] startPC;
    Sprite[] startXBOX;

    public Sprite[] interfazDif; //0 -- inactivo 1 -- activo
    public Sprite[] interfazPG; //0 -- inactivo 1 -- activo 
    public Sprite[] banderas; //0 -- golpista 1 -- imperio 2 -- regente 3 -- Resistencia 4 -- R.Asalto 5 -- R.Especiales 6 -- R.Investigacion 7 -- Cupula 
    public Sprite[] prota; //0 -- golpista 1 -- imperio 2 -- regente 3 -- Resistencia 4 -- R.Asalto 5 -- R.Especiales 6 -- R.Investigacion 7 -- Cupula 8 -- Nada

    //Introduce Nombre
    int posLetra;
    int posColumna;
    string[] abecedario;
    int[] nombrePorLetras;
    bool introduce;

    void Start ()
    {
        manager = GameObject.Find("Menu");
        ajustes = manager.GetComponent<Ajustes>();
        partidaDisponible = new bool[3];
        
        moverPC = Resources.LoadAll<Sprite>("Sprites/Interfaz/botones/PC/4direcciones");
        seleccionPC = Resources.LoadAll<Sprite>("Sprites/Interfaz/botones/PC/N");
        moverXBOX = Resources.LoadAll<Sprite>("Sprites/Interfaz/botones/XBOX/Mover");
        seleccionXBOX = Resources.LoadAll<Sprite>("Sprites/Interfaz/botones/XBOX/A");
        volverPC = Resources.LoadAll<Sprite>("Sprites/Interfaz/botones/PC/M");
        volverXBOX = Resources.LoadAll<Sprite>("Sprites/Interfaz/botones/XBOX/B");
        startPC = Resources.LoadAll<Sprite>("Sprites/Interfaz/botones/PC/Esc");
        startXBOX = Resources.LoadAll<Sprite>("Sprites/Interfaz/botones/XBOX/Start");
        xXBOX = Resources.LoadAll<Sprite>("Sprites/Interfaz/botones/XBOX/X");
        bPC = Resources.LoadAll<Sprite>("Sprites/Interfaz/botones/PC/B");

        manager.transform.GetChild(4).transform.GetChild(7).gameObject.SetActive(false);
        manager.transform.GetChild(4).transform.GetChild(8).gameObject.SetActive(false);

        indice = 0;
        indDif = indPG = 0;
         
        mueveFlecha = true;
        activo = true;
        creditos = false;
        dificultad = partidaGuardada = false;
        confirmacionAct = aceptar = false;

        menuCreditos.SetActive(false);
        menuControles.SetActive(false);
        dificultades.SetActive(false);
        partidasGuardadas.SetActive(false);

        
        //inter = FindObjectOfType<Intermediario>();
        confirmacion.SetActive(false);

        //mandoActivo = inter.mandoActivo;
        CambiaControl();

        IntroduceNombre(false);
        IniciaAbecedario();
    }



    void Update ()
    {
        digitalX = Input.GetAxis("D-Horizontal");
        digitalY = Input.GetAxis("D-Vertical");

        idioma = Intermediario.idiomaEstatico;

        if (pulsado)
        {
            if (digitalY == 0 && digitalX == 0)
            {
                pulsado = false;
            }
        }

        if (mandoActivo)
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

        if (activo)
        {
            if (mueveFlecha)
            {
                mueveFlecha = false;

                if (indice > 5)
                {
                    indice = 0;
                }
                else if (indice < 0)
                {
                    indice = 5;
                }

                flecha.transform.position = lista.transform.GetChild(indice + 1).transform.GetChild(0).transform.position;
            }

            if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
            {
                Accion();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || (!pulsado && digitalY < 0))
            {
                pulsado = true;
                mueveFlecha = true;
                indice++;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || (!pulsado && digitalY > 0))
            {
                pulsado = true;
                mueveFlecha = true;
                indice--;
            }
        }
        else
        {
            if (!creditos && !controles && !dificultad && !partidaGuardada && !introduceNombre)
            {
                if (ajustes.retroceder && !ajustes.activo)
                {
                    ajustes.retroceder = false;
                    activo = true;
                }
            }
            else if (controles)
            {
                if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("B") || Input.GetButtonUp("A"))
                {
                    controles = false;
                    menuControles.SetActive(creditos);
                    DesactivaLista(true);
                }
            }
            else if (creditos)
            {
                if(Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("B") || Input.GetButtonUp("A"))
                {
                    creditos = false;
                    DesactivaLista(true);
                    menuCreditos.SetActive(creditos);
                }
            }
            else if (dificultad)
            {
                if (mueveFlecha)
                {
                    mueveFlecha = false;

                    if (indDif > 3)
                    {
                        indDif = 0;
                    }
                    else if (indDif < 0)
                    {
                        indDif = 3;
                    }

                    int aux = indDif + 2;
                    dificultades.transform.GetChild(aux).GetComponent<Image>().sprite = interfazDif[1];
                }

                if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                {
                    Intermediario.dificultad = indDif;

                    ActivaDificultad(false);
                    IntroduceNombre(true);
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || (!pulsado && digitalY < 0))
                {
                    pulsado = true;
                    mueveFlecha = true;
                    
                    int aux = indDif + 2;
                    dificultades.transform.GetChild(aux).GetComponent<Image>().sprite = interfazDif[0];

                    indDif++;
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || (!pulsado && digitalY > 0))
                {
                    pulsado = true;
                    mueveFlecha = true;

                    int aux = indDif + 2;
                    dificultades.transform.GetChild(aux).GetComponent<Image>().sprite = interfazDif[0];

                    indDif--;
                }
                else if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("B"))
                {
                    DesactivaLista(true);
                    ActivaDificultad(false);
                }
            }
            else if (partidaGuardada)
            {
                if (!confirmacionAct)
                {
                    if (mueveFlecha)
                    {
                        mueveFlecha = false;

                        if (indPG > 2)
                        {
                            indPG = 0;
                        }
                        else if (indPG < 0)
                        {
                            indPG = 2;
                        }

                        int aux = indPG + 2;
                        partidasGuardadas.transform.GetChild(aux).GetComponent<Image>().sprite = interfazPG[1];
                    }

                    if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                    {
                        if (partidaDisponible[indPG])
                        {
                            DesactivarMenu();

                            Intermediario.cargar = true;
                            Intermediario.ficheroCarga = indPG;
                            Intermediario.partidaNueva = false;
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || (!pulsado && digitalY < 0))
                    {
                        pulsado = true;
                        mueveFlecha = true;

                        int aux = indPG + 2;
                        partidasGuardadas.transform.GetChild(aux).GetComponent<Image>().sprite = interfazPG[0];

                        indPG++;
                    }
                    else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || (!pulsado && digitalY > 0))
                    {
                        pulsado = true;
                        mueveFlecha = true;

                        int aux = indPG + 2;
                        partidasGuardadas.transform.GetChild(aux).GetComponent<Image>().sprite = interfazPG[0];

                        indPG--;
                    }
                    else if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("B"))
                    {
                        DesactivaLista(true);
                        ActivarPartidaGuardada(false);
                    }
                    else if (Input.GetKeyDown(KeyCode.B) || Input.GetButtonUp("X"))
                    {
                        if (partidaDisponible[indPG])
                        {
                            ActivaConfirmacion(true);
                        }
                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                    {
                        if (aceptar)
                        {
                            SistemaGuardado.BorrarFichero(indPG);
                            ActualizaInterfazPG(indPG, false);
                        }

                        ActivaConfirmacion(false);
                    }
                    else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || (!pulsado && digitalX != 0))
                    {
                        pulsado = true;

                        if (aceptar)
                        {
                            aceptar = false;
                            confirmacion.transform.GetChild(3).position = confirmacion.transform.GetChild(2).GetChild(1).position;
                        }
                        else
                        {
                            aceptar = true;
                            confirmacion.transform.GetChild(3).position = confirmacion.transform.GetChild(1).GetChild(1).position;
                        }
                    }
                }
                
            }
            else if (introduceNombre)
            {
                if (!introduce)
                {
                    if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || (!pulsado && digitalY < 0))
                    {
                        pulsado = true;
                        posLetra++;

                        if (posLetra > 29)
                        {
                            posLetra = 0;
                        }

                        if (posColumna == 0 && posLetra != 28 && posLetra != 29)
                        {
                            nombreMenu.transform.GetChild(0).GetComponent<Text>().text = abecedario[posLetra].ToUpper();
                        }
                        else
                        {
                            nombreMenu.transform.GetChild(posColumna).GetComponent<Text>().text = abecedario[posLetra];
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || (!pulsado && digitalY > 0))
                    {
                        pulsado = true;
                        posLetra--;

                        if (posLetra < 0)
                        {
                            posLetra = 29;
                        }

                        if (posColumna == 0 && posLetra != 28 && posLetra != 29)
                        {
                            nombreMenu.transform.GetChild(0).GetComponent<Text>().text = abecedario[posLetra].ToUpper();
                        }
                        else
                        {
                            nombreMenu.transform.GetChild(posColumna).GetComponent<Text>().text = abecedario[posLetra];
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                    {
                        if(posColumna == 0)
                        {
                            nombrePorLetras[posColumna] = posLetra;
                        }
                        else
                        {
                            nombrePorLetras[posColumna] = posLetra;
                        }

                        if(posLetra != 28)
                        {
                            posColumna++;
                        }

                        if (posColumna > 8 || posLetra == 28)
                        {
                            introduce = true;
                        }
                        else
                        {
                            nombreMenu.transform.GetChild(posColumna).GetComponent<Text>().text = abecedario[posLetra];
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("B"))
                    {
                        nombreMenu.transform.GetChild(posColumna).GetComponent<Text>().text = "";

                        posColumna--;

                        if(posColumna < 0)
                        {
                            nombreMenu.transform.GetChild(0).GetComponent<Text>().text = "A";
                            ActivaDificultad(true);
                            introduceNombre = false;
                            IntroduceNombre(false);
                        }
                        else if (posColumna == 0)
                        {
                            posColumna = 0;
                            nombreMenu.transform.GetChild(posColumna).GetComponent<Text>().text = abecedario[posLetra].ToUpper();
                        }
                        else
                        {
                            nombreMenu.transform.GetChild(posColumna).GetComponent<Text>().text = abecedario[posLetra];
                        }
                    }
                }
                else
                {
                    IntroduceNombre();
                }
            }
        }
    }



	void Accion()
    {
        activo = false;

        if(indice == 0)
        {
            DesactivaLista(false);
            ActivaDificultad(true);
        }
        else if(indice == 1)
        {
            CompruebaPartidaGuardada();
            DesactivaLista(false);

            ActivarPartidaGuardada(true);

        }
        else if (indice == 2)
        {
            DesactivaLista(false);
            controles = true;
            menuControles.SetActive(controles);
        }
        else if (indice == 3)
        {
            ajustes.ActivaMenu();
        }
        else if (indice == 4)
        {
            creditos = true;
            DesactivaLista(false);
            menuCreditos.SetActive(creditos);
        }
        else if (indice == 5)
        {
            Application.Quit();
        }
	}



    void DesactivarMenu()
    {
        activo = false;
        this.gameObject.SetActive(activo);
    }



    void CambiaControl()
    {
        if (mandoActivo)
        {
            mandoActivo = false;

            manager.transform.GetChild(4).transform.GetChild(2).GetComponent<Image>().sprite = seleccionPC[0];
            manager.transform.GetChild(4).transform.GetChild(4).GetComponent<Image>().sprite = volverPC[0];
            manager.transform.GetChild(4).transform.GetChild(6).GetComponent<Image>().sprite = moverPC[0];
            manager.transform.GetChild(4).transform.GetChild(8).GetComponent<Image>().sprite = bPC[0];

            manager.transform.GetChild(3).transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = seleccionPC[0];
            manager.transform.GetChild(3).transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = volverPC[0];
            manager.transform.GetChild(3).transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = moverPC[0];
            manager.transform.GetChild(3).transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = startPC[0];
        }
        else
        {
            mandoActivo = true;

            manager.transform.GetChild(4).transform.GetChild(2).GetComponent<Image>().sprite = seleccionXBOX[0];
            manager.transform.GetChild(4).transform.GetChild(4).GetComponent<Image>().sprite = volverXBOX[0];
            manager.transform.GetChild(4).transform.GetChild(6).GetComponent<Image>().sprite = moverXBOX[0];
            manager.transform.GetChild(4).transform.GetChild(8).GetComponent<Image>().sprite = xXBOX[0];

            manager.transform.GetChild(3).transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = seleccionXBOX[0];
            manager.transform.GetChild(3).transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = volverXBOX[0];
            manager.transform.GetChild(3).transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = moverXBOX[0];
            manager.transform.GetChild(3).transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = startXBOX[0];
        }
    }



    void DesactivaLista(bool valor)
    {
        activo = valor;
        fondo.SetActive(activo);
        lista.SetActive(activo);
    }



    void ActivaDificultad(bool valor)
    {
        dificultad = valor;

        if (dificultad)
        {
            dificultades.transform.GetChild(2).GetComponent<Image>().sprite = interfazDif[1];
            dificultades.transform.GetChild(3).GetComponent<Image>().sprite = interfazDif[0];
            dificultades.transform.GetChild(4).GetComponent<Image>().sprite = interfazDif[0];
            dificultades.transform.GetChild(5).GetComponent<Image>().sprite = interfazDif[0];

            indDif = 0;
        }

        dificultades.SetActive(dificultad);
    }



    void ActivarPartidaGuardada(bool valor)
    {
        partidaGuardada = valor;

        if (partidaGuardada)
        {
            manager.transform.GetChild(4).transform.GetChild(8).gameObject.SetActive(true);
            manager.transform.GetChild(4).transform.GetChild(7).gameObject.SetActive(true);

            partidasGuardadas.transform.GetChild(2).GetComponent<Image>().sprite = interfazPG[1];
            partidasGuardadas.transform.GetChild(3).GetComponent<Image>().sprite = interfazPG[0];
            partidasGuardadas.transform.GetChild(4).GetComponent<Image>().sprite = interfazPG[0];

            indPG = 0;
        }
        else
        {
            manager.transform.GetChild(4).transform.GetChild(8).gameObject.SetActive(false);
            manager.transform.GetChild(4).transform.GetChild(7).gameObject.SetActive(false);
        }

        partidasGuardadas.SetActive(partidaGuardada);
    }



    void ActualizaInterfazPG(int indicePG, bool existe)
    {
        int aux = indicePG + 2;
        
        if (!existe)
        {
            partidaDisponible[indicePG] = false;
            partidasGuardadas.transform.GetChild(aux).GetChild(1).gameObject.SetActive(false);
            partidasGuardadas.transform.GetChild(aux).GetChild(2).gameObject.SetActive(false);
            partidasGuardadas.transform.GetChild(aux).GetChild(3).gameObject.SetActive(false);
            partidasGuardadas.transform.GetChild(aux).GetChild(4).gameObject.SetActive(false);
            partidasGuardadas.transform.GetChild(aux).GetChild(5).gameObject.SetActive(false);
            partidasGuardadas.transform.GetChild(aux).GetChild(6).gameObject.SetActive(false);
            partidasGuardadas.transform.GetChild(aux).GetChild(7).gameObject.SetActive(false);
        }
        else
        {
            partidaDisponible[indicePG] = true;
            PlayerData data = SistemaGuardado.CargarJugador(indicePG);

            BDData dataBD = SistemaGuardado.CargarBD(indicePG);
            partidasGuardadas.transform.GetChild(aux).GetChild(1).gameObject.SetActive(true);
            partidasGuardadas.transform.GetChild(aux).GetChild(2).gameObject.SetActive(true);

            int faccion = dataBD.faccion;
            //0 -- golpista 1 -- imperio 2 -- regente 3 -- Resistencia 4 -- R.Asalto 5 -- R.Especiales 6 -- R.Investigacion 7 -- Cupula 8 -- Nada
            partidasGuardadas.transform.GetChild(aux).GetChild(2).GetComponent<Image>().sprite = prota[faccion];

            if (faccion == 8)
            {
                partidasGuardadas.transform.GetChild(aux).GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                partidasGuardadas.transform.GetChild(aux).GetChild(1).GetComponent<Image>().sprite = banderas[faccion];
            }
                
            partidasGuardadas.transform.GetChild(aux).GetChild(3).gameObject.SetActive(true);
            partidasGuardadas.transform.GetChild(aux).GetChild(3).GetComponent<Text>().text = dataBD.nombreProta;

            partidasGuardadas.transform.GetChild(aux).GetChild(4).gameObject.SetActive(true);
            partidasGuardadas.transform.GetChild(aux).GetChild(4).GetComponent<Text>().text = "LV: " + dataBD.nivel[0];

            partidasGuardadas.transform.GetChild(aux).GetChild(5).gameObject.SetActive(true);
            
            if (idioma == 1)
            {
                switch (data.dificultad)
                {
                    case 0:
                        partidasGuardadas.transform.GetChild(aux).GetChild(5).GetComponent<Text>().text = "Easy";
                        break;
                    case 1:
                        partidasGuardadas.transform.GetChild(aux).GetChild(5).GetComponent<Text>().text = "Medium";
                        break;
                    case 2:
                        partidasGuardadas.transform.GetChild(aux).GetChild(5).GetComponent<Text>().text = "Hard";
                        break;
                    case 3:
                        partidasGuardadas.transform.GetChild(aux).GetChild(5).GetComponent<Text>().text = "Titan";
                        break;
                }
            }
            else
            {
                switch (data.dificultad)
                {
                    case 0:
                        partidasGuardadas.transform.GetChild(aux).GetChild(5).GetComponent<Text>().text = "Facil";
                        break;
                    case 1:
                        partidasGuardadas.transform.GetChild(aux).GetChild(5).GetComponent<Text>().text = "Intermedio";
                        break;
                    case 2:
                        partidasGuardadas.transform.GetChild(aux).GetChild(5).GetComponent<Text>().text = "Difícil";
                        break;
                    case 3:
                        partidasGuardadas.transform.GetChild(aux).GetChild(5).GetComponent<Text>().text = "Titán";
                        break;
                }
            }
            

            partidasGuardadas.transform.GetChild(aux).GetChild(6).gameObject.SetActive(true);
            
            if(idioma == 1)
            {
                switch (dataBD.indiceInicial)
                {
                    case 0:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "Origin Town";
                        break;
                    case 1:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "R5";
                        break;
                    case 2:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "El Paso";
                        break;
                    case 3:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "R6";
                        break;
                    case 4:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "Pedrán";
                        break;
                    case 5:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "R7";
                        break;
                    case 6:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "R8";
                        break;
                    case 7:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "Forest Town";
                        break;
                    case 8:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "R9";
                        break;
                    case 9:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "River Town";
                        break;
                    case 10:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "University";
                        break;
                    case 11:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "R10";
                        break;
                    case 12:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "Canda";
                        break;
                    case 13:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "Hope Forest";
                        break;
                    case 14:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "R11";
                        break;
                    case 15:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "New University";
                        break;
                    case 17:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "Refuge Town";
                        break;
                    case 18:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "Big Grotto";
                        break;
                    case 19:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "Sand Town";
                        break;
                    case 20:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "R12";
                        break;
                    case 21:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "Manfa";
                        break;
                    case 22:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "R4";
                        break;
                    case 23:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "Great Temple of Ancia";
                        break;
                    case 24:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "R1";
                        break;
                    case 25:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "Imperial City";
                        break;
                    case 26:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "R2";
                        break;
                    case 27:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "Albay Town";
                        break;
                    case 28:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "R3";
                        break;
                    case 29:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "Porto Bello";
                        break;
                }
            }
            else
            {
                switch (dataBD.indiceInicial)
                {
                    case 0:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "Pueblo Origen";
                        break;
                    case 1:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "R5";
                        break;
                    case 2:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "El Paso";
                        break;
                    case 3:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "R6";
                        break;
                    case 4:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "Pedrán";
                        break;
                    case 5:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "R7";
                        break;
                    case 6:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "R8";
                        break;
                    case 7:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "Pueblo del Bosque";
                        break;
                    case 8:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "R9";
                        break;
                    case 9:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "Pueblo del Rio";
                        break;
                    case 10:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "Universidad";
                        break;
                    case 11:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "R10";
                        break;
                    case 12:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "Canda";
                        break;
                    case 13:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "Bosque Esperanza";
                        break;
                    case 14:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "R11";
                        break;
                    case 15:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "Nueva Universidad";
                        break;
                    case 17:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "Pueblo Refugio";
                        break;
                    case 18:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "Gran Gruta";
                        break;
                    case 19:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "Pueblo Arena";
                        break;
                    case 20:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "R12";
                        break;
                    case 21:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "Manfa";
                        break;
                    case 22:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "R4";
                        break;
                    case 23:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "Gran Templo de Ancia";
                        break;
                    case 24:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "R1";
                        break;
                    case 25:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "Ciudad Imperial";
                        break;
                    case 26:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "R2";
                        break;
                    case 27:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "Pueblo Albay";
                        break;
                    case 28:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "R3";
                        break;
                    case 29:
                        partidasGuardadas.transform.GetChild(aux).GetChild(6).GetComponent<Text>().text = "Porto Bello";
                        break;
                }
            }

            partidasGuardadas.transform.GetChild(aux).GetChild(7).gameObject.SetActive(true);

            string minutos;

            if (dataBD.minutos < 10)
            {
                minutos = "0" + dataBD.minutos;
            }
            else
            {
                minutos = "" + dataBD.minutos;
            }

            partidasGuardadas.transform.GetChild(aux).GetChild(7).GetComponent<Text>().text = dataBD.horas + ":" + minutos;
        }
    }



    void CompruebaPartidaGuardada()
    {
        string path;
        
        for (int i = 0; i < 3; i++)
        {
            if (i == 0)
            {
                path = Application.persistentDataPath + "/save.pe";
            }
            else if (i == 1)
            {
                path = Application.persistentDataPath + "/save1.pe";
            }
            else
            {
                path = Application.persistentDataPath + "/save2.pe";
            }

            if (File.Exists(path))
            {
                ActualizaInterfazPG(i, true);
            }
            else
            {
                ActualizaInterfazPG(i, false);
            }
        }
    }



    void ActivaConfirmacion(bool activar)
    {
        confirmacionAct = activar;
        confirmacion.SetActive(activar);
        aceptar = false;

        if (activar)
        {
            if(idioma == 0)
            {
                confirmacion.transform.GetChild(0).GetComponent<Text>().text = "¿Seguro quieres borrar la partida? \n Esto eliminará los datos guardados";
                confirmacion.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = "No";
                confirmacion.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "Sí";
            }
            else
            {
                confirmacion.transform.GetChild(0).GetComponent<Text>().text = "Are you sure you want to delete the game? \n This will delete any previous data";
                confirmacion.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = "No";
                confirmacion.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "Yes";
            }

            confirmacion.transform.GetChild(3).position = confirmacion.transform.GetChild(2).GetChild(1).position;
        }
    }



    void IntroduceNombre(bool activar)
    {
        introduceNombre = activar;
        //textoActivo = false;

        this.transform.GetChild(8).gameObject.SetActive(activar);
        posColumna = 0;
        posLetra = 0;

        nombrePorLetras = new int[9];
        introduce = false;
    }



    void IniciaAbecedario()
    {
        abecedario = new string[30];

        abecedario[0] = "a";
        abecedario[1] = "b";
        abecedario[2] = "c";
        abecedario[3] = "d";
        abecedario[4] = "e";
        abecedario[5] = "f";
        abecedario[6] = "g";
        abecedario[7] = "h";
        abecedario[8] = "i";
        abecedario[9] = "j";
        abecedario[10] = "k";
        abecedario[11] = "l";
        abecedario[12] = "m";
        abecedario[13] = "n";
        abecedario[14] = "ñ";
        abecedario[15] = "o";
        abecedario[16] = "p";
        abecedario[17] = "q";
        abecedario[18] = "r";
        abecedario[19] = "s";
        abecedario[20] = "t";
        abecedario[21] = "u";
        abecedario[22] = "v";
        abecedario[23] = "w";
        abecedario[24] = "x";
        abecedario[25] = "y";
        abecedario[26] = "z";
        abecedario[27] = "ç";
        abecedario[28] = "END";
        abecedario[29] = " ";
    }



    void IntroduceNombre()
    {
        if(posColumna != 0)
        {
            nombre = "";

            for (int i = 0; i < posColumna; i++)
            {
                if(nombrePorLetras[i] != 28)
                {
                    if(i == 0)
                    {
                        nombre += abecedario[nombrePorLetras[i]].ToUpper();
                    }
                    else
                    {
                        nombre += abecedario[nombrePorLetras[i]];
                    }
                }
            }
        }

        DesactivarMenu();

        Intermediario.partidaNueva = true;
        Intermediario.cargar = false;
        Intermediario.nombre = nombre;
    }
}