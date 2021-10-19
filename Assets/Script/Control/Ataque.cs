using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ataque : System.Object {
    public string nombre;
    public string nombreIngles;
    [TextArea(10,5)]
    public string descripcion;
    [TextArea(10,5)]
    public string descripcionIngles;

    public enum tipoAtaque
    {
        FISICO,
        MAGICO,
        APOYO_POSITIVO,
        APOYO_NEGATIVO,
        APOYO_MIXTO
    }

    public enum tipoAtaqueIngles
    {
        PHYSICAL,
        MAGICAL,
        POSITIVE_SUPPORT,
        NEGATIVE_SUPPORT,
        MIX_SUPPORT
    }

    public enum elementoAtaque
    {
        FRIKI,
        FIESTERO,
        TIRANO,
        DORMILON,
        RESPONSABLE,
        NEUTRO,
        APOYO
    }

    public enum elementoAtaqueIngles
    {
        GEEK,
        PARTYMAN,
        TYRANT,
        SLEEPER,
        RESPONSIBLE,
        NEUTRAL,
        SUPPORT
    }

    public tipoAtaque tipo;
    public tipoAtaqueIngles tipoIngles;
    public elementoAtaque elemento;
    public elementoAtaqueIngles elementoIngles;

    public int energia, energiaActual;
    public int potencia;
    public int precision;

    public int numeroDeMejoras;
    public bool aumentaAtaqueF, aumentaAtaqueM, aumentaDefensaF, aumentaDefensaM, aumentaEva, aumentaVid, aumentaVel;
    public int mejoraAtaqueF, mejoraAtaqueM, mejoraDefensaF, mejoraDefensaM, mejoraEva, mejoraVid, mejoraVel;

    public bool provocaDolor, provocaNauseas, provocaMareo, provocaSueno, provocaAmnesia, provocaHemorragia;
    public int probabilidadDolor, probabilidadNauseas, probabilidadMareo, probabilidadSueno, probabilidadAmnesia, probabilidadHemorragia;
    bool dormido;

    public RuntimeAnimatorController animacionAtaque;



    public Ataque()
    {

    }



    public Ataque (Ataque referencia)
    {
        nombre = referencia.nombre;
        nombreIngles = referencia.nombreIngles;

        descripcion = referencia.descripcion;
        descripcionIngles = referencia.descripcionIngles;

        tipo = referencia.tipo;

        elemento = referencia.elemento;
        elementoIngles = referencia.elementoIngles;

        energia = referencia.energia;
        energiaActual = referencia.energiaActual;

        potencia = referencia.potencia;
        precision = referencia.precision;

        numeroDeMejoras = referencia.numeroDeMejoras;
        aumentaAtaqueF = referencia.aumentaAtaqueF;
        aumentaAtaqueM = referencia.aumentaAtaqueM;
        aumentaDefensaF = referencia.aumentaDefensaF;
        aumentaDefensaM = referencia.aumentaDefensaM;
        aumentaEva = referencia.aumentaEva;
        aumentaVid = referencia.aumentaVid;
        aumentaVel = referencia.aumentaVel;

        mejoraAtaqueF = referencia.mejoraAtaqueF;
        mejoraAtaqueM = referencia.mejoraAtaqueM;
        mejoraDefensaF = referencia.mejoraDefensaF;
        mejoraDefensaM = referencia.mejoraDefensaM;
        mejoraEva = referencia.mejoraEva;
        mejoraVid = referencia.mejoraVid;
        mejoraVel = referencia.mejoraVel;

        dormido = referencia.dormido;

        probabilidadDolor = referencia.probabilidadDolor;
        probabilidadNauseas = referencia.probabilidadNauseas;
        probabilidadMareo = referencia.probabilidadMareo;
        probabilidadSueno = referencia.probabilidadSueno;
        probabilidadAmnesia = referencia.probabilidadAmnesia;
        probabilidadHemorragia = referencia.probabilidadHemorragia;
    }



    public Ataque BuscaAtaque(int valor)
    {
        dormido = false;
        potencia = 0;
        numeroDeMejoras = 0;
        aumentaAtaqueF = aumentaAtaqueM = aumentaDefensaF = aumentaDefensaM = aumentaEva = aumentaVid = aumentaVel = false;
        mejoraAtaqueF = mejoraAtaqueM = mejoraDefensaF = mejoraDefensaM = mejoraEva = mejoraVid = mejoraVel = 0;
        probabilidadDolor = probabilidadNauseas = probabilidadMareo = probabilidadSueno = probabilidadAmnesia = probabilidadHemorragia = 0;

        //EL NOMBRE MÁXIMO 16 CARACTERES
        switch (valor)
        {
            case 0:
                nombre = "BOLA PAPEL";
                nombreIngles = "PAPER BALL";

                descripcion = "Lanzas una bola de papel con todas tus fuerzas";
                descripcionIngles = "You throw a paper ball with all your strength";

                potencia = 25;
                energia = 25;
                precision = 100;

                tipo = tipoAtaque.FISICO;
                tipoIngles = tipoAtaqueIngles.PHYSICAL;
                elemento = elementoAtaque.NEUTRO;

                break;
            case 1:
                nombre = "DESTELLO";
                nombreIngles = "FLASH";

                descripcion = "Generas una luz muy brillante que causa daño a la vista del objetivo";
                descripcionIngles = "You generate a very bright light that causes damage to the sight of the target";


                potencia = 20;
                energia = 25;
                precision = 100;

                tipo = tipoAtaque.MAGICO;
                tipoIngles = tipoAtaqueIngles.MAGICAL;
                elemento = elementoAtaque.NEUTRO;

                break;
            case 2:
                nombre = "PAPIROFLEXIA";
                nombreIngles = "ORIGAMI";

                descripcion = "Preparas armas de papel y se las lanzas al enemigo";
                descripcionIngles = "Prepare paper weapons and throw them at the enemy";

                potencia = 30;
                energia = 20;
                precision = 100;

                tipo = tipoAtaque.FISICO;
                tipoIngles = tipoAtaqueIngles.PHYSICAL;
                elemento = elementoAtaque.FRIKI;

                break;
            case 3:
                nombre = "PIROBOLA";
                nombreIngles = "FIREBALL";

                descripcion = "Lanzas una pequeña bola de fuego al enemigo";
                descripcionIngles = "You throw a small fireball at the enemy ";

                potencia = 30;
                energia = 25;
                precision = 100;

                tipo = tipoAtaque.MAGICO;
                tipoIngles = tipoAtaqueIngles.MAGICAL;
                elemento = elementoAtaque.FRIKI;

                break;
            case 4:
                nombre = "ALMOHADAZO";
                nombreIngles = "PILLOWBLOW";

                descripcion = "Golpeas fuertemente con una almohada";
                descripcionIngles = "You hit hard with a pillow ";

                potencia = 25;
                energia = 20;
                precision = 100;

                tipo = tipoAtaque.FISICO;
                tipoIngles = tipoAtaqueIngles.PHYSICAL;
                elemento = elementoAtaque.DORMILON;

                break;
            case 5:
                nombre = "OCASO";
                nombreIngles = "SUNSET";

                descripcion = "Lanzas un pequeño hechizo que causa daño mágico y puede llegar a provocar sueño en el objetivo";
                descripcionIngles = "You throw a small spell that causes magic damage and can make the target sleepy";

                potencia = 25;
                energia = 25;
                precision = 100;

                tipo = tipoAtaque.MAGICO;
                tipoIngles = tipoAtaqueIngles.MAGICAL;
                elemento = elementoAtaque.DORMILON;

                probabilidadSueno = 20;

                break;
            case 6:
                nombre = "DESCARO";
                nombreIngles = "NERVE";

                descripcion = "La falta de vergüenza hace que te lances a por el enemigo";
                descripcionIngles = "The lack of shame makes you throw yourself to the enemy";

                potencia = 30;
                energia = 25;
                precision = 100;

                tipo = tipoAtaque.FISICO;
                tipoIngles = tipoAtaqueIngles.PHYSICAL;
                elemento = elementoAtaque.FIESTERO;

                break;
            case 7:
                nombre = "SORPRESA";
                nombreIngles = "SURPRISE";

                descripcion = "Apareces de repente justo delante del objetivo asustándole lo cual le causa una pequeña cantidad de daño";
                descripcionIngles = "You suddenly appear right in front of the target, scaring him, which causes a small amount of damage";

                potencia = 20;
                energia = 20;
                precision = 100;

                tipo = tipoAtaque.MAGICO;
                tipoIngles = tipoAtaqueIngles.MAGICAL;
                elemento = elementoAtaque.FIESTERO;

                break;
            case 8:
                nombre = "ABSORBER";
                nombreIngles = "ABSORB";

                descripcion = "Absorbes la energía vital del objetivo y te añade un porcentaje a ti";
                descripcionIngles = "You absorb the vital energy of the objective and add a part to you";

                potencia = 30;
                energia = 25;
                precision = 100;

                tipo = tipoAtaque.MAGICO;
                tipoIngles = tipoAtaqueIngles.MAGICAL;
                elemento = elementoAtaque.TIRANO;

                numeroDeMejoras = 1;
                aumentaVid = true;
                mejoraVid = 15;

                break;
            case 9:
                nombre = "APRESAR";
                nombreIngles = "CAPTURE";

                descripcion = "Sujetas fuertemente al objetivo causándole daño por la presión ejercida";
                descripcionIngles = "Hold the target strongly causing damage from the pressure exerted";

                potencia = 30;
                energia = 20;
                precision = 100;

                tipo = tipoAtaque.FISICO;
                tipoIngles = tipoAtaqueIngles.PHYSICAL;
                elemento = elementoAtaque.TIRANO;

                break;
            case 10:
                nombre = "ANTICIPACIÓN";
                nombreIngles = "ANTICIPATION";

                descripcionIngles = "You prepare for combat and throw a precise blow";
                descripcion = "Te preparas para el combate y lanzas un golpe preciso";

                potencia = 35;
                energia = 25;
                precision = 100;

                tipo = tipoAtaque.FISICO;
                tipoIngles = tipoAtaqueIngles.PHYSICAL;
                elemento = elementoAtaque.RESPONSABLE;

                break;
            case 11:
                nombre = "LUCIDEZ";
                nombreIngles = "LUCIDITY";

                descripcionIngles = "Meditation gives you flashes of knowledge that is channeled into magic";
                descripcion = "La meditación te proporciona destellos de conocimiento que se canaliza en un hechizo";

                potencia = 30;
                energia = 20;
                precision = 100;

                tipo = tipoAtaque.MAGICO;
                tipoIngles = tipoAtaqueIngles.MAGICAL;
                elemento = elementoAtaque.RESPONSABLE;

                break;







            case 12:
                nombre = "BOTELLAZO";
                nombreIngles = "BOTTLEHIT";

                descripcionIngles = "You throw a glass bottle against the target. May cause target amnesia";
                descripcion = "Lanzas una botella de vidrio contra el objetivo. Puede provocar amnesia al objetivo";

                potencia = 40;
                energia = 15;
                precision = 95;

                tipo = tipoAtaque.FISICO;
                tipoIngles = tipoAtaqueIngles.PHYSICAL;
                elemento = elementoAtaque.FIESTERO;

                probabilidadAmnesia = 20;

                break;
            case 13:
                nombre = "LIGOTEO";
                nombreIngles = "FLIRT";

                descripcionIngles = "You try to make the target fall in love. This bothers him and causes mental damage. May cause target nausea";
                descripcion = "Tratas de ligar con el objetivo lo cual le incomoda y causa daño mental. Puede provocar nauseas al objetivo";

                potencia = 40;
                energia = 15;
                precision = 95;

                tipo = tipoAtaque.MAGICO;
                tipoIngles = tipoAtaqueIngles.MAGICAL;
                elemento = elementoAtaque.FIESTERO;

                probabilidadNauseas = 15;

                break;
            case 14:
                nombre = "BOFETÓN";
                nombreIngles = "PUNCH";

                descripcionIngles = "You hit hard with your open hand";
                descripcion = "Das un fuerte golpe con la mano abierta";

                potencia = 50;
                energia = 15;
                precision = 95;

                tipo = tipoAtaque.FISICO;
                tipoIngles = tipoAtaqueIngles.PHYSICAL;
                elemento = elementoAtaque.NEUTRO;

                break;
            case 15:
                nombre = "CONFUSIÓN";
                nombreIngles = "CONFUSION";

                descripcionIngles = "You cause the target to hear voices inside his head. This causes him to harm himself. It can cause headache";
                descripcion = "Provocas que el objetivo escuche voces dentro de él haciendo que se haga daño. Puede provocar dolor de cabeza";

                potencia = 50;
                energia = 15;
                precision = 90;

                tipo = tipoAtaque.MAGICO;
                tipoIngles = tipoAtaqueIngles.MAGICAL;
                elemento = elementoAtaque.NEUTRO;

                probabilidadDolor = 25;

                break;
            case 16:
                nombre = "RONQUIDO";
                nombreIngles = "SNORE";

                descripcionIngles = "You provoke a powerful noise that causes magical damage. You must be asleep to use it";
                descripcion = "Provocas un poderoso estruendo que causa daño mágico. Debes estar dormido para poder usarlo";

                potencia = 50;
                energia = 15;
                precision = 95;

                tipo = tipoAtaque.MAGICO;
                tipoIngles = tipoAtaqueIngles.MAGICAL;
                elemento = elementoAtaque.DORMILON;
                
                dormido = true;

                break;
            case 17:
                nombre = "SONAMBULISMO";
                nombreIngles = "SOMNAMBULISM";

                descripcionIngles = "You unconsciously throw several punches. You must be asleep for it";
                descripcion = "Lanzas inconscientemente una serie de golpes. Debes estar dormido para ello";

                potencia = 60;
                energia = 15;
                precision = 90;

                tipo = tipoAtaque.FISICO;
                tipoIngles = tipoAtaqueIngles.PHYSICAL;
                elemento = elementoAtaque.DORMILON;
                
                dormido = true;

                break;
            case 18:
                nombre = "PARASITAR";
                nombreIngles = "PARASITE";

                descripcionIngles = "It infects the enemy which causes strong wounds";
                descripcion = "Infectas al enemigo lo que le provoca fuertes heridas";

                potencia = 55;
                energia = 15;
                precision = 90;

                tipo = tipoAtaque.FISICO;
                tipoIngles = tipoAtaqueIngles.PHYSICAL;
                elemento = elementoAtaque.TIRANO;

                break;
            case 19:
                nombre = "MANIPULACIÓN";
                descripcion = "Controlas al objetivo para que se autoinfligirse daño";

                nombreIngles = "MANIPULATION";
                descripcionIngles = "You control the target to self-inflict damage";

                potencia = 50;
                energia = 15;
                precision = 95;

                tipo = tipoAtaque.MAGICO;
                tipoIngles = tipoAtaqueIngles.MAGICAL;
                elemento = elementoAtaque.TIRANO;

                break;
            case 20:
                nombre = "TIRACHINAS";
                descripcion = "Lanzas piedras al enemigo con tu arma de bolsillo";

                nombreIngles = "SLINGSHOT";
                descripcionIngles = "Throw stones at the enemy with your pocket weapon";

                potencia = 45;
                energia = 15;
                precision = 95;

                tipo = tipoAtaque.FISICO;
                tipoIngles = tipoAtaqueIngles.PHYSICAL;
                elemento = elementoAtaque.FRIKI;

                break;
            case 21:
                nombre = "CALAMBRE";
                descripcion = "Provocas una gran descarga eléctrica sobre el enemigo";

                nombreIngles = "SHOCK";
                descripcionIngles = "You cause a great electric shock on the enemy";

                potencia = 50;
                energia = 15;
                precision = 90;

                tipo = tipoAtaque.MAGICO;
                tipoIngles = tipoAtaqueIngles.MAGICAL;
                elemento = elementoAtaque.FRIKI;

                break;
            case 22:
                nombre = "JUICIO";
                descripcion = "Calculas las opciones más probables para causar buenos golpes físicos";

                nombreIngles = "JUDGMENT";
                descripcionIngles = "You calculate the most likely options to cause good physical bumps";

                potencia = 50;
                energia = 15;
                precision = 95;

                tipo = tipoAtaque.FISICO;
                tipoIngles = tipoAtaqueIngles.PHYSICAL;
                elemento = elementoAtaque.RESPONSABLE;

                break;
            case 23:
                nombre = "ANÁLISIS";
                descripcion = "Obtienes el poder de detectar los puntos débiles del enemigo y los atacas";

                nombreIngles = "ANALYSIS";
                descripcionIngles = "You get the power to detect the enemy's weak points and attack them";

                potencia = 50;
                energia = 15;
                precision = 90;

                tipo = tipoAtaque.MAGICO;
                tipoIngles = tipoAtaqueIngles.MAGICAL;
                elemento = elementoAtaque.RESPONSABLE;

                break;








            case 24:
                nombre = "SUEÑO LÚCIDO";
                descripcion = "Eres consciente de tu estado durmiente lo que te permite lanzar un poderoso hechizo. Debes estar dormido";

                nombreIngles = "LUCID DREAM";
                descripcionIngles = "You are aware of your sleeping state which allows you to cast a powerful spell. You must be asleep";

                potencia = 90;
                energia = 10;
                precision = 85;

                tipo = tipoAtaque.MAGICO;
                tipoIngles = tipoAtaqueIngles.MAGICAL;
                elemento = elementoAtaque.DORMILON;
                
                dormido = true;

                break;
            case 25:
                nombre = "PESADILLA";
                descripcion = "Tienes un mal sueño que provoca que lances un fuerte ataque. Debes estar dormido";

                nombreIngles = "NIGHTMARE";
                descripcionIngles = "You have a bad dream that causes you to throw a strong attack. You must be asleep";

                potencia = 85;
                energia = 10;
                precision = 85;

                tipo = tipoAtaque.FISICO;
                tipoIngles = tipoAtaqueIngles.PHYSICAL;
                elemento = elementoAtaque.DORMILON;
                
                dormido = true;

                break;

            case 26:
                nombre = "PALABRAS RARAS";
                descripcion = "Utiliza un hechizo por error al pronunciar unas palabras raras";

                nombreIngles = "RARE WORDS";
                descripcionIngles = "Use a spell by mistake when pronouncing some strange words";

                potencia = 85;
                energia = 10;
                precision = 80;

                tipo = tipoAtaque.MAGICO;
                tipoIngles = tipoAtaqueIngles.MAGICAL;
                elemento = elementoAtaque.FIESTERO;

                break;
            case 27:
                nombre = "EUFORIA";
                descripcion = "Entras en un estado de éxtasis que libera tu cuerpo permitiéndote realizar ataques muy potentes";

                nombreIngles = "EUPHORIA";
                descripcionIngles = "You enter a state of ecstasy that releases your body allowing you to make very powerful attacks";

                potencia = 80;
                energia = 10;
                precision = 85;

                tipo = tipoAtaque.FISICO;
                tipoIngles = tipoAtaqueIngles.PHYSICAL;
                elemento = elementoAtaque.FIESTERO;

                break;
            case 28:
                nombre = "RODILLAZO";
                descripcion = "Propinas un potente golpe con la rodilla";

                nombreIngles = "KNEEHIT";
                descripcionIngles = "You throw a powerful blow with the knee";

                potencia = 85;
                energia = 10;
                precision = 85;

                tipo = tipoAtaque.FISICO;
                tipoIngles = tipoAtaqueIngles.PHYSICAL;
                elemento = elementoAtaque.NEUTRO;

                break;
            case 29:
                nombre = "ANSIEDAD";
                descripcion = "Causas un gran malestar que oprime el pecho del enemigo";

                nombreIngles = "ANXIETY";
                descripcionIngles = "You cause a great malaise that oppresses the enemy's chest";

                potencia = 80;
                energia = 10;
                precision = 85;

                tipo = tipoAtaque.MAGICO;
                tipoIngles = tipoAtaqueIngles.MAGICAL;
                elemento = elementoAtaque.NEUTRO;

                break;
            case 30:
                nombre = "SOMBRA";
                descripcion = "Golpeas fuertemente desde las sombras lo que dificulta la esquiva al rival";
                
                nombreIngles = "SHADOW";
                descripcionIngles = "You hit hard from the shadows making it difficult to dodge the opponent";

                potencia = 85;
                energia = 10;
                precision = 100;

                tipo = tipoAtaque.FISICO;
                tipoIngles = tipoAtaqueIngles.PHYSICAL;
                elemento = elementoAtaque.FRIKI;

                break;
            case 31:
                nombre = "PODER ARCANO";
                descripcion = "Usas un antiguo y poderoso hechizo para dañar a tu enemigo";

                nombreIngles = "ARCANE POWER";
                descripcionIngles = "You use an ancient and powerful spell to harm your enemy";

                potencia = 85;
                energia = 10;
                precision = 85;

                tipo = tipoAtaque.MAGICO;
                tipoIngles = tipoAtaqueIngles.MAGICAL;
                elemento = elementoAtaque.FRIKI;

                break;
            case 32:
                nombre = "DESGARRAR";
                descripcion = "Poderoso corte que provoca profundas heridas en el rival. Puede provocar hemorragia";

                nombreIngles = "TEAR";
                descripcionIngles = "Powerful cut that causes deep wounds in the opponent. It can cause bleeding";

                potencia = 80;
                energia = 10;
                precision = 80;

                tipo = tipoAtaque.FISICO;
                tipoIngles = tipoAtaqueIngles.PHYSICAL;
                elemento = elementoAtaque.TIRANO;

                break;
            case 33:
                nombre = "MALDICIÓN";
                descripcion = "Lanzas un hechizo que maldice al objetivo y le causa un gran sufrimiento";

                nombreIngles = "CURSE";
                descripcionIngles = "You cast a spell that curses the target and causes him great suffering";

                potencia = 90;
                energia = 10;
                precision = 85;

                tipo = tipoAtaque.MAGICO;
                tipoIngles = tipoAtaqueIngles.MAGICAL;
                elemento = elementoAtaque.TIRANO;

                break;
            case 34:
                nombre = "GRAVEDAD";
                descripcion = "Empleas tus conocimientos de física para aumentar la gravedad sobre un enemigo";

                nombreIngles = "GRAVITY";
                descripcionIngles = "You use your knowledge of physics to increase gravity over an enemy";

                potencia = 85;
                energia = 10;
                precision = 85;

                tipo = tipoAtaque.MAGICO;
                tipoIngles = tipoAtaqueIngles.MAGICAL;
                elemento = elementoAtaque.RESPONSABLE;

                break;
            case 35:
                nombre = "EXPLOSIÓN";
                descripcion = "Utilizas tus conocimientos químicos para crear un pequeño explosivo casero";

                nombreIngles = "EXPLOSION";
                descripcionIngles = "You use your chemical knowledge to create a small homemade explosive";

                potencia = 80;
                energia = 10;
                precision = 85;

                tipo = tipoAtaque.FISICO;
                tipoIngles = tipoAtaqueIngles.PHYSICAL;
                elemento = elementoAtaque.RESPONSABLE;

                break;






            case 36:
                nombre = "CORTAESTRELLAS";
                descripcion = "Realizas un enorme corte que hace daño masivo";

                nombreIngles = "STARKILLER";
                descripcionIngles = "You make a huge cut that does massive damage";

                potencia = 125;
                energia = 5;
                precision = 75;

                tipo = tipoAtaque.FISICO;
                tipoIngles = tipoAtaqueIngles.PHYSICAL;
                elemento = elementoAtaque.FRIKI;

                break;
            case 37:
                nombre = "ALMA DRAGÓN";
                descripcion = "Invocas el poder del dragón para lanzar una poderosa llamarada";

                nombre = "DRAGON SOUL";
                descripcion = "You invoke the dragon's power to launch a powerful flare";

                potencia = 115;
                energia = 5;
                precision = 80;

                tipo = tipoAtaque.MAGICO;
                tipoIngles = tipoAtaqueIngles.MAGICAL;
                elemento = elementoAtaque.FRIKI;

                break;
            case 38:
                nombre = "INSOMNIO";
                descripcion = "El no poder dormir provoca una furia incontrolable en ti que empleas en contra del enemigo";

                nombreIngles = "INSOMNIA";
                descripcionIngles = "Not being able to sleep causes an uncontrollable fury that you use against the enemy";

                potencia = 120;
                energia = 5;
                precision = 80;

                tipo = tipoAtaque.FISICO;
                tipoIngles = tipoAtaqueIngles.PHYSICAL;
                elemento = elementoAtaque.DORMILON;

                break;
            case 39:
                nombre = "SUEÑO ETERNO";
                descripcion = "Arrastras al enemigo al reino de los sueños provocándole dolor por las terribles pesadillas que padece";

                nombreIngles = "ETERNAL DREAM";
                descripcionIngles = "Drag the enemy to the kingdom of dreams causing pain for the terrible nightmares he suffers";

                potencia = 140;
                energia = 5;
                precision = 75;

                tipo = tipoAtaque.MAGICO;
                tipoIngles = tipoAtaqueIngles.MAGICAL;
                elemento = elementoAtaque.DORMILON;

                break;
            case 40:
                nombre = "DANZA MÍSTICA";
                descripcion = "Realizas un baile que obliga a tu enemigo a bailar sin parar causándole una gran fatiga";

                nombreIngles = "MYSTIC DANCE";
                descripcionIngles = "You perform a dance that forces your enemy to dance without stopping causing great fatigue";

                potencia = 110;
                energia = 5;
                precision = 80;

                tipo = tipoAtaque.FISICO;
                tipoIngles = tipoAtaqueIngles.PHYSICAL;
                elemento = elementoAtaque.FIESTERO;

                break;
            case 41:
                nombre = "CANTO";
                descripcion = "Produces un terrible sonido que hace que el objetivo sienta que su cabeza va a explotar";

                nombreIngles = "SING";
                descripcionIngles = "You produce a terrible sound that makes the target feel that its head is going to explode";

                potencia = 120;
                energia = 5;
                precision = 75;

                tipo = tipoAtaque.MAGICO;
                tipoIngles = tipoAtaqueIngles.MAGICAL;
                elemento = elementoAtaque.FIESTERO;

                break;
            case 42:
                nombre = "CALAMIDAD";
                descripcion = "Provoca un gran cataclismo natural que daña gravemente al rival";

                nombreIngles = "CALAMITY";
                descripcionIngles = "Provoca un gran cataclismo natural que daña gravemente al rival";

                potencia = 140;
                energia = 5;
                precision = 70;

                tipo = tipoAtaque.FISICO;
                tipoIngles = tipoAtaqueIngles.PHYSICAL;
                elemento = elementoAtaque.TIRANO;

                break;
            case 43:
                nombre = "SACRIFICIO";
                descripcion = "Te haces daño para invocar un espíritu que causa enorme daño mágico";

                nombreIngles = "SACRIFICE";
                descripcionIngles = "You hurt yourself to invoke a spirit that causes enormous magic damage";

                potencia = 150;
                energia = 5;
                precision = 70;

                tipo = tipoAtaque.MAGICO;
                tipoIngles = tipoAtaqueIngles.MAGICAL;
                elemento = elementoAtaque.TIRANO;

                numeroDeMejoras = 1;
                aumentaVid = true;
                mejoraVid = -15;

                break;
            case 44:
                nombre = "JURAMENTO";
                descripcion = "Eres fiel a tus ideales y luchas con todas tus fuerzas para defenderlos";

                nombreIngles = "OATH";
                descripcionIngles = "You are faithful to your ideals and fight with all your strength to defend them";

                potencia = 120;
                energia = 5;
                precision = 80;

                tipo = tipoAtaque.FISICO;
                tipoIngles = tipoAtaqueIngles.PHYSICAL;
                elemento = elementoAtaque.RESPONSABLE;

                break;
            case 45:
                nombre = "FORMULACIÓN";
                descripcion = "Usas tus conocimientos matemáticos para formular un hechizo enormemente destructivo";

                nombreIngles = "FORMULATION";
                descripcionIngles = "You use your mathematical knowledge to formulate a hugely destructive spell";

                potencia = 145;
                energia = 5;
                precision = 70;

                tipo = tipoAtaque.MAGICO;
                tipoIngles = tipoAtaqueIngles.MAGICAL;
                elemento = elementoAtaque.RESPONSABLE;

                break;
            case 46:
                nombre = "ESTRÉS";
                descripcion = "Empleas la rabia y la presión acumuladas para lanzar un golpe enormemente poderoso";

                nombreIngles = "STRESS";
                descripcionIngles = "You use the rage and pressure accumulated to launch a hugely powerful hit";

                potencia = 140;
                energia = 5;
                precision = 75;

                tipo = tipoAtaque.FISICO;
                tipoIngles = tipoAtaqueIngles.PHYSICAL;
                elemento = elementoAtaque.NEUTRO;

                break;
            case 47:
                nombre = "INVOCACIÓN";
                descripcion = "Llamas al espíritu de un antiguo guerrero que lucha en tu lugar";

                nombreIngles = "INVOCATION";
                descripcionIngles = "You call the spirit of an ancient warrior who fights in your place";

                potencia = 120;
                energia = 5;
                precision = 80;

                tipo = tipoAtaque.MAGICO;
                tipoIngles = tipoAtaqueIngles.MAGICAL;
                elemento = elementoAtaque.NEUTRO;

                break;







            case 48:
                nombre = "MOTIVACION";
                descripcion = "Te repites el mismo mantra una y otra vez para mejorar tu Ataque y Defensa Físicos";

                nombreIngles = "MOTIVATION";
                descripcionIngles = "You repeat the same mantra over and over again to improve your Physical Attack and Defence";

                energia = 25;
                precision = 100;

                tipo = tipoAtaque.APOYO_POSITIVO;
                tipoIngles = tipoAtaqueIngles.POSITIVE_SUPPORT;
                elemento = elementoAtaque.APOYO;

                numeroDeMejoras = 2;
                aumentaAtaqueF = true;
                aumentaDefensaF = true;
                mejoraAtaqueF = mejoraAtaqueF = 1;
                
                break;
            case 49:
                nombre = "PARRAFADA";
                descripcion = "Sueltas un discurso que agobia al rival y le baja tanto la Defensa Física como Mágica";

                nombreIngles = "SPIEL";
                descripcionIngles = "You release a speech that overwhelms the opponent and lowers both Physical and Magic Defence";

                energia = 25;
                precision = 100;

                tipo = tipoAtaque.APOYO_NEGATIVO;
                tipoIngles = tipoAtaqueIngles.NEGATIVE_SUPPORT;
                elemento = elementoAtaque.APOYO;

                numeroDeMejoras = 2;
                aumentaDefensaF = aumentaDefensaM = true;
                mejoraDefensaF = mejoraDefensaM = -1;
                
                break;
            case 50:
                nombre = "DESCANSO";
                descripcion = "Te echas una siesta para recuperar fuerzas";

                nombreIngles = "BREAK";
                descripcionIngles = "You take a nap to regain strength";

                energia = 15;
                precision = 100;

                tipo = tipoAtaque.APOYO_POSITIVO;
                tipoIngles = tipoAtaqueIngles.POSITIVE_SUPPORT;
                elemento = elementoAtaque.APOYO;

                numeroDeMejoras = 1;
                aumentaVid = true;
                mejoraVid = 50;
                
                break;
            case 51:
                nombre = "BEBER";
                descripcion = "Aumentas tu ataque mágico y defensa mágica mucho, pero reduce la velocidad y la evasión";

                nombreIngles = "DRINK";
                descripcionIngles = "You increase your magic attack and magic defence a lot, but reduce speed and evasion";

                energia = 20;
                precision = 100;

                tipo = tipoAtaque.APOYO_MIXTO;
                tipoIngles = tipoAtaqueIngles.MIX_SUPPORT;
                elemento = elementoAtaque.APOYO;

                numeroDeMejoras = 4;
                aumentaAtaqueM = aumentaDefensaM = aumentaEva = aumentaEva = true;
                mejoraAtaqueM = mejoraDefensaM = 2;
                mejoraEva = mejoraVel = -1;
                
                break;
            case 52:
                nombre = "CHISMORREO";
                descripcion = "Sueltas un bulo que baja la Defensa Física del enemigo";

                nombreIngles = "GOSSIP";
                descripcionIngles = "You release a hoax that lowers the Physical Defence of the enemy";

                energia = 30;
                precision = 100;

                tipo = tipoAtaque.APOYO_NEGATIVO;
                tipoIngles = tipoAtaqueIngles.NEGATIVE_SUPPORT;
                elemento = elementoAtaque.APOYO;

                numeroDeMejoras = 1;
                aumentaDefensaF = true;
                mejoraDefensaF = -1;
                
                break;
            case 53:
                nombre = "ENTUSIASMO";
                descripcion = "Entras en un estado de euforia que aumenta mucho tu Ataque Físico";

                nombreIngles = "ENTHUSIASM";
                descripcionIngles = "You enter a state of euphoria that greatly increases your Physical Attack";

                energia = 30;
                precision = 100;

                tipo = tipoAtaque.APOYO_POSITIVO;
                tipoIngles = tipoAtaqueIngles.POSITIVE_SUPPORT;
                elemento = elementoAtaque.APOYO;

                numeroDeMejoras = 1;
                aumentaAtaqueF = true;
                mejoraAtaqueF = 1;
                
                break;
            case 54:
                nombre = "BRONCA";
                descripcion = "Lanzas una enorme regañina que reduce la Velocidad y la Evasión del enemigo";

                nombreIngles = "ANGER";
                descripcionIngles = "You throw a huge scolding that reduces the Speed and Evasion of the enemy";

                energia = 25;
                precision = 100;

                tipo = tipoAtaque.APOYO_NEGATIVO;
                tipoIngles = tipoAtaqueIngles.NEGATIVE_SUPPORT;
                elemento = elementoAtaque.APOYO;

                numeroDeMejoras = 2;
                aumentaVel = aumentaEva = true;
                mejoraEva = mejoraVel = 1;
                
                break;
            case 55:
                nombre = "COSQUILLAS";
                descripcion = "Haces cosquillas al enemigo lo que baja su Defensa Mágica";

                nombreIngles = "TICKLE";
                descripcionIngles = "You tickle the enemy which lowers his Magic Defence";

                energia = 30;
                precision = 100;

                tipo = tipoAtaque.APOYO_NEGATIVO;
                tipoIngles = tipoAtaqueIngles.NEGATIVE_SUPPORT;
                elemento = elementoAtaque.APOYO;

                numeroDeMejoras = 1;
                aumentaDefensaM = true;
                mejoraDefensaM = -1;
                
                break;
            case 56:
                nombre = "IGNORAR";
                descripcion = "Pasas de cualquier cosa que dice el enemigo. Aumenta mucho la Evasión, pero reduce la Defensa Física";

                nombreIngles = "IGNORE";
                descripcionIngles = "You ignore the enemy. It greatly increases Evasion, but reduces Physical Defence";

                energia = 30;
                precision = 100;

                tipo = tipoAtaque.APOYO_MIXTO;
                tipoIngles = tipoAtaqueIngles.MIX_SUPPORT;
                elemento = elementoAtaque.APOYO;

                numeroDeMejoras = 2;
                aumentaDefensaF = aumentaEva = true;
                mejoraDefensaF = -1;
                mejoraEva = 2;
                
                break;
            case 57:
                nombre = "SOBRECARGAR";
                descripcion = "Provocas un fuerte dolor de cabeza al objetivo";

                nombreIngles = "OVERLOAD";
                descripcionIngles = "You cause a strong headache to the target";

                energia = 15;
                precision = 100;

                tipo = tipoAtaque.APOYO_NEGATIVO;
                tipoIngles = tipoAtaqueIngles.NEGATIVE_SUPPORT;
                elemento = elementoAtaque.APOYO;

                probabilidadDolor = 100;

                break;
            case 58:
                nombre = "GIRO RÁPIDO";
                descripcion = "Haces girar muy rápido al enemigo lo que le provoca un fuerte mareo";

                nombreIngles = "FAST SPIN";
                descripcionIngles = "You make the enemy spin very fast which causes a strong dizziness";

                potencia = 0;
                energia = 15;
                energiaActual = energia;
                precision = 100;

                tipo = tipoAtaque.APOYO_NEGATIVO;
                tipoIngles = tipoAtaqueIngles.NEGATIVE_SUPPORT;
                elemento = elementoAtaque.APOYO;

                probabilidadMareo = 100;

                break;
            case 59:
                nombre = "PESTILENCIA";
                descripcion = "Provocas nauseas al enemigo a causa del mal olor que produces";

                nombreIngles = "PESTILENCE";
                descripcionIngles = "You cause nausea to the enemy because of the bad smell you produce";

                energia = 15;
                precision = 100;

                tipo = tipoAtaque.APOYO_NEGATIVO;
                tipoIngles = tipoAtaqueIngles.NEGATIVE_SUPPORT;
                elemento = elementoAtaque.APOYO;

                probabilidadNauseas = 100;

                break;
            case 60:
                nombre = "HIPNOSIS";
                descripcion = "Duermes al enemigo con tus poderes psíquicos";

                nombreIngles = "HIPNOSIS";
                descripcionIngles = "Duermes al enemigo con tus poderes psíquicos";

                energia = 15;
                precision = 100;

                tipo = tipoAtaque.APOYO_NEGATIVO;
                tipoIngles = tipoAtaqueIngles.NEGATIVE_SUPPORT;
                elemento = elementoAtaque.APOYO;

                probabilidadSueno = 100;

                break;
            case 61:
                nombre = "CHACHARA";

                descripcion = "Provocas un estado de amnesia en el objetivo que le hace olvidar un ataque al azar";

                energia = 15;
                precision = 100;

                tipo = tipoAtaque.APOYO_NEGATIVO;
                tipoIngles = tipoAtaqueIngles.NEGATIVE_SUPPORT;
                elemento = elementoAtaque.APOYO;

                probabilidadAmnesia = 100;

                break;
            case 62:
                nombre = "CORTE";

                descripcion = "Provocas una fuerte hemorragia al objetivo";

                energia = 15;
                precision = 100;

                tipo = tipoAtaque.APOYO_NEGATIVO;
                tipoIngles = tipoAtaqueIngles.NEGATIVE_SUPPORT;
                elemento = elementoAtaque.APOYO;

                probabilidadHemorragia = 0;

                break;
            case 63:
                nombre = "HUMILLACION";
                nombreIngles = "HUMILIATION ";

                descripcion = "Dejas en ridículo al objetivo bajando su Defensa Física y Mágica, pero aumentando su Ataque Físico";
                descripcionIngles = "You ridicule the target by lowering his Physical and Magical Defense, but increasing his Physical Attack";

                energia = 25;
                precision = 100;

                tipo = tipoAtaque.APOYO_MIXTO;
                tipoIngles = tipoAtaqueIngles.MIX_SUPPORT;
                elemento = elementoAtaque.APOYO;

                numeroDeMejoras = 3;
                aumentaAtaqueF = aumentaDefensaF = aumentaDefensaM = true;
                mejoraAtaqueF = 1;
                mejoraDefensaF = mejoraDefensaM = -1;

                break;
            case 64:
                nombre = "FORTALECER";
                nombreIngles = "STRENGTHEN ";

                descripcion = "Preparas tu cuerpo para aumentar tu Defensa Física y Mágica";
                descripcionIngles = "You prepare your body to increase your Physical and Magical Defense";

                energia = 25;
                precision = 100;

                tipo = tipoAtaque.APOYO_POSITIVO;
                tipoIngles = tipoAtaqueIngles.POSITIVE_SUPPORT;
                elemento = elementoAtaque.APOYO;

                numeroDeMejoras = 2;
                aumentaDefensaF = aumentaDefensaM = true;
                mejoraDefensaF = mejoraDefensaM = 1;

                break;
            case 65:
                nombre = "AUTOCONTROL";
                nombreIngles = "SELF-CONTROL";

                descripcion = "El conocimiento de tu propia fuerza te ayuda a mejorar tu Ataque y Defensa mágicas";
                descripcionIngles = "Knowledge of your own strength helps you improve your Magical Attack and Defense";

                energia = 25;
                precision = 100;

                tipo = tipoAtaque.APOYO_POSITIVO;
                tipoIngles = tipoAtaqueIngles.POSITIVE_SUPPORT;
                elemento = elementoAtaque.APOYO;

                numeroDeMejoras = 2;
                aumentaAtaqueM = aumentaDefensaM = true;
                mejoraAtaqueM = 2;
                mejoraDefensaM = 1;

                break;
            case 66:
                nombre = "CALENTAMIENTO";
                nombreIngles = "WARM-UP";

                descripcion = "Te preparas físicamente para el combate aumentando tu Velocidad y Evasión";
                descripcionIngles = "You prepare physically for combat by increasing your Speed and Evasion";

                energia = 25;
                precision = 100;

                tipo = tipoAtaque.APOYO_POSITIVO;
                tipoIngles = tipoAtaqueIngles.POSITIVE_SUPPORT;
                elemento = elementoAtaque.APOYO;

                numeroDeMejoras = 2;
                aumentaEva = aumentaVel = true;
                mejoraEva = mejoraVel = 1;

                break;
            case 67:
                nombre = "BROMA";
                nombreIngles = "JOKE";

                descripcion = "Esta burla hace que el objetivo se sienta ofendido lo cual baja su Defensa Física y su Evasión";
                descripcionIngles = "This mockery makes the target feel offended which lowers his Physical Defense and Evasion";

                energia = 25;
                precision = 100;

                tipo = tipoAtaque.APOYO_NEGATIVO;
                tipoIngles = tipoAtaqueIngles.NEGATIVE_SUPPORT;
                elemento = elementoAtaque.APOYO;

                numeroDeMejoras = 2;
                aumentaDefensaF = aumentaEva = true;
                mejoraDefensaF = mejoraEva = -1;

                break;
            case 68:
                nombre = "INDIRECTA";
                nombreIngles = "HINT";

                descripcion = "Sueltas puyas a tu objetivo lo cual baja su ataque y defensa física";
                descripcionIngles = "You release attacks on your target which lowers his Physical Attack and Defense";

                energia = 25;
                precision = 100;

                tipo = tipoAtaque.APOYO_NEGATIVO;
                tipoIngles = tipoAtaqueIngles.NEGATIVE_SUPPORT;
                elemento = elementoAtaque.APOYO;

                numeroDeMejoras = 2;
                aumentaAtaqueF = aumentaDefensaF = true;
                mejoraAtaqueF = mejoraDefensaF = -1;

                break;
            case 69:
                nombre = "MANTRA";
                nombreIngles = "MANTRA";

                descripcion = "Aumenta un poco tu defensa física y mucho tu defensa mágica, pero reduce la velocidad";
                descripcionIngles = descripcion = "Increase your Physical Defense a little and your Magical Defense a lot, but make you slower";

                energia = 20;
                precision = 100;

                tipo = tipoAtaque.APOYO_MIXTO;
                tipoIngles = tipoAtaqueIngles.MIX_SUPPORT;
                elemento = elementoAtaque.APOYO;

                numeroDeMejoras = 3;
                aumentaDefensaF = aumentaDefensaM = aumentaVel = true;
                mejoraDefensaF = 1;
                mejoraDefensaM = 2;
                mejoraVel = -1;

                break;
            case 70:
                nombre = "DESCARGA";
                nombreIngles = "DOWNLOAD";

                descripcion = "Aumenta tu velocidad y evasión mucho, pero reduce tu ataque físico y mágico";
                descripcionIngles = "Increase your speed and evasion a lot, but reduce your Physical and Magical attack";

                energia = 20;
                precision = 100;

                tipo = tipoAtaque.APOYO_MIXTO;
                tipoIngles = tipoAtaqueIngles.MIX_SUPPORT;
                elemento = elementoAtaque.APOYO;

                numeroDeMejoras = 4;
                aumentaAtaqueF = aumentaAtaqueM = aumentaEva = aumentaVel = true;
                mejoraAtaqueF = mejoraAtaqueM = -1;
                mejoraEva = mejoraVel = 2;

                break;
            case 71:
                nombre = "CARGA";
                nombreIngles = "LOAD";
                
                descripcion = "Aumenta tu ataque físico y mágico además de tu velocidad, reduce tu vida";
                descripcionIngles = "Increase your Physical and Magical Attack and your speed, reduce your life";

                energia = 15;
                precision = 100;

                tipo = tipoAtaque.APOYO_MIXTO;
                tipoIngles = tipoAtaqueIngles.MIX_SUPPORT;
                elemento = elementoAtaque.APOYO;

                numeroDeMejoras = 4;
                aumentaAtaqueF = aumentaAtaqueM = aumentaVid = aumentaVel = true;
                mejoraAtaqueF = mejoraAtaqueM = mejoraVel = 1;
                mejoraVid = -15;

                break;

        }

        energiaActual = energia;

        if (tipo == tipoAtaque.APOYO_MIXTO)
        {
            tipoIngles = tipoAtaqueIngles.MIX_SUPPORT;
        }
        else if (tipo == tipoAtaque.APOYO_NEGATIVO)
        {
            tipoIngles = tipoAtaqueIngles.NEGATIVE_SUPPORT;
        }
        else if (tipo == tipoAtaque.APOYO_POSITIVO)
        {
            tipoIngles = tipoAtaqueIngles.POSITIVE_SUPPORT;
        }
        else if (tipo == tipoAtaque.FISICO)
        {
            tipoIngles = tipoAtaqueIngles.PHYSICAL;
        }
        else
        {
            tipoIngles = tipoAtaqueIngles.MAGICAL;
        }

        if(elemento == elementoAtaque.DORMILON)
        {
            elementoIngles = elementoAtaqueIngles.SLEEPER;
        }
        else if (elemento == elementoAtaque.FIESTERO)
        {
            elementoIngles = elementoAtaqueIngles.PARTYMAN;
        }
        else if (elemento == elementoAtaque.FRIKI)
        {
            elementoIngles = elementoAtaqueIngles.GEEK;
        }
        else if (elemento == elementoAtaque.NEUTRO)
        {
            elementoIngles = elementoAtaqueIngles.NEUTRAL;
        }
        else if (elemento == elementoAtaque.RESPONSABLE)
        {
            elementoIngles = elementoAtaqueIngles.RESPONSIBLE;
        }
        else if (elemento == elementoAtaque.TIRANO)
        {
            elementoIngles = elementoAtaqueIngles.TYRANT;
        }
        else
        {
            elementoIngles = elementoAtaqueIngles.SUPPORT;
        }

        return this; 
    }
}
