using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListaPociones : MonoBehaviour
{
    GameObject listaPociones;
    Pociones menuPociones;
    BaseDatos baseDeDatos;
    ControlJugador controlJugador;

    bool activo;
    bool mueve;
    bool pulsado;

    float digitalX;
    float digitalY;
    int pos;

    MusicaManager musica;
    public Sprite[] imagenesIngredientes;
    int[] cantidadPlantas;
    int[] indiceIngredientes;
    int[] posIngredientes;
    int numeroIngredientes;

    void Start()
    {
        GameObject aux = GameObject.Find("MensajesEnPantalla");
        listaPociones = aux.transform.GetChild(10).gameObject;
        menuPociones = aux.transform.GetChild(11).GetComponent<Pociones>();
        baseDeDatos = GameObject.Find("GameManager").GetComponent<BaseDatos>();
        musica = GameObject.Find("EfectosSonido").GetComponent<MusicaManager>();
        controlJugador = GameObject.Find("Player").GetComponent<ControlJugador>();

        pos = 0;

        pulsado = false;
        
        DesactivaMenu();
        cantidadPlantas = new int [4];
        indiceIngredientes = new int[4];
        posIngredientes = new int [4];
        numeroIngredientes = 0;

        listaPociones.transform.GetChild(5).GetChild(0).gameObject.SetActive(false);
        listaPociones.transform.GetChild(5).GetChild(1).gameObject.SetActive(false);
        listaPociones.transform.GetChild(5).GetChild(2).gameObject.SetActive(false);
        listaPociones.transform.GetChild(5).GetChild(3).gameObject.SetActive(false);
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
                        for(int i = 0; i < numeroIngredientes; i++)
                        {
                            baseDeDatos.cantidadesObjetosConsumibles[posIngredientes[i]] -= cantidadPlantas[i];
                        }

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
            listaPociones.SetActive(true);
            pos = 0;
            controlJugador.setMensajeActivo(true);
            MueveFlecha();

            for (int i = 0; i < 10; i++)
            {
                if (baseDeDatos.recetasPocionesDesbloqueadas[i])
                {
                    listaPociones.transform.GetChild(2).GetChild(i).GetComponent<Text>().text = baseDeDatos.nombrePociones[i];
                }
                else
                {
                    listaPociones.transform.GetChild(2).GetChild(i).GetComponent<Text>().text = "---------------";
                }
            }

            listaPociones.transform.GetChild(3).GetChild(0).transform.position = listaPociones.transform.GetChild(3).GetChild(1).transform.position;

            mueve = true;

            if (baseDeDatos.mandoActivo)
            {
                listaPociones.transform.GetChild(4).GetChild(3).GetComponent<Image>().sprite = baseDeDatos.seleccionXBOX[0];
                listaPociones.transform.GetChild(4).GetChild(5).GetComponent<Image>().sprite = baseDeDatos.volverXBOX[0];
                listaPociones.transform.GetChild(4).GetChild(7).GetComponent<Image>().sprite = baseDeDatos.moverXBOX[0];
            }
            else
            {
                listaPociones.transform.GetChild(4).GetChild(3).GetComponent<Image>().sprite = baseDeDatos.seleccionPC[0];
                listaPociones.transform.GetChild(4).GetChild(5).GetComponent<Image>().sprite = baseDeDatos.volverPC[0];
                listaPociones.transform.GetChild(4).GetChild(7).GetComponent<Image>().sprite = baseDeDatos.moverPC[0];
            }
        }
    }



    void DesactivaMenu()
    {
        activo = false;
        listaPociones.SetActive(false);
        controlJugador.setMensajeActivo(false);
    }



    void IniciaEvento()
    {
        if (baseDeDatos.recetasPocionesDesbloqueadas[pos])
        {
            activo = false;
            listaPociones.SetActive(false);
            menuPociones.IniciaEvento(pos);
        }
    }



    void CambiaControl()
    {
        if (!baseDeDatos.mandoActivo)
        {
            baseDeDatos.mandoActivo = true;

            listaPociones.transform.GetChild(4).GetChild(3).GetComponent<Image>().sprite = baseDeDatos.seleccionXBOX[0];
            listaPociones.transform.GetChild(4).GetChild(5).GetComponent<Image>().sprite = baseDeDatos.volverXBOX[0];
            listaPociones.transform.GetChild(4).GetChild(7).GetComponent<Image>().sprite = baseDeDatos.moverXBOX[0];
        }
        else
        {
            baseDeDatos.mandoActivo = false;

            listaPociones.transform.GetChild(4).GetChild(3).GetComponent<Image>().sprite = baseDeDatos.seleccionPC[0];
            listaPociones.transform.GetChild(4).GetChild(5).GetComponent<Image>().sprite = baseDeDatos.volverPC[0];
            listaPociones.transform.GetChild(4).GetChild(7).GetComponent<Image>().sprite = baseDeDatos.moverPC[0];
        }
    }



    void MueveFlecha()
    {
        listaPociones.transform.GetChild(5).GetChild(0).gameObject.SetActive(false);
        listaPociones.transform.GetChild(5).GetChild(1).gameObject.SetActive(false);
        listaPociones.transform.GetChild(5).GetChild(2).gameObject.SetActive(false);
        listaPociones.transform.GetChild(5).GetChild(3).gameObject.SetActive(false);

        if (baseDeDatos.recetasPocionesDesbloqueadas[pos])
        {
            ActualizaIngredientes();
        }

        int aux = 1 + pos;
        listaPociones.transform.GetChild(3).GetChild(0).transform.position = listaPociones.transform.GetChild(3).GetChild(aux).transform.position;

        mueve = false;
    }



    void ActualizaIngredientes()
    {
        //0--> Sauce, 1--> Laurel, 2--> Dedalera, 3--> Alga 4--> Amanita, 5--> Rizoma, 6--> Acebo, 7--> Esporas, 8--> Abedul, 9--> Nomeolvides, 10--> Espino, 11--> Calocybe, 12--> Rosas, 13--> Hepatica
        switch (pos)
        {
            case 0:
                cantidadPlantas[0] = 2;
                numeroIngredientes = 1;
                indiceIngredientes[0] = 4;

                listaPociones.transform.GetChild(5).GetChild(0).gameObject.SetActive(true);
                listaPociones.transform.GetChild(5).GetChild(0).GetChild(0).GetComponent<Image>().sprite = imagenesIngredientes[0];
                listaPociones.transform.GetChild(5).GetChild(0).GetChild(1).GetComponent<Text>().text = "Sauce";
                listaPociones.transform.GetChild(5).GetChild(0).GetChild(2).GetComponent<Text>().text = "x" + cantidadPlantas[0];
                break;
            case 1:
                cantidadPlantas[0] = 2;
                cantidadPlantas[1] = 1;
                numeroIngredientes = 2;
                indiceIngredientes[0] = 5;
                indiceIngredientes[1] = 6;

                listaPociones.transform.GetChild(5).GetChild(0).gameObject.SetActive(true);
                listaPociones.transform.GetChild(5).GetChild(1).gameObject.SetActive(true);

                listaPociones.transform.GetChild(5).GetChild(0).GetChild(0).GetComponent<Image>().sprite = imagenesIngredientes[1];
                listaPociones.transform.GetChild(5).GetChild(0).GetChild(1).GetComponent<Text>().text = "Laurel";
                listaPociones.transform.GetChild(5).GetChild(0).GetChild(2).GetComponent<Text>().text = "x" + cantidadPlantas[0];

                listaPociones.transform.GetChild(5).GetChild(1).GetChild(0).GetComponent<Image>().sprite = imagenesIngredientes[2];
                listaPociones.transform.GetChild(5).GetChild(1).GetChild(1).GetComponent<Text>().text = "Dedalera";
                listaPociones.transform.GetChild(5).GetChild(1).GetChild(2).GetComponent<Text>().text = "x" + cantidadPlantas[1];
                break;
        }
    }



    bool PuedeIniciar()
    {
        bool resultado = false;
        int elementosPoseidos = 0;

        for (int i = 0; i < numeroIngredientes; i++)
        {
            int pos = 0;
            bool encontrado = false;

            while (!encontrado && pos < baseDeDatos.numeroObjetosConsumibles)
            {
                if (baseDeDatos.objetosConsumibles[pos].indiceTipo == indiceIngredientes[i])
                {
                    if (baseDeDatos.cantidadesObjetosConsumibles[pos] >= cantidadPlantas[i])
                    {
                        posIngredientes[elementosPoseidos] = pos;
                        elementosPoseidos++;
                    }

                    encontrado = true;

                    if (elementosPoseidos == numeroIngredientes) //podemos realizar la poción
                    {
                        resultado = true;
                    }
                }
                else
                {
                    pos++;
                }
            }
        }

        return resultado;
    }
}

