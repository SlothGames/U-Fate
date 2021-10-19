using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DespachoGeneral1 : MonoBehaviour {
    int posMensaje;
    int numeroMensajes;

    string mensaje;
    string emisor;

    bool mostrado;//true si esta parte de la historia ya ha sido mostrada
    bool activo;
    bool pasaPrimerMensaje;
    bool start = false;
    bool isFadeIn = false;
    bool mision;

    float alpha;
    float fadeTime;

    public GameObject prota;
    public GameObject bloqueaEscaleras;
    public GameObject elementosAnim;
    public GameObject fondoNegro;
    public GameObject textoMision;
    public GameObject bloqueoEstadistica;
    GameObject manager;

    ControlJugador controlJugador;
    Camara camara;

    BaseDatos baseDeDatos;


    void Awake()
    {
        mostrado = false;
    }



    void Start()
    {
        manager = GameObject.Find("GameManager");
        baseDeDatos = manager.GetComponent<BaseDatos>();

        alpha = 0;
        fadeTime = 2f;
        numeroMensajes = 16;

        activo = false;
        pasaPrimerMensaje = false;
        mision = false;

        controlJugador = GameObject.Find("Player").GetComponent<ControlJugador>();
        camara = GameObject.Find("Main Camera").GetComponent<Camara>();

        textoMision.SetActive(false);

        elementosAnim.SetActive(false);       
    }



    void Update()
    {
        if (!mostrado)
        {
            if (activo)
            {
                if (!mision)
                {
                    if (pasaPrimerMensaje)
                    {
                        pasaPrimerMensaje = false;

                        MuestraSiguienteMensaje();
                    }
                    else if (TextBox.ocultar)
                    {
                        if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                        {
                            posMensaje++;

                            if (posMensaje < numeroMensajes)
                            {
                                MuestraSiguienteMensaje();
                            }
                        }
                    }
                }
                else
                {
                    if (TextBox.menuActivo)
                    {
                        if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonUp("A"))
                        {
                            CierraAnimacion();
                        }
                    }
                }
            }
            else
            {
                if (baseDeDatos.numeroMisionesPrincipales >= 4)
                {
                    if (baseDeDatos.listaMisionesPrincipales[2].completada)
                    {
                        mostrado = true;
                    }
                }
            }
        }
        else
        {
            bloqueaEscaleras.SetActive(false);
            bloqueoEstadistica.SetActive(false);

            StartCoroutine(DestruyeObjeto());
        }

    }



    void MuestraSiguienteMensaje()
    {
        emisor = "General";

        if(baseDeDatos.idioma == 1)
        {
            switch (posMensaje)
            {
                case 0:
                    mensaje = "* Hem! * First of all let me introduce myself. I'm General Arnold, leader of the group known as The Resistance. I am also the most veteran member. To give you an idea, I have been here since the first student movement that liberalized anyone who wanted it to access the University of Áncia.";
                    break;
                case 1:
                    mensaje = "More than 30 years have passed from that and during all that time we had been watching and fighting for the university to continue being an oasis of peace and freedom for everyone, whether they were students or professors. It seemed that we had achieved a perfect balance between two worlds that seemed eternally enmity.";
                    break;
                case 2:
                    mensaje = "However, this balance has been disturbed since few months, almost without realizing it and leaving us hardly any room for manoeuvre. It all started when a group of wise men proposed to the emperor a new curriculum to carry out the University of Áncia to be the envy of the entire world. The proposal completely amazed him.";
                    break;
                case 3:
                    mensaje = "Our court spies got a copy of the plan and our intelligence team saw nothing wrong with it. Everything seemed in order and we decided not to intervene. That was our first big mistake. Soon after landing at the university everything changed. The atmosphere suddenly became weird.";
                    break;
                case 4:
                    mensaje = "The balance we had achieved was broken in an instant resulting in an increasingly evident confrontation between both sides. Teachers began to massively suspend students, the workload increased to limits never seen, enrollment rates became prohibitive for most people.";
                    break;
                case 5:
                    mensaje = "The students did not admit all that great amount of changes against him for a long time and a direct conflict broke out. The student leaders made a united front to deal with the issue diplomatically. They decided that our intervention was not necessary when we offered it and that they could reverse the situation. I approved this decision and the negotiations began.";
                    break;
                case 6:
                    mensaje = "That was the second big mistake ... The waters seemed to return to their channel and an agreement seemed close. But then a new unforeseen happened. Suddenly the united front of students broke down, a faction positioned itself next to the direction and harangued theirs against the rest of the students. Negotiations broke down and management hardened its policy again.";
                    break;
                case 8:
                    mensaje = "That was when we decided to take action. Our agents began to investigate what had happened during the negotiations and what was our surprise that of the group of wise men who were supposed to rule the university there were only two left. The rest had been dying one after another in strange conditions and being replaced by others we knew nothing about.";
                    break;
                case 9:
                    mensaje = "Of those two, one was the one who led a group they called The Dome, which led the university in the shadow with these new members who had joined. The name of this sage according to the documents was Von Matterhorn...";
                    break;
                case 10:
                    mensaje = "The second was taken prisoner for disagreeing with Von Matterhorn. Possibly he was not the only one and that explained the rest of the deaths, but this second subject had something that made him essential and therefore could not kill him like the rest. We tried to rescue him to bring here but when we got there, he had already managed to flee by himself. We never heard from him or who exactly he was.";
                    break;
                case 11:
                    mensaje = "* Hem! * I think I'm getting too long. Moving forward to this day I owe you an explanation for what happened. We discovered that The Dome had planned to attack students entering the new course. This puzzled us since they had always acted in the shadow to avoid being related to any incident.";
                    break;
                case 12:
                    mensaje = "We did not know what the method used would be and for that reason we decided to move on to a more offensive attitude launched a series of simultaneous operations against The Dome among them which rescued you a few hours ago. It is at this point where you enter.";
                    break;
                case 13:
                    mensaje = "You were some of those who confronted the members of The Dome when things got complicated. You have demonstrated the value that is necessary in these times. Some of you have even enlisted and I thank you for your delivery. However, I must ask you a favor.";
                    break;
                case 14:
                    mensaje = "We have lost communication with a group of explorers in the Department of Statistics. These things happen often and usually nothing serious, but if the situation were not that they will need urgent help. I don't have enough troops at the moment to send them there, we're working on it, but we can't wait.";
                    break;
                case 15:
                    mensaje = "I want you to form a team and help that group if necessary. You will only carry supplies; we do not want you to fight because you have no experience. I know it's a lot to ask, but we need you. If anyone does not want to intervene, he can say it now without fear of reproach, I will understand ................ It seems that there is no one. I thank you for your collaboration and wish you luck.";
                    break;
            }
        }
        else
        {
            switch (posMensaje)
            {
                case 0:
                    mensaje = "*Ejem* Primero de todo permitidme presentarme. Soy el general Arnold, líder del grupo conocido como La Resistencia. Soy también su miembro más veterano hasta la fecha. Para que os hagáis una idea llevo aquí desde el primer movimiento estudiantil que liberalizó el acceso a todo aquel que lo quisiera a la Universidad de Áncia.";
                    break;
                case 1:
                    mensaje = "De eso han pasado ya más de 30 años y durante todo ese tiempo habíamos estado vigilando y luchando para que la universidad siguiera siendo un oasis de paz y libertad para todos, ya fueran alumnos o profesores. Parecía que habíamos logrado un equilibrio perfecto entre dos mundos que parecían eternamente enemistados.";
                    break;
                case 2:
                    mensaje = "Sin embargo este equilibrio se ha visto perturbado desde hace unos meses, casi sin darnos cuenta y sin dejarnos apenas margen de maniobra. Todo comenzó cuando un grupo de sabios propuso al emperador un nuevo plan de estudios para llevar a la universidad de Áncia a ser la envidia del mundo entero. La propuesta le maravilló por completo.";
                    break;
                case 3:
                    mensaje = "Nuestros espías en la corte consiguieron una copia del plan y nuestro equipo de inteligencia no vio nada malo en él. Todo parecía en orden y decidimos no intervenir. Ese fue nuestro primer gran error. Al poco tiempo de desembarcar en la universidad todo cambió. El ambiente se enrareció de repente.";
                    break;
                case 4:
                    mensaje = "El equilibrio que habíamos logrado se rompió en un instante dando lugar a una confrontación cada vez más evidente entre ambos bandos. Los profesores comenzaron a suspender masivamente a los estudiantes, la carga de trabajo subió hasta límites nunca vistos, las tasas de matriculación subieron hasta un punto prohibitivo para la mayoría de la gente.";
                    break;
                case 5:
                    mensaje = "Los alumnos no admitieron toda esa gran cantidad de cambios en su contra durante mucho tiempo y estalló un conflicto directo. Los líderes estudiantiles hicieron un frente unido para tratar el tema de manera diplomática. Decidieron que no era necesaria nuestra intervención cuando se la ofrecimos y que ellos podrían revertir la situación. Yo di mi visto bueno a esta decisión y comenzaron las negociaciones.";
                    break;
                case 6:
                    mensaje = "Ese fue el segundo gran error... Las aguas parecían volver a su cauce y un acuerdo parecía próximo. Pero entonces ocurrió un nuevo imprevisto. De repente el frente unido de estudiantes se rompió, una facción se posicionó junto a la dirección y arengó a los suyos en contra del resto de estudiantes. Se rompieron las negociaciones y la dirección volvió a endurecer su política.";
                    break;
                case 8:
                    mensaje = "Fue entonces cuando decidimos entrar en acción. Nuestros agentes comenzaron a investigar lo que había ocurrido durante las negociaciones y cual fue nuestra sorpresa que del grupo de sabios que se suponía que regían la universidad solo quedaban dos. El resto habían ido muriendo uno tras otro en extrañas condiciones y siendo sustituidos por otros de los que nada sabíamos.";
                    break;
                case 9:
                    mensaje = "De esos dos uno era el que encabezaba a un grupo al que llamaban La Cúpula, la cual dirigía la universidad en la sombra junto con estos nuevos miembros que se habían ido sumando. El nombre de este sabio según los documentos era Von Matterhorn...";
                    break;
                case 10:
                    mensaje = "El segundo fue hecho preso por estar en desacuerdo con Von Matterhorn. Posiblemente no fue el único y aquello explicaba el resto de muertes, pero este segundo sujeto tenía algo que le hacía imprescindible y por ello no pudo matarlo como al resto. Tratamos de rescatarlo para traerlo aquí pero cuando conseguimos llegar él ya había logrado huir por sí mismo. Nunca volvimos a saber de él ni quien era exactamente.";
                    break;
                case 11:
                    mensaje = "* Ejem * Creo que me estoy alargando demasiado con estas viejas historias. Avanzando hasta el día de hoy os debo una explicación por lo ocurrido. Descubrimos que La Cúpula tenía planeado atacar a los estudiantes que ingresaban al nuevo curso. Esto nos desconcertó ya que siempre habían actuado en la sombra para evitar ser relacionados con cualquier incidente.";
                    break;
                case 12:
                    mensaje = "Desconocíamos cual sería el método empleado y por ello decidimos pasar a una actitud más ofensiva lanzado una serie de operaciones simultaneas contra La Cúpula entre ellas la que os rescató hace unas pocas horas. Es en este punto donde entráis vosotros.";
                    break;
                case 13:
                    mensaje = "Vosotros fuisteis algunos de los que se enfrentaron a los miembros de La Cúpula cuando la cosa se puso complicada. Habéis demostrado la valía que en estos tiempos es necesaria. Algunos incluso ya os habéis alistado y os agradezco vuestra entrega. Sin embargo, debo pediros un favor.";
                    break;
                case 14:
                    mensaje = "Hemos perdido comunicación con un grupo de exploradores en el departamento de Estadística. Estas cosas pasan a menudo y no suele ser nada grave, pero si la situación no fuera esa necesitarán ayuda urgente. No tengo efectivos suficientes en este momento para enviarlos allí, estamos trabajando en ello, pero no podemos esperar.";
                    break;
                case 15:
                    mensaje = "Quiero que vosotros forméis un equipo y prestéis ayuda a ese grupo de ser necesario. Solo llevaréis suministros no queremos que luchéis pues no tenéis experiencia. Se que es mucho pediros, pero os necesitamos. Si alguno no quiere intervenir puede decirlo ahora sin miedo a reproche alguno, lo comprenderé................ Parece que no hay nadie. Os agradezco vuestra colaboración y os deseo suerte.";
                    break;
            }
        }


        if (posMensaje < 15)
        {
            TextBox.MuestraTextoHistoria(mensaje, emisor);
        }
        else
        {
            TextBox.MuestraTextoHistoriaMision(mensaje, emisor, 15);
            mision = true;
        }
    }



    void IniciaTexto()
    {
        activo = true;
        pasaPrimerMensaje = true;
    }



    void CierraAnimacion()
    {
        mostrado = true;
        StartCoroutine(Termina());
    }



    void OnGUI()
    {
        if (!start)
            return;

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);

        Texture2D tex;
        tex = new Texture2D(1, 1);
        tex.SetPixel(0, 0, Color.black);
        tex.Apply();

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), tex);

        if (isFadeIn)
        {
            alpha = Mathf.Lerp(alpha, 1.1f, fadeTime * Time.deltaTime);
        }
        else
        {
            alpha = Mathf.Lerp(alpha, -0.1f, fadeTime * Time.deltaTime);

            if (alpha < 0)
            {
                start = false;
            }

        }
    }




    void FadeIn()
    {
        start = true;
        isFadeIn = true;
    }




    void FadeOut()
    {
        isFadeIn = false;
    }



    IEnumerator Termina()
    {
        baseDeDatos.indiceObjetivo = 10;
        bloqueaEscaleras.SetActive(false);
        bloqueoEstadistica.SetActive(false);
        fondoNegro.SetActive(true);
        FadeIn();
        elementosAnim.SetActive(false);

        yield return new WaitForSeconds(fadeTime);

        fondoNegro.SetActive(false);
        FadeOut();

        //menuMision.SetActive(false);

        controlJugador.setMensajeActivo(false);

        activo = false;

        TextBox.OcultaTextoFinCombate();

        camara.TerminaHistoria();

        yield return new WaitForSeconds(1);

        textoMision.SetActive(true);

        if (baseDeDatos.idioma == 0)
        {
            textoMision.transform.GetChild(1).GetComponent<Text>().text = "Nueva Misión";
        }
        else if (baseDeDatos.idioma == 1)
        {
            textoMision.transform.GetChild(1).GetComponent<Text>().text = "New Mision";
        }

        yield return new WaitForSeconds(2);

        textoMision.SetActive(false);
    }



    IEnumerator Inicia()
    {
        controlJugador.setMensajeActivo(true);
        elementosAnim.SetActive(true);
        fondoNegro.SetActive(true);
        prota.transform.position = new Vector3(-82.3f, -630, prota.transform.position.z);

        FadeIn();
        camara.IniciaHistoria(6);

        yield return new WaitForSeconds(fadeTime);

        fondoNegro.SetActive(false);
        FadeOut();

        yield return new WaitForSeconds(1.5f);

        IniciaTexto();
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (!mostrado)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(Inicia());
            }
        }
    }



    IEnumerator DestruyeObjeto()
    {
        yield return new WaitForSeconds(8);
        Destroy(gameObject);
    }
}
