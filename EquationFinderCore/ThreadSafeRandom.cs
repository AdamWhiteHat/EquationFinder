using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquationFinderCore
{
	public sealed class StaticRandom
	{
		private static readonly Random _instance;
		public static Random Instance
		{
			get
			{
				return _instance;
			}
		}

		private StaticRandom()
		{ }	// Explicit private constructor with no public constructors prevents other classes from instantiating it

		static StaticRandom()
		{ // Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
			_instance = Factory.Random();
			int rounds = 250;
			while (rounds-- > 0) { _instance.Next(); }
		}

		public class Factory
		{
			public static Random Random()
			{
				Random result = new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0));

				int rounds = 250;
				while (rounds-- > 0) { result.Next(); }
				return result;
			}
		}
	}
}
