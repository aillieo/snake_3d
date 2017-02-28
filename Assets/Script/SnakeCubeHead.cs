using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SnakeCubeHead : MonoBehaviour {


	float cubeDistance;
	int moveDim;
	FaceIndex currentFaceIndex;
	FaceIndex nextFaceIndex;
	CubePos cubePos;
	CubePos deltaPos;

	Transform cameraTransform;

	// Use this for initialization
	void Start () {


		cameraTransform = Camera.main.transform;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool CheckHead(){


		nextFaceIndex = FaceIndex.none;
		if (cubePos.x + deltaPos.x < 0) {
			//Debug.Log ("x neg");
			nextFaceIndex = FaceIndex.x_neg;
		}
		else if(cubePos.x + deltaPos.x > moveDim )
		{
			//Debug.Log ("x pos");
			nextFaceIndex = FaceIndex.x_pos;
		}
		else if (cubePos.y + deltaPos.y < 0) {
			//Debug.Log ("y neg");
			nextFaceIndex = FaceIndex.y_neg;
		}
		else if(cubePos.y + deltaPos.y > moveDim )
		{
			//Debug.Log ("y pos");
			nextFaceIndex = FaceIndex.y_pos;
		}
		else if (cubePos.z + deltaPos.z < 0) {
			//Debug.Log ("z neg");
			nextFaceIndex = FaceIndex.z_neg;
		}
		else if(cubePos.z + deltaPos.z > moveDim )
		{
			//Debug.Log ("z pos");
			nextFaceIndex = FaceIndex.z_pos;
		}


		updateCamera ();


		if (nextFaceIndex != FaceIndex.none) {
			HandleEdge ();
		}

		return true;
	}



	void updateCamera(){
	
		switch(nextFaceIndex){

		case FaceIndex.x_neg:
			cameraTransform.position = new Vector3 (-30,0,0);
			cameraTransform.eulerAngles = new Vector3 (0,90,0);
			break;
		case FaceIndex.x_pos:
			cameraTransform.position = new Vector3 (30,0,0);
			cameraTransform.eulerAngles = new Vector3 (0,-90,0);
			break;
		case FaceIndex.y_neg:
			cameraTransform.position = new Vector3 (0,-30,0);
			cameraTransform.eulerAngles = new Vector3 (-90,0,0);
			break;
		case FaceIndex.y_pos:
			cameraTransform.position = new Vector3 (0,30,0);
			cameraTransform.eulerAngles = new Vector3 (90,0,0);
			break;
		case FaceIndex.z_neg:
			cameraTransform.position = new Vector3 (0,0,-30);
			cameraTransform.eulerAngles = new Vector3 (0,0,0);
			break;
		case FaceIndex.z_pos:
			cameraTransform.position = new Vector3 (0,0,30);
			cameraTransform.eulerAngles = new Vector3 (0,180,0);
			break;
		}
	
	}

	void HandleEdge(){

		switch(nextFaceIndex){

		case FaceIndex.x_neg:
			deltaPos.x = 0;
			break;
		case FaceIndex.x_pos:
			deltaPos.x = 0;
			break;
		case FaceIndex.y_neg:
			deltaPos.y = 0;
			break;
		case FaceIndex.y_pos:
			deltaPos.y = 0;
			break;
		case FaceIndex.z_neg:
			deltaPos.z = 0;
			break;
		case FaceIndex.z_pos:
			deltaPos.z = 0;
			break;
		}


		switch(currentFaceIndex){

		case FaceIndex.x_neg:
			deltaPos.x = 1;
			break;
		case FaceIndex.x_pos:
			deltaPos.x = -1;
			break;
		case FaceIndex.y_neg:
			deltaPos.y = 1;
			break;
		case FaceIndex.y_pos:
			deltaPos.y = -1;
			break;
		case FaceIndex.z_neg:
			deltaPos.z = 1;
			break;
		case FaceIndex.z_pos:
			deltaPos.z = -1;
			break;
		}


		currentFaceIndex = nextFaceIndex;



	}


	public void Move(){
	
		cubePos.x += deltaPos.x;
		cubePos.y += deltaPos.y;
		cubePos.z += deltaPos.z;
		this.transform.localPosition = new Vector3(cubePos.x * cubeDistance, cubePos.y * cubeDistance , cubePos.z * cubeDistance);

	}



	public void Init(CubePos cp, CubePos dp, float cd, int md , FaceIndex cfi){

		cubePos = cp;
		deltaPos = dp;
		cubeDistance = cd;
		moveDim = md;
		currentFaceIndex = cfi;

	}

	public void SetDeltaPos(CubePos delta ){

		deltaPos = delta;

	}

}
