using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Destrucción de los objetos
*/

public class Destroyable : MonoBehaviour {
	public string destroyState;
	public float timeForDisable;
	Animator anim;

	void Start () {
		anim = GetComponent<Animator> ();
	}

	IEnumerator OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Attack") {
			anim.Play (destroyState);
			Destroy (col.gameObject);
			yield return new WaitForSeconds (timeForDisable);

			foreach (Collider2D c in GetComponents<Collider2D>()) {
				c.enabled = false;
			}
		}
	}

	void Update(){
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo (0);

		if (stateInfo.IsName (destroyState) && stateInfo.normalizedTime >= 1) {
			Destroy (gameObject);
		}
	}
}
