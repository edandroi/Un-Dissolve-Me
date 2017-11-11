using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backward : MonoBehaviour {
	public GameObject ButtonBackward;
	public int BackwardNum;
	public bool isClicked;
	// Use this for initialization
	void Start () {
		isClicked = false;
	}
	
	//this function is called if the object is clicked
	void OnMouseDown(){
		isClicked = true;
	}
}
