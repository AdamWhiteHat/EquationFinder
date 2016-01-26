using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EquationFactories
{
	public static class PostfixNotation
	{
		private static string AllowedCharacters = InfixNotation.Numbers + InfixNotation.Operators + " ";

		public static Expression<Func<int>> ExpressionTree(string postfixNotationString)
		{
			if (string.IsNullOrWhiteSpace(postfixNotationString))
			{
				throw new ArgumentException("Argument postfixNotationString must not be null, empty or whitespace.", "postfixNotationString");
			}

			Stack<Expression> stack = new Stack<Expression>();
			string sanitizedString = new string(postfixNotationString.Where(c => AllowedCharacters.Contains(c)).ToArray());
			List<string> enumerablePostfixTokens = sanitizedString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

			foreach (string token in enumerablePostfixTokens)
			{
				if (token.Length < 1)
				{
					throw new Exception("Token.Length is less than one.");
				}
				
				int tokenValue = 0;
				bool parseSuccess = int.TryParse(token, out tokenValue);


				if (token.Length > 1) // Numbers > 10 will have a token length > 1
				{
					if (InfixNotation.IsNumeric(token) && parseSuccess)
					{
						stack.Push(Expression.Constant(tokenValue));
					}
					else
					{
						throw new Exception("Operators and operands must be separated by a space.");
					}
				}
				else
				{
					char tokenChar = token[0];

					if (InfixNotation.Numbers.Contains(tokenChar) && parseSuccess)
					{
						stack.Push(Expression.Constant(tokenValue));
					}
					else if (InfixNotation.Operators.Contains(tokenChar))
					{
						if (stack.Count < 2) // There must be two operands for the operator to operate on
						{
							throw new FormatException("The algebraic string has not sufficient values in the expression for the number of operators; There must be two operands for the operator to operate on.");
						}

						Expression left = stack.Pop();
						Expression right = stack.Pop();
						Expression operation = null;	
						
						// ^ token uses Math.Pow, which both gives and takes double, hence convert
						if (tokenChar == '^')
						{
							if (left.Type != typeof(double))
							{
								left = Expression.Convert(left, typeof(double));
							}
							if (right.Type != typeof(double))
							{
								right = Expression.Convert(right, typeof(double));
							}
						}
						else // Math.Pow returns a double, so we must check here for all other operators
						{
							if (left.Type != typeof(int))
							{
								left = Expression.Convert(left, typeof(int));
							}
							if (right.Type != typeof(int))
							{
								right = Expression.Convert(right, typeof(int));
							}
						}

						if (tokenChar == '+')
						{
							operation = Expression.AddChecked(left, right);
						}
						else if (tokenChar == '-')
						{
							operation = Expression.SubtractChecked(left, right);
						}
						else if (tokenChar == '*')
						{
							operation = Expression.MultiplyChecked(left, right);
						}
						else if (tokenChar == '/')
						{
							operation = Expression.Divide(left, right);
						}
						else if (tokenChar == '^')
						{
							operation = Expression.Power(left, right);
						}

						if (operation != null)
						{
							stack.Push(operation);
						}
						else
						{
							throw new Exception("Value never got set.");
						}
					}
					else
					{
						throw new Exception(string.Format("Unrecognized character '{0}'.", tokenChar));
					}
				}

			}

			if (stack.Count == 1)
			{
				return Expression.Lambda<Func<int>>(stack.Pop());
			}
			else
			{
				throw new Exception("The input has too many values for the number of operators.");
			}

		} // method


		public static int Evaluate(string postfixNotationString)
		{
			if (string.IsNullOrWhiteSpace(postfixNotationString))
			{
				throw new ArgumentException("Argument postfixNotationString must not be null, empty or whitespace.", "postfixNotationString");
			}

			Stack<string> stack = new Stack<string>();
			string sanitizedString = new string(postfixNotationString.Where(c => AllowedCharacters.Contains(c)).ToArray());
			List<string> enumerablePostfixTokens = sanitizedString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

			foreach (string token in enumerablePostfixTokens)
			{
				if (token.Length > 0)
				{
					if (token.Length > 1)
					{
						if (InfixNotation.IsNumeric(token))
						{
							stack.Push(token);
						}
						else
						{
							throw new Exception("Operators and operands must be separated by a space.");
						}
					}
					else
					{
						char tokenChar = token[0];

						if (InfixNotation.Numbers.Contains(tokenChar))
						{
							stack.Push(tokenChar.ToString());
						}
						else if (InfixNotation.Operators.Contains(tokenChar))
						{
							if (stack.Count < 2)
							{
								throw new FormatException("The algebraic string has not sufficient values in the expression for the number of operators.");
							}

							string r = stack.Pop();
							string l = stack.Pop();

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
							if (tokenChar == '+')
							{
								value = lhs + rhs;
							}
							else if (tokenChar == '-')
							{
								value = lhs - rhs;
							}
							else if (tokenChar == '*')
							{
								value = lhs * rhs;
							}
							else if (tokenChar == '/')
							{
								value = lhs / rhs;
							}
							else if (tokenChar == '^')
							{
								value = (int)Math.Pow(lhs, rhs);
							}

							if (value != int.MinValue)
							{
								stack.Push(value.ToString());
							}
							else
							{
								throw new Exception("Value never got set.");
							}
						}
						else
						{
							throw new Exception(string.Format("Unrecognized character '{0}'.", tokenChar));
						}
					}
				}
				else
				{
					throw new Exception("Token length is less than one.");
				}
			}

			if (stack.Count == 1)
			{
				int result = 0;
				if (!int.TryParse(stack.Pop(), out result))
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
