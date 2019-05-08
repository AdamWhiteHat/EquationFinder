/*
 *
 * Developed by Adam White
 *  https://csharpcodewhisperer.blogspot.com
 * 
 */
using System;
using System.Linq;
using System.Globalization;
using System.Linq.Expressions;
using System.Collections.Generic;
using EquationFinderCore;

namespace EquationFactories
{
	public class ExpressionBuilder<T> where T : struct
	{

		private IEquationFinderArgs _equationArgs = null;
		private IEquationFinderArgs EquationArgs
		{
			get { return _equationArgs; }
			set { if (_equationArgs == null) { _equationArgs = value; } }
		}

		private Expression _expression = null;
		public Expression Expression
		{
			get
			{
				if (_expression == null)
				{
					_expression = GenerateExpression();
				}
				return _expression;
			}
		}

		private Expression<Func<T>> _expressionTree = null;
		public Expression<Func<T>> ExpressionTree
		{
			get
			{
				if (_expressionTree == null)
				{
					_expressionTree = Expression.Lambda<Func<T>>(Expression);
				}
				return _expressionTree;
			}
		}

		private Func<T> _func = null;
		public Func<T> Func
		{
			get
			{
				if (_func == null)
				{
					_func = ExpressionTree.Compile();
				}
				return _func;
			}
		}

		private T? _result = null;
		public T Result
		{
			get
			{
				if (_result == null)
				{
					_result = Func();
				}
				return _result.HasValue ? _result.Value : default(T);
			}
		}

		public ExpressionBuilder(IEquationFinderArgs equationArgs)
		{
			if (equationArgs == null)
			{
				throw new ArgumentNullException("equationArgs");
			}
			EquationArgs = new EquationFinderArgs(equationArgs.TargetValue, equationArgs.NumberOfOperations, equationArgs.TermPool, equationArgs.OperatorPool);
		}

		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0} = {1:0.######}", Expression.ToString().Replace("(", "").Replace(")", ""), Result);
		}



		#region PRIVATE METHODS

		private int termCount { get; set; }
		private int opCount { get; set; }
		private Expression GenerateExpression()
		{
			if (EquationArgs == null)
			{
				throw new Exception("EquationArgs must be initialized and not null.");
			}

			termCount = EquationArgs.TermPool.Count;
			opCount = EquationArgs.OperatorPool.Length;

			Expression result = GenerateRandomConstant();

			int counter = EquationArgs.NumberOfOperations;
			while (counter-- > 0)
			{
				result = AddRandomOperation(result);
			}

			//result.Derivative("");

			return result;
		}

		private Expression AddRandomOperation(Expression lhs)
		{
			Expression rhs = Expression.Empty();
			rhs = GenerateRandomConstant();

			string randomOperation = EquationArgs.OperatorPool.ElementAt(EquationArgs.Rand.Next(0, opCount)).ToString();
			switch (randomOperation)
			{
				case "+":
					return Expression.AddChecked(lhs, rhs);
				case "-":
					return Expression.SubtractChecked(lhs, rhs);
				case "*":
					return Expression.MultiplyChecked(lhs, rhs);
				case "/":
					return Expression.Divide(lhs, GenerateRandomConstant(true));
				case "^":
					var leftHandSide = lhs.Reduce();
					var invokedExpr = Expression.Invoke(leftHandSide);
					return Expression.Lambda<Func<T>>(Expression.Power(rhs, invokedExpr));

				default:
					throw new Exception(string.Format("Operation '{0}' not within expected range.", randomOperation));
			}
		}

		private Expression GenerateRandomConstant(bool noZero = false)
		{
			T randDecimal = GetRandomDecimal(noZero);
			return Expression.Constant(randDecimal);
		}

		private T GetRandomDecimal(bool noZero = false)
		{
			int term = EquationArgs.TermPool[EquationArgs.Rand.Next(0, termCount)];
			if (noZero)
			{
				term = EquationArgs.Rand.Next(1, termCount);
			}

			return (T)Convert.ChangeType(term, typeof(T));
		}

		#endregion

	}
}
