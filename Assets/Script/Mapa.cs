using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mapa : MonoBehaviour
{
    int indice;
    float digitalX;
    float digitalY;
    public int indiceDestino;

    public bool activo;
    bool pulsado;
    bool parpadea;
    bool viaje;
    public bool destinoElegido;
    public bool volver;

    MusicaManager efectos;
    BaseDatos baseDeDatos;


    void Start()
    {
        efectos = GameObject.Find("EfectosSonido").GetComponent<MusicaManager>();
        baseDeDatos = GameObject.Find("GameManager").GetComponent<BaseDatos>();
        destinoElegido = false;
        volver = false;

        DesactivaMenu();    
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

            if (!parpadea)
            {
                StartCoroutine(ParpadeaIcono());
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

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || (!pulsado && digitalY > 0))
            {
                pulsado = true;

                efectos.ProduceEfecto(11);

                if (PuedeMover(0))
                {
                    MueveIndicador(0);
                    CambiaNombre();
                }
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || (!pulsado && digitalY < 0))
            {
                pulsado = true;

                efectos.ProduceEfecto(11);

                if (PuedeMover(3))
                {
                    MueveIndicador(3);
                    CambiaNombre();
                }
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || (!pulsado && digitalX < 0))
            {
                pulsado = true;

                efectos.ProduceEfecto(11);

                if (PuedeMover(1))
                {
                    MueveIndicador(1);
                    CambiaNombre();
                }
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || (!pulsado && digitalX > 0))
            {
                pulsado = true;

                efectos.ProduceEfecto(11);

                if (PuedeMover(2))
                {
                    MueveIndicador(2);
                    CambiaNombre();
                }
            }
            else if (Input.GetButtonUp("B") || Input.GetKeyDown(KeyCode.M))
            {
                efectos.ProduceEfecto(10);
                DesactivaMenu();
            }

            if (viaje)
            {
                if (Input.GetButtonUp("A") || Input.GetKeyDown(KeyCode.N))
                {
                    int i = indice;

                    if ( i == 0 || i == 2 || i == 4 || i == 7 || i == 9 || i == 10 || i == 12 || i == 13 || i == 15 || i == 17 || i == 19 || i == 21
                        || i == 23 || i == 25 || i == 27 || i == 29)
                    {
                        if (baseDeDatos.zonaVisitada[i])
                        {
                            indiceDestino = i;
                        }

                        destinoElegido = true;
                        DesactivaMenu();
                    }
                    
                }
                else if (Input.GetButtonUp("B") || Input.GetKeyDown(KeyCode.M))
                {
                    DesactivaMenu();
                    indice = -1;
                    volver = true;
                }
            }
        }
    }



    public void ActivaMenu(bool viaja)
    {
        activo = true;
        viaje = viaja;
        indice = baseDeDatos.indiceInicial;
        pulsado = true;
        int pos = indice + 3;
        indiceDestino = -1;
        parpadea = false;

        this.transform.GetChild(1).GetChild(0).position = this.transform.GetChild(1).GetChild(pos).position;
        this.transform.GetChild(1).GetChild(1).position = this.transform.GetChild(1).GetChild(pos).position;

        int aux = baseDeDatos.indiceObjetivo + 3;
        this.transform.GetChild(1).GetChild(2).position = this.transform.GetChild(1).GetChild(aux).position;

        this.gameObject.SetActive(activo);

        int aux2;

        string nombre = baseDeDatos.NombreMapa(indice);
        this.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = nombre;

        for (int i = 1; i < 30; i++)
        {
            aux2 = i + 3;

            if(i == 2 || i == 4 || i == 7 || i == 9 || i == 10 || i == 12 || i == 13 || i == 15 || i == 17 || i == 19 || i == 21 
                || i == 23 || i == 25 || i == 27 || i == 29)
            {
                if (baseDeDatos.zonaVisitada[i])
                {
                    this.transform.GetChild(1).GetChild(aux2).GetChild(0).gameObject.SetActive(false);
                }
            }
            
            
        }

        if (viaje)
        {
            this.transform.GetChild(2).gameObject.SetActive(false);
            this.transform.GetChild(3).gameObject.SetActive(true);

            if (baseDeDatos.mandoActivo)
            {
                this.transform.GetChild(3).GetChild(1).GetComponent<Image>().sprite = baseDeDatos.seleccionXBOX[0];
                this.transform.GetChild(3).GetChild(3).GetComponent<Image>().sprite = baseDeDatos.moverXBOX[0];
            }
            else
            {
                this.transform.GetChild(3).GetChild(1).GetComponent<Image>().sprite = baseDeDatos.seleccionPC[0];
                this.transform.GetChild(3).GetChild(3).GetComponent<Image>().sprite = baseDeDatos.moverPC[0];
            }
        }
        else
        {
            this.transform.GetChild(2).gameObject.SetActive(true);
            this.transform.GetChild(3).gameObject.SetActive(false);

            if (baseDeDatos.mandoActivo)
            {
                this.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = baseDeDatos.moverXBOX[0];
            }
            else
            {
                this.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = baseDeDatos.moverPC[0];
            }
        }

        CambiaNombre();
    }



    void DesactivaMenu()
    {
        activo = false;
        this.gameObject.SetActive(activo);
        pulsado = false;
    }



    void CambiaNombre()
    {
        string nombre = "";

        if (baseDeDatos.zonaVisitada[indice])
        {
            if(baseDeDatos.idioma == 1)
            {
                switch (indice)
                {
                    case 0:
                        nombre = "Origin Town";
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
                        nombre = "Forest Town";
                        break;
                    case 8:
                        nombre = "R9";
                        break;
                    case 9:
                        nombre = "River Town";
                        break;
                    case 10:
                        nombre = "University";
                        break;
                    case 11:
                        nombre = "R10";
                        break;
                    case 12:
                        nombre = "Canda";
                        break;
                    case 13:
                        nombre = "Hope Forest";
                        break;
                    case 14:
                        nombre = "R11";
                        break;
                    case 15:
                        nombre = "New University";
                        break;
                    case 17:
                        nombre = "Refuge Town";
                        break;
                    case 18:
                        nombre = "Big Grotto";
                        break;
                    case 19:
                        nombre = "Sand Town";
                        break;
                    case 20:
                        nombre = "R12";
                        break;
                    case 21:
                        nombre = "Manfa";
                        break;
                    case 22:
                        nombre = "R4";
                        break;
                    case 23:
                        nombre = "Great Temple of Áncia";
                        break;
                    case 24:
                        nombre = "R1";
                        break;
                    case 25:
                        nombre = "Imperial City";
                        break;
                    case 26:
                        nombre = "R2";
                        break;
                    case 27:
                        nombre = "Albay Town";
                        break;
                    case 28:
                        nombre = "R3";
                        break;
                    case 29:
                        nombre = "Porto Bello";
                        break;
                }
            }
            else
            {
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
                        nombre = "Pueblo Bosque";
                        break;
                    case 8:
                        nombre = "R9";
                        break;
                    case 9:
                        nombre = "Pueblo Rio";
                        break;
                    case 10:
                        nombre = "Universidad";
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
                        nombre = "Campus 2";
                        break;
                    case 17:
                        nombre = "Pueblo Refugio";
                        break;
                    case 18:
                        nombre = "Gran Gruta";
                        break;
                    case 19:
                        nombre = "Pueblo Arena";
                        break;
                    case 20:
                        nombre = "R12";
                        break;
                    case 21:
                        nombre = "Manfa";
                        break;
                    case 22:
                        nombre = "R4";
                        break;
                    case 23:
                        nombre = "Gran Academia";
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
                        nombre = "Pueblo Alba";
                        break;
                    case 28:
                        nombre = "R3";
                        break;
                    case 29:
                        nombre = "Porto Bello";
                        break;
                }
            }
        }
        else
        {
            nombre = "???";
        }

        this.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = nombre;
    }



    bool PuedeMover(int direccion) //0 --> arriba 1 --> izquierda 2 --> derecha 3 --> abajo
    {
        bool puede = false;

        switch (indice)
        {
            case 0:
                if(direccion == 0)
                {
                    puede = true;
                }
                else
                {
                    puede = false;
                }
                break;
            case 1:
                if (direccion == 0 || direccion == 3)
                {
                    puede = true;
                }
                else
                {
                    puede = false;
                }
                break;
            case 2:
                if (direccion != 0)
                {
                    puede = true;
                }
                else
                {
                    puede = false;
                }
                break;
            case 3:
                if (direccion == 2 || direccion == 1)
                {
                    puede = true;
                }
                else
                {
                    puede = false;
                }
                break;
            case 4:
                puede = true;
                break;
            case 5:
                puede = true;

                break;
            case 6:
                if (direccion == 1 || direccion == 2)
                {
                    puede = true;
                }
                else
                {
                    puede = false;
                }
                break;
            case 7:
                if (direccion == 1)
                {
                    puede = true;
                }
                else
                {
                    puede = false;
                }
                break;
            case 8:
                if (direccion == 1 || direccion == 2)
                {
                    puede = true;
                }
                else
                {
                    puede = false;
                }
                break;
            case 9:
                if (direccion == 2)
                {
                    puede = true;
                }
                else
                {
                    puede = false;
                }
                break;
            case 10:
                if (direccion == 3)
                {
                    puede = true;
                }
                else
                {
                    puede = false;
                }
                break;
            case 11:
                if (direccion == 0 || direccion == 3)
                {
                    puede = true;
                }
                else
                {
                    puede = false;
                }
                break;
            case 12:
                if (direccion == 0 || direccion == 3)
                {
                    puede = true;
                }
                else
                {
                    puede = false;
                }
                break;
            case 13:
                if (direccion == 0)
                {
                    puede = true;
                }
                else
                {
                    puede = false;
                }
                break;
            case 14:
                if (direccion != 0)
                {
                    puede = true;
                }
                else
                {
                    puede = false;
                }
                break;
            case 15:
                if (direccion == 0)
                {
                    puede = true;
                }
                else
                {
                    puede = false;
                }
                break;
            case 17:
                if (direccion == 1 || direccion == 2)
                {
                    puede = true;
                }
                else
                {
                    puede = false;
                }
                break;
            case 18:
                if (direccion == 1 || direccion == 2)
                {
                    puede = true;
                }
                else
                {
                    puede = false;
                }
                break;
            case 19:
                if (direccion == 0 || direccion == 2)
                {
                    puede = true;
                }
                else
                {
                    puede = false;
                }
                break;
            case 20:
                if (direccion == 0 || direccion == 3)
                {
                    puede = true;
                }
                else
                {
                    puede = false;
                }
                break;
            case 21:
                if (direccion == 3)
                {
                    puede = true;
                }
                else
                {
                    puede = false;
                }
                break;
            case 22:
                if (direccion != 3)
                {
                    puede = true;
                }
                else
                {
                    puede = false;
                }
                break;
            case 23:
                if (direccion == 3)
                {
                    puede = true;
                }
                else
                {
                    puede = false;
                }
                break;
            case 24:
                if (direccion != 0)
                {
                    puede = true;
                }
                else
                {
                    puede = false;
                }
                break;
            case 25:
                if (direccion == 1)
                {
                    puede = true;
                }
                else
                {
                    puede = false;
                }
                break;
            case 26:
                if (direccion == 0 || direccion == 3)
                {
                    puede = true;
                }
                else
                {
                    puede = false;
                }
                break;
            case 27:
                if (direccion == 0 || direccion == 3)
                {
                    puede = true;
                }
                else
                {
                    puede = false;
                }
                break;
            case 28:
                if (direccion == 0 || direccion == 3)
                {
                    puede = true;
                }
                else
                {
                    puede = false;
                }
                break;
            case 29:
                if (direccion == 0)
                {
                    puede = true;
                }
                else
                {
                    puede = false;
                }
                break;
        }

        return puede;
    }



    void MueveIndicador(int direccion)
    {
        string nombre = "";

        switch (indice)
        {
            case 0:
                indice = 1;
                break;
            case 1:
                if (direccion == 0)
                {
                    indice = 2;
                }
                else
                {
                    indice = 0;
                }
                break;
            case 2:
                if(direccion == 3)
                {
                    indice = 1;
                }
                else if (direccion == 2)
                {
                    indice = 22;
                }
                else
                {
                    indice = 3;
                }
                break;
            case 3:
                if (direccion == 2)
                {
                    indice = 2;
                }
                else
                {
                    indice = 4;
                }
                break;
            case 4:
                if(direccion == 0)
                {
                    indice = 5;
                }
                else if(direccion == 1)
                {
                    indice = 14;
                }
                else if(direccion == 2)
                {
                    indice = 3;
                }
                else
                {
                    indice = 11;
                }
                break;
            case 5:
                if (direccion == 0)
                {
                    indice = 10;
                }
                else if (direccion == 1)
                {
                    indice = 8;
                }
                else if (direccion == 2)
                {
                    indice = 6;
                }
                else
                {
                    indice = 4;
                }
                break;
            case 6:
                if (direccion == 1)
                {
                    indice = 5;
                }
                else
                {
                    indice = 7;
                }
                break;
            case 7:
                indice = 6;
                break;
            case 8:
                if (direccion == 1)
                {
                    indice = 9;
                }
                else
                {
                    indice = 5;
                }
                break;
            case 9:
                indice = 8;
                break;
            case 10:
                indice = 5;
                break;
            case 11:
                if (direccion == 0)
                {
                    indice = 4;
                }
                else
                {
                    indice = 12;
                }
                break;
            case 12:
                if (direccion == 0)
                {
                    indice = 11;
                }
                else
                {
                    indice = 13;
                }
                break;
            case 13:
                indice = 12;
                break;
            case 14:
                if (direccion == 1)
                {
                    indice = 17;
                }
                else if(direccion == 2)
                {
                    indice = 4;
                }
                else
                {
                    indice = 15;
                }
                break;
            case 15:
                indice = 14;
                break;
            case 17:
                if (direccion == 1)
                {
                    indice = 18;
                }
                else
                {
                    indice = 14;
                }
                break;
            case 18:
                if (direccion == 1)
                {
                    indice = 19;
                }
                else
                {
                    indice = 17;
                }
                break;
            case 19:
                if (direccion == 0)
                {
                    indice = 20;
                }
                else
                {
                    indice = 18;
                }
                break;
            case 20:
                if (direccion == 0)
                {
                    indice = 21;
                }
                else
                {
                    indice = 19;
                }
                break;
            case 21:
                indice = 20;
                break;
            case 22:
                if (direccion == 0)
                {
                    indice = 23;
                }
                else if(direccion == 1)
                {
                    indice = 2;
                }
                else
                {
                    indice = 24;
                }
                break;
            case 23:
                indice = 22;
                break;
            case 24:
                if (direccion == 2)
                {
                    indice = 25;
                }
                else if(direccion == 1)
                {
                    indice = 22;
                }
                else
                {
                    indice = 26;
                }
                break;
            case 25:
                indice = 24;
                break;
            case 26:
                if (direccion == 0)
                {
                    indice = 24;
                }
                else
                {
                    indice = 27;
                }
                break;
            case 27:
                if (direccion == 0)
                {
                    indice = 26;
                }
                else
                {
                    indice = 28;
                }
                break;
            case 28:
                if (direccion == 0)
                {
                    indice = 27;
                }
                else
                {
                    indice = 29;
                }
                break;
            case 29:
                indice = 28;
                break;
        }

        if (baseDeDatos.zonaVisitada[indice])
        {
            nombre = baseDeDatos.NombreMapa(indice);
        }
        else
        {
            nombre = "???";
        }

        int pos = indice + 3;
        this.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = nombre;
        this.transform.GetChild(1).GetChild(1).position = this.transform.GetChild(1).GetChild(pos).position;
    }



    void CambiaControl()
    {
        if (viaje)
        {
            if (!baseDeDatos.mandoActivo)
            {
                baseDeDatos.mandoActivo = true;

                this.transform.GetChild(3).GetChild(1).GetComponent<Image>().sprite = baseDeDatos.seleccionXBOX[0];
                this.transform.GetChild(3).GetChild(3).GetComponent<Image>().sprite = baseDeDatos.moverXBOX[0];
            }
            else
            {
                baseDeDatos.mandoActivo = false;

                this.transform.GetChild(3).GetChild(1).GetComponent<Image>().sprite = baseDeDatos.seleccionPC[0];
                this.transform.GetChild(3).GetChild(3).GetComponent<Image>().sprite = baseDeDatos.moverPC[0];
            }
        }
        else
        {
            if (!baseDeDatos.mandoActivo)
            {
                baseDeDatos.mandoActivo = true;

                this.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = baseDeDatos.moverXBOX[0];
            }
            else
            {
                baseDeDatos.mandoActivo = false;

                this.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = baseDeDatos.moverPC[0];
            }
        }
    }



    IEnumerator ParpadeaIcono()
    {
        parpadea = true;
        this.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(0.75f);
        this.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.75f);
        parpadea = false;
    }
}
