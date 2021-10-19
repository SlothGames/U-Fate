using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class ControlJugador : MonoBehaviour 
{
    public enum movimiento
    {
        /*
            Última dirección de movimiento del jugador
        */
        ARRIBA,
        IZQUIERDA,
        DERECHA,
        ABAJO
    }


    public movimiento mover;
    Animator anim;
    Rigidbody2D rb2d;
    public Vector2 mov;

    public Canvas pausa;

    public Image confirmar;
    public Image flecha, flechaConf;

    int posFlecha, posFlechaConf;
    public int dinero = 0;
   

    MenuEquipo equipo;
    public GameObject menuEquipo;
    MenuMision misiones;
    public GameObject menuInventario;
    Inventario inventario;
    MusicaManager musica;
    BaseDatos baseDeDatos;
    ListaPociones listaPociones;
    ListaEsgrima listaEsgrima;

    bool combateActivo, menuPausa, equipoActivo, inventarioActivo, guardarActivo, salirActivo, iniciado, misionesActivo, pasandoMensaje;
    bool interroganteActivo, exclamacionActiva, conversacionActivo;
    public bool activado;
    public bool a, b, c, d, e;

    public float speed;

    public GameObject pos1, pos2, pos3, pos4, pos5, pos6;
    public GameObject posConf1, posConf2;
    public GameObject conversacion, interrogacion, exclamacion;
    public GameObject pantallaCarga;
    public GameObject gestorMusica;
    public GameObject menuMisiones;
    PantallaCarga carga;

    public Text muestraDinero;

    /// Control de dificultad 
    /////////////////////////////////////////////
    public int dificultad = 1;
    /* 
     * 0 --> Facil     
     * 1 --> Intermedio        
     * 2 --> Dificil
     * 3 --> Titan
    */

    /*
    int porcentajeDanio;
    public int puntuacionActual;
    int diferenciaNivel;
    int numeroTurnos;
    int numeroEnemigos;
    int numeroAliados;
    int numeroObjetosUsados;
    int numeroCombatesDesdeCura;

    bool usaMedico;
    bool ganaCombate;
    bool esJefe;
    bool huye;
    */

    bool pulsado;
    public bool teleportMostrado;

    float digitalX;
    float digitalY;


    void Awake()
    {
        activado = false;
        carga = pantallaCarga.GetComponent<PantallaCarga>();
        baseDeDatos = GameObject.Find("GameManager").GetComponent<BaseDatos>();
    }



    void Start ()
    {
        combateActivo = false;
        equipoActivo = false;
        inventarioActivo = false;
        guardarActivo = false;
        salirActivo = false;
        iniciado = false;
        menuPausa = false;
        misionesActivo = false;
        pulsado = false;
        teleportMostrado = false;
        a = true;

        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();  
        equipo = menuEquipo.GetComponent<MenuEquipo>();
        mover = movimiento.ABAJO;
        b = true;

        DesactivarConfirmar();
        DesactivarMenu();

        c = true;

        musica = gestorMusica.GetComponent<MusicaManager>();
        listaPociones = this.gameObject.GetComponent<ListaPociones>();
        listaEsgrima = this.gameObject.GetComponent<ListaEsgrima>();
        inventario = menuInventario.GetComponent<Inventario>();
        d = true;
        misiones = menuMisiones.GetComponent<MenuMision>();
        e = true;
        activado = true;
    }



    public void setCombateActivo(bool valor)
    {
        combateActivo = valor;
    }



    public void setMensajeActivo(bool valor)
    {
        if(valor == false)
        {
            StartCoroutine(EsperaCofre());
        }
        else
        {
            pasandoMensaje = valor;
        }
    }



    public void SetInterrogante (bool valor)
    {
        if(valor != interroganteActivo)
        {
            interroganteActivo = valor;
            interrogacion.SetActive(valor);
        }
    }



    public void SetExclamacion (bool valor)
    {
        if (valor != exclamacionActiva)
        {
            exclamacionActiva = valor;
            exclamacion.SetActive(valor);
        }
    }



    public void SetConversacion (bool valor)
    {
        if (valor != conversacionActivo)
        {
            conversacionActivo = valor;
            conversacion.SetActive(valor);
        }
    }



    void Update()
    {
        if (!carga.activo)
        {
            digitalX = Input.GetAxis("D-Horizontal");
            digitalY = Input.GetAxis("D-Vertical");

            mov = new Vector2(0, 0);

            if (!TextBox.on && !combateActivo && !menuPausa && !pasandoMensaje)
            {
                speed = 2f;

                if (Input.GetKey(KeyCode.M) || Input.GetButton("B"))
                {
                    speed *= 1.2f;
                }

                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || digitalY > 0)
                {
                    mov = new Vector2(0, speed);
                    mover = movimiento.ARRIBA;
                }
                else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || digitalY < 0)
                {
                    mov = new Vector2(0, -speed);
                    mover = movimiento.ABAJO;
                }
                else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || digitalX < 0)
                {
                    mov = new Vector2(-speed, 0);
                    mover = movimiento.IZQUIERDA;
                }
                else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || digitalX > 0)
                {
                    mov = new Vector2(speed, 0);
                    mover = movimiento.DERECHA;
                }
                else if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                {
                    ComprobarFrente();
                }
                else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonUp("Start"))
                {
                    musica.ProduceEfecto(25);
                    GeneraMapa();
                    muestraDinero.text = "" + dinero;
                    this.ActivarMenu();
                }
            }
            else if (menuPausa)
            {
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
                        cambiaControl();
                    }
                }
                else
                {
                    if (Input.GetButtonUp("A") || Input.GetButtonUp("B") || Input.GetButtonUp("X") || Input.GetButtonUp("Y") || Input.GetButtonUp("Start") || Input.GetButtonUp("Select") || (digitalY != 0) || (digitalX != 0))
                    {
                        cambiaControl();
                    }
                }

                if (salirActivo)
                {
                    if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || (digitalX < 0 && !pulsado))
                    {
                        pulsado = true;
                        musica.ProduceEfecto(11);
                        posFlechaConf--;
                        if (posFlechaConf < 0)
                        {
                            posFlechaConf = 1;
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || (digitalX > 0 && !pulsado))
                    {
                        pulsado = true;
                        musica.ProduceEfecto(11);
                        posFlechaConf++;
                        if (posFlechaConf > 1)
                        {
                            posFlechaConf = 0;
                        }
                    }

                    if (posFlechaConf == 0)
                    {
                        flechaConf.transform.position = posConf1.transform.position;
                    }
                    else
                    {
                        flechaConf.transform.position = posConf2.transform.position;
                    }

                    if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                    {
                        musica.ProduceEfecto(10);

                        if (posFlechaConf == 0)
                        {
                            if (posFlecha == 4)
                            {
                                SceneManager.LoadScene("Titulo Juego");
                            }
                            else
                            {
                                SalirDelJuego();
                            }
                        }
                        else if (posFlechaConf == 1)
                        {
                            DesactivarConfirmar();
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("B"))
                    {
                        musica.ProduceEfecto(12);
                        DesactivarConfirmar();
                    }
                }
                else if (equipoActivo)
                {
                    if (!equipo.activo && !iniciado)
                    {
                        equipo.ActivarMenu();
                        iniciado = true;
                        pausa.gameObject.SetActive(false);
                    }
                    else if (!equipo.activo && iniciado)
                    {
                        iniciado = false;
                        equipoActivo = false;
                        pausa.gameObject.SetActive(true);
                    }
                }
                else if (misionesActivo)
                {
                    if (!misiones.activo && !iniciado)
                    {
                        misiones.IniciaMenu();
                        iniciado = true;
                        pausa.gameObject.SetActive(false);
                    }
                    else if (!misiones.activo && iniciado)
                    {
                        iniciado = false;
                        misionesActivo = false;
                        pausa.gameObject.SetActive(true);
                    }
                }
                else if (inventarioActivo)
                {
                    if (!inventario.activo && !iniciado)
                    {
                        inventario.IniciarMenu(false);
                        iniciado = true;
                        pausa.gameObject.SetActive(false);
                    }
                    else if (!inventario.activo && iniciado)
                    {
                        iniciado = false;
                        inventarioActivo = false;
                        pausa.gameObject.SetActive(true);
                    }
                }
                else
                {
                    //pausa.transform.GetChild(8).GetComponent<Text>().text = "Pos: " + posFlecha;
                    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || (!pulsado && digitalY > 0))
                    {
                        pulsado = true;
                        musica.ProduceEfecto(11);
                        posFlecha--;

                        if (posFlecha < 0)
                        {
                            posFlecha = 5;
                        }
                        else if (posFlecha == 4)
                        {
                            posFlecha = 3;
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || (!pulsado && digitalY < 0))
                    {
                        pulsado = true;
                        musica.ProduceEfecto(11);
                        posFlecha++;

                        if (posFlecha > 5)
                        {
                            posFlecha = 0;
                        }
                        else if(posFlecha == 4)
                        {
                            posFlecha = 5;
                        }
                    }

                    if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonUp("B") || Input.GetButtonUp("Start"))
                    {
                        musica.ProduceEfecto(25);
                        this.DesactivarMenu();
                        posFlecha = 0;
                    }
                    else if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                    {
                        musica.ProduceEfecto(10);

                        if (posFlecha == 0)
                        {
                            DesactivarMenu();
                        }
                        else if (posFlecha == 1)
                        {
                            equipoActivo = true;
                        }
                        else if (posFlecha == 2)
                        {
                            inventarioActivo = true;
                        }
                        else if (posFlecha == 3)
                        {
                            misionesActivo = true;
                        }
                        else if (posFlecha == 4 || posFlecha == 5)
                        {
                            ActivarConfirmar();
                        }
                    }

                    if (posFlecha == 0)
                    {
                        flecha.transform.position = pos1.transform.position;
                    }
                    else if (posFlecha == 1)
                    {
                        flecha.transform.position = pos2.transform.position;
                    }
                    else if (posFlecha == 2)
                    {
                        flecha.transform.position = pos3.transform.position;
                    }
                    else if (posFlecha == 3)
                    {
                        flecha.transform.position = pos4.transform.position;
                    }
                    else if (posFlecha == 4)
                    {
                        flecha.transform.position = pos5.transform.position;
                    }
                    else if (posFlecha == 5)
                    {
                        flecha.transform.position = pos6.transform.position;
                    }
                }
            }

            /*
                En caso de que haya movimiento animamos al personaje
                En el momento que el movimiento es 0 se anula la animacion
            */
            if (mov != Vector2.zero)
            {
                Animacion();
                CalcularZonaM();
            }
            else
            {
                anim.SetBool("walk", false);
            }
        }
    }



    void FixedUpdate ()
    {
        /*
            Función de movimiento del jugador
        */
        rb2d.MovePosition (rb2d.position + mov * speed * Time.deltaTime);
    }



    void CalcularZonaM()
    {
        Ray2D r = new Ray2D(transform.position, Vector3.zero);
        RaycastHit2D hit = Physics2D.Raycast(r.origin, r.direction, 1f, 1 << 9);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("ZonaM"))
            {
                hit.collider.SendMessage("CalcularZonaM", SendMessageOptions.DontRequireReceiver);
            }
        }
    }



    void ComprobarFrente()
    {
        Ray2D r;

        if (mover == movimiento.ARRIBA)
            r = new Ray2D(transform.position, Vector3.up);
        else if (mover == movimiento.ABAJO)
            r = new Ray2D(transform.position, Vector3.down);
        else if (mover == movimiento.IZQUIERDA)
            r = new Ray2D(transform.position, Vector3.left);
        else
            r = new Ray2D(transform.position, Vector3.right);

        RaycastHit2D hit = Physics2D.Raycast(r.origin, r.direction, 0.5f, 1 << 9);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Cofre"))
            {
                pasandoMensaje = true;
                hit.transform.gameObject.tag = "Obstaculo";
                hit.collider.SendMessage("AbrirCofre", SendMessageOptions.DontRequireReceiver);
            }
            else if (hit.collider.CompareTag("NPC"))
            {
                hit.collider.SendMessage("Conversacion", SendMessageOptions.DontRequireReceiver);
                SetConversacion(false);
            }
            else if (hit.collider.CompareTag("Tienda"))
            {
                hit.collider.SendMessage("AbrirTienda", SendMessageOptions.DontRequireReceiver);
            }
            else if (hit.collider.CompareTag("Medico"))
            {
                hit.collider.SendMessage("IniciarMedico", SendMessageOptions.DontRequireReceiver);
            }
            else if (hit.collider.CompareTag("PuntoGuardado"))
            {
                hit.collider.SendMessage("IniciarPuntoGuardado", SendMessageOptions.DontRequireReceiver);
            }
            else if (hit.collider.CompareTag("Palanca"))
            {
                hit.collider.SendMessage("ActivarInterruptor", SendMessageOptions.DontRequireReceiver);
            }
            else if (hit.collider.CompareTag("SeleccionEquipo"))
            {
                hit.collider.SendMessage("IniciaSeleccion", SendMessageOptions.DontRequireReceiver);
            }
            else if (hit.collider.CompareTag("Caldero"))
            {
                listaPociones.ActivaMenu();
            }
            else if (hit.collider.CompareTag("Teleport"))
            {
                hit.collider.SendMessage("Conversacion", SendMessageOptions.DontRequireReceiver);
            }
            else if (hit.collider.CompareTag("Esgrima"))
            {
                listaEsgrima.ActivaMenu();
            }
        }
    }



    public void Animacion()
    {
        anim.SetFloat("movX", mov.x);
        anim.SetFloat("movY", mov.y);
        anim.SetBool("walk", true);
    }



    void DesactivarMenu()
    {
        menuPausa = false;
        pausa.gameObject.SetActive(menuPausa);
    }



    void ActivarMenu()
    {
        menuPausa = true;
        pausa.gameObject.SetActive(menuPausa);

        if (baseDeDatos.mandoActivo)
        {
            pausa.transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite = baseDeDatos.seleccionXBOX[0];
            pausa.transform.GetChild(4).GetChild(2).GetComponent<Image>().sprite = baseDeDatos.volverXBOX[0];
            pausa.transform.GetChild(4).GetChild(4).GetComponent<Image>().sprite = baseDeDatos.moverXBOX[0];
        }
        else
        {
            pausa.transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite = baseDeDatos.seleccionPC[0];
            pausa.transform.GetChild(4).GetChild(2).GetComponent<Image>().sprite = baseDeDatos.volverPC[0];
            pausa.transform.GetChild(4).GetChild(4).GetComponent<Image>().sprite = baseDeDatos.moverPC[0];
        }

        if(baseDeDatos.idioma == 1)
        {
            pausa.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>().text = "Continue";
            pausa.transform.GetChild(2).GetChild(1).GetChild(0).GetComponent<Text>().text = "Squad";
            pausa.transform.GetChild(2).GetChild(2).GetChild(0).GetComponent<Text>().text = "Inventory";
            pausa.transform.GetChild(2).GetChild(3).GetChild(0).GetComponent<Text>().text = "Missions";
            pausa.transform.GetChild(2).GetChild(4).GetChild(0).GetComponent<Text>().text = "Main Menu";
            pausa.transform.GetChild(2).GetChild(5).GetChild(0).GetComponent<Text>().text = "Exit";

            pausa.transform.GetChild(4).GetChild(1).GetComponent<Text>().text = "Select";
            pausa.transform.GetChild(4).GetChild(3).GetComponent<Text>().text = "Back";
            pausa.transform.GetChild(4).GetChild(5).GetComponent<Text>().text = "Move";

            pausa.transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<Text>().text = "Yes";
            pausa.transform.GetChild(3).GetChild(1).GetChild(0).GetComponent<Text>().text = "No";
            pausa.transform.GetChild(3).GetChild(2).GetComponent<Text>().text = "Do you want to leave?";
        }
        else
        {
            pausa.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>().text = "Continuar";
            pausa.transform.GetChild(2).GetChild(1).GetChild(0).GetComponent<Text>().text = "Equipo";
            pausa.transform.GetChild(2).GetChild(2).GetChild(0).GetComponent<Text>().text = "Objetos";
            pausa.transform.GetChild(2).GetChild(3).GetChild(0).GetComponent<Text>().text = "Misiones";
            pausa.transform.GetChild(2).GetChild(4).GetChild(0).GetComponent<Text>().text = "Menú Principal";
            pausa.transform.GetChild(2).GetChild(5).GetChild(0).GetComponent<Text>().text = "Salir";

            pausa.transform.GetChild(4).GetChild(1).GetComponent<Text>().text = "Seleccionar";
            pausa.transform.GetChild(4).GetChild(3).GetComponent<Text>().text = "Volver";
            pausa.transform.GetChild(4).GetChild(5).GetComponent<Text>().text = "Mover";

            pausa.transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<Text>().text = "Sí";
            pausa.transform.GetChild(3).GetChild(1).GetChild(0).GetComponent<Text>().text = "No";
            pausa.transform.GetChild(3).GetChild(2).GetComponent<Text>().text = "¿Seguro quieres salir?";
        }
    }



    void SalirDelJuego()
    {
        Application.Quit();
    }



    void ActivarConfirmar()
    {
        salirActivo = true;
        posFlechaConf = 1;
        confirmar.gameObject.SetActive(true);
    }



    void DesactivarConfirmar()
    {
        salirActivo = false;
        confirmar.gameObject.SetActive(false);
    }



    IEnumerator EsperaCofre()
    {
        yield return new WaitForSeconds(0.1f);
        pasandoMensaje = false;
    }


    /*
    public void UsaMedico()
    {
        usaMedico = true;

        //ActualizaPuntuacion();
    }
    */

    /*
    public void TerminaCombate(bool victoria, int diferenciaNivelC, int turno, float danio, int numeroAliadosC, int numeroEnemigosC, bool jefe, int numeroObjetos, bool huyeC)
    {
        huye = huyeC;

        if (!huye)
        {
            ganaCombate = victoria;
            numeroCombatesDesdeCura++;
            diferenciaNivel = diferenciaNivelC;
            numeroTurnos = turno;
            porcentajeDanio = (int)danio;
            numeroAliados = numeroAliadosC;
            numeroEnemigos = numeroEnemigosC;
            esJefe = jefe;
            numeroObjetosUsados = numeroObjetos;
        }

        ActualizaPuntuacion();
    }
    */

    /*
    public void ActualizaPuntuacion()
    {
        int aux = 0;

        if (usaMedico)
        {
            if (numeroCombatesDesdeCura >= 5)
            {
                aux -= 2;
            }
            else if (numeroCombatesDesdeCura == 4)
            {
                aux -= 3;
            }
            else if (numeroCombatesDesdeCura == 3)
            {
                aux -= 4;
            }
            else if (numeroCombatesDesdeCura == 2)
            {
                aux -= 5;
            }
            else if (numeroCombatesDesdeCura == 1)
            {
                aux -= 6;
            }

            numeroCombatesDesdeCura = 0;
            usaMedico = false;
        }
        else
        {
            if(diferenciaNivel > -7)
            {
                if (!huye)
                {
                    if (ganaCombate)
                    {
                        if (porcentajeDanio < 25)
                        {
                            aux += 6;
                        }
                        else if (porcentajeDanio < 50)
                        {
                            aux += 4;
                        }
                        else if (porcentajeDanio < 75)
                        {
                            aux += 2;
                        }
                        else if (porcentajeDanio < 100)
                        {
                            aux++;
                        }
                        else if (porcentajeDanio < 125)
                        {
                            aux--;
                        }
                        else if (porcentajeDanio < 150)
                        {
                            aux -= 2;
                        }
                        else if (porcentajeDanio < 200)
                        {
                            aux -= 4;
                        }
                        else 
                        {
                            aux -= 6;
                        }

                        if (!esJefe)
                        {
                            if (numeroTurnos < 5)
                            {
                                if(numeroEnemigos < numeroAliados)
                                {
                                    int diferencia = numeroAliados - numeroEnemigos;

                                    if(diferencia == 1)
                                    {
                                        if (diferenciaNivel < 0)
                                        {
                                            if (diferenciaNivel < -5)
                                            {
                                                aux += 3;
                                            }
                                            else
                                            {
                                                aux += 4;
                                            }
                                        }
                                        else if (diferenciaNivel < 5)
                                        {
                                            aux += 5;
                                        }
                                        else
                                        {
                                            aux += 6;
                                        }
                                    }
                                    else
                                    {
                                        if (diferenciaNivel < 0)
                                        {
                                            if (diferenciaNivel < -5)
                                            {
                                                aux += 2;
                                            }
                                            else
                                            {
                                                aux += 3;
                                            }
                                        }
                                        else if (diferenciaNivel < 5)
                                        {
                                            aux += 4;
                                        }
                                        else
                                        {
                                            aux += 5;
                                        }
                                    }
                                }
                                else if(numeroAliados == numeroEnemigos)
                                {
                                    if (diferenciaNivel < 0)
                                    {
                                        if (diferenciaNivel < -5)
                                        {
                                            aux += 4;
                                        }
                                        else
                                        {
                                            aux += 5;
                                        }
                                    }
                                    else if (diferenciaNivel < 5)
                                    {
                                        aux += 6;
                                    }
                                    else
                                    {
                                        aux += 7;
                                    }
                                }
                                else
                                {
                                    int diferencia = numeroAliados - numeroEnemigos;

                                    if (diferencia == 1)
                                    {
                                        if (diferenciaNivel < 0)
                                        {
                                            if (diferenciaNivel < -5)
                                            {
                                                aux += 5;
                                            }
                                            else
                                            {
                                                aux += 6;
                                            }
                                        }
                                        else if (diferenciaNivel < 5)
                                        {
                                            aux += 7;
                                        }
                                        else
                                        {
                                            aux += 8;
                                        }
                                    }
                                    else
                                    {
                                        if (diferenciaNivel < 0)
                                        {
                                            if (diferenciaNivel < -5)
                                            {
                                                aux += 6;
                                            }
                                            else
                                            {
                                                aux += 7;
                                            }
                                        }
                                        else if (diferenciaNivel < 5)
                                        {
                                            aux += 8;
                                        }
                                        else
                                        {
                                            aux += 9;
                                        }
                                    }
                                }
                            }
                            else if (numeroTurnos < 10)
                            {
                                if (numeroEnemigos < numeroAliados)
                                {
                                    int diferencia = numeroAliados - numeroEnemigos;

                                    if (diferencia == 1)
                                    {
                                        if (diferenciaNivel < 0)
                                        {
                                            if (diferenciaNivel < -5)
                                            {
                                                aux -= 3;
                                            }
                                            else
                                            {
                                                aux -= 2;
                                            }
                                        }
                                        else if (diferenciaNivel < 5)
                                        {
                                            aux -= 1;
                                        }
                                        else
                                        {
                                            aux += 1;
                                        }
                                    }
                                    else
                                    {
                                        if (diferenciaNivel < 0)
                                        {
                                            if (diferenciaNivel < -5)
                                            {
                                                aux -= 4;
                                            }
                                            else
                                            {
                                                aux -= 5;
                                            }
                                        }
                                        else if (diferenciaNivel < 5)
                                        {
                                            aux -= 2;
                                        }
                                        else
                                        {
                                            aux -= 1;
                                        }
                                    }
                                }
                                else if (numeroAliados == numeroEnemigos)
                                {
                                    if (diferenciaNivel < 0)
                                    {
                                        if (diferenciaNivel < -5)
                                        {
                                            aux += 2;
                                        }
                                        else
                                        {
                                            aux += 3;
                                        }
                                    }
                                    else if (diferenciaNivel < 5)
                                    {
                                        aux += 4;
                                    }
                                    else
                                    {
                                        aux += 5;
                                    }
                                }
                                else
                                {
                                    int diferencia = numeroAliados - numeroEnemigos;

                                    if (diferencia == 1)
                                    {
                                        if (diferenciaNivel < 0)
                                        {
                                            if (diferenciaNivel < -5)
                                            {
                                                aux += 3;
                                            }
                                            else
                                            {
                                                aux += 4;
                                            }
                                        }
                                        else if (diferenciaNivel < 5)
                                        {
                                            aux += 5;
                                        }
                                        else
                                        {
                                            aux += 6;
                                        }
                                    }
                                    else
                                    {
                                        if (diferenciaNivel < 0)
                                        {
                                            if (diferenciaNivel < -5)
                                            {
                                                aux += 4;
                                            }
                                            else
                                            {
                                                aux += 5;
                                            }
                                        }
                                        else if (diferenciaNivel < 5)
                                        {
                                            aux += 6;
                                        }
                                        else
                                        {
                                            aux += 7;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (numeroEnemigos < numeroAliados)
                                {
                                    int diferencia = numeroAliados - numeroEnemigos;

                                    if (diferencia == 1)
                                    {
                                        if (diferenciaNivel < 0)
                                        {
                                            if (diferenciaNivel < -5)
                                            {
                                                aux -= 5;
                                            }
                                            else
                                            {
                                                aux -= 4;
                                            }
                                        }
                                        else if (diferenciaNivel < 5)
                                        {
                                            aux -= 3;
                                        }
                                        else
                                        {
                                            aux -= 1;
                                        }
                                    }
                                    else
                                    {
                                        if (diferenciaNivel < 0)
                                        {
                                            if (diferenciaNivel < -5)
                                            {
                                                aux -= 6;
                                            }
                                            else
                                            {
                                                aux -= 7;
                                            }
                                        }
                                        else if (diferenciaNivel < 5)
                                        {
                                            aux -= 4;
                                        }
                                        else
                                        {
                                            aux -= 3;
                                        }
                                    }
                                }
                                else if (numeroAliados == numeroEnemigos)
                                {
                                    if (diferenciaNivel < 0)
                                    {
                                        if (diferenciaNivel < -5)
                                        {
                                            aux -= 1;
                                        }
                                        else
                                        {
                                            aux += 1;
                                        }
                                    }
                                    else if (diferenciaNivel < 5)
                                    {
                                        aux += 2;
                                    }
                                    else
                                    {
                                        aux += 3;
                                    }
                                }
                                else
                                {
                                    int diferencia = numeroAliados - numeroEnemigos;

                                    if (diferencia == 1)
                                    {
                                        if (diferenciaNivel < 0)
                                        {
                                            if (diferenciaNivel < -5)
                                            {
                                                aux += 1;
                                            }
                                            else
                                            {
                                                aux += 2;
                                            }
                                        }
                                        else if (diferenciaNivel < 5)
                                        {
                                            aux += 3;
                                        }
                                        else
                                        {
                                            aux += 4;
                                        }
                                    }
                                    else
                                    {
                                        if (diferenciaNivel < 0)
                                        {
                                            if (diferenciaNivel < -5)
                                            {
                                                aux += 2;
                                            }
                                            else
                                            {
                                                aux += 3;
                                            }
                                        }
                                        else if (diferenciaNivel < 5)
                                        {
                                            aux += 4;
                                        }
                                        else
                                        {
                                            aux += 5;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (numeroTurnos < 5)
                            {
                                if (numeroEnemigos < numeroAliados)
                                {
                                    int diferencia = numeroAliados - numeroEnemigos;

                                    if (diferencia == 1)
                                    {
                                        if (diferenciaNivel < 0)
                                        {
                                            if (diferenciaNivel < -5)
                                            {
                                                aux += 4;
                                            }
                                            else
                                            {
                                                aux += 5;
                                            }
                                        }
                                        else if (diferenciaNivel < 5)
                                        {
                                            aux += 6;
                                        }
                                        else
                                        {
                                            aux += 7;
                                        }
                                    }
                                    else
                                    {
                                        if (diferenciaNivel < 0)
                                        {
                                            if (diferenciaNivel < -5)
                                            {
                                                aux += 2;
                                            }
                                            else
                                            {
                                                aux += 3;
                                            }
                                        }
                                        else if (diferenciaNivel < 5)
                                        {
                                            aux += 4;
                                        }
                                        else
                                        {
                                            aux += 5;
                                        }
                                    }
                                }
                                else if (numeroAliados == numeroEnemigos)
                                {
                                    if (diferenciaNivel < 0)
                                    {
                                        if (diferenciaNivel < -5)
                                        {
                                            aux += 5;
                                        }
                                        else
                                        {
                                            aux += 6;
                                        }
                                    }
                                    else if (diferenciaNivel < 5)
                                    {
                                        aux += 7;
                                    }
                                    else
                                    {
                                        aux += 8;
                                    }
                                }
                                else
                                {
                                    int diferencia = numeroAliados - numeroEnemigos;

                                    if (diferencia == 1)
                                    {
                                        if (diferenciaNivel < 0)
                                        {
                                            if (diferenciaNivel < -5)
                                            {
                                                aux += 6;
                                            }
                                            else
                                            {
                                                aux += 7;
                                            }
                                        }
                                        else if (diferenciaNivel < 5)
                                        {
                                            aux += 8;
                                        }
                                        else
                                        {
                                            aux += 9;
                                        }
                                    }
                                    else
                                    {
                                        if (diferenciaNivel < 0)
                                        {
                                            if (diferenciaNivel < -5)
                                            {
                                                aux += 7;
                                            }
                                            else
                                            {
                                                aux += 8;
                                            }
                                        }
                                        else if (diferenciaNivel < 5)
                                        {
                                            aux += 9;
                                        }
                                        else
                                        {
                                            aux += 10;
                                        }
                                    }
                                }
                            }
                            else if (numeroTurnos < 10)
                            {
                                if (numeroEnemigos < numeroAliados)
                                {
                                    int diferencia = numeroAliados - numeroEnemigos;

                                    if (diferencia == 1)
                                    {
                                        if (diferenciaNivel < 0)
                                        {
                                            if (diferenciaNivel < -5)
                                            {
                                                aux -= 3;
                                            }
                                            else
                                            {
                                                aux -= 2;
                                            }
                                        }
                                        else if (diferenciaNivel < 5)
                                        {
                                            aux -= 1;
                                        }
                                        else
                                        {
                                            aux += 1;
                                        }
                                    }
                                    else
                                    {
                                        if (diferenciaNivel < 0)
                                        {
                                            if (diferenciaNivel < -5)
                                            {
                                                aux -= 4;
                                            }
                                            else
                                            {
                                                aux -= 5;
                                            }
                                        }
                                        else if (diferenciaNivel < 5)
                                        {
                                            aux -= 2;
                                        }
                                        else
                                        {
                                            aux -= 1;
                                        }
                                    }
                                }
                                else if (numeroAliados == numeroEnemigos)
                                {
                                    if (diferenciaNivel < 0)
                                    {
                                        if (diferenciaNivel < -5)
                                        {
                                            aux += 2;
                                        }
                                        else
                                        {
                                            aux += 3;
                                        }
                                    }
                                    else if (diferenciaNivel < 5)
                                    {
                                        aux += 4;
                                    }
                                    else
                                    {
                                        aux += 5;
                                    }
                                }
                                else
                                {
                                    int diferencia = numeroAliados - numeroEnemigos;

                                    if (diferencia == 1)
                                    {
                                        if (diferenciaNivel < 0)
                                        {
                                            if (diferenciaNivel < -5)
                                            {
                                                aux += 3;
                                            }
                                            else
                                            {
                                                aux += 4;
                                            }
                                        }
                                        else if (diferenciaNivel < 5)
                                        {
                                            aux += 5;
                                        }
                                        else
                                        {
                                            aux += 6;
                                        }
                                    }
                                    else
                                    {
                                        if (diferenciaNivel < 0)
                                        {
                                            if (diferenciaNivel < -5)
                                            {
                                                aux += 4;
                                            }
                                            else
                                            {
                                                aux += 5;
                                            }
                                        }
                                        else if (diferenciaNivel < 5)
                                        {
                                            aux += 6;
                                        }
                                        else
                                        {
                                            aux += 7;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (numeroEnemigos < numeroAliados)
                                {
                                    int diferencia = numeroAliados - numeroEnemigos;

                                    if (diferencia == 1)
                                    {
                                        if (diferenciaNivel < 0)
                                        {
                                            if (diferenciaNivel < -5)
                                            {
                                                aux -= 5;
                                            }
                                            else
                                            {
                                                aux -= 4;
                                            }
                                        }
                                        else if (diferenciaNivel < 5)
                                        {
                                            aux -= 3;
                                        }
                                        else
                                        {
                                            aux -= 1;
                                        }
                                    }
                                    else
                                    {
                                        if (diferenciaNivel < 0)
                                        {
                                            if (diferenciaNivel < -5)
                                            {
                                                aux -= 6;
                                            }
                                            else
                                            {
                                                aux -= 7;
                                            }
                                        }
                                        else if (diferenciaNivel < 5)
                                        {
                                            aux -= 4;
                                        }
                                        else
                                        {
                                            aux -= 3;
                                        }
                                    }
                                }
                                else if (numeroAliados == numeroEnemigos)
                                {
                                    if (diferenciaNivel < 0)
                                    {
                                        if (diferenciaNivel < -5)
                                        {
                                            aux -= 1;
                                        }
                                        else
                                        {
                                            aux += 1;
                                        }
                                    }
                                    else if (diferenciaNivel < 5)
                                    {
                                        aux += 2;
                                    }
                                    else
                                    {
                                        aux += 3;
                                    }
                                }
                                else
                                {
                                    int diferencia = numeroAliados - numeroEnemigos;

                                    if (diferencia == 1)
                                    {
                                        if (diferenciaNivel < 0)
                                        {
                                            if (diferenciaNivel < -5)
                                            {
                                                aux += 1;
                                            }
                                            else
                                            {
                                                aux += 2;
                                            }
                                        }
                                        else if (diferenciaNivel < 5)
                                        {
                                            aux += 3;
                                        }
                                        else
                                        {
                                            aux += 4;
                                        }
                                    }
                                    else
                                    {
                                        if (diferenciaNivel < 0)
                                        {
                                            if (diferenciaNivel < -5)
                                            {
                                                aux += 2;
                                            }
                                            else
                                            {
                                                aux += 3;
                                            }
                                        }
                                        else if (diferenciaNivel < 5)
                                        {
                                            aux += 4;
                                        }
                                        else
                                        {
                                            aux += 5;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!esJefe)
                        {
                            aux -= 75;
                        }
                        else
                        {
                            aux -= 50;
                        }
                    }
                }
                else
                {
                    aux -= 5;
                }

                if (numeroObjetosUsados == 3)
                {
                    aux--;
                }
                else if (numeroObjetosUsados == 4)
                {
                    aux -= 2;
                }
                else if (numeroObjetosUsados == 5)
                {
                    aux -= 3;
                }
                else if (numeroObjetosUsados == 6)
                {
                    aux -= 4;
                }
                else if (numeroObjetosUsados >= 7)
                {
                    aux -= 5;
                }
            }
            else
            {
                if (!ganaCombate)
                {
                    if (!esJefe)
                    {
                        aux -= 80;
                    }
                    else
                    {
                        aux -= 55;
                    }
                }
            }
        }

        puntuacionActual += aux;

        if(puntuacionActual < 0)
        {
            puntuacionActual = 0;
        }
        else if(puntuacionActual > 550)
        {
            puntuacionActual = 550;
        }


        if (puntuacionActual < 40)
        {
            dificultad = 1;
        }
        else if (puntuacionActual < 100)
        {
            dificultad = 2;
        }
        else if (puntuacionActual < 170)
        {
            dificultad = 3;
        }
        else if (puntuacionActual < 250)
        {
            dificultad = 4;
        }
        else if (puntuacionActual < 340)
        {
            dificultad = 5;
        }
        else if (puntuacionActual < 440)
        {
            dificultad = 6;
        }
        else if (puntuacionActual <= 550)
        {
            dificultad = 7;
        }

        usaMedico = false;
        ganaCombate = false;
        esJefe = false;
        huye = false;

        SistemaGuardado.GuardarPuntuacion(this);
    }
    */


    void cambiaControl()
    {
        if (!menuPausa)
        {
            if (baseDeDatos.mandoActivo)
            {
                baseDeDatos.mandoActivo = false;
            }
            else
            {
                baseDeDatos.mandoActivo = true;
            }
        }
        else
        {
            if (!baseDeDatos.mandoActivo)
            {
                baseDeDatos.mandoActivo = true;

                pausa.transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite = baseDeDatos.seleccionXBOX[0];
                pausa.transform.GetChild(4).GetChild(2).GetComponent<Image>().sprite = baseDeDatos.volverXBOX[0];
                pausa.transform.GetChild(4).GetChild(4).GetComponent<Image>().sprite = baseDeDatos.moverXBOX[0];
            }
            else
            {
                baseDeDatos.mandoActivo = false;

                pausa.transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite = baseDeDatos.seleccionPC[0];
                pausa.transform.GetChild(4).GetChild(2).GetComponent<Image>().sprite = baseDeDatos.volverPC[0];
                pausa.transform.GetChild(4).GetChild(4).GetComponent<Image>().sprite = baseDeDatos.moverPC[0];
            }
        }
    }



    void GeneraMapa()
    {
        int pos = baseDeDatos.indiceInicial + 3;

        pausa.transform.GetChild(1).GetChild(0).position = pausa.transform.GetChild(1).GetChild(pos).position;
        pausa.transform.GetChild(1).GetChild(1).position = pausa.transform.GetChild(1).GetChild(pos).position;

        int aux = baseDeDatos.indiceObjetivo + 3;
        pausa.transform.GetChild(1).GetChild(2).position = pausa.transform.GetChild(1).GetChild(aux).position;
    }



    public void EstableceFaccion(int faccion) //0 -- golpista 1 -- imperio 2 -- regente 3 -- Resistencia 4 -- R.Asalto 5 -- R.Especiales 6 -- R.Investigacion 7 -- Cupula 8 -- Nada 9 -- Anarquista
    {
        anim.SetBool("resistencia", false);
        anim.SetBool("sinFaccion", false);
        anim.SetBool("guardiaImperial", false);
        anim.SetBool("fuerzaAsalto", false);
        anim.SetBool("equipoInteligencia", false);
        anim.SetBool("fuerzasEspeciales", false);
        anim.SetBool("emperador", false);
        anim.SetBool("cupula", false);
        anim.SetBool("guardiaReal", false);

        baseDeDatos.faccion = faccion;

        switch (faccion)
        {
            case 0:
                anim.SetBool("emperador", true);
                break;
            case 1:
                anim.SetBool("guardiaImperial", true);
                break;
            case 2:
                anim.SetBool("guardiaReal", true);
                break;
            case 3:
                anim.SetBool("resistencia", true);
                break;
            case 4:
                anim.SetBool("fuerzaAsalto", true);
                break;
            case 5:
                anim.SetBool("fuerzasEspeciales", true);
                break;
            case 6:
                anim.SetBool("equipoInteligencia", true);
                break;
            case 7:
                anim.SetBool("cupula", true);
                break;
            case 8:
                anim.SetBool("sinFaccion", true);
                break;
        }
    }



    public int GetDificultad()
    {
        return dificultad;
    }



    public void SetDificultad(int valor)
    {
        dificultad = valor;
    }
}
