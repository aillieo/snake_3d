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
	//bool rotating = false;

	Vector3 updateMove;
	float updateRotate;
	Vector3 targetPos;
	Vector3 targetRot;
	Vector3 rotateAxis;
	Vector3 rotateBase;
	float rotateAngle;

	Transform left_bottom;
	Transform right_bottom;
	Transform left_top;
	Transform right_top;

	bool willRotate = false;

	Transform cameraFocusTransform;

	ScreenInputDirectioin screenInputDirectioin = ScreenInputDirectioin.none;

	// Use this for initialization
	void Start () {

		cameraFocusTransform = GameObject.Find("CameraFocus").transform;
		left_bottom = GameObject.Find("Left_Bottom").transform;
		right_bottom = GameObject.Find("Right_Bottom").transform;
		left_top = GameObject.Find("Left_Top").transform;
		right_top = GameObject.Find("Right_Top").transform;

	}
	
	// Update is called once per frame
	void Update () {


	}

	public bool CheckHead(){



		//moving = false;
		//rotating = false;
		transform.localPosition = targetPos;
		transform.eulerAngles = targetRot;

		if(screenInputDirectioin != ScreenInputDirectioin.none)
		{
			HandleOperation ();
			screenInputDirectioin = ScreenInputDirectioin.none;
			return true;
		}



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


	void HandleOperation()
	{

		if (screenInputDirectioin == ScreenInputDirectioin.none) {
			return;
		}

		Vector3 deltaVec3 = new Vector3();
		if (screenInputDirectioin == ScreenInputDirectioin.left) {
			rotateAngle = 90f;
			rotateBase = left_bottom.position;
			rotateAxis = left_bottom.position - left_top.position;
			deltaVec3 = left_bottom.position - right_bottom.position;

		} 
		else if (screenInputDirectioin == ScreenInputDirectioin.right) {
			rotateAngle = 90f;
			rotateBase = right_bottom.position;
			rotateAxis = right_top.position - right_bottom.position;
			deltaVec3 = right_bottom.position - left_bottom.position;
		}


		deltaCubePos.x = (int)deltaVec3.x;
		deltaCubePos.y = (int)deltaVec3.y;
		deltaCubePos.z = (int)deltaVec3.z;


		willRotate = true;


		transform.RotateAround (rotateBase,rotateAxis,rotateAngle);
		targetRot = transform.eulerAngles;
		transform.RotateAround (rotateBase,rotateAxis,-rotateAngle);

	}


	void HandleEdge(){

		//Vector3 rotateOffset = new Vector3 (0, 0, 0);

		switch(nextFaceIndex){

		case FaceIndex.x_neg:
			deltaCubePos.x = 0;
			//rotateOffset.x = 0.5f;
			break;
		case FaceIndex.x_pos:
			deltaCubePos.x = 0;
			//rotateOffset.x = -0.5f;
			break;
		case FaceIndex.y_neg:
			deltaCubePos.y = 0;
			//rotateOffset.y = 0.5f;
			break;
		case FaceIndex.y_pos:
			deltaCubePos.y = 0;
			//rotateOffset.y = -0.5f;
			break;
		case FaceIndex.z_neg:
			deltaCubePos.z = 0;
			//rotateOffset.z = 0.5f;
			break;
		case FaceIndex.z_pos:
			deltaCubePos.z = 0;
			//rotateOffset.z = -0.5f;
			break;
		}


		switch(currentFaceIndex){

		case FaceIndex.x_neg:
			deltaCubePos.x = 1;
			//rotateOffset.x = 0.5f;
			break;
		case FaceIndex.x_pos:
			deltaCubePos.x = -1;
			//rotateOffset.x = -0.5f;
			break;
		case FaceIndex.y_neg:
			deltaCubePos.y = 1;
			//rotateOffset.y = 0.5f;
			break;
		case FaceIndex.y_pos:
			deltaCubePos.y = -1;
			//rotateOffset.y = -0.5f;
			break;
		case FaceIndex.z_neg:
			deltaCubePos.z = 1;
			//rotateOffset.z = 0.5f;
			break;
		case FaceIndex.z_pos:
			deltaCubePos.z = -1;
			//rotateOffset.z = -0.5f;
			break;
		}

		willRotate = true;

		Vector3 vecCurrent = Utils.getVector3ByFaceIndex (currentFaceIndex);
		Vector3 vecNext = Utils.getVector3ByFaceIndex (nextFaceIndex);
		rotateAxis = Vector3.Cross (vecCurrent, vecNext);

		rotateBase = 0.5f * (right_bottom.position + left_bottom.position );
		rotateAngle = 90;
		transform.RotateAround (rotateBase,rotateAxis,rotateAngle);
		targetRot = transform.eulerAngles;
		transform.RotateAround (rotateBase,rotateAxis,-rotateAngle);

		//rotateBase = transform.position + rotateOffset * cubeDistance;


		currentFaceIndex = nextFaceIndex;



	}


	public void HandleInput (ScreenInputDirectioin sid)
	{
		screenInputDirectioin = sid;
	}


	public void Move(){
	

		cubePos.x += deltaCubePos.x;
		cubePos.y += deltaCubePos.y;
		cubePos.z += deltaCubePos.z;
		targetPos = new Vector3 (cubePos.x * cubeDistance, cubePos.y * cubeDistance, cubePos.z * cubeDistance);

		if (willRotate) {


			updateRotate = rotateAngle / moveTime;
			//rotating = true;
			willRotate = false;
			StartCoroutine (IRotate());
					
		} else {

			updateMove = (targetPos - transform.localPosition) / moveTime;
			moving = true;
			StartCoroutine (IMove());
		}

	}


	IEnumerator IMove()
	{

		float movedDis = 0;
		while(movedDis < cubeDistance)
		{
			movedDis += updateMove.magnitude * Time.deltaTime;
			transform.localPosition = transform.localPosition + updateMove * Time.deltaTime;
			yield return new WaitForEndOfFrame ();
		}
		moving = false;


	}


	IEnumerator IRotate()
	{

		float rotatedAngle = 0;
		while( rotatedAngle < 90)
		{
			rotatedAngle += updateRotate*Time.deltaTime;
			transform.RotateAround (rotateBase, rotateAxis,updateRotate*Time.deltaTime);
			cameraFocusTransform.eulerAngles = transform.eulerAngles;
			yield return new WaitForEndOfFrame ();
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
