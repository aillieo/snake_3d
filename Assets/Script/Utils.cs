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

public struct CubePos
{
	public int x;
	public int y;
	public int z;
}

public static class utils
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

}
