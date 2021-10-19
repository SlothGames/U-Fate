using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeleccionEquipo : MonoBehaviour {
    public GameObject retirarPersonaje;
    public GameObject aniadirPersonaje;
    public GameObject menuSeleccion;
    GameObject manager;

    BaseDatos baseDeDatos;
    ControlJugador controlJugador;
    MusicaManager musica;

    bool borrarPersonaje;
    bool incluyePersonaje;
    bool activo;

    int posFlecha;
    int posConf;
    int posBD;
    int totalElementos;
    int pagina;
    int aMostrar;

    bool pulsado;

    float digitalX;
    float digitalY;

    void Start ()
    {
        pulsado = false;
        manager = GameObject.Find("GameManager");
        musica = GameObject.Find("EfectosSonido").GetComponent<MusicaManager>();
        baseDeDatos = manager.GetComponent<BaseDatos>();
        controlJugador = GameObject.Find("Player").GetComponent<ControlJugador>();

        DesactivarAniadido();
        DesactivarRetirada();
        DesactivaSeleccion();

        posConf = 0;
        posBD = 0;
        pagina = 0;
        aMostrar = 0;
    }



    void Update()
    {
        if (activo)
        {
            if (!TextBox.on)
            {
                digitalX = Input.GetAxis("D-Horizontal");
                digitalY = Input.GetAxis("D-Vertical");

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


                if (pulsado)
                {
                    if (digitalY == 0)
                    {
                        pulsado = false;
                    }
                }


                if (borrarPersonaje)
                {
                    if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                    {
                        if (baseDeDatos.listaMisionesPrincipales[1].completada)
                        {
                            musica.ProduceEfecto(10);
                            baseDeDatos.listaPersonajesAliados[baseDeDatos.equipoAliado[posConf + 1].indicePersonaje] = baseDeDatos.equipoAliado[posConf + 1];
                            baseDeDatos.personajesAlmacenados[baseDeDatos.numeroAlmacenado] = baseDeDatos.equipoAliado[posConf + 1].indicePersonaje;
                            baseDeDatos.numeroAlmacenado++;
                            baseDeDatos.numeroIntegrantesEquipo--;

                            if (baseDeDatos.numeroIntegrantesEquipo == 1)
                            {
                                DesactivarRetirada();
                                ActivaMenu();
                            }
                            else if (baseDeDatos.numeroIntegrantesEquipo == 2)
                            {
                                if (posConf == 0)
                                {
                                    baseDeDatos.equipoAliado[1] = baseDeDatos.equipoAliado[2];

                                    retirarPersonaje.transform.GetChild(2).transform.GetChild(0).GetComponent<Image>().sprite = baseDeDatos.equipoAliado[1].imagen;
                                    retirarPersonaje.transform.GetChild(2).transform.GetChild(1).GetComponent<Text>().text = baseDeDatos.equipoAliado[1].nombre;
                                    
                                    if(baseDeDatos.idioma == 1)
                                    {
                                        retirarPersonaje.transform.GetChild(2).transform.GetChild(2).GetComponent<Text>().text = baseDeDatos.equipoAliado[1].elementoIngles + "";
                                    }
                                    else
                                    {
                                        retirarPersonaje.transform.GetChild(2).transform.GetChild(2).GetComponent<Text>().text = baseDeDatos.equipoAliado[1].elemento + "";
                                    }

                                    retirarPersonaje.transform.GetChild(2).transform.GetChild(3).GetComponent<Text>().text = "Lv: " + baseDeDatos.equipoAliado[1].nivel;
                                }

                                retirarPersonaje.transform.GetChild(3).gameObject.SetActive(false);
                            }
                        }
                        else
                        {
                            if(baseDeDatos.idioma == 1)
                            {
                                TextBox.MuestraTexto("You can't do that now.", false);
                            }
                            else
                            {
                                TextBox.MuestraTexto("No puedes hacer eso ahora.", false);
                            }
                        }

                    }
                    else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) ||
                        Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) 
                        || (!pulsado && digitalY != 0))
                    {
                        musica.ProduceEfecto(11);
                        pulsado = true;

                        if (posConf == 0)
                        {
                            posConf = 1;
                            retirarPersonaje.transform.GetChild(4).transform.position = retirarPersonaje.transform.GetChild(3).transform.GetChild(4).transform.position;
                        }
                        else
                        {
                            retirarPersonaje.transform.GetChild(4).transform.position = retirarPersonaje.transform.GetChild(2).transform.GetChild(4).transform.position;
                            posConf = 0;
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("B"))
                    {
                        musica.ProduceEfecto(12);
                        DesactivarRetirada();
                        ActivaMenu();
                    }
                }
                else if (incluyePersonaje)
                {
                    if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                    {
                        musica.ProduceEfecto(10);
                        int indice = baseDeDatos.personajesAlmacenados[posBD];

                        if (baseDeDatos.numeroIntegrantesEquipo < 3)
                        {
                            baseDeDatos.equipoAliado[baseDeDatos.numeroIntegrantesEquipo] = baseDeDatos.listaPersonajesAliados[indice];
                            baseDeDatos.numeroIntegrantesEquipo++;

                            if (baseDeDatos.numeroAlmacenado > 2)
                            {
                                for (int i = posBD; i < baseDeDatos.numeroAlmacenado - 1; i++)
                                {
                                    baseDeDatos.personajesAlmacenados[i] = baseDeDatos.personajesAlmacenados[i + 1];
                                }
                            }
                            else if (baseDeDatos.numeroAlmacenado == 2)
                            {
                                if(posBD == 0)
                                {
                                    baseDeDatos.personajesAlmacenados[0] = baseDeDatos.personajesAlmacenados[1];
                                }
                            }
                            else
                            {
                                DesactivarAniadido();
                                ActivaMenu();
                            }

                            baseDeDatos.numeroAlmacenado--;
                            aMostrar--;

                            if (aMostrar == 0)
                            {
                                DesactivarAniadido();
                            }
                            else
                            {
                                PasaPagina();
                            }
                            
                        }
                        else
                        {
                            if(baseDeDatos.idioma == 1)
                            {
                                TextBox.MuestraTexto("Maximum number reached.", false);
                            }
                            else
                            {
                                TextBox.MuestraTexto("Equipo al máximo.", false);
                            }
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || (!pulsado && digitalY > 0))
                    {
                        musica.ProduceEfecto(11);
                        pulsado = true;

                        if (posConf == 1)
                        {
                            posConf = 0;
                            aniadirPersonaje.transform.GetChild(6).transform.position = aniadirPersonaje.transform.GetChild(2).transform.GetChild(0).transform.position;
                            posBD--;
                        }
                        else if (posConf == 2)
                        {
                            posConf = 1;
                            aniadirPersonaje.transform.GetChild(6).transform.position = aniadirPersonaje.transform.GetChild(3).transform.GetChild(0).transform.position;
                            posBD--;
                        }
                        else if(posConf == 3)
                        {
                            posConf = 2;
                            aniadirPersonaje.transform.GetChild(6).transform.position = aniadirPersonaje.transform.GetChild(4).transform.GetChild(0).transform.position;
                            posBD--;
                        }
                        else
                        {
                            if(pagina != 0)
                            {
                                pagina--;
                                PasaPagina();
                                posConf = 0;
                                aniadirPersonaje.transform.GetChild(6).transform.position = aniadirPersonaje.transform.GetChild(2).transform.GetChild(0).transform.position;
                            }
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || (!pulsado && digitalY < 0))
                    {
                        musica.ProduceEfecto(11);
                        pulsado = true;

                        if (posConf == 0)
                        {
                            if(aMostrar > 1)
                            {
                                posConf = 1;
                                aniadirPersonaje.transform.GetChild(6).transform.position = aniadirPersonaje.transform.GetChild(3).transform.GetChild(0).transform.position;
                                posBD++;
                            }
                        }
                        else if (posConf == 1)
                        {
                            if(aMostrar > 2)
                            {
                                posConf = 2;
                                aniadirPersonaje.transform.GetChild(6).transform.position = aniadirPersonaje.transform.GetChild(4).transform.GetChild(0).transform.position;
                                posBD++;
                            }
                        }
                        else if (posConf == 2)
                        {
                            if(aMostrar > 3)
                            {
                                posConf = 3;
                                aniadirPersonaje.transform.GetChild(6).transform.position = aniadirPersonaje.transform.GetChild(5).transform.GetChild(0).transform.position;
                                posBD++;
                            }
                        }
                        else
                        {
                            if((totalElementos - 1) > posBD)
                            {
                                posConf = 0;
                                aniadirPersonaje.transform.GetChild(6).transform.position = aniadirPersonaje.transform.GetChild(2).transform.GetChild(0).transform.position;
                                pagina++;
                                PasaPagina();
                            }
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("B"))
                    {
                        musica.ProduceEfecto(12);
                        DesactivarAniadido();
                        ActivaMenu();
                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                    {
                        musica.ProduceEfecto(10);

                        if (posFlecha == 0)
                        {
                            if(baseDeDatos.numeroAlmacenado == 0)
                            {
                                if(baseDeDatos.idioma == 1)
                                {
                                    TextBox.MuestraTexto("There are no allies available.", false);
                                }
                                else
                                {
                                    TextBox.MuestraTexto("No hay aliados disponibles.", false);
                                }
                            }
                            else
                            {
                                DesactivaMenu();
                                ActivarAniadido();
                                posBD = 0;
                            }
                        }
                        else if(posFlecha == 1)
                        {
                            if (baseDeDatos.numeroIntegrantesEquipo == 1)
                            {
                                if(baseDeDatos.idioma == 1)
                                {
                                    TextBox.MuestraTexto("Minimum number of members reached.", false);
                                }
                                else
                                {
                                    TextBox.MuestraTexto("Equipo al mínimo.", false);
                                }
                            }
                            else
                            {
                                DesactivaMenu();
                                ActivaRetirada();
                                posBD = 0;
                                posConf = 0;
                                PasaPagina();
                            }
                        }
                        else
                        {
                            DesactivaSeleccion();
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || (!pulsado && digitalY > 0))
                    {
                        musica.ProduceEfecto(11);
                        pulsado = true;

                        if (posFlecha == 0)
                        {
                            posFlecha = 2;
                        }
                        else
                        {
                            posFlecha--;
                        }

                        menuSeleccion.transform.GetChild(5).transform.position = menuSeleccion.transform.GetChild(2 + posFlecha).transform.GetChild(2).transform.position;
                    }
                    else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || (!pulsado && digitalY < 0))
                    {
                        musica.ProduceEfecto(11);
                        pulsado = true;

                        if (posFlecha == 2)
                        {
                            posFlecha = 0;
                        }
                        else
                        {
                            posFlecha++;
                        }

                        menuSeleccion.transform.GetChild(5).transform.position = menuSeleccion.transform.GetChild(2 + posFlecha).transform.GetChild(2).transform.position;
                    }
                    else if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("B"))
                    {
                        musica.ProduceEfecto(12);
                        DesactivaSeleccion();
                    }
                }
            }
        }
    }



    public void IniciaSeleccion()
    {
        if (!activo)
        {
            if (baseDeDatos.mandoActivo)
            {
                menuSeleccion.transform.GetChild(6).GetChild(0).GetComponent<Image>().sprite = baseDeDatos.seleccionXBOX[0];
                menuSeleccion.transform.GetChild(6).GetChild(2).GetComponent<Image>().sprite = baseDeDatos.volverXBOX[0];
                menuSeleccion.transform.GetChild(6).GetChild(4).GetComponent<Image>().sprite = baseDeDatos.moverXBOX[0];
            }
            else
            {
                menuSeleccion.transform.GetChild(6).GetChild(0).GetComponent<Image>().sprite = baseDeDatos.seleccionPC[0];
                menuSeleccion.transform.GetChild(6).GetChild(2).GetComponent<Image>().sprite = baseDeDatos.volverPC[0];
                menuSeleccion.transform.GetChild(6).GetChild(4).GetComponent<Image>().sprite = baseDeDatos.moverPC[0];
            }

            controlJugador.setMensajeActivo(true);
            controlJugador.SetInterrogante(false);
            totalElementos = baseDeDatos.numeroAlmacenado;
            ActivaMenu();
            StartCoroutine(EsperaInicio());
        }
    }



    void DesactivaSeleccion()
    {
        DesactivaMenu();
        controlJugador.setMensajeActivo(false);
        activo = false;
        menuSeleccion.SetActive(activo);
    }



    void ActivaRetirada()
    {
        borrarPersonaje = true;
        retirarPersonaje.SetActive(borrarPersonaje);
        posConf = 0;
        retirarPersonaje.transform.GetChild(4).transform.position = retirarPersonaje.transform.GetChild(2).transform.GetChild(4).transform.position;

        if(baseDeDatos.numeroIntegrantesEquipo == 2)
        {
            retirarPersonaje.transform.GetChild(3).gameObject.SetActive(false);
        }
        else
        {
            retirarPersonaje.transform.GetChild(3).gameObject.SetActive(true);
        }

        if(baseDeDatos.idioma == 1)
        {
            retirarPersonaje.transform.GetChild(2).transform.GetChild(2).GetComponent<Text>().text = baseDeDatos.equipoAliado[1].elementoIngles + "";
            retirarPersonaje.transform.GetChild(3).transform.GetChild(2).GetComponent<Text>().text = baseDeDatos.equipoAliado[2].elementoIngles + "";
        }
        else
        {
            retirarPersonaje.transform.GetChild(2).transform.GetChild(2).GetComponent<Text>().text = baseDeDatos.equipoAliado[1].elemento + "";
            retirarPersonaje.transform.GetChild(3).transform.GetChild(2).GetComponent<Text>().text = baseDeDatos.equipoAliado[2].elemento + "";
        }

        retirarPersonaje.transform.GetChild(2).transform.GetChild(0).GetComponent<Image>().sprite = baseDeDatos.equipoAliado[1].imagen;
        retirarPersonaje.transform.GetChild(2).transform.GetChild(1).GetComponent<Text>().text = baseDeDatos.equipoAliado[1].nombre;
        retirarPersonaje.transform.GetChild(2).transform.GetChild(3).GetComponent<Text>().text = "Lv: " + baseDeDatos.equipoAliado[1].nivel;

        retirarPersonaje.transform.GetChild(3).transform.GetChild(0).GetComponent<Image>().sprite = baseDeDatos.equipoAliado[2].imagen;
        retirarPersonaje.transform.GetChild(3).transform.GetChild(1).GetComponent<Text>().text = baseDeDatos.equipoAliado[2].nombre;
        retirarPersonaje.transform.GetChild(3).transform.GetChild(3).GetComponent<Text>().text = "Lv:" + baseDeDatos.equipoAliado[2].nivel;
    }



    void DesactivarRetirada()
    {
        borrarPersonaje = false;
        retirarPersonaje.SetActive(borrarPersonaje);
    }



    void ActivarAniadido()
    {
        incluyePersonaje = true;
        aniadirPersonaje.SetActive(incluyePersonaje);
        posBD = 0;
        posConf = 0;
        pagina = 0;
        aniadirPersonaje.transform.GetChild(6).transform.position = aniadirPersonaje.transform.GetChild(2).transform.GetChild(0).transform.position;
        PasaPagina();
    }



    void DesactivarAniadido()
    {
        incluyePersonaje = false;
        aniadirPersonaje.SetActive(incluyePersonaje);
    }



    void ActivaMenu()
    {
        posFlecha = 0;
        menuSeleccion.SetActive(true);
        menuSeleccion.transform.GetChild(5).transform.position = menuSeleccion.transform.GetChild(2).transform.GetChild(2).transform.position;

        if(baseDeDatos.idioma == 1)
        {
            menuSeleccion.transform.GetChild(1).GetComponent<Text>().text = "Team Selection";
            menuSeleccion.transform.GetChild(2).GetChild(1).GetComponent<Text>().text = "Add";
            menuSeleccion.transform.GetChild(3).GetChild(1).GetComponent<Text>().text = "Relieve";
            menuSeleccion.transform.GetChild(4).GetChild(1).GetComponent<Text>().text = "Back";

            menuSeleccion.transform.GetChild(6).GetChild(1).GetComponent<Text>().text = "Select";
            menuSeleccion.transform.GetChild(6).GetChild(3).GetComponent<Text>().text = "Back";
            menuSeleccion.transform.GetChild(6).GetChild(5).GetComponent<Text>().text = "Move";
        }
        else
        {
            menuSeleccion.transform.GetChild(1).GetComponent<Text>().text = "Selección de Equipo";
            menuSeleccion.transform.GetChild(2).GetChild(1).GetComponent<Text>().text = "Añadir";
            menuSeleccion.transform.GetChild(3).GetChild(1).GetComponent<Text>().text = "Retirar";
            menuSeleccion.transform.GetChild(4).GetChild(1).GetComponent<Text>().text = "Salir";

            menuSeleccion.transform.GetChild(6).GetChild(1).GetComponent<Text>().text = "Seleccionar";
            menuSeleccion.transform.GetChild(6).GetChild(3).GetComponent<Text>().text = "Volver";
            menuSeleccion.transform.GetChild(6).GetChild(5).GetComponent<Text>().text = "Mover";
        }
    }



    void DesactivaMenu()
    {

    }



    void PasaPagina()
    {
        totalElementos = baseDeDatos.numeroAlmacenado;
        
        aMostrar = totalElementos - ((pagina + 1) * 4);
             
        if (aMostrar >= 0)
        {
            aMostrar = 4;
        }
        else
        {
            aMostrar = totalElementos - (pagina * 4);
        }

        posBD = pagina * 4;

        if (posBD == 0)
        {
            aniadirPersonaje.transform.GetChild(7).gameObject.SetActive(false);
        }
        else
        {
            aniadirPersonaje.transform.GetChild(7).gameObject.SetActive(true);
        }

        if (aMostrar == 4)
        {
            if((totalElementos - ((pagina + 1) * 4)) > 0)
            {
                aniadirPersonaje.transform.GetChild(8).gameObject.SetActive(true);
            }
            else
            {
                aniadirPersonaje.transform.GetChild(8).gameObject.SetActive(false);
            }

            for (int i = 0; i < 4; i++)
            {
                aniadirPersonaje.transform.GetChild(2 + i).gameObject.SetActive(true);
            }
        }
        else
        {
            aniadirPersonaje.transform.GetChild(8).gameObject.SetActive(false);

            for(int i = 0; i < 4; i++)
            {
                if(i < aMostrar)
                {
                    aniadirPersonaje.transform.GetChild(2 + i).gameObject.SetActive(true);
                }
                else
                {
                    aniadirPersonaje.transform.GetChild(2 + i).gameObject.SetActive(false);
                }
            }
        }
        

        for (int i = 0; i < aMostrar; i++)
        {
            aniadirPersonaje.transform.GetChild(2 + i).transform.GetChild(1).GetComponent<Image>().sprite = baseDeDatos.listaPersonajesAliados[baseDeDatos.personajesAlmacenados[posBD + i]].imagen;
            aniadirPersonaje.transform.GetChild(2 + i).transform.GetChild(2).GetComponent<Text>().text = baseDeDatos.listaPersonajesAliados[baseDeDatos.personajesAlmacenados[posBD + i]].nombre;
            aniadirPersonaje.transform.GetChild(2 + i).transform.GetChild(3).GetComponent<Text>().text = "Lv: " + baseDeDatos.listaPersonajesAliados[baseDeDatos.personajesAlmacenados[posBD + i]].nivel;
            aniadirPersonaje.transform.GetChild(2 + i).transform.GetChild(4).GetComponent<Text>().text = baseDeDatos.listaPersonajesAliados[baseDeDatos.personajesAlmacenados[posBD + i]].vidaActual + "/" + baseDeDatos.listaPersonajesAliados[baseDeDatos.personajesAlmacenados[posBD]].vidaModificada;
           
            if(baseDeDatos.idioma == 1)
            {
                aniadirPersonaje.transform.GetChild(2 + i).transform.GetChild(5).GetComponent<Text>().text = baseDeDatos.listaPersonajesAliados[baseDeDatos.personajesAlmacenados[posBD + i]].elementoIngles + "";
            }
            else
            {
                aniadirPersonaje.transform.GetChild(2 + i).transform.GetChild(5).GetComponent<Text>().text = baseDeDatos.listaPersonajesAliados[baseDeDatos.personajesAlmacenados[posBD + i]].elemento + "";
            }
        }
    }



    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            controlJugador.SetInterrogante(true);
        }

        yield return null;
    }



    IEnumerator OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            controlJugador.SetInterrogante(false);
        }

        yield return null;
    }



    IEnumerator EsperaInicio()
    {
        yield return new WaitForSeconds(0.1f);
        activo = true;
    }



    void cambiaControl()
    {
        if (!baseDeDatos.mandoActivo)
        {
            baseDeDatos.mandoActivo = true;

            menuSeleccion.transform.GetChild(6).GetChild(0).GetComponent<Image>().sprite = baseDeDatos.seleccionXBOX[0];
            menuSeleccion.transform.GetChild(6).GetChild(2).GetComponent<Image>().sprite = baseDeDatos.volverXBOX[0];
            menuSeleccion.transform.GetChild(6).GetChild(4).GetComponent<Image>().sprite = baseDeDatos.moverXBOX[0];
        }
        else
        {
            baseDeDatos.mandoActivo = false;

            menuSeleccion.transform.GetChild(6).GetChild(0).GetComponent<Image>().sprite = baseDeDatos.seleccionPC[0];
            menuSeleccion.transform.GetChild(6).GetChild(2).GetComponent<Image>().sprite = baseDeDatos.volverPC[0];
            menuSeleccion.transform.GetChild(6).GetChild(4).GetComponent<Image>().sprite = baseDeDatos.moverPC[0];
        }
    }
}
