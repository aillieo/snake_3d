using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum FaceIndex
{
	x_pos,
	x_neg,
	y_pos,
	y_neg,
	z_pos,
	z_neg,
	none
}


public enum ScreenInputDirectioin
{
	up,
	down,
	left,
	right,
	none
}

public enum SnakeChangeDirection
{
	left,
	right,
	none
}

public struct CubePos
{
	public int x;
	public int y;
	public int z;
}



public static class Utils
{
	public static void SetComponentX(this Transform trans , float x)
	{
		trans.position = new Vector3 (x, trans.position.y , trans.position.z);
	}

	public static void SetComponentY(this Transform trans , float y)
	{
		trans.position = new Vector3 (trans.position.x, y , trans.position.z);
	}

	public static void SetComponentZ(this Transform trans , float z)
	{
		trans.position = new Vector3 (trans.position.x, trans.position.y , z);
	}



	public static Vector3 ToVec3(this CubePos cp, float cubeSize)
	{
		return new Vector3 ((float)cp.x * cubeSize, (float)cp.y * cubeSize, (float)cp.z * cubeSize);
	}
		

	public static int GetIndex(this CubePos cp, int dim)
	{
		return cp.x * (dim+2)* (dim+2) + cp.y * (dim+2) + cp.z;
	}

	public static CubePos CubePos(int i, int j, int k)
	{
		CubePos cp;
		cp.x = i;
		cp.y = j;
		cp.z = k;
		return cp;
	}

	public static CubePos AddDelta(this CubePos cp, CubePos deltaCubePos)
	{
		cp.x += deltaCubePos.x;
		cp.y += deltaCubePos.y;
		cp.z += deltaCubePos.z;
		return cp;
	}

	public static CubePos CubePos(Vector3 vec3)
	{
		CubePos cp;
		int ix = (int)vec3.x;
		int iy = (int)vec3.y;
		int iz = (int)vec3.z;
		cp.x = ix>0 ? 1 : (ix<0 ? -1 : 0);
		cp.y = iy>0 ? 1 : (iy<0 ? -1 : 0);
		cp.z = iz>0 ? 1 : (iz<0 ? -1 : 0);
		return cp;
	}


	public static Vector3 GetVector3ByFaceIndex(FaceIndex fi)
	{
		switch(fi){

		case FaceIndex.x_neg:
			return new Vector3 (-1,0,0);
		case FaceIndex.x_pos:
			return new Vector3 (1,0,0);
		case FaceIndex.y_neg:
			return new Vector3 (0,-1,0);
		case FaceIndex.y_pos:
			return new Vector3 (0,1,0);
		case FaceIndex.z_neg:
			return new Vector3 (0,0,-1);
		case FaceIndex.z_pos:
			return new Vector3 (0,0,1);
		}

		return Vector3.zero;
	}


	public static float GetAngleWithDirection(Vector2 from, Vector2 to){

		float ret = Vector2.Angle (from , to);
		if ((Vector3.Cross (from, to)).z > 0) {
			return ret;
		} else {
			return -ret;
		}
	}

}
