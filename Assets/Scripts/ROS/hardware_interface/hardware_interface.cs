///////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2012, hiDOF INC.
//
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//   * Redistributions of source code must retain the above copyright notice,
//     this list of conditions and the following disclaimer.
//   * Redistributions in binary form must reproduce the above copyright
//     notice, this list of conditions and the following disclaimer in the
//     documentation and/or other materials provided with the distribution.
//   * Neither the name of hiDOF, Inc. nor the names of its
//     contributors may be used to endorse or promote products derived from
//     this software without specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
// ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
// POSSIBILITY OF SUCH DAMAGE.
//////////////////////////////////////////////////////////////////////////////

/*
 * Author: Wim Meeussen
 * Ported by Noam Weiss
 */
using System;
using System.Collections.Generic;

namespace hardware_interface
{
	/** \brief Abstract Hardware Interface
	 *
	 */
	public interface IHardwareInterface
	{
		void claim ( string resource );
		void clearClaims ();
		HashSet<string> getClaims ();
	}

	public class HardwareInterface : IHardwareInterface
	{
		/** \name Resource management
		*/

		/// Claim a resource by name
		public virtual void claim (string resource) { claims_.Add ( resource ); }

		/// Clear the resources this interface is claiming
		public virtual void clearClaims () { claims_.Clear (); }

		/// Get the list of resources this interface is currently claiming
//		std::set<std::string> getClaims() const  { return claims_; }
		public virtual HashSet<string> getClaims () { return claims_; }
		/*\}*/

		HashSet<string> claims_;
	}


	/// An exception related to a \ref HardwareInterface
	public class HardwareInterfaceException : Exception
	{
		public HardwareInterfaceException (string message) { msg = message; }
		~HardwareInterfaceException () {}

		public virtual string what () { return msg; }

		string msg;
	}
}