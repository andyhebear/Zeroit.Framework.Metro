// ***********************************************************************
// Assembly         : Zeroit.Framework.Metro
// Author           : ZEROIT
// Created          : 11-28-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="MetroTrackerPathCollection.cs" company="Zeroit Dev Technologies">
//    This program is for creating Metro Style controls.
//    Copyright ©  2017  Zeroit Dev Technologies
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <https://www.gnu.org/licenses/>.
//
//    You can contact me at zeroitdevnet@gmail.com or zeroitdev@outlook.com
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Zeroit.Framework.Metro
{
    /// <summary>
    /// Class MetroTrackerPathCollection.
    /// </summary>
    /// <seealso cref="System.Collections.ObjectModel.Collection{Zeroit.Framework.Metro.MetroTrackerPath}" />
    public class MetroTrackerPathCollection : Collection<MetroTrackerPath>
	{
        /// <summary>
        /// The enc list
        /// </summary>
        private static List<WeakReference> __ENCList;

        /// <summary>
        /// Initializes static members of the <see cref="MetroTrackerPathCollection"/> class.
        /// </summary>
        [DebuggerNonUserCode]
		static MetroTrackerPathCollection()
		{
			MetroTrackerPathCollection.__ENCList = new List<WeakReference>();
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="MetroTrackerPathCollection"/> class.
        /// </summary>
        [DebuggerNonUserCode]
		public MetroTrackerPathCollection()
		{
			MetroTrackerPathCollection.__ENCAddToList(this);
		}

        /// <summary>
        /// Encs the add to list.
        /// </summary>
        /// <param name="value">The value.</param>
        [DebuggerNonUserCode]
		private static void __ENCAddToList(object value)
		{
			List<WeakReference> _ENCList = MetroTrackerPathCollection.__ENCList;
			Monitor.Enter(_ENCList);
			try
			{
				if (MetroTrackerPathCollection.__ENCList.Count == MetroTrackerPathCollection.__ENCList.Capacity)
				{
					int item = 0;
					int count = checked(MetroTrackerPathCollection.__ENCList.Count - 1);
					for (int i = 0; i <= count; i = checked(i + 1))
					{
						if (MetroTrackerPathCollection.__ENCList[i].IsAlive)
						{
							if (i != item)
							{
								MetroTrackerPathCollection.__ENCList[item] = MetroTrackerPathCollection.__ENCList[i];
							}
							item = checked(item + 1);
						}
					}
					MetroTrackerPathCollection.__ENCList.RemoveRange(item, checked(MetroTrackerPathCollection.__ENCList.Count - item));
					MetroTrackerPathCollection.__ENCList.Capacity = MetroTrackerPathCollection.__ENCList.Count;
				}
				MetroTrackerPathCollection.__ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
			}
			finally
			{
				Monitor.Exit(_ENCList);
			}
		}

        /// <summary>
        /// Removes all elements from the <see cref="T:System.Collections.ObjectModel.Collection`1" />.
        /// </summary>
        protected override void ClearItems()
		{
			IEnumerator<MetroTrackerPath> enumerator = null;
			using (enumerator)
			{
				enumerator = this.GetEnumerator();
				while (enumerator.MoveNext())
				{
					MetroTrackerPath current = enumerator.Current;
					EventHandler<MetroTrackerPathCollectionEventArgs> eventHandler = this.ItemRemoving;
					if (eventHandler != null)
					{
						eventHandler(this, new MetroTrackerPathCollectionEventArgs(current));
					}
				}
			}
			base.ClearItems();
		}

        /// <summary>
        /// Inserts an element into the <see cref="T:System.Collections.ObjectModel.Collection`1" /> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
        /// <param name="item">The object to insert. The value can be null for reference types.</param>
        protected override void InsertItem(int index, MetroTrackerPath item)
		{
			base.InsertItem(index, item);
			EventHandler<MetroTrackerPathCollectionEventArgs> eventHandler = this.ItemAdded;
			if (eventHandler != null)
			{
				eventHandler(this, new MetroTrackerPathCollectionEventArgs(item));
			}
		}

        /// <summary>
        /// Removes the element at the specified index of the <see cref="T:System.Collections.ObjectModel.Collection`1" />.
        /// </summary>
        /// <param name="index">The zero-based index of the element to remove.</param>
        protected override void RemoveItem(int index)
		{
			EventHandler<MetroTrackerPathCollectionEventArgs> eventHandler = this.ItemRemoving;
			if (eventHandler != null)
			{
				eventHandler(this, new MetroTrackerPathCollectionEventArgs(this[index]));
			}
			base.RemoveItem(index);
		}

        /// <summary>
        /// Replaces the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to replace.</param>
        /// <param name="item">The new value for the element at the specified index. The value can be null for reference types.</param>
        protected override void SetItem(int index, MetroTrackerPath item)
		{
			EventHandler<MetroTrackerPathCollectionEventArgs> eventHandler = this.ItemRemoving;
			if (eventHandler != null)
			{
				eventHandler(this, new MetroTrackerPathCollectionEventArgs(this[index]));
			}
			base.SetItem(index, item);
			eventHandler = this.ItemAdded;
			if (eventHandler != null)
			{
				eventHandler(this, new MetroTrackerPathCollectionEventArgs(item));
			}
		}

        /// <summary>
        /// Occurs when [item added].
        /// </summary>
        public event EventHandler<MetroTrackerPathCollectionEventArgs> ItemAdded;

        /// <summary>
        /// Occurs when [item removing].
        /// </summary>
        public event EventHandler<MetroTrackerPathCollectionEventArgs> ItemRemoving;
	}
}