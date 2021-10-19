using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour {
	Transform target;
    BaseDatos baseDeDatos;
    public GameObject Teleportes;

    public bool combate;
    public bool historia;
    bool portalAbierto;
    int numeroPortales;

    float posx, posy, posz;


	void Start ()
    {
        baseDeDatos = GameObject.Find("GameManager").GetComponent<BaseDatos>();
        combate = false;
        historia = false;
        portalAbierto = false;
        numeroPortales = 8;
    }



	void Awake()
    {
		target = GameObject.FindGameObjectWithTag ("Player").transform;
	}



	void Update()
    {
        if (!historia)
        {
            if (!combate)
            {
                float posXaux = target.position.x;
                float posYaux = target.position.y;

                switch (baseDeDatos.areaCamara)
                {
                    case 1: //Pueblo Origen
                        if (baseDeDatos.zonaCamara == 0) //CasaProta
                        {
                            posXaux = 107;

                            if (posYaux < -411.3f)
                            {
                                posYaux = -411.3f;
                            }
                            else if (posYaux > -409.5f)
                            {
                                posYaux = -409.5f;
                            }
                            else
                            {
                                posYaux = target.position.y;
                            }
                        }
                        else if (baseDeDatos.zonaCamara == 1) //Casa2
                        {
                            posXaux = 83;

                            if (posYaux < -411.3f)
                            {
                                posYaux = -411.3f;
                            }
                            else if (posYaux > -409.5f)
                            {
                                posYaux = -409.5f;
                            }
                            else
                            {
                                posYaux = target.position.y;
                            }
                        }
                        else if (baseDeDatos.zonaCamara == 2) //Casa3
                        {
                            posXaux = 60;

                            if (posYaux < -411.3f)
                            {
                                posYaux = -411.3f;
                            }
                            else if (posYaux > -409.5f)
                            {
                                posYaux = -409.5f;
                            }
                            else
                            {
                                posYaux = target.position.y;
                            }
                        }
                        else //Calle
                        {
                            if (posXaux < 65.5f)
                            {
                                posXaux = 65.5f;
                            }
                            else if (posXaux > 97.5f)
                            {
                                posXaux = 97.5f;
                            }

                            if (posYaux < -377)
                            {
                                posYaux = -377;
                            }
                            else if (posYaux > -346.5f)
                            {
                                posYaux = -346.5f;
                            }
                        }

                        break;
                    case 2: //R5
                        if (posXaux < 61.5f)
                        {
                            posXaux = 61.5f;
                        }
                        else if (posXaux > 99)
                        {
                            posXaux = 99;
                        }

                        if (posYaux < -330)
                        {
                            posYaux = -330;
                        }
                        else if (posYaux > -291)
                        {
                            posYaux = -291;
                        }

                        break;
                    case 3: //El paso
                        if (baseDeDatos.zonaCamara == 0) //Casa1
                        {
                            posXaux = 60.5f;

                            if (posYaux < -208.5f)
                            {
                                posYaux = -208.5f;
                            }
                            else if (posYaux > -205.5f)
                            {
                                posYaux = -205.5f;
                            }
                            else
                            {
                                posYaux = target.position.y;
                            }
                        }
                        else if (baseDeDatos.zonaCamara == 1) //Casa2
                        {
                            posXaux = 82.5f;

                            if (posYaux < -208.5f)
                            {
                                posYaux = -208.5f;
                            }
                            else if (posYaux > -205.5f)
                            {
                                posYaux = -205.5f;
                            }
                            else
                            {
                                posYaux = target.position.y;
                            }
                        }
                        else if (baseDeDatos.zonaCamara == 2) //Posada
                        {
                            posXaux = 106f;

                            if (posYaux < -208.5f)
                            {
                                posYaux = -208.5f;
                            }
                            else if (posYaux > -205.5f)
                            {
                                posYaux = -205.5f;
                            }
                            else
                            {
                                posYaux = target.position.y;
                            }
                        }
                        else //Calle
                        {
                            if (posXaux < 62)
                            {
                                posXaux = 62;
                            }
                            else if (posXaux > 97.5f)
                            {
                                posXaux = 97.5f;
                            }

                            if (posYaux < -274)
                            {
                                posYaux = -274;
                            }
                            else if (posYaux > -234.5f)
                            {
                                posYaux = -234.5f;
                            }
                        }

                        break;
                    case 4: //R6
                        if (posXaux < -42)
                        {
                            posXaux = -42;
                        }
                        else if (posXaux > 35)
                        {
                            posXaux = 35;
                        }

                        if (posYaux < -330.5f)
                        {
                            posYaux = -330.5f;
                        }
                        else if (posYaux > -256)
                        {
                            posYaux = -256;
                        }

                        break;
                    case 5: //Pedran
                        if (baseDeDatos.zonaCamara == 0) //Calle
                        {
                            if (posXaux < -187.5f)
                            {
                                posXaux = -187.5f;
                            }
                            else if (posXaux > -79)
                            {
                                posXaux = -79;
                            }

                            if (posYaux < -320)
                            {
                                posYaux = -320;
                            }
                            else if (posYaux > -255)
                            {
                                posYaux = -255;
                            }
                        }
                        else if (baseDeDatos.zonaCamara == 1) //Casa1
                        {
                            posXaux = -49;
                            posYaux = -257.5f;
                        }
                        else if (baseDeDatos.zonaCamara == 2) //Dragón Rojo
                        {
                            posXaux = -234.5f;

                            if (posYaux < -268.5f)
                            {
                                posYaux = -268.5f;
                            }
                            else if (posYaux > -262.5f)
                            {
                                posYaux = -262.5f;
                            }
                            else
                            {
                                posYaux = target.position.y;
                            }
                        }
                        else if (baseDeDatos.zonaCamara == 3) //Floristería
                        {
                            posXaux = -234.5f;

                            if (posYaux < -235.6f)
                            {
                                posYaux = -235.6f;
                            }
                            else if (posYaux > -228.5f)
                            {
                                posYaux = -228.5f;
                            }
                            else
                            {
                                posYaux = target.position.y;
                            }
                        }
                        else if (baseDeDatos.zonaCamara == 4) //GuardiaPB
                        {
                            if (posXaux < -241.3f)
                            {
                                posXaux = -241.3f;
                            }
                            else if (posXaux > -232.5f)
                            {
                                posXaux = -232.5f;
                            }

                            if (posYaux < -311.4f)
                            {
                                posYaux = -311.4f;
                            }
                            else if (posYaux > -300.4f)
                            {
                                posYaux = -300.4f;
                            }
                        }
                        else if (baseDeDatos.zonaCamara == 5) //GuardiaP1
                        {

                        }
                        else if (baseDeDatos.zonaCamara == 6) //Parada
                        {
                            posXaux = -282;

                            if (posYaux < -267.6f)
                            {
                                posYaux = -267.6f;
                            }
                            else if (posYaux > -262.5f)
                            {
                                posYaux = -262.5f;
                            }
                            else
                            {
                                posYaux = target.position.y;
                            }
                        }
                        else if (baseDeDatos.zonaCamara == 7) //Academia
                        {
                            posXaux = -305;

                            if (posYaux < -268.6f)
                            {
                                posYaux = -268.6f;
                            }
                            else if (posYaux > -261.2f)
                            {
                                posYaux = -261.2f;
                            }
                            else
                            {
                                posYaux = target.position.y;
                            }
                        }
                        else if (baseDeDatos.zonaCamara == 8) //Casa6
                        {
                            posXaux = -258.5f;

                            if (posYaux < -265.5f)
                            {
                                posYaux = -265.5f;
                            }
                            else if (posYaux > -264.4f)
                            {
                                posYaux = -264.4f;
                            }
                            else
                            {
                                posYaux = target.position.y;
                            }
                        }
                        else if (baseDeDatos.zonaCamara == 9) //Casa Grande
                        {
                            if (posXaux < -275.3f)
                            {
                                posXaux = -275.3f;
                            }
                            else if (posXaux > -270.7f)
                            {
                                posXaux = -270.7f;
                            }
                            else
                            {
                                posXaux = target.position.x;
                            }

                            if (posYaux < -292.6f)
                            {
                                posYaux = -292.6f;
                            }
                            else if (posYaux > -289.4f)
                            {
                                posYaux = -289.4f;
                            }
                            else
                            {
                                posYaux = target.position.y;
                            }
                        }
                        else if (baseDeDatos.zonaCamara == 10) //Marabunta
                        {
                            posXaux = -309;

                            if (posYaux < -295.6f)
                            {
                                posYaux = -295.6f;
                            }
                            else if (posYaux > -290)
                            {
                                posYaux = -290;
                            }
                            else
                            {
                                posYaux = target.position.y;
                            }
                        }
                        else if (baseDeDatos.zonaCamara == 11) //Resi1
                        {
                            posXaux = -259;

                            if (posYaux < -216.6f)
                            {
                                posYaux = -216.6f;
                            }
                            else if (posYaux > -210.4f)
                            {
                                posYaux = -210.4f;
                            }
                            else
                            {
                                posYaux = target.position.y;
                            }
                        }
                        else if (baseDeDatos.zonaCamara == 12) //Resi2
                        {
                            posXaux = -259;

                            if (posYaux < -242.5f)
                            {
                                posYaux = -242.5f;
                            }
                            else if (posYaux > -236.4f)
                            {
                                posYaux = -236.4f;
                            }
                            else
                            {
                                posYaux = target.position.y;
                            }
                        }
                        else if (baseDeDatos.zonaCamara == 13) //Casa
                        {
                            posXaux = -282;

                            if (posYaux < -242.6f)
                            {
                                posYaux = -242.6f;
                            }
                            else if (posYaux > -241.3f)
                            {
                                posYaux = -241.3f;
                            }
                            else
                            {
                                posYaux = target.position.y;
                            }
                        }
                        break;
                    case 6://R7
                        if (posXaux < -178)
                        {
                            posXaux = -178;
                        }
                        else if (posXaux > -92)
                        {
                            posXaux = -92;
                        }

                        if (posYaux < -233)
                        {
                            posYaux = -233;
                        }
                        else if (posYaux > -129)
                        {
                            posYaux = -129;
                        }

                        break;
                    case 7:
 
                        break;
                    case 8:

                        break;
                    case 9:

                        break;
                    case 10:

                        break;
                    case 11: //Universidad 1
                        if (baseDeDatos.zonaCamara == 0) //Patio
                        {
                            if (posXaux < -170)
                            {
                                posXaux = -170;
                            }
                            else if (posXaux > -104)
                            {
                                posXaux = -104;
                            }

                            if (posYaux < -92)
                            {
                                posYaux = -92;
                            }
                            else if (posYaux > -50)
                            {
                                posYaux = -50;
                            }
                        }
                        else if (baseDeDatos.zonaCamara == 1) //Edificio Oeste PB
                        {
                            if (posXaux < -230.2f)
                            {
                                posXaux = -230.2f;
                            }
                            else if (posXaux > -207.8f)
                            {
                                posXaux = -207.8f;
                            }

                            if (posYaux < -87.5f)
                            {
                                posYaux = -87.5f;
                            }
                            else if (posYaux > -56.5f)
                            {
                                posYaux = -56.5f;
                            }
                        }
                        else if (baseDeDatos.zonaCamara == 2)//Aula 0.1
                        {
                            posXaux = -334;

                            if (posYaux < -19.5f)
                            {
                                posYaux = -19.5f;
                            }
                            else if (posYaux > -16.5f)
                            {
                                posYaux = -16.5f;
                            }
                        }
                        else if (baseDeDatos.zonaCamara == 3)//Aula 0.2 Cambiar x y
                        {
                            if (posXaux < -337.5f)
                            {
                                posXaux = -337.5f;
                            }
                            else if (posXaux > -331.5f)
                            {
                                posXaux = -331.5f;
                            }

                            if (posYaux < -41)
                            {
                                posYaux = -41;
                            }
                            else if (posYaux > -38)
                            {
                                posYaux = -38;
                            }
                        }
                        else if (baseDeDatos.zonaCamara == 4)//Aula 0.3
                        {
                            posXaux = -390;

                            if (posYaux < -17.5f)
                            {
                                posYaux = -17.5f;
                            }
                            else if (posYaux > -15.5f)
                            {
                                posYaux = -15.5f;
                            }
                        }
                        else if (baseDeDatos.zonaCamara == 5)//Aula 0.4
                        {
                            posXaux = -390;

                            if (posYaux < -42.5f)
                            {
                                posYaux = -42.5f;
                            }
                            else if (posYaux > -39.5f)
                            {
                                posYaux = -39.5f;
                            }
                        }
                        else if (baseDeDatos.zonaCamara == 6)//P1
                        {
                            if (posXaux < -230)
                            {
                                posXaux = -230;
                            }
                            else if (posXaux > -208)
                            {
                                posXaux = -208;
                            }

                            if (posYaux < -21.5f)
                            {
                                posYaux = -21.5f;
                            }
                            else if (posYaux > 5.5f)
                            {
                                posYaux = 5.5f;
                            }
                        }
                        else if (baseDeDatos.zonaCamara == 7)//Despacho Politica
                        {
                            posXaux = -267.5f;
                            posYaux = -35;
                        }
                        else if (baseDeDatos.zonaCamara == 8)//Despacho Esgrima
                        {
                            posXaux = -266;
                            posYaux = -52;
                        }
                        else if (baseDeDatos.zonaCamara == 9)//Despacho MagiaB
                        {
                            posXaux = -266;
                            posYaux = -86;
                        }
                        else if (baseDeDatos.zonaCamara == 10)//Despacho MagiaN
                        {
                            posXaux = -266;
                            posYaux = -70;
                        }
                        else if (baseDeDatos.zonaCamara == 11)//Sala Profesores
                        {
                            posXaux = -299;
                            posYaux = -35;
                        }
                        else if (baseDeDatos.zonaCamara == 12)//Almacen
                        {
                            posXaux = -295;
                            posYaux = -61;
                        }
                        else if (baseDeDatos.zonaCamara == 13)//Sala Castigo
                        {
                            posXaux = -295;
                            posYaux = -48;
                        }
                        else if (baseDeDatos.zonaCamara == 14) //Ala norte PB
                        {
                            if (posXaux < -149)
                            {
                                posXaux = -149;
                            }
                            else if (posXaux > -116.8f)
                            {
                                posXaux = -116.8f;
                            }

                            if (posYaux < -23.5f)
                            {
                                posYaux = -23.5f;
                            }
                            else if (posYaux > 6.5f)
                            {
                                posYaux = 6.5f;
                            }
                        }
                        else if (baseDeDatos.zonaCamara == 15) //Ala norte P1
                        {
                            if (posXaux < -149)
                            {
                                posXaux = -149;
                            }
                            else if (posXaux > -115.5f)
                            {
                                posXaux = -115.5f;
                            }

                            if (posYaux < 31)
                            {
                                posYaux = 31;
                            }
                            else if (posYaux > 63.5f)
                            {
                                posYaux = 63.5f;
                            }
                        }
                        else if (baseDeDatos.zonaCamara == 16) //Ala norte P2
                        {
                            posXaux = -136;
                            posYaux = 96;
                        }
                        else if (baseDeDatos.zonaCamara == 17) //Despacho Director
                        {
                            posXaux = -106;
                            posYaux = 97.4f;
                        }

                        break;
                    case 12: //R10
                        if (posXaux > -110)
                        {
                            posXaux = -110;
                        }
                        else if (posXaux < -162)
                        {
                            posXaux = -162;
                        }

                        if (posYaux < -420)
                        {
                            posYaux = -420;
                        }
                        else if (posYaux > -340)
                        {
                            posYaux = -340;
                        }

                        break;
                    case 13: //Canda
                        if (posXaux < -144)
                        {
                            posXaux = -144;
                        }
                        else if (posXaux > -90)
                        {
                            posXaux = -90;
                        }

                        if (posYaux < -477)
                        {
                            posYaux = -477;
                        }
                        else if (posYaux > -441.5f)
                        {
                            posYaux = -441.5f;
                        }

                        break;
                    case 14: //Bosque Esperanza
                        if (posXaux < -142)
                        {
                            posXaux = -142;
                        }
                        else if (posXaux > -97)
                        {
                            posXaux = -97;
                        }

                        if (posYaux < -541)
                        {
                            posYaux = -541;
                        }
                        else if (posYaux > -505.5f)
                        {
                            posYaux = -505.5f;
                        }

                        break;
                    case 15://R11
                        if (baseDeDatos.zonaCamara == 0)
                        {
                            if (posXaux < -449.7f)
                            {
                                posXaux = -449.7f;
                            }
                            else if (posXaux > -353.3)
                            {
                                posXaux = -353.3f;
                            }

                            if (posYaux < -320)
                            {
                                posYaux = -320;
                            }
                            else if (posYaux > -261)
                            {
                                posYaux = -261;
                            }
                        }
                        else if (baseDeDatos.zonaCamara == 1)
                        {

                        }

                        break;
                    case 16: //Uni2

                        break;
                    case 18: //Pueblo Refugio

                        break;
                    case 19: //Gran Gruta

                        break;
                    case 20: //Pueblo Arena

                        break;
                    case 21: //R12
                        if (baseDeDatos.zonaCamara == 0)
                        {
                            if (posXaux < -981.7f)
                            {
                                posXaux = -981.7f;
                            }
                            else if (posXaux > -907.3f)
                            {
                                posXaux = -907.3f;
                            }

                            if (posYaux < -169)
                            {
                                posYaux = -169;
                            }
                            else if (posYaux > -69.3f)
                            {
                                posYaux = -69.3f;
                            }
                        }
                        else if (baseDeDatos.zonaCamara == 1)
                        {
                            posXaux = -1036.5f;

                            if (posYaux < -145.6f)
                            {
                                posYaux = -145.6f;
                            }
                            else if (posYaux > -144.4f)
                            {
                                posYaux = -144.4f;
                            }
                        }
                        
                        break;
                    case 22: //Manfa
                        if (baseDeDatos.zonaCamara == 0) //Calle
                        {
                            if (posXaux < -966)
                            {
                                posXaux = -966;
                            }
                            else if (posXaux > -928)
                            {
                                posXaux = -928;
                            }

                            if (posYaux < -44.3f)
                            {
                                posYaux = -44.3f;
                            }
                            else if (posYaux > -1.5f)
                            {
                                posYaux = -1.5f;
                            }
                        }
                        break;
                    case 23: //R4

                        break;
                    case 24: //Templo

                        break;
                    case 25: //R1

                        break;
                    case 26: //Ciudad Imperial

                        break;
                    case 27: //R2

                        break;
                    case 28: //Pueblo Costa

                        break;
                    case 29: //R3

                        break;
                    case 30: //Base resistencia
                        if (baseDeDatos.zonaCamara == 0) //Caseta
                        {
                            posXaux = -149;
                            posYaux = -557.5f;
                        }
                        else if (baseDeDatos.zonaCamara == 1) //PB
                        {
                            if (posYaux < -599.5f)
                            {
                                posYaux = -599.5f;
                            }
                            else if (posYaux > -582.3)
                            {
                                posYaux = -582.3f;
                            }

                            if (posXaux < -140.5f)
                            {
                                posXaux = -140.5f;
                            }
                            else if (posXaux > -123.5f)
                            {
                                posXaux = -123.5f;
                            }
                        }
                        else if (baseDeDatos.zonaCamara == 2) //Sala reuniones
                        {
                            posXaux = -168;

                            if (posYaux < -582.4f)
                            {
                                posYaux = -582.4f;
                            }
                            else if (posYaux > -580.4f)
                            {
                                posYaux = -580.4f;
                            }
                        }
                        else if (baseDeDatos.zonaCamara == 3) //almacen
                        {
                            posXaux = -167;
                            posYaux = -617.8f;
                        }
                        else if (baseDeDatos.zonaCamara == 4) //cafetería
                        {
                            if (posYaux < -643.5f)
                            {
                                posYaux = -643.5f;
                            }
                            else if (posYaux > -640.3f)
                            {
                                posYaux = -640.3f;
                            }

                            if (posXaux < -208.2f)
                            {
                                posXaux = -208.2f;
                            }
                            else if (posXaux > -192.8f)
                            {
                                posXaux = -192.8f;
                            }
                        }
                        else if (baseDeDatos.zonaCamara == 5) //tienda
                        {
                            if (posYaux < -600.5f)
                            {
                                posYaux = -600.5f;
                            }
                            else if (posYaux > -598)
                            {
                                posYaux = -598;
                            }

                            posXaux = -168;
                        }
                        else if (baseDeDatos.zonaCamara == 6) //P1
                        {
                            if (posYaux < -648.4f)
                            {
                                posYaux = -648.4f;
                            }
                            else if (posYaux > -632.3f)
                            {
                                posYaux = -632.3f;
                            }

                            if (posXaux < -140.2f)
                            {
                                posXaux = -140.2f;
                            }
                            else if (posXaux > -120.7f)
                            {
                                posXaux = -120.7f;
                            }
                        }
                        else if (baseDeDatos.zonaCamara == 7) //Despacho General
                        {
                            posXaux = -84;
                            posYaux = -628.4f;
                        }
                        else if (baseDeDatos.zonaCamara == 8)//enfermería
                        {
                            posXaux = -81;

                            if (posYaux < -657.4f)
                            {
                                posYaux = -657.4f;
                            }
                            else if (posYaux > -651.4f)
                            {
                                posYaux = -651.4f;
                            }
                        }
                        else if (baseDeDatos.zonaCamara == 9)//Laboratorio
                        {
                            if (posXaux < -79)
                            {
                                posXaux = -79;
                            }
                            else if (posXaux > -78)
                            {
                                posXaux = -78;
                            }

                            if (posYaux < -603.4f)
                            {
                                posYaux = -603.4f;
                            }
                            else if (posYaux > -594.5f)
                            {
                                posYaux = -594.5f;
                            }
                        }
                        else if (baseDeDatos.zonaCamara == 10) //Despacho Laboratorio
                        {
                            posXaux = -78.5f;
                            posYaux = -572.9f;
                        }
                        else if (baseDeDatos.zonaCamara == 11) //Almacen Cocina
                        {
                            posXaux = -232.7f;
                            posYaux = -638.4f;
                        }
                        
                        break;
                    case 31: //Porto Belo

                        break;
                }

                transform.position = new Vector3(
                    posXaux,
                    posYaux,
                    -15
                    );
            }
            else
            {
                transform.position = new Vector3(
                56.9f,
                -27,
                -15
                );
            }
        }
        else
        {
            transform.position = new Vector3(
                posx,
                posy,
                posz
                );
        }
	}



    public void IniciaHistoria(int indice)
    {
        historia = true;

        switch (indice)
        {
            case 0://Historia Inicial
                posx = 106.5f;
                posy = -412f;
                posz = -15;

                transform.position = new Vector3(
                posx,
                posy,
                posz
                );
                break;
            case 1://Historia Inicial 2
                posx = 92;
                posy = -373.5f;
                posz = -15;

                transform.position = new Vector3(
                posx,
                posy,
                posz
                );
                break;
            case 2://Taberna El Paso
                posx = 105;
                posy = -210;
                posz = -15;

                transform.position = new Vector3(
                posx,
                posy,
                posz
                );
                break;
            case 3://Guardia1
                posx = -240.6f;
                posy = -305;
                posz = -15;

                transform.position = new Vector3(
                posx,
                posy,
                posz
                );
                break;
            case 4: // Inauguración
                posx = -110;
                posy = 39;
                posz = -15;

                transform.position = new Vector3(
                posx,
                posy,
                posz
                );
                break;
            case 5: // Inauguración 2
                posx = -81.7f;
                posy = -656;
                posz = -15;

                transform.position = new Vector3(
                posx,
                posy,
                posz
                );
                break;
            case 6: // DespachoGeneral
                posx = -84;
                posy = -628.4f;
                posz = -15;
                
                transform.position = new Vector3(
                posx,
                posy,
                posz
                );
                break;
            case 7: // Rescate
                posx = -215;
                posy = -80.5f;
                posz = -15;

                transform.position = new Vector3(
                posx,
                posy,
                posz
                );
                break;
            case 8: // Inicia Teleport
                posx = -135;
                posy = -320;
                posz = -15;

                transform.position = new Vector3(
                posx,
                posy,
                posz
                );
                break;
        }
    }



    public void TerminaHistoria()
    {
        historia = false;
        combate = false;
    }



    public void FijaCamara(int indiceAreaDestino, int indiceZonaDestino)
    {
        if (portalAbierto)
        {
            for (int i = 0; i < numeroPortales; i++)
            {
                Teleport teleport = Teleportes.transform.GetChild(i).GetComponent<Teleport>();
                teleport.ApagaTeleport();
            }

            portalAbierto = false;
        }

        baseDeDatos.areaCamara = indiceAreaDestino;
        baseDeDatos.zonaCamara = indiceZonaDestino;
    }



    public void SetPortalActivo(bool valor)
    {
        portalAbierto = valor;
    }
}  
