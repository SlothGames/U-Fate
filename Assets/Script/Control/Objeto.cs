using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Objeto : System.Object
{
    public string nombre;
    public string nombreIngles;

    public int indice;
    public int indiceAtq;

    [TextArea(10, 5)]
    public string descripcion;
    [TextArea(10, 5)]
    public string descripcionIngles;

    public enum tipoObjeto
    {
        CURACION,
        CURA_ESTADO,
        REVIVIR,
        POTENCIADOR,
        APRENDE_ATAQUE,
        HUIDA,
        EQUIPO,
        CLAVE,
        INGREDIENTE,
        VENTA
    }


    public enum tipoEquipo
    {
        CABEZA,
        CUERPO,
        ESCUDO,
        COMPLEMENTO,
        BOTAS,
        ARMA,
        CLAVE,
        CONSUMIBLE,
        ATAQUE
    }


    public enum rareza
    {
        COMUN,
        RARO,
        MITICO,
        LEGENDARIO
    }


    public enum tipoObjetoIngles
    {
        HEALING,
        REVIVE,
        CURE_STATE,
        LIVE,
        POTENTIATOR,
        LEARN_ATTACK,
        FLIGHT,
        EQUIPMENT,
        KEY,
        INGREDIENT,
        SALE
    }


    public enum tipoEquipoIngles
    {
        HEAD,
        BODY,
        SHIELD,
        COMPLEMENT,
        BOOTS,
        WEAPON,
        KEY,
        CONSUMABLE,
        ATACK
    }


    public enum rarezaIngles
    {
        COMMON,
        RARE,
        MYTHICAL,
        LEGENDARY
    }


    public tipoObjeto tipo;
    public tipoEquipo tipoEq;
    public rareza tipoRareza;

    public tipoObjetoIngles tipoIngles;
    public tipoEquipoIngles tipoEqIngles;
    public rarezaIngles tipoRarezaIngles;

    public int aumentoAtaqueFisico, aumentoAtaqueMagico;
    public int aumentoDefensaFisica, aumentoDefensaMagica;
    public int aumentoEvasion;
    public int aumentoVida;
    public int aumentoVelocidad;
    public int aumentoEnergia;
    public int numeroDeMejoras;
    public int indiceTipo;
    public int valorCompra, valorVenta;

    public bool aumentaAtaqueF, aumentaAtaqueM, aumentaDefensaF, aumentaDefensaM, aumentaEva, aumentaVid, aumentaVel, aumentaEner;

    int turnosActivo;
    public Sprite icono;
    Sprite[] imagenes;
    public Sprite imagen;



    public Objeto()
    {
    }



    public Objeto (Objeto referencia)
    {
        nombre = referencia.nombre;
        nombreIngles = referencia.nombreIngles;

        indice = referencia.indice;
        descripcion = referencia.descripcion;
        descripcionIngles = referencia.descripcionIngles;

        tipo = referencia.tipo;
        tipoEq = referencia.tipoEq; ;
        tipoRareza = referencia.tipoRareza;

        tipoIngles = referencia.tipoIngles;
        tipoEqIngles = referencia.tipoEqIngles;
        tipoRarezaIngles = referencia.tipoRarezaIngles;

        indiceAtq = referencia.indiceAtq;

        aumentoAtaqueFisico = referencia.aumentoAtaqueFisico;
        aumentoAtaqueMagico = referencia.aumentoAtaqueMagico;
        aumentoDefensaFisica = referencia.aumentoDefensaFisica;
        aumentoDefensaMagica = referencia.aumentoDefensaMagica;
        aumentoEvasion = referencia.aumentoEvasion;
        aumentoVida = referencia.aumentoVida;
        aumentoVelocidad = referencia.aumentoVelocidad;
        aumentoEnergia = referencia.aumentoEnergia;
        numeroDeMejoras = referencia.numeroDeMejoras;
        indiceTipo = referencia.indiceTipo;

        aumentaAtaqueF = referencia.aumentaAtaqueF;
        aumentaAtaqueM = referencia.aumentaAtaqueM;
        aumentaDefensaF = referencia.aumentaDefensaF;
        aumentaDefensaM = referencia.aumentaDefensaM;
        aumentaEva = referencia.aumentaEva;
        aumentaVid = referencia.aumentaVid;
        aumentaVel = referencia.aumentaVel;
        aumentaEner = referencia.aumentaEner;
        turnosActivo = referencia.turnosActivo;
        icono = referencia.icono;

        valorCompra = referencia.valorCompra;
        valorVenta = referencia.valorVenta;
    }



    public Objeto IniciarObjeto(int valor)
    {
        //EL NOMBRE MÁXIMO 16 CARACTERES
        indice = valor;
        indiceAtq = -1;

        numeroDeMejoras = 0;

        aumentoVida = aumentoVelocidad = aumentoEvasion = aumentoDefensaMagica = aumentoEnergia = aumentoAtaqueMagico = aumentoAtaqueFisico = numeroDeMejoras = aumentoDefensaFisica = 0;
        aumentaAtaqueF = aumentaAtaqueM = aumentaVid = aumentaDefensaM = aumentaEva = aumentaVel = aumentaEner = aumentaDefensaF = false;

        switch (valor)
        {
            case 0:
                nombre = "Galleta";
                descripcion = "Recupera 20 puntos de vida.";

                nombreIngles = "Cookie";
                descripcionIngles = "Recover 20 life points.";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Comida/food_41");
                imagen = imagenes[0];
                tipo = tipoObjeto.CURACION;
                tipoEq = tipoEquipo.CONSUMIBLE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.HEALING;
                tipoEqIngles = tipoEquipoIngles.CONSUMABLE;
                tipoRarezaIngles = rarezaIngles.COMMON;

                aumentoVida = 20;
                aumentaVid = true;

                indiceTipo = 0;
                valorCompra = 250;
                valorVenta = 125;
                break;
            case 1:
                nombre = "Manzana";
                descripcion = "Recupera 50 puntos de vida.";

                nombreIngles = "Apple";
                descripcionIngles = "Recover 50 life points.";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Comida/food_01");
                imagen = imagenes[0];
                tipo = tipoObjeto.CURACION;
                tipoEq = tipoEquipo.CONSUMIBLE;
                tipoRareza = rareza.RARO;

                tipoIngles = tipoObjetoIngles.HEALING;
                tipoEqIngles = tipoEquipoIngles.CONSUMABLE;
                tipoRarezaIngles = rarezaIngles.RARE;

                aumentoVida = 50;
                aumentaVid = true;

                indiceTipo = 1;
                valorCompra = 500;
                valorVenta = 250;
                break;
            case 2:
                nombre = "Pizza";
                descripcion = "Este objeto devolverá a la vida a cualquier aliado caído en combate.";

                nombreIngles = "Pizza";
                descripcionIngles = "This item will bring to life any ally fallen in combat.";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Comida/food_128");
                imagen = imagenes[0];

                tipo = tipoObjeto.REVIVIR;
                tipoEq = tipoEquipo.CONSUMIBLE;
                tipoRareza = rareza.RARO;

                tipoIngles = tipoObjetoIngles.REVIVE;
                tipoEqIngles = tipoEquipoIngles.CONSUMABLE;
                tipoRarezaIngles = rarezaIngles.COMMON;

                aumentoVida = 1;
                aumentaVid = true;

                indiceTipo = 2;
                valorCompra = 1300;
                valorVenta = 650;
                break;
            case 3:
                nombre = "Vitaminas";
                descripcion = "Aumenta un 10% tu ataque y defensa física";

                nombreIngles = "Vitamins";
                descripcionIngles = "Increase your physical attack and defence by 10%";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Pastillas/pill_01");
                imagen = imagenes[0];

                tipo = tipoObjeto.POTENCIADOR;
                tipoEq = tipoEquipo.CONSUMIBLE;

                tipoIngles = tipoObjetoIngles.POTENTIATOR;
                tipoEqIngles = tipoEquipoIngles.CONSUMABLE;

                numeroDeMejoras = 2;
                aumentoDefensaFisica = aumentoAtaqueFisico = 10;
                aumentaAtaqueF = true;
                aumentaDefensaF = true;

                indiceTipo = 3;
                valorCompra = 250;
                valorVenta = 125;
                break;
            case 4:
                nombre = "Escudo Básico";
                descripcion = "Aumenta en 10 puntos la defensa física";

                nombreIngles = "Basic Shield";
                descripcionIngles = "Increase physical defence by 10 points";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Objetos/Equipamiento/Shield Pack");
                imagen = imagenes[0];

                tipo = tipoObjeto.EQUIPO;
                tipoEq = tipoEquipo.ESCUDO;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.EQUIPMENT;
                tipoEqIngles = tipoEquipoIngles.SHIELD;
                tipoRarezaIngles = rarezaIngles.COMMON;

                numeroDeMejoras = 1;
                aumentoDefensaFisica = 10;
                aumentaDefensaF = true;

                indiceTipo = 0;
                valorCompra = 800;
                valorVenta = 400;
                break;
            case 5:
                nombre = "Escudo Reforzado";
                descripcion = "Aumenta en 20 puntos la defensa física";

                nombreIngles = "Reinforced Shield";
                descripcionIngles = "Increase physical defence by 20 points";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Objetos/Equipamiento/Shield Pack");
                imagen = imagenes[3];

                tipo = tipoObjeto.EQUIPO;
                tipoEq = tipoEquipo.ESCUDO;
                tipoRareza = rareza.RARO;

                tipoIngles = tipoObjetoIngles.EQUIPMENT;
                tipoEqIngles = tipoEquipoIngles.SHIELD;
                tipoRarezaIngles = rarezaIngles.RARE;

                numeroDeMejoras = 1;
                aumentoDefensaFisica = 20;
                aumentaDefensaF = true;

                indiceTipo = 1;
                valorCompra = 1500;
                valorVenta = 750;
                break;
            case 6:
                nombre = "Armadura Básica";
                descripcion = "Aumenta tu vida en 20 puntos y te da 5 puntos en defensa física y mágica";

                nombreIngles = "Basic Armor";
                descripcionIngles = "Increase your life by 20 points and give you 5 points in physical and magical defence";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Armaduras/armor_01");
                imagen = imagenes[0];

                tipo = tipoObjeto.EQUIPO;
                tipoEq = tipoEquipo.CUERPO;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.EQUIPMENT;
                tipoEqIngles = tipoEquipoIngles.BODY;
                tipoRarezaIngles = rarezaIngles.COMMON;

                numeroDeMejoras = 3;
                aumentoVida = 20;
                aumentoDefensaMagica = aumentoDefensaFisica = 5;
                aumentaVid = aumentaDefensaF = aumentaDefensaM = true;

                indiceTipo = 2;
                valorCompra = 800;
                valorVenta = 400;
                break;
            case 7:
                nombre = "Armadura Bronce";
                descripcion = "Aumenta tu vida en 120 puntos y te da 15 puntos en defensa física y mágica";

                nombreIngles = "Bronze Armor";
                descripcionIngles = "Increase your life by 120 points and give you 15 points in physical and magical defence";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Armaduras/armor_06");
                imagen = imagenes[0];

                tipo = tipoObjeto.EQUIPO;
                tipoEq = tipoEquipo.CUERPO;
                tipoRareza = rareza.RARO;

                tipoIngles = tipoObjetoIngles.EQUIPMENT;
                tipoEqIngles = tipoEquipoIngles.BODY;
                tipoRarezaIngles = rarezaIngles.RARE;

                numeroDeMejoras = 3;
                aumentoVida = 120;
                aumentoDefensaMagica = aumentoDefensaFisica = 15;
                aumentaVid = aumentaDefensaF = aumentaDefensaM = true;

                indiceTipo = 3;
                valorCompra = 3000;
                valorVenta = 1500;
                break;
            case 8:
                nombre = "ATQ.01";

                nombreIngles = "ATCK.01";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_261");
                imagen = imagenes[0];

                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 0;
                valorCompra = 500;
                valorVenta = 250;
                break;
            case 9:
                nombre = "ATQ.02";

                nombreIngles = "ATCK.02";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_261");
                imagen = imagenes[0];
                
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 1;
                valorCompra = 500;
                valorVenta = 250;
                break;
            case 10:
                nombre = "ATQ.03";

                nombreIngles = "ATCK.03";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_265");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 2;
                valorCompra = 500;
                valorVenta = 250;
                break;
            case 11:
                nombre = "ATQ.04";

                nombreIngles = "ATCK.04";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_265");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 3;
                valorCompra = 500;
                valorVenta = 250;
                break;
            case 12:
                nombre = "ATQ.05";

                nombreIngles = "ATCK.05";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_266");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 4;
                valorCompra = 500;
                valorVenta = 250;
                break;
            case 13:
                nombre = "ATQ.06";

                nombreIngles = "ATCK.06";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_266");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 5;
                valorCompra = 500;
                valorVenta = 250;
                break;
            case 14:
                nombre = "ATQ.07";

                nombreIngles = "ATCK.07";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_268");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 6;
                valorCompra = 500;
                valorVenta = 250;
                break;
            case 15:
                nombre = "ATQ.08";

                nombreIngles = "ATCK.08";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_268");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 7;
                valorCompra = 500;
                valorVenta = 250;
                break;
            case 16:
                nombre = "ATQ.09";

                nombreIngles = "ATCK.09";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_270");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 8;
                valorCompra = 500;
                valorVenta = 250;
                break;
            case 17:
                nombre = "ATQ.10";

                nombreIngles = "ATCK.10";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_270");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 9;
                valorCompra = 500;
                valorVenta = 250;
                break;
            case 18:
                nombre = "ATQ.11";

                nombreIngles = "ATCK.11";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_269");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 10;
                valorCompra = 500;
                valorVenta = 250;
                break;
            case 19:
                nombre = "ATQ.12";

                nombreIngles = "ATCK.12";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_269");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 11;
                valorCompra = 500;
                valorVenta = 250;
                break;
            case 20:
                nombre = "ATQ.13";

                nombreIngles = "ATCK.13";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_268");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 12;
                valorCompra = 1000;
                valorVenta = 500;
                break;
            case 21:
                nombre = "ATQ.14";

                nombreIngles = "ATCK.14";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_268");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 13;
                valorCompra = 1000;
                valorVenta = 500;
                break;
            case 22:
                nombre = "ATQ.15";

                nombreIngles = "ATCK.15";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_261");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 14;
                valorCompra = 1000;
                valorVenta = 500;
                break;
            case 23:
                nombre = "ATQ.16";

                nombreIngles = "ATCK.16";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_261");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 15;
                valorCompra = 1000;
                valorVenta = 500;
                break;
            case 24:
                nombre = "ATQ.17";

                nombreIngles = "ATCK.17";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_266");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 16;
                valorCompra = 1000;
                valorVenta = 500;
                break;
            case 25:
                nombre = "ATQ.18";

                nombreIngles = "ATCK.18";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_266");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 17;
                valorCompra = 1000;
                valorVenta = 500;
                break;
            case 26:
                nombre = "ATQ.19";

                nombreIngles = "ATCK.19";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_270");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 18;
                valorCompra = 1000;
                valorVenta = 500;
                break;
            case 27:
                nombre = "ATQ.20";

                nombreIngles = "ATCK.20";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_270");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 19;
                valorCompra = 1000;
                valorVenta = 500;
                break;
            case 28:
                nombre = "ATQ.21";

                nombreIngles = "ATCK.21";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_265");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 20;
                valorCompra = 1000;
                valorVenta = 500;
                break;
            case 29:
                nombre = "ATQ.22";

                nombreIngles = "ATCK.22";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_265");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 21;
                valorCompra = 1000;
                valorVenta = 500;
                break;
            case 30:
                nombre = "ATQ.23";

                nombreIngles = "ATCK.23";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_269");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 22;
                valorCompra = 1000;
                valorVenta = 500;
                break;
            case 31:
                nombre = "ATQ.24";

                nombreIngles = "ATCK.24";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_269");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 23;
                valorCompra = 1000;
                valorVenta = 500;
                break;
            case 32:
                nombre = "ATQ.25";

                nombreIngles = "ATCK.25";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_266");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 24;
                valorCompra = 1500;
                valorVenta = 750;
                break;
            case 33:
                nombre = "ATQ.26";

                nombreIngles = "ATCK.26";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_270");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 25;
                valorCompra = 1500;
                valorVenta = 750;
                break;
            case 34:
                nombre = "ATQ.27";

                nombreIngles = "ATCK.27";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_270");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 26;
                valorCompra = 1500;
                valorVenta = 750;
                break;
            case 35:
                nombre = "ATQ.28";

                nombreIngles = "ATCK.28";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_268");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 27;
                valorCompra = 1500;
                valorVenta = 750;
                break;
            case 36:
                nombre = "ATQ.29";

                nombreIngles = "ATCK.29";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_261");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 28;
                valorCompra = 1500;
                valorVenta = 750;
                break;
            case 37:
                nombre = "ATQ.30";

                nombreIngles = "ATCK.30";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_261");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 29;
                valorCompra = 1500;
                valorVenta = 750;
                break;
            case 38:
                nombre = "ATQ.31";

                nombreIngles = "ATCK.31";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_265");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 30;
                valorCompra = 1500;
                valorVenta = 750;
                break;
            case 39:
                nombre = "ATQ.32";

                nombreIngles = "ATCK.32";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_265");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 31;
                valorCompra = 1500;
                valorVenta = 750;
                break;
            case 40:
                nombre = "ATQ.33";

                nombreIngles = "ATCK.33";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_270");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 32;
                valorCompra = 1500;
                valorVenta = 750;
                break;
            case 41:
                nombre = "ATQ.34";

                nombreIngles = "ATCK.34";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_270");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 33;
                valorCompra = 1500;
                valorVenta = 750;
                break;
            case 42:
                nombre = "ATQ.35";

                nombreIngles = "ATCK.35";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_269");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 34;
                valorCompra = 1500;
                valorVenta = 750;
                break;
            case 43:
                nombre = "ATQ.36";

                nombreIngles = "ATCK.36";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_269");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 35;
                valorCompra = 1500;
                valorVenta = 750;
                break;
            case 44:
                nombre = "ATQ.37";

                nombreIngles = "ATCK.37";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_265");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 36;
                valorCompra = 2000;
                valorVenta = 1000;
                break;
            case 45:
                nombre = "ATQ.38";

                nombreIngles = "ATCK.38";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_265");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 37;
                valorCompra = 2000;
                valorVenta = 1000;
                break;
            case 46:
                nombre = "ATQ.39";

                nombreIngles = "ATCK.39";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_266");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 38;
                valorCompra = 2000;
                valorVenta = 1000;
                break;
            case 47:
                nombre = "ATQ.40";

                nombreIngles = "ATCK.40";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_266");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 39;
                valorCompra = 2000;
                valorVenta = 1000;
                break;
            case 48:
                nombre = "ATQ.41";

                nombreIngles = "ATCK.41";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_268");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 40;
                valorCompra = 2000;
                valorVenta = 1000;
                break;
            case 49:
                nombre = "ATQ.42";

                nombreIngles = "ATCK.42";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_268");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 41;
                valorCompra = 2000;
                valorVenta = 1000;
                break;
            case 50:
                nombre = "ATQ.43";

                nombreIngles = "ATCK.43";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_270");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 42;
                valorCompra = 2000;
                valorVenta = 1000;
                break;
            case 51:
                nombre = "ATQ.44";

                nombreIngles = "ATCK.44";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_270");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 43;
                valorCompra = 2000;
                valorVenta = 1000;
                break;
            case 52:
                nombre = "ATQ.45";

                nombreIngles = "ATCK.45";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_269");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 44;
                valorCompra = 2000;
                valorVenta = 1000;
                break;
            case 53:
                nombre = "ATQ.46";

                nombreIngles = "ATCK.46";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_269");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 45;
                valorCompra = 2000;
                valorVenta = 1000;
                break;
            case 54:
                nombre = "ATQ.47";

                nombreIngles = "ATCK.47";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_261");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 46;
                valorCompra = 2000;
                valorVenta = 1000;
                break;
            case 55:
                nombre = "ATQ.48";

                nombreIngles = "ATCK.48";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_261");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 47;
                valorCompra = 2000;
                valorVenta = 1000;
                break;
            case 56:
                nombre = "ATQ.49";

                nombreIngles = "ATCK.49";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_263");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 48;
                valorCompra = 500;
                valorVenta = 250;
                break;
            case 57:
                nombre = "ATQ.50";

                nombreIngles = "ATCK.50";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_263");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 49;
                valorCompra = 500;
                valorVenta = 250;
                break;
            case 58:
                nombre = "ATQ.51";

                nombreIngles = "ATCK.51";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_263");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 50;
                valorCompra = 500;
                valorVenta = 250;
                break;
            case 59:
                nombre = "ATQ.52";

                nombreIngles = "ATCK.52";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_263");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 51;
                valorCompra = 500;
                valorVenta = 250;
                break;
            case 60:
                nombre = "ATQ.53";

                nombreIngles = "ATCK.53";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_263");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 52;
                valorCompra = 500;
                valorVenta = 250;
                break;
            case 61:
                nombre = "ATQ.54";

                nombreIngles = "ATCK.54";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_263");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 53;
                valorCompra = 500;
                valorVenta = 250;
                break;
            case 62:
                nombre = "ATQ.55";

                nombreIngles = "ATCK.55";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_263");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 54;
                valorCompra = 500;
                valorVenta = 250;
                break;
            case 63:
                nombre = "ATQ.56";

                nombreIngles = "ATCK.56";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_263");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 55;
                valorCompra = 500;
                valorVenta = 250;
                break;
            case 64:
                nombre = "ATQ.57";

                nombreIngles = "ATCK.57";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_263");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 56;
                valorCompra = 500;
                valorVenta = 250;
                break;
            case 65:
                nombre = "ATQ.58";

                nombreIngles = "ATCK.58";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_263");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 57;
                valorCompra = 500;
                valorVenta = 250;
                break;
            case 66:
                nombre = "ATQ.59";

                nombreIngles = "ATCK.59";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_263");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 58;
                valorCompra = 500;
                valorVenta = 250;
                break;
            case 67:
                nombre = "ATQ.60";

                nombreIngles = "ATCK.60";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_263");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 59;
                valorCompra = 500;
                valorVenta = 250;
                break;
            case 68:
                nombre = "ATQ.61";

                nombreIngles = "ATCK.61";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_263");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 60;
                valorCompra = 500;
                valorVenta = 250;
                break;
            case 69:
                nombre = "ATQ.62";

                nombreIngles = "ATCK.62";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_263");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 61;
                valorCompra = 500;
                valorVenta = 250;
                break;
            case 70:
                nombre = "ATQ.63";

                nombreIngles = "ATCK.63";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_263");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 62;
                valorCompra = 500;
                valorVenta = 250;
                break;
            case 71:
                nombre = "ATQ.64";

                nombreIngles = "ATCK.64";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_263");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 63;
                valorCompra = 500;
                valorVenta = 250;
                break;
            case 72:
                nombre = "ATQ.65";

                nombreIngles = "ATCK.65";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_263");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 64;
                valorCompra = 500;
                valorVenta = 250;
                break;
            case 73:
                nombre = "ATQ.66";

                nombreIngles = "ATCK.66";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_263");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 65;
                valorCompra = 500;
                valorVenta = 250;
                break;
            case 74:
                nombre = "ATQ.67";

                nombreIngles = "ATCK.67";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_263");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 66;
                valorCompra = 500;
                valorVenta = 250;
                break;
            case 75:
                nombre = "ATQ.68";

                nombreIngles = "ATCK.68";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_263");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 67;
                valorCompra = 500;
                valorVenta = 250;
                break;
            case 76:
                nombre = "ATQ.69";

                nombreIngles = "ATCK.69";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_263");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 68;
                valorCompra = 500;
                valorVenta = 250;
                break;
            case 77:
                nombre = "ATQ.70";

                nombreIngles = "ATCK.70";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_263");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 69;
                valorCompra = 500;
                valorVenta = 250;
                break;
            case 78:
                nombre = "ATQ.71";

                nombreIngles = "ATCK.71";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_263");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 70;
                valorCompra = 500;
                valorVenta = 250;
                break;
            case 79:
                nombre = "ATQ.72";

                nombreIngles = "ATCK.72";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Ataques/quest_263");
                imagen = imagenes[0];
                tipo = tipoObjeto.APRENDE_ATAQUE;
                tipoEq = tipoEquipo.ATAQUE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.LEARN_ATTACK;
                tipoEqIngles = tipoEquipoIngles.ATACK;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceAtq = 71;
                valorCompra = 500;
                valorVenta = 250;
                break;
            case 80:
                nombre = "Llave común";
                descripcion = "Una llave que te dio tu padre de su época de estudiante y que sirve para abrir ciertos tipos de cofres.";

                nombreIngles = "Common key";
                descripcionIngles = "A key that your father gave you from his student days and open certain types of chests.";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Objetos/Equipamiento/roguelikeitems");
                imagen = imagenes[50];
                tipo = tipoObjeto.CLAVE;
                tipoEq = tipoEquipo.CLAVE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.KEY;
                tipoEqIngles = tipoEquipoIngles.KEY;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceTipo = 0;
                valorCompra = 0;
                valorVenta = 0;
                break;
            case 81:
                nombre = "Casco Básico";
                descripcion = "Aumenta en 10 puntos la defensa mágica y 5 la velocidad.";

                nombreIngles = "Basic Helmet";
                descripcionIngles = "Increase magic defence by 10 points and speed 5.";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Cascos/helmet_03");
                imagen = imagenes[0];
                tipo = tipoObjeto.EQUIPO;
                tipoEq = tipoEquipo.CABEZA;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.EQUIPMENT;
                tipoEqIngles = tipoEquipoIngles.HEAD;
                tipoRarezaIngles = rarezaIngles.COMMON;

                numeroDeMejoras = 2;
                aumentoDefensaMagica = 10;
                aumentoVelocidad = 5;
                aumentaDefensaM = aumentaVel = true;

                indiceTipo = 4;
                valorCompra = 800;
                valorVenta = 400;
                break;
            case 82:
                nombre = "Casco de Paladín";
                descripcion = "Aumenta en 30 puntos la defensa física y mágica.";

                nombreIngles = "Paladin Helmet";
                descripcionIngles = "Increase physical and magic defence by 30 points.";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Cascos/helmet_10");
                imagen = imagenes[0];
                tipo = tipoObjeto.EQUIPO;
                tipoEq = tipoEquipo.CABEZA;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.EQUIPMENT;
                tipoEqIngles = tipoEquipoIngles.HEAD;
                tipoRarezaIngles = rarezaIngles.COMMON;

                numeroDeMejoras = 2;
                aumentoDefensaMagica = 30;
                aumentoDefensaFisica = 30;
                aumentaDefensaF = aumentaDefensaM = true;

                indiceTipo = 5;
                valorCompra = 3000;
                valorVenta = 1500;
                break;
            case 83:
                nombre = "Casco Guerrero";
                descripcion = "Aumenta en 30 puntos la defensa física, pero reduce la velocidad en 5 puntos.";

                nombreIngles = "Warrior Helmet";
                descripcionIngles = "Increase physical defence by 30 points, but reduce speed by 5 points.";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Cascos/helmet_07");
                imagen = imagenes[0];
                tipo = tipoObjeto.EQUIPO;
                tipoEq = tipoEquipo.CABEZA;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.EQUIPMENT;
                tipoEqIngles = tipoEquipoIngles.HEAD;
                tipoRarezaIngles = rarezaIngles.COMMON;

                numeroDeMejoras = 2;
                aumentoDefensaFisica = 30;
                aumentoVelocidad = -5;
                aumentaDefensaF = aumentaVel = true;

                indiceTipo = 6;
                valorCompra = 1500;
                valorVenta = 750;
                break;
            case 84:
                nombre = "Sombrero Mago";
                descripcion = "Aumenta en 20 puntos la defensa mágica y en 5 puntos el ataque mágico.";

                nombreIngles = "Magician Hat";
                descripcionIngles = "Increase the magic defence by 20 points and the magic attack by 5 points.";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Cascos/helmet_26");
                imagen = imagenes[0];
                tipo = tipoObjeto.EQUIPO;
                tipoEq = tipoEquipo.CABEZA;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.EQUIPMENT;
                tipoEqIngles = tipoEquipoIngles.HEAD;
                tipoRarezaIngles = rarezaIngles.COMMON;

                numeroDeMejoras = 2;
                aumentoDefensaMagica = 30;
                aumentoAtaqueMagico = 5;
                aumentaDefensaM = aumentaAtaqueM = true;

                indiceTipo = 7;
                valorCompra = 2000;
                valorVenta = 1000;
                break;
            case 85:
                nombre = "Espada Básica";
                descripcion = "Aumenta en 10 puntos el ataque físico.";

                nombreIngles = "Basic Sword";
                descripcionIngles = "Increase the physical attack by 10 points.";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Armas/weapon_01");
                imagen = imagenes[0];
                tipo = tipoObjeto.EQUIPO;
                tipoEq = tipoEquipo.ARMA;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.EQUIPMENT;
                tipoEqIngles = tipoEquipoIngles.WEAPON;
                tipoRarezaIngles = rarezaIngles.COMMON;

                numeroDeMejoras = 1;
                aumentoAtaqueFisico = 10;
                aumentaAtaqueF = true;

                indiceTipo = 8;
                valorCompra = 800;
                valorVenta = 400;
                break;
            case 86:
                nombre = "Espada Caballero";
                descripcion = "Aumenta en 20 puntos el ataque físico y en 10 la defensa física.";

                nombreIngles = "Knight Sword";
                descripcionIngles = "Increase physical attack by 20 points and physical defence by 10.";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Armas/weapon_35");
                imagen = imagenes[0];
                tipo = tipoObjeto.EQUIPO;
                tipoEq = tipoEquipo.ARMA;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.EQUIPMENT;
                tipoEqIngles = tipoEquipoIngles.WEAPON;
                tipoRarezaIngles = rarezaIngles.COMMON;

                numeroDeMejoras = 2;
                aumentoAtaqueFisico = 20;
                aumentoDefensaFisica = 10;
                aumentaAtaqueF = aumentaDefensaF = true;

                indiceTipo = 9;
                valorCompra = 2000;
                valorVenta = 1000;
                break;
            case 87:
                nombre = "Espina Mágica";
                descripcion = "Aumenta en 100 puntos el ataque mágico, pero reduce la salud en 20 puntos y la defensa mágica en 30.";

                nombreIngles = "Magic Thorn";
                descripcionIngles = "Increases the magic attack by 100 points, but reduces health by 20 points and the magic defence by 30.";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Armas/weapon_404");
                imagen = imagenes[0];
                tipo = tipoObjeto.EQUIPO;
                tipoEq = tipoEquipo.ARMA;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.EQUIPMENT;
                tipoEqIngles = tipoEquipoIngles.WEAPON;
                tipoRarezaIngles = rarezaIngles.COMMON;

                numeroDeMejoras = 3;
                aumentoAtaqueMagico = 100;
                aumentoDefensaMagica = -20;
                aumentoVida = -30;
                aumentaAtaqueM = aumentaDefensaM = aumentaVid = true;

                indiceTipo = 10;
                valorCompra = 20000;
                valorVenta = 10000;
                break;
            case 88:
                nombre = "Llave Rara";
                descripcion = "Una llave que permite abrir los cofres raros.";

                nombreIngles = "Rare Key";
                descripcionIngles = "A key that allows to open the rare chests.";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Objetos/Equipamiento/roguelikeitems");
                imagen = imagenes[50];
                tipo = tipoObjeto.CLAVE;
                tipoEq = tipoEquipo.CLAVE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.KEY;
                tipoEqIngles = tipoEquipoIngles.KEY;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceTipo = 1;
                valorCompra = 0;
                valorVenta = 0;
                break;
            case 89:
                nombre = "Gema de la Vida";
                descripcion = "Gema mística que Gámez te pidió proteger.";

                nombreIngles = "Life Gem";
                descripcionIngles = "Mystical gem that Gámez asked you to protect.";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Objetos/Equipamiento/roguelikeitems");
                imagen = imagenes[41];
                tipo = tipoObjeto.CLAVE;
                tipoEq = tipoEquipo.CLAVE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.KEY;
                tipoEqIngles = tipoEquipoIngles.KEY;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceTipo = 2;
                valorCompra = 0;
                valorVenta = 0;
                break;
            case 90:
                nombre = "Mapa";
                descripcion = "Mapa del Reino de Áncia.";

                nombreIngles = "Map";
                descripcionIngles = "Map of the Kingdom of Áncia.";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Objetos/Equipamiento/Mapa");
                imagen = imagenes[0];
                
                tipo = tipoObjeto.CLAVE;
                tipoEq = tipoEquipo.CLAVE;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.KEY;
                tipoEqIngles = tipoEquipoIngles.KEY;
                tipoRarezaIngles = rarezaIngles.COMMON;

                indiceTipo = 3;
                valorCompra = 0;
                valorVenta = 0;
                break;
            case 91:
                nombre = "Sauce";
                descripcion = "Ingrediente usado para ciertos tipos de pociones.";

                nombreIngles = "Willow";
                descripcionIngles = "Ingredient used for certain types of potions.";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Objetos/Equipamiento/roguelikeitems");
                imagen = imagenes[155];

                tipo = tipoObjeto.INGREDIENTE;
                tipoEq = tipoEquipo.CONSUMIBLE;

                tipoIngles = tipoObjetoIngles.INGREDIENT;
                tipoEqIngles = tipoEquipoIngles.CONSUMABLE;

                indiceTipo = 4;
                valorCompra = 250;
                valorVenta = 125;
                break;
            case 92:
                nombre = "Laurel";
                descripcion = "Ingrediente usado para ciertos tipos de pociones.";

                nombreIngles = "Laurel";
                descripcionIngles = "Ingredient used for certain types of potions.";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Objetos/Equipamiento/roguelikeitems");
                imagen = imagenes[156];

                tipo = tipoObjeto.INGREDIENTE;
                tipoEq = tipoEquipo.CONSUMIBLE;

                tipoIngles = tipoObjetoIngles.INGREDIENT;
                tipoEqIngles = tipoEquipoIngles.CONSUMABLE;

                indiceTipo = 5;
                valorCompra = 250;
                valorVenta = 125;
                break;
            case 93:
                nombre = "Dedalera";
                descripcion = "Ingrediente usado para ciertos tipos de pociones.";

                nombreIngles = "Dedalera";
                descripcionIngles = "Ingredient used for certain types of potions.";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Objetos/Equipamiento/roguelikeitems");
                imagen = imagenes[157];

                tipo = tipoObjeto.INGREDIENTE;
                tipoEq = tipoEquipo.CONSUMIBLE;

                tipoIngles = tipoObjetoIngles.INGREDIENT;
                tipoEqIngles = tipoEquipoIngles.CONSUMABLE;

                indiceTipo = 6;
                valorCompra = 250;
                valorVenta = 125;
                break;
            case 94:
                nombre = "Alga";
                descripcion = "Ingrediente usado para ciertos tipos de pociones.";

                nombreIngles = "Alga";
                descripcionIngles = "Ingredient used for certain types of potions.";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Objetos/Equipamiento/roguelikeitems");
                imagen = imagenes[158];

                tipo = tipoObjeto.INGREDIENTE;
                tipoEq = tipoEquipo.CONSUMIBLE;

                tipoIngles = tipoObjetoIngles.INGREDIENT;
                tipoEqIngles = tipoEquipoIngles.CONSUMABLE;

                indiceTipo = 7;
                valorCompra = 250;
                valorVenta = 125;
                break;
            case 95:
                nombre = "Amanita";
                descripcion = "Ingrediente usado para ciertos tipos de pociones.";

                nombreIngles = "Amanita";
                descripcionIngles = "Ingredient used for certain types of potions.";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Objetos/Equipamiento/roguelikeitems");
                imagen = imagenes[159];

                tipo = tipoObjeto.INGREDIENTE;
                tipoEq = tipoEquipo.CONSUMIBLE;

                tipoIngles = tipoObjetoIngles.INGREDIENT;
                tipoEqIngles = tipoEquipoIngles.CONSUMABLE;

                indiceTipo = 8;
                valorCompra = 250;
                valorVenta = 125;
                break;
            case 96:
                nombre = "Rizoma";
                descripcion = "Ingrediente usado para ciertos tipos de pociones.";

                nombreIngles = "Rizoma";
                descripcionIngles = "Ingredient used for certain types of potions.";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Objetos/Equipamiento/roguelikeitems");
                imagen = imagenes[160];

                tipo = tipoObjeto.INGREDIENTE;
                tipoEq = tipoEquipo.CONSUMIBLE;

                tipoIngles = tipoObjetoIngles.INGREDIENT;
                tipoEqIngles = tipoEquipoIngles.CONSUMABLE;

                indiceTipo = 9;
                valorCompra = 250;
                valorVenta = 125;
                break;
            case 97:
                nombre = "Acebo";
                descripcion = "Ingrediente usado para ciertos tipos de pociones.";

                nombreIngles = "Holly";
                descripcionIngles = "Ingredient used for certain types of potions.";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Objetos/Equipamiento/roguelikeitems");
                imagen = imagenes[161];

                tipo = tipoObjeto.INGREDIENTE;
                tipoEq = tipoEquipo.CONSUMIBLE;

                tipoIngles = tipoObjetoIngles.INGREDIENT;
                tipoEqIngles = tipoEquipoIngles.CONSUMABLE;

                indiceTipo = 10;
                valorCompra = 250;
                valorVenta = 125;
                break;
            case 98:
                nombre = "Esporas";
                descripcion = "Ingrediente usado para ciertos tipos de pociones.";

                nombreIngles = "Spores";
                descripcionIngles = "Ingredient used for certain types of potions.";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Objetos/Equipamiento/roguelikeitems");
                imagen = imagenes[168];

                tipo = tipoObjeto.INGREDIENTE;
                tipoEq = tipoEquipo.CONSUMIBLE;

                tipoIngles = tipoObjetoIngles.INGREDIENT;
                tipoEqIngles = tipoEquipoIngles.CONSUMABLE;

                indiceTipo = 11;
                valorCompra = 250;
                valorVenta = 125;
                break;
            case 99:
                nombre = "Abedul";
                descripcion = "Ingrediente usado para ciertos tipos de pociones.";

                nombreIngles = "Birch";
                descripcionIngles = "Ingredient used for certain types of potions.";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Objetos/Equipamiento/roguelikeitems");
                imagen = imagenes[169];

                tipo = tipoObjeto.INGREDIENTE;
                tipoEq = tipoEquipo.CONSUMIBLE;

                tipoIngles = tipoObjetoIngles.INGREDIENT;
                tipoEqIngles = tipoEquipoIngles.CONSUMABLE;

                indiceTipo = 12;
                valorCompra = 250;
                valorVenta = 125;
                break;
            case 100:
                nombre = "Nomeolvides";
                descripcion = "Ingrediente usado para ciertos tipos de pociones.";

                nombreIngles = "Forget-me-not";
                descripcionIngles = "Ingredient used for certain types of potions.";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Objetos/Equipamiento/roguelikeitems");
                imagen = imagenes[171];

                tipo = tipoObjeto.INGREDIENTE;
                tipoEq = tipoEquipo.CONSUMIBLE;

                tipoIngles = tipoObjetoIngles.INGREDIENT;
                tipoEqIngles = tipoEquipoIngles.CONSUMABLE;

                indiceTipo = 13;
                valorCompra = 250;
                valorVenta = 125;
                break;
            case 101:
                nombre = "Espino";
                descripcion = "Ingrediente usado para ciertos tipos de pociones.";

                nombreIngles = "Thorn";
                descripcionIngles = "Ingredient used for certain types of potions.";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Objetos/Equipamiento/roguelikeitems");
                imagen = imagenes[170];

                tipo = tipoObjeto.INGREDIENTE;
                tipoEq = tipoEquipo.CONSUMIBLE;

                tipoIngles = tipoObjetoIngles.INGREDIENT;
                tipoEqIngles = tipoEquipoIngles.CONSUMABLE;

                indiceTipo = 14;
                valorCompra = 250;
                valorVenta = 125;
                break;
            case 102:
                nombre = "Calocybe";
                descripcion = "Ingrediente usado para ciertos tipos de pociones.";

                nombreIngles = "Calocybe";
                descripcionIngles = "Ingredient used for certain types of potions.";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Objetos/Equipamiento/roguelikeitems");
                imagen = imagenes[172];

                tipo = tipoObjeto.INGREDIENTE;
                tipoEq = tipoEquipo.CONSUMIBLE;

                tipoIngles = tipoObjetoIngles.INGREDIENT;
                tipoEqIngles = tipoEquipoIngles.CONSUMABLE;

                indiceTipo = 15;
                valorCompra = 250;
                valorVenta = 125;
                break;
            case 103:
                nombre = "Rosas";
                descripcion = "Ingrediente usado para ciertos tipos de pociones.";

                nombreIngles = "Roses";
                descripcionIngles = "Ingredient used for certain types of potions.";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Objetos/Equipamiento/roguelikeitems");
                imagen = imagenes[173];

                tipo = tipoObjeto.INGREDIENTE;
                tipoEq = tipoEquipo.CONSUMIBLE;

                tipoIngles = tipoObjetoIngles.INGREDIENT;
                tipoEqIngles = tipoEquipoIngles.CONSUMABLE;

                indiceTipo = 16;
                valorCompra = 250;
                valorVenta = 125;
                break;
            case 104:
                nombre = "Hepática";
                descripcion = "Ingrediente usado para ciertos tipos de pociones.";

                nombreIngles = "Hepática";
                descripcionIngles = "Ingredient used for certain types of potions.";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Objetos/Equipamiento/roguelikeitems");
                imagen = imagenes[174];

                tipo = tipoObjeto.INGREDIENTE;
                tipoEq = tipoEquipo.CONSUMIBLE;

                tipoIngles = tipoObjetoIngles.INGREDIENT;
                tipoEqIngles = tipoEquipoIngles.CONSUMABLE;

                indiceTipo = 17;
                valorCompra = 250;
                valorVenta = 125;
                break;
            case 105:
                nombre = "Moneda Reto";
                descripcion = "Moneda empleada para retar a alguien a un duelo de Esgrima ancés.";

                nombreIngles = "Challenge Coin";
                descripcionIngles = "Coin used to challenge someone to an Ancés Fencing duel.";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Objetos/Equipamiento/rpgItems");
                imagen = imagenes[42];

                tipo = tipoObjeto.INGREDIENTE;
                tipoEq = tipoEquipo.CONSUMIBLE;

                tipoIngles = tipoObjetoIngles.INGREDIENT;
                tipoEqIngles = tipoEquipoIngles.CONSUMABLE;

                indiceTipo = 18;
                valorCompra = 250;
                valorVenta = 125;
                break;
            case 106:
                nombre = "Lágrima Plateada";
                descripcion = "Aumenta en 5 puntos todas las estadísticas.";

                nombreIngles = "Silver Tear";
                descripcionIngles = "Increase all your statistics by 5 points.";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Complementos/accessory_04");
                imagen = imagenes[0];

                tipo = tipoObjeto.EQUIPO;
                tipoEq = tipoEquipo.COMPLEMENTO;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.EQUIPMENT;
                tipoEqIngles = tipoEquipoIngles.COMPLEMENT;
                tipoRarezaIngles = rarezaIngles.COMMON;

                numeroDeMejoras = 6;
                aumentoDefensaFisica = aumentoVelocidad = aumentoVida = aumentoAtaqueMagico = aumentoAtaqueFisico = aumentoDefensaMagica = 5;
                aumentaDefensaF = aumentaAtaqueF = aumentaAtaqueM = aumentaDefensaM = aumentaVid = aumentaVel = true;

                indiceTipo = 11;
                valorCompra = 1200;
                valorVenta = 600;
                break;
            case 107:
                nombre = "Botas Básicas";
                descripcion = "Aumenta en 10 puntos la velocidad.";

                nombreIngles = "Basic Boots";
                descripcionIngles = "Increase speed by 10 points.";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Inventario/Botas/boot_02");
                imagen = imagenes[0];

                tipo = tipoObjeto.EQUIPO;
                tipoEq = tipoEquipo.BOTAS;
                tipoRareza = rareza.COMUN;

                tipoIngles = tipoObjetoIngles.EQUIPMENT;
                tipoEqIngles = tipoEquipoIngles.BOOTS;
                tipoRarezaIngles = rarezaIngles.COMMON;

                numeroDeMejoras = 1;
                aumentoVelocidad = 10;
                aumentaVel = true;

                indiceTipo = 12;
                valorCompra = 800;
                valorVenta = 400;
                break;
            case 108:
                nombre = "Mineral Común";
                descripcion = "Objeto apreciado por ciertos mercaderes.";

                nombreIngles = "Common Mineral";
                descripcionIngles = "Object appreciated by certain merchants.";

                imagenes = Resources.LoadAll<Sprite>("Sprites/Objetos/Equipamiento/roguelikeitems");
                imagen = imagenes[39];

                tipo = tipoObjeto.VENTA;
                tipoEq = tipoEquipo.CONSUMIBLE;

                tipoIngles = tipoObjetoIngles.SALE;
                tipoEqIngles = tipoEquipoIngles.CONSUMABLE;

                indiceTipo = 19;
                valorCompra = 0;
                valorVenta = 500;
                break;
            case 109:
                nombre = "Mineral Raro";
                descripcion = "Objeto valioso para bastantes mercaderes.";

                nombreIngles = "Rare Mineral";
                descripcionIngles = "Valuable object for many merchants.";


                imagenes = Resources.LoadAll<Sprite>("Sprites/Objetos/Equipamiento/roguelikeitems");
                imagen = imagenes[39];

                tipo = tipoObjeto.VENTA;
                tipoEq = tipoEquipo.CONSUMIBLE;

                tipoIngles = tipoObjetoIngles.SALE;
                tipoEqIngles = tipoEquipoIngles.CONSUMABLE;

                indiceTipo = 20;
                valorCompra = 0;
                valorVenta = 1000;
                break;
        }

        if (tipoEq == tipoEquipo.CABEZA)
        {
            imagenes = Resources.LoadAll<Sprite>("Sprites/Interfaz/Iconos Objetos/equipment_07");
            icono = imagenes[0];
        }
        else if (tipoEq == tipoEquipo.CUERPO)
        {
            imagenes = Resources.LoadAll<Sprite>("Sprites/Interfaz/Iconos Objetos/equipment_02");
            icono = imagenes[0];
        }
        else if (tipoEq == tipoEquipo.BOTAS)
        {
            imagenes = Resources.LoadAll<Sprite>("Sprites/Interfaz/Iconos Objetos/equipment_05");
            icono = imagenes[0];
        }
        else if (tipoEq == tipoEquipo.ARMA)
        {
            imagenes = Resources.LoadAll<Sprite>("Sprites/Interfaz/Iconos Objetos/equipment_11");
            icono = imagenes[0];
        }
        else if (tipoEq == tipoEquipo.ESCUDO)
        {
            imagenes = Resources.LoadAll<Sprite>("Sprites/Interfaz/Iconos Objetos/equipment_10");
            icono = imagenes[0];
        }
        else if (tipoEq == tipoEquipo.COMPLEMENTO)
        {
            imagenes = Resources.LoadAll<Sprite>("Sprites/Interfaz/Iconos Objetos/equipment_03");
            icono = imagenes[0];
        }
        else if (tipoEq == tipoEquipo.ATAQUE)
        {
            indiceTipo = indiceAtq;
            imagenes = Resources.LoadAll<Sprite>("Sprites/Interfaz/Iconos Estadisticas/VidaSocial");
            icono = imagenes[0];
        }
        else if (tipoEq == tipoEquipo.CLAVE)
        {
            imagenes = Resources.LoadAll<Sprite>("Sprites/Interfaz/Iconos Objetos/equipment_09");
            icono = imagenes[0];
        }
        else
        {
            imagenes = Resources.LoadAll<Sprite>("Sprites/Interfaz/Iconos Objetos/equipment_08");
            icono = imagenes[0];
        }

        return this;
    }

}
