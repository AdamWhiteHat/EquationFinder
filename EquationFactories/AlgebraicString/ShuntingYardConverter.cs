using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquationFactories
{
	public static class ShuntingYardConverter
	{
		private static string AllowedCharacters = InfixNotationParser.Numbers + InfixNotationParser.Operators + "()";

		private enum Associativity
		{
			Left, Right
		}
		private static Dictionary<char, int> PrecedenceDictionary = new Dictionary<char, int>()
		{	
			{'+', 1}, {'-', 1},
			{'*', 2}, {'/', 2},
			{'^', 3}
		};
		private static Dictionary<char, Associativity> AssociativityDictionary = new Dictionary<char, Associativity>()
		{
			{'+', Associativity.Left}, {'-', Associativity.Left}, {'*', Associativity.Left}, {'/', Associativity.Left},
			{'^', Associativity.Right}
		};

		public static bool IsNumeric(string text)
		{
			if (!string.IsNullOrWhiteSpace(text))
			{
				return false;
			}

			return text.All(c => InfixNotationParser.Numbers.Contains(c));
		}

		private static void AddToOutput(List<char> output, params char[] chars)
		{
			if (chars != null && chars.Length > 0)
			{
				foreach (char c in chars)
				{
					output.Add(c);
				}
				output.Add(' ');
			}
		}

		public static string Convert(string infixNotationString)
		{
			if (string.IsNullOrWhiteSpace(infixNotationString))
			{
				throw new ArgumentException("Argument infixNotationString must not be null, empty or whitespace.", "infixNotationString");
			}

			List<char> output = new List<char>();
			Stack<char> operatorStack = new Stack<char>();
			string sanitizedString = new string(infixNotationString.Where(c => AllowedCharacters.Contains(c)).ToArray());

			string number = string.Empty;
			List<string> enumerableInfixTokens = new List<string>();
			foreach (char c in sanitizedString)
			{
				if (InfixNotationParser.Operators.Contains(c) || "()".Contains(c))
				{
					if (number.Length > 0)
					{
						enumerableInfixTokens.Add(number);
						number = string.Empty;
					}
					enumerableInfixTokens.Add(c.ToString());
				}
				else if (InfixNotationParser.Numbers.Contains(c))
				{
					number += c.ToString();
				}
				else
				{
					throw new Exception(string.Format("Unexpected character '{0}'.", c));
				}
			}

			if (number.Length > 0)
			{
				enumerableInfixTokens.Add(number);
				number = string.Empty;
			}

			foreach (string token in enumerableInfixTokens)
			{
				if (IsNumeric(token))
				{
					AddToOutput(output, token.ToArray());
				}
				else if (token.Length == 1)
				{
					char c = token[0];

					if (InfixNotationParser.Numbers.Contains(c))
					{
						AddToOutput(output, c);
					}
					else if (InfixNotationParser.Operators.Contains(c))
					{
						if (operatorStack.Count > 0)
						{
							char o = operatorStack.Peek();
							if ((AssociativityDictionary[c] == Associativity.Left &&
								PrecedenceDictionary[c] <= PrecedenceDictionary[o])
									||
								(AssociativityDictionary[c] == Associativity.Right &&
								PrecedenceDictionary[c] < PrecedenceDictionary[o]))
							{
								AddToOutput(output, operatorStack.Pop());
							}
						}
						operatorStack.Push(c);
					}
					else if (c == '(')
					{
						operatorStack.Push(c);
					}
					else if (c == ')')
					{
						bool leftParenthesisFound = false;
						while (operatorStack.Count > 0)
						{
							char o = operatorStack.Peek();
							if (o != '(')
							{
								AddToOutput(output, operatorStack.Pop());
							}
							else
							{
								operatorStack.Pop();
								leftParenthesisFound = true;
							}
						}

						if (!leftParenthesisFound)
						{
							throw new FormatException("The algebraic string contains mismatched parentheses (missing a left parenthesis).");
						}
					}
					else
					{
						throw new Exception(string.Format("Unrecognized character '{0}'.", c));
					}
				}
				else
				{
					throw new Exception(string.Format("String '{0}' is not numeric and has a length greater than 1."));
				}
			} // foreach

			while (operatorStack.Count > 0)
			{
				char o = operatorStack.Pop();
				if (o == '(')
				{
					throw new FormatException("The algebraic string contains mismatched parentheses (extra left parenthesis).");
				}
				else if (o == ')')
				{
					throw new FormatException("The algebraic string contains mismatched parentheses (extra right parenthesis).");
				}
				else
				{
					AddToOutput(output, o);
				}
			}

			return new string(output.ToArray());
		}
	}
}
