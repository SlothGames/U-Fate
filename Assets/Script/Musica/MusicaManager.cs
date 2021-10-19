using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicaManager : MonoBehaviour {
    public AudioClip[] canciones;
    /// Lista canciones
    //////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////// 
    /// Efectos:
    /// 0 --> Cambio escena
    /// 1 --> Inicio Combate
    /// 2 --> Efecto golpe físico
    /// 3 --> Efecto golpe magico
    /// 4 --> Efecto apoyo positivo
    /// 5 --> Sube Nivel
    /// 6 --> Efecto negativo
    /// 7 --> Aprende Hechizo
    /// 8 --> Monedas Tienda
    /// 9 --> Objeto Cura
    /// 10 --> Seleccionar Boton
    /// 11 --> Mover Flecha
    /// 12 --> Volver
    /// 13 --> Cofre
    /// 14 --> Comprar/Vender
    /// 15 --> Abre tienda
    /// 16 --> Error
    /// Cocina Pociones
    /// 17 --> Acierto
    /// 18 --> Fallo
    /// 19 --> Victoria
    /// 20 --> Derrota
    /// 21 --> Pierde Ronda
    /// Esgrima
    /// 22 --> Gana ronda
    /// 23 --> Fallo esgrima
    /// 24 --> Acierto esgrima
    /// Menu Pausa
    /// 25 --> Menu Pausa
    /// TextBox
    /// 26 --> Pasar mensaje
    /// 27 --> Deletreo
    /////////////////////////////////////////////////////////////////// 
    ///////////////////////////////////////////////////////////////////
    /// Musica:
    /// 0 --> Pueblo Origen
    /// 1 --> Bosque
    /// 2 --> El Paso
    /// 3 --> Pedran
    /// 4 --> Universidad
    /// 5 --> Mazmorra
    /// 6 --> Batalla 1
    /// 7 --> Canda
    /// 8 --> Paso montañoso
    /// 9 --> Base
    /// 10 --> Game Over
    /// 11 --> Combate
    /// 12 --> Combate Jefe
    /// 13 --> Combate JefeFinal
    /// 14 --> Victoria
    /// 15 --> Cocina 
    /// 16 --> Vacio
    /// 17 --> Esgrima
    /// ///////////////////////////////////////////////////////////////

    AudioSource fuenteAudio;
    public int ultimoIndice;

    void Start()
    {
        fuenteAudio = GetComponent<AudioSource>();
    }



    public void CambiaCancion(int indice)
    {
        fuenteAudio.clip = canciones[indice];
        fuenteAudio.Play();

        if(indice != 11 && indice != 12 && indice != 13 && indice != 14)
        {
            ultimoIndice = indice;
        }
    }



    public void ProduceEfecto(int indice)
    {
        fuenteAudio.clip = canciones[indice];
        fuenteAudio.Play();
    }



    public void VuelveMusica()
    {
        CambiaCancion(ultimoIndice);
    }



    public void ParaMusica()
    {
        fuenteAudio.Pause();
    }
}
