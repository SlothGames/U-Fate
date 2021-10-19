using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Medico : MonoBehaviour 
{
    int numeroMensajes;
    int pos;
    int posMensaje;
    public int indice;

    bool aceptar;
    bool activo;
    bool menuActivo;
    bool on;

    string[] mensajes;

    GameObject manager;
    ControlJugador controlJugador;
    BaseDatos baseDeDatos;
    public GameObject menuMedico;
    MusicaManager musica;

    bool pulsado;

    float digitalX;
    float digitalY;



    void Start ()
    {
        manager = GameObject.Find("GameManager");
        controlJugador = GameObject.Find("Player").GetComponent<ControlJugador>();
        baseDeDatos = manager.GetComponent<BaseDatos>();
        musica = GameObject.Find("EfectosSonido").GetComponent<MusicaManager>();

        aceptar = false;
        activo = false;

        numeroMensajes = 5;
        pos = 0;

        mensajes = new string[numeroMensajes];
        EstableceConversacion();

        menuMedico.transform.GetChild(2).gameObject.SetActive(false);
        DesactivarMenu();
        DesactivaMedico();
    }



    void DesactivarMenu()
    {
        menuActivo = false;
        menuMedico.transform.GetChild(1).gameObject.SetActive(menuActivo);
        menuMedico.transform.GetChild(2).gameObject.SetActive(menuActivo);
        menuMedico.transform.GetChild(5).gameObject.SetActive(menuActivo);
    }



    void DesactivaMedico()
    {
        on = false;
        menuMedico.transform.GetChild(0).gameObject.SetActive(on);
        controlJugador.setMensajeActivo(false);
        activo = false;
    }



    void IniciarTexto()
    {
        on = true;
        posMensaje = 0;
        menuMedico.transform.GetChild(0).gameObject.SetActive(on);
        Escribir(mensajes[posMensaje]);
    }



    void IniciarMenu()
    {
        if(baseDeDatos.idioma == 1)
        {
            menuMedico.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "Yes \n\n No";
        }
        else
        {
            menuMedico.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "Sí \n\n No";
        }

        menuActivo = true;
        menuMedico.transform.GetChild(1).gameObject.SetActive(menuActivo);
        pos = 0;
        menuMedico.transform.GetChild(2).gameObject.SetActive(menuActivo);
        menuMedico.transform.GetChild(2).transform.position = menuMedico.transform.GetChild(3).transform.position;
        menuMedico.transform.GetChild(5).gameObject.SetActive(menuActivo);
        menuMedico.transform.GetChild(5).transform.GetChild(0).GetComponent<Text>().text = controlJugador.dinero + "";
    }



    void Update ()
    {
        if (activo)
        {
            if (!menuActivo)
            {
                if (on)
                {
                    if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                    {
                        musica.ProduceEfecto(10);

                        if(menuMedico.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text == mensajes[posMensaje])
                        {
                            if (posMensaje == 0)
                            {
                                posMensaje++;
                                Escribir(mensajes[posMensaje]);
                            }
                            else if (posMensaje == 2)
                            {
                                posMensaje++;
                                Escribir(mensajes[posMensaje]);
                            }
                            else
                            {
                                DesactivaMedico();
                            }
                        }
                    }
                }
            }
            else
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

                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || (!pulsado && digitalY != 0))
                {
                    pulsado = true;
                    musica.ProduceEfecto(11);

                    if(pos == 0)
                    {
                        pos = 1;
                        menuMedico.transform.GetChild(2).transform.position = menuMedico.transform.GetChild(4).transform.position;
                    }
                    else
                    {
                        pos = 0;
                        menuMedico.transform.GetChild(2).transform.position = menuMedico.transform.GetChild(3).transform.position;
                    }

                }
                else if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                {
                    musica.ProduceEfecto(10);

                    if (pos == 0)
                    {
                        aceptar = true;
                    }
                    else
                    {
                        aceptar = false;
                    }

                    if (aceptar)
                    {
                        bool pagado = false;

                        switch (indice)
                        {
                            case 0: //Mama
                                pagado = true;
                                break;
                            case 1:
                                if (controlJugador.dinero >= 50)
                                {
                                    pagado = true;
                                }
                                break;
                            case 2: //Taberna El paso
                                if (controlJugador.dinero >= 75)
                                {
                                    pagado = true;
                                }
                                break;
                            case 3: //Dragon Rojo
                                if (baseDeDatos.faccion == 0 || baseDeDatos.faccion == 8 || baseDeDatos.faccion == 2 || baseDeDatos.faccion == 9)
                                {
                                    if (controlJugador.dinero >= 100)
                                    {
                                        pagado = true;
                                    }
                                }
                                else if (baseDeDatos.faccion == 3 || baseDeDatos.faccion == 4 || baseDeDatos.faccion == 5 || baseDeDatos.faccion == 6)
                                {
                                    if (controlJugador.dinero >= 50)
                                    {
                                        pagado = true;
                                    }
                                }
                                else if (baseDeDatos.faccion == 7)
                                {
                                    if (controlJugador.dinero >= 200)
                                    {
                                        pagado = true;
                                    }
                                }
                                else
                                {
                                    if (controlJugador.dinero >= 100)
                                    {
                                        pagado = true;
                                    }
                                }
                                break;
                            case 4://Marabunta
                                if (baseDeDatos.faccion == 7) 
                                {
                                    if (controlJugador.dinero >= 50)
                                    {
                                        pagado = true;
                                    }
                                }
                                else if (baseDeDatos.faccion == 3 || baseDeDatos.faccion == 4 || baseDeDatos.faccion == 5 || baseDeDatos.faccion == 6)
                                {
                                    if (controlJugador.dinero >= 200)
                                    {
                                        pagado = true;
                                    }
                                }
                                else
                                {
                                    if (controlJugador.dinero >= 100)
                                    {
                                        pagado = true;
                                    }
                                }
                                
                                break;
                            case 5: //Rebentaero
                                if (baseDeDatos.faccion == 8 || baseDeDatos.faccion == 3 || baseDeDatos.faccion == 4 || baseDeDatos.faccion == 5 || baseDeDatos.faccion == 6 || baseDeDatos.faccion == 7)
                                {
                                    if (controlJugador.dinero >= 100)
                                    {
                                        pagado = true;
                                    }
                                }
                                else if (baseDeDatos.faccion == 0 || baseDeDatos.faccion == 2)
                                {
                                    if (controlJugador.dinero >= 50)
                                    {
                                        pagado = true;
                                    }
                                }
                                else if (baseDeDatos.faccion == 9)
                                {
                                    if (controlJugador.dinero >= 200)
                                    {
                                        pagado = true;
                                    }
                                }
                                else
                                {
                                    if (controlJugador.dinero >= 100)
                                    {
                                        pagado = true;
                                    }
                                }

                                break;
                        }
                        
                                                
                        if(!pagado)
                        {
                            posMensaje = 4;
                            Escribir(mensajes[posMensaje]);
                        }
                        else
                        {
                            RealizaPago();
                            posMensaje++;
                            Escribir(mensajes[posMensaje]);
                        }
                    }
                    else
                    {
                        posMensaje = 3;
                        Escribir(mensajes[posMensaje]);
                    }

                    DesactivarMenu();
                }
            }
        }
	}



    public void Escribir(string t)
    {
        on = true;
        menuMedico.transform.GetChild(0).gameObject.SetActive(on);
        //textBox.textTB.text = t;
        StartCoroutine(Deletrear(t));
    }



    public IEnumerator Deletrear(string t)
    {
        menuMedico.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "";

        yield return new WaitForSeconds(.1f);

        for (int i = 0; i < t.Length + 1; i++)
        {
            menuMedico.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = t.Substring(0, i);
            yield return new WaitForSeconds(.01f);
        }

        if(posMensaje == 1)
        {
            IniciarMenu();
        }
    }



    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            controlJugador.SetInterrogante(true);
        }

        yield return null;
    }



    private IEnumerator OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            controlJugador.SetInterrogante(false);
        }

        yield return null;
    }



    public void IniciarMedico()
    {
        if (!activo)
        {
            activo = true;
            IniciarTexto();
            controlJugador.setMensajeActivo(true);
            controlJugador.SetInterrogante(false);
        }
    }



    void RealizaPago()
    {
        if(indice != 0)
        {
            switch (indice)
            {
                case 1:
                    controlJugador.dinero -= 50;
                    break;
                case 2:
                    controlJugador.dinero -= 75;
                    break;
                case 3:
                    if (baseDeDatos.faccion == 0 || baseDeDatos.faccion == 8 || baseDeDatos.faccion == 2 || baseDeDatos.faccion == 9)
                    {
                        controlJugador.dinero -= 100;
                    }
                    else if (baseDeDatos.faccion == 3 || baseDeDatos.faccion == 4 || baseDeDatos.faccion == 5 || baseDeDatos.faccion == 6)
                    {
                        controlJugador.dinero -= 50;
                    }
                    else if (baseDeDatos.faccion == 7)
                    {
                        controlJugador.dinero -= 200;
                    }
                    else
                    {
                        controlJugador.dinero -= 100;
                    }
                    break;
                case 4:
                    if (baseDeDatos.faccion == 7)
                    {
                        controlJugador.dinero -= 50;
                    }
                    else if (baseDeDatos.faccion == 3 || baseDeDatos.faccion == 4 || baseDeDatos.faccion == 5 || baseDeDatos.faccion == 6)
                    {
                        controlJugador.dinero -= 200;
                    }
                    else
                    {
                        controlJugador.dinero -= 100;
                    }
                    break;
                case 5:
                    if (baseDeDatos.faccion == 8 || baseDeDatos.faccion == 3 || baseDeDatos.faccion == 4 || baseDeDatos.faccion == 5 || baseDeDatos.faccion == 6 || baseDeDatos.faccion == 7)
                    {
                        controlJugador.dinero -= 100;
                    }
                    else if (baseDeDatos.faccion == 0 || baseDeDatos.faccion == 2)
                    {
                        controlJugador.dinero -= 50;
                    }
                    else if (baseDeDatos.faccion == 9)
                    {
                        controlJugador.dinero -= 200;
                    }
                    else
                    {
                        controlJugador.dinero -= 100;
                    }
                    break;
            }

            menuMedico.transform.GetChild(5).transform.GetChild(0).GetComponent<Text>().text = controlJugador.dinero + "";
        }

        Cura();
    }



    void Cura()
    {
        int numeroPensonajes = baseDeDatos.numeroIntegrantesEquipo;

        for(int i = 0; i < numeroPensonajes; i++)
        {
            baseDeDatos.equipoAliado[i].vidaActual = baseDeDatos.equipoAliado[i].vidaModificada;
            baseDeDatos.equipoAliado[i].estado = Personajes.estadoPersonaje.SANO;

            int numeroAtaques = baseDeDatos.equipoAliado[i].numeroAtaques;

            for(int j = 0; j < numeroAtaques; j++)
            {
                baseDeDatos.equipoAliado[i].habilidades[j].energiaActual = baseDeDatos.equipoAliado[i].habilidades[j].energia;
            }
        }

        //controlJugador.UsaMedico();
    }



    void EstableceConversacion()
    {
        if(baseDeDatos.idioma == 1)
        {
            switch (indice)
            {
                case 0:
                    mensajes[0] = " Hi dear. You look tired.";
                    mensajes[1] = "Do you want me to give you something to make you feel better?";
                    mensajes[2] = " Wait a second while I prepare it for you.................................................................. It’s ready.";
                    mensajes[3] = "Come back whenever you need something sweetie.";

                    break;
                case 1:
                    mensajes[0] = "Hello user, I am your trusted medical robot. I take care to cure any problem you have at a small price.";
                    mensajes[1] = "Do you want me to cure your team for just 50 coins?";
                    mensajes[2] = "Wait a second while I apply the treatment.................................................................. Finish";
                    mensajes[3] = "Let me know whenever you need something.";
                    mensajes[4] = "Sorry, you do not have enough funds to pay for my services. Come back when you're solvent.";

                    break;
                case 2:
                    mensajes[0] = "Welcome traveler to our humble inn.";
                    mensajes[1] = "Do you want to take our best food for just 75 coins? This will recover your life points and your ER.";
                    mensajes[2] = "Wait a second while we prepare everything.................................................................. It’s ready.";
                    mensajes[3] = "Let me know whenever you need something.";
                    mensajes[4] = "I'm sorry you don't have enough money to afford it.";

                    break;
                case 3:
                    if (baseDeDatos.faccion == 0 || baseDeDatos.faccion == 8 || baseDeDatos.faccion == 2 || baseDeDatos.faccion == 9)
                    {
                        mensajes[0] = "Welcome traveler.";
                        mensajes[1] = "Do you want to drink something for just 100 coins?";
                        mensajes[2] = "Wait a second while I serve you.................................................................. Ready.";
                        mensajes[3] = "Let me know whenever you need something.";
                        mensajes[4] = "I'm sorry but you can't afford it.";
                    }
                    else if (baseDeDatos.faccion == 3 || baseDeDatos.faccion == 4 || baseDeDatos.faccion == 5 || baseDeDatos.faccion == 6)
                    {
                        mensajes[0] = "Welcome comrade.";
                        mensajes[1] = "Do you want to drink something for just 50 coins?";
                        mensajes[2] = "Wait a second while I serve you.................................................................. Ready.";
                        mensajes[3] = "Let me know whenever you need something.";
                        mensajes[4] = "I'm sorry but you can't afford it.";
                    }
                    else if (baseDeDatos.faccion == 7)
                    {
                        mensajes[0] = "Why the hell did you come here?";
                        mensajes[1] = "If you want to take something here for you are 200 coins.";
                        mensajes[2] = "Do not move.................................................................. Here you have it.";
                        mensajes[3] = "Take it fast and go away.";
                        mensajes[4] = "You can't afford it so go away.";
                    }
                    else
                    {
                        mensajes[0] = "Greetings emperor, you are more than welcome here.";
                        mensajes[1] = "If you want to take something, it would be 100 coins if you don't mind.";
                        mensajes[2] = "Do not move.................................................................. Here you have it.";
                        mensajes[3] = "Let me know whenever you need something.";
                        mensajes[4] = "You can't afford it, your majesty.";
                    }

                    break;
                case 4:
                    if (baseDeDatos.faccion == 0 || baseDeDatos.faccion == 8 || baseDeDatos.faccion == 2 || baseDeDatos.faccion == 9)
                    {
                        mensajes[0] = "Welcome traveler.";
                        mensajes[1] = "Do you want to drink something for just 100 coins?";
                        mensajes[2] = "Wait a second while I serve you.................................................................. Ready.";
                        mensajes[3] = "Let me know whenever you need something.";
                        mensajes[4] = "I'm sorry but you can't afford it.";
                    }
                    else if (baseDeDatos.faccion == 7)
                    {
                        mensajes[0] = "Welcome comrade.";
                        mensajes[1] = "Do you want to drink something for just 50 coins?";
                        mensajes[2] = "Wait a second while I serve you.................................................................. Ready.";
                        mensajes[3] = "Let me know whenever you need something.";
                        mensajes[4] = "I'm sorry but you can't afford it.";
                    }
                    else if (baseDeDatos.faccion == 3 || baseDeDatos.faccion == 4 || baseDeDatos.faccion == 5 || baseDeDatos.faccion == 6)
                    {
                        mensajes[0] = "Why the hell did you come here?";
                        mensajes[1] = "If you want to take something here for you are 200 coins.";
                        mensajes[2] = "Do not move.................................................................. Here you have it.";
                        mensajes[3] = "Take it fast and go away.";
                        mensajes[4] = "You can't afford it so go away.";
                    }
                    else
                    {
                        mensajes[0] = "Greetings emperor, you are more than welcome here.";
                        mensajes[1] = "If you want to take something, it would be 100 coins if you don't mind.";
                        mensajes[2] = "Do not move.................................................................. Here you have it.";
                        mensajes[3] = "Let me know whenever you need something.";
                        mensajes[4] = "You can't afford it, your majesty.";
                    }

                    break;
                case 5:
                    if (baseDeDatos.faccion == 8 || baseDeDatos.faccion == 3 || baseDeDatos.faccion == 4 || baseDeDatos.faccion == 5 || baseDeDatos.faccion == 6 || baseDeDatos.faccion == 7)
                    {
                        mensajes[0] = "Welcome traveler.";
                        mensajes[1] = "Do you want to drink something for just 100 coins?";
                        mensajes[2] = "Wait a second while I serve you.................................................................. Ready.";
                        mensajes[3] = "Let me know whenever you need something.";
                        mensajes[4] = "I'm sorry but you can't afford it.";
                    }
                    else if (baseDeDatos.faccion == 0 || baseDeDatos.faccion == 2)
                    {
                        mensajes[0] = "Welcome comrade.";
                        mensajes[1] = "Do you want to drink something for just 50 coins?";
                        mensajes[2] = "Wait a second while I serve you.................................................................. Ready.";
                        mensajes[3] = "Let me know whenever you need something.";
                        mensajes[4] = "I'm sorry but you can't afford it.";
                    }
                    else if (baseDeDatos.faccion == 9)
                    {
                        mensajes[0] = "Why the hell did you come here?";
                        mensajes[1] = "If you want to take something here for you are 200 coins.";
                        mensajes[2] = "Do not move.................................................................. Here you have it.";
                        mensajes[3] = "Take it fast and go away.";
                        mensajes[4] = "You can't afford it so go away.";
                    }
                    else
                    {
                        mensajes[0] = "Greetings emperor, you are more than welcome here.";
                        mensajes[1] = "If you want to take something, it would be 100 coins if you don't mind.";
                        mensajes[2] = "Do not move.................................................................. Here you have it.";
                        mensajes[3] = "Let me know whenever you need something.";
                        mensajes[4] = "You can't afford it, your majesty.";
                    }

                    break;
            }
        }
        else
        {
            switch (indice)
            {
                case 0:
                    mensajes[0] = "Hola cariño. Te noto algo cansado.";
                    mensajes[1] = "¿Quieres que te de algo para que te sientas mejor?";
                    mensajes[2] = "Espera un segundo mientras te lo preparo.................................................................. Listo";
                    mensajes[3] = "Vuelve siempre que necesites algo cielo.";
                    break;
                case 1:
                    mensajes[0] = "Hola usuario, soy tu robot médico de confianza. Yo me encargo de curar cualquier problema que tengas a un módico precio.";
                    mensajes[1] = "¿Quieres que cure a tu equipo por tan solo 50 monedas?";
                    mensajes[2] = "Espera un segundo mientras te aplicamos el tratamiento.................................................................. Listo";
                    mensajes[3] = "Avísame siempre que necesites algo";
                    mensajes[4] = "Lo siento no dispones de suficientes fondos para afrontar el pago de mis servicios. Vuelve cuando seas solvente.";
                    break;
                case 2:
                    mensajes[0] = "Bienvenido viajero a nuestra humilde posada.";
                    mensajes[1] = "¿Quieres tomarte nuestro plato estrella por tan solo 75 monedas? Esto recuperará tus puntos de vida y tus ER.";
                    mensajes[2] = "Espera un segundo mientras lo preparamos todo.................................................................. Listo";
                    mensajes[3] = "Avísame siempre que necesites algo";
                    mensajes[4] = "Lo siento no dispones de suficiente dinero para permitírtelo.";
                    break;
                case 3:
                    if (baseDeDatos.faccion == 0 || baseDeDatos.faccion == 8 || baseDeDatos.faccion == 2 || baseDeDatos.faccion == 9)
                    {
                        mensajes[0] = "Bienvenido viajero.";
                        mensajes[1] = "¿Quieres tomarte algo por tan solo 100 monedas?";
                        mensajes[2] = "Espera un segundo mientras te lo sirvo.................................................................. Listo";
                        mensajes[3] = "Avísame siempre que necesites algo";
                        mensajes[4] = "Lo siento pero no te lo puedes permitir.";
                    }
                    else if (baseDeDatos.faccion == 3 || baseDeDatos.faccion == 4 || baseDeDatos.faccion == 5 || baseDeDatos.faccion == 6)
                    {
                        mensajes[0] = "Bienvenido compañero.";
                        mensajes[1] = "¿Quieres tomarte algo por tan solo 50 monedas?";
                        mensajes[2] = "Espera un segundo mientras te lo sirvo.................................................................. Listo";
                        mensajes[3] = "Avísame siempre que necesites algo";
                        mensajes[4] = "Lo siento pero no te lo puedes permitir.";
                    }
                    else if (baseDeDatos.faccion == 7)
                    {
                        mensajes[0] = "¿Por qué demonios has venido aquí?";
                        mensajes[1] = "Si quieres tomar algo aquí para ti son 200 monedas.";
                        mensajes[2] = "No te muevas.................................................................. Aquí tienes.";
                        mensajes[3] = "Tómatelo rápido y vete.";
                        mensajes[4] = "No te lo puedes pagar así que márchate.";
                    }
                    else
                    {
                        mensajes[0] = "Saludos emperador, es usted más que bienvenido aquí.";
                        mensajes[1] = "Si quieres tomar algo serían 100 monedas si no le importa.";
                        mensajes[2] = "No se mueva.................................................................. Aquí tiene.";
                        mensajes[3] = "Avísame siempre que necesite algo.";
                        mensajes[4] = "No se lo puedes permitir su majestad.";
                    }
                    break;
                case 4:
                    if (baseDeDatos.faccion == 0 || baseDeDatos.faccion == 8 || baseDeDatos.faccion == 2 || baseDeDatos.faccion == 9)
                    {
                        mensajes[0] = "Bienvenido viajero.";
                        mensajes[1] = "¿Quieres tomarte algo por tan solo 100 monedas?";
                        mensajes[2] = "Espera un segundo mientras te lo sirvo.................................................................. Listo";
                        mensajes[3] = "Avísame siempre que necesites algo";
                        mensajes[4] = "Lo siento pero no te lo puedes permitir.";
                    }
                    else if (baseDeDatos.faccion == 7)
                    {
                        mensajes[0] = "Bienvenido compañero.";
                        mensajes[1] = "¿Quieres tomarte algo por tan solo 50 monedas?";
                        mensajes[2] = "Espera un segundo mientras te lo sirvo.................................................................. Listo";
                        mensajes[3] = "Avísame siempre que necesites algo";
                        mensajes[4] = "Lo siento pero no te lo puedes permitir.";
                    }
                    else if (baseDeDatos.faccion == 3 || baseDeDatos.faccion == 4 || baseDeDatos.faccion == 5 || baseDeDatos.faccion == 6)
                    {
                        mensajes[0] = "¿Por qué demonios has venido aquí?";
                        mensajes[1] = "Si quieres tomar algo aquí para ti son 200 monedas.";
                        mensajes[2] = "No te muevas.................................................................. Aquí tienes.";
                        mensajes[3] = "Tómatelo rápido y vete.";
                        mensajes[4] = "No te lo puedes pagar así que márchate.";
                    }
                    else
                    {
                        mensajes[0] = "Saludos emperador, es usted más que bienvenido aquí.";
                        mensajes[1] = "Si quieres tomar algo serían 100 monedas si no le importa.";
                        mensajes[2] = "No se mueva.................................................................. Aquí tiene.";
                        mensajes[3] = "Avísame siempre que necesite algo.";
                        mensajes[4] = "No se lo puedes permitir su majestad.";
                    }
                    break;
                case 5:
                    if (baseDeDatos.faccion == 8 || baseDeDatos.faccion == 3 || baseDeDatos.faccion == 4 || baseDeDatos.faccion == 5 || baseDeDatos.faccion == 6 || baseDeDatos.faccion == 7)
                    {
                        mensajes[0] = "Bienvenido viajero.";
                        mensajes[1] = "¿Quieres tomarte algo por tan solo 100 monedas?";
                        mensajes[2] = "Espera un segundo mientras te lo sirvo.................................................................. Listo";
                        mensajes[3] = "Avísame siempre que necesites algo";
                        mensajes[4] = "Lo siento pero no te lo puedes permitir.";
                    }
                    else if (baseDeDatos.faccion == 0 || baseDeDatos.faccion == 2)
                    {
                        mensajes[0] = "Bienvenido compañero.";
                        mensajes[1] = "¿Quieres tomarte algo por tan solo 50 monedas?";
                        mensajes[2] = "Espera un segundo mientras te lo sirvo.................................................................. Listo";
                        mensajes[3] = "Avísame siempre que necesites algo";
                        mensajes[4] = "Lo siento pero no te lo puedes permitir.";
                    }
                    else if (baseDeDatos.faccion == 9)
                    {
                        mensajes[0] = "¿Por qué demonios has venido aquí?";
                        mensajes[1] = "Si quieres tomar algo aquí para ti son 200 monedas.";
                        mensajes[2] = "No te muevas.................................................................. Aquí tienes.";
                        mensajes[3] = "Tómatelo rápido y vete.";
                        mensajes[4] = "No te lo puedes pagar así que márchate.";
                    }
                    else
                    {
                        mensajes[0] = "Saludos emperador, es usted más que bienvenido aquí.";
                        mensajes[1] = "Si quieres tomar algo serían 100 monedas si no le importa.";
                        mensajes[2] = "No se mueva.................................................................. Aquí tiene.";
                        mensajes[3] = "Avísame siempre que necesite algo.";
                        mensajes[4] = "No se lo puedes permitir su majestad.";
                    }
                    break;
            }
        }
    }
}
