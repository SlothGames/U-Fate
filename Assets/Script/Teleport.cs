using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{
    bool animacionActiva;
    bool pulsado;
    bool aceptar;
    bool activo;
    bool menuActivo;

    int numeroMensajes;
    int pos;
    int posMensaje;

    ControlJugador controlJugador;
    public GameObject menuTeleport;
    MusicaManager efectos;
    BaseDatos baseDeDatos;
    Animator animPer, animPortal;
    Mapa mapa;

    float digitalX;
    float digitalY;

    string[] mensajes;

    public Sprite[] imagenOrientacion = new Sprite[4];
    public int orientacion; //0 - Norte, 1 - Este, 2 - Oeste, 3 - Sur 



    void Start()
    {
        controlJugador = GameObject.Find("Player").GetComponent<ControlJugador>();
        efectos = GameObject.Find("EfectosSonido").GetComponent<MusicaManager>();
        mapa = GameObject.Find("MensajesEnPantalla").transform.GetChild(12).GetComponent<Mapa>();
        baseDeDatos = GameObject.Find("GameManager").GetComponent<BaseDatos>();

        aceptar = false;
        activo = false;
        animacionActiva = false;

        pos = 0;

        animPer = GetComponent<Animator>();
        animPortal = this.transform.GetChild(0).GetComponent<Animator>();

        menuTeleport.transform.GetChild(2).gameObject.SetActive(false);
        DesactivarMenu();
        DesactivaTeleport();
    }



    void Update()
    {
        if (activo)
        {
            if (!baseDeDatos.teleportActivo)
            {
                if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                {
                    if (posMensaje == 0)
                    {
                        posMensaje++;
                        Escribir(mensajes[posMensaje]);
                    }
                    else
                    {
                        DesactivaTeleport();
                    }
                }
            }
            else
            {
                if (!animacionActiva)
                {
                    if (!mapa.activo && !mapa.destinoElegido && !mapa.volver)
                    {
                        if (!menuActivo)
                        {
                            if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                            {
                                efectos.ProduceEfecto(10);

                                if (menuTeleport.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text == mensajes[posMensaje])
                                {
                                    if (!controlJugador.teleportMostrado)
                                    {
                                        if (posMensaje == 0 || posMensaje == 1)
                                        {
                                            posMensaje++;
                                            Escribir(mensajes[posMensaje]);
                                        }
                                        else
                                        {
                                            DesactivaTeleport();
                                            controlJugador.teleportMostrado = true;
                                        }
                                    }
                                    else
                                    {
                                        if (posMensaje == 0)
                                        {
                                            posMensaje++;
                                            Escribir(mensajes[posMensaje]);
                                        }
                                        else
                                        {
                                            DesactivaTeleport();
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
                                efectos.ProduceEfecto(11);
                                if (pos == 0)
                                {
                                    pos = 1;
                                    menuTeleport.transform.GetChild(2).transform.position = menuTeleport.transform.GetChild(4).transform.position;
                                }
                                else
                                {
                                    pos = 0;
                                    menuTeleport.transform.GetChild(2).transform.position = menuTeleport.transform.GetChild(3).transform.position;
                                }

                                pulsado = true;
                            }
                            else if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                            {
                                efectos.ProduceEfecto(10);

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
                                    if (controlJugador.dinero >= 100)
                                    {
                                        mapa.ActivaMenu(true);
                                    }
                                    else
                                    {
                                        posMensaje = 4;

                                        if (!controlJugador.teleportMostrado)
                                        {
                                            posMensaje = 5;
                                        }

                                        Escribir(mensajes[posMensaje]);
                                    }
                                }
                                else
                                {
                                    posMensaje = 3;

                                    if (!controlJugador.teleportMostrado)
                                    {
                                        posMensaje = 4;
                                    }
                                    Escribir(mensajes[posMensaje]);
                                }

                                DesactivarMenu();
                            }
                        }
                    }
                    else if (mapa.destinoElegido)
                    {
                        if (!controlJugador.teleportMostrado)
                        {
                            posMensaje = 3;
                        }
                        else
                        {
                            posMensaje = 2;
                        }

                        mapa.destinoElegido = false;
                        Escribir(mensajes[posMensaje]);
                        RealizaPago();
                    }
                    else if (mapa.volver)
                    {
                        if (!controlJugador.teleportMostrado)
                        {
                            posMensaje = 4;
                        }
                        else
                        {
                            posMensaje = 3;
                        }

                        mapa.volver = false;
                        Escribir(mensajes[posMensaje]);
                    }
                }
            }
        }
    }



    void DesactivarMenu()
    {
        menuActivo = false;
        menuTeleport.transform.GetChild(1).gameObject.SetActive(menuActivo);
        menuTeleport.transform.GetChild(2).gameObject.SetActive(menuActivo);
        menuTeleport.transform.GetChild(5).gameObject.SetActive(menuActivo);
    }



    void DesactivaTeleport()
    {
        activo = false;
        menuTeleport.transform.GetChild(0).gameObject.SetActive(activo);
        controlJugador.setMensajeActivo(false);
    }



    void IniciarTexto()
    {
        activo = true;
        posMensaje = 0;
        menuTeleport.transform.GetChild(0).gameObject.SetActive(activo);
        Escribir(mensajes[posMensaje]);
    }



    void IniciarMenu()
    {
        menuActivo = true;
        menuTeleport.transform.GetChild(1).gameObject.SetActive(menuActivo);

        if (baseDeDatos.idioma == 1)
        {
            menuTeleport.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "Yes \n\n No";
        }
        else
        {
            menuTeleport.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "Sí \n\n No";
        }

        pos = 0;
        menuTeleport.transform.GetChild(2).gameObject.SetActive(menuActivo);
        menuTeleport.transform.GetChild(2).transform.position = menuTeleport.transform.GetChild(3).transform.position;
        menuTeleport.transform.GetChild(5).gameObject.SetActive(menuActivo);
        menuTeleport.transform.GetChild(5).transform.GetChild(0).GetComponent<Text>().text = controlJugador.dinero + "";
    }



    public void Escribir(string t)
    {
        activo = true;
        menuTeleport.transform.GetChild(0).gameObject.SetActive(activo);
        //textBox.textTB.text = t;
        StartCoroutine(Deletrear(t));
    }



    public IEnumerator Deletrear(string t)
    {
        menuTeleport.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "";

        yield return new WaitForSeconds(.1f);

        for (int i = 0; i < t.Length + 1; i++)
        {
            menuTeleport.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = t.Substring(0, i);
            yield return new WaitForSeconds(.01f);
        }

        if (!controlJugador.teleportMostrado)
        {
            if (posMensaje == 2)
            {
                IniciarMenu();
            }
        }
        else
        {
            if (posMensaje == 1)
            {
                IniciarMenu();
            }
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



    public void IniciaTeleport()
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
        controlJugador.dinero -= 100;
        //this.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        StartCoroutine(AbrePortal());
    }



    void EstableceConversacion()
    {
        if (baseDeDatos.teleportActivo)
        {
            if (!controlJugador.teleportMostrado)
            {
                numeroMensajes = 7;
                mensajes = new string[numeroMensajes];

                if(baseDeDatos.idioma == 1)
                {
                    mensajes[0] = "Welcome, welcome... We are the Magic Teleporter Union. Our service is responsible for creating magical portals so that our customers can travel fast at a good price.";
                    mensajes[1] = "We are starting to establish our business throughout the region of Áncia as we have already achieved in other regions of the world.";
                    mensajes[2] = "For only 100 coins we will send you to the place you want as long as we have installed a portal. Do you want to try?";
                    mensajes[3] = "Thank you for trusting our service. I will open the portal.";
                    mensajes[4] = "Come back whenever you want.";
                    mensajes[5] = "I'm sorry but you are need more coins to use our service.";
                    mensajes[6] = "The portal is ready, enjoy the trip.";
                }
                else
                {
                    mensajes[0] = "Bienvenido, bienvenido... Somos el gremio de transportistas mágicos. Nuestro servicio se encarga de crear portales mágicos para que nuestros clientes puedan viajar rápido a buen precio.";
                    mensajes[1] = "Estamos empezando a establecer nuestro negocio por toda la región de Áncia como ya hemos logrado en otras regiones del mundo.";
                    mensajes[2] = "Por solo 100 monedas te enviaremos al lugar que desees siempre y cuando hayamos instalado un portal. ¿Te apetece probar?";
                    mensajes[3] = "Gracias por confiar en nuestro servicio. En seguida le abro el portal.";
                    mensajes[4] = "Vuelva siempre que lo desee.";
                    mensajes[5] = "Lo siento, pero te faltan monedas para poder emplear nuestro servicio.";
                    mensajes[6] = "El portal está listo, disfrute del viaje.";
                }

            }
            else
            {
                numeroMensajes = 6;
                mensajes = new string[numeroMensajes];

                if(baseDeDatos.idioma == 1)
                {
                    mensajes[0] = "Welcome, welcome... We are the Magic Teleporter Union. Our service is responsible for creating magical portals so that our customers can travel fast at a good price.";
                    mensajes[1] = "For only 100 coins we will send you to the place you want as long as we have installed a portal. Do you want to try?";
                    mensajes[2] = "Thank you for trusting our service. I will open the portal.";
                    mensajes[3] = "Come back whenever you want.";
                    mensajes[4] = "I'm sorry but you are need more coins to use our service.";
                    mensajes[5] = "The portal is ready, enjoy the trip.";
                }
                else
                {
                    mensajes[0] = "Bienvenido, bienvenido... Somos el gremio de transportistas mágicos. Nuestro servicio se encarga de crear portales mágicos para que nuestros clientes puedan viajar rápido a buen precio.";
                    mensajes[1] = "Por solo 100 monedas te enviaremos al lugar que desees siempre y cuando hayamos instalado un portal. ¿Te apetece probar?";
                    mensajes[2] = "Gracias por confiar en nuestro servicio. En seguida le abro el portal.";
                    mensajes[3] = "Vuelva siempre que lo desee.";
                    mensajes[4] = "Lo siento, pero te faltan monedas para poder emplear nuestro servicio.";
                    mensajes[5] = "El portal está listo, disfrute del viaje.";
                }
            }
        }
        else
        {
            numeroMensajes = 2;
            mensajes = new string[numeroMensajes];

            if (baseDeDatos.idioma == 1)
            {
                mensajes[0] = "Welcome, welcome... We are the Magic Teleporter Union. Our service is responsible for creating magical portals so that our customers can travel fast at a good price.";
                mensajes[1] = "Although feeling it a lot today we cannot offer this service, we are working to have everything ready and we hope you continue to trust our service.";
            }
            else
            {
                mensajes[0] = "Bienvenido, bienvenido... Somos el gremio de transportistas mágicos. Nuestro servicio se encarga de crear portales mágicos para que nuestros clientes puedan viajar rápido a buen precio.";
                mensajes[1] = "Aunque sintiéndolo mucho hoy no podemos ofrecer este servicio, estamos trabajando para tenerlo todo listo y esperamos sigas confiando en nuestro servicio.";
            }
        }
    }



    IEnumerator AbrePortal()
    {
        animacionActiva = true;

        animPortal.SetBool("abre", true);
        animPer.Play("Teleport-Activo");

        if (!controlJugador.teleportMostrado)
        {
            posMensaje = 6;
        }
        else
        {
            posMensaje = 5;
        }

        this.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);

        WarpMultiple warp = this.transform.GetChild(0).GetChild(0).GetComponent<WarpMultiple>();

        if(baseDeDatos.idioma == 1)
        {
            warp.EstableceNombreIngles(mapa.indiceDestino);
        }
        else
        {
            warp.EstableceNombre(mapa.indiceDestino);
        }

        yield return new WaitForSeconds(3);

        Escribir(mensajes[posMensaje]);

        animacionActiva = false;
    }



    void Conversacion()
    {
        EstableceConversacion();
        IniciaTeleport();
    }



    public void ApagaTeleport()
    {
        DesactivaTeleport();
        animacionActiva = false;
        //this.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        animPortal.SetBool("abre", false);
    }
}
