using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

	CubePos cubePos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public CubePos getCubePos()
	{
		return cubePos;
	}

	public void SetCubePos(CubePos cp)
	{
		cubePos = cp;
		transform.localPosition = cp.ToVec3 ();
	}

}
