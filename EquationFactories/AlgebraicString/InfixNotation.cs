/*
 *
 * Developed by Adam White
 *  https://csharpcodewhisperer.blogspot.com
 * 
 */
using System;
using System.Linq;
using System.Numerics;
using System.Collections.Generic;

namespace EquationFactories
{
	public static class InfixNotation
	{
		public static string Numbers = "0123456789";
		public static string Operators = "+-*/^";

		public static bool IsNumeric(string text)
		{
			return string.IsNullOrWhiteSpace(text) ? false : text.All(c => Numbers.Contains(c));
		}

		public static BigInteger Evaluate(string infixNotationString)
		{
			string postFixNotationString = ShuntingYardConverter.Convert(infixNotationString);
			return PostfixNotation.Evaluate(postFixNotationString);
		}
	}
}
