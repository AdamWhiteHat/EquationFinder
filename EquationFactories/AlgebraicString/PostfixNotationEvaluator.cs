using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquationFactories
{
	public static class PostfixNotationEvaluator
	{
		private static string Numbers = "0123456789";
		private static string Operators = "+-*/^";
		private static string AllowedCharacters = Numbers + Operators + " ";

		public static int Evaluate(string postfixNotationString)
		{
			if (string.IsNullOrWhiteSpace(postfixNotationString))
			{
				throw new ArgumentException("Argument postfixNotationString must not be null, empty or whitespace.", "postfixNotationString");
			}

			List<char> output = new List<char>();
			Stack<string> valueStack = new Stack<string>();
			string sanitizedString = new string(postfixNotationString.Where(c => AllowedCharacters.Contains(c)).ToArray());
			List<string> enumerablePostfixTokens = sanitizedString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
			
			foreach (string token in enumerablePostfixTokens)
			{
				if (token.Length > 0)
				{
					if (token.Length > 1)
					{
						if (ShuntingYardConverter.IsNumeric(token))
						{
							valueStack.Push(token);
						}
						else
						{
							throw new Exception("Operators and operands must be separated by a space.");
						}
					}
					else
					{
						char c = token[0];

						if (Numbers.Contains(c))
						{
							valueStack.Push(c.ToString());
						}
						else if (Operators.Contains(c))
						{
							if (valueStack.Count < 2)
							{
								throw new FormatException("The algebraic string has not sufficient values in the expression for the number of operators.");
							}

							string r = valueStack.Pop();
							string l = valueStack.Pop();

							int rhs = int.MinValue;
							int lhs = int.MinValue;

							bool parseSuccess = int.TryParse(r, out rhs);
							parseSuccess &= int.TryParse(l, out lhs);
							parseSuccess &= (rhs != int.MinValue && lhs != int.MinValue);

							if (!parseSuccess)
							{
								throw new Exception("Unable to parse valueStack characters to Int32.");
							}

							int value = int.MinValue;
							if (c == '+')
							{
								value = lhs + rhs;
							}
							else if (c == '-')
							{
								value = lhs - rhs;
							}
							else if (c == '*')
							{
								value = lhs * rhs;
							}
							else if (c == '/')
							{
								value = lhs / rhs;
							}
							else if (c == '^')
							{
								value = (int)Math.Pow(lhs, rhs);
							}

							if (value != int.MinValue)
							{
								valueStack.Push(value.ToString());
							}
							else
							{
								throw new Exception("Value never got set.");
							}
						}
						else
						{
							throw new Exception(string.Format("Unrecognized character '{0}'.", c));
						}
					}
				}
				else
				{
					throw new Exception("Token length is less than one.");
				}
			}

			if (valueStack.Count == 1)
			{
				int result = 0;
				if (!int.TryParse(valueStack.Pop(), out result))
				{
					throw new Exception("Last value on stack could not be parsed into an integer.");
				}
				else
				{
					return result;
				}
			}
			else
			{
				throw new Exception("The input has too many values for the number of operators.");
			}

		} // method
	} // class
} // namespace
