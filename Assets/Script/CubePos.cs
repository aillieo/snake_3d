using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePos {

	public static float cubeSize = 0;
	public static int matrixDim = 0;

	public int x;
	public int y;
	public int z;


	public CubePos(int i, int j, int k)
	{
		x = i;
		y = j;
		z = k;
	}



	public Vector3 ToVec3()
	{
		return new Vector3 ((float)x * cubeSize, (float)y * cubeSize, (float)z * cubeSize);
	}


	public int GetIndex()
	{
		// Debug.Log ( "-----" + x.ToString() + " " + y.ToString() + " "+ z.ToString() + " ");
		return x * (matrixDim+2)* (matrixDim+2) + y * (matrixDim+2) + z;
	}


	public CubePos(int index){

		z = index % (matrixDim + 2);
		index -= z;
		index /= (matrixDim + 2);
		y = index % (matrixDim + 2);
		index -= y;
		index /= (matrixDim + 2);
		x = index;

		//Debug.Log ( "-----" + x.ToString() + " " + y.ToString() + " "+ z.ToString() + " ");

	}
		



	public CubePos(Vector3 vec3)
	{
		int ix = (int)vec3.x;
		int iy = (int)vec3.y;
		int iz = (int)vec3.z;
		x = ix>0 ? 1 : (ix<0 ? -1 : 0);
		y = iy>0 ? 1 : (iy<0 ? -1 : 0);
		z = iz>0 ? 1 : (iz<0 ? -1 : 0);
	}


	static public CubePos operator+ (CubePos a , CubePos b)
	{
		return new CubePos (a.x+b.x, a.y+b.y, a.z+b.z);
	}


	static public bool operator== (CubePos a , CubePos b)
	{
		return (a.x==b.x && a.y==b.y && a.z==b.z);
	}

	static public bool operator!= (CubePos a , CubePos b)
	{
		return !(a==b);
	}

	public override bool Equals(System.Object obj)
	{
		if (obj == null || this.GetType() != obj.GetType()) return false;

		return this == ((CubePos)obj);
	}


	public override int GetHashCode()
	{
		return this.GetIndex().GetHashCode ();
	}
}
