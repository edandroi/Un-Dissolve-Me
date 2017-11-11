using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : MonoBehaviour {

		public bool isSinking;
		public float speed;
		Animator anim;

		// Use this for initialization
		void Start () {
			isSinking = false;
			anim = GetComponent<Animator> ();
			anim.SetBool ("Sinking", isSinking);
			anim.SetFloat ("Speed", speed);
		}

		// Update is called once per frame
		void Update () {
			if (Input.GetKeyDown (KeyCode.Space)) {
				isSinking = !isSinking;
				anim.SetBool ("Sinking", isSinking);

			}

		}
	}