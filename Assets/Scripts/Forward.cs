using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forward : MonoBehaviour {

	public GameObject ButtonForward;
	public int ButtonNumber;
	public bool isClicked;

	// Use this for initialization
	void Start () {

		isClicked = false;

	}
	
	// Update is called once per frame
	void Update () {

	
	}

	//this function is called if the object is clicked
	void OnMouseDown(){
		isClicked = true;
	}
}
