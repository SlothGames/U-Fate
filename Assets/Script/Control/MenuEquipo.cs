using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuEquipo : MonoBehaviour {
    public Canvas menu;
    public Image flecha;
    int posFlecha;

    public bool activo;
    public bool reactivar;

    public GameObject[] cuadroPersonajes;
    public GameObject[] barraVida;
    public GameObject[] barraExp;
    public GameObject menuInformacion;

    GameObject manager;
    BaseDatos baseDeDatos;
    MusicaManager musica;

    public GameObject fichaPersonaje;
    FichaPersonaje ficha;

    int integrantesEquipo;

    bool pulsado;

    float digitalX;
    float digitalY;

    void Start ()
    {
        manager = GameObject.Find("GameManager");
        musica = GameObject.Find("EfectosSonido").GetComponent<MusicaManager>();
        baseDeDatos = manager.GetComponent<BaseDatos>();

        ficha = fichaPersonaje.GetComponent<FichaPersonaje>();
        integrantesEquipo = 0;

        activo = false;
        DesactivarMenu();
        reactivar = true;
    }
	


	void Update()
    {
        if (activo)
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
                this.transform.GetChild(7).GetChild(1).GetComponent<Image>().sprite = baseDeDatos.seleccionXBOX[0];
                this.transform.GetChild(7).GetChild(3).GetComponent<Image>().sprite = baseDeDatos.volverXBOX[0];
                this.transform.GetChild(7).GetChild(5).GetComponent<Image>().sprite = baseDeDatos.moverXBOX[0];
            }
            else
            {
                this.transform.GetChild(7).GetChild(1).GetComponent<Image>().sprite = baseDeDatos.seleccionPC[0];
                this.transform.GetChild(7).GetChild(3).GetComponent<Image>().sprite = baseDeDatos.volverPC[0];
                this.transform.GetChild(7).GetChild(5).GetComponent<Image>().sprite = baseDeDatos.moverPC[0];
            }

            if (!ficha.activo)
            {
                if (reactivar)
                {
                    MueveFlecha();
                    reactivar = false;
                }

                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || (!pulsado && digitalY > 0))
                {
                    pulsado = true;

                    musica.ProduceEfecto(11);

                    if (posFlecha == 0)
                    {
                        posFlecha = 3;
                    }
                    else if (posFlecha == 3)
                    {
                        posFlecha = integrantesEquipo - 1;
                    }
                    else
                    {
                        posFlecha--;
                    }
                    MueveFlecha();
                }
                else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || (!pulsado && digitalY < 0))
                {
                    pulsado = true;
                    musica.ProduceEfecto(11);
                    if (posFlecha == 0)
                    {
                        if (integrantesEquipo > 1)
                        {
                            posFlecha++;
                        }
                        else
                        {
                            posFlecha = 3;
                        }
                    }
                    else if (posFlecha == 1)
                    {
                        if (integrantesEquipo > 2)
                        {
                            posFlecha++;
                        }
                        else
                        {
                            posFlecha = 3;
                        }
                    }
                    else if (posFlecha == 3)
                    {
                        posFlecha = 0;
                    }
                    else
                    {
                        posFlecha++;
                    }

                    MueveFlecha();
                }
                else if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("B"))
                {
                    musica.ProduceEfecto(12);
                    this.DesactivarMenu();
                }
                else if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                {
                    musica.ProduceEfecto(10);
                    if (posFlecha == 3)
                    {
                        DesactivarMenu();
                    }
                    else if (posFlecha >= 0 && posFlecha < 3)
                    {
                        ficha.ActivaMenu(posFlecha);
                    }
                }
            }
            else
            {
                reactivar = true;
            }
        }
    }



    void MueveFlecha()
    {
        flecha.transform.position = cuadroPersonajes[posFlecha].transform.GetChild(0).transform.position;

        if (posFlecha != 3)
        {
            menuInformacion.transform.GetChild(1).GetComponent<Text>().text = "LV: " + baseDeDatos.equipoAliado[posFlecha].nivel;

            if(baseDeDatos.idioma == 1)
            {
                menuInformacion.transform.GetChild(0).GetComponent<Text>().text = baseDeDatos.equipoAliado[posFlecha].nombreIngles;
                menuInformacion.transform.GetChild(2).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posFlecha].elementoIngles;
            }
            else
            {
                menuInformacion.transform.GetChild(0).GetComponent<Text>().text = baseDeDatos.equipoAliado[posFlecha].nombre;
                menuInformacion.transform.GetChild(2).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posFlecha].elemento;
            }
            
            menuInformacion.transform.GetChild(5).transform.GetChild(0).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posFlecha].vidaModificada;
            menuInformacion.transform.GetChild(6).transform.GetChild(0).GetComponent<Text>(). text = "" + baseDeDatos.equipoAliado[posFlecha].ataqueFisicoModificado;
            menuInformacion.transform.GetChild(7).transform.GetChild(0).GetComponent<Text>(). text = "" + baseDeDatos.equipoAliado[posFlecha].defensaFisicaModificada;
            menuInformacion.transform.GetChild(8).transform.GetChild(0).GetComponent<Text>(). text = "" + baseDeDatos.equipoAliado[posFlecha].ataqueMagicoModificado;
            menuInformacion.transform.GetChild(9).transform.GetChild(0).GetComponent<Text>(). text = "" + baseDeDatos.equipoAliado[posFlecha].defensaMagicaModificada;
            menuInformacion.transform.GetChild(10).transform.GetChild(0).GetComponent<Text>(). text = "" + baseDeDatos.equipoAliado[posFlecha].velocidadModificada;

            if (baseDeDatos.equipoAliado[posFlecha].llevaCasco)
            {
                menuInformacion.transform.GetChild(11).transform.GetChild(1).GetComponent<Text>().text = "E";
            }
            else
            {
                menuInformacion.transform.GetChild(11).transform.GetChild(1).GetComponent<Text>().text = "";
            }

            if (baseDeDatos.equipoAliado[posFlecha].llevaArmadura)
            {
                menuInformacion.transform.GetChild(12).transform.GetChild(1).GetComponent<Text>().text = "E";
            }
            else
            {
                menuInformacion.transform.GetChild(12).transform.GetChild(1).GetComponent<Text>().text = "";
            }

            if (baseDeDatos.equipoAliado[posFlecha].llevaBotas)
            {
                menuInformacion.transform.GetChild(13).transform.GetChild(1).GetComponent<Text>().text = "E";
            }
            else
            {
                menuInformacion.transform.GetChild(13).transform.GetChild(1).GetComponent<Text>().text = "";
            }

            if (baseDeDatos.equipoAliado[posFlecha].llevaArma)
            {
                menuInformacion.transform.GetChild(14).transform.GetChild(1).GetComponent<Text>().text = "E";
            }
            else
            {
                menuInformacion.transform.GetChild(14).transform.GetChild(1).GetComponent<Text>().text = "";
            }

            if (baseDeDatos.equipoAliado[posFlecha].llevaEscudo)
            {
                menuInformacion.transform.GetChild(15).transform.GetChild(1).GetComponent<Text>().text = "E";
            }
            else
            {
                menuInformacion.transform.GetChild(15).transform.GetChild(1).GetComponent<Text>().text = "";
            }

            if (baseDeDatos.equipoAliado[posFlecha].llevaComplemento)
            {
                menuInformacion.transform.GetChild(16).transform.GetChild(1).GetComponent<Text>().text = "E";
            }
            else
            {
                menuInformacion.transform.GetChild(16).transform.GetChild(1).GetComponent<Text>().text = "";
            }
            
        }
        else
        {
            menuInformacion.transform.GetChild(0).GetComponent<Text>().text = "";
            menuInformacion.transform.GetChild(1).GetComponent<Text>().text = "LV: ";
            menuInformacion.transform.GetChild(2).GetComponent<Text>().text = "";
            menuInformacion.transform.GetChild(5).transform.GetChild(0).GetComponent<Text>().text = "";
            menuInformacion.transform.GetChild(6).transform.GetChild(0).GetComponent<Text>().text = "";
            menuInformacion.transform.GetChild(7).transform.GetChild(0).GetComponent<Text>().text = "";
            menuInformacion.transform.GetChild(8).transform.GetChild(0).GetComponent<Text>().text = "";
            menuInformacion.transform.GetChild(9).transform.GetChild(0).GetComponent<Text>().text = "";
            menuInformacion.transform.GetChild(10).transform.GetChild(0).GetComponent<Text>().text = "";

            menuInformacion.transform.GetChild(11).transform.GetChild(1).GetComponent<Text>().text = "";
            menuInformacion.transform.GetChild(12).transform.GetChild(1).GetComponent<Text>().text = "";
            menuInformacion.transform.GetChild(13).transform.GetChild(1).GetComponent<Text>().text = "";
            menuInformacion.transform.GetChild(14).transform.GetChild(1).GetComponent<Text>().text = "";
            menuInformacion.transform.GetChild(15).transform.GetChild(1).GetComponent<Text>().text = "";
            menuInformacion.transform.GetChild(16).transform.GetChild(1).GetComponent<Text>().text = "";
        }
    }



    public void ActivarMenu()
    {
        if (baseDeDatos.mandoActivo)
        {
            this.transform.GetChild(7).GetChild(1).GetComponent<Image>().sprite = baseDeDatos.seleccionXBOX[0];
            this.transform.GetChild(7).GetChild(3).GetComponent<Image>().sprite = baseDeDatos.volverXBOX[0];
            this.transform.GetChild(7).GetChild(5).GetComponent<Image>().sprite = baseDeDatos.moverXBOX[0];
        }
        else
        {
            this.transform.GetChild(7).GetChild(1).GetComponent<Image>().sprite = baseDeDatos.seleccionPC[0];
            this.transform.GetChild(7).GetChild(3).GetComponent<Image>().sprite = baseDeDatos.volverPC[0];
            this.transform.GetChild(7).GetChild(5).GetComponent<Image>().sprite = baseDeDatos.moverPC[0];
        }

        activo = true;
        posFlecha = 0;
        flecha.transform.position = cuadroPersonajes[posFlecha].transform.GetChild(0).transform.position;
        menu.gameObject.SetActive(activo);

        if(baseDeDatos.idioma == 1)
        {
            menu.transform.GetChild(7).GetChild(0).GetComponent<Text>().text = "Select";
            menu.transform.GetChild(7).GetChild(2).GetComponent<Text>().text = "Back";
            menu.transform.GetChild(7).GetChild(4).GetComponent<Text>().text = "Move";

            menu.transform.GetChild(4).GetChild(1).GetComponent<Text>().text = "Back";
            
            menuInformacion.transform.GetChild(5).GetComponent<Text>().text = "Max PS";
            menuInformacion.transform.GetChild(6).GetComponent<Text>().text = "Physical Atck.";
            menuInformacion.transform.GetChild(7).GetComponent<Text>().text = "Physical Def";
            menuInformacion.transform.GetChild(8).GetComponent<Text>().text = "Magical Atck";
            menuInformacion.transform.GetChild(9).GetComponent<Text>().text = "Magical Def";
            menuInformacion.transform.GetChild(10).GetComponent<Text>().text = "Speed";

            menuInformacion.transform.GetChild(2).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posFlecha].elementoIngles;

            menuInformacion.transform.GetChild(11).GetComponent<Text>().text = "Helmet";
            menuInformacion.transform.GetChild(12).GetComponent<Text>().text = "Armor";
            menuInformacion.transform.GetChild(13).GetComponent<Text>().text = "Boots";
            menuInformacion.transform.GetChild(14).GetComponent<Text>().text = "Weapon";
            menuInformacion.transform.GetChild(15).GetComponent<Text>().text = "Shield";
            menuInformacion.transform.GetChild(16).GetComponent<Text>().text = "Comple.";
        }
        else
        {
            menu.transform.GetChild(7).GetChild(0).GetComponent<Text>().text = "Seleccionar";
            menu.transform.GetChild(7).GetChild(2).GetComponent<Text>().text = "Volver";
            menu.transform.GetChild(7).GetChild(4).GetComponent<Text>().text = "Mover";

            menu.transform.GetChild(4).GetChild(1).GetComponent<Text>().text = "Volver";

            menuInformacion.transform.GetChild(2).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posFlecha].elemento;
            menuInformacion.transform.GetChild(5).GetComponent<Text>().text = "PS Máximo";
            menuInformacion.transform.GetChild(6).GetComponent<Text>().text = "Atq. Físico";
            menuInformacion.transform.GetChild(7).GetComponent<Text>().text = "Def. Física";
            menuInformacion.transform.GetChild(8).GetComponent<Text>().text = "Atq. Mágico";
            menuInformacion.transform.GetChild(9).GetComponent<Text>().text = "Def Mágica";
            menuInformacion.transform.GetChild(10).GetComponent<Text>().text = "Velocidad";

            menuInformacion.transform.GetChild(11).GetComponent<Text>().text = "Casco";
            menuInformacion.transform.GetChild(12).GetComponent<Text>().text = "Armadura";
            menuInformacion.transform.GetChild(13).GetComponent<Text>().text = "Botas";
            menuInformacion.transform.GetChild(14).GetComponent<Text>().text = "Arma";
            menuInformacion.transform.GetChild(15).GetComponent<Text>().text = "Escudo";
            menuInformacion.transform.GetChild(16).GetComponent<Text>().text = "Comple.";
        }

        menuInformacion.transform.GetChild(5).transform.GetChild(0).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posFlecha].vidaModificada;
        menuInformacion.transform.GetChild(6).transform.GetChild(0).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posFlecha].ataqueFisicoModificado;
        menuInformacion.transform.GetChild(7).transform.GetChild(0).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posFlecha].defensaFisicaModificada;
        menuInformacion.transform.GetChild(8).transform.GetChild(0).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posFlecha].ataqueMagicoModificado;
        menuInformacion.transform.GetChild(9).transform.GetChild(0).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posFlecha].defensaMagicaModificada;
        menuInformacion.transform.GetChild(10).transform.GetChild(0).GetComponent<Text>().text = "" + baseDeDatos.equipoAliado[posFlecha].velocidadModificada;
        integrantesEquipo = baseDeDatos.numeroIntegrantesEquipo;

        menuInformacion.transform.GetChild(0).GetComponent<Text>().text = baseDeDatos.equipoAliado[posFlecha].nombre;
        menuInformacion.transform.GetChild(1).GetComponent<Text>().text = "LV: " + baseDeDatos.equipoAliado[posFlecha].nivel;

        Sprite imagen;
        Sprite[] imagenes;

        for (int i = 0; i < 3; i++)
        {
            if(i < baseDeDatos.numeroIntegrantesEquipo)
            {
                cuadroPersonajes[i].SetActive(true);
                if(i == 0)
                {
                    if (baseDeDatos.faccion == 0) //golpista
                    {
                        imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/TrajesProta/Emperador");
                        imagen = imagenes[1];
                        cuadroPersonajes[i].transform.GetChild(1).GetComponent<Image>().sprite = imagen;
                    }
                    else if (baseDeDatos.faccion == 1) //imperio
                    {
                        imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/TrajesProta/GuardiaImperial");
                        imagen = imagenes[1];
                        cuadroPersonajes[i].transform.GetChild(1).GetComponent<Image>().sprite = imagen;
                    }
                    else if (baseDeDatos.faccion == 2) //regente
                    {
                        imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/TrajesProta/GuardiaReal");
                        imagen = imagenes[1];
                        cuadroPersonajes[i].transform.GetChild(1).GetComponent<Image>().sprite = imagen;
                    }
                    else if (baseDeDatos.faccion == 3) //resistencia
                    {
                        imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/TrajesProta/Resistencia");
                        imagen = imagenes[1];
                        cuadroPersonajes[i].transform.GetChild(1).GetComponent<Image>().sprite = imagen;
                    }
                    else if (baseDeDatos.faccion == 4) //R.Asalto
                    {
                        imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/TrajesProta/FuerzasAsalto");
                        imagen = imagenes[1];
                        cuadroPersonajes[i].transform.GetChild(1).GetComponent<Image>().sprite = imagen;
                    }
                    else if (baseDeDatos.faccion == 5) //R.Especiales
                    {
                        imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/TrajesProta/EquipoFuerzasEspeciales");
                        imagen = imagenes[1];
                        cuadroPersonajes[i].transform.GetChild(1).GetComponent<Image>().sprite = imagen;
                    }
                    else if (baseDeDatos.faccion == 6) //R.Investigación
                    {
                        imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/TrajesProta/EquipoInteligencia");
                        imagen = imagenes[1];
                        cuadroPersonajes[i].transform.GetChild(1).GetComponent<Image>().sprite = imagen;
                    }
                    else if (baseDeDatos.faccion == 7) //Cúpula
                    {
                        imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/TrajesProta/Cupula");
                        imagen = imagenes[1];
                        cuadroPersonajes[i].transform.GetChild(1).GetComponent<Image>().sprite = imagen;
                    }
                    else if (baseDeDatos.faccion == 8) //Ninguna
                    {
                        cuadroPersonajes[i].transform.GetChild(1).GetComponent<Image>().sprite = baseDeDatos.equipoAliado[i].imagen;
                    }
                    else if (baseDeDatos.faccion == 9) //anarquista
                    {
                        imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/TrajesProta/Anarquista");
                        imagen = imagenes[1];
                        cuadroPersonajes[i].transform.GetChild(1).GetComponent<Image>().sprite = imagen;
                    }
                }
                else
                {
                    cuadroPersonajes[i].transform.GetChild(1).GetComponent<Image>().sprite = baseDeDatos.equipoAliado[i].imagen;
                }
              
                cuadroPersonajes[i].transform.GetChild(2).GetComponent<Text>().text = baseDeDatos.equipoAliado[i].nombre;
                cuadroPersonajes[i].transform.GetChild(3).GetComponent<Text>().text = "PS: " + baseDeDatos.equipoAliado[i].vidaActual + "/" + baseDeDatos.equipoAliado[i].vidaModificada;
                float vida = (float)baseDeDatos.equipoAliado[i].vidaActual / (float)baseDeDatos.equipoAliado[i].vidaModificada;
                barraVida[i].transform.localScale = new Vector3(vida, 1, 1);
                cuadroPersonajes[i].transform.GetChild(4).GetComponent<Text>().text = "EXP: " + baseDeDatos.equipoAliado[i].experiencia + "/" + baseDeDatos.equipoAliado[i].proximoNivel;
                float exp = (float)baseDeDatos.equipoAliado[i].experiencia / (float)baseDeDatos.equipoAliado[i].proximoNivel;
                barraExp[i].transform.localScale = new Vector3(exp, 1, 1);
            }
            else
            {
                cuadroPersonajes[i].SetActive(false);
            }
        }
    }



    void DesactivarMenu()
    {
        activo = false;
        posFlecha = 0;
        menu.gameObject.SetActive(activo);
    }
}
