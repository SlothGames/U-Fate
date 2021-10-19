using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionCombate : MonoBehaviour {
    public static AnimacionCombate anima;
    
    void Awake()
    {
        if (anima == null)
            anima = this;

        anima.gameObject.SetActive(false);
    }

    public static void IniciaCombate()
    {
        anima.gameObject.SetActive(true);
        AnimacionCombate.anima.IniciaEspera();
    }

    public void IniciaEspera()
    {
        StartCoroutine(Espera());
    }


    IEnumerator Espera()
    {
        yield return new WaitForSeconds(1);
        anima.gameObject.SetActive(false);
    }
}
