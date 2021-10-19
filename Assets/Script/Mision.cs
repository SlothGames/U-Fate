using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mision : System.Object
{
    public enum tipoMision
    {
        RECLUTAMIENTO,
        PRINCIPAL,
        SECUNDARIA
    }

    public enum tipoMisionIngles
    {
        RECRUITMENT,
        PRINCIPAL,
        SECONDARY
    }

    public string titulo;
    public string tituloIngles;

    public int indice;

    [TextArea(10, 5)]
    public string descripcion;
    [TextArea(10, 5)]
    public string descripcionIngles;

    public tipoMision tipoDeMision;
    public tipoMisionIngles tipoDeMisionIngles;

    public string[] recompensas;
    public string[] recompensasIngles;
    public string[] estadoActual;
    public string[] estadoActualIngles;
    public bool completada = false;
    public string origen;
    public string origenIngles;

    public Mision() {}



    public Mision(Mision referencia)
    {
        titulo = referencia.titulo;
        tituloIngles = referencia.tituloIngles;

        indice = referencia.indice;

        descripcion = referencia.descripcion;
        descripcionIngles = referencia.descripcionIngles;

        tipoDeMision = referencia.tipoDeMision;
        tipoDeMisionIngles = referencia.tipoDeMisionIngles;

        recompensas = new string[referencia.recompensas.Length];
        for (int i = 0; i < referencia.recompensas.Length; i++)
        {
            recompensas[i] = referencia.recompensas[i];
        }

        estadoActual = new string[referencia.estadoActual.Length];
        for (int i = 0; i < referencia.estadoActual.Length; i++)
        {
            estadoActual[i] = referencia.estadoActual[i];
        }

        completada = referencia.completada;
    }

    


    public Mision IniciarMision(int valor)
    {
        indice = valor;

        switch (valor)
        {
            case 0:
                titulo = "Limpiando la universidad";
                tituloIngles = "Cleaning the university";

                descripcion = "Un chico llamado Pedro te ha pedido ayuda para hacer menos peligrosa la universidad. Debes derrotar a 10 Esqueletos y volver después para hablar con él para que se una a tu equipo";
                descripcionIngles = "A boy named Pedro has asked you for help to make the University less dangerous. You must defeat 10 Skeletons. Come back later to talk to him and he will join your team.";

                estadoActual = new string[2];
                estadoActualIngles = new string[2];

                estadoActual[0] = "- Enemigos derrotados 0/10.";
                estadoActualIngles[0] = "- Enemies defeat 0/10.";
                estadoActual[1] = "- Habla con Pedro al terminar el encargo.";
                estadoActualIngles[1] = "- Talk to Pedro at the end of the assignment.";

                tipoDeMision = tipoMision.RECLUTAMIENTO;
                tipoDeMisionIngles= tipoMisionIngles.RECRUITMENT;

                recompensas = new string[1];
                recompensasIngles = new string[1];

                recompensas[0] = "- Pedro se unirá a tu equipo.";
                recompensasIngles[0] = "- Pedro will join your team.";

                origen = "Universidad";
                origenIngles = "University";

                break;
            case 1:
                titulo = "Salvando el bosque";
                tituloIngles = "Saving the forest";

                descripcion = "Gámez te ha pedido ayuda para recuperar un objeto de gran importancia para el bosque. Según él, se trata de la Gema de la Vida. Su poder permite al bosque mantener su vitalidad y que tras cada invierno todo vuelva a brotar. Debes recuperarla y devolvérsela para que la recoloque en el sitio que le corresponde.";
                descripcionIngles = "Gámez has asked you for help to recover an object of great importance to the forest. According to him, it's about the Gem of Life. Its power allows the forest to maintain its vitality and after each winter everything sprouts again. You must retrieve it and return it so that it can be repositioned in its proper place.";

                estadoActual = new string[2];
                estadoActualIngles = new string[2];

                estadoActual[0] = "- Recupera la Gema de la Vida.";
                estadoActualIngles[0] = "- Recover the Gem of Life.";
                estadoActual[1] = "- Devuelve la Gema de la Vida a Gámez.";
                estadoActualIngles[1] = "- Return the Gem of Life to Gámez.";

                tipoDeMision = tipoMision.RECLUTAMIENTO;
                tipoDeMisionIngles = tipoMisionIngles.RECRUITMENT;

                recompensas = new string[1];
                recompensasIngles = new string[1];

                recompensas[0] = "- Gámez se unirá a tu equipo.";
                recompensasIngles[0] = "- Gámez will join your team.";

                origen = "R7";
                origenIngles = "R7";

                break;
            case 2:
                titulo = "Pelea de bar";
                tituloIngles = "Bar fight";

                descripcion = "Un chico llamado Carlos tiene un conflicto con algunos tipos en los bares de Pedrán y te ha pedido ayuda para darles una lección y echarlos de allí. Deberás derrotar a los 3 matones que le persiguen.";
                descripcionIngles = "A boy named Carlos has a conflict with some guys in Pedrán’s taverns. He has asked you for help to fight with them and throw them out of there. You must defeat the 3 thugs who chase him.";

                estadoActual = new string[4];
                estadoActualIngles = new string[4];
                
                estadoActual[0] = "- Derrota a los matones del Dragón Rojo.";
                estadoActualIngles[0] = "- Defeat the Red Dragon thugs.";
                estadoActual[1] = "- Derrota a los matones de la Parada.";
                estadoActualIngles[1] = "- Defeat the Parada thugs.";
                estadoActual[2] = "- Derrota a los matones de la Marabunta.";
                estadoActualIngles[2] = "- Defeat the Marabunta thugs.";
                estadoActual[3] = "- Habla con Carlos.";
                estadoActualIngles[3] = "- Talk with Carlos.";

                tipoDeMision = tipoMision.RECLUTAMIENTO;
                tipoDeMisionIngles = tipoMisionIngles.RECRUITMENT;
                
                recompensas = new string[1];
                recompensasIngles = new string[1];
                
                recompensas[0] = "- Carlos se unirá a tu equipo.";
                recompensasIngles[0] = "- Carlos will join your team.";

                origen = "Pedrán";
                origenIngles = "Pedrán";

                break;
            case 3:
                titulo = "¿Pruebas?";
                tituloIngles = "Evidences?";

                descripcion = "Un misterioso sujeto enmascarado nos ha hablado de unas pruebas que implicarían a la Cúpula, el problema es que no están en su poder y necesita que alguien le ayude a recuperarlas. Debemos vigilar la taberna de El Paso por si aparecieran los soldados que custodian estas pruebas y recuperarlas.";
                descripcionIngles = "A mysterious masked man has told us about some evidences that would involve the Dome. The problem is that they are not in his power and he needs someone to help him to recover them. We must watch El Paso tavern in case the soldiers guarding these evidences appear and recover them.";

                estadoActual = new string[2];
                estadoActualIngles = new string[2];
                
                estadoActual[0] = "- Derrota a los guardias.";
                estadoActualIngles[0] = "- Defeat the guards.";
                estadoActual[1] = "- Entrega las pruebas.";
                estadoActualIngles[1] = "- Deliver the evidence.";

                tipoDeMision = tipoMision.SECUNDARIA;
                tipoDeMisionIngles = tipoMisionIngles.SECONDARY;
                
                recompensas = new string[1];
                recompensasIngles = new string[1];
                
                recompensas[0] = "- 500 monedas.";
                recompensasIngles[0] = "- 500 coins.";

                origen = "R10";
                origenIngles = "R10";
                
                break;
            case 4:
                titulo = "Venganza";
                tituloIngles = "Revenge";

                descripcion = "Nani era uno de los muchos habitantes de Canda y busca venganza contra aquellos que arrasaron su hogar. Sabe donde se encuentran los dos que dirigieron el ataque y quiere que te encargues de ellos ya que a él podrían reconocerlo y huir.";
                descripcionIngles = "Nani lived in Canda when the attack occurred and seeks revenge against those who razed his home. He knows where the two who led the attack are and wants you to take care of them because they could recognize him and run away."; 

                estadoActual = new string[2];
                estadoActualIngles = new string[2];

                estadoActual[0] = "- Derrota al objetivo de Pedrán.";
                estadoActualIngles[0] = "- Defeat the enemy of Pedrán.";
                estadoActual[1] = "- Derrota al objetivo de la R5.";
                estadoActualIngles[1] = "- Defeat the enemy of R5.";

                tipoDeMision = tipoMision.RECLUTAMIENTO;
                tipoDeMisionIngles = tipoMisionIngles.RECRUITMENT;
                
                recompensas = new string[1];
                recompensasIngles = new string[1];
                
                recompensas[0] = "- Nani se unirá a tu equipo.";
                recompensasIngles[0] = "- Nani will join your team.";

                origen = "Canda";
                origenIngles = "Canda";

                break;
            case 5:
                titulo = "Juego de espías";
                descripcion = "Elvira es una de las mejores espías de la resistencia, pero cierto objetivo se le está resistiendo. Te pide ayuda para localizar a Henry, un sujeto que podría tener relación con la Cúpula. Deberás reunir información de gente que haya podido verlo recientemente y pruebas que le relacionen con la Cúpula.";
                estadoActual = new string[2];
                estadoActual[0] = "- Encuentra a alguien que pueda darte información en Pedrán.";
                estadoActual[1] = "- Encuentra los documentos que demuestres su implicación en la Cúpula.";
                tipoDeMision = tipoMision.RECLUTAMIENTO;
                recompensas = new string[1];
                recompensas[0] = "- Elvira se unirá a tu equipo.";
                origen = "Bosque Esperanza";
                break;
            case 6:
                titulo = "En tierra hostil";
                descripcion = "Una exploradora de Manfa (falta nombre) nos ha pedido ayuda para realizar una misión para la Resistencia sin darnos muchos más detalles de ello. Nos ha pedido que nos encontremos con ella en el piso franco que allí tiene la Resistencia.";
                estadoActual = new string[1];
                estadoActual[0] = "- Encuentra el piso franco en Manfa.";
                tipoDeMision = tipoMision.SECUNDARIA;
                recompensas = new string[1];
                recompensas[0] = "- 100 monedas.";
                origen = "Base Resistencia";
                break;
            case 7:
                titulo = "-";
                descripcion = "-";
                estadoActual = new string[1];
                estadoActual[0] = "-";
                tipoDeMision = tipoMision.PRINCIPAL;
                recompensas = new string[1];
                recompensas[0] = "- Isser se unirá a tu equipo.";
                origen = "";
                break;
            case 8:
                titulo = "General";
                descripcion = "-";
                estadoActual = new string[1];
                estadoActual[0] = "-";
                tipoDeMision = tipoMision.PRINCIPAL;
                recompensas = new string[1];
                recompensas[0] = "- General se unirá a tu equipo.";
                origen = "";
                break;
            case 9:
                titulo = "Combate de entreno";
                tituloIngles = "Training Combat";

                descripcion = "Luis quiere prepararse para el examen de acceso a la Guardia Imperial y te pide ayuda para entrenar. Quiere un combate para probar su fuerza.";
                descripcionIngles = "Luis wants to prepare for the entrance exam to the Imperial Guard and asks for help to train. He wants a fight to prove his strength.";

                estadoActual = new string[2];
                estadoActualIngles = new string[2];

                estadoActual[0] = "- Habla con Luis cuando estés listo.";
                estadoActualIngles[0] = "- Talk with Luis when you are ready.";
                estadoActual[1] = "- Enfréntate a Luis en una batalla de entrenamiento.";
                estadoActualIngles[1] = "- Face Luis in a training battle.";

                tipoDeMision = tipoMision.SECUNDARIA;
                tipoDeMisionIngles = tipoMisionIngles.SECONDARY;

                recompensas = new string[1];
                recompensasIngles = new string[1];

                recompensas[0] = "- 200 monedas.";
                recompensasIngles[0] = "- 200 coins.";

                origen = "El Paso";
                origenIngles = "El Paso";

                break;
            case 10:
                titulo = "Comienza el viaje";
                tituloIngles = "The journey begins";

                descripcion = "Es hora de comenzar una nueva vida lejos de casa e ir a la universidad. Tu padre te hizo una recomendación antes de salir, que visitaras la taberna de El Paso antes de marchar a Pedrán.";
                descripcionIngles = "It's time to start a new life away from home and go to the University. Your father made you a recommendation before leaving. Visit El Paso tavern before going to Pedrán.";

                estadoActual = new string[1];
                estadoActualIngles = new string[1];

                estadoActual[0] = "- Ve a la taberna de El Paso.";
                estadoActualIngles[0] = "- Go to El Paso Tavern.";

                tipoDeMision = tipoMision.PRINCIPAL;
                tipoDeMisionIngles = tipoMisionIngles.PRINCIPAL;

                recompensas = new string[1];
                recompensasIngles = new string[1];

                recompensas[0] = "";
                recompensasIngles[0] = "";

                origen = "Pueblo Origen";
                origenIngles = "Origin Town";

                break;
            case 11:
                titulo = "Mejor acompañado";
                tituloIngles = "Better with company";

                descripcion = "Sara te ha propuesto acompañarte hasta Pedrán para hacer tu camino más fácil. De ti depende decidir si quieres que vaya contigo, pero has de recordar que toda decisión tiene sus consecuencias.";
                descripcionIngles = "Sara has proposed to accompany you to Pedrán to make your path easier. It is up to you to decide if you want to go with her, but you have to remember that every decision has its consequences.";

                estadoActual = new string[1];
                estadoActualIngles = new string[1];

                estadoActual[0] = "- Completa el registro para la universidad.";
                estadoActualIngles[0] = "- Complete the registration for the university.";

                tipoDeMision = tipoMision.RECLUTAMIENTO;
                tipoDeMisionIngles = tipoMisionIngles.RECRUITMENT;
                
                recompensas = new string[1];
                recompensasIngles = new string[1];
                
                recompensas[0] = "- Sara se unirá a tu equipo.";
                recompensasIngles[0] = "- Sara will join your team.";
                
                origen = "El Paso";
                origenIngles = "El Paso";

                break;
            case 12:
                titulo = "Mejor solo...";
                tituloIngles = "Better alone...";

                descripcion = "Sara te ha propuesto acompañarte, pero has decidido viajar solo. Por ello tu próxima misión será ir hasta la sede de la Guardia en Pedrán para completar tu registro en la universidad.";
                descripcionIngles = "Sara has proposed to accompany you, but you have decided to travel alone. Therefore, your next mission will be to go to the headquarters of the Guard in Pedrán to complete your registration at the university.";

                estadoActual = new string[1];
                estadoActualIngles = new string[1];

                estadoActual[0] = "- Completa el registro para la universidad.";
                estadoActualIngles[0] = "- Complete the registration for the university.";

                tipoDeMision = tipoMision.PRINCIPAL;
                tipoDeMisionIngles = tipoMisionIngles.PRINCIPAL;
                
                recompensas = new string[1];
                recompensasIngles = new string[1];
                
                recompensas[0] = "- 500 monedas.";
                recompensasIngles[0] = "- 500 coins.";
                
                origen = "El Paso";
                origenIngles = "El Paso";

                break;
            case 13:
                titulo = "Camino al registro";
                tituloIngles = "Walk to the registry";

                descripcion = "Has encontrado un nuevo acompañante para tu viaje. Ahora tu deber es ir a la sede de la Guardia para registraros en la universidad.";
                descripcionIngles = "You have found a new companion for your trip. Now your duty is to go to the headquarters of the Guard to register at the university.";

                estadoActual = new string[1];
                estadoActualIngles = new string[1];

                estadoActual[0] = "- Completa el registro para la universidad.";
                estadoActualIngles[0] = "- Complete the registration for the university.";

                tipoDeMision = tipoMision.PRINCIPAL;
                tipoDeMisionIngles = tipoMisionIngles.PRINCIPAL;
                
                recompensas = new string[1];
                recompensasIngles = new string[1];
                
                recompensas[0] = "- 500 monedas.";
                recompensasIngles[0] = "- 500 coins.";
                
                origen = "El Paso";
                origenIngles = "El Paso";
                
                break;
            case 14:
                titulo = "Primer día de clase";
                tituloIngles = "First day of class";

                descripcion = "Ya eres oficialmente miembro de la Universidad de Áncia. Toca ir a la gran presentación del curso en el edificio principal.";
                descripcionIngles = "You are already officially a member of the University of Áncia. It is time to go to the great presentation of the course in the main building.";

                estadoActual = new string[1];
                estadoActualIngles = new string[1];
                
                estadoActual[0] = "- Llega a presentación del curso.";
                estadoActualIngles[0] = "- Get to the course presentation.";

                tipoDeMision = tipoMision.PRINCIPAL;
                tipoDeMisionIngles = tipoMisionIngles.PRINCIPAL;
                
                recompensas = new string[1];
                recompensasIngles = new string[1];
                
                recompensas[0] = "";
                recompensasIngles[0] = "";
                
                origen = "Pedran";
                origenIngles = "Pedran";
                
                break;
            case 15:
                titulo = "Cuestión de probabilidades";
                tituloIngles = "Issue of probabilities";

                descripcion = "El hecho de enfrentarte a La Cúpula ha provocado que te ganes su enemistad. Sin embargo, la Resistencia quiere que formes parte de ellos y te han pedido ayuda para buscar sus exploradores desaparecidos en el departamento de Estadística. Este se encuentra al Oeste del campus principal.";
                descripcionIngles = "The fact of facing The Dome has caused you to win their enmity. However, the Resistance wants you to be part of them and they have asked you for help to search for their missing explorers in the Statistics department. This is located west of the main campus.";

                estadoActual = new string[1];
                estadoActualIngles = new string[1];
                
                estadoActual[0] = "- Encuentra al grupo de exploradores desaparecidos.";
                estadoActualIngles[0] = "- Find the group of missing explorers.";

                tipoDeMision = tipoMision.PRINCIPAL;
                tipoDeMisionIngles = tipoMisionIngles.PRINCIPAL;
                
                recompensas = new string[2];
                recompensasIngles = new string[2];
                
                recompensas[0] = "- 100 monedas";
                recompensasIngles[0] = "- 100 coins";
                recompensas[1] = "- Aumento defensivo";
                recompensasIngles[1] = "- Defensive increase";

                origen = "Base Resistencia";
                origenIngles = "Resistance Base";

                break;
            case 16:
                titulo = "Cuestión de probabilidades";
                tituloIngles = "Issue of probabilities ";

                descripcion = "Has enfrentado a uno de los líderes de la Cúpula para dar tiempo a tus compañeros de huir. La derrota era inevitable pero justo a tiempo aparecieron aliados para sacaros de allí a salvo.";
                descripcionIngles = "You have faced one of the leaders of the Dome to give your companions time to flee. The defeat was inevitable but just in time allies appeared to get you out of there safe.";

                estadoActual = new string[1];
                estadoActualIngles = new string[1];
                
                estadoActual[0] = "- Encuentra al grupo de exploradores desaparecidos.";
                estadoActualIngles[0] = "- Find the group of missing explorers.";

                tipoDeMision = tipoMision.PRINCIPAL;
                tipoDeMisionIngles = tipoMisionIngles.PRINCIPAL;
                
                recompensas = new string[2];
                recompensasIngles = new string[2];
                
                recompensas[0] = "- 100 monedas";
                recompensasIngles[0] = "- 100 coins";
                recompensas[1] = "- Aumento ofensivo";
                recompensasIngles[1] = "- Offensive increase";

                origen = "Base Resistencia";
                origenIngles = "Resistance Base";

                break;
            case 17:
                titulo = "Becario";
                tituloIngles = "Rookie";

                descripcion = "Has aceptado entrar en La Resistencia y ya tienes tu primera misión. Debes comenzar tus clases en la universidad y estar atento a los acontecimientos que allí ocurran.";
                descripcionIngles = "You have agreed to enter the Resistance and you already have your first mission. You must start your classes at the university and be aware of the events that occur there.";

                estadoActual = new string[2];
                estadoActualIngles = new string[2];
                
                estadoActual[0] = "- Vuelve a la universidad.";
                estadoActualIngles[0] = "- Come back to University.";
                estadoActual[1] = "- Entra en el aula 0.1.";
                estadoActualIngles[1] = "- Enter the classroom 0.1.";

                tipoDeMision = tipoMision.PRINCIPAL;
                tipoDeMisionIngles = tipoMisionIngles.PRINCIPAL;
                
                recompensas = new string[1];
                recompensasIngles = new string[1];
                
                recompensas[0] = "";
                recompensasIngles[0] = "";
                
                origen = "Base Resistencia";
                origenIngles = "Resistance Base";

                break;
            case 18:
                titulo = "Continua tu viaje";
                tituloIngles = "Continue your trip";

                descripcion = "Has rechazado a la Resistencia y ya no puedes contar con ellos. Dirígete de vuelta hacia la universidad.";
                descripcionIngles = "You have rejected the Resistance and you can no longer count on them. Head back to the university.";

                estadoActual = new string[1];
                estadoActualIngles = new string[1];
                
                estadoActual[0] = "- Llega a Universidad.";
                estadoActualIngles[0] = "- Come back to the University.";

                tipoDeMision = tipoMision.PRINCIPAL;
                tipoDeMisionIngles = tipoMisionIngles.PRINCIPAL;
                
                recompensas = new string[1];
                recompensasIngles = new string[1];
                
                recompensas[0] = "";
                recompensasIngles[0] = "";
                
                origen = "Base Resistencia";
                origenIngles = "Resistance Base";

                break;
            case 19:
                titulo = "A las armas";
                tituloIngles = "Take the weapons";

                descripcion = "Has aceptado entrar en La Resistencia y ya tienes tu primera misión. Debes dirigirte a la universidad e infiltrarte junto a un grupo de la Resistencia para rescatar a uno de sus miembros y para robar unos importantes documentos.";
                descripcionIngles = "You have agreed to enter the Resistance and you already have your first mission. You must go to the university and infiltrate with a Resistance group to rescue one of its members and to steal some important documents.";

                estadoActual = new string[3];
                estadoActualIngles = new string[3];
                
                estadoActual[0] = "- Vuelve a la Universidad.";
                estadoActualIngles[0] = "- Come back to the University.";
                estadoActual[1] = "- Rescata al teniente Gueorgui.";
                estadoActualIngles[1] = "- Rescue Lieutenant Gueorgui.";
                estadoActual[2] = "- Recupera la información del despacho de Elric.";
                estadoActualIngles[2] = "- Retrieve information from Elric's office.";

                tipoDeMision = tipoMision.PRINCIPAL;
                tipoDeMisionIngles = tipoMisionIngles.PRINCIPAL;
                
                recompensas = new string[1];
                recompensasIngles = new string[1];
                
                recompensas[0] = "";
                recompensasIngles[0] = "";
                
                origen = "Base Resistencia";
                origenIngles = "Resistance Base";

                break;
            case 20:
                titulo = "Libro perdido";
                tituloIngles = "Lost book";

                descripcion = "Ha desaparecido un libro en la biblioteca y Tob nos ha pedido que le ayudemos a encontrarlo para poder aprobar su examen. Debes encontrarlo y llevárselo cuanto antes para que pueda aprobar su examen.";
                descripcionIngles = "A book has disappeared in the library and Tob has asked us to help him find it in order to pass his exam. You must find it and take it as soon as possible so you can pass your exam.";

                estadoActual = new string[2];
                estadoActualIngles = new string[2];
                
                estadoActual[0] = "- Encuentra el libro perdido.";
                estadoActualIngles[0] = "- Find the lost book.";
                estadoActual[1] = "- Entrega el libro a Tob.";
                estadoActualIngles[1] = "- Deliver the book to Tob.";

                tipoDeMision = tipoMision.RECLUTAMIENTO;
                tipoDeMisionIngles = tipoMisionIngles.RECRUITMENT;
                
                recompensas = new string[1];
                recompensasIngles = new string[1];
                
                recompensas[0] = "Tob se unirá a tu equipo";
                recompensasIngles[0] = "Tob will join your team";

                origen = "Universidad";
                origenIngles = "University";

                break;
            case 21:
                titulo = "Buscando un remplazo";
                tituloIngles = "Looking for a replacement";

                descripcion = "Has negociado comprarle la gema al ladrón por 10.000 monedas. Habla con él cuando tengas el pago del rescate.";
                descripcionIngles = "You have negotiated to buy the gem from the thief for 10,000 coins. Talk to him when you have the rescue payment.";

                estadoActual = new string[1];
                estadoActualIngles = new string[1];
                
                estadoActual[0] = "- Págale 10.000 monedas al ladrón.";
                estadoActualIngles[0] = "- Pay the thief 10,000 coins.";

                tipoDeMision = tipoMision.SECUNDARIA;
                tipoDeMisionIngles = tipoMisionIngles.SECONDARY;
                
                recompensas = new string[1];
                recompensasIngles = new string[1];
                
                recompensas[0] = "";
                recompensasIngles[0] = "";
                
                origen = "R7";
                origenIngles = "R7";
                
                break;
            case 22:
                titulo = "La búsqueda";
                tituloIngles = "La búsqueda";

                descripcion = "Helena ha encontrado la entrada al templo oculto en Canda. Quiere resolver el misterio de que hay en este lugar y te pide ayuda. Ella te esperará en el pueblo para mostrarte la entrada.";
                descripcionIngles = "Helena ha encontrado la entrada al templo oculto en Canda. Quiere resolver el misterio de que hay en este lugar y te pide ayuda. Ella te esperará en el pueblo para mostrarte la entrada.";
                
                estadoActual = new string[2];
                estadoActualIngles = new string[2];
                
                estadoActual[0] = "- Habla con Helena en Canda.";
                estadoActualIngles[0] = "- Habla con Helena en Canda.";
                estadoActual[1] = "- Explora las ruinas.";
                estadoActualIngles[1] = "- Explora las ruinas.";
                
                tipoDeMision = tipoMision.RECLUTAMIENTO;
                tipoDeMisionIngles = tipoMisionIngles.RECRUITMENT;
                
                recompensas = new string[1];
                recompensasIngles = new string[1];
                
                recompensas[0] = "Helena se unirá a tu equipo";
                recompensasIngles[0] = "Helena se unirá a tu equipo";
                
                origen = "R7";
                origenIngles = "R7";
                
                break;
            case 23:
                titulo = "Demo";
                tituloIngles = "Demo";

                descripcion = "Enhorabuena has llegado al final de la demo. A partir de aquí no aparecerán más misiones de historia, pero puedes probar el resto de decisiones y explorar cuanto quieras. Espero hayas disfrutado del juego.";
                descripcionIngles = "Congratulations you have reached the end of the demo. From here no more history missions will appear, but you can try the rest of the decisions and explore as much as you want. I hope you enjoyed the game.";

                estadoActual = new string[1];
                estadoActualIngles = new string[1];

                estadoActual[0] = "- Disfruta.";
                estadoActualIngles[0] = "- Enjoy.";

                tipoDeMision = tipoMision.PRINCIPAL;
                tipoDeMisionIngles = tipoMisionIngles.PRINCIPAL;
                recompensas = new string[0];
                recompensasIngles = new string[0];

                origen = "Áncia";
                origenIngles = "Áncia";

                break;
        }

        return this;
    }
}
