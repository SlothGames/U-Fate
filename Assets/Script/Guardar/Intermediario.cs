using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intermediario : MonoBehaviour
{
    bool activo;
    bool iniciado;
    public static bool partidaNueva;
    public static bool cargar;
    bool enEspera;
    bool idiomaCargado;
    public bool tengoValor;
    public bool mandoActivo;
    public static string nombre;

    //private static Intermediario intermediario = null;
    
    public int idioma; //0 --> español   1 --> ingles
    public static int idiomaEstatico;
    public static int dificultad;
    public static int ficheroCarga;
     
    Scene scene;
    GameObject manager;
    GameObject menu;
    PantallaCarga pantallaCarga;

    Sprite[] seleccionPC;
    Sprite[] moverPC;
    Sprite[] volverPC;
    Sprite[] seleccionXBOX;
    Sprite[] moverXBOX;
    Sprite[] volverXBOX;

    void Awake ()
    {
        moverPC = Resources.LoadAll<Sprite>("Sprites/Interfaz/botones/PC/4direcciones");
        seleccionPC = Resources.LoadAll<Sprite>("Sprites/Interfaz/botones/PC/N");
        moverXBOX = Resources.LoadAll<Sprite>("Sprites/Interfaz/botones/XBOX/Mover");
        seleccionXBOX = Resources.LoadAll<Sprite>("Sprites/Interfaz/botones/XBOX/A");
        volverPC = Resources.LoadAll<Sprite>("Sprites/Interfaz/botones/PC/M");
        volverXBOX = Resources.LoadAll<Sprite>("Sprites/Interfaz/botones/XBOX/B");

        partidaNueva = cargar = false;
        enEspera = false;
        iniciado = false;
        tengoValor = false;
        DesactivaIntermediario();
        DontDestroyOnLoad(gameObject);
        menu = GameObject.Find("Menu");
	}



    void Update()
    {
        idiomaEstatico = idioma;
        
        if (idiomaEstatico == 1)
        {
            this.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = "Loading...";
        }
        else
        {
            this.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = "Cargando...";
        }

        if (activo)
        {
            if (!iniciado)
            {
                if (scene.name == "Juego")
                {
                    manager = GameObject.Find("InterfazCombate");
                    iniciado = true;
                }
                else
                {
                    scene = SceneManager.GetActiveScene();
                }
            }
            else
            {
                pantallaCarga = manager.transform.GetChild(6).GetComponent<PantallaCarga>();

                if (cargar)
                {
                    pantallaCarga.ActivaCarga(ficheroCarga);
                }
                else if (partidaNueva)
                {
                    pantallaCarga.DesactivaCarga(partidaNueva);
                }

                pantallaCarga.EstableceConfig(idioma, mandoActivo, nombre, dificultad);

                DesactivaIntermediario();
                Destroy(gameObject);
            }
        }
        else
        {
            if (partidaNueva || cargar)
            {
                if (!enEspera)
                {
                    IniciaIntermediario();
                }
            }
        }

        if (tengoValor)
        {
            if (!idiomaCargado)
            {
                if (idioma == 0)
                {
                    menu.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = "NUEVA PARTIDA";
                    menu.transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().text = "CARGAR PARTIDA";
                    menu.transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = "CONTROLES";
                    menu.transform.GetChild(0).transform.GetChild(4).GetComponent<Text>().text = "OPCIONES";
                    menu.transform.GetChild(0).transform.GetChild(5).GetComponent<Text>().text = "CREDITOS";
                    menu.transform.GetChild(0).transform.GetChild(6).GetComponent<Text>().text = "SALIR";

                    menu.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "AJUSTES";
                    menu.transform.GetChild(1).transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = "RESOLUCIÓN";
                    menu.transform.GetChild(1).transform.GetChild(0).transform.GetChild(2).transform.GetChild(1).GetComponent<Text>().text = "PANTALLA COMPLETA";
                    menu.transform.GetChild(1).transform.GetChild(0).transform.GetChild(3).transform.GetChild(0).GetComponent<Text>().text = "VOLUMEN";
                    menu.transform.GetChild(1).transform.GetChild(0).transform.GetChild(4).transform.GetChild(0).GetComponent<Text>().text = "VOLVER";
                    menu.transform.GetChild(1).transform.GetChild(0).transform.GetChild(10).transform.GetChild(0).GetComponent<Text>().text = "¿Quieres guardar los cambios?";
                    menu.transform.GetChild(1).transform.GetChild(0).transform.GetChild(10).transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = "Sí";
                    menu.transform.GetChild(1).transform.GetChild(0).transform.GetChild(10).transform.GetChild(3).transform.GetChild(0).GetComponent<Text>().text = "No";

                    menu.transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = "University RPG \n\n Escrito, diseñado y desarrollado: \n Jorge Juan Ñíguez Fernández \n\n Música: \n Tema Principal: Spectacular Sound Productions Fly A Kite by Spectacular Sound \n\n Musica ambiental: Tale on the Late, It's time for adventure by Komiku \n\n Esta música se ha obtenido a través de la página  freemusicarchive.org bajo licencia CC \n\n Arte: \n FINALBOSSBLUES y BLODYAVENGER comprado en gamedevmarket.net";

                    menu.transform.GetChild(4).transform.GetChild(1).GetComponent<Text>().text = "Seleccionar";
                    menu.transform.GetChild(4).transform.GetChild(3).GetComponent<Text>().text = "Atrás";
                    menu.transform.GetChild(4).transform.GetChild(5).GetComponent<Text>().text = "Mover";
                    menu.transform.GetChild(4).transform.GetChild(7).GetComponent<Text>().text = "Borrar";
                    menu.transform.GetChild(4).transform.GetChild(9).GetComponent<Text>().text = "Usa el teclado para introducir tu nombre, Enter para terminar y Esc para volver";

                    menu.transform.GetChild(5).transform.GetChild(0).GetComponent<Text>().text = "Partidas Guardadas";

                    //Dificultad
                    menu.transform.GetChild(6).transform.GetChild(0).GetComponent<Text>().text = "Selecciona Dificultad";
                    menu.transform.GetChild(6).transform.GetChild(2).GetChild(0).GetComponent<Text>().text = "Fácil";
                    menu.transform.GetChild(6).transform.GetChild(3).GetChild(0).GetComponent<Text>().text = "Intermedio";
                    menu.transform.GetChild(6).transform.GetChild(4).GetChild(0).GetComponent<Text>().text = "Difícil";
                    menu.transform.GetChild(6).transform.GetChild(5).GetChild(0).GetComponent<Text>().text = "Titán";

                    //Nombre
                    menu.transform.GetChild(8).transform.GetChild(0).GetComponent<Text>().text = "Introduce tu nombre";
                    menu.transform.GetChild(8).transform.GetChild(3).GetComponent<Text>().text = "Nombre:";

                    //Controles
                    menu.transform.GetChild(3).transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "Seleccionar, interactuar, confirmar";
                    menu.transform.GetChild(3).transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Cancelar, rechazar, correr";
                    menu.transform.GetChild(3).transform.GetChild(2).GetChild(1).GetComponent<Text>().text = "Mover personaje y flecha opciones";
                    menu.transform.GetChild(3).transform.GetChild(3).GetChild(1).GetComponent<Text>().text = "Menú Pausa";
                }
                else if (idioma == 1)
                {
                    menu.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = "NEW GAME";
                    menu.transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().text = "LOAD GAME";
                    menu.transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = "CONTROLS";
                    menu.transform.GetChild(0).transform.GetChild(4).GetComponent<Text>().text = "OPTIONS";
                    menu.transform.GetChild(0).transform.GetChild(5).GetComponent<Text>().text = "CREDITS";
                    menu.transform.GetChild(0).transform.GetChild(6).GetComponent<Text>().text = "EXIT";

                    menu.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "SETTINGS";
                    menu.transform.GetChild(1).transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = "RESOLUTION";
                    menu.transform.GetChild(1).transform.GetChild(0).transform.GetChild(2).transform.GetChild(1).GetComponent<Text>().text = "FULL SCREEN";
                    menu.transform.GetChild(1).transform.GetChild(0).transform.GetChild(3).transform.GetChild(0).GetComponent<Text>().text = "VOLUME";
                    menu.transform.GetChild(1).transform.GetChild(0).transform.GetChild(4).transform.GetChild(0).GetComponent<Text>().text = "BACK";
                    menu.transform.GetChild(1).transform.GetChild(0).transform.GetChild(10).transform.GetChild(0).GetComponent<Text>().text = "Do you want save changes?";
                    menu.transform.GetChild(1).transform.GetChild(0).transform.GetChild(10).transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = "Yes";
                    menu.transform.GetChild(1).transform.GetChild(0).transform.GetChild(10).transform.GetChild(3).transform.GetChild(0).GetComponent<Text>().text = "No";

                    menu.transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = "University RPG \n\n Write, design and develop: \n Jorge Juan Ñíguez Fernández \n\n Music: \n Main Theme: Spectacular Sound Productions Fly A Kite by Spectacular Sound \n\n Ambient music: Tale on the Late, It's time for adventure by Komiku \n\n This music has been obtained through the page freemusicarchive.org under CC license \n\n Art: \n FINALBOSSBLUES and BLODYAVENGER buy on gamedevmarket.net";

                    menu.transform.GetChild(4).transform.GetChild(1).GetComponent<Text>().text = "Select";
                    menu.transform.GetChild(4).transform.GetChild(3).GetComponent<Text>().text = "Back";
                    menu.transform.GetChild(4).transform.GetChild(5).GetComponent<Text>().text = "Move";
                    menu.transform.GetChild(4).transform.GetChild(7).GetComponent<Text>().text = "Delete";
                    menu.transform.GetChild(4).transform.GetChild(9).GetComponent<Text>().text = "Use the keyboard to introduce your name, Enter to finish and Esc to come back";

                    menu.transform.GetChild(5).transform.GetChild(0).GetComponent<Text>().text = "Saved Files";

                    //Dificultad
                    menu.transform.GetChild(6).transform.GetChild(0).GetComponent<Text>().text = "Select Level";
                    menu.transform.GetChild(6).transform.GetChild(2).GetChild(0).GetComponent<Text>().text = "Easy";
                    menu.transform.GetChild(6).transform.GetChild(3).GetChild(0).GetComponent<Text>().text = "Medium";
                    menu.transform.GetChild(6).transform.GetChild(4).GetChild(0).GetComponent<Text>().text = "Hard";
                    menu.transform.GetChild(6).transform.GetChild(5).GetChild(0).GetComponent<Text>().text = "Titan";

                    //Nombre
                    menu.transform.GetChild(8).transform.GetChild(0).GetComponent<Text>().text = "Introduce your name";
                    menu.transform.GetChild(8).transform.GetChild(3).GetComponent<Text>().text = "Name:";

                    //Controles
                    menu.transform.GetChild(3).transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "Select, interact, confirm";
                    menu.transform.GetChild(3).transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Cancel, refuse, run";
                    menu.transform.GetChild(3).transform.GetChild(2).GetChild(1).GetComponent<Text>().text = "Move character and options arrow";
                    menu.transform.GetChild(3).transform.GetChild(3).GetChild(1).GetComponent<Text>().text = "Pause menu";
                }

                if (mandoActivo)
                {
                    menu.transform.GetChild(4).transform.GetChild(2).GetComponent<Image>().sprite = seleccionXBOX[0];
                    menu.transform.GetChild(4).transform.GetChild(4).GetComponent<Image>().sprite = volverXBOX[0];
                    menu.transform.GetChild(4).transform.GetChild(6).GetComponent<Image>().sprite = moverXBOX[0];
                }
                else
                {
                    menu.transform.GetChild(4).transform.GetChild(2).GetComponent<Image>().sprite = seleccionPC[0];
                    menu.transform.GetChild(4).transform.GetChild(4).GetComponent<Image>().sprite = volverPC[0];
                    menu.transform.GetChild(4).transform.GetChild(6).GetComponent<Image>().sprite = moverPC[0];
                }
                
                idiomaCargado = true;
            }
        }
    }



    public void IniciaIntermediario()
    {
        iniciado = false;
        this.transform.GetChild(0).gameObject.SetActive(true);
        StartCoroutine(Espera());
    }



    public void DesactivaIntermediario()
    {
        activo = false;
        partidaNueva = false;
        cargar = false;
        this.transform.GetChild(0).gameObject.SetActive(activo);
    }



    IEnumerator Espera()
    {
        enEspera = true;
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Juego");
        yield return new WaitForSeconds(1);
        activo = true;
        enEspera = false;
    }


    public void setIdioma(int valor)
    {
        idioma = valor;
    }
}
