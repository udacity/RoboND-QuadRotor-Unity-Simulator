using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PIDController
{
	public double kp;
	public double ki;
	public double kd;
	double errorSum;
	public double setPoint;
	public double maxWindup;
	double startTime;
	double lastTimestamp;
	double lastError;
	double lastWindup;


	public PIDController (double _kp = 0, double _ki = 0, double _kd = 0, double _maxWindup = 20, double _startTime = 0)
	{
		kp = _kp;
		ki = _ki;
		kd = _kd;
		errorSum = 0;
		setPoint = 0;
		maxWindup = _maxWindup;
		startTime = _startTime;
		
		lastTimestamp = 0;
		lastError = 0;
	}

	public void Reset ()
	{
		kp = 0;
		ki = 0;
		kd = 0;
		setPoint = 0;
		errorSum = 0;
		lastTimestamp = 0;
		lastError = 0;
		lastWindup = 0;
	}

	public void SetTarget (double target)
	{
		setPoint = target;
	}

	public void SetKP (double _kp)
	{
		kp = _kp;
	}

	public void SetKI (double _ki)
	{
		ki = _ki;
	}

	public void SetKD (double _kd)
	{
		kd = _kd;
	}

	public void SetMaxWindup (double max)
	{
		maxWindup = max;
	}

	public void SetStartTime (double time)
	{
		startTime = time;
	}

	public double Update (double measuredValue, double timestamp)
	{
		double deltaTime = timestamp - lastTimestamp;
		if ( deltaTime == 0 )
		{
			Debug.Log ( "delta time was 0" );
			return 0;
		}

		double error = setPoint - measuredValue;
		double deltaError = error - lastError;
		lastTimestamp = timestamp;
		lastError = error;

		errorSum += error * deltaTime;
		if ( errorSum > maxWindup )
			errorSum = maxWindup;
		else
		if ( errorSum < -maxWindup )
			errorSum = -maxWindup;

		double p = kp * error;
		double i = ki * errorSum;
		double d = kd * ( deltaError / deltaTime );

		return p + i + d;
	}
}