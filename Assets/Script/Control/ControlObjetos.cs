using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlObjetos : MonoBehaviour
{
    BaseDatos baseDeDatos;

    //Mision Pedro
    public bool misionPedroActiva;
    public GameObject Pedro;
    bool pedroDesactivado;
    public int contadorMonstruosPedro;
    public int posicionMisionPedro;
    public int posicionGeneralMisionPedro;

    //Mision Gamez
    public bool misionGamezActiva;
    public GameObject Gamez;
    public GameObject Ladron;
    public GameObject CofreGema;
    public bool tieneGema;
    bool ladronDesactivado;
    bool gamezDesactivado;
    public bool rescatePagado;
    public int posicionMisionGamez;
    public int posicionGeneralMisionGamez;

    //Misión H 
    //0 -- uni 1 -- canda 2 -- pedran
    public bool[] hActiva;
    public GameObject[] personajesH;
    public bool misionHActiva;
    public bool conversacionH;

    //Misión Nani
    public bool misionNaniActiva;
    public GameObject Nani;
    public GameObject ObjetivoCupula;
    public GameObject Orco;
    public bool perdonarVidaOrco, orcoDerrotado, guardiaDerrotado;
    public int posicionMisionNani, posicionGeneralMisionNani;
    bool naniDesactivado, orcoDesactivado, guardiaCupulaDesactivado;

    ControlJugador controlJugador;



    private void Awake()
    {
        baseDeDatos = GameObject.Find("GameManager").GetComponent<BaseDatos>();
        controlJugador = GameObject.Find("Player").GetComponent<ControlJugador>();

        //Mision Pedro
        misionPedroActiva = false;
        pedroDesactivado = false;
        contadorMonstruosPedro = 0;
        posicionMisionPedro = posicionGeneralMisionPedro = -1;

        //Mision Gamez
        gamezDesactivado = ladronDesactivado = false;
        misionGamezActiva = false;
        tieneGema = false;
        rescatePagado = false;
        posicionMisionGamez = posicionGeneralMisionGamez = -1;

        //Mision Helena
        misionHActiva = false;
        hActiva = new bool[personajesH.Length];
        hActiva[0] = true;

        for (int i = 1; i < personajesH.Length; i++)
        {
            hActiva[i] = false;
            personajesH[i].SetActive(false);
        }

        //Mision Nani
        misionNaniActiva = false;
        perdonarVidaOrco = false;
        posicionGeneralMisionNani = posicionMisionNani = -1;
        naniDesactivado = orcoDesactivado = guardiaCupulaDesactivado = false;

    }



    void Update ()
    {
        //Mision Pedro
        if (baseDeDatos.personajesDesactivados[2] && !pedroDesactivado)
        {
            pedroDesactivado = true;
            Pedro.SetActive(false);
        }

        if (misionPedroActiva)
        {
            if(posicionMisionPedro == -1)
            {
                for (int i = 0; i < baseDeDatos.numeroMisionesReclutamiento; i++)
                {
                    if (baseDeDatos.listaMisionesReclutamiento[i].indice == 0)
                    {
                        posicionMisionPedro = i;
                    }
                }

                for (int i = 0; i < baseDeDatos.numeroMisionesActivas; i++)
                {
                    if (baseDeDatos.listaMisionesActivas[i].indice == 0)
                    {
                        posicionGeneralMisionPedro = i;

                        if (baseDeDatos.listaMisionesActivas[i].indice == 0)
                        {
                            if (baseDeDatos.idioma == 1)
                            {
                                baseDeDatos.listaMisionesActivas[posicionGeneralMisionPedro].estadoActual[0] = "- Enemies defeated " + contadorMonstruosPedro + "/10.";
                            }
                            else
                            {
                                baseDeDatos.listaMisionesActivas[posicionGeneralMisionPedro].estadoActual[0] = "- Enemigos derrotados " + contadorMonstruosPedro + "/10.";
                            }
                        }
                    }
                }
            }
            else if (baseDeDatos.listaMisionesReclutamiento[posicionMisionPedro].completada)
            {
                misionPedroActiva = false;
            }
        }

        //Mision Gamez
        if (baseDeDatos.personajesDesactivados[0] && !ladronDesactivado)
        {
            ladronDesactivado = true;
            Ladron.SetActive(false);
        }

        if (baseDeDatos.personajesDesactivados[1] && !gamezDesactivado)
        {
            gamezDesactivado = true;
            Gamez.SetActive(false);
        }

        if (misionGamezActiva)
        {
            if (posicionMisionGamez == -1)
            {
                for (int i = 0; i < baseDeDatos.numeroMisionesReclutamiento; i++)
                {
                    if (baseDeDatos.listaMisionesReclutamiento[i].indice == 1)
                    {
                        posicionMisionGamez = i;
                    }
                }

                for (int i = 0; i < baseDeDatos.numeroMisionesActivas; i++)
                {
                    if (baseDeDatos.listaMisionesActivas[i].indice == 1)
                    {
                        posicionGeneralMisionGamez = i;
                    }
                }
            }
            else if (baseDeDatos.listaMisionesReclutamiento[posicionMisionGamez].completada)
            {
                misionGamezActiva = false;
            }

            if (!tieneGema)
            {
                if (baseDeDatos.numeroObjetosClave != 0)
                {
                    for (int i = 0; i < baseDeDatos.numeroObjetosClave; i++)
                    {
                        if (baseDeDatos.listaObjetosClave[i].indiceTipo == 2)
                        {
                            tieneGema = true;
                        }
                    }
                }
            }
        }

        //Misión H
        if (misionHActiva)
        {
            if (!TextBox.ocultar && conversacionH)
            {
                if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                {
                    if (hActiva[0])
                    {
                        personajesH[0].SetActive(false);
                        personajesH[1].SetActive(true);
                    }
                    else if (hActiva[1])
                    {
                        personajesH[1].SetActive(false);
                        personajesH[2].SetActive(true);
                    }
                    else if (hActiva[3])
                    {
                        personajesH[3].SetActive(false);
                    }

                    conversacionH = false;
                }
                else if (Input.GetKeyDown(KeyCode.B) || Input.GetButtonUp("X"))
                {
                    if (hActiva[2])
                    {
                        personajesH[2].SetActive(false);
                        personajesH[3].SetActive(true);
                    }

                    conversacionH = false;
                }
            }
        }

        //Misión Nani
        if (baseDeDatos.personajesDesactivados[3] && !orcoDesactivado)
        {
            orcoDesactivado = true;
            Orco.SetActive(false);
        }

        if (baseDeDatos.personajesDesactivados[4] && !guardiaCupulaDesactivado)
        {
            guardiaCupulaDesactivado = true;
            ObjetivoCupula.SetActive(false);
        }

        if (baseDeDatos.personajesDesactivados[5] && !naniDesactivado)
        {
            naniDesactivado = true;
            Nani.SetActive(false);
        }

        if (misionNaniActiva)
        {
            if (posicionMisionNani == -1)
            {
                for (int i = 0; i < baseDeDatos.numeroMisionesReclutamiento; i++)
                {
                    if (baseDeDatos.listaMisionesReclutamiento[i].indice == 4)
                    {
                        posicionMisionNani = i;
                    }
                }

                for (int i = 0; i < baseDeDatos.numeroMisionesActivas; i++)
                {
                    if (baseDeDatos.listaMisionesActivas[i].indice == 4)
                    {
                        posicionGeneralMisionNani = i;
                    }
                }
            }

            if (baseDeDatos.listaMisionesReclutamiento[posicionMisionNani].completada)
            {
                misionNaniActiva = false;
            }
        }
    }



    public void CierraMisionSecundaria(int indice)
    {
        switch (indice)
        {
            case 0:
                Pedro.SetActive(false);
                baseDeDatos.ApagaPersonajes(2);
                break;
            case 1:
                Gamez.SetActive(false);
                baseDeDatos.ApagaPersonajes(1);
                Ladron.SetActive(false);
                break;
            case 2:
                personajesH[4].SetActive(false);
                break;
            case 4:
                Nani.SetActive(false);
                baseDeDatos.ApagaPersonajes(5);
                break;
        }

        controlJugador.SetConversacion(false);
    }



    public void PersonajeHActivo()
    {
        for(int i = 0; i < hActiva.Length; i++)
        {
            if (!hActiva[i])
            {
                personajesH[i].SetActive(false);
            }
        }
    }
}
