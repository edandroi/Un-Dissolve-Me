using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatAnimationsBackward : MonoBehaviour {
	Backward script;
	public bool clickedBack;
	Animator anim;
	// Use this for initialization
	void Start () {
		script = GameObject.Find("Button Backward").GetComponent<Backward>();
		clickedBack = false;

		anim = GetComponent<Animator> ();

	//	anim.SetBool ("ClickedB",clickedBack);
		
	}
	
	// Update is called once per frame
	void Update () {
		if (script.isClicked == false) {
			
			clickedBack = false;
		//	anim.SetBool ("ClickedB",clickedBack);
		}

		if (script.isClicked == true) {
			
			clickedBack  = true;
		//	anim.SetBool ("ClickedB",clickedBack);


		}
	}
}
