using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntermediarioJuego : MonoBehaviour
{
    bool activo;
    bool iniciado;
    public static bool cargar;
    bool enEspera;

    //private static Intermediario intermediario = null;

    public int idioma; //0 --> español   1 --> ingles
    public static int idiomaCarga; //0 --> español   1 --> ingles
    public static int ficheroCarga;

    Scene scene;
    GameObject manager;
    PantallaCarga pantallaCarga;



    void Awake()
    {
        enEspera = false;
        iniciado = false;
        DesactivaIntermediario();
        DontDestroyOnLoad(gameObject);
    }



    void Update()
    {
        if (activo)
        {
            idioma = idiomaCarga;

            if (!iniciado)
            {
                if (scene.name == "Juego")
                {
                    manager = GameObject.Find("InterfazCombate");
                    iniciado = true;
                }
                else
                {
                    scene = SceneManager.GetActiveScene();
                }
            }
            else
            {
                pantallaCarga = manager.transform.GetChild(6).GetComponent<PantallaCarga>();
                pantallaCarga.ActivaCarga(ficheroCarga);


                pantallaCarga.EstableceIdioma(idioma);

                DesactivaIntermediario();
                Destroy(gameObject);
            }
        }
        else
        {
            if (cargar)
            {
                if (!enEspera)
                {
                    IniciaIntermediario();
                }
            }
        }
    }



    public void IniciaIntermediario()
    {
        iniciado = false;
        this.transform.GetChild(0).gameObject.SetActive(true);
        StartCoroutine(Espera());
    }



    public void DesactivaIntermediario()
    {
        activo = false;
        cargar = false;
        this.transform.GetChild(0).gameObject.SetActive(activo);
    }



    IEnumerator Espera()
    {
        enEspera = true;
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Juego");
        yield return new WaitForSeconds(1);
        activo = true;
        enEspera = false;
    }
}
