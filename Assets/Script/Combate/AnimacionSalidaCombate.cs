using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionSalidaCombate : MonoBehaviour {
    public static AnimacionSalidaCombate anima;
    Animator anim;

    public void Awake()
    {
        anim = GetComponent<Animator>();

        if (anima == null)
            anima = this;
    }

    public static void SalirCombate()
    {
        anima.IniciaEspera();
    }

    public void IniciaEspera()
    {
        anim.SetBool("acaba", true);
        StartCoroutine(Espera());
    }



    IEnumerator Espera()
    {
        yield return new WaitForSeconds(0.01f);
        anim.SetBool("acaba", false);
    }
}
