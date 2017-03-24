using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {


	public SnakeCubeHead snakeCubeHead;
	public GameObject cameraFocus;
	Camera cam;
	Vector2 inputPoint1;
	Vector2 inputPoint2;
	Vector2 snakePoint1;
	Vector2 snakePoint2;

	// Use this for initialization
	void Start () {
		cam = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {


		// whether a valid input

		if(Input.GetKeyDown("a"))
		{
			
			inputPoint1 = new Vector2 (0,0);
			inputPoint2 = new Vector2 (-1f,0);
			changeSnakeDirection ();
		}

		if(Input.GetKeyDown("s"))
		{
			
			inputPoint1 = new Vector2 (0,0);
			inputPoint2 = new Vector2 (0,-1f);
			changeSnakeDirection ();
		}

		if(Input.GetKeyDown("d"))
		{
			inputPoint1 = new Vector2 (0,0);
			inputPoint2 = new Vector2 (1f,0);
			changeSnakeDirection ();
		}

		if(Input.GetKeyDown("w"))
		{
			inputPoint1 = new Vector2 (0,0);
			inputPoint2 = new Vector2 (0,1f);
			changeSnakeDirection ();
		}

		if (Input.GetMouseButtonDown (0)) {

			inputPoint1 = Input.mousePosition;
		}

		if (Input.GetMouseButtonUp (0)) {

			inputPoint2 = Input.mousePosition;
			if((inputPoint1 - inputPoint2).magnitude > 15.0f){
				changeSnakeDirection ();
			}
		}
	}


	void changeSnakeDirection ()
	{

		Vector3 snakeDirection = snakeCubeHead.GetCurrentDirection ();
		snakePoint1 =  cam.WorldToScreenPoint (new Vector3(0,0,0));
		snakePoint2 =  cam.WorldToScreenPoint (snakeDirection);

		Vector2 screenVec2 = inputPoint2 - inputPoint1;
		Vector2 snakeVec2 = snakePoint2 - snakePoint1;

		float angle = Utils.GetAngleWithDirection (snakeVec2, screenVec2);
		// Debug.Log ("snake= "+snakeVec2.ToString() + "     input= " + screenVec2.ToString() + "     angle= " + angle.ToString());

		if (angle > 60 && angle < 120 ) {
			snakeCubeHead.HandleInput (SnakeChangeDirection.left);
		}
		else if(angle > -120 && angle < -60)
		{
			snakeCubeHead.HandleInput (SnakeChangeDirection.right);
		}


	}
}
