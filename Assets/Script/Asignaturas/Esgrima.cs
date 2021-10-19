using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Esgrima : MonoBehaviour
{
    bool activo;
    bool completado;
    bool victoria;
    bool salir;
    bool cuentaInicial;
    bool tomaDecision;
    bool direccionElegida;
    bool ataqueElegido;
    bool fallaDireccion;
    bool fallaAtaque;
    bool muestraAtaques;
    bool direccionesMostradas;
    bool decisionJugador;
    bool resolucion;
    bool limpiando;
    bool reinicia;

    int indiceMision;
    int puntuacionLograda;
    int numeroVidas;
    int numeroVidasEnemigo;
    int dificultad;
    int[] patrones; //0 arriba, 1 izquierda, 2 derecha, 3 abajo
    int numeroPatrones;
    int[] tiempoReaccion;
    float tiempoDecideDireccion;
    float tiempoDecideAtaque;
    int patronActual;
    int direccionJugador;
    int direccionEnemigo;
    float cuentaAtras;
    int ataqueAliado; //0 piedra, 1 tijera, 2 papel
    int ataqueEnemigo;
    int experiencia;

    float digitalX;
    float digitalY;
    bool pulsado;

    BaseDatos baseDeDatos;
    ControlJugador controlJugador;
    MusicaManager efectos;
    MusicaManager musica;

    public Sprite corazonVacio, corazonLleno;
    Sprite[] imagenAliado, imagenEnemigo;



    void Start()
    {
        activo = false;
        victoria = false;
        cuentaInicial = false;

        baseDeDatos = GameObject.Find("GameManager").GetComponent<BaseDatos>();
        controlJugador = GameObject.Find("Player").GetComponent<ControlJugador>();
        efectos = GameObject.Find("EfectosSonido").GetComponent<MusicaManager>();
        musica = GameObject.Find("Musica").GetComponent<MusicaManager>();

        tiempoReaccion = new int[10];
        tiempoReaccion[0] = 3;
        patrones = new int[4];
        pulsado = false;
        resolucion = false;
        limpiando = false;

        DesactivaMenu();
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

            if (!cuentaInicial)
            {
                if (!completado)
                {
                    if (!ataqueElegido && !tomaDecision && !resolucion)
                    {
                        tiempoDecideAtaque -= Time.deltaTime;
                        this.transform.GetChild(2).GetComponent<Text>().text = "Time: " + tiempoDecideAtaque.ToString("f0");

                        if (Input.GetButtonUp("B") || Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("A") || Input.GetKeyDown(KeyCode.N)
                                        || Input.GetButtonUp("Y") || Input.GetKeyDown(KeyCode.V))
                        {
                            ataqueElegido = true;
                            muestraAtaques = true;
                            EnemigoDecideAtaque();

                            if (Input.GetButtonUp("B") || Input.GetKeyDown(KeyCode.M))
                            {
                                ataqueAliado = 2;
                            }
                            else if (Input.GetButtonUp("A") || Input.GetKeyDown(KeyCode.N))
                            {
                                ataqueAliado = 1;
                            }
                            else
                            {
                                ataqueAliado = 0;
                            }
                        }

                        if(tiempoDecideAtaque <= 0 && !ataqueElegido)
                        {
                            resolucion = true;
                            StartCoroutine(LimpiaInterfaz(true));
                        }
                    }
                    else if (ataqueElegido && !tomaDecision && ! resolucion)
                    {
                        if (muestraAtaques)
                        {
                            StartCoroutine(MuestraAtaque());
                        }
                    }
                    else if (tomaDecision && !resolucion)
                    {
                        if (fallaAtaque)
                        {
                            if (direccionesMostradas)
                            {
                                tiempoDecideDireccion -= Time.deltaTime;
                                this.transform.GetChild(2).GetComponent<Text>().text = "Time: " + tiempoDecideDireccion.ToString("f0");

                                if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || (!pulsado && digitalY < 0) ||
                                    Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || (!pulsado && digitalY > 0) ||
                                    Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || (!pulsado && digitalX < 0) ||
                                    Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || (!pulsado && digitalX > 0))
                                {
                                    this.transform.GetChild(7).GetChild(4).gameObject.SetActive(true);
                                    direccionElegida = true;
                                    tomaDecision = false;
                                    resolucion = true;

                                    if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || (!pulsado && digitalY < 0))
                                    {
                                        patronActual = 3;
                                        this.transform.GetChild(7).GetChild(4).GetComponent<Image>().sprite = baseDeDatos.abajoPC[0];
                                    }
                                    else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || (!pulsado && digitalY > 0))
                                    {
                                        patronActual = 0;
                                        this.transform.GetChild(7).GetChild(4).GetComponent<Image>().sprite = baseDeDatos.arribaPC[0];
                                    }
                                    else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || (!pulsado && digitalX < 0))
                                    {
                                        patronActual = 1;
                                        this.transform.GetChild(7).GetChild(4).GetComponent<Image>().sprite = baseDeDatos.izquierdaPC[0];
                                    }
                                    else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || (!pulsado && digitalX > 0))
                                    {
                                        patronActual = 2;
                                        this.transform.GetChild(7).GetChild(4).GetComponent<Image>().sprite = baseDeDatos.derechaPC[0];
                                    }
                                }

                                if(tiempoDecideDireccion <= 0 && !direccionElegida)
                                {
                                    resolucion = true;
                                    StartCoroutine(LimpiaInterfaz(true));
                                }
                            }
                        }
                        else
                        {
                            if (!decisionJugador)
                            {
                                tiempoDecideDireccion -= Time.deltaTime;
                                this.transform.GetChild(2).GetComponent<Text>().text = "Time: " + tiempoDecideDireccion.ToString("f0");

                                if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || (!pulsado && digitalY < 0) ||
                                        Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || (!pulsado && digitalY > 0) ||
                                        Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || (!pulsado && digitalX < 0) ||
                                        Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || (!pulsado && digitalX > 0))
                                {
                                    this.transform.GetChild(7).gameObject.SetActive(true);
                                    direccionElegida = true;
                                    decisionJugador = true;

                                    if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || (!pulsado && digitalY < 0))
                                    {
                                        direccionJugador = 3;
                                    }
                                    else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || (!pulsado && digitalY > 0))
                                    {
                                        direccionJugador = 0;
                                    }
                                    else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || (!pulsado && digitalX < 0))
                                    {
                                        direccionJugador = 1;
                                    }
                                    else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || (!pulsado && digitalX > 0))
                                    {
                                        direccionJugador = 2;
                                    }

                                    StartCoroutine(MuestraDireccion());
                                }
                            }
                            else
                            {
                                EnemigoRespondeDireccion();
                                resolucion = true;
                                tomaDecision = false;
                            }

                        }
                    }
                    else if (resolucion)
                    {
                        if (!limpiando)
                        {
                            if (fallaAtaque)
                            {
                                int k = 0;
                                fallaDireccion = false;

                                while (k < numeroPatrones && !fallaDireccion)
                                {
                                    if (patronActual == patrones[k])
                                    {
                                        fallaDireccion = true;
                                    }

                                    k++;
                                }
                            }
                            else
                            {
                                if (direccionEnemigo == direccionJugador)
                                {
                                    fallaDireccion = true;
                                }
                                else
                                {
                                    fallaDireccion = false;
                                }
                            }

                            StartCoroutine(LimpiaInterfaz(false));
                        }
                    }
                }
                else
                {
                    if (salir)
                    {
                        if (Input.GetButtonUp("B") || Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("A") || Input.GetKeyDown(KeyCode.N))
                        {
                            DesactivaMenu();
                            musica.VuelveMusica();
                        }
                    }
                }
            }
            else
            {
                cuentaAtras -= Time.deltaTime;
                this.transform.GetChild(10).GetComponent<Text>().text = "" + cuentaAtras.ToString("f0");

                if (cuentaAtras <= 0)
                {
                    this.transform.GetChild(10).GetComponent<Text>().text = "" + cuentaAtras.ToString("f0");
                    this.transform.GetChild(10).gameObject.SetActive(false);
                    cuentaInicial = false;
                    EstableceMensaje(0);
                }
            }

            if(puntuacionLograda < 0)
            {
                puntuacionLograda = 0;
                this.transform.GetChild(0).GetComponent<Text>().text = "Act: " + puntuacionLograda;
            }
        }
    }



    void ActivaMenu()
    {
        efectos.ProduceEfecto(1);
        activo = true;
        this.transform.GetChild(10).gameObject.SetActive(true);
        this.transform.GetChild(11).gameObject.SetActive(false);
        this.transform.GetChild(12).gameObject.SetActive(false);

        this.gameObject.SetActive(activo);
        
        musica.CambiaCancion(17);
        completado = false;
        cuentaAtras = 3;
        cuentaInicial = true;
        ataqueElegido = false;
        tomaDecision = false;
        resolucion = false;
        direccionesMostradas = false;
        limpiando = false;
        victoria = false;
        salir = false;
        direccionElegida = false;
        fallaDireccion = false;
        fallaAtaque = false;
        muestraAtaques = false;
        decisionJugador = false;
        reinicia = false;

        for (int i = 0; i < 3; i++) //Colorea oscuro los botones
        {
            this.transform.GetChild(5).GetChild(i).GetComponent<Image>().color = new Color(50f / 255f, 50f / 255f, 50f / 255f, 255f / 255f);
            this.transform.GetChild(6).GetChild(i).GetComponent<Image>().color = new Color(50f / 255f, 50f / 255f, 50f / 255f, 255f / 255f);
        }

        if (baseDeDatos.mandoActivo)
        {
            this.transform.GetChild(5).GetChild(0).GetComponent<Image>().sprite = baseDeDatos.yXBOX[0];
            this.transform.GetChild(5).GetChild(1).GetComponent<Image>().sprite = baseDeDatos.seleccionXBOX[0];
            this.transform.GetChild(5).GetChild(2).GetComponent<Image>().sprite = baseDeDatos.volverXBOX[0];
        }
        else
        {
            this.transform.GetChild(5).GetChild(0).GetComponent<Image>().sprite = baseDeDatos.vPC[0];
            this.transform.GetChild(5).GetChild(1).GetComponent<Image>().sprite = baseDeDatos.seleccionPC[0];
            this.transform.GetChild(5).GetChild(2).GetComponent<Image>().sprite = baseDeDatos.volverPC[0];
        }

        puntuacionLograda = 0;
        this.transform.GetChild(0).GetComponent<Text>().text = "Act: " + puntuacionLograda;
        this.transform.GetChild(0).GetComponent<Text>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
        this.transform.GetChild(1).GetComponent<Text>().text = "Rec: " + baseDeDatos.puntuacionRecordEsgrima[indiceMision];
        this.transform.GetChild(1).GetComponent<Text>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);

        for(int i = 0; i < 3; i++)
        {
            this.transform.GetChild(3).GetChild(i).GetComponent<Image>().sprite = corazonLleno;
            this.transform.GetChild(4).GetChild(i).GetComponent<Image>().sprite = corazonLleno;
        }

        numeroVidas = 3;
        numeroVidasEnemigo = 3;
        EstableceMensaje(-1);

        for(int i = 0; i < 4; i++)
        {
            this.transform.GetChild(7).GetChild(i).GetComponent<Image>().color = new Color(50f / 255f, 50f / 255f, 50f / 255f, 255f / 255f);
        }

        this.transform.GetChild(7).GetChild(4).GetChild(0).gameObject.SetActive(false);
        this.transform.GetChild(7).GetChild(4).GetChild(1).gameObject.SetActive(false);
        this.transform.GetChild(7).GetChild(4).gameObject.SetActive(false);
    }



    void DesactivaMenu()
    {
        activo = false;
        salir = false;
        
        this.gameObject.SetActive(activo);
        this.transform.GetChild(11).gameObject.SetActive(false);
        controlJugador.setMensajeActivo(false);
    }



    public void IniciaEvento(int indice, int experienciaEve)
    {
        experiencia = experienciaEve;
        indiceMision = indice;
        patronActual = 0;
        ataqueElegido = false;
        muestraAtaques = false;
        tomaDecision = false;
        decisionJugador = false;

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


        switch (indice)
        {
            case 0:
                dificultad = 0;
                imagenEnemigo = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Lionetta");
                break;
            case 1:
                dificultad = 4;
                imagenEnemigo = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Lionetta");
                break;

        }

        this.transform.GetChild(8).GetComponent<Image>().sprite = imagenAliado[1];
        this.transform.GetChild(9).GetComponent<Image>().sprite = imagenEnemigo[1];

        ReiniciaTiempos();
        ActivaMenu();
    }



    IEnumerator MuestraAtaque()
    {
        muestraAtaques = false;

        this.transform.GetChild(5).GetChild(ataqueAliado).GetComponent<Image>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
        this.transform.GetChild(6).GetChild(ataqueEnemigo).GetComponent<Image>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);

        int resultado = CompruebaGanador();

        yield return new WaitForSeconds(1);

        int mensaje;

        if(resultado == 0)
        {
            mensaje = 1;
        }
        else if (resultado == -1)
        {
            mensaje = 3;
        }
        else
        {
            mensaje = 2;
        }

        EstableceMensaje(mensaje);

        this.transform.GetChild(5).GetChild(ataqueAliado).GetComponent<Image>().color = new Color(50f / 255f, 50f / 255f, 50f / 255f, 255f / 255f);
        this.transform.GetChild(6).GetChild(ataqueEnemigo).GetComponent<Image>().color = new Color(50f / 255f, 50f / 255f, 50f / 255f, 255f / 255f);

        tomaDecision = true;
        direccionesMostradas = false;

        if (resultado == 0)
        {
            ataqueElegido = false;
            tomaDecision = false;
            yield return new WaitForSeconds(0.5f);
            EstableceMensaje(0);
        }
        else if (resultado == 1)
        {
            fallaAtaque = false;
        }
        else
        {
            fallaAtaque = true;
            EnemigoDecideDireccion();
            StartCoroutine(MuestraDireccion());
        }
    }



    IEnumerator Fin()
    {
        musica.ParaMusica();
        completado = true;

        yield return new WaitForSeconds(0.1f);

        this.transform.GetChild(11).GetChild(1).GetComponent<Text>().text = "Ganador";

        if (victoria)
        {
            efectos.ProduceEfecto(19);

            int aux = 500 * numeroVidas + 500 * dificultad;
            puntuacionLograda += aux;

            this.transform.GetChild(11).GetChild(0).GetComponent<Image>().sprite = imagenAliado[1];

            if (baseDeDatos.puntuacionRecordEsgrima[indiceMision] < puntuacionLograda)
            {
                baseDeDatos.puntuacionRecordEsgrima[indiceMision] = puntuacionLograda;
                this.transform.GetChild(12).gameObject.SetActive(true);
            }
        }
        else
        {
            this.transform.GetChild(11).GetChild(0).GetComponent<Image>().sprite = imagenEnemigo[1];

            efectos.ProduceEfecto(20);
        }
        this.transform.GetChild(0).GetComponent<Text>().text = "Act: " + puntuacionLograda;

        this.transform.GetChild(11).gameObject.SetActive(true);

        yield return new WaitForSeconds(1);

        for (int j = 0; j < baseDeDatos.numeroIntegrantesEquipo; j++)
        {
            if (baseDeDatos.equipoAliado[j].vidaActual > 0)
            {
                baseDeDatos.equipoAliado[j].experiencia += experiencia;
            }

            if (baseDeDatos.equipoAliado[j].experiencia >= baseDeDatos.equipoAliado[j].proximoNivel)
            {
                while (baseDeDatos.equipoAliado[j].experiencia >= baseDeDatos.equipoAliado[j].proximoNivel)
                {
                    baseDeDatos.SubirNivelAliado(j);
                }
            }
        }


        salir = true;
    }



    void CambiaControl()
    {
        if (!baseDeDatos.mandoActivo)
        {
            baseDeDatos.mandoActivo = true;

            this.transform.GetChild(5).GetChild(0).GetComponent<Image>().sprite = baseDeDatos.yXBOX[0];
            this.transform.GetChild(5).GetChild(1).GetComponent<Image>().sprite = baseDeDatos.seleccionXBOX[0];
            this.transform.GetChild(5).GetChild(2).GetComponent<Image>().sprite = baseDeDatos.volverXBOX[0];
        }
        else
        {
            baseDeDatos.mandoActivo = false;

            this.transform.GetChild(5).GetChild(0).GetComponent<Image>().sprite = baseDeDatos.vPC[0];
            this.transform.GetChild(5).GetChild(1).GetComponent<Image>().sprite = baseDeDatos.seleccionPC[0];
            this.transform.GetChild(5).GetChild(2).GetComponent<Image>().sprite = baseDeDatos.volverPC[0];
        }
    }



    IEnumerator MuestraDireccion()
    {
        if (fallaAtaque)
        {
            for(int i = 0; i < numeroPatrones; i++)
            {
                this.transform.GetChild(7).GetChild(patrones[i]).GetComponent<Image>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            }
        }
        else
        {
            this.transform.GetChild(7).GetChild(direccionJugador).GetComponent<Image>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
        }

        yield return new WaitForSeconds(0.1f);

        direccionesMostradas = true;
    }



    void EnemigoDecideDireccion()
    {
        for(int j = 0; j < 4; j++)
        {
            patrones[j] = -1;
        }

        switch (dificultad)
        {
            case 0:
                numeroPatrones = 1;
                break;
            case 1:
                numeroPatrones = 1;
                break;
            case 2:
                numeroPatrones = 2;
                break;
            case 3:
                numeroPatrones = 2;
                break;
            case 4:
                numeroPatrones = 3;
                break;
        }

        if(numeroPatrones == 1)
        {
            patrones[0] = Random.Range(0, 4);
        }
        else
        {
            patrones[0] = Random.Range(0, 4);

            int i = 1;
            int aux;

            while (i < numeroPatrones)
            {
                aux = Random.Range(0, 4);
                bool encontrado = false;

                for (int j = 0; j < i; j++)
                {
                    if (!encontrado && patrones[j] == aux)
                    {
                        encontrado = true;
                    }
                }

                if (!encontrado)
                {
                    patrones[i] = aux;
                    i++;
                }
            }
        }
    }



    void EnemigoDecideAtaque()
    {
        int ataqueElegido;
        int victoria = 0;
        int empate = 0;
        int derrota = 0;

        switch (dificultad)
        {
            case 0:
                victoria = 20;
                empate = 50;
                derrota = 95;
                break;
            case 1:
                victoria = 30;
                empate = 60;
                derrota = 95;
                break;
            case 2:
                victoria = 40;
                empate = 75;
                derrota = 95;
                break;
            case 3:
                victoria = 60;
                empate = 80;
                derrota = 95;
                break;
            case 4:
                victoria = 70;
                empate = 85;
                derrota = 95;
                break;
        }

        int random = Random.Range(0, 100);
        
        if (random < victoria)
        {
            if (ataqueAliado == 0)
            {
                ataqueElegido = 2;
            }
            else if (ataqueAliado == 1)
            {
                ataqueElegido = 0;
            }
            else
            {
                ataqueElegido = 1;
            }
        }
        else if (random < empate)
        {
            ataqueElegido = ataqueAliado;
        }
        else if (random < derrota)
        {
            if (ataqueAliado == 0)
            {
                ataqueElegido = 1;
            }
            else if (ataqueAliado == 1)
            {
                ataqueElegido = 2;
            }
            else
            {
                ataqueElegido = 0;
            }
        }
        else
        {
            ataqueElegido = Random.Range(0, 3);
        }

        ataqueEnemigo = ataqueElegido;
    }



    void EstableceMensaje(int valor)
    {
        string mensaje = "";
        
        switch (valor)
        {
            case -1:
                mensaje = "";
                break;
            case 0:
                mensaje = "Decide un ataque";
                break;
            case 1:
                mensaje = "Empate";
                efectos.ProduceEfecto(2);
                ReiniciaTiempos();
                break;
            case 2:
                mensaje = "Decide una dirección a bloquear";
                efectos.ProduceEfecto(24);
                break;
            case 3:
                mensaje = "Decide una dirección no bloqueada";
                efectos.ProduceEfecto(23);
                break;
            case 4:
                mensaje = "Ganas esta ronda";
                efectos.ProduceEfecto(22);
                puntuacionLograda += 150;
                break;
            case 5:
                mensaje = "Pierdes esta ronda";
                puntuacionLograda -= 150;
                efectos.ProduceEfecto(21);
                break;
            case 6:
                mensaje = "Te quedaste sin tiempo";
                puntuacionLograda -= 150;
                efectos.ProduceEfecto(21);
                break;
            case 7:
                mensaje = "Salvas la ronda";
                puntuacionLograda += 50;
                efectos.ProduceEfecto(2);
                break;
            case 8:
                mensaje = "Tu rival salva la ronda";
                efectos.ProduceEfecto(2);
                break;
            case 9:
                mensaje = "Duelo perdido";
                efectos.ProduceEfecto(20);
                break;
            case 10:
                mensaje = "Duelo ganado";
                efectos.ProduceEfecto(19);
                break;
        }

        this.transform.GetChild(0).GetComponent<Text>().text = "Act: " + puntuacionLograda;
        this.transform.GetChild(13).GetComponent<Text>().text = mensaje;
    }



    int CompruebaGanador()
    {
        //0 piedra, 1 tijera, 2 papel
        int resultado = 0; //-1 derrota, 0 empate, 1 victoria

        if(ataqueAliado == 0)
        {
            if(ataqueEnemigo == 1)
            {
                resultado = 1;
            }
            else if(ataqueEnemigo == 2)
            {
                resultado = -1;
            }
        }
        else if (ataqueAliado == 1)
        {
            if (ataqueEnemigo == 0)
            {
                resultado = -1;
            }
            else if (ataqueEnemigo == 2)
            {
                resultado = 1;
            }
        }
        else
        {
            if (ataqueEnemigo == 0)
            {
                resultado = 1;
            }
            else if (ataqueEnemigo == 1)
            {
                resultado = -1;
            }
        }

        return resultado;
    }



    void ReiniciaTiempos() 
    {
        cuentaAtras = 3;

        switch (indiceMision)
        {
            case 0:
                tiempoDecideAtaque = 4;
                tiempoDecideDireccion = 4;
                break;
            case 1:
                tiempoDecideAtaque = 2;
                tiempoDecideDireccion = 1;
                break;
        }
    }



    void EnemigoRespondeDireccion() //Respuesta del enemigo contra la direccion del jugador
    {
        int victoria = 0;
        int derrota = 0;

        switch (dificultad)
        {
            case 0:
                victoria = 30;
                derrota = 90;
                break;
            case 1:
                victoria = 40;
                derrota = 90;
                break;
            case 2:
                victoria = 50;
                derrota = 90;
                break;
            case 3:
                victoria = 60;
                derrota = 90;
                break;
            case 4:
                victoria = 70;
                derrota = 90;
                break;
        }

        int random = Random.Range(0, 100);

        if (random < victoria)
        {
            if (direccionJugador == 0)
            {
                direccionEnemigo = 1;
            }
            else if (direccionJugador == 1)
            {
                direccionEnemigo = 2;
            }
            else if (direccionJugador == 2)
            {
                direccionEnemigo = 3;
            }
            else
            {
                direccionEnemigo = 0;
            }
        }
        else if (random < derrota)
        {
            direccionEnemigo = direccionJugador;
        }
        else
        {
            direccionEnemigo = Random.Range(0, 4);
        }

        this.transform.GetChild(7).GetChild(4).gameObject.SetActive(true);

        if (direccionEnemigo == 3)
        {
            this.transform.GetChild(7).GetChild(4).GetComponent<Image>().sprite = baseDeDatos.abajoPC[0];
        }
        else if (direccionEnemigo == 0)
        {
            this.transform.GetChild(7).GetChild(4).GetComponent<Image>().sprite = baseDeDatos.arribaPC[0];
        }
        else if (direccionEnemigo == 1)
        {
            this.transform.GetChild(7).GetChild(4).GetComponent<Image>().sprite = baseDeDatos.izquierdaPC[0];
        }
        else
        { 
            this.transform.GetChild(7).GetChild(4).GetComponent<Image>().sprite = baseDeDatos.derechaPC[0];
        }
    }



    void ReiniciaRonda(bool empiezaCuentaAtras)
    {
        ataqueElegido = false;
        tomaDecision = false;
        resolucion = false;
        decisionJugador = false;

        cuentaInicial = empiezaCuentaAtras;
        this.transform.GetChild(10).gameObject.SetActive(empiezaCuentaAtras);

        ReiniciaTiempos();
    }



    IEnumerator LimpiaInterfaz(bool finTiempo)
    {
        limpiando = true;

        if (finTiempo)
        {
            this.transform.GetChild(7).GetChild(4).GetChild(1).gameObject.SetActive(true);
            EstableceMensaje(6);
            numeroVidas--;
            this.transform.GetChild(3).GetChild(numeroVidas).GetComponent<Image>().sprite = corazonVacio;
            reinicia = true;
        }
        else
        {
            if (fallaAtaque)
            {
                if (fallaDireccion)
                {
                    this.transform.GetChild(7).GetChild(4).GetChild(1).gameObject.SetActive(true);
                    EstableceMensaje(5);
                    numeroVidas--;
                    this.transform.GetChild(3).GetChild(numeroVidas).GetComponent<Image>().sprite = corazonVacio;
                    reinicia = true;
                }
                else
                {
                    this.transform.GetChild(7).GetChild(4).GetChild(0).gameObject.SetActive(true);
                    EstableceMensaje(7);
                    reinicia = false;
                }
            }
            else
            {
                if (fallaDireccion)
                {
                    this.transform.GetChild(7).GetChild(4).GetChild(1).gameObject.SetActive(true);
                    EstableceMensaje(4);
                    numeroVidasEnemigo--;
                    this.transform.GetChild(4).GetChild(numeroVidasEnemigo).GetComponent<Image>().sprite = corazonVacio;
                    reinicia = true;
                }
                else
                {
                    this.transform.GetChild(7).GetChild(4).GetChild(0).gameObject.SetActive(true);
                    EstableceMensaje(8);
                    reinicia = false;
                }
            }
        }
        

        yield return new WaitForSeconds(1);


        if(numeroVidas != 0 && numeroVidasEnemigo != 0)
        {
            this.transform.GetChild(7).GetChild(4).GetChild(0).gameObject.SetActive(false);
            this.transform.GetChild(7).GetChild(4).GetChild(1).gameObject.SetActive(false);
            this.transform.GetChild(7).GetChild(4).gameObject.SetActive(false);

            for (int i = 0; i < 4; i++)
            {
                this.transform.GetChild(7).GetChild(i).GetComponent<Image>().color = new Color(50f / 255f, 50f / 255f, 50f / 255f, 255f / 255f);
            }

            limpiando = false;

            ReiniciaRonda(reinicia);

            if (!reinicia)
            {
                EstableceMensaje(0);
            }
        }
        else
        {
            if (numeroVidas == 0)
            {
                EstableceMensaje(9);
                victoria = false;
            }
            else
            {
                EstableceMensaje(10);
                victoria = true;
            }

            StartCoroutine(Fin());
        }
    }
}
