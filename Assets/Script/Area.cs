using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Area : MonoBehaviour {

	Animator anim;
    BaseDatos baseDeDatos;


	void Start ()
    {
		anim = GetComponent<Animator> ();
        baseDeDatos = GameObject.Find("GameManager").GetComponent<BaseDatos>();
    }




	public IEnumerator ShowArea (string name, int nivel){
		anim.Play ("Area-Show");
		transform.GetChild (0).GetComponent<Text> ().text = name;
		transform.GetChild (1).GetComponent<Text> ().text = name;

        if(nivel != -1)
        {
            if(baseDeDatos.idioma == 1)
            {
                transform.GetChild(3).GetComponent<Text>().text = "LV. recommended: " + nivel;
            }
            else
            {
                transform.GetChild(3).GetComponent<Text>().text = "LV. recomendado: " + nivel;
            }
        }
        else
        {
            transform.GetChild(3).GetComponent<Text>().text = "";
        }

		yield return new WaitForSeconds (1f);
		anim.Play ("Area-FadeOut"); 
	}
}
