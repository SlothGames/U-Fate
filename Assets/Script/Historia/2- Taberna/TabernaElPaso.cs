using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabernaElPaso : MonoBehaviour 
{
    int posMensaje;

    string mensaje;
    string emisor;

    bool mostrado;//true si esta parte de la historia ya ha sido mostrada
    bool activo;
    bool pasaPrimerMensaje;
    bool start = false;
    bool isFadeIn = false;
    bool mision;
    bool aceptar;
    bool opcionElegida;
    bool eleccion;
    bool textoEleccion;

    float alpha;
    float fadeTime;

    public GameObject prota;
    public GameObject saraAnim, posIni, posFin;
    public GameObject fondoNegro;
    public GameObject textoMision;
    public GameObject soldado1, soldado2;

    GameObject manager;

    ControlJugador controlJugador;
    Camara camara;

    Animator saraAnimador;

    BaseDatos baseDeDatos;



    void Awake()
    {
        mostrado = false;
        textoEleccion = false;
    }



    void Start()
    {
        manager = GameObject.Find("GameManager");
        baseDeDatos = manager.GetComponent<BaseDatos>();

        alpha = 0;
        fadeTime = 2f;

        saraAnim.SetActive(false);

        activo = false;
        pasaPrimerMensaje = false;
        mision = false;
        aceptar = false;

        controlJugador = GameObject.Find("Player").GetComponent<ControlJugador>();
        camara = GameObject.Find("Main Camera").GetComponent<Camara>();

        saraAnimador = saraAnim.GetComponent<Animator>();

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
                                posMensaje++;

                                MuestraSiguienteMensaje();
                            }
                        }
                    }
                    else
                    {
                        if (TextBox.ocultar && TextBox.menuActivo)
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
                    if (TextBox.menuActivo)
                    {
                        if (!opcionElegida)
                        {
                            if (Input.GetKeyDown(KeyCode.B) || Input.GetButtonUp("X"))
                            {
                                opcionElegida = true;
                                aceptar = true;
                                TextBox.OcultaEleccion();
                            }
                            else if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("B"))
                            {
                                opcionElegida = true;
                                aceptar = false;
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
                                MuestraTextoEleccion();
                            }
                            else
                            {
                                if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                                {
                                    eleccion = false;

                                    MensajeFinal();
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (baseDeDatos.numeroMisionesPrincipales >= 2)
                {
                    mostrado = true;
                }
            }
        }
        else
        {
            soldado1.SetActive(false);
            soldado2.SetActive(false);
            StartCoroutine(DestruyeObjeto());
        }
    }



    void MuestraSiguienteMensaje()
    {
        emisor = "Sara";

        if(baseDeDatos.idioma == 1) //inglés
        {
            switch (posMensaje)
            {
                case 0:
                    mensaje = "I hope you don't come looking for a room in this tavern. If so, I apologize in advance, because of me you have run out of the last one. But I haven't taken it, it's just that I have bad luck.";
                    break;
                case 1:
                    mensaje = "To give you an idea of my luck when I came here, a bridge was broken, monsters attacked me, it rained in the desert ... And this will matter to you. Well, just when I arrived a boy had arrived just a moment before me and got the last room.";
                    break;
                case 2:
                    mensaje = "Before the educational cuts of the empire there were large state residences for us and we did not have these problems. They were great places where students met and lived beyond life in the classroom.";
                    break;
                case 3:
                    mensaje = "Damn bourgeois and their greed, they destroyed the university! Surely, they were the ones who pressed for all to be private and thus take more money. Everything is a conspiracy. And with my luck I must walk to Pedrán to find other accommodation if there is any left.";
                    break;
                case 4:
                    mensaje = "With everything I've been going to be a martyrdom. At least I have been able to regain strength in this tavern, although like everything previously paying the price...";
                    break;
                case 5:
                    mensaje = "Oh! I have already rolled up as my parents say and I have not introduced myself. My name is Sara I will start studying this year at the University of Áncia and with a scholarship. You look like going to University too, right?";
                    StartCoroutine(EsperaExclamacion());
                    saraAnimador.Play("Sorpresa");
                    break;
                case 6:
                    saraAnimador.Play("Idle - Down");
                    mensaje = "I see... So, you have also been awarded a scholarship to start this year... What a curious thing, coming here I have already met two other people in the same situation and our university had not gave anyone as brilliant as that person was for many years.";
                    break;
                case 7:
                    mensaje = "Maybe things are starting to improve and state aid begins to arrive. Anyway, I'm glad for us and since we both go to Pedrán, do you want us to go there together? The roads have become dangerous and we could help each other.";
                    break;
            }
        }
        else
        {
            switch (posMensaje)
            {
                case 0:
                    mensaje = "Espero que no vengas buscando una habitación a esta taberna. De ser así lo siento de antemano, te has ido a cruzar en el camino de la tía más gafe de la historia.";
                    break;
                case 1:
                    mensaje = "Para que te hagas una idea cuando venía hacia aquí, se rompió un puente, me atacaron monstruos, llovió en el desierto... Y esto a qué viene te preguntarás. Pues resulta que acabo de llegar y ya no hay ni una sola habitación libre debido al inicio del curso.";
                    break;
                case 2:
                    mensaje = "Antes de los recortes educativos del imperio había grandes residencias estatales para nosotros y no teníamos que vernos en estos follones. Eran grandes lugares donde los estudiantes se reunían y convivían más allá de la vida en las aulas.";
                    break;
                case 3:
                    mensaje = "¡Malditos burgueses y su codicia, destruyeron la universidad! Seguro que han sido ellos los que presionaron para que todas fueran privadas y así llevarse más dinero. Todo es una conspiración. Y para más con mi suerte me toca a mi andar hasta Pedrán para buscar otro alojamiento si es que queda alguno libre.";
                    break;
                case 4:
                    mensaje = "Con todo lo que he andado va a ser un martirio. Al menos he podido recuperar fuerzas en esta taberna aunque como todo pagando previamente el precio...";
                    break;
                case 5:
                    mensaje = "Ostras ya me he vuelto a enrollar como dicen mis padres y no me he presentado si quiera. Me llamo Sara voy a empezar a estudiar este año en la Universidad de Áncia y con una beca ni más ni menos. Tú tienes pinta de ir a la uni también, ¿verdad?";
                    StartCoroutine(EsperaExclamacion());
                    saraAnimador.Play("Sorpresa");
                    break;
                case 6:
                    saraAnimador.Play("Idle-Down");
                    mensaje = "Entiendo así que a ti también te han becado para comenzar este año... Que cosa más curiosa, viniendo hacia aquí ya he conocido a otros dos de primer año en la misma situación y nuestra universidad hacía muchos años que no becaba a nadie por brillante que fuera esa persona.";
                    break;
                case 7:
                    mensaje = "Lo mismo las cosas están empezando a mejorar y las ayudas estatales empiezan a llegar. Sea como sea me alegro por nosotros y puesto que ambos nos dirigimos a Pedrán, ¿quieres que vayamos juntos hasta allí? Los caminos se han vuelto peligrosos y podríamos ayudarnos.";
                    break;
            }
        }

        if (posMensaje < 7)
        {
            TextBox.MuestraTextoHistoria(mensaje, emisor);
        }
        else
        {
            TextBox.MuestraTextoConEleccion(mensaje, emisor, 0);
            eleccion = true;
        }
    }



    void MensajeFinal()
    {
        emisor = "Sara";
        int auxMision;

        if(baseDeDatos.idioma == 1)
        {
            if (aceptar)
            {
                mensaje = "Great! I was tired of traveling alone all the time and I love talking, I confess that people looked at me weird when I did it alone. I hope we become friends.";
                baseDeDatos.AniadePersonaje(20);
                baseDeDatos.IncluyeMision(11);
                auxMision = 13;
            }
            else
            {
                mensaje = "What a pity, but I can't force anyone to be with me. I hope we'll see each other again soon.";
                //baseDeDatos.IncluyeMision(12);
                auxMision = 12;
            }
        }
        else
        {
            if (aceptar)
            {
                mensaje = "¡Genial! Estaba harta de viajar sola todo el rato. Espero que nos llevemos bien.";
                baseDeDatos.AniadePersonaje(20);
                baseDeDatos.IncluyeMision(11);
                auxMision = 13;
            }
            else
            {
                mensaje = "Una pena la verdad, pero bueno no puedo obligar a nadie de estar conmigo. Espero nos volvamos a ver pronto.";
                //baseDeDatos.IncluyeMision(12);
                auxMision = 12;
            }
        }

        TextBox.MuestraTextoHistoriaMision(mensaje, emisor, auxMision);
        mision = true;
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
        baseDeDatos.indiceObjetivo = 4;
        fondoNegro.SetActive(true);
        FadeIn();
        saraAnim.SetActive(false);

        yield return new WaitForSeconds(fadeTime);

        fondoNegro.SetActive(false);
        FadeOut();

        controlJugador.setMensajeActivo(false);

        activo = false;

        TextBox.OcultaTextoFinCombate();

        camara.TerminaHistoria();

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

        yield return new WaitForSeconds(1);

        textoMision.SetActive(true);

        if(baseDeDatos.idioma == 0)
        {
            textoMision.transform.GetChild(1).GetComponent<Text>().text = "Nueva Misión";
        }
        else if (baseDeDatos.idioma == 1)
        {
            textoMision.transform.GetChild(1).GetComponent<Text>().text = "New Mision";
        }

        yield return new WaitForSeconds(2);

        textoMision.SetActive(false);
    }



    IEnumerator EsperaSara()
    {
        activo = true;
        controlJugador.setMensajeActivo(true);
        fondoNegro.SetActive(true);
        saraAnim.gameObject.SetActive(true);

        saraAnim.transform.position = posIni.transform.position;
        saraAnim.transform.GetChild(0).gameObject.SetActive(false);
        FadeIn();
        camara.IniciaHistoria(2);

        yield return new WaitForSeconds(fadeTime);
        saraAnimador.Play("Idle-Up");

        fondoNegro.SetActive(false);
        FadeOut();

        yield return new WaitForSeconds(1.5f);
        saraAnimador.Play("Negar");

        yield return new WaitForSeconds(0.4f);
        saraAnimador.Play("TabernaElPaso1");
        
        yield return new WaitForSeconds(4.01f);

        saraAnim.transform.GetChild(0).gameObject.SetActive(true);

        yield return new WaitForSeconds(0.75f);

        saraAnim.transform.GetChild(0).gameObject.SetActive(false);
        
        
        yield return new WaitForSeconds(2.5f);
        
        saraAnim.transform.position = posFin.transform.position;

        IniciaTexto();
    }



    IEnumerator EsperaExclamacion()
    {
        saraAnim.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        saraAnim.transform.GetChild(0).gameObject.SetActive(false);
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (!mostrado)
        {
            if (other.CompareTag("Player"))
            {
                baseDeDatos.CumpleMision(10);

                StartCoroutine(EsperaSara());
            }
        }
    }



    void LateUpdate()
    {
        if (activo)
        {
            AnimatorStateInfo currentAnimatorStateInfo = saraAnimador.GetCurrentAnimatorStateInfo(0);

            if (currentAnimatorStateInfo.IsName("Idle-Up") || currentAnimatorStateInfo.IsName("Negar"))
            {
                saraAnim.transform.position = posIni.transform.position;
            }
        }
    }



    IEnumerator DestruyeObjeto()
    {
        yield return new WaitForSeconds(8);
        Destroy(gameObject);
    }



    void MuestraTextoEleccion()
    {
        emisor = baseDeDatos.listaPersonajesAliados[0].nombre;

        if (baseDeDatos.idioma == 1)
        {
            if (aceptar)
            {
                mensaje = "I think it's a good idea to go together.";
            }
            else
            {
                mensaje = "The truth is that I prefer to go alone.";
            }
        }
        else //español
        {
            if (aceptar)
            {
                mensaje = "Creo que es buena idea que vayamos juntos.";
            }
            else
            {
                mensaje = "Lo cierto es que prefiero ir solo.";
            }
        }

        TextBox.MuestraTextoHistoria(mensaje, emisor);
    }
}
