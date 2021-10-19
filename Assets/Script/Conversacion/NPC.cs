using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour 
{
    ControlJugador controlJugador;

    private void Start()
    {
        controlJugador = GameObject.Find("Player").GetComponent<ControlJugador>();
    }



    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            controlJugador.SetConversacion(true);
        }

        yield return null;
    }



    private IEnumerator OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            controlJugador.SetConversacion(false);
        }

        yield return null;
    }
}
