using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MusicaData
{
    public int ultimoIndice;

	public MusicaData(MusicaManager musica)
    {
        ultimoIndice = musica.ultimoIndice;
    }
}
