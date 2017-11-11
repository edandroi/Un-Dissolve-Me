using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonAnimations : MonoBehaviour 
{
	public Forward[] forwardScripts;
	public Backward[] backwardScripts;
	private List<Vector2> positions;
	private Transform[] buttonHolders;
	//public bool clicked;
	public float speed;
	Animator anim;
//	int ButtonNum;

	float stoppingPoint;
	bool lastClickedForward;
	private int currentActiveForwardIndex;
	private int currentActiveBackwardIndex;

	// Use this for initialization
	void Start () {

		lastClickedForward = true;

		anim = GetComponent<Animator> ();

		anim.Play("Forward",0,0.6f); //play the state called 'Forward', but start at the halfway point
		stoppingPoint = 0.5f; //point in clip between 0 and 1 (1 is the end)
		anim.speed = 0;
		currentActiveForwardIndex = 0;
		currentActiveBackwardIndex = 0;

		buttonHolders = new Transform[2] {
			forwardScripts [0].transform.parent,
			backwardScripts [0].transform.parent
		};

		positions = new List<Vector2> ();
		for (int i = 0; i < buttonHolders.Length; i++) {
			positions.Add(buttonHolders [i].position);
		}


		for (var i = 0; i < forwardScripts.Length; i++) {
			//make the first button active, all the other ones inactive
			if (i == 0 ) {
				forwardScripts [i].gameObject.SetActive (true);
			} else {
				forwardScripts [i].gameObject.SetActive (false);
			}
		}

		for (var i = 0; i < backwardScripts.Length; i++) {
			//make the first button active, all the other ones inactive
			if (i == 0) {
				backwardScripts [i].gameObject.SetActive (true);
			} else {
				backwardScripts [i].gameObject.SetActive (false);
			}
		}

	}

	// Update is called once per frame
	void Update () {


		for (int i = 0; i < forwardScripts.Length; i++) {
			if (forwardScripts[i].isClicked == true) {
				if (lastClickedForward == false) { //if until now we were playing backwards
					stoppingPoint = 1.0f - stoppingPoint; //mirror for reverse state
				}
				anim.speed = 0.40f; //play back at half speed

				anim.Play ("Forward", 0, stoppingPoint); //start playing at the mirrored stopping point
				anim.Update(0); //we want the animator to notice we changed the state

				lastClickedForward = true;
				stoppingPoint += 0.12f; //stop at 1/4 of the time of the animation
				//todo: make sure this doesn't go above 1.0
				stoppingPoint = Mathf.Clamp01(stoppingPoint);

				forwardScripts[i].isClicked = false; //so this only runs once for every click


//				forwardScripts [i].gameObject.SetActive (false); //turn this one off
//				int nextIndex = i+1;
//				//u,nextIndex = Random.Range (0, forwardScripts.Length);
//				if (nextIndex >= forwardScripts.Length) {
//					nextIndex = 0; //loop around
//				}
//				forwardScripts[nextIndex].gameObject.SetActive(true);
//				currentActiveForwardIndex = nextIndex;
				ShiftButtons();
				RandomizePositions ();
			}

		

		}

		for (int i = 0; i < backwardScripts.Length; i++) {
			if (backwardScripts[i].isClicked == true) {
				if (lastClickedForward == true){//if until now we were playing forwards

					stoppingPoint = 1.0f - stoppingPoint; //mirror for reverse state
				}
				anim.speed = 0.40f; //play back at half speed

				anim.Play ("Backward",0,stoppingPoint); //start playing at the mirrored stopping point
				anim.Update(0);//we want the animator to notice we changed the state


				lastClickedForward = false;
				stoppingPoint += 0.12f; //stop at 1/4 of the time of the animation
				//todo: make sure this doesn't go below zero
				stoppingPoint = Mathf.Clamp01(stoppingPoint);
				backwardScripts[i].isClicked = false; //so this only runs once for every click

//				backwardScripts [i].gameObject.SetActive (false); //turn this one off
//				int nextIndex = i+1;
//				if (nextIndex >= backwardScripts.Length) {
//					nextIndex = 0; //loop around
//				}
//				backwardScripts[nextIndex].gameObject.SetActive(true);
//				currentActiveBackwardIndex = nextIndex;
				ShiftButtons();
				RandomizePositions ();
			}
		}


		//did the clip go past the stopping point? If so, stop it
		AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo(0);
		float playbackTime = currentState.normalizedTime % 1; //this will be 0 if we're at the start of the animation, 1 if we're at the end.
		if (stoppingPoint > 0.99f) {
			EndGameForward ();
		}
		if (playbackTime > stoppingPoint) { //we went past the stopping point -stop animating
			anim.speed = 0;
		}

	}

	void RandomizePositions(){
		List<Vector2> positionPool = new List<Vector2> ();
		for (int i = 0; i < positions.Count; i++) {
			positionPool.Add(positions[i]);
		}

		for (int i = 0; i < positions.Count; i++) {
			Vector2 randomPos = positionPool [Random.Range (0, positionPool.Count)];
			positionPool.Remove (randomPos);
			buttonHolders [i].position = randomPos;
			}
		}

	void EndGameForward(){
		SceneManager.LoadScene ("EndScreen Forward");
	}

	void ShiftButtons(){
		forwardScripts [currentActiveForwardIndex].gameObject.SetActive (false);
		backwardScripts [currentActiveBackwardIndex].gameObject.SetActive (false);

		currentActiveForwardIndex = (currentActiveForwardIndex + 1) % forwardScripts.Length;
		currentActiveBackwardIndex = (currentActiveBackwardIndex + 1) % backwardScripts.Length;

		forwardScripts [currentActiveForwardIndex].gameObject.SetActive (true);
		backwardScripts [currentActiveBackwardIndex].gameObject.SetActive (true);
	}
}
