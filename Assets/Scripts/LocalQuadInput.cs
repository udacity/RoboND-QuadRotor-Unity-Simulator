using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * LocalQuadInput: queries keyboard and either applies force/torque directly to a QuadController, or to a QRKeyboardTeleop.
 * If using the Teleop, check 'useTeleop' in the inspector, and there must be a QRKeyboardTeleop component assigned.
 * If controlling a local controller otherwise, uncheck 'useTeleop' and there must be a QuadController component assigned.
 */

public class LocalQuadInput : MonoBehaviour
{
	public QuadController droneController;
	public QRKeyboardTeleop teleop;
	public PathFollower pather;
	public bool useTeleop;

	bool motorEnabled;
	float thrust = 0;

	bool useTwist;
	float yAngVel;
	float yLinVel;
	float lastInputTime;
	bool localInputActive;


	void LateUpdate ()
	{
		float thrustInput = Input.GetAxis ( "Thrust" );
		if ( thrustInput != 0 )
			thrust = thrust += thrustInput * Time.deltaTime / 3;
		if ( Input.GetKeyDown ( KeyCode.Semicolon ) )
			thrust = 0;
		thrust = Mathf.Clamp ( thrust, -1f, 1f );

		if ( Input.GetKeyDown ( KeyCode.B ) )
			useTwist = !useTwist;

		if ( Input.anyKey || thrust != 0 )
			lastInputTime = Time.time;

		if ( Time.time - lastInputTime > 2f )
			localInputActive = false;
		else
			localInputActive = true;
		QuadUI.InputActive = localInputActive;

		if ( useTwist )
		{
			float yaw = Input.GetAxis ( "Yaw" );
			yAngVel += yaw * 2 * Time.deltaTime;
			yLinVel += thrustInput * 2 * Time.deltaTime;

			if ( localInputActive )
			{
				if ( useTeleop )
				{
					teleop.SendTwist ( Vector3.up * yLinVel, Vector3.up * yAngVel );
					
				} else
				{
					droneController.SetLinearVelocity ( Vector3.up * yLinVel );
					droneController.SetAngularVelocity ( Vector3.up * yAngVel );
				}
			}

		} else
		{
			Vector3 input = new Vector3 ( Input.GetAxis ( "Horizontal" ), thrust, Input.GetAxis ( "Vertical" ) );
			Vector3 force = new Vector3 ( 0, input.y, 0 );
			float x = input.z / 2 + input.x / 2;
			float z = input.z / 2 - input.x / 2;
			Vector3 torque = new Vector3 ( x, Input.GetAxis ( "Yaw" ), z );

			force *= droneController.maxForce;
			torque *= -droneController.maxTorqueRadians;

			if ( localInputActive )
			{
				if ( useTeleop )
				{
					teleop.SendWrench ( force, torque );
					
				} else
				{
					droneController.ApplyMotorForce ( force );
					droneController.ApplyMotorTorque ( torque );
				}
			}
		}

		if ( Input.GetKeyDown ( KeyCode.R ) )
		{
			yLinVel = 0;
			yAngVel = 0;
			if ( useTeleop )
				teleop.TriggerReset ();
			else
				droneController.ResetOrientation ();
		}

		if ( Input.GetKeyDown ( KeyCode.G ) )
		{
			if ( useTeleop )
				teleop.SetGravity ( !droneController.UseGravity );
			else
				droneController.UseGravity = !droneController.UseGravity;
		}

		if ( Input.GetKeyDown ( KeyCode.P ) )
		{
			PathPlanner.AddNode ( droneController.Position, droneController.Rotation );
		}
		if ( Input.GetKeyDown ( KeyCode.O ) )
		{
			thrust = 0;
			droneController.ResetOrientation ();
			pather.SetPath ( new Pathing.Path ( PathPlanner.GetPath () ) );
			PathPlanner.Clear ();
		}
	}
}