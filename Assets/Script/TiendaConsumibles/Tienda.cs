using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tienda : MonoBehaviour
{
    GameObject manager;
    ControlJugador controlJugador;
    BaseDatos baseDeDatos;

    public Image menuTienda;
    public Image cuadroTienda;
    public Image flechaOpciones, flechaTienda;
    public Image marcadorOpcion;
    public Image[] fichasPersonajes;
    public Image cantidadCompra;
    public Image ventanaConfirmacion;
    public Image flechaArriba, flechaAbajo;

    public GameObject comprarPos, venderPos, salirPos;
    public GameObject opciones;
    public GameObject[] listaPosicionesObjetos;
    public GameObject[] habilidad1;
    public GameObject[] habilidad2;
    public GameObject[] habilidad3;
    public GameObject[] habilidad4;

    public Text descripcion;
    public Text mostrarDinero;
    MusicaManager musica;
    
    int posFlechaOpciones, posFlechaTienda;
    public int[] indicesObjetos;
    public int[] objetosAVender;

    int objetosMostrar;
    int numeroObjetosVender;
    int dinero;
    int cantidad, precio;
    int indiceObjetoAComprar;
    int posFlechaConfirmacion;
    int paginasVentas, paginaActual;


    bool mueveFlechaOpciones, mueveFlechaTienda, cambiaCantidad, mueveConfirmacion;
    public bool aumentaAtaqueM, aumentaDefensaF, aumentaDefensaM, aumentaEva, aumentaVid, aumentaVel;
    bool abierto;
    bool opcionesActivo;
    bool comprarActivo;
    bool ventaActiva;
    bool confirmacionActiva;
    bool listaVentaActiva;
    public bool[] consumible;
    bool iniciado;

    bool pulsado;

    float digitalX;
    float digitalY;


    void Start ()
    {
        opcionesActivo = false;
        abierto = false;
        confirmacionActiva = false;
        comprarActivo = ventaActiva = false;
        mueveConfirmacion = false;
        listaVentaActiva = false;
        iniciado = false;

        manager = GameObject.Find("GameManager");
        controlJugador = GameObject.Find("Player").GetComponent<ControlJugador>();
        baseDeDatos = manager.GetComponent<BaseDatos>();
        musica = GameObject.Find("EfectosSonido").GetComponent<MusicaManager>();

        posFlechaOpciones = posFlechaTienda = paginaActual = 0;

        mueveFlechaOpciones = mueveFlechaTienda = false;
        ventanaConfirmacion.gameObject.SetActive(false);
        flechaAbajo.gameObject.SetActive(false);
        flechaArriba.gameObject.SetActive(false);

        aumentaAtaqueM = aumentaDefensaF = aumentaDefensaM = aumentaEva = aumentaVid = aumentaVel = false;
        menuTienda.gameObject.SetActive(false);

        cantidadCompra.gameObject.SetActive(false);
    }
	


	void Update ()
    {
        if (abierto)
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
                    Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
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

            if (!comprarActivo && !ventaActiva)
            {
                if (mueveFlechaOpciones)
                {
                    if (posFlechaOpciones == 0)
                    {
                        flechaOpciones.transform.position = comprarPos.transform.position;
                    }
                    else if (posFlechaOpciones == 1)
                    {
                        flechaOpciones.transform.position = venderPos.transform.position;
                    }
                    else
                    {
                        flechaOpciones.transform.position = salirPos.transform.position;
                    }

                    mueveFlechaOpciones = false;
                }
                else if (mueveFlechaTienda)
                {
                    if (!listaVentaActiva)
                    {
                        if (posFlechaTienda < indicesObjetos.Length)
                        {
                            for (int i = 0; i < baseDeDatos.numeroIntegrantesEquipo; i++)
                            {
                                habilidad1[i].gameObject.SetActive(false);
                                habilidad2[i].gameObject.SetActive(false);
                                habilidad3[i].gameObject.SetActive(false);
                                habilidad4[i].gameObject.SetActive(false);
                            }

                            flechaTienda.transform.position = listaPosicionesObjetos[posFlechaTienda].transform.GetChild(4).GetComponent<Transform>().position;
                            marcadorOpcion.transform.position = listaPosicionesObjetos[posFlechaTienda].transform.position;

                            if(baseDeDatos.idioma == 1)
                            {
                                if(baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].tipo == Objeto.tipoObjeto.APRENDE_ATAQUE)
                                {
                                    if (baseDeDatos.listaAtaques[baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].indiceAtq].tipo == Ataque.tipoAtaque.FISICO || baseDeDatos.listaAtaques[baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].indiceAtq].tipo == Ataque.tipoAtaque.MAGICO)
                                    {
                                        descripcion.text = baseDeDatos.listaAtaques[baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].indiceAtq].nombreIngles + ": " + baseDeDatos.listaAtaques[baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].indiceAtq].descripcionIngles + ". \n\n Power: " + baseDeDatos.listaAtaques[baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].indiceAtq].potencia + "   Precision: " + baseDeDatos.listaAtaques[baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].indiceAtq].precision + "\n Element: " + baseDeDatos.listaAtaques[baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].indiceAtq].elementoIngles + "  " + baseDeDatos.listaAtaques[baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].indiceAtq].tipoIngles;
                                    }
                                    else
                                    {
                                        descripcion.text = baseDeDatos.listaAtaques[baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].indiceAtq].nombreIngles + ": " + baseDeDatos.listaAtaques[baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].indiceAtq].descripcionIngles + "\n\n SUPPORT";
                                    }
                                }
                                else
                                {
                                    descripcion.text = baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].descripcionIngles;
                                }
                            }
                            else
                            {
                                if (baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].tipo == Objeto.tipoObjeto.APRENDE_ATAQUE)
                                {
                                    if (baseDeDatos.listaAtaques[baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].indiceAtq].tipo == Ataque.tipoAtaque.FISICO || baseDeDatos.listaAtaques[indicesObjetos[posFlechaTienda]].tipo == Ataque.tipoAtaque.MAGICO)
                                    {
                                        descripcion.text = baseDeDatos.listaAtaques[baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].indiceAtq].nombre + ": " + baseDeDatos.listaAtaques[baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].indiceAtq].descripcion + ". \n\n Potencia: " + baseDeDatos.listaAtaques[baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].indiceAtq].potencia + "   Precision: " + baseDeDatos.listaAtaques[baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].indiceAtq].precision + "\n Elemento: " + baseDeDatos.listaAtaques[baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].indiceAtq].elemento + "  " + baseDeDatos.listaAtaques[baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].indiceAtq].tipo;
                                    }
                                    else
                                    {
                                        descripcion.text = baseDeDatos.listaAtaques[baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].indiceAtq].nombre + ": " + baseDeDatos.listaAtaques[baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].indiceAtq].descripcion + "\n\n APOYO";
                                    }
                                }
                                else
                                {
                                    descripcion.text = baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].descripcion;
                                }
                            }


                            if (baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].tipo == Objeto.tipoObjeto.EQUIPO)
                            {
                                for (int i = 0; i < baseDeDatos.numeroIntegrantesEquipo; i++)
                                {
                                    aumentaAtaqueM = aumentaDefensaF = aumentaDefensaM = aumentaEva = aumentaVid = aumentaVel = false;

                                    if (baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].numeroDeMejoras > 0)
                                    {
                                        int mejora, actual;
                                        habilidad1[i].gameObject.SetActive(true);

                                        if (baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].aumentaAtaqueF)
                                        {
                                            actual = baseDeDatos.equipoAliado[i].ataqueFisicoModificado;
                                            mejora = baseDeDatos.equipoAliado[i].ataqueFisico + baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].aumentoAtaqueFisico;

                                            if(baseDeDatos.idioma == 1)
                                            {
                                                habilidad1[i].transform.GetChild(0).GetComponent<Text>().text = "Phy. Atck.";
                                            }
                                            else
                                            {
                                                habilidad1[i].transform.GetChild(0).GetComponent<Text>().text = "Atq. Fis.";
                                            }

                                            habilidad1[i].transform.GetChild(1).GetComponent<Text>().text = "" + actual;

                                            if (mejora > actual)
                                            {
                                                habilidad1[i].transform.GetChild(2).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                                habilidad1[i].transform.GetChild(3).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                            }
                                            else if (mejora < actual)
                                            {
                                                habilidad1[i].transform.GetChild(2).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                                habilidad1[i].transform.GetChild(3).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                            }
                                            else
                                            {
                                                habilidad1[i].transform.GetChild(2).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f);
                                                habilidad1[i].transform.GetChild(3).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f); ;
                                            }

                                            habilidad1[i].transform.GetChild(3).GetComponent<Text>().text = "" + mejora;
                                        }
                                        else if (baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].aumentaAtaqueM)
                                        {
                                            aumentaAtaqueM = true;
                                            actual = baseDeDatos.equipoAliado[i].ataqueMagicoModificado;
                                            mejora = baseDeDatos.equipoAliado[i].ataqueMagico + baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].aumentoAtaqueMagico;

                                            if (baseDeDatos.idioma == 1)
                                            {
                                                habilidad1[i].transform.GetChild(0).GetComponent<Text>().text = "Mag. Atck.";
                                            }
                                            else
                                            {
                                                habilidad1[i].transform.GetChild(0).GetComponent<Text>().text = "Atq. Mag.";
                                            }
                                            
                                            habilidad1[i].transform.GetChild(1).GetComponent<Text>().text = "" + actual;

                                            if (mejora > actual)
                                            {
                                                habilidad1[i].transform.GetChild(2).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                                habilidad1[i].transform.GetChild(3).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                            }
                                            else if (mejora < actual)
                                            {
                                                habilidad1[i].transform.GetChild(2).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                                habilidad1[i].transform.GetChild(3).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                            }
                                            else
                                            {
                                                habilidad1[i].transform.GetChild(2).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f);
                                                habilidad1[i].transform.GetChild(3).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f); ;
                                            }

                                            habilidad1[i].transform.GetChild(3).GetComponent<Text>().text = "" + mejora;
                                        }
                                        else if (baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].aumentaDefensaF)
                                        {
                                            aumentaDefensaF = true;
                                            actual = baseDeDatos.equipoAliado[i].defensaFisicaModificada;
                                            mejora = baseDeDatos.equipoAliado[i].defensaFisica + baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].aumentoDefensaFisica;

                                            if (baseDeDatos.idioma == 1)
                                            {
                                                habilidad1[i].transform.GetChild(0).GetComponent<Text>().text = "Phy. Def.";
                                            }
                                            else
                                            {
                                                habilidad1[i].transform.GetChild(0).GetComponent<Text>().text = "Def. Fis.";
                                            }
                                            
                                            habilidad1[i].transform.GetChild(1).GetComponent<Text>().text = "" + actual;

                                            if (mejora > actual)
                                            {
                                                habilidad1[i].transform.GetChild(2).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                                habilidad1[i].transform.GetChild(3).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                            }
                                            else if (mejora < actual)
                                            {
                                                habilidad1[i].transform.GetChild(2).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                                habilidad1[i].transform.GetChild(3).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                            }
                                            else
                                            {
                                                habilidad1[i].transform.GetChild(2).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f);
                                                habilidad1[i].transform.GetChild(3).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f); ;
                                            }

                                            habilidad1[i].transform.GetChild(3).GetComponent<Text>().text = "" + mejora;
                                        }
                                        else if (baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].aumentaDefensaM)
                                        {
                                            aumentaDefensaM = true;
                                            actual = baseDeDatos.equipoAliado[i].defensaMagicaModificada;
                                            mejora = baseDeDatos.equipoAliado[i].defensaMagica + baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].aumentoDefensaMagica;

                                            if (baseDeDatos.idioma == 1)
                                            {
                                                habilidad1[i].transform.GetChild(0).GetComponent<Text>().text = "Mag. Def.";
                                            }
                                            else
                                            {
                                                habilidad1[i].transform.GetChild(0).GetComponent<Text>().text = "Def. Mag.";
                                            }
                                            
                                            habilidad1[i].transform.GetChild(1).GetComponent<Text>().text = "" + actual;

                                            if (mejora > actual)
                                            {
                                                habilidad1[i].transform.GetChild(2).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                                habilidad1[i].transform.GetChild(3).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                            }
                                            else if (mejora < actual)
                                            {
                                                habilidad1[i].transform.GetChild(2).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                                habilidad1[i].transform.GetChild(3).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                            }
                                            else
                                            {
                                                habilidad1[i].transform.GetChild(2).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f);
                                                habilidad1[i].transform.GetChild(3).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f); ;
                                            }

                                            habilidad1[i].transform.GetChild(3).GetComponent<Text>().text = "" + mejora;
                                        }
                                        else if (baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].aumentaVel)
                                        {
                                            aumentaVel = true;
                                            actual = baseDeDatos.equipoAliado[i].velocidadModificada;
                                            mejora = baseDeDatos.equipoAliado[i].velocidad + baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].aumentoVelocidad;

                                            if (baseDeDatos.idioma == 1)
                                            {
                                                habilidad1[i].transform.GetChild(0).GetComponent<Text>().text = "Speed";
                                            }
                                            else
                                            {
                                                habilidad1[i].transform.GetChild(0).GetComponent<Text>().text = "Vel";
                                            }
                                            
                                            habilidad1[i].transform.GetChild(1).GetComponent<Text>().text = "" + actual;

                                            if (mejora > actual)
                                            {
                                                habilidad1[i].transform.GetChild(2).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                                habilidad1[i].transform.GetChild(3).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                            }
                                            else if (mejora < actual)
                                            {
                                                habilidad1[i].transform.GetChild(2).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                                habilidad1[i].transform.GetChild(3).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                            }
                                            else
                                            {
                                                habilidad1[i].transform.GetChild(2).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f);
                                                habilidad1[i].transform.GetChild(3).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f); ;
                                            }

                                            habilidad1[i].transform.GetChild(3).GetComponent<Text>().text = "" + mejora;
                                        }
                                        else if (baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].aumentaVid)
                                        {
                                            aumentaVid = true;
                                            actual = baseDeDatos.equipoAliado[i].vidaModificada;
                                            mejora = baseDeDatos.equipoAliado[i].vida + baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].aumentoVida;

                                            if (baseDeDatos.idioma == 1)
                                            {
                                                habilidad1[i].transform.GetChild(0).GetComponent<Text>().text = "Life";
                                            }
                                            else
                                            {
                                                habilidad1[i].transform.GetChild(0).GetComponent<Text>().text = "Vida";
                                            }
                                            
                                            habilidad1[i].transform.GetChild(1).GetComponent<Text>().text = "" + actual;

                                            if (mejora > actual)
                                            {
                                                habilidad1[i].transform.GetChild(2).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                                habilidad1[i].transform.GetChild(3).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                            }
                                            else if (mejora < actual)
                                            {
                                                habilidad1[i].transform.GetChild(2).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                                habilidad1[i].transform.GetChild(3).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                            }
                                            else
                                            {
                                                habilidad1[i].transform.GetChild(2).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f);
                                                habilidad1[i].transform.GetChild(3).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f); ;
                                            }

                                            habilidad1[i].transform.GetChild(3).GetComponent<Text>().text = "" + mejora;
                                        }
                                    }

                                    if (baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].numeroDeMejoras > 1)
                                    {
                                        int mejora, actual;
                                        habilidad2[i].gameObject.SetActive(true);

                                        if (baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].aumentaAtaqueM && !aumentaAtaqueM)
                                        {
                                            aumentaAtaqueM = true;
                                            actual = baseDeDatos.equipoAliado[i].ataqueMagicoModificado;
                                            mejora = baseDeDatos.equipoAliado[i].ataqueMagico + baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].aumentoAtaqueMagico;

                                            if (baseDeDatos.idioma == 1)
                                            {
                                                habilidad2[i].transform.GetChild(0).GetComponent<Text>().text = "Mag. Atck.";
                                            }
                                            else
                                            {
                                                habilidad2[i].transform.GetChild(0).GetComponent<Text>().text = "Atq. Mag.";
                                            }
                                            
                                            habilidad2[i].transform.GetChild(1).GetComponent<Text>().text = "" + actual;

                                            if (mejora > actual)
                                            {
                                                habilidad2[i].transform.GetChild(2).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                                habilidad2[i].transform.GetChild(3).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                            }
                                            else if (mejora < actual)
                                            {
                                                habilidad2[i].transform.GetChild(2).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                                habilidad2[i].transform.GetChild(3).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                            }
                                            else
                                            {
                                                habilidad2[i].transform.GetChild(2).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f);
                                                habilidad2[i].transform.GetChild(3).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f); ;
                                            }

                                            habilidad2[i].transform.GetChild(3).GetComponent<Text>().text = "" + mejora;
                                        }
                                        else if (baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].aumentaDefensaF && !aumentaDefensaF)
                                        {
                                            aumentaDefensaF = true;
                                            actual = baseDeDatos.equipoAliado[i].defensaFisicaModificada;
                                            mejora = baseDeDatos.equipoAliado[i].defensaFisica + baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].aumentoDefensaFisica;

                                            if (baseDeDatos.idioma == 1)
                                            {
                                                habilidad2[i].transform.GetChild(0).GetComponent<Text>().text = "Phy. Def.";
                                            }
                                            else
                                            {
                                                habilidad2[i].transform.GetChild(0).GetComponent<Text>().text = "Def. Fis.";
                                            }
                                            
                                            habilidad2[i].transform.GetChild(1).GetComponent<Text>().text = "" + actual;

                                            if (mejora > actual)
                                            {
                                                habilidad2[i].transform.GetChild(2).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                                habilidad2[i].transform.GetChild(3).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                            }
                                            else if (mejora < actual)
                                            {
                                                habilidad2[i].transform.GetChild(2).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                                habilidad2[i].transform.GetChild(3).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                            }
                                            else
                                            {
                                                habilidad2[i].transform.GetChild(2).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f);
                                                habilidad2[i].transform.GetChild(3).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f); ;
                                            }

                                            habilidad2[i].transform.GetChild(3).GetComponent<Text>().text = "" + mejora;
                                        }
                                        else if (baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].aumentaDefensaM && !aumentaDefensaM)
                                        {
                                            aumentaDefensaM = true;
                                            actual = baseDeDatos.equipoAliado[i].defensaMagicaModificada;
                                            mejora = baseDeDatos.equipoAliado[i].defensaMagica + baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].aumentoDefensaMagica;

                                            if (baseDeDatos.idioma == 1)
                                            {
                                                habilidad2[i].transform.GetChild(0).GetComponent<Text>().text = "Mag. Def.";
                                            }
                                            else
                                            {
                                                habilidad2[i].transform.GetChild(0).GetComponent<Text>().text = "Def. Mag.";
                                            }
                                            
                                            habilidad2[i].transform.GetChild(1).GetComponent<Text>().text = "" + actual;

                                            if (mejora > actual)
                                            {
                                                habilidad2[i].transform.GetChild(2).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                                habilidad2[i].transform.GetChild(3).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                            }
                                            else if (mejora < actual)
                                            {
                                                habilidad2[i].transform.GetChild(2).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                                habilidad2[i].transform.GetChild(3).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                            }
                                            else
                                            {
                                                habilidad2[i].transform.GetChild(2).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f);
                                                habilidad2[i].transform.GetChild(3).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f); ;
                                            }

                                            habilidad2[i].transform.GetChild(3).GetComponent<Text>().text = "" + mejora;
                                        }
                                        else if (baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].aumentaVel && !aumentaVel)
                                        {
                                            aumentaVel = true;
                                            actual = baseDeDatos.equipoAliado[i].velocidadModificada;
                                            mejora = baseDeDatos.equipoAliado[i].velocidad + baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].aumentoVelocidad;

                                            if (baseDeDatos.idioma == 1)
                                            {
                                                habilidad2[i].transform.GetChild(0).GetComponent<Text>().text = "Speed";
                                            }
                                            else
                                            {
                                                habilidad2[i].transform.GetChild(0).GetComponent<Text>().text = "Vel.";
                                            }
                                            
                                            habilidad2[i].transform.GetChild(1).GetComponent<Text>().text = "" + actual;

                                            if (mejora > actual)
                                            {
                                                habilidad2[i].transform.GetChild(2).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                                habilidad2[i].transform.GetChild(3).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                            }
                                            else if (mejora < actual)
                                            {
                                                habilidad2[i].transform.GetChild(2).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                                habilidad2[i].transform.GetChild(3).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                            }
                                            else
                                            {
                                                habilidad2[i].transform.GetChild(2).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f);
                                                habilidad2[i].transform.GetChild(3).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f); ;
                                            }

                                            habilidad2[i].transform.GetChild(3).GetComponent<Text>().text = "" + mejora;
                                        }
                                        else if (baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].aumentaVid && !aumentaVid)
                                        {
                                            aumentaVid = true;
                                            actual = baseDeDatos.equipoAliado[i].vidaModificada;
                                            mejora = baseDeDatos.equipoAliado[i].vida + baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].aumentoVida;

                                            if (baseDeDatos.idioma == 1)
                                            {
                                                habilidad2[i].transform.GetChild(0).GetComponent<Text>().text = "Life";
                                            }
                                            else
                                            {
                                                habilidad2[i].transform.GetChild(0).GetComponent<Text>().text = "Vida";
                                            }
                                            
                                            habilidad2[i].transform.GetChild(1).GetComponent<Text>().text = "" + actual;

                                            if (mejora > actual)
                                            {
                                                habilidad2[i].transform.GetChild(2).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                                habilidad2[i].transform.GetChild(3).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                            }
                                            else if (mejora < actual)
                                            {
                                                habilidad2[i].transform.GetChild(2).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                                habilidad2[i].transform.GetChild(3).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                            }
                                            else
                                            {
                                                habilidad2[i].transform.GetChild(2).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f);
                                                habilidad2[i].transform.GetChild(3).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f); ;
                                            }

                                            habilidad2[i].transform.GetChild(3).GetComponent<Text>().text = "" + mejora;
                                        }
                                    }

                                    if (baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].numeroDeMejoras > 2)
                                    {
                                        int mejora, actual;
                                        habilidad3[i].gameObject.SetActive(true);

                                        if (baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].aumentaDefensaF && !aumentaDefensaF)
                                        {
                                            aumentaDefensaF = true;
                                            actual = baseDeDatos.equipoAliado[i].defensaFisicaModificada;
                                            mejora = baseDeDatos.equipoAliado[i].defensaFisica + baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].aumentoDefensaFisica;

                                            if (baseDeDatos.idioma == 1)
                                            {
                                                habilidad3[i].transform.GetChild(0).GetComponent<Text>().text = "Phy. Def.";
                                            }
                                            else
                                            {
                                                habilidad3[i].transform.GetChild(0).GetComponent<Text>().text = "Def. Fis.";
                                            }
                                            
                                            habilidad3[i].transform.GetChild(1).GetComponent<Text>().text = "" + actual;

                                            if (mejora > actual)
                                            {
                                                habilidad3[i].transform.GetChild(2).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                                habilidad3[i].transform.GetChild(3).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                            }
                                            else if (mejora < actual)
                                            {
                                                habilidad3[i].transform.GetChild(2).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                                habilidad3[i].transform.GetChild(3).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                            }
                                            else
                                            {
                                                habilidad3[i].transform.GetChild(2).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f);
                                                habilidad3[i].transform.GetChild(3).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f); ;
                                            }

                                            habilidad3[i].transform.GetChild(3).GetComponent<Text>().text = "" + mejora;
                                        }
                                        else if (baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].aumentaDefensaM && !aumentaDefensaM)
                                        {
                                            aumentaDefensaM = true;
                                            actual = baseDeDatos.equipoAliado[i].defensaMagicaModificada;
                                            mejora = baseDeDatos.equipoAliado[i].defensaMagica + baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].aumentoDefensaMagica;

                                            if (baseDeDatos.idioma == 1)
                                            {
                                                habilidad3[i].transform.GetChild(0).GetComponent<Text>().text = "Mag. Def.";
                                            }
                                            else
                                            {
                                                habilidad3[i].transform.GetChild(0).GetComponent<Text>().text = "Def. Mag.";
                                            }
                                            
                                            habilidad3[i].transform.GetChild(1).GetComponent<Text>().text = "" + actual;

                                            if (mejora > actual)
                                            {
                                                habilidad3[i].transform.GetChild(2).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                                habilidad3[i].transform.GetChild(3).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                            }
                                            else if (mejora < actual)
                                            {
                                                habilidad3[i].transform.GetChild(2).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                                habilidad3[i].transform.GetChild(3).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                            }
                                            else
                                            {
                                                habilidad3[i].transform.GetChild(2).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f);
                                                habilidad3[i].transform.GetChild(3).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f); ;
                                            }

                                            habilidad3[i].transform.GetChild(3).GetComponent<Text>().text = "" + mejora;
                                        }
                                        else if (baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].aumentaVel && !aumentaVel)
                                        {
                                            aumentaVel = true;
                                            actual = baseDeDatos.equipoAliado[i].velocidadModificada;
                                            mejora = baseDeDatos.equipoAliado[i].velocidad + baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].aumentoVelocidad;

                                            if (baseDeDatos.idioma == 1)
                                            {
                                                habilidad3[i].transform.GetChild(0).GetComponent<Text>().text = "Speed";
                                            }
                                            else
                                            {
                                                habilidad3[i].transform.GetChild(0).GetComponent<Text>().text = "Vel.";
                                            }
                                            
                                            habilidad3[i].transform.GetChild(1).GetComponent<Text>().text = "" + actual;

                                            if (mejora > actual)
                                            {
                                                habilidad3[i].transform.GetChild(2).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                                habilidad3[i].transform.GetChild(3).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                            }
                                            else if (mejora < actual)
                                            {
                                                habilidad3[i].transform.GetChild(2).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                                habilidad3[i].transform.GetChild(3).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                            }
                                            else
                                            {
                                                habilidad3[i].transform.GetChild(2).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f);
                                                habilidad3[i].transform.GetChild(3).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f); ;
                                            }

                                            habilidad3[i].transform.GetChild(3).GetComponent<Text>().text = "" + mejora;
                                        }
                                        else if (baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].aumentaVid && !aumentaVid)
                                        {
                                            aumentaVid = true;
                                            actual = baseDeDatos.equipoAliado[i].vidaModificada;
                                            mejora = baseDeDatos.equipoAliado[i].vida + baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].aumentoVida;

                                            if (baseDeDatos.idioma == 1)
                                            {
                                                habilidad3[i].transform.GetChild(0).GetComponent<Text>().text = "Life";
                                            }
                                            else
                                            {
                                                habilidad3[i].transform.GetChild(0).GetComponent<Text>().text = "Vida";
                                            }
                                            
                                            habilidad3[i].transform.GetChild(1).GetComponent<Text>().text = "" + actual;

                                            if (mejora > actual)
                                            {
                                                habilidad3[i].transform.GetChild(2).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                                habilidad3[i].transform.GetChild(3).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                            }
                                            else if (mejora < actual)
                                            {
                                                habilidad3[i].transform.GetChild(2).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                                habilidad3[i].transform.GetChild(3).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                            }
                                            else
                                            {
                                                habilidad3[i].transform.GetChild(2).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f);
                                                habilidad3[i].transform.GetChild(3).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f); ;
                                            }

                                            habilidad3[i].transform.GetChild(3).GetComponent<Text>().text = "" + mejora;
                                        }
                                    }

                                    if (baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].numeroDeMejoras > 3)
                                    {
                                        int mejora, actual;
                                        habilidad4[i].gameObject.SetActive(true);

                                        if (baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].aumentaDefensaM && !aumentaDefensaM)
                                        {
                                            aumentaDefensaM = true;
                                            actual = baseDeDatos.equipoAliado[i].defensaMagicaModificada;
                                            mejora = baseDeDatos.equipoAliado[i].defensaMagica + baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].aumentoDefensaMagica;

                                            if (baseDeDatos.idioma == 1)
                                            {
                                                habilidad4[i].transform.GetChild(0).GetComponent<Text>().text = "Mag. Def.";
                                            }
                                            else
                                            {
                                                habilidad4[i].transform.GetChild(0).GetComponent<Text>().text = "Def. Mag.";
                                            }
                                            
                                            habilidad4[i].transform.GetChild(1).GetComponent<Text>().text = "" + actual;

                                            if (mejora > actual)
                                            {
                                                habilidad4[i].transform.GetChild(2).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                                habilidad4[i].transform.GetChild(3).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                            }
                                            else if (mejora < actual)
                                            {
                                                habilidad4[i].transform.GetChild(2).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                                habilidad4[i].transform.GetChild(3).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                            }
                                            else
                                            {
                                                habilidad4[i].transform.GetChild(2).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f);
                                                habilidad4[i].transform.GetChild(3).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f); ;
                                            }

                                            habilidad4[i].transform.GetChild(3).GetComponent<Text>().text = "" + mejora;
                                        }
                                        else if (baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].aumentaVel && !aumentaVel)
                                        {
                                            aumentaVel = true;
                                            actual = baseDeDatos.equipoAliado[i].velocidadModificada;
                                            mejora = baseDeDatos.equipoAliado[i].velocidad + baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].aumentoVelocidad;

                                            if (baseDeDatos.idioma == 1)
                                            {
                                                habilidad4[i].transform.GetChild(0).GetComponent<Text>().text = "Speed";
                                            }
                                            else
                                            {
                                                habilidad4[i].transform.GetChild(0).GetComponent<Text>().text = "Vel.";
                                            }
                                            
                                            habilidad4[i].transform.GetChild(1).GetComponent<Text>().text = "" + actual;

                                            if (mejora > actual)
                                            {
                                                habilidad4[i].transform.GetChild(2).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                                habilidad4[i].transform.GetChild(3).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                            }
                                            else if (mejora < actual)
                                            {
                                                habilidad4[i].transform.GetChild(2).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                                habilidad4[i].transform.GetChild(3).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                            }
                                            else
                                            {
                                                habilidad4[i].transform.GetChild(2).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f);
                                                habilidad4[i].transform.GetChild(3).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f); ;
                                            }

                                            habilidad4[i].transform.GetChild(3).GetComponent<Text>().text = "" + mejora;
                                        }
                                        else if (baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].aumentaVid && !aumentaVid)
                                        {
                                            aumentaVid = true;
                                            actual = baseDeDatos.equipoAliado[i].vidaModificada;
                                            mejora = baseDeDatos.equipoAliado[i].vida + baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].aumentoVida;

                                            if (baseDeDatos.idioma == 1)
                                            {
                                                habilidad4[i].transform.GetChild(0).GetComponent<Text>().text = "Life";
                                            }
                                            else
                                            {
                                                habilidad4[i].transform.GetChild(0).GetComponent<Text>().text = "Vida";
                                            }
                                            
                                            habilidad4[i].transform.GetChild(1).GetComponent<Text>().text = "" + actual;

                                            if (mejora > actual)
                                            {
                                                habilidad4[i].transform.GetChild(2).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                                habilidad4[i].transform.GetChild(3).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                            }
                                            else if (mejora < actual)
                                            {
                                                habilidad4[i].transform.GetChild(2).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                                habilidad4[i].transform.GetChild(3).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                            }
                                            else
                                            {
                                                habilidad4[i].transform.GetChild(2).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f);
                                                habilidad4[i].transform.GetChild(3).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f); ;
                                            }

                                            habilidad4[i].transform.GetChild(3).GetComponent<Text>().text = "" + mejora;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (posFlechaTienda < objetosMostrar)
                        {
                            for (int i = 0; i < baseDeDatos.numeroIntegrantesEquipo; i++)
                            {
                                habilidad1[i].gameObject.SetActive(false);
                                habilidad2[i].gameObject.SetActive(false);
                                habilidad3[i].gameObject.SetActive(false);
                                habilidad4[i].gameObject.SetActive(false);
                            }

                            flechaTienda.transform.position = listaPosicionesObjetos[posFlechaTienda].transform.GetChild(4).GetComponent<Transform>().position;
                            marcadorOpcion.transform.position = listaPosicionesObjetos[posFlechaTienda].transform.position;

                            int aux = posFlechaTienda + (paginaActual * 10);

                            if (consumible[aux])
                            {
                                if(baseDeDatos.idioma == 1)
                                {
                                    descripcion.text = baseDeDatos.objetosConsumibles[objetosAVender[aux]].descripcionIngles;
                                }
                                else
                                {
                                    descripcion.text = baseDeDatos.objetosConsumibles[objetosAVender[aux]].descripcion;
                                }
                            }
                            else
                            {
                                if (baseDeDatos.idioma == 1)
                                {
                                    descripcion.text = baseDeDatos.objetosEquipo[objetosAVender[aux]].descripcion;
                                }
                                else
                                {
                                    descripcion.text = baseDeDatos.objetosEquipo[objetosAVender[aux]].descripcion;
                                }

                                for (int i = 0; i < baseDeDatos.numeroIntegrantesEquipo; i++)
                                {
                                    aumentaAtaqueM = aumentaDefensaF = aumentaDefensaM = aumentaEva = aumentaVid = aumentaVel = false;

                                    if (baseDeDatos.objetosEquipo[objetosAVender[posFlechaTienda]].numeroDeMejoras > 0)
                                    {
                                        int mejora, actual;
                                        habilidad1[i].gameObject.SetActive(true);

                                        if (baseDeDatos.objetosEquipo[objetosAVender[posFlechaTienda]].aumentaAtaqueF)
                                        {
                                            actual = baseDeDatos.equipoAliado[i].ataqueFisicoModificado;
                                            mejora = baseDeDatos.equipoAliado[i].ataqueFisico + baseDeDatos.objetosEquipo[objetosAVender[posFlechaTienda]].aumentoAtaqueFisico;

                                            if (baseDeDatos.idioma == 1)
                                            {
                                                habilidad1[i].transform.GetChild(0).GetComponent<Text>().text = "Phy. Atck.";
                                            }
                                            else
                                            {
                                                habilidad1[i].transform.GetChild(0).GetComponent<Text>().text = "Atq. Fis.";
                                            }
                                            
                                            habilidad1[i].transform.GetChild(1).GetComponent<Text>().text = "" + actual;

                                            if (mejora > actual)
                                            {
                                                habilidad1[i].transform.GetChild(2).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                                habilidad1[i].transform.GetChild(3).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                            }
                                            else if (mejora < actual)
                                            {
                                                habilidad1[i].transform.GetChild(2).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                                habilidad1[i].transform.GetChild(3).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                            }
                                            else
                                            {
                                                habilidad1[i].transform.GetChild(2).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f);
                                                habilidad1[i].transform.GetChild(3).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f); ;
                                            }

                                            habilidad1[i].transform.GetChild(3).GetComponent<Text>().text = "" + mejora;
                                        }
                                        else if (baseDeDatos.objetosEquipo[objetosAVender[posFlechaTienda]].aumentaAtaqueM)
                                        {
                                            aumentaAtaqueM = true;
                                            actual = baseDeDatos.equipoAliado[i].ataqueMagicoModificado;
                                            mejora = baseDeDatos.equipoAliado[i].ataqueMagico + baseDeDatos.objetosEquipo[objetosAVender[posFlechaTienda]].aumentoAtaqueMagico;

                                            if (baseDeDatos.idioma == 1)
                                            {
                                                habilidad1[i].transform.GetChild(0).GetComponent<Text>().text = "Mag. Atck.";
                                            }
                                            else
                                            {
                                                habilidad1[i].transform.GetChild(0).GetComponent<Text>().text = "Atq. Mag.";
                                            }
                                            
                                            habilidad1[i].transform.GetChild(1).GetComponent<Text>().text = "" + actual;

                                            if (mejora > actual)
                                            {
                                                habilidad1[i].transform.GetChild(2).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                                habilidad1[i].transform.GetChild(3).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                            }
                                            else if (mejora < actual)
                                            {
                                                habilidad1[i].transform.GetChild(2).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                                habilidad1[i].transform.GetChild(3).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                            }
                                            else
                                            {
                                                habilidad1[i].transform.GetChild(2).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f);
                                                habilidad1[i].transform.GetChild(3).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f); ;
                                            }

                                            habilidad1[i].transform.GetChild(3).GetComponent<Text>().text = "" + mejora;
                                        }
                                        else if (baseDeDatos.objetosEquipo[objetosAVender[posFlechaTienda]].aumentaDefensaF)
                                        {
                                            aumentaDefensaF = true;
                                            actual = baseDeDatos.equipoAliado[i].defensaFisicaModificada;
                                            mejora = baseDeDatos.equipoAliado[i].defensaFisica + baseDeDatos.objetosEquipo[objetosAVender[posFlechaTienda]].aumentoDefensaFisica;

                                            if (baseDeDatos.idioma == 1)
                                            {
                                                habilidad1[i].transform.GetChild(0).GetComponent<Text>().text = "Phy. Def.";
                                            }
                                            else
                                            {
                                                habilidad1[i].transform.GetChild(0).GetComponent<Text>().text = "Def. Fis.";
                                            }
                                            
                                            habilidad1[i].transform.GetChild(1).GetComponent<Text>().text = "" + actual;

                                            if (mejora > actual)
                                            {
                                                habilidad1[i].transform.GetChild(2).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                                habilidad1[i].transform.GetChild(3).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                            }
                                            else if (mejora < actual)
                                            {
                                                habilidad1[i].transform.GetChild(2).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                                habilidad1[i].transform.GetChild(3).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                            }
                                            else
                                            {
                                                habilidad1[i].transform.GetChild(2).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f);
                                                habilidad1[i].transform.GetChild(3).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f); ;
                                            }

                                            habilidad1[i].transform.GetChild(3).GetComponent<Text>().text = "" + mejora;
                                        }
                                        else if (baseDeDatos.objetosEquipo[objetosAVender[posFlechaTienda]].aumentaDefensaM)
                                        {
                                            aumentaDefensaM = true;
                                            actual = baseDeDatos.equipoAliado[i].defensaMagicaModificada;
                                            mejora = baseDeDatos.equipoAliado[i].defensaMagica + baseDeDatos.objetosEquipo[objetosAVender[posFlechaTienda]].aumentoDefensaMagica;

                                            if (baseDeDatos.idioma == 1)
                                            {
                                                habilidad1[i].transform.GetChild(0).GetComponent<Text>().text = "Mag. Def.";
                                            }
                                            else
                                            {
                                                habilidad1[i].transform.GetChild(0).GetComponent<Text>().text = "Def. Mag.";
                                            }
                                            
                                            habilidad1[i].transform.GetChild(1).GetComponent<Text>().text = "" + actual;

                                            if (mejora > actual)
                                            {
                                                habilidad1[i].transform.GetChild(2).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                                habilidad1[i].transform.GetChild(3).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                            }
                                            else if (mejora < actual)
                                            {
                                                habilidad1[i].transform.GetChild(2).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                                habilidad1[i].transform.GetChild(3).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                            }
                                            else
                                            {
                                                habilidad1[i].transform.GetChild(2).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f);
                                                habilidad1[i].transform.GetChild(3).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f); ;
                                            }

                                            habilidad1[i].transform.GetChild(3).GetComponent<Text>().text = "" + mejora;
                                        }
                                        else if (baseDeDatos.objetosEquipo[objetosAVender[posFlechaTienda]].aumentaVel)
                                        {
                                            aumentaVel = true;
                                            actual = baseDeDatos.equipoAliado[i].velocidadModificada;
                                            mejora = baseDeDatos.equipoAliado[i].velocidad + baseDeDatos.objetosEquipo[objetosAVender[posFlechaTienda]].aumentoVelocidad;

                                            if (baseDeDatos.idioma == 1)
                                            {
                                                habilidad1[i].transform.GetChild(0).GetComponent<Text>().text = "Speed";
                                            }
                                            else
                                            {
                                                habilidad1[i].transform.GetChild(0).GetComponent<Text>().text = "Vel.";
                                            }
                                            
                                            habilidad1[i].transform.GetChild(1).GetComponent<Text>().text = "" + actual;

                                            if (mejora > actual)
                                            {
                                                habilidad1[i].transform.GetChild(2).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                                habilidad1[i].transform.GetChild(3).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                            }
                                            else if (mejora < actual)
                                            {
                                                habilidad1[i].transform.GetChild(2).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                                habilidad1[i].transform.GetChild(3).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                            }
                                            else
                                            {
                                                habilidad1[i].transform.GetChild(2).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f);
                                                habilidad1[i].transform.GetChild(3).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f); ;
                                            }

                                            habilidad1[i].transform.GetChild(3).GetComponent<Text>().text = "" + mejora;
                                        }
                                        else if (baseDeDatos.objetosEquipo[objetosAVender[posFlechaTienda]].aumentaVid)
                                        {
                                            aumentaVid = true;
                                            actual = baseDeDatos.equipoAliado[i].vidaModificada;
                                            mejora = baseDeDatos.equipoAliado[i].vida + baseDeDatos.objetosEquipo[objetosAVender[posFlechaTienda]].aumentoVida;

                                            if (baseDeDatos.idioma == 1)
                                            {
                                                habilidad1[i].transform.GetChild(0).GetComponent<Text>().text = "Life";
                                            }
                                            else
                                            {
                                                habilidad1[i].transform.GetChild(0).GetComponent<Text>().text = "Vida";
                                            }
                                            
                                            habilidad1[i].transform.GetChild(1).GetComponent<Text>().text = "" + actual;

                                            if (mejora > actual)
                                            {
                                                habilidad1[i].transform.GetChild(2).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                                habilidad1[i].transform.GetChild(3).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                            }
                                            else if (mejora < actual)
                                            {
                                                habilidad1[i].transform.GetChild(2).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                                habilidad1[i].transform.GetChild(3).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                            }
                                            else
                                            {
                                                habilidad1[i].transform.GetChild(2).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f);
                                                habilidad1[i].transform.GetChild(3).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f); ;
                                            }

                                            habilidad1[i].transform.GetChild(3).GetComponent<Text>().text = "" + mejora;
                                        }
                                    }

                                    if (baseDeDatos.objetosEquipo[objetosAVender[posFlechaTienda]].numeroDeMejoras > 1)
                                    {
                                        int mejora, actual;
                                        habilidad2[i].gameObject.SetActive(true);

                                        if (baseDeDatos.objetosEquipo[objetosAVender[posFlechaTienda]].aumentaAtaqueM && !aumentaAtaqueM)
                                        {
                                            aumentaAtaqueM = true;
                                            actual = baseDeDatos.equipoAliado[i].ataqueMagicoModificado;
                                            mejora = baseDeDatos.equipoAliado[i].ataqueMagico + baseDeDatos.objetosEquipo[objetosAVender[posFlechaTienda]].aumentoAtaqueMagico;

                                            if (baseDeDatos.idioma == 1)
                                            {
                                                habilidad2[i].transform.GetChild(0).GetComponent<Text>().text = "Mag. Atck.";
                                            }
                                            else
                                            {
                                                habilidad2[i].transform.GetChild(0).GetComponent<Text>().text = "Atq. Mag.";
                                            }
                                           
                                            habilidad2[i].transform.GetChild(1).GetComponent<Text>().text = "" + actual;

                                            if (mejora > actual)
                                            {
                                                habilidad2[i].transform.GetChild(2).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                                habilidad2[i].transform.GetChild(3).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                            }
                                            else if (mejora < actual)
                                            {
                                                habilidad2[i].transform.GetChild(2).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                                habilidad2[i].transform.GetChild(3).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                            }
                                            else
                                            {
                                                habilidad2[i].transform.GetChild(2).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f);
                                                habilidad2[i].transform.GetChild(3).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f); ;
                                            }

                                            habilidad2[i].transform.GetChild(3).GetComponent<Text>().text = "" + mejora;
                                        }
                                        else if (baseDeDatos.objetosEquipo[objetosAVender[posFlechaTienda]].aumentaDefensaF && !aumentaDefensaF)
                                        {
                                            aumentaDefensaF = true;
                                            actual = baseDeDatos.equipoAliado[i].defensaFisicaModificada;
                                            mejora = baseDeDatos.equipoAliado[i].defensaFisica + baseDeDatos.objetosEquipo[objetosAVender[posFlechaTienda]].aumentoDefensaFisica;

                                            if (baseDeDatos.idioma == 1)
                                            {
                                                habilidad2[i].transform.GetChild(0).GetComponent<Text>().text = "Phy. Def.";
                                            }
                                            else
                                            {
                                                habilidad2[i].transform.GetChild(0).GetComponent<Text>().text = "Def. Fis.";
                                            }
                                           
                                            habilidad2[i].transform.GetChild(1).GetComponent<Text>().text = "" + actual;

                                            if (mejora > actual)
                                            {
                                                habilidad2[i].transform.GetChild(2).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                                habilidad2[i].transform.GetChild(3).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                            }
                                            else if (mejora < actual)
                                            {
                                                habilidad2[i].transform.GetChild(2).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                                habilidad2[i].transform.GetChild(3).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                            }
                                            else
                                            {
                                                habilidad2[i].transform.GetChild(2).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f);
                                                habilidad2[i].transform.GetChild(3).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f); ;
                                            }

                                            habilidad2[i].transform.GetChild(3).GetComponent<Text>().text = "" + mejora;
                                        }
                                        else if (baseDeDatos.objetosEquipo[objetosAVender[posFlechaTienda]].aumentaDefensaM && !aumentaDefensaM)
                                        {
                                            aumentaDefensaM = true;
                                            actual = baseDeDatos.equipoAliado[i].defensaMagicaModificada;
                                            mejora = baseDeDatos.equipoAliado[i].defensaMagica + baseDeDatos.objetosEquipo[objetosAVender[posFlechaTienda]].aumentoDefensaMagica;

                                            if (baseDeDatos.idioma == 1)
                                            {
                                                habilidad2[i].transform.GetChild(0).GetComponent<Text>().text = "Mag. Def.";
                                            }
                                            else
                                            {
                                                habilidad2[i].transform.GetChild(0).GetComponent<Text>().text = "Def. Mag.";
                                            }
                                            
                                            habilidad2[i].transform.GetChild(1).GetComponent<Text>().text = "" + actual;

                                            if (mejora > actual)
                                            {
                                                habilidad2[i].transform.GetChild(2).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                                habilidad2[i].transform.GetChild(3).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                            }
                                            else if (mejora < actual)
                                            {
                                                habilidad2[i].transform.GetChild(2).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                                habilidad2[i].transform.GetChild(3).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                            }
                                            else
                                            {
                                                habilidad2[i].transform.GetChild(2).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f);
                                                habilidad2[i].transform.GetChild(3).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f); ;
                                            }

                                            habilidad2[i].transform.GetChild(3).GetComponent<Text>().text = "" + mejora;
                                        }
                                        else if (baseDeDatos.objetosEquipo[objetosAVender[posFlechaTienda]].aumentaVel && !aumentaVel)
                                        {
                                            aumentaVel = true;
                                            actual = baseDeDatos.equipoAliado[i].velocidadModificada;
                                            mejora = baseDeDatos.equipoAliado[i].velocidad + baseDeDatos.objetosEquipo[objetosAVender[posFlechaTienda]].aumentoVelocidad;

                                            if (baseDeDatos.idioma == 1)
                                            {
                                                habilidad2[i].transform.GetChild(0).GetComponent<Text>().text = "Speed";
                                            }
                                            else
                                            {
                                                habilidad2[i].transform.GetChild(0).GetComponent<Text>().text = "Vel.";
                                            }
                                            
                                            habilidad2[i].transform.GetChild(1).GetComponent<Text>().text = "" + actual;

                                            if (mejora > actual)
                                            {
                                                habilidad2[i].transform.GetChild(2).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                                habilidad2[i].transform.GetChild(3).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                            }
                                            else if (mejora < actual)
                                            {
                                                habilidad2[i].transform.GetChild(2).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                                habilidad2[i].transform.GetChild(3).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                            }
                                            else
                                            {
                                                habilidad2[i].transform.GetChild(2).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f);
                                                habilidad2[i].transform.GetChild(3).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f); ;
                                            }

                                            habilidad2[i].transform.GetChild(3).GetComponent<Text>().text = "" + mejora;
                                        }
                                        else if (baseDeDatos.objetosEquipo[objetosAVender[posFlechaTienda]].aumentaVid && !aumentaVid)
                                        {
                                            aumentaVid = true;
                                            actual = baseDeDatos.equipoAliado[i].vidaModificada;
                                            mejora = baseDeDatos.equipoAliado[i].vida + baseDeDatos.objetosEquipo[objetosAVender[posFlechaTienda]].aumentoVida;

                                            if (baseDeDatos.idioma == 1)
                                            {
                                                habilidad2[i].transform.GetChild(0).GetComponent<Text>().text = "Life";
                                            }
                                            else
                                            {
                                                habilidad2[i].transform.GetChild(0).GetComponent<Text>().text = "Vida";
                                            }
                                            
                                            habilidad2[i].transform.GetChild(1).GetComponent<Text>().text = "" + actual;

                                            if (mejora > actual)
                                            {
                                                habilidad2[i].transform.GetChild(2).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                                habilidad2[i].transform.GetChild(3).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                            }
                                            else if (mejora < actual)
                                            {
                                                habilidad2[i].transform.GetChild(2).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                                habilidad2[i].transform.GetChild(3).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                            }
                                            else
                                            {
                                                habilidad2[i].transform.GetChild(2).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f);
                                                habilidad2[i].transform.GetChild(3).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f); ;
                                            }

                                            habilidad2[i].transform.GetChild(3).GetComponent<Text>().text = "" + mejora;
                                        }
                                    }

                                    if (baseDeDatos.objetosEquipo[objetosAVender[posFlechaTienda]].numeroDeMejoras > 2)
                                    {
                                        int mejora, actual;
                                        habilidad3[i].gameObject.SetActive(true);

                                        if (baseDeDatos.objetosEquipo[objetosAVender[posFlechaTienda]].aumentaDefensaF && !aumentaDefensaF)
                                        {
                                            aumentaDefensaF = true;
                                            actual = baseDeDatos.equipoAliado[i].defensaFisicaModificada;
                                            mejora = baseDeDatos.equipoAliado[i].defensaFisica + baseDeDatos.objetosEquipo[objetosAVender[posFlechaTienda]].aumentoDefensaFisica;

                                            if (baseDeDatos.idioma == 1)
                                            {
                                                habilidad3[i].transform.GetChild(0).GetComponent<Text>().text = "Phy. Def.";
                                            }
                                            else
                                            {
                                                habilidad3[i].transform.GetChild(0).GetComponent<Text>().text = "Def. Fis.";
                                            }
                                            
                                            habilidad3[i].transform.GetChild(1).GetComponent<Text>().text = "" + actual;

                                            if (mejora > actual)
                                            {
                                                habilidad3[i].transform.GetChild(2).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                                habilidad3[i].transform.GetChild(3).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                            }
                                            else if (mejora < actual)
                                            {
                                                habilidad3[i].transform.GetChild(2).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                                habilidad3[i].transform.GetChild(3).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                            }
                                            else
                                            {
                                                habilidad3[i].transform.GetChild(2).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f);
                                                habilidad3[i].transform.GetChild(3).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f); ;
                                            }

                                            habilidad3[i].transform.GetChild(3).GetComponent<Text>().text = "" + mejora;
                                        }
                                        else if (baseDeDatos.objetosEquipo[objetosAVender[posFlechaTienda]].aumentaDefensaM && !aumentaDefensaM)
                                        {
                                            aumentaDefensaM = true;
                                            actual = baseDeDatos.equipoAliado[i].defensaMagicaModificada;
                                            mejora = baseDeDatos.equipoAliado[i].defensaMagica + baseDeDatos.objetosEquipo[objetosAVender[posFlechaTienda]].aumentoDefensaMagica;

                                            if (baseDeDatos.idioma == 1)
                                            {
                                                habilidad3[i].transform.GetChild(0).GetComponent<Text>().text = "Mag. Def.";
                                            }
                                            else
                                            {
                                                habilidad3[i].transform.GetChild(0).GetComponent<Text>().text = "Def. Mag.";
                                            }
                                            
                                            habilidad3[i].transform.GetChild(1).GetComponent<Text>().text = "" + actual;

                                            if (mejora > actual)
                                            {
                                                habilidad3[i].transform.GetChild(2).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                                habilidad3[i].transform.GetChild(3).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                            }
                                            else if (mejora < actual)
                                            {
                                                habilidad3[i].transform.GetChild(2).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                                habilidad3[i].transform.GetChild(3).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                            }
                                            else
                                            {
                                                habilidad3[i].transform.GetChild(2).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f);
                                                habilidad3[i].transform.GetChild(3).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f); ;
                                            }

                                            habilidad3[i].transform.GetChild(3).GetComponent<Text>().text = "" + mejora;
                                        }
                                        else if (baseDeDatos.objetosEquipo[objetosAVender[posFlechaTienda]].aumentaVel && !aumentaVel)
                                        {
                                            aumentaVel = true;
                                            actual = baseDeDatos.equipoAliado[i].velocidadModificada;
                                            mejora = baseDeDatos.equipoAliado[i].velocidad + baseDeDatos.objetosEquipo[objetosAVender[posFlechaTienda]].aumentoVelocidad;

                                            if (baseDeDatos.idioma == 1)
                                            {
                                                habilidad3[i].transform.GetChild(0).GetComponent<Text>().text = "Speed";
                                            }
                                            else
                                            {
                                                habilidad3[i].transform.GetChild(0).GetComponent<Text>().text = "Vel.";
                                            }
                                            
                                            habilidad3[i].transform.GetChild(1).GetComponent<Text>().text = "" + actual;

                                            if (mejora > actual)
                                            {
                                                habilidad3[i].transform.GetChild(2).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                                habilidad3[i].transform.GetChild(3).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                            }
                                            else if (mejora < actual)
                                            {
                                                habilidad3[i].transform.GetChild(2).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                                habilidad3[i].transform.GetChild(3).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                            }
                                            else
                                            {
                                                habilidad3[i].transform.GetChild(2).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f);
                                                habilidad3[i].transform.GetChild(3).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f); ;
                                            }

                                            habilidad3[i].transform.GetChild(3).GetComponent<Text>().text = "" + mejora;
                                        }
                                        else if (baseDeDatos.objetosEquipo[objetosAVender[posFlechaTienda]].aumentaVid && !aumentaVid)
                                        {
                                            aumentaVid = true;
                                            actual = baseDeDatos.equipoAliado[i].vidaModificada;
                                            mejora = baseDeDatos.equipoAliado[i].vida + baseDeDatos.objetosEquipo[objetosAVender[posFlechaTienda]].aumentoVida;

                                            if (baseDeDatos.idioma == 1)
                                            {
                                                habilidad3[i].transform.GetChild(0).GetComponent<Text>().text = "Life";
                                            }
                                            else
                                            {
                                                habilidad3[i].transform.GetChild(0).GetComponent<Text>().text = "Vida";
                                            }
                                           
                                            habilidad3[i].transform.GetChild(1).GetComponent<Text>().text = "" + actual;

                                            if (mejora > actual)
                                            {
                                                habilidad3[i].transform.GetChild(2).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                                habilidad3[i].transform.GetChild(3).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                            }
                                            else if (mejora < actual)
                                            {
                                                habilidad3[i].transform.GetChild(2).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                                habilidad3[i].transform.GetChild(3).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                            }
                                            else
                                            {
                                                habilidad3[i].transform.GetChild(2).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f);
                                                habilidad3[i].transform.GetChild(3).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f); ;
                                            }

                                            habilidad3[i].transform.GetChild(3).GetComponent<Text>().text = "" + mejora;
                                        }
                                    }

                                    if (baseDeDatos.objetosEquipo[objetosAVender[posFlechaTienda]].numeroDeMejoras > 3)
                                    {
                                        int mejora, actual;
                                        habilidad4[i].gameObject.SetActive(true);

                                        if (baseDeDatos.objetosEquipo[objetosAVender[posFlechaTienda]].aumentaDefensaM && !aumentaDefensaM)
                                        {
                                            aumentaDefensaM = true;
                                            actual = baseDeDatos.equipoAliado[i].defensaMagicaModificada;
                                            mejora = baseDeDatos.equipoAliado[i].defensaMagica + baseDeDatos.objetosEquipo[objetosAVender[posFlechaTienda]].aumentoDefensaMagica;

                                            if (baseDeDatos.idioma == 1)
                                            {
                                                habilidad4[i].transform.GetChild(0).GetComponent<Text>().text = "Mag. Def.";
                                            }
                                            else
                                            {
                                                habilidad4[i].transform.GetChild(0).GetComponent<Text>().text = "Def. Mag.";
                                            }
                                            
                                            habilidad4[i].transform.GetChild(1).GetComponent<Text>().text = "" + actual;

                                            if (mejora > actual)
                                            {
                                                habilidad4[i].transform.GetChild(2).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                                habilidad4[i].transform.GetChild(3).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                            }
                                            else if (mejora < actual)
                                            {
                                                habilidad4[i].transform.GetChild(2).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                                habilidad4[i].transform.GetChild(3).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                            }
                                            else
                                            {
                                                habilidad4[i].transform.GetChild(2).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f);
                                                habilidad4[i].transform.GetChild(3).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f); ;
                                            }

                                            habilidad4[i].transform.GetChild(3).GetComponent<Text>().text = "" + mejora;
                                        }
                                        else if (baseDeDatos.objetosEquipo[objetosAVender[posFlechaTienda]].aumentaVel && !aumentaVel)
                                        {
                                            aumentaVel = true;
                                            actual = baseDeDatos.equipoAliado[i].velocidadModificada;
                                            mejora = baseDeDatos.equipoAliado[i].velocidad + baseDeDatos.objetosEquipo[objetosAVender[posFlechaTienda]].aumentoVelocidad;

                                            if (baseDeDatos.idioma == 1)
                                            {
                                                habilidad4[i].transform.GetChild(0).GetComponent<Text>().text = "Speed";
                                            }
                                            else
                                            {
                                                habilidad4[i].transform.GetChild(0).GetComponent<Text>().text = "Vel";
                                            }
                                            
                                            habilidad4[i].transform.GetChild(1).GetComponent<Text>().text = "" + actual;

                                            if (mejora > actual)
                                            {
                                                habilidad4[i].transform.GetChild(2).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                                habilidad4[i].transform.GetChild(3).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                            }
                                            else if (mejora < actual)
                                            {
                                                habilidad4[i].transform.GetChild(2).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                                habilidad4[i].transform.GetChild(3).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                            }
                                            else
                                            {
                                                habilidad4[i].transform.GetChild(2).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f);
                                                habilidad4[i].transform.GetChild(3).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f); ;
                                            }

                                            habilidad4[i].transform.GetChild(3).GetComponent<Text>().text = "" + mejora;
                                        }
                                        else if (baseDeDatos.objetosEquipo[objetosAVender[posFlechaTienda]].aumentaVid && !aumentaVid)
                                        {
                                            aumentaVid = true;
                                            actual = baseDeDatos.equipoAliado[i].vidaModificada;
                                            mejora = baseDeDatos.equipoAliado[i].vida + baseDeDatos.objetosEquipo[objetosAVender[posFlechaTienda]].aumentoVida;

                                            if (baseDeDatos.idioma == 1)
                                            {
                                                habilidad4[i].transform.GetChild(0).GetComponent<Text>().text = "Life";
                                            }
                                            else
                                            {
                                                habilidad4[i].transform.GetChild(0).GetComponent<Text>().text = "Vida";
                                            }
                                            
                                            habilidad4[i].transform.GetChild(1).GetComponent<Text>().text = "" + actual;

                                            if (mejora > actual)
                                            {
                                                habilidad4[i].transform.GetChild(2).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                                habilidad4[i].transform.GetChild(3).GetComponent<Text>().color = new Color(41.0f / 255.0f, 160.0f / 255.0f, 245.0f / 255.0f);
                                            }
                                            else if (mejora < actual)
                                            {
                                                habilidad4[i].transform.GetChild(2).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                                habilidad4[i].transform.GetChild(3).GetComponent<Text>().color = new Color(245.0f / 255.0f, 41.0f / 255.0f, 47.0f / 255.0f);
                                            }
                                            else
                                            {
                                                habilidad4[i].transform.GetChild(2).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f);
                                                habilidad4[i].transform.GetChild(3).GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f); ;
                                            }

                                            habilidad4[i].transform.GetChild(3).GetComponent<Text>().text = "" + mejora;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    mueveFlechaTienda = false;
                }
                else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || (!pulsado && digitalY > 0))
                {
                    pulsado = true;

                    musica.ProduceEfecto(11);

                    if (opcionesActivo)
                    {
                        posFlechaOpciones--;

                        if (posFlechaOpciones < 0)
                        {
                            posFlechaOpciones = 2;
                        }

                        mueveFlechaOpciones = true;
                    }
                    else
                    {
                        posFlechaTienda--;

                        if (posFlechaTienda < 0)
                        {
                            if (!listaVentaActiva)
                            {
                                posFlechaTienda = indicesObjetos.Length - 1;
                            }
                            else
                            {
                                if (posFlechaTienda < 0)
                                {
                                    if (paginasVentas != 1)
                                    {
                                        paginaActual--;

                                        if (paginaActual < 0)
                                        {
                                            paginaActual = paginasVentas - 1;
                                        }

                                        ActualizarLista(false);

                                        posFlechaTienda = objetosMostrar - 1;
                                    }
                                }
                            }
                        }

                        mueveFlechaTienda = true;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || (!pulsado && digitalY < 0))
                {
                    pulsado = true;

                    musica.ProduceEfecto(11);

                    if (opcionesActivo)
                    {
                        posFlechaOpciones++;

                        if (posFlechaOpciones > 2)
                        {
                            posFlechaOpciones = 0;
                        }

                        mueveFlechaOpciones = true;
                    }
                    else
                    {
                        posFlechaTienda++;

                        if (!listaVentaActiva)
                        { 
                            if (posFlechaTienda > indicesObjetos.Length - 1)
                            {
                                posFlechaTienda = 0;
                            }
                        }
                        else
                        {
                            if(posFlechaTienda > (objetosMostrar-1))
                            {
                                posFlechaTienda = 0;

                                if (paginasVentas != 1)
                                {
                                    paginaActual++;

                                    if(paginaActual == paginasVentas)
                                    {
                                        paginaActual = 0;
                                    }

                                    ActualizarLista(false);
                                }
                            }
                        }
                        mueveFlechaTienda = true;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                {
                    musica.ProduceEfecto(10);
                    if (opcionesActivo)
                    {
                        if (posFlechaOpciones == 0)
                        {
                            MostrarComprar();
                        }
                        else if (posFlechaOpciones == 1)
                        {
                            MostrarVender();
                        }
                        else if (posFlechaOpciones == 2)
                        {
                            Salir();
                        }
                    }
                    else
                    {
                        if (!listaVentaActiva)
                        {
                            if (dinero >= baseDeDatos.listaObjetos[indicesObjetos[posFlechaTienda]].valorCompra)
                            {
                                Comprar(indicesObjetos[posFlechaTienda], true);
                            }
                        }
                        else
                        {
                            int aux = posFlechaTienda + (10 * paginaActual);

                            if (consumible[aux])
                            {
                                if (baseDeDatos.cantidadesObjetosConsumibles[objetosAVender[aux]] != 0)
                                {
                                    Comprar(aux, false);
                                }
                            }
                            else
                            {
                                if (baseDeDatos.cantidadesObjetosEquipo[objetosAVender[aux]] != 0)
                                {
                                    Comprar(aux, false);
                                }
                            }
                        }
                    }
                }
                else if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("B"))
                {
                    musica.ProduceEfecto(12);

                    if (opcionesActivo)
                    {
                        Salir();
                    }
                    else
                    {
                        for (int i = 0; i < baseDeDatos.numeroIntegrantesEquipo; i++)
                        {
                            habilidad1[i].gameObject.SetActive(false);
                            habilidad2[i].gameObject.SetActive(false);
                            habilidad3[i].gameObject.SetActive(false);
                            habilidad4[i].gameObject.SetActive(false);
                        }

                        MostrarOpciones();
                    }
                }
            }
            else if (comprarActivo)
            {
                if (!confirmacionActiva)
                {
                    if (cambiaCantidad)
                    {
                        cantidadCompra.transform.GetChild(1).GetComponent<Text>().text = "" + cantidad;
                        cantidadCompra.transform.GetChild(3).GetComponent<Text>().text = "" + precio;

                        cambiaCantidad = false;
                    }
                    else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || (!pulsado && digitalY < 0))
                    {
                        pulsado = true;
                        musica.ProduceEfecto(11);
                        cantidad--;
                        
                        if (baseDeDatos.listaObjetos[indiceObjetoAComprar].tipo == Objeto.tipoObjeto.APRENDE_ATAQUE)
                        {
                            cantidad = 1;
                        }
                        else if (cantidad < 1)
                        {
                            cantidad = (int)(dinero / baseDeDatos.listaObjetos[indiceObjetoAComprar].valorCompra);
                        }

                        precio = cantidad * baseDeDatos.listaObjetos[indiceObjetoAComprar].valorCompra;

                        cambiaCantidad = true;
                    }
                    else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || (!pulsado && digitalY > 0))
                    {
                        pulsado = true;
                        musica.ProduceEfecto(11);
                        cantidad++;

                        if (baseDeDatos.listaObjetos[indiceObjetoAComprar].tipo == Objeto.tipoObjeto.APRENDE_ATAQUE)
                        {
                            cantidad = 1;
                        }
                        else if ((cantidad * baseDeDatos.listaObjetos[indiceObjetoAComprar].valorCompra) > dinero)
                        {
                            cantidad--;
                        }

                        precio = cantidad * baseDeDatos.listaObjetos[indiceObjetoAComprar].valorCompra;

                        cambiaCantidad = true;
                    }
                    else if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                    {
                        if(baseDeDatos.listaObjetos[indiceObjetoAComprar].tipo == Objeto.tipoObjeto.APRENDE_ATAQUE && baseDeDatos.ataquesDesbloqueados[baseDeDatos.listaObjetos[indiceObjetoAComprar].indiceAtq])
                        {
                            musica.ProduceEfecto(12);
                        }
                        else
                        {
                            musica.ProduceEfecto(10);
                            Confirmar(true);
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("B"))
                    {
                        musica.ProduceEfecto(12);
                        comprarActivo = false;

                        cantidadCompra.gameObject.SetActive(false);
                    }
                }
                else
                {
                    if (mueveConfirmacion)
                    {
                        if(posFlechaConfirmacion == 0)
                        {
                            ventanaConfirmacion.transform.GetChild(7).transform.position = ventanaConfirmacion.transform.GetChild(8).transform.position;
                        }
                        else
                        {
                            ventanaConfirmacion.transform.GetChild(7).transform.position = ventanaConfirmacion.transform.GetChild(9).transform.position;
                        }

                        mueveConfirmacion = false;
                    }
                    else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || (!pulsado && digitalY < 0))
                    {
                        pulsado = true;
                        musica.ProduceEfecto(11);
                        posFlechaConfirmacion++;

                        if(posFlechaConfirmacion > 1)
                        {
                            posFlechaConfirmacion = 0;
                        }

                        mueveConfirmacion = true;
                    }
                    else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || (!pulsado && digitalY > 0))
                    {
                        pulsado = true;
                        musica.ProduceEfecto(11);
                        posFlechaConfirmacion--;

                        if (posFlechaConfirmacion < 0)
                        {
                            posFlechaConfirmacion = 1;
                        }

                        mueveConfirmacion = true;
                    }
                    else if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                    {
                        musica.ProduceEfecto(10);
                        if (posFlechaConfirmacion == 0)
                        {
                            HacerCompra();
                        }
                        else
                        {
                            CancelarConfirmacion();
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("B"))
                    {
                        musica.ProduceEfecto(12);
                        CancelarConfirmacion();
                    }
                }
            }
            else if (ventaActiva)
            {
                if (!confirmacionActiva)
                {
                    if (cambiaCantidad)
                    {
                        cantidadCompra.transform.GetChild(1).GetComponent<Text>().text = "" + cantidad;
                        cantidadCompra.transform.GetChild(3).GetComponent<Text>().text = "" + precio;

                        cambiaCantidad = false;
                    }
                    else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || (!pulsado && digitalY < 0))
                    {
                        pulsado = true;

                        musica.ProduceEfecto(11);
                        cantidad--;

                        int aux = posFlechaTienda + (paginaActual * 10);

                        if (cantidad < 1)
                        {
                            if (consumible[aux])
                            {
                                cantidad = baseDeDatos.cantidadesObjetosConsumibles[objetosAVender[aux]];
                                precio = cantidad * baseDeDatos.objetosConsumibles[objetosAVender[aux]].valorVenta;
                            }
                            else
                            {
                                cantidad = baseDeDatos.cantidadesObjetosEquipo[objetosAVender[aux]];
                                precio = cantidad * baseDeDatos.objetosEquipo[objetosAVender[aux]].valorVenta;
                            }
                        }


                        cambiaCantidad = true;
                    }
                    else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || (!pulsado && digitalY > 0))
                    {
                        pulsado = true;

                        musica.ProduceEfecto(11);

                        int aux = posFlechaTienda + (paginaActual * 10);

                        cantidad++;

                        if (consumible[aux])
                        {
                            if (baseDeDatos.cantidadesObjetosConsumibles[objetosAVender[aux]] < cantidad)
                            {
                                cantidad = 1;
                            }

                            precio = cantidad * baseDeDatos.objetosConsumibles[objetosAVender[aux]].valorVenta;
                        }
                        else
                        {
                            if (baseDeDatos.cantidadesObjetosEquipo[objetosAVender[aux]] < cantidad)
                            {
                                cantidad = 1;
                            }

                            precio = cantidad * baseDeDatos.objetosEquipo[objetosAVender[aux]].valorVenta;
                        }

                        cambiaCantidad = true;
                    }
                    else if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                    {
                        musica.ProduceEfecto(10);
                        Confirmar(false);
                    }
                    else if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("B"))
                    {
                        musica.ProduceEfecto(12);
                        ventaActiva = false;

                        cantidadCompra.gameObject.SetActive(false);
                    }
                }
                else
                {
                    if (mueveConfirmacion)
                    {
                        if (posFlechaConfirmacion == 0)
                        {
                            ventanaConfirmacion.transform.GetChild(7).transform.position = ventanaConfirmacion.transform.GetChild(8).transform.position;
                        }
                        else
                        {
                            ventanaConfirmacion.transform.GetChild(7).transform.position = ventanaConfirmacion.transform.GetChild(9).transform.position;
                        }

                        mueveConfirmacion = false;
                    }
                    else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || (!pulsado && digitalY < 0))
                    {
                        pulsado = true;

                        musica.ProduceEfecto(11);
                        posFlechaConfirmacion++;

                        if (posFlechaConfirmacion > 1)
                        {
                            posFlechaConfirmacion = 0;
                        }

                        mueveConfirmacion = true;
                    }
                    else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || (!pulsado && digitalY > 0))
                    {
                        pulsado = true;
                        musica.ProduceEfecto(11);
                        posFlechaConfirmacion--;

                        if (posFlechaConfirmacion < 0)
                        {
                            posFlechaConfirmacion = 1;
                        }

                        mueveConfirmacion = true;
                    }
                    else if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                    {
                        musica.ProduceEfecto(10);
                        if (posFlechaConfirmacion == 0)
                        {
                            HacerVenta();
                        }
                        else
                        {
                            CancelarConfirmacion();
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("B"))
                    {
                        musica.ProduceEfecto(12);
                        CancelarConfirmacion();
                    }
                }
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



    void AbrirTienda()
    {
        if (!abierto)
        {
            if (baseDeDatos.mandoActivo)
            {
                menuTienda.transform.GetChild(5).GetChild(4).GetComponent<Image>().sprite = baseDeDatos.seleccionXBOX[0];
                menuTienda.transform.GetChild(5).GetChild(6).GetComponent<Image>().sprite = baseDeDatos.volverXBOX[0];
                menuTienda.transform.GetChild(5).GetChild(8).GetComponent<Image>().sprite = baseDeDatos.moverXBOX[0];
            }
            else
            {
                menuTienda.transform.GetChild(5).GetChild(4).GetComponent<Image>().sprite = baseDeDatos.seleccionPC[0];
                menuTienda.transform.GetChild(5).GetChild(6).GetComponent<Image>().sprite = baseDeDatos.volverPC[0];
                menuTienda.transform.GetChild(5).GetChild(8).GetComponent<Image>().sprite = baseDeDatos.moverPC[0];
            }

            if(baseDeDatos.idioma == 1)
            {
                menuTienda.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "Shop";

                menuTienda.transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<Text>().text = "Buy";
                menuTienda.transform.GetChild(3).GetChild(1).GetChild(0).GetComponent<Text>().text = "Sell";
                menuTienda.transform.GetChild(3).GetChild(2).GetChild(0).GetComponent<Text>().text = "Exit";

                menuTienda.transform.GetChild(5).GetChild(3).GetComponent<Text>().text = "Select";
                menuTienda.transform.GetChild(5).GetChild(5).GetComponent<Text>().text = "Back";
                menuTienda.transform.GetChild(5).GetChild(7).GetComponent<Text>().text = "Move";

                cantidadCompra.transform.GetChild(0).GetComponent<Text>().text = "Amount:";
                cantidadCompra.transform.GetChild(2).GetComponent<Text>().text = "Price:";
            }
            else
            {
                menuTienda.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "Tienda";

                cantidadCompra.transform.GetChild(0).GetComponent<Text>().text = "Cantidad:";
                cantidadCompra.transform.GetChild(2).GetComponent<Text>().text = "Precio:";

                menuTienda.transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<Text>().text = "Comprar";
                menuTienda.transform.GetChild(3).GetChild(1).GetChild(0).GetComponent<Text>().text = "Vender";
                menuTienda.transform.GetChild(3).GetChild(2).GetChild(0).GetComponent<Text>().text = "Salir";

                menuTienda.transform.GetChild(5).GetChild(3).GetComponent<Text>().text = "Seleccionar";
                menuTienda.transform.GetChild(5).GetChild(5).GetComponent<Text>().text = "Volver";
                menuTienda.transform.GetChild(5).GetChild(7).GetComponent<Text>().text = "Mover";
                //menuTienda.transform.GetChild(2).GetChild(1).GetChild(0).GetComponent<Text>().text = "";
            }

            musica.ProduceEfecto(15);
            controlJugador.setMensajeActivo(true);
            controlJugador.SetInterrogante(false);
            menuTienda.gameObject.SetActive(true);

            for (int i = 0; i < 3; i++)
            {
                habilidad1[i].SetActive(false);
                habilidad2[i].SetActive(false);
                habilidad3[i].SetActive(false);
                habilidad4[i].SetActive(false);
            }

            for (int i = 0; i < fichasPersonajes.Length; i++)
            {
                if (i < baseDeDatos.numeroIntegrantesEquipo)
                {
                    fichasPersonajes[i].gameObject.SetActive(true);

                    if(baseDeDatos.idioma == 1)
                    {
                        fichasPersonajes[i].transform.GetChild(0).GetComponent<Text>().text = baseDeDatos.equipoAliado[i].nombreIngles;
                    }
                    else
                    {
                        fichasPersonajes[i].transform.GetChild(0).GetComponent<Text>().text = baseDeDatos.equipoAliado[i].nombre;
                    }
                }
                else
                {
                    fichasPersonajes[i].gameObject.SetActive(false);
                }
            }

            dinero = controlJugador.dinero;
            mostrarDinero.text = "" + dinero;
            MostrarOpciones();
            abierto = true;
        }
    }



    void MostrarOpciones()
    {
        posFlechaOpciones = 0;
        mueveFlechaOpciones = true;
        opciones.SetActive(true);
        cuadroTienda.gameObject.SetActive(false);
        opcionesActivo = true;
        listaVentaActiva = false;
        flechaAbajo.gameObject.SetActive(false);
        flechaArriba.gameObject.SetActive(false);
    }



    void MostrarComprar()
    {
        posFlechaTienda = 0;
        mueveFlechaTienda = true;
        opciones.SetActive(false);
        cuadroTienda.gameObject.SetActive(true);
        opcionesActivo = false;

        ActualizarLista(true);
    }



    void MostrarVender()
    {
        paginaActual = 0;
        posFlechaTienda = 0;
        mueveFlechaTienda = true;
        opciones.SetActive(false);
        cuadroTienda.gameObject.SetActive(true);
        listaVentaActiva = true;
        iniciado = false;

        numeroObjetosVender = baseDeDatos.numeroObjetosConsumibles + baseDeDatos.numeroObjetosEquipo;

        objetosAVender = new int[numeroObjetosVender];
        consumible = new bool[numeroObjetosVender];

        if (baseDeDatos.numeroObjetosConsumibles != 0)
        {
            for (int i = 0; i < baseDeDatos.numeroObjetosConsumibles; i++)
            {
                objetosAVender[i] = i;
                consumible[i] = true;
            }
        }
           

        if(baseDeDatos.numeroObjetosEquipo != 0)
        {
            for (int i = baseDeDatos.numeroObjetosConsumibles; i < numeroObjetosVender; i++)
            {
                objetosAVender[i] = i - baseDeDatos.numeroObjetosConsumibles;
                consumible[i] = false;
            }
        }

        ActualizarLista(false);

        if (paginasVentas > 1)
        {
            flechaAbajo.gameObject.SetActive(true);
        }

        opcionesActivo = false;
    }



    void Salir()
    {
        abierto = false;
        controlJugador.setMensajeActivo(false);
        menuTienda.gameObject.SetActive(false);
        opcionesActivo = false;
    }



    void Comprar(int indice, bool compra)
    {
        indiceObjetoAComprar = indice;

        if (compra)
        {
            comprarActivo = true;

            cantidad = 1;
            precio = baseDeDatos.listaObjetos[indice].valorCompra;
        }
        else
        {
            ventaActiva = true;
            
            cantidad = 0;
            if (consumible[indice])
            {
                precio = cantidad * baseDeDatos.objetosConsumibles[objetosAVender[indice]].valorVenta;
            }
            else
            {
                precio = cantidad * baseDeDatos.objetosEquipo[objetosAVender[indice]].valorVenta;
            }
        }

        cantidadCompra.gameObject.SetActive(true);

        cantidadCompra.transform.GetChild(1).GetComponent<Text>().text = "" + cantidad;
        cantidadCompra.transform.GetChild(3).GetComponent<Text>().text = "" + precio;
    }



    void Confirmar(bool comprar)
    {
        confirmacionActiva = true;
        posFlechaConfirmacion = 1;

        ventanaConfirmacion.gameObject.SetActive(true);

        if(baseDeDatos.idioma == 1)
        {
            ventanaConfirmacion.transform.GetChild(0).GetComponent<Text>().text = baseDeDatos.listaObjetos[indiceObjetoAComprar].nombreIngles;
        }
        else
        {
            ventanaConfirmacion.transform.GetChild(0).GetComponent<Text>().text = baseDeDatos.listaObjetos[indiceObjetoAComprar].nombre;
        }

        if (baseDeDatos.idioma == 1)
        {
            ventanaConfirmacion.transform.GetChild(2).GetComponent<Text>().text = "Amount: " + cantidad;
            ventanaConfirmacion.transform.GetChild(3).GetComponent<Text>().text = "Total Price: " + precio;

            if (comprar)
            {
                ventanaConfirmacion.transform.GetChild(4).GetComponent<Text>().text = "Are you sure you want to buy?";
            }
            else
            {
                ventanaConfirmacion.transform.GetChild(4).GetComponent<Text>().text = "Are you sure you want to sell?";
            }
        }
        else
        {
            ventanaConfirmacion.transform.GetChild(2).GetComponent<Text>().text = "Cantidad: " + cantidad;
            ventanaConfirmacion.transform.GetChild(3).GetComponent<Text>().text = "Precio Total: " + precio;

            if (comprar)
            {
                ventanaConfirmacion.transform.GetChild(4).GetComponent<Text>().text = "¿Seguro quieres comprar?";
            }
            else
            {
                ventanaConfirmacion.transform.GetChild(4).GetComponent<Text>().text = "¿Seguro quieres vender?";
            }
        }

        ventanaConfirmacion.transform.GetChild(7).transform.position = ventanaConfirmacion.transform.GetChild(9).transform.position;
    }



    void CancelarConfirmacion()
    {
        confirmacionActiva = false;
        comprarActivo = false;
        ventaActiva = false;

        cantidadCompra.gameObject.SetActive(false);
        ventanaConfirmacion.gameObject.SetActive(false);
    }



    void HacerCompra()
    {
        dinero -= precio;
        controlJugador.dinero = dinero;

        baseDeDatos.IncluirEnInventario(indiceObjetoAComprar, cantidad);

        CancelarConfirmacion();
        mostrarDinero.text = "" + dinero;

        ActualizarLista(true);
    }



    void HacerVenta()
    {
        dinero += precio;
        controlJugador.dinero = dinero;

        if (consumible[indiceObjetoAComprar])
        {
            baseDeDatos.QuitarDeInventario(indiceObjetoAComprar, cantidad, 0);
        }
        else
        {
            int aux = indiceObjetoAComprar - baseDeDatos.numeroObjetosConsumibles;
            baseDeDatos.QuitarDeInventario(aux, cantidad, 1);
        }

        CancelarConfirmacion();
        mostrarDinero.text = "" + dinero;

        ActualizarLista(false);
    }



    void ActualizarLista(bool compra)
    {
        if (compra)
        {
            flechaArriba.gameObject.SetActive(false);
            flechaAbajo.gameObject.SetActive(false);

            flechaTienda.gameObject.SetActive(true);
            descripcion.gameObject.SetActive(true);

            for (int i = 0; i < listaPosicionesObjetos.Length; i++)
            {
                listaPosicionesObjetos[i].gameObject.SetActive(true);

                if (i < indicesObjetos.Length)
                {
                    listaPosicionesObjetos[i].transform.GetChild(0).GetComponent<Image>().sprite = baseDeDatos.listaObjetos[indicesObjetos[i]].icono;

                    if(baseDeDatos.idioma == 1)
                    {
                        listaPosicionesObjetos[i].transform.GetChild(1).GetComponent<Text>().text = baseDeDatos.listaObjetos[indicesObjetos[i]].nombreIngles;
                    }
                    else
                    {
                        listaPosicionesObjetos[i].transform.GetChild(1).GetComponent<Text>().text = baseDeDatos.listaObjetos[indicesObjetos[i]].nombre;
                    }

                    int cantidadEnBolsa = 0;

                    if (baseDeDatos.listaObjetos[indicesObjetos[i]].tipo == Objeto.tipoObjeto.EQUIPO)
                    {
                        bool colocado = false;

                        for(int j = 0; j < baseDeDatos.numeroObjetosEquipo; j++)
                        {
                            if (!colocado)
                            {
                                if (baseDeDatos.objetosEquipo[j].indice == indicesObjetos[i])
                                {
                                    cantidadEnBolsa = baseDeDatos.cantidadesObjetosEquipo[j];
                                    colocado = true;
                                }
                            }
                        }

                        if (!colocado)
                        {
                            cantidadEnBolsa = 0;
                        }
                    }
                    else if (baseDeDatos.listaObjetos[indicesObjetos[i]].tipo == Objeto.tipoObjeto.APRENDE_ATAQUE)
                    {
                        bool colocado = false;

                        for (int j = 0; j < baseDeDatos.numeroObjetosAtaques; j++)
                        {
                            if (!colocado)
                            {
                                if (baseDeDatos.objetosAtaques[j].indice == indicesObjetos[i])
                                {
                                    cantidadEnBolsa = 1;
                                    colocado = true;
                                }
                            }
                        }

                        if (!colocado)
                        {
                            cantidadEnBolsa = 0;
                        }
                    }
                    else
                    {
                        bool colocado = false;

                        for (int j = 0; j < baseDeDatos.numeroObjetosConsumibles; j++)
                        {
                            if (!colocado)
                            {
                                if (baseDeDatos.objetosConsumibles[j].indice == indicesObjetos[i])
                                {
                                    cantidadEnBolsa = baseDeDatos.cantidadesObjetosConsumibles[j];
                                    colocado = true;
                                }
                            }
                        }

                        if (!colocado)
                        {
                            cantidadEnBolsa = 0;
                        }
                    }

                    listaPosicionesObjetos[i].transform.GetChild(2).GetComponent<Text>().text = "" + cantidadEnBolsa;
                    listaPosicionesObjetos[i].transform.GetChild(3).GetComponent<Text>().text = "" + baseDeDatos.listaObjetos[indicesObjetos[i]].valorCompra;
                }
            }

            for (int i = indicesObjetos.Length; i < 10; i++)
            {
                listaPosicionesObjetos[i].gameObject.SetActive(false);
            }
        }
        else
        {
            flechaTienda.gameObject.SetActive(true);
            descripcion.gameObject.SetActive(true);

            for (int i = 0; i < 10; i++)
            {
                listaPosicionesObjetos[i].gameObject.SetActive(true);
            }

            flechaTienda.gameObject.SetActive(true);
            marcadorOpcion.gameObject.SetActive(true);
            descripcion.gameObject.SetActive(true);

            if (!iniciado)
            {
                iniciado = true;
                paginasVentas = (int)(numeroObjetosVender / 10);
                paginasVentas++;
            }

            PasarPaginaVenta();
        }
    }



    void PasarPaginaVenta()
    {
        int indiceActual = paginaActual * 10;
        mueveFlechaTienda = true;
        posFlechaTienda = 0;
        objetosMostrar = numeroObjetosVender - indiceActual;

        if (objetosMostrar >= 10)
        {
            objetosMostrar = 10;
        }

        if (paginaActual == 0)
        {
            if(paginasVentas != 1)
            {
                flechaArriba.gameObject.SetActive(false);
                flechaAbajo.gameObject.SetActive(true);
            }
            else
            {
                flechaArriba.gameObject.SetActive(false);
                flechaAbajo.gameObject.SetActive(false);
            }
        }
        else
        {
            if (paginaActual < (paginasVentas - 1))
            {
                flechaArriba.gameObject.SetActive(true);
                flechaAbajo.gameObject.SetActive(true);
            }
            else
            {
                flechaArriba.gameObject.SetActive(true);
                flechaAbajo.gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < objetosMostrar; i++)
        {
            int cantidadEnBolsa = 0;
            int j = indiceActual + i;
            

            if (consumible[j])
            {
                listaPosicionesObjetos[i].transform.GetChild(0).GetComponent<Image>().sprite = baseDeDatos.objetosConsumibles[objetosAVender[j]].icono;

                if(baseDeDatos.idioma == 1)
                {
                    listaPosicionesObjetos[i].transform.GetChild(1).GetComponent<Text>().text = baseDeDatos.objetosConsumibles[objetosAVender[j]].nombreIngles;
                }
                else
                {
                    listaPosicionesObjetos[i].transform.GetChild(1).GetComponent<Text>().text = baseDeDatos.objetosConsumibles[objetosAVender[j]].nombre;
                }

                cantidadEnBolsa = baseDeDatos.cantidadesObjetosConsumibles[objetosAVender[j]];
                listaPosicionesObjetos[i].transform.GetChild(3).GetComponent<Text>().text = "" + baseDeDatos.objetosConsumibles[objetosAVender[j]].valorVenta;
            }
            else
            {
                listaPosicionesObjetos[i].transform.GetChild(0).GetComponent<Image>().sprite = baseDeDatos.objetosEquipo[objetosAVender[j]].icono;

                if (baseDeDatos.idioma == 1)
                {
                    listaPosicionesObjetos[i].transform.GetChild(1).GetComponent<Text>().text = baseDeDatos.objetosEquipo[objetosAVender[j]].nombreIngles;
                }
                else
                {
                    listaPosicionesObjetos[i].transform.GetChild(1).GetComponent<Text>().text = baseDeDatos.objetosEquipo[objetosAVender[j]].nombre;
                }
               
                listaPosicionesObjetos[i].transform.GetChild(3).GetComponent<Text>().text = "" + baseDeDatos.objetosEquipo[objetosAVender[j]].valorVenta;
                cantidadEnBolsa = baseDeDatos.cantidadesObjetosEquipo[objetosAVender[j]];
            }

            listaPosicionesObjetos[i].transform.GetChild(2).GetComponent<Text>().text = "" + cantidadEnBolsa;
        }

        if (objetosMostrar < 10)
        {
            for (int i = objetosMostrar; i < 10; i++)
            {
                listaPosicionesObjetos[i].gameObject.SetActive(false);
            }
        }
    }



    void CambiaControl()
    {
        if (!baseDeDatos.mandoActivo)
        {
            baseDeDatos.mandoActivo = true;

            menuTienda.transform.GetChild(5).GetChild(4).GetComponent<Image>().sprite = baseDeDatos.seleccionXBOX[0];
            menuTienda.transform.GetChild(5).GetChild(6).GetComponent<Image>().sprite = baseDeDatos.volverXBOX[0];
            menuTienda.transform.GetChild(5).GetChild(8).GetComponent<Image>().sprite = baseDeDatos.moverXBOX[0];
        }
        else
        {
            baseDeDatos.mandoActivo = false;

            menuTienda.transform.GetChild(5).GetChild(4).GetComponent<Image>().sprite = baseDeDatos.seleccionPC[0];
            menuTienda.transform.GetChild(5).GetChild(6).GetComponent<Image>().sprite = baseDeDatos.volverPC[0];
            menuTienda.transform.GetChild(5).GetChild(8).GetComponent<Image>().sprite = baseDeDatos.moverPC[0];
        }
    }
}
