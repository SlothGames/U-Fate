using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrePuertas : MonoBehaviour {
    GameObject manager;
    ControlJugador controlJugador;
    public GameObject puertaObjetivo;
    public int zona, indice;
    public bool abierto, animacionActiva;
    Animator anim;

	
	void Start () {
        manager = GameObject.Find("GameManager");
        controlJugador = GameObject.Find("Player").GetComponent<ControlJugador>();

        anim = GetComponent<Animator>();
        animacionActiva = false;

        InicializaPorIndice();

        if (abierto)
        {
            anim.Play("Palanca_Idle_Right");
        }
        else
        {
            anim.Play("Palanca_Idle_Left");
        }

        puertaObjetivo.SetActive(!abierto);
    }
	


    void InicializaPorIndice()
    {
        switch (zona)
        {
            case 0:
                abierto = false;
                break;
        }
    }



    public void ActivarInterruptor()
    {
        if (!animacionActiva)
        {
            controlJugador.setMensajeActivo(true);
            controlJugador.SetInterrogante(false);

            animacionActiva = true;

            if (abierto)
            {
                abierto = false;
                anim.Play("Palanca_Move_Left");
            }
            else
            {
                abierto = true;
                anim.Play("Palanca_Move");
            }

            puertaObjetivo.SetActive(!abierto);
            StartCoroutine(Espera());
        }
        
    }



    IEnumerator Espera()
    {
        yield return new WaitForSeconds(0.25f);
        animacionActiva = false;
        controlJugador.setMensajeActivo(false);
        controlJugador.SetInterrogante(true);
    }



    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            controlJugador.SetInterrogante(true);
        }

        yield return null;
    }



    private IEnumerator OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            controlJugador.SetInterrogante(false);
        }

        yield return null;
    }
}
