using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeCube : CubeWithPos {


	CubeWithPos nextSnakeCube;
	CubePos nextCubePos;
	float moveTime;
	//bool moving = false;


	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {

	}

	public void SetNextSnakeCube( CubeWithPos cwp ){

		nextSnakeCube = cwp;
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
	
		//transform.localPosition = nextPos;
		updateMove = (targetPos - transform.localPosition) / moveTime;
		//moving = true;
		StartCoroutine(IMove());

		cubePos = nextCubePos;

	}



}
