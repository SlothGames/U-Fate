using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FichasAjedrez : System.Object
{
    public enum tipoFicha
    {
        PEON,
        TORRE,
        ALFIL,
        CABALLO,
        REY,
        REINA
    }

    public int vidas;

    public tipoFicha tipo;
    
    public bool escudo;
    public bool vive;
    public int posX, posY;

    public FichasAjedrez() { }

    public FichasAjedrez(FichasAjedrez referencia)
    {
        tipo = referencia.tipo;
        vidas = referencia.vidas;
        escudo = referencia.escudo;
        vive = referencia.vive;
        posX = referencia.posX;
        posY = referencia.posY;
    }



    public FichasAjedrez IniciaFicha(int indice)
    {
        escudo = false;
        vive = true;
        posX = posY = 0;

        switch (indice)
        {
            case 0:
                tipo = tipoFicha.PEON;
                vidas = 2;
                break;
            case 1:
                tipo = tipoFicha.TORRE;
                vidas = 4;
                break;
            case 2:
                tipo = tipoFicha.ALFIL;
                vidas = 2;
                break;
            case 3:
                tipo = tipoFicha.CABALLO;
                vidas = 3;
                break;
            case 4:
                tipo = tipoFicha.REINA;
                vidas = 3;
                break;
            case 5:
                tipo = tipoFicha.REY;
                vidas = 2;
                break;

        }
        return this;
    }
}
