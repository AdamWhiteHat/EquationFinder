using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquationFactories
{
	public static class InfixNotationParser
	{
		public static int Parse(string infixNotationString)
		{
			string postFixNotationString = ShuntingYardConverter.Convert(infixNotationString);
			int result = PostfixNotationEvaluator.Evaluate(postFixNotationString);
			return result;
		}
	}
}
