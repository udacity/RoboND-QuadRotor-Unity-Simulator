///////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2012, hiDOF INC.
// Copyright (C) 2013, PAL Robotics S.L.
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

/// \author Wim Meeussen, Adolfo Rodriguez Tsouroukdissian
//#include <ros/console.h>
//#include <hardware_interface/internal/demangle_symbol.h>
using Ros_CSharp;
using hardware_interface;
using System;
using System.Collections.Generic;

namespace hardware_interface
{
	/**
	 * \brief Non-templated Base Class that contains a virtual destructor.
	 *
	 * This will allow to destroy the templated children without having to know the template type.
	 */
//	class ResourceManagerBase
//	{
//	public:
//	virtual ~ResourceManagerBase() {}
//	};

	/**
	 * \brief Class for handling named resources.
	 *
	 * Resources are encapsulated inside handle instances, and this class allows to register and get them by name.
	 *
	 * \tparam ResourceHandle Resource handle type. Must implement the following method:
	 *  \code
	 *   std::string getName() const;
	 *  \endcode
	 */
	public abstract class AResourceHandle
	{
		public abstract string getName ();
	}

	public class ResourceManager<ResourceHandle> where ResourceHandle : AResourceHandle
	{
//		typedef ResourceManager<ResourceHandle> resource_manager_type;
		/** \name Non Real-Time Safe Functions
		*\{*/

		public ResourceManager () { resource_map_ = new Dictionary<string, ResourceHandle> (); }
		~ResourceManager () {}

		/** \return Vector of resource names registered to this interface. */
		public List<string> getNames ()
		{
			List<string> l = new List<string> ();
			foreach ( KeyValuePair<string, ResourceHandle> pair in resource_map_ )
				l.Add ( pair.Key );
			
			return l;
		}

		/**
		* \brief Register a new resource.
		* If the resource name already exists, the previously stored resource value will be replaced with \e val.
		* \param handle Resource value. Its type should implement a <tt>std::string getName()</tt> method.
		*/
		public void registerHandle (ResourceHandle handle)
		{
			if ( !resource_map_.ContainsKey ( handle.getName () ) )
			{
				resource_map_.Add ( handle.getName (), handle );

			} else
			{
				ROS.Warn ( "Replacing previously registered handle '" + handle.getName () + "' in '" + this.GetType () + "'." );
				resource_map_[ handle.getName () ] = handle;
			}
		}

		/**
		* \brief Get a resource handle by name.
		* \param name Resource name.
		* \return Resource associated to \e name. If the resource name is not found, an exception is thrown.
		*/
		public ResourceHandle getHandle(string name)
		{
			ResourceHandle r;
			if ( resource_map_.TryGetValue ( name, out r ) )
				return r;

			throw new KeyNotFoundException ("Could not find resource '" + name + "' in '" + this.GetType () + "'.");
			return null;
		}

		/**
		* \brief Combine a list of interfaces into one.
		*
		* Every registered handle in each of the managers is registered into the result interface
		* \param managers The list of resource managers to be combined.
		* \param result The interface where all the handles will be registered.
		* \return Resource associated to \e name. If the resource name is not found, an exception is thrown.
		*/
		public static void concatManagers (List<ResourceManager<ResourceHandle>> managers, out ResourceManager<ResourceHandle> result)
		{
			result = new ResourceManager<ResourceHandle> ();

			foreach ( ResourceManager<ResourceHandle> rm in managers )
			{
				List<string> names = rm.getNames ();
				foreach ( string name in names )
				{
					result.registerHandle ( rm.getHandle ( name ) );
				}
			}
		}

		/*\}*/

		protected Dictionary<string, ResourceHandle> resource_map_;
	}
}