/*
 *
 * Developed by Adam Rakaska
 *  http://www.csharpprogramming.tips
 * 
 */
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

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
