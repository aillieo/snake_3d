using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SnakeCubeHead : CubeWithPos {



	int moveDim;
	float moveTime;

	FaceIndex currentFaceIndex;
	FaceIndex nextFaceIndex;

	CubePos deltaCubePos;

	//bool moving = false;
	//bool rotating = false;


	float updateRotate;

	Vector3 targetRot;
	Vector3 rotateAxis;
	Vector3 rotateBase;
	float rotateAngle;

	Transform left_bottom;
	Transform right_bottom;
	Transform left_top;
	Transform right_top;

	bool willRotate = false;
	bool willRotateCamera = false;

	CameraFocus cameraFocus;

	Food food;

	SnakeChangeDirection snakeChangeDirection = SnakeChangeDirection.none;

	// Use this for initialization
	void Start () {

		cameraFocus = GameObject.Find("CameraFocus").GetComponent<CameraFocus>();
		left_bottom = GameObject.Find("Left_Bottom").transform;
		right_bottom = GameObject.Find("Right_Bottom").transform;
		left_top = GameObject.Find("Left_Top").transform;
		right_top = GameObject.Find("Right_Top").transform;

	}
	
	// Update is called once per frame
	void Update () {


	}

	public bool CheckHead(){


		// change snake to target state
		//moving = false;
		//rotating = false;




		// whether snake head need rotate over edge
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
			return true;
		}

		// whether there is a operation to handle
		if(snakeChangeDirection != SnakeChangeDirection.none)
		{
			HandleOperation ();
			snakeChangeDirection = SnakeChangeDirection.none;
			return true;
		}





		// whether there is food before snake
		bool hasFood = true;
		if(hasFood)
		{
			HandleFood();
		}


		nextCubePos = nextCubePos.AddDelta(deltaCubePos);

		return true;
	}

	void HandleFood(){





		//...

		food.OnAte ();

	}

	void HandleOperation()
	{

		if (snakeChangeDirection == SnakeChangeDirection.none) {
			return;
		}

		Vector3 deltaVec3 = new Vector3();
		if (snakeChangeDirection == SnakeChangeDirection.left) {
			rotateAngle = 90f;
			rotateBase = left_bottom.position;
			rotateAxis = left_bottom.position - left_top.position;
			deltaVec3 = left_bottom.position - right_bottom.position;

		} 
		else if (snakeChangeDirection == SnakeChangeDirection.right) {
			rotateAngle = 90f;
			rotateBase = right_bottom.position;
			rotateAxis = right_top.position - right_bottom.position;
			deltaVec3 = right_bottom.position - left_bottom.position;
		}

		deltaCubePos = Utils.CubePos (deltaVec3);

		//Debug.Log (deltaCubePos.x.ToString() + "," + deltaCubePos.y.ToString() + "," + deltaCubePos.z.ToString());


		willRotate = true;
		willRotateCamera = false;


		transform.RotateAround (rotateBase,rotateAxis,rotateAngle);
		targetRot = transform.eulerAngles;
		transform.RotateAround (rotateBase,rotateAxis,-rotateAngle);


		nextCubePos = nextCubePos.AddDelta(deltaCubePos);

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
		willRotateCamera = true;

		Vector3 vecCurrent = Utils.GetVector3ByFaceIndex (currentFaceIndex);
		Vector3 vecNext = Utils.GetVector3ByFaceIndex (nextFaceIndex);
		rotateAxis = Vector3.Cross (vecCurrent, vecNext);

		rotateBase = 0.5f * (right_bottom.position + left_bottom.position );
		rotateAngle = 90;
		transform.RotateAround (rotateBase,rotateAxis,rotateAngle);
		targetRot = transform.eulerAngles;
		transform.RotateAround (rotateBase,rotateAxis,-rotateAngle);

		//rotateBase = transform.position + rotateOffset * cubeDistance;


		currentFaceIndex = nextFaceIndex;

		nextCubePos = nextCubePos.AddDelta(deltaCubePos);

	}


	public void HandleInput (SnakeChangeDirection scd)
	{
		snakeChangeDirection = scd;
	}


	override public void Move(){
	

		cubePos = nextCubePos;
		targetPos = cubePos.ToVec3 (cubeDistance);

		if (willRotate) {


			updateRotate = rotateAngle / moveTime;
			//rotating = true;
			willRotate = false;
			StartCoroutine (IRotate());
			if (willRotateCamera) {
				cameraFocus.Rotate(rotateAxis,updateRotate,targetRot);
			}
				

		} else {

			updateMove = (targetPos - transform.localPosition) / moveTime;
			//moving = true;
			StartCoroutine (IMove());
		}

	}





	IEnumerator IRotate()
	{

		float rotatedAngle = 0;
		while( rotatedAngle < 90)
		{
			rotatedAngle += updateRotate*Time.deltaTime;
			transform.RotateAround (rotateBase, rotateAxis,updateRotate*Time.deltaTime);
			yield return new WaitForFixedUpdate ();
		}

		willRotate = false;
		transform.eulerAngles = targetRot;
	}



	public void Init(CubePos cp, CubePos dcp, float cd, float mt ,int md , FaceIndex cfi){

		cubePos = cp;
		deltaCubePos = dcp;
		cubeDistance = cd;
		moveTime = mt;
		moveDim = md;
		currentFaceIndex = cfi;

		nextCubePos = cubePos;
		targetPos = nextCubePos.ToVec3(cubeDistance);


	}

	public void SetDeltaCubePos(CubePos delta ){

		deltaCubePos = delta;

	}

	public Vector3 GetCurrentDirection(){
	
		return deltaCubePos.ToVec3 (cubeDistance);
	}


	public void SetFood(Food f)
	{
		food = f;
	}

}
