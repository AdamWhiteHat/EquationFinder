using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquationFinderCore
{
	public sealed class StaticRandom
	{
		public static Random Instance { get { return Nested.instance; } }

		private StaticRandom()
		{ }	// Explicit private constructor with no public constructors prevents other classes from instantiating it

		private class Nested
		{
			internal static readonly Random instance;
			static Nested()
			{ // Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
				instance = new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0));
				int rounds = instance.Next(100, 200);
				while (rounds-- > 0) { instance.Next(); }
			}			
		}
	}
}
