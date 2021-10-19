using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaMonstruos : MonoBehaviour
{
    [Range(0, 100)]
    public int probabilidadMonstruo;

    public int nivelMin;

    bool activo;

    GameObject manager;
    NuevoTablero tablero;
    BaseDatos baseDeDatos;

    MusicaManager efectos;
    MusicaManager musica;

    [SerializeField]
    int[] monstruos;


    void Start()
    {
        manager = GameObject.Find("GameManager");
        tablero = manager.GetComponent<NuevoTablero>();
        baseDeDatos = manager.GetComponent<BaseDatos>();
        activo = true;

        efectos = GameObject.Find("EfectosSonido").GetComponent<MusicaManager>();
        musica = GameObject.Find("Musica").GetComponent<MusicaManager>();
    }



    void CalcularZonaM()
    {
        if (activo)
        {
            if (!tablero.recienSalido)
            {
                int aux = Random.Range(0, 500);

                if (aux <= probabilidadMonstruo)
                {
                    Personajes enemigo1, enemigo2, enemigo3;
                    aux = Random.Range(0, 100);
                    int aux2 = Random.Range(0, monstruos.Length);

                    int numeroEnemigos = 1;

                    enemigo1 = baseDeDatos.BuscarPersonajeIndice(monstruos[aux2], false);
                    enemigo2 = null;
                    enemigo3 = null;

                    if (aux > 30)
                    {
                        aux2 = Random.Range(0, monstruos.Length);
                        enemigo2 = baseDeDatos.BuscarPersonajeIndice(monstruos[aux2], false);
                        numeroEnemigos = 2;
                    }

                    if (aux > 80)
                    {
                        aux2 = Random.Range(0, monstruos.Length);
                        enemigo3 = baseDeDatos.BuscarPersonajeIndice(monstruos[aux2], false);
                        numeroEnemigos = 3;
                    }


                    AnimacionCombate.IniciaCombate();
                    efectos.ProduceEfecto(1);
                    tablero.IniciarCombate(enemigo1, enemigo2, enemigo3, numeroEnemigos, nivelMin, false, -1, -1);
                    musica.CambiaCancion(11);
                }
                else
                {
                    StartCoroutine(Espera());
                }
            }
            else
            {
                StartCoroutine(EsperaSalida());
            }
        }

    }



    IEnumerator Espera()
    {
        activo = false;

        yield return new WaitForSeconds(0.1f);

        activo = true;
    }



    IEnumerator EsperaSalida()
    {
        activo = false;

        yield return new WaitForSeconds(0.5f);

        activo = true;
        tablero.recienSalido = false;
    }
}
