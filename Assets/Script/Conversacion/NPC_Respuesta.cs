using UnityEngine;

public class NPC_Respuesta : MonoBehaviour
{
    BaseDatos baseDeDatos;
    GameObject manager;

    [SerializeField]
    [TextArea(10, 5)]
    public string mensaje;
    string emisor;
    public bool mision;
    public int indiceZona;
    public int indiceNPC;
    public int indiceMision;
    int indiceEleccion; //Esta variable establece el texto de elección en el TextBox
    int mensajeActual;
    bool eleccion;
    bool activo;
    bool multiplesMensajes;
    int numeroElecciones;

    NuevoTablero tablero;
    ControlJugador jugador;
    MusicaManager musica;
    ControlObjetos controlObjetos;

    public bool seGira;
    public Sprite[] imagenOrientacion = new Sprite[4];
    public int orientacion; //0 - Norte, 1 - Este, 2 - Oeste, 3 - Sur 


    private void Start()
    {
        manager = GameObject.Find("GameManager");
        baseDeDatos = manager.GetComponent<BaseDatos>();
        tablero = manager.GetComponent<NuevoTablero>();
        controlObjetos = manager.GetComponent<ControlObjetos>();
        jugador = GameObject.Find("Player").GetComponent<ControlJugador>();
        musica = GameObject.Find("Musica").GetComponent<MusicaManager>();

        indiceMision = -1;
        eleccion = false;
        activo = false;
        indiceEleccion = -1;
        multiplesMensajes = false;
        mision = false;
        mensajeActual = 0;
        numeroElecciones = 0;
    }



    private void Update()
    {
        if (activo)
        {
            if (eleccion)
            {
                if (TextBox.ocultar) 
                {
                    if (Input.GetKeyDown(KeyCode.B) || Input.GetButtonUp("X"))
                    {
                        eleccion = false;
                        bool combate = false;
                        TextBox.OcultaEleccion();

                        if (baseDeDatos.idioma == 1)
                        {
                            switch (indiceMision)
                            {
                                case 0:
                                    mensaje = "Perfect, the job is to defeat a few of those skeleton-shaped monsters that appear in the forest. If you defeat 10 I think it will help to make the road a little safer.";
                                    break;
                                case 1:
                                    mensaje = "The forest was right to trust you. The thieves have hidden here and put the object in a chest. I will wait here to prevent them from fleeing. you must go in search of them.";
                                    break;
                                case 3:
                                    mensaje = "Thank you so much for help me. As I heard a group of soldiers of the Dome will escort the documents from Canda to the Imperial City, I will stay here hidden in case they pass. You wait at the inn in El Paso, they sure make a stop on the road there in case they dodge me.";
                                    break;
                                case 4:
                                    if (indiceEleccion == 12)
                                    {
                                        mensaje = "I knew I could count with your strength. The first objective is in a tavern in Pedrán. The site is called Parada and the subject is a Dome Guard. The second is one of those damn orcs. He was the one who led the attack and is now in the R5. Go and bring me their souls as payment for what they did.";
                                    }
                                    else if (indiceEleccion == 13)
                                    {
                                        mensaje = "Damn kid get ready to pay your boldness.";
                                        combate = true;
                                        mision = true;
                                    }
                                    else
                                    {
                                        mensaje = "I knew this day would come, get ready to fight.";
                                        combate = true;
                                        mision = true;
                                    }

                                    break;
                                case 9:
                                    mensaje = "Great! Talk to me when you're ready.";
                                    break;
                                case 20:
                                    mensaje = "I owe you my life, friend. Let me know if you find it.";
                                    break;
                                case 21:
                                    mensaje = "You shouldn't have put your nose where nobody calls you, this cost a fortune and I plan to charge it. Get ready to fight.";
                                    combate = true;
                                    mision = true;
                                    break;
                            }
                        }
                        else
                        {
                            switch (indiceMision)
                            {
                                case 0:
                                    mensaje = "Perfecto, pues el trabajo es derrotar a unos cuantos de esos monstruos con forma de esqueleto que aparecen en el bosque. Con derrotar unos 10 creo que servirá para hacer un poco más seguro el camino.";
                                    break;
                                case 1:
                                    mensaje = "El bosque tenía razón al confiar en ti. Los ladrones se han escondido aquí y han guardado el objeto en un cofre. Yo esperaré aquí para evitar que puedan abandonarlo, tú ve en su búsqueda.";
                                    break;
                                case 3:
                                    mensaje = "Muchas gracias por ayudarme. Según tengo entendido un grupo de soldados de la Cúpula escoltará los documentos desde Canda hasta la Ciudad Imperial, yo me quedaré aquí escondido por si pasan. Tú espera en la posada de El Paso, seguro hacen una parada en el camino allí en caso de que me esquiven.";
                                    break;
                                case 4:
                                    if (indiceEleccion == 12)
                                    {
                                        mensaje = "Sabía que podía contar con tu fuerza. El primer objetivo se encuentra en una taberna de Pedrán. El sitio se llama Parada y el sujeto es un guardia de la Cúpula. El segundo es uno de esos malditos orcos. Fue quien lideró el ataque y ahora suele rondar la R5. Ve y tráeme sus almas a modo de pago por lo que hicieron.";
                                    }
                                    else if (indiceEleccion == 13)
                                    {
                                        mensaje = "Maldito crio prepárate para pagar tu osadía.";
                                        combate = true;
                                        mision = true;
                                    }
                                    else
                                    {
                                        mensaje = "Sabía que este día llegaría, prepárate a luchar.";
                                        combate = true;
                                        mision = true;
                                    }

                                    break;
                                case 9:
                                    mensaje = "¡Genial! Habla conmigo cuando estés preparado.";
                                    break;
                                case 20:
                                    mensaje = "Te debo la vida amigo. Avísame si lo encuentras.";
                                    break;
                                case 21:
                                    mensaje = "No debiste haberte metido donde no te llamaban, esto vale una fortuna y pienso cobrármelo. Prepárate para luchar.";
                                    combate = true;
                                    mision = true;
                                    break;
                            }
                        }

                        if (eleccion)
                        {
                            TextBox.MuestraTextoConEleccion(mensaje, emisor, indiceEleccion);
                        }
                        else if (!combate)
                        {
                            TextBox.MuestraTextoConMision(mensaje, false, true, indiceMision);
                        }
                        else
                        {
                            TextBox.MuestraTexto(mensaje, false);
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonUp("B"))
                    {
                        eleccion = false;
                        TextBox.OcultaEleccion();

                        if (baseDeDatos.idioma == 1)
                        {
                            switch (indiceMision)
                            {
                                case 0:
                                    mensaje = "Maybe on another occasion you can help me.";
                                    break;
                                case 1:
                                    mensaje = "I hope you reconsider it, the forest needs you.";
                                    break;
                                case 3:
                                    mensaje = "/*Whispers*/ I must gather evidence to prove my theory and take it to him, perhaps we will get his support.";
                                    break;
                                case 4:
                                    if (indiceEleccion == 9)
                                    {
                                        mensaje = "...";
                                    }
                                    else if (indiceEleccion == 10)
                                    {
                                        multiplesMensajes = true;
                                        mensaje = "I was the leader of a proud tribe of orcs. Despite our hostile nature we had decided to live in peace. However, one day some soldiers entered our village and kidnapped our families.";
                                    }
                                    else if (indiceEleccion == 11)
                                    {
                                        controlObjetos.perdonarVidaOrco = true;
                                        baseDeDatos.ApagaPersonajes(3);
                                        mensaje = "I wish I could think like that ... However, there may still be hope for me. I think I will start a new journey in search of a meaning to what is left of my life. I appreciate that you listen my story, I don't know if anyone else would have been so understanding.";
                                    }
                                    else if (indiceEleccion == 13)
                                    {
                                        mensaje = "That's how I like it, move away and I won't see you here again.";
                                    }
                                    else
                                    {
                                        mensaje = "I will not leave those murderers without their punishment. With or without you they will pay.";
                                    }
                                    break;
                                case 9:
                                    mensaje = "Don’t worry. Let me know if you change your mind.";
                                    break;
                                case 20:
                                    mensaje = "Today is not my lucky day. What a mess.";
                                    break;
                                case 21:
                                    mensaje = "Exactly turn around and I don't want to see you again.";
                                    mision = false;
                                    break;
                            }
                        }
                        else
                        {
                            switch (indiceMision)
                            {
                                case 0:
                                    mensaje = "Quizás en otra ocasión puedas ayudarme.";
                                    break;
                                case 1:
                                    mensaje = "Espero lo reconsideres, el bosque te necesita.";
                                    break;
                                case 3:
                                    mensaje = "/*Susurros*/ Debo reunir pruebas para demostrar mi teoría y llevárselas a él, quizás así consigamos su apoyo.";
                                    break;
                                case 4:
                                    if (indiceEleccion == 9)
                                    {
                                        mensaje = "...";
                                    }
                                    else if (indiceEleccion == 10)
                                    {
                                        multiplesMensajes = true;
                                        mensaje = "Yo era el líder de una orgullosa tribu de orcos. A pesar de nuestra naturaleza hostil nosotros habíamos decidido vivir en paz. Sin embargo, un día unos soldados entraron en nuestra aldea y secuestraron a nuestras familias.";
                                    }
                                    else if (indiceEleccion == 11)
                                    {
                                        controlObjetos.perdonarVidaOrco = true;
                                        baseDeDatos.ApagaPersonajes(3);
                                        mensaje = "Ojalá yo pudiera pensar así... Sin embargo, quizás aún haya esperanzas para mí. Creo que comenzaré un nuevo viaje en busca de un sentido a lo que me queda de vida. Agradezco que hayas escuchado mi historia, no sé si alguien más habría sido tan comprensivo.";
                                    }
                                    else if(indiceEleccion == 13)
                                    {
                                        mensaje = "Así me gusta, desfila y que no te vuelva a ver por aquí.";
                                    }
                                    else
                                    {
                                        mensaje = "No pienso dejar a esos asesinos sin su castigo. Contigo o sin ti lo pagarán.";
                                    }
                                    break;
                                case 9:
                                    mensaje = "No pasa nada. Avísame si cambias de opinión.";
                                    break;
                                case 20:
                                    mensaje = "Ni en esto tengo suerte, que desastre de día.";
                                    break;
                                case 21:
                                    mensaje = "Exacto date la vuelta y que no te vuelva a ver.";
                                    mision = false;
                                    break;
                            }
                        }

                        TextBox.MuestraTexto(mensaje, false);
                    }
                    else if ((Input.GetKeyDown(KeyCode.V) || Input.GetButtonUp("Y")) && numeroElecciones == 3)
                    {
                        eleccion = false;
                        mision = false;
                        bool combate = false;
                        TextBox.OcultaEleccion();

                        if (baseDeDatos.idioma == 1)
                        {
                            switch (indiceMision)
                            {
                                case 21:
                                    mensaje = "Ok, we will make a deal, I give you the contents of the chest for 10,000 coins and not one less I am going to accept.";
                                    break;
                            }
                        }
                        else
                        {
                            switch (indiceMision)
                            {
                                case 4:
                                    if(indiceEleccion == 9)
                                    {
                                        numeroElecciones = 2;
                                        eleccion = true;
                                        indiceEleccion = 10;
                                        mensaje = "Sí, yo dirigí aquel ataque y no pasa un solo día en que no maldiga aquel momento. No olvido ni un solo rostro, ni un sonido y mucho menos olvido aquel olor a muerte… Si has venido a matarme adelante, pero si lo deseas puedo contarte mi historia.";
                                    }
                                    
                                    break;
                                case 21:
                                    mensaje = "Está bien, haremos un trato, te doy el contenido del cofre por 10.000 monedas y ni una menos estoy dispuesto a aceptar.";
                                    break;
                            }
                        }

                        if (eleccion)
                        {
                            TextBox.MuestraTextoConEleccion(mensaje, emisor, indiceEleccion);
                        }
                        else if (!combate)
                        {
                            TextBox.MuestraTextoConMision(mensaje, false, true, indiceMision);
                        }
                        else
                        {
                            TextBox.MuestraTexto(mensaje, false);
                        }
                    }
                }
            }
            else if (multiplesMensajes)
            {
                if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                {
                    if (TextBox.ocultar)
                    {
                        mensajeActual++;

                        if (baseDeDatos.idioma == 1)
                        {
                            MensajesPorZonaIngles();
                        }
                        else
                        {
                            MensajesPorZona();
                        }

                        if (eleccion)
                        {
                            TextBox.MuestraTextoConEleccion(mensaje, emisor, indiceEleccion);
                        }
                        else
                        {
                            TextBox.MuestraTexto(mensaje, false);
                        }
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                {
                    if (TextBox.ocultar)
                    {
                        if (!mision)
                        {
                            mision = false;
                            eleccion = false;
                            activo = false;
                        }
                        else
                        {
                            switch (indiceMision)
                            {
                                case 4:
                                    if(indiceEleccion != 12)
                                    {
                                        IniciaCombate();
                                    }
                                    break;
                                case 9:
                                    IniciaCombate();
                                    break;
                                case 21:
                                    IniciaCombate();
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }



    void Conversacion()
    {
        if (!TextBox.on)
        {
            mensajeActual = 0;
            multiplesMensajes = false;

            if (seGira && !multiplesMensajes)
            {
                if (jugador.mover == ControlJugador.movimiento.ABAJO)
                {
                    if (orientacion != 0)
                    {
                        orientacion = 0;
                        GetComponent<SpriteRenderer>().sprite = imagenOrientacion[0];
                    }
                }
                else if (jugador.mover == ControlJugador.movimiento.IZQUIERDA)
                {
                    if (orientacion != 2)
                    {
                        orientacion = 2;
                        GetComponent<SpriteRenderer>().sprite = imagenOrientacion[2];
                    }
                }
                else if (jugador.mover == ControlJugador.movimiento.DERECHA)
                {
                    if (orientacion != 1)
                    {
                        orientacion = 1;
                        GetComponent<SpriteRenderer>().sprite = imagenOrientacion[1];
                    }
                }
                else
                {
                    if (orientacion != 3)
                    {
                        orientacion = 3;
                        GetComponent<SpriteRenderer>().sprite = imagenOrientacion[3];
                    }
                }
            }

            if (baseDeDatos.idioma == 0)
            {
                MensajesPorZona();
            }
            else if (baseDeDatos.idioma == 1)
            {
                MensajesPorZonaIngles();
            }

            activo = true;

            if (eleccion)
            {
                TextBox.MuestraTextoConEleccion(mensaje, emisor, indiceEleccion);
            }
            else
            {
                TextBox.MuestraTexto(mensaje, false);
            }
        }
    }



    void IniciaCombate()
    {
        AnimacionCombate.IniciaCombate();
        TextBox.OcultaTextoFinCombate();
        
        switch (indiceMision)
        {
            case 4:
                if(indiceEleccion == 13)
                {
                    tablero.IniciarCombate(baseDeDatos.BuscarPersonajeIndice(27, false), baseDeDatos.BuscarPersonajeIndice(27, false), null, 1, 15, false, -1, 3);
                }
                else
                {
                    tablero.IniciarCombate(baseDeDatos.BuscarPersonajeIndice(36, false), null, null, 1, 15, false, -1, 2);
                }
                break;
            case 9:
                tablero.IniciarCombate(baseDeDatos.BuscarPersonajeIndice(13, true), null, null, 1, 5, false, -1, 0);
                break;
            case 21:
                tablero.IniciarCombate(baseDeDatos.BuscarPersonajeIndice(51, false), baseDeDatos.BuscarPersonajeIndice(51, false), baseDeDatos.BuscarPersonajeIndice(51, false), 3, 9, false, -1, 1);
                break;
        }

        musica.CambiaCancion(11);

        indiceMision = -1;
    }



    void MensajesPorZona()
    {
        numeroElecciones = 0;
        mision = false;
        indiceMision = -1;

        if (indiceZona == 0) //Universidad
        {
            if (indiceNPC == 0)
            {
                mensaje = "Estoy meditando como optimizar la cantidad de suspensos reduciendo el esfuerzo, no me molestes.";
            }
            else if (indiceNPC == 1)
            {
                mensaje = "Siento el ambiente tenso como si todo fuera a estallar de un momento a otro, espero estar equivocado con lo que me ha costado llegar hasta aquí...";
            }
            else if (indiceNPC == 2)
            {
                mensaje = "Las aulas están casi vacías y cada vez hay menos profesores. Cuando se darán cuenta que esta absurda guerra no beneficia a nadie. Sé que es una opinión impopular aquí, pero es lo que de verdad creo";
            }
            else if (indiceNPC == 3)
            {
                mensaje = "No debemos olvidar a aquellos que lucharon por nosotros. Estoy seguro de que al final venceremos.";
            }
            else if (indiceNPC == 4)
            {
                mensaje = "Es pisar este patio y ya me dan escalofríos";
            }
            else if (indiceNPC == 5)
            {
                mensaje = "He suspendido a 38 alumnos y eso que solo tengo 10. Seguro que con esto me gano un ascenso.";
            }
            else if (indiceNPC == 6)
            {
                mensaje = "Tenemos que hacer turnos para proteger la estatua de nuestro héroe de posibles saboteos por parte de la Cúpula.";
            }
            else if (indiceNPC == 7)
            {
                mensaje = "Hoy tengo un día tranquilo solo tengo 23 horas de clases, con suerte hasta podré dormir una hora si me doy prisa en llegar a casa";
            }
            else if (indiceNPC == 8)
            {
                mensaje = "Ganar combates te hará ganar experiencia. Cuando acumules suficiente subirás de nivel y podrás mejorar tus habilidades.";
            }
            else if (indiceNPC == 9)
            {
                mensaje = "Cuando yo era joven los estudiantes nos esforzábamos por lograr nuestras metas, ahora solo piensan en irse de fiesta y emborracharse.";
            }
            else if (indiceNPC == 10)
            {
                mensaje = "Me han suspendido un examen con nota negativa. ¿Cómo es posible tener menos de un 0?";
            }
            else if (indiceNPC == 11)
            {
                mensaje = "Me encargo de la vigilancia del departamento de matemáticas. Son el grupo más peligroso de la Cúpula con diferencia.";
            }
            else if (indiceNPC == 12)
            {
                mensaje = "No te fíes de nadie aquí, de hecho, no te fíes ni de mi ni de lo que acabo de decirte, nunca se sabe quién puede ser un espía... Espera, ¿eres un espía?";
            }
            else if (indiceNPC == 13)
            {
                mensaje = "Bienvenido a la universidad el lugar donde los sueños mueren.";
            }
            else if (indiceNPC == 14)
            {
                mensaje = "Aún recuerdo cuando me decían que mis mejores años serían cuando llegara a la Universidad. Que gran mentira me colaron.";
            }
            else if (indiceNPC == 15)
            {
                mensaje = "No todos estamos hechos para luchar sea o no la causa justa";
            }
            else if (indiceNPC == 16)
            {
                mensaje = "Monumento al héroe de los estudiantes, aquel que logró sacarse todas las asignaturas a la primera.";
            }
            else if (indiceNPC == 17)
            {
                bool aceptada = false;
                bool completada = false;
                indiceEleccion = 6;

                if (baseDeDatos.numeroMisionesReclutamiento != 0)
                {
                    for (int i = 0; i < baseDeDatos.numeroMisionesReclutamiento; i++)
                    {
                        if (baseDeDatos.listaMisionesReclutamiento[i].indice == 0)
                        {
                            aceptada = true;

                            if (controlObjetos.contadorMonstruosPedro == 10)
                            {
                                completada = true;
                            }
                        }
                    }
                }

                emisor = "Pedro";

                if (!aceptada)
                {
                    mensaje = "Hola compañero, mi nombre es Pedro y tengo una propuesta que hacerte. Me encanta venir a esta Universidad y me gustaría convertirla en un lugar mejor. Ayúdame con un pequeño trabajo y colaboraré en lo que necesites.";
                    mision = true;
                    eleccion = true;
                    indiceMision = 0;
                }
                else
                {
                    if (completada)
                    {
                        mensaje = "Has hecho un grandísimo trabajo será un honor ayudarte en tus futuras aventuras.";
                        mision = false;

                        for (int i = 0; i < baseDeDatos.numeroMisionesReclutamiento; i++)
                        {
                            if (baseDeDatos.listaMisionesSecundarias[i].indice == 0)
                            {
                                if (baseDeDatos.listaMisionesReclutamiento[i].completada)
                                {
                                    completada = true;
                                }
                                else
                                {
                                    completada = false;
                                }
                            }
                        }

                        if (!completada)
                        {
                            baseDeDatos.CumpleMision(0);
                        }
                    }
                    else
                    {
                        mensaje = "Ven a hablar conmigo cuando hayas terminado el trabajo.";
                        mision = false;
                    }
                }
            }
            else if (indiceNPC == 18)
            {
                mensaje = "¿Dedicar un cartel a una piedra? ¿A quién se le habrá ocurrido semejante tontería?";
            }
            else if (indiceNPC == 19)
            {
                mensaje = "Aquí descansan los alumnos caídos durante los exámenes de la última semana";
            }
            else if (indiceNPC == 20)
            {
                mensaje = "Bienvenidos a la Universidad del Imperio de Áncia";
            }
            else if (indiceNPC == 21)
            {
                mensaje = "¡PELIGRO! Departamento de matemáticas. Mantenerse alejados";
            }
            else if (indiceNPC == 22)
            {
                mensaje = "No podemos dejarte pasar por aquí.";
            }
            else if (indiceNPC == 23)
            {
                mensaje = "No es seguro acercarse para nadie, hasta nosotros sabemos eso.";
            }
            else if (indiceNPC == 24)
            {
                mensaje = "El Ala Oeste está cerrado hasta después de la inauguración.";
            }
            else if (indiceNPC == 25)
            {
                mensaje = "Todavía no he programado esta parte así que date la vuelta que no hay nada";
            }
        }
        else if (indiceZona == 1) //R7
        {
            if (indiceNPC == 0)
            {
                mensaje = "Si cruzas este puente llegas a la Universidad de Áncia. Ten cuidado allí, es una zona muy peligrosa.";
            }
            else if (indiceNPC == 1)
            {
                mensaje = "Creo que me he vuelto a perder. Este bosque es bastante lioso.";
            }
            else if (indiceNPC == 2)
            {
                mensaje = "Esta ruta lleva a R8";
            }
            else if (indiceNPC == 3)
            {
                mensaje = "Antes los caminos eran mucho más seguro. Con el antiguo emperador esto no habría ocurrido.";
            }
            else if (indiceNPC == 4)
            {
                mensaje = "Esta ruta lleva a R9";
            }
            else if (indiceNPC == 5)
            {
                bool aceptada = false;
                bool completada = false;
                indiceEleccion = 8;

                if (baseDeDatos.numeroMisionesReclutamiento != 0)
                {
                    for (int i = 0; i < baseDeDatos.numeroMisionesReclutamiento; i++)
                    {
                        if (baseDeDatos.listaMisionesReclutamiento[i].indice == 1)
                        {
                            aceptada = true;

                            if (controlObjetos.tieneGema)
                            {
                                completada = true;
                            }
                        }
                    }
                }

                emisor = "Gámez";

                if (!aceptada)
                {
                    mensaje = "El bosque me habla y te ha estado observando. Él dice que eres de fiar y que deberíamos ayudarnos mutuamente. Hay un tipo que ha robado un objeto muy valioso para el bosque. Ayúdame a recuperarlo antes de que caiga en las manos equivocadas y te ayudaré cuando me necesites.";
                    mision = true;
                    eleccion = true;
                    indiceMision = 1;
                }
                else
                {
                    if (completada)
                    {
                        mensaje = "Gracias por salvar al bosque. Él cree que deberías ser tú quien te quedes esa gema y estoy de acuerdo, has demostrado ser el más adecuado para protegerla. Estoy en deuda contigo, cuenta con mi hacha.";
                        mision = false;

                        baseDeDatos.CumpleMision(1);
                    }
                    else
                    {
                        mensaje = "Hay muchos frentes por los que luchar. Yo trato de cuidar el bosque y a aquellos que habitan en él. Como me entere que haces algo para dañarlo te buscaré y acabaré contigo.";
                        mision = false;
                    }
                }
            }
            else if (indiceNPC == 6)
            {
                mensaje = "El bosque es un lugar maravilloso donde meditar y estar en paz contigo mismo y con la naturaleza.";
            }
            else if (indiceNPC == 7)
            {
                mensaje = "No quiero volver a la universidad así que me escondo aquí para que no me encuentren. Vete antes de que alguien te vea.";
            }
            else if (indiceNPC == 8)
            {
                mensaje = "Existen 4 tipos distintos de cofres. Los comunes, los raros, los míticos y los legendarios. Cada uno necesita una llave mágica distinta para abrirlo. Cualquier otra cosa que hagas para intentarlo será inútil.";
            }
            else if (indiceNPC == 9)
            {
                mensaje = "Me gasté toda la paga de este mes en Pedrán y ahora no me queda ni una mísera moneda para comprar el equipamiento que me hace falta para entrenar.";
            }
            else if (indiceNPC == 10)
            {
                mensaje = "La Resistencia lucha por que los estudiantes seamos libres de nuevo. Ojalá el nuevo emperador lo viera y nos diera el apoyo que necesitamos para acabar con esta guerra.";
            }
            else if (indiceNPC == 11)
            {
                mensaje = "Tengo que venir a menudo al bosque ya que hay muchos materiales que necesito para mis experimentos que solo se encuentran aquí.";
            }
            else if (indiceNPC == 12)
            {
                mensaje = "Antes este camino era menos intrincado y seguro, pero casi de la noche a la mañana todo cambió y se volvió laberintico y lleno de maleantes. ¿Qué habrá pasado?";
            }
            else if (indiceNPC == 13)
            {
                mensaje = "Existen 5 tipos elementales. El Friki es fuerte contra el Fiestero y el Dormilón. El Fiestero contra el Tirano y el Responsable. El Dormilón contra el Fiestero y el Tirano. El Responsable contra el Friki y el Dormilón. Y por último el Tirano contra el Friki y el Responsable. Todos ellos son débiles contra su mismo tipo. Sin embargo, se dice que existe un sexto elemento el Neutro el cual solo es débil contra el Tirano, pero cuyos ataques no son especialmente fuertes contra ningún otro.";
            }
            else if (indiceNPC == 14)
            {
                bool activa = false;

                for (int i = 0; i < baseDeDatos.numeroMisionesReclutamiento; i++)
                {
                    if (baseDeDatos.listaMisionesReclutamiento[i].indice == 1)
                    {
                        activa = true;
                    }
                }

                if (activa)
                {
                    bool aceptada = false;
                    bool completada = false;
                    indiceEleccion = 7;

                    if (baseDeDatos.numeroMisionesSecundarias != 0)
                    {
                        for (int i = 0; i < baseDeDatos.numeroMisionesSecundarias; i++)
                        {
                            if (baseDeDatos.listaMisionesSecundarias[i].indice == 21)
                            {
                                aceptada = true;

                                if (jugador.dinero >= 10000)
                                {
                                    completada = true;
                                    jugador.dinero -= 10000;
                                    baseDeDatos.ApagaPersonajes(0);
                                }
                            }
                        }
                    }

                    emisor = "Pícaro";

                    if (!aceptada)
                    {
                        mensaje = "¿Qué haces aquí? Este cofre es nuestro así que vete por donde has venido o atente a las consecuencias.";
                        mision = true;
                        eleccion = true;
                        numeroElecciones = 3;
                        indiceMision = 21;
                    }
                    else
                    {
                        if (completada)
                        {
                            mensaje = "Has cumplido tu parte, así que nosotros cumpliremos la nuestra. Toma la Gema antes de que nos arrepintamos.";
                            mision = false;

                            baseDeDatos.CumpleMision(21);
                        }
                        else
                        {
                            mensaje = "Hasta que no tengas el pago íntegro no quiero verte por aquí.";
                            mision = false;
                        }
                    }
                }
                else
                {
                    mensaje = "Muchacho será mejor que te vayas de aquí, no se te ha perdido nada.";
                }
            }
        }
        else if (indiceZona == 2) //Pedran
        {
            if (indiceNPC == 0)
            {
                mensaje = "Aquí... sufriendo.";
            }
            else if (indiceNPC == 1)
            {
                mensaje = "Falta gente trabajando en el campo.";
            }
            else if (indiceNPC == 2)
            {
                mensaje = "El campo es vida, pero a mí me quita la mía.";
            }
            else if (indiceNPC == 3)
            {
                mensaje = "El mercado es el mejor sitio para obtener productos muy útiles.";
            }
            else if (indiceNPC == 4)
            {
                mensaje = "En mis tiempos todo esto era campo.";
            }
            else if (indiceNPC == 5)
            {
                mensaje = "Si tienes algún problema siempre puedes contar con la Guardia Imperial para ayudarte.";
            }
            else if (indiceNPC == 6)
            {
                mensaje = "Estás frente a la sede de la Guardia Imperial.";
            }
            else if (indiceNPC == 7)
            {
                mensaje = "Los soldados también necesitamos un descanso de vez en cuando.";
            }
            else if (indiceNPC == 8)
            {
                mensaje = "Estoy... perfect... perfectamente... *hip*";
            }
            else if (indiceNPC == 9)
            {
                mensaje = "Necesitaba un poco de aire fresco y al lado de la fuente es siempre mejor.";
            }
            else if (indiceNPC == 10)
            {
                mensaje = "Hay 3 grandes locales en Pedrán y se dice que cada uno está asociado a una facción. El primero es el Dragón Rojo el cual estaría lleno de partidarios de la Resistencia. El segundo es la Parada que estaría alineada con la Guardia Imperial. Y el último y más grande es la Marabunta alineada con la Universidad.";
            }
            else if (indiceNPC == 11)
            {
                mensaje = "Bienvenidos al Dragón Rojo, el local con más ritmo de todo Pedrán.";
            }
            else if (indiceNPC == 12)
            {
                mensaje = "Bienvenido a la Parada, un lugar donde relajarse y retomar fuerzas.";
            }
            else if (indiceNPC == 13)
            {
                mensaje = "Pasa al increíble Marabunta, el local más grande de todo Pedrán.";
            }
            else if (indiceNPC == 14)
            {
                mensaje = "¿Qué haces aquí? ¿No ves que estamos trabajando y podrías tener un accidente?";
            }
            else if (indiceNPC == 15)
            {
                mensaje = "Este es un gran proyecto de ingeniería que requiere su tiempo, lo mismo me muerto antes de que se acabe la obra.";
            }
            else if (indiceNPC == 16)
            {
                mensaje = "Estoy tomando un descanso del trabajo.";
            }
            else if (indiceNPC == 17)
            {
                mensaje = "¿Quién eres tú y por qué me estás hablando?";
            }
            else if (indiceNPC == 18)
            {
                mensaje = "Este paso da a la R7.";
            }
            else if (indiceNPC == 19)
            {
                mensaje = "Este paso da a la R6.";
            }
            else if (indiceNPC == 20)
            {
                mensaje = "Este paso da a la R11.";
            }
            else if (indiceNPC == 21)
            {
                mensaje = "Este paso da a la R3.";
            }
            else if (indiceNPC == 22)
            {
                mensaje = "Esta es la ciudad de Pedrán, la ciudad que nunca duerme, aquí encontrarás de todo lo que necesites.";
            }
            else if (indiceNPC == 23)
            {
                mensaje = "Estoy esperando a unos amigos para ir al Dragón Rojo, pero como siempre llegan todos tarde.";
            }
            else if (indiceNPC == 24)
            {
                mensaje = "La meditación es importante, pero hay que saber relajarse de vez en cuando también.";
            }
            else if (indiceNPC == 25)
            {
                mensaje = "Estoy esperando a alguien, pero no sé porque motivo te lo digo, no es cosa tuya.";
            }
            else if (indiceNPC == 26)
            {
                mensaje = "Me gusta esconderme en los callejones para asustar a la gente.";
            }
            else if (indiceNPC == 27)
            {
                mensaje = "Prefiero apartarme de la ciudad siempre que puedo, el problema es que los caminos son peligrosos hoy día.";
            }
            else if (indiceNPC == 28)
            {
                mensaje = "Estoy buscando una exclusiva para mi periódico. Creemos que hay casos de corrupción en la sede de la Guardia Imperial, ¿no sabrás tú algo?";
            }
            else if (indiceNPC == 29)
            {
                mensaje = "Ese par siempre está discutiendo sin llegar a ponerse de acuerdo.";
            }
            else if (indiceNPC == 30)
            {
                mensaje = "En Pedrán hay tantos locales que nunca se a cuál ir y me quedo aquí todo el tiempo.";
            }
            else if (indiceNPC == 31)
            {
                mensaje = "Nosotros nos encargamos de cuidar de la gente que disfruta en Pedrán. Si necesitas ayuda ve a la sede de la Guardia Imperial y haremos todo lo posible.";
            }
            else if (indiceNPC == 32)
            {
                mensaje = "Necesitaba descansar del laboratorio un poco y tomarme algo por aquí.";
            }
            else if (indiceNPC == 33)
            {
                mensaje = "Esta ciudad ha cambiado mucho desde que yo era joven.";
            }
            else if (indiceNPC == 34)
            {
                mensaje = "Con un gobierno republicano el poder residiría íntegramente en el pueblo, no en una familia que se aprovecha de nosotros y que nos quita el poder de decidir.";
            }
            else if (indiceNPC == 35)
            {
                mensaje = "El emperador se prepara durante toda su vida para llevar al imperio de la manera adecuada y mejorar nuestras vidas. Nuestra misión es ayudar en esa visión.";
            }
            else if (indiceNPC == 36)
            {
                mensaje = "He venido aquí de viaje desde Manfa. Decían que esta era una ciudad increíble pero la mía es mucho más acogedora.";
            }
            else if (indiceNPC == 37)
            {
                bool aceptada = false;
                bool completada = false;
                indiceEleccion = 6;

                if (baseDeDatos.numeroMisionesReclutamiento != 0)
                {
                    for (int i = 0; i < baseDeDatos.numeroMisionesReclutamiento; i++)
                    {
                        if (baseDeDatos.listaMisionesReclutamiento[i].indice == 2)
                        {
                            aceptada = true;
                            /*
                            if (baseDeDatos.listaMisionesReclutamiento[i].subMisionCompletada[0] && baseDeDatos.listaMisionesReclutamiento[i].subMisionCompletada[1] && baseDeDatos.listaMisionesReclutamiento[i].subMisionCompletada[2])
                            {
                                completada = true;
                            }
                            */
                        }
                    }
                }

                emisor = "Carlos";
                mensaje = "En la version completa te ofreceré una misión.";
                /*
                if (!aceptada)
                {
                    mensaje = "Hola amigo, mi nombre es Carlos y tengo un problema muy gordo con el que quizás puedas ayudarme. Hay unos tipos que me están dando problemas y me esperan en mis tabernas favoritas de Pedrán. Si acabaras con esos maleantes te estaría muy agradecido y podría echarte una mano en lo que necesites. ¿Qué te parece?";
                    mision = true;
                    eleccion = true;
                    numeroElecciones = 3;
                    indiceMision = 0;
                }
                else
                {
                    if (completada)
                    {
                        mensaje = "Me he enterado de la que has liado. Menuda paliza le has dado a esos matones así que cuenta conmigo de aquí en adelante.";
                        mision = false;

                        for (int i = 0; i < baseDeDatos.numeroMisionesReclutamiento; i++)
                        {
                            if (baseDeDatos.listaMisionesSecundarias[i].indice == 0)
                            {
                                if (baseDeDatos.listaMisionesReclutamiento[i].completada)
                                {
                                    completada = true;
                                }
                                else
                                {
                                    completada = false;
                                }
                            }
                        }

                        if (!completada)
                        {
                            baseDeDatos.CumpleMision(0);
                        }
                    }
                    else
                    {
                        mensaje = "Esos matones habrán puesto a gente en cada una de las tabernas de Pedrán";
                        mision = false;
                    }
                }
                */
            }
            else if (indiceNPC == 38)
            {
                mensaje = "Los estudiantes deben pasar primero por la sede de la guardia para registrarse.";
            }
            else if (indiceNPC == 39)
            {
                mensaje = "No podemos dejaros pasar hasta que hagáis el registro, lo siento.";
            }
            else if (indiceNPC == 40)
            {
                mensaje = "Los estudiantes deben pasar por la Universidad para la inauguración.";
            }
            else if (indiceNPC == 41)
            {
                mensaje = "No debes perderte la inauguración de la Universidad es de las fechas más remarcables del Imperio.";
            }
            else if (indiceNPC == 42)
            {
                mensaje = "Mi marido me convenció para que viniéramos a esta ciudad de vacaciones y la verdad vaya chasco de viaje, no hay nada interesante que ver a parte de ese enorme mamotreto de piedra.";
            }
            else if (indiceNPC == 43)
            {
                mensaje = "Bienvenido a El Paso, soy su alcalde así que permíteme que te destaque un par de cosas de la ciudad. Lo primero es la variedad de ocio, contamos con 3 posadas con gran actividad. En la esquina noroeste de la ciudad encontrarás al mayor proveedor de material botánico de todo el imperio. A mi espalda encontrarás la sede de la Guardia Imperial. Y finalmente debes ir al sur a ver la gran estatua de Sestae.";
            }
            else if (indiceNPC == 44)
            {
                mensaje = "Sé que todavía soy un novato, pero tengo ambición y quiero llegar a general de la Guardia Imperial.";
            }
            else if (indiceNPC == 45)
            {
                mensaje = "Patrullar por Pedrán es muy estresante hay demasiados maleantes a pesar de la enorme presencia de la Guardia Imperial. No sé porque los jefes no intentan atajar de raíz este problema";
            }
            else if (indiceNPC == 46)
            {
                mensaje = "Estoy vigilando que estos dos no terminen a golpes entre ellos.";
            }
            else if (indiceNPC == 47)
            {
                mensaje = "Me aseguro que no se comentan actos vandálicos contra la estatua. El movimiento antirreligioso ha causado más de un problema en otras ciudades y no queremos eso aquí.";
            }
            else if (indiceNPC == 48)
            {
                mensaje = "Pedí que me trasladaran aquí para estar más cerca de mi familia, pero lo cierto es que echo un poco de menos la emoción de a aventura en tierras lejanas y desconocidas para la mayoría de los de aquí.";
            }
            else if (indiceNPC == 49)
            {
                mensaje = "Aunque seamos del mismo reino poco o nada tiene que ver como son las cosas en mi ciudad, Manfa, a como lo son aquí o en Porto Bello.";
            }
            else if (indiceNPC == 50)
            {
                mensaje = "Cuando el equilibrio se fraguó Sestae adquirió conciencia y decidió probar su poder creando primero la tierra, los mares y el cielo. Entonces vio que aquello era bueno, pero no suficiente para él. Por ello decidió crear la vida tal y como conocemos, pero faltaba algo… Decidió conceder a cada ser una porción de su poder mágico para equilibrar su creación.";
            }
            else if (indiceNPC == 51)
            {
                mensaje = "Cada estatua se hace con un material distinto que representa que Sestae fue el creador de todo y en todo hay una parte de él. Ojalá todos fueran capaces de comprender lo mucho que le debemos al creador.";
            }
        }
        else if (indiceZona == 3) //R10
        {
            if (indiceNPC == 0)
            {
                mensaje = "Estoy de camino a Pedrán en busca de nuevas oportunidades. Desde que Canda se fue a pique debemos buscarnos la vida.";
            }
            else if (indiceNPC == 1)
            {
                mensaje = "Voy camino a la Ciudad Imperial para pedir reunión con el emperador. Quiero hablarle sobre la reconstrucción de Canda y con suerte llevarme un buen pellizco de comisiones con las construcciones.";
            }
            else if (indiceNPC == 2)
            {
                mensaje = "Aún recuerdo cuando Canda era un pueblo apacible y bonito. Los estudiantes iban allí después de sus clases. Rebosaban felicidad y esperanza... No sé qué pasó, pero todo cambió de un día para otro, estaban tan... frustrados. Después ocurrió aquella calamidad.";
            }
            else if (indiceNPC == 3)
            {
                mensaje = "Si no hubiese sido por aquellos 4 estudiantes todos en Canda habríamos muerto. Ellos prometieron hacerse cargo de los responsables y devolver la vida a Canda. Sin embargo, los años han pasado y no se ha vuelto a saber de ellos. Ojalá estén bien.";
            }
            else if (indiceNPC == 4)
            {
                mensaje = "¿Has oído los rumores? Dicen que hay una organización que pretende aprovechar la enfermedad del emperador y la juventud del príncipe heredero para acabar con su reinado y colocarse ellos. Yo apostaría a que el jefe de la Guardia Imperial es el que los lidera, no es de fiar.";
            }
            else if (indiceNPC == 5)
            {
                bool aceptada = false;
                bool completada = false;

                if (baseDeDatos.numeroMisionesReclutamiento != 0)
                {
                    for (int i = 0; i < baseDeDatos.numeroMisionesReclutamiento; i++)
                    {
                        if (baseDeDatos.listaMisionesReclutamiento[i].indice == 3)
                        {
                            aceptada = true;
                            /*
                            if (baseDeDatos.listaMisionesReclutamiento[i].subMisionCompletada[0])
                            {
                                completada = true;
                            }
                            */
                        }
                    }
                }

                emisor = "???";
                mensaje = "En la version completa te ofreceré una misión.";
                /*
                if (!aceptada)
                {
                    mensaje = "Ey tú... Sí tú, no me mires con esa cara de pasmarote. Debo pedirte una cosa, algo que podría cambiar el destino de este imperio para siempre. Tengo pruebas que implican a la Cúpula con altos cargos del gobierno. El problema es que no las tengo físicamente. Ayúdame a recuperarlas y serán de gran ayuda para limpiar este país.";
                    mision = true;
                    indiceMision = 0;
                }
                else
                {
                    if (completada)
                    {
                        mensaje = "Eso ha estado muy bien será un honor ayudarte en tus futuras aventuras.";
                        mision = false;

                        for (int i = 0; i < baseDeDatos.numeroMisionesReclutamiento; i++)
                        {
                            if (baseDeDatos.listaMisionesSecundarias[i].indice == 0)
                            {
                                if (baseDeDatos.listaMisionesReclutamiento[i].completada)
                                {
                                    completada = true;
                                }
                                else
                                {
                                    completada = false;
                                }
                            }
                        }

                        if (!completada)
                        {
                            baseDeDatos.CumpleMision(0);
                        }
                    }
                    else
                    {
                        mensaje = "Ven a hablar conmigo cuando hayas terminado el trabajo.";
                        mision = false;
                    }
                }
                */
            }
            else if (indiceNPC == 6)
            {
                mensaje = "La Resistencia existe, en la Universidad quieren silenciarlo, pero yo creo en ellos. Nos liberarán del yugo del nuevo rector y devolverán la paz al imperio. Ojalá el emperador y sus consejeros creyeran en ellos y les dieran su apoyo.";
            }
        }
        else if (indiceZona == 4) //Canda
        {
            if (indiceNPC == 0) //Nani
            {
                bool aceptada = false;
                bool completada = false;

                if (baseDeDatos.numeroMisionesReclutamiento != 0)
                {
                    for (int i = 0; i < baseDeDatos.numeroMisionesReclutamiento; i++)
                    {
                        if (baseDeDatos.listaMisionesReclutamiento[i].indice == 4)
                        {
                            aceptada = true;

                            if (controlObjetos.guardiaDerrotado && (controlObjetos.orcoDerrotado || controlObjetos.perdonarVidaOrco))
                            {
                                completada = true;
                            }
                        }
                    }
                }

                emisor = "Nani";

                if (!aceptada)
                {
                    mensaje = "Voy a ser directo. Sé quién destruyó este pueblo y quiero venganza. Te he estado vigilando últimamente y sé que eres de fuerte. Ayúdame y no tendrás aliado más fiel que yo.";
                    mision = true;
                    eleccion = true;
                    indiceMision = 4;
                    indiceEleccion = 12;
                }
                else
                {
                    if (completada)
                    {
                        if (controlObjetos.perdonarVidaOrco)
                        {
                            mensaje = "Entiendo… Así que eso ocurrió… Sus crímenes siguen siendo imperdonables pero mis actos no han sido mejores. Mi oferta de unirme a ti sigue en pie y voy a cumplirla, cuenta conmigo para lo que necesites. Quizás así conozca quienes realmente planearon este ataque.";
                        }
                        else
                        {
                            mensaje = "Así que el trabajo ya está hecho... Mi corazón al fin podrá descansar al saber que los responsables pagaron por sus crímenes. Esto no me devolverá a los que se marcharon, pero quizás calme las voces de mi cabeza. Cuenta conmigo para lo que necesites.";
                        }

                        mision = false;

                        baseDeDatos.CumpleMision(4);
                    }
                    else
                    {
                        mensaje = "Ellos nos lo arrebataron todo, va siendo hora de devolverles el favor.";
                        mision = false;
                    }
                }
            }
            else if (indiceNPC == 1)
            {
                mensaje = "La velocidad y la evasión son factores muy a tener en cuenta durante los combates. Una gran velocidad te puede ayudar a golpear en más de una ocasión antes de que el rival sea consciente de que ha ocurrido y la segunda ayuda a esquivar los golpes del rival.";
            }
            else if (indiceNPC == 2)
            {
                mensaje = "Este era el lugar con más encanto de todo el pueblo. Se trataba de una gran taberna donde profesores y estudiantes venían a disfrutar y charlar en armonía. Aún me pregunto de donde saldrían todos esos monstruos y el motivo por el cual atacarían este pequeño pueblo.";
            }
            else if (indiceNPC == 3)
            {
                mensaje = "La Guardia Imperial abandonó este sitio así que somos nosotros quien nos hemos hecho cargo de cuidar del pueblo y de los que aquí quedan.";
            }
            else if (indiceNPC == 4)
            {
                mensaje = "Ojalá se pueda reconstruir el pueblo y vuelva la vida que había aquí.";
            }
            else if (indiceNPC == 5)
            {
                mensaje = "Yo crecí aquí así que me da mucha pena ver como ha quedado todo reducido a escombros.";
            }
            else if (indiceNPC == 6)
            {
                mensaje = "Empiezo a entender por qué me llaman lento. Vine aquí cuando me dijeron que esta era la zona de moda entre los estudiantes.";
            }
            else if (indiceNPC == 7)
            {
                mensaje = "Aún tengo esperanzas en que este pueblo reviva y vuelva a tener el encanto de antes.";
            }
            else if (indiceNPC == 8)
            {
                mensaje = "Los que vivíamos aquí nos vimos forzados a emigrar en busca de nuevas oportunidades. Muchos nos miraron mal al llegar e incluso nos decían que les quitábamos el trabajo. ¿Se creen que nos marchamos por gusto? Ojalá pudiera haber seguido aquí con mi familia en vez de irme.";
            }
            else if (indiceNPC == 9)
            {
                mensaje = "Sigo creyendo en que llegará la ayuda prometida por el emperador. Mientras tanto nosotros seguiremos trabajando para mantenernos unidos y reconstruir el pueblo.";
            }
            else if (indiceNPC == 10)
            {
                mensaje = "Lo que cultivamos apenas llega para cubrir las comidas básicas. Temo cuando llegue el invierno que será de nosotros.";
            }
            else if (indiceNPC == 11)
            {
                mensaje = "Ojalá el dios Sestae se apiade de nosotros y escuche nuestras plegarias. Voy todos los días a rezar a la estatua con la esperanza de que ello ocurra, pero nada ha cambiado.";
            }
            else if (indiceNPC == 12)
            {
                mensaje = "Pertenezco a una organización que presta ayuda médica en los pueblos con más necesidades dentro del imperio. Después del masivo ataque que sufrió esta gente me mandaron a ayudar y desde entonces aquí sigo. Es un trabajo muy duro pero gratificante.";
            }
            else if (indiceNPC == 13)
            {
                mensaje = "Esos primeros hombres son conocidos como los antiguos. Los antiguos surgieron con un gran poder mágico, pero sobre todo mental. Tenían el don de crear artefactos inimaginables hoy día y su civilización avanzó sin que nada pudiera detenerles. Sin embargo, también fueron tremendamente arrogantes y olvidaron pronto a quien les había otorgado esos dones.";
            }
            else if (indiceNPC == 14)
            {
                mensaje = "Nadie atacará el pueblo de Canda mientras nosotros estemos aquí. Este lugar fue un símbolo de la unión entre estudiantes y profesores en la Universidad y no dejaremos que sea profanado de nuevo.";
            }
            else if (indiceNPC == 15)
            {
                controlObjetos.conversacionH = true;
                mensaje = "Según lo que he podido descubrir los atacantes buscaban la entrada a un antiguo templo aquí en Canda. No debía estar ya que no dejaron piedra sin remover y aquí nadie ha visto dicho templo. He oído que en Pedrán vive la mujer más anciana del pueblo. Iré a hablar con ella para que me cuente más sobre este misterioso templo.";
            }
        }
        else if (indiceZona == 5) //Entrada Base Resistencia
        {
            if (indiceNPC == 0)
            {
                mensaje = "La universidad debería ser una experiencia que nunca olvidar para bien, pero ahora se ha convertido en una pesadilla para todas los estudiantes que van.";
            }
            else if (indiceNPC == 1)
            {
                mensaje = "La Resistencia siempre necesita gente comprometida para seguir persiguiendo su meta. Esta batalla no se ganará si no luchamos.";
            }
            else if (indiceNPC == 2)
            {
                mensaje = "Aprovechamos el bosque para esconder la base y protegernos de posibles espías.";
            }
            else if (indiceNPC == 3)
            {
                mensaje = "La potencia de los ataques varía en función de varios factores. Tu fuerza de ataque, físico o mágico, la defensa del rival, la potencia del ataque y finalmente tu equipamiento.";
            }
            else if (indiceNPC == 4)
            {
                mensaje = "En este bosque siempre hay muchos jóvenes, me pregunto por qué será.";
            }
            else if (indiceNPC == 5)
            {
                mensaje = "En la version completa te ofreceré una misión.";
                /*
                if (misionDesbloqueada[5])
                {
                    mensaje = "¿Perteneces a la Resistencia verdad? No hace falte que contestes mi trabajo es saber cosas y sé que te has alistado. El caso es que se me ha asignado espiar a un objetivo pero no lo encuentro por ninguna parte. ¿Me ayudarías?";
                    mision = true;
                    indiceMision = 5;
                }
                else
                {
                    mensaje = "¿Dónde se habrá metido?";
                    mision = false;
                }
                */
            }
            else if (indiceNPC == 6)
            {
                mensaje = "Cuando tengo un descanso en el laboratorio doy un paseo por aquí. La ciencia tiene mucho que aprender de la naturaleza y yo intento aprenderlo.";
            }
            else if (indiceNPC == 7)
            {
                mensaje = "La Resistencia se ordena en cuatro divisiones. La División General, la de Inteligencia, la de Fuerzas Especiales y la del Equipo de Asalto. Cada una tiene unas funciones muy marcadas.";
            }
            else if (indiceNPC == 8)
            {
                mensaje = "Llegado el momento lo normal es que los reclutas decidan a que división quieren pertenecer. Yo opté por la de Inteligencia.";
            }
            else if (indiceNPC == 9)
            {
                if (baseDeDatos.listaMisionesPrincipales[4].indice == 18)
                {
                    mensaje = "Será mejor que te des la vuelta.";
                }
                else
                {
                    mensaje = "Adelante camarada te esperábamos.";
                }
            }
        }
        else if (indiceZona == 6) // Planta Baja Base Resistencia
        {
            if (indiceNPC == 0)
            {
                mensaje = "Los oficiales debemos dar ejemplo con una actitud impoluta, nos jugamos mucho en esta guerra.";
            }
            else if (indiceNPC == 1)
            {
                mensaje = "Estoy haciendo inventario del almacén, aquí hay un montón de cosas que hemos rescatado en alguna misión, armas viejas, tesoros olvidados y otras cosas que no se ni que hacen aquí.";
            }
            else if (indiceNPC == 2)
            {
                mensaje = "Necesito urgentemente unas vacaciones, tantos informes me están volviendo loca.";
            }
            else if (indiceNPC == 3)
            {
                /*
                if (misionDesbloqueada[6])
                {
                    mensaje = "No puedo seguir esperando apoyos de los jefazos. Tengo que pedirte un favor, necesito que vayas a Manfa y me eches una mano allí con una misión especial.";
                    mision = true;
                    indiceMision = 6;
                }
                else
                {
                    mensaje = "Yo me dedico a misiones de exploración en la ciudad de Manfa. Se rumorea que la Cúpula está montando bases allí. He venido aquí para solicitar aliados, sin embargo parece que va para largo";
                    mision = false;
                }
                */
                mensaje = "Yo me dedico a misiones de exploración en la ciudad de Manfa. Se rumorea que la Cúpula está montando bases allí. He venido aquí para solicitar apoyo, sin embargo, parece que va para largo";
            }
            else if (indiceNPC == 4)
            {
                mensaje = "Estos dos del grupo de asalto parece que tengan el cerebro lavado, se lo toman todo demasiado a pecho.";
            }
            else if (indiceNPC == 5)
            {
                mensaje = "Estoy currando en la copistería para sacarme unas cuantas monedas extras y poder mandarlas a casa.";
            }
            else if (indiceNPC == 6)
            {
                mensaje = "La misión que aquí desempeñamos es fundamental para que todos los estudiantes del mundo sean libres.";
            }
            else if (indiceNPC == 7)
            {
                mensaje = "La tienda de suministros de la derecha te proveerá de lo que te haga falta para cumplir con tus misiones sin problema.";
            }
            else if (indiceNPC == 8)
            {
                mensaje = "Sin permiso explícito de la comandante Connor no puedes pasar.";
            }
            else if (indiceNPC == 9)
            {
                mensaje = "El jefe Isser está chalado. Será un genio de la tecnología, pero del resto de cosas no tiene ni idea.";
            }
            else if (indiceNPC == 10)
            {
                mensaje = "En cuanto gane lo suficiente pienso dejar la Resistencia, irme bien lejos del imperio y hacerme músico.";
            }
            else if (indiceNPC == 11)
            {
                mensaje = "Mis misiones favoritas son las de infiltración, hace que me sienta tan viva.";
            }
            else if (indiceNPC == 12)
            {
                mensaje = "Aún no he decidido a que grupo alistarme, ¿cuál crees que me vendría mejor?";
            }
            else if (indiceNPC == 13)
            {
                mensaje = "Existen 3 tipos de objetos principalmente. Los consumibles, los de equipo y los que enseñan habilidades. La división de ciencia se encarga de desarrollarlos en el laboratorio.";
            }
            else if (indiceNPC == 14)
            {
                mensaje = "La cafetería es el mejor sitio para reponerse y lograr objetos curativos para el viaje.";
            }
            else if (indiceNPC == 15)
            {
                mensaje = "Que bien sienta algo caliente de vez en cuando, revitaliza cuerpo y alma.";
            }
            else if (indiceNPC == 16)
            {
                mensaje = "Está siendo un día tranquilo hoy por aquí, cuando hay reunión o un grupo numeroso vuelve de una misión es imposible trabajar aquí.";
            }
            else if (indiceNPC == 17)
            {
                mensaje = "Fuerza y honor, la Resistencia saldrá victoriosa.";
            }
            else if (indiceNPC == 18)
            {
                mensaje = "Bienvenido camarada.";
            }
            else if (indiceNPC == 19)
            {
                mensaje = "Cuanto más ayudes a la División de Inteligencia mejores objetos podremos producir.";
            }
        }
        else if (indiceZona == 7) // Primera Planta Base Resistencia
        {
            if (indiceNPC == 0)
            {
                mensaje = "Gracias al General somos un grupo organizado y tenemos esperanza de derrotar a la Cúpula.";
            }
            else if (indiceNPC == 1)
            {
                mensaje = "Protegeremos al General con nuestras vidas si es necesario.";
            }
            else if (indiceNPC == 2)
            {
                mensaje = "Ni se te ocurra acercarte aquí sin permiso.";
            }
            else if (indiceNPC == 3)
            {
                mensaje = "Busco información de las distintas ciudades que forman el imperio de Áncia.";
            }
            else if (indiceNPC == 4)
            {
                mensaje = "Yo era un aventurero como tu hasta que una flecha me hirió en la rodilla. Ahora me dedico a recopilar información que pueda ser útil de los libros que vamos rescatando en nuestras misiones.";
            }
            else if (indiceNPC == 5)
            {
                mensaje = "Estoy buscando unos libros que me han pedido en el laboratorio.";
            }
            else if (indiceNPC == 6)
            {
                mensaje = "Estoy deseando que llegue la hora del descanso para irme a la cafetería.";
            }
            else if (indiceNPC == 7)
            {
                mensaje = "Estoy ayudando a la bibliotecaria a hacer inventario de los libros que tenemos, pero es un auténtico caos, no hacen más que entrar y salir libros.";
            }
            else if (indiceNPC == 8)
            {
                mensaje = "Si necesitas ayuda médica no dudes en entrar aquí, daremos apoyo a todo aquel que nos necesite.";
            }
            else if (indiceNPC == 9)
            {
                mensaje = "Pertenezco al grupo de diplomacia, tratamos de lograr la paz entre los dos bandos sin más violencia, pero ninguno de los dos sectores parece dispuesto a dar su mano a torcer, seguiremos trabajando duramente por ello.";
            }
            else if (indiceNPC == 10)
            {
                mensaje = "Estás en la primera planta. Aquí se encuentra el centro médico, el despacho del General y la biblioteca.";
            }
            else if (indiceNPC == 11)
            {
                mensaje = "Cada victoria nos aporta experiencia, y esto a su vez nos hace mejorar nuestro nivel de combate. Es importante saber aprovechar estas mejoras para especializarnos del modo que creemos más correcto.";
            }
            else if (indiceNPC == 12)
            {
                mensaje = ".........";
            }
            else if (indiceNPC == 13)
            {
                mensaje = "Aquí vengo a relajarme cuando estoy estresada. El silencio me viene genial.";
            }
            else if (indiceNPC == 14)
            {
                mensaje = "Shhh, hay que guardar silencio en la biblioteca.";
            }
            else if (indiceNPC == 15)
            {
                mensaje = "El general te espera en su despacho.";
            }
            else if (indiceNPC == 16)
            {
                mensaje = "La Resistencia es mi vida, le he dedicado cada pensamiento, cada acción, cada momento. Amo la universidad y lo que representa, por ello no pienso ceder.";
            }
        }
        else if (indiceZona == 8) // Laboratorio Base Resistencia
        {
            if (indiceNPC == 0)
            {
                mensaje = "Yo me encargo de mejorar nuestra producción de artilugios curativos.";
            }
            else if (indiceNPC == 1)
            {
                mensaje = "Mi labor es crear las mejores armas para poder enfrentarnos a la Cúpula.";
            }
            else if (indiceNPC == 2)
            {
                mensaje = "Toda ayuda es poca, si puedes echarnos una mano te estaríamos muy agradecidos.";
            }
            else if (indiceNPC == 3)
            {
                mensaje = "¿Qué a que me dedico? Trato de sintetizar todos los ataques que descubrís en conocimiento que podáis enseñar a otros de manera rápida.";
            }
            else if (indiceNPC == 4)
            {
                mensaje = "Este es el laboratorio de la Resistencia. Aquí nos encargamos de mejorar todo el material que usáis los soldados en vuestras batallas...";
            }
            else if (indiceNPC == 5)
            {
                mensaje = "Nada en la vida es para ser temido, es sólo para ser comprendido. Ahora es el momento de entender más, de modo que podamos temer menos.";
            }
            else if (indiceNPC == 6)
            {
                mensaje = "Trabajamos a destajo para proporcionaros lo mejor de lo mejor, cuantos más avances hagamos menores serán las bajas que debamos lamentar en nuestro bando.";
            }
            else if (indiceNPC == 7)
            {
                mensaje = "Los miembros de la División de Inteligencia nos encargamos de desarrollar nuevo equipamiento y analizar los datos que recopilamos sobre el enemigo.";
            }
            else if (indiceNPC == 8)
            {
                mensaje = "Es cierto que jefe Isser es un poco desquiciante, pero es un gran hombre y daría mi vida por él.";
            }
            else if (indiceNPC == 9)
            {
                mensaje = "Sin la División de Inteligencia la Resistencia habría sucumbido hace mucho.";
            }
            else if (indiceNPC == 10)
            {
                mensaje = "El laboratorio debe ser el lugar donde más seguridad hay de toda la base. Si algo ocurriera este sitio debe resistir o ser arrasado por completo, la Cúpula no debe saber nada de este lugar.";
            }
            else if (indiceNPC == 11)
            {
                mensaje = "Las misiones de robo de información y de obtención de tecnología son claves para lograr avances más rápidos.";
            }
            else if (indiceNPC == 12)
            {
                mensaje = "Puede parecer que nuestra división en la más segura, pero nada más lejos de la verdad. Las misiones que se nos encargan suelen ser muy peligrosas y los experimentos no siempre salen bien.";
            }
            else if (indiceNPC == 13)
            {
                mensaje = "Ten cuidado de no tocar nada, como se entere el comandante Isser te caerá una bronca enorme.";
            }
            else if (indiceNPC == 14)
            {
                mensaje = "Trabajar en la División de Inteligencia es de lo más divertido, adoro probar todos nuestros nuevos proyectos, aunque las pociones a veces saben fatal.";
            }
        }
        else if (indiceZona == 9) //
        {

        }
        else if (indiceZona == 10) //Enfermería
        {
            if (indiceNPC == 0)
            {
                mensaje = "¿Dónde habré metido el maldito libro? Ahora no se si le debo dar una de las pociones rojas, la negra o la verde.";
            }
            else if (indiceNPC == 1)
            {
                mensaje = "Estoy pensando en alistarme al equipo médico, son lo más, aunque me da pánico la sangre.";
            }
            else if (indiceNPC == 2)
            {
                mensaje = "En la enfermería te pueden curar cualquier herida o problema que tengas. Parecen un poco torpes, sin embargo, son los mejores y todo a un módico precio.";
            }
            else if (indiceNPC == 3)
            {
                mensaje = "Tengo que estar muy pendiente de los pacientes, por favor no me distraigas ahora.";
            }
            else if (indiceNPC == 4)
            {
                mensaje = "Aunque parezca que hoy hay mucha gente en verdad estamos bastante tranquilos, ni siquiera se han llenado las camas y creo que al menos uno de los pacientes está fingiendo para saltarse su turno de guardia.";
            }
            else if (indiceNPC == 5)
            {
                mensaje = "Los de la División de Inteligencia nos trajeron al robot médico para echarnos una mano y la verdad es que no ha venido de escándalo. No dábamos a basto y él puede trabajar durante 24 horas sin descanso.";
            }
            else if (indiceNPC == 6)
            {
                mensaje = "En nuestras misiones tratamos de traer todo el material médico posible para ayudar a la enfermería. No podemos comprar muchos recursos ya que llamaría demasiado la atención de la Cúpula.";
            }
            else if (indiceNPC == 7)
            {
                mensaje = "Dirigía una misión con otros tres valerosos soldados. Resultó ser una trampa de la Cúpula y solo pude sacarle a él. Está muy mal herido. Espero que los médicos le puedan salvar.";
            }
            else if (indiceNPC == 8)
            {
                mensaje = "Que pastilla le darías tú, ¿la roja o la azul?";
            }
        }
        else if (indiceZona == 11) //Pueblo Origen
        {
            if (indiceNPC == 0)
            {
                mensaje = "Haces bien en irte a estudiar, el campo es muy duro y cada vez hay menos trabajo. Por suerte aquí en el pueblo es más viable y no se trabaja demasiado.";
            }
            else if (indiceNPC == 1)
            {
                mensaje = "Cuando vuelvas quiero que me cuentes todo lo que veas en tus viajes, que a mí aún no me dejan irme de aquí y mis padres siempre están muy ocupados.";
            }
            else if (indiceNPC == 2)
            {
                mensaje = "¿Estás seguro que quieres irte de aquí? La ciudad es peligrosa y está muy sucia. Esto es mucho más bonito y sano.";
            }
            else if (indiceNPC == 3)
            {
                mensaje = "Tus padres nos han dicho que te vas a la universidad. Seguro que te va todo genial.";
            }
            else if (indiceNPC == 4)
            {
                mensaje = "Según avances en tu viaje conocerás a mucha gente nueva. Es cosa tuya decidir quién quieres que te acompañe o no.";
            }
            else if (indiceNPC == 5)
            {
                mensaje = "Yo también fui a la universidad cuando era joven. Estudiaba para ser médico y fue allí donde conocí a mi difunta esposa. Cuando terminamos la carrera nos mudamos aquí y vivimos felices por siempre. Espero que tú tengas la misma suerte que yo tuve.";
            }
            else if (indiceNPC == 6)
            {
                mensaje = "Mi abuelo siempre está contando batallitas sobre la universidad, a mí nunca me ha llamado mucho así que no creo que vaya. Sin embargo, cuando se enteró que alguien del pueblo si había logrado entrar se ilusionó mucho.";
            }
            else if (indiceNPC == 7)
            {
                mensaje = "No te olvides de venir a vernos de vez en cuando. Rezaremos a Dios para que todo te vaya bien.";
            }
            else if (indiceNPC == 8)
            {
                mensaje = "Hijo mío estoy enormemente orgulloso de ti. Siempre he sabido que tenías algo especial y estoy convencido de que tú marcarás un antes y un después en la vida de mucha gente. Te quiero.";
            }
            else if (indiceNPC == 9)
            {
                mensaje = "Pueblo Origen. Villa de vida y comienzo de caminos.";
            }
            else if (indiceNPC == 10)
            {
                mensaje = "Hay una curiosa leyenda que dice que el Dios Sestae cada ciertos años genera estos cofres para ayudar a los viajeros que realmente lo necesitan para facilitarles el camino.";
            }
        }
        else if (indiceZona == 12) //R5
        {
            if (indiceNPC == 0)
            {
                mensaje = "Si continuas por este camino encontrarás unas hierbas altas en las que suelen esconderse monstruos que podrían atacarte. Si eso ocurre tendrás que usar tus ataques para luchar contra ellos o tratar de huir, aunque esto no siempre es posible.";
            }
            else if (indiceNPC == 1)
            {
                mensaje = "En El Paso encontrarás las cosas básicas para empezar tu viaje y una posada muy agradable para recuperar fuerzas a un buen precio.";
            }
            else if (indiceNPC == 2)
            {
                mensaje = "Estos cofres otorgan objetos para ayudar a los viajeros a hacer su camino más sencillo. Según dicen existen 4 tipos de ellos y cada uno otorga distintos tipos de bonificaciones. Sin embargo, hace falta una llave distinta para abrir cada uno de ellos.";
            }
            else if (indiceNPC == 3)
            {
                mensaje = "Los ataques de Apoyo pueden mejorar tus habilidades o reducir la de tus enemigos. Esto te dará ventaja en los combates más complejos. Por ejemplo, cuanto mayor sea la diferencia entre tu velocidad y la de tu enemigo más veces podrás golpearle aumentando el daño que le haces.";
            }
            else if (indiceNPC == 4)
            {
                mensaje = "Es importante explorar y tomarse las cosas con calma. Nunca sabes si hay algo importante que se te ha pasado por alto si vas demasiado rápido.";
            }
            else if (indiceNPC == 5)
            {
                mensaje = "Para aprender ataques nuevos es necesario encontrar y usar los libros adecuados que te los enseñen. Por desgracia llega un momento que ya no puedes recordar tantas cosas.";
            }
            else if (indiceNPC == 6)
            {
                mensaje = "Debes tener cuidado de no quedarte sin puntos ER en un ataque. Si eso ocurre no podrás lanzar más ese ataque hasta que descanses en alguna posada o uses algún objeto que recupere tus puntos.";
            }
            else if (indiceNPC == 7)
            {
                mensaje = "Ganar combates te hará lograr experiencia y llegado el momento te hará subir de nivel. Esto hará que tus habilidades mejoren progresivamente.";
            } 
            else if (indiceNPC == 8)
            {
                emisor = "Lak'tuk";
                mensaje = "Muchacho, no estoy de humor para hablar así que será mejor que sigas tu camino.";
               
                if (controlObjetos.misionNaniActiva)
                {
                    if (multiplesMensajes)
                    {
                        if (mensajeActual == 1)
                        {
                            mensaje = "Tras estos soldados había un ser encapuchado. Era quien daba las órdenes a aquellos soldados y nos dio dos opciones. Atacar el pueblo de Canda o que todos pereciéramos allí. Lo que elegimos ya puedes hacerte una idea de que fue.";
                        }
                        else if (mensajeActual == 2)
                        {
                            mensaje = "Sin embargo, todo fue una mentira. Los que volvimos del ataque a nuestra aldea comprendimos que no había dos opciones realmente. Los soldados nos esperaban e intentaron matarnos a todos. Solo yo escapé...";
                        }
                        else if (mensajeActual == 3)
                        {
                            eleccion = true;
                            multiplesMensajes = false;
                            indiceMision = 4;
                            indiceEleccion = 11;
                            mensaje = "Desde entonces vago por estas rutas en busca de penitencia por todos mis errores… Quizás sea este el momento de empuñar el arma por última vez. ¿Qué piensas hacer ahora?";
                        }
                    }
                    else
                    {
                        eleccion = true;
                        numeroElecciones = 3;
                        indiceEleccion = 9;
                        indiceMision = 4;
                    }
                }
            }
        }
        else if (indiceZona == 13) //El Paso
        {
            if (indiceNPC == 0)
            {
                mensaje = "El Paso es un sitio con bastante actividad para ser tan pequeño, ya que muchos estudiantes pasamos por aquí camino a la universidad y otros muchos viajeros van camino al palacio. Bastantes incluso se quedan porque es más barato alojarse aquí que no en Pedrán o en Ciudad Imperial.";
            }
            else if (indiceNPC == 1)
            {
                mensaje = "Este camino te llevará a la R6 y se extiende hasta Pedrán. Si tu objetivo es llegar a la Universidad de Áncia te recomiendo que vayas por aquí.";
            }
            else if (indiceNPC == 2)
            {
                mensaje = "Si sigues este camino llegarás a la R4. Ten cuidado en no perderte ya que el camino se bifurca, uno se dirige a la Ciudad Imperial  y el otro va rumbo a el viejo templo de la montaña. Aunque he de decir que debes tener cuidado si coges esta ruta. A pesar de nuestros esfuerzos por asegurar los caminos siguen siendo peligrosos desde la aparición de aquellos monstruos.";
            }
            else if (indiceNPC == 3)
            {
                bool aceptada = false;
                bool completada = false;
                indiceEleccion = 4;

                if (baseDeDatos.numeroMisionesSecundarias != 0)
                {
                    for (int i = 0; i < baseDeDatos.numeroMisionesSecundarias; i++)
                    {
                        if (baseDeDatos.listaMisionesSecundarias[i].indice == 9)
                        {
                            aceptada = true;

                            if (baseDeDatos.listaMisionesSecundarias[i].completada)
                            {
                                completada = true;
                            }
                        }
                    }
                }

                emisor = "Luis";

                if (!aceptada)
                {
                    mensaje = "Permíteme que me presente, mi nombre es Luis y estoy entrenando para acceder a la Guardia Imperial. Tú pareces fuerte y necesito entrenar para mi prueba de acceso a la Guardia Imperial. ¿Tendrías un combate de entrenamiento conmigo?";
                    mision = false;
                    eleccion = true;
                    indiceMision = 9;
                }
                else
                {
                    if (completada)
                    {
                        mensaje = "Debo seguir entrenando para hacerme más fuerte y entrar en la Guardia Imperial. Ella se sentirá orgullosa si lo logro y así podré verle sonreír de nuevo.";
                        eleccion = false;
                        mision = false;
                    }
                    else
                    {
                        mensaje = "¡Vamos allá!";
                        indiceMision = 9;
                        eleccion = false;
                        mision = true;
                    }
                }
            }
            else if (indiceNPC == 4)
            {
                mensaje = "Siempre que necesito un descanso del trabajo vengo aquí a no pensar en nada y simplemente disfrutar de las vistas.";
            }
            else if (indiceNPC == 5)
            {
                mensaje = "Yo era profesor en la Universidad, pero lo dejé cuando la nueva dirección se hizo cargo de ella. No estaba de acuerdo con los métodos que decidieron implementar. Según ellos eran para mejorar la calidad de la educación, pero a mí me parecía que solo lo empeoraban todo.";
            }
            else if (indiceNPC == 6)
            {
                mensaje = "Alquilo camas a los viajeros y así me saco unas cuantas monedas a fin de mes. Siento no poder alquilarte ninguna, pero es que está todo hasta arriba ahora mismo.";
            }
            else if (indiceNPC == 7)
            {
                mensaje = "La dueña de la casa nos trata genial. Sus hijos también fueron a la universidad así que nos tiene un aprecio especial y nos deja quedarnos por un precio casi irrisorio.";
            }
            else if (indiceNPC == 8)
            {
                mensaje = "Estoy planteándome dejar la uni. Las cosas no están saliendo como yo creía y me siento enormemente agobiado. Quizás mi padre tenía razón y lo mejor para mí era quedarme a trabajar con él en la tienda. No sé qué debería hacer.";
            }
            else if (indiceNPC == 9)
            {
                mensaje = "Este va a ser mi primer año en la universidad. Estoy deseando que empiecen las clases.";
            }
            else if (indiceNPC == 10)
            {
                mensaje = "Siempre me quedo embobada mirando el fuego después de comer. Su movimiento es tan hipnótico...";
            }
            else if (indiceNPC == 11)
            {
                mensaje = "La posada de El Paso es el mejor sitio para recuperar fuerzas.";
            }
            else if (indiceNPC == 12)
            {
                mensaje = "Yo llegué a El Paso para solo un par de días y ya llevo 5 años viviendo aquí con mi marido. Él era profesor, pero hace un par de años lo dejó, desde entonces sé que está triste, aunque intente ocultarlo, adoraba su trabajo.";
            }
            else if (indiceNPC == 13)
            {
                mensaje = "Hay negocios que un buen estudiante debe conocer. En la posada puedes recuperar fuerzas, en la armería obtener artículos para el combate. La de suministros provee de objetos consumibles para un gran número de situaciones y en las librerías encontrarás libros donde aprender ataques.";
            }
            else if (indiceNPC == 14)
            {
                mensaje = ".........";
            }
            else if (indiceNPC == 15)
            {
                mensaje = "Adelante, entra a nuestra posada. Mi madre prepara unos platos increíbles que prácticamente revive a un muerto y a un precio increíble.";
            }
            else if (indiceNPC == 16)
            {
                mensaje = "Con esta máquina de mi derecha puedes gestionar a los aliados que te van a acompañar. Puedes incluir o quitar aliados con un máximo de tres miembros contándote a ti mismo.";
            }
            else if (indiceNPC == 17)
            {
                mensaje = "Permíteme que te hable del inicio de nuestros tiempos. Al principio los elementos mágicos luchaban entre si sin control alguno. Del fragor de esta batalla los elementos vieron que aquella lucha carecía de sentido y decidieron unirse en único ser, el Dios Sestae. Él reuniría todos los poderes mágicos y traería el equilibrio.";
            }
            else if (indiceNPC == 18)
            {
                mensaje = "En estas fechas la venida masiva de estudiantes hace que el pueblo requiera de muchos productos frescos y puedo venderlos a muy buen precio. Voy a tener que hacer varios viajes a la granja para aprovechar el tirón al máximo.";
            }
            else if (indiceNPC == 19)
            {
                mensaje = "La universidad es algo que me ilusiona y me da miedo a partes iguales. Es un cambio muy grande que puede definir tu vida en muy poco tiempo y espero haber tomado la decisión correcta.";
            }
            else if (indiceNPC == 20)
            {
                mensaje = "Mi marido será el alcalde y en el pueblo mandará todo lo que él quiera, pero en mi casa mando yo y más le vale tenerlo en cuenta.";
            }
            else if (indiceNPC == 21)
            {
                mensaje = "Bienvenido a El Paso, soy su alcalde así que permíteme que te destaque un par de cosas del pueblo. Al norte del pueblo encontrarás las tiendas, el servicio de portales mágicos y la posada. Al sur se encuentra la R5. Al este la R4 y al oeste la R6. Pasada esa ruta encontrarás la gran ciudad de Pedrán. Ánimo en tu viaje.";
            }
            else if (indiceNPC == 22)
            {
                mensaje = "Homenaje del pueblo El Paso al dios Sestae. Esperamos nos otorgue su protección y su bendición. Ayude así a nuestros jóvenes de camino a su futuro.";
            }
        }
        else if (indiceZona == 14) //Edificios-Pedran
        {
            //Guardia
            if (indiceNPC == 0)
            {
                mensaje = "Estoy acompañando a mi madre para que acabe con unos papeleos burocráticos rápidos que debe hacer. Mañana cumplo mi quinto aniversario aquí esperando creo que ya solo me quedan un par más.";
            }
            else if (indiceNPC == 1)
            {
                mensaje = "Me ha tocado el puesto más aburrido de la historia. Me paso el día aquí de pie sin hacer nada vigilando las moscas porque aquí nunca pasa nada interesante. Cuando me alisté creía que tendría un poco más de acción, pero al parecer no hay dinero para realizar más patrullas por los caminos.";
            }
            else if (indiceNPC == 2)
            {
                mensaje = "Si tienes que realizar alguna gestión coge turno y espera a que te toque.";
            }
            else if (indiceNPC == 3)
            {
                mensaje = "Lo siento, pero solo los miembros de la guardia pueden subir por aquí.";
            }
            else if (indiceNPC == 4)
            {
                mensaje = "Me tienen la obra parada por no sé qué de unos permisos. Pero nadie aquí sabe que permisos son esos, solo que sin los permisos no puedo construir.";
            }
            else if (indiceNPC == 5)
            {
                mensaje = "Me han puesto un multazo por entrar en la ciudad con pociones no reglamentarias. Entre lo que me costaron las pociones y la multa no repito lo juro.";
            }
            else if (indiceNPC == 6)
            {
                mensaje = "Al pardillo ese le hemos incautado unas pociones de importación buenísimas, vaya fiestón nos vamos a pegar esta noche en la garita.";
            }
            else if (indiceNPC == 7)
            {
                mensaje = "Vengo a acompañar al atontado de mi hijo que le ha caído una multa por tontear con pociones ilegales. Verás la bronca que se va a llevar cuando lleguemos a casa.";
            }
            else if (indiceNPC == 8)
            {
                mensaje = "Solo 500 números más y me toca, se nota que es una mañana tranquila.";
            }
            else if (indiceNPC == 9)
            {
                mensaje = "Trabajo como herrero para la Guardia Imperial. Mi material es de primerísima calidad y el precio está más que justificado.";
            }
            else if (indiceNPC == 10)
            {
                mensaje = "Vengo a ver a mi padre. Él es un ejemplo de vida para mí y quiero seguir sus pasos. Por ello me estoy preparando para entrar en la Guardia.";
            }
            //Dragón Rojo
            else if (indiceNPC == 11)
            {
                mensaje = "Estoy esperando a mis colegas. Siempre me hacen lo mismo quedamos a una hora y solo estoy yo. La próxima vez les digo de quedar media hora antes para que estén a la hora.";
            }
            else if (indiceNPC == 12)
            {
                mensaje = "Este bar es mi favorito con diferencia de todo Pedrán. Buen ambiente, la gente que viene a tocar hace música genial, los precios son ajustados... No necesito nada más para ser feliz.";
            }
            else if (indiceNPC == 13)
            {
                mensaje = "Solemos venir aquí a tomar un descanso de vez en cuando y echar unas cartas, hablar o entretenernos de alguna otra forma.";
            }
            else if (indiceNPC == 14)
            {
                mensaje = "No montes jaleo o te echo a patadas de aquí en un momento. La gente de aquí solo se lo quiere pasar bien.";
            }
            else if (indiceNPC == 15) //Matón 
            {

            }
            //Parada
            else if (indiceNPC == 16)
            {
                mensaje = " La vida del profesor es más dura de lo que los alumnos son capaces de concebir. Tenemos una gran responsabilidad en nuestras manos.";
            }
            else if (indiceNPC == 17)
            {
                mensaje = "Si llego a saber que solo venía ella hoy a la posada me habría quedado en mi casa bebiendo tranquilamente.";
            }
            else if (indiceNPC == 18)
            {
                mensaje = " Tomar algo con los amigos es siempre más divertido que sola.";
            }
            else if (indiceNPC == 19)
            {
                mensaje = "¿Ni aquí puedo pasar un rato tranquilo sin que venga un niñato a hacerme preguntas?";
            }
            else if (indiceNPC == 20)//Matón
            {

            }
            else if (indiceNPC == 45)
            {
                emisor = "Guardia";
                mensaje = "¿Es que no puede uno beber tranquilo?";

                if (controlObjetos.misionNaniActiva)
                {
                    eleccion = true;
                    indiceEleccion = 13;
                    indiceMision = 4;
                }
            }
            //Marabunta
            else if (indiceNPC == 21)
            {
                mensaje = "Lo cierto es que nunca tuve muy claro si alistarme o no. No sé me daba nada especialmente bien así que me metí aquí para probar suerte.";
            }
            else if (indiceNPC == 22)
            {
                mensaje = "No tengo nada que decir.";
            }
            else if (indiceNPC == 23)
            {
                mensaje = "Vengo de vez en cuando para asegurarme que mis subordinados no deshonran el buen nombre de la Guardia.";
            }
            else if (indiceNPC == 24)
            {
                mensaje = "Estos sitios están reservados para las reuniones de los jefes.";
            }
            else if (indiceNPC == 25) //Matón
            {

            }
            else if (indiceNPC == 26)
            {
                mensaje = "Él dice que no vale para nada, pero es el más habilidoso de nuestra promoción con diferencia.";
            }
            else if (indiceNPC == 27)
            {
                mensaje = "En mis años mozos llegué a formar parte de la guardia personal del Emperador. Sin embargo, la edad no perdona a nadie y ahora me encargo de entrenar a los futuros soldados del Emperador.";
            }
            //Residencia
            else if (indiceNPC == 28)
            {
                mensaje = "Menos mal que pude pillar esta residencia a tiempo o habría sido un auténtico follón ir y venir de los pueblos cercanos.";
            }
            else if (indiceNPC == 29)
            {
                mensaje = "El dueño de la residencia es una persona maravillosa. Se preocupa un montón por todos nosotros y nos hace sentir como que somos una gran familia.";
            }
            else if (indiceNPC == 30)
            {
                mensaje = "Vivir en Pedrán es una pasada. Todos los días hay alguna fiesta en alguna posada o gente con ganas de salir de excursión por las distintas rutas que rodean a la ciudad.";
            }
            else if (indiceNPC == 31)
            {
                mensaje = "Me estoy planteando volverme a casa. La gran ciudad no está hecha para mí. Me gusta mucho mi pueblecito donde conozco a todo el mundo. Aquí nadie se preocupa por nadie y a nadie le importa si te va bien o mal.";
            }
            else if (indiceNPC == 32)
            {
                mensaje = "Las dos únicas residencias de estudiantes que quedan en Pedrán son las de mi hermano y esta en la que estás que es la mía. Estamos completamente desbordados, pero no permiten la apertura de más cosa que no entiendo.";
            }
            //Resi 2
            else if (indiceNPC == 33)
            {
                mensaje = "Aquí nos ayudamos mucho entre nosotros para estudiar. Entre todos conseguimos terminar las cosas antes que no yendo cada uno por su lado.";
            }
            else if (indiceNPC == 34)
            {
                mensaje = "Este es mi segundo año en la residencia. Si no fuera por este sitio no podría estudiar en la universidad ya que no me puedo permitir los estudios y además pagar el alquiler de una habitación en ningún otro sitio.";
            }
            else if (indiceNPC == 35)
            {
                mensaje = "Como los dueños de ambas residencias son hermanos los días de fiesta quedamos las dos residencias para una gran comida conjunta. Es bastante divertido.";
            }
            else if (indiceNPC == 36)
            {
                mensaje = "Intento dejar mis cosas siempre lo más recogidas posible para no molestar al resto. Somos muchos los que vivimos aquí y es importante no molestarnos los unos a los otros.";
            }
            else if (indiceNPC == 37)
            {
                mensaje = "Algunos nobles están presionando para cerrar las residencias universitarias que quedan. No sé qué traman, pero no será nada bueno seguro.";
            }
            else if (indiceNPC == 38)
            {
                mensaje = "A veces nuestras diferentes creencias generan ciertos roces, pero no por ello dejamos de querernos y respetarnos. Eso es lo más importante en toda relación.";
            }
            else if (indiceNPC == 39)
            {
                mensaje = "La ciencia y la religión son completamente incompatibles, sin embargo, el amor es más fuerte que cualquier diferencia que haya entre las personas.";
            }
            //Casa
            else if (indiceNPC == 40)
            {
                mensaje = "Creo que mi hijo se avergüenza de mí. Se que no soy un ejemplo a seguir y que no he triunfado en la vida. Pero todo lo que hago lo hago por él.";
            }
            else if (indiceNPC == 41)
            {
                mensaje = "No soporto la actitud de mi padre, es un fracasado y apenas logra llegar a casa por las noches. No soporto a los borrachos.";
            }
            //Casa Grande
            else if (indiceNPC == 42)
            {
                mensaje = "La vida de noble es demasiado estresante. Si lo llego a saber nunca me habría casado con la Duquesa.";
            }
            else if (indiceNPC == 43)
            {
                mensaje = "Llevo años trabajando para la familia de la Duquesa y he de decir que siempre me han tratado excelentemente.";
            }
            else if (indiceNPC == 44)
            {
                mensaje = "Quizás en algún momento tenga un trabajo para un joven fuerte como tú.";
            }
        }
        else if (indiceZona == 15)//R6
        {
            if (indiceNPC == 0)
            {
                mensaje = "Lo siento, pero no puedo dejar pasar a nadie sin autorización por aquí. Es una zona muy peligrosa.";
            }
            else if (indiceNPC == 1)
            {
                mensaje = "Me encanta venir aquí a tomar el aire fresco. De vez en cuando necesito desconectar.";
            }
            else if (indiceNPC == 2)
            {
                mensaje = "Cuando era joven mis amigos y yo veníamos aquí a jugar. Cuando lo recuerdo siempre pienso: ¡En qué mierda estarían pensando nuestros padres para dejarnos jugar en un sitio tan peligroso!";
            }
            else if (indiceNPC == 3)
            {
                mensaje = "Nuestras tiendas están abiertas las 24 horas al día, 7 días a la semana, los 12 meses consecutivos, 365 días del año... No lo entiendo, ¿quién puede necesitar una espada demoniaca a las 4 de la mañana de un martes?";
            }
            else if (indiceNPC == 4)
            {
                mensaje = "Estudiar es importante. Si no estudias no podrás aprender los ataques adecuados para defenderte en combate. Además, un libro no se gasta por leerlo.";
            }
            else if (indiceNPC == 5)
            {
                mensaje = "No me pagan lo suficiente para todo lo que tengo que soportar. Los monstruos nos persiguen, los perros nos ladran, las personas se quejan de que tardamos mucho en entregar las cartas... Te juro que al próximo que me ponga una pega le hago tragarse las cartas.";
            }
            else if (indiceNPC == 6)
            {
                mensaje = "Estoy recogiendo muestras para mis próximos experimentos. Antes usábamos a los becarios para esto pero con eso de los derechos de los estudiantes ya no nos dejan enviarlos a tareas tediosas o peligrosas que nada tienen que ver con su educación.";
            }
            else if (indiceNPC == 7)
            {
                mensaje = "Trabajamos mucho a pesar de los tiempos de paz que vivimos. Y pensar que yo me alisté justamente porque creía que no tendría que echar horas de más.";
            }
            else if (indiceNPC == 8)
            {
                mensaje = "Me fio muy poco de los puentes, son de madera, están viejos y pasa muchísima gente a diario. Por ello prefiero mi guardia desde aquí que no tengo que cruzar nada más que uno.";
            }
            else if (indiceNPC == 9)
            {
                mensaje = "Ha habido una serie de problemas y no podemos dejar pasar a nadie. Sentimos las molestias.";
            }
            else if (indiceNPC == 10)
            {
                mensaje = "Si sabes cocinar pociones puedes usar nuestro caldero.";
            }
            else if (indiceNPC == 11)
            {
                mensaje = "Preparar tus propias pociones es un método barato y divertido de generar tus propios recursos cuando vayas de expedición.";
            }
        }
        else if (indiceZona == 16)
        {

        }
        else if (indiceZona == 17) //E. Principal PB
        {
            if (indiceNPC == 0)
            {
                mensaje = "Aunque se trate de solo una réplica de la corona imperial no podemos permitir ningún insulto o muestra de deshonra hacia ella. Ten cuidado con lo que haces muchacho.";
            }
            else if (indiceNPC == 1)
            {
                mensaje = "Madre mía que sablazo me metieron para matricularme aquí y encima lo exponen en público. Esto ya es cachondeo.";
            }
            else if (indiceNPC == 2)
            {
                mensaje = "Esta es sin duda mi pieza de la exposición favorita. Ese dinosaurio gigantesco es algo impresionante y cada vez que me pongo frente a él me imagino un mundo fantástico con criaturas que no podemos ni imaginar.";
            }
            else if (indiceNPC == 3)
            {
                mensaje = "Este sitio siempre me impresiona cuando cruzo sus puertas. Es un sitio tan grande y fascinante... Tantos años de conocimiento y progreso reunidos en un mismo lugar. Creo que muchas veces perdemos la perspectiva y olvidamos lo importante que son estos sitios.";
            }
            else if (indiceNPC == 4)
            {
                mensaje = "*Shhhhh* No hables conmigo, soy una estatua más.";
            }
            else if (indiceNPC == 5)
            {
                mensaje = "A la derecha está el salón de actos y la biblioteca. A la izquierda las oficinas y la tienda de materiales.";
            }
            else if (indiceNPC == 6)
            {
                mensaje = "No entiendo en que estarían pensando los Antiguos con esto del arte moderno. Está claro que esto es un montón de basura inútil.";
            }
            else if (indiceNPC == 7)
            {
                mensaje = "Reggaetonerus Maximus: Ejemplo a tamaño real de una de las especies que poblaban el mundo de los Antiguos y que se cree fue uno de los causantes de su decadencia.";
            }
            else if (indiceNPC == 8)
            {
                mensaje = "T-Rex verde: Se cree que estas bestias legendarias eran mascotas de los poderosos Antiguos. Ellos los sacaban a pasear, los alimentaban y criaban como si fueran parte de su familia.";
            }
            else if (indiceNPC == 9)
            {
                mensaje = "Caldero: Este mágico caldero era el usado por Gregory Hyden padre fundador de la Universidad de Áncia para elaborar sus experimentos.";
            }
            else if (indiceNPC == 10)
            {
                mensaje = "Bandera de la Universidad de Áncia: Símbolo que representa a la Universidad al rededor del mundo.";
            }
            else if (indiceNPC == 11)
            {
                mensaje = "Armadura de los Guardianes: Este atuendo pertenecía a los famosos Guardianes de Áncia. Los Guardianes fueron un cuerpo de sabios guerreros que defendieron al imperio durante la guerra de los 100 años contra el imperio invasor de Frandia.";
            }
            else if (indiceNPC == 12)
            {
                mensaje = "Corona Imperial: Replica de la corona del imperio que representa el poder del Emperador.";
            }
            else if (indiceNPC == 13)
            {
                mensaje = "Casco Universitario: Casco que se le entrega a aquellos que son nombrados caballeros de la universidad. Los mejores entre los mejores de la universidad se les otorga esta distinción.";
            }
            else if (indiceNPC == 14)
            {
                mensaje = "Obra nº 0: Escultura del considerado arte moderno de los antiguos. Su representación queda abierta a cada espectador.";
            }
            else if (indiceNPC == 15)
            {
                mensaje = "Precios bajos: Precio de una matrícula a precio reducido para el ingreso a la universidad.";
            }
            else if (indiceNPC == 16)
            {
                controlObjetos.misionHActiva = true;
                controlObjetos.conversacionH = true;
                mensaje = "El ataque a Canda, la disolución de las asociaciones estudiantiles, la enfermedad del emperador, el enfrentamiento de la religiosas y antirreligiosas… Son demasiados acontecimientos extraños en muy poco tiempo. Esto debería ser investigado en profundidad. Decidido… Iré a Canda a ver si consigo más información.";
            }
            else if (indiceNPC == 17)
            {
                mensaje = "No puedes pasar.";
            }
            else if (indiceNPC == 18)
            {
                mensaje = "Esta armadura parece que vaya a echar a andar en cualquier momento";
            }
            else if (indiceNPC == 19)
            {
                mensaje = "Lo que más me molesta hacer en la universidad es el papeleo. La burocracia aquí siempre es muy liosa.";
            }
            else if (indiceNPC == 20)
            {
                mensaje = "Si tienes que hacer papeleo tienes que ir a la planta de abajo.";
            }
            else if (indiceNPC == 21)
            {
                mensaje = "Para superar las asignaturas deberás realizar una prueba final que será única de esa clase.";
            }
            else if (indiceNPC == 22)
            {
                mensaje = "Mi sueño es convertirme en caballero… pero con mis notas no llego ni a recluta para serte sincero.";
            }
            else if (indiceNPC == 23)
            {
                mensaje = "Me gusta venir de vez en cuando al museo de la universidad. Me ayuda a motivarme de cara al futuro.";
            }
            else if (indiceNPC == 24)
            {
                mensaje = "Este artículo es tan inspirador…";
            }
            else if (indiceNPC == 25)
            {
                mensaje = "Larga vida al emperador. Viva Áncia.";
            }
            else if (indiceNPC == 26)
            {
                mensaje = "Hay demasiado bullicio en toda la planta pese a ser un museo, que poca educación.";
            }
            else if (indiceNPC == 27)
            {
                mensaje = "Estoy intentando convencer a la universidad para que introduzcan un curso para formar herreros mágicos. Creo que es algo que sería muy beneficioso para todos.";
            }
            else if (indiceNPC == 28)
            {
                mensaje = "Deberían montar un puesto de comida o una cafetería aquí en la universidad. Tener que volver a Pedrán o alguno de los otros pueblos que hay cerca para comer algo es bastante engorroso.";
            }
            else if (indiceNPC == 29)
            {
                mensaje = "Yo en verdad no estoy inscrito en la universidad, pero las universitarias me vuelven loco así que vengo de vez en cuando.";
            }
            else if (indiceNPC == 30)
            {
                mensaje = "Se dice que el director es un genio en todos los campos de la magia e incluso se rumorea que podría dar clases este año.";
            }
            else if (indiceNPC == 31)
            {
                mensaje = "Trato de vender artilugios mágicos para la universidad, pero no consigo concertar ninguna reunión con la dirección.";
            }
            else if (indiceNPC == 32)
            {
                mensaje = "A esta universidad viene gente de todas las partes del imperio e incluso gente de otros países.";
            }
            else if (indiceNPC == 33)
            {
                mensaje = "Cuando luchas contra los monstruos salvajes es importante plantearse una buena estrategia, si te lanzas a atacar nada más puede que se vuelva en tu contra.";
            }
            else if (indiceNPC == 34)
            {
                mensaje = "Menos mal que existen los portales mágicos sino venir desde Manfa hasta aquí atravesando el desierto sería una pesadilla.";
            }
            else if (indiceNPC == 35)
            {
                mensaje = "El respeto que infunde decir que eres profesor en esta institución es casi como decir que perteneces a una casa noble. Es fascinante ver el respeto que se nos tiene y a su vez es una gran responsabilidad.";
            }
            else if (indiceNPC == 36)
            {
                mensaje = "Si olvidamos nuestra historia estaremos condenados a repetir nuestros errores. Aquellos que niegan estos acontecimientos y tratan de convencerte de que algunas cosas nunca ocurrieron o que se alteraron para atacar nuestra historia son los más peligrosos terroristas.";
            }
        }
        else if (indiceZona == 18) //E. Principal P1
        {
            if (indiceNPC == 0)
            {
                mensaje = "Son demasiados los libros que nos obligan a estudiar. Como pretenden que saque tiempo para leerme 10 cada semana si el más pequeño tiene 500 páginas. Esta no es manera de aprender.";
            }
            else if (indiceNPC == 1)
            {
                mensaje = "Hace unos días pasó una cosa muy rara. Unos estudiantes estaban hablando de esconder un libro en la R8. Espero que no hablaran en serio.";
            }
            else if (indiceNPC == 2)
            {
                mensaje = "A veces es tanta la presión que sufro cuando hago un examen que siento que me asfixio. Es como si todos esos meses de esfuerzo y sacrificio se fueran a esfumar por una prueba que no dura ni la décima parte del tiempo que le he dedicado.";
            }
            else if (indiceNPC == 3)
            {
                mensaje = "Esta sala de estudio es la que contiene la mayor variedad de textos de todo tipo que puedas encontrar en todo el Imperio, si se le añadiera los archivos privados de los profesores puede que sea la mayor de todo el mundo.";
            }
            else if (indiceNPC == 4)
            {
                mensaje = "Tengo examen mañana y no se ni por dónde empezar. Me voy a llevar un suspenso más grande que yo.";
            }
            else if (indiceNPC == 5)
            {
                mensaje = "Muchas veces me planteo como de responsables somos de que la universidad sea tan difícil como lo es ahora. Muchos de los profesores que catalogamos de crueles y tiranos puede que cuando empezaran a enseñar no fueran así, sino que fueran personas llenas de ilusión a la que la vida ha machacado hasta convertirlos en lo que son ahora.";
            }
            else if (indiceNPC == 6)
            {
                mensaje = "Vengo de vez en cuando aquí en busca de talento que incorporar a mis empresas al rededor del mundo. Quizás tú puedas ser uno de mis futuros grandes fichajes";
            }
            else if (indiceNPC == 7)
            {
                mensaje = "Que ñoña se pone cuando venimos a estudiar juntas. Tengo un examen pronto y no hace más que distraerme.";
            }
            else if (indiceNPC == 8)
            {
                mensaje = "Lo mejor que me ha pasado al venir a la universidad ha sido conocerla a ella. Espero que nuestro amor dure toda la vida.";
            }
            else if (indiceNPC == 9)
            {
                mensaje = "Necesitaba un poco de calma así que por favor no me molestes.";
            }
            else if (indiceNPC == 10)
            {
                mensaje = "Si vengo aquí me siento menos mal por no estudiar. Es un plan sin fisuras...";
            }
            else if (indiceNPC == 11)
            {
                mensaje = "Pensaba que tenía aquí los apuntes de esa semana, pero no los encuentro.";
            }
            else if (indiceNPC == 12)
            {
                mensaje = "Esta es la zona de estudio más grande de toda la Universidad, aquí puedes encontrar casi cualquier libro que se te ocurra y si no lo encuentras puedes encargármelo a mí.";
            }
            else if (indiceNPC == 13)
            {
                mensaje = "La universidad también puede servir para lograr nuevas habilidades que te sirvan en el combate y que no podrás lograr de ninguna otra forma.";
            }
            else if (indiceNPC == 14)
            {
                mensaje = "Esta biblioteca es de las más completas que se pueden acceder públicamente. Es una auténtica gozada venir y descubrir libros nuevos.";
            }
            else if (indiceNPC == 15)
            {
                mensaje = "Yo nunca pude ir a la universidad, las tasas eran demasiado altas para mi familia, pero eso no me frenó y he logrado una buena vida. Lo más importante es darlo todo sin importar las condiciones que te rodean.";
            }
            else if (indiceNPC == 16)
            {
                mensaje = "Estoy buscando mis ganas de estudiar, quizás estén aquí.";
            }
            else if (indiceNPC == 17)
            {
                mensaje = "¿Me ves? Sabía que esta capa de invisibilidad era demasiado barata para ser real.";
            }
            else if (indiceNPC == 18)
            {
                mensaje = "Aún recuerdo mis días en esta biblioteca. Fue aquí donde aprendí casi todo lo que se sobre medicina hoy día. Echo de menos aquellos días.";
            }
            else if (indiceNPC == 19)
            {
                mensaje = "Hay muchas formas de acercarse al Dios Sestae y muchas interpretaciones sobre su mensaje. Sin embargo, el imperio ha promocionado la versión que más le interesa empujándonos a quienes no pensamos igual al ostracismo.";
            }
            else if (indiceNPC == 20)
            {
                mensaje = "Si nos despistamos la Cúpula hará lo que quiera con la universidad… Espera, ¿tú no serás uno de ellos?";
            }
            else if (indiceNPC == 21)
            {
                bool aceptada = false;
                bool completada = false;

                if (baseDeDatos.numeroMisionesReclutamiento != 0)
                {
                    for (int i = 0; i < baseDeDatos.numeroMisionesReclutamiento; i++)
                    {
                        if (baseDeDatos.listaMisionesReclutamiento[i].indice == 20)
                        {
                            aceptada = true;
                            /*
                            if (baseDeDatos.listaMisionesReclutamiento[i].subMisionCompletada[0])
                            {
                                completada = true;
                            }
                            */
                        }
                    }
                }

                emisor = "Tob";

                if (!aceptada)
                {
                    mensaje = "¡Que macabro destino el mío! ¿Cómo ha podido ocurrir esto? Ha desaparecido el único libro de Magia para idiotas. Se que no nos conocemos de nada, pero, ¿podrías ayudarme a encontrarlo? Si lo encuentras te ayudaré en lo que necesites de aquí en adelante.";
                    mision = true;
                    indiceMision = 20;
                }
                else
                {
                    if (completada)
                    {
                        mensaje = "¡Lo has encontrado! Es maravilloso, te debo la vida.Cuenta conmigo en todo lo que necesites amigo mío.";
                        mision = false;

                        for (int i = 0; i < baseDeDatos.numeroMisionesReclutamiento; i++)
                        {
                            if (baseDeDatos.listaMisionesSecundarias[i].indice == 20)
                            {
                                if (baseDeDatos.listaMisionesReclutamiento[i].completada)
                                {
                                    completada = true;
                                }
                                else
                                {
                                    completada = false;
                                }
                            }
                        }

                        if (!completada)
                        {
                            baseDeDatos.CumpleMision(0);
                        }
                    }
                    else
                    {
                        mensaje = "Yo seguiré buscando por aquí, si lo encuentras ven a verme cuanto antes por favor.";
                        mision = false;
                    }
                }
            }
            else if (indiceNPC == 22)
            {
                mensaje = "La Guardia Imperial no tiene jurisdicción aquí. Somos nosotros los encargados de asegurar que se mantiene el orden.";
            }
        }
        else if (indiceZona == 19) //Pueblo Templo
        {
            if (indiceNPC == 0)
            {
                mensaje = "Son demasiados los libros que nos obligan a estudiar. Como pretenden que saque tiempo para leerme 10 cada semana si el más pequeño tiene 500 páginas. Esta no es manera de aprender.";
            }
            else if (indiceNPC == 1)
            {
                mensaje = "Habíamos quedado para estudiar hace un rato pero aún no llega nadie. ¿Me habré equivocado de sitio otra vez?.";
            }
            else if (indiceNPC == 2)
            {
                mensaje = "A veces es tanta la presión que siento cuando hago un examen que siento que me asfixio. Siento que todos esos meses de esfuerzo y sacrificio se van a esfumar por una prueba que no dura ni la decima parte del tiempo que le he dedicado.";
            }
            else if (indiceNPC == 3)
            {
                mensaje = "Esta sala de estudio es la que contiene la mayor variedad de textos de todo tipo que puedas encontrar en todo el Imperio, si se le añadiera los archivos privados de los profesores puede que sea la mayor de todo el mundo.";
            }
            else if (indiceNPC == 4)
            {
                mensaje = "Tengo examen mañana y no se ni por donde empezar. Me voy a llevar un suspenso más grande que yo.";
            }
            else if (indiceNPC == 5)
            {
                mensaje = "Muchas veces me planteo como de responsables somos de que la universidad sea tan difícil como lo es ahora. Muchos de los profesores que catalogamos de crueles y tiranos puede que cuando empezaran a enseñar no fueran así sino que fueran personas llenas de ilusión a la que la vida ha machacado hasta convertirlos en lo que son ahora.";
            }
            else if (indiceNPC == 6)
            {
                mensaje = "Vengo de vez en cuando aquí en busca de talento que incorporar a mis empresas al rededor del mundo. Quizás tú puedas ser uno de mis futuros grandes fichajes";
            }
            else if (indiceNPC == 7)
            {
                mensaje = "Que ñoña se pone cuando venimos a estudiar juntas. Tengo un examen pronto y no hace más que distraerme.";
            }
            else if (indiceNPC == 8)
            {
                mensaje = "Lo mejor que me ha pasado al venir a la universidad ha sido conocerla a ella. Espero que nuestro amor dure toda la vida.";
            }
            else if (indiceNPC == 9)
            {
                mensaje = "Necesitaba un poco de calma así que por favor no me molestes.";
            }
            else if (indiceNPC == 10)
            {
                mensaje = "Si vengo aquí me siento menos mal por no estudiar. Es un plan sin fisuras...";
            }
            else if (indiceNPC == 11)
            {
                bool aceptada = false;
                bool completada = false;

                if (baseDeDatos.numeroMisionesSecundarias != 0)
                {
                    for (int i = 0; i < baseDeDatos.numeroMisionesSecundarias; i++)
                    {
                        if (baseDeDatos.listaMisionesSecundarias[i].indice == 9)
                        {
                            aceptada = true;

                            if (baseDeDatos.listaMisionesSecundarias[i].completada)
                            {
                                completada = true;
                            }
                        }
                    }
                }

                if (!aceptada)
                {
                    mensaje = "Tengo que pedirte un favor. Tú pareces fuerte y necesito entrenar para mi prueba de acceso a la Guardia Imperial. ¿Tendrías un combate de entrenamiento conmigo?";
                    mision = true;
                    indiceMision = 9;
                }
                else
                {
                    if (completada)
                    {
                        mensaje = "Debo seguir entrenando para hacerme más fuerte y entrar en la Guardia Imperial. Ella se sentirá orgullosa si lo logro y así podré verle sonreir de nuevo.";
                        mision = false;
                    }
                    else
                    {
                        mensaje = "¿Estás preparado para luchar?";
                        indiceMision = 9;
                        mision = false;
                    }
                }
            }
        }
        else if (indiceZona == 20) //Ala oeste PB
        {
            if (indiceNPC == 0)
            {
                mensaje = "El profesor Albert es de los difíciles de encontrar. Es atento, explica bien y no nos trata a patadas. Se nota que siente pasión por la enseñanza.";
            }
            else if (indiceNPC == 1)
            {
                mensaje = "¡¿A quién se le ocurre mezclar HierbaDragón con Flores de Hada?! Todo el mundo sabe que son incompatibles.";
            }
            else if (indiceNPC == 2)
            {
                mensaje = "Yo solo quería experimentar combinaciones nuevas. En la universidad deberían potenciar que fuéramos creativos y atrevidos. No que simplemente replicáramos lo que otros ya han hecho.";
            }
            else if (indiceNPC == 3)
            {
                mensaje = "Nunca soy capaz de recordar el horario así que tengo que consultarlo en la pantalla. El problema es que la pantalla hoy no funciona así que estoy sin rumbo.";
            }
            else if (indiceNPC == 4)
            {
                mensaje = "¿Quién ha puesto estas cajas aquí en medio?";
            }
            else if (indiceNPC == 5)
            {
                mensaje = "Estoy cansada de tener clases abarrotadas, alumnos y compañeros desmotivados. Esta universidad necesita un cambio urgente.";
            }
            else if (indiceNPC == 6)
            {
                mensaje = "Están pasando cosas raras en los laboratorios, nos están haciendo trabajar en un proyecto, pero no nos dicen de que se trata. Nos han separado en grupos pequeños de trabajo muy especializados.";
            }
            else if (indiceNPC == 7)
            {
                mensaje = "El sótano está vetado para los alumnos.";
            }
            else if (indiceNPC == 8)
            {
                mensaje = "¿No te llama la atención que haya guardias que bloquean el acceso a según qué zonas? Porque a mí me parece rarísimo la verdad.";
            }
            else if (indiceNPC == 9)
            {
                mensaje = "Ya era hora de que la directiva empleara mano dura, estos jóvenes de hoy en día están demasiado mimados.";
            }
            else if (indiceNPC == 10)
            {
                mensaje = "Antes había agrupaciones de estudiantes que defendían nuestros derechos en la universidad. Sin embargo, con la nueva directiva se prohibieron y fueron obligadas a disolverse. Ahora estamos solos ante el peligro.";
            }
            else if (indiceNPC == 11)
            {
                mensaje = "Aula 0.1";
            }
            else if (indiceNPC == 12)
            {
                mensaje = "Aula 0.2";
            }
            else if (indiceNPC == 13)
            {
                mensaje = "Aula 0.3";
            }
            else if (indiceNPC == 14)
            {
                mensaje = "Aula 0.4";
            }
            else if (indiceNPC == 15)
            {
                mensaje = "Me alegro que me hayan asignado como ayudante del profesor Albert, era la mejor opción sin lugar a dudas.";
            }
            else if (indiceNPC == 16)
            {
                mensaje = "El comienzo de la vida universitaria es bastante complejo. No dejes que eso te desanime y si necesitas cualquier cosa habla conmigo. Es también parte de mi trabajo guiaros en vuestro camino mientras seáis mis alumnos.";
            }
            else if (indiceNPC == 17)
            {
                mensaje = "Para poder enfrentar mi examen deberás superar antes al menos 5 pruebas de Esgrima. Si quieres una explicación de las normas habla con mi ayudante. Es Gabriel, el chico que está sobre el tatami.";
            }
            else if (indiceNPC == 18)
            {
                mensaje = "Voy a convertirme en el mejor esgrimista de todo el imperio.";
            }
            else if (indiceNPC == 19)
            {
                mensaje = "La profesora Lionetta es una leyenda de la esgrima. Incluso los mejores miembros de la Guardia Imperial tendrían difícil igualarla.";
            }
            else if (indiceNPC == 20)
            {
                mensaje = "Te estaré vigilando muy de cerca.";
            }
            else if (indiceNPC == 21)
            {
                mensaje = "Mira que tocarme de asistente del profesor Elric. Con el mal rollo que dan él y su clase.";
            }
            else if (indiceNPC == 22)
            {
                mensaje = "En la esgrima de Áncia hay dos fases muy simples. La primera es ganar un duelo de Piedra-Papel-Tijera. El ganador decide una dirección en la que el perdedor no puede mirar. Si se mira en esa dirección pierdes una ronda, en caso contrario se vuelve a empezar. Si pierdes tres rondas pierdes el combate. Si quieres probar escoge un arma de los estantes.";
            }
            else if (indiceNPC == 23)
            {
                mensaje = "Para poder realizar mi examen deberás demostrar que dominas el arte de crear pociones realizando al menos 5 de la lista. Si quieres una explicación de cómo prepararlas habla con mi ayudante. Es Vaughan, la chica que está elaborando pociones al fondo del aula.";
            }
            else if (indiceNPC == 24)
            {
                mensaje = "Para realizar una poción debes memorizar los pasos a seguir que se te mostrarán al comienzo de cada ronda. Si fallas en algún paso debes de seguir intentándolo desde ese punto sin equivocarte más de tres veces o echarás a perder la receta. Tampoco debes tardar mucho tiempo o se te quemará. Puedes usar cualquier caldero para comenzar a preparar tu poción.";
            }
        }
        else if (indiceZona == 21) //Ala oeste P1
        {
            if (indiceNPC == 0)
            {
                mensaje = "A veces veo a la gente que no estudia y que es feliz con sus vidas, que no tienen unas ojeras gigantes ni agobios y me pregunto qué hago yo aquí realmente.";
            }
            else if (indiceNPC == 1)
            {
                mensaje = "¿Eres de primer año? Pues mucho ánimo, es posiblemente el peor año, pero tranquilo los demás son malos también.";
            }
            else if (indiceNPC == 2)
            {
                mensaje = "No dejes que mi amigo te desanime, la universidad es una experiencia única y te abrirá un montón de puertas.";
            }
            else if (indiceNPC == 3)
            {
                mensaje = "¡Viva la Resistencia!";
            }
            else if (indiceNPC == 4)
            {
                mensaje = "Cuidado con lo que haces, estaremos vigilando.";
            }
            else if (indiceNPC == 5)
            {
                mensaje = "Para mí los estudios son como una red de seguridad. Mi verdadero sueño es ser una estrella de la esgrima como la profesora Lionetta.";
            }
            else if (indiceNPC == 6)
            {
                mensaje = "Al subir a esta planta donde están los despachos de los profesores es como si la felicidad se desvaneciera e hiciera mucho frío.";
            }
            else if (indiceNPC == 7)
            {
                mensaje = "Es importante llevar siempre un buen surtido de objetos encima, nunca sabes cuando te van a hacer falta.";
            }
            else if (indiceNPC == 8)
            {
                mensaje = "Los despachos están cerrados con llave desde que inició el curso, ¿qué habrá pasado?";
            }
            else if (indiceNPC == 9)
            {
                mensaje = "De vez en cuando es bueno salir por Pedrán y desconectar de las clases y la responsabilidad.";
            }
            else if (indiceNPC == 10)
            {
                mensaje = "No entiendo la enorme desconexión que hay entre los jóvenes y nuestra comunidad religiosa. Quizás algunas cosas no estemos haciendo bien.";
            }
            else if (indiceNPC == 11)
            {
                mensaje = "Despacho de Kwolek";
            }
            else if (indiceNPC == 12)
            {
                mensaje = "Despacho de Elric";
            }
            else if (indiceNPC == 13)
            {
                mensaje = "Despacho de Lionetta";
            }
            else if (indiceNPC == 14)
            {
                mensaje = "Despacho de Albert";
            }
        }
    }



    void MensajesPorZonaIngles()
    {
        if (indiceZona == 0) //Universidad
        {
            if (indiceNPC == 0)
            {
                mensaje = "I'm thinking how to optimize the number of rejected students reducing the effort, so don't bother me.";
            }
            else if (indiceNPC == 1)
            {
                mensaje = "I feel the tense atmosphere as if everything was going to explode from one moment to another, I hope to be wrong it has cost me a lot arrive here...";
            }
            else if (indiceNPC == 2)
            {
                mensaje = "The classrooms are almost empty and there are not too many teachers. When they will see that this silly war does not benefit anyone. I know it's an unpopular opinion here, but it's what I really believe.";
            }
            else if (indiceNPC == 3)
            {
                mensaje = "We must not forget those who fought for us. I am sure that in the end we will win.";
            }
            else if (indiceNPC == 4)
            {
                mensaje = "When I put a foot in this place, I get sick.";
            }
            else if (indiceNPC == 5)
            {
                mensaje = "I have rejected 38 students and I only have 10. Surely with this I earn a promotion.";
            }
            else if (indiceNPC == 6)
            {
                mensaje = "We have to take turns protecting the statue of our hero from possible sabotage by the Dome.";
            }
            else if (indiceNPC == 7)
            {
                mensaje = "Today I have a quiet day I only have 23 hours of classes, hopefully I can sleep an hour if I hurry to go home.";
            }
            else if (indiceNPC == 8)
            {
                mensaje = "Winning fights will make you gain experience. When you accumulate enough you will level up and you can improve your skills.";
            }
            else if (indiceNPC == 9)
            {
                mensaje = "When I was young the students made efforts to achieve our goals, now they just think about going party and getting drunk.";
            }
            else if (indiceNPC == 10)
            {
                mensaje = "I have been rejected an exam with a negative grade. How is it possible to have less than 0?";
            }
            else if (indiceNPC == 11)
            {
                mensaje = "I take care of the math department surveillance. They are the most dangerous group of the Dome.";
            }
            else if (indiceNPC == 12)
            {
                mensaje = "Do not trust anyone here, in fact, do not trust me or what I just told you, you never know who can be a spy... Wait, are you a spy?";
            }
            else if (indiceNPC == 13)
            {
                mensaje = "Welcome to the University the place where dreams die.";
            }
            else if (indiceNPC == 14)
            {
                mensaje = "I still remember when they told me that my best years would be when I arrived at the University. What a great lie they told me.";
            }
            else if (indiceNPC == 15)
            {
                mensaje = "Not everyone is made to fight even if it is a good cause.";
            }
            else if (indiceNPC == 16)
            {
                mensaje = "Monument to the hero of the students, the only one who could pass all the subjects with no rejections.";
            }
            else if (indiceNPC == 17)
            {
                bool aceptada = false;
                bool completada = false;
                indiceEleccion = 6;

                if (baseDeDatos.numeroMisionesReclutamiento != 0)
                {
                    for (int i = 0; i < baseDeDatos.numeroMisionesReclutamiento; i++)
                    {
                        if (baseDeDatos.listaMisionesReclutamiento[i].indice == 0)
                        {
                            aceptada = true;

                            if (controlObjetos.contadorMonstruosPedro == 10)
                            {
                                completada = true;
                            }
                        }
                    }
                }

                emisor = "Pedro";

                if (!aceptada)
                {
                    mensaje = "Hi buddy, my name is Pedro and I have a proposal to make you. I love coming to this University and I would like to make it a better place. Help me with a small job and I will collaborate with you.";
                    mision = true;
                    eleccion = true;
                    indiceMision = 0;
                }
                else
                {
                    if (completada)
                    {
                        mensaje = "You made a very good work. It will be an honour to help you in your future adventures.";
                        mision = false;

                        for (int i = 0; i < baseDeDatos.numeroMisionesReclutamiento; i++)
                        {
                            if (baseDeDatos.listaMisionesSecundarias[i].indice == 0)
                            {
                                if (baseDeDatos.listaMisionesReclutamiento[i].completada)
                                {
                                    completada = true;
                                }
                                else
                                {
                                    completada = false;
                                }
                            }
                        }

                        if (!completada)
                        {
                            baseDeDatos.CumpleMision(0);
                        }
                    }
                    else
                    {
                        mensaje = "Come to talk with me when you have finished work.";
                        mision = false;
                    }
                }
            }
            else if (indiceNPC == 18)
            {
                mensaje = "Dedicate a sign to a stone? Who would have come up with such nonsense?";
            }
            else if (indiceNPC == 19)
            {
                mensaje = "Here the students who were rejected during the exams last week rest.";
            }
            else if (indiceNPC == 20)
            {
                mensaje = "Welcome to the Empire of Áncia’s University.";
            }
            else if (indiceNPC == 21)
            {
                mensaje = "DANGER! Department of Mathematics. Stay away.";
            }
            else if (indiceNPC == 22)
            {
                mensaje = "We can't let you pass through here.";
            }
            else if (indiceNPC == 23)
            {
                mensaje = "It is not safe to approach, even we know that.";
            }
            else if (indiceNPC == 24)
            {
                mensaje = "The West Wing is closed until the inauguration.";
            }
            else if (indiceNPC == 25)
            {
                mensaje = "I have not programmed this part yet so turn around there is nothing.";
            }
        }
        else if (indiceZona == 1) //R7
        {
            if (indiceNPC == 0)
            {
                mensaje = "If you cross this bridge you arrive at the University of Áncia. Be careful there, it is a very dangerous area.";
            }
            else if (indiceNPC == 1)
            {
                mensaje = "I think I'm lost again. This forest is quite confusing.";
            }
            else if (indiceNPC == 2)
            {
                mensaje = "This route arrives to R8.";
            }
            else if (indiceNPC == 3)
            {
                mensaje = "Long ago the roads were much safer. With the last emperor this would not have happened.";
            }
            else if (indiceNPC == 4)
            {
                mensaje = "This route arrives to R9.";
            }
            else if (indiceNPC == 5)
            {
                bool aceptada = false;
                bool completada = false;
                indiceEleccion = 8;

                if (baseDeDatos.numeroMisionesReclutamiento != 0)
                {
                    for (int i = 0; i < baseDeDatos.numeroMisionesReclutamiento; i++)
                    {
                        if (baseDeDatos.listaMisionesReclutamiento[i].indice == 1)
                        {
                            aceptada = true;

                            if (controlObjetos.tieneGema)
                            {
                                completada = true;
                            }
                        }
                    }
                }

                emisor = "Gámez";

                if (!aceptada)
                {
                    mensaje = "The forest speaks to me and it has been watching you. He says that you are trustworthy and that we should help each other. There is a guy who has stolen a very valuable object to the forest. Help me recover it before it falls into the wrong hands and I will help you when you need me.";
                    mision = true;
                    eleccion = true;
                    indiceMision = 1;
                }
                else
                {
                    if (completada)
                    {
                        mensaje = "Thanks for saving the forest. He believes that you should be the one to keep that gem and I agree, you have proven to be the most suitable to protect it. I'm indebted to you, count with my axe.";
                        mision = false;

                        baseDeDatos.CumpleMision(1);
                    }
                    else
                    {
                        mensaje = "There are many fronts to fight for. I try to take care of the forest and those who live in it. As I find out that you are doing something to harm him, I will look for you and kill you.";
                        mision = false;
                    }
                }
            }
            else if (indiceNPC == 6)
            {
                mensaje = "The forest is a wonderful place to meditate and be at peace with yourself and nature.";
            }
            else if (indiceNPC == 7)
            {
                mensaje = "I don't want to go back to the University, so I hide here. I don't want to be found. Go before someone sees you.";
            }
            else if (indiceNPC == 8)
            {
                mensaje = "There are 4 different types of chests. The common, the rare, the mythical and the legendary. Everyone needs a different magic key to open it. Anything else you do to try will be useless.";
            }
            else if (indiceNPC == 9)
            {
                mensaje = "I spent all of the month's pay in Pedrán and now I don't have a single coin to buy the equipment I need to train.";
            }
            else if (indiceNPC == 10)
            {
                mensaje = "The Resistance fights for students to be free again. I wish the new emperor would see it and give us the support we need to end this war.";
            }
            else if (indiceNPC == 11)
            {
                mensaje = "I have to come often to the forest because there are many materials that I need for my experiments. I can only find them here.";
            }
            else if (indiceNPC == 12)
            {
                mensaje = "Long ago this road was less intricate and safe, but suddenly everything changed and became labyrinthine and full of thugs. What will have happened?";
            }
            else if (indiceNPC == 13)
            {
                mensaje = "There are 5 elementary types. Geek is strong against Partyman and Sleeper. The Partyman against the Tyrant and the Responsible. The Sleeper against the Partyman and the Tyrant. The Responsible against Geek and Sleeper. And finally, the Tyrant against Geek and the Responsible. All of them are weak against their same type. However, it is said that there is a sixth element the Neutral which is only weak against the Tyrant, but whose attacks are not especially strong against any other.";
            }
            else if (indiceNPC == 14)
            {
                bool activa = false;

                for (int i = 0; i < baseDeDatos.numeroMisionesReclutamiento; i++)
                {
                    if (baseDeDatos.listaMisionesReclutamiento[i].indice == 1)
                    {
                        activa = true;
                    }
                }

                if (activa)
                {
                    bool aceptada = false;
                    bool completada = false;
                    indiceEleccion = 7;

                    if (baseDeDatos.numeroMisionesSecundarias != 0)
                    {
                        for (int i = 0; i < baseDeDatos.numeroMisionesSecundarias; i++)
                        {
                            if (baseDeDatos.listaMisionesSecundarias[i].indice == 21)
                            {
                                aceptada = true;

                                if (jugador.dinero >= 10000)
                                {
                                    baseDeDatos.ApagaPersonajes(0);
                                    completada = true;
                                    jugador.dinero -= 10000;
                                }
                            }
                        }
                    }

                    emisor = "Pícaro";

                    if (!aceptada)
                    {
                        mensaje = "What are you doing here? This chest is ours, so go where you came from or pay the consequences.";
                        mision = true;
                        eleccion = true;
                        numeroElecciones = 3;
                        indiceMision = 21;
                    }
                    else
                    {
                        if (completada)
                        {
                            mensaje = "You have done your part, so we will do ours. Take the Gem before we change our opinion.";
                            mision = false;

                            baseDeDatos.CumpleMision(21);
                        }
                        else
                        {
                            mensaje = "Until you have the full payment, I don't want to see you here.";
                            mision = false;
                        }
                    }
                }
                else
                {
                    mensaje = "Boy you better get out of here, you haven't missed anything.";
                }
            }
        }
        else if (indiceZona == 2) //Pedran
        {
            if (indiceNPC == 0)
            {
                mensaje = "Here ... suffering.";
            }
            else if (indiceNPC == 1)
            {
                mensaje = "There are not enough people working in the field There are not enough people working in the farm.";
            }
            else if (indiceNPC == 2)
            {
                mensaje = "The farm is life, but it takes my life.";
            }
            else if (indiceNPC == 3)
            {
                mensaje = "The market is the best place to get very useful products.";
            }
            else if (indiceNPC == 4)
            {
                mensaje = "Back in my days all this was countryside.";
            }
            else if (indiceNPC == 5)
            {
                mensaje = "If you have any problem you can always count on the Imperial Guard to help you.";
            }
            else if (indiceNPC == 6)
            {
                mensaje = "You are in front of the headquarters of the Imperial Guard.";
            }
            else if (indiceNPC == 7)
            {
                mensaje = "Soldiers also need a break once in a while.";
            }
            else if (indiceNPC == 8)
            {
                mensaje = "I’m... perfect... perfectly... *hip*";
            }
            else if (indiceNPC == 9)
            {
                mensaje = "I needed some fresh air and next to the fountain is always better.";
            }
            else if (indiceNPC == 10)
            {
                mensaje = "There are 3 large inns in Pedrán and it is said that each is associated with a faction. The first one is the Red Dragon which would be full of supporters of the Resistance. The second is the Parada that would be aligned with the Imperial Guard. And the last and largest is the Marabunta aligned with the University.";
            }
            else if (indiceNPC == 11)
            {
                mensaje = "Welcome to the Red Dragon, the place with the most rhythm of all Pedrán.";
            }
            else if (indiceNPC == 12)
            {
                mensaje = "Welcome to the Parada, a place to relax and regain strength.";
            }
            else if (indiceNPC == 13)
            {
                mensaje = "Come to the incredible Marabunta, the biggest inn of all Pedrán.";
            }
            else if (indiceNPC == 14)
            {
                mensaje = "What are you doing here? Don't you see that we are working and you could have an accident? ";
            }
            else if (indiceNPC == 15)
            {
                mensaje = "This is a great engineering project that requires time, maybe I am die before the work is finished.";
            }
            else if (indiceNPC == 16)
            {
                mensaje = "I'm taking a break from work.";
            }
            else if (indiceNPC == 17)
            {
                mensaje = "Who are you and why are you talking to me?";
            }
            else if (indiceNPC == 18)
            {
                mensaje = "This road reaches to R7.";
            }
            else if (indiceNPC == 19)
            {
                mensaje = "This road reaches to R6.";
            }
            else if (indiceNPC == 20)
            {
                mensaje = "This road reaches to R11.";
            }
            else if (indiceNPC == 21)
            {
                mensaje = "This road reaches to R10.";
            }
            else if (indiceNPC == 22)
            {
                mensaje = "This is the city of Pedrán, the city that never sleeps, here you will find everything you need.";
            }
            else if (indiceNPC == 23)
            {
                mensaje = "I'm waiting for some friends to go to the Red Dragon, but as always they arrive late.";
            }
            else if (indiceNPC == 24)
            {
                mensaje = "Meditation is important, but you have to know how to relax from time to time too.";
            }
            else if (indiceNPC == 25)
            {
                mensaje = "I'm waiting for someone, but I don't know why I tell you that, it's not your business.";
            }
            else if (indiceNPC == 26)
            {
                mensaje = "I like to hide in the dark streets to scare people.";
            }
            else if (indiceNPC == 27)
            {
                mensaje = "I prefer to leave the city whenever I can, the problem is that the roads are dangerous today.";
            }
            else if (indiceNPC == 28)
            {
                mensaje = "I am looking for an exclusive to my newspaper. We believe there are cases of corruption at the headquarters of the Imperial Guard, won't you know anything?";
            }
            else if (indiceNPC == 29)
            {
                mensaje = "That couple is always arguing without agreeing.";
            }
            else if (indiceNPC == 30)
            {
                mensaje = " In Pedrán there are so many inns that I never know where to go and I stay here all the time.";
            }
            else if (indiceNPC == 31)
            {
                mensaje = "We take care of the people who enjoy Pedrán. If you need help go to the headquarters of the Imperial Guard and we will do our best.";
            }
            else if (indiceNPC == 32)
            {
                mensaje = "I needed to get some rest from the lab and have a drink around here.";
            }
            else if (indiceNPC == 33)
            {
                mensaje = "This city has changed a lot since I was young.";
            }
            else if (indiceNPC == 34)
            {
                mensaje = "With a republican government the power would reside entirely in the people, not in a family that takes advantage of its power and that takes away the power to decide.";
            }
            else if (indiceNPC == 35)
            {
                mensaje = "The emperor prepares all his life to lead the Empire in the right way and improve our lives. Our mission is to help him in that vision.";
            }
            else if (indiceNPC == 36)
            {
                mensaje = "I have come here on a trip from Manfa. They said that this was an amazing city but mine is more welcoming.";
            }
            else if (indiceNPC == 37)
            {
                mensaje = "In the full version I will offer you a mission.";
            }
            else if (indiceNPC == 38)
            {
                mensaje = "Students must first pass through the headquarters of the guard to register.";
            }
            else if (indiceNPC == 39)
            {
                mensaje = "We can't let you pass until you register yourself, I'm sorry.";
            }
            else if (indiceNPC == 40)
            {
                mensaje = "Students must pass through the University for the inauguration.";
            }
            else if (indiceNPC == 41)
            {
                mensaje = "You should not miss the inauguration of the University is one of the most remarkable days of the Empire.";
            }
            else if (indiceNPC == 42)
            {
                mensaje = "My husband convinced me to come to this city on holidays. What a disaster! There is nothing interesting to see apart from that huge stone statue.";
            }
            else if (indiceNPC == 43)
            {
                mensaje = "Welcome to El Paso, I am the mayor so let me show you a couple of things about the city. The first is the variety of leisure, we have 3 inns with great activity. In the northwest corner of the city you will find the largest supplier of botanical material in the entire Empire. At my back you will find the headquarters of the Imperial Guard. And finally, you must go south to see the great Sestae’s statue.";
            }
            else if (indiceNPC == 44)
            {
                mensaje = "I know that I am still a rookie, but I have ambition and I want to become General of the Imperial Guard.";
            }
            else if (indiceNPC == 45)
            {
                mensaje = "Patrolling in Pedrán is very stressful, there are too many thugs despite of the enormous presence of the Imperial Guard. I don't know why bosses don't try to eliminate this problem.";
            }
            else if (indiceNPC == 46)
            {
                mensaje = "I'm watching that these two don't end up hitting each other.";
            }
            else if (indiceNPC == 47)
            {
                mensaje = "I make sure that no acts of vandalism are committed against the statue. The anti-religious movement has caused more than one problem in other cities and we don't want that here.";
            }
            else if (indiceNPC == 48)
            {
                mensaje = "I asked to be moved here to be closer to my family, but the truth is that I miss the excitement of adventure in distant and unknown lands.";
            }
            else if (indiceNPC == 49)
            {
                mensaje = "Although we are from the same kingdom, little or nothing resembles my city, Manfa, as it is here or in Porto Bello.";
            }
            else if (indiceNPC == 50)
            {
                mensaje = "When the balance was forged Sestae became aware and decided to prove his power. He first created the earth, the seas and the sky. Then he saw that it was good, but not enough for him. That is why he decided to create life as we know, but something was missing... He decided to give each one a portion of his magical power to balance his creation.";
            }
            else if (indiceNPC == 51)
            {
                mensaje = "Each statue is made with a different material that represents that Sestae was the creator of everything and in everything there is a part of it. I wish everyone were able to understand how much we owe the creator.";
            }
        }
        else if (indiceZona == 3) //R10
        {
            if (indiceNPC == 0)
            {
                mensaje = "I am on my way to Pedrán in search of new opportunities. Since Canda was destroyed we must look for what to do.";
            }
            else if (indiceNPC == 1)
            {
                mensaje = "I am on my way to the Imperial City to request a meeting with the emperor. I want to talk about the reconstruction of Canda and hopefully take a good commission with the constructions.";
            }
            else if (indiceNPC == 2)
            {
                mensaje = "I still remember when Canda was a peaceful and beautiful town. The students went there after their classes. They were filled with happiness and hope... I don't know what happened, but everything suddenly changed, they were so... frustrated. Then that calamity happened.";
            }
            else if (indiceNPC == 3)
            {
                mensaje = "If it had not been for those 4 students, everyone in Canda would have died. They promised to take charge of those responsible and return the life to Canda. However, the years have passed and no one has heard from them again. I hope they are well.";
            }
            else if (indiceNPC == 4)
            {
                mensaje = "Have you heard the rumours? They say that there is an organization that aims to take advantage of the illness of the emperor and the youth of the successor prince. They want to end their reign and put themselves on the throne. I would bet that the head of the Imperial Guard is the leader, he is not reliable.";
            }
            else if (indiceNPC == 5)
            {
                bool aceptada = false;
                bool completada = false;

                if (baseDeDatos.numeroMisionesReclutamiento != 0)
                {
                    for (int i = 0; i < baseDeDatos.numeroMisionesReclutamiento; i++)
                    {
                        if (baseDeDatos.listaMisionesReclutamiento[i].indice == 3)
                        {
                            aceptada = true;
                            /*
                            if (baseDeDatos.listaMisionesReclutamiento[i].subMisionCompletada[0])
                            {
                                completada = true;
                            }
                            */
                        }
                    }
                }

                emisor = "???";
                mensaje = "In the full version I will offer you a mission.";
                /*
                if (!aceptada)
                {
                    mensaje = "Hey you... Yes you, don't look at me with that stunned face. I must ask you something, something that could change the destiny of this empire forever. I have evidence that involves the Dome with important members of the government. The problem is that I don't have them physically. Help me to recover them and they will be of great help to clean this country.";
                    mision = true;
                    indiceMision = 0;
                }
                else
                {
                    if (completada)
                    {
                        mensaje = "Eso ha estado muy bien será un honor ayudarte en tus futuras aventuras.";
                        mision = false;

                        for (int i = 0; i < baseDeDatos.numeroMisionesReclutamiento; i++)
                        {
                            if (baseDeDatos.listaMisionesSecundarias[i].indice == 0)
                            {
                                if (baseDeDatos.listaMisionesReclutamiento[i].completada)
                                {
                                    completada = true;
                                }
                                else
                                {
                                    completada = false;
                                }
                            }
                        }

                        if (!completada)
                        {
                            baseDeDatos.CumpleMision(0);
                        }
                    }
                    else
                    {
                        mensaje = "Ven a hablar conmigo cuando hayas terminado el trabajo.";
                        mision = false;
                    }
                }
                */
            }
            else if (indiceNPC == 6)
            {
                mensaje = "The resistance exists. In University they want to silence him, but I believe in them. They will free us from the yoke of the new director and return peace to the empire. I wish the emperor and his advisors would believe in them and give them their support.";
            }
        }
        else if (indiceZona == 4) //Canda
        {
            if (indiceNPC == 0)
            {
                bool aceptada = false;
                bool completada = false;

                if (baseDeDatos.numeroMisionesReclutamiento != 0)
                {
                    for (int i = 0; i < baseDeDatos.numeroMisionesReclutamiento; i++)
                    {
                        if (baseDeDatos.listaMisionesReclutamiento[i].indice == 4)
                        {
                            aceptada = true;

                            if (controlObjetos.guardiaDerrotado && (controlObjetos.orcoDerrotado || controlObjetos.perdonarVidaOrco))
                            {
                                completada = true;
                            }
                        }
                    }
                }

                emisor = "Nani";

                if (!aceptada)
                {
                    mensaje = "I will be direct. I know who destroyed this town and I want revenge. I've been watching you lately and I know you're trustworthy. Help me and you will have no more faithful ally than me.";
                    mision = true;
                    eleccion = true;
                    indiceMision = 4;
                    indiceEleccion = 12;
                }
                else
                {
                    if (completada)
                    {
                        if (controlObjetos.perdonarVidaOrco)
                        {
                            mensaje = "I understand... So that happened ... His crimes remain unforgivable but my actions have not been better. My offer to join you still stands and I will comply, count on me for what you need. Maybe that's how I know who really planned this attack.";
                        }
                        else
                        {
                            mensaje = "So the work is already done ... My heart will finally be able to rest knowing that the perpetrators paid for their crimes. This will not return me to those who left, but it may calm the voices in my head. Count on me for anything you need.";
                        }

                        mision = false;

                        baseDeDatos.CumpleMision(4);
                    }
                    else
                    {
                        mensaje = "They took everything away from us, it's time to return the favour.";
                        mision = false;
                    }
                }
            }
            else if (indiceNPC == 1)
            {
                mensaje = "Speed and evasion are very important factors to take into account during the fighting. A great speed can help you hit on more than one occasion before the opponent is aware that it has happened and the second helps to avoid the blows of the rival.";
            }
            else if (indiceNPC == 2)
            {
                mensaje = "This was the most charming place in the whole town. It was a great inn where teachers and students came to enjoy and chat in harmony. I still wonder where all those monsters would come from and why they would attack this little town.";
            }
            else if (indiceNPC == 3)
            {
                mensaje = "The Imperial Guard abandoned this site so we are the ones who have taken care of the town and those who remain here.";
            }
            else if (indiceNPC == 4)
            {
                mensaje = " I hope the town can be rebuilt and the life that was here comes back.";
            }
            else if (indiceNPC == 5)
            {
                mensaje = "I grew up here so it makes me sad to see how everything has been reduced to rubble.";
            }
            else if (indiceNPC == 6)
            {
                mensaje = "I begin to understand why they call me slow. I came here when they told me that this was the hot spot for students.";
            }
            else if (indiceNPC == 7)
            {
                mensaje = "I still hope that this town revives and has the charm of before.";
            }
            else if (indiceNPC == 8)
            {
                mensaje = "Those of us who lived here were forced to emigrate in search of new opportunities. Many looked at us badly upon arrival and even told us that we took their work. Do you think we left our homes for pleasure? I wish I could have stayed here with my family instead of leaving.";
            }
            else if (indiceNPC == 9)
            {
                mensaje = "I still believe that the help promised by the emperor will come. Meanwhile we will continue working to keep us together and rebuild the town.";
            }
            else if (indiceNPC == 10)
            {
                mensaje = "What we grow is barely enough to cover basic meals. I fear when winter comes that will happen.";
            }
            else if (indiceNPC == 11)
            {
                mensaje = "May God Sestae have mercy on us and listen to our prayers. I go every day to pray to the statue with the hope that it will happen, but nothing has changed.";
            }
            else if (indiceNPC == 12)
            {
                mensaje = "I belong to an organization that provides medical help in the neediest villages within the empire. After the massive attack that these people suffered, they sent me to help and since then I am still here. It is a very hard but rewarding job.";
            }
            else if (indiceNPC == 13)
            {
                mensaje = "Those first men are known as the ancients. The ancients emerged with great magical power, especially mental. They had the gift of creating unimaginable artefacts today and their civilization advanced without anything to stop them. However, they were also tremendously arrogant and soon forgot who had given them those gifts.";
            }
            else if (indiceNPC == 14)
            {
                mensaje = "No one will attack the town of Canda while we are here. This place was a symbol of the union between students and professors at the University and we will not let it be desecrated again.";
            }
            else if (indiceNPC == 15)
            {
                controlObjetos.conversacionH = true;
                mensaje = "According to what I have been able to discover, the attackers were looking for the entrance to an ancient temple here in Canda. They should not have found it since they left no stone unturned and here no one has seen the temple. I have heard that the oldest woman in town now lives in Pedrán. I will go talk to her to tell me more about this mysterious temple.";
            }
        }
        else if (indiceZona == 5) //Entrada Base Resistencia
        {
            if (indiceNPC == 0)
            {
                mensaje = "The university should be an experience that you never forget for good, but now it has become a nightmare for all the students who go.";
            }
            else if (indiceNPC == 1)
            {
                mensaje = "The Resistance always needs committed people to continue pursuing its goal. This battle will not be won if we do not fight.";
            }
            else if (indiceNPC == 2)
            {
                mensaje = "We take advantage of the forest to hide the base and protect ourselves from possible spies.";
            }
            else if (indiceNPC == 3)
            {
                mensaje = "The power of the attacks varies depending on several factors. Your attack strength, physical or magical, the defence of the opponent, the power of the attack and finally your equipment.";
            }
            else if (indiceNPC == 4)
            {
                mensaje = "In this forest there are always many young people, I wonder why it will be.";
            }
            else if (indiceNPC == 5)
            {
                mensaje = "In the full version I will offer you a mission.";
                /*
                if (misionDesbloqueada[5])
                {
                    mensaje = "¿Perteneces a la Resistencia verdad? No hace falte que contestes mi trabajo es saber cosas y sé que te has alistado. El caso es que se me ha asignado espiar a un objetivo pero no lo encuentro por ninguna parte. ¿Me ayudarías?";
                    mision = true;
                    indiceMision = 5;
                }
                else
                {
                    mensaje = "¿Dónde se habrá metido?";
                    mision = false;
                }
                */
            }
            else if (indiceNPC == 6)
            {
                mensaje = "When I have a rest in the laboratory, I take a walk here. Science has a lot to learn from nature and I try to learn it.";
            }
            else if (indiceNPC == 7)
            {
                mensaje = "The Resistance is ordered in four divisions. The General Division, Intelligence, Special Forces and Assault Team. Each one has very marked functions.";
            }
            else if (indiceNPC == 8)
            {
                mensaje = "Each recruit decides which division they want to belong to. I opted for Intelligence.";
            }
            else if (indiceNPC == 9)
            {
                if (baseDeDatos.listaMisionesPrincipales[4].indice == 18)
                {
                    mensaje = "You better turn around.";
                }
                else
                {
                    mensaje = "Come on comrade we were waiting for you.";
                }
            }
        }
        else if (indiceZona == 6) // Planta Baja Base Resistencia
        {
            if (indiceNPC == 0)
            {
                mensaje = "The officers must be an example with an unpolluted attitude, we risk a lot in this war.";
            }
            else if (indiceNPC == 1)
            {
                mensaje = "I am doing inventory of the warehouse, here are a lot of things that we have rescued in some mission, old weapons, forgotten treasures and other things that I do not even know what they do here.";
            }
            else if (indiceNPC == 2)
            {
                mensaje = "I urgently need vacations. So many reports are driving me crazy.";
            }
            else if (indiceNPC == 3)
            {
                /*
                if (misionDesbloqueada[6])
                {
                    mensaje = "No puedo seguir esperando apoyos de los jefazos. Tengo que pedirte un favor, necesito que vayas a Manfa y me eches una mano allí con una misión especial.";
                    mision = true;
                    indiceMision = 6;
                }
                else
                {
                    mensaje = "Yo me dedico a misiones de exploración en la ciudad de Manfa. Se rumorea que la Cúpula está montando bases allí. He venido aquí para solicitar aliados, sin embargo parece que va para largo";
                    mision = false;
                }
                */
                mensaje = " I dedicate myself to exploration missions in the city of Manfa. It is rumoured that the Dome is mounting bases there. I have come here to request support. It seems to go for long.";
            }
            else if (indiceNPC == 4)
            {
                mensaje = "Those two in the assault group seem to have their brain washed, they take it all too seriously.";
            }
            else if (indiceNPC == 5)
            {
                mensaje = "I'm working in the store to get a few extra coins and send them home.";
            }
            else if (indiceNPC == 6)
            {
                mensaje = "The mission we perform here is essential for all students to be free.";
            }
            else if (indiceNPC == 7)
            {
                mensaje = "The supply store will provide you with what you need to complete your missions without problem.";
            }
            else if (indiceNPC == 8)
            {
                mensaje = "Without the explicit permission of Commander Connor you cannot pass.";
            }
            else if (indiceNPC == 9)
            {
                mensaje = "Chief Isser is crazy. He will be a technology genius, but he has no idea of the rest of the things.";
            }
            else if (indiceNPC == 10)
            {
                mensaje = "As soon as I win enough money, I plan to leave the Resistance. I will go away from the empire and become a musician.";
            }
            else if (indiceNPC == 11)
            {
                mensaje = "My favourite missions are infiltration, it makes me feel so alive.";
            }
            else if (indiceNPC == 12)
            {
                mensaje = "I have not yet decided which group to enlist, which one do you think would be better for me?";
            }
            else if (indiceNPC == 13)
            {
                mensaje = "There are 3 types of objects mainly. Consumables, equipment and those who teach skills. The science division is responsible for developing them in the laboratory.";
            }
            else if (indiceNPC == 14)
            {
                mensaje = "The café is the best place to recover and exchange stories.";
            }
            else if (indiceNPC == 15)
            {
                mensaje = "That feels good warm every once in a while, revitalizes body and soul.";
            }
            else if (indiceNPC == 16)
            {
                mensaje = "It's a quiet day here today, when there is a meeting or a large group returns from a mission it is impossible to work here.";
            }
            else if (indiceNPC == 17)
            {
                mensaje = "Strength and honour, the Resistance will be victorious.";
            }
            else if (indiceNPC == 18)
            {
                mensaje = "Welcome comrade.";
            }
            else if (indiceNPC == 19)
            {
                mensaje = "The more you help the Intelligence Division the better objects we can produce.";
            }
        }
        else if (indiceZona == 7) // Primera Planta Base Resistencia
        {
            if (indiceNPC == 0)
            {
                mensaje = " Thanks to the General we are an organized group and we hope to defeat the Dome.";
            }
            else if (indiceNPC == 1)
            {
                mensaje = "We will protect the General with our lives if it is necessary.";
            }
            else if (indiceNPC == 2)
            {
                mensaje = "Don't even think about coming here without permission.";
            }
            else if (indiceNPC == 3)
            {
                mensaje = "I am looking for information on the different cities that form the empire of Áncia.";
            }
            else if (indiceNPC == 4)
            {
                mensaje = "I was an adventurer like you until an arrow hurt my knee. Now I am dedicated to collecting information that may be useful from the books that we are rescuing in our missions.";
            }
            else if (indiceNPC == 5)
            {
                mensaje = "I'm looking for some books that I have been asked in the laboratory.";
            }
            else if (indiceNPC == 6)
            {
                mensaje = " I'm looking forward to the break time to go to the café.";
            }
            else if (indiceNPC == 7)
            {
                mensaje = "I am helping the librarian to make inventory of the books we have, but it is a real chaos, they do not stop entering and leaving books.";
            }
            else if (indiceNPC == 8)
            {
                mensaje = "If you need medical help, do not hesitate to enter here, we will support everyone who needs us.";
            }
            else if (indiceNPC == 9)
            {
                mensaje = "I belong to the diplomacy group, we try to achieve peace without further violence, but neither sector seems willing to stop. We will continue working hard for it.";
            }
            else if (indiceNPC == 10)
            {
                mensaje = "You are on the first floor. Here is the medical center, the General's office and the library.";
            }
            else if (indiceNPC == 11)
            {
                mensaje = "Each victory brings us experience, and this in turn makes us improve our level of combat. It is important to know how to take advantage of these improvements to specialize in the way we think is most correct.";
            }
            else if (indiceNPC == 12)
            {
                mensaje = ".........";
            }
            else if (indiceNPC == 13)
            {
                mensaje = "Here I come to relax when I'm stressed. The silence is great for me.";
            }
            else if (indiceNPC == 14)
            {
                mensaje = "Shhh, you have to keep quiet in the library.";
            }
            else if (indiceNPC == 15)
            {
                mensaje = "The general awaits you in his office.";
            }
            else if (indiceNPC == 16)
            {
                mensaje = "Resistance is my life, I have dedicated every thought, every action, every moment. I love the University and what it represents, so I will not give in.";
            }
        }
        else if (indiceZona == 10) //Enfermería
        {
            if (indiceNPC == 0)
            {
                mensaje = "Where will I have put the damn book? Now I don't know if I should give him one of the red, black or green potions.";
            }
            else if (indiceNPC == 1)
            {
                mensaje = "I am thinking of joining the medical team, they are the best. The problem is that it scares me to see blood.";
            }
            else if (indiceNPC == 2)
            {
                mensaje = "In the infirmary they can cure you any wound or problem that you have. They seem a bit useless, however, they are the best and all at a reasonable price.";
            }
            else if (indiceNPC == 3)
            {
                mensaje = "I have to be very aware of patients, please do not distract me now.";
            }
            else if (indiceNPC == 4)
            {
                mensaje = "Although it seems that there are a lot of people today, we are really quite calm, the beds have not even been filled and I think that at least one of the patients is pretending to skip his guard duty.";
            }
            else if (indiceNPC == 5)
            {
                mensaje = "Those of the Intelligence Division brought us the medical robot to lend us a hand and the truth is that it has not served much. We could not with everything and he can work for 24 hours without rest.";
            }
            else if (indiceNPC == 6)
            {
                mensaje = "In our missions we try to bring all possible medical equipment to help the infirmary. We cannot buy many resources because it would attract too much attention from the Dome.";
            }
            else if (indiceNPC == 7)
            {
                mensaje = " I led a mission with three other courageous soldiers. It turned out to be a Dome trap and I could only get him out. He is very badly hurt. I hope doctors can save him.";
            }
            else if (indiceNPC == 8)
            {
                mensaje = "What pill would you give him, the red one or the blue one?";
            }
        }
        else if (indiceZona == 11) //Pueblo Origen
        {
            if (indiceNPC == 0)
            {
                mensaje = "It is a good idea to go to study, the farm is very hard and there is less and less work. Luckily here in this town is more viable and you don't work too much.";
            }
            else if (indiceNPC == 1)
            {
                mensaje = "When you come back, I want you to tell me everything you see on your trips. My parents don't let me go and they are always very busy.";
            }
            else if (indiceNPC == 2)
            {
                mensaje = "Are you sure you want to leave the town? The city is dangerous and very dirty. This is much prettier and healthier.";
            }
            else if (indiceNPC == 3)
            {
                mensaje = "Your parents have told us that you are going to the university. Surely everything is going great for you.";
            }
            else if (indiceNPC == 4)
            {
                mensaje = " As you progress on your trip you will meet many new people. It's up to you to decide who you want to follow you or not.";
            }
            else if (indiceNPC == 5)
            {
                mensaje = "I also went to the university when I was young. I was studying to be a doctor and it was there when I met my late wife. When we finished our studies, we moved here and lived happily. I hope you have the same fate that I had.";
            }
            else if (indiceNPC == 6)
            {
                mensaje = "My grandfather is always telling stories about the university, I don’t like studying so I don't think I will go. However, when he discovered that someone from the town had been able to enter, he was very excited.";
            }
            else if (indiceNPC == 7)
            {
                mensaje = "Don't forget to come see us once in a while. We will pray to God so that everything goes well for you.";
            }
            else if (indiceNPC == 8)
            {
                mensaje = "My son, I am so proud of you. I have always known that you had something special and I am convinced that you will change the lives of many people. I love you.";
            }
            else if (indiceNPC == 9)
            {
                mensaje = "Origin Town. Village of life and beginning of travellers.";
            }
            else if (indiceNPC == 10)
            {
                mensaje = "There is a curious legend that says that God Sestae in certain years generates these chests to help travellers who really need it to make their way easier.";
            }
        }
        else if (indiceZona == 12) //R5
        {
            if (indiceNPC == 0)
            {
                mensaje = "If you continue along this road you will find tall grass. These usually hide monsters that could attack you. If that happens you will have to use your attacks to fight them or try to run away, although this is not always possible.";
            }
            else if (indiceNPC == 1)
            {
                mensaje = "In El Paso you will find the basic things to start your trip and a very nice inn to regain strength at a good price.";
            }
            else if (indiceNPC == 2)
            {
                mensaje = "These chests gives items to help travellers make their way easier. People say there are 4 types of them and each one gives different types of bonuses. However, a different key is required to open each of them.";
            }
            else if (indiceNPC == 3)
            {
                mensaje = "Support attacks can improve your abilities or reduce that of your enemies. This will give you an advantage in the most complex combats. For example, the greater the difference between your speed and that of your enemy, the more times you can hit him by increasing the damage you do to him.";
            }
            else if (indiceNPC == 4)
            {
                mensaje = "It is important to explore and take things easy. You never know if there is something important that you have missed if you go too fast.";
            }
            else if (indiceNPC == 5)
            {
                mensaje = " To learn new attacks it is necessary to find and use the right books that teach them. Unfortunately, there comes a time that you can no longer remember so many things.";
            }
            else if (indiceNPC == 6)
            {
                mensaje = "You must be careful not to run out of ER points in an attack. If that happens you will not be able to launch that attack until you rest in an inn or use an object that recovers your points.";
            }
            else if (indiceNPC == 7)
            {
                mensaje = "Winning combats will make you gain experience and when the time comes you will level up. This will make your skills progressively better. ";
            }
            else if (indiceNPC == 8)
            {
                emisor = "Lak'tuk";
                mensaje = "Boy, I'm not in the mood to talk so you better go away.";

                if (controlObjetos.misionNaniActiva)
                {
                    if (multiplesMensajes)
                    {
                        if (mensajeActual == 1)
                        {
                            mensaje = "Behind these soldiers was a hooded person. It was he who gave the orders to those soldiers and gave us two options. Attack the town of Canda or we would all die there. What we choose you can already get an idea of what it was.";
                        }
                        else if (mensajeActual == 2)
                        {
                            mensaje = "However, it was all a lie. Those who returned from the attack on our village understood that there were not really two options. The soldiers were waiting for us and tried to kill us all. Only I escaped...";
                        }
                        else if (mensajeActual == 3)
                        {
                            eleccion = true;
                            multiplesMensajes = false;
                            indiceMision = 4;
                            indiceEleccion = 11;
                            mensaje = "Since then I wander these routes in search of penance for all my mistakes... Perhaps this is the time to wield the weapon for the last time. What will you do now?";
                        }
                    }
                    else
                    {
                        eleccion = true;
                        numeroElecciones = 3;
                        indiceEleccion = 9;
                        indiceMision = 4;
                    }
                }
            }
        }
        else if (indiceZona == 13) //El Paso
        {
            if (indiceNPC == 0)
            {
                mensaje = "El Paso is a place with enough activity to be so small. There are many students who pass here on the way to the university and many other travellers are on their way to the palace. Enough even stay because it is cheaper to stay here than in Pedrán or in Imperial City.";
            }
            else if (indiceNPC == 1)
            {
                mensaje = "This road will take you to the R6 and extends to Pedrán. If your goal is to arrive at the University of Áncia I recommend you go here.";
            }
            else if (indiceNPC == 2)
            {
                mensaje = "If you follow this path you will reach R4. Be careful not to get lost as the road forks, one goes to the Capital city and the other goes to the old mountain temple. Although I have to say that you must be careful if you take this route. Despite our efforts to ensure the roads remain dangerous since the appearance of those monsters.";
            }
            else if (indiceNPC == 3)
            {
                bool aceptada = false;
                bool completada = false;
                indiceEleccion = 4;

                if (baseDeDatos.numeroMisionesSecundarias != 0)
                {
                    for (int i = 0; i < baseDeDatos.numeroMisionesSecundarias; i++)
                    {
                        if (baseDeDatos.listaMisionesSecundarias[i].indice == 9)
                        {
                            aceptada = true;

                            if (baseDeDatos.listaMisionesSecundarias[i].completada)
                            {
                                completada = true;
                            }
                        }
                    }
                }

                emisor = "Luis";

                if (!aceptada)
                {
                    mensaje = "Let me introduce myself, my name is Luis and I am training to access the Imperial Guard. You seem strong and I need to train for my entrance test to the Imperial Guard. Would you have a training match with me?";
                    mision = false;
                    eleccion = true;
                    indiceMision = 9;
                }
                else
                {
                    if (completada)
                    {
                        mensaje = "I must continue training to become stronger and enter the Imperial Guard. She will be proud if I make it and so I can see her smile again.";
                        eleccion = false;
                        mision = false;
                    }
                    else
                    {
                        mensaje = "Let’s do it!";
                        indiceMision = 9;
                        eleccion = false;
                        mision = true;
                    }
                }
            }
            else if (indiceNPC == 4)
            {
                mensaje = "Whenever I need a break from work, I come here to not think and just enjoy the views.";
            }
            else if (indiceNPC == 5)
            {
                mensaje = "I was a professor at the University, but I quit when the new director took care of her. I did not agree with the methods they decided to implement. According to them they were to improve the quality of education, but it seemed to me that they only made everything worse.";
            }
            else if (indiceNPC == 6)
            {
                mensaje = "I rent beds to travellers and I get a few coins at the end of the month. I'm sorry I can't rent you any, but it's that everything is up right now.";
            }
            else if (indiceNPC == 7)
            {
                mensaje = "The owner of the house treats us great. Her children also went to the University so she loves us and lets us stay for a ridiculous price.";
            }
            else if (indiceNPC == 8)
            {
                mensaje = "I am considering leaving the University. Things are not going as I thought and I feel greatly overwhelmed. Maybe my father was right and it was best for me to stay with him in the store. I don't know what I should do.";
            }
            else if (indiceNPC == 9)
            {
                mensaje = "This is going to be my first year at the University. I want classes to start.";
            }
            else if (indiceNPC == 10)
            {
                mensaje = "I always stare at the fire after eating. His movement is very hypnotic...";
            }
            else if (indiceNPC == 11)
            {
                mensaje = "El Paso’s inn is the best place to regain strength.";
            }
            else if (indiceNPC == 12)
            {
                mensaje = "I arrived in El Paso for only a couple of days and I have been living here with my husband for 5 years. He was a teacher, but some time ago he left his job, since then I know he is sad, although he tries to hide it, he loved his work.";
            }
            else if (indiceNPC == 13)
            {
                mensaje = "There are businesses that a good student should know. In the inn you can regain strength, in the armoury get items for combat. The supply gives you consumables for a large number of situations and in bookstores you will find books where to learn attacks.";
            }
            else if (indiceNPC == 14)
            {
                mensaje = ".........";
            }
            else if (indiceNPC == 15)
            {
                mensaje = "Go ahead, enter our inn. My mother prepares an incredible meal that practically revives a dead man and at an incredible price.";
            }
            else if (indiceNPC == 16)
            {
                mensaje = "With this machine on my right you can manage the allies that will accompany you. You can include or remove allies with a maximum of three two members and you.";
            }
            else if (indiceNPC == 17)
            {
                mensaje = "Let me tell you about the beginning of our times. At first the magical elements fought each other without any control. From the heat of this battle, the elements saw that this fight was meaningless and decided to unite in one being, the God Sestae. He would gather all the magical powers and bring the balance.";
            }
            else if (indiceNPC == 18)
            {
                mensaje = "In these days the massive coming of students makes the town require many fresh products and I can sell them at a very good price. I will have to make several trips to the farm to make the most of it.";
            }
            else if (indiceNPC == 19)
            {
                mensaje = "University is something that excites me and scares me equally. It is a very big change that can define your life in a very short time and I hope I have made the right decision.";
            }
            else if (indiceNPC == 20)
            {
                mensaje = "My husband is the mayor and in town he will command everything he wants, but in my house, I command and it is better to keep it in mind.";
            }
            else if (indiceNPC == 21)
            {
                mensaje = "Welcome to El Paso, I'm the mayor so let me tell you a couple of things about the town. To the north of the town you will find the shops, the magic portals service and the inn. To the south is the R5. To the east the R4 and to the west the R6. After that route you will find the great city of Pedrán. Cheer up on your trip.";
            }
            else if (indiceNPC == 22)
            {
                mensaje = "Tribute of the El Paso people to the God Sestae. We hope you give us your protection and your blessing. Help our young people on their way to their future.";
            }
        }
        else if (indiceZona == 14) //Edificios-Pedran
        {
            //Guardia
            if (indiceNPC == 0)
            {
                mensaje = "I am accompanying my mother to finish some quick bureaucratic paperwork that she must do. Tomorrow I am celebrating my fifth anniversary here waiting. I think I only have one more pair left.";
            }
            else if (indiceNPC == 1)
            {
                mensaje = "The most boring position in history has touched me. I spend the day standing here doing nothing watching flies because nothing interesting ever happens here. When I joined, I thought I would have a little more action, but apparently there is no money to send more patrols on the roads.";
            }
            else if (indiceNPC == 2)
            {
                mensaje = "If you have to do some management, take a turn and wait for it to touch you.";
            }
            else if (indiceNPC == 3)
            {
                mensaje = "I'm sorry, but only guard members can go upstairs.";
            }
            else if (indiceNPC == 4)
            {
                mensaje = "The construction is stopped because some permits are missing. But nobody here knows what permissions are those, only that without the permissions I can't build.";
            }
            else if (indiceNPC == 5)
            {
                mensaje = "I have been fined for entering the city with illegal potions. Between what the potions cost me and the fine I don't repeat it I swear.";
            }
            else if (indiceNPC == 6)
            {
                mensaje = "We have seized some import potions. We're going to have a party tonight in the bedrooms.";
            }
            else if (indiceNPC == 7)
            {
                mensaje = "I come to accompany the idiot of my son who has dropped a fine for fooling around with illegal potions. You will see the scolding that is going to take when we get home.";
            }
            else if (indiceNPC == 8)
            {
                mensaje = "Just 500 more numbers and it's my turn. It's a quiet morning.";
            }
            else if (indiceNPC == 9)
            {
                mensaje = "I work as a blacksmith for the Imperial Guard. My material is of the highest quality and the price is more than justified.";
            }
            else if (indiceNPC == 10)
            {
                mensaje = "I come to see my father. He is an example of life for me and I want to follow his footsteps. That is why I am preparing to enter the Guard.";
            }
            //Dragón Rojo
            else if (indiceNPC == 11)
            {
                mensaje = "I am waiting for my colleagues. They always do the same to me. We meet for an hour and only I arrive. Next time I tell them to stay half an hour earlier so they are on time.";
            }
            else if (indiceNPC == 12)
            {
                mensaje = "This tavern is my favourite of all Pedrán. Good atmosphere, the people who come to play make great music, the prices are tight ... I don't need anything else to be happy.";
            }
            else if (indiceNPC == 13)
            {
                mensaje = "We usually come here to take a break from time to time and play with cards, talk or entertain in some other way.";
            }
            else if (indiceNPC == 14)
            {
                mensaje = "Don't make a fuss or I'll kick you out of here in a moment. The people here just want to have fun.";
            }
            else if (indiceNPC == 15) //Matón 
            {

            }
            //Parada
            else if (indiceNPC == 16)
            {
                mensaje = "The teacher's life is harder than students are able to conceive. We have a great responsibility in our hands.";
            }
            else if (indiceNPC == 17)
            {
                mensaje = "If I had known that only she was coming to the inn today I would have stayed at home drinking quietly.";
            }
            else if (indiceNPC == 18)
            {
                mensaje = "Drinking something with friends is always more fun than alone.";
            }
            else if (indiceNPC == 19)
            {
                mensaje = "Not even here can I have a quiet time without a child coming to ask me questions?";
            }
            else if (indiceNPC == 20)//Matón
            {

            }
            else if (indiceNPC == 45)
            {
                emisor = "Guardian";
                mensaje = "Can't one drink calmly?";

                if (controlObjetos.misionNaniActiva)
                {
                    eleccion = true;
                    indiceEleccion = 13;
                    indiceMision = 4;
                }
            }
            //Marabunta
            else if (indiceNPC == 21)
            {
                mensaje = "The truth is that I was never quite sure if enlist myself or not. I wasn't especially good at anything so I got here to try my luck.";
            }
            else if (indiceNPC == 22)
            {
                mensaje = "I have nothing to say.";
            }
            else if (indiceNPC == 23)
            {
                mensaje = "I come from time to time to make sure that my subordinates do not dishonour the good name of the Guard.";
            }
            else if (indiceNPC == 24)
            {
                mensaje = "These sites are reserved for boss meetings.";
            }
            else if (indiceNPC == 25) //Matón
            {

            }
            else if (indiceNPC == 26)
            {
                mensaje = "He says it is worthless, but he is the most skilled of our promotion";
            }
            else if (indiceNPC == 27)
            {
                mensaje = "In my young years I became part of the Emperor's personal guard. However, age does not forgive anyone and now I am in charge of training the future soldiers of the Emperor.";
            }
            //Residencia
            else if (indiceNPC == 28)
            {
                mensaje = "Luckily I could catch this residence on time or I would have been a real mess to come and go from nearby towns.";
            }
            else if (indiceNPC == 29)
            {
                mensaje = "The owner of the residence is a wonderful person. He cares a lot for all of us and makes us feel like we are a big family.";
            }
            else if (indiceNPC == 30)
            {
                mensaje = "Living in Pedrán is great. Every day there is a party at an inn or people wanting to go on an excursion on the different routes around the city.";
            }
            else if (indiceNPC == 31)
            {
                mensaje = "I'm considering come back home. The big city is not made for me. I really like my little town where I know everyone. Nobody here cares for anyone and nobody cares if you are good or not.";
            }
            else if (indiceNPC == 32)
            {
                mensaje = "The only two student residences that remain in Pedrán are my brother's one and this is where you are it’s mine. We are completely overwhelmed, but do not allow the opening of anything else and I do not understand it.";
            }
            //Resi 2
            else if (indiceNPC == 33)
            {
                mensaje = "Here we help each other a lot to study. Together we finish things before each one going by his side.";
            }
            else if (indiceNPC == 34)
            {
                mensaje = "This is my second year in residence. If this site did not exist, I could not study at the university. I cannot afford studies and also pay the rent of a room anywhere else.";
            }
            else if (indiceNPC == 35)
            {
                mensaje = "As the owners of both residences are brothers, we meet on holidays both residences and we have got a great meal. It's quite funny.";
            }
            else if (indiceNPC == 36)
            {
                mensaje = "I try to always leave my things as collected as possible to not disturb the rest. There are many of us who live here and it is important to respect the others.";
            }
            else if (indiceNPC == 37)
            {
                mensaje = "Some nobles are pressing to close the remaining university residences. I don't know what they are up to, but it won't be good for sure.";
            }
            else if (indiceNPC == 38)
            {
                mensaje = "Sometimes our different beliefs generate certain friction, but we don't stop loving and respecting each other. That is the most important thing in every relationship.";
            }
            else if (indiceNPC == 39)
            {
                mensaje = "Science and religion are completely incompatible, however, love is stronger than any difference between people.";
            }
            //Casa
            else if (indiceNPC == 40)
            {
                mensaje = "I think my son is ashamed of me. I know that I am not an example to follow and that I have not succeeded in my life. But all I do is for him.";
            }
            else if (indiceNPC == 41)
            {
                mensaje = " I can't stand my father's attitude, he's a failure and he hardly comes at home on nights. I can't understand drunkards.";
            }
            //Casa Grande
            else if (indiceNPC == 42)
            {
                mensaje = "The noble life is too stressful. If I had known this, I would never have married the Duchess.";
            }
            else if (indiceNPC == 43)
            {
                mensaje = "I have been working for the Duchess's family for years and I must say that they have always treated me excellently.";
            }
            else if (indiceNPC == 44)
            {
                mensaje = "Maybe at some point I have a job for a strong young man like you.";
            }
        }
        else if (indiceZona == 15)//R6
        {
            if (indiceNPC == 0)
            {
                mensaje = "I'm sorry, but I can't let anyone pass without authorization here. It is a very dangerous area.";
            }
            else if (indiceNPC == 1)
            {
                mensaje = "I love coming here for fresh air. From time to time I need to disconnect.";
            }
            else if (indiceNPC == 2)
            {
                mensaje = "When I was young my friends and I came here to play. When I remember it, I always think about what our parents would be thinking to let us play in such a dangerous place.";
            }
            else if (indiceNPC == 3)
            {
                mensaje = "Our stores are open 24 hours a day, 7 days a week, 12 consecutive months, 365 days a year... I don't understand, who can need a demonic sword at 4 in the morning on a Tuesday?";
            }
            else if (indiceNPC == 4)
            {
                mensaje = "Study is important. If you don't study, you won't be able to learn the right attacks to defend yourself in combat. In addition, a book is not spent by reading it.";
            }
            else if (indiceNPC == 5)
            {
                mensaje = "They don't pay me enough for everything I have to endure. The monsters chase us, the dogs bark at us, people complain that it takes us a long time to deliver the letters ... I swear that the next one who complains I will swallow the letters.";
            }
            else if (indiceNPC == 6)
            {
                mensaje = "I am collecting samples for my next experiments. We used to use students for this, but with that of human rights they no longer let us send them to tedious or dangerous tasks that have nothing to do with their education.";
            }
            else if (indiceNPC == 7)
            {
                mensaje = "We work hard despite the peacetime we live. I enlisted just because I believed that I would not have to take extra hours.";
            }
            else if (indiceNPC == 8)
            {
                mensaje = "I trust very little about the bridges, they are made of wood, they are old and many people cross it daily. That is why I prefer to monitor from here that I do not have to cross anything but one.";
            }
            else if (indiceNPC == 9)
            {
                mensaje = " There have been problems and we cannot let anyone pass. We apologize for the inconveniences.";
            }
            else if (indiceNPC == 10)
            {
                mensaje = " If you know how to cook potions you can use our cauldron.";
            }
            else if (indiceNPC == 11)
            {
                mensaje = "Preparar tus propias pociones es un método barato y divertido de generar tus propios recursos cuando vayas de expedición.";
            }
        }
        else if (indiceZona == 17)
        {
            if (indiceNPC == 0)
            {
                mensaje = " It is only a replica of the imperial crown but we cannot allow any insult or show of dishonour against it. Be careful what you do boy.";
            }
            else if (indiceNPC == 1)
            {
                mensaje = "My God, it has cost me more than ever to enrol here and they expose it in public. This is a joke.";
            }
            else if (indiceNPC == 2)
            {
                mensaje = "This is my favourite exhibition piece. That gigantic dinosaur is incredible and every time I stand in front of it, I imagine a fantastic world with creatures that we cannot even imagine.";
            }
            else if (indiceNPC == 3)
            {
                mensaje = "This place always impresses me when I cross its doors. It is such a great and fascinating place ... So many years of knowledge and progress gathered in one place. I think that many times we lose perspective and forget how important these sites are.";
            }
            else if (indiceNPC == 4)
            {
                mensaje = "*Shhhhh* Don't talk to me, I'm a statue.";
            }
            else if (indiceNPC == 5)
            {
                mensaje = "On the right is the auditorium and library. On the left the offices and the materials store.";
            }
            else if (indiceNPC == 6)
            {
                mensaje = "I don't understand what the Ancients would be thinking with modern art. It is clear that this is a lot of useless garbage.";
            }
            else if (indiceNPC == 7)
            {
                mensaje = "Reggaetonerus Maximus: Real-size example of one of the species that populated the world of the Ancients and that is believed to be one of the causes of its decline.";
            }
            else if (indiceNPC == 8)
            {
                mensaje = "Green T-Rex: It is believed that these legendary beasts were pets of the powerful Ancients. They took them out for a walk, fed and care as if they were part of their family.";
            }
            else if (indiceNPC == 9)
            {
                mensaje = "Cauldron: This magical cauldron was the one used by Gregory Hyden, founding father of the University of Áncia to elaborate his experiments.";
            }
            else if (indiceNPC == 10)
            {
                mensaje = "Flag of the University of Áncia: Symbol that represents the University around the world.";
            }
            else if (indiceNPC == 11)
            {
                mensaje = "Armor of the Guardians: This outfit belonged to the famous Guardians of Áncia. The Guardians were a body of wise warriors who defended the empire during the 100-year war against the invading empire of Frandia.";
            }
            else if (indiceNPC == 12)
            {
                mensaje = "Imperial Crown: Replica of the crown of the empire that represents the Emperor's power.";
            }
            else if (indiceNPC == 13)
            {
                mensaje = "University Helmet: Helmet that is given to those who are appointed gentlemen of the university. The best among the best in the university are awarded this distinction.";
            }
            else if (indiceNPC == 14)
            {
                mensaje = "Piece nº 0: Sculpture of the considered modern art of the ancients. The representation is open to each viewer.";
            }
            else if (indiceNPC == 15)
            {
                mensaje = "Low prices: Registration price at a reduced price for university admission.";
            }
            else if (indiceNPC == 16)
            {
                controlObjetos.misionHActiva = true;
                controlObjetos.conversacionH = true;
                mensaje = "The attack on Canda, the dissolution of the student associations, the illness of the emperor, the confrontation of the religious and anti-religious ... There are too many strange events in a very short time. This should be investigated in depth. Decided ... I will go to Canda to see if I get more information.";
            }
            else if (indiceNPC == 17)
            {
                mensaje = "You cannot pass.";
            }
            else if (indiceNPC == 18)
            {
                mensaje = "This armour seems to start walking at any time. ";
            }
            else if (indiceNPC == 19)
            {
                mensaje = "What bothers me to do at the University is paperwork. The bureaucracy here is always very messy.";
            }
            else if (indiceNPC == 20)
            {
                mensaje = "If you have to do paperwork you have to go to the lower floor.";
            }
            else if (indiceNPC == 21)
            {
                mensaje = "To pass the subjects you must take a final test that will be unique in that class.";
            }
            else if (indiceNPC == 22)
            {
                mensaje = "My dream is to become a gentleman ... but with my notes I do not even recruit to be honest.";
            }
            else if (indiceNPC == 23)
            {
                mensaje = "I like to come to the university museum from time to time. It helps me to motivate myself for the future.";
            }
            else if (indiceNPC == 24)
            {
                mensaje = "This piece is so inspiring…";
            }
            else if (indiceNPC == 25)
            {
                mensaje = "Long live to the emperor. Long live to Áncia.";
            }
            else if (indiceNPC == 26)
            {
                mensaje = "There is too much noise despite of being a museum. There is no education.";
            }
            else if (indiceNPC == 27)
            {
                mensaje = "I am trying to convince the university to introduce a course to form magic blacksmiths. I think it's something that would be very beneficial for everyone.";
            }
            else if (indiceNPC == 28)
            {
                mensaje = "They should put a food stand or café here at the university. Having to come back to Pedrán or one of the other towns nearby is quite uncomfortable.";
            }
            else if (indiceNPC == 29)
            {
                mensaje = "I really am not enrolled in the University, but the university students drive me crazy so I come usually.";
            }
            else if (indiceNPC == 30)
            {
                mensaje = "It is said that the director is a genius in everything related to magic and it is even rumoured that he could teach this year.";
            }
            else if (indiceNPC == 31)
            {
                mensaje = "I try to sell magic gadgets for the University, but I can't arrange a meeting with the management.";
            }
            else if (indiceNPC == 32)
            {
                mensaje = "People from all parts of the empire and even people from other countries come to this university.";
            }
            else if (indiceNPC == 33)
            {
                mensaje = "When you fight against wild monsters it is important to consider a good strategy, if you throw yourself only to attack it may turn against you.";
            }
            else if (indiceNPC == 34)
            {
                mensaje = "Luckily there are magic portals. Coming from Manfa to here through the desert would be a nightmare.";
            }
            else if (indiceNPC == 35)
            {
                mensaje = "The respect of saying that you are a teacher in this institution is almost like saying that you belong to a noble house. It is fascinating to see the respect it imposes and in turn it is a great responsibility.";
            }
            else if (indiceNPC == 36)
            {
                mensaje = "If we forget our history, we will be condemned to repeat our mistakes. Those who deny these events and try to convince you that some things never happened or were altered to attack our history are the most dangerous terrorists.";
            }
        }
        else if (indiceZona == 18) //E. Principal P1
        {
            if (indiceNPC == 0)
            {
                mensaje = "There are too many books that force us to study. How do they pretend that I can read 10 every week if the smallest has 500 pages? This is no way to learn.";
            }
            else if (indiceNPC == 1)
            {
                mensaje = "A few days ago a very strange thing happened. Some students were talking about hiding a book on R8. I hope they didn't do it.";
            }
            else if (indiceNPC == 2)
            {
                mensaje = "Sometimes the pressure I suffer when I do an exam makes me feel like I'm suffocating. It is as if all those months of effort and sacrifice are going to fade away by a test that is not even a tenth of the time I have dedicated.";
            }
            else if (indiceNPC == 3)
            {
                mensaje = "This study room is the one that contains the greatest variety of texts of all kinds that you can find throughout the Empire, if the private files of the professors were added it may be the largest in the world.";
            }
            else if (indiceNPC == 4)
            {
                mensaje = "I have an exam tomorrow and I don't know where to start. I'm going to obtain a bigger refuse than me.";
            }
            else if (indiceNPC == 5)
            {
                mensaje = "Many times I think about how responsible we are that the university is as difficult as it is now. Many of the teachers we catalogue as cruel and tyrant may not be like that when they started teaching. Surely they were people full of illusion that life has crushed to make them what they are now.";
            }
            else if (indiceNPC == 6)
            {
                mensaje = "I come here from time to time in search of talent to incorporate my companies around the world. Maybe you can be one of my future great incorporations.";
            }
            else if (indiceNPC == 7)
            {
                mensaje = "How annoying it gets when we come to study together. I have an exam soon and she just distracts me.";
            }
            else if (indiceNPC == 8)
            {
                mensaje = "The best thing that happened to me when I came to university has been to meet her. I hope our love lasts all my life.";
            }
            else if (indiceNPC == 9)
            {
                mensaje = "I needed some calm so please don't bother me.";
            }
            else if (indiceNPC == 10)
            {
                mensaje = "If I come here, I don't feel so bad about not studying. It's a great plan...";
            }
            else if (indiceNPC == 11)
            {
                mensaje = "I thought I had the notes of that week here, but I can't find them.";
            }
            else if (indiceNPC == 12)
            {
                mensaje = "This is the largest study area of the entire University, here you can find almost any book you can think of and if you can't find it you can order it from me.";
            }
            else if (indiceNPC == 13)
            {
                mensaje = "The university can also serve to achieve new skills that serve you in combat and that you will not be able to achieve in any other way.";
            }
            else if (indiceNPC == 14)
            {
                mensaje = "This library is one of the most complete that can be accessed publicly. It is a real joy to come and discover new books.";
            }
            else if (indiceNPC == 15)
            {
                mensaje = "I could never go to university, the rates were too high for my family, but that did not stop me and I have achieved a good life. The most important thing is to give everything regardless of the conditions around you.";
            }
            else if (indiceNPC == 16)
            {
                mensaje = "I am looking for my intention to study, maybe they are here.";
            }
            else if (indiceNPC == 17)
            {
                mensaje = "Do you see me? I knew that this invisibility cloak was too cheap to be real.";
            }
            else if (indiceNPC == 18)
            {
                mensaje = "I still remember my days in this library. It was here that I learned almost everything I know about medicine today. I miss those days.";
            }
            else if (indiceNPC == 19)
            {
                mensaje = "There are many ways to approach God Sestae and many interpretations of his message. However, the empire has promoted the version that interests him most. They have pushed those who do not think equal to ostracism.";
            }
            else if (indiceNPC == 20)
            {
                mensaje = "If we are not careful, the Dome will do what it wants with the university... Wait, won't you be one of them?";
            }
            else if (indiceNPC == 21)
            {
                bool aceptada = false;
                bool completada = false;

                if (baseDeDatos.numeroMisionesReclutamiento != 0)
                {
                    for (int i = 0; i < baseDeDatos.numeroMisionesReclutamiento; i++)
                    {
                        if (baseDeDatos.listaMisionesReclutamiento[i].indice == 20)
                        {
                            aceptada = true;
                            /*
                            if (baseDeDatos.listaMisionesReclutamiento[i].subMisionCompletada[0])
                            {
                                completada = true;
                            }
                            */
                        }
                    }
                }

                emisor = "Tob";

                if (!aceptada)
                {
                    mensaje = "What a macabre destiny of mine! How could this happen? The only book of Magic for Idiots has disappeared. I know we have never met, but could you help me find it? If you find it, I will help you with what you need from now on.";
                    mision = true;
                    indiceMision = 20;
                }
                else
                {
                    if (completada)
                    {
                        mensaje = "You found it! It's wonderful, I owe you my life. Count me on everything you need my friend.";
                        mision = false;

                        for (int i = 0; i < baseDeDatos.numeroMisionesReclutamiento; i++)
                        {
                            if (baseDeDatos.listaMisionesSecundarias[i].indice == 20)
                            {
                                if (baseDeDatos.listaMisionesReclutamiento[i].completada)
                                {
                                    completada = true;
                                }
                                else
                                {
                                    completada = false;
                                }
                            }
                        }

                        if (!completada)
                        {
                            baseDeDatos.CumpleMision(0);
                        }
                    }
                    else
                    {
                        mensaje = "I will continue searching here, if you find it, come see me as soon as possible please.";
                        mision = false;
                    }
                }
            }
            else if (indiceNPC == 22)
            {
                mensaje = "The Imperial Guard has no jurisdiction here. We are responsible for ensuring that order is maintained.";
            }
        }
        else if (indiceZona == 20) //Ala oeste PB
        {
            if (indiceNPC == 0)
            {
                mensaje = "Professor Albert is special. He is helpful, explains well and does not mistreat us. You can tell he has a passion for teaching.";
            }
            else if (indiceNPC == 1)
            {
                mensaje = "Who can think of mixing Dragongrass with Fairy Flowers?! Everyone knows that they are incompatible.";
            }
            else if (indiceNPC == 2)
            {
                mensaje = "I just wanted to experience new combinations. In the University they should promote that we be creative and daring. Not that we simply replicated what others have already done.";
            }
            else if (indiceNPC == 3)
            {
                mensaje = "I am never able to remember the schedule so I have to check it on the screen. The problem is that the screen doesn't work today so I'm lost.";
            }
            else if (indiceNPC == 4)
            {
                mensaje = "Who has put these boxes here in the middle?";
            }
            else if (indiceNPC == 5)
            {
                mensaje = "I'm tired of having crowded classes, students and unmotivated classmates. This University needs an urgent change.";
            }
            else if (indiceNPC == 6)
            {
                mensaje = "Weird things are happening in the labs. We are working on a project, but we are not told what it is. We have been separated into very specialized small work groups.";
            }
            else if (indiceNPC == 7)
            {
                mensaje = "The lower floor is banned for students.";
            }
            else if (indiceNPC == 8)
            {
                mensaje = "Isn't it weird that there are guards that block access to some areas? Because it seems very strange to me.";
            }
            else if (indiceNPC == 9)
            {
                mensaje = "It was time for the director to use a hard hand, these young people are too spoiled.";
            }
            else if (indiceNPC == 10)
            {
                mensaje = "Before there were groups of students who defended our rights at the University. However, with the new directive they were banned and forced to dissolve. Now we are against danger.";
            }
            else if (indiceNPC == 11)
            {
                mensaje = "Classroom 0.1";
            }
            else if (indiceNPC == 12)
            {
                mensaje = "Classroom 0.2";
            }
            else if (indiceNPC == 13)
            {
                mensaje = "Classroom 0.3";
            }
            else if (indiceNPC == 14)
            {
                mensaje = "Classroom 0.4";
            }
            else if (indiceNPC == 15)
            {
                mensaje = "I'm glad they assigned me as Professor Albert's assistant, it was the best option for me.";
            }
            else if (indiceNPC == 16)
            {
                mensaje = "The beginning of university life is quite complex. Don't let that put you off. If you need anything, talk to me. It is also part of my job to guide you on your way while you are my student.";
            }
            else if (indiceNPC == 17)
            {
                mensaje = "In order to face my exam you must pass at least 5 Fencing tests. If you want an explanation of the rules, talk to my assistant. He's Gabriel, the boy on the tatami.";
            }
            else if (indiceNPC == 18)
            {
                mensaje = "I'm going to become the best fencer in the whole empire.";
            }
            else if (indiceNPC == 19)
            {
                mensaje = "Professor Lionetta is a fencing legend. Even the best members of the Imperial Guard would have trouble matching it.";
            }
            else if (indiceNPC == 20)
            {
                mensaje = "I will be watching you.";
            }
            else if (indiceNPC == 21)
            {
                mensaje = "I didn't want to be Professor Elric's assistant. I don't like him or his class.";
            }
            else if (indiceNPC == 22)
            {
                mensaje = "In the Fencing of Áncia there are two very simple phases. The first is to win a duel of Rock Paper Scissors. The winner decides a direction in which the loser cannot look. If you look in that direction you lose a round, otherwise it starts again. If you lose three rounds you lose the fight. If you want to try choose a weapon from the shelves.";
            }
            else if (indiceNPC == 23)
            {
                mensaje = "To take my exam you must show that you master the art of creating potions by making 5 of the list. If you want an explanation of how to prepare them, talk to my assistant. She is Vaughan, the girl who is making potions in the back of the classroom.";
            }
            else if (indiceNPC == 24)
            {
                mensaje = "To make a potion you must memorize the steps to follow that will be shown to you at the beginning of each round. If you fail at any step you should continue trying from that point without making a mistake more than three times or you will ruin the recipe. You should not take long time or it will burn. You can use any cauldron to start preparing your potion.";
            }
        }
        else if (indiceZona == 21) //Ala oeste P1
        {
            if (indiceNPC == 0)
            {
                mensaje = "Sometimes I see people who don't study and who are happy with their lives, who don't have giant eye bags or burdens and I wonder what I really do here.";
            }
            else if (indiceNPC == 1)
            {
                mensaje = "Is it your first year? Well cheer up, it's possibly the worst year, but don't worry the following are bad too.";
            }
            else if (indiceNPC == 2)
            {
                mensaje = "Do not let my friend discourage you, the university is a unique experience and it will open a lot of doors to you.";
            }
            else if (indiceNPC == 3)
            {
                mensaje = "Long live to the Resistance!";
            }
            else if (indiceNPC == 4)
            {
                mensaje = " Be careful what you do, we will be watching.";
            }
            else if (indiceNPC == 5)
            {
                mensaje = "For me, studying is like a safety net. My real dream is to be a fencing star like Professor Lionetta.";
            }
            else if (indiceNPC == 6)
            {
                mensaje = "Going up to this floor where the teachers' offices are, it is as if happiness vanishes and it was very cold.";
            }
            else if (indiceNPC == 7)
            {
                mensaje = "It is important to always carry a good assortment of objects, you never know when you are going to need it.";
            }
            else if (indiceNPC == 8)
            {
                mensaje = "The offices are locked since the course has started, what will have happened?";
            }
            else if (indiceNPC == 9)
            {
                mensaje = "Sometimes it is good go to Pedrán and disconnect from classes and responsibility.";
            }
            else if (indiceNPC == 10)
            {
                mensaje = "I don’t understand the huge disconnection between young people and our religious community. Maybe we are not doing something well.";
            }
            else if (indiceNPC == 11)
            {
                mensaje = "Kwolek’s office.";
            }
            else if (indiceNPC == 12)
            {
                mensaje = "Elric’s office.";
            }
            else if (indiceNPC == 13)
            {
                mensaje = "Lionetta’s office.";
            }
            else if (indiceNPC == 14)
            {
                mensaje = "Albert’s office.";
            }
        }
    }
}
