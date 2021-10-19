using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuMision : MonoBehaviour {
    public GameObject mision, tipoMision, listaMision, titulo;
    public GameObject flecha;

    BaseDatos baseDeDatos;
    GameObject manager;
    MusicaManager musica;

    int pos;
    int pagina;
    int numeroPaginas;
    int numeroPosicionesPorPagina;
    int numeroElementosTotales;
    int categoria; //0 --> Todas     1 --> Principal     2 --> Secundaria    3 --> Reclutamiento 

    Sprite casillaSinCompletar;
    Sprite casillaCompletada;
    Sprite[] imagenes;

    public bool activo;

    bool pulsado;

    float digitalX;
    float digitalY;

    void Start ()
    {
        manager = GameObject.Find("GameManager");
        baseDeDatos = manager.GetComponent<BaseDatos>();
        musica = GameObject.Find("EfectosSonido").GetComponent<MusicaManager>();
        activo = false;

        imagenes = Resources.LoadAll<Sprite>("Sprites/Interfaz/Fondos/_checkmark_sheet");
        casillaSinCompletar = imagenes[0];
        casillaCompletada = imagenes[12];

        this.gameObject.SetActive(activo);
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

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || (!pulsado && digitalY < 0))
        {
            pulsado = true;
            musica.ProduceEfecto(11);
            if (pos != (numeroPosicionesPorPagina - 1))
            {
                pos++;
            }
            else
            {
                if (numeroPaginas != 0)
                {
                    pagina++;

                    if (pagina == numeroPaginas)
                    {
                        pagina = 0;
                    }
                }

                pos = 0;
            }


            MueveFlecha();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || (!pulsado && digitalY > 0))
        {
            pulsado = true;
            musica.ProduceEfecto(11);
            if (pos != 0)
            {
                pos--;
            }
            else
            {
                pos = numeroPosicionesPorPagina - 1;

                if (numeroPaginas != 0)
                {
                    pagina = numeroPaginas - 1;
                }

            }

            MueveFlecha();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || (!pulsado && digitalX < 0))
        {
            pulsado = true;
            musica.ProduceEfecto(11);
            categoria--;

            if(categoria < 0)
            {
                categoria = 3;
            }

            MueveCategoria();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || (!pulsado && digitalX > 0))
        {
            pulsado = true;
            musica.ProduceEfecto(11);
            categoria++;

            if (categoria > 3)
            {
                categoria = 0;
            }

            MueveCategoria();
        }
        else if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("B"))
        {
            musica.ProduceEfecto(12);
            CierraMenu();
        }
    }



    void CalculaPosicionesPagina(int pagina)
    {
        if(categoria == 0)
        {
            if(baseDeDatos.numeroMisionesActivas <= 10)
            {
                numeroPosicionesPorPagina = baseDeDatos.numeroMisionesActivas;
            }
            else
            {
                numeroPosicionesPorPagina = baseDeDatos.numeroMisionesActivas % pagina;
            }
        }
        else if (categoria == 1)
        {
            if (baseDeDatos.numeroMisionesPrincipales <= 10)
            {
                numeroPosicionesPorPagina = baseDeDatos.numeroMisionesPrincipales;
            }
            else
            {
                numeroPosicionesPorPagina = baseDeDatos.numeroMisionesPrincipales % pagina;
            }
        }
        else if (categoria == 2)
        {
            if (baseDeDatos.numeroMisionesSecundarias <= 10)
            {
                numeroPosicionesPorPagina = baseDeDatos.numeroMisionesSecundarias;
            }
            else
            {
                numeroPosicionesPorPagina = baseDeDatos.numeroMisionesSecundarias % pagina;
            }
        }
        else
        {
            if (baseDeDatos.numeroMisionesSecundarias <= 10)
            {
                numeroPosicionesPorPagina = baseDeDatos.numeroMisionesReclutamiento;
            }
            else
            {
                numeroPosicionesPorPagina = baseDeDatos.numeroMisionesReclutamiento % pagina;
            }
        }
    }



    public void IniciaMenu()
    {
        activo = true;
        this.gameObject.SetActive(activo);
        categoria = 0;
        pos = 0;
        MueveCategoria();
        MueveFlecha();
        ActualizaListaMisiones();
        numeroElementosTotales = baseDeDatos.numeroMisionesActivas;
        numeroPaginas = numeroElementosTotales / 10;

        if(baseDeDatos.idioma == 1)
        {
            titulo.transform.GetChild(0).GetComponent<Text>().text = "MISSION REGISTER";
        }
        else
        {
            titulo.transform.GetChild(0).GetComponent<Text>().text = "REGISTRO MISIONES";
        }

        CalculaPosicionesPagina(1);
    }



    void CierraMenu()
    {
        activo = false;
        this.gameObject.SetActive(activo);
    }



    void ActualizaMision()
    {
        int posicionActual = pos + pagina * 10;

        if(categoria == 0)
        {
            if(baseDeDatos.idioma == 1)
            {
                mision.transform.GetChild(0).GetComponent<Text>().text = baseDeDatos.listaMisionesActivas[posicionActual].origenIngles;
                mision.transform.GetChild(1).GetComponent<Text>().text = baseDeDatos.listaMisionesActivas[posicionActual].tituloIngles;
                mision.transform.GetChild(2).GetComponent<Text>().text = baseDeDatos.listaMisionesActivas[posicionActual].descripcionIngles;
            }
            else
            {
                mision.transform.GetChild(0).GetComponent<Text>().text = baseDeDatos.listaMisionesActivas[posicionActual].origen;
                mision.transform.GetChild(1).GetComponent<Text>().text = baseDeDatos.listaMisionesActivas[posicionActual].titulo;
                mision.transform.GetChild(2).GetComponent<Text>().text = baseDeDatos.listaMisionesActivas[posicionActual].descripcion;
            }

            string aux = "";
            int longitud = baseDeDatos.listaMisionesActivas[posicionActual].recompensas.Length;

            for (int i = 0; i < longitud; i++)
            {
                if(baseDeDatos.idioma == 1)
                {
                    aux += baseDeDatos.listaMisionesActivas[posicionActual].recompensasIngles[i];
                }
                else
                {
                    aux += baseDeDatos.listaMisionesActivas[posicionActual].recompensas[i];
                }

                if (i != (longitud - 1))
                {
                    aux += "\n";
                }
            }

            mision.transform.GetChild(4).GetComponent<Text>().text = aux;

            aux = "";
            longitud = baseDeDatos.listaMisionesActivas[posicionActual].estadoActual.Length;

            for (int i = 0; i < longitud; i++)
            {
                if (baseDeDatos.idioma == 1)
                {
                    aux += baseDeDatos.listaMisionesActivas[posicionActual].estadoActualIngles[i];
                }
                else
                {
                    aux += baseDeDatos.listaMisionesActivas[posicionActual].estadoActual[i];
                }
                
                if (i != (longitud - 1))
                {
                    aux += "\n";
                }
            }

            mision.transform.GetChild(3).GetComponent<Text>().text = aux;
        }
        else if(categoria == 1)
        {
            if (baseDeDatos.idioma == 1)
            {
                mision.transform.GetChild(0).GetComponent<Text>().text = baseDeDatos.listaMisionesPrincipales[posicionActual].origenIngles;
                mision.transform.GetChild(1).GetComponent<Text>().text = baseDeDatos.listaMisionesPrincipales[posicionActual].tituloIngles;
                mision.transform.GetChild(2).GetComponent<Text>().text = baseDeDatos.listaMisionesPrincipales[posicionActual].descripcionIngles;
            }
            else
            {
                mision.transform.GetChild(0).GetComponent<Text>().text = baseDeDatos.listaMisionesPrincipales[posicionActual].origen;
                mision.transform.GetChild(1).GetComponent<Text>().text = baseDeDatos.listaMisionesPrincipales[posicionActual].titulo;
                mision.transform.GetChild(2).GetComponent<Text>().text = baseDeDatos.listaMisionesPrincipales[posicionActual].descripcion;
            }

            string aux = "";
            int longitud = baseDeDatos.listaMisionesPrincipales[posicionActual].recompensas.Length;

            for (int i = 0; i < longitud; i++)
            {
                if (baseDeDatos.idioma == 1)
                {
                    aux += baseDeDatos.listaMisionesPrincipales[posicionActual].recompensasIngles[i];
                }
                else
                {
                    aux += baseDeDatos.listaMisionesPrincipales[posicionActual].recompensas[i];
                }

                if (i != (longitud - 1))
                {
                    aux += "\n";
                }
            }
            mision.transform.GetChild(4).GetComponent<Text>().text = aux;

            aux = "";
            longitud = baseDeDatos.listaMisionesPrincipales[posicionActual].estadoActual.Length;

            for (int i = 0; i < longitud; i++)
            {
                if (baseDeDatos.idioma == 1)
                {
                    aux += baseDeDatos.listaMisionesPrincipales[posicionActual].estadoActualIngles[i];
                }
                else
                {
                    aux += baseDeDatos.listaMisionesPrincipales[posicionActual].estadoActual[i];
                }

                if (i != (longitud - 1))
                {
                    aux += "\n";
                }
            }
            mision.transform.GetChild(3).GetComponent<Text>().text = aux;
        }
        else if(categoria == 2)
        {
            if (baseDeDatos.numeroMisionesSecundarias != 0)
            {
                if (baseDeDatos.idioma == 1)
                {
                    mision.transform.GetChild(0).GetComponent<Text>().text = baseDeDatos.listaMisionesSecundarias[posicionActual].origenIngles;
                    mision.transform.GetChild(1).GetComponent<Text>().text = baseDeDatos.listaMisionesSecundarias[posicionActual].tituloIngles;
                    mision.transform.GetChild(2).GetComponent<Text>().text = baseDeDatos.listaMisionesSecundarias[posicionActual].descripcionIngles;
                }
                else
                {
                    mision.transform.GetChild(0).GetComponent<Text>().text = baseDeDatos.listaMisionesSecundarias[posicionActual].origen;
                    mision.transform.GetChild(1).GetComponent<Text>().text = baseDeDatos.listaMisionesSecundarias[posicionActual].titulo;
                    mision.transform.GetChild(2).GetComponent<Text>().text = baseDeDatos.listaMisionesSecundarias[posicionActual].descripcion;
                }

                string aux = "";
                int longitud = baseDeDatos.listaMisionesSecundarias[posicionActual].recompensas.Length;

                for (int i = 0; i < longitud; i++)
                {
                    if (baseDeDatos.idioma == 1)
                    {
                        aux += baseDeDatos.listaMisionesSecundarias[posicionActual].recompensasIngles[i];
                    }
                    else
                    {
                        aux += baseDeDatos.listaMisionesSecundarias[posicionActual].recompensas[i];
                    }

                    if (i != (longitud - 1))
                    {
                        aux += "\n";
                    }
                }

                mision.transform.GetChild(4).GetComponent<Text>().text = aux;

                aux = "";
                longitud = baseDeDatos.listaMisionesSecundarias[posicionActual].estadoActual.Length;

                for (int i = 0; i < longitud; i++)
                {
                    if (baseDeDatos.idioma == 1)
                    {
                        aux += baseDeDatos.listaMisionesSecundarias[posicionActual].estadoActualIngles[i];
                    }
                    else
                    {
                        aux += baseDeDatos.listaMisionesSecundarias[posicionActual].estadoActual[i];
                    }

                    if (i != (longitud - 1))
                    {
                        aux += "\n";
                    }
                }

                mision.transform.GetChild(3).GetComponent<Text>().text = aux;
            }
            else
            {
                mision.transform.GetChild(0).GetComponent<Text>().text = "";
                mision.transform.GetChild(1).GetComponent<Text>().text = "";
                mision.transform.GetChild(2).GetComponent<Text>().text = "";
                mision.transform.GetChild(4).GetComponent<Text>().text = "";
                mision.transform.GetChild(3).GetComponent<Text>().text = "";
            }
        }
        else
        {
            if(baseDeDatos.numeroMisionesReclutamiento != 0)
            {
                if (baseDeDatos.idioma == 1)
                {
                    mision.transform.GetChild(0).GetComponent<Text>().text = baseDeDatos.listaMisionesReclutamiento[posicionActual].origenIngles;
                    mision.transform.GetChild(1).GetComponent<Text>().text = baseDeDatos.listaMisionesReclutamiento[posicionActual].tituloIngles;
                    mision.transform.GetChild(2).GetComponent<Text>().text = baseDeDatos.listaMisionesReclutamiento[posicionActual].descripcionIngles;
                }
                else
                {
                    mision.transform.GetChild(0).GetComponent<Text>().text = baseDeDatos.listaMisionesReclutamiento[posicionActual].origen;
                    mision.transform.GetChild(1).GetComponent<Text>().text = baseDeDatos.listaMisionesReclutamiento[posicionActual].titulo;
                    mision.transform.GetChild(2).GetComponent<Text>().text = baseDeDatos.listaMisionesReclutamiento[posicionActual].descripcion;
                }

                string aux = "";
                int longitud = baseDeDatos.listaMisionesReclutamiento[posicionActual].recompensas.Length;

                for (int i = 0; i < longitud; i++)
                {
                    if (baseDeDatos.idioma == 1)
                    {
                        aux += baseDeDatos.listaMisionesReclutamiento[posicionActual].recompensasIngles[i];
                    }
                    else
                    {
                        aux += baseDeDatos.listaMisionesReclutamiento[posicionActual].recompensas[i];
                    }

                    if (i != (longitud - 1))
                    {
                        aux += "\n";
                    }
                }

                mision.transform.GetChild(4).GetComponent<Text>().text = aux;

                aux = "";
                longitud = baseDeDatos.listaMisionesReclutamiento[posicionActual].estadoActual.Length;

                for (int i = 0; i < longitud; i++)
                {
                    if (baseDeDatos.idioma == 1)
                    {
                        aux += baseDeDatos.listaMisionesReclutamiento[posicionActual].estadoActualIngles[i];
                    }
                    else
                    {
                        aux += baseDeDatos.listaMisionesReclutamiento[posicionActual].estadoActual[i];
                    }

                    if (i != (longitud - 1))
                    {
                        aux += "\n";
                    }
                }

                mision.transform.GetChild(3).GetComponent<Text>().text = aux;
            }
            else
            {
                mision.transform.GetChild(0).GetComponent<Text>().text = "";
                mision.transform.GetChild(1).GetComponent<Text>().text = "";
                mision.transform.GetChild(2).GetComponent<Text>().text = "";
                mision.transform.GetChild(4).GetComponent<Text>().text = "";
                mision.transform.GetChild(3).GetComponent<Text>().text = "";
            }
        }
    }



    void MueveFlecha()
    {
        int posicion = pos + 20;
        flecha.transform.position = listaMision.transform.GetChild(posicion).transform.position;

        ActualizaMision();
    }



    void MueveCategoria()
    {
        if(baseDeDatos.idioma == 1)
        {
            switch (categoria)
            {
                case 0:
                    tipoMision.transform.GetChild(0).GetComponent<Text>().text = "< All >";
                    break;
                case 1:
                    tipoMision.transform.GetChild(0).GetComponent<Text>().text = "< Main >";
                    break;
                case 2:
                    tipoMision.transform.GetChild(0).GetComponent<Text>().text = "< Secondary >";
                    break;
                case 3:
                    tipoMision.transform.GetChild(0).GetComponent<Text>().text = "< Recruitment >";
                    break;
            }
        }
        else
        {
            switch (categoria)
            {
                case 0:
                    tipoMision.transform.GetChild(0).GetComponent<Text>().text = "< Todas >";
                    break;
                case 1:
                    tipoMision.transform.GetChild(0).GetComponent<Text>().text = "< Principal >";
                    break;
                case 2:
                    tipoMision.transform.GetChild(0).GetComponent<Text>().text = "< Secundaria >";
                    break;
                case 3:
                    tipoMision.transform.GetChild(0).GetComponent<Text>().text = "< Reclutamiento >";
                    break;
            }
        }

        pos = 0;
        pagina = 0;
        CalculaPosicionesPagina(1);
        MueveFlecha();
        ActualizaMision();
        ActualizaListaMisiones();
    }



    void ActualizaListaMisiones()
    {
        int aux = pagina * 10;
        int auxPos = 0;

        if(categoria == 0)
        {
            for(int i = aux; i < 10; i++)
            {
                if(i < baseDeDatos.numeroMisionesActivas)
                {
                    if(baseDeDatos.idioma == 1)
                    {
                        listaMision.transform.GetChild(auxPos).GetComponent<Text>().text = baseDeDatos.listaMisionesActivas[i].tituloIngles;
                    }
                    else
                    {
                        listaMision.transform.GetChild(auxPos).GetComponent<Text>().text = baseDeDatos.listaMisionesActivas[i].titulo;
                    }
                    
                    if (baseDeDatos.listaMisionesActivas[i].completada)
                    {
                        listaMision.transform.GetChild(auxPos + 10).GetComponent<Image>().sprite = casillaCompletada;
                    }
                    else
                    {
                        listaMision.transform.GetChild(auxPos + 10).GetComponent<Image>().sprite = casillaSinCompletar;
                    }
                }
                else
                {
                    listaMision.transform.GetChild(auxPos).GetComponent<Text>().text = "------------";
                    listaMision.transform.GetChild(auxPos + 10).GetComponent<Image>().sprite = casillaSinCompletar;
                }

                auxPos++;
            }
        }
        else if(categoria == 1)
        {
            for (int i = aux; i < 10; i++)
            {
                if (i < baseDeDatos.numeroMisionesPrincipales)
                {
                    if (baseDeDatos.idioma == 1)
                    {
                        listaMision.transform.GetChild(auxPos).GetComponent<Text>().text = baseDeDatos.listaMisionesPrincipales[i].tituloIngles;
                    }
                    else
                    {
                        listaMision.transform.GetChild(auxPos).GetComponent<Text>().text = baseDeDatos.listaMisionesPrincipales[i].titulo;
                    }

                    if (baseDeDatos.listaMisionesPrincipales[i].completada)
                    {
                        listaMision.transform.GetChild(auxPos + 10).GetComponent<Image>().sprite = casillaCompletada;
                    }
                    else
                    {
                        listaMision.transform.GetChild(auxPos + 10).GetComponent<Image>().sprite = casillaSinCompletar;
                    }
                }
                else
                {
                    listaMision.transform.GetChild(auxPos).GetComponent<Text>().text = "------------";
                    listaMision.transform.GetChild(auxPos + 10).GetComponent<Image>().sprite = casillaSinCompletar;
                }

                auxPos++;
            }
        }
        else if(categoria == 2)
        {
            for (int i = aux; i < 10; i++)
            {
                if (i < baseDeDatos.numeroMisionesSecundarias)
                {
                    if (baseDeDatos.idioma == 1)
                    {
                        listaMision.transform.GetChild(auxPos).GetComponent<Text>().text = baseDeDatos.listaMisionesSecundarias[i].tituloIngles;
                    }
                    else
                    {
                        listaMision.transform.GetChild(auxPos).GetComponent<Text>().text = baseDeDatos.listaMisionesSecundarias[i].titulo;
                    }

                    if (baseDeDatos.listaMisionesSecundarias[i].completada)
                    {
                        listaMision.transform.GetChild(auxPos + 10).GetComponent<Image>().sprite = casillaCompletada;
                    }
                    else
                    {
                        listaMision.transform.GetChild(auxPos + 10).GetComponent<Image>().sprite = casillaSinCompletar;
                    }
                }
                else
                {
                    listaMision.transform.GetChild(auxPos).GetComponent<Text>().text = "------------";
                    listaMision.transform.GetChild(auxPos + 10).GetComponent<Image>().sprite = casillaSinCompletar;
                }

                auxPos++;
            }
        }
        else
        {
            for (int i = aux; i < 10; i++)
            {
                if (i < baseDeDatos.numeroMisionesReclutamiento)
                {
                    if (baseDeDatos.idioma == 1)
                    {
                        listaMision.transform.GetChild(auxPos).GetComponent<Text>().text = baseDeDatos.listaMisionesReclutamiento[i].tituloIngles;
                    }
                    else
                    {
                        listaMision.transform.GetChild(auxPos).GetComponent<Text>().text = baseDeDatos.listaMisionesReclutamiento[i].titulo;
                    }
                    

                    if (baseDeDatos.listaMisionesReclutamiento[i].completada)
                    {
                        listaMision.transform.GetChild(auxPos + 10).GetComponent<Image>().sprite = casillaCompletada;
                    }
                    else
                    {
                        listaMision.transform.GetChild(auxPos + 10).GetComponent<Image>().sprite = casillaSinCompletar;
                    }
                }
                else
                {
                    listaMision.transform.GetChild(auxPos).GetComponent<Text>().text = "------------";
                    listaMision.transform.GetChild(auxPos + 10).GetComponent<Image>().sprite = casillaSinCompletar;
                }

                auxPos++;
            }
        }
    }
}
