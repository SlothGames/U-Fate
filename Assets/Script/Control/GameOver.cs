using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour 
{
    int pos;
    int contador;

    public bool activo;
    bool carga;
    public bool espera;

    public GameObject pantallaFin;
    public GameObject puntoGuardadoMaestro;

    PuntoGuardado puntoGuardado;
    BaseDatos baseDeDatos;

    bool pulsado;

    float digitalY;

    MusicaManager musica;


    void Start ()
    {
        baseDeDatos = GameObject.Find("GameManager").GetComponent<BaseDatos>();
        puntoGuardado = puntoGuardadoMaestro.GetComponent<PuntoGuardado>();
        pulsado = false;
        digitalY = 0;
        contador = 0;
        musica = GameObject.Find("Musica").GetComponent<MusicaManager>();
        DesactivaMenu();
        carga = false;
    }



    void Update ()
    {
        if (activo)
        {
            digitalY = Input.GetAxis("D-Vertical");

            if (pulsado)
            {
                if (digitalY == 0)
                {
                    pulsado = false;
                }
            }

            if (!carga)
            { 
                if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                {
                    if (pos == 0)
                    {
                        contador++;
                        carga = true;
                        puntoGuardado.activo = true;
                        //DesactivaMenu();
                        pantallaFin.SetActive(false);
                        puntoGuardado.IniciaPGGameOver();
                    }
                    else
                    {
                        Application.Quit();
                    }
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || (!pulsado && digitalY != 0))
                {
                    pulsado = true;

                    if (pos == 0)
                    {
                        pos = 1;
                        pantallaFin.transform.GetChild(3).transform.position = pantallaFin.transform.GetChild(2).transform.GetChild(2).transform.position;
                    }
                    else
                    {
                        pos = 0;
                        pantallaFin.transform.GetChild(3).transform.position = pantallaFin.transform.GetChild(1).transform.GetChild(2).transform.position;
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("B"))
                {
                    contador--;

                    if(contador == 0)
                    {
                        pantallaFin.SetActive(true);
                        carga = false;
                    }
                    
                }
                else if(Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                {
                    contador++;
                }
            }
        }
	}



    public void ActivaMenu()
    {
        if(baseDeDatos.idioma == 1)
        {
            pantallaFin.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Load";
            pantallaFin.transform.GetChild(2).GetChild(1).GetComponent<Text>().text = "Exit";
        }
        else
        {
            pantallaFin.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Cargar";
            pantallaFin.transform.GetChild(2).GetChild(1).GetComponent<Text>().text = "Salir";
        }

        activo = true;
        carga = false;

        pantallaFin.SetActive(activo);
        pos = 1;
        pantallaFin.transform.GetChild(3).transform.position = pantallaFin.transform.GetChild(2).transform.GetChild(2).transform.position;
        musica.CambiaCancion(10);
    } 



    void DesactivaMenu()
    {
        activo = false;
        
        pantallaFin.SetActive(activo);
    }
}
