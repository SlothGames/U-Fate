using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicaPantallaInicial : MonoBehaviour {
    Scene scene;

    bool iniciado;
    public bool efecto;
    public AudioClip[] canciones;
    /*
     * 0 --> Mover
     * 1 --> Confirmar
     * 2 --> Volver
     */ 
    AudioSource fuenteAudio;

    bool pulsado;

    float digitalX;
    float digitalY;

    void Awake ()
    {
        DontDestroyOnLoad(gameObject);
        iniciado = false;
        fuenteAudio = GetComponent<AudioSource>();
    }
	
	

	void Update ()
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

        if (!iniciado)
        {
            if (scene.name == "Juego")
            {
                iniciado = true;
            }
            else if(scene.name == "Portada")
            {
                if (efecto)
                {
                    if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                    {
                        ProduceEfecto(1);
                    }
                    else if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("B"))
                    {
                        ProduceEfecto(2);
                    }
                    else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) 
                        || (!pulsado && digitalY < 0) || (!pulsado && digitalY > 0))
                    {
                        pulsado = true;
                        ProduceEfecto(0);
                    }
                }
            }
            else
            {
                scene = SceneManager.GetActiveScene();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }



    void ProduceEfecto(int indice)
    {
        fuenteAudio.clip = canciones[indice];
        fuenteAudio.Play();
    }
}
