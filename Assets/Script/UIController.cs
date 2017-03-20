using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {


	public SnakeCubeHead snakeCubeHead;
	public GameObject cameraFocus;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown("a"))
		{
			changeSnakeDirection (ScreenInputDirectioin.left);
		}

		if(Input.GetKeyDown("s"))
		{
			changeSnakeDirection (ScreenInputDirectioin.down);
		}

		if(Input.GetKeyDown("d"))
		{
			changeSnakeDirection (ScreenInputDirectioin.right);
		}

		if(Input.GetKeyDown("w"))
		{
			changeSnakeDirection (ScreenInputDirectioin.up);
		}

	}


	void changeSnakeDirection (ScreenInputDirectioin sid)
	{
		
		switch(sid)
		{

		case ScreenInputDirectioin.up:

			Debug.Log ("Up");
			break;

		case ScreenInputDirectioin.down:

			Debug.Log ("Down");
			break;

		case ScreenInputDirectioin.left:

			Debug.Log ("Left");
			snakeCubeHead.HandleInput (sid);
			break;

		case ScreenInputDirectioin.right:

			Debug.Log ("Right");
			snakeCubeHead.HandleInput (sid);
			break;

		}
	}
}
