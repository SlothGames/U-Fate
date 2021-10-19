using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PantallaCarga : MonoBehaviour {
    public bool activo;
    int contador;
    int fichero;

    bool iniciado1, iniciado2, iniciado3;
    bool lanzarJuego, lanzaCarga;
    bool cargar;

    GameObject manager;
    public GameObject puntoGuardadoMaestro;

    BaseDatos baseDeDatos;
    NuevoTablero tablero;
    ControlJugador jugador;
    PuntoGuardado guardado;
    public GameObject controlJugador;


    void Awake ()
    {
        activo = true;
        lanzarJuego = false;
        lanzaCarga = false;
        
        iniciado1 = iniciado2 = iniciado3 = false;
       
        manager = GameObject.Find("GameManager");
        puntoGuardadoMaestro = GameObject.Find("PuntoGuardadoMaestro");
        baseDeDatos = manager.GetComponent<BaseDatos>();
        jugador = controlJugador.GetComponent<ControlJugador>();
        tablero = manager.GetComponent<NuevoTablero>();
        guardado = puntoGuardadoMaestro.GetComponent<PuntoGuardado>();

        contador = 0;
        this.gameObject.SetActive(activo);

       
	}



    void Update ()
    {
        if (cargar)
        {
            if(baseDeDatos.idioma == -1)
            {
                this.transform.GetChild(1).GetComponent<Text>().text = "";
            }
            else if (baseDeDatos.idioma == 1)
            {
                this.transform.GetChild(1).GetComponent<Text>().text = "Loading...";
            }
            else
            {
                this.transform.GetChild(1).GetComponent<Text>().text = "Cargando...";
            }

            if (contador != 3)
            {
                //this.transform.GetChild(2).GetComponent<Text>().text = jugador.a + " " + jugador.b + " " + jugador.c + " " + jugador.d + " " + jugador.e + " ";
                //this.transform.GetChild(2).GetComponent<Text>().text += " " + controlJugador;
                if (jugador.activado && !iniciado1)
                {
                    //this.transform.GetChild(2).GetComponent<Text>().text += " jugador ";
                    contador++;
                    iniciado1 = true;
                }

                if (baseDeDatos.iniciado && !iniciado2)
                {
                    //this.transform.GetChild(2).GetComponent<Text>().text += " base ";
                    contador++;
                    iniciado2 = true;
                }

                if (tablero.iniciado && !iniciado3)
                {
                    //this.transform.GetChild(2).GetComponent<Text>().text += " tablero ";
                    contador++;
                    iniciado3 = true;
                }
            }
            else
            {
                if (!lanzaCarga)
                {
                    lanzaCarga = true;
                   
                    guardado.CargarPartida(fichero);
                }
                else
                {
                    if (!lanzarJuego)
                    {
                        if (guardado.cargado)
                        {
                            lanzarJuego = true;
                        }
                    }
                    else if (lanzarJuego)
                    {
                        DesactivaCarga(false);
                    }
                }
            }
        }
    }



    public void ActivaCarga(int ficheroCarga)
    {
        contador = 0;
        iniciado1 = iniciado2 = iniciado3 = false;
        activo = true;
        this.gameObject.SetActive(activo);
        lanzarJuego = false;
        fichero = ficheroCarga;

        cargar = true;
    }



    public void DesactivaCarga(bool juegoNuevo)
    {
        if (juegoNuevo)
        {
            GameObject animacion = GameObject.Find("ControladorAnimaciones");
            Inicio inicio = animacion.GetComponent<Inicio>();
            inicio.IniciaCartero();
        }

        activo = false;
        cargar = false;
        this.gameObject.SetActive(activo);
    }



    public void EstableceConfig(int idiomaNuevo, bool mandoActivo, string nombre, int dificultad)
    {
        baseDeDatos.idioma = idiomaNuevo;
        baseDeDatos.mandoActivo = mandoActivo;
        baseDeDatos.nombreProta = nombre;
        jugador.SetDificultad(dificultad);
        baseDeDatos.CambiaNombreProta();
    }



    public void EstableceIdioma(int idiomaNuevo)
    {
        baseDeDatos.idioma = idiomaNuevo;
    }
}
