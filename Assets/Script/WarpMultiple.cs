using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class WarpMultiple : MonoBehaviour {
    //Punto de destino
    public GameObject[] target;
    MusicaManager musica, efectos;
    Camara camara;
    BaseDatos baseDeDatos;

    public int indiceDestino;
    int indiceCancion;
    int indiceViaje;
    int indiceAreaDestino;
    int indiceZonaDestino;

    bool start = false;
    bool isFadeIn = false;
    public bool cambiaCancion;
    public bool cambiaPos;

    float alpha = 0;
    float fadeTime = 1f;
    string nombreMapa;
    int nivel;

    MapaActivo controlMapas;
    GameObject area;


    void Awake()
    {
        Assert.IsNotNull(target);

        GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;

        nivel = -1;
        indiceViaje = -1;
        area = GameObject.FindGameObjectWithTag("Area");
        musica = GameObject.Find("Musica").GetComponent<MusicaManager>();
        efectos = GameObject.Find("EfectosSonido").GetComponent<MusicaManager>();
        camara = GameObject.Find("Main Camera").GetComponent<Camara>();
        baseDeDatos = GameObject.Find("GameManager").GetComponent<BaseDatos>();
        controlMapas = GameObject.Find("GameManager").GetComponent<MapaActivo>();
    }




    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (indiceViaje != -1)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<Animator>().enabled = false;
                other.GetComponent<ControlJugador>().enabled = false;
                FadeIn();
                efectos.ProduceEfecto(0);

                yield return new WaitForSeconds(fadeTime);

                if (cambiaPos)
                {
                    ControlJugador controlador;
                    controlador = other.GetComponent<ControlJugador>();

                    if (controlador.mover == ControlJugador.movimiento.ABAJO)
                    {
                        controlador.mov = new Vector2(0f, 2f);
                        controlador.Animacion();
                    }
                    else if (controlador.mover == ControlJugador.movimiento.ARRIBA)
                    {
                        controlador.mov = new Vector2(0f, -2f);
                        controlador.Animacion();
                    }
                    else if (controlador.mover == ControlJugador.movimiento.IZQUIERDA)
                    {
                        controlador.mov = new Vector2(2f, 0f);
                        controlador.Animacion();
                    }
                    else
                    {
                        controlador.mov = new Vector2(-2f, 0f);
                        controlador.Animacion();
                    }

                }

                //Aqui se establece el indice de destino
                if (indiceDestino >= 0 && indiceDestino <= 29)
                {
                    controlMapas.EnciendeMapa(indiceDestino);
                }
                other.transform.position = target[indiceViaje].transform.GetChild(0).transform.position;

                camara.FijaCamara(indiceAreaDestino, indiceZonaDestino);

                FadeOut();
                Teleport teleporte = this.transform.parent.parent.GetComponent<Teleport>();
                teleporte.ApagaTeleport();

                if (cambiaCancion)
                {
                    musica.CambiaCancion(indiceCancion);
                }

                other.GetComponent<Animator>().enabled = true;
                other.GetComponent<ControlJugador>().enabled = true;

                StartCoroutine(area.GetComponent<Area>().ShowArea(nombreMapa, nivel));

                indiceViaje = -1;
            }
        }
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
                start = false;
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



    public void EstableceNombre(int indice)
    {
        nombreMapa = "";
        indiceDestino = indice;
        indiceViaje = -1;
        baseDeDatos.indiceInicial = indice;

        switch (indiceDestino)
        {
            case 0:
                nombreMapa = "Pueblo Origen";
                indiceViaje = 0;
                indiceAreaDestino = 1;
                indiceZonaDestino = 3;
                break;
            case 2:
                nombreMapa = "El Paso";
                indiceViaje = 1;
                indiceAreaDestino = 3;
                indiceZonaDestino = 3;
                break;
            case 4:
                nombreMapa = "Pedrán";
                indiceViaje = 2;
                indiceAreaDestino = 5;
                indiceZonaDestino = 0;
                break;
            case 10:
                nombreMapa = "Universidad";
                indiceViaje = 3;
                indiceAreaDestino = 7;
                indiceZonaDestino = 0;
                break;
            case 12:
                nombreMapa = "Canda";
                indiceViaje = 4;
                indiceAreaDestino = 9;
                indiceZonaDestino = 0;
                break;
            case 13:
                nombreMapa = "Bosque Esperanza";
                indiceViaje = 5;
                indiceAreaDestino = 10;
                indiceZonaDestino = 0;
                break;
        }

        EstableceCancion();
    }



    public void EstableceNombreIngles(int indice)
    {
        nombreMapa = "";
        indiceDestino = indice;
        indiceViaje = -1;
        baseDeDatos.indiceInicial = indice;

        switch (indiceDestino)
        {
            case 0:
                nombreMapa = "Origin Town";
                indiceViaje = 0;
                indiceAreaDestino = 1;
                indiceZonaDestino = 3;
                break;
            case 2:
                nombreMapa = "El Paso";
                indiceAreaDestino = 3;
                indiceZonaDestino = 3;
                break;
            case 4:
                nombreMapa = "Pedrán";
                indiceViaje = 2;
                indiceAreaDestino = 5;
                indiceZonaDestino = 0;
                break;
            case 10:
                nombreMapa = "University";
                indiceViaje = 3;
                indiceAreaDestino = 7;
                indiceZonaDestino = 0;
                break;
            case 12:
                nombreMapa = "Canda";
                indiceViaje = 4;
                indiceAreaDestino = 9;
                indiceZonaDestino = 0;
                break;
            case 13:
                nombreMapa = "Hope Forest";
                indiceViaje = 5;
                indiceAreaDestino = 10;
                indiceZonaDestino = 0;
                break;
        }

        EstableceCancion();
    }



    void EstableceCancion()
    {
        indiceCancion = -1;

        if (indiceDestino == 1 || indiceDestino == 13 || indiceDestino == 5 || indiceDestino == 11 || indiceDestino == 22 || indiceDestino == 26 || indiceDestino == 28)
        {
            indiceCancion = 1;
        }
        else if (indiceDestino == 3 || indiceDestino == 6 || indiceDestino == 8 || indiceDestino == 6 || indiceDestino == 24)
        {
            indiceCancion = 8;
        }
        else
        {
            switch (indiceDestino)
            {
                case 0:
                    indiceCancion = 0;
                    break;
                case 2:
                    indiceCancion = 2;
                    break;
                case 4:
                    indiceCancion = 3;
                    break;
                case 10:
                    indiceCancion = 4;
                    break;
                case 12:
                    indiceCancion = 7;
                    break;
                case 19:
                    indiceCancion = -2;
                    break;
                case 21:
                    indiceCancion = -8;
                    break;
                case 43:
                    indiceCancion = 9;
                    break;
            }
        }
    }
}
