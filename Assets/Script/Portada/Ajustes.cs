using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Ajustes : MonoBehaviour {
    public bool activo, confirmacionActiva, mueveFlecha;
    public bool retroceder, pantallaCompleta;
    public bool cargarConfiguracion;

    public int pos, posConfirmacion, posResolucion, numeroResoluciones, posVolumen;
    public float volumen;

    public GameObject flecha, flechaConf;
    public AudioMixer audioMixer;

    public Image menu;
    public Sprite confirmado, noConfirmado;

    List<string> options = new List<string>();
    Resolution[] resolutions;

    ConfiguracionData data;

    bool pulsado;

    float digitalX;
    float digitalY;

    void Awake()
    {
        data = SistemaGuardado.CargarConfiguracion();

        resolutions = Screen.resolutions;
        numeroResoluciones = resolutions.Length;

        for (int i = 0; i < numeroResoluciones; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                posResolucion = i;
            }
        }

        posConfirmacion = 1;
        
        if (data == null)
        {
            pantallaCompleta = true;
            pos = 0;
            volumen = 1f;
            posVolumen = 10;
        }
        else
        {
            CargarInfo();
        }
        
        pos = 0;
        mueveFlecha = true;
        retroceder = false;

        SetPantallaCompleta();
        SetVolumen();
        SetResolucion();
        DesactivaConfirmacion();
    }



    void Update()
    {
        if (activo)
        {
            if (mueveFlecha)
            {
                if (confirmacionActiva)
                {
                    if(posConfirmacion == 0)
                    {
                        flechaConf.transform.position = menu.transform.GetChild(10).transform.GetChild(2).transform.GetChild(1).transform.position;
                    }
                    else
                    {
                        flechaConf.transform.position = menu.transform.GetChild(10).transform.GetChild(3).transform.GetChild(1).transform.position;
                    }
                }
                else
                {
                    if (pos > 3)
                    {
                        pos = 0;
                    }
                    else if (pos < 0)
                    {
                        pos = 3;
                    }

                    flecha.transform.position = menu.transform.GetChild(6 + pos).transform.position;
                }

                mueveFlecha = false;
            }

            digitalX = Input.GetAxis("D-Horizontal");
            digitalY = Input.GetAxis("D-Vertical");

            if (pulsado)
            {
                if (digitalY == 0 && digitalX == 0)
                {
                    pulsado = false;
                }
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || (!pulsado && digitalY < 0))
            {
                pulsado = true;

                if (!confirmacionActiva)
                {
                    mueveFlecha = true;
                    pos++;
                }
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || (!pulsado && digitalY > 0))
            {
                pulsado = true;

                if (!confirmacionActiva)
                {
                    mueveFlecha = true;
                    pos--;
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || (!pulsado && digitalX < 0))
            {
                pulsado = true;

                if (!confirmacionActiva)
                {
                    if (pos == 0)
                    {
                        posResolucion++;

                        if (posResolucion == numeroResoluciones)
                        {
                            posResolucion = 0;
                        }

                        menu.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text = options[posResolucion];
                    }
                    else if (pos == 2)
                    {
                        if (volumen > -80)
                        {
                            posVolumen--;
                            volumen -= 8.2f;

                            if (volumen < -80)
                            {
                                volumen = -80;
                            }

                            SetVolumen();
                        }
                    }
                }
                else
                {
                    if(posConfirmacion == 0)
                    {
                        posConfirmacion = 1;
                    }
                    else
                    {
                        posConfirmacion = 0;
                    }

                    mueveFlecha = true;
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || (!pulsado && digitalX > 0))
            {
                pulsado = true;

                if (!confirmacionActiva)
                {
                    if (pos == 0)
                    {
                        posResolucion--;

                        if (posResolucion == -1)
                        {
                            posResolucion = numeroResoluciones - 1;
                        }

                        menu.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text = options[posResolucion];
                    }
                    else if (pos == 2)
                    {
                        if (volumen < 1)
                        {
                            posVolumen++;
                            volumen += 8.2f;

                            if (volumen > 1)
                            {
                                volumen = 1;
                            }

                            SetVolumen();
                        }
                    }
                }
                else
                {
                    if (posConfirmacion == 0)
                    {
                        posConfirmacion = 1;
                    }
                    else
                    {
                        posConfirmacion = 0;
                    }

                    mueveFlecha = true;
                }
            }
            else if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
            {
                if (!confirmacionActiva)
                {
                    if (pos == 1)
                    {
                        if (pantallaCompleta)
                        {
                            pantallaCompleta = false;
                            menu.transform.GetChild(2).transform.GetChild(0).GetComponent<Image>().sprite = noConfirmado;
                        }
                        else
                        {
                            pantallaCompleta = true;
                            menu.transform.GetChild(2).transform.GetChild(0).GetComponent<Image>().sprite = confirmado;
                        }

                    }
                    else if (pos == 3)
                    {
                        ActivarConfirmacion();
                    }
                }
                else
                {
                    if(posConfirmacion == 0)
                    {
                        /*
                        if (resolutions[posResolucion].width != Screen.currentResolution.width ||
                            resolutions[posResolucion].height != Screen.currentResolution.height)
                        {
                            SetResolucion();
                        }
                        */
                        SetResolucion();

                        if (Screen.fullScreen != pantallaCompleta)
                        {
                            SetPantallaCompleta();
                        }

                        SistemaGuardado.GuardarConfiguracion(this);

                        DesactivaConfirmacion();
                    }
                    else
                    {
                        DesactivaConfirmacion();
                    }
                }
                
            }
            else if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("B"))
            {
                ActivarConfirmacion();
            }
        }
    }



    void DesactivaMenu()
    {
        activo = false;
        menu.gameObject.SetActive(activo);
        retroceder = true;
    }



    void DesactivaConfirmacion()
    {
        confirmacionActiva = false;
        menu.transform.GetChild(10).gameObject.SetActive(false);
        DesactivaMenu();
    }



    void ActivarConfirmacion()
    {
        confirmacionActiva = true;
        menu.transform.GetChild(10).gameObject.SetActive(true);
        posConfirmacion = 1;
        mueveFlecha = true;
    }



    public void ActivaMenu()
    {
        data = SistemaGuardado.CargarConfiguracion();

        CargarInfo();

        activo = true;
        mueveFlecha = true;
        pos = 0;
        menu.gameObject.SetActive(activo);
    }



    public void SetVolumen()
    {
        switch (posVolumen)
        {
            case 0:
                menu.transform.GetChild(3).transform.GetChild(1).GetComponent<Text>().text = "0";
                break;
            case 1:
                menu.transform.GetChild(3).transform.GetChild(1).GetComponent<Text>().text = "10";
                break;
            case 2:
                menu.transform.GetChild(3).transform.GetChild(1).GetComponent<Text>().text = "20";
                break;
            case 3:
                menu.transform.GetChild(3).transform.GetChild(1).GetComponent<Text>().text = "30";
                break;
            case 4:
                menu.transform.GetChild(3).transform.GetChild(1).GetComponent<Text>().text = "40";
                break;
            case 5:
                menu.transform.GetChild(3).transform.GetChild(1).GetComponent<Text>().text = "50";
                break;
            case 6:
                menu.transform.GetChild(3).transform.GetChild(1).GetComponent<Text>().text = "60";
                break;
            case 7:
                menu.transform.GetChild(3).transform.GetChild(1).GetComponent<Text>().text = "70";
                break;
            case 8:
                menu.transform.GetChild(3).transform.GetChild(1).GetComponent<Text>().text = "80";
                break;
            case 9:
                menu.transform.GetChild(3).transform.GetChild(1).GetComponent<Text>().text = "90";
                break;
            case 10:
                menu.transform.GetChild(3).transform.GetChild(1).GetComponent<Text>().text = "100";
                break;
        }

        audioMixer.SetFloat("Volumen", volumen);
    }



    void SetPantallaCompleta()
    {
        if (pantallaCompleta)
        {
            menu.transform.GetChild(2).transform.GetChild(0).GetComponent<Image>().sprite = confirmado;
        }
        else
        {
            menu.transform.GetChild(2).transform.GetChild(0).GetComponent<Image>().sprite = noConfirmado;
        }

        Screen.fullScreen = pantallaCompleta;
    }



    void SetResolucion()
    {
        Resolution resolution = resolutions[posResolucion];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }



    void CargarInfo()
    {
        menu.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text = options[posResolucion];

        if(data != null)
        {
            pantallaCompleta = data.pantallaCompleta;

            if (!pantallaCompleta)
            {
                menu.transform.GetChild(2).transform.GetChild(0).GetComponent<Image>().sprite = noConfirmado;
            }
            else
            {
                menu.transform.GetChild(2).transform.GetChild(0).GetComponent<Image>().sprite = confirmado;
            }

            volumen = data.volumen;

            if (volumen == 1)
            {
                posVolumen = 10;
            }
            else if (volumen == -80)
            {
                posVolumen = 0;
            }
            else if (volumen == 0)
            {
                volumen = 1;
                posVolumen = 10;
                volumen = data.volumen;
            }
            else
            {
                posVolumen = data.posVolumen;
            }
        }
    }
}
