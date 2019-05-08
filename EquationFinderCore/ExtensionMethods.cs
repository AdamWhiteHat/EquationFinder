/*
 *
 * Developed by Adam White
 *  https://csharpcodewhisperer.blogspot.com
 * 
 */
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace EquationFinderCore
{
	public static class BlockingCollectionExtensionMethods
	{
		public static void AddRange<T>(this BlockingCollection<T> source, IEnumerable<T> collection)
		{
			bool isString = (typeof(T) == typeof(string));
			bool isStringCollection = (collection is IEnumerable<string>);

			foreach (T item in collection)
			{
				if (item == null)
				{
					continue;
				}

				if (isString && string.IsNullOrWhiteSpace(Convert.ToString(item)))
				{
					continue;
				}

				// else
				source.Add(item);
			}
		}
	}
}
