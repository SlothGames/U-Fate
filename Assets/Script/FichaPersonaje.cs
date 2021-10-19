using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FichaPersonaje : MonoBehaviour {
    public Canvas menu;
    public GameObject info, menuObjetos, confirmacion;
    public GameObject ayuda;
    public Image imagen;

    public bool activo;
    bool realizaAccion;
    bool confirmacionActiva;

    int pos;
    int posObjeto;
    int posConfirmacion;
    int numeroObjetos;
    int posicionEnEquipo;

    GameObject manager;
    BaseDatos baseDeDatos;
    MusicaManager musica;

    bool pulsado;

    float digitalX;
    float digitalY;



    void Start ()
    {
        manager = GameObject.Find("GameManager");
        baseDeDatos = manager.GetComponent<BaseDatos>();
        musica = GameObject.Find("EfectosSonido").GetComponent<MusicaManager>();

        DesactivaMenu();
        pos = 0;
        posObjeto = 0;
        posConfirmacion = 0;
        realizaAccion = false;
        confirmacionActiva = false;
        numeroObjetos = 0;

        DesactivarConfirmacion();
        DesactivarMenuObjetos();

        info.transform.GetChild(3).transform.GetChild(2).gameObject.SetActive(false);
        info.transform.GetChild(4).transform.GetChild(2).gameObject.SetActive(false);
        info.transform.GetChild(6).transform.GetChild(2).gameObject.SetActive(false);
        info.transform.GetChild(5).transform.GetChild(2).gameObject.SetActive(false);
        info.transform.GetChild(7).transform.GetChild(2).gameObject.SetActive(false);
        info.transform.GetChild(8).transform.GetChild(2).gameObject.SetActive(false);
        info.transform.GetChild(9).transform.GetChild(2).gameObject.SetActive(false);
    }



    void Update ()
    {
        digitalX = Input.GetAxis("D-Horizontal");
        digitalY = Input.GetAxis("D-Vertical");

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

        if (realizaAccion)
        {
            if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("B"))
            {
                musica.ProduceEfecto(12);
                DesactivarMenuObjetos();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || (!pulsado && digitalY < 0))
            {
                musica.ProduceEfecto(11);
                pulsado = true;

                if (numeroObjetos != 0)
                {
                    posObjeto++;

                    if (posObjeto == numeroObjetos)
                    {
                        posObjeto = 0;
                    }

                    MueveFlechaObjeto();
                }
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || (!pulsado && digitalY > 0))
            {
                musica.ProduceEfecto(11);
                pulsado = true;

                if (numeroObjetos != 0)
                {
                    posObjeto--;

                    if (posObjeto < 0)
                    {
                        posObjeto = numeroObjetos - 1;
                    }

                    MueveFlechaObjeto();
                }
            }
            else if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
            {
                musica.ProduceEfecto(10);
                if (numeroObjetos != 0)
                {
                    CompruebaSiHayObjeto();
                    DesactivarMenuObjetos();
                }
            }
        }
        else if (confirmacionActiva)
        {
            if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("B"))
            {
                musica.ProduceEfecto(12);
                DesactivarConfirmacion();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || (!pulsado && digitalY != 0))
            {
                musica.ProduceEfecto(11);
                pulsado = true;
                MueveFlechaConfirmacion();
            }
            else if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
            {
                musica.ProduceEfecto(10);
                if (posConfirmacion == 1)
                {
                    QuitarObjeto();
                    ActualizarListaObjetos();
                }
                
                DesactivarConfirmacion();
                DesactivarMenuObjetos();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("B"))
            {
                musica.ProduceEfecto(12);
                DesactivaMenu();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || (!pulsado && digitalY < 0))
            {
                musica.ProduceEfecto(11);
                pulsado = true;
                pos++;

                if (pos > 5)
                {
                    pos = 0;
                }

                MueveFlecha();
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || (!pulsado && digitalY > 0))
            {
                musica.ProduceEfecto(11);
                pulsado = true;
                pos--;

                if (pos < 0)
                {
                    pos = 5;
                }

                MueveFlecha();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || (!pulsado && digitalX < 0))
            {
                musica.ProduceEfecto(11);
                pulsado = true;

                if(baseDeDatos.numeroIntegrantesEquipo > 1)
                {
                    posicionEnEquipo--;

                    if (posicionEnEquipo < 0)
                    {
                        posicionEnEquipo = baseDeDatos.numeroIntegrantesEquipo - 1;
                    }

                    ActivaMenu(posicionEnEquipo);
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || (!pulsado && digitalX > 0))
            {
                musica.ProduceEfecto(11);
                pulsado = true;

                if (baseDeDatos.numeroIntegrantesEquipo > 1)
                {
                    posicionEnEquipo++;

                    if (baseDeDatos.numeroIntegrantesEquipo > 2)
                    {
                        if (posicionEnEquipo > 2)
                        {
                            posicionEnEquipo = 0;
                        }
                    }
                    else
                    {
                        if (posicionEnEquipo > 1)
                        {
                            posicionEnEquipo = 0;
                        }
                    }
                       

                    ActivaMenu(posicionEnEquipo);
                }
            }
            else if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
            {
                musica.ProduceEfecto(10);
                IniciaMenuObjetos();
            }
        }
    }



    void CompruebaSiHayObjeto()
    {
        if (pos == 0)
        {
            if (!baseDeDatos.equipoAliado[posicionEnEquipo].llevaCasco || baseDeDatos.equipoAliado[posicionEnEquipo].casco.indice != baseDeDatos.cascos[posObjeto].indice)
            {
                if(baseDeDatos.cantidadCascos[posObjeto] != 0)
                {
                    EquiparObjeto();
                    DesactivarMenuObjetos();
                }
            }
            else
            {
                ActivarConfirmacion();
            }
        }
        else if (pos == 1)
        {
            if (!baseDeDatos.equipoAliado[posicionEnEquipo].llevaArmadura || baseDeDatos.equipoAliado[posicionEnEquipo].armadura.indice != baseDeDatos.armaduras[posObjeto].indice)
            {
                if (baseDeDatos.cantidadArmaduras[posObjeto] != 0)
                {
                    EquiparObjeto();
                    DesactivarMenuObjetos();
                }
            }
            else
            {
                ActivarConfirmacion();
            }
        }
        else if (pos == 2)
        {
            if (!baseDeDatos.equipoAliado[posicionEnEquipo].llevaBotas || baseDeDatos.equipoAliado[posicionEnEquipo].botas.indice != baseDeDatos.botas[posObjeto].indice)
            {
                if (baseDeDatos.cantidadBotas[posObjeto] != 0)
                {
                    EquiparObjeto();
                    DesactivarMenuObjetos();
                }
            }
            else
            {
                ActivarConfirmacion();
            }
        }
        else if (pos == 3)
        {
            if (!baseDeDatos.equipoAliado[posicionEnEquipo].llevaArma || baseDeDatos.equipoAliado[posicionEnEquipo].arma.indice != baseDeDatos.armas[posObjeto].indice)
            {
                if (baseDeDatos.cantidadArmas[posObjeto] != 0)
                {
                    EquiparObjeto();
                    DesactivarMenuObjetos();
                }
            }
            else
            {
                ActivarConfirmacion();
            }
        }
        else if (pos == 4)
        {
            if (!baseDeDatos.equipoAliado[posicionEnEquipo].llevaEscudo || baseDeDatos.equipoAliado[posicionEnEquipo].escudo.indice != baseDeDatos.escudos[posObjeto].indice)
            {
                if (baseDeDatos.cantidadEscudos[posObjeto] != 0)
                {
                    EquiparObjeto();
                    DesactivarMenuObjetos();
                }
            }
            else
            {
                ActivarConfirmacion();
            }
        }
        else if (pos == 5)
        {
            if (!baseDeDatos.equipoAliado[posicionEnEquipo].llevaComplemento || baseDeDatos.equipoAliado[posicionEnEquipo].complemento.indice != baseDeDatos.complemento[posObjeto].indice)
            {
                if (baseDeDatos.cantidadComplemento[posObjeto] != 0)
                {
                    EquiparObjeto();
                    DesactivarMenuObjetos();
                }
            }
            else
            {
                ActivarConfirmacion();
            }
        }

        ActualizarListaObjetos();
    }



    public void ActivaMenu(int posEquipo)
    {
        if (baseDeDatos.mandoActivo)
        {
            ayuda.transform.GetChild(3).GetComponent<Image>().sprite = baseDeDatos.volverXBOX[0];
            ayuda.transform.GetChild(1).GetComponent<Image>().sprite = baseDeDatos.seleccionXBOX[0];
            ayuda.transform.GetChild(5).GetComponent<Image>().sprite = baseDeDatos.moverXBOX[0];
        }
        else
        {
            ayuda.transform.GetChild(3).GetComponent<Image>().sprite = baseDeDatos.volverPC[0];
            ayuda.transform.GetChild(1).GetComponent<Image>().sprite = baseDeDatos.seleccionPC[0];
            ayuda.transform.GetChild(5).GetComponent<Image>().sprite = baseDeDatos.moverPC[0];
        }

        pos = 0;
        posicionEnEquipo = posEquipo;
        activo = true;
        realizaAccion = false;
        confirmacionActiva = false;
        menu.gameObject.SetActive(activo);
        MueveFlecha();

        Sprite imagenAux;
        Sprite[] imagenes;

        if (posEquipo == 0)
        {
            if (baseDeDatos.faccion == 0) //golpista
            {
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/TrajesProta/Emperador");
                imagenAux = imagenes[1];
                imagen.sprite = imagenAux;
            }
            else if (baseDeDatos.faccion == 1) //imperio
            {
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/TrajesProta/GuardiaImperial");
                imagenAux = imagenes[1];
                imagen.sprite = imagenAux;
            }
            else if (baseDeDatos.faccion == 2) //regente
            {
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/TrajesProta/GuardiaReal");
                imagenAux = imagenes[1];
                imagen.sprite = imagenAux;
            }
            else if (baseDeDatos.faccion == 3) //resistencia
            {
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/TrajesProta/Resistencia");
                imagenAux = imagenes[1];
                imagen.sprite = imagenAux;
            }
            else if (baseDeDatos.faccion == 4) //R.Asalto
            {
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/TrajesProta/FuerzasAsalto");
                imagenAux = imagenes[1];
                imagen.sprite = imagenAux;
            }
            else if (baseDeDatos.faccion == 5) //R.Especiales
            {
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/TrajesProta/EquipoFuerzasEspeciales");
                imagenAux = imagenes[1];
                imagen.sprite = imagenAux;
            }
            else if (baseDeDatos.faccion == 6) //R.Investigación
            {
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/TrajesProta/EquipoInteligencia");
                imagenAux = imagenes[1];
                imagen.sprite = imagenAux;
            }
            else if (baseDeDatos.faccion == 7) //Cúpula
            {
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/TrajesProta/Cupula");
                imagenAux = imagenes[1];
                imagen.sprite = imagenAux;
            }
            else if (baseDeDatos.faccion == 8) //Ninguna
            {
                imagen.sprite = baseDeDatos.equipoAliado[posEquipo].imagen;
            }
            else if (baseDeDatos.faccion == 9) //anarquista
            {
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/TrajesProta/Anarquista");
                imagenAux = imagenes[1];
                imagen.sprite = imagenAux;
            }
        }
        else
        {
            imagen.sprite = baseDeDatos.equipoAliado[posEquipo].imagen;
        }
        
        int numeroAtaques = baseDeDatos.equipoAliado[posEquipo].numeroAtaques;

        if (baseDeDatos.idioma == 1)
        {
            info.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = baseDeDatos.equipoAliado[posEquipo].nombreIngles;

            info.transform.GetChild(2).transform.GetChild(1).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posEquipo].elementoIngles;

            info.transform.GetChild(3).transform.GetChild(0).GetComponent<Text>().text = "Max. LP";
            info.transform.GetChild(4).transform.GetChild(0).GetComponent<Text>().text = "Phy. Atck.";
            info.transform.GetChild(6).transform.GetChild(0).GetComponent<Text>().text = "Phy. Def.";
            info.transform.GetChild(5).transform.GetChild(0).GetComponent<Text>().text = "Mag. Atck.";
            info.transform.GetChild(7).transform.GetChild(0).GetComponent<Text>().text = "Mag. Def.";
            info.transform.GetChild(8).transform.GetChild(0).GetComponent<Text>().text = "Speed";

            info.transform.GetChild(12).transform.GetChild(0).GetComponent<Text>().text = "Helmet.";
            info.transform.GetChild(13).transform.GetChild(0).GetComponent<Text>().text = "Armor";
            info.transform.GetChild(14).transform.GetChild(0).GetComponent<Text>().text = "Boots";
            info.transform.GetChild(15).transform.GetChild(0).GetComponent<Text>().text = "Weapon";
            info.transform.GetChild(16).transform.GetChild(0).GetComponent<Text>().text = "Shield";
            info.transform.GetChild(17).transform.GetChild(0).GetComponent<Text>().text = "Complement";

            info.transform.GetChild(20).transform.GetChild(0).GetComponent<Text>().text = baseDeDatos.equipoAliado[posEquipo].habilidades[0].nombreIngles + "  ER: " + baseDeDatos.equipoAliado[posEquipo].habilidades[0].energiaActual + "/" + baseDeDatos.equipoAliado[posEquipo].habilidades[0].energia;

            if (numeroAtaques > 1)
            {
                info.transform.GetChild(21).transform.GetChild(0).gameObject.SetActive(true);
                info.transform.GetChild(21).transform.GetChild(0).GetComponent<Text>().text = baseDeDatos.equipoAliado[posEquipo].habilidades[1].nombreIngles + "  ER: " + baseDeDatos.equipoAliado[posEquipo].habilidades[1].energiaActual + "/" + baseDeDatos.equipoAliado[posEquipo].habilidades[1].energia;
            }
            else
            {
                info.transform.GetChild(21).transform.GetChild(0).gameObject.SetActive(false);
                info.transform.GetChild(22).transform.GetChild(0).gameObject.SetActive(false);
                info.transform.GetChild(23).transform.GetChild(0).gameObject.SetActive(false);
            }

            if (numeroAtaques > 2)
            {
                info.transform.GetChild(22).transform.GetChild(0).gameObject.SetActive(true);
                info.transform.GetChild(22).transform.GetChild(0).GetComponent<Text>().text = baseDeDatos.equipoAliado[posEquipo].habilidades[2].nombreIngles + "  ER: " + baseDeDatos.equipoAliado[posEquipo].habilidades[2].energiaActual + "/" + baseDeDatos.equipoAliado[posEquipo].habilidades[2].energia;
            }
            else
            {
                info.transform.GetChild(22).transform.GetChild(0).gameObject.SetActive(false);
                info.transform.GetChild(23).transform.GetChild(0).gameObject.SetActive(false);
            }

            if (numeroAtaques == 4)
            {
                info.transform.GetChild(23).transform.GetChild(0).gameObject.SetActive(true);
                info.transform.GetChild(23).transform.GetChild(0).GetComponent<Text>().text = baseDeDatos.equipoAliado[posEquipo].habilidades[3].nombreIngles + "  ER: " + baseDeDatos.equipoAliado[posEquipo].habilidades[3].energiaActual + "/" + baseDeDatos.equipoAliado[posEquipo].habilidades[3].energia;
            }
            else
            {
                info.transform.GetChild(23).transform.GetChild(0).gameObject.SetActive(false);
            }

            if (baseDeDatos.equipoAliado[posEquipo].casco == null)
            {
                info.transform.GetChild(12).transform.GetChild(1).GetComponent<Text>().text = "";
            }
            else
            {
                info.transform.GetChild(12).transform.GetChild(1).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posEquipo].casco.nombreIngles;
            }

            if (baseDeDatos.equipoAliado[posEquipo].armadura == null)
            {
                info.transform.GetChild(13).transform.GetChild(1).GetComponent<Text>().text = "";
            }
            else
            {
                info.transform.GetChild(13).transform.GetChild(1).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posEquipo].armadura.nombreIngles;
            }

            if (baseDeDatos.equipoAliado[posEquipo].botas == null)
            {
                info.transform.GetChild(14).transform.GetChild(1).GetComponent<Text>().text = "";
            }
            else
            {
                info.transform.GetChild(14).transform.GetChild(1).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posEquipo].botas.nombreIngles;
            }

            if (baseDeDatos.equipoAliado[posEquipo].arma == null)
            {
                info.transform.GetChild(15).transform.GetChild(1).GetComponent<Text>().text = "";
            }
            else
            {
                info.transform.GetChild(15).transform.GetChild(1).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posEquipo].arma.nombreIngles;
            }

            if (baseDeDatos.equipoAliado[posEquipo].escudo == null)
            {
                info.transform.GetChild(16).transform.GetChild(1).GetComponent<Text>().text = "";
            }
            else
            {
                info.transform.GetChild(16).transform.GetChild(1).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posEquipo].escudo.nombreIngles;
            }

            if (baseDeDatos.equipoAliado[posEquipo].complemento == null)
            {
                info.transform.GetChild(17).transform.GetChild(1).GetComponent<Text>().text = "";
            }
            else
            {
                info.transform.GetChild(17).transform.GetChild(1).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posEquipo].complemento.nombreIngles;
            }

            ayuda.transform.GetChild(2).GetComponent<Text>().text = "Back";
            ayuda.transform.GetChild(0).GetComponent<Text>().text = "Select";
            ayuda.transform.GetChild(4).GetComponent<Text>().text = "Move";
        }
        else
        {
            info.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = baseDeDatos.equipoAliado[posEquipo].nombre;

            info.transform.GetChild(2).transform.GetChild(1).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posEquipo].elemento;

            info.transform.GetChild(3).transform.GetChild(0).GetComponent<Text>().text = "PS Máx.";
            info.transform.GetChild(4).transform.GetChild(0).GetComponent<Text>().text = "Atq. Fís.";
            info.transform.GetChild(6).transform.GetChild(0).GetComponent<Text>().text = "Def. Fís.";
            info.transform.GetChild(5).transform.GetChild(0).GetComponent<Text>().text = "Atq. Mág.";
            info.transform.GetChild(7).transform.GetChild(0).GetComponent<Text>().text = "Def. Mág.";
            info.transform.GetChild(8).transform.GetChild(0).GetComponent<Text>().text = "Velocidad";
            
            info.transform.GetChild(12).transform.GetChild(0).GetComponent<Text>().text = "Casco";
            info.transform.GetChild(13).transform.GetChild(0).GetComponent<Text>().text = "Armadura";
            info.transform.GetChild(14).transform.GetChild(0).GetComponent<Text>().text = "Botas";
            info.transform.GetChild(15).transform.GetChild(0).GetComponent<Text>().text = "Arma";
            info.transform.GetChild(16).transform.GetChild(0).GetComponent<Text>().text = "Escudo";
            info.transform.GetChild(17).transform.GetChild(0).GetComponent<Text>().text = "Complemento";

            info.transform.GetChild(20).transform.GetChild(0).GetComponent<Text>().text = baseDeDatos.equipoAliado[posEquipo].habilidades[0].nombre + "  ER: " + baseDeDatos.equipoAliado[posEquipo].habilidades[0].energiaActual + "/" + baseDeDatos.equipoAliado[posEquipo].habilidades[0].energia;

            if (numeroAtaques > 1)
            {
                info.transform.GetChild(21).transform.GetChild(0).gameObject.SetActive(true);
                info.transform.GetChild(21).transform.GetChild(0).GetComponent<Text>().text = baseDeDatos.equipoAliado[posEquipo].habilidades[1].nombre + "  ER: " + baseDeDatos.equipoAliado[posEquipo].habilidades[1].energiaActual + "/" + baseDeDatos.equipoAliado[posEquipo].habilidades[1].energia;
            }
            else
            {
                info.transform.GetChild(21).transform.GetChild(0).gameObject.SetActive(false);
                info.transform.GetChild(22).transform.GetChild(0).gameObject.SetActive(false);
                info.transform.GetChild(23).transform.GetChild(0).gameObject.SetActive(false);
            }

            if (numeroAtaques > 2)
            {
                info.transform.GetChild(22).transform.GetChild(0).gameObject.SetActive(true);
                info.transform.GetChild(22).transform.GetChild(0).GetComponent<Text>().text = baseDeDatos.equipoAliado[posEquipo].habilidades[2].nombre + "  ER: " + baseDeDatos.equipoAliado[posEquipo].habilidades[2].energiaActual + "/" + baseDeDatos.equipoAliado[posEquipo].habilidades[2].energia;
            }
            else
            {
                info.transform.GetChild(22).transform.GetChild(0).gameObject.SetActive(false);
                info.transform.GetChild(23).transform.GetChild(0).gameObject.SetActive(false);
            }

            if (numeroAtaques == 4)
            {
                info.transform.GetChild(23).transform.GetChild(0).gameObject.SetActive(true);
                info.transform.GetChild(23).transform.GetChild(0).GetComponent<Text>().text = baseDeDatos.equipoAliado[posEquipo].habilidades[3].nombre + "  ER: " + baseDeDatos.equipoAliado[posEquipo].habilidades[3].energiaActual + "/" + baseDeDatos.equipoAliado[posEquipo].habilidades[3].energia;
            }
            else
            {
                info.transform.GetChild(23).transform.GetChild(0).gameObject.SetActive(false);
            }

            if (baseDeDatos.equipoAliado[posEquipo].casco == null)
            {
                info.transform.GetChild(12).transform.GetChild(1).GetComponent<Text>().text = "";
            }
            else
            {
                info.transform.GetChild(12).transform.GetChild(1).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posEquipo].casco.nombre;
            }

            if (baseDeDatos.equipoAliado[posEquipo].armadura == null)
            {
                info.transform.GetChild(13).transform.GetChild(1).GetComponent<Text>().text = "";
            }
            else
            {
                info.transform.GetChild(13).transform.GetChild(1).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posEquipo].armadura.nombre;
            }

            if (baseDeDatos.equipoAliado[posEquipo].botas == null)
            {
                info.transform.GetChild(14).transform.GetChild(1).GetComponent<Text>().text = "";
            }
            else
            {
                info.transform.GetChild(14).transform.GetChild(1).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posEquipo].botas.nombre;
            }

            if (baseDeDatos.equipoAliado[posEquipo].arma == null)
            {
                info.transform.GetChild(15).transform.GetChild(1).GetComponent<Text>().text = "";
            }
            else
            {
                info.transform.GetChild(15).transform.GetChild(1).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posEquipo].arma.nombre;
            }

            if (baseDeDatos.equipoAliado[posEquipo].escudo == null)
            {
                info.transform.GetChild(16).transform.GetChild(1).GetComponent<Text>().text = "";
            }
            else
            {
                info.transform.GetChild(16).transform.GetChild(1).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posEquipo].escudo.nombre;
            }

            if (baseDeDatos.equipoAliado[posEquipo].complemento == null)
            {
                info.transform.GetChild(17).transform.GetChild(1).GetComponent<Text>().text = "";
            }
            else
            {
                info.transform.GetChild(17).transform.GetChild(1).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posEquipo].complemento.nombre;
            }

            ayuda.transform.GetChild(2).GetComponent<Text>().text = "Volver";
            ayuda.transform.GetChild(0).GetComponent<Text>().text = "Select";
            ayuda.transform.GetChild(4).GetComponent<Text>().text = "Move";
        }

        info.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posEquipo].nivel;
        
        info.transform.GetChild(3).transform.GetChild(1).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posEquipo].vidaModificada;
        info.transform.GetChild(4).transform.GetChild(1).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posEquipo].ataqueFisicoModificado;
        info.transform.GetChild(5).transform.GetChild(1).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posEquipo].ataqueMagicoModificado;
        info.transform.GetChild(6).transform.GetChild(1).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posEquipo].defensaFisicaModificada;
        info.transform.GetChild(7).transform.GetChild(1).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posEquipo].defensaMagicaModificada;
        info.transform.GetChild(8).transform.GetChild(1).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posEquipo].velocidadModificada;
    }



    void DesactivaMenu()
    {
        activo = false;
        menu.gameObject.SetActive(activo);
    }



    void MueveFlecha()
    {
        if (pos == 0)
        {
            info.transform.GetChild(19).transform.position = info.transform.GetChild(12).transform.GetChild(2).position;
        }
        else if (pos == 1)
        {
            info.transform.GetChild(19).transform.position = info.transform.GetChild(13).transform.GetChild(2).position;
        }
        else if (pos == 2)
        {
            info.transform.GetChild(19).transform.position = info.transform.GetChild(14).transform.GetChild(2).position;
        }
        else if (pos == 3)
        {
            info.transform.GetChild(19).transform.position = info.transform.GetChild(15).transform.GetChild(2).position;
        }
        else if (pos == 4)
        {
            info.transform.GetChild(19).transform.position = info.transform.GetChild(16).transform.GetChild(2).position;
        }
        else
        {
            info.transform.GetChild(19).transform.position = info.transform.GetChild(17).transform.GetChild(2).position;
        }
    }



    void EquiparObjeto()
    {
        if(pos == 0)
        {
            if (baseDeDatos.equipoAliado[posicionEnEquipo].llevaCasco)
            {
                int ind = indiceObjetoViejo();
                baseDeDatos.QuitarObjeto(posicionEnEquipo, ind, pos);
            }
        }
        else if (pos == 1)
        {
            if (baseDeDatos.equipoAliado[posicionEnEquipo].llevaArmadura)
            {
                int ind = indiceObjetoViejo();
                baseDeDatos.QuitarObjeto(posicionEnEquipo, ind, pos);
            }
        }
        else if (pos == 2)
        {
            if (baseDeDatos.equipoAliado[posicionEnEquipo].llevaBotas)
            {
                int ind = indiceObjetoViejo();
                baseDeDatos.QuitarObjeto(posicionEnEquipo, ind, pos);
            }
        }
        else if (pos == 3)
        {
            if (baseDeDatos.equipoAliado[posicionEnEquipo].llevaArma)
            {
                int ind = indiceObjetoViejo();
                baseDeDatos.QuitarObjeto(posicionEnEquipo, ind, pos);
            }
        }
        else if (pos == 4)
        {
            if (baseDeDatos.equipoAliado[posicionEnEquipo].llevaEscudo)
            {
                int ind = indiceObjetoViejo();
                baseDeDatos.QuitarObjeto(posicionEnEquipo, ind, pos);
            }
        }
        else if (pos == 5)
        {
            if (baseDeDatos.equipoAliado[posicionEnEquipo].llevaComplemento)
            {
                int ind = indiceObjetoViejo();
                baseDeDatos.QuitarObjeto(posicionEnEquipo, ind, pos);
            }
        }

        baseDeDatos.EquiparObjeto(posicionEnEquipo, posObjeto, pos);
    }



    int indiceObjetoViejo()
    {
        int aux = 0;

        if (pos == 0)
        {
            for (int i = 0; i < baseDeDatos.numeroCascos; i++)
            {
                if(baseDeDatos.cascos[i].indice == baseDeDatos.equipoAliado[posicionEnEquipo].casco.indice)
                {
                    aux = i;
                }
            }
        }
        else if (pos == 1)
        {
            for (int i = 0; i < baseDeDatos.numeroArmaduras; i++)
            {
                if (baseDeDatos.armaduras[i].indice == baseDeDatos.equipoAliado[posicionEnEquipo].armadura.indice)
                {
                    aux = i;
                }
            }
        }
        else if (pos == 2)
        {
            for (int i = 0; i < baseDeDatos.numeroBotas; i++)
            {
                if (baseDeDatos.botas[i].indice == baseDeDatos.equipoAliado[posicionEnEquipo].botas.indice)
                {
                    aux = i;
                }
            }
        }
        else if (pos == 3)
        {
            for (int i = 0; i < baseDeDatos.numeroArmas; i++)
            {
                if (baseDeDatos.armas[i].indice == baseDeDatos.equipoAliado[posicionEnEquipo].arma.indice)
                {
                    aux = i;
                }
            }
        }
        else if (pos == 4)
        {
            for (int i = 0; i < baseDeDatos.numeroEscudos; i++)
            {
                if (baseDeDatos.escudos[i].indice == baseDeDatos.equipoAliado[posicionEnEquipo].escudo.indice)
                {
                    aux = i;
                }
            }
        }
        else if (pos == 5)
        {
            for (int i = 0; i < baseDeDatos.numeroComplemento; i++)
            {
                if (baseDeDatos.complemento[i].indice == baseDeDatos.equipoAliado[posicionEnEquipo].complemento.indice)
                {
                    aux = i;
                }
            }
        }


        return aux;
    }



    void QuitarObjeto()
    {
        baseDeDatos.QuitarObjeto(posicionEnEquipo, posObjeto, pos);
    }



    void IniciaMenuObjetos()
    {
        realizaAccion = true;
        menuObjetos.SetActive(realizaAccion);
        posObjeto = 0;

        if(pos == 0)
        {
            numeroObjetos = baseDeDatos.numeroCascos;

            for(int i = 0; i < 10; i++)
            {
                if(i < numeroObjetos)
                {
                    if(baseDeDatos.idioma == 1)
                    {
                        menuObjetos.transform.GetChild(i + 1).GetComponent<Text>().text = baseDeDatos.cascos[i].nombreIngles;
                    }
                    else
                    {
                        menuObjetos.transform.GetChild(i + 1).GetComponent<Text>().text = baseDeDatos.cascos[i].nombre;
                    }

                    menuObjetos.transform.GetChild(i + 11).GetComponent<Text>().text = "" + baseDeDatos.cantidadCascos[i];
                }
                else
                {
                    menuObjetos.transform.GetChild(i + 1).GetComponent<Text>().text = "----------";
                    menuObjetos.transform.GetChild(i + 11).GetComponent<Text>().text = "";
                }
            }
        }
        else if (pos == 1)
        {
            numeroObjetos = baseDeDatos.numeroArmaduras;

            for (int i = 0; i < 10; i++)
            {
                if (i < numeroObjetos)
                {
                    if(baseDeDatos.idioma == 1)
                    {
                        menuObjetos.transform.GetChild(i + 1).GetComponent<Text>().text = baseDeDatos.armaduras[i].nombreIngles;
                    }
                    else
                    {
                        menuObjetos.transform.GetChild(i + 1).GetComponent<Text>().text = baseDeDatos.armaduras[i].nombre;
                    }
                    
                    menuObjetos.transform.GetChild(i + 11).GetComponent<Text>().text = "" + baseDeDatos.cantidadArmaduras[i];
                }
                else
                {
                    menuObjetos.transform.GetChild(i + 1).GetComponent<Text>().text = "----------";
                    menuObjetos.transform.GetChild(i + 11).GetComponent<Text>().text = "";
                }
            }
        }
        else if (pos == 2)
        {
            numeroObjetos = baseDeDatos.numeroBotas;

            for (int i = 0; i < 10; i++)
            {
                if (i < numeroObjetos)
                {
                    if (baseDeDatos.idioma == 1)
                    {
                        menuObjetos.transform.GetChild(i + 1).GetComponent<Text>().text = baseDeDatos.botas[i].nombreIngles;
                    }
                    else
                    {
                        menuObjetos.transform.GetChild(i + 1).GetComponent<Text>().text = baseDeDatos.botas[i].nombre;
                    }
                    
                    menuObjetos.transform.GetChild(i + 11).GetComponent<Text>().text = "" + baseDeDatos.cantidadBotas[i];
                }
                else
                {
                    menuObjetos.transform.GetChild(i + 1).GetComponent<Text>().text = "----------";
                    menuObjetos.transform.GetChild(i + 11).GetComponent<Text>().text = "";
                }
            }
        }
        else if (pos == 3)
        {
            numeroObjetos = baseDeDatos.numeroArmas;

            for (int i = 0; i < 10; i++)
            {
                if (i < numeroObjetos)
                {
                    if (baseDeDatos.idioma == 1)
                    {
                        menuObjetos.transform.GetChild(i + 1).GetComponent<Text>().text = baseDeDatos.armas[i].nombreIngles;
                    }
                    else
                    {
                        menuObjetos.transform.GetChild(i + 1).GetComponent<Text>().text = baseDeDatos.armas[i].nombre;
                    }
                    
                    menuObjetos.transform.GetChild(i + 11).GetComponent<Text>().text = "" + baseDeDatos.cantidadArmas[i];
                }
                else
                {
                    menuObjetos.transform.GetChild(i + 1).GetComponent<Text>().text = "----------";
                    menuObjetos.transform.GetChild(i + 11).GetComponent<Text>().text = "";
                }
            }
        }
        else if (pos == 4)
        {
            numeroObjetos = baseDeDatos.numeroEscudos;

            for (int i = 0; i < 10; i++)
            {
                if (i < numeroObjetos)
                {
                    if (baseDeDatos.idioma == 1)
                    {
                        menuObjetos.transform.GetChild((i + 1)).GetComponent<Text>().text = baseDeDatos.escudos[i].nombreIngles;
                    }
                    else
                    {
                        menuObjetos.transform.GetChild((i + 1)).GetComponent<Text>().text = baseDeDatos.escudos[i].nombre;
                    }
                    
                    menuObjetos.transform.GetChild((i + 11)).GetComponent<Text>().text = "" + baseDeDatos.cantidadEscudos[i];
                }
                else
                {
                    menuObjetos.transform.GetChild((i + 1)).GetComponent<Text>().text = "----------";
                    menuObjetos.transform.GetChild((i + 11)).GetComponent<Text>().text = "";
                }
            }
        }
        else if (pos == 5)
        {
            numeroObjetos = baseDeDatos.numeroComplemento;

            for (int i = 0; i < 10; i++)
            {
                if (i < numeroObjetos)
                {
                    if (baseDeDatos.idioma == 1)
                    {
                        menuObjetos.transform.GetChild(i + 1).GetComponent<Text>().text = baseDeDatos.complemento[i].nombreIngles;
                    }
                    else
                    {
                        menuObjetos.transform.GetChild(i + 1).GetComponent<Text>().text = baseDeDatos.complemento[i].nombre;
                    }
                    
                    menuObjetos.transform.GetChild(i + 11).GetComponent<Text>().text = "" + baseDeDatos.cantidadComplemento[i];
                }
                else
                {
                    menuObjetos.transform.GetChild(i + 1).GetComponent<Text>().text = "----------";
                    menuObjetos.transform.GetChild(i + 11).GetComponent<Text>().text = "";
                }
            }
        }

        MueveFlechaObjeto();
    }



    void DesactivarMenuObjetos()
    {
        realizaAccion = false;
        menuObjetos.SetActive(realizaAccion);
        info.transform.GetChild(3).transform.GetChild(2).gameObject.SetActive(false);
        info.transform.GetChild(4).transform.GetChild(2).gameObject.SetActive(false);
        info.transform.GetChild(5).transform.GetChild(2).gameObject.SetActive(false);
        info.transform.GetChild(6).transform.GetChild(2).gameObject.SetActive(false);
        info.transform.GetChild(7).transform.GetChild(2).gameObject.SetActive(false);
        info.transform.GetChild(8).transform.GetChild(2).gameObject.SetActive(false);
        info.transform.GetChild(9).transform.GetChild(2).gameObject.SetActive(false);
    }



    void MueveFlechaObjeto()
    {
        menuObjetos.transform.GetChild(0).transform.position = menuObjetos.transform.GetChild(21 + posObjeto).transform.position;

        CalculaBonificacion();
    }



    void ActualizarListaObjetos()
    {
        if(pos == 0)
        {
            if (!baseDeDatos.equipoAliado[posicionEnEquipo].llevaCasco)
            {
                info.transform.GetChild(12).transform.GetChild(1).GetComponent<Text>().text = "";
            }
            else
            {
                if(baseDeDatos.idioma == 1)
                {
                    info.transform.GetChild(12).transform.GetChild(1).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posicionEnEquipo].casco.nombreIngles;
                }
                else
                {
                    info.transform.GetChild(12).transform.GetChild(1).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posicionEnEquipo].casco.nombre;
                }
            }
        }
        else if (pos == 1)
        {
            if (!baseDeDatos.equipoAliado[posicionEnEquipo].llevaArmadura)
            {
                info.transform.GetChild(13).transform.GetChild(1).GetComponent<Text>().text = "";
            }
            else
            {
                if (baseDeDatos.idioma == 1)
                {
                    info.transform.GetChild(13).transform.GetChild(1).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posicionEnEquipo].armadura.nombreIngles;
                }
                else
                {
                    info.transform.GetChild(13).transform.GetChild(1).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posicionEnEquipo].armadura.nombre;
                }
            }
        }
        else if (pos == 2)
        {
            if (!baseDeDatos.equipoAliado[posicionEnEquipo].llevaBotas)
            {
                info.transform.GetChild(14).transform.GetChild(1).GetComponent<Text>().text = "";
            }
            else
            {
                if (baseDeDatos.idioma == 1)
                {
                    info.transform.GetChild(14).transform.GetChild(1).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posicionEnEquipo].botas.nombreIngles;
                }
                else
                {
                    info.transform.GetChild(14).transform.GetChild(1).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posicionEnEquipo].botas.nombre;
                }
            }
        }
        else if (pos == 3)
        {
            if (!baseDeDatos.equipoAliado[posicionEnEquipo].llevaArma)
            {
                info.transform.GetChild(15).transform.GetChild(1).GetComponent<Text>().text = "";
            }
            else
            {
                if (baseDeDatos.idioma == 1)
                {
                    info.transform.GetChild(15).transform.GetChild(1).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posicionEnEquipo].arma.nombreIngles;
                }
                else
                {
                    info.transform.GetChild(15).transform.GetChild(1).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posicionEnEquipo].arma.nombre;
                }
            }
        }
        else if (pos == 4)
        {
            if (!baseDeDatos.equipoAliado[posicionEnEquipo].llevaEscudo)
            {
                info.transform.GetChild(16).transform.GetChild(1).GetComponent<Text>().text = "";
            }
            else
            {
                if (baseDeDatos.idioma == 1)
                {
                    info.transform.GetChild(16).transform.GetChild(1).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posicionEnEquipo].escudo.nombreIngles;
                }
                else
                {
                    info.transform.GetChild(16).transform.GetChild(1).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posicionEnEquipo].escudo.nombre;
                }
            }
        }
        else if (pos == 5)
        {
            if (!baseDeDatos.equipoAliado[posicionEnEquipo].llevaComplemento)
            {
                info.transform.GetChild(17).transform.GetChild(1).GetComponent<Text>().text = "";
            }
            else
            {
                if (baseDeDatos.idioma == 1)
                {
                    info.transform.GetChild(17).transform.GetChild(1).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posicionEnEquipo].complemento.nombreIngles;
                }
                else
                {
                    info.transform.GetChild(17).transform.GetChild(1).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posicionEnEquipo].complemento.nombre;
                }
            }
        }

        ActualizarEstadisticas();
    }



    void ActualizarEstadisticas()
    {
        info.transform.GetChild(3).transform.GetChild(1).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posicionEnEquipo].vidaModificada;
        info.transform.GetChild(4).transform.GetChild(1).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posicionEnEquipo].ataqueFisicoModificado;
        info.transform.GetChild(5).transform.GetChild(1).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posicionEnEquipo].ataqueMagicoModificado;
        info.transform.GetChild(6).transform.GetChild(1).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posicionEnEquipo].defensaFisicaModificada;
        info.transform.GetChild(7).transform.GetChild(1).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posicionEnEquipo].defensaMagicaModificada;
        info.transform.GetChild(8).transform.GetChild(1).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posicionEnEquipo].velocidadModificada;
    }



    void CalculaBonificacion()
    {
        int aux;
        bool llevaObjeto;

        if (pos == 0)
        {
            if(baseDeDatos.numeroCascos != 0)
            {
                llevaObjeto = baseDeDatos.equipoAliado[posicionEnEquipo].llevaArmadura;

                if (baseDeDatos.cascos[posObjeto].aumentaVid)
                {
                    info.transform.GetChild(3).transform.GetChild(2).gameObject.SetActive(true);

                    if (llevaObjeto)
                    {
                        if (baseDeDatos.cascos[posObjeto].indice == baseDeDatos.equipoAliado[posicionEnEquipo].casco.indice)
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].vidaModificada - baseDeDatos.cascos[posObjeto].aumentoVida;
                        }
                        else
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].vidaModificada + baseDeDatos.cascos[posObjeto].aumentoVida - baseDeDatos.equipoAliado[posicionEnEquipo].casco.aumentoVida;
                        }
                    }
                    else
                    {
                        aux = baseDeDatos.equipoAliado[posicionEnEquipo].vidaModificada + baseDeDatos.cascos[posObjeto].aumentoVida;
                    }

                    info.transform.GetChild(3).transform.GetChild(2).GetComponent<Text>().text = "--> " + aux;

                    if (aux > baseDeDatos.equipoAliado[posicionEnEquipo].vidaModificada)
                    {
                        info.transform.GetChild(3).transform.GetChild(2).GetComponent<Text>().color = new Color(27.0f / 255.0f, 72.0f / 255.0f, 137.0f / 255.0f);
                    }
                    else if (aux < baseDeDatos.equipoAliado[posicionEnEquipo].vidaModificada)
                    {
                        info.transform.GetChild(3).transform.GetChild(2).GetComponent<Text>().color = new Color(137.0f / 255.0f, 27.0f / 255.0f, 40.0f / 255.0f);
                    }
                    else
                    {
                        info.transform.GetChild(3).transform.GetChild(2).GetComponent<Text>().color = new Color(50.0f / 255.0f, 50.0f / 255.0f, 50.0f / 255.0f);
                    }
                }
                else
                {
                    info.transform.GetChild(3).transform.GetChild(2).gameObject.SetActive(false);
                }

                if (baseDeDatos.cascos[posObjeto].aumentaAtaqueF)
                {
                    info.transform.GetChild(4).transform.GetChild(2).gameObject.SetActive(true);

                    if (llevaObjeto)
                    {
                        if (baseDeDatos.cascos[posObjeto].indice == baseDeDatos.equipoAliado[posicionEnEquipo].casco.indice)
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].ataqueFisicoModificado - baseDeDatos.cascos[posObjeto].aumentoAtaqueFisico;
                        }
                        else
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].ataqueFisicoModificado + baseDeDatos.cascos[posObjeto].aumentoAtaqueFisico - baseDeDatos.equipoAliado[posicionEnEquipo].casco.aumentoAtaqueFisico;
                        }
                    }
                    else
                    {
                        aux = baseDeDatos.equipoAliado[posicionEnEquipo].ataqueFisicoModificado + baseDeDatos.cascos[posObjeto].aumentoAtaqueFisico;
                    }

                    info.transform.GetChild(4).transform.GetChild(2).GetComponent<Text>().text = "--> " + aux;

                    if (aux > baseDeDatos.equipoAliado[posicionEnEquipo].ataqueFisicoModificado)
                    {
                        info.transform.GetChild(4).transform.GetChild(2).GetComponent<Text>().color = new Color(27.0f / 255.0f, 72.0f / 255.0f, 137.0f / 255.0f);
                    }
                    else if (aux < baseDeDatos.equipoAliado[posicionEnEquipo].ataqueFisicoModificado)
                    {
                        info.transform.GetChild(4).transform.GetChild(2).GetComponent<Text>().color = new Color(137.0f / 255.0f, 27.0f / 255.0f, 40.0f / 255.0f);
                    }
                    else
                    {
                        info.transform.GetChild(4).transform.GetChild(2).GetComponent<Text>().color = new Color(50.0f / 255.0f, 50.0f / 255.0f, 50.0f / 255.0f);
                    }
                }
                else
                {
                    info.transform.GetChild(4).transform.GetChild(2).gameObject.SetActive(false);
                }

                if (baseDeDatos.cascos[posObjeto].aumentaDefensaF)
                {
                    info.transform.GetChild(5).transform.GetChild(2).gameObject.SetActive(true);

                    if (llevaObjeto)
                    {
                        if (baseDeDatos.cascos[posObjeto].indice == baseDeDatos.equipoAliado[posicionEnEquipo].casco.indice)
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].defensaFisicaModificada - baseDeDatos.cascos[posObjeto].aumentoDefensaFisica;
                        }
                        else
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].defensaFisicaModificada + baseDeDatos.cascos[posObjeto].aumentoDefensaFisica - baseDeDatos.equipoAliado[posicionEnEquipo].casco.aumentoDefensaFisica;
                        }
                    }
                    else
                    {
                        aux = baseDeDatos.equipoAliado[posicionEnEquipo].defensaFisicaModificada + baseDeDatos.cascos[posObjeto].aumentoDefensaFisica;
                    }

                    info.transform.GetChild(6).transform.GetChild(2).GetComponent<Text>().text = "--> " + aux;

                    if (aux > baseDeDatos.equipoAliado[posicionEnEquipo].defensaFisicaModificada)
                    {
                        info.transform.GetChild(6).transform.GetChild(2).GetComponent<Text>().color = new Color(27.0f / 255.0f, 72.0f / 255.0f, 137.0f / 255.0f);
                    }
                    else if (aux < baseDeDatos.equipoAliado[posicionEnEquipo].defensaFisicaModificada)
                    {
                        info.transform.GetChild(6).transform.GetChild(2).GetComponent<Text>().color = new Color(137.0f / 255.0f, 27.0f / 255.0f, 40.0f / 255.0f);
                    }
                    else
                    {
                        info.transform.GetChild(6).transform.GetChild(2).GetComponent<Text>().color = new Color(50.0f / 255.0f, 50.0f / 255.0f, 50.0f / 255.0f);
                    }
                }
                else
                {
                    info.transform.GetChild(6).transform.GetChild(2).gameObject.SetActive(false);
                }

                if (baseDeDatos.cascos[posObjeto].aumentaAtaqueM)
                {
                    info.transform.GetChild(5).transform.GetChild(2).gameObject.SetActive(true);

                    if (llevaObjeto)
                    {
                        if (baseDeDatos.cascos[posObjeto].indice == baseDeDatos.equipoAliado[posicionEnEquipo].casco.indice)
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].ataqueMagicoModificado - baseDeDatos.cascos[posObjeto].aumentoAtaqueMagico;
                        }
                        else
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].ataqueMagicoModificado + baseDeDatos.cascos[posObjeto].aumentoAtaqueMagico - baseDeDatos.equipoAliado[posicionEnEquipo].casco.aumentoAtaqueMagico;
                        }
                    }
                    else
                    {
                        aux = baseDeDatos.equipoAliado[posicionEnEquipo].ataqueMagicoModificado + baseDeDatos.cascos[posObjeto].aumentoAtaqueMagico;
                    }

                    info.transform.GetChild(5).transform.GetChild(2).GetComponent<Text>().text = "--> " + aux;

                    if (aux > baseDeDatos.equipoAliado[posicionEnEquipo].ataqueMagicoModificado)
                    {
                        info.transform.GetChild(5).transform.GetChild(2).GetComponent<Text>().color = new Color(27.0f / 255.0f, 72.0f / 255.0f, 137.0f / 255.0f);
                    }
                    else if (aux < baseDeDatos.equipoAliado[posicionEnEquipo].ataqueMagicoModificado)
                    {
                        info.transform.GetChild(5).transform.GetChild(2).GetComponent<Text>().color = new Color(137.0f / 255.0f, 27.0f / 255.0f, 40.0f / 255.0f);
                    }
                    else
                    {
                        info.transform.GetChild(5).transform.GetChild(2).GetComponent<Text>().color = new Color(50.0f / 255.0f, 50.0f / 255.0f, 50.0f / 255.0f);
                    }
                }
                else
                {
                    info.transform.GetChild(5).transform.GetChild(2).gameObject.SetActive(false);
                }

                if (baseDeDatos.cascos[posObjeto].aumentaDefensaM)
                {
                    info.transform.GetChild(7).transform.GetChild(2).gameObject.SetActive(true);

                    if (llevaObjeto)
                    {
                        if (baseDeDatos.cascos[posObjeto].indice == baseDeDatos.equipoAliado[posicionEnEquipo].casco.indice)
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].defensaMagicaModificada - baseDeDatos.cascos[posObjeto].aumentoDefensaMagica;
                        }
                        else
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].defensaMagicaModificada + baseDeDatos.cascos[posObjeto].aumentoDefensaMagica - baseDeDatos.equipoAliado[posicionEnEquipo].casco.aumentoDefensaMagica;
                        }
                    }
                    else
                    {
                        aux = baseDeDatos.equipoAliado[posicionEnEquipo].defensaMagicaModificada + baseDeDatos.cascos[posObjeto].aumentoDefensaMagica;
                    }

                    info.transform.GetChild(7).transform.GetChild(2).GetComponent<Text>().text = "--> " + aux;

                    if (aux > baseDeDatos.equipoAliado[posicionEnEquipo].defensaMagicaModificada)
                    {
                        info.transform.GetChild(7).transform.GetChild(2).GetComponent<Text>().color = new Color(27.0f / 255.0f, 72.0f / 255.0f, 137.0f / 255.0f);
                    }
                    else if (aux < baseDeDatos.equipoAliado[posicionEnEquipo].defensaMagicaModificada)
                    {
                        info.transform.GetChild(7).transform.GetChild(2).GetComponent<Text>().color = new Color(137.0f / 255.0f, 27.0f / 255.0f, 40.0f / 255.0f);
                    }
                    else
                    {
                        info.transform.GetChild(7).transform.GetChild(2).GetComponent<Text>().color = new Color(50.0f / 255.0f, 50.0f / 255.0f, 50.0f / 255.0f);
                    }
                }
                else
                {
                    info.transform.GetChild(7).transform.GetChild(2).gameObject.SetActive(false);
                }

                if (baseDeDatos.cascos[posObjeto].aumentaVel)
                {
                    info.transform.GetChild(8).transform.GetChild(2).gameObject.SetActive(true);

                    if (llevaObjeto)
                    {
                        if (baseDeDatos.cascos[posObjeto].indice == baseDeDatos.equipoAliado[posicionEnEquipo].casco.indice)
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].velocidadModificada - baseDeDatos.cascos[posObjeto].aumentoVelocidad;
                        }
                        else
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].velocidadModificada + baseDeDatos.cascos[posObjeto].aumentoVelocidad - baseDeDatos.equipoAliado[posicionEnEquipo].casco.aumentoVelocidad;
                        }
                    }
                    else
                    {
                        aux = baseDeDatos.equipoAliado[posicionEnEquipo].velocidadModificada + baseDeDatos.cascos[posObjeto].aumentoVelocidad;
                    }

                    info.transform.GetChild(8).transform.GetChild(2).GetComponent<Text>().text = "--> " + aux;

                    if (aux > baseDeDatos.equipoAliado[posicionEnEquipo].velocidadModificada)
                    {
                        info.transform.GetChild(8).transform.GetChild(2).GetComponent<Text>().color = new Color(27.0f / 255.0f, 72.0f / 255.0f, 137.0f / 255.0f);
                    }
                    else if (aux < baseDeDatos.equipoAliado[posicionEnEquipo].velocidadModificada)
                    {
                        info.transform.GetChild(8).transform.GetChild(2).GetComponent<Text>().color = new Color(137.0f / 255.0f, 27.0f / 255.0f, 40.0f / 255.0f);
                    }
                    else
                    {
                        info.transform.GetChild(8).transform.GetChild(2).GetComponent<Text>().color = new Color(50.0f / 255.0f, 50.0f / 255.0f, 50.0f / 255.0f);
                    }
                }
                else
                {
                    info.transform.GetChild(8).transform.GetChild(2).gameObject.SetActive(false);
                }
            }
        }
        else if (pos == 1)
        {
            if(baseDeDatos.numeroArmaduras != 0)
            {
                llevaObjeto = baseDeDatos.equipoAliado[posicionEnEquipo].llevaArmadura;

                if (baseDeDatos.armaduras[posObjeto].aumentaVid)
                {
                    info.transform.GetChild(3).transform.GetChild(2).gameObject.SetActive(true);

                    if (llevaObjeto)
                    {
                        if(baseDeDatos.armaduras[posObjeto].indice == baseDeDatos.equipoAliado[posicionEnEquipo].armadura.indice)
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].vidaModificada - baseDeDatos.armaduras[posObjeto].aumentoVida;
                        }
                        else
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].vidaModificada + baseDeDatos.armaduras[posObjeto].aumentoVida - baseDeDatos.equipoAliado[posicionEnEquipo].armadura.aumentoVida;
                        }
                    }
                    else
                    {
                        aux = baseDeDatos.equipoAliado[posicionEnEquipo].vidaModificada + baseDeDatos.armaduras[posObjeto].aumentoVida;
                    }

                    info.transform.GetChild(3).transform.GetChild(2).GetComponent<Text>().text = "--> " + aux;

                    if (aux > baseDeDatos.equipoAliado[posicionEnEquipo].vidaModificada)
                    {
                        info.transform.GetChild(3).transform.GetChild(2).GetComponent<Text>().color = new Color(27.0f / 255.0f, 72.0f / 255.0f, 137.0f / 255.0f);
                    }
                    else if (aux < baseDeDatos.equipoAliado[posicionEnEquipo].vidaModificada)
                    {
                        info.transform.GetChild(3).transform.GetChild(2).GetComponent<Text>().color = new Color(137.0f / 255.0f, 27.0f / 255.0f, 40.0f / 255.0f);
                    }
                    else
                    {
                        info.transform.GetChild(3).transform.GetChild(2).GetComponent<Text>().color = new Color(50.0f / 255.0f, 50.0f / 255.0f, 50.0f / 255.0f);
                    }
                }
                else
                {
                    info.transform.GetChild(3).transform.GetChild(2).gameObject.SetActive(false);
                }

                if (baseDeDatos.armaduras[posObjeto].aumentaAtaqueF)
                {
                    info.transform.GetChild(4).transform.GetChild(2).gameObject.SetActive(true);

                    if (llevaObjeto)
                    {
                        if (baseDeDatos.armaduras[posObjeto].indice == baseDeDatos.equipoAliado[posicionEnEquipo].armadura.indice)
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].ataqueFisicoModificado - baseDeDatos.armaduras[posObjeto].aumentoAtaqueFisico;
                        }
                        else
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].ataqueFisicoModificado + baseDeDatos.armaduras[posObjeto].aumentoAtaqueFisico - baseDeDatos.equipoAliado[posicionEnEquipo].armadura.aumentoAtaqueFisico;
                        }
                    }
                    else
                    {
                        aux = baseDeDatos.equipoAliado[posicionEnEquipo].ataqueFisicoModificado + baseDeDatos.armaduras[posObjeto].aumentoAtaqueFisico;
                    }

                    info.transform.GetChild(4).transform.GetChild(2).GetComponent<Text>().text = "--> " + aux;

                    if (aux > baseDeDatos.equipoAliado[posicionEnEquipo].ataqueFisicoModificado)
                    {
                        info.transform.GetChild(4).transform.GetChild(2).GetComponent<Text>().color = new Color(27.0f / 255.0f, 72.0f / 255.0f, 137.0f / 255.0f);
                    }
                    else if (aux < baseDeDatos.equipoAliado[posicionEnEquipo].ataqueFisicoModificado)
                    {
                        info.transform.GetChild(4).transform.GetChild(2).GetComponent<Text>().color = new Color(137.0f / 255.0f, 27.0f / 255.0f, 40.0f / 255.0f);
                    }
                    else
                    {
                        info.transform.GetChild(4).transform.GetChild(2).GetComponent<Text>().color = new Color(50.0f / 255.0f, 50.0f / 255.0f, 50.0f / 255.0f);
                    }
                }
                else
                {
                    info.transform.GetChild(4).transform.GetChild(2).gameObject.SetActive(false);
                }

                if (baseDeDatos.armaduras[posObjeto].aumentaDefensaF)
                {
                    info.transform.GetChild(6).transform.GetChild(2).gameObject.SetActive(true);

                    if (llevaObjeto)
                    {
                        if (baseDeDatos.armaduras[posObjeto].indice == baseDeDatos.equipoAliado[posicionEnEquipo].armadura.indice)
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].defensaFisicaModificada - baseDeDatos.armaduras[posObjeto].aumentoDefensaFisica;
                        }
                        else
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].defensaFisicaModificada + baseDeDatos.armaduras[posObjeto].aumentoDefensaFisica - baseDeDatos.equipoAliado[posicionEnEquipo].armadura.aumentoDefensaFisica;
                        }
                    }
                    else
                    {
                        aux = baseDeDatos.equipoAliado[posicionEnEquipo].defensaFisicaModificada + baseDeDatos.armaduras[posObjeto].aumentoDefensaFisica;
                    }
                    
                    info.transform.GetChild(6).transform.GetChild(2).GetComponent<Text>().text = "--> " + aux;

                    if (aux > baseDeDatos.equipoAliado[posicionEnEquipo].defensaFisicaModificada)
                    {
                        info.transform.GetChild(6).transform.GetChild(2).GetComponent<Text>().color = new Color(27.0f / 255.0f, 72.0f / 255.0f, 137.0f / 255.0f);
                    }
                    else if (aux < baseDeDatos.equipoAliado[posicionEnEquipo].defensaFisicaModificada)
                    {
                        info.transform.GetChild(6).transform.GetChild(2).GetComponent<Text>().color = new Color(137.0f / 255.0f, 27.0f / 255.0f, 40.0f / 255.0f);
                    }
                    else
                    {
                        info.transform.GetChild(6).transform.GetChild(2).GetComponent<Text>().color = new Color(50.0f / 255.0f, 50.0f / 255.0f, 50.0f / 255.0f);
                    }
                }
                else
                {
                    info.transform.GetChild(6).transform.GetChild(2).gameObject.SetActive(false);
                }

                if (baseDeDatos.armaduras[posObjeto].aumentaAtaqueM)
                {
                    info.transform.GetChild(5).transform.GetChild(2).gameObject.SetActive(true);

                    if (llevaObjeto)
                    {
                        if (baseDeDatos.armaduras[posObjeto].indice == baseDeDatos.equipoAliado[posicionEnEquipo].armadura.indice)
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].ataqueMagicoModificado - baseDeDatos.armaduras[posObjeto].aumentoAtaqueMagico;
                        }
                        else
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].ataqueMagicoModificado + baseDeDatos.armaduras[posObjeto].aumentoAtaqueMagico - baseDeDatos.equipoAliado[posicionEnEquipo].armadura.aumentoAtaqueMagico;
                        }
                    }
                    else
                    {
                        aux = baseDeDatos.equipoAliado[posicionEnEquipo].ataqueMagicoModificado + baseDeDatos.armaduras[posObjeto].aumentoAtaqueMagico;
                    }

                    info.transform.GetChild(5).transform.GetChild(2).GetComponent<Text>().text = "--> " + aux;

                    if (aux > baseDeDatos.equipoAliado[posicionEnEquipo].ataqueMagicoModificado)
                    {
                        info.transform.GetChild(5).transform.GetChild(2).GetComponent<Text>().color = new Color(27.0f / 255.0f, 72.0f / 255.0f, 137.0f / 255.0f);
                    }
                    else if (aux < baseDeDatos.equipoAliado[posicionEnEquipo].ataqueMagicoModificado)
                    {
                        info.transform.GetChild(5).transform.GetChild(2).GetComponent<Text>().color = new Color(137.0f / 255.0f, 27.0f / 255.0f, 40.0f / 255.0f);
                    }
                    else
                    {
                        info.transform.GetChild(5).transform.GetChild(2).GetComponent<Text>().color = new Color(50.0f / 255.0f, 50.0f / 255.0f, 50.0f / 255.0f);
                    }
                }
                else
                {
                    info.transform.GetChild(5).transform.GetChild(2).gameObject.SetActive(false);
                }

                if (baseDeDatos.armaduras[posObjeto].aumentaDefensaM)
                {
                    info.transform.GetChild(7).transform.GetChild(2).gameObject.SetActive(true);

                    if (llevaObjeto)
                    {
                        if (baseDeDatos.armaduras[posObjeto].indice == baseDeDatos.equipoAliado[posicionEnEquipo].armadura.indice)
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].defensaMagicaModificada - baseDeDatos.armaduras[posObjeto].aumentoDefensaMagica;
                        }
                        else
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].defensaMagicaModificada + baseDeDatos.armaduras[posObjeto].aumentoDefensaMagica - baseDeDatos.equipoAliado[posicionEnEquipo].armadura.aumentoDefensaMagica;
                        }
                    }
                    else
                    {
                        aux = baseDeDatos.equipoAliado[posicionEnEquipo].defensaMagicaModificada + baseDeDatos.armaduras[posObjeto].aumentoDefensaMagica;
                    }

                    info.transform.GetChild(7).transform.GetChild(2).GetComponent<Text>().text = "--> " + aux;

                    if (aux > baseDeDatos.equipoAliado[posicionEnEquipo].defensaMagicaModificada)
                    {
                        info.transform.GetChild(7).transform.GetChild(2).GetComponent<Text>().color = new Color(27.0f / 255.0f, 72.0f / 255.0f, 137.0f / 255.0f);
                    }
                    else if (aux < baseDeDatos.equipoAliado[posicionEnEquipo].defensaMagicaModificada)
                    {
                        info.transform.GetChild(7).transform.GetChild(2).GetComponent<Text>().color = new Color(137.0f / 255.0f, 27.0f / 255.0f, 40.0f / 255.0f);
                    }
                    else
                    {
                        info.transform.GetChild(7).transform.GetChild(2).GetComponent<Text>().color = new Color(50.0f / 255.0f, 50.0f / 255.0f, 50.0f / 255.0f);
                    }
                }
                else
                {
                    info.transform.GetChild(7).transform.GetChild(2).gameObject.SetActive(false);
                }

                if (baseDeDatos.armaduras[posObjeto].aumentaVel)
                {
                    info.transform.GetChild(8).transform.GetChild(2).gameObject.SetActive(true);

                    if (llevaObjeto)
                    {
                        if (baseDeDatos.armaduras[posObjeto].indice == baseDeDatos.equipoAliado[posicionEnEquipo].armadura.indice)
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].velocidadModificada - baseDeDatos.armaduras[posObjeto].aumentoVelocidad;
                        }
                        else
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].velocidadModificada + baseDeDatos.armaduras[posObjeto].aumentoVelocidad - baseDeDatos.equipoAliado[posicionEnEquipo].armadura.aumentoVelocidad;
                        }
                    }
                    else
                    {
                        aux = baseDeDatos.equipoAliado[posicionEnEquipo].velocidadModificada + baseDeDatos.armaduras[posObjeto].aumentoVelocidad;
                    }

                    info.transform.GetChild(8).transform.GetChild(2).GetComponent<Text>().text = "--> " + aux;

                    if (aux > baseDeDatos.equipoAliado[posicionEnEquipo].velocidadModificada)
                    {
                        info.transform.GetChild(8).transform.GetChild(2).GetComponent<Text>().color = new Color(27.0f / 255.0f, 72.0f / 255.0f, 137.0f / 255.0f);
                    }
                    else if (aux < baseDeDatos.equipoAliado[posicionEnEquipo].velocidadModificada)
                    {
                        info.transform.GetChild(8).transform.GetChild(2).GetComponent<Text>().color = new Color(137.0f / 255.0f, 27.0f / 255.0f, 40.0f / 255.0f);
                    }
                    else
                    {
                        info.transform.GetChild(8).transform.GetChild(2).GetComponent<Text>().color = new Color(50.0f / 255.0f, 50.0f / 255.0f, 50.0f / 255.0f);
                    }
                }
                else
                {
                    info.transform.GetChild(8).transform.GetChild(2).gameObject.SetActive(false);
                }
            }
        }
        else if (pos == 2)
        {
            if(baseDeDatos.numeroBotas != 0)
            {
                llevaObjeto = baseDeDatos.equipoAliado[posicionEnEquipo].llevaBotas;

                if (baseDeDatos.botas[posObjeto].aumentaVid)
                {
                    info.transform.GetChild(3).transform.GetChild(2).gameObject.SetActive(true);

                    if (llevaObjeto)
                    {
                        if (baseDeDatos.botas[posObjeto].indice == baseDeDatos.equipoAliado[posicionEnEquipo].botas.indice)
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].vidaModificada - baseDeDatos.botas[posObjeto].aumentoVida;
                        }
                        else
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].vidaModificada + baseDeDatos.botas[posObjeto].aumentoVida - baseDeDatos.equipoAliado[posicionEnEquipo].botas.aumentoVida;
                        }
                    }
                    else
                    {
                        aux = baseDeDatos.equipoAliado[posicionEnEquipo].vidaModificada + baseDeDatos.botas[posObjeto].aumentoVida;
                    }

                    info.transform.GetChild(3).transform.GetChild(2).GetComponent<Text>().text = "--> " + aux;

                    if (aux > baseDeDatos.equipoAliado[posicionEnEquipo].vidaModificada)
                    {
                        info.transform.GetChild(3).transform.GetChild(2).GetComponent<Text>().color = new Color(27.0f / 255.0f, 72.0f / 255.0f, 137.0f / 255.0f);
                    }
                    else if (aux < baseDeDatos.equipoAliado[posicionEnEquipo].vidaModificada)
                    {
                        info.transform.GetChild(3).transform.GetChild(2).GetComponent<Text>().color = new Color(137.0f / 255.0f, 27.0f / 255.0f, 40.0f / 255.0f);
                    }
                    else
                    {
                        info.transform.GetChild(3).transform.GetChild(2).GetComponent<Text>().color = new Color(50.0f / 255.0f, 50.0f / 255.0f, 50.0f / 255.0f);
                    }
                }
                else
                {
                    info.transform.GetChild(3).transform.GetChild(2).gameObject.SetActive(false);
                }

                if (baseDeDatos.botas[posObjeto].aumentaAtaqueF)
                {
                    info.transform.GetChild(4).transform.GetChild(2).gameObject.SetActive(true);

                    if (llevaObjeto)
                    {
                        if (baseDeDatos.botas[posObjeto].indice == baseDeDatos.equipoAliado[posicionEnEquipo].botas.indice)
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].ataqueFisicoModificado - baseDeDatos.botas[posObjeto].aumentoAtaqueFisico;
                        }
                        else
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].ataqueFisicoModificado + baseDeDatos.botas[posObjeto].aumentoAtaqueFisico - baseDeDatos.equipoAliado[posicionEnEquipo].botas.aumentoAtaqueFisico;
                        }
                    }
                    else
                    {
                        aux = baseDeDatos.equipoAliado[posicionEnEquipo].ataqueFisicoModificado + baseDeDatos.botas[posObjeto].aumentoAtaqueFisico;
                    }

                    info.transform.GetChild(4).transform.GetChild(2).GetComponent<Text>().text = "--> " + aux;

                    if (aux > baseDeDatos.equipoAliado[posicionEnEquipo].ataqueFisicoModificado)
                    {
                        info.transform.GetChild(4).transform.GetChild(2).GetComponent<Text>().color = new Color(27.0f / 255.0f, 72.0f / 255.0f, 137.0f / 255.0f);
                    }
                    else if (aux < baseDeDatos.equipoAliado[posicionEnEquipo].ataqueFisicoModificado)
                    {
                        info.transform.GetChild(4).transform.GetChild(2).GetComponent<Text>().color = new Color(137.0f / 255.0f, 27.0f / 255.0f, 40.0f / 255.0f);
                    }
                    else
                    {
                        info.transform.GetChild(4).transform.GetChild(2).GetComponent<Text>().color = new Color(50.0f / 255.0f, 50.0f / 255.0f, 50.0f / 255.0f);
                    }
                }
                else
                {
                    info.transform.GetChild(4).transform.GetChild(2).gameObject.SetActive(false);
                }

                if (baseDeDatos.botas[posObjeto].aumentaDefensaF)
                {
                    info.transform.GetChild(6).transform.GetChild(2).gameObject.SetActive(true);

                    if (llevaObjeto)
                    {
                        if (baseDeDatos.botas[posObjeto].indice == baseDeDatos.equipoAliado[posicionEnEquipo].botas.indice)
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].defensaFisicaModificada - baseDeDatos.botas[posObjeto].aumentoDefensaFisica;
                        }
                        else
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].defensaFisicaModificada + baseDeDatos.botas[posObjeto].aumentoDefensaFisica - baseDeDatos.equipoAliado[posicionEnEquipo].botas.aumentoDefensaFisica;
                        }
                    }
                    else
                    {
                        aux = baseDeDatos.equipoAliado[posicionEnEquipo].defensaFisicaModificada + baseDeDatos.botas[posObjeto].aumentoDefensaFisica;
                    }

                    info.transform.GetChild(6).transform.GetChild(2).GetComponent<Text>().text = "--> " + aux;

                    if (aux > baseDeDatos.equipoAliado[posicionEnEquipo].defensaFisicaModificada)
                    {
                        info.transform.GetChild(6).transform.GetChild(2).GetComponent<Text>().color = new Color(27.0f / 255.0f, 72.0f / 255.0f, 137.0f / 255.0f);
                    }
                    else if (aux < baseDeDatos.equipoAliado[posicionEnEquipo].defensaFisicaModificada)
                    {
                        info.transform.GetChild(6).transform.GetChild(2).GetComponent<Text>().color = new Color(137.0f / 255.0f, 27.0f / 255.0f, 40.0f / 255.0f);
                    }
                    else
                    {
                        info.transform.GetChild(6).transform.GetChild(2).GetComponent<Text>().color = new Color(50.0f / 255.0f, 50.0f / 255.0f, 50.0f / 255.0f);
                    }
                }
                else
                {
                    info.transform.GetChild(6).transform.GetChild(2).gameObject.SetActive(false);
                }

                if (baseDeDatos.botas[posObjeto].aumentaAtaqueM)
                {
                    info.transform.GetChild(5).transform.GetChild(2).gameObject.SetActive(true);

                    if (llevaObjeto)
                    {
                        if (baseDeDatos.botas[posObjeto].indice == baseDeDatos.equipoAliado[posicionEnEquipo].botas.indice)
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].ataqueMagicoModificado - baseDeDatos.botas[posObjeto].aumentoAtaqueMagico;
                        }
                        else
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].ataqueMagicoModificado + baseDeDatos.botas[posObjeto].aumentoAtaqueMagico - baseDeDatos.equipoAliado[posicionEnEquipo].botas.aumentoAtaqueMagico;
                        }
                    }
                    else
                    {
                        aux = baseDeDatos.equipoAliado[posicionEnEquipo].ataqueMagicoModificado + baseDeDatos.botas[posObjeto].aumentoAtaqueMagico;
                    }

                    info.transform.GetChild(5).transform.GetChild(2).GetComponent<Text>().text = "--> " + aux;

                    if (aux > baseDeDatos.equipoAliado[posicionEnEquipo].ataqueMagicoModificado)
                    {
                        info.transform.GetChild(5).transform.GetChild(2).GetComponent<Text>().color = new Color(27.0f / 255.0f, 72.0f / 255.0f, 137.0f / 255.0f);
                    }
                    else if (aux < baseDeDatos.equipoAliado[posicionEnEquipo].ataqueMagicoModificado)
                    {
                        info.transform.GetChild(5).transform.GetChild(2).GetComponent<Text>().color = new Color(137.0f / 255.0f, 27.0f / 255.0f, 40.0f / 255.0f);
                    }
                    else
                    {
                        info.transform.GetChild(5).transform.GetChild(2).GetComponent<Text>().color = new Color(50.0f / 255.0f, 50.0f / 255.0f, 50.0f / 255.0f);
                    }
                }
                else
                {
                    info.transform.GetChild(5).transform.GetChild(2).gameObject.SetActive(false);
                }

                if (baseDeDatos.botas[posObjeto].aumentaDefensaM)
                {
                    info.transform.GetChild(7).transform.GetChild(2).gameObject.SetActive(true);

                    if (llevaObjeto)
                    {
                        if (baseDeDatos.botas[posObjeto].indice == baseDeDatos.equipoAliado[posicionEnEquipo].botas.indice)
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].defensaMagicaModificada - baseDeDatos.botas[posObjeto].aumentoDefensaMagica;
                        }
                        else
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].defensaMagicaModificada + baseDeDatos.botas[posObjeto].aumentoDefensaMagica - baseDeDatos.equipoAliado[posicionEnEquipo].botas.aumentoDefensaMagica;
                        }
                    }
                    else
                    {
                        aux = baseDeDatos.equipoAliado[posicionEnEquipo].defensaMagicaModificada + baseDeDatos.botas[posObjeto].aumentoDefensaMagica;
                    }

                    info.transform.GetChild(7).transform.GetChild(2).GetComponent<Text>().text = "--> " + aux;

                    if (aux > baseDeDatos.equipoAliado[posicionEnEquipo].defensaMagicaModificada)
                    {
                        info.transform.GetChild(7).transform.GetChild(2).GetComponent<Text>().color = new Color(27.0f / 255.0f, 72.0f / 255.0f, 137.0f / 255.0f);
                    }
                    else if (aux < baseDeDatos.equipoAliado[posicionEnEquipo].defensaMagicaModificada)
                    {
                        info.transform.GetChild(7).transform.GetChild(2).GetComponent<Text>().color = new Color(137.0f / 255.0f, 27.0f / 255.0f, 40.0f / 255.0f);
                    }
                    else
                    {
                        info.transform.GetChild(7).transform.GetChild(2).GetComponent<Text>().color = new Color(50.0f / 255.0f, 50.0f / 255.0f, 50.0f / 255.0f);
                    }
                }
                else
                {
                    info.transform.GetChild(7).transform.GetChild(2).gameObject.SetActive(false);
                }

                if (baseDeDatos.botas[posObjeto].aumentaVel)
                {
                    info.transform.GetChild(8).transform.GetChild(2).gameObject.SetActive(true);

                    if (llevaObjeto)
                    {
                        if (baseDeDatos.botas[posObjeto].indice == baseDeDatos.equipoAliado[posicionEnEquipo].botas.indice)
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].velocidadModificada - baseDeDatos.botas[posObjeto].aumentoVelocidad;
                        }
                        else
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].velocidadModificada + baseDeDatos.botas[posObjeto].aumentoVelocidad - baseDeDatos.equipoAliado[posicionEnEquipo].botas.aumentoVelocidad;
                        }
                    }
                    else
                    {
                        aux = baseDeDatos.equipoAliado[posicionEnEquipo].velocidadModificada + baseDeDatos.botas[posObjeto].aumentoVelocidad;
                    }

                    info.transform.GetChild(8).transform.GetChild(2).GetComponent<Text>().text = "--> " + aux;

                    if (aux > baseDeDatos.equipoAliado[posicionEnEquipo].velocidadModificada)
                    {
                        info.transform.GetChild(8).transform.GetChild(2).GetComponent<Text>().color = new Color(27.0f / 255.0f, 72.0f / 255.0f, 137.0f / 255.0f);
                    }
                    else if (aux < baseDeDatos.equipoAliado[posicionEnEquipo].velocidadModificada)
                    {
                        info.transform.GetChild(8).transform.GetChild(2).GetComponent<Text>().color = new Color(137.0f / 255.0f, 27.0f / 255.0f, 40.0f / 255.0f);
                    }
                    else
                    {
                        info.transform.GetChild(8).transform.GetChild(2).GetComponent<Text>().color = new Color(50.0f / 255.0f, 50.0f / 255.0f, 50.0f / 255.0f);
                    }
                }
                else
                {
                    info.transform.GetChild(8).transform.GetChild(2).gameObject.SetActive(false);
                }
            }
        }
        else if (pos == 3)
        {
            if(baseDeDatos.numeroArmas != 0)
            {
                llevaObjeto = baseDeDatos.equipoAliado[posicionEnEquipo].llevaArma;

                if (baseDeDatos.armas[posObjeto].aumentaVid)
                {
                    info.transform.GetChild(3).transform.GetChild(2).gameObject.SetActive(true);

                    if (llevaObjeto)
                    {
                        if (baseDeDatos.armas[posObjeto].indice == baseDeDatos.equipoAliado[posicionEnEquipo].arma.indice)
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].vidaModificada - baseDeDatos.armas[posObjeto].aumentoVida;
                        }
                        else
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].vidaModificada + baseDeDatos.armas[posObjeto].aumentoVida - baseDeDatos.equipoAliado[posicionEnEquipo].arma.aumentoVida;
                        }
                    }
                    else
                    {
                        aux = baseDeDatos.equipoAliado[posicionEnEquipo].vidaModificada + baseDeDatos.armas[posObjeto].aumentoVida;
                    }

                    info.transform.GetChild(3).transform.GetChild(2).GetComponent<Text>().text = "--> " + aux;

                    if (aux > baseDeDatos.equipoAliado[posicionEnEquipo].vidaModificada)
                    {
                        info.transform.GetChild(3).transform.GetChild(2).GetComponent<Text>().color = new Color(27.0f / 255.0f, 72.0f / 255.0f, 137.0f / 255.0f);
                    }
                    else if (aux < baseDeDatos.equipoAliado[posicionEnEquipo].vidaModificada)
                    {
                        info.transform.GetChild(3).transform.GetChild(2).GetComponent<Text>().color = new Color(137.0f / 255.0f, 27.0f / 255.0f, 40.0f / 255.0f);
                    }
                    else
                    {
                        info.transform.GetChild(3).transform.GetChild(2).GetComponent<Text>().color = new Color(50.0f / 255.0f, 50.0f / 255.0f, 50.0f / 255.0f);
                    }
                }
                else
                {
                    info.transform.GetChild(3).transform.GetChild(2).gameObject.SetActive(false);
                }

                if (baseDeDatos.armas[posObjeto].aumentaAtaqueF)
                {
                    info.transform.GetChild(4).transform.GetChild(2).gameObject.SetActive(true);

                    if (llevaObjeto)
                    {
                        if (baseDeDatos.armas[posObjeto].indice == baseDeDatos.equipoAliado[posicionEnEquipo].arma.indice)
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].ataqueFisicoModificado - baseDeDatos.armas[posObjeto].aumentoAtaqueFisico;
                        }
                        else
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].ataqueFisicoModificado + baseDeDatos.armas[posObjeto].aumentoAtaqueFisico - baseDeDatos.equipoAliado[posicionEnEquipo].arma.aumentoAtaqueFisico;
                        }
                    }
                    else
                    {
                        aux = baseDeDatos.equipoAliado[posicionEnEquipo].ataqueFisicoModificado + baseDeDatos.armas[posObjeto].aumentoAtaqueFisico;
                    }

                    info.transform.GetChild(4).transform.GetChild(2).GetComponent<Text>().text = "--> " + aux;

                    if (aux > baseDeDatos.equipoAliado[posicionEnEquipo].ataqueFisicoModificado)
                    {
                        info.transform.GetChild(4).transform.GetChild(2).GetComponent<Text>().color = new Color(27.0f / 255.0f, 72.0f / 255.0f, 137.0f / 255.0f);
                    }
                    else if (aux < baseDeDatos.equipoAliado[posicionEnEquipo].ataqueFisicoModificado)
                    {
                        info.transform.GetChild(4).transform.GetChild(2).GetComponent<Text>().color = new Color(137.0f / 255.0f, 27.0f / 255.0f, 40.0f / 255.0f);
                    }
                    else
                    {
                        info.transform.GetChild(4).transform.GetChild(2).GetComponent<Text>().color = new Color(50.0f / 255.0f, 50.0f / 255.0f, 50.0f / 255.0f);
                    }
                }
                else
                {
                    info.transform.GetChild(4).transform.GetChild(2).gameObject.SetActive(false);
                }

                if (baseDeDatos.armas[posObjeto].aumentaDefensaF)
                {
                    info.transform.GetChild(6).transform.GetChild(2).gameObject.SetActive(true);

                    if (llevaObjeto)
                    {
                        if (baseDeDatos.armas[posObjeto].indice == baseDeDatos.equipoAliado[posicionEnEquipo].arma.indice)
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].defensaFisicaModificada - baseDeDatos.armas[posObjeto].aumentoDefensaFisica;
                        }
                        else
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].defensaFisicaModificada + baseDeDatos.armas[posObjeto].aumentoDefensaFisica - baseDeDatos.equipoAliado[posicionEnEquipo].arma.aumentoDefensaFisica;
                        }
                    }
                    else
                    {
                        aux = baseDeDatos.equipoAliado[posicionEnEquipo].defensaFisicaModificada + baseDeDatos.armas[posObjeto].aumentoDefensaFisica;
                    }

                    info.transform.GetChild(6).transform.GetChild(2).GetComponent<Text>().text = "--> " + aux;

                    if (aux > baseDeDatos.equipoAliado[posicionEnEquipo].defensaFisicaModificada)
                    {
                        info.transform.GetChild(6).transform.GetChild(2).GetComponent<Text>().color = new Color(27.0f / 255.0f, 72.0f / 255.0f, 137.0f / 255.0f);
                    }
                    else if (aux < baseDeDatos.equipoAliado[posicionEnEquipo].defensaFisicaModificada)
                    {
                        info.transform.GetChild(6).transform.GetChild(2).GetComponent<Text>().color = new Color(137.0f / 255.0f, 27.0f / 255.0f, 40.0f / 255.0f);
                    }
                    else
                    {
                        info.transform.GetChild(6).transform.GetChild(2).GetComponent<Text>().color = new Color(50.0f / 255.0f, 50.0f / 255.0f, 50.0f / 255.0f);
                    }
                }
                else
                {
                    info.transform.GetChild(6).transform.GetChild(2).gameObject.SetActive(false);
                }

                if (baseDeDatos.armas[posObjeto].aumentaAtaqueM)
                {
                    info.transform.GetChild(5).transform.GetChild(2).gameObject.SetActive(true);

                    if (llevaObjeto)
                    {
                        if (baseDeDatos.armas[posObjeto].indice == baseDeDatos.equipoAliado[posicionEnEquipo].arma.indice)
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].ataqueMagicoModificado - baseDeDatos.armas[posObjeto].aumentoAtaqueMagico;
                        }
                        else
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].ataqueMagicoModificado + baseDeDatos.armas[posObjeto].aumentoAtaqueMagico - baseDeDatos.equipoAliado[posicionEnEquipo].arma.aumentoAtaqueMagico;
                        }
                    }
                    else
                    {
                        aux = baseDeDatos.equipoAliado[posicionEnEquipo].ataqueMagicoModificado + baseDeDatos.armas[posObjeto].aumentoAtaqueMagico;
                    }

                    info.transform.GetChild(5).transform.GetChild(2).GetComponent<Text>().text = "--> " + aux;

                    if (aux > baseDeDatos.equipoAliado[posicionEnEquipo].ataqueMagicoModificado)
                    {
                        info.transform.GetChild(5).transform.GetChild(2).GetComponent<Text>().color = new Color(27.0f / 255.0f, 72.0f / 255.0f, 137.0f / 255.0f);
                    }
                    else if (aux < baseDeDatos.equipoAliado[posicionEnEquipo].ataqueMagicoModificado)
                    {
                        info.transform.GetChild(5).transform.GetChild(2).GetComponent<Text>().color = new Color(137.0f / 255.0f, 27.0f / 255.0f, 40.0f / 255.0f);
                    }
                    else
                    {
                        info.transform.GetChild(5).transform.GetChild(2).GetComponent<Text>().color = new Color(50.0f / 255.0f, 50.0f / 255.0f, 50.0f / 255.0f);
                    }
                }
                else
                {
                    info.transform.GetChild(5).transform.GetChild(2).gameObject.SetActive(false);
                }

                if (baseDeDatos.armas[posObjeto].aumentaDefensaM)
                {
                    info.transform.GetChild(7).transform.GetChild(2).gameObject.SetActive(true);

                    if (llevaObjeto)
                    {
                        if (baseDeDatos.armas[posObjeto].indice == baseDeDatos.equipoAliado[posicionEnEquipo].arma.indice)
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].defensaMagicaModificada - baseDeDatos.armas[posObjeto].aumentoDefensaMagica;
                        }
                        else
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].defensaMagicaModificada + baseDeDatos.armas[posObjeto].aumentoDefensaMagica - baseDeDatos.equipoAliado[posicionEnEquipo].arma.aumentoDefensaMagica;
                        }
                    }
                    else
                    {
                        aux = baseDeDatos.equipoAliado[posicionEnEquipo].defensaMagicaModificada + baseDeDatos.armas[posObjeto].aumentoDefensaMagica;
                    }

                    info.transform.GetChild(7).transform.GetChild(2).GetComponent<Text>().text = "--> " + aux;

                    if (aux > baseDeDatos.equipoAliado[posicionEnEquipo].defensaMagicaModificada)
                    {
                        info.transform.GetChild(7).transform.GetChild(2).GetComponent<Text>().color = new Color(27.0f / 255.0f, 72.0f / 255.0f, 137.0f / 255.0f);
                    }
                    else if (aux < baseDeDatos.equipoAliado[posicionEnEquipo].defensaMagicaModificada)
                    {
                        info.transform.GetChild(7).transform.GetChild(2).GetComponent<Text>().color = new Color(137.0f / 255.0f, 27.0f / 255.0f, 40.0f / 255.0f);
                    }
                    else
                    {
                        info.transform.GetChild(7).transform.GetChild(2).GetComponent<Text>().color = new Color(50.0f / 255.0f, 50.0f / 255.0f, 50.0f / 255.0f);
                    }
                }
                else
                {
                    info.transform.GetChild(7).transform.GetChild(2).gameObject.SetActive(false);
                }

                if (baseDeDatos.armas[posObjeto].aumentaVel)
                {
                    info.transform.GetChild(8).transform.GetChild(2).gameObject.SetActive(true);

                    if (llevaObjeto)
                    {
                        if (baseDeDatos.armas[posObjeto].indice == baseDeDatos.equipoAliado[posicionEnEquipo].arma.indice)
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].velocidadModificada - baseDeDatos.armas[posObjeto].aumentoVelocidad;
                        }
                        else
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].velocidadModificada + baseDeDatos.armas[posObjeto].aumentoVelocidad - baseDeDatos.equipoAliado[posicionEnEquipo].arma.aumentoVelocidad;
                        }
                    }
                    else
                    {
                        aux = baseDeDatos.equipoAliado[posicionEnEquipo].velocidadModificada + baseDeDatos.armas[posObjeto].aumentoVelocidad;
                    }

                    info.transform.GetChild(8).transform.GetChild(2).GetComponent<Text>().text = "--> " + aux;

                    if (aux > baseDeDatos.equipoAliado[posicionEnEquipo].velocidadModificada)
                    {
                        info.transform.GetChild(8).transform.GetChild(2).GetComponent<Text>().color = new Color(27.0f / 255.0f, 72.0f / 255.0f, 137.0f / 255.0f);
                    }
                    else if (aux < baseDeDatos.equipoAliado[posicionEnEquipo].velocidadModificada)
                    {
                        info.transform.GetChild(8).transform.GetChild(2).GetComponent<Text>().color = new Color(137.0f / 255.0f, 27.0f / 255.0f, 40.0f / 255.0f);
                    }
                    else
                    {
                        info.transform.GetChild(8).transform.GetChild(2).GetComponent<Text>().color = new Color(50.0f / 255.0f, 50.0f / 255.0f, 50.0f / 255.0f);
                    }
                }
                else
                {
                    info.transform.GetChild(8).transform.GetChild(2).gameObject.SetActive(false);
                }
            }
        }
        else if (pos == 4)
        {
            if(baseDeDatos.numeroEscudos != 0)
            {
                llevaObjeto = baseDeDatos.equipoAliado[posicionEnEquipo].llevaEscudo;

                if (baseDeDatos.escudos[posObjeto].aumentaVid)
                {
                    info.transform.GetChild(3).transform.GetChild(2).gameObject.SetActive(true);

                    if (llevaObjeto)
                    {
                        if (baseDeDatos.escudos[posObjeto].indice == baseDeDatos.equipoAliado[posicionEnEquipo].escudo.indice)
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].vidaModificada - baseDeDatos.escudos[posObjeto].aumentoVida;
                        }
                        else
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].vidaModificada + baseDeDatos.escudos[posObjeto].aumentoVida - baseDeDatos.equipoAliado[posicionEnEquipo].escudo.aumentoVida;
                        }
                    }
                    else
                    {
                        aux = baseDeDatos.equipoAliado[posicionEnEquipo].vidaModificada + baseDeDatos.escudos[posObjeto].aumentoVida;
                    }

                    info.transform.GetChild(3).transform.GetChild(2).GetComponent<Text>().text = "--> " + aux;

                    if (aux > baseDeDatos.equipoAliado[posicionEnEquipo].vidaModificada)
                    {
                        info.transform.GetChild(3).transform.GetChild(2).GetComponent<Text>().color = new Color(27.0f / 255.0f, 72.0f / 255.0f, 137.0f / 255.0f);
                    }
                    else if (aux < baseDeDatos.equipoAliado[posicionEnEquipo].vidaModificada)
                    {
                        info.transform.GetChild(3).transform.GetChild(2).GetComponent<Text>().color = new Color(137.0f / 255.0f, 27.0f / 255.0f, 40.0f / 255.0f);
                    }
                    else
                    {
                        info.transform.GetChild(3).transform.GetChild(2).GetComponent<Text>().color = new Color(50.0f / 255.0f, 50.0f / 255.0f, 50.0f / 255.0f);
                    }
                }
                else
                {
                    info.transform.GetChild(3).transform.GetChild(2).gameObject.SetActive(false);
                }

                if (baseDeDatos.escudos[posObjeto].aumentaAtaqueF)
                {
                    info.transform.GetChild(4).transform.GetChild(2).gameObject.SetActive(true);

                    if (llevaObjeto)
                    {
                        if (baseDeDatos.escudos[posObjeto].indice == baseDeDatos.equipoAliado[posicionEnEquipo].escudo.indice)
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].ataqueFisicoModificado - baseDeDatos.escudos[posObjeto].aumentoAtaqueFisico;
                        }
                        else
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].ataqueFisicoModificado + baseDeDatos.escudos[posObjeto].aumentoAtaqueFisico - baseDeDatos.equipoAliado[posicionEnEquipo].escudo.aumentoAtaqueFisico;
                        }
                    }
                    else
                    {
                        aux = baseDeDatos.equipoAliado[posicionEnEquipo].ataqueFisicoModificado + baseDeDatos.escudos[posObjeto].aumentoAtaqueFisico;
                    }

                    info.transform.GetChild(4).transform.GetChild(2).GetComponent<Text>().text = "--> " + aux;

                    if (aux > baseDeDatos.equipoAliado[posicionEnEquipo].ataqueFisicoModificado)
                    {
                        info.transform.GetChild(4).transform.GetChild(2).GetComponent<Text>().color = new Color(27.0f / 255.0f, 72.0f / 255.0f, 137.0f / 255.0f);
                    }
                    else if (aux < baseDeDatos.equipoAliado[posicionEnEquipo].ataqueFisicoModificado)
                    {
                        info.transform.GetChild(4).transform.GetChild(2).GetComponent<Text>().color = new Color(137.0f / 255.0f, 27.0f / 255.0f, 40.0f / 255.0f);
                    }
                    else
                    {
                        info.transform.GetChild(4).transform.GetChild(2).GetComponent<Text>().color = new Color(50.0f / 255.0f, 50.0f / 255.0f, 50.0f / 255.0f);
                    }
                }
                else
                {
                    info.transform.GetChild(4).transform.GetChild(2).gameObject.SetActive(false);
                }

                if (baseDeDatos.escudos[posObjeto].aumentaDefensaF)
                {
                    info.transform.GetChild(6).transform.GetChild(2).gameObject.SetActive(true);

                    if (llevaObjeto)
                    {
                        if (baseDeDatos.escudos[posObjeto].indice == baseDeDatos.equipoAliado[posicionEnEquipo].escudo.indice)
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].defensaFisicaModificada - baseDeDatos.escudos[posObjeto].aumentoDefensaFisica;
                        }
                        else
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].defensaFisicaModificada + baseDeDatos.escudos[posObjeto].aumentoDefensaFisica - baseDeDatos.equipoAliado[posicionEnEquipo].escudo.aumentoDefensaFisica;
                        }
                    }
                    else
                    {
                        aux = baseDeDatos.equipoAliado[posicionEnEquipo].defensaFisicaModificada + baseDeDatos.escudos[posObjeto].aumentoDefensaFisica;
                    }

                    info.transform.GetChild(6).transform.GetChild(2).GetComponent<Text>().text = "--> " + aux;

                    if (aux > baseDeDatos.equipoAliado[posicionEnEquipo].defensaFisicaModificada)
                    {
                        info.transform.GetChild(6).transform.GetChild(2).GetComponent<Text>().color = new Color(27.0f / 255.0f, 72.0f / 255.0f, 137.0f / 255.0f);
                    }
                    else if (aux < baseDeDatos.equipoAliado[posicionEnEquipo].defensaFisicaModificada)
                    {
                        info.transform.GetChild(6).transform.GetChild(2).GetComponent<Text>().color = new Color(137.0f / 255.0f, 27.0f / 255.0f, 40.0f / 255.0f);
                    }
                    else
                    {
                        info.transform.GetChild(6).transform.GetChild(2).GetComponent<Text>().color = new Color(50.0f / 255.0f, 50.0f / 255.0f, 50.0f / 255.0f);
                    }
                }
                else
                {
                    info.transform.GetChild(6).transform.GetChild(2).gameObject.SetActive(false);
                }

                if (baseDeDatos.escudos[posObjeto].aumentaAtaqueM)
                {
                    info.transform.GetChild(5).transform.GetChild(2).gameObject.SetActive(true);

                    if (llevaObjeto)
                    {
                        if (baseDeDatos.escudos[posObjeto].indice == baseDeDatos.equipoAliado[posicionEnEquipo].escudo.indice)
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].ataqueMagicoModificado - baseDeDatos.escudos[posObjeto].aumentoAtaqueMagico;
                        }
                        else
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].ataqueMagicoModificado + baseDeDatos.escudos[posObjeto].aumentoAtaqueMagico - baseDeDatos.equipoAliado[posicionEnEquipo].escudo.aumentoAtaqueMagico;
                        }
                    }
                    else
                    {
                        aux = baseDeDatos.equipoAliado[posicionEnEquipo].ataqueMagicoModificado + baseDeDatos.escudos[posObjeto].aumentoAtaqueMagico;
                    }

                    info.transform.GetChild(5).transform.GetChild(2).GetComponent<Text>().text = "--> " + aux;

                    if (aux > baseDeDatos.equipoAliado[posicionEnEquipo].ataqueMagicoModificado)
                    {
                        info.transform.GetChild(5).transform.GetChild(2).GetComponent<Text>().color = new Color(27.0f / 255.0f, 72.0f / 255.0f, 137.0f / 255.0f);
                    }
                    else if (aux < baseDeDatos.equipoAliado[posicionEnEquipo].ataqueMagicoModificado)
                    {
                        info.transform.GetChild(5).transform.GetChild(2).GetComponent<Text>().color = new Color(137.0f / 255.0f, 27.0f / 255.0f, 40.0f / 255.0f);
                    }
                    else
                    {
                        info.transform.GetChild(5).transform.GetChild(2).GetComponent<Text>().color = new Color(50.0f / 255.0f, 50.0f / 255.0f, 50.0f / 255.0f);
                    }
                }
                else
                {
                    info.transform.GetChild(5).transform.GetChild(2).gameObject.SetActive(false);
                }

                if (baseDeDatos.escudos[posObjeto].aumentaDefensaM)
                {
                    info.transform.GetChild(7).transform.GetChild(2).gameObject.SetActive(true);

                    if (llevaObjeto)
                    {
                        if (baseDeDatos.escudos[posObjeto].indice == baseDeDatos.equipoAliado[posicionEnEquipo].escudo.indice)
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].defensaMagicaModificada - baseDeDatos.escudos[posObjeto].aumentoDefensaMagica;
                        }
                        else
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].defensaMagicaModificada + baseDeDatos.escudos[posObjeto].aumentoDefensaMagica - baseDeDatos.equipoAliado[posicionEnEquipo].escudo.aumentoDefensaMagica;
                        }
                    }
                    else
                    {
                        aux = baseDeDatos.equipoAliado[posicionEnEquipo].defensaMagicaModificada + baseDeDatos.escudos[posObjeto].aumentoDefensaMagica;
                    }

                    info.transform.GetChild(7).transform.GetChild(2).GetComponent<Text>().text = "--> " + aux;

                    if (aux > baseDeDatos.equipoAliado[posicionEnEquipo].defensaMagicaModificada)
                    {
                        info.transform.GetChild(7).transform.GetChild(2).GetComponent<Text>().color = new Color(27.0f / 255.0f, 72.0f / 255.0f, 137.0f / 255.0f);
                    }
                    else if (aux < baseDeDatos.equipoAliado[posicionEnEquipo].defensaMagicaModificada)
                    {
                        info.transform.GetChild(7).transform.GetChild(2).GetComponent<Text>().color = new Color(137.0f / 255.0f, 27.0f / 255.0f, 40.0f / 255.0f);
                    }
                    else
                    {
                        info.transform.GetChild(7).transform.GetChild(2).GetComponent<Text>().color = new Color(50.0f / 255.0f, 50.0f / 255.0f, 50.0f / 255.0f);
                    }
                }
                else
                {
                    info.transform.GetChild(7).transform.GetChild(2).gameObject.SetActive(false);
                }

                if (baseDeDatos.escudos[posObjeto].aumentaVel)
                {
                    info.transform.GetChild(8).transform.GetChild(2).gameObject.SetActive(true);

                    if (llevaObjeto)
                    {
                        if (baseDeDatos.escudos[posObjeto].indice == baseDeDatos.equipoAliado[posicionEnEquipo].escudo.indice)
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].velocidadModificada - baseDeDatos.escudos[posObjeto].aumentoVelocidad;
                        }
                        else
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].velocidadModificada + baseDeDatos.escudos[posObjeto].aumentoVelocidad - baseDeDatos.equipoAliado[posicionEnEquipo].escudo.aumentoVelocidad;
                        }
                    }
                    else
                    {
                        aux = baseDeDatos.equipoAliado[posicionEnEquipo].velocidadModificada + baseDeDatos.escudos[posObjeto].aumentoVelocidad;
                    }

                    info.transform.GetChild(8).transform.GetChild(2).GetComponent<Text>().text = "--> " + aux;

                    if (aux > baseDeDatos.equipoAliado[posicionEnEquipo].velocidadModificada)
                    {
                        info.transform.GetChild(8).transform.GetChild(2).GetComponent<Text>().color = new Color(27.0f / 255.0f, 72.0f / 255.0f, 137.0f / 255.0f);
                    }
                    else if (aux < baseDeDatos.equipoAliado[posicionEnEquipo].velocidadModificada)
                    {
                        info.transform.GetChild(8).transform.GetChild(2).GetComponent<Text>().color = new Color(137.0f / 255.0f, 27.0f / 255.0f, 40.0f / 255.0f);
                    }
                    else
                    {
                        info.transform.GetChild(8).transform.GetChild(2).GetComponent<Text>().color = new Color(50.0f / 255.0f, 50.0f / 255.0f, 50.0f / 255.0f);
                    }
                }
                else
                {
                    info.transform.GetChild(8).transform.GetChild(2).gameObject.SetActive(false);
                }
            }
        }
        else if (pos == 5)
        {
            if(baseDeDatos.numeroComplemento != 0)
            {
                llevaObjeto = baseDeDatos.equipoAliado[posicionEnEquipo].llevaComplemento;

                if (baseDeDatos.complemento[posObjeto].aumentaVid)
                {
                    info.transform.GetChild(3).transform.GetChild(2).gameObject.SetActive(true);

                    if (llevaObjeto)
                    {
                        if (baseDeDatos.complemento[posObjeto].indice == baseDeDatos.equipoAliado[posicionEnEquipo].complemento.indice)
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].vidaModificada - baseDeDatos.complemento[posObjeto].aumentoVida;
                        }
                        else
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].vidaModificada + baseDeDatos.complemento[posObjeto].aumentoVida - baseDeDatos.equipoAliado[posicionEnEquipo].complemento.aumentoVida;
                        }
                    }
                    else
                    {
                        aux = baseDeDatos.equipoAliado[posicionEnEquipo].vidaModificada + baseDeDatos.complemento[posObjeto].aumentoVida;
                    }

                    info.transform.GetChild(3).transform.GetChild(2).GetComponent<Text>().text = "--> " + aux;

                    if (aux > baseDeDatos.equipoAliado[posicionEnEquipo].vidaModificada)
                    {
                        info.transform.GetChild(3).transform.GetChild(2).GetComponent<Text>().color = new Color(27.0f / 255.0f, 72.0f / 255.0f, 137.0f / 255.0f);
                    }
                    else if (aux < baseDeDatos.equipoAliado[posicionEnEquipo].vidaModificada)
                    {
                        info.transform.GetChild(3).transform.GetChild(2).GetComponent<Text>().color = new Color(137.0f / 255.0f, 27.0f / 255.0f, 40.0f / 255.0f);
                    }
                    else
                    {
                        info.transform.GetChild(3).transform.GetChild(2).GetComponent<Text>().color = new Color(50.0f / 255.0f, 50.0f / 255.0f, 50.0f / 255.0f);
                    }
                }
                else
                {
                    info.transform.GetChild(3).transform.GetChild(2).gameObject.SetActive(false);
                }

                if (baseDeDatos.complemento[posObjeto].aumentaAtaqueF)
                {
                    info.transform.GetChild(4).transform.GetChild(2).gameObject.SetActive(true);

                    if (llevaObjeto)
                    {
                        if (baseDeDatos.complemento[posObjeto].indice == baseDeDatos.equipoAliado[posicionEnEquipo].complemento.indice)
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].ataqueFisicoModificado - baseDeDatos.complemento[posObjeto].aumentoAtaqueFisico;
                        }
                        else
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].ataqueFisicoModificado + baseDeDatos.complemento[posObjeto].aumentoAtaqueFisico - baseDeDatos.equipoAliado[posicionEnEquipo].complemento.aumentoAtaqueFisico;
                        }
                    }
                    else
                    {
                        aux = baseDeDatos.equipoAliado[posicionEnEquipo].ataqueFisicoModificado + baseDeDatos.complemento[posObjeto].aumentoAtaqueFisico;
                    }

                    info.transform.GetChild(4).transform.GetChild(2).GetComponent<Text>().text = "--> " + aux;

                    if (aux > baseDeDatos.equipoAliado[posicionEnEquipo].ataqueFisicoModificado)
                    {
                        info.transform.GetChild(4).transform.GetChild(2).GetComponent<Text>().color = new Color(27.0f / 255.0f, 72.0f / 255.0f, 137.0f / 255.0f);
                    }
                    else if (aux < baseDeDatos.equipoAliado[posicionEnEquipo].ataqueFisicoModificado)
                    {
                        info.transform.GetChild(4).transform.GetChild(2).GetComponent<Text>().color = new Color(137.0f / 255.0f, 27.0f / 255.0f, 40.0f / 255.0f);
                    }
                    else
                    {
                        info.transform.GetChild(4).transform.GetChild(2).GetComponent<Text>().color = new Color(50.0f / 255.0f, 50.0f / 255.0f, 50.0f / 255.0f);
                    }
                }
                else
                {
                    info.transform.GetChild(4).transform.GetChild(2).gameObject.SetActive(false);
                }

                if (baseDeDatos.complemento[posObjeto].aumentaDefensaF)
                {
                    info.transform.GetChild(6).transform.GetChild(2).gameObject.SetActive(true);

                    if (llevaObjeto)
                    {
                        if (baseDeDatos.complemento[posObjeto].indice == baseDeDatos.equipoAliado[posicionEnEquipo].complemento.indice)
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].defensaFisicaModificada - baseDeDatos.complemento[posObjeto].aumentoDefensaFisica;
                        }
                        else
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].defensaFisicaModificada + baseDeDatos.complemento[posObjeto].aumentoDefensaFisica - baseDeDatos.equipoAliado[posicionEnEquipo].complemento.aumentoDefensaFisica;
                        }
                    }
                    else
                    {
                        aux = baseDeDatos.equipoAliado[posicionEnEquipo].defensaFisicaModificada + baseDeDatos.complemento[posObjeto].aumentoDefensaFisica;
                    }

                    info.transform.GetChild(6).transform.GetChild(2).GetComponent<Text>().text = "--> " + aux;

                    if (aux > baseDeDatos.equipoAliado[posicionEnEquipo].defensaFisicaModificada)
                    {
                        info.transform.GetChild(6).transform.GetChild(2).GetComponent<Text>().color = new Color(27.0f / 255.0f, 72.0f / 255.0f, 137.0f / 255.0f);
                    }
                    else if (aux < baseDeDatos.equipoAliado[posicionEnEquipo].defensaFisicaModificada)
                    {
                        info.transform.GetChild(6).transform.GetChild(2).GetComponent<Text>().color = new Color(137.0f / 255.0f, 27.0f / 255.0f, 40.0f / 255.0f);
                    }
                    else
                    {
                        info.transform.GetChild(6).transform.GetChild(2).GetComponent<Text>().color = new Color(50.0f / 255.0f, 50.0f / 255.0f, 50.0f / 255.0f);
                    }
                }
                else
                {
                    info.transform.GetChild(6).transform.GetChild(2).gameObject.SetActive(false);
                }

                if (baseDeDatos.complemento[posObjeto].aumentaAtaqueM)
                {
                    info.transform.GetChild(5).transform.GetChild(2).gameObject.SetActive(true);

                    if (llevaObjeto)
                    {
                        if (baseDeDatos.complemento[posObjeto].indice == baseDeDatos.equipoAliado[posicionEnEquipo].complemento.indice)
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].ataqueMagicoModificado - baseDeDatos.complemento[posObjeto].aumentoAtaqueMagico;
                        }
                        else
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].ataqueMagicoModificado + baseDeDatos.complemento[posObjeto].aumentoAtaqueMagico - baseDeDatos.equipoAliado[posicionEnEquipo].complemento.aumentoAtaqueMagico;
                        }
                    }
                    else
                    {
                        aux = baseDeDatos.equipoAliado[posicionEnEquipo].ataqueMagicoModificado + baseDeDatos.complemento[posObjeto].aumentoAtaqueMagico;
                    }

                    info.transform.GetChild(5).transform.GetChild(2).GetComponent<Text>().text = "--> " + aux;

                    if (aux > baseDeDatos.equipoAliado[posicionEnEquipo].ataqueMagicoModificado)
                    {
                        info.transform.GetChild(5).transform.GetChild(2).GetComponent<Text>().color = new Color(27.0f / 255.0f, 72.0f / 255.0f, 137.0f / 255.0f);
                    }
                    else if (aux < baseDeDatos.equipoAliado[posicionEnEquipo].ataqueMagicoModificado)
                    {
                        info.transform.GetChild(5).transform.GetChild(2).GetComponent<Text>().color = new Color(137.0f / 255.0f, 27.0f / 255.0f, 40.0f / 255.0f);
                    }
                    else
                    {
                        info.transform.GetChild(5).transform.GetChild(2).GetComponent<Text>().color = new Color(50.0f / 255.0f, 50.0f / 255.0f, 50.0f / 255.0f);
                    }
                }
                else
                {
                    info.transform.GetChild(5).transform.GetChild(2).gameObject.SetActive(false);
                }

                if (baseDeDatos.complemento[posObjeto].aumentaDefensaM)
                {
                    info.transform.GetChild(7).transform.GetChild(2).gameObject.SetActive(true);

                    if (llevaObjeto)
                    {
                        if (baseDeDatos.complemento[posObjeto].indice == baseDeDatos.equipoAliado[posicionEnEquipo].complemento.indice)
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].defensaMagicaModificada - baseDeDatos.complemento[posObjeto].aumentoDefensaMagica;
                        }
                        else
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].defensaMagicaModificada + baseDeDatos.complemento[posObjeto].aumentoDefensaMagica - baseDeDatos.equipoAliado[posicionEnEquipo].complemento.aumentoDefensaMagica;
                        }
                    }
                    else
                    {
                        aux = baseDeDatos.equipoAliado[posicionEnEquipo].defensaMagicaModificada + baseDeDatos.complemento[posObjeto].aumentoDefensaMagica;
                    }

                    info.transform.GetChild(7).transform.GetChild(2).GetComponent<Text>().text = "--> " + aux;

                    if (aux > baseDeDatos.equipoAliado[posicionEnEquipo].defensaMagicaModificada)
                    {
                        info.transform.GetChild(7).transform.GetChild(2).GetComponent<Text>().color = new Color(27.0f / 255.0f, 72.0f / 255.0f, 137.0f / 255.0f);
                    }
                    else if (aux < baseDeDatos.equipoAliado[posicionEnEquipo].defensaMagicaModificada)
                    {
                        info.transform.GetChild(7).transform.GetChild(2).GetComponent<Text>().color = new Color(137.0f / 255.0f, 27.0f / 255.0f, 40.0f / 255.0f);
                    }
                    else
                    {
                        info.transform.GetChild(7).transform.GetChild(2).GetComponent<Text>().color = new Color(50.0f / 255.0f, 50.0f / 255.0f, 50.0f / 255.0f);
                    }
                }
                else
                {
                    info.transform.GetChild(7).transform.GetChild(2).gameObject.SetActive(false);
                }

                if (baseDeDatos.complemento[posObjeto].aumentaVel)
                {
                    info.transform.GetChild(8).transform.GetChild(2).gameObject.SetActive(true);

                    if (llevaObjeto)
                    {
                        if (baseDeDatos.complemento[posObjeto].indice == baseDeDatos.equipoAliado[posicionEnEquipo].complemento.indice)
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].velocidadModificada - baseDeDatos.complemento[posObjeto].aumentoVelocidad;
                        }
                        else
                        {
                            aux = baseDeDatos.equipoAliado[posicionEnEquipo].velocidadModificada + baseDeDatos.complemento[posObjeto].aumentoVelocidad - baseDeDatos.equipoAliado[posicionEnEquipo].complemento.aumentoVelocidad;
                        }
                    }
                    else
                    {
                        aux = baseDeDatos.equipoAliado[posicionEnEquipo].velocidadModificada + baseDeDatos.complemento[posObjeto].aumentoVelocidad;
                    }

                    info.transform.GetChild(8).transform.GetChild(2).GetComponent<Text>().text = "--> " + aux;

                    if (aux > baseDeDatos.equipoAliado[posicionEnEquipo].velocidadModificada)
                    {
                        info.transform.GetChild(8).transform.GetChild(2).GetComponent<Text>().color = new Color(27.0f / 255.0f, 72.0f / 255.0f, 137.0f / 255.0f);
                    }
                    else if (aux < baseDeDatos.equipoAliado[posicionEnEquipo].velocidadModificada)
                    {
                        info.transform.GetChild(8).transform.GetChild(2).GetComponent<Text>().color = new Color(137.0f / 255.0f, 27.0f / 255.0f, 40.0f / 255.0f);
                    }
                    else
                    {
                        info.transform.GetChild(8).transform.GetChild(2).GetComponent<Text>().color = new Color(50.0f / 255.0f, 50.0f / 255.0f, 50.0f / 255.0f);
                    }
                }
                else
                {
                    info.transform.GetChild(8).transform.GetChild(2).gameObject.SetActive(false);
                }
            }
        }
    }



    void ActivarConfirmacion()
    {
        if(baseDeDatos.idioma == 1)
        {
            confirmacion.transform.GetChild(1).GetComponent<Text>().text = "Keep";
            confirmacion.transform.GetChild(2).GetComponent<Text>().text = "Remove";
        }
        else
        {
            confirmacion.transform.GetChild(1).GetComponent<Text>().text = "Mantener";
            confirmacion.transform.GetChild(2).GetComponent<Text>().text = "Quitar";
        }

        confirmacionActiva = true;
        posConfirmacion = 0;
        confirmacion.SetActive(confirmacion);
        MueveFlechaConfirmacion();
    }



    void DesactivarConfirmacion()
    {
        confirmacionActiva = false;
        confirmacion.SetActive(confirmacionActiva);
    }



    void MueveFlechaConfirmacion()
    {
        if(posConfirmacion == 0)
        {
            posConfirmacion = 1;
            confirmacion.transform.GetChild(0).transform.position = confirmacion.transform.GetChild(4).transform.position;
        }
        else
        {
            posConfirmacion = 0;
            confirmacion.transform.GetChild(0).transform.position = confirmacion.transform.GetChild(3).transform.position;
        }
    }



    void CambiaControl()
    {
        if (!baseDeDatos.mandoActivo)
        {
            baseDeDatos.mandoActivo = true;

            ayuda.transform.GetChild(3).GetComponent<Image>().sprite = baseDeDatos.volverXBOX[0];
            ayuda.transform.GetChild(1).GetComponent<Image>().sprite = baseDeDatos.seleccionXBOX[0];
            ayuda.transform.GetChild(5).GetComponent<Image>().sprite = baseDeDatos.moverXBOX[0];
        }
        else
        {
            baseDeDatos.mandoActivo = false;

            ayuda.transform.GetChild(3).GetComponent<Image>().sprite = baseDeDatos.volverPC[0];
            ayuda.transform.GetChild(1).GetComponent<Image>().sprite = baseDeDatos.seleccionPC[0];
            ayuda.transform.GetChild(5).GetComponent<Image>().sprite = baseDeDatos.moverPC[0];
        }
    }
}
