using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeCube : CubeWithPos {


	CubeWithPos nextSnakeCube;
	float moveTime;
	//bool moving = false;
	bool willMove = false;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {

	}

	public void SetNextSnakeCube( CubeWithPos cwp ){

		nextSnakeCube = cwp;
		nextCubePos = nextSnakeCube.GetCubePos ();

	}

	public void SetMovePara(float cd , float mt){

		cubeDistance = cd;
		moveTime = mt;
	
	}


	public void PreMove(){

		targetPos = nextSnakeCube.transform.localPosition;
		nextCubePos = nextSnakeCube.GetCubePos ();

	}

	override public void  Move() {
	
		if (willMove) {

			//transform.localPosition = nextPos;
			updateMove = (targetPos - transform.localPosition) / moveTime;
			//moving = true;
			StartCoroutine (IMove ());

			cubePos = nextCubePos;
		} else {
			willMove = true;
		}
	}


	public void SetReadyToMove()
	{
		willMove = true;
	}


}
