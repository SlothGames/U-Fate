using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapaActivo : MonoBehaviour
{
    public GameObject escenarios;

    public void EnciendeMapa(int indiceMapa)
    {
        switch (indiceMapa)
        {
            case 0:
                escenarios.transform.GetChild(1).gameObject.SetActive(true);
                escenarios.transform.GetChild(2).gameObject.SetActive(true);

                escenarios.transform.GetChild(3).gameObject.SetActive(false);
                break;
            case 1:
                escenarios.transform.GetChild(1).gameObject.SetActive(true);
                escenarios.transform.GetChild(2).gameObject.SetActive(true);
                escenarios.transform.GetChild(3).gameObject.SetActive(true);

                escenarios.transform.GetChild(4).gameObject.SetActive(false);
                escenarios.transform.GetChild(23).gameObject.SetActive(false);
                break;
            case 2:
                escenarios.transform.GetChild(2).gameObject.SetActive(true);
                escenarios.transform.GetChild(3).gameObject.SetActive(true);
                escenarios.transform.GetChild(4).gameObject.SetActive(true);
                escenarios.transform.GetChild(23).gameObject.SetActive(true);

                escenarios.transform.GetChild(1).gameObject.SetActive(false);
                escenarios.transform.GetChild(5).gameObject.SetActive(false);
                escenarios.transform.GetChild(24).gameObject.SetActive(false);
                escenarios.transform.GetChild(25).gameObject.SetActive(false);
                break;
            case 3:
                escenarios.transform.GetChild(3).gameObject.SetActive(true);
                escenarios.transform.GetChild(4).gameObject.SetActive(true);
                escenarios.transform.GetChild(5).gameObject.SetActive(true);

                escenarios.transform.GetChild(2).gameObject.SetActive(false);
                escenarios.transform.GetChild(6).gameObject.SetActive(false);
                escenarios.transform.GetChild(12).gameObject.SetActive(false);
                escenarios.transform.GetChild(15).gameObject.SetActive(false);
                escenarios.transform.GetChild(23).gameObject.SetActive(false);
                break;
            case 4:
                escenarios.transform.GetChild(4).gameObject.SetActive(true);
                escenarios.transform.GetChild(5).gameObject.SetActive(true);
                escenarios.transform.GetChild(6).gameObject.SetActive(true);
                escenarios.transform.GetChild(12).gameObject.SetActive(true);
                escenarios.transform.GetChild(15).gameObject.SetActive(true);

                escenarios.transform.GetChild(3).gameObject.SetActive(false);
                escenarios.transform.GetChild(7).gameObject.SetActive(false);
                escenarios.transform.GetChild(9).gameObject.SetActive(false);
                escenarios.transform.GetChild(11).gameObject.SetActive(false);
                escenarios.transform.GetChild(13).gameObject.SetActive(false);
                escenarios.transform.GetChild(16).gameObject.SetActive(false);
                escenarios.transform.GetChild(18).gameObject.SetActive(false);
                break;
            case 5:
                escenarios.transform.GetChild(5).gameObject.SetActive(true);
                escenarios.transform.GetChild(6).gameObject.SetActive(true);
                escenarios.transform.GetChild(7).gameObject.SetActive(true);
                escenarios.transform.GetChild(9).gameObject.SetActive(true);
                escenarios.transform.GetChild(11).gameObject.SetActive(true);

                escenarios.transform.GetChild(4).gameObject.SetActive(false);
                escenarios.transform.GetChild(8).gameObject.SetActive(false);
                escenarios.transform.GetChild(10).gameObject.SetActive(false);
                escenarios.transform.GetChild(12).gameObject.SetActive(false);
                escenarios.transform.GetChild(15).gameObject.SetActive(false);
                break;
            case 6:
                escenarios.transform.GetChild(6).gameObject.SetActive(true);
                escenarios.transform.GetChild(7).gameObject.SetActive(true);
                escenarios.transform.GetChild(8).gameObject.SetActive(true);

                escenarios.transform.GetChild(11).gameObject.SetActive(false);
                escenarios.transform.GetChild(5).gameObject.SetActive(false);
                escenarios.transform.GetChild(9).gameObject.SetActive(false);
                break;
            case 7:
                escenarios.transform.GetChild(7).gameObject.SetActive(true);
                escenarios.transform.GetChild(8).gameObject.SetActive(true);

                escenarios.transform.GetChild(6).gameObject.SetActive(false);
                break;
            case 8:
                escenarios.transform.GetChild(6).gameObject.SetActive(true);
                escenarios.transform.GetChild(9).gameObject.SetActive(true);
                escenarios.transform.GetChild(10).gameObject.SetActive(true);

                escenarios.transform.GetChild(5).gameObject.SetActive(false);
                escenarios.transform.GetChild(7).gameObject.SetActive(false);
                escenarios.transform.GetChild(11).gameObject.SetActive(false);
                break;
            case 9:
                escenarios.transform.GetChild(9).gameObject.SetActive(true);
                escenarios.transform.GetChild(10).gameObject.SetActive(true);

                escenarios.transform.GetChild(6).gameObject.SetActive(false);
                break;
            case 10:
                escenarios.transform.GetChild(6).gameObject.SetActive(true);
                escenarios.transform.GetChild(11).gameObject.SetActive(true);

                escenarios.transform.GetChild(5).gameObject.SetActive(false);
                escenarios.transform.GetChild(7).gameObject.SetActive(false);
                escenarios.transform.GetChild(9).gameObject.SetActive(false);
                break;
            case 11:
                escenarios.transform.GetChild(5).gameObject.SetActive(true);
                escenarios.transform.GetChild(12).gameObject.SetActive(true);
                escenarios.transform.GetChild(13).gameObject.SetActive(true);

                escenarios.transform.GetChild(4).gameObject.SetActive(false);
                escenarios.transform.GetChild(6).gameObject.SetActive(false);
                escenarios.transform.GetChild(14).gameObject.SetActive(false);
                escenarios.transform.GetChild(15).gameObject.SetActive(false);
                break;
            case 12:
                escenarios.transform.GetChild(12).gameObject.SetActive(true);
                escenarios.transform.GetChild(13).gameObject.SetActive(true);
                escenarios.transform.GetChild(14).gameObject.SetActive(true);

                escenarios.transform.GetChild(5).gameObject.SetActive(false);
                break;
            case 13:
                escenarios.transform.GetChild(13).gameObject.SetActive(true);
                escenarios.transform.GetChild(14).gameObject.SetActive(true);

                escenarios.transform.GetChild(12).gameObject.SetActive(false);
                break;
            case 14:
                escenarios.transform.GetChild(5).gameObject.SetActive(true);
                escenarios.transform.GetChild(15).gameObject.SetActive(true);
                escenarios.transform.GetChild(16).gameObject.SetActive(true);
                escenarios.transform.GetChild(18).gameObject.SetActive(true);

                escenarios.transform.GetChild(4).gameObject.SetActive(false);
                escenarios.transform.GetChild(6).gameObject.SetActive(false);
                escenarios.transform.GetChild(12).gameObject.SetActive(false);
                break;
            case 15:
                escenarios.transform.GetChild(15).gameObject.SetActive(true);
                escenarios.transform.GetChild(16).gameObject.SetActive(true);

                escenarios.transform.GetChild(5).gameObject.SetActive(false);
                escenarios.transform.GetChild(18).gameObject.SetActive(false);
                break;
            case 17:
                escenarios.transform.GetChild(15).gameObject.SetActive(true);
                escenarios.transform.GetChild(18).gameObject.SetActive(true);
                escenarios.transform.GetChild(19).gameObject.SetActive(true);

                escenarios.transform.GetChild(5).gameObject.SetActive(false);
                escenarios.transform.GetChild(16).gameObject.SetActive(false);
                escenarios.transform.GetChild(20).gameObject.SetActive(false);
                break;
            case 18:
                escenarios.transform.GetChild(18).gameObject.SetActive(true);
                escenarios.transform.GetChild(19).gameObject.SetActive(true);
                escenarios.transform.GetChild(20).gameObject.SetActive(true);

                escenarios.transform.GetChild(15).gameObject.SetActive(false);
                escenarios.transform.GetChild(21).gameObject.SetActive(false);
                break;
            case 19:
                escenarios.transform.GetChild(19).gameObject.SetActive(true);
                escenarios.transform.GetChild(20).gameObject.SetActive(true);
                escenarios.transform.GetChild(21).gameObject.SetActive(true);

                escenarios.transform.GetChild(18).gameObject.SetActive(false);
                escenarios.transform.GetChild(22).gameObject.SetActive(false);
                break;
            case 20:
                escenarios.transform.GetChild(20).gameObject.SetActive(true);
                escenarios.transform.GetChild(21).gameObject.SetActive(true);
                escenarios.transform.GetChild(22).gameObject.SetActive(true);

                escenarios.transform.GetChild(19).gameObject.SetActive(false);
                break;
            case 21:
                escenarios.transform.GetChild(21).gameObject.SetActive(true);
                escenarios.transform.GetChild(22).gameObject.SetActive(true);

                escenarios.transform.GetChild(20).gameObject.SetActive(false);
                break;
            case 22:
                escenarios.transform.GetChild(3).gameObject.SetActive(true);
                escenarios.transform.GetChild(23).gameObject.SetActive(true);
                escenarios.transform.GetChild(24).gameObject.SetActive(true);
                escenarios.transform.GetChild(25).gameObject.SetActive(true);

                escenarios.transform.GetChild(2).gameObject.SetActive(false);
                escenarios.transform.GetChild(26).gameObject.SetActive(false);
                escenarios.transform.GetChild(27).gameObject.SetActive(false);
                break;
            case 23:
                escenarios.transform.GetChild(23).gameObject.SetActive(true);
                escenarios.transform.GetChild(24).gameObject.SetActive(true);

                escenarios.transform.GetChild(3).gameObject.SetActive(false);
                escenarios.transform.GetChild(25).gameObject.SetActive(false);
                break;
            case 24:
                escenarios.transform.GetChild(23).gameObject.SetActive(true);
                escenarios.transform.GetChild(25).gameObject.SetActive(true);
                escenarios.transform.GetChild(26).gameObject.SetActive(true);
                escenarios.transform.GetChild(27).gameObject.SetActive(true);

                escenarios.transform.GetChild(3).gameObject.SetActive(false);
                escenarios.transform.GetChild(24).gameObject.SetActive(false);
                escenarios.transform.GetChild(28).gameObject.SetActive(false);
                break;
            case 25:
                escenarios.transform.GetChild(25).gameObject.SetActive(true);
                escenarios.transform.GetChild(26).gameObject.SetActive(true);

                escenarios.transform.GetChild(23).gameObject.SetActive(false);
                escenarios.transform.GetChild(27).gameObject.SetActive(false);
                break;
            case 26:
                escenarios.transform.GetChild(25).gameObject.SetActive(true);
                escenarios.transform.GetChild(27).gameObject.SetActive(true);
                escenarios.transform.GetChild(28).gameObject.SetActive(true);

                escenarios.transform.GetChild(23).gameObject.SetActive(false);
                escenarios.transform.GetChild(26).gameObject.SetActive(false);
                escenarios.transform.GetChild(29).gameObject.SetActive(false);
                break;
            case 27:
                escenarios.transform.GetChild(27).gameObject.SetActive(true);
                escenarios.transform.GetChild(28).gameObject.SetActive(true);
                escenarios.transform.GetChild(29).gameObject.SetActive(true);

                escenarios.transform.GetChild(25).gameObject.SetActive(false);
                escenarios.transform.GetChild(30).gameObject.SetActive(false);
                break;
            case 28:
                escenarios.transform.GetChild(28).gameObject.SetActive(true);
                escenarios.transform.GetChild(29).gameObject.SetActive(true);
                escenarios.transform.GetChild(30).gameObject.SetActive(true);

                escenarios.transform.GetChild(27).gameObject.SetActive(false);
                break;
            case 29:
                escenarios.transform.GetChild(29).gameObject.SetActive(true);
                escenarios.transform.GetChild(30).gameObject.SetActive(true);

                escenarios.transform.GetChild(28).gameObject.SetActive(false);
                break;
        }
    }



    public void IniciaMapas(int indiceInicial)
    {
        for (int i = 1; i < 32; i++)
        {
            escenarios.transform.GetChild(i).gameObject.SetActive(false);
        }

        EnciendeMapa(indiceInicial);
    }
}
