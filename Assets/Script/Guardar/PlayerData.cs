using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int dinero;
    public int dificultad;
    public int puntuacionActual;
    public float[] posicion;
    public bool teleportMostrado;

    //Control misiones secundarias
    public bool misionPedroActiva;
    public int contadorMonstruosPedro;

    public bool misionGamezActiva;
    public bool tieneGema;
    public bool rescatePagado;

    public bool[] hActiva;
    public bool misionHActiva;

    public bool misionNaniActiva;
    public bool perdonarVidaOrco, orcoDerrotado, guardiaDerrotado;


    public PlayerData (ControlJugador jugador, ControlObjetos controlObjetos)
    {
        dinero = jugador.dinero;

        posicion = new float[3];
        posicion[0] = jugador.transform.position.x;
        posicion[1] = jugador.transform.position.y;
        posicion[2] = jugador.transform.position.z;
        dificultad = jugador.GetDificultad();
        teleportMostrado = jugador.teleportMostrado;

        misionPedroActiva = controlObjetos.misionPedroActiva;
        contadorMonstruosPedro = controlObjetos.contadorMonstruosPedro;

        misionGamezActiva = controlObjetos.misionGamezActiva;
        tieneGema = controlObjetos.tieneGema;
        rescatePagado = controlObjetos.rescatePagado;
        orcoDerrotado = controlObjetos.orcoDerrotado;
        guardiaDerrotado = controlObjetos.guardiaDerrotado;

        hActiva = new bool[controlObjetos.hActiva.Length];
        
        for(int i = 0; i < hActiva.Length; i++)
        {
            hActiva[i] = controlObjetos.hActiva[i];
        }

        misionHActiva = controlObjetos.misionHActiva;

        misionNaniActiva = controlObjetos.misionNaniActiva;
        perdonarVidaOrco = controlObjetos.perdonarVidaOrco;
    }
}
