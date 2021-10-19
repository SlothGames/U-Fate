using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class BaseDatos : MonoBehaviour
{
    public string nombreProta;
    public Personajes[] listaPersonajesAliados;
    public Equipamiento[] equipamientoPersonajesAliados;
    public Personajes[] listaPersonajesEnemigos;
    public Personajes[] equipoAliado;
    public int numeroIntegrantesEquipo;
    public int[] personajesAlmacenados;
    public int numeroAlmacenado;

    public int idioma; // 0 --> español   1 --> inglés

    public Ataque[] listaAtaques;

    public Sprite[] iconosObjetos;
    /*
        CABEZA,
        CUERPO,
        ESCUDO,
        COMPLEMENTO,
        BOTAS,
        ARMA,
        CONSUMIBLE
    */

    //Inventario
    public Objeto[] listaObjetos;
    public int numeroObjetosTotales;
    public Objeto[] objetosConsumibles;
    public int[] cantidadesObjetosConsumibles;
    public int numeroObjetosConsumibles;
    public Objeto[] objetosAtaques;
    public int[] cantidadesObjetosAtaques;
    public int numeroObjetosAtaques;
    public Objeto[] objetosEquipo;
    public int[] cantidadesObjetosEquipo;
    public int numeroObjetosEquipo;
    public Objeto[] listaObjetosClave;
    public int[] cantidadObjetosClave;
    public int numeroObjetosClave;

    public Objeto[] cascos;
    public int[] cantidadCascos;
    public int numeroCascos;
    public Objeto[] armaduras;
    public int[] cantidadArmaduras;
    public int numeroArmaduras;
    public Objeto[] escudos;
    public int[] cantidadEscudos;
    public int numeroEscudos;
    public Objeto[] complemento;
    public int[] cantidadComplemento;
    public int numeroComplemento;
    public Objeto[] botas;
    public int[] cantidadBotas;
    public int numeroBotas;
    public Objeto[] armas;
    public int[] cantidadArmas;
    public int numeroArmas;
    public bool[] ataquesDesbloqueados;

    //Misiones
    public Mision[] listaMisiones;
    public Mision[] listaMisionesActivas;
    public int numeroMisionesActivas;
    public Mision[] listaMisionesPrincipales;
    public int numeroMisionesPrincipales;
    public Mision[] listaMisionesSecundarias;
    public int numeroMisionesSecundarias;
    public Mision[] listaMisionesReclutamiento;
    public int numeroMisionesReclutamiento;

    public bool iniciado;
    public bool[] cofres;
    public bool mandoActivo;

    //Pociones
    public bool[] recetasPocionesDesbloqueadas = new bool[10];
    public string[] nombrePociones = new string[10];
    public string[] nombrePocionesIngles = new string[10];
    public bool puedeHacerPociones;
    public int[] puntuacionRecordPociones = new int[10];

    //Esgrima
    public bool[] retosEsgrimaDesbloqueados = new bool[10];
    public string[] nombreRetosEsgrima = new string[10];
    public string[] nombreRetosEsgrimaIngles = new string[10];
    public bool puedeHacerEsgrima;
    public int[] puntuacionRecordEsgrima = new int[10];

    ControlJugador controlJugador;
    ControlObjetos controlSecundario;
   
    //Botones mando
    public Sprite[] seleccionPC;
    public Sprite[] moverPC;
    public Sprite[] volverPC;
    public Sprite[] seleccionXBOX;
    public Sprite[] moverXBOX;
    public Sprite[] volverXBOX;
    public Sprite[] startPC;
    public Sprite[] startXBOX;
    public Sprite[] xXBOX;
    public Sprite[] yXBOX;
    public Sprite[] arribaXBOX;
    public Sprite[] abajoXBOX;
    public Sprite[] izquierdaXBOX;
    public Sprite[] derechaXBOX;
    public Sprite[] arribaPC;
    public Sprite[] abajoPC;
    public Sprite[] izquierdaPC;
    public Sprite[] derechaPC;
    public Sprite[] vPC;
    public Sprite[] bPC;

    //Mapa
    public int indiceObjetivo;
    public int indiceInicial;
    public bool[] zonaVisitada;

    //Jugador
    public int faccion; //0 -- golpista 1 -- imperio 2 -- regente 3 -- Resistencia 4 -- R.Asalto 5 -- R.Especiales 6 -- R.Investigacion 7 -- Cupula 8 -- Nada 9 -- Anarquista

    //Tiempo Juego;
    public float segundos;
    public float minutos;
    public float horas;

    //Partidas Guardadas
    public bool[] partidaDisponible;
    public Sprite[] interfazPG;
    public Sprite[] prota;
    public Sprite[] banderas;

    //Posicion Camara
    public int areaCamara;
    public int zonaCamara;

    //Teleport
    public bool teleportActivo;

    //Mapas
    public MapaActivo controlMapas;

    //Personajes desactivados
    ///////////////////////////////
    ///0 - Ladron
    ///1 - Gamez
    ///2 - Pedro
    ///3 - Orco - Nani
    ///4 - Cupula - Nani
    ///5 - Nani
    ///////////////////////////////
    public bool[] personajesDesactivados;



    void Awake()
    {
        idioma = -1;
        personajesDesactivados = new bool[20];
        partidaDisponible = new bool[3];

        string path;

        for (int i = 0; i < 3; i++)
        {
            if (i == 0)
            {
                path = Application.persistentDataPath + "/save.pe";
            }
            else if (i == 1)
            {
                path = Application.persistentDataPath + "/save1.pe";
            }
            else
            {
                path = Application.persistentDataPath + "/save2.pe";
            }

            if (File.Exists(path))
            {
                partidaDisponible[i] = true;
            }
            else
            {
                partidaDisponible[i] = false;
            }
        }

        faccion = 8;

        //Botones
        moverPC = Resources.LoadAll<Sprite>("Sprites/Interfaz/botones/PC/4direcciones");
        seleccionPC = Resources.LoadAll<Sprite>("Sprites/Interfaz/botones/PC/N");
        moverXBOX = Resources.LoadAll<Sprite>("Sprites/Interfaz/botones/XBOX/Mover");
        seleccionXBOX = Resources.LoadAll<Sprite>("Sprites/Interfaz/botones/XBOX/A");
        volverPC = Resources.LoadAll<Sprite>("Sprites/Interfaz/botones/PC/M");
        volverXBOX = Resources.LoadAll<Sprite>("Sprites/Interfaz/botones/XBOX/B");
        startPC = Resources.LoadAll<Sprite>("Sprites/Interfaz/botones/PC/Esc");
        startXBOX = Resources.LoadAll<Sprite>("Sprites/Interfaz/botones/XBOX/Start");
        xXBOX = Resources.LoadAll<Sprite>("Sprites/Interfaz/botones/XBOX/X");
        yXBOX = Resources.LoadAll<Sprite>("Sprites/Interfaz/botones/XBOX/Y");
        vPC = Resources.LoadAll<Sprite>("Sprites/Interfaz/botones/PC/V");
        bPC = Resources.LoadAll<Sprite>("Sprites/Interfaz/botones/PC/B");
        arribaPC = Resources.LoadAll<Sprite>("Sprites/Interfaz/botones/PC/Arriba");
        izquierdaPC = Resources.LoadAll<Sprite>("Sprites/Interfaz/botones/PC/Izquierda");
        derechaPC = Resources.LoadAll<Sprite>("Sprites/Interfaz/botones/PC/Derecha");
        abajoPC = Resources.LoadAll<Sprite>("Sprites/Interfaz/botones/PC/Abajo");
        abajoXBOX = Resources.LoadAll<Sprite>("Sprites/Interfaz/botones/XBOX/Abajo");
        arribaXBOX = Resources.LoadAll<Sprite>("Sprites/Interfaz/botones/XBOX/Arriba");
        izquierdaXBOX = Resources.LoadAll<Sprite>("Sprites/Interfaz/botones/XBOX/Izquierda");
        derechaXBOX = Resources.LoadAll<Sprite>("Sprites/Interfaz/botones/XBOX/Derecha");

        controlJugador = GameObject.Find("Player").GetComponent<ControlJugador>();
        controlSecundario = GameObject.Find("GameManager").GetComponent<ControlObjetos>();
        controlMapas = GameObject.Find("GameManager").GetComponent<MapaActivo>();

        idioma = 0;
        numeroMisionesActivas = 0;
        iniciado = true;

        //Cofres
        cofres = new bool[50];

        for(int i = 0; i < cofres.Length; i++)
        {
            cofres[i] = false;
        }

        //Inicia Ataques
        listaAtaques = new Ataque[72];
        for (int i = 0; i < listaAtaques.Length; i++)
        {
            //listaAtaques[i].IniciarAtaque(i);
            Ataque aux = new Ataque();
            listaAtaques[i] = aux.BuscaAtaque(i);
        }


        //Inicializar Personajes
        equipoAliado = new Personajes[3];
        listaPersonajesAliados = new Personajes[21];
        listaPersonajesEnemigos = new Personajes[52];
        personajesAlmacenados = new int[30];
        numeroAlmacenado = 0;

        for (int i = 0; i < listaPersonajesAliados.Length; i++)
        {
            Personajes aux = new Personajes();
            listaPersonajesAliados[i] = aux.IniciarPersonaje(i, true);
        }

        equipoAliado[0] = listaPersonajesAliados[0];
        numeroIntegrantesEquipo = 1;


        for (int i = 0; i < listaPersonajesEnemigos.Length; i++)
        {
            Personajes aux = new Personajes();
            listaPersonajesEnemigos[i] = aux.IniciarPersonaje(i, false);
        }


        //Inicializar Objetos
        listaObjetos = new Objeto[120];
        equipamientoPersonajesAliados = new Equipamiento[listaPersonajesAliados.Length];
        objetosConsumibles = new Objeto[20]; //cambiar en funcion de la cantidad de objetos totales que haya
        cantidadesObjetosConsumibles = new int[objetosConsumibles.Length];
        objetosAtaques = new Objeto[72];
        cantidadesObjetosAtaques = new int[objetosAtaques.Length];
        objetosEquipo = new Objeto[20];
        cantidadesObjetosEquipo = new int[objetosEquipo.Length];
        listaObjetosClave = new Objeto[20];
        cantidadObjetosClave = new int[listaObjetosClave.Length];

        cascos = new Objeto[10];
        armaduras = new Objeto[10];
        botas = new Objeto[10];
        armas = new Objeto[10];
        escudos = new Objeto[10];
        complemento = new Objeto[10];

        cantidadCascos = new int[10];
        cantidadArmaduras = new int[10];
        cantidadBotas = new int[10];
        cantidadArmas = new int[10];
        cantidadEscudos = new int[10];
        cantidadComplemento = new int[10];

        numeroObjetosConsumibles = numeroObjetosAtaques = numeroObjetosEquipo = numeroCascos = numeroArmaduras = numeroBotas = numeroArmas = numeroEscudos = numeroComplemento = 0;

        for (int i = 0; i < 20; i++)
        {
            cantidadesObjetosConsumibles[i] = 0;
            cantidadesObjetosAtaques[i] = 0;
            cantidadesObjetosEquipo[i] = 0;
            cantidadObjetosClave[i] = 0;
        }

        for(int i = 0; i < 10; i++)
        {
            cantidadCascos[i] = 0;
            cantidadArmaduras[i] = 0;
            cantidadBotas[i] = 0;
            cantidadArmas[i] = 0;
            cantidadEscudos[i] = 0;
            cantidadComplemento[i] = 0;
        }

        for (int i = 0; i < listaObjetos.Length; i++)
        {
            Objeto aux = new Objeto();
            listaObjetos[i] = aux.IniciarObjeto(i);

            /*
            if (listaObjetos[i].tipo == Objeto.tipoObjeto.EQUIPO)
            {
                if (listaObjetos[i].tipoEq == Objeto.tipoEquipo.CABEZA)
                {
                    listaObjetos[i].icono = iconosObjetos[2];
                }
                else if (listaObjetos[i].tipoEq == Objeto.tipoEquipo.CUERPO)
                {
                    listaObjetos[i].icono = iconosObjetos[3];
                }
                else if (listaObjetos[i].tipoEq == Objeto.tipoEquipo.BOTAS)
                {
                    listaObjetos[i].icono = iconosObjetos[4];
                }
                else if (listaObjetos[i].tipoEq == Objeto.tipoEquipo.COMPLEMENTO)
                {
                    listaObjetos[i].icono = iconosObjetos[5];
                }
                else if (listaObjetos[i].tipoEq == Objeto.tipoEquipo.ARMA)
                {
                    listaObjetos[i].icono = iconosObjetos[6];
                }
                else if (listaObjetos[i].tipoEq == Objeto.tipoEquipo.ESCUDO)
                {
                    listaObjetos[i].icono = iconosObjetos[7];
                }
            }
            else if (listaObjetos[i].tipo == Objeto.tipoObjeto.APRENDE_ATAQUE)
            {
                listaObjetos[i].icono = iconosObjetos[1];
            }
            else if (listaObjetos[i].tipo == Objeto.tipoObjeto.CLAVE)
            {
                listaObjetos[i].icono = iconosObjetos[8];
            }
            else
            {
                listaObjetos[i].icono = iconosObjetos[0];
            }
            */
        }

        listaMisiones = new Mision[40];
        listaMisionesActivas = new Mision[listaMisiones.Length];
        listaMisionesPrincipales = new Mision[listaMisiones.Length];
        listaMisionesReclutamiento = new Mision[listaMisiones.Length];
        listaMisionesSecundarias = new Mision[listaMisiones.Length];
        numeroMisionesPrincipales = numeroMisionesReclutamiento = numeroMisionesSecundarias = 0;

        for (int i = 0; i < listaMisiones.Length; i++)
        {
            Mision aux = new Mision();
            listaMisiones[i] = aux.IniciarMision(i);
        }

        ataquesDesbloqueados = new bool[72];
        for (int i = 0; i < ataquesDesbloqueados.Length; i++)
        {
            ataquesDesbloqueados[i] = false;
        }

        //Pociones
        puedeHacerPociones = false;

        for(int i = 0; i < 10; i++)
        {
            puntuacionRecordPociones[i] = 0;
        }

        nombrePociones[0] = "Poción Vida";
        nombrePociones[1] = "Poción Ataque F";
        nombrePociones[2] = "Poción Defensa F";
        nombrePociones[3] = "Poción Defensa M";
        nombrePociones[4] = "Poción Ataque M";
        nombrePociones[5] = "Poción Velocidad";
        nombrePociones[6] = "Poción Vida+";
        nombrePociones[7] = "Poción Energizante";
        nombrePociones[8] = "Poción Vida++";
        nombrePociones[9] = "Panacea";

        nombrePocionesIngles[0] = "Poción Vida";
        nombrePocionesIngles[1] = "Poción Ataque F";
        nombrePocionesIngles[2] = "Poción Defensa F";
        nombrePocionesIngles[3] = "Poción Defensa M";
        nombrePocionesIngles[4] = "Poción Ataque M";
        nombrePocionesIngles[5] = "Poción Velocidad";
        nombrePocionesIngles[6] = "Poción Vida+";
        nombrePocionesIngles[7] = "Poción Energizante";
        nombrePocionesIngles[8] = "Poción Vida++";
        nombrePocionesIngles[9] = "Panacea";

        //Esgrima
        puedeHacerEsgrima = false;

        for (int i = 0; i < 10; i++)
        {
            puntuacionRecordEsgrima[i] = 0;
        }

        nombreRetosEsgrima[0] = "Poción Vida";
        nombreRetosEsgrima[1] = "Poción Ataque F";
        nombreRetosEsgrima[2] = "Poción Defensa F";
        nombreRetosEsgrima[3] = "Poción Defensa M";
        nombreRetosEsgrima[4] = "Poción Ataque M";
        nombreRetosEsgrima[5] = "Poción Velocidad";
        nombreRetosEsgrima[6] = "Poción Vida+";
        nombreRetosEsgrima[7] = "Poción Energizante";
        nombreRetosEsgrima[8] = "Poción Vida++";
        nombreRetosEsgrima[9] = "Panacea";

        nombreRetosEsgrimaIngles[0] = "Poción Vida";
        nombreRetosEsgrimaIngles[1] = "Poción Ataque F";
        nombreRetosEsgrimaIngles[2] = "Poción Defensa F";
        nombreRetosEsgrimaIngles[3] = "Poción Defensa M";
        nombreRetosEsgrimaIngles[4] = "Poción Ataque M";
        nombreRetosEsgrimaIngles[5] = "Poción Velocidad";
        nombreRetosEsgrimaIngles[6] = "Poción Vida+";
        nombreRetosEsgrimaIngles[7] = "Poción Energizante";
        nombreRetosEsgrimaIngles[8] = "Poción Vida++";
        nombreRetosEsgrimaIngles[9] = "Panacea";

        iniciado = true;

        zonaVisitada = new bool[30];

        zonaVisitada[0] = true;
        indiceInicial = 0;
        indiceObjetivo = 2;

        for(int i = 1; i < 30; i++)
        {
            zonaVisitada[i] = false;
        }

        retosEsgrimaDesbloqueados[0] = true;
        retosEsgrimaDesbloqueados[1] = true;

        teleportActivo = false;

        ActivaMapas(0);
    }



    public void ActivaMapas(int indice)
    {
        indiceInicial = indice;
        controlMapas.IniciaMapas(indice);
    }



    public Personajes BuscarPersonajeIndice(int valor, bool aliado)
    {
        Personajes m;

        if (aliado)
        {
            if(valor <= this.listaPersonajesAliados.Length)
            {
                m = this.listaPersonajesAliados[valor];
            }
            else
            {
                m = null;
            }
        }
        else
        {
            if (valor <= this.listaPersonajesEnemigos.Length)
            {
                m = new Personajes(this.listaPersonajesEnemigos[valor]);
            }
            else
            {
                m = null;
            }
        }
        return m;
    }



    public Ataque BuscarAtaqueIndice(int valor)
    {
        Ataque m;

        if (valor <= this.listaAtaques.Length)
        {
            m = this.listaAtaques[valor];
        }
        else
        {
            m = null;
        }
        
        return m;
    }



    public Objeto BuscarObjetoIndice(int valor)
    {
        Objeto m;

        if (valor <= this.listaObjetos.Length)
        {
            m = this.listaObjetos[valor];
        }
        else
        {
            m = null;
        }

        return m;
    }



    public void AniadePersonaje(int indice)
    {
        if(numeroIntegrantesEquipo < 3)
        {
            equipoAliado[numeroIntegrantesEquipo] = listaPersonajesAliados[indice];
            numeroIntegrantesEquipo++;
        }
        else
        {
            personajesAlmacenados[numeroAlmacenado] = indice;
            numeroAlmacenado++;
        }
    }
    


    public void IncluirEnInventario(int indice, int cantidad)
    {
        int indiceTipo = listaObjetos[indice].indiceTipo;

        if (listaObjetos[indice].tipo == Objeto.tipoObjeto.APRENDE_ATAQUE)
        {
            objetosAtaques[numeroObjetosAtaques] = listaObjetos[indice];
            cantidadesObjetosAtaques[numeroObjetosAtaques] = 1;
            numeroObjetosAtaques++;
            ataquesDesbloqueados[listaObjetos[indice].indiceAtq] = true;
        }
        else if (listaObjetos[indice].tipo == Objeto.tipoObjeto.EQUIPO)
        {
            bool situado = false;

            if(numeroObjetosEquipo != 0)
            {
                for (int i = 0; i < numeroObjetosEquipo; i++)
                {
                    if (!situado)
                    {
                        if(indice == objetosEquipo[i].indice)
                        {
                            situado = true;
                            cantidadesObjetosEquipo[i] += cantidad;

                            if (cantidadesObjetosEquipo[i] > 99)
                            {
                                cantidadesObjetosEquipo[i] = 99;
                            }
                        }
                    }
                }

                if (!situado)
                {
                    objetosEquipo[numeroObjetosEquipo] = listaObjetos[indice];

                    cantidadesObjetosEquipo[numeroObjetosEquipo] += cantidad;

                    if (cantidadesObjetosEquipo[numeroObjetosEquipo] > 99)
                    {
                        cantidadesObjetosEquipo[numeroObjetosEquipo] = 99;
                    }

                    numeroObjetosEquipo++;
                }
            }
            else
            {
                objetosEquipo[0] = listaObjetos[indice];
                numeroObjetosEquipo++;

                cantidadesObjetosEquipo[0] += cantidad;


                if (cantidadesObjetosEquipo[0] > 99)
                {
                    cantidadesObjetosEquipo[0] = 99;
                }
            }

            if (listaObjetos[indice].tipoEq == Objeto.tipoEquipo.CABEZA)
            {
                bool colocado = false;

                if(numeroCascos != 0)
                {
                    for(int i = 0; i < numeroCascos; i++)
                    {
                        if (!colocado)
                        {
                            if (cascos[i].indice == listaObjetos[indice].indice)
                            {
                                cantidadCascos[i] += cantidad;
                                colocado = true;
                            }
                        }
                    }

                    if (!colocado)
                    {
                        cascos[numeroCascos] = listaObjetos[indice];
                        cantidadCascos[numeroCascos] = cantidad;
                        numeroCascos++;
                    }
                }
                else
                {
                    cascos[numeroCascos] = listaObjetos[indice];
                    cantidadCascos[numeroCascos] = cantidad;
                    numeroCascos++;
                }
            }
            else if (listaObjetos[indice].tipoEq == Objeto.tipoEquipo.CUERPO)
            {
                bool colocado = false;

                if (numeroArmaduras != 0)
                {
                    for (int i = 0; i < numeroArmaduras; i++)
                    {
                        if (!colocado)
                        {
                            if (armaduras[i].indice == listaObjetos[indice].indice)
                            {
                                cantidadArmaduras[i] += cantidad;
                                colocado = true;
                            }
                        }
                    }

                    if (!colocado)
                    {
                        armaduras[numeroArmaduras] = listaObjetos[indice];
                        cantidadArmaduras[numeroArmaduras] = cantidad;
                        numeroArmaduras++;
                    }
                }
                else
                {
                    armaduras[numeroArmaduras] = listaObjetos[indice];
                    cantidadArmaduras[numeroArmaduras] = cantidad;
                    numeroArmaduras++;
                }
            }
            else if (listaObjetos[indice].tipoEq == Objeto.tipoEquipo.BOTAS)
            {
                bool colocado = false;

                if (numeroBotas != 0)
                {
                    for (int i = 0; i < numeroBotas; i++)
                    {
                        if (!colocado)
                        {
                            if (botas[i].indice == listaObjetos[indice].indice)
                            {
                                cantidadBotas[i] += cantidad;
                                colocado = true;
                            }
                        }
                    }

                    if (!colocado)
                    {
                        botas[numeroBotas] = listaObjetos[indice];
                        cantidadBotas[numeroBotas] = cantidad;
                        numeroBotas++;
                    }
                }
                else
                {
                    botas[numeroBotas] = listaObjetos[indice];
                    cantidadBotas[numeroBotas] = cantidad;
                    numeroBotas++;
                }
            }
            else if (listaObjetos[indice].tipoEq == Objeto.tipoEquipo.ARMA)
            {
                bool colocado = false;

                if (numeroArmas != 0)
                {
                    for (int i = 0; i < numeroArmas; i++)
                    {
                        if (!colocado)
                        {
                            if (armas[i].indice == listaObjetos[indice].indice)
                            {
                                cantidadArmas[i] += cantidad;
                                colocado = true;
                            }
                        }
                    }

                    if (!colocado)
                    {
                        armas[numeroArmas] = listaObjetos[indice];
                        cantidadArmas[numeroArmas] = cantidad;
                        numeroArmas++;
                    }
                }
                else
                {
                    armas[numeroArmas] = listaObjetos[indice];
                    cantidadArmas[numeroArmas] = cantidad;
                    numeroArmas++;
                }
            }
            else if (listaObjetos[indice].tipoEq == Objeto.tipoEquipo.ESCUDO)
            {
                bool colocado = false;

                if (numeroEscudos != 0)
                {
                    for (int i = 0; i < numeroEscudos; i++)
                    {
                        if (!colocado)
                        {
                            if (escudos[i].indice == listaObjetos[indice].indice)
                            {
                                cantidadEscudos[i] += cantidad;
                                colocado = true;
                            }
                        }
                    }

                    if (!colocado)
                    {
                        escudos[numeroEscudos] = listaObjetos[indice];
                        cantidadEscudos[numeroEscudos] = cantidad;
                        numeroEscudos++;
                    }
                }
                else
                {
                    escudos[numeroEscudos] = listaObjetos[indice];
                    cantidadEscudos[numeroEscudos] = cantidad;
                    numeroEscudos++;
                }
            }
            else if (listaObjetos[indice].tipoEq == Objeto.tipoEquipo.COMPLEMENTO)
            {
                bool colocado = false;

                if (numeroComplemento != 0)
                {
                    for (int i = 0; i < numeroComplemento; i++)
                    {
                        if (!colocado)
                        {
                            if (complemento[i].indice == listaObjetos[indice].indice)
                            {
                                cantidadComplemento[i] += cantidad;
                                colocado = true;
                            }
                        }
                    }

                    if (!colocado)
                    {
                        complemento[numeroComplemento] = listaObjetos[indice];
                        cantidadComplemento[numeroComplemento] = cantidad;
                        numeroComplemento++;
                    }
                }
                else
                {
                    complemento[numeroComplemento] = listaObjetos[indice];
                    cantidadComplemento[numeroComplemento] = cantidad;
                    numeroComplemento++;
                }
            }
        }
        else if(listaObjetos[indice].tipo == Objeto.tipoObjeto.CLAVE)
        {
            listaObjetosClave[numeroObjetosClave] = listaObjetos[indice];
            cantidadObjetosClave[numeroObjetosClave] = 1;
            numeroObjetosClave++;
        }
        else
        {
            bool situado = false;
            if (numeroObjetosConsumibles != 0)
            {
                for (int i = 0; i < numeroObjetosConsumibles; i++)
                {
                    if (!situado)
                    {
                        if (listaObjetos[indice].indice == objetosConsumibles[i].indice)
                        {
                            situado = true;
                            cantidadesObjetosConsumibles[i] += cantidad;

                            if (cantidadesObjetosConsumibles[i] > 99)
                            {
                                cantidadesObjetosConsumibles[i] = 99;
                            }
                        }
                    }
                }

                if (!situado)
                {
                    objetosConsumibles[numeroObjetosConsumibles] = listaObjetos[indice];

                    cantidadesObjetosConsumibles[numeroObjetosConsumibles] += cantidad;

                    if (cantidadesObjetosConsumibles[numeroObjetosConsumibles] > 99)
                    {
                        cantidadesObjetosConsumibles[numeroObjetosConsumibles] = 99;
                    }

                    numeroObjetosConsumibles++;
                }
            }
            else
            {
                objetosConsumibles[0] = listaObjetos[indice];
                numeroObjetosConsumibles++;

                cantidadesObjetosConsumibles[0] += cantidad;


                if (cantidadesObjetosConsumibles[0] > 99)
                {
                    cantidadesObjetosConsumibles[0] = 99;
                }
            }
        }
    }



    public void QuitarDeInventario(int indice, int cantidad, int tipo)
    {
        if (tipo == 0)
        {
            cantidadesObjetosConsumibles[indice] -= cantidad;
        }
        else
        {
            cantidadesObjetosEquipo[indice] -= cantidad;
        }
    }


    /*
    public int CalculaBonificacionObjetoNuevo(int indicePersonaje, int indiceObjeto, int indiceMejora)
    {
        /*
         *  0 --> Atq. Fis
         *  1 --> Def. Fis
         *  2 --> Atq. Mag
         *  3 --> Def. Mag
         *  4 --> Vida
         *  5 --> Evasion
         *  6 --> Velocidad
        */
        /*
        int valor = 0;

        switch (indiceMejora)
        {
            case 0:
                valor = equipoAliado[indicePersonaje].ataqueFisico + listaObjetos[indiceObjeto].aumentoAtaqueFisico;
                break;
            case 1:
                valor = equipoAliado[indicePersonaje].defensaFisica + listaObjetos[indiceObjeto].aumentoDefensaFisica;
                break;
            case 2:
                valor = equipoAliado[indicePersonaje].ataqueMagico + listaObjetos[indiceObjeto].aumentoAtaqueMagico;
                break;
            case 3:
                valor = equipoAliado[indicePersonaje].defensaMagica + listaObjetos[indiceObjeto].aumentoDefensaMagica;
                break;
            case 4:
                valor = equipoAliado[indicePersonaje].vida + listaObjetos[indiceObjeto].aumentoVida;
                break;
            case 5:
                valor = equipoAliado[indicePersonaje].evasion + listaObjetos[indiceObjeto].aumentoEvasion;
                break;
            case 6:
                valor = equipoAliado[indicePersonaje].velocidad + listaObjetos[indiceObjeto].aumentoVelocidad;
                break;
        }

        return valor;
    }
    */


    public void EquiparObjeto(int indicePersonaje, int indiceEquipoNuevo, int indiceZonaEquipar)
    {
        bool equipar = false;

        if(indiceZonaEquipar == 0)
        {
            if (equipoAliado[indicePersonaje].llevaCasco)
            {
                if(equipoAliado[indicePersonaje].casco.indice != cascos[indiceEquipoNuevo].indice)
                {
                    equipar = true;
                }
            }
            else
            {
                equipar = true;
            }

            if (equipar)
            {
                equipoAliado[indicePersonaje].llevaCasco = true;
                equipoAliado[indicePersonaje].casco = cascos[indiceEquipoNuevo];

                int aux = cascos[indiceEquipoNuevo].indice;

                for(int i = 0; i < numeroObjetosEquipo; i++)
                {
                    if(aux == objetosEquipo[i].indice)
                    {
                        cantidadesObjetosEquipo[i]--;
                    }
                }

                cantidadCascos[indiceEquipoNuevo]--;

                if (cascos[indiceEquipoNuevo].aumentaAtaqueF)
                {
                    equipoAliado[indicePersonaje].ataqueFisicoModificado = equipoAliado[indicePersonaje].ataqueFisicoModificado + equipoAliado[indicePersonaje].casco.aumentoAtaqueFisico;
                    equipoAliado[indicePersonaje].ataqueFisicoActual = equipoAliado[indicePersonaje].ataqueFisicoModificado;
                }

                if (cascos[indiceEquipoNuevo].aumentaAtaqueM)
                {
                    equipoAliado[indicePersonaje].ataqueMagicoModificado = equipoAliado[indicePersonaje].ataqueMagicoModificado + equipoAliado[indicePersonaje].casco.aumentoAtaqueMagico;
                    equipoAliado[indicePersonaje].ataqueMagicoActual = equipoAliado[indicePersonaje].ataqueMagicoModificado;
                }

                if (cascos[indiceEquipoNuevo].aumentaDefensaF)
                {
                    equipoAliado[indicePersonaje].defensaFisicaModificada = equipoAliado[indicePersonaje].defensaFisicaModificada + equipoAliado[indicePersonaje].casco.aumentoDefensaFisica;
                    equipoAliado[indicePersonaje].defensaFisicaActual = equipoAliado[indicePersonaje].defensaFisicaModificada;
                }

                if (cascos[indiceEquipoNuevo].aumentaDefensaM)
                {
                    equipoAliado[indicePersonaje].defensaMagicaModificada = equipoAliado[indicePersonaje].defensaMagicaModificada + equipoAliado[indicePersonaje].casco.aumentoDefensaMagica;
                    equipoAliado[indicePersonaje].defensaMagicaActual = equipoAliado[indicePersonaje].defensaMagicaModificada;
                }

                if (cascos[indiceEquipoNuevo].aumentaVid)
                {
                    equipoAliado[indicePersonaje].vidaModificada = equipoAliado[indicePersonaje].vidaModificada + equipoAliado[indicePersonaje].casco.aumentoVida;

                    if (equipoAliado[indicePersonaje].vidaModificada < equipoAliado[indicePersonaje].vida)
                    {
                        if (equipoAliado[indicePersonaje].vidaModificada < equipoAliado[indicePersonaje].vida)
                        {
                            equipoAliado[indicePersonaje].vidaActual = equipoAliado[indicePersonaje].vidaModificada;
                        }
                    }
                }

                if (cascos[indiceEquipoNuevo].aumentaVel)
                {
                    equipoAliado[indicePersonaje].velocidadModificada = equipoAliado[indicePersonaje].velocidadModificada + equipoAliado[indicePersonaje].casco.aumentoVelocidad;
                    equipoAliado[indicePersonaje].velocidadActual = equipoAliado[indicePersonaje].velocidadModificada;
                }
            }
        }
        else if (indiceZonaEquipar == 1)
        {
            if (equipoAliado[indicePersonaje].llevaArmadura)
            {
                if (equipoAliado[indicePersonaje].armadura.indice != armaduras[indiceEquipoNuevo].indice)
                {
                    equipar = true;
                }
            }
            else
            {
                equipar = true;
            }

            if (equipar)
            {
                equipoAliado[indicePersonaje].llevaArmadura = true;
                equipoAliado[indicePersonaje].armadura = armaduras[indiceEquipoNuevo];
                cantidadArmaduras[indiceEquipoNuevo]--;

                int aux = armaduras[indiceEquipoNuevo].indice;

                for (int i = 0; i < numeroObjetosEquipo; i++)
                {
                    if (aux == objetosEquipo[i].indice)
                    {
                        cantidadesObjetosEquipo[i]--;
                    }
                }

                if (armaduras[indiceEquipoNuevo].aumentaAtaqueF)
                {
                    equipoAliado[indicePersonaje].ataqueFisicoModificado = equipoAliado[indicePersonaje].ataqueFisicoModificado + equipoAliado[indicePersonaje].armadura.aumentoAtaqueFisico;
                    equipoAliado[indicePersonaje].ataqueFisicoActual = equipoAliado[indicePersonaje].ataqueFisicoModificado;
                }

                if (armaduras[indiceEquipoNuevo].aumentaAtaqueM)
                {
                    equipoAliado[indicePersonaje].ataqueMagicoModificado = equipoAliado[indicePersonaje].ataqueMagicoModificado + equipoAliado[indicePersonaje].armadura.aumentoAtaqueMagico;
                    equipoAliado[indicePersonaje].ataqueMagicoActual = equipoAliado[indicePersonaje].ataqueMagicoModificado;
                }

                if (armaduras[indiceEquipoNuevo].aumentaDefensaF)
                {
                    equipoAliado[indicePersonaje].defensaFisicaModificada = equipoAliado[indicePersonaje].defensaFisicaModificada + equipoAliado[indicePersonaje].armadura.aumentoDefensaFisica;
                    equipoAliado[indicePersonaje].defensaFisicaActual = equipoAliado[indicePersonaje].defensaFisicaModificada;
                }

                if (armaduras[indiceEquipoNuevo].aumentaDefensaM)
                {
                    equipoAliado[indicePersonaje].defensaMagicaModificada = equipoAliado[indicePersonaje].defensaMagicaModificada + equipoAliado[indicePersonaje].armadura.aumentoDefensaMagica;
                    equipoAliado[indicePersonaje].defensaMagicaActual = equipoAliado[indicePersonaje].defensaMagicaModificada;
                }

                if (armaduras[indiceEquipoNuevo].aumentaVid)
                {
                    equipoAliado[indicePersonaje].vidaModificada = equipoAliado[indicePersonaje].vidaModificada + equipoAliado[indicePersonaje].armadura.aumentoVida;

                    if (equipoAliado[indicePersonaje].vidaModificada < equipoAliado[indicePersonaje].vida)
                    {
                        if (equipoAliado[indicePersonaje].vidaModificada < equipoAliado[indicePersonaje].vida)
                        {
                            equipoAliado[indicePersonaje].vidaActual = equipoAliado[indicePersonaje].vidaModificada;
                        }
                    }
                }

                if (armaduras[indiceEquipoNuevo].aumentaVel)
                {
                    equipoAliado[indicePersonaje].velocidadModificada = equipoAliado[indicePersonaje].velocidadModificada + equipoAliado[indicePersonaje].armadura.aumentoVelocidad;
                    equipoAliado[indicePersonaje].velocidadActual = equipoAliado[indicePersonaje].velocidadModificada;
                }
            }
        }
        else if (indiceZonaEquipar == 2)
        {
            if (equipoAliado[indicePersonaje].llevaBotas)
            {
                if (equipoAliado[indicePersonaje].botas.indice != botas[indiceEquipoNuevo].indice)
                {
                    equipar = true;
                }
            }
            else
            {
                equipar = true;
            }

            if (equipar)
            {
                equipoAliado[indicePersonaje].llevaBotas = true;
                equipoAliado[indicePersonaje].botas = botas[indiceEquipoNuevo];
                cantidadBotas[indiceEquipoNuevo]--;

                int aux = botas[indiceEquipoNuevo].indice;

                for (int i = 0; i < numeroObjetosEquipo; i++)
                {
                    if (aux == objetosEquipo[i].indice)
                    {
                        cantidadesObjetosEquipo[i]--;
                    }
                }

                if (botas[indiceEquipoNuevo].aumentaAtaqueF)
                {
                    equipoAliado[indicePersonaje].ataqueFisicoModificado = equipoAliado[indicePersonaje].ataqueFisicoModificado + equipoAliado[indicePersonaje].botas.aumentoAtaqueFisico;
                    equipoAliado[indicePersonaje].ataqueFisicoActual = equipoAliado[indicePersonaje].ataqueFisicoModificado;
                }

                if (botas[indiceEquipoNuevo].aumentaAtaqueM)
                {
                    equipoAliado[indicePersonaje].ataqueMagicoModificado = equipoAliado[indicePersonaje].ataqueMagicoModificado + equipoAliado[indicePersonaje].botas.aumentoAtaqueMagico;
                    equipoAliado[indicePersonaje].ataqueMagicoActual = equipoAliado[indicePersonaje].ataqueMagicoModificado;
                }

                if (botas[indiceEquipoNuevo].aumentaDefensaF)
                {
                    equipoAliado[indicePersonaje].defensaFisicaModificada = equipoAliado[indicePersonaje].defensaFisicaModificada + equipoAliado[indicePersonaje].botas.aumentoDefensaFisica;
                    equipoAliado[indicePersonaje].defensaFisicaActual = equipoAliado[indicePersonaje].defensaFisicaModificada;
                }

                if (botas[indiceEquipoNuevo].aumentaDefensaM)
                {
                    equipoAliado[indicePersonaje].defensaMagicaModificada = equipoAliado[indicePersonaje].defensaMagicaModificada + equipoAliado[indicePersonaje].botas.aumentoDefensaMagica;
                    equipoAliado[indicePersonaje].defensaMagicaActual = equipoAliado[indicePersonaje].defensaMagicaModificada;
                }

                if (botas[indiceEquipoNuevo].aumentaVid)
                {
                    equipoAliado[indicePersonaje].vidaModificada = equipoAliado[indicePersonaje].vidaModificada + equipoAliado[indicePersonaje].botas.aumentoVida;

                    if (equipoAliado[indicePersonaje].vidaModificada < equipoAliado[indicePersonaje].vida)
                    {
                        if (equipoAliado[indicePersonaje].vidaModificada < equipoAliado[indicePersonaje].vida)
                        {
                            equipoAliado[indicePersonaje].vidaActual = equipoAliado[indicePersonaje].vidaModificada;
                        }
                    }
                }

                if (botas[indiceEquipoNuevo].aumentaVel)
                {
                    equipoAliado[indicePersonaje].velocidadModificada = equipoAliado[indicePersonaje].velocidadModificada + equipoAliado[indicePersonaje].botas.aumentoVelocidad;
                    equipoAliado[indicePersonaje].velocidadActual = equipoAliado[indicePersonaje].velocidadModificada;
                }
            }
        }
        else if (indiceZonaEquipar == 3)
        {
            if (equipoAliado[indicePersonaje].llevaArma)
            {
                if (equipoAliado[indicePersonaje].arma.indice != armas[indiceEquipoNuevo].indice)
                {
                    equipar = true;
                }
            }
            else
            {
                equipar = true;
            }

            if (equipar)
            {
                equipoAliado[indicePersonaje].llevaArma = true;
                equipoAliado[indicePersonaje].arma = armas[indiceEquipoNuevo];
                cantidadArmas[indiceEquipoNuevo]--;

                int aux = armas[indiceEquipoNuevo].indice;

                for (int i = 0; i < numeroObjetosEquipo; i++)
                {
                    if (aux == objetosEquipo[i].indice)
                    {
                        cantidadesObjetosEquipo[i]--;
                    }
                }

                if (armas[indiceEquipoNuevo].aumentaAtaqueF)
                {
                    equipoAliado[indicePersonaje].ataqueFisicoModificado = equipoAliado[indicePersonaje].ataqueFisicoModificado + equipoAliado[indicePersonaje].arma.aumentoAtaqueFisico;
                    equipoAliado[indicePersonaje].ataqueFisicoActual = equipoAliado[indicePersonaje].ataqueFisicoModificado;
                }

                if (armas[indiceEquipoNuevo].aumentaAtaqueM)
                {
                    equipoAliado[indicePersonaje].ataqueMagicoModificado = equipoAliado[indicePersonaje].ataqueMagicoModificado + equipoAliado[indicePersonaje].arma.aumentoAtaqueMagico;
                    equipoAliado[indicePersonaje].ataqueMagicoActual = equipoAliado[indicePersonaje].ataqueMagicoModificado;
                }

                if (armas[indiceEquipoNuevo].aumentaDefensaF)
                {
                    equipoAliado[indicePersonaje].defensaFisicaModificada = equipoAliado[indicePersonaje].defensaFisicaModificada + equipoAliado[indicePersonaje].arma.aumentoDefensaFisica;
                    equipoAliado[indicePersonaje].defensaFisicaActual = equipoAliado[indicePersonaje].defensaFisicaModificada;
                }

                if (armas[indiceEquipoNuevo].aumentaDefensaM)
                {
                    equipoAliado[indicePersonaje].defensaMagicaModificada = equipoAliado[indicePersonaje].defensaMagicaModificada + equipoAliado[indicePersonaje].arma.aumentoDefensaMagica;
                    equipoAliado[indicePersonaje].defensaMagicaActual = equipoAliado[indicePersonaje].defensaMagicaModificada;
                }

                if (armas[indiceEquipoNuevo].aumentaVid)
                {
                    equipoAliado[indicePersonaje].vidaModificada = equipoAliado[indicePersonaje].vidaModificada + equipoAliado[indicePersonaje].arma.aumentoVida;

                    if (equipoAliado[indicePersonaje].vidaModificada < equipoAliado[indicePersonaje].vida)
                    {
                        if (equipoAliado[indicePersonaje].vidaModificada < equipoAliado[indicePersonaje].vida)
                        {
                            equipoAliado[indicePersonaje].vidaActual = equipoAliado[indicePersonaje].vidaModificada;
                        }
                    }
                }

                if (armas[indiceEquipoNuevo].aumentaVel)
                {
                    equipoAliado[indicePersonaje].velocidadModificada = equipoAliado[indicePersonaje].velocidadModificada + equipoAliado[indicePersonaje].arma.aumentoVelocidad;
                    equipoAliado[indicePersonaje].velocidadActual = equipoAliado[indicePersonaje].velocidadModificada;
                }
            }
        }
        else if (indiceZonaEquipar == 4)
        {
            if (equipoAliado[indicePersonaje].llevaEscudo)
            {
                if (equipoAliado[indicePersonaje].escudo.indice != escudos[indiceEquipoNuevo].indice)
                {
                    equipar = true;
                }
            }
            else
            {
                equipar = true;
            }

            if (equipar)
            {
                equipoAliado[indicePersonaje].llevaEscudo = true;
                equipoAliado[indicePersonaje].escudo = escudos[indiceEquipoNuevo];
                cantidadEscudos[indiceEquipoNuevo]--;

                int aux = escudos[indiceEquipoNuevo].indice;

                for (int i = 0; i < numeroObjetosEquipo; i++)
                {
                    if (aux == objetosEquipo[i].indice)
                    {
                        cantidadesObjetosEquipo[i]--;
                    }
                }

                if (escudos[indiceEquipoNuevo].aumentaAtaqueF)
                {
                    equipoAliado[indicePersonaje].ataqueFisicoModificado = equipoAliado[indicePersonaje].ataqueFisicoModificado + equipoAliado[indicePersonaje].escudo.aumentoAtaqueFisico;
                    equipoAliado[indicePersonaje].ataqueFisicoActual = equipoAliado[indicePersonaje].ataqueFisicoModificado;
                }

                if (escudos[indiceEquipoNuevo].aumentaAtaqueM)
                {
                    equipoAliado[indicePersonaje].ataqueMagicoModificado = equipoAliado[indicePersonaje].ataqueMagicoModificado + equipoAliado[indicePersonaje].escudo.aumentoAtaqueMagico;
                    equipoAliado[indicePersonaje].ataqueMagicoActual = equipoAliado[indicePersonaje].ataqueMagicoModificado;
                }

                if (escudos[indiceEquipoNuevo].aumentaDefensaF)
                {
                    equipoAliado[indicePersonaje].defensaFisicaModificada = equipoAliado[indicePersonaje].defensaFisicaModificada + equipoAliado[indicePersonaje].escudo.aumentoDefensaFisica;
                    equipoAliado[indicePersonaje].defensaFisicaActual = equipoAliado[indicePersonaje].defensaFisicaModificada;
                }

                if (escudos[indiceEquipoNuevo].aumentaDefensaM)
                {
                    equipoAliado[indicePersonaje].defensaMagicaModificada = equipoAliado[indicePersonaje].defensaMagicaModificada + equipoAliado[indicePersonaje].escudo.aumentoDefensaMagica;
                    equipoAliado[indicePersonaje].defensaMagicaActual = equipoAliado[indicePersonaje].defensaMagicaModificada;
                }

                if (escudos[indiceEquipoNuevo].aumentaVid)
                {
                    equipoAliado[indicePersonaje].vidaModificada = equipoAliado[indicePersonaje].vidaModificada + equipoAliado[indicePersonaje].escudo.aumentoVida;

                    if (equipoAliado[indicePersonaje].vidaModificada < equipoAliado[indicePersonaje].vida)
                    {
                        if (equipoAliado[indicePersonaje].vidaModificada < equipoAliado[indicePersonaje].vida)
                        {
                            equipoAliado[indicePersonaje].vidaActual = equipoAliado[indicePersonaje].vidaModificada;
                        }
                    }
                }

                if (escudos[indiceEquipoNuevo].aumentaVel)
                {
                    equipoAliado[indicePersonaje].velocidadModificada = equipoAliado[indicePersonaje].velocidadModificada + equipoAliado[indicePersonaje].escudo.aumentoVelocidad;
                    equipoAliado[indicePersonaje].velocidadActual = equipoAliado[indicePersonaje].velocidadModificada;
                }
            }
        }
        else
        {
            if (equipoAliado[indicePersonaje].llevaComplemento)
            {
                if (equipoAliado[indicePersonaje].complemento.indice != complemento[indiceEquipoNuevo].indice)
                {
                    equipar = true;
                }
            }
            else
            {
                equipar = true;
            }

            if (equipar)
            {
                equipoAliado[indicePersonaje].llevaComplemento = true;
                equipoAliado[indicePersonaje].complemento = complemento[indiceEquipoNuevo];
                cantidadComplemento[indiceEquipoNuevo]--;

                int aux = complemento[indiceEquipoNuevo].indice;

                for (int i = 0; i < numeroObjetosEquipo; i++)
                {
                    if (aux == objetosEquipo[i].indice)
                    {
                        cantidadesObjetosEquipo[i]--;
                    }
                }

                if (complemento[indiceEquipoNuevo].aumentaAtaqueF)
                {
                    equipoAliado[indicePersonaje].ataqueFisicoModificado = equipoAliado[indicePersonaje].ataqueFisicoModificado + equipoAliado[indicePersonaje].complemento.aumentoAtaqueFisico;
                    equipoAliado[indicePersonaje].ataqueFisicoActual = equipoAliado[indicePersonaje].ataqueFisicoModificado;
                }

                if (complemento[indiceEquipoNuevo].aumentaAtaqueM)
                {
                    equipoAliado[indicePersonaje].ataqueMagicoModificado = equipoAliado[indicePersonaje].ataqueMagicoModificado + equipoAliado[indicePersonaje].complemento.aumentoAtaqueMagico;
                    equipoAliado[indicePersonaje].ataqueMagicoActual = equipoAliado[indicePersonaje].ataqueMagicoModificado;
                }

                if (complemento[indiceEquipoNuevo].aumentaDefensaF)
                {
                    equipoAliado[indicePersonaje].defensaFisicaModificada = equipoAliado[indicePersonaje].defensaFisicaModificada + equipoAliado[indicePersonaje].complemento.aumentoDefensaFisica;
                    equipoAliado[indicePersonaje].defensaFisicaActual = equipoAliado[indicePersonaje].defensaFisicaModificada;
                }

                if (complemento[indiceEquipoNuevo].aumentaDefensaM)
                {
                    equipoAliado[indicePersonaje].defensaMagicaModificada = equipoAliado[indicePersonaje].defensaMagicaModificada + equipoAliado[indicePersonaje].complemento.aumentoDefensaMagica;
                    equipoAliado[indicePersonaje].defensaMagicaActual = equipoAliado[indicePersonaje].defensaMagicaModificada;
                }

                if (complemento[indiceEquipoNuevo].aumentaVid)
                {
                    equipoAliado[indicePersonaje].vidaModificada = equipoAliado[indicePersonaje].vidaModificada + equipoAliado[indicePersonaje].complemento.aumentoVida;

                    if (equipoAliado[indicePersonaje].vidaModificada < equipoAliado[indicePersonaje].vida)
                    {
                        if (equipoAliado[indicePersonaje].vidaModificada < equipoAliado[indicePersonaje].vida)
                        {
                            equipoAliado[indicePersonaje].vidaActual = equipoAliado[indicePersonaje].vidaModificada;
                        }
                    }
                }

                if (complemento[indiceEquipoNuevo].aumentaVel)
                {
                    equipoAliado[indicePersonaje].velocidadModificada = equipoAliado[indicePersonaje].velocidadModificada + equipoAliado[indicePersonaje].complemento.aumentoVelocidad;
                    equipoAliado[indicePersonaje].velocidadActual = equipoAliado[indicePersonaje].velocidadModificada;
                }
            }
        }
    }



    public void QuitarObjeto(int indicePersonaje, int indiceObjeto, int indiceZonaEquipar)
    {
        int indice = 0;

        if(indiceZonaEquipar == 0)
        {
            cantidadCascos[indiceObjeto]++;

            int aux = cascos[indiceObjeto].indice;

            for (int i = 0; i < numeroObjetosEquipo; i++)
            {
                if (aux == objetosEquipo[i].indice)
                {
                    cantidadesObjetosEquipo[i]++;
                }
            }

            equipoAliado[indicePersonaje].llevaCasco = false;
            indice = cascos[indiceObjeto].indice;
        }
        else if(indiceZonaEquipar == 1)
        {
            cantidadArmaduras[indiceObjeto]++;

            int aux = armaduras[indiceObjeto].indice;

            for (int i = 0; i < numeroObjetosEquipo; i++)
            {
                if (aux == objetosEquipo[i].indice)
                {
                    cantidadesObjetosEquipo[i]++;
                }
            }

            equipoAliado[indicePersonaje].llevaArmadura = false;
            indice = armaduras[indiceObjeto].indice;
        }
        else if(indiceZonaEquipar == 2)
        {
            cantidadBotas[indiceObjeto]++;

            int aux = botas[indiceObjeto].indice;

            for (int i = 0; i < numeroObjetosEquipo; i++)
            {
                if (aux == objetosEquipo[i].indice)
                {
                    cantidadesObjetosEquipo[i]++;
                }
            }

            equipoAliado[indicePersonaje].llevaBotas = false;
            indice = botas[indiceObjeto].indice;
        }
        else if(indiceZonaEquipar == 3)
        {
            cantidadArmas[indiceObjeto]++;

            int aux = armas[indiceObjeto].indice;

            for (int i = 0; i < numeroObjetosEquipo; i++)
            {
                if (aux == objetosEquipo[i].indice)
                {
                    cantidadesObjetosEquipo[i]++;
                }
            }

            equipoAliado[indicePersonaje].llevaArma = false;
            indice = armas[indiceObjeto].indice;
        }
        else if(indiceZonaEquipar == 4)
        {
            cantidadEscudos[indiceObjeto]++;

            int aux = escudos[indiceObjeto].indice;

            for (int i = 0; i < numeroObjetosEquipo; i++)
            {
                if (aux == objetosEquipo[i].indice)
                {
                    cantidadesObjetosEquipo[i]++;
                }
            }

            equipoAliado[indicePersonaje].llevaEscudo = false;
            indice = escudos[indiceObjeto].indice;
        }
        else if(indiceZonaEquipar == 5)
        {
            cantidadComplemento[indiceObjeto]++;

            int aux = complemento[indiceObjeto].indice;

            for (int i = 0; i < numeroObjetosEquipo; i++)
            {
                if (aux == objetosEquipo[i].indice)
                {
                    cantidadesObjetosEquipo[i]++;
                }
            }

            equipoAliado[indicePersonaje].llevaComplemento = false;
            indice = complemento[indiceObjeto].indice;
        }

        if (listaObjetos[indice].aumentaVid)
        {
            equipoAliado[indicePersonaje].vidaModificada -= listaObjetos[indice].aumentoVida;

            if(equipoAliado[indicePersonaje].vidaModificada < equipoAliado[indicePersonaje].vidaActual)
            {
                equipoAliado[indicePersonaje].vidaActual = equipoAliado[indicePersonaje].vidaModificada;
            }
        }

        if (listaObjetos[indice].aumentaAtaqueF)
        {
            equipoAliado[indicePersonaje].ataqueFisicoModificado -= listaObjetos[indice].aumentoAtaqueFisico;
            equipoAliado[indicePersonaje].ataqueFisicoActual = equipoAliado[indicePersonaje].ataqueFisicoModificado;
        }

        if (listaObjetos[indice].aumentaAtaqueM)
        {
            equipoAliado[indicePersonaje].ataqueMagicoModificado -= listaObjetos[indice].aumentoAtaqueMagico;
            equipoAliado[indicePersonaje].ataqueMagicoActual = equipoAliado[indicePersonaje].ataqueMagicoModificado;
        }

        if (listaObjetos[indice].aumentaDefensaF)
        {
            equipoAliado[indicePersonaje].defensaFisicaModificada -= listaObjetos[indice].aumentoDefensaFisica;
            equipoAliado[indicePersonaje].defensaFisicaActual = equipoAliado[indicePersonaje].defensaFisicaModificada;
        }

        if (listaObjetos[indice].aumentaDefensaM)
        {
            equipoAliado[indicePersonaje].defensaMagicaModificada -= listaObjetos[indice].aumentoDefensaMagica;
            equipoAliado[indicePersonaje].defensaMagicaActual = equipoAliado[indicePersonaje].defensaMagicaModificada;
        }

        if (listaObjetos[indice].aumentaVel)
        {
            equipoAliado[indicePersonaje].velocidadModificada -= listaObjetos[indice].aumentoVelocidad;
            equipoAliado[indicePersonaje].velocidadActual = equipoAliado[indicePersonaje].velocidadModificada;
        }
    }



    public void IncluyeMision(int valor)
    {
        
        listaMisionesActivas[numeroMisionesActivas] = listaMisiones[valor];
        numeroMisionesActivas++;

        if(listaMisiones[valor].tipoDeMision == Mision.tipoMision.PRINCIPAL)
        {
            if(valor == 10)
            {
                IncluirEnInventario(0, 3);
                IncluirEnInventario(80, 1);
                IncluirEnInventario(90, 1);
            }

            listaMisionesPrincipales[numeroMisionesPrincipales] = listaMisiones[valor];
            numeroMisionesPrincipales++;
        }
        else if (listaMisiones[valor].tipoDeMision == Mision.tipoMision.SECUNDARIA)
        {
            listaMisionesSecundarias[numeroMisionesSecundarias] = listaMisiones[valor];
            numeroMisionesSecundarias++;
        }
        else
        {
            listaMisionesReclutamiento[numeroMisionesReclutamiento] = listaMisiones[valor];
            numeroMisionesReclutamiento++;

            switch (valor)
            {
                case 0:
                    controlSecundario.misionPedroActiva = true;
                    break;
                case 1:
                    controlSecundario.misionGamezActiva = true;
                    break;
                case 4:
                    controlSecundario.misionNaniActiva = true;
                    break;
            }
        }
    }



    public void CumpleMision(int indice)
    {
        bool principal = false;
        bool reclutamiento = false;
        bool secundaria = false;

        for(int i = 0; i < numeroMisionesActivas; i++)
        {
            if(listaMisionesActivas[i].indice == indice)
            {
                listaMisionesActivas[i].completada = true;
            }
        }

        switch (indice)
        {
            case 0:
                AniadePersonaje(19);
                controlSecundario.CierraMisionSecundaria(indice);
                reclutamiento = true;
                break;
            case 1:
                AniadePersonaje(12);
                controlSecundario.CierraMisionSecundaria(indice);
                reclutamiento = true;
                break;
            case 2:
                AniadePersonaje(6);
                reclutamiento = true;
                break;
            case 3:
                controlJugador.dinero += 500;
                secundaria = true;
                break;
            case 4:
                AniadePersonaje(8);
                controlSecundario.CierraMisionSecundaria(indice);
                reclutamiento = true;
                break;
            case 5:
                AniadePersonaje(7);
                reclutamiento = true;
                break;
            case 6:
                controlJugador.dinero += 100;
                secundaria = true;
                break;
            case 7:
                AniadePersonaje(2);
                break;
            case 8:
                AniadePersonaje(1);
                break;
            case 9:
                controlJugador.dinero += 200;
                secundaria = true;
                break;
            case 10:
                principal = true;
                break;
            case 11:
                reclutamiento = true;
                break;
            case 12:
                controlJugador.dinero += 500;
                principal = true;
                break;
            case 13:
                controlJugador.dinero += 500;
                principal = true;
                break;
            case 14:
                principal = true;
                break;
            case 15:
                controlJugador.dinero += 100;
                equipoAliado[0].defensaFisica += 4;
                equipoAliado[0].defensaFisicaActual += 4;
                equipoAliado[0].defensaFisicaModificada += 4;

                equipoAliado[0].defensaMagica += 4;
                equipoAliado[0].defensaMagicaActual += 4;
                equipoAliado[0].defensaMagicaModificada += 4;
                principal = true;
                break;
            case 16:
                controlJugador.dinero += 100;
                equipoAliado[0].ataqueFisico += 4;
                equipoAliado[0].ataqueFisicoActual += 4;
                equipoAliado[0].ataqueFisicoModificado += 4;

                equipoAliado[0].ataqueMagico += 4;
                equipoAliado[0].ataqueMagicoActual += 4;
                equipoAliado[0].ataqueMagicoModificado += 4;
                principal = true;
                break;
            case 17:
                principal = true;
                break;
            case 18:
                principal = true;
                break;
            case 19:
                principal = true;
                break;
            case 21:
                controlSecundario.rescatePagado = true;
                secundaria = true;
                ApagaPersonajes(0);
                break;
            case 22:
                AniadePersonaje(10);
                reclutamiento = true;
                controlSecundario.CierraMisionSecundaria(2);
                break;
        }
        
        if (principal)
        {
            for (int i = 0; i < numeroMisionesPrincipales; i++)
            {
                if (listaMisionesPrincipales[i].indice == indice)
                {
                    listaMisionesPrincipales[i].completada = true;
                }
            }
        }
        else if (secundaria)
        {
            for (int i = 0; i < numeroMisionesSecundarias; i++)
            {
                if (listaMisionesSecundarias[i].indice == indice)
                {
                    listaMisionesSecundarias[i].completada = true;
                }
            }
        }
        else if (reclutamiento)
        {
            for (int i = 0; i < numeroMisionesReclutamiento; i++)
            {
                if (listaMisionesReclutamiento[i].indice == indice)
                {
                    listaMisionesReclutamiento[i].completada = true;
                }
            }
        }
    }



    public void ApagaPersonajes(int indice)
    {
        personajesDesactivados[indice] = true;
    }



    public void SubirNivelAliado(int indice)
    {
        equipoAliado[indice].nivel++;

        if (equipoAliado[indice].elemento == Personajes.elementoPersonaje.DORMILON)
        {
            equipoAliado[indice].vida += 4;
            equipoAliado[indice].vidaActual += 4;
            equipoAliado[indice].vidaModificada += 4;

            equipoAliado[indice].ataqueFisico += 1;
            equipoAliado[indice].ataqueFisicoActual += 1;
            equipoAliado[indice].ataqueFisicoModificado += 1;

            equipoAliado[indice].defensaFisica += 2;
            equipoAliado[indice].defensaFisicaModificada += 2;
            equipoAliado[indice].defensaFisicaActual += 2;

            equipoAliado[indice].ataqueMagico += 2;
            equipoAliado[indice].ataqueMagicoActual += 2;
            equipoAliado[indice].ataqueMagicoModificado += 2;

            equipoAliado[indice].defensaMagica += 3;
            equipoAliado[indice].defensaMagicaModificada += 3;
            equipoAliado[indice].defensaMagicaActual += 3;

            equipoAliado[indice].velocidad += 1;
            equipoAliado[indice].velocidadActual += 1;
            equipoAliado[indice].velocidadModificada += 1;

            equipoAliado[indice].experiencia -= equipoAliado[indice].proximoNivel;

            equipoAliado[indice].proximoNivel = (int)(10 + (5 * equipoAliado[indice].nivel * equipoAliado[indice].nivel * equipoAliado[indice].nivel) / 4);
        }
        else if (equipoAliado[indice].elemento == Personajes.elementoPersonaje.FIESTERO)
        {
            equipoAliado[indice].vida += 1;
            equipoAliado[indice].vidaActual += 1;
            equipoAliado[indice].vidaModificada += 1;

            equipoAliado[indice].ataqueFisico += 4;
            equipoAliado[indice].ataqueFisicoActual += 4;
            equipoAliado[indice].ataqueFisicoModificado += 4;

            equipoAliado[indice].defensaFisica += 2;
            equipoAliado[indice].defensaFisicaModificada += 2;
            equipoAliado[indice].defensaFisicaActual += 2;

            equipoAliado[indice].ataqueMagico += 1;
            equipoAliado[indice].ataqueMagicoActual += 1;
            equipoAliado[indice].ataqueMagicoModificado += 1;

            equipoAliado[indice].defensaMagica += 2;
            equipoAliado[indice].defensaMagicaModificada += 2;
            equipoAliado[indice].defensaMagicaActual += 2;

            equipoAliado[indice].velocidad += 3;
            equipoAliado[indice].velocidadActual += 3;
            equipoAliado[indice].velocidadModificada += 3;

            equipoAliado[indice].experiencia -= equipoAliado[indice].proximoNivel;

            equipoAliado[indice].proximoNivel = 10 + (equipoAliado[indice].nivel * equipoAliado[indice].nivel * equipoAliado[indice].nivel);
        }
        else if (equipoAliado[indice].elemento == Personajes.elementoPersonaje.FRIKI)
        {
            equipoAliado[indice].vida += 2;
            equipoAliado[indice].vidaActual += 2;
            equipoAliado[indice].vidaModificada += 2;

            equipoAliado[indice].ataqueFisico += 1;
            equipoAliado[indice].ataqueFisicoActual += 1;
            equipoAliado[indice].ataqueFisicoModificado += 1;

            equipoAliado[indice].defensaFisica += 1;
            equipoAliado[indice].defensaFisicaModificada += 1;
            equipoAliado[indice].defensaFisicaActual += 1;

            equipoAliado[indice].ataqueMagico += 4;
            equipoAliado[indice].ataqueMagicoActual += 4;
            equipoAliado[indice].ataqueMagicoModificado += 4;

            equipoAliado[indice].defensaMagica += 3;
            equipoAliado[indice].defensaMagicaModificada += 3;
            equipoAliado[indice].defensaMagicaActual += 3;

            equipoAliado[indice].velocidad += 2;
            equipoAliado[indice].velocidadActual += 2;
            equipoAliado[indice].velocidadModificada += 2;

            equipoAliado[indice].experiencia -= equipoAliado[indice].proximoNivel;

            equipoAliado[indice].proximoNivel = (int)(10 + (4 * equipoAliado[indice].nivel * equipoAliado[indice].nivel * equipoAliado[indice].nivel) / 5);
        }
        else if (equipoAliado[indice].elemento == Personajes.elementoPersonaje.NEUTRO)
        {
            equipoAliado[indice].vida += 2;
            equipoAliado[indice].vidaActual += 2;
            equipoAliado[indice].vidaModificada += 2;

            equipoAliado[indice].ataqueFisico += 2;
            equipoAliado[indice].ataqueFisicoActual += 2;
            equipoAliado[indice].ataqueFisicoModificado += 2;

            equipoAliado[indice].defensaFisica += 2;
            equipoAliado[indice].defensaFisicaModificada += 2;
            equipoAliado[indice].defensaFisicaActual += 2;

            equipoAliado[indice].ataqueMagico += 2;
            equipoAliado[indice].ataqueMagicoActual += 2;
            equipoAliado[indice].ataqueMagicoModificado += 2;

            equipoAliado[indice].defensaMagica += 2;
            equipoAliado[indice].defensaMagicaModificada += 2;
            equipoAliado[indice].defensaMagicaActual += 2;

            equipoAliado[indice].velocidad += 2;
            equipoAliado[indice].velocidadActual += 2;
            equipoAliado[indice].velocidadModificada += 2;

            equipoAliado[indice].experiencia -= equipoAliado[indice].proximoNivel;

            equipoAliado[indice].proximoNivel = 10 + (equipoAliado[indice].nivel * equipoAliado[indice].nivel * equipoAliado[indice].nivel);
        }
        else if (equipoAliado[indice].elemento == Personajes.elementoPersonaje.RESPONSABLE)
        {
            equipoAliado[indice].vida += 4;
            equipoAliado[indice].vidaActual += 4;
            equipoAliado[indice].vidaModificada += 4;

            equipoAliado[indice].ataqueFisico += 2;
            equipoAliado[indice].ataqueFisicoActual += 2;
            equipoAliado[indice].ataqueFisicoModificado += 2;

            equipoAliado[indice].defensaFisica += 1;
            equipoAliado[indice].defensaFisicaModificada += 1;
            equipoAliado[indice].defensaFisicaActual += 1;

            equipoAliado[indice].ataqueMagico += 3;
            equipoAliado[indice].ataqueMagicoActual += 3;
            equipoAliado[indice].ataqueMagicoModificado += 3;

            equipoAliado[indice].defensaMagica += 2;
            equipoAliado[indice].defensaMagicaModificada += 2;
            equipoAliado[indice].defensaMagicaActual += 2;

            equipoAliado[indice].velocidad += 3;
            equipoAliado[indice].velocidadActual += 3;
            equipoAliado[indice].velocidadModificada += 3;

            equipoAliado[indice].experiencia -= equipoAliado[indice].proximoNivel;

            equipoAliado[indice].proximoNivel = (int)(10 + (4 * equipoAliado[indice].nivel * equipoAliado[indice].nivel * equipoAliado[indice].nivel) / 5);
        }
        else if (equipoAliado[indice].elemento == Personajes.elementoPersonaje.TIRANO)
        {
            equipoAliado[indice].vida += 2;
            equipoAliado[indice].vidaActual += 2;
            equipoAliado[indice].vidaModificada += 2;

            equipoAliado[indice].ataqueFisico += 4;
            equipoAliado[indice].ataqueFisicoActual += 4;
            equipoAliado[indice].ataqueFisicoModificado += 4;

            equipoAliado[indice].defensaFisica += 3;
            equipoAliado[indice].defensaFisicaModificada += 3;
            equipoAliado[indice].defensaFisicaActual += 3;

            equipoAliado[indice].ataqueMagico += 3;
            equipoAliado[indice].ataqueMagicoActual += 3;
            equipoAliado[indice].ataqueMagicoModificado += 3;

            equipoAliado[indice].defensaMagica += 3;
            equipoAliado[indice].defensaMagicaModificada += 3;
            equipoAliado[indice].defensaMagicaActual += 3;

            equipoAliado[indice].velocidad += 1;
            equipoAliado[indice].velocidadActual += 1;
            equipoAliado[indice].velocidadModificada += 1;

            equipoAliado[indice].experiencia -= equipoAliado[indice].proximoNivel;

            equipoAliado[indice].proximoNivel = (int)(10 + (5 * equipoAliado[indice].nivel * equipoAliado[indice].nivel * equipoAliado[indice].nivel) / 4);
        }
    }



    public void AbreCofre(int indice)
    {
        cofres[indice] = true;
    }



    public string NombreMapa(int indice)
    {
        string nombre = "";

        switch (indice)
        {
            case 0:
                nombre = "Pueblo Origen";
                break;
            case 1:
                nombre = "R5";
                break;
            case 2:
                nombre = "El Paso";
                break;
            case 3:
                nombre = "R6";
                break;
            case 4:
                nombre = "Pedrán";
                break;
            case 5:
                nombre = "R7";
                break;
            case 6:
                nombre = "R8";
                break;
            case 7:
                nombre = "Pueblo del Bosque";
                break;
            case 8:
                nombre = "R9";
                break;
            case 9:
                nombre = "Pueblo Río";
                break;
            case 10:
                nombre = "Universidad de Ancia";
                break;
            case 11:
                nombre = "R10";
                break;
            case 12:
                nombre = "Canda";
                break;
            case 13:
                nombre = "Bosque Esperanza";
                break;
            case 14:
                nombre = "R11";
                break;
            case 15:
                nombre = "Campus del Sur";
                break;
            case 16:
                nombre = "R12";
                break;
            case 17:
                nombre = "Pueblo Refugio";
                break;
            case 18:
                nombre = "Grán Tunel";
                break;
            case 19:
                nombre = "Pueblo Arena";
                break;
            case 20:
                nombre = "R13";
                break;
            case 21:
                nombre = "Manfa";
                break;
            case 22:
                nombre = "R4";
                break;
            case 23:
                nombre = "Templo";
                break;
            case 24:
                nombre = "R1";
                break;
            case 25:
                nombre = "Ciudad Imperial";
                break;
            case 26:
                nombre = "R2";
                break;
            case 27:
                nombre = "Pueblo Costero";
                break;
            case 28:
                nombre = "R3";
                break;
            case 29:
                nombre = "Puerto";
                break;
        }

        return nombre;
    }



    private void Update()
    {
        if(horas != 999)
        {
            segundos += Time.deltaTime;

            if (segundos >= 60)
            {
                minutos++;
                segundos -= 60;
            }

            if (minutos >= 60)
            {
                horas++;
                minutos -= 60;
            }
        }
    }



    public void CambiaNombreProta()
    {
        listaPersonajesAliados[0].nombre = nombreProta;
        listaPersonajesAliados[0].nombreIngles = nombreProta;
        equipoAliado[0].nombre = listaPersonajesAliados[0].nombre;
    }
}