//=================================================================================================
// Copyright (c) 2012-2016, Institute of Flight Systems and Automatic Control,
// Technische Universität Darmstadt.
// All rights reserved.

// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//     * Redistributions of source code must retain the above copyright
//       notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright
//       notice, this list of conditions and the following disclaimer in the
//       documentation and/or other materials provided with the distribution.
//     * Neither the name of hector_quadrotor nor the names of its contributors
//       may be used to endorse or promote products derived from this software
//       without specific prior written permission.

// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//=================================================================================================
using System;

public static class MatlabHelpers
{
	public const double rtInf = double.PositiveInfinity;
	public static bool rtIsInf (double d) { return double.IsInfinity ( d ); }

	public const double rtNaN = double.NaN;
	public static bool rtIsNaN (double d) { return double.IsNaN ( d ); }

	public static double rt_powd_snf (double u0, double u1)
	{
		double y;
		double d0;
		double d1;
		if (rtIsNaN(u0) || rtIsNaN(u1)) {
			y = rtNaN;
		} else {
			d0 = Math.Abs ( u0 );
			d1 = Math.Abs ( u1 );
			if (rtIsInf(u1)) {
				if (d0 == 1.0) {
					y = rtNaN;
				} else if (d0 > 1.0) {
					if (u1 > 0.0) {
						y = rtInf;
					} else {
						y = 0.0;
					}
				} else if (u1 > 0.0) {
					y = 0.0;
				} else {
					y = rtInf;
				}
			} else if (d1 == 0.0) {
				y = 1.0;
			} else if (d1 == 1.0) {
				if (u1 > 0.0) {
					y = u0;
				} else {
					y = 1.0 / u0;
				}
			} else if (u1 == 2.0) {
				y = u0 * u0;
			} else if ((u1 == 0.5) && (u0 >= 0.0)) {
				y = Math.Sqrt(u0);
			} else if ((u0 < 0.0) && (u1 > Math.Floor(u1))) {
				y = rtNaN;
			} else {
				y = Math.Pow(u0, u1);
			}
		}
		
		return y;
	}
}