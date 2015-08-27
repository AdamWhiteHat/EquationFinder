﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationFinder
{
	public partial class AlgebraicString : IExpression
	{
		public EquationFinderArgs EquationArgs { get; private set; }
		public string Expression { get; private set; }

		public decimal TargetValue { get { return EquationArgs.TargetValue; } }
		public int NumberOfOperations { get { return EquationArgs.NumberOfOperations; } }
		public string OperatorPool { get { return EquationArgs.OperatorPool; } }
		public string TermPool { get { return EquationArgs.TermPool; } }

		public decimal CalculatedValue
		{
			get
			{
				return Evaluate();
			}
		}
		private decimal? _result = null;

		public decimal Evaluate()
		{
			if (_result == null)
			{
				_result = Solve();
			}
			return (decimal)_result;
		}

		public bool IsCorrect
		{
			get { return (CalculatedValue == TargetValue); }
		}

		public AlgebraicString(EquationFinderArgs equationArgs)
		{
			EquationArgs = equationArgs;
			BuildExpression();
			Evaluate();
		}

		public IExpression NewExpression(IEquationFinderArgs equationArgs)
		{
			return new AlgebraicString((EquationFinderArgs)equationArgs);
		}

		public Task<IExpression> NewTask(EquationFinderArgs equationArgs)
		{
			return new Task<IExpression>(new Func<IExpression>(delegate { return new AlgebraicString(equationArgs); }));
		}

		protected void BuildExpression()
		{
			Expression = GenerateRandomExpression();
		}

		public string GenerateRandomExpression()
		{
			List<string> operators = new List<string>(NumberOfOperations);
			int counter = NumberOfOperations - 1;
			while (counter-- > 0)
				operators.Add(OperatorPool.ElementAt(StaticRandom.Instance.Next(0, OperatorPool.Length)).ToString());

			List<string> terms = new List<string>(NumberOfOperations);
			counter = NumberOfOperations;
			while (counter-- > 0)
				terms.Add(TermPool.ElementAt(StaticRandom.Instance.Next(0, TermPool.Length)).ToString());

			counter = 0;
			string result = terms[counter++];

			foreach (string op in operators)
				result += string.Format(" {0} {1}", op, terms[counter++]);

			return result;
		}

		private decimal Solve()
		{
			return StaticScriptControl.Evaluate(Expression);
		}

		public ExpressionResults GetResults()
		{
			return new ExpressionResults(TargetValue, this);
		}

		public override string ToString()
		{
			return Expression + " = " + CalculatedValue.ToString();
		}
	}
}