using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Tup = Tuple<int, double[], double>;
using Algorithm = System.Func<double[], double>;

// .....
// basically empty Tuple class to mimic .NET's since no access to that one here
public class Tuple<T1, T2, T3>
{
	public T1 first;
	public T2 second;
	public T3 third;

	public Tuple () : this (default(T1), default(T2), default(T3)) {}

	public Tuple (T1 t1, T2 t2, T3 t3)
	{
		first = t1;
		second = t2;
		third = t3;
	}
}


public class Twiddle
{
	Algorithm algorithm;
	double[] parms;
	bool firstRun;
	double bestErr;
	double[] dp;
	int iterations;

	public Twiddle (Algorithm _algorithm, double[] _params)
	{
		algorithm = _algorithm;
		parms = _params;
		firstRun = true;
		bestErr = 0;
		// ??
		dp = new double[_params.Length];
		for ( int i = 0; i < dp.Length; i++ )
			dp [ i ] = 0.2;
		iterations = 0;
	}

	public Tup Run ()
	{
		if ( firstRun )
		{
			// ??
			bestErr = algorithm ( parms );
//			self.best_err_ = self.algorithm_(self.params_)
			iterations++;
			firstRun = false;
			return new Tup ( iterations, parms, bestErr );
		}

		for ( int i = 0; i < parms.Length; i++ )
		{
			// update parameter and run algorithm
			parms [ i ] += dp [ i ];
			double err = algorithm ( parms );

			if ( err < bestErr )
			{
				// looks good, increase parameters a little
				bestErr = err;
				dp [ i ] *= 1.1;
			} else
			{
				// error got worse, decrease the parameter
				parms [ i ] -= 2 * dp [ i ];
				err = algorithm ( parms );

				if ( err < bestErr )
				{
					bestErr = err;
					dp [ i ] *= 1.1;
				} else
				{
					parms [ i ] += dp [ i ];
					dp [ i ] *= 0.9;
				}
			}
		}
		iterations++;
		return new Tup ( iterations, parms, bestErr );
	}
}