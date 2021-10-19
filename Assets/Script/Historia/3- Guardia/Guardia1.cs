using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Guardia1 : MonoBehaviour
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
    bool aceptar;
    bool opcionElegida;
    bool eleccion;
    bool textoEleccion;

    float alpha;
    float fadeTime;

    public GameObject prota;
    public GameObject saraAnim, posIni, posFin;
    public GameObject fondoNegro;
    //public GameObject menuMision;
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
        eleccion = false;
    }



    void Start()
    {
        manager = GameObject.Find("GameManager");
        baseDeDatos = manager.GetComponent<BaseDatos>();

        alpha = 0;
        fadeTime = 2f;
        numeroMensajes = 6;
        posMensaje = 0;

        saraAnim.SetActive(false);

        activo = false;
        pasaPrimerMensaje = false;
        mision = false;
        aceptar = false;
        textoEleccion = false;

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
                                if (baseDeDatos.listaMisionesPrincipales[1].indice == 12)
                                {
                                    if (posMensaje == 5)
                                    {
                                        StartCoroutine(EsperaSara2());
                                    }
                                    else
                                    {
                                        posMensaje++;

                                        MuestraSiguienteMensaje();
                                    }
                                }
                                else
                                {
                                    if (posMensaje == 0)
                                    {
                                        StartCoroutine(EsperaSara2());
                                    }
                                    else
                                    {
                                        posMensaje++;

                                        MuestraSiguienteMensaje();
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

                                    MensajeFinal();
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (baseDeDatos.numeroMisionesPrincipales >= 3)
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

        if(baseDeDatos.idioma == 1)
        {
            if (baseDeDatos.listaMisionesPrincipales[1].indice == 12)
            {
                numeroMensajes = 6;

                switch (posMensaje)
                {
                    case 0:
                        mensaje = "Hey, you're the boy from the El Paso tavern. How was your trip? I had some other problem. For example, on one of the bridges that crosses the R6 I tripped and fell into the water.";
                        break;
                    case 1:
                        mensaje = "Then when one of the guards tried to help me out of the water a giant monster appeared and ate it and if this seems to you little, it turns out that the monster choked with the boot of the guard, drowned and fell into the water.";
                        break;
                    case 2:
                        mensaje = "But because the monster was so big, it caused a huge wave that dragged me for many meters. I thought I would drown. Luckily I came to a cave to climb back to the top of the R6.";
                        break;
                    case 3:
                        mensaje = "From there I had no more serious problems until arriving in Pedrán. I came as soon as I could register, but I had to wait about 5 hours in line to do it. I have to say that Pedrán officials are very efficient.";
                        break;
                    case 4:
                        mensaje = " In the Imperial City, which is where I come from, it would have taken months to seal the paper. It is seen that it has to be very precise to put the seal so it takes per person...";
                        break;
                    case 5:
                        mensaje = "Oh I've already rolled up again as my parents say. Let me help you with the record. Only you stay so I don't think it takes long.";
                        break;
                    case 6:
                        saraAnimador.Play("Risa");
                        mensaje = "Well, it would be resolved. Now we must go to the university for the opening ceremony of the course. You should head north of Pedrán, even if you are careful with R7. It is quite labyrinthine. See you there.";
                        break;
                }

                if (posMensaje < 6)
                {
                    TextBox.MuestraTextoHistoria(mensaje, emisor);
                }
                else if (posMensaje == 6)
                {
                    TextBox.MuestraTextoHistoriaMision(mensaje, emisor, 14);
                    mision = true;
                }
            }
            else if (baseDeDatos.listaMisionesPrincipales[1].indice == 13)
            {
                switch (posMensaje)
                {
                    case 0:
                        mensaje = "I would already have my signed document. You have to see what it took to get here, but we finally made it. And to thank you, let me help you with your access document.";
                        break;
                    case 1:
                        mensaje = "Everything is already fixed. The guard told me that we should go to the university as soon as possible if we do not want to miss the opening ceremony of the course. For this we must cross the R7 that is north of Pedrán.";
                        break;
                    case 2:
                        mensaje = "I really liked traveling with you and I think we make a good team. I will not cheat you, I have always been told that the university is a very cool place, but it is very complicated, so I would like us to continue to team up.";
                        break;
                    case 3:
                        mensaje = "It would be easier for us to collaborate from now on. What do you think about the idea? Do we make equipment?";
                        break;
                    case 4:
                        mensaje = "Remember to pick up a few items in the store before going to college. The road can be complicated for rookies like us.";
                        break;
                }

                if (posMensaje < 3)
                {
                    TextBox.MuestraTextoHistoria(mensaje, emisor);
                }
                if (posMensaje == 3)
                {
                    TextBox.MuestraTextoConEleccion(mensaje, emisor, 0);
                    eleccion = true;
                }
                else if (posMensaje == 4)
                {
                    TextBox.MuestraTextoHistoriaMision(mensaje, emisor, 14);
                    mision = true;
                }
            }
        }
        else
        {
            if (baseDeDatos.listaMisionesPrincipales[1].indice == 12)
            {
                numeroMensajes = 6;

                switch (posMensaje)
                {
                    case 0:
                        mensaje = "Vaya, si eres el chico de la taberna de El Paso. ¿Cómo te ha ido el viaje? Yo tuve algún que otro problema. Por ejemplo, en uno de los puentes que cruza la R6 me tropecé y caí al agua.";
                        break;
                    case 1:
                        mensaje = "Luego cuando uno de los guardas intentó ayudarme a salir del agua apareció un monstruo gigante y se lo comió y si esto te parece poco resulta que el monstruo se atragantó con la bota del guarda, se ahogó y cayó al agua.";
                        break;
                    case 2:
                        mensaje = "Pero como el monstruo era tan grande provocó una enorme ola que me arrastró durante muchos metros. Pensaba que ya no lo contaba. Menos mal que llegué a una caverna por la que subir de nuevo a lo alto de la R6.";
                        break;
                    case 3:
                        mensaje = "Desde allí ya no tuve más inconvenientes graves hasta llegar a Pedrán. Vine lo antes que pude a registrarme, pero tuve que esperar unas 5 horas en cola para poder hacerlo. He de decir que los funcionarios de Pedrán son muy eficientes.";
                        break;
                    case 4:
                        mensaje = "En la Ciudad Imperial, que es de donde yo vengo, habrían tardado meses en sellarme el papel. Se ve que tiene que ser muy precisos para poner el sello por lo que se tarda por persona...";
                        break;
                    case 5:
                        mensaje = "Ostras ya me he vuelto a enrollar como dicen mis padres. Déjame que te eche una mano con el registro. Solo quedas tú así que no creo que tarde mucho.";
                        break;
                    case 6:
                        saraAnimador.Play("Risa");
                        mensaje = "Pues ya estaría resuelto. Ahora debemos ir hasta la universidad para el acto de inauguración del curso. Debes dirigirte al norte de Pedrán, aunque ten cuidado con la R7. Es bastante laberíntica. Nos vemos allí.";
                        break;
                }

                if (posMensaje < 6)
                {
                    TextBox.MuestraTextoHistoria(mensaje, emisor);
                }
                else if (posMensaje == 6)
                {
                    TextBox.MuestraTextoHistoriaMision(mensaje, emisor, 14);
                    mision = true;
                }
            }
            else if (baseDeDatos.listaMisionesPrincipales[1].indice == 13)
            {
                switch (posMensaje)
                {
                    case 0:
                        mensaje = "Y ya tendría mi documento firmado. Hay que ver lo que ha costado llegar hasta aquí, pero al fin lo logramos. Y para agradecértelo déjame que te eche una mano con tu documento de acceso.";
                        break;
                    case 1:
                        mensaje = "Ya está todo arreglado. El guarda me ha dicho que debemos ir cuanto antes hacia la universidad si no queremos perdernos el acto de inauguración del curso. Para ello debemos cruzar la R7 que está al norte de Pedrán.";
                        break;
                    case 2:
                        mensaje = "Me ha gustado mucho viajar contigo y creo que hacemos buen equipo. No te voy a engañar siempre me han dicho que la universidad es un sitio muy guay, pero es muy complicado, por ello me gustaría que siguiéramos haciendo equipo.";
                        break;
                    case 3:
                        mensaje = "Sería más fácil para nosotros colaborar de aquí en adelante. ¿Qué te parece la idea? ¿Hacemos equipo?";
                        break;
                    case 4:
                        mensaje = "Recuerda recoger unos cuantos objetos en la tienda antes de ir a la universidad. El camino puede ser complicado para unos novatos como nosotros.";
                        break;
                }

                if (posMensaje < 3)
                {
                    TextBox.MuestraTextoHistoria(mensaje, emisor);
                }
                if(posMensaje == 3)
                {
                    TextBox.MuestraTextoConEleccion(mensaje, emisor, 1);
                    eleccion = true;
                }
                else if(posMensaje == 4)
                {
                    TextBox.MuestraTextoHistoriaMision(mensaje, emisor, 14);
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
        saraAnim.SetActive(false);
        
        yield return new WaitForSeconds(fadeTime);

        fondoNegro.SetActive(false);
        FadeOut();

        //menuMision.SetActive(false);

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

        if (baseDeDatos.idioma == 0)
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
        controlJugador.setMensajeActivo(true);
        fondoNegro.SetActive(true);
        saraAnim.gameObject.SetActive(true);

        saraAnim.transform.GetChild(0).gameObject.SetActive(false);
        prota.transform.position = new Vector3(-249.5f, -307.6f, prota.transform.position.z);

        FadeIn();
        camara.IniciaHistoria(3);
        activo = true;
        saraAnimador.Play("Idle-Up");

        yield return new WaitForSeconds(fadeTime);

        fondoNegro.SetActive(false);
        FadeOut();

        yield return new WaitForSeconds(1.5f);

        saraAnimador.Play("Guardia1");

        yield return new WaitForSeconds(3);

        saraAnimador.Play("Idle-Down");

        IniciaTexto();
    }



    IEnumerator EsperaSara2()
    {
        activo = false;

        saraAnimador.Play("Guardia2");

        yield return new WaitForSeconds(4.3f);

        saraAnimador.Play("Idle-Down");

        posMensaje++;

        if (posMensaje <= numeroMensajes)
        {
            MuestraSiguienteMensaje();
        }

        activo = true;
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (!mostrado)
        {
            soldado1.SetActive(false);
            soldado2.SetActive(false);

            if (other.CompareTag("Player"))
            {
                if (baseDeDatos.listaMisionesPrincipales[1].indice == 12)
                {
                    baseDeDatos.CumpleMision(12);
                }
                else
                {
                    baseDeDatos.CumpleMision(13);
                    baseDeDatos.CumpleMision(11);
                }

                StartCoroutine(EsperaSara());
            }
        }
    }



    void LateUpdate()
    {
        if (activo)
        {
            AnimatorStateInfo currentAnimatorStateInfo = saraAnimador.GetCurrentAnimatorStateInfo(0);

            if (currentAnimatorStateInfo.IsName("Idle-Down"))
            {
                saraAnim.transform.position = posFin.transform.position;
            }
            else if (currentAnimatorStateInfo.IsName("Idle-Up"))
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
                mensaje = "I think it's a good idea to go together. The University seems like a place that better not to go alone.";
            }
            else
            {
                mensaje = "The truth is that I prefer to go alone. It is not a big deal.";
            }
        }
        else //español
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

        TextBox.MuestraTextoHistoria(mensaje, emisor);
    }



    void MensajeFinal()
    {
        emisor = "Sara";

        if (baseDeDatos.idioma == 1)
        {
            if (aceptar)
            {
                mensaje = "I love that you have accepted! We will make a great team. Come on we must get to the opening ceremony.";
                saraAnimador.Play("Risa");
            }
            else
            {
                mensaje = "A pity, although I hope we continue to get along so well. See you at the opening ceremony.";
                baseDeDatos.numeroIntegrantesEquipo--;
                saraAnimador.Play("Negar");
            }
        }
        else
        {
            if (aceptar)
            {
                mensaje = "¡Como me alegra que hayas aceptado! Haremos un gran equipo. Venga debemos llegar al acto de inauguración.";
                saraAnimador.Play("Risa");
            }
            else
            {
                mensaje = "Una lástima, aunque espero que nos sigamos llevando tan bien en adelante. Te veo en el acto de inauguración.";
                baseDeDatos.numeroIntegrantesEquipo--;
                saraAnimador.Play("Negar");
            }
        }

        TextBox.MuestraTextoHistoria(mensaje, emisor);

        posMensaje = 3;
        eleccion = false;
    }
}
