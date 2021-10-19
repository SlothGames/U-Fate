using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConfiguracionData
{
    public bool pantallaCompleta;
    public int numeroVolumen, posResolucion, posVolumen;
    public float volumen;



    public ConfiguracionData(Ajustes ajuste)
    {
        pantallaCompleta = ajuste.pantallaCompleta;
        posResolucion = ajuste.posResolucion;
        volumen = ajuste.volumen;
        posVolumen = ajuste.posVolumen;
    }
}
