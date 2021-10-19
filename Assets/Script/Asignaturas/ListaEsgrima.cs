using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListaEsgrima : MonoBehaviour
{
    GameObject listaEsgrima;
    Esgrima menuEsgrima;
    BaseDatos baseDeDatos;
    ControlJugador controlJugador;

    bool activo;
    bool mueve;
    bool pulsado;

    float digitalX;
    float digitalY;
    int pos;
    int posIngrediente;
    int cantidadMonedas; //Numero de monedas necesarias para iniciar el reto
    int experiencia;
    int dificultad;

    MusicaManager musica;



    void Start()
    {
        GameObject aux = GameObject.Find("MensajesEnPantalla");
        listaEsgrima = aux.transform.GetChild(14).gameObject;
        menuEsgrima  = aux.transform.GetChild(13).GetComponent<Esgrima>();
        baseDeDatos = GameObject.Find("GameManager").GetComponent<BaseDatos>();
        musica = GameObject.Find("EfectosSonido").GetComponent<MusicaManager>();
        controlJugador = GameObject.Find("Player").GetComponent<ControlJugador>();

        pos = 0;

        pulsado = false;

        DesactivaMenu();
    }



    void Update()
    {
        if (activo)
        {
            if (!mueve)
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

                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || (!pulsado && digitalY > 0))
                {
                    pulsado = true;
                    mueve = true;
                    musica.ProduceEfecto(11);

                    pos--;

                    if (pos < 0)
                    {
                        pos = 9;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || (!pulsado && digitalY < 0))
                {
                    pulsado = true;
                    mueve = true;
                    musica.ProduceEfecto(11);

                    pos++;

                    if (pos > 9)
                    {
                        pos = 0;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                {
                    musica.ProduceEfecto(10);

                    if (PuedeIniciar())
                    {
                        baseDeDatos.cantidadesObjetosConsumibles[posIngrediente] -= cantidadMonedas;

                        IniciaEvento();
                    }
                }
                else if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("B"))
                {
                    musica.ProduceEfecto(12);

                    DesactivaMenu();
                }
            }
            else
            {
                MueveFlecha();
            }
        }
    }



    public void ActivaMenu()
    {
        if (!activo)
        {
            activo = true;
            listaEsgrima.SetActive(true);
            pos = 0;
            controlJugador.setMensajeActivo(true);

            for (int i = 0; i < 10; i++)
            {
                if (baseDeDatos.retosEsgrimaDesbloqueados[i])
                {
                    listaEsgrima.transform.GetChild(2).GetChild(i).GetComponent<Text>().text = baseDeDatos.nombreRetosEsgrima[i];
                }
                else
                {
                    listaEsgrima.transform.GetChild(2).GetChild(i).GetComponent<Text>().text = "---------------";
                }
            }

            listaEsgrima.transform.GetChild(3).GetChild(0).transform.position = listaEsgrima.transform.GetChild(3).GetChild(1).transform.position;

            mueve = true;

            if (baseDeDatos.mandoActivo)
            {
                listaEsgrima.transform.GetChild(4).GetChild(3).GetComponent<Image>().sprite = baseDeDatos.seleccionXBOX[0];
                listaEsgrima.transform.GetChild(4).GetChild(5).GetComponent<Image>().sprite = baseDeDatos.volverXBOX[0];
                listaEsgrima.transform.GetChild(4).GetChild(7).GetComponent<Image>().sprite = baseDeDatos.moverXBOX[0];
            }
            else
            {
                listaEsgrima.transform.GetChild(4).GetChild(3).GetComponent<Image>().sprite = baseDeDatos.seleccionPC[0];
                listaEsgrima.transform.GetChild(4).GetChild(5).GetComponent<Image>().sprite = baseDeDatos.volverPC[0];
                listaEsgrima.transform.GetChild(4).GetChild(7).GetComponent<Image>().sprite = baseDeDatos.moverPC[0];
            }
        }
    }



    void DesactivaMenu()
    {
        activo = false;
        listaEsgrima.SetActive(false);
        controlJugador.setMensajeActivo(false);
    }



    void IniciaEvento()
    {
        activo = false;
        listaEsgrima.SetActive(false);
        menuEsgrima.IniciaEvento(pos, experiencia);
    }



    void CambiaControl()
    {
        if (!baseDeDatos.mandoActivo)
        {
            baseDeDatos.mandoActivo = true;

            listaEsgrima.transform.GetChild(4).GetChild(3).GetComponent<Image>().sprite = baseDeDatos.seleccionXBOX[0];
            listaEsgrima.transform.GetChild(4).GetChild(5).GetComponent<Image>().sprite = baseDeDatos.volverXBOX[0];
            listaEsgrima.transform.GetChild(4).GetChild(7).GetComponent<Image>().sprite = baseDeDatos.moverXBOX[0];
        }
        else
        {
            baseDeDatos.mandoActivo = false;

            listaEsgrima.transform.GetChild(4).GetChild(3).GetComponent<Image>().sprite = baseDeDatos.seleccionPC[0];
            listaEsgrima.transform.GetChild(4).GetChild(5).GetComponent<Image>().sprite = baseDeDatos.volverPC[0];
            listaEsgrima.transform.GetChild(4).GetChild(7).GetComponent<Image>().sprite = baseDeDatos.moverPC[0];
        }
    }



    void MueveFlecha()
    {
        ActualizaReto();

        int aux = 1 + pos;
        listaEsgrima.transform.GetChild(3).GetChild(0).transform.position = listaEsgrima.transform.GetChild(3).GetChild(aux).transform.position;

        mueve = false;
    }



    bool PuedeIniciar()
    {
        bool resultado = false;

        if (baseDeDatos.retosEsgrimaDesbloqueados[pos])
        {
            int posAux = 0;
            bool encontrado = false;

            while (!encontrado && posAux < baseDeDatos.numeroObjetosConsumibles)
            {
                if (baseDeDatos.objetosConsumibles[posAux].indiceTipo == 18)
                {
                    if (baseDeDatos.cantidadesObjetosConsumibles[posAux] >= cantidadMonedas)
                    {
                        resultado = true;
                    }

                    encontrado = true;
                }
                else
                {
                    posAux++;
                }
            }
        }
        
        return resultado;
    }



    void ActualizaReto()
    {
        if (baseDeDatos.retosEsgrimaDesbloqueados[pos])
        {
            switch (pos)
            {
                case 0:
                    cantidadMonedas = 1;
                    experiencia = 200;
                    dificultad = 0;
                    break;
                case 1:
                    cantidadMonedas = 2;
                    experiencia = 300;
                    dificultad = 4;
                    break;
            }

            listaEsgrima.transform.GetChild(5).GetChild(0).GetChild(0).gameObject.SetActive(true);
            listaEsgrima.transform.GetChild(5).GetChild(0).GetChild(1).GetComponent<Text>().text = "x" + cantidadMonedas;

            string nombreDif = "";

            if (baseDeDatos.idioma == 1)
            {
                if (dificultad == 0)
                {
                    nombreDif = "Easy";
                }
                else if (dificultad == 1)
                {
                    nombreDif = "Medio";
                }
                else if (dificultad == 2)
                {
                    nombreDif = "Difícil";
                }
                else if (dificultad == 3)
                {
                    nombreDif = "Muy Difícil";
                }
                else if (dificultad == 4)
                {
                    nombreDif = "Titán";
                }
            }
            else
            {
                if (dificultad == 0)
                {
                    nombreDif = "Fácil";
                }
                else if (dificultad == 1)
                {
                    nombreDif = "Medio";
                }
                else if (dificultad == 2)
                {
                    nombreDif = "Difícil";
                }
                else if (dificultad == 3)
                {
                    nombreDif = "Muy Difícil";
                }
                else if (dificultad == 4)
                {
                    nombreDif = "Titán";
                }
            }

            listaEsgrima.transform.GetChild(5).GetChild(1).GetComponent<Text>().text = nombreDif;
            listaEsgrima.transform.GetChild(5).GetChild(2).GetComponent<Text>().text = "x" + experiencia + " exp";
        }
        else
        {
            listaEsgrima.transform.GetChild(5).GetChild(0).GetChild(0).gameObject.SetActive(false);
            listaEsgrima.transform.GetChild(5).GetChild(0).GetChild(1).GetComponent<Text>().text = "";
            listaEsgrima.transform.GetChild(5).GetChild(1).GetComponent<Text>().text = "";
            listaEsgrima.transform.GetChild(5).GetChild(2).GetComponent<Text>().text = "";
        }
        
    }
}
