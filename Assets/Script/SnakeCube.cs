using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeCube : MonoBehaviour {


	Transform nextSnakeCubeTrans;
	Vector3 nextPos;
	float moveTime;
	float moveDistance;
	bool moving = false;
	Vector3 updateMove;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {

	}

	public void SetNextSnakeCubeTrans( Transform trans){

		nextSnakeCubeTrans = trans;
	}

	public void SetMovePara(float md , float mt){

		moveDistance = md;
		moveTime = mt;
	
	}

	public void PreMove(){

		nextPos = nextSnakeCubeTrans.localPosition;

	}

	public void Move(){
	
		//transform.localPosition = nextPos;
		updateMove = (nextPos - transform.localPosition) / moveTime;
		//moving = true;
		StartCoroutine(IMove());

	}

	IEnumerator IMove(){

		float movedDis = 0;
		while (movedDis <= moveDistance) {
		
			movedDis += updateMove.magnitude * Time.deltaTime;
			transform.localPosition = transform.localPosition + updateMove * Time.deltaTime;
			yield return new WaitForEndOfFrame ();
		
		}
		transform.localPosition = nextPos;

	}

}
