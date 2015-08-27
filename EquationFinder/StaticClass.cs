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

namespace EquationFinder
{
	public static class StaticRandom
	{
		private static int _seed;
		private static readonly Random _instance = new Random(Interlocked.Increment(ref _seed));

		static StaticRandom()
		{
			_seed = Environment.TickCount;
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

		public static string GenerateRandomStringExpression(int NumberOfOperations, string OperatorPool, string TermPool)
		{
			List<string> operators = new List<string>(NumberOfOperations);
			List<string> terms = new List<string>(NumberOfOperations);

			int counter = NumberOfOperations - 1;
			while (counter-- > 0)
				operators.Add(OperatorPool.ElementAt(StaticRandom.Instance.Next(0, OperatorPool.Length)).ToString());

			counter = NumberOfOperations;
			while (counter-- > 0)
				terms.Add(TermPool.ElementAt(StaticRandom.Instance.Next(0, OperatorPool.Length)).ToString());

			counter = 0;
			string result = terms[counter++];

			foreach (string op in operators)
				result += string.Format(" {0} {1}", op, terms[counter++]);

			return result;
		}

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
