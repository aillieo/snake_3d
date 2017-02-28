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

		if(moving)
		{
			transform.Translate(updateMove * Time.deltaTime);
		}

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
		updateMove = nextPos - transform.localPosition;
		updateMove = updateMove / moveTime;
		moving = true;
	}

}
