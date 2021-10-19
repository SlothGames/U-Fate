using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipamiento : MonoBehaviour {
    public bool[] objetosEquipados;
    public int[] indiceObjetoEquipo;
    /*
     *  0 --> Cabeza
     *  1 --> Cuerpo
     *  2 --> Botas
     *  3 --> Complemento
     *  5 --> Arma
     *  6 --> Escudo
    */

    // Use this for initialization
    void Awake ()
    {
        objetosEquipados = new bool[6];
        indiceObjetoEquipo = new int[6];

        for (int i = 0; i < 6; i++)
        {
            objetosEquipados[i] = false;
            indiceObjetoEquipo[i] = -1;
        }

    }
}
