/*
 *
 * Developed by Adam White
 *  https://csharpcodewhisperer.blogspot.com
 * 
 */
using System;
using System.Linq;
using System.Text;
using System.Numerics;
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

		public static BigInteger String2Decimal(string Input)
		{
			BigInteger lResult = 0;
			BigInteger.TryParse(Input, out lResult);
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

			int termCount = EquationArgs.TermPool.Count;
			int opCount = EquationArgs.OperatorPool.Length;

			int counter = EquationArgs.NumberOfOperations - 1;
			while (counter-- > 0)
			{
				operators.Add(EquationArgs.OperatorPool.ElementAt(EquationArgs.Rand.Next(0, opCount)).ToString());
			}

			counter = EquationArgs.NumberOfOperations;
			while (counter-- > 0)
			{
				terms.Add(EquationArgs.TermPool.ElementAt(EquationArgs.Rand.Next(0, termCount)).ToString());
			}

			counter = 0;

			StringBuilder stringBuilder = new StringBuilder(terms[counter++]);

			foreach (string op in operators)
			{
				stringBuilder.AppendFormat(" {0} {1}", op, terms[counter++]);
			}

			return stringBuilder.ToString();
		}

		public static BigInteger Pow(BigInteger value, BigInteger exponent)
		{
			int use = 1;
			BigInteger result = new BigInteger();
			BigInteger exponentLeft = new BigInteger();

			result = BigInteger.Abs(value);
			exponentLeft = BigInteger.Abs(exponent);

			while (exponentLeft > 0)
			{
				if (exponentLeft > int.MaxValue - 2)
				{
					use = (int.MaxValue - 2);
					exponentLeft = BigInteger.Subtract(exponentLeft, use);
				}
				else
				{
					exponentLeft = BigInteger.Zero;
					use = (int)exponentLeft;
				}
				result = BigInteger.Multiply(result, BigInteger.Pow(result, use));
			}
			return result;
		}
	}
}
