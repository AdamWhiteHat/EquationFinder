/*
 *
 * Developed by Adam White
 *  https://csharpcodewhisperer.blogspot.com
 * 
 */
using System;
using System.Linq;
using System.Numerics;
using EquationFinderCore;
using System.Globalization;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace EquationFactories
{
	public class AlgebraicExpression2 : IEquation
	{
		public bool IsSolution
		{
			get
			{
				if (EquationArgs.TargetValuePredicate == ResultPredicate.IsDivisibleBy)
				{
					return ((Result != 0) && (Result % EquationArgs.TargetValue == 0));
				}
				else if (EquationArgs.TargetValuePredicate == ResultPredicate.IsEqualTo)
				{
					return (Result == EquationArgs.TargetValue);
				}
				return (Result == EquationArgs.TargetValue);
			}
		}

		public BigInteger Result { get { return Solve(); } }
		private BigInteger? _result = null;
		private IEquationFinderArgs EquationArgs { get; set; }
		private Expression Equation { get; set; }


		public static Dictionary<char, Func<Expression, Expression, Expression>> OperationTypeDictionary = new Dictionary<char, Func<Expression, Expression, Expression>>()
		{
			{'+', Expression.Add},
			{'-', Expression.Subtract},
			{'*', Expression.Multiply},
			{'/', Expression.Divide},
			{'^', Expression.Power}
		};

		private BigInteger _lastTerm = 0;
		private BigInteger GenerateTerm
		{
			get
			{
				_lastTerm = EquationArgs.TermPool.ElementAt(EquationArgs.Rand.Next(0, EquationArgs.TermPool.Count()));
				return _lastTerm;
			}
		}

		private char _lastOperation = '\0';
		private Func<Expression, Expression, Expression> GenerateOperator
		{
			get
			{
				_lastOperation = EquationArgs.OperatorPool.ElementAt(EquationArgs.Rand.Next(0, EquationArgs.OperatorPool.Count()));
				return OperationTypeDictionary[_lastOperation];
			}
		}

		public Func<Expression, Expression, Expression> NextOperation()
		{
			return GenerateOperator;
		}

		public AlgebraicExpression2()
		{ }

		public AlgebraicExpression2(IEquationFinderArgs args)
		{
			GenerateNewAndEvaluate(args);
		}

		public void GenerateNewAndEvaluate(IEquationFinderArgs args)
		{
			_result = null;
			EquationArgs = args;
			int counter = EquationArgs.NumberOfOperations;

			Expression expression = Expression.Empty();
			Func<Expression, Expression, Expression> operation = NextOperation();
			Expression lhs = AlgebraicBuilder.Constant(GenerateTerm);
			Expression rhs = AlgebraicBuilder.Constant(GenerateTerm);
			while (_lastOperation == '/' && _lastTerm == 0)
			{
				operation = NextOperation();
				rhs = AlgebraicBuilder.Constant(GenerateTerm);
			}
			expression = operation(lhs, rhs);
			counter -= 2;

			while (counter-- > 0)
			{
				if (EquationArgs.Rand.Next(0, 2) == 1)
				{
					operation = NextOperation();
					rhs = AlgebraicBuilder.Constant(GenerateTerm);

					while (_lastOperation == '/' && _lastTerm == 0)
					{
						operation = NextOperation();
						rhs = AlgebraicBuilder.Constant(GenerateTerm);
					}
					expression = operation(expression, rhs);
				}
				else
				{
					operation = NextOperation();
					lhs = AlgebraicBuilder.Constant(GenerateTerm);

					while (_lastOperation == '/')
					{
						operation = NextOperation();
					}
					expression = operation(lhs, expression);
				}
			}
			Equation = expression;
			Solve();
			BigInteger result = Result;
			bool solved = IsSolution;
		}

		private BigInteger Solve()
		{
			if (_result == null)
			{
				Expression<Func<BigInteger>> exprssn = Expression.Lambda<Func<BigInteger>>(Equation);
				Func<BigInteger> equationDelegate = exprssn.Compile();
				_result = equationDelegate.Invoke();
			}
			return (BigInteger)_result;
		}

		public override string ToString()
		{
			string expressionString = Equation.ToString();

			// If only one operation, either + or *, remove all parentheses
			if (EquationArgs.OperatorPool.Length == 1 && (EquationArgs.OperatorPool == "+" || EquationArgs.OperatorPool == "*"))
			{
				expressionString = expressionString.Replace("(", "").Replace(")", "");
			}
			else // Remove the superfluous parenthesis at beginning and end
			{
				expressionString = expressionString.Remove(0, 1);
				expressionString = expressionString.Remove(expressionString.Length - 1, 1);
			}
			return string.Format(CultureInfo.CurrentCulture, "{0} = {1:0.##}", expressionString, Result);
		}
	}

	public static class AlgebraicBuilder
	{
		public static Expression Constant(BigInteger value) { return Expression.Constant(value); }

		public static Expression<Func<T>> InvokeLambda<T>(this Expression<Func<T>> lhs, Func<Expression, Expression, BinaryExpression> operation, Expression<Func<T>> rhs)
		{
			InvocationExpression invokedExpr = Expression.Invoke(rhs, lhs.Parameters.Cast<Expression>());
			return Expression.Lambda<Func<T>>(operation(lhs.Body, invokedExpr), lhs.Parameters);
		}
	}
}