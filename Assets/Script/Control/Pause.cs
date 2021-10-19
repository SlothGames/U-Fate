using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {
	bool active;
	Canvas canvas;

	void Start () {
		/*
			Al iniciar el juego desactivamos el menú de pausa
		*/
		canvas = GetComponent<Canvas> ();
		canvas.enabled = false;
	}




	void Update () {
		/*
			Cuando pulsamos el botón esc activamos el booleano 
			active que muestra el menú pausa
		*/
		if (Input.GetKeyDown ("escape")) {
			active = !active;
			canvas.enabled = active;
			Time.timeScale = (active) ? 0 : 1f;
		}
		
	}
}
