using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DespachoGeneral2 : MonoBehaviour {
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
    public GameObject bloqueaEscaleras;
    public GameObject fondoNegro;
    public GameObject textoMision;
    public GameObject elementosAnim;
    public GameObject general;
    public GameObject bloqueaBase;

    GameObject manager;

    ControlJugador controlJugador;
    Camara camara;

    Animator generalAnimador;
    Animator animadorLucas;
    Animator animadorJugador;

    BaseDatos baseDeDatos;



    void Awake()
    {
        mostrado = false;
        eleccion = false;
    }



    void Start()
    {
        manager = GameObject.Find("GameManager");
        baseDeDatos = manager.GetComponent<BaseDatos>();

        alpha = 0;
        fadeTime = 2f;
        posMensaje = 0;

        activo = false;
        pasaPrimerMensaje = false;
        mision = false;
        aceptar = false;
        textoEleccion = false;

        controlJugador = GameObject.Find("Player").GetComponent<ControlJugador>();
        camara = GameObject.Find("Main Camera").GetComponent<Camara>();

        generalAnimador = general.GetComponent<Animator>();
        animadorJugador = elementosAnim.transform.GetChild(2).GetComponent<Animator>();
        animadorLucas = elementosAnim.transform.GetChild(1).GetComponent<Animator>();

        for(int i = 0; i < 3; i++)
        {
            elementosAnim.transform.GetChild(i).gameObject.SetActive(false);
        }

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
                                if (baseDeDatos.listaMisionesPrincipales[3].indice == 15)
                                {
                                    if(posMensaje == 5 && !aceptar)
                                    {
                                        StartCoroutine(JugadorSale());
                                    }
                                    else
                                    {
                                        posMensaje++;

                                        MuestraSiguienteMensaje();
                                    }
                                }
                                else
                                {
                                    if(posMensaje != 4)
                                    {
                                        posMensaje++;

                                        MuestraSiguienteMensaje();
                                    }
                                    else
                                    {
                                        if (!aceptar)
                                        {
                                            StartCoroutine(JugadorSale());
                                        }
                                        else
                                        {
                                            StartCoroutine(SegundaAnimacion());
                                        }
                                    }
                                }
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

                                    MensajeEleccion();
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
        emisor = "General";

        if (baseDeDatos.idioma == 1)
        {
            if (baseDeDatos.listaMisionesPrincipales[3].indice == 15)
            {
                switch (posMensaje)
                {
                    case 0:
                        mensaje = "It’s good to see you in one piece " + baseDeDatos.listaPersonajesAliados[0].nombre + ", I have already been informed how the mission was carried out... It is a terrible news what happened with those boys.";
                        break;
                    case 1:
                        mensaje = " A team arrived shortly after you left, but we couldn't do anything to rescue them... If we had achieved a little more time who knows if we could have saved their lives…";
                        break;
                    case 2:
                        mensaje = "You made the right decision and obeyed my orders in a very complex situation which shows loyalty, a quality very difficult to find these days, and that I value much more than courage or strength.";
                        break;
                    case 3:
                        mensaje = "You showed temperance in making that decision and as far as I know you were the last to leave. I don't know if for fear or for another reason, but you were at the bottom of the canyon like a real soldier.";
                        break;
                    case 4:
                        mensaje = "That's why I want to make an offer from the Resistance and that you must think carefully. Would you like to join our team?";
                        break;
                    case 5:
                        if (aceptar)
                        {
                            mensaje = "But now it's time to get serious. Because you join us, we must assign you your first mission. It's time to go back to class…";
                        }
                        else
                        {
                            mensaje = "Maybe at another time our paths will come back together, but now I have to ask you to leave our base and above all, do not talk about us to anyone or we will be forced to take action.";
                        }
                        break;
                    case 6:
                        if (aceptar)
                        {
                            mensaje = "I understand that this idea scares you after what happened at the reception, but we must infiltrate and control the situation inside. Those of you who are new don't have signed up yet.";
                        }
                        else
                        {
                            emisor = "Kazama";
                            mensaje = "Are you sure to let him go? It could be dangerous for us and disclose our compromised information.";
                        }
                        break;
                    case 7:
                        if (aceptar)
                        {
                            mensaje = "It is important to have eyes on all possible levels of the university. We do not believe that they will make such an obvious attack after the failure of the previous one. Surely, they will have you guarded, to look for subversive elements.";
                        }
                        else
                        {
                            mensaje = "Take the measures you deem appropriate, but I hope that the boy does his part. Perhaps in the future it will be a very valuable element as in your day it happened with you.";
                        }
                        break;
                    case 8:
                        mensaje = "It is time to prepare and look for the slightest trace left, the slightest opening from which we can deal a powerful blow to your plans. You can be calm; you will not be the only one. There will be more members spread throughout the university.";
                        break;
                    case 9:
                        mensaje = "However, you will not know who they are to prevent the Dome from identifying you if someone is arrested. You will work as sleeping cells until we communicate otherwise.";
                        break;
                    case 10:
                        mensaje = "The key to your activation will be “It's time to move the skeleton”. If someone tells you this you must obey what that person tells you. This key is exclusive to you and nobody should know it. That said, it's time to leave young boy. I wish you luck.";
                        break;
                }

                if (posMensaje < 4)
                {
                    TextBox.MuestraTextoHistoria(mensaje, emisor);
                }
                else if (posMensaje == 4)
                {
                    TextBox.MuestraTextoConEleccion(mensaje, emisor, 3);
                    eleccion = true;
                }
                else if (posMensaje < 10 && aceptar)
                {
                    TextBox.MuestraTextoHistoria(mensaje, emisor);
                }
                else if (posMensaje < 7 && !aceptar)
                {
                    TextBox.MuestraTextoHistoria(mensaje, emisor);
                }
                else if (posMensaje == 7 && !aceptar)
                {
                    TextBox.MuestraTextoHistoriaMision(mensaje, emisor, 18);
                    mision = true;
                }
                else if (posMensaje == 10 && aceptar)
                {
                    TextBox.MuestraTextoHistoriaMision(mensaje, emisor, 17);
                    mision = true;
                }
            }
            else if (baseDeDatos.listaMisionesPrincipales[3].indice == 16)
            {
                switch (posMensaje)
                {
                    case 0:
                        mensaje = "What the hell were you thinking! I gave you very clear orders not to confront the members of the Dome, that if it happened, you would flee. And what happens to you? Face one of the heads of their organization and alone.";
                        break;
                    case 1:
                        generalAnimador.SetBool("vuelveIdle", true);
                        mensaje = "Didn't you see what happened to those who tried before you? Did you want to join them? Damn it, I'm just trying to protect you, but you're like bugs going to the light…";
                        break;
                    case 2:
                        generalAnimador.Play("Idle-Up");
                        mensaje = "I've already buried too many brave... But I have to thank you, if it wasn't for your recklessness, I would have had to dig new graves. Connor could arrive to save them and now they are in the infirmary. It seems everyone is going to be saved.";
                        break;
                    case 3:
                        generalAnimador.Play("Idle-Down");
                        mensaje = "That's why I want to make an offer from the Resistance and that you must think carefully. Would you like to join our team?";
                        break;
                    case 4:
                        if (aceptar)
                        {
                            mensaje = "Because you join us, we must assign you your first mission without further delay. And since you have shown an unparalleled courage, I have a special request to make. But first... Lucas please pass.";
                        }
                        else
                        {
                            mensaje = "Maybe at another time our paths will come back together, but now I have to ask you to leave our base and above all, do not talk about us to anyone or we will be forced to take action.";
                        }
                        break;
                    case 5:
                        if (aceptar)
                        {
                            emisor = "Lucas";
                            mensaje = "Ensign Lucas is presented.";
                        }
                        else
                        {
                            elementosAnim.transform.GetChild(0).gameObject.SetActive(true);
                            emisor = "Kazama";
                            mensaje = "Are you sure to let him go? It could be dangerous for us and disclose our compromised information.";
                        }
                        break;
                    case 6:
                        if (aceptar)
                        {
                            mensaje = baseDeDatos.listaPersonajesAliados[0].nombre + " I present to you Ensign Lucas it will be he who directs this mission. Please soldier explain the objectives of the operation.";
                        }
                        else
                        {
                            mensaje = "Take the measures you deem appropriate, but I hope that the boy does his part. Perhaps in the future it will be a very valuable element as in your day it happened with you.";
                        }
                        break;
                    case 7:
                        emisor = "Lucas";
                        mensaje = "Yes sir! Our last expedition in enemy territory proved doubly unsuccessful. Lieutenant Gueorgui of the investigation group was taken prisoner and we were unable to steal the information he was looking for along with members of the exploration team.";
                        break;
                    case 8:
                        emisor = "Lucas";
                        mensaje = "We have been able to confirm that both are in the west wing of the campus. So, the operation has two well-defined objectives. The first, rescue the lieutenant who is in the basement of the building. The second get the information found in Professor Elric's office.";
                        break;
                    case 9:
                        mensaje = "Exactly that is why your goal will be to infiltrate when classes end today and meet both objectives. You will be accompanied by members of the special forces team as a support. Good luck soldiers.";
                        break;
                    case 10:
                        animadorLucas.Play("IdleLeft");
                        emisor = "Lucas";
                        mensaje = "Rookie see you at university. Don't take long to arrive.";
                        break;
                }

                if (posMensaje < 3)
                {
                    TextBox.MuestraTextoHistoria(mensaje, emisor);
                }
                else if (posMensaje == 3)
                {
                    TextBox.MuestraTextoConEleccion(mensaje, emisor, 3);
                    eleccion = true;
                }
                else if (posMensaje < 10 && aceptar)
                {
                    TextBox.MuestraTextoHistoria(mensaje, emisor);
                }
                else if (posMensaje < 6 && !aceptar)
                {
                    TextBox.MuestraTextoHistoria(mensaje, emisor);
                }
                else if (posMensaje == 6 && !aceptar)
                {
                    TextBox.MuestraTextoHistoriaMision(mensaje, emisor, 18);
                    mision = true;
                }
                else if (posMensaje == 10 && aceptar)
                {
                    TextBox.MuestraTextoHistoriaMision(mensaje, emisor, 19);
                    mision = true;
                }
            }
        }
        else
        {
            if (baseDeDatos.listaMisionesPrincipales[3].indice == 15)
            {
                switch (posMensaje)
                {
                    case 0:
                        mensaje = "Me alegro de verte de una pieza "+ baseDeDatos.listaPersonajesAliados[0].nombre + ", ya me han informado como se desenvolvió la misión… Es una terrible noticia lo ocurrido con esos muchachos.";
                        break;
                    case 1:
                        mensaje = "Un equipo llegó al poco de iros vosotros, pero ya no pudimos hacer nada por rescatarles… Si hubiéramos logrado un poco más de tiempo quién sabe si podríamos haber salvado sus vidas…";
                        break;
                    case 2:
                        mensaje = "Tomaste la decisión correcta y obedeciste mis órdenes en una situación muy compleja lo que demuestra lealtad, una cualidad muy difícil de encontrar en estos días, y que yo valoro mucho más que el valor o la fuerza.";
                        break;
                    case 3:
                        mensaje = "Demostraste templanza al tomar esa decisión y hasta donde yo sé fuiste el último en salir.  Desconozco si por temor o por que otro motivo, pero estuviste al pie del cañón como un auténtico soldado.";
                        break;
                    case 4:
                        mensaje = "Es por esto que quiero hacerte una oferta en nombre de toda la Resistencia y que debes pensarte detenidamente. ¿Quieres formar parte de nuestro equipo?";
                        break;
                    case 5:
                        if (aceptar)
                        {
                            mensaje = "Pero ahora toca ponerse serios. Dado que te unes a nosotros debemos asignarte tu primera misión. Toca volver a clase…";
                        }
                        else
                        {
                            mensaje = "Quizás en otro momento nuestros caminos se vuelvan a juntar, ahora he de pedirte que abandones nuestra base y sobre todo que no hables de nosotros a nadie o nos veremos obligados a tomar medidas.";
                        }
                        break;
                    case 6:
                        if (aceptar)
                        {
                            mensaje = "Entiendo que te asuste esta idea después de lo ocurrido en la recepción, pero debemos infiltrarnos y controlar la situación desde dentro. A los nuevos no os tienen fichados todavía.";
                        }
                        else
                        {
                            emisor = "Kazama";
                            mensaje = "¿Estás seguro de dejarle marchar? Podría ser peligroso para nosotros y divulgar nuestra información comprometida.";
                        }
                        break;
                    case 7:
                        if (aceptar)
                        {
                            mensaje = "Es importante tener ojos en todos los estamentos de la universidad posibles. No creemos que vayan a hacer un ataque tan evidente después del fracaso del ataque anterior. Seguramente os tendrán vigilados, para buscar elementos subversivos. ";
                        }
                        else
                        {
                            mensaje = "Toma las medidas que creas oportunas, pero confío en que el muchacho cumpla su parte. Quizás en el futuro sea un activo muy valioso como en su día ocurrió contigo.";
                        }
                        break;
                    case 8:
                        mensaje = "Toca prepararse y buscar el más mínimo rastro que se dejen, la más mínima abertura desde la cual podamos asestarles un poderoso golpe a sus planes. Puedes estar tranquilo, no vas a ser el único. Habrá más miembros repartidos por toda la universidad.";
                        break;
                    case 9:
                        mensaje = "Sin embargo, no sabrás quienes son para evitar que la Cúpula pueda identificaros si alguien es detenido. Trabajaréis como células durmientes hasta que nosotros os comuniquemos lo contrario.";
                        break;
                    case 10:
                        mensaje = "La clave para tu activación será “Es hora de mover el esqueleto”. Si alguien te dice esto deberás obedecer lo que esa persona te diga. Esta clave es exclusiva para ti y nadie debe conocerla. Dicho esto, es hora de partir joven. Te deseo mucha suerte.";
                        break;
                }

                if (posMensaje < 4)
                {
                    TextBox.MuestraTextoHistoria(mensaje, emisor);
                }
                else if (posMensaje == 4)
                {
                    TextBox.MuestraTextoConEleccion(mensaje, emisor, 3);
                    eleccion = true;
                }
                else if (posMensaje < 10 && aceptar)
                {
                    TextBox.MuestraTextoHistoria(mensaje, emisor);
                }
                else if (posMensaje < 7 && !aceptar)
                {
                    TextBox.MuestraTextoHistoria(mensaje, emisor);
                }
                else if (posMensaje == 7 && !aceptar)
                {
                    TextBox.MuestraTextoHistoriaMision(mensaje, emisor, 18);
                    mision = true;
                }
                else if (posMensaje == 10 && aceptar)
                {
                    TextBox.MuestraTextoHistoriaMision(mensaje, emisor, 17);
                    mision = true;
                }
            }
            else if (baseDeDatos.listaMisionesPrincipales[3].indice == 16)
            {
                switch (posMensaje)
                {
                    case 0:
                        mensaje = "¡En qué demonios estabas pensando! Os di órdenes muy claras de no enfrentaros a los miembros de la Cúpula, que si ocurría huyerais. ¿Y qué se te ocurre a ti? Enfrentarte a uno de los cabezas de la su organización y además solo.";
                        break;
                    case 1:
                        generalAnimador.SetBool("vuelveIdle", true);
                        mensaje = "¿Acaso no viste lo que le ocurrió a quienes lo intentaron antes que tú? ¿Querías unirte a ellos? Maldita sea solo intento protegeros, pero sois como bichos con la luz…";
                        break;
                    case 2:
                        generalAnimador.Play("Idle-Up");
                        mensaje = "Ya he enterrado demasiados valientes… Pero he de darte las gracias, si no es por tu temeridad habría tenido que cavar nuevas tumbas. Connor pudo llegar para salvarlos y ahora están en la enfermería. Parece que todos se van a salvar.";
                        break;
                    case 3:
                        generalAnimador.Play("Idle-Down");
                        mensaje = "Es por esto que quiero hacerte una oferta en nombre de toda la Resistencia y que debes pensarte detenidamente. ¿Quieres formar parte de nuestro equipo?";
                        break;
                    case 4:
                        if (aceptar)
                        {
                            mensaje = "Dado que te unes a nosotros debemos asignarte tu primera misión sin más dilación. Y ya que has mostrado un valor sin parangón tengo una petición especial que hacerte. Pero antes... Lucas pasa por favor.";
                        }
                        else
                        {
                            mensaje = "Quizás en otro momento nuestros caminos se vuelvan a juntar, ahora he de pedirte que abandones nuestra base y sobre todo que no hables de nosotros a nadie o nos veremos obligados a tomar medidas.";
                        }
                        break;
                    case 5:
                        if (aceptar)
                        {
                            emisor = "Lucas";
                            mensaje = "Se presenta el alférez Lucas.";
                        }
                        else
                        {
                            elementosAnim.transform.GetChild(0).gameObject.SetActive(true);
                            emisor = "Kazama";
                            mensaje = "¿Estás seguro de dejarle marchar? Podría ser peligroso para nosotros y divulgar nuestra información comprometida.";
                        }
                        break;
                    case 6:
                        if (aceptar)
                        {
                            mensaje = baseDeDatos.listaPersonajesAliados[0].nombre + " te presento al alférez Lucas será él quien dirija esta misión. Por favor soldado explíquele los objetivos de la operación.";
                        }
                        else
                        {
                            mensaje = "Toma las medidas que creas oportunas, pero confío en que el muchacho cumpla su parte. Quizás en el futuro sea un activo muy valioso como en su día ocurrió contigo.";
                        }
                        break;
                    case 7:
                        emisor = "Lucas";
                        mensaje = "¡Sí señor! Nuestra última expedición en territorio enemigo resultó doblemente infructuosa. El teniente Gueorgui del grupo de investigación fue tomado prisionero y no logramos robar la información que éste buscaba junto a miembros del equipo de exploración.";
                        break;
                    case 8:
                        emisor = "Lucas";
                        mensaje = "Hemos podido confirmar que ambos se encuentran en el ala oeste del campus. Por lo que la operación cuenta con dos objetivos bien definidos. El primero, rescatar al teniente quien se encuentra en el sótano del edificio. El segundo conseguir la información que se encuentra en el despacho del profesor Elric.";
                        break;
                    case 9:
                        mensaje = "Exacto es por eso que vuestro objetivo será infiltraros cuando las clases terminen hoy y cumplir ambos objetivos. Os acompañarán miembros del equipo de fuerzas especiales a modo de apoyo. Mucha suerte soldados.";
                        break;
                    case 10:
                        animadorLucas.Play("IdleLeft");
                        emisor = "Lucas";
                        mensaje = "Nos vemos en la universidad novato. No tardes mucho en llegar.";
                        break;
                }

                if (posMensaje < 3)
                {
                    TextBox.MuestraTextoHistoria(mensaje, emisor);
                }
                else if (posMensaje == 3)
                {
                    TextBox.MuestraTextoConEleccion(mensaje, emisor, 3);
                    eleccion = true;
                }
                else if (posMensaje < 10 && aceptar)
                {
                    TextBox.MuestraTextoHistoria(mensaje, emisor);
                }
                else if (posMensaje < 6 && !aceptar)
                {
                    TextBox.MuestraTextoHistoria(mensaje, emisor);
                }
                else if (posMensaje == 6 && !aceptar)
                {
                    TextBox.MuestraTextoHistoriaMision(mensaje, emisor, 18);
                    mision = true;
                }
                else if (posMensaje == 10 && aceptar)
                {
                    TextBox.MuestraTextoHistoriaMision(mensaje, emisor, 19);
                    mision = true;
                }
            }
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

        controlJugador.mov = new Vector2(0f, -2f);
        controlJugador.Animacion();

        if (aceptar)
        {
            camara.FijaCamara(30, 6);
            controlJugador.EstableceFaccion(3);
        }
        else
        {
            camara.FijaCamara(10, 0);
            prota.transform.position = new Vector3(-111.5f, -543.3f, prota.transform.position.z);
        }
        
        yield return new WaitForSeconds(fadeTime);

        fondoNegro.SetActive(false);
        FadeOut();

        controlJugador.setMensajeActivo(false);

        activo = false;

        TextBox.OcultaTextoFinCombate();

        camara.TerminaHistoria();

        textoMision.SetActive(true);
        baseDeDatos.IncluyeMision(23);

        yield return new WaitForSeconds(2);

        textoMision.SetActive(false);

        yield return new WaitForSeconds(1);

        textoMision.SetActive(true);

        
        if (baseDeDatos.idioma == 1)
        {
            textoMision.transform.GetChild(1).GetComponent<Text>().text = "New Mision";
        }
        else
        {
            textoMision.transform.GetChild(1).GetComponent<Text>().text = "Nueva Misión";
        }

        yield return new WaitForSeconds(2);

        textoMision.SetActive(false);
    }



    IEnumerator Inicia()
    {
        generalAnimador.Play("Idle-Down");
        elementosAnim.transform.GetChild(2).gameObject.SetActive(true);

        if (baseDeDatos.listaMisionesPrincipales[3].indice == 16)
        {
            generalAnimador.Play("MovimientoFrenteMesa");
        }
        
        controlJugador.setMensajeActivo(true);
        fondoNegro.SetActive(true);
        prota.transform.position = new Vector3(-137.5f, -631, prota.transform.position.z);

        FadeIn();
        camara.IniciaHistoria(6);

        yield return new WaitForSeconds(fadeTime);

        fondoNegro.SetActive(false);
        FadeOut();

        yield return new WaitForSeconds(1.5f);

        IniciaTexto();
    }



    IEnumerator SegundaAnimacion()
    {
        activo = false;
        elementosAnim.transform.GetChild(1).gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        animadorLucas.Play("LucasAnda");

        yield return new WaitForSeconds(1.5f);
        elementosAnim.transform.GetChild(1).transform.position = elementosAnim.transform.GetChild(3).transform.position;
        animadorLucas.Play("IdleUp");

        posMensaje++;
        MuestraSiguienteMensaje();

        activo = true;
    }



    IEnumerator JugadorSale()
    {
        activo = false;

        animadorJugador.Play("jugadorSale");

        yield return new WaitForSeconds(2);

        posMensaje++;

        MuestraSiguienteMensaje();
        elementosAnim.transform.GetChild(0).gameObject.SetActive(true);

        activo = true;
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (!mostrado)
        {
            if((baseDeDatos.listaMisionesPrincipales[3].indice == 15 || baseDeDatos.listaMisionesPrincipales[3].indice == 16) && baseDeDatos.listaMisionesPrincipales[3].completada && baseDeDatos.numeroMisionesPrincipales < 5)
            {
                if (other.CompareTag("Player"))
                {
                    StartCoroutine(Inicia());
                }
            }
        }
    }


    /*
    void LateUpdate()
    {
        if (activo)
        {
            AnimatorStateInfo currentAnimatorStateInfo = saraAnimador.GetCurrentAnimatorStateInfo(0);

            if (currentAnimatorStateInfo.IsName("Idle-Down"))
            {
                //saraAnim.transform.position = posFin.transform.position;
            }
            else if (currentAnimatorStateInfo.IsName("Idle-Up"))
            {
            //    saraAnim.transform.position = posIni.transform.position;
            }
        }
    }
    */


    IEnumerator DestruyeObjeto()
    {
        if(baseDeDatos.listaMisionesPrincipales[4].indice == 18)
        {
            bloqueaBase.transform.position = new Vector3(-111.5f, -541.3f, 0);
        }

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
                mensaje = "Me parece buena idea que vayamos juntos. La Universidad parece un sitio al que mejor no ir solo.";
            }
            else
            {
                mensaje = "Lo cierto es que prefiero ir solo. No creo que sea para tanto.";
            }
        }
        else //español
        {
            if (aceptar)
            {
                mensaje = "¡Viva la Resistencia!";
            }
            else
            {
                mensaje = "No quiero formar parte.";
            }
        }

        TextBox.MuestraTextoHistoria(mensaje, emisor);
    }



    void MensajeEleccion()
    {
        emisor = "General";

        if (baseDeDatos.idioma == 1)
        {
            if (aceptar)
            {
                mensaje = "This is great news, welcome to our family and I hope that with your help we will achieve a free university for all!";
            }
            else
            {
                mensaje = " Oh... It's sad news to hear it and I honestly didn't expect it, but I have to accept your decision.";
            }
        }
        else
        {
            if (aceptar)
            {
                mensaje = "¡Es una grandísima noticia, bienvenido a nuestra familia y espero que con tu ayuda logremos una universidad libre para todos!";
            }
            else
            {
                mensaje = "Oh… Es una triste noticia escucharla y sinceramente no me lo esperaba, pero he de aceptar tu decisión.";
            }
        }

        TextBox.MuestraTextoHistoria(mensaje, emisor);

        if (baseDeDatos.listaMisionesPrincipales[3].indice == 15)
        {
            posMensaje = 4;
        }
        else
        {
            posMensaje = 3;
        }

        eleccion = false;
    }



    void LateUpdate()
    {
        if (activo)
        {
            AnimatorStateInfo currentAnimatorStateInfo = animadorLucas.GetCurrentAnimatorStateInfo(0);

            if (currentAnimatorStateInfo.IsName("IdleUp") || currentAnimatorStateInfo.IsName("IdleLeft"))
            {
                elementosAnim.transform.GetChild(1).transform.position = elementosAnim.transform.GetChild(3).transform.position;
            }
        }
    }
}
