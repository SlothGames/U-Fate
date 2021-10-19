using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cofre : MonoBehaviour {
    public string openState;

    public Image menu;
    public Image[] iconoObjeto;

    public Text[] nombreObjeto;
    public Text[] cantidad;

    GameObject manager;
    BaseDatos baseDeDatos;
    ControlJugador controlJugador;
    MusicaManager musica;

    Animator anim;

    bool abierto;
    public bool usado;
    bool activo;

    public int[] indiceObjetos;
    public int[] cantidadEnCofre;
    public int indice;
    public int tipo;

    float digitalX;
    float digitalY;


    void Start()
    {
        anim = GetComponent<Animator>();
        manager = GameObject.Find("GameManager");
        baseDeDatos = manager.GetComponent<BaseDatos>();
        controlJugador = GameObject.Find("Player").GetComponent<ControlJugador>();
        menu.gameObject.SetActive(false);
        musica = GameObject.Find("EfectosSonido").GetComponent<MusicaManager>();

        abierto = false;
        usado = false;

        for (int i = 0; i < 4; i++)
        {
            iconoObjeto[i].gameObject.SetActive(false);
            nombreObjeto[i].gameObject.SetActive(false);
            cantidad[i].gameObject.SetActive(false);
        }
    }



    void AbrirCofre()
    {
        if (!abierto)
        {
            digitalX = Input.GetAxis("D-Horizontal");
            digitalY = Input.GetAxis("D-Vertical");

            bool puede = PuedeAbrirCofre();
            
            if (puede)
            {
                if (baseDeDatos.mandoActivo)
                {
                    menu.transform.GetChild(5).GetComponent<Image>().sprite = baseDeDatos.seleccionXBOX[0];
                }
                else
                {
                    menu.transform.GetChild(5).GetComponent<Image>().sprite = baseDeDatos.seleccionPC[0];
                }

                Animacion();
                abierto = true;

                musica.ProduceEfecto(13);

                StartCoroutine(EsperaMensaje());
                controlJugador.SetInterrogante(false);

                for (int i = 0; i < indiceObjetos.Length; i++)
                {
                    baseDeDatos.IncluirEnInventario(indiceObjetos[i], cantidadEnCofre[i]);
                }
            }
            else
            {
                controlJugador.setMensajeActivo(false);
            }
        }
    }



    void Update()
    {
        if (!usado)
        {
            if (activo)
            {
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

                if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                {
                    for (int i = 0; i < indiceObjetos.Length; i++)
                    {
                        iconoObjeto[i].gameObject.SetActive(false);
                        nombreObjeto[i].gameObject.SetActive(false);
                        cantidad[i].gameObject.SetActive(false);
                    }

                    menu.gameObject.SetActive(false);
                    controlJugador.setMensajeActivo(false);

                    usado = true;
                    baseDeDatos.AbreCofre(indice);
                }
            }
            else
            {
                if (baseDeDatos.cofres[indice])
                {
                    usado = true;
                    abierto = true;
                    Animacion();
                }
            }
        }
    }



    public void Animacion()
    {
        anim.Play(openState);
    }



    bool PuedeAbrirCofre()
    {
        bool puede = false;

        if (tipo == 0)
        {
            puede = true;
        }
        else if (tipo == 1)
        {
            if (baseDeDatos.cofres[35]) //este valor hay que cambiarlo
            {
                puede = true;
            }
        }
        else if (tipo == 2)
        {
            if (baseDeDatos.cofres[35]) //este valor hay que cambiarlo
            {
                puede = true;
            }
        }
        else
        {
            if (baseDeDatos.cofres[35]) //este valor hay que cambiarlo
            {
                puede = true;
            }
        }

        return puede;
    }



    IEnumerator EsperaMensaje()
    {
        if (baseDeDatos.idioma == 1)
        {
            menu.transform.GetChild(0).GetComponent<Text>().text = "Collected Items";
            menu.transform.GetChild(6).GetComponent<Text>().text = "Continue";
        }
        else
        {
            menu.transform.GetChild(0).GetComponent<Text>().text = "Objetos Logrados";
            menu.transform.GetChild(6).GetComponent<Text>().text = "Continuar";
        }

        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < indiceObjetos.Length; i++)
        {
            iconoObjeto[i].gameObject.SetActive(true);
            iconoObjeto[i].sprite = baseDeDatos.listaObjetos[indiceObjetos[i]].icono;
            nombreObjeto[i].gameObject.SetActive(true);

            if (baseDeDatos.idioma == 1)
            {
                nombreObjeto[i].text = baseDeDatos.listaObjetos[indiceObjetos[i]].nombreIngles;
            }
            else
            {
                nombreObjeto[i].text = baseDeDatos.listaObjetos[indiceObjetos[i]].nombre;
            }

            cantidad[i].gameObject.SetActive(true);
            cantidad[i].text = "" + cantidadEnCofre[i];
        }

        menu.gameObject.SetActive(true);

        if (baseDeDatos.mandoActivo)
        {
            menu.transform.GetChild(5).GetComponent<Image>().sprite = baseDeDatos.seleccionXBOX[0];
        }
        else
        {
            menu.transform.GetChild(5).GetComponent<Image>().sprite = baseDeDatos.seleccionPC[0];
        }


        yield return new WaitForSeconds(0.2f);

        activo = true;
    }



    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (!abierto)
        {
            if (other.CompareTag("Player"))
            {
                controlJugador.SetInterrogante(true);
            }
        }

        yield return null;
    }



    private IEnumerator OnTriggerExit2D(Collider2D other)
    {
        if (!abierto)
        {
            if (other.CompareTag("Player"))
            {
                controlJugador.SetInterrogante(false);
            }
        }

        yield return null;
    }



    void cambiaControl()
    {
        if (!baseDeDatos.mandoActivo)
        {
            baseDeDatos.mandoActivo = true;

            menu.transform.GetChild(1).GetComponent<Image>().sprite = baseDeDatos.seleccionXBOX[0];
        }
        else
        {
            baseDeDatos.mandoActivo = false;

            menu.transform.GetChild(1).GetComponent<Image>().sprite = baseDeDatos.seleccionPC[0];
        }

        print(baseDeDatos.mandoActivo);
    }
}
