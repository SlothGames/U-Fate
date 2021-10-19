using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Personajes : System.Object {
    public enum tipoPersonaje
    {
        PEQUENIO,
        GRANDE,
        GIGANTE
    }


    public enum elementoPersonaje
    {
        FRIKI,
        FIESTERO,
        TIRANO,
        DORMILON,
        RESPONSABLE, 
        NEUTRO
    }


    public enum tipoPersonajeIngles
    {
        PEQUENIO,
        GRANDE,
        GIGANTE
    }

    //Geek Partyman Sleeper Tyrant Responsible Neutral
    
    public enum elementoPersonajeIngles
    {
        GEEK,
        PARTYMAN,
        TYRANT,
        SLEEPER,
        RESPONSIBLE, 
        NEUTRAL
    }


    public enum estadoPersonaje
    {
        DOLOR,
        NAUSEAS,
        MAREO,
        SUEÑO,
        AMNESIA,
        HEMORRAGIA,
        SANO,
        DERROTADO
    }


    public enum estadoPersonajeIngles
    {
        PAIN,
        SICKNESS,
        DIZZINESS,
        SLEEP,
        AMNESIA,
        HEMORRHAGE,
        HEALTHY,
        DEFEATED
    }

    /*
     * Friki derrota a Fiestero y Dormilon      *
        El friki pasa del fiestero y mantiene entretenido al dormilon
     * Fiestero derrota a Tirano y Responsable  *
        El fiestero ignora al tirano y corrompe al responsable
     * Tirano derrota a Friki y Responsable     *
        El tirano agobia al friki y al responsable
     * Dormilon derrota a Fiestero y Tirano     *
        El dormilon duerme ignorando al fiestero y al tirano
     * Responsable derrota a Friki y Dormilon   *
        El responsable regaña al friki y al dormilon
     * Neutral no derrota a nadie   *
        Debil contra Tirano
    */

    public string nombre;
    public string nombreIngles;

    [TextArea(10,5)]
    public string descripcion;
    [TextArea(10, 5)]
    public string descripcionIngles;

    public int indicePersonaje;

    public int ataqueFisico, ataqueFisicoActual, ataqueFisicoModificado;
    public int ataqueMagico, ataqueMagicoActual, ataqueMagicoModificado;
    public int defensaFisica, defensaFisicaActual, defensaFisicaModificada;
    public int defensaMagica, defensaMagicaActual, defensaMagicaModificada; 
    public int velocidad, velocidadActual, velocidadModificada; //una velocidad muy superior puede permitir golpear mas de una vez
    public int vida, vidaActual, vidaModificada;
    public float evasion, evasionActual;
    public float precision, precisionActual;
    public int rapido;//Como de rapido adquiere experiencia
    public int energiaInvocacion, energiaInvocacionActual;


    public Sprite imagen;
    Sprite[] imagenes;

    public int nivel; 
    public int experiencia, proximoNivel; // experiencia acumulada y experiencia necesaria para llegar al proximo nivel
    public int dinero;

    public int aumentoAtaqueFisico, aumentoAtaqueMagico;
    public int aumentoDefensaFisica, aumentoDefensaMagica;
    public int aumentoEvasion;
    public int aumentoVelocidad;

    public int numeroAtaques;
    public int[] indicesAtaque;
    public Ataque[] habilidades;
    Ataque ataque;
    public tipoPersonaje tipo;
    public tipoPersonajeIngles tipoIngles;
    public elementoPersonaje elemento;
    public elementoPersonajeIngles elementoIngles;
    public Sprite imagenElemento;
    public estadoPersonaje estado;
    public estadoPersonajeIngles estadoIngles;

    public int indiceObjeto;
    public int objetoRecompensa;


    public RuntimeAnimatorController animacion;

    public Objeto casco, armadura, botas, arma, escudo, complemento;
    public bool llevaCasco, llevaArmadura, llevaBotas, llevaArma, llevaEscudo, llevaComplemento;

    public bool desbloqueado;



    public Personajes()
    {

    }



    public Personajes(Personajes referencia)
    {
        nombre = referencia.nombre;
        nombreIngles = referencia.nombreIngles;

        descripcion = referencia.descripcion;
        descripcionIngles = referencia.descripcionIngles;

        indicePersonaje = referencia.indicePersonaje;

        ataqueFisico = referencia.ataqueFisico;
        ataqueFisicoActual = referencia.ataqueFisicoActual;
        ataqueFisicoModificado = referencia.ataqueFisicoModificado;

        ataqueMagico = referencia.ataqueMagico;
        ataqueMagicoActual = referencia.ataqueMagicoActual;
        ataqueMagicoModificado = referencia.ataqueMagicoModificado;

        defensaFisica = referencia.defensaFisica;
        defensaFisicaActual = referencia.defensaFisicaActual;
        defensaFisicaModificada = referencia.defensaFisicaModificada;

        defensaMagica = referencia.defensaMagica;
        defensaMagicaActual = referencia.defensaMagicaActual;
        defensaMagicaModificada = referencia.defensaMagicaModificada;

        velocidad = referencia.velocidad; 
        velocidadActual = referencia.velocidadActual;
        velocidadModificada = referencia.velocidadModificada;

        vida = referencia.vida; 
        vidaActual = referencia.vidaActual;
        vidaModificada = referencia.vidaModificada;

        evasion = referencia.evasion; 
        evasionActual = referencia.evasionActual;

        precision = referencia.precision;
        precisionActual = referencia.precisionActual;

        imagen = referencia.imagen;
        nivel = referencia.nivel;
        experiencia = referencia.experiencia; 
        proximoNivel = referencia.proximoNivel;
        dinero = referencia.dinero;

        aumentoAtaqueFisico = referencia.aumentoAtaqueFisico;
        aumentoAtaqueMagico = referencia.aumentoAtaqueMagico;
        aumentoDefensaFisica = referencia.aumentoDefensaFisica; 
        aumentoDefensaMagica = referencia.aumentoDefensaMagica;
        aumentoEvasion = referencia.aumentoEvasion;
        aumentoVelocidad = referencia.aumentoVelocidad;

        numeroAtaques = referencia.numeroAtaques;
        indicesAtaque = referencia.indicesAtaque;
        habilidades = referencia.habilidades;
        ataque = referencia.ataque;

        tipo = referencia.tipo;
        tipoIngles = referencia.tipoIngles;

        elemento = referencia.elemento;
        elementoIngles = referencia.elementoIngles;

        imagenElemento = referencia.imagenElemento;

        indiceObjeto = referencia.indiceObjeto;
        objetoRecompensa = referencia.objetoRecompensa;

        animacion = referencia.animacion;

        llevaCasco = referencia.llevaCasco;
        llevaArmadura = referencia.llevaArmadura;
        llevaBotas = referencia.llevaBotas;
        llevaArma = referencia.llevaArmadura;
        llevaEscudo = referencia.llevaEscudo;
        llevaComplemento = referencia.llevaComplemento;

        casco = referencia.casco;
        armadura = referencia.armadura;
        botas = referencia.botas;
        arma = referencia.arma;
        escudo = referencia.escudo;
        complemento = referencia.complemento;

        rapido = referencia.rapido;

        estado = referencia.estado;
        estadoIngles = referencia.estadoIngles;

        desbloqueado = referencia.desbloqueado;

        energiaInvocacion = referencia.energiaInvocacion;
        energiaInvocacionActual = referencia.energiaInvocacionActual;
    }



    public Personajes IniciarPersonaje(int valor, bool aliado)
    {
        //Temporal
        energiaInvocacionActual = 0;
        energiaInvocacion = 100;

        if (aliado)
        {
            IniciaAliado(valor);
            nombreIngles = nombre;
        }
        else
        {
            IniciaEnemigo(valor);
            rapido = -1;
        }

        indicePersonaje = valor;

        evasion = 1;
        vidaActual = vidaModificada = vida;
        ataqueFisicoActual = ataqueFisicoModificado = ataqueFisico;
        ataqueMagicoActual = ataqueMagicoModificado = ataqueMagico;
        defensaFisicaActual = defensaFisicaModificada = defensaFisica;
        defensaMagicaActual = defensaMagicaModificada = defensaMagica;
        velocidadActual = velocidadModificada = velocidad;
        evasionActual = evasion;
        precisionActual = precision;
        estado = estadoPersonaje.SANO;
        estadoIngles = estadoPersonajeIngles.HEALTHY;

        desbloqueado = false;
        setElemento();

        return this;
    }



    void IniciaAliado(int valor)
    {
        casco = armadura = botas = arma = escudo = complemento = null;
        descripcion = "";

        switch (valor)
        {
            case 0:
                nombre = "Victor";
                elemento = elementoPersonaje.NEUTRO;

                nivel = 5;
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/Prota");
                imagen = imagenes[1];

                rapido = 1;

                vida = 45;

                ataqueFisico = 12;
                defensaFisica = 16;
                ataqueMagico = 11;
                defensaMagica = 17;

                velocidad = 13;

                experiencia = 0;
                proximoNivel = 40;

                numeroAtaques = 2;
                indicesAtaque = new int[4];
                indicesAtaque[0] = 1;
                indicesAtaque[1] = 48;
                indicesAtaque[2] = -1;
                indicesAtaque[3] = -1;
                
                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                llevaCasco = llevaArmadura = llevaBotas = llevaArma = llevaEscudo = llevaComplemento = false;

                desbloqueado = true;

                break;
            case 1:
                nombre = "General";
                elemento = elementoPersonaje.RESPONSABLE;

                nivel = 1;
                
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/General");
                imagen = imagenes[1];

                vida = 35;

                ataqueFisico = 3;
                defensaFisica = 2;
                ataqueMagico = 4;
                defensaMagica = 3;

                velocidad = 6;

                experiencia = 0;
                proximoNivel = 10;

                
                numeroAtaques = 4;
                indicesAtaque = new int[4];
                indicesAtaque[0] = 5;
                indicesAtaque[1] = 7;
                indicesAtaque[2] = 9;
                indicesAtaque[3] = 49;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 2:
                nombre = "Isser";
                elemento = elementoPersonaje.FRIKI;

                nivel = 1;
                
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/Isser");
                imagen = imagenes[1];

                vida = 30;

                ataqueFisico = 2;
                defensaFisica = 1;
                ataqueMagico = 5;
                defensaMagica = 4;

                velocidad = 3;

                experiencia = 0;
                proximoNivel = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[4];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;
                indicesAtaque[3] = 9;

                habilidades = new Ataque[4];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 3:
                nombre = "Connor";
                elemento = elementoPersonaje.FIESTERO;

                nivel = 1;
                
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/Connor");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;

                experiencia = 0;
                proximoNivel = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[4];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;
                indicesAtaque[3] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 4:
                nombre = "Kazama";
                elemento = elementoPersonaje.TIRANO;

                nivel = 1;
                
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/Kazama");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;

                experiencia = 0;
                proximoNivel = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[4];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;
                indicesAtaque[3] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 5:
                nombre = "Betsabe";
                elemento = elementoPersonaje.RESPONSABLE;

                nivel = 1;
                
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/Betsabe");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;

                experiencia = 0;
                proximoNivel = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[4];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;
                indicesAtaque[3] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 6:
                nombre = "Carlos";
                elemento = elementoPersonaje.FIESTERO;

                nivel = 1;
               
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/Carlos");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;

                experiencia = 0;
                proximoNivel = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[4];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;
                indicesAtaque[3] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 7:
                nombre = "Elvira";
                elemento = elementoPersonaje.DORMILON;

                nivel = 1;
               
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/Elvira");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;
              

                experiencia = 0;
                proximoNivel = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[4];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;
                indicesAtaque[3] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 8:
                nombre = "Nani";
                elemento = elementoPersonaje.FRIKI;

                nivel = 5;
                
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/Nani");
                imagen = imagenes[1];

                vida = 30;

                ataqueFisico = 7;
                defensaFisica = 4;
                ataqueMagico = 3;
                defensaMagica = 5;

                velocidad = 20;
              

                experiencia = 0;
                proximoNivel = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[4];
                indicesAtaque[0] = 2;
                indicesAtaque[1] = 53;
                indicesAtaque[2] = 9;
                indicesAtaque[3] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 9:
                nombre = "Jack";
                elemento = elementoPersonaje.FRIKI;

                nivel = 1;
                
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/Jack El Misterioso");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;
              
                experiencia = 0;
                proximoNivel = 10;

                numeroAtaques = 3;
                indicesAtaque = new int[4];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;
                indicesAtaque[3] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 10:
                nombre = "Helena";
                elemento = elementoPersonaje.FIESTERO;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/Helena");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;
              

                experiencia = 0;
                proximoNivel = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[4];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;
                indicesAtaque[3] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 11:
                nombre = "Dan";
                elemento = elementoPersonaje.RESPONSABLE;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/Dan");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;
              
                experiencia = 0;
                proximoNivel = 10;

                numeroAtaques = 3;
                indicesAtaque = new int[4];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;
                indicesAtaque[3] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 12:
                nombre = "Gamez";
                elemento = elementoPersonaje.RESPONSABLE;

                nivel = 5;
                
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/Gamez");
                imagen = imagenes[1];

                vida = 35;

                ataqueFisico = 15;
                defensaFisica = 15;
                ataqueMagico = 5;
                defensaMagica = 7;

                velocidad = 8;

                experiencia = 0;
                proximoNivel = 50;


                numeroAtaques = 2;
                indicesAtaque = new int[4];
                indicesAtaque[0] = 10;
                indicesAtaque[1] = 52;
                indicesAtaque[2] = 49;
                indicesAtaque[3] = 49;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 13:
                nombre = "Luis";
                elemento = elementoPersonaje.FIESTERO;

                nivel = 1;
                
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/Luis");
                imagen = imagenes[1];

                vida = 30;

                ataqueFisico = 7;
                defensaFisica = 6;
                ataqueMagico = 3;
                defensaMagica = 4;

                velocidad = 8;

                experiencia = 60;
                proximoNivel = 10;

                dinero = 0;

                numeroAtaques = 4;
                indicesAtaque = new int[4];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 6;
                indicesAtaque[2] = 69;
                indicesAtaque[3] = 67;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 14:
                nombre = "Daniel";
                elemento = elementoPersonaje.TIRANO;

                nivel = 1;
                
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/Daniel");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;
              

                experiencia = 0;
                proximoNivel = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[4];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;
                indicesAtaque[3] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 15:
                nombre = "Angel";
                elemento = elementoPersonaje.FRIKI;

                nivel = 1;
                
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/Angel");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;

                experiencia = 0;
                proximoNivel = 10;
                
                numeroAtaques = 3;
                indicesAtaque = new int[4];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;
                indicesAtaque[3] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 16:
                nombre = "Lucas";
                elemento = elementoPersonaje.RESPONSABLE;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/UltimoCurso/Lucas");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;

                experiencia = 0;
                proximoNivel = 10;

                numeroAtaques = 3;
                indicesAtaque = new int[4];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;
                indicesAtaque[3] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;


                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 17:
                nombre = "Bryan";
                elemento = elementoPersonaje.FIESTERO;

                nivel = 1;
               
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/UltimoCurso/Bryan");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;
              

                experiencia = 0;
                proximoNivel = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[4];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;
                indicesAtaque[3] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 18:
                nombre = "Alex";
                elemento = elementoPersonaje.FRIKI;

                nivel = 1;
                
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/Alex");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;
              

                experiencia = 0;
                proximoNivel = 10;

                numeroAtaques = 3;
                indicesAtaque = new int[4];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;
                indicesAtaque[3] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }
                break;
            case 19:
                nombre = "Pedro";
                elemento = elementoPersonaje.FIESTERO;

                nivel = 5;
                
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/Pedro");
                imagen = imagenes[1];

                vida = 33;

                ataqueFisico = 5;
                defensaFisica = 8;
                ataqueMagico = 12;
                defensaMagica = 11;

                velocidad = 8;

                experiencia = 0;
                proximoNivel = 200;

                numeroAtaques = 2;
                indicesAtaque = new int[4];
                indicesAtaque[0] = 7;
                indicesAtaque[1] = 55;
                indicesAtaque[2] = 49;
                indicesAtaque[3] = 49;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 20:
                nombre = "Sara";
                elemento = elementoPersonaje.FIESTERO;

                nivel = 5;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Aliados/Sara");
                imagen = imagenes[1];

                vida = 35;

                ataqueFisico = 10;
                defensaFisica = 12;
                ataqueMagico = 10;
                defensaMagica = 12;

                velocidad = 14;

                experiencia = 0;
                proximoNivel = 50;

                numeroAtaques = 2;
                indicesAtaque = new int[4];
                indicesAtaque[0] = 6;
                indicesAtaque[1] = 49;
                indicesAtaque[2] = 49;
                indicesAtaque[3] = 49;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
        }
    }

    

    void IniciaEnemigo(int valor)
    {
        switch (valor)
        {
            case 0:
                nombre = "Alien Gris";
                nombreIngles = "Gray Alien";

                elemento = elementoPersonaje.FIESTERO;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Alien/Gris");
                imagen = imagenes[1];

                vida = 15;

                ataqueFisico = 10;
                defensaFisica = 25;
                ataqueMagico = 12;
                defensaMagica = 20;

                velocidad = 4;

                experiencia = 60;
                proximoNivel = 10;

                dinero = 10;
                
                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;
                indicesAtaque[2] = 15;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 0;

                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    indicesAtaque[i] = 3 + i;
                }

                habilidades = new Ataque[numeroAtaques];

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 1:
                nombre = "Alien Verde";
                elemento = elementoPersonaje.FIESTERO;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Alien/Verde");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;
              
                experiencia = 0;
                proximoNivel = 10;

                dinero = 10;
                
                numeroAtaques = 3;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 0;

                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    indicesAtaque[i] = 3 + i;
                }

                habilidades = new Ataque[numeroAtaques];

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 2:
                nombre = "Bruja Blanca";
                elemento = elementoPersonaje.RESPONSABLE;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Bruja/Blanca/witch1_1");
                imagen = imagenes[1];

                vida = 28;

                ataqueFisico = 3;
                defensaFisica = 4;
                ataqueMagico = 9;
                defensaMagica = 6;

                velocidad = 3;

                experiencia = 55;
                proximoNivel = 10;

                dinero = 26;

                
                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 11;
                indicesAtaque[1] = 23;
                indicesAtaque[2] = 64;
                indicesAtaque[3] = 65;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 1;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 3:
                nombre = "Bruja Verde";
                elemento = elementoPersonaje.RESPONSABLE;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Bruja/Verde/witch2_1");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;
              

                experiencia = 0;
                proximoNivel = 10;

                dinero = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 0;

                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    indicesAtaque[i] = 3 + i;
                }

                habilidades = new Ataque[numeroAtaques];

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 4:
                nombre = "Quemado";
                nombreIngles = "Burned";

                elemento = elementoPersonaje.RESPONSABLE;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Cabeza Llamas");
                imagen = imagenes[1];

                vida = 22;

                ataqueFisico = 5;
                defensaFisica = 4;
                ataqueMagico = 6;
                defensaMagica = 7;

                velocidad = 5;

                experiencia = 52;
                proximoNivel = 10;

                dinero = 75;
                
                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 7;
                indicesAtaque[1] = 11;
                indicesAtaque[2] = 23;
                indicesAtaque[3] = 55;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 1;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 5:
                nombre = "Cactus";
                elemento = elementoPersonaje.DORMILON;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Cactus");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;
              

                experiencia = 0;
                proximoNivel = 10;

                dinero = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 0;

                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    indicesAtaque[i] = 3 + i;
                }

                habilidades = new Ataque[numeroAtaques];

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 6:
                nombre = "Cthulhu-A";
                elemento = elementoPersonaje.DORMILON;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Cthulhu/cuchulu 1");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;
              

                experiencia = 0;
                proximoNivel = 10;

                dinero = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 0;

                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    indicesAtaque[i] = 3 + i;
                }

                habilidades = new Ataque[numeroAtaques];

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 7:
                nombre = "Cthulhu-R";
                elemento = elementoPersonaje.FIESTERO;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Cthulhu/cuchulu 2");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;
              

                experiencia = 0;
                proximoNivel = 10;

                dinero = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 0;

                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    indicesAtaque[i] = 3 + i;
                }

                habilidades = new Ataque[numeroAtaques];

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 8:
                nombre = "Cthulhu-V";
                elemento = elementoPersonaje.TIRANO;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Cthulhu/cuchulu 3");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;
              

                experiencia = 0;
                proximoNivel = 10;

                dinero = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 0;

                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    indicesAtaque[i] = 3 + i;
                }

                habilidades = new Ataque[numeroAtaques];

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 9:
                nombre = "Cthulhu-N";
                elemento = elementoPersonaje.FRIKI;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Cthulhu/cuchulu 4");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;
              

                experiencia = 0;
                proximoNivel = 10;

                dinero = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 0;

                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    indicesAtaque[i] = 3 + i;
                }

                habilidades = new Ataque[numeroAtaques];

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 10:
                nombre = "Demonio";
                elemento = elementoPersonaje.TIRANO;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Demonio Rojo/hell-beast-idle-right");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;
              

                experiencia = 0;
                proximoNivel = 10;

                dinero = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 0;

                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    indicesAtaque[i] = 3 + i;
                }

                habilidades = new Ataque[numeroAtaques];

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 11:
                nombre = "Encapuchado-M";
                elemento = elementoPersonaje.NEUTRO;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Encapuchados/Enemigo3");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;
              

                experiencia = 0;
                proximoNivel = 10;

                dinero = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 0;

                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    indicesAtaque[i] = 3 + i;
                }

                habilidades = new Ataque[numeroAtaques];

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 12:
                nombre = "Encapuchado-R";
                elemento = elementoPersonaje.DORMILON;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Encapuchados/Enemigo3");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;
              

                experiencia = 0;
                proximoNivel = 10;

                dinero = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 0;

                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    indicesAtaque[i] = 3 + i;
                }

                habilidades = new Ataque[numeroAtaques];

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 13:
                nombre = "Encapuchado-Rs";
                elemento = elementoPersonaje.FRIKI;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Encapuchados/Enemigo3");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;
              

                experiencia = 0;
                proximoNivel = 10;

                dinero = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 0;

                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    indicesAtaque[i] = 3 + i;
                }

                habilidades = new Ataque[numeroAtaques];

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 14:
                nombre = "Encapuchado-A";
                elemento = elementoPersonaje.RESPONSABLE;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Encapuchados/Enemigo3");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;
              

                experiencia = 0;
                proximoNivel = 10;

                dinero = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 0;

                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    indicesAtaque[i] = 3 + i;
                }

                habilidades = new Ataque[numeroAtaques];

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 15:
                nombre = "Encapuchado-V";
                elemento = elementoPersonaje.TIRANO;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Encapuchados/Enemigo3");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;
              

                experiencia = 0;
                proximoNivel = 10;

                dinero = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 0;

                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    indicesAtaque[i] = 3 + i;
                }

                habilidades = new Ataque[numeroAtaques];

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 16:
                nombre = "Secuaz";
                nombreIngles = "Henchman";

                elemento = elementoPersonaje.DORMILON;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Esqueletos/Pirata");
                imagen = imagenes[1];

                vida = 20;

                ataqueFisico = 7;
                defensaFisica = 5;
                ataqueMagico = 4;
                defensaMagica = 6;

                velocidad = 8;

                experiencia = 50;
                proximoNivel = 10;

                dinero = 70;

                
                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 5;
                indicesAtaque[1] = 2;
                indicesAtaque[2] = 1;
                indicesAtaque[3] = 54;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 1;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 17:
                nombre = "Secuaz Pirata";
                elemento = elementoPersonaje.FIESTERO;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Esqueletos/Pirata");
                imagen = imagenes[4];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;
                

                experiencia = 0;
                proximoNivel = 10;

                dinero = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 0;

                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    indicesAtaque[i] = 3 + i;
                }

                habilidades = new Ataque[numeroAtaques];

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 18:
                nombre = "Esqueleto Raso";
                nombreIngles = "Skeleton Soldier";

                elemento = elementoPersonaje.FRIKI;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Esqueletos/Esqueleto1");
                imagen = imagenes[1];

                vida = 15;

                ataqueFisico = 7;
                defensaFisica = 6;
                ataqueMagico = 4;
                defensaMagica = 4;

                velocidad = 8;

                experiencia = 55;
                proximoNivel = 10;

                dinero = 90;

                
                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 2;
                indicesAtaque[1] = 20;
                indicesAtaque[2] = 14;
                indicesAtaque[3] = 56;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 2;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 19:
                nombre = "Esqueleto Sargento";
                nombreIngles = "Sergeant Skeleton";

                elemento = elementoPersonaje.FRIKI;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Esqueletos/Esqueleto2");
                imagen = imagenes[1];

                vida = 35;

                ataqueFisico = 18;
                defensaFisica = 20;
                ataqueMagico = 48;
                defensaMagica = 49;

                velocidad = 10;

                experiencia = 60;
                proximoNivel = 10;

                dinero = 90;

                
                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 2;
                indicesAtaque[1] = 20;
                indicesAtaque[2] = 14;
                indicesAtaque[3] = 56;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 0;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 20:
                nombre = "Esqueleto Explorador";
                nombreIngles = "Explorer Skeleton";

                elemento = elementoPersonaje.FRIKI;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Esqueletos/Esqueleto3");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;
                

                experiencia = 0;
                proximoNivel = 10;

                dinero = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 0;

                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    indicesAtaque[i] = 3 + i;
                }

                habilidades = new Ataque[numeroAtaques];

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 21:
                nombre = "Esqueleto Capitán";
                elemento = elementoPersonaje.FRIKI;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Esqueletos/Esqueleto4");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;
                

                experiencia = 0;
                proximoNivel = 10;

                dinero = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 0;

                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    indicesAtaque[i] = 3 + i;
                }

                habilidades = new Ataque[numeroAtaques];

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 22:
                nombre = "Esqueleto Gran Explorador";
                elemento = elementoPersonaje.FRIKI;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Esqueletos/Esqueleto5");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;
                

                experiencia = 0;
                proximoNivel = 10;

                dinero = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 0;

                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    indicesAtaque[i] = 3 + i;
                }

                habilidades = new Ataque[numeroAtaques];

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 23:
                nombre = "Esqueleto General";
                elemento = elementoPersonaje.RESPONSABLE;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Esqueletos/Esqueleto6");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;
                

                experiencia = 0;
                proximoNivel = 10;

                dinero = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 0;

                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    indicesAtaque[i] = 3 + i;
                }

                habilidades = new Ataque[numeroAtaques];

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 24:
                nombre = "Esqueleto Cazador";
                elemento = elementoPersonaje.RESPONSABLE;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Esqueletos/Esqueleto7");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;
                

                experiencia = 0;
                proximoNivel = 10;

                dinero = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 0;

                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    indicesAtaque[i] = 3 + i;
                }

                habilidades = new Ataque[numeroAtaques];

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 25:
                nombre = "Esqueleto Bruja";
                elemento = elementoPersonaje.TIRANO;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Esqueletos/Esqueleto8");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;
                

                experiencia = 0;
                proximoNivel = 10;

                dinero = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 0;

                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    indicesAtaque[i] = 3 + i;
                }

                habilidades = new Ataque[numeroAtaques];

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 26:
                nombre = "Fantasma Blanco";
                nombreIngles = "White Boo";

                elemento = elementoPersonaje.TIRANO;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Fantasma Blanco");
                imagen = imagenes[1];

                vida = 15;

                ataqueFisico = 7;
                defensaFisica = 6;
                ataqueMagico = 4;
                defensaMagica = 4;

                velocidad = 16;

                experiencia = 55;
                proximoNivel = 10;

                dinero = 80;
                
                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 12;
                indicesAtaque[1] = 0;
                indicesAtaque[2] = 1;
                indicesAtaque[3] = 54;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 2;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 27:
                nombre = "Guardia";
                nombreIngles = "Guard";

                elemento = elementoPersonaje.RESPONSABLE;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/GuardiaCupula");
                imagen = imagenes[1];

                vida = 25;

                ataqueFisico = 7;
                defensaFisica = 8;
                ataqueMagico = 4;
                defensaMagica = 8;

                velocidad = 10;

                experiencia = 60;
                proximoNivel = 10;

                dinero = 40;

                
                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 22;
                indicesAtaque[1] = 18;
                indicesAtaque[2] = 49;
                indicesAtaque[3] = 52;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = -1;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 28:
                nombre = "Lagarto Verde";
                nombreIngles = "Green Lizard";

                elemento = elementoPersonaje.FIESTERO;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Hombres Lagarto/monster_lizardman1");
                imagen = imagenes[1];

                vida = 40;

                ataqueFisico = 15;
                defensaFisica = 15;
                ataqueMagico = 10;
                defensaMagica = 20;

                velocidad = 19;
                

                experiencia = 50;
                proximoNivel = 10;

                dinero = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 0;

                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    indicesAtaque[i] = 3 + i;
                }

                habilidades = new Ataque[numeroAtaques];

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 29:
                nombre = "Lagarto Rojo";
                elemento = elementoPersonaje.FIESTERO;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Hombres Lagarto/monster_lizardman2");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;
                

                experiencia = 0;
                proximoNivel = 10;

                dinero = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 0;

                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    indicesAtaque[i] = 3 + i;
                }

                habilidades = new Ataque[numeroAtaques];

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 30:
                nombre = "Dormilón";
                nombreIngles = "Sleepyhead";

                elemento = elementoPersonaje.DORMILON;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Log/log");
                imagen = imagenes[0];

                vida = 10;

                ataqueFisico = 2;
                defensaFisica = 3;
                ataqueMagico = 3;
                defensaMagica = 4;

                velocidad = 2;

                experiencia = 33;
                proximoNivel = 10;

                dinero = 50;

                
                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 1;
                indicesAtaque[1] = 4;
                indicesAtaque[2] = 9;
                indicesAtaque[3] = 55;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = -1;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 31:
                nombre = "Mago Negro";
                elemento = elementoPersonaje.TIRANO;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Mago Negro");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;
                

                experiencia = 0;
                proximoNivel = 10;

                dinero = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 0;

                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    indicesAtaque[i] = 3 + i;
                }

                habilidades = new Ataque[numeroAtaques];

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 32:
                nombre = "Mago Rojo";
                elemento = elementoPersonaje.TIRANO;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Elric");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;
                

                experiencia = 0;
                proximoNivel = 10;

                dinero = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 0;

                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    indicesAtaque[i] = 3 + i;
                }

                habilidades = new Ataque[numeroAtaques];

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 33:
                nombre = "Muerte";
                elemento = elementoPersonaje.TIRANO;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Muerte/reaper_blade_1");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;
                

                experiencia = 0;
                proximoNivel = 10;

                dinero = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 0;

                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    indicesAtaque[i] = 3 + i;
                }

                habilidades = new Ataque[numeroAtaques];

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 34:
                nombre = "Orco";
                nombreIngles = "Orc";

                elemento = elementoPersonaje.FIESTERO;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Orcos/orco 1");
                imagen = imagenes[1];

                vida = 10;

                ataqueFisico = 7;
                defensaFisica = 6;
                ataqueMagico = 4;
                defensaMagica = 5;

                velocidad = 6;

                experiencia = 50;
                proximoNivel = 10;

                dinero = 60;
                
                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 12;
                indicesAtaque[1] = 0;
                indicesAtaque[2] = 1;
                indicesAtaque[3] = 54;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 1;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 35:
                nombre = "Orco Armado";
                nombreIngles = "Armed Orc";

                elemento = elementoPersonaje.FIESTERO;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Orcos/orco 2");
                imagen = imagenes[1];

                vida = 30;

                ataqueFisico = 7;
                defensaFisica = 6;
                ataqueMagico = 4;
                defensaMagica = 4;

                velocidad = 8;

                experiencia = 55;
                proximoNivel = 10;

                dinero = 100;

                
                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 12;
                indicesAtaque[1] = 0;
                indicesAtaque[2] = 1;
                indicesAtaque[3] = 54;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 2;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 36:
                nombre = "Orco Cazador";
                nombreIngles = "Hunter Orc";
                elemento = elementoPersonaje.FIESTERO;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Orcos/orco 3");
                imagen = imagenes[1];

                vida = 50;

                ataqueFisico = 15;
                defensaFisica = 20;
                ataqueMagico = 8;
                defensaMagica = 12;

                velocidad = 10;
                

                experiencia = 0;
                proximoNivel = 10;

                dinero = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 2;

                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    indicesAtaque[i] = 3 + i;
                }

                habilidades = new Ataque[numeroAtaques];

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 37:
                nombre = "Orco Exploradora";
                nombreIngles = "Explorer Orc";

                elemento = elementoPersonaje.FIESTERO;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Orcos/orco 4");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;
                
                experiencia = 0;
                proximoNivel = 10;

                dinero = 10;

                numeroAtaques = 3;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 0;

                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    indicesAtaque[i] = 3 + i;
                }

                habilidades = new Ataque[numeroAtaques];

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 38:
                nombre = "Orco Capitana";
                nombreIngles = "Captain Orc";

                elemento = elementoPersonaje.FIESTERO;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Orcos/orco 5");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;
                

                experiencia = 0;
                proximoNivel = 10;

                dinero = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 0;

                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    indicesAtaque[i] = 3 + i;
                }

                habilidades = new Ataque[numeroAtaques];

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 39:
                nombre = "Orco Comandante";
                elemento = elementoPersonaje.FIESTERO;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Orcos/orco 6");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;
                

                experiencia = 0;
                proximoNivel = 10;

                dinero = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 0;

                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    indicesAtaque[i] = 3 + i;
                }

                habilidades = new Ataque[numeroAtaques];

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 40:
                nombre = "Orco Gran Cazador";
                elemento = elementoPersonaje.FIESTERO;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Orcos/orco 9");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;
                

                experiencia = 0;
                proximoNivel = 10;

                dinero = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 0;

                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    indicesAtaque[i] = 3 + i;
                }

                habilidades = new Ataque[numeroAtaques];

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 41:
                nombre = "Robot Guarda";
                elemento = elementoPersonaje.FRIKI;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Robots/Robot1");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;
                

                experiencia = 0;
                proximoNivel = 10;

                dinero = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 0;

                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    indicesAtaque[i] = 3 + i;
                }

                habilidades = new Ataque[numeroAtaques];

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 42:
                nombre = "Robot Ferreo";
                elemento = elementoPersonaje.FRIKI;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Robots/Robot2");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;
                

                experiencia = 0;
                proximoNivel = 10;

                dinero = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 0;

                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    indicesAtaque[i] = 3 + i;
                }

                habilidades = new Ataque[numeroAtaques];

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 43:
                nombre = "Robot Vigía";
                elemento = elementoPersonaje.FRIKI;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Robots/Robot3");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;
                

                experiencia = 0;
                proximoNivel = 10;

                dinero = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 0;

                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    indicesAtaque[i] = 3 + i;
                }

                habilidades = new Ataque[numeroAtaques];

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 44:
                nombre = "Robot Vigía Ferreo";
                elemento = elementoPersonaje.FRIKI;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Robots/Robot3");
                imagen = imagenes[4];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;
                

                experiencia = 0;
                proximoNivel = 10;

                dinero = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 0;

                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    indicesAtaque[i] = 3 + i;
                }

                habilidades = new Ataque[numeroAtaques];

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 45:
                nombre = "Slime Azul";
                nombreIngles = "Blue Slime";

                elemento = elementoPersonaje.DORMILON;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Slime/Slime");
                imagen = imagenes[1];

                vida = 10;

                ataqueFisico = 2;
                defensaFisica = 3;
                ataqueMagico = 3;
                defensaMagica = 3;

                velocidad = 2;

                experiencia = 40;
                proximoNivel = 10;

                dinero = 45;

                
                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 2;
                indicesAtaque[2] = 4;
                indicesAtaque[3] = 49;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = -1;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 46:
                nombre = "Slime Rosa";
                nombreIngles = "Pink Slime";

                elemento = elementoPersonaje.DORMILON;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Slime/slime_monster_spritesheet");
                imagen = imagenes[7];

                vida = 10;

                ataqueFisico = 3;
                defensaFisica = 4;
                ataqueMagico = 3;
                defensaMagica = 4;

                velocidad = 3;

                experiencia = 45;
                proximoNivel = 10;

                dinero = 50;

                
                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 1;
                indicesAtaque[1] = 4;
                indicesAtaque[2] = 9;
                indicesAtaque[3] = 55;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = -1;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }
                break;
            case 47:
                nombre = "Tostada Zombie";
                elemento = elementoPersonaje.FIESTERO;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Tostada Zombie");
                imagen = imagenes[8];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;
                

                experiencia = 0;
                proximoNivel = 10;

                dinero = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 0;

                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    indicesAtaque[i] = 3 + i;
                }

                habilidades = new Ataque[numeroAtaques];

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 48:
                nombre = "Vampiro H";
                elemento = elementoPersonaje.TIRANO;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Vampiros/Vampiro1");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;
                

                experiencia = 0;
                proximoNivel = 10;

                dinero = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 0;

                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    indicesAtaque[i] = 3 + i;
                }

                habilidades = new Ataque[numeroAtaques];

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 49:
                nombre = "Vampiro M";
                elemento = elementoPersonaje.TIRANO;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Vampiros/Vampiro2");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;
                

                experiencia = 0;
                proximoNivel = 10;

                dinero = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 0;

                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    indicesAtaque[i] = 3 + i;
                }

                habilidades = new Ataque[numeroAtaques];

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 50:
                nombre = "Verdugo";
                elemento = elementoPersonaje.TIRANO;

                nivel = 1;
                descripcion = "";
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/Enemigos/Pequeños/Verdugo/executioner_axe_1");
                imagen = imagenes[1];

                vida = 300;

                ataqueFisico = 10;
                defensaFisica = 10;
                ataqueMagico = 10;
                defensaMagica = 10;

                velocidad = 10;

                experiencia = 0;
                proximoNivel = 10;

                dinero = 10;

                
                numeroAtaques = 3;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 0;
                indicesAtaque[1] = 1;
                indicesAtaque[2] = 9;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 0;

                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                for (int i = 0; i < numeroAtaques; i++)
                {
                    indicesAtaque[i] = 3 + i;
                }

                habilidades = new Ataque[numeroAtaques];

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
            case 51:
                nombre = "Pícaro";
                nombreIngles = "Rogue";

                elemento = elementoPersonaje.TIRANO;

                nivel = 1;
                imagenes = Resources.LoadAll<Sprite>("Sprites/Personajes/NPC/Picaro");
                imagen = imagenes[1];

                vida = 30;

                ataqueFisico = 4;
                defensaFisica = 6;
                ataqueMagico = 5;
                defensaMagica = 7;

                velocidad = 10;

                experiencia = 80;

                dinero = 90;


                numeroAtaques = 4;
                indicesAtaque = new int[numeroAtaques];
                indicesAtaque[0] = 2;
                indicesAtaque[1] = 5;
                indicesAtaque[2] = 9;
                indicesAtaque[3] = 48;

                habilidades = new Ataque[numeroAtaques];
                tipo = tipoPersonaje.PEQUENIO;

                objetoRecompensa = 0;

                habilidades = new Ataque[numeroAtaques];

                for (int i = 0; i < numeroAtaques; i++)
                {
                    ataque = new Ataque();
                    habilidades[i] = ataque.BuscaAtaque(indicesAtaque[i]);
                }

                break;
        }

        evasion = 1;

    }//los enemigos siempre tendrán 4 ataques



    public void setElemento()
    {
        Sprite[] aux = Resources.LoadAll<Sprite>("Sprites/Interfaz/Combate/ElementosInterfazCombate");

        if(elemento == elementoPersonaje.NEUTRO)
        {
            elementoIngles = elementoPersonajeIngles.NEUTRAL;
            imagenElemento = aux[3];
        }
        else if (elemento == elementoPersonaje.DORMILON)
        {
            elementoIngles = elementoPersonajeIngles.SLEEPER;
            imagenElemento = aux[2];
        }
        else if (elemento == elementoPersonaje.FIESTERO)
        {
            elementoIngles = elementoPersonajeIngles.PARTYMAN;
            imagenElemento = aux[10];
        }
        else if (elemento == elementoPersonaje.FRIKI)
        {
            elementoIngles = elementoPersonajeIngles.GEEK;
            imagenElemento = aux[12];
        }
        else if (elemento == elementoPersonaje.RESPONSABLE)
        {
            elementoIngles = elementoPersonajeIngles.RESPONSIBLE;
            imagenElemento = aux[13];
        }
        else if (elemento == elementoPersonaje.TIRANO)
        {
            elementoIngles = elementoPersonajeIngles.TYRANT;
            imagenElemento = aux[7];
        }
    }
}
