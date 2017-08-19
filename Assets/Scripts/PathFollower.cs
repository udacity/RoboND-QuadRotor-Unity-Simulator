using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathing;
using Ros_CSharp;

//using Vec3 = Messages.geometry_msgs.Vector3;
//using PoseStamped = Messages.geometry_msgs.PoseStamped;
//using Imu = Messages.sensor_msgs.Imu;

public enum DecelerationFactor
{
	Slow,
	Normal,
	Fast
}

public class PathFollower : MonoBehaviour
{
	public Path Path { get { return path; } }
	public bool HasPath { get { return path != null; } }
	public bool active;

	public QuadController quad;

	// simple follow vars
	public float maxVelocity = 10;
	public float maxTilt = 10;
	public float acceleration = 10;
	public float arriveDist = 5;
	public float rotSpeed = 90;

	// controller vars
	public float maxSpeed = 5;
	public float maxTorque = 17;
	public float minDist = 1;
	public LayerMask groundMask;

	public PositionControllerNode posController;
	public HoverControllerNode hoverController;
	public AttitudeControllerNode attController;

	Transform tr;
	Rigidbody rb;
	Path path;
	PathSample destination;
	int curNode;
//	Vector3 force;
//	Vector3 torque;
	[SerializeField]
	bool following;


	double thrust;
//	Vec3 torque;

	void Awake ()
	{
		tr = transform;
		rb = GetComponent<Rigidbody> ();
		groundMask = LayerMask.GetMask ( "Ground" );

//		posController.torqueCallback = TorqueUpdateCallback;
//		posController.thrustCallback = ThrustUpdateCallback;
//		attController.torqueCallback = TorqueUpdateCallback;
//		attController.thrustCallback = ThrustUpdateCallback;
//		hoverController.thrustCallback = ThrustUpdateCallback;
	}

	void FixedUpdate ()
	{
		if ( active && destination != null )
		{
			float testDistance = minDist * minDist;
//			if ( curNode == path.Nodes.Length - 1 )
//				testDistance = arriveDist * arriveDist;
			if ( ( destination.position - tr.position ).sqrMagnitude < testDistance )
			{
				if ( curNode == path.Nodes.Length - 1 )
				{
					path = null;
					destination = null;
					following = false;
					// clear the visualization of the path
					PathPlanner.ClearViz ();
					return;
				}

				curNode++;
				arriveDist = ( path.Nodes [ curNode ].position - destination.position ).magnitude * 0.05f;
				destination = path.Nodes [ curNode ];
//				SetControllers ();
			}
			following = true;
			UpdateSteering ();
		}
	}

	void LateUpdate ()
	{
	}

	void UpdateSteering ()
	{
		Vector3 force = Vector3.zero;
		AdjustTilt ();
//		force += Seek ();
		force += Arrive ();
		force = Vector3.ClampMagnitude ( force, maxVelocity );
		rb.velocity = force;
	}

	Vector3 Seek ()
	{
		Vector3 toTarget = destination.position - rb.position;
		Vector3 desiredVelocity = toTarget.normalized * maxVelocity;
		return desiredVelocity - rb.velocity;
	}

	Vector3 Arrive ()
	{
		Vector3 toTarget = destination.position - rb.position;
		float dist = toTarget.magnitude;
//		Vector3 desiredVelocity = toTarget.normalized * maxVelocity;
//		if ( dist < arriveDist )
//		{
//			desiredVelocity *= ( toTarget.sqrMagnitude / dist ) * 2;
//		}

		float deceleration = 2f;
		float decelerationTweaker = 0.3f;

		float speed = dist / ( deceleration * decelerationTweaker );
		Vector3 desiredVelocity = toTarget * speed / dist; // dist is supposed to be toTarget.magnitude


		return desiredVelocity - rb.velocity;
	}

	Vector3 Avoidance ()
	{
		return Vector3.zero;
	}

	void AdjustTilt ()
	{
		Vector3 toTarget = destination.position - tr.position;
		Quaternion q1 = Quaternion.LookRotation ( quad.Forward, tr.up );
		Quaternion q2 = Quaternion.LookRotation ( toTarget );
		Quaternion qOffset = q1 * Quaternion.Inverse ( tr.rotation );

		tr.rotation = qOffset * Quaternion.RotateTowards ( q1, q2, 5 * Time.deltaTime );
		
//		Vector3 axis = Vector3.Cross ( toTarget.normalized, Vector3.up );
//		Debug.DrawLine ( tr.position, tr.position + axis * 10,  Color.red );
		
	}

/*	void UpdateSteering ()
	{
		torque = new Vec3 ();
		thrust = 0;

		PoseStamped ps = new PoseStamped ();
		ps.header = new Messages.std_msgs.Header ();
		ps.pose = new Messages.geometry_msgs.Pose ();
		ps.header.Stamp = ROS.GetTime ();
		ps.pose.position = new Messages.geometry_msgs.Point ( quad.Position.ToRos () );
		ps.pose.orientation = new Messages.geometry_msgs.Quaternion ( quad.Rotation.ToRos () );
//		posController.UpdatePose ( ps );
//		Quaternion q = quad.Rotation;
//		Imu imu = new Imu ();
//		imu.orientation = new Messages.geometry_msgs.Quaternion ();
//		imu.orientation.x = q.x;
//		imu.orientation.y = q.y;
//		imu.orientation.z = q.z;
//		imu.orientation.w = q.w;
//		attController.UpdateImu ( imu );
		hoverController.UpdatePose ( ps );

		quad.ApplyMotorForce ( Vector3.up * (float) thrust );
		quad.ApplyMotorTorque ( torque.ToUnityVector () );
	}*/

	float FixAngle (float angle)
	{
		if ( angle > 180f )
			angle -= 360f;
		if ( angle < -180f )
			angle += 360f;

		return angle;
	}

	public void SetPath (Path p)
	{
		if ( p != null && p.Nodes != null && p.Nodes.Length > 1 )
		{
			path = p;
			tr.position = p.Nodes [ 0 ].position;
			tr.rotation = p.Nodes [ 0 ].orientation;
			curNode = 1;
			destination = p.Nodes [ 1 ];
//			SetControllers ();

			Debug.Log ( "path set" );
			
		} else
		{
			path = null;
			destination = null;
			following = false;
		}
	}

/*	void SetControllers ()
	{
		double startTime = ROS.GetTime ().data.toSec ();
		posController.SetGoal ( new Messages.geometry_msgs.Point ( destination.position ) );
		hoverController.SetGoal ( destination.position.y );
		posController.SetStartTime ( startTime );
		attController.SetStartTime ( startTime );
		hoverController.SetStartTime ( startTime );
	}

	void TorqueUpdateCallback (Vec3 rpy)
	{
		torque.x += rpy.x;
		torque.y += rpy.y;
		torque.z += rpy.z;
	}

	void ThrustUpdateCallback (double _thrust)
	{
		thrust += _thrust;
	}*/
}