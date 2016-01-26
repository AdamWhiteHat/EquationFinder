using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

		public static int Evaluate(string infixNotationString)
		{
			string postFixNotationString = ShuntingYardConverter.Convert(infixNotationString);
			int result = PostfixNotation.Evaluate(postFixNotationString);
			return result;
		}
	}
}
