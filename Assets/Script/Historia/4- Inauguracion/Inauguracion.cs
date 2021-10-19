using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inauguracion : MonoBehaviour
{
    int posMensaje;
    int numeroMensajes;

    string mensaje;
    string emisor;

    bool mostrado;//true si esta parte de la historia ya ha sido mostrada
    bool activo;
    bool pasaPrimerMensaje;
    bool start = false;
    bool isFadeIn = false;
    bool primeraParte, segundaParte, terceraParte;

    float alpha;
    float fadeTime;

    public GameObject prota, protaAnim;
    public GameObject saraAnim;
    public GameObject rebeldes, cupula;
    public GameObject profesor;
    public GameObject fondoNegro, fondoTranslucido;
    public GameObject textoMision;
    public GameObject elementosAnimacion;
    public GameObject medico;
    public GameObject soldado1, soldado2;
    GameObject manager;
    public GameObject escenaFin;

    GameOver finJuego;

    ControlJugador controlJugador;
    Camara camara;

    Animator saraAnimador;
    Animator profAnimador;
    Animator protaAnimador;

    BaseDatos baseDeDatos;
    NuevoTablero tablero;
    MusicaManager musica;

    void Awake()
    {
        profesor.transform.GetChild(0).gameObject.SetActive(false);
        mostrado = false;
        rebeldes.SetActive(false);
        elementosAnimacion.SetActive(false);
        fondoTranslucido.SetActive(false);
        medico.SetActive(false);
    }



    void Start()
    {
        manager = GameObject.Find("GameManager");
        baseDeDatos = manager.GetComponent<BaseDatos>();
        tablero = manager.GetComponent<NuevoTablero>();
        musica = GameObject.Find("Musica").GetComponent<MusicaManager>();

        alpha = 0;
        fadeTime = 2f;
        numeroMensajes = 14;

        activo = false;
        pasaPrimerMensaje = false;
        primeraParte = segundaParte = terceraParte = false;

        controlJugador = GameObject.Find("Player").GetComponent<ControlJugador>();
        camara = GameObject.Find("Main Camera").GetComponent<Camara>();

        saraAnimador = saraAnim.GetComponent<Animator>();
        profAnimador = profesor.GetComponent<Animator>();
        protaAnimador = protaAnim.GetComponent<Animator>();

        textoMision.SetActive(false);

        finJuego = escenaFin.transform.GetComponent<GameOver>();
    }



    void Update()
    {
        if (!mostrado)
        {
            if (!tablero.combateActivo) 
            {
                if (!finJuego.activo)
                {
                    if (activo)
                    {
                        if (primeraParte)
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
                                    if (posMensaje == 6)
                                    {
                                        StartCoroutine(SegundaAnimacion());
                                    }
                                    else
                                    {
                                        posMensaje++;

                                        if (posMensaje < numeroMensajes)
                                        {
                                            MuestraSiguienteMensaje();
                                        }
                                        else
                                        {
                                            IniciaCombate();
                                        }
                                    }
                                }
                            }
                        }
                        else if (segundaParte)
                        {
                            if (!TextBox.ocultar)
                            {
                                if (pasaPrimerMensaje)
                                {
                                    pasaPrimerMensaje = false;

                                    camara.IniciaHistoria(4);

                                    MuestraSiguienteMensaje();
                                }
                            }
                            else
                            {
                                if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                                {
                                    StartCoroutine(TercerActo());
                                }
                            }
                        }
                        else if (terceraParte)
                        {
                            if (!TextBox.ocultar)
                            {
                                if (pasaPrimerMensaje)
                                {
                                    pasaPrimerMensaje = false;

                                    MuestraSiguienteMensaje();
                                }
                            }
                            else
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
                        if (baseDeDatos.numeroMisionesPrincipales > 3)
                        {
                            mostrado = true;
                        }
                    }
                }
            }
            else
            {
                if(posMensaje != 0)
                {
                    segundaParte = true;
                    activo = true;
                    pasaPrimerMensaje = true;
                    cupula.SetActive(false);
                }
            }
        }
        else
        {
            StartCoroutine(DestruyeObjeto());
            soldado1.SetActive(false);
            soldado2.SetActive(false);
        }
    }



    void MuestraSiguienteMensaje()
    {
        if (primeraParte)
        {
            if(baseDeDatos.idioma == 1)
            {
                switch (posMensaje)
                {
                    case 0:
                        emisor = "Professor";
                        mensaje = "Well, I think we are all we should. Welcome to everyone at the beginning of a new course at the best university in the world. The great University of Áncia.";
                        break;
                    case 1:
                        emisor = "Professor";
                        mensaje = "Here begins your journey to become the leaders of the future. Scientists, ambassadors, architects, artists, doctors ... Everything you've ever dreamed of is possible between these walls full of years of knowledge.";
                        break;
                    case 2:
                        emisor = "Professor";
                        mensaje = "You just have to let us guide you on your way with our knowledge of the subjects you wish to study as many other students have done course after course before you. You have already made great progress in getting the scholarships that give you access to our prestigious institution.";
                        break;
                    case 3:
                        emisor = "Professor";
                        mensaje = "However, one more step is necessary to finish the entry process and it is thanks to this machine that we have here. You must go past her as we call you. This will complete the registration...";
                        break;
                    case 4:
                        emisor = "Crowd";
                        mensaje = "* Murmur *   * Murmur *    * Murmur *    * Murmur *    * Murmur *";
                        break;
                    case 5:
                        emisor = "Sara";
                        mensaje = "No one had mentioned anything about an extra process. What a strange thing.";
                        break;
                    case 6:
                        emisor = "Professor";
                        profesor.transform.GetChild(0).gameObject.SetActive(true);
                        fondoTranslucido.SetActive(true);
                        mensaje = "What happened to the lights?! Keep calm, it is solved immediately.";
                        break;
                    case 7:
                        emisor = "???";
                        profesor.transform.GetChild(0).gameObject.SetActive(false);
                        profAnimador.Play("Idle-Left");
                        mensaje = "We will not allow you to do what you want. You have tried to cross a line that we do not even imagine you would cross. Listen to me all, this has been a trap. They are trying to wash your brain with that machine. You must get away as much as you can.";
                        break;
                    case 8:
                        emisor = "Professor";
                        mensaje = "Who are you supposed to be?";
                        break;
                    case 9:
                        emisor = "???";
                        mensaje = "We are the Resistance and we are going to free these people from your tyrannical claws.";
                        break;
                    case 10:
                        emisor = "Professor";
                        mensaje = "Guardians stop them all, let no one come out!";
                        break;
                    case 11:
                        emisor = "???";
                        mensaje = "Members of the Resistance, attack!";
                        break;
                    case 12:
                        emisor = "Sara";
                        saraAnimador.Play("Idle-Left");
                        protaAnimador.Play("Idle-Right");
                        mensaje = " I don't like the look this is taking. We must get out of here.";
                        break;
                    case 13:
                        emisor = "Soldado";
                        mensaje = "Take those two over there. They are trying to run away.";
                        break;
                }
            }
            else
            {
                switch (posMensaje)
                {
                    case 0:
                        emisor = "Profesor";
                        mensaje = "Bueno, creo que ya estamos todos los que debíamos. Bienvenidos a todos y todas al inicio de un nuevo curso en la mejor universidad del mundo. La gran Universidad de Áncia.";
                        break;
                    case 1:
                        emisor = "Profesor";
                        mensaje = "Solo debéis dejar que os guiemos en vuestro camino con nuestros conocimientos sobre las materias que deseáis estudiar como muchos otros alumnos han hecho curso tras curso antes que vosotros. Ya habéis logrado avanzar mucho al conseguir las becas que os dan acceso a nuestra prestigiosa institución.";
                        break;
                    case 2:
                        emisor = "Profesor";
                        mensaje = "Solo debéis dejar que os guiemos en vuestro camino con nuestros conocimientos sobre las materias que deseáis estudiar como muchos otros alumnos han hecho curso tras curso antes que vosotros. Ya habéis logrado avanzar mucho al conseguir las becas que os dan acceso a nuestra prestigiosa institución.";
                        break;
                    case 3:
                        emisor = "Profesor";
                        mensaje = "Sin embargo es necesario un paso más para terminar el proceso de entrada y es gracias a esta máquina que tenemos aquí. Deberéis ir pasando por delante de ella según os vayamos llamando. Con esto se completará el registro...";
                        break;
                    case 4:
                        emisor = "Multitud";
                        mensaje = "* Murmullo *   * Murmullo *    * Murmullo *    * Murmullo *    * Murmullo *";
                        break;
                    case 5:
                        emisor = "Sara";
                        mensaje = "Nadie había mencionado nada de un proceso extra. Qué cosa más rara.";
                        break;
                    case 6:
                        emisor = "Profesor";
                        profesor.transform.GetChild(0).gameObject.SetActive(true);
                        fondoTranslucido.SetActive(true);
                        mensaje = "¡¿Qué le ha pasado a las luces?! Mantened todos la calma, seguro que se soluciona de inmediato.";
                        break;
                    case 7:
                        emisor = "???";
                        profesor.transform.GetChild(0).gameObject.SetActive(false);
                        profAnimador.Play("Idle-Left");
                        mensaje = "No vamos a permitir que os salgáis con la vuestra. Habéis intentado cruzar una raya que ni nosotros imaginamos que traspasaríais. Escucharme todos, esto ha sido una trampa. Están intentando lavaros el cerebro con esa máquina. Debéis alejaros todo lo que podáis de ella.";
                        break;
                    case 8:
                        emisor = "Profesor";
                        mensaje = "¿Quién se supone que sois vosotros?";
                        break;
                    case 9:
                        emisor = "???";
                        mensaje = "Somos la Resistencia y vamos a liberar a esta gente de vuestras tiránicas garras.";
                        break;
                    case 10:
                        emisor = "Profesor";
                        mensaje = "¡Guardas detenedlos a todos, que no salga nadie!";
                        break;
                    case 11:
                        emisor = "???";
                        mensaje = "Miembros de la Resistencia, a por ellos.";
                        break;
                    case 12:
                        emisor = "Sara";
                        saraAnimador.Play("Idle-Left");
                        protaAnimador.Play("Idle-Right");
                        mensaje = "No me gusta la pinta que está tomando esto. Debemos abrirnos paso y salir de aquí.";
                        break;
                    case 13:
                        emisor = "Soldado";
                        mensaje = "Coged a esos dos de ahí. Están intentando huir.";
                        break;
                }
            }
        }
        else if (segundaParte)
        {
            if(baseDeDatos.idioma == 1)
            {
                emisor = "???";
                protaAnimador.Play("Idle-Left");
                mensaje = "We are done with all the guards. We must leave quickly before more come. Take everyone to the Base. Quick...";
            }
            else
            {
                emisor = "???";
                protaAnimador.Play("Idle-Left");
                mensaje = "Ya hemos terminado con todos los guardias. Debemos salir rápido antes de que vengan más. Llevad a todos a la Base. Rápido...";
            }
        }
        else
        {
            if (baseDeDatos.idioma == 1)
            {
                emisor = "Medic";
                mensaje = "Everything is in order. The general awaits you and a group in his office. It is just the room next to this.";
            }
            else
            {
                emisor = "Médico";
                mensaje = "Está todo en orden. El general os espera a ti y un grupo en su despacho. Es justo la habitación al lado de esta.";
            }
        }

        TextBox.MuestraTextoHistoria(mensaje, emisor);
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
        baseDeDatos.indiceObjetivo = 13;
        fondoNegro.SetActive(true);
        FadeIn();
        saraAnim.SetActive(false);

        yield return new WaitForSeconds(fadeTime);

        fondoNegro.SetActive(false);
        FadeOut();

        controlJugador.setMensajeActivo(false);

        activo = false;

        TextBox.OcultaTextoFinCombate();
        medico.SetActive(false);

        camara.TerminaHistoria();
        camara.FijaCamara(30, 8);

        textoMision.SetActive(true);

        if (baseDeDatos.idioma == 0)
        {
            textoMision.transform.GetChild(1).GetComponent<Text>().text = "Misión Completada";
        }
        else if (baseDeDatos.idioma == 1)
        {
            textoMision.transform.GetChild(1).GetComponent<Text>().text = "Mission complete";
        }

        yield return new WaitForSeconds(2);

        textoMision.SetActive(false);
    }



    IEnumerator PrimeraAnimacion()
    {
        controlJugador.setMensajeActivo(true);
        fondoNegro.SetActive(true);
        elementosAnimacion.SetActive(true);
        primeraParte = true;

        prota.transform.position = new Vector3(-82, -655, prota.transform.position.z);

        FadeIn();
        camara.IniciaHistoria(4);

        yield return new WaitForSeconds(fadeTime);

        profAnimador.Play("Idle-Down");
        saraAnimador.Play("Idle-Up");
        protaAnimador.Play("Idle-Up");

        fondoNegro.SetActive(false);
        FadeOut();

        yield return new WaitForSeconds(1.5f);

        profAnimador.Play("Inauguracion1");

        yield return new WaitForSeconds(6.4f);

        profAnimador.Play("Idle-Down");

        IniciaTexto();
    }



    IEnumerator SegundaAnimacion()
    {
        activo = false;
        fondoNegro.SetActive(true);
        rebeldes.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        fondoTranslucido.SetActive(false);
        fondoNegro.SetActive(false);
        
        posMensaje++;

        MuestraSiguienteMensaje();

        activo = true;
    }



    IEnumerator TercerActo()
    {
        FadeIn();
        segundaParte = false;

        yield return new WaitForSeconds(fadeTime);

        elementosAnimacion.SetActive(false);
        musica.CambiaCancion(9);
        camara.IniciaHistoria(5);
        medico.SetActive(true);
        baseDeDatos.ActivaMapas(13);
        FadeOut();

        yield return new WaitForSeconds(1.5f);

        terceraParte = true;
        pasaPrimerMensaje = true;
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (!mostrado)
        {
            if (other.CompareTag("Player"))
            {
                baseDeDatos.CumpleMision(14);

                StartCoroutine(PrimeraAnimacion());
            }
        }
    }



    void IniciaCombate()
    {
        primeraParte = false;
        activo = false;
        
        AnimacionCombate.IniciaCombate();
        TextBox.OcultaTextoFinCombate();
        camara.TerminaHistoria();
        tablero.IniciarCombate(baseDeDatos.BuscarPersonajeIndice(27, false), baseDeDatos.BuscarPersonajeIndice(27, false), null, 2, 12, true, 4, -1);
    }



    IEnumerator DestruyeObjeto()
    {
        yield return new WaitForSeconds(8);
        
        Destroy(gameObject);
    }
}
