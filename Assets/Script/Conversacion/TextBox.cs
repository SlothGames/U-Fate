using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBox : MonoBehaviour 
{
    BaseDatos baseDeDatos;
    GameObject manager;
    MusicaManager musica;

    public static TextBox textBox;

    public static bool on = false;
    public static bool ocultar = false;
    public static bool apagar = false;
    bool combateActivo = false;
    bool misionActiva = false;
    bool eleccionActiva = false;
    public static bool menuActivo;
    bool historia;
    bool rapido;

    public Image menuMision;
    //public Image flecha;

    int numeroElecciones;
    public int indiceMision;

    //public GameObject pos0;
    public GameObject aceptar;
    public GameObject nombre;
    public GameObject menuEleccion;

    Text textTB;

    string[] elecciones;
    string mensajesConversacion;

    float digitalX;
    float digitalY;


    void Awake()
    {
        if(textBox == null)
            textBox = this;

        textTB = GetComponentInChildren<Text>();
        textBox.gameObject.SetActive(on);
        historia = false;
        rapido = false;

        menuActivo = false;
        TextBox.textBox.menuMision.gameObject.SetActive(menuActivo);
        OcultaEleccion();

        manager = GameObject.Find("GameManager");
        baseDeDatos = manager.GetComponent<BaseDatos>();
        musica = GameObject.Find("EfectosSonido").GetComponent<MusicaManager>();
    }



    public static void MuestraTexto(string t, bool combate)
    {
        //TextBox.textBox.musica.ProduceEfecto(27);
        TextBox.textBox.combateActivo = combate;
        TextBox.textBox.Escribir(t);
        TextBox.textBox.nombre.SetActive(false);
        TextBox.textBox.historia = false;
        TextBox.textBox.eleccionActiva = false;
    }



    public static void MuestraTextoHistoria(string t, string personaje)
    {
        //TextBox.textBox.musica.ProduceEfecto(27);
        TextBox.textBox.historia = true;
        TextBox.textBox.nombre.SetActive(true);
        TextBox.textBox.nombre.transform.GetChild(0).GetComponent<Text>().text = personaje;
        TextBox.textBox.Escribir(t);
        TextBox.textBox.eleccionActiva = false;
    }



    public static void MuestraTextoHistoriaMision(string t, string personaje, int indice)
    {
        //TextBox.textBox.musica.ProduceEfecto(27);
        TextBox.textBox.historia = true;
        TextBox.textBox.nombre.SetActive(true);
        TextBox.textBox.nombre.transform.GetChild(0).GetComponent<Text>().text = personaje;
        TextBox.textBox.Escribir(t);
        TextBox.textBox.misionActiva = true;
        TextBox.textBox.combateActivo = false;
        TextBox.textBox.indiceMision = indice;
        TextBox.textBox.eleccionActiva = false;
    }



    public static void MuestraTextoConMision(string t, bool combate, bool mision, int indice)
    {
        //TextBox.textBox.musica.ProduceEfecto(27);
        TextBox.textBox.historia = false;
        TextBox.textBox.combateActivo = combate;
        TextBox.textBox.indiceMision = indice;
        TextBox.textBox.misionActiva = mision;
        TextBox.textBox.Escribir(t);
        TextBox.textBox.nombre.SetActive(false);
        TextBox.textBox.eleccionActiva = false;
    }



    public static void MuestraTextoConEleccion(string t, string personaje, int indice)
    {
        //TextBox.textBox.musica.ProduceEfecto(27);
        TextBox.textBox.historia = false;
        TextBox.textBox.indiceMision = indice;
        TextBox.textBox.Escribir(t);
        TextBox.textBox.nombre.SetActive(true);
        TextBox.textBox.nombre.transform.GetChild(0).GetComponent<Text>().text = personaje;
        TextBox.textBox.eleccionActiva = true;
        TextBox.textBox.EstableceElecciones();
    }



    public void Escribir(string t)
    {
        on = true;
        ocultar = false;
        textBox.gameObject.SetActive(on);
        //textBox.textTB.text = t;
        mensajesConversacion = t;
        
        StartCoroutine(Deletrear(t));
    }



    public IEnumerator Deletrear(string t)
    {
        textBox.textTB.text = "";
        rapido = false;

        yield return new WaitForSeconds(.1f);

        for (int i = 0; i < t.Length + 1; i++)
        {
            if (!rapido)
            {
                textBox.textTB.text = t.Substring(0, i);
                yield return new WaitForSeconds(.01f);
            }
        }
        
        if ( t == textBox.textTB.text)
        {
            ocultar = true;

            if (eleccionActiva)
            {
                MuestraEleccion();
            }
        }
    }



    public void OcultarTexto()
    {
        on = false;
        ocultar = false;
        combateActivo = false;
        textBox.gameObject.SetActive(on);
    }



    public void OcultaMenuMision()
    {
        OcultarTexto();
        menuActivo = false;
        misionActiva = false;
        menuMision.gameObject.SetActive(menuActivo);
    }



    void MuestraMenuMision()
    {
        menuActivo = true;
        menuMision.gameObject.SetActive(menuActivo);

        //flecha.transform.position = pos0.transform.position;
        aceptar.SetActive(true);

        if(baseDeDatos.idioma == 1)
        {
            menuMision.transform.GetChild(0).GetComponent<Text>().text = baseDeDatos.listaMisiones[indiceMision].tituloIngles;
            menuMision.transform.GetChild(1).GetComponent<Text>().text = baseDeDatos.listaMisiones[indiceMision].descripcionIngles;
        }
        else
        {
            menuMision.transform.GetChild(0).GetComponent<Text>().text = baseDeDatos.listaMisiones[indiceMision].titulo;
            menuMision.transform.GetChild(1).GetComponent<Text>().text = baseDeDatos.listaMisiones[indiceMision].descripcion;
        }
        

        string aux = "";
        int longitud = baseDeDatos.listaMisiones[indiceMision].recompensas.Length;

        for (int i = 0; i < longitud; i++)
        {
            if(baseDeDatos.idioma == 1)
            {
                aux += baseDeDatos.listaMisiones[indiceMision].recompensasIngles[i];
            }
            else
            {
                aux += baseDeDatos.listaMisiones[indiceMision].recompensas[i];
            }

            if (i != (longitud-1))
            {
                aux += "\n";
            }
        }

        menuMision.transform.GetChild(2).GetComponent<Text>().text = aux;

        if(baseDeDatos.idioma == 1)
        {
            menuMision.transform.GetChild(4).GetChild(1).GetComponent<Text>().text = "Accept";
        }
        else
        {
            menuMision.transform.GetChild(4).GetChild(1).GetComponent<Text>().text = "Aceptar";
        }
    }



    void MuestraEleccion()
    {
        menuEleccion.SetActive(true);
        menuActivo = true;

        menuEleccion.transform.GetChild(0).gameObject.SetActive(true);
        menuEleccion.transform.GetChild(1).gameObject.SetActive(true);
        menuEleccion.transform.GetChild(2).gameObject.SetActive(true);

        if (baseDeDatos.mandoActivo)
        {
            menuEleccion.transform.GetChild(2).GetComponent<Image>().sprite = baseDeDatos.yXBOX[0];
            menuEleccion.transform.GetChild(0).GetComponent<Image>().sprite = baseDeDatos.xXBOX[0];
            menuEleccion.transform.GetChild(1).GetComponent<Image>().sprite = baseDeDatos.volverXBOX[0];
        }
        else
        {
            menuEleccion.transform.GetChild(2).GetComponent<Image>().sprite = baseDeDatos.vPC[0];
            menuEleccion.transform.GetChild(0).GetComponent<Image>().sprite = baseDeDatos.bPC[0];
            menuEleccion.transform.GetChild(1).GetComponent<Image>().sprite = baseDeDatos.volverPC[0];
        }

        if (numeroElecciones == 2)
        {
            menuEleccion.transform.GetChild(2).gameObject.SetActive(false);
        }
    }



    public static void OcultaEleccion()
    {
        TextBox.textBox.eleccionActiva = false;
        menuActivo = false;
        TextBox.textBox.menuEleccion.SetActive(false);
    }



    void Update()
    {
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

        if (on)
        {
            digitalX = Input.GetAxis("D-Horizontal");
            digitalY = Input.GetAxis("D-Vertical");
        }

        if (!eleccionActiva)
        {
            if (!historia)
            {
                if (!combateActivo)
                {
                    if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                    {
                        if(ocultar || apagar)
                        {
                            musica.ProduceEfecto(26);
                            rapido = false;

                            if (!menuActivo)
                            {
                                if (!misionActiva)
                                {
                                    OcultarTexto();
                                    apagar = false;
                                }
                                else
                                {
                                    MuestraMenuMision();
                                }
                            }
                            else
                            {
                                baseDeDatos.IncluyeMision(indiceMision);
                                OcultaMenuMision();
                            }
                        }
                        else if(textBox.textTB.text != "")
                        {
                            rapido = true;
                            textBox.textTB.text = mensajesConversacion;
                        }
                    }
                }
            }
            else
            {
                if (Input.GetButtonUp("A") || Input.GetKeyDown(KeyCode.N))
                {
                    if(ocultar || apagar)
                    {
                        rapido = false;
                        musica.ProduceEfecto(26);

                        if (!menuActivo)
                        {
                            if (!misionActiva)
                            {
                                
                                OcultarTexto();
                                apagar = false;
                            }
                            else
                            {
                                MuestraMenuMision();
                            }
                        }
                        else
                        {
                            baseDeDatos.IncluyeMision(indiceMision);
                            OcultaMenuMision();
                        }
                    }
                    else if (textBox.textTB.text != "")
                    {
                        rapido = true;
                        textBox.textTB.text = mensajesConversacion;
                    }
                }
            }
        }
    }



    public static void OcultaTextoFinCombate()
    {
        TextBox.textBox.OcultarTexto();
    }



    void EstableceElecciones()
    {
        switch (indiceMision)
        {
            case 0: //Taberna El paso
                numeroElecciones = 2;
                elecciones = new string[numeroElecciones];

                if(baseDeDatos.idioma == 1)
                {
                    elecciones[0] = "I think it's a good idea to go together.";
                    elecciones[1] = "The truth is that I prefer to go alone.";
                }
                else
                {
                    elecciones[0] = "Creo que es buena idea que vayamos juntos";
                    elecciones[1] = "Lo cierto es que prefiero ir solo";
                }
                
                break;
            case 1: //Guardia 1
                numeroElecciones = 2;
                elecciones = new string[numeroElecciones];

                if (baseDeDatos.idioma == 1)
                {
                    elecciones[0] = "I think it's a good idea.";
                    elecciones[1] = "I don't think it will happen.";
                }
                else
                {
                    elecciones[0] = "Me parece buena idea.";
                    elecciones[1] = "Va a ser que no.";
                }

                break;
            case 2: //Rescate
                numeroElecciones = 2;
                elecciones = new string[numeroElecciones];

                if (baseDeDatos.idioma == 1)
                {
                    elecciones[0] = " I'm going to give you a spanking.";
                    elecciones[1] = "I was leaving...";
                }
                else
                {
                    elecciones[0] = "Te doy a dar hasta en el carnet de identidad.";
                    elecciones[1] = "Yo ya me iba...";
                }

                break;
            case 3: //Despacho General 2
                numeroElecciones = 2;
                elecciones = new string[numeroElecciones];

                if (baseDeDatos.idioma == 1)
                {
                    elecciones[0] = "Long live the Resistance!";
                    elecciones[1] = "I don't want to be part.";
                }
                else
                {
                    elecciones[0] = "¡Viva la Resistencia!";
                    elecciones[1] = "No quiero formar parte.";
                }

                break;
            case 4: //Prueba Luis 1
                numeroElecciones = 2;
                elecciones = new string[numeroElecciones];

                if (baseDeDatos.idioma == 1)
                {
                    elecciones[0] = "Let's train!";
                    elecciones[1] = "I don't have time for it.";
                }
                else
                {
                    elecciones[0] = "¡Vamos a entrenar!";
                    elecciones[1] = "No tengo tiempo para ello.";
                }

                break;
            case 5: //Reclutamiento Tob
                numeroElecciones = 2;
                elecciones = new string[numeroElecciones];

                if (baseDeDatos.idioma == 1)
                {
                    elecciones[0] = "Sure, what do you need?";
                    elecciones[1] = "I do not want to do it.";
                }
                else
                {
                    elecciones[0] = "Claro, ¿qué necesitas?";
                    elecciones[1] = "No me apetece.";
                }

                break;
            case 6: //Mision Pedro
                numeroElecciones = 2;
                elecciones = new string[numeroElecciones];

                if (baseDeDatos.idioma == 1)
                {
                    elecciones[0] = "Sure, what do you need?";
                    elecciones[1] = "I do not want to do it.";
                }
                else
                {
                    elecciones[0] = "Claro, ¿qué necesitas?";
                    elecciones[1] = "No me apetece.";
                }

                break;
            case 7://Ladrón Gema Gámez
                numeroElecciones = 3;
                elecciones = new string[numeroElecciones];

                if (baseDeDatos.idioma == 1)
                {
                    elecciones[0] = "I'm going to take the gem.";
                    elecciones[1] = "I was leaving...";
                    elecciones[2] = "We can make an agreement.";
                }
                else
                {
                    elecciones[0] = "Me voy a llevar la gema.";
                    elecciones[1] = "Yo ya me iba...";
                    elecciones[2] = "Podemos llegar a un acuerdo.";
                }
                break;
            case 8: //Gámez
                numeroElecciones = 2;
                elecciones = new string[numeroElecciones];

                if (baseDeDatos.idioma == 1)
                {
                    elecciones[0] = "I will help the forest.";
                    elecciones[1] = "This forest seems buildable.";
                }
                else
                {
                    elecciones[0] = "Ayudaré al bosque.";
                    elecciones[1] = "Este bosque parece edificable.";
                }

                break;
            case 9: //Mision Nani-Orco
                numeroElecciones = 3;
                elecciones = new string[numeroElecciones];

                if (baseDeDatos.idioma == 1)
                {
                    elecciones[0] = "Prepare yourself to pay for your sins.";
                    elecciones[1] = "I think it's a good suggestion.";
                    elecciones[2] = "You destroyed Canda.";
                }
                else
                {
                    elecciones[0] = "Prepárate para pagar por tus pecados.";
                    elecciones[1] = "Creo que es una buena sugerencia.";
                    elecciones[2] = "Tú destruiste Canda.";
                }
                break;
            case 10: //Misión Nani-Orco 2
                numeroElecciones = 2;
                elecciones = new string[numeroElecciones];

                if (baseDeDatos.idioma == 1)
                {
                    elecciones[0] = "Prepare yourself to pay for your sins.";
                    elecciones[1] = "Keep talking.";
                }
                else
                {
                    elecciones[0] = "Prepárate para pagar por tus pecados.";
                    elecciones[1] = "Sigue hablando.";
                }

                break;
            case 11: //Misión Nani-Orco 3
                numeroElecciones = 2;
                elecciones = new string[numeroElecciones];

                if (baseDeDatos.idioma == 1)
                {
                    elecciones[0] = "Prepare yourself to pay for your sins.";
                    elecciones[1] = "Go away, the culprit was the hooded.";
                }
                else
                {
                    elecciones[0] = "Prepárate para pagar por tus pecados.";
                    elecciones[1] = "Vete, el culpable fue el encapuchado.";
                }

                break;
            case 12:
                numeroElecciones = 2;
                elecciones = new string[numeroElecciones];

                if (baseDeDatos.idioma == 1)
                {
                    elecciones[0] = "I will make them pay.";
                    elecciones[1] = "I don't think I can help you.";
                }
                else
                {
                    elecciones[0] = "Les haré pagar.";
                    elecciones[1] = "No creo poder ayudarte.";
                }
                break;
            case 13:
                numeroElecciones = 2;
                elecciones = new string[numeroElecciones];

                if (baseDeDatos.idioma == 1)
                {
                    elecciones[0] = "This will be your last drink.";
                    elecciones[1] = " I'm sorry, I was leaving.";
                }
                else
                {
                    elecciones[0] = "Esta será tu última bebida.";
                    elecciones[1] = "Lo siento, ya me iba.";
                }
                break;
        }

        if (numeroElecciones == 2)
        {
            menuEleccion.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = elecciones[0];
            menuEleccion.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = elecciones[1];
        }
        else
        {
            menuEleccion.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = elecciones[0];
            menuEleccion.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = elecciones[1];
            menuEleccion.transform.GetChild(2).GetChild(1).GetComponent<Text>().text = elecciones[2];
        }
    }



    void CambiaControl()
    {
        if (!baseDeDatos.mandoActivo)
        {
            baseDeDatos.mandoActivo = true;

            menuEleccion.transform.GetChild(2).GetComponent<Image>().sprite = baseDeDatos.yXBOX[0];
            menuEleccion.transform.GetChild(0).GetComponent<Image>().sprite = baseDeDatos.xXBOX[0];
            menuEleccion.transform.GetChild(1).GetComponent<Image>().sprite = baseDeDatos.volverXBOX[0];
        }
        else
        {
            baseDeDatos.mandoActivo = false;
            
            menuEleccion.transform.GetChild(2).GetComponent<Image>().sprite = baseDeDatos.vPC[0];
            menuEleccion.transform.GetChild(0).GetComponent<Image>().sprite = baseDeDatos.bPC[0];
            menuEleccion.transform.GetChild(1).GetComponent<Image>().sprite = baseDeDatos.volverPC[0];
        }
    }
}
