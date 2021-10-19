using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BDData
{
    public string nombreProta;
    //Lista Personajes Aliados
    public int[] ataqueFisico, ataqueFisicoActual, ataqueFisicoModificado;
    public int[] ataqueMagico, ataqueMagicoActual, ataqueMagicoModificado;
    public int[] defensaFisica, defensaFisicaActual, defensaFisicaModificada;
    public int[] defensaMagica, defensaMagicaActual, defensaMagicaModificada;
    public int[] velocidad, velocidadActual, velocidadModificada;
    public int[] vida, vidaActual, vidaModificada;
    public int[] energiaR1, energiaR2, energiaR3; //energia restante de cada miembro del equipo
    public float[] evasion, evasionActual;
    public float[] precision, precisionActual;

    public int[] nivel;
    public int[] experiencia, proximoNivel; // experiencia acumulada y experiencia necesaria para llegar al proximo nivel

    public int[] numeroAtaques;
    public int[][] indicesAtaque;

    public bool[] llevaCasco;
    public bool[] llevaArmadura;
    public bool[] llevaBotas;
    public bool[] llevaArma;
    public bool[] llevaEscudo;
    public bool[] llevaComplemento;

    public int[] indiceCasco;
    public int[] indiceArmadura;
    public int[] indiceBotas;
    public int[] indiceArma;
    public int[] indiceEscudo;
    public int[] indiceComplemento;


    //PersonajesAlamacenados
    public int[] personajesAlmacenados;
    public int numeroAlmacenado;


    //Lista Equipo Aliado
    public int numeroIntegrantes;
    public int[] indices;


    //Consumibles
    public int[] indicesConsumibles;
    public int[] cantidadesConsumibles;
    public int cantidadConsumiblesTotal;

    //O. ataque
    public int[] indicesOAtaque;
    public int[] cantidadesOAtaque;
    public int cantidadOAtaqueTotal;

    //O. equipo
    public int[] indicesOEquipo;
    public int[] cantidadesOEquipo;
    public int cantidadOEquipoTotal;

    //O.clave
    public int[] indicesOClave;
    public int[] cantidadesOClave;
    public int cantidadOClaveTotal;

    //Cascos
    public int[] indicesOCascos;
    public int[] cantidadesOCascos;
    public int cantidadOCascosTotal;

    //Armaduras
    public int[] indicesOArmaduras;
    public int[] cantidadesOArmaduras;
    public int cantidadOArmadurasTotal;

    //Botas
    public int[] indicesOBotas;
    public int[] cantidadesOBotas;
    public int cantidadOBotasTotal;

    //Arma
    public int[] indicesOArmas;
    public int[] cantidadesOArmas;
    public int cantidadOArmasTotal;

    //Escudos
    public int[] indicesOEscudos;
    public int[] cantidadesOEscudos;
    public int cantidadOEscudosTotal;

    //Complementos
    public int[] indicesOComplementos;
    public int[] cantidadesOComplementos;
    public int cantidadOComplementosTotal;

    //Ataques Adquiridos
    public bool[] ataquesDesbloqueados;

    //Misiones
    public int numeroMisionesActivas;
    public int[] indicesMisionesActivas;
    public int numeroMisionesPrincipales;
    public bool[] misionCompletada;
    public int[] indicesMisionesPrincipales;
    public int numeroMisionesSecundarias;
    public int[] indicesMisionesSecundarias;
    public int numeroMisionesReclutamiento;
    public int[] indicesMisionesReclutamiento;

    public bool[] cofres;
    public int numeroCofres;

    //Mapa
    public int indiceObjetivo;
    public int indiceInicial;
    public bool[] zonaVisitada;

    //Pociones
    public bool[] recetasPocionesDesbloqueadas = new bool[10];
    public bool puedeHacerPociones;

    //Jugador
    public int faccion;

    //Tiempo Juego;
    public float segundos;
    public float minutos;
    public float horas;

    //Camara
    public int zonaCamara;
    public int areaCamara;

    //Esgrima
    public bool[] retosEsgrimaDesbloqueados = new bool[10];

    //Teleport
    public bool teleportActivo;

    //Personajes Desactivados
    public bool[] personajesDesactivados;



    public BDData(BaseDatos baseDeDatos)
    {
        //Pociones
        puedeHacerPociones = baseDeDatos.puedeHacerPociones;
        for(int i = 0; i < 10; i++)
        {
            recetasPocionesDesbloqueadas[i] = baseDeDatos.recetasPocionesDesbloqueadas[i];
        }

        //Esgrima
        for (int i = 0; i < 10; i++)
        {
            retosEsgrimaDesbloqueados[i] = baseDeDatos.retosEsgrimaDesbloqueados[i];
        }


        nombreProta = baseDeDatos.nombreProta;
        numeroCofres = baseDeDatos.cofres.Length;
        cofres = new bool[numeroCofres];

        for(int i = 0;  i < numeroCofres; i++)
        {
            cofres[i] = baseDeDatos.cofres[i];
        }

        //Mapa
        indiceObjetivo = baseDeDatos.indiceObjetivo;
        indiceInicial = baseDeDatos.indiceInicial;

        zonaVisitada = new bool [baseDeDatos.zonaVisitada.Length];
        for(int i = 0; i < zonaVisitada.Length; i++)
        {
            zonaVisitada[i] = baseDeDatos.zonaVisitada[i];
        }

        //Jugador
        faccion = baseDeDatos.faccion;

        //Tiempo Juego;
        segundos = baseDeDatos.segundos;
        minutos = baseDeDatos.minutos;
        horas = baseDeDatos.horas;

        //Lista Equipo Aliado
        numeroIntegrantes = baseDeDatos.numeroIntegrantesEquipo;
        indices = new int[numeroIntegrantes];
        energiaR1 = new int[baseDeDatos.equipoAliado[0].numeroAtaques];

        for(int i = 0; i < baseDeDatos.equipoAliado[0].numeroAtaques; i++)
        {
            energiaR1[i] = baseDeDatos.equipoAliado[0].habilidades[i].energiaActual;
        }

        if(baseDeDatos.numeroIntegrantesEquipo > 1)
        {
            energiaR2 = new int[baseDeDatos.equipoAliado[1].numeroAtaques];

            for (int i = 0; i < baseDeDatos.equipoAliado[1].numeroAtaques; i++)
            {
                energiaR2[i] = baseDeDatos.equipoAliado[1].habilidades[i].energiaActual;
            }
        }

        if(baseDeDatos.numeroIntegrantesEquipo > 2)
        {
            energiaR3 = new int[baseDeDatos.equipoAliado[2].numeroAtaques];

            for (int i = 0; i < baseDeDatos.equipoAliado[2].numeroAtaques; i++)
            {
                energiaR3[i] = baseDeDatos.equipoAliado[2].habilidades[i].energiaActual;
            }
        }

        numeroAlmacenado = baseDeDatos.numeroAlmacenado;
        personajesAlmacenados = new int[numeroAlmacenado];

        if(numeroAlmacenado != 0)
        {
            for (int i = 0; i < numeroAlmacenado; i++)
            {
                personajesAlmacenados[i] = baseDeDatos.personajesAlmacenados[i];
            }
        }
        

        for (int i = 0; i < numeroIntegrantes; i++)
        {
            indices[i] = baseDeDatos.equipoAliado[i].indicePersonaje;
            baseDeDatos.listaPersonajesAliados[indices[i]] = baseDeDatos.equipoAliado[i];
        }

        //Teleport
        teleportActivo = baseDeDatos.teleportActivo;

        //Lista Personajes Aliados
        ataqueFisico = new int[baseDeDatos.listaPersonajesAliados.Length];
        ataqueFisicoActual = new int[baseDeDatos.listaPersonajesAliados.Length];
        ataqueFisicoModificado = new int[baseDeDatos.listaPersonajesAliados.Length];
        ataqueMagico = new int[baseDeDatos.listaPersonajesAliados.Length];
        ataqueMagicoActual = new int[baseDeDatos.listaPersonajesAliados.Length];
        ataqueMagicoModificado = new int[baseDeDatos.listaPersonajesAliados.Length];
        defensaFisica = new int[baseDeDatos.listaPersonajesAliados.Length];
        defensaFisicaActual = new int[baseDeDatos.listaPersonajesAliados.Length];
        defensaFisicaModificada = new int[baseDeDatos.listaPersonajesAliados.Length];
        defensaMagica = new int[baseDeDatos.listaPersonajesAliados.Length];
        defensaMagicaActual = new int[baseDeDatos.listaPersonajesAliados.Length];
        defensaMagicaModificada = new int[baseDeDatos.listaPersonajesAliados.Length];
        velocidad = new int[baseDeDatos.listaPersonajesAliados.Length];
        velocidadActual = new int[baseDeDatos.listaPersonajesAliados.Length];
        velocidadModificada = new int[baseDeDatos.listaPersonajesAliados.Length];
        vida = new int[baseDeDatos.listaPersonajesAliados.Length];
        vidaActual = new int[baseDeDatos.listaPersonajesAliados.Length];
        vidaModificada = new int[baseDeDatos.listaPersonajesAliados.Length];
        evasion = new float[baseDeDatos.listaPersonajesAliados.Length];
        evasionActual = new float[baseDeDatos.listaPersonajesAliados.Length];
        precision = new float[baseDeDatos.listaPersonajesAliados.Length];
        precisionActual = new float[baseDeDatos.listaPersonajesAliados.Length];

        nivel = new int[baseDeDatos.listaPersonajesAliados.Length];
        experiencia = new int[baseDeDatos.listaPersonajesAliados.Length];
        proximoNivel = new int[baseDeDatos.listaPersonajesAliados.Length];

        numeroAtaques = new int[baseDeDatos.listaPersonajesAliados.Length];
        indicesAtaque = new int[baseDeDatos.listaPersonajesAliados.Length][];

        llevaCasco = new bool[baseDeDatos.listaPersonajesAliados.Length];
        llevaArmadura = new bool[baseDeDatos.listaPersonajesAliados.Length];
        llevaBotas = new bool[baseDeDatos.listaPersonajesAliados.Length];
        llevaArma = new bool[baseDeDatos.listaPersonajesAliados.Length];
        llevaEscudo = new bool[baseDeDatos.listaPersonajesAliados.Length];
        llevaComplemento = new bool[baseDeDatos.listaPersonajesAliados.Length];

        indiceCasco = new int[baseDeDatos.listaPersonajesAliados.Length];
        indiceArmadura = new int[baseDeDatos.listaPersonajesAliados.Length];
        indiceBotas = new int[baseDeDatos.listaPersonajesAliados.Length];
        indiceArma = new int[baseDeDatos.listaPersonajesAliados.Length];
        indiceEscudo = new int[baseDeDatos.listaPersonajesAliados.Length];
        indiceComplemento = new int[baseDeDatos.listaPersonajesAliados.Length];

        for (int i = 0; i < baseDeDatos.listaPersonajesAliados.Length; i++)
        {
            ataqueFisico[i] = baseDeDatos.listaPersonajesAliados[i].ataqueFisico;
            ataqueFisicoActual[i] = baseDeDatos.listaPersonajesAliados[i].ataqueFisicoActual;
            ataqueFisicoModificado[i] = baseDeDatos.listaPersonajesAliados[i].ataqueFisicoModificado;
            ataqueMagico[i] = baseDeDatos.listaPersonajesAliados[i].ataqueMagico;
            ataqueMagicoActual[i] = baseDeDatos.listaPersonajesAliados[i].ataqueMagicoActual;
            ataqueMagicoModificado[i] = baseDeDatos.listaPersonajesAliados[i].ataqueMagicoModificado;
            defensaFisica[i] = baseDeDatos.listaPersonajesAliados[i].defensaFisica;
            defensaFisicaActual[i] = baseDeDatos.listaPersonajesAliados[i].defensaFisicaActual;
            defensaFisicaModificada[i] = baseDeDatos.listaPersonajesAliados[i].defensaFisicaModificada;
            defensaMagica[i] = baseDeDatos.listaPersonajesAliados[i].defensaMagica;
            defensaMagicaActual[i] = baseDeDatos.listaPersonajesAliados[i].defensaMagicaActual;
            defensaMagicaModificada[i] = baseDeDatos.listaPersonajesAliados[i].defensaMagicaModificada;
            velocidad[i] = baseDeDatos.listaPersonajesAliados[i].velocidad;
            velocidadActual[i] = baseDeDatos.listaPersonajesAliados[i].velocidadActual;
            velocidadModificada[i] = baseDeDatos.listaPersonajesAliados[i].velocidadModificada;
            vida[i] = baseDeDatos.listaPersonajesAliados[i].vida;
            vidaActual[i] = baseDeDatos.listaPersonajesAliados[i].vidaActual;
            vidaModificada[i] = baseDeDatos.listaPersonajesAliados[i].vidaModificada;
            evasion[i] = baseDeDatos.listaPersonajesAliados[i].evasion;
            evasionActual[i] = baseDeDatos.listaPersonajesAliados[i].evasionActual;
            precision[i] = baseDeDatos.listaPersonajesAliados[i].precision;
            precisionActual[i] = baseDeDatos.listaPersonajesAliados[i].precisionActual;

            nivel[i] = baseDeDatos.listaPersonajesAliados[i].nivel;
            experiencia[i] = baseDeDatos.listaPersonajesAliados[i].experiencia;
            proximoNivel[i] = baseDeDatos.listaPersonajesAliados[i].proximoNivel;

            numeroAtaques[i] = baseDeDatos.listaPersonajesAliados[i].numeroAtaques;

            indicesAtaque[i] = new int[4];
            
            for (int j = 0; j < 4; j++)
            {
                indicesAtaque[i][j] = baseDeDatos.listaPersonajesAliados[i].indicesAtaque[j];
            }

            llevaCasco[i] = baseDeDatos.listaPersonajesAliados[i].llevaCasco;
            if (llevaCasco[i])
            {
                indiceCasco[i] = baseDeDatos.listaPersonajesAliados[i].casco.indice;
            }
            else
            {
                indiceCasco[i] = -1;
            }

            llevaArmadura[i] = baseDeDatos.listaPersonajesAliados[i].llevaArmadura;
            if (llevaArmadura[i])
            {
                indiceArmadura[i] = baseDeDatos.listaPersonajesAliados[i].armadura.indice;
            }
            else
            {
                indiceArmadura[i] = -1;
            }

            llevaBotas[i] = baseDeDatos.listaPersonajesAliados[i].llevaBotas;
            if (llevaBotas[i])
            {
                indiceBotas[i] = baseDeDatos.listaPersonajesAliados[i].botas.indice;
            }
            else
            {
                indiceBotas[i] = -1;
            }

            llevaArma[i] = baseDeDatos.listaPersonajesAliados[i].llevaArma;
            if (llevaArma[i])
            {
                indiceArma[i] = baseDeDatos.listaPersonajesAliados[i].arma.indice;
            }
            else
            {
                indiceArma[i] = -1;
            }

            llevaEscudo[i] = baseDeDatos.listaPersonajesAliados[i].llevaEscudo;
            if (llevaEscudo[i])
            {
                indiceEscudo[i] = baseDeDatos.listaPersonajesAliados[i].escudo.indice;
            }
            else
            {
                indiceEscudo[i] = -1;
            }

            llevaComplemento[i] = baseDeDatos.listaPersonajesAliados[i].llevaComplemento;
            if (llevaComplemento[i])
            {
                indiceComplemento[i] = baseDeDatos.listaPersonajesAliados[i].complemento.indice;
            }
            else
            {
                indiceComplemento[i] = -1;
            }
        }



        //Consumibles
        indicesConsumibles = new int[baseDeDatos.objetosConsumibles.Length];
        cantidadesConsumibles = new int[baseDeDatos.objetosConsumibles.Length];
        cantidadConsumiblesTotal = baseDeDatos.numeroObjetosConsumibles;

        for (int i = 0; i < cantidadConsumiblesTotal; i++)
        {
            indicesConsumibles[i] = baseDeDatos.objetosConsumibles[i].indice;
            cantidadesConsumibles[i] = baseDeDatos.cantidadesObjetosConsumibles[i];
        }



        //O. ataque
        indicesOAtaque = new int[baseDeDatos.objetosAtaques.Length];
        cantidadesOAtaque = new int[baseDeDatos.objetosAtaques.Length];
        cantidadOAtaqueTotal = baseDeDatos.numeroObjetosAtaques;

        for (int i = 0; i < cantidadOAtaqueTotal; i++)
        {
            indicesOAtaque[i] = baseDeDatos.objetosAtaques[i].indice;
            cantidadesOAtaque[i] = baseDeDatos.cantidadesObjetosAtaques[i];
        }



        //O. equipo
        indicesOEquipo = new int[baseDeDatos.objetosEquipo.Length];
        cantidadesOEquipo = new int[baseDeDatos.objetosEquipo.Length];
        cantidadOEquipoTotal = baseDeDatos.numeroObjetosEquipo;

        for (int i = 0; i < cantidadOEquipoTotal; i++)
        {
            indicesOEquipo[i] = baseDeDatos.objetosEquipo[i].indice;
            cantidadesOEquipo[i] = baseDeDatos.cantidadesObjetosEquipo[i];
        }



        //O.clave
        indicesOClave = new int[baseDeDatos.listaObjetosClave.Length];
        cantidadesOClave = new int[baseDeDatos.listaObjetosClave.Length];
        cantidadOClaveTotal = baseDeDatos.numeroObjetosClave;

        for (int i = 0; i < cantidadOClaveTotal; i++)
        {
            indicesOClave[i] = baseDeDatos.listaObjetosClave[i].indice;
            cantidadesOClave[i] = baseDeDatos.cantidadObjetosClave[i];
        }


        //Cascos
        indicesOCascos = new int[baseDeDatos.cascos.Length];
        cantidadesOCascos = new int[baseDeDatos.cascos.Length];
        cantidadOCascosTotal = baseDeDatos.numeroCascos;

        for (int i = 0; i < cantidadOCascosTotal; i++)
        {
            indicesOCascos[i] = baseDeDatos.cascos[i].indice;
            cantidadesOCascos[i] = baseDeDatos.cantidadCascos[i];
        }



        //Armaduras
        indicesOArmaduras = new int[baseDeDatos.armaduras.Length];
        cantidadesOArmaduras = new int[baseDeDatos.armaduras.Length];
        cantidadOArmadurasTotal = baseDeDatos.numeroArmaduras;

        for (int i = 0; i < cantidadOArmadurasTotal; i++)
        {
            indicesOArmaduras[i] = baseDeDatos.armaduras[i].indice;
            cantidadesOArmaduras[i] = baseDeDatos.cantidadArmaduras[i];
        }



        //Botas
        indicesOBotas = new int[baseDeDatos.botas.Length];
        cantidadesOBotas = new int[baseDeDatos.botas.Length];
        cantidadOBotasTotal = baseDeDatos.numeroBotas;

        for (int i = 0; i < cantidadOBotasTotal; i++)
        {
            indicesOBotas[i] = baseDeDatos.botas[i].indice;
            cantidadesOBotas[i] = baseDeDatos.cantidadBotas[i];
        }



        //Arma
        indicesOArmas = new int[baseDeDatos.armas.Length];
        cantidadesOArmas = new int[baseDeDatos.armas.Length];
        cantidadOArmasTotal = baseDeDatos.numeroArmas;

        for (int i = 0; i < cantidadOArmasTotal; i++)
        {
            indicesOArmas[i] = baseDeDatos.armas[i].indice;
            cantidadesOArmas[i] = baseDeDatos.cantidadArmas[i];
        }



        //Escudos
        indicesOEscudos = new int[baseDeDatos.escudos.Length];
        cantidadesOEscudos = new int[baseDeDatos.escudos.Length];
        cantidadOEscudosTotal = baseDeDatos.numeroEscudos;

        for (int i = 0; i < cantidadOEscudosTotal; i++)
        {
            indicesOEscudos[i] = baseDeDatos.escudos[i].indice;
            cantidadesOEscudos[i] = baseDeDatos.cantidadEscudos[i];
        }



        //Complementos
        indicesOComplementos = new int[baseDeDatos.complemento.Length];
        cantidadesOComplementos = new int[baseDeDatos.complemento.Length];
        cantidadOComplementosTotal = baseDeDatos.numeroComplemento;

        for (int i = 0; i < cantidadOComplementosTotal; i++)
        {
            indicesOComplementos[i] = baseDeDatos.complemento[i].indice;
            cantidadesOComplementos[i] = baseDeDatos.cantidadComplemento[i];
        }



        //Ataques Adquiridos
        ataquesDesbloqueados = new bool[72];
        for (int i = 0; i < 72; i++)
        {
            ataquesDesbloqueados[i] = baseDeDatos.ataquesDesbloqueados[i];
        }



        //Misiones
        numeroMisionesActivas = baseDeDatos.numeroMisionesActivas;
        indicesMisionesActivas = new int[numeroMisionesActivas];
        misionCompletada = new bool[numeroMisionesActivas];
        
        numeroMisionesPrincipales = baseDeDatos.numeroMisionesPrincipales;
        indicesMisionesPrincipales = new int[numeroMisionesPrincipales];
        //Solo paso indice por eso no guarda si algo está completado
        for (int i = 0; i < numeroMisionesPrincipales; i++)
        {
            indicesMisionesPrincipales[i] = baseDeDatos.listaMisionesPrincipales[i].indice;
        }

        numeroMisionesSecundarias = baseDeDatos.numeroMisionesSecundarias;
        indicesMisionesSecundarias = new int[numeroMisionesSecundarias];
        for (int i = 0; i < numeroMisionesSecundarias; i++)
        {
            indicesMisionesSecundarias[i] = baseDeDatos.listaMisionesSecundarias[i].indice;
        }

        numeroMisionesReclutamiento = baseDeDatos.numeroMisionesReclutamiento;
        indicesMisionesReclutamiento = new int[numeroMisionesReclutamiento];
        for (int i = 0; i < numeroMisionesReclutamiento; i++)
        {
            indicesMisionesReclutamiento[i] = baseDeDatos.listaMisionesReclutamiento[i].indice;
        }

        for (int i = 0; i < numeroMisionesActivas; i++)
        {
            indicesMisionesActivas[i] = baseDeDatos.listaMisionesActivas[i].indice;
            misionCompletada[i] = baseDeDatos.listaMisionesActivas[i].completada;
        }

        zonaCamara = baseDeDatos.zonaCamara;
        areaCamara = baseDeDatos.areaCamara;

        //Personajes Desactivados
        personajesDesactivados = new bool[baseDeDatos.personajesDesactivados.Length];

        for (int i = 0; i < personajesDesactivados.Length; i++)
        {
            personajesDesactivados[i] = baseDeDatos.personajesDesactivados[i];
        }
    }
}