/*
 *
 * Developed by Adam Rakaska
 *  http://www.csharpprogramming.tips
 * 
 */
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace EquationFinderCore
{
	public static class HelperClass
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

		public static bool IsDebug()
		{
			bool result = false;
#if DEBUG
			result = true;
#endif
			return result;
		}

		public static string GenerateRandomEquation(IEquationFinderArgs EquationArgs)
		{
			List<string> operators = new List<string>(EquationArgs.NumberOfOperations);
			List<string> terms = new List<string>(EquationArgs.NumberOfOperations);

			int counter = EquationArgs.NumberOfOperations - 1;
			while (counter-- > 0)
			{
				operators.Add(EquationArgs.OperatorPool.ElementAt(EquationArgs.Rand.Next(0, EquationArgs.OperatorPool.Length)).ToString());
			}

			counter = EquationArgs.NumberOfOperations;
			while (counter-- > 0)
			{
				terms.Add(EquationArgs.TermPool.ElementAt(EquationArgs.Rand.Next(0, EquationArgs.TermPool.Length)).ToString());
			}

			counter = 0;

			StringBuilder stringBuilder = new StringBuilder(terms[counter++]);

			foreach (string op in operators)
			{
				stringBuilder.AppendFormat(" {0} {1}", op, terms[counter++]);
			}

			return stringBuilder.ToString();
		}
	}	
}
