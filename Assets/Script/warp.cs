using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/*
Código para el cambio de escenario
*/

public class warp : MonoBehaviour {
    //Punto de destino
	public GameObject target;
    MusicaManager musica, efectos;

    public int indiceDestino;
    int indiceCancion;

	bool start = false;
	bool isFadeIn = false;
    public int indiceAreaDestino;
    public int indiceZonaDestino;
    public int direccionPersonaje;

	float alpha = 0;
	float fadeTime = 1f;
    string nombreMapa;
    int nivel;

	GameObject area;
    Camara camara;
    BaseDatos baseDeDatos;
    MapaActivo controlMapas;


    void Start()
    {
        camara = GameObject.Find("Main Camera").GetComponent<Camara>();
        Assert.IsNotNull (target);
        baseDeDatos = GameObject.Find("GameManager").GetComponent<BaseDatos>();

        GetComponent<SpriteRenderer> ().enabled = false;
		transform.GetChild(0).GetComponent<SpriteRenderer> ().enabled = false;

        EstableceCancion();

        area = GameObject.FindGameObjectWithTag ("Area");
        musica = GameObject.Find("Musica").GetComponent<MusicaManager>();
        efectos = GameObject.Find("EfectosSonido").GetComponent<MusicaManager>();
        controlMapas = GameObject.Find("GameManager").GetComponent<MapaActivo>();
    }



	IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Animator>().enabled = false;
            other.GetComponent<ControlJugador>().enabled = false;
            FadeIn();
            efectos.ProduceEfecto(0);

            if (baseDeDatos.idioma == 1)
            {
                EstableceNombreIngles();
            }
            else
            {
                EstableceNombre();
            }

            yield return new WaitForSeconds(fadeTime);

            ControlJugador controlador;
            controlador = other.GetComponent<ControlJugador>();

            if (direccionPersonaje == 0) //Norte
            {
                controlador.mov = new Vector2(0f, 2f);
                controlador.Animacion();
            }
            else if (direccionPersonaje == 1) //Sur
            {
                controlador.mov = new Vector2(0f, -2f);
                controlador.Animacion();
            }
            else if (direccionPersonaje == 2) //Este
            {
                controlador.mov = new Vector2(2f, 0f);
                controlador.Animacion();
            }
            else //Oeste
            {
                controlador.mov = new Vector2(-2f, 0f);
                controlador.Animacion();
            }

            camara.FijaCamara(indiceAreaDestino, indiceZonaDestino);
            other.transform.position = target.transform.GetChild(0).transform.position;

            FadeOut();

            if (indiceCancion >= 0)
            {
                musica.CambiaCancion(indiceCancion);
            }

            other.GetComponent<Animator>().enabled = true;
            other.GetComponent<ControlJugador>().enabled = true;

            StartCoroutine(area.GetComponent<Area>().ShowArea(nombreMapa, nivel));

            if (indiceDestino <= 29 && indiceDestino >= 0)
            {
                baseDeDatos.indiceInicial = indiceDestino;
            }

            if (!baseDeDatos.zonaVisitada[baseDeDatos.indiceInicial])
            {
                DesbloqueaRegion();
            }
        }
	}



	void OnGUI(){
		if (!start)
			return;

		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);

		Texture2D tex;
		tex = new Texture2D (1, 1);
		tex.SetPixel (0, 0, Color.black);
		tex.Apply ();

		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), tex);

		if (isFadeIn) {
			alpha = Mathf.Lerp (alpha, 1.1f, fadeTime * Time.deltaTime);
		} else {
			alpha = Mathf.Lerp (alpha, -0.1f, fadeTime * Time.deltaTime);

			if (alpha < 0)
				start = false;
		}
	}



	void FadeIn(){
		start = true;
		isFadeIn = true;
	}



	void FadeOut(){
		isFadeIn = false;
	}



    void EstableceNombre()
    {
        nombreMapa = "";
        nivel = -1;

        switch (indiceDestino)
        {
            case -1:
                nombreMapa = "";
                break;
            case 0:
                nombreMapa = "Pueblo Origen";
                break;
            case 1:
                nombreMapa = "R5";
                nivel = 4;
                break;
            case 2:
                nombreMapa = "El Paso";
                break;
            case 3:
                nombreMapa = "R6";
                nivel = 6;
                break;
            case 4:
                nombreMapa = "Pedrán";
                break;
            case 5:
                nombreMapa = "R7";
                nivel = 8;
                break;
            case 6:
                nombreMapa = "R8";
                nivel = 10;
                break;
            case 7:
                nombreMapa = "Pueblo Bosque";
                break;
            case 8:
                nombreMapa = "R9";
                nivel = 10;
                break;
            case 9:
                nombreMapa = "Pueblo Rio";
                break;
            case 10:
                nombreMapa = "Universidad";
                break;
            case 11:
                nombreMapa = "R10";
                nivel = 15;
                break;
            case 12:
                nombreMapa = "Canda";
                break;
            case 13:
                nombreMapa = "Bosque Esperanza";
                break;
            case 14:
                nombreMapa = "R11";
                nivel = 15;
                break;
            case 15:
                nombreMapa = "Campus 2";
                break;
            case 17:
                nombreMapa = "Pueblo Refugio";
                break;
            case 18:
                nombreMapa = "Gran Gruta";
                nivel = 15;
                break;
            case 19:
                nombreMapa = "Pueblo Arena";
                break;
            case 20:
                nombreMapa = "R12";
                nivel = 15;
                break;
            case 21:
                nombreMapa = "Manfa";
                break;
            case 22:
                nombreMapa = "R4";
                nivel = 15;
                break;
            case 23:
                nombreMapa = "Gran Academia";
                break;
            case 24:
                nombreMapa = "R1";
                nivel = 15;
                break;
            case 25:
                nombreMapa = "Ciudad Imperial";
                break;
            case 26:
                nombreMapa = "R2";
                nivel = 15;
                break;
            case 27:
                nombreMapa = "Pueblo";
                break;
            case 28:
                nombreMapa = "R3";
                nivel = 15;
                break;
            case 29:
                nombreMapa = "Porto Bello";
                break;
            case 30:
                nombreMapa = "Ala Norte - PB";
                break;
            case 31:
                nombreMapa = "Ala Norte - P1";
                break;
            case 32:
                nombreMapa = "Aula 0.1";
                break;
            case 33:
                nombreMapa = "Aula 0.2";
                break;
            case 34:
                nombreMapa = "Aula 0.3";
                break;
            case 35:
                nombreMapa = "Aula 0.4";
                break;
            case 36:
                nombreMapa = "Despacho de Albert";
                break;
            case 37:
                nombreMapa = "Despacho de Lionetta";
                break;
            case 38:
                nombreMapa = "Despacho de Kwolek";
                break;
            case 39:
                nombreMapa = "Despacho de Elric";
                break;
            case 40:
                nombreMapa = "Ala Oeste - PB";
                break;
            case 41:
                nombreMapa = "Ala Oeste - Sotano";
                nivel = 17;
                break;
            case 42:
                nombreMapa = "Ala Oeste - P1";
                break;
            case 43:
                nombreMapa = "Base Resistencia - PB";
                break;
            case 44:
                nombreMapa = "Base Resistencia - P1";
                break;
            case 45:
                nombreMapa = "Base Resistencia - Laboratorio";
                break;
            case 46:
                nombreMapa = "Base Resistencia - Armería";
                break;
            case 47:
                nombreMapa = "Base Resistencia - Desván";
                break;
            case 48:
                nombreMapa = "Ala Norte - P2";
                break;
            case 49:
                nombreMapa = "Despacho del Director";
                break;
            case 50:
                nombreMapa = "";
                break;
        }

        if(indiceDestino >= 0 && indiceDestino <= 29)
        {
            controlMapas.EnciendeMapa(indiceDestino);
        }
    }



    void EstableceNombreIngles()
    {
        nombreMapa = "";
        nivel = -1;

        switch (indiceDestino)
        {
            case -1:
                nombreMapa = "";
                break;
            case 0:
                nombreMapa = "Origin Town";
                break;
            case 1:
                nombreMapa = "R5";
                nivel = 4;
                break;
            case 2:
                nombreMapa = "El Paso";
                break;
            case 3:
                nombreMapa = "R6";
                nivel = 6;
                break;
            case 4:
                nombreMapa = "Pedrán";
                break;
            case 5:
                nombreMapa = "R7";
                nivel = 8;
                break;
            case 6:
                nombreMapa = "R8";
                nivel = 10;
                break;
            case 7:
                nombreMapa = "Forest Town";
                break;
            case 8:
                nombreMapa = "R9";
                nivel = 10;
                break;
            case 9:
                nombreMapa = "River Town";
                break;
            case 10:
                nombreMapa = "University";
                break;
            case 11:
                nombreMapa = "R10";
                nivel = 15;
                break;
            case 12:
                nombreMapa = "Canda";
                break;
            case 13:
                nombreMapa = "Hope Forest";
                break;
            case 14:
                nombreMapa = "R11";
                nivel = 15;
                break;
            case 15:
                nombreMapa = "New University";
                break;
            case 17:
                nombreMapa = "Refuge Town";
                break;
            case 18:
                nombreMapa = "Big Grotto";
                nivel = 15;
                break;
            case 19:
                nombreMapa = "Sand Town";
                break;
            case 20:
                nombreMapa = "R12";
                nivel = 15;
                break;
            case 21:
                nombreMapa = "Manfa";
                break;
            case 22:
                nombreMapa = "R4";
                nivel = 15;
                break;
            case 23:
                nombreMapa = "Great Temple of Ancia";
                break;
            case 24:
                nombreMapa = "R1";
                nivel = 15;
                break;
            case 25:
                nombreMapa = "Imperial City";
                break;
            case 26:
                nombreMapa = "R2";
                nivel = 15;
                break;
            case 27:
                nombreMapa = "Albay Town";
                break;
            case 28:
                nombreMapa = "R3";
                nivel = 15;
                break;
            case 29:
                nombreMapa = "Porto Bello";
                break;
            case 30:
                nombreMapa = "North Building - PB";
                break;
            case 31:
                nombreMapa = "North Building - P1";
                break;
            case 32:
                nombreMapa = "Room 0.1";
                break;
            case 33:
                nombreMapa = "Room 0.2";
                break;
            case 34:
                nombreMapa = "Room 0.3";
                break;
            case 35:
                nombreMapa = "Room 0.4";
                break;
            case 36:
                nombreMapa = "Albert's office";
                break;
            case 37:
                nombreMapa = "Lionetta's office";
                break;
            case 38:
                nombreMapa = "Kwolek's office";
                break;
            case 39:
                nombreMapa = "Elric's office";
                break;
            case 40:
                nombreMapa = "West Building - PB";
                break;
            case 41:
                nombreMapa = "West Building - P-1";
                nivel = 17;
                break;
            case 42:
                nombreMapa = "West Building - P1";
                break;
            case 43:
                nombreMapa = "Resistance Base - PB";
                break;
            case 44:
                nombreMapa = "Resistance Base - P1";
                break;
            case 45:
                nombreMapa = "Resistance Base - Laboratory";
                break;
            case 46:
                nombreMapa = "Resistance Base - Armory";
                break;
            case 47:
                nombreMapa = "Resistance Base - Attic";
                break;
            case 48:
                nombreMapa = "North Building - P2";
                break;
            case 49:
                nombreMapa = "Despacho del Director";
                break;
            case 50:
                nombreMapa = "";
                break;
        }

        if (indiceDestino >= 0 && indiceDestino <= 29)
        {
            controlMapas.EnciendeMapa(indiceDestino);
        }
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



    void DesbloqueaRegion()
    {
        if (!baseDeDatos.zonaVisitada[baseDeDatos.indiceInicial])
        {
            baseDeDatos.zonaVisitada[baseDeDatos.indiceInicial] = true;
        }
    }
}