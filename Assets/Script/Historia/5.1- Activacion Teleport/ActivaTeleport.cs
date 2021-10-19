using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivaTeleport : MonoBehaviour
{
    int posMensaje;

    string mensaje;
    string emisor;

    bool mostrado;//true si esta parte de la historia ya ha sido mostrada
    bool activo;
    bool pasaPrimerMensaje;
    bool start = false;
    bool isFadeIn = false;
    bool termina;

    float alpha;
    float fadeTime;

    public GameObject prota;
    public GameObject teleport;
    public GameObject fondoNegro;
    GameObject manager;

    Animator teleportAnimador;

    ControlJugador controlJugador;
    Camara camara;

    BaseDatos baseDeDatos;


    void Awake()
    {
        mostrado = false;
    }



    void Start()
    {
        manager = GameObject.Find("GameManager");
        baseDeDatos = manager.GetComponent<BaseDatos>();

        alpha = 0;
        fadeTime = 2f;

        activo = false;
        pasaPrimerMensaje = false;
        termina = false;

        controlJugador = GameObject.Find("Player").GetComponent<ControlJugador>();
        camara = GameObject.Find("Main Camera").GetComponent<Camara>();

        teleportAnimador = teleport.GetComponent<Animator>();

        teleport.SetActive(false);
    }



    void Update()
    {
        if (!mostrado)
        {
            if (activo)
            {
                if (!termina)
                {
                    if (pasaPrimerMensaje)
                    {
                        pasaPrimerMensaje = false;

                        MuestraSiguienteMensaje();
                    }
                    else if (TextBox.ocultar)
                    {
                        if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                        {
                            posMensaje++;

                            MuestraSiguienteMensaje();
                        }
                    }
                }
                else
                {
                    if (TextBox.ocultar)
                    {
                        if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                        {
                            CierraAnimacion();
                        }
                    }
                }
            }
            else
            {
                if (baseDeDatos.teleportActivo)
                {
                    mostrado = true;
                }
            }
        }
        else
        {
            StartCoroutine(DestruyeObjeto());
        }
    }



    void MuestraSiguienteMensaje()
    {
        emisor = "Transportista";

        if (baseDeDatos.idioma == 1)
        {
            emisor = "Teleporter";

            switch (posMensaje)
            {
                case 0:
                    mensaje = "Welcome, welcome ... Let me introduce myself. I am a member of the Magic Teleporter Union.";
                    break;
                case 1:
                    mensaje = "We are announcing to all the people of Pedrán that our services are already available for those who wish. However, we have not yet established portals in all cities.";
                    break;
                case 2:
                    mensaje = "But do not worry. We will expand our services to reach farther places.";
                    break;
                case 3:
                    mensaje = "If you want to use our service, talk to any member of the Union in cities and towns throughout the Empire. Here in Pedrán you will find it to the north of the city, just behind the headquarters of the Imperial Guard.";
                    break;
            }
        }
        else
        {
            switch (posMensaje)
            {
                case 0:
                    mensaje = "Bienvenido, bienvenido... Permítame que me presente. Soy miembro del gremio de transportistas mágicos. ";
                    break;
                case 1:
                    mensaje = "Estamos anunciando a toda la gente de Pedrán que nuestros servicios ya están disponibles para aquellos que lo deseen. Sin embargo, aún no hemos logrado instaurar portales en todas las ciudades.";
                    break;
                case 2:
                    mensaje = "Pero no se preocupe. Poco a poco iremos ampliando nuestros servicios para alcanzar sitios aún más lejanos.";
                    break;
                case 3:
                    mensaje = "Si desea hacer uso de nuestro servicio hable con cualquier miembro del gremio en ciudades y pueblos a lo largo y ancho del Imperio. Aquí en Pedrán lo encontrará al norte de la ciudad, justo detrás de la sede de la Guardia Imperial.";
                    break;
            }
        }

        TextBox.MuestraTextoHistoria(mensaje, emisor);

        if (posMensaje == 3)
        {
            termina = true;
        }
    }



    void IniciaTexto()
    {
        activo = true;
        pasaPrimerMensaje = true;
    }



    void CierraAnimacion()
    {
        mostrado = true;
        StartCoroutine(Termina());
    }



    void OnGUI()
    {
        if (!start)
            return;

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);

        Texture2D tex;
        tex = new Texture2D(1, 1);
        tex.SetPixel(0, 0, Color.black);
        tex.Apply();

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), tex);

        if (isFadeIn)
        {
            alpha = Mathf.Lerp(alpha, 1.1f, fadeTime * Time.deltaTime);
        }
        else
        {
            alpha = Mathf.Lerp(alpha, -0.1f, fadeTime * Time.deltaTime);

            if (alpha < 0)
            {
                start = false;
            }

        }
    }



    void FadeIn()
    {
        start = true;
        isFadeIn = true;
    }



    void FadeOut()
    {
        isFadeIn = false;
    }



    IEnumerator Termina()
    {
        baseDeDatos.indiceObjetivo = 10;
        fondoNegro.SetActive(true);
        FadeIn();
        teleport.SetActive(false);

        yield return new WaitForSeconds(fadeTime);

        fondoNegro.SetActive(false);
        FadeOut();

        controlJugador.setMensajeActivo(false);
        baseDeDatos.teleportActivo = true;
        activo = false;

        TextBox.OcultaTextoFinCombate();

        camara.TerminaHistoria();
    }



    IEnumerator Inicia()
    {
        controlJugador.setMensajeActivo(true);
        teleport.SetActive(true);
        fondoNegro.SetActive(true);

        FadeIn();
        camara.IniciaHistoria(8);

        yield return new WaitForSeconds(fadeTime);

        fondoNegro.SetActive(false);
        FadeOut();
        teleportAnimador.Play("ActivaTeleport");

        yield return new WaitForSeconds(1.5f);

        IniciaTexto();
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (!mostrado && baseDeDatos.numeroMisionesPrincipales == 4)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(Inicia());
            }
        }
    }



    IEnumerator DestruyeObjeto()
    {
        yield return new WaitForSeconds(8);
        Destroy(gameObject);
    }
}
