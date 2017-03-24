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
	public Food food;

	List<SnakeCube> snakeCubes = new List<SnakeCube> ();
	Dictionary<int,bool> cubePosEmpty = new Dictionary<int,bool> ();

	float moveTimer = 0;

	// Use this for initialization
	void Start () {

		ReadConfig ();
		//Debug.Log ("cubeSize" + config.cubeSize.ToString());
		//Debug.Log ("gapSize" + config.gapSize.ToString());
		//Debug.Log ("matrixDim" + config.matrixDim.ToString());

		float baseOffset = (-0.5f)*(config.cubeSize*config.matrixDim);
		GameObject.Find ("BasePoint").transform.position = new Vector3 (baseOffset,baseOffset,baseOffset);

		CubePos.cubeSize = config.cubeSize;
		CubePos.matrixDim = config.matrixDim;


		InitCubeMatrix ();


		InitSnake ();


		InitFood ();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if (checkMoveTimer()) {

			CubePos cpToBeEmpty =  snakeCubes [snakeCubes.Count - 1].GetCubePos ();
			cubePosEmpty [cpToBeEmpty.GetIndex ()] = true;

			foreach (SnakeCube sc in snakeCubes) {
				sc.PreMove ();
			}

			bool moveSnake = snakeCubeHead.CheckHead ();

			CubePos cpToBeFull =  snakeCubeHead.GetNextCubePos ();
			cubePosEmpty [cpToBeFull.GetIndex ()] = false;


			if (snakeCubeHead.WillGrow()) {
				AddSnakeCube ();
				ResetFood ();
			}


			if (moveSnake) {
				snakeCubeHead.Move ();
				foreach (SnakeCube sc in snakeCubes) {
					sc.Move ();
				}
			}





		
		}
		
	}

	void AddSnakeCube()
	{
		/*
		float cubeOffset = config.cubeSize;
		float scale = config.cubeSize;
		CubePos cp = snakeCubes [snakeCubes.Count - 1].GetCubePos ();
		float moveTime = 0.9f * config.moveInterval;

		SnakeCube sc = Instantiate (snakeCube) as SnakeCube;
		sc.transform.localScale = new Vector3 (scale,scale,scale);
		sc.transform.parent = GameObject.Find ("BasePoint").transform;
		sc.transform.localPosition = cp.ToVec3();
		sc.SetMovePara (cubeOffset, moveTime);
		sc.SetCubePos(cp);
		Debug.Log ("-----" + cp.x.ToString() + " " + cp.y.ToString() + " "+ cp.z.ToString() + " ");
		cubePosEmpty [cp.GetIndex()] = false;

		sc.SetNextSnakeCube (snakeCubes[snakeCubes.Count -1]);

		snakeCubes.Add(sc);
		*/
	}

	void ResetFood()
	{
		
		int newIndex = -1;
		int count = cubePosEmpty.Count;

		int rand = UnityEngine.Random.Range (0,count-1);
		List<int> keysBeforeRand = new List<int>();
	
		int iter = 0;

		// keys equal and after rand
		foreach (KeyValuePair<int,bool> item in cubePosEmpty) {

			if (iter < rand) {
				iter++;
				keysBeforeRand.Add (item.Key);
			} else {
				if (item.Value) {

					newIndex = item.Key;
					break;
				}
			}
		
		}


		// keys before rand
		if(newIndex == -1)
		{
			for (int i = 0; i < rand; i++) {

				if (cubePosEmpty [keysBeforeRand[i]]) {

					newIndex = keysBeforeRand[i];
					break;
				}

			}
		}

		if (newIndex != -1) {

			food.SetCubePos (new CubePos(newIndex));

		
		} else {


			// no more empty position to place food
			// game over


		}

	}


	void InitCubeMatrix(){

		int dim = config.matrixDim;
		float cubeOffset = config.cubeSize;
		float scale = config.cubeSize;

		for (int i = 0; i < dim+2; i++) {
			for (int j = 0; j < dim+2; j++){
				for (int k = 0; k < dim+2; k++){

					if (((i-1) * (j-1) * (k-1) * (dim - i) * (dim - j) * (dim - k) == 0)&&(i>0 && i < dim +1 && j > 0 && j < dim +1 && k > 0 && k < dim+1)) {

						float x = i * cubeOffset;
						float y = j * cubeOffset;
						float z = k * cubeOffset;
						FixedCube f = Instantiate (fixedCube, new Vector3(0,0,0), Quaternion.Euler (new Vector3 (0, 0, 0))) as FixedCube;
						f.transform.localScale = new Vector3 (scale,scale,scale);
						f.transform.parent = GameObject.Find ("BasePoint").transform;
						f.transform.localPosition = new Vector3 (x, y, z);

					}

					else if ( 
						i * (dim + 1 - i)  == 0 && (j>0 && j< dim +1 ) && (k>0 && k< dim +1 ) ||
						j * (dim + 1 - j)  == 0 && (k>0 && k< dim +1 ) && (i>0 && i< dim +1 ) ||
						k * (dim + 1 - k)  == 0 && (i>0 && i< dim +1 ) && (j>0 && j< dim +1 ) ) {

						CubePos cp = new CubePos (i,j,k);
						int key = cp.GetIndex();
						// Debug.Log (key.ToString() + "-----" + i.ToString() + " " + j.ToString() + " "+ k.ToString() + " ");
						cubePosEmpty.Add (key,true);

					}
				}
			}
		}

		// Debug.Log (cubePosEmpty.Count.ToString());

		FixedCube main = Instantiate (fixedCube, new Vector3(0,0,0), Quaternion.Euler (new Vector3 (0, 0, 0))) as FixedCube;
		scale = cubeOffset * dim; 
		main.transform.localScale = new Vector3 (scale,scale,scale);
		main.transform.parent = GameObject.Find ("BasePoint").transform;
		float position = cubeOffset * (dim+1) / 2;
		main.transform.localPosition = new Vector3 (position,position,position);

	}

	void InitSnake(){


		int dim = config.matrixDim;
		float cubeOffset = config.cubeSize;
		float scale = config.cubeSize;
		CubePos cp = new CubePos (0, 4, 0);
		float moveTime = 0.9f * config.moveInterval;

		if (dim % 2 == 0) {
			cp.x = dim / 2 ;
		} 
		else {
			cp.x = (dim + 1) / 2 ;
		}

		float x = cp.x * cubeOffset;
		float y = cp.y * cubeOffset;
		float z = cp.z * cubeOffset;

		CubePos deltaCubePos = new CubePos(0,1,0);

		snakeCubeHead = Instantiate (snakeCubeHead) as SnakeCubeHead;
		snakeCubeHead.transform.localScale = new Vector3 (scale,scale,scale);
		snakeCubeHead.transform.parent = GameObject.Find ("BasePoint").transform;
		snakeCubeHead.transform.localPosition = new Vector3 (x, y, z);
		snakeCubeHead.Init (cp, deltaCubePos, cubeOffset, moveTime , config.matrixDim + 1, FaceIndex.z_neg);
		cubePosEmpty [cp.GetIndex()] = false;

		UIController uc = GameObject.Find ("UIController").GetComponent<UIController>();
		uc.snakeCubeHead = snakeCubeHead;


		for(int i = 3 ; i > 0 ; i--)
		{
			y = i * cubeOffset;
			SnakeCube sc = Instantiate (snakeCube) as SnakeCube;
			sc.transform.localScale = new Vector3 (scale,scale,scale);
			sc.transform.parent = GameObject.Find ("BasePoint").transform;
			sc.transform.localPosition = new Vector3 (x, y, z);
			sc.SetMovePara (cubeOffset, moveTime);
			CubePos thisCubePos = new CubePos (cp.x, cp.y - 4 + i, cp.z);
			sc.SetCubePos(thisCubePos);
			cubePosEmpty [thisCubePos.GetIndex()] = false;


			if(snakeCubes.Count == 0){

				sc.SetNextSnakeCube (snakeCubeHead);
			}
			else{
				sc.SetNextSnakeCube (snakeCubes[snakeCubes.Count -1]);
			}

			snakeCubes.Add(sc);


		}





	}

	void InitFood(){

		int dim = config.matrixDim;
		float scale = config.cubeSize;
		CubePos cp = new CubePos (0,dim,0);
		if (dim % 2 == 0) {
			cp.x = dim / 2 ;
		} 
		else {
			cp.x = (dim + 1) / 2 ;
		}

		food = Instantiate (food) as Food;
		food.transform.localScale = new Vector3 (scale,scale,scale);
		food.transform.parent = GameObject.Find ("BasePoint").transform;
		//food.transform.localPosition = cp.ToVec3 (cubeOffset);

		food.SetCubePos(cp);
	
		snakeCubeHead.InitFood (food);

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
