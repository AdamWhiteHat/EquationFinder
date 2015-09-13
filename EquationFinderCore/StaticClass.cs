/*
 *
 * Developed by Adam Rakaska
 *  http://www.csharpprogramming.tips
 * 
 */
using System;
using System.Linq;
using System.Text;
//using MSScriptControl;
using System.Threading;
using System.Collections.Generic;

namespace EquationFinderCore
{
	public static class StaticRandom
	{
		private static int _seed;
		private static readonly Random _instance;

		static StaticRandom()
		{
			unchecked
			{
				_seed = Environment.TickCount * DateTime.Now.Millisecond;
			}
			_instance = new Random(_seed);
		}

		public static Random Instance { get { return _instance; } }

		public static int Next()
		{
			return _instance.Next();
		}

		public static int Next(int maxValue)
		{
			return _instance.Next(maxValue);
		}

		public static int Next(int minValue, int maxValue)
		{
			return _instance.Next(minValue, maxValue);
		}
	}

	public static class StaticClass
	{
		public const string DecimalNumbers = "0123456789";
		public const string AlgebraicOperators = "+-*/";

		public static int String2Int(string Input)
		{
			int iReturn = 0;
			if (Int32.TryParse(Input, out iReturn))
			{
				return iReturn;
			}
			else
			{
				return 0; 
			}
		}

		public static decimal String2Decimal(string Input)
		{
			decimal lResult = 0;
			decimal.TryParse(Input, out lResult);
			return lResult;
		}
	}
}
