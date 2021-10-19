using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inicio : MonoBehaviour {
    int posMensaje;
    int numeroMensajes;
    int indiceHistoria;

    string mensaje;
    string emisor;

    bool mostrado;//true si esta parte de la historia ya ha sido mostrada
    bool activo;
    bool pasaPrimerMensaje;
    bool start = false;
    bool isFadeIn = false;
    bool mision;

    float alpha;
    float fadeTime;

    public GameObject prota;
    public GameObject cartero;
    public GameObject mama, mamaAnim;
    public GameObject papa;
    public GameObject fondoNegro;
    public GameObject textoMision;

    ControlJugador controlJugador;
    Camara camara;

    Animator mamaAnimador, carteroAnimador;

    public Sprite imagenDerecha, imagenAbajo;

    BaseDatos baseDeDatos;

    GameObject manager;


    void Start ()
    {
        manager = GameObject.Find("GameManager");
        baseDeDatos = manager.GetComponent<BaseDatos>();

        indiceHistoria = 0;
        alpha = 0;
        fadeTime = 2f;
        numeroMensajes = 8;

        mamaAnim.SetActive(false);
        cartero.SetActive(false);
        //menuMisionActivo = false;
        mision = false;

        activo = false;
        mostrado = false;
        pasaPrimerMensaje = false;
        controlJugador = GameObject.Find("Player").GetComponent<ControlJugador>();
        camara = GameObject.Find("Main Camera").GetComponent<Camara>();

        mamaAnimador = mamaAnim.GetComponent<Animator>();
        carteroAnimador = cartero.GetComponent<Animator>();
    }
	


	void Update ()
    {
        if (!mostrado)
        {
            if (activo)
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

                            if (posMensaje < numeroMensajes)
                            {
                                MuestraSiguienteMensaje();
                            }
                        }
                    }
                }
                else
                {
                    if (TextBox.menuActivo)
                    {
                        if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                        {
                            CierraAnimacion();
                        }
                    }
                }
            }
        }
	}



    void MuestraSiguienteMensaje()
    {
        if(baseDeDatos.idioma == 1)
        {
            switch (posMensaje)
            {
                case 0:
                    mensaje = "Son we have great news to give you. Today a very important letter has arrived and you have been waiting for a long time. You have been admitted to the University of Áncia!!";
                    emisor = "Mom";
                    mamaAnim.GetComponent<SpriteRenderer>().sprite = imagenDerecha;
                    break;
                case 1:
                    mensaje = "And you have been awarded a scholarship. It has given us so much joy that we have told everyone in town while you were away. They are super proud of you.";
                    emisor = "Dad";
                    break;
                case 2:
                    mensaje = "We always knew you were special and we knew, both your father and me, that you would do great things. It was only a matter of time before others saw the same as us.";
                    emisor = "Mom";
                    break;
                case 3:
                    mensaje = "Mom, we must stop overwhelming him. He has to embark on his journey and he must enjoy it without us pressing him.";
                    emisor = "Dad";
                    break;
                case 4:
                    mensaje = "Son, you have to go to Pedrán and there at the headquarters of the Guard you can complete the University registration. Go and start your adventure. First of all, take this key, it will serve you throughout your trip. It's from my student days and I think it will help you too.";
                    emisor = "Dad";
                    break;
                case 5:
                    mensaje = "I'm going to miss you so much ... Have a great trip and don't forget to write to us frequently. Take this map too. If you do not know where to go, consult it and it will surely help you.";
                    emisor = "Mom";
                    mamaAnim.GetComponent<SpriteRenderer>().sprite = imagenAbajo;
                    break;
                case 6:
                    mensaje = " Let me give you one last tip. You should stop by the El Paso Tavern. Many people usually pass through that town and at this time many are students on their way to university.";
                    emisor = "Dad";
                    break;
                case 7:
                    mensaje = "Maybe you meet interesting people and you make friends for the University. In addition, El Paso is just the town north of ours and you have to go there on the way to Pedrán. I love you so much son.";
                    emisor = "Mom";
                    break;
            }
        }
        else
        {
            switch (posMensaje)
            {
                case 0:
                    mensaje = "Hijo tenemos una grandísima noticia que darte. Hoy ha llegado una carta muy importante y que llevabas mucho tiempo esperando. !!Has sido admitido en la universidad de Áncia!!";
                    emisor = "Mamá";
                    mamaAnim.GetComponent<SpriteRenderer>().sprite = imagenDerecha;
                    break;
                case 1:
                    mensaje = "Y encima te han concedido una beca. Nos ha dado tanta alegría que se lo hemos contado a todos los del pueblo mientras estabas fuera. Están súper orgullosos de ti.";
                    emisor = "Papá";
                    break;
                case 2:
                    mensaje = "Siempre supimos que eras especial y sabíamos, tanto tu padre como yo, que harías cosas geniales. Solo era cuestión de tiempo que otros vieran lo mismo que nosotros.";
                    emisor = "Mamá";
                    break;
                case 3:
                    mensaje = "Mamá, debemos dejar de agobiarle. Tiene que emprender su viaje y debe disfrutar de ello sin que nosotros le presionemos.";
                    emisor = "Papá";
                    break;
                case 4:
                    mensaje = "Hijo, tienes que ir a Pedrán y allí en la sede de la Guardia podrás completar el registro de la Universidad. Ve y comienza tu aventura. Antes que nada, toma esta llave, te servirá en todo tu viaje. Es de mi época de estudiante y creo que a ti también te ayudará.";
                    emisor = "Papá";
                    break;
                case 5:
                    mensaje = "Te voy a echar tanto de menos... Ten muy buen viaje y no olvides escribirnos frecuentemente. Toma también este mapa. Si alguna vez no sabes dónde ir consúltalo y seguro te ayudará.";
                    emisor = "Mamá";
                    mamaAnim.GetComponent<SpriteRenderer>().sprite = imagenAbajo;
                    break;
                case 6:
                    mensaje = "Déjame darte un último consejo. Deberías pasarte por la taberna de El Paso. Por ese pueblo suele pasar mucha gente y en esta época muchos son estudiantes camino a la universidad.";
                    emisor = "Papá";
                    break;
                case 7:
                    mensaje = "Quizás conozcas gente interesante y vas haciendo amigos para la universidad. Además, El Paso es justo el pueblo al norte del nuestro y tienes que pasar por allí de camino a Pedrán. Te quiero mucho hijo.";
                    emisor = "Mamá";
                    break;
            }
        }

        if (posMensaje < 7)
        {
            TextBox.MuestraTextoHistoria(mensaje, emisor);
        }
        else
        {
            TextBox.MuestraTextoHistoriaMision(mensaje, emisor, 10);
            mision = true;
        }
    }



    void IniciaAnimacion()
    {
        StartCoroutine(EsperaInicio(false));
        camara.IniciaHistoria(indiceHistoria);
        prota.transform.position = new Vector3(106, -412, 0);
        mamaAnimador.enabled = false;
        mamaAnim.transform.position = mama.transform.position;
        mama.SetActive(false);
    }



    public void IniciaCartero()
    {
        StartCoroutine(EsperaCartero());
        camara.IniciaHistoria(1);
        controlJugador.setMensajeActivo(true);
    }



    void CierraAnimacion()
    {
        mostrado = true;
        StartCoroutine(EsperaInicio(true));
        cartero.SetActive(false);
        mamaAnim.SetActive(false);
        mama.SetActive(true);
        camara.FijaCamara(1, 0);
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



    IEnumerator EsperaInicio(bool termina)
    {
        fondoNegro.SetActive(true);
        FadeIn();

        yield return new WaitForSeconds(fadeTime);

        fondoNegro.SetActive(false);
        FadeOut();

        if (!termina)
        {
            activo = true;
            pasaPrimerMensaje = true;
        }
        else
        {
            controlJugador.setMensajeActivo(false);

            activo = false;

            TextBox.OcultaTextoFinCombate();

            camara.TerminaHistoria();

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
    }



    IEnumerator EsperaCartero()
    {
        mamaAnim.SetActive(true);
        cartero.SetActive(true);

        fondoNegro.SetActive(true);
        FadeIn();

        yield return new WaitForSeconds(fadeTime);

        fondoNegro.SetActive(false);
        FadeOut();

        carteroAnimador.Play("EntregarCarta");
        mamaAnimador.Play("Carta");

        yield return new WaitForSeconds(11);

        IniciaAnimacion();
    }
}
