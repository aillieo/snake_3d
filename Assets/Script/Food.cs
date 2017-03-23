using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

	CubePos cubePos;
	float cubeDistance;
	bool needReset = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Init(float cd)
	{
		cubeDistance = cd;
	}

	public CubePos getCubePos()
	{
		return cubePos;
	}

	public void SetCubePos(CubePos cp)
	{
		cubePos = cp;
		transform.localPosition = cp.ToVec3 (cubeDistance);
	}

	public void OnAte()
	{
		needReset = true;
	}

	public bool NeedReset()
	{
		return needReset;
	}
}
