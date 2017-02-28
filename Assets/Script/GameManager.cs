using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class GameManager : MonoBehaviour {


	ConfigData config = new ConfigData();

	public FixedCube fixedCube;
	public SnakeCube snakeCube;
	public SnakeCubeHead snakeCubeHead;

	List<SnakeCube> snakeCubes = new List<SnakeCube> ();

	float moveTimer = 0;

	// Use this for initialization
	void Start () {

		ReadConfig ();
		//Debug.Log ("cubeSize" + config.cubeSize.ToString());
		//Debug.Log ("gapSize" + config.gapSize.ToString());
		//Debug.Log ("matrixDim" + config.matrixDim.ToString());

		float baseOffset = (-0.5f)*(config.cubeSize*config.matrixDim + config.gapSize*(config.matrixDim -1));
		GameObject.Find ("BasePoint").transform.position = new Vector3 (baseOffset,baseOffset,baseOffset);

		InitCubeMatrix ();

		InitSnake ();

		UIController uc = GameObject.Find ("UIController").GetComponent<UIController>();
		uc.snakeCubeHead = snakeCubeHead;

		InitFood ();

	}
	
	// Update is called once per frame
	void Update () {
		
		if (checkMoveTimer()) {

			//float cubeOffset = config.cubeSize + config.gapSize;

			foreach (SnakeCube sc in snakeCubes) {
				sc.PreMove ();
			}

			if (snakeCubeHead.CheckHead ()) {
			
				snakeCubeHead.Move ();
				foreach (SnakeCube sc in snakeCubes) {
					sc.Move ();
				}
			
			}

		
		}
		
	}









	void InitCubeMatrix(){

		float dim = config.matrixDim;
		float cubeOffset = config.cubeSize + config.gapSize;
		float scale = config.cubeSize;

		for (int i = 1; i < dim+1; i++) {
			for (int j = 1; j < dim+1; j++){
				for (int k = 1; k < dim+1; k++){

					if ((i-1) * (j-1) * (k-1) * (dim - i) * (dim - j) * (dim - k) == 0) {

						float x = i * cubeOffset;
						float y = j * cubeOffset;
						float z = k * cubeOffset;
						FixedCube f = Instantiate (fixedCube, new Vector3(0,0,0), Quaternion.Euler (new Vector3 (0, 0, 0))) as FixedCube;
						f.transform.localScale = new Vector3 (scale,scale,scale);
						f.transform.parent = GameObject.Find ("BasePoint").transform;
						f.transform.localPosition = new Vector3 (x, y, z);



					}
				}
			}
		}
	}

	void InitSnake(){


		int dim = config.matrixDim;
		float cubeOffset = config.cubeSize + config.gapSize;
		float scale = config.cubeSize;
		CubePos cp;

		cp.x = 0;
		cp.y = 4;
		cp.z = 0;
		if (dim % 2 == 0) {
			cp.x = dim / 2 ;
		} 
		else {
			cp.x = (dim + 1) / 2 ;
		}

		float x = cp.x * cubeOffset;
		float y = cp.y * cubeOffset;
		float z = cp.z * cubeOffset;

		CubePos deltaCubePos;
		deltaCubePos.x = 0;
		deltaCubePos.y = 1;
		deltaCubePos.z = 0;

		snakeCubeHead = Instantiate (snakeCubeHead) as SnakeCubeHead;
		snakeCubeHead.transform.localScale = new Vector3 (scale,scale,scale);
		snakeCubeHead.transform.parent = GameObject.Find ("BasePoint").transform;
		snakeCubeHead.transform.localPosition = new Vector3 (x, y, z);
		snakeCubeHead.Init (cp, deltaCubePos, cubeOffset, config.moveInterval , config.matrixDim + 1, FaceIndex.z_neg);

		for(int i = 3 ; i > 0 ; i--)
		{
			y = i * cubeOffset;
			SnakeCube sc = Instantiate (snakeCube) as SnakeCube;
			sc.transform.localScale = new Vector3 (scale,scale,scale);
			sc.transform.parent = GameObject.Find ("BasePoint").transform;
			sc.transform.localPosition = new Vector3 (x, y, z);
			sc.SetMovePara (cubeOffset, config.moveInterval);

			if(snakeCubes.Count == 0){

				sc.SetNextSnakeCubeTrans (snakeCubeHead.transform);
			}
			else{
				sc.SetNextSnakeCubeTrans (snakeCubes[0].transform);
			}

			snakeCubes.Insert(0,sc);

		}





	}

	void InitFood(){
	}






	bool checkMoveTimer(){

		moveTimer += Time.deltaTime;
		if (moveTimer > config.moveInterval) {
			moveTimer -= config.moveInterval;
			return true;
		}
		return false;
	}

	void ReadConfig(){

		string filepath = Application.dataPath + "/Json/Config.json";

		if (!File.Exists(filepath))
		{
			Debug.Log (filepath + "do not exist");
			return;
		}
		StreamReader sr = new StreamReader(filepath);
		if (sr == null)
		{
			Debug.Log (filepath + "read failed");
			return;
		}
		string json = sr.ReadToEnd();

		if (json.Length > 0) {
			config = JsonUtility.FromJson<ConfigData> (json);
		} 
		else {
			Debug.Log (filepath + "empty file");
		}
	}


	[Serializable]
	public class ConfigData {

		public int matrixDim;
		public float gapSize;
		public float cubeSize;
		public float moveInterval;

	}


}
