using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlIdioma : MonoBehaviour {
    int pos; // 0 --> Español   1 --> Inglés
    int numeroIdiomas;

    public GameObject flecha;
    GameObject manager;

    public GameObject interfaz;

    Scene scene;
    Intermediario pantallaCarga;

    bool activo;
    bool iniciado;
    bool idiomaCambiado;
    bool pulsado;

    float digitalX;
    float digitalY;

    bool mandoActivo;

    Sprite[] seleccionPC;
    Sprite[] moverPC;
    Sprite[] seleccionXBOX;
    Sprite[] moverXBOX;



    void Start ()
    {
        moverPC = Resources.LoadAll<Sprite>("Sprites/Interfaz/botones/PC/4direcciones");
        seleccionPC = Resources.LoadAll<Sprite>("Sprites/Interfaz/botones/PC/N");
        moverXBOX = Resources.LoadAll<Sprite>("Sprites/Interfaz/botones/XBOX/Mover");
        seleccionXBOX = Resources.LoadAll<Sprite>("Sprites/Interfaz/botones/XBOX/A");

        pos = 0;
        numeroIdiomas = 2;
        flecha.transform.position = this.transform.GetChild(pos).transform.GetChild(2).transform.position;
        activo = true;
        interfaz.transform.GetChild(0).GetComponent<Image>().sprite = seleccionPC[0];
        interfaz.transform.GetChild(1).GetComponent<Text>().text = "Seleccionar";
        interfaz.transform.GetChild(2).GetComponent<Image>().sprite = moverPC[0];
        interfaz.transform.GetChild(3).GetComponent<Text>().text = "Mover";
        DontDestroyOnLoad(gameObject);
        iniciado = false;
        idiomaCambiado = false;
        cambiaControl();
    }



    void Update ()
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

            if (mandoActivo)
            {
                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.N) || Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.Escape) || 
                    Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    cambiaControl();
                }
            }
            else
            {
                if(Input.GetButtonUp("A") || Input.GetButtonUp("B") || Input.GetButtonUp("X") || Input.GetButtonUp("Y") || Input.GetButtonUp("Start") || Input.GetButtonUp("Select") || (digitalY != 0) || (digitalX != 0))
                {
                    cambiaControl();
                }
            }

            if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
            {
                SceneManager.LoadScene("Titulo Juego");

                activo = false;

                this.transform.GetChild(0).gameObject.SetActive(false);
                this.transform.GetChild(1).gameObject.SetActive(false);
                this.transform.GetChild(2).gameObject.SetActive(false);
                this.transform.GetChild(3).gameObject.SetActive(false);
                this.transform.GetChild(4).gameObject.SetActive(false);
        }
            else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || (!pulsado && digitalY > 0))
            {
                pulsado = true;
                pos++;

                if (pos == numeroIdiomas)
                {
                    pos = 0;
                }

                MueveFlecha();
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || (!pulsado && digitalY < 0))
            {
                pulsado = true;

                pos--;

                if (pos == -1)
                {
                    pos = numeroIdiomas - 1;
                }

                MueveFlecha();
            }
        }
        else
        {
            if (!iniciado)
            {
                if (scene.name == "Portada")
                {
                    manager = GameObject.Find("CanvasCarga");
                    iniciado = true;
                }
                else if(scene.name == "Titulo Juego")
                {
                    if (!idiomaCambiado)
                    {
                        manager = GameObject.Find("Titulo");
                        CambiaIdiomaPantallaTitulo();
                        idiomaCambiado = true;
                    }
                }
                else
                {
                    scene = SceneManager.GetActiveScene();
                }
            }
            else
            {
                pantallaCarga = manager.GetComponent<Intermediario>();
                pantallaCarga.idioma = pos;
                pantallaCarga.mandoActivo = mandoActivo;
                pantallaCarga.tengoValor = true;
                Destroy(gameObject);
            }
        }
    }



    void MueveFlecha()
    {
        flecha.transform.position = this.transform.GetChild(pos).transform.GetChild(2).transform.position;

        switch (pos)
        {
            case 0:
                interfaz.transform.GetChild(1).GetComponent<Text>().text = "Seleccionar";
                interfaz.transform.GetChild(3).GetComponent<Text>().text = "Mover";
                this.transform.GetChild(4).GetComponent<Text>().text = "Selecciona tu idioma";
                break;
            case 1:
                interfaz.transform.GetChild(1).GetComponent<Text>().text = "Select";
                interfaz.transform.GetChild(3).GetComponent<Text>().text = "Move";
                this.transform.GetChild(4).GetComponent<Text>().text = "Select your language";
                break;
        }
    }



    void CambiaIdiomaPantallaTitulo()
    {
        switch(pos)
        {
            case 0:
                manager.transform.GetChild(1).GetComponent<Text>().text = "Presiona cualquier botón";
                break;
            case 1:
                manager.transform.GetChild(1).GetComponent<Text>().text = "Press any key to continue";
                break;
        }
    }



    void cambiaControl()
    {
        if (mandoActivo)
        {
            mandoActivo = false;

            interfaz.transform.GetChild(0).GetComponent<Image>().sprite = seleccionPC[0];
            interfaz.transform.GetChild(2).GetComponent<Image>().sprite = moverPC[0];
        }
        else
        {
            mandoActivo = true;

            interfaz.transform.GetChild(0).GetComponent<Image>().sprite = seleccionXBOX[0];
            interfaz.transform.GetChild(2).GetComponent<Image>().sprite = moverXBOX[0];
        }
    }
}
