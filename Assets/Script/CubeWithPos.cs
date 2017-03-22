using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeWithPos : MonoBehaviour {

	protected CubePos cubePos;
	protected float cubeDistance;
	protected Vector3 targetPos;
	protected Vector3 updateMove;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public CubePos GetCubePos(){
		return cubePos;
	}

	public void SetCubePos(CubePos cp)
	{
		cubePos = cp;
	}

	public virtual void Move(){

		StartCoroutine(IMove());

	}

	protected IEnumerator IMove()
	{

		float movedDis = 0;
		while(movedDis < cubeDistance)
		{
			movedDis += updateMove.magnitude * Time.deltaTime;
			transform.localPosition = transform.localPosition + updateMove * Time.deltaTime;
			yield return new WaitForEndOfFrame ();
		}
		transform.localPosition = targetPos;


	}

}
