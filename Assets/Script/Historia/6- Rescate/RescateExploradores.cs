using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RescateExploradores : MonoBehaviour
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
    bool mision;
    bool luchar;
    bool opcionElegida;
    bool eleccion;
    bool textoEleccion;

    float alpha;
    float fadeTime;

    public GameObject prota, protaAnim;
    public GameObject elementosAnim;
    public GameObject profesor;
    public GameObject soldados, alumno1, alumno2, alumno3, alumno4, soldadosCaidos;
    public GameObject fondoNegro;
    public GameObject textoMision;
    public GameObject personajesEscenario;

    GameObject manager;

    ControlJugador controlJugador;
    Camara camara;

    BaseDatos baseDeDatos;

    Animator profesorAnimador, protaAnimador, alumno1Animador, alumno2Animador, alumno3Animador, alumno4Animador, connorAnimador;

    bool pulsado;

    float digitalX;



    void Awake()
    {
        mostrado = false;

        protaAnimador = protaAnim.GetComponent<Animator>();
        protaAnim.SetActive(false);

        profesorAnimador = profesor.GetComponent<Animator>();
        profesor.SetActive(false);

        connorAnimador = soldados.transform.GetChild(0).GetComponent<Animator>();
        soldados.SetActive(false);

        alumno1Animador = alumno1.GetComponent<Animator>();
        alumno1.SetActive(false);

        alumno2Animador = alumno2.GetComponent<Animator>();
        alumno2.SetActive(false);

        alumno3Animador = alumno3.GetComponent<Animator>();
        alumno3.SetActive(false);

        alumno4Animador = alumno4.GetComponent<Animator>();
        alumno4.SetActive(false);

        soldadosCaidos.SetActive(false);
    }



    void Start()
    {
        manager = GameObject.Find("GameManager");
        baseDeDatos = manager.GetComponent<BaseDatos>();

        alpha = 0;
        fadeTime = 2f;
        numeroMensajes = 8;

        activo = false;
        pasaPrimerMensaje = false;
        mision = false;
        luchar = false;
        eleccion = false;
        textoEleccion = false;

        controlJugador = GameObject.Find("Player").GetComponent<ControlJugador>();
        camara = GameObject.Find("Main Camera").GetComponent<Camara>();

        textoMision.SetActive(false);

    }



    void Update()
    {
        if (!mostrado)
        {
            if (activo)
            {
                if (!eleccion)
                {
                    digitalX = Input.GetAxis("D-Horizontal");

                    if (pulsado)
                    {
                        if (digitalX == 0)
                        {
                            pulsado = false;
                        }
                    }

                    if (!mision)
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
                                if (posMensaje == 7 && luchar)
                                {
                                    StartCoroutine(SegundaAnimacion());
                                }
                                else if (posMensaje == 9 && luchar)
                                {
                                    StartCoroutine(TerceraAnimacion());
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
                                        CierraAnimacion();
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!opcionElegida)
                        {
                            if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                            {
                                opcionElegida = true;
                                mision = false;

                                posMensaje++;
                                //eleccion.SetActive(false);

                                if (posMensaje < numeroMensajes)
                                {
                                    MuestraSiguienteMensaje();
                                }
                            }
                            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || (!pulsado && digitalX != 0))
                            {
                                pulsado = true;
                                luchar = !luchar;

                                if (luchar)
                                {
                                    //eleccion.transform.GetChild(2).transform.position = eleccion.transform.GetChild(0).transform.GetChild(1).transform.position;
                                }
                                else
                                {
                                    //eleccion.transform.GetChild(2).transform.position = eleccion.transform.GetChild(1).transform.GetChild(1).transform.position;
                                }
                            }
                        }
                        else
                        {
                            mision = false;
                        }
                    }
                }
                else
                {
                    if (TextBox.menuActivo)
                    {
                        if (!opcionElegida)
                        {
                            if (Input.GetKeyDown(KeyCode.B) || Input.GetButtonUp("X"))
                            {
                                for (int i = 0; i < baseDeDatos.numeroMisionesActivas; i++)
                                {
                                    if (baseDeDatos.listaMisionesActivas[i].indice == 15)
                                    {
                                        baseDeDatos.listaMisionesActivas[i] = baseDeDatos.listaMisiones[16];
                                    }
                                }

                                for (int i = 0; i < baseDeDatos.numeroMisionesPrincipales; i++)
                                {
                                    if (baseDeDatos.listaMisionesPrincipales[i].indice == 15)
                                    {
                                        baseDeDatos.listaMisionesPrincipales[i] = baseDeDatos.listaMisiones[16];
                                    }
                                }
                                opcionElegida = true;
                                luchar = true;
                                TextBox.OcultaEleccion();
                            }
                            else if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("B"))
                            {
                                opcionElegida = true;
                                luchar = false;
                                TextBox.OcultaEleccion();
                            }
                        }
                    }
                    else
                    {
                        if (opcionElegida)
                        {
                            if (!textoEleccion)
                            {
                                textoEleccion = true;
                                posMensaje++;
                                MuestraTextoEleccion();
                            }
                            else
                            {
                                if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                                {
                                    eleccion = false;

                                    MuestraSiguienteMensaje();
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (baseDeDatos.numeroMisionesPrincipales > 5)
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
        if(baseDeDatos.idioma == 1)
        {
            switch (posMensaje)
            {
                case 0:
                    mensaje = "This is a disaster. They are all very badly injured. This was almost never supposed to be anything.";
                    emisor = "Student 1";

                    break;
                case 1:
                    mensaje = "We must get out of here ... I'm going to throw up. Also, it is possible that the thing that did this was still here and they are real soldiers. I just signed up and it was because of the coolness of the uniform.";
                    emisor = "Student 2";

                    break;
                case 2:
                    mensaje = "You are a coward. We can't leave them lying here.";
                    emisor = "Student 3";

                    break;
                case 3:
                    mensaje = "I couldn't agree more with the lady. I have a better idea, why don't you join them? HA HA HA HA";
                    emisor = "???";
                    profesorAnimador.Play("Andar-1");
                    protaAnimador.Play("Idle-Up");

                    break;
                case 4:
                    mensaje = "Ru...  run...  runaway fast. This is too... too much for you. Leave us here. We are already suspended...";
                    emisor = "Injured";
                    break;
                case 5:
                    mensaje = "I agree with this guy. Let's all hurry out of here. ¡Ruuuuuuuuuuuuuuun!";
                    emisor = "Student 4";
                    StartCoroutine(EsperaHuida());

                    break;
                case 6:
                    emisor = "???";
                    mensaje = "HA HA HA HA. Is this the fearsome power of the Resistance? You are nothing more than little children who play heroes and villains without understanding anything ... Oh, there is still one. What are you going to do, are you going to run like your friends or are you going to be an idiot and face me alone?";

                    break;
                case 7:
                    emisor = "???";

                    if (luchar)
                    {
                        numeroMensajes = 11;
                        mensaje = "So you are another unconscious that pretends to face me. Expulsion will be the least of your problems after this, get ready to fight tadpole.";
                    }
                    else
                    {
                        mensaje = "HA HA HA HA. Another coward who runs away. Run little rat, your moment is coming, just extend the inevitable...";
                        protaAnimador.Play("Huir");
                    }
                    break;
                case 8:
                    mensaje = "Not so fast idiot. Why don't you mess with someone your size to see if you're equally brave? We have you surrounded, although I am just enough to kick your ass without problems.";
                    emisor = "???";
                    break;
                case 9:
                    emisor = "???";
                    mensaje = "Captain Connor, how long without seeing us here. Although I would love to accept your offer, I am afraid that I will have to postpone our meeting for another time.";
                    break;
                case 10:
                    emisor = "Connor";
                    mensaje = "I knew he would run away. Soldiers take the injured fast. And you, the redhead. Go back to the base. The boss will want to talk to you after what you just did. My report will be on the table by then.";
                    break;
            }
        }
        else
        {
            switch (posMensaje)
            {
                case 0:
                    mensaje = "Esto es un desastre. Están todos muy mal heridos. Se suponía que esto casi nunca era nada.";
                    emisor = "Alumno 1";
                    break;
                case 1:
                    mensaje = "Debemos salir cagando leches de aquí... Me están dando ganas incluso de vomitar. Además, es posible que la cosa que hiciera esto siguiera por aquí y ellos son soldados de verdad. Yo apenas me acabo de apuntar y ha sido por lo molón del uniforme.";
                    emisor = "Alumno 2";
                    break;
                case 2:
                    mensaje = "Eres un gallina. No podemos dejarlos aquí tirados como si nada.";
                    emisor = "Alumna 3";
                    break;
                case 3:
                    mensaje = "No podía estar más de acuerdo con la señorita. Se me ocurre una idea mejor, ¿por qué no os unís a ellos? JA JA JA JA";
                    emisor = "???";
                    profesorAnimador.Play("Andar-1");
                    protaAnimador.Play("Idle-Up");
                    break;
                case 4:
                    mensaje = "Hu...  huid...  huid rápido. Este es dem... demasiado para vosotros. Dejadnos aquí. Nosotros ya estamos suspensos...";
                    emisor = "Herido";
                    break;
                case 5:
                    mensaje = "Yo estoy de acuerdo con este tipo. Salgamos todos a toda prisa de aquí. ¡Correeeeeeeeeeeeeeed!";
                    emisor = "Alumna 4";
                    StartCoroutine(EsperaHuida());
                    break;
                case 6:
                    emisor = "???";
                    mensaje = "JA JA JA JA. ¿Es este el temible poder de La Resistencia? No sois más que unos niñatos que jugáis a héroes y villanos sin entender nada... Vaya si queda uno todavía. ¿Tú que vas a hacer, vas a correr como tus amigos o vas a ser tan idiota como para enfrentarte a mí solo?";
                    break;
                case 7:
                    emisor = "???";

                    if (luchar)
                    {
                        numeroMensajes = 11;
                        mensaje = "Así que eres otro inconsciente que pretende enfrentarme. La expulsión será el menor de tus problemas tras esto, prepárate para luchar renacuajo. ";
                    }
                    else
                    {
                        mensaje = "JA JA JA JA. Otro cobarde que huye. Corre pequeña rata, tu momento está por llegar, solo alargas lo inevitable...";
                        protaAnimador.Play("Huir");
                    }
                    break;
                case 8:
                    mensaje = "No tan rápido idiota. ¿Por qué no te metes con alguien de tu tamaño a ver si eres igual de valiente? Te tenemos rodeado, aunque yo sola me basto para patearte el culo sin problemas.";
                    emisor = "???";
                    break;
                case 9:
                    emisor = "???";
                    mensaje = "Capitana Connor, cuanto tiempo sin vernos por aquí. Aunque me encantaría aceptar tu oferta me temo que voy a tener que aplazar nuestro encuentro para otro momento.";
                    break;
                case 10:
                    emisor = "Connor";
                    mensaje = "Sabía que saldría corriendo. Soldados llevaros rápido a los heridos. Y tú, el pelirrojo. Vuelve a la base. El jefazo querrá hablar contigo después de lo que acabas de hacer. Mi informe estará sobre la mesa para entonces.";
                    break;
            }
        }

        if (posMensaje == 6)
        {
            TextBox.MuestraTextoConEleccion(mensaje, emisor, 2);
            eleccion = true;
        }
        else
        {
            TextBox.MuestraTextoHistoria(mensaje, emisor);
        }
    }



    void MuestraTextoEleccion()
    {
        emisor = baseDeDatos.listaPersonajesAliados[0].nombre;

        if (baseDeDatos.idioma == 1)
        {
            if (luchar)
            {
                mensaje = "Te doy a dar hasta en el carnet de identidad.";
            }
            else
            {
                mensaje = "Yo ya me iba...";
            }
        }
        else //español
        {
            if (luchar)
            {
                mensaje = "Te doy a dar hasta en el carnet de identidad.";
            }
            else
            {
                mensaje = "Yo ya me iba...";
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
        elementosAnim.SetActive(false);
        baseDeDatos.ActivaMapas(13);
        baseDeDatos.indiceObjetivo = 13;

        fondoNegro.SetActive(true);
        FadeIn();

        yield return new WaitForSeconds(fadeTime);

        fondoNegro.SetActive(false);
        FadeOut();

        //menuMision.SetActive(false);

        if (luchar)
        {
            baseDeDatos.CumpleMision(16);
        }
        else
        {
            baseDeDatos.CumpleMision(15);
        }

        controlJugador.setMensajeActivo(false);

        activo = false;

        TextBox.OcultaTextoFinCombate();

        camara.TerminaHistoria();
        camara.FijaCamara(30, 6);

        personajesEscenario.SetActive(true);

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



    IEnumerator Inicia()
    {
        controlJugador.setMensajeActivo(true);
        fondoNegro.SetActive(true);
        personajesEscenario.SetActive(false);

        prota.transform.position = new Vector3(-137, -632, prota.transform.position.z);

        FadeIn();
        camara.IniciaHistoria(7);

        elementosAnim.SetActive(true);
        protaAnim.SetActive(true);
        profesor.SetActive(true);
        alumno1.SetActive(true);
        alumno2.SetActive(true);
        alumno3.SetActive(true);
        alumno4.SetActive(true);
        soldadosCaidos.SetActive(true);

        yield return new WaitForSeconds(fadeTime);

        fondoNegro.SetActive(false);
        FadeOut();

        yield return new WaitForSeconds(1.5f);

        IniciaTexto();
    }



    IEnumerator SegundaAnimacion()
    {
        activo = false;
        fondoNegro.SetActive(true);
        soldados.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        fondoNegro.SetActive(false);

        posMensaje++;

        MuestraSiguienteMensaje();

        activo = true;
    }



    IEnumerator TerceraAnimacion()
    {
        activo = false;
        fondoNegro.SetActive(true);
        profesor.SetActive(false);

        yield return new WaitForSeconds(1);

        fondoNegro.SetActive(false);
        connorAnimador.Play("Anda");

        yield return new WaitForSeconds(1.2f);

        posMensaje++;

        MuestraSiguienteMensaje();

        activo = true;
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (!mostrado)
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



    IEnumerator EsperaHuida()
    {
        yield return new WaitForSeconds(0.7f);

        alumno1Animador.Play("Huir");
        alumno2Animador.Play("Huir");
        alumno3Animador.Play("Huir");
        alumno4Animador.Play("Huir");

        yield return new WaitForSeconds(1);

        alumno1.SetActive(false);
        alumno2.SetActive(false);
        alumno3.SetActive(false);
        alumno4.SetActive(false);
    }
}
