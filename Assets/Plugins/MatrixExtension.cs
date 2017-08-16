using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MatrixExtension
{
	public static Matrix4x4 RosToUnity ()
	{
		Matrix4x4 m = Matrix4x4.identity;
		Matrix4x4 m2 = new Matrix4x4 ();
		m2 [ 0 ] = m [ 0 ]; m2 [ 1 ] = m [ 2 ]; m2 [ 2 ] = m [ 1 ]; m2 [ 3 ] = m [ 3 ];
		m2 [ 4 ] = m [ 8 ]; m2 [ 5 ] = m [ 10 ]; m2 [ 6 ] = m [ 9 ]; m2 [ 7 ] = m [ 7 ];
		m2 [ 8 ] = m [ 4 ]; m2 [ 9 ] = m [ 6 ]; m2 [ 10 ] = m [ 5 ]; m2 [ 11 ] = m [ 11 ];
		m2 [ 12 ] = m [ 12 ]; m2 [ 13 ] = m [ 14 ]; m2 [ 14 ] = m [ 13 ]; m2 [ 15 ] = m [ 15 ];
		return m2;
	}

	public static Matrix4x4 ToUnity (this Matrix4x4 m)
	{
		Matrix4x4 m2 = new Matrix4x4 ();
		m2 [ 0 ] = m [ 0 ]; m2 [ 1 ] = m [ 2 ]; m2 [ 2 ] = m [ 1 ]; m2 [ 3 ] = m [ 3 ];
		m2 [ 4 ] = m [ 8 ]; m2 [ 5 ] = m [ 10 ]; m2 [ 6 ] = m [ 9 ]; m2 [ 7 ] = m [ 7 ];
		m2 [ 8 ] = m [ 4 ]; m2 [ 9 ] = m [ 6 ]; m2 [ 10 ] = m [ 5 ]; m2 [ 11 ] = m [ 11 ];
		m2 [ 12 ] = m [ 12 ]; m2 [ 13 ] = m [ 14 ]; m2 [ 14 ] = m [ 13 ]; m2 [ 15 ] = m [ 15 ];
		return m2;
	}

	public static Matrix4x4 ToRos (this Matrix4x4 m)
	{
		return m.ToUnity ().inverse;
	}

	public static Quaternion ExtractRotation (this Matrix4x4 matrix)
	{
		Vector3 forward;
		forward.x = matrix.m02;
		forward.y = matrix.m12;
		forward.z = matrix.m22;

		Vector3 upwards;
		upwards.x = matrix.m01;
		upwards.y = matrix.m11;
		upwards.z = matrix.m21;

		return Quaternion.LookRotation ( forward, upwards );
	}

	public static Vector3 ExtractPosition (this Matrix4x4 matrix)
	{
		Vector3 position;
		position.x = matrix.m03;
		position.y = matrix.m13;
		position.z = matrix.m23;
		return position;
	}

	public static Vector3 ExtractScale (this Matrix4x4 matrix)
	{
		Vector3 scale;
		scale.x = new Vector4 ( matrix.m00, matrix.m10, matrix.m20, matrix.m30 ).magnitude;
		scale.y = new Vector4 ( matrix.m01, matrix.m11, matrix.m21, matrix.m31 ).magnitude;
		scale.z = new Vector4 ( matrix.m02, matrix.m12, matrix.m22, matrix.m32 ).magnitude;
		return scale;
	}
}


public static class VectorExtension
{
	public static Vector3 ToRos (this Vector3 v)
	{
		return new Vector3 ( v.z, -v.x, v.y );
	}

	public static Vector3 ToUnity (this Vector3 v)
	{
		return new Vector3 ( -v.y, v.z, v.x );
	}
}

public static class QuaternionExtension
{
	public static Quaternion ToRos (this Quaternion q)
	{
//		return new Quaternion ( q.z, -q.x, q.y, q.w );
		return new Quaternion ( -q.z, q.x, -q.y, q.w );
	}

	public static Quaternion ToUnity (this Quaternion q)
	{
//		return new Quaternion ( -q.y, q.z, q.x, q.w );
		return new Quaternion ( q.y, -q.z, -q.x, q.w );
	}
}