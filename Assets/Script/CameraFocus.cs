using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour {

	float updateRotate;
	Vector3 rotateAxis;
	Vector3 targetRot;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void Rotate(Vector3 axis, float uRotate)
	{
		updateRotate = uRotate;
		rotateAxis = axis;


		transform.RotateAround (transform.position,rotateAxis,90);
		targetRot = transform.eulerAngles;
		transform.RotateAround (transform.position,rotateAxis,-90);


		StartCoroutine (IRotate());

	}

	IEnumerator IRotate()
	{
		float rotatedAngle = 0;
		while( rotatedAngle < 90)
		{
			rotatedAngle += updateRotate*Time.deltaTime;
			transform.RotateAround (transform.position, rotateAxis,updateRotate*Time.deltaTime);
			yield return new WaitForFixedUpdate ();
		}

		transform.eulerAngles = targetRot;
	}
}
