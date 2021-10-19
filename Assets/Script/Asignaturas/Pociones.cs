using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pociones : MonoBehaviour
{
    bool activo;
    bool rondasInfinitas;
    bool mostrandoSerie;
    bool completado;
    bool victoria;
    bool mostrado;
    bool salir;
    bool cuentaInicial;

    int indiceMision;
    int puntuacionLograda;
    int numeroVidas;
    int numeroRondas;
    int rondaActual;
    int velocidad; //mas lento 1 mas rapido 10
    int numeroPatrones; //numero por ronda
    int[] patrones; //1 arriba, 2 izquierda, 3 derecha, 4 abajo
    int[] tiempoInicial;
    float tiempo;
    int patronActual;
    int indicePocionIncluir;
    float cuentaAtras;

    float digitalX;
    float digitalY;

    BaseDatos baseDeDatos;
    ControlJugador controlJugador;
    MusicaManager efectos;
    MusicaManager musica;

    public Sprite corazonVacio, corazonLleno;
    public Sprite basura;
    public Sprite[] pociones = new Sprite[12];



    void Start()
    {
        activo = false;
        rondasInfinitas = false;
        victoria = false;
        cuentaInicial = false;

        baseDeDatos = GameObject.Find("GameManager").GetComponent<BaseDatos>();
        controlJugador = GameObject.Find("Player").GetComponent<ControlJugador>();
        efectos = GameObject.Find("EfectosSonido").GetComponent<MusicaManager>();
        musica = GameObject.Find("Musica").GetComponent<MusicaManager>();

        tiempoInicial = new int[10];
        tiempoInicial[0] = 100;

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
                if (numeroVidas != 0 && !completado && tiempo > 0)
                {
                    if (!mostrandoSerie)
                    {
                        tiempo -= Time.deltaTime;
                        this.transform.GetChild(6).GetComponent<Text>().text = "Time: " + tiempo.ToString("f0");
                        this.transform.GetChild(4).GetComponent<Text>().text = "Act: " + puntuacionLograda;

                        /*if (puntuacionLograda > baseDeDatos.puntuacionRecordPociones[indiceMision])
                        {
                            this.transform.GetChild(5).GetComponent<Text>().text = "Rec: " + puntuacionLograda;
                            baseDeDatos.puntuacionRecordPociones[indiceMision] = puntuacionLograda;
                        }*/

                        if (rondaActual != numeroRondas)
                        {
                            if (patronActual < numeroPatrones)
                            {
                                if (patrones[patronActual] == 0)
                                {
                                    if (Input.GetButtonUp("Y") || Input.GetKeyDown(KeyCode.V))
                                    {
                                        puntuacionLograda += (100 * velocidad);
                                        patronActual++;
                                        efectos.ProduceEfecto(17);
                                    }
                                    else if (Input.GetButtonUp("B") || Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("X") || Input.GetKeyDown(KeyCode.B)
                                        || Input.GetButtonUp("A") || Input.GetKeyDown(KeyCode.N))
                                    {
                                        numeroVidas--;
                                        int aux = 9 - numeroVidas;
                                        this.transform.GetChild(aux).GetComponent<Image>().sprite = corazonVacio;
                                        efectos.ProduceEfecto(18);
                                    }
                                }
                                else if (patrones[patronActual] == 2)
                                {
                                    if (Input.GetButtonUp("X") || Input.GetKeyDown(KeyCode.B))
                                    {
                                        puntuacionLograda += (100 * velocidad);
                                        patronActual++;
                                        efectos.ProduceEfecto(17);
                                    }
                                    else if (Input.GetButtonUp("B") || Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("A") || Input.GetKeyDown(KeyCode.N)
                                        || Input.GetButtonUp("Y") || Input.GetKeyDown(KeyCode.V))
                                    {
                                        numeroVidas--;
                                        int aux = 9 - numeroVidas;
                                        this.transform.GetChild(aux).GetComponent<Image>().sprite = corazonVacio;
                                        efectos.ProduceEfecto(18);
                                    }
                                }
                                else if (patrones[patronActual] == 3)
                                {
                                    if (Input.GetButtonUp("B") || Input.GetKeyDown(KeyCode.M))
                                    {
                                        puntuacionLograda += (100 * velocidad);
                                        patronActual++;
                                        efectos.ProduceEfecto(17);
                                    }
                                    else if (Input.GetButtonUp("A") || Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("X") || Input.GetKeyDown(KeyCode.B)
                                        || Input.GetButtonUp("Y") || Input.GetKeyDown(KeyCode.V))
                                    {
                                        numeroVidas--;
                                        int aux = 9 - numeroVidas;
                                        this.transform.GetChild(aux).GetComponent<Image>().sprite = corazonVacio;
                                        efectos.ProduceEfecto(18);
                                    }
                                }
                                else
                                {
                                    if (Input.GetButtonUp("A") || Input.GetKeyDown(KeyCode.N))
                                    {
                                        puntuacionLograda += (100 * velocidad);
                                        patronActual++;
                                        efectos.ProduceEfecto(17);
                                    }
                                    else if (Input.GetButtonUp("B") || Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("X") || Input.GetKeyDown(KeyCode.B)
                                        || Input.GetButtonUp("Y") || Input.GetKeyDown(KeyCode.V))
                                    {
                                        numeroVidas--;
                                        int aux = 9 - numeroVidas;
                                        this.transform.GetChild(aux).GetComponent<Image>().sprite = corazonVacio;
                                        efectos.ProduceEfecto(18);
                                    }
                                }

                                if (Input.GetButtonUp("A") || Input.GetKeyDown(KeyCode.N))
                                {
                                    StartCoroutine(MuestraPulsacion(1));
                                }
                                else if (Input.GetButtonUp("B") || Input.GetKeyDown(KeyCode.M))
                                {
                                    StartCoroutine(MuestraPulsacion(3));
                                }
                                else if (Input.GetButtonUp("Y") || Input.GetKeyDown(KeyCode.V))
                                {
                                    StartCoroutine(MuestraPulsacion(0));
                                }
                                else if (Input.GetButtonUp("X") || Input.GetKeyDown(KeyCode.B))
                                {
                                    StartCoroutine(MuestraPulsacion(2));
                                }
                            }
                            else
                            {
                                ActualizaSerie();
                            }
                        }
                    }
                }
                else
                {
                    if (!mostrado)
                    {
                        if (numeroVidas == 0 || tiempo <= 0 )
                        {
                            victoria = false;
                        }

                        mostrado = true;

                        StartCoroutine(Fin());
                    }
                    else if (salir)
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
                this.transform.GetChild(12).GetComponent<Text>().text = "" + cuentaAtras.ToString("f0");

                if(cuentaAtras <= 0)
                {
                    this.transform.GetChild(12).gameObject.SetActive(false);
                    cuentaInicial = false;
                }
            }
        }
    }



    void ActivaMenu()
    {
        activo = true;
        this.transform.GetChild(10).gameObject.SetActive(false);
        this.gameObject.SetActive(activo);
        musica.CambiaCancion(15);
        puntuacionLograda = 0;
        completado = false;
        cuentaAtras = 3;
        cuentaInicial = true;
        this.transform.GetChild(12).gameObject.SetActive(true);

        for (int i = 0; i < 4; i++)
        {
            this.transform.GetChild(i).GetComponent<Image>().color = new Color(50f / 255f, 50f / 255f, 50f / 255f, 255f / 255f);
        }

        if (baseDeDatos.mandoActivo)
        {
            this.transform.GetChild(0).GetComponent<Image>().sprite = baseDeDatos.yXBOX[0];
            this.transform.GetChild(1).GetComponent<Image>().sprite = baseDeDatos.seleccionXBOX[0];
            this.transform.GetChild(2).GetComponent<Image>().sprite = baseDeDatos.xXBOX[0];
            this.transform.GetChild(3).GetComponent<Image>().sprite = baseDeDatos.volverXBOX[0];
        }
        else
        {
            this.transform.GetChild(0).GetComponent<Image>().sprite = baseDeDatos.vPC[0];
            this.transform.GetChild(2).GetComponent<Image>().sprite = baseDeDatos.bPC[0];
            this.transform.GetChild(1).GetComponent<Image>().sprite = baseDeDatos.seleccionPC[0];
            this.transform.GetChild(3).GetComponent<Image>().sprite = baseDeDatos.volverPC[0];
        }

        puntuacionLograda = 0;
        this.transform.GetChild(4).GetComponent<Text>().text = "Act: " + puntuacionLograda;
        this.transform.GetChild(4).GetComponent<Text>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
        this.transform.GetChild(5).GetComponent<Text>().text = "Rec: " + baseDeDatos.puntuacionRecordPociones[indiceMision];
        this.transform.GetChild(6).GetComponent<Text>().text = "Time: " + tiempoInicial[indiceMision];
        this.transform.GetChild(6).GetComponent<Text>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);

        this.transform.GetChild(7).GetComponent<Image>().sprite = corazonLleno;
        this.transform.GetChild(8).GetComponent<Image>().sprite = corazonLleno;
        this.transform.GetChild(9).GetComponent<Image>().sprite = corazonLleno;

        numeroVidas = 3;
    }



    void DesactivaMenu()
    {
        activo = false;
        mostrado = false;
        salir = false;
        this.gameObject.SetActive(activo);
        this.transform.GetChild(11).gameObject.SetActive(false);
        controlJugador.setMensajeActivo(false);
    }



    public void IniciaEvento(int indice)
    {
        indiceMision = indice;
        rondaActual = -1;
        //controlJugador.setMensajeActivo(true);
        patronActual = 0;

        switch (indice)
        {
            case 0:
                numeroRondas = 2;
                break;
            case 1:
                numeroRondas = 2;
                break;
        }

        ActivaMenu();
        ActualizaSerie();
    }



    IEnumerator MuestraSerie()
    {
        float tiempo = 2f - (velocidad * 0.15f);
        int contador = 0;

        yield return new WaitForSeconds(3.5f);

        while (contador < numeroPatrones)
        {
            int patron = patrones[contador];
            this.transform.GetChild(patron).GetComponent<Image>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);

            yield return new WaitForSeconds(tiempo);

            for (int i = 0; i < 4; i++)
            {
                this.transform.GetChild(i).GetComponent<Image>().color = new Color(50f / 255f, 50f / 255f, 50f / 255f, 255f / 255f);
            }

            yield return new WaitForSeconds(0.25f);

            contador++;
        }

        mostrandoSerie = false;
    }



    IEnumerator MuestraPulsacion(int pulsado)
    {
        this.transform.GetChild(pulsado).GetComponent<Image>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);

        yield return new WaitForSeconds(0.1f);

        this.transform.GetChild(pulsado).GetComponent<Image>().color = new Color(50f / 255f, 50f / 255f, 50f / 255f, 255f / 255f);
    }


    IEnumerator Fin()
    {
        musica.ParaMusica();

        yield return new WaitForSeconds(0.1f);

        if (victoria)
        {
            efectos.ProduceEfecto(19);

            int aux = 500 * numeroVidas;
            puntuacionLograda += aux;

            this.transform.GetChild(10).GetChild(0).GetComponent<Image>().sprite = pociones[indiceMision];

            if(baseDeDatos.puntuacionRecordPociones[indiceMision] < puntuacionLograda)
            {
                baseDeDatos.puntuacionRecordPociones[indiceMision] = puntuacionLograda;
                this.transform.GetChild(11).gameObject.SetActive(true);
            }

            baseDeDatos.IncluirEnInventario(indicePocionIncluir, 1);

            this.transform.GetChild(10).GetChild(1).GetComponent<Text>().text = "Exito";
        }
        else
        {
            efectos.ProduceEfecto(20);

            this.transform.GetChild(10).GetChild(0).GetComponent<Image>().sprite = basura;

            this.transform.GetChild(10).GetChild(1).GetComponent<Text>().text = "Fracaso";
        }

        this.transform.GetChild(10).gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        salir = true;
    }



    void CambiaControl()
    {
        if (!baseDeDatos.mandoActivo)
        {
            baseDeDatos.mandoActivo = true;

            this.transform.GetChild(0).GetComponent<Image>().sprite = baseDeDatos.yXBOX[0];
            this.transform.GetChild(1).GetComponent<Image>().sprite = baseDeDatos.seleccionXBOX[0];
            this.transform.GetChild(2).GetComponent<Image>().sprite = baseDeDatos.xXBOX[0];
            this.transform.GetChild(3).GetComponent<Image>().sprite = baseDeDatos.volverXBOX[0];
        }
        else
        {
            baseDeDatos.mandoActivo = false;

            this.transform.GetChild(0).GetComponent<Image>().sprite = baseDeDatos.vPC[0];
            this.transform.GetChild(2).GetComponent<Image>().sprite = baseDeDatos.bPC[0];
            this.transform.GetChild(1).GetComponent<Image>().sprite = baseDeDatos.seleccionPC[0];
            this.transform.GetChild(3).GetComponent<Image>().sprite = baseDeDatos.volverPC[0];
        }
    }



    void ActualizaSerie()
    {
        mostrandoSerie = true;
        rondaActual++;
        patronActual = 0;

        if(rondaActual != numeroRondas)
        {
            switch (indiceMision)
            {
                case 0:
                    if (rondaActual == 0)
                    {
                        tiempo = 20;
                        indicePocionIncluir = 0;//FaltaCorregir
                        this.transform.GetChild(6).GetComponent<Text>().text = "Time: " + tiempo.ToString("f0");
                        velocidad = 1;
                        numeroPatrones = 3;
                    }
                    else
                    {
                        velocidad = 2;
                        numeroPatrones = 4;
                    }

                    patrones = new int[numeroPatrones];

                    for (int i = 0; i < numeroPatrones; i++)
                    {
                        patrones[i] = Random.Range(0, 4);
                    }

                    break;
                case 1:
                    if (rondaActual == 0)
                    {
                        tiempo = 20;
                        indicePocionIncluir = 0;//FaltaCorregir
                        this.transform.GetChild(6).GetComponent<Text>().text = "Time: " + tiempo.ToString("f0");
                        velocidad = 1;
                        numeroPatrones = 3;
                    }
                    else
                    {
                        velocidad = 2;
                        numeroPatrones = 4;
                    }

                    patrones = new int[numeroPatrones];

                    for (int i = 0; i < numeroPatrones; i++)
                    {
                        patrones[i] = Random.Range(0, 4);
                    }
                    break;
            }

            StartCoroutine(MuestraSerie());
        }
        else
        {
            victoria = true;
            completado = true;
        }
    }
}
