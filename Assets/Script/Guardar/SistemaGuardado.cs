using UnityEngine;
using System.IO; //Para trabajar con ficheros
using System.Runtime.Serialization.Formatters.Binary; //Para trabajar con los archivos binarios

public static class SistemaGuardado
{
    public static void GuardarJugador (ControlJugador jugador, ControlObjetos controlObjetos, int archivo)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path;

        if (archivo == 0)
        {
            path = Application.persistentDataPath + "/save.pe";
        }
        else if(archivo == 1)
        {
            path = Application.persistentDataPath + "/save1.pe";
        }
        else
        {
            path = Application.persistentDataPath + "/save2.pe";
        }

        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(jugador, controlObjetos);

        formatter.Serialize(stream, data);
        stream.Close();
    }



    public static PlayerData CargarJugador (int fichero)
    {
        string path;

        if (fichero == 0)
        {
            path = Application.persistentDataPath + "/save.pe";
        }
        else if (fichero == 1)
        {
            path = Application.persistentDataPath + "/save1.pe";
        }
        else
        {
            path = Application.persistentDataPath + "/save2.pe";
        }

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Fichero no encontrado en " + path);
            return null;
        }
    }



    public static void GuardarBD(BaseDatos baseDeDatos, int fichero)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string pathBD;

        if (fichero == 0)
        {
            pathBD = Application.persistentDataPath + "/inbd.pe";
        }
        else if (fichero == 1)
        {
            pathBD = Application.persistentDataPath + "/inbd1.pe";
        }
        else
        {
            pathBD = Application.persistentDataPath + "/inbd2.pe";
        }

        FileStream stream = new FileStream(pathBD, FileMode.Create);

        BDData data = new BDData(baseDeDatos);

        formatter.Serialize(stream, data);
        stream.Close();
    }



    public static BDData CargarBD(int fichero)
    {
        string pathBD;

        if (fichero == 0)
        {
            pathBD = Application.persistentDataPath + "/inbd.pe";
        }
        else if (fichero == 1)
        {
            pathBD = Application.persistentDataPath + "/inbd1.pe";
        }
        else
        {
            pathBD = Application.persistentDataPath + "/inbd2.pe";
        }
        if (File.Exists(pathBD))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(pathBD, FileMode.Open);
            BDData data = formatter.Deserialize(stream) as BDData;

            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Fichero no encontrado en " + pathBD);
            return null;
        }
    }



    public static void GuardarConfiguracion(Ajustes ajuste)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string pathAjuste = Application.persistentDataPath + "/aju.pe";

        FileStream stream = new FileStream(pathAjuste, FileMode.Create);

        ConfiguracionData data = new ConfiguracionData(ajuste);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    


    public static ConfiguracionData CargarConfiguracion()
    {
        string pathAjuste = Application.persistentDataPath + "/aju.pe";

        if (File.Exists(pathAjuste))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(pathAjuste, FileMode.Open);

            ConfiguracionData data = formatter.Deserialize(stream) as ConfiguracionData;
            stream.Close();

            return data;
        }
        else
        {
            return null;
        }
    }



    public static void GuardarMusica(MusicaManager musica, int fichero)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string pathMus;

        if (fichero == 0)
        {
            pathMus = Application.persistentDataPath + "/mus.pe";
        }
        else if (fichero == 1)
        {
            pathMus = Application.persistentDataPath + "/mus1.pe";
        }
        else
        {
            pathMus = Application.persistentDataPath + "/mus2.pe";
        }

        FileStream stream = new FileStream(pathMus, FileMode.Create);

        MusicaData data = new MusicaData(musica);

        formatter.Serialize(stream, data);
        stream.Close();
    }



    public static MusicaData CargarMusica(int fichero)
    {
        string pathMus;

        if (fichero == 0)
        {
            pathMus = Application.persistentDataPath + "/mus.pe";
        }
        else if (fichero == 1)
        {
            pathMus = Application.persistentDataPath + "/mus1.pe";
        }
        else
        {
            pathMus = Application.persistentDataPath + "/mus2.pe";
        }

        if (File.Exists(pathMus))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(pathMus, FileMode.Open);

            MusicaData data = formatter.Deserialize(stream) as MusicaData;
            stream.Close();

            return data;
        }
        else
        {
            return null;
        }
    }



    public static void BorrarFichero(int fichero)
    {
        string path;
        string pathBD;
        string pathMus;

        if (fichero == 0)
        {
            path = Application.persistentDataPath + "/save.pe";
            pathBD = Application.persistentDataPath + "/inbd.pe";
            pathMus = Application.persistentDataPath + "/mus.pe";
        }
        else if (fichero == 1)
        {
            path = Application.persistentDataPath + "/save1.pe";
            pathBD = Application.persistentDataPath + "/inbd1.pe";
            pathMus = Application.persistentDataPath + "/mus1.pe";
        }
        else
        {
            path = Application.persistentDataPath + "/save2.pe";
            pathBD = Application.persistentDataPath + "/inbd2.pe";
            pathMus = Application.persistentDataPath + "/mus2.pe";
        }

        File.Delete(path);
        File.Delete(pathBD);
        File.Delete(pathMus);
    }
}
