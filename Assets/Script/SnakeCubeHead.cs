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
	float updateRotate;
	Vector3 targetPos;
	Vector3 targetRot;
	Vector3 rotateAxis;
	Vector3 rotateBase;

	bool willRotate = false;

	Transform cameraFocusTransform;

	// Use this for initialization
	void Start () {

		cameraFocusTransform = GameObject.Find("CameraFocus").transform;

	}
	
	// Update is called once per frame
	void Update () {

		if (rotating)
		{

			transform.RotateAround (rotateBase, new Vector3(1,0,0),updateRotate*Time.deltaTime);
			cameraFocusTransform.eulerAngles = transform.eulerAngles;

		}

		if(moving)
		{

			transform.localPosition = transform.localPosition + updateMove * Time.deltaTime;

			
		}



	}

	public bool CheckHead(){



		moving = false;
		rotating = false;
		transform.localPosition = targetPos;
		transform.eulerAngles = targetRot;

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

			HandleEdge ();
		}

		return true;
	}




	void HandleEdge(){

		Vector3 rotateOffset = new Vector3 (0, 0, 0);

		switch(nextFaceIndex){

		case FaceIndex.x_neg:
			deltaCubePos.x = 0;
			rotateOffset.x = 0.5f;
			break;
		case FaceIndex.x_pos:
			deltaCubePos.x = 0;
			rotateOffset.x = -0.5f;
			break;
		case FaceIndex.y_neg:
			deltaCubePos.y = 0;
			rotateOffset.y = 0.5f;
			break;
		case FaceIndex.y_pos:
			deltaCubePos.y = 0;
			rotateOffset.y = -0.5f;
			break;
		case FaceIndex.z_neg:
			deltaCubePos.z = 0;
			rotateOffset.z = 0.5f;
			break;
		case FaceIndex.z_pos:
			deltaCubePos.z = 0;
			rotateOffset.z = -0.5f;
			break;
		}


		switch(currentFaceIndex){

		case FaceIndex.x_neg:
			deltaCubePos.x = 1;
			rotateOffset.x = 0.5f;
			break;
		case FaceIndex.x_pos:
			deltaCubePos.x = -1;
			rotateOffset.x = -0.5f;
			break;
		case FaceIndex.y_neg:
			deltaCubePos.y = 1;
			rotateOffset.y = 0.5f;
			break;
		case FaceIndex.y_pos:
			deltaCubePos.y = -1;
			rotateOffset.y = -0.5f;
			break;
		case FaceIndex.z_neg:
			deltaCubePos.z = 1;
			rotateOffset.z = 0.5f;
			break;
		case FaceIndex.z_pos:
			deltaCubePos.z = -1;
			rotateOffset.z = -0.5f;
			break;
		}

		willRotate = true;

		Vector3 vecCurrent = Utils.getVector3ByFaceIndex (currentFaceIndex);
		Vector3 vecNext = Utils.getVector3ByFaceIndex (nextFaceIndex);
		rotateAxis = Vector3.Cross (vecCurrent, vecNext);

		transform.RotateAround (rotateBase,rotateAxis,90);
		targetRot = transform.eulerAngles;
		transform.RotateAround (rotateBase,rotateAxis,-90);

		rotateBase = transform.position + rotateOffset * cubeDistance;

		currentFaceIndex = nextFaceIndex;



	}


	public void Move(){
	

		cubePos.x += deltaCubePos.x;
		cubePos.y += deltaCubePos.y;
		cubePos.z += deltaCubePos.z;
		targetPos = new Vector3 (cubePos.x * cubeDistance, cubePos.y * cubeDistance, cubePos.z * cubeDistance);

		if (willRotate) {


			updateRotate = 90 / moveTime;
			rotating = true;
			willRotate = false;
					
		} else {

			updateMove = (targetPos - transform.localPosition) / moveTime;
			moving = true;
		}

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
