using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SnakeCubeHead : MonoBehaviour {


	float cubeDistance;
	int moveDim;
	float moveTime;

	FaceIndex currentFaceIndex;
	FaceIndex nextFaceIndex;

	CubePos cubePos;
	CubePos deltaCubePos;

	bool moving = false;
	bool rotating = false;

	Vector3 updateMove;
	Vector3 updateRotate;
	Vector3 targetPos;
	Vector3 targetRot;
	Vector3 rotateAxis;


	Transform cameraFocusTransform;

	// Use this for initialization
	void Start () {

		cameraFocusTransform = GameObject.Find("CameraFocus").transform;

	}
	
	// Update is called once per frame
	void Update () {

		if(moving)
		{
			transform.Translate (updateMove * Time.deltaTime);
		}

		if (rotating)
		{
			cameraFocusTransform.RotateAround (Vector3.zero,rotateAxis,90/moveTime * Time.deltaTime);
		}

	}

	public bool CheckHead(){



		moving = false;
		rotating = false;
		transform.localPosition = targetPos;
		cameraFocusTransform.eulerAngles = targetRot;




		nextFaceIndex = FaceIndex.none;
		if (cubePos.x + deltaCubePos.x < 0) {
			//Debug.Log ("x neg");
			nextFaceIndex = FaceIndex.x_neg;
		}
		else if(cubePos.x + deltaCubePos.x > moveDim )
		{
			//Debug.Log ("x pos");
			nextFaceIndex = FaceIndex.x_pos;
		}
		else if (cubePos.y + deltaCubePos.y < 0) {
			//Debug.Log ("y neg");
			nextFaceIndex = FaceIndex.y_neg;
		}
		else if(cubePos.y + deltaCubePos.y > moveDim )
		{
			//Debug.Log ("y pos");
			nextFaceIndex = FaceIndex.y_pos;
		}
		else if (cubePos.z + deltaCubePos.z < 0) {
			//Debug.Log ("z neg");
			nextFaceIndex = FaceIndex.z_neg;
		}
		else if(cubePos.z + deltaCubePos.z > moveDim )
		{
			//Debug.Log ("z pos");
			nextFaceIndex = FaceIndex.z_pos;
		}





		if (nextFaceIndex != FaceIndex.none) {
			UpdateCamera ();
			HandleEdge ();
		}

		return true;
	}



	void UpdateCamera(){

		Vector3 vecCurrent = Utils.getVector3ByFaceIndex (currentFaceIndex);
		Vector3 vecNext = Utils.getVector3ByFaceIndex (nextFaceIndex);
		rotateAxis = Vector3.Cross (vecCurrent, vecNext);

		Transform targetTrans = cameraFocusTransform;
		targetTrans.RotateAround (Vector3.zero,rotateAxis,90);
		targetRot = targetTrans.eulerAngles;
		targetTrans.RotateAround (Vector3.zero,rotateAxis,-90);

		rotating = true;

	}

	void HandleEdge(){

		switch(nextFaceIndex){

		case FaceIndex.x_neg:
			deltaCubePos.x = 0;
			break;
		case FaceIndex.x_pos:
			deltaCubePos.x = 0;
			break;
		case FaceIndex.y_neg:
			deltaCubePos.y = 0;
			break;
		case FaceIndex.y_pos:
			deltaCubePos.y = 0;
			break;
		case FaceIndex.z_neg:
			deltaCubePos.z = 0;
			break;
		case FaceIndex.z_pos:
			deltaCubePos.z = 0;
			break;
		}


		switch(currentFaceIndex){

		case FaceIndex.x_neg:
			deltaCubePos.x = 1;
			break;
		case FaceIndex.x_pos:
			deltaCubePos.x = -1;
			break;
		case FaceIndex.y_neg:
			deltaCubePos.y = 1;
			break;
		case FaceIndex.y_pos:
			deltaCubePos.y = -1;
			break;
		case FaceIndex.z_neg:
			deltaCubePos.z = 1;
			break;
		case FaceIndex.z_pos:
			deltaCubePos.z = -1;
			break;
		}


		currentFaceIndex = nextFaceIndex;



	}


	public void Move(){
	
		cubePos.x += deltaCubePos.x;
		cubePos.y += deltaCubePos.y;
		cubePos.z += deltaCubePos.z;


		targetPos = new Vector3(cubePos.x * cubeDistance, cubePos.y * cubeDistance , cubePos.z * cubeDistance);

		updateMove = (targetPos - transform.localPosition) / moveTime;

		moving = true;


	}



	public void Init(CubePos cp, CubePos dcp, float cd, float mt ,int md , FaceIndex cfi){

		cubePos = cp;
		deltaCubePos = dcp;
		cubeDistance = cd;
		moveTime = mt;
		moveDim = md;
		currentFaceIndex = cfi;

		targetPos = new Vector3(cubePos.x * cubeDistance, cubePos.y * cubeDistance , cubePos.z * cubeDistance);

	}

	public void SetDeltaCubePos(CubePos delta ){

		deltaCubePos = delta;

	}

}
