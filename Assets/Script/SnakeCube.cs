using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeCube : MonoBehaviour {


	Transform nextSnakeCubeTrans;
	Vector3 nextPos;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetNextSnakeCubeTrans( Transform trans){

		nextSnakeCubeTrans = trans;
	}


	public void PreMove(){

		nextPos = nextSnakeCubeTrans.localPosition;

	}

	public void Move(){
	
		transform.localPosition = nextPos;
	}

}
