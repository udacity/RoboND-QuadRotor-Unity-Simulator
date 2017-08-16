/*
 * A wrapper for a ref-counted list which auto deletes elements when their refcount becomes 0? whatever.
 */

/*********************************************************************
* Software License Agreement (BSD License)
*
*  Copyright (c) 2008, Willow Garage, Inc.
*  All rights reserved.
*
*  Redistribution and use in source and binary forms, with or without
*  modification, are permitted provided that the following conditions
*  are met:
*
*   * Redistributions of source code must retain the above copyright
*     notice, this list of conditions and the following disclaimer.
*   * Redistributions in binary form must reproduce the above
*     copyright notice, this list of conditions and the following
*     disclaimer in the documentation and/or other materials provided
*     with the distribution.
*   * Neither the name of the Willow Garage nor the names of its
*     contributors may be used to endorse or promote products derived
*     from this software without specific prior written permission.
*
*  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
*  "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
*  LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS
*  FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE
*  COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
*  INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
*  BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
*  LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
*  CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT
*  LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN
*  ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
*  POSSIBILITY OF SUCH DAMAGE.
*********************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Ros_CSharp;

namespace actionlib
{
	/*******************************************************************************
	* ManagedList
	*******************************************************************************/
	public class ManagedList<T> where T : class
	{
		public int Count { get { return list.Count; } }

		List<TrackedElement> list = new List<TrackedElement> ();

		/*******************************************************************************
		* TrackedElement
		*******************************************************************************/
		public class TrackedElement
		{
			object lockObject = new object ();
			List<Handle> handles = new List<Handle> ();
			public T element;
			bool valid;

			TrackedElement () {}

			public TrackedElement (T element)
			{
				this.element = element;
				valid = true;
			}

			public Handle CreateHandle ()
			{
				lock ( lockObject )
				{
					if ( valid )
					{
						Handle h = new Handle ( this );
						handles.Add ( h );
						return h;
					}
				}
				return null;
			}

			public void RemoveHandle (Handle h)
			{
				lock ( lockObject )
				{
					if ( valid && handles.Count > 0 )
					{
						
						handles.Remove ( h );
						if ( handles.Count == 0 )
						{
							if ( typeof (T).IsValueType )
								element = default (T);
							else
								element = null;
							valid = false;
						}
					}
				}
			}
		}

		/*******************************************************************************
		* Handle
		*******************************************************************************/
		public class Handle
		{
			TrackedElement trackedElement;
			Iterator iterator;

			Handle () {}

			public Handle (TrackedElement te)
			{
				trackedElement = te;
			}

			public static Handle CreateHandle (TrackedElement te, Iterator it)
			{
				if ( te == null || it == null )
					return null;
				Handle h = te.CreateHandle ();
				h.iterator = it;

				return h;
			}

			~Handle ()
			{
				if ( trackedElement != null )
					trackedElement.RemoveHandle ( this );
			}

			public T GetElement ()
			{
				return iterator.Element.element;
//				return trackedElement.element;
			}
		}

		/*******************************************************************************
		* Iterator
		*******************************************************************************/
		public class Iterator
		{
			public TrackedElement Element { get { return enumerator.Current; } }

			List<TrackedElement>.Enumerator enumerator;

//			Iterator () {}

			internal Iterator (ManagedList<T> list)
//			public Iterator (List<TrackedElement> list)
			{
				enumerator = list.list.GetEnumerator ();
			}

			public Handle CreateHandle ()
			{
				if ( enumerator.Current == null )
				{
					Debug.LogError ( "Getting a handle from an expired iterator" );
					return null;
				}

				return enumerator.Current.CreateHandle ();
			}

			public T GetElement ()
			{
				if ( enumerator.Current != null )
					return enumerator.Current.element;
				return null;
			}

			public static Iterator operator ++(Iterator a)
			{
				a.enumerator.MoveNext ();
				return a;
			}
		}

		/*******************************************************************************
		* ManagedList methods
		*******************************************************************************/
		public Handle Add (T element)
		{
			TrackedElement t = new TrackedElement ( element );
			list.Add ( t );
			return Handle.CreateHandle ( t, GetIterator () );
		}

		public void Remove (T element)
		{
			
		}

		public void Clear ()
		{
			list.Clear ();
		}

		public T this [int index]
		{
			get { return list [ index ].element; }
			set { list [ index ].element = value; }
		}

		public Iterator GetIterator ()
		{
			return new Iterator ( this );
		}
	}

}
/**
 * \brief wrapper around an STL list to help with reference counting
 * Provides handles elements in an STL list. When all the handles go out of scope,
 * the element in the list is destroyed.
 */
//template <class T>
//class ManagedList
//{
//private:
//  struct TrackedElem
//  {
//    T elem;
//    boost::weak_ptr<void> handle_tracker_;
//  };
//
//public:
//  class Handle;
//
//  class iterator
//  {
//    public:
//      iterator() { }
//      T& operator*()  { return it_->elem; }
//      T& operator->() { return it_->elem; }
//      const T& operator*()  const { return it_->elem; }
//      const T& operator->() const { return it_->elem; }
//      bool operator==(const iterator& rhs) const { return it_ == rhs.it_; }
//      bool operator!=(const iterator& rhs) const { return !(*this == rhs); }
//      void operator++() { it_++; }
//      Handle createHandle();                   //!< \brief Creates a refcounted Handle from an iterator
//      friend class ManagedList;
//    private:
//      iterator(typename std::list<TrackedElem>::iterator it) : it_(it) { }
//      typename std::list<TrackedElem>::iterator it_;
//  };
//
//  typedef typename boost::function< void(iterator)> CustomDeleter;
//
//private:
//  class ElemDeleter
//  {
//    public:
//      ElemDeleter(iterator it, CustomDeleter deleter, const boost::shared_ptr<DestructionGuard>& guard) :
//        it_(it), deleter_(deleter), guard_(guard)
//      { }
//
//      void operator() (void*)
//      {
//        DestructionGuard::ScopedProtector protector(*guard_);
//        if (!protector.isProtected())
//        {
//          ROS_ERROR_NAMED("actionlib", "ManagedList: The DestructionGuard associated with this list has already been destructed. You must delete all list handles before deleting the ManagedList");
//          return;
//        }
//
//        ROS_DEBUG_NAMED("actionlib", "IN DELETER");
//        if (deleter_)
//          deleter_(it_);
//      }
//
//    private:
//      iterator it_;
//      CustomDeleter deleter_;
//      boost::shared_ptr<DestructionGuard> guard_;
//  };
//
//public:
//
//  class Handle
//  {
//    public:
//      /**
//       * \brief Construct an empty handle
//       */
//      Handle() : it_(iterator()), handle_tracker_(boost::shared_ptr<void>()), valid_(false) { }
//
//      const Handle& operator=(const Handle& rhs)
//      {
//    	if ( rhs.valid_ ) {
//          it_ = rhs.it_;
//    	}
//        handle_tracker_ = rhs.handle_tracker_;
//        valid_ = rhs.valid_;
//        return rhs;
//      }
//
//      /**
//       * \brief stop tracking the list element with this handle, even though the
//       * Handle hasn't gone out of scope
//       */
//      void reset()
//      {
//        valid_ = false;
//#ifndef _MSC_VER
//        // this prevents a crash on MSVC, but I bet the problem is elsewhere.
//        // it puts the lotion in the basket.
//        it_ = iterator();
//#endif
//        handle_tracker_.reset();
//      }
//
//      /**
//       * \brief get the list element that this handle points to
//       * fails/asserts if this is an empty handle
//       * \return Reference to the element this handle points to
//       */
//      T& getElem()
//      {
//        assert(valid_);
//        return *it_;
//      }
//      
//      const T& getElem() const
//      {
//        assert(valid_);
//        return *it_;
//      }
//
//      /**
//       * \brief Checks if two handles point to the same list elem
//       */
//      bool operator==(const Handle& rhs) const
//      {
//          assert(valid_);
//          assert(rhs.valid_);
//        return (it_ == rhs.it_);
//      }
//
//      friend class ManagedList;
//      // Need this friend declaration so that iterator::createHandle() can
//      // call the private Handle::Handle() declared below.
//      friend class iterator;
//    private:
//      Handle( const boost::shared_ptr<void>& handle_tracker, iterator it) :
//        it_(it), handle_tracker_(handle_tracker), valid_(true)
//      { }
//
//      iterator it_;
//      boost::shared_ptr<void> handle_tracker_;
//      bool valid_;
//  };
//
//  ManagedList() { }
//
//  /**
//   * \brief Add an element to the back of the ManagedList
//   */
//  Handle add(const T& elem)
//  {
//    return add(elem, boost::bind(&ManagedList<T>::defaultDeleter, this, _1) );
//  }
//
//  /**
//   * \brief Add an element to the back of the ManagedList, along with a Custom deleter
//   * \param elem The element we want to add
//   * \param deleter Object on which operator() is called when refcount goes to 0
//   */
//  Handle add(const T& elem, CustomDeleter custom_deleter, const boost::shared_ptr<DestructionGuard>& guard)
//  {
//    TrackedElem tracked_t;
//    tracked_t.elem = elem;
//
//    typename std::list<TrackedElem>::iterator list_it = list_.insert(list_.end(), tracked_t);
//    iterator managed_it = iterator(list_it);
//
//    ElemDeleter deleter(managed_it, custom_deleter, guard);
//    boost::shared_ptr<void> tracker( (void*) NULL, deleter);
//
//    list_it->handle_tracker_ = tracker;
//
//    return Handle(tracker, managed_it);
//  }
//
//  /**
//   * \brief Removes an element from the ManagedList
//   */
//  void erase(iterator it)
//  {
//    list_.erase(it.it_);
//  }
//
//  iterator end()    { return iterator(list_.end()); }
//  iterator begin()  { return iterator(list_.begin()); }
//
//private:
//  void defaultDeleter(iterator it)
//  {
//    erase(it);
//  }
//  std::list<TrackedElem> list_;
//};
//
//
//template<class T>
//typename ManagedList<T>::Handle ManagedList<T>::iterator::createHandle()
//{
//  if (it_->handle_tracker_.expired())
//    ROS_ERROR_NAMED("actionlib", "Tried to create a handle to a list elem with refcount 0");
//
//  boost::shared_ptr<void> tracker = it_->handle_tracker_.lock();
//
//  return Handle(tracker, *this);
//}
//
//}