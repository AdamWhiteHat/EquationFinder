/*
 *
 * Developed by Adam Rakaska
 *  http://www.csharpprogramming.tips
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquationFinderCore;

namespace EquationFactories
{
	public partial class AlgebraicString
	{
		EquationFinderArgs EquationArgs { get; set; }
		string Equation { get; set; }
		string TermPool { get { return EquationArgs.TermPool; } }
		string OperatorPool { get { return EquationArgs.OperatorPool; } }
		decimal TargetValue { get { return EquationArgs.TargetValue; } }
		int NumberOfOperations { get { return EquationArgs.NumberOfOperations; } }
		public bool IsCorrect { get { return (Result == TargetValue); } }

		public decimal Result
		{
			get
			{
				if (_result == null) { _result = Solve(); }
				return (decimal)_result;
			}
		}
		private decimal? _result = null;

		public AlgebraicString()
		{
		}

		public AlgebraicString(EquationFinderArgs equationArgs)
		{
			EquationArgs = equationArgs;
			GenerateAndEvaluate();
		}

		public bool GenerateAndEvaluate()
		{
			Equation = GenerateRandomEquation();
			return IsSolution;
		}

		public bool IsSolution
		{
			get { return (Result == TargetValue); }
		}

		string GenerateRandomEquation()
		{
			List<string> operators = new List<string>(NumberOfOperations);
			List<string> terms = new List<string>(NumberOfOperations);

			int counter = NumberOfOperations - 1;
			while (counter-- > 0)
				operators.Add(OperatorPool.ElementAt(EquationArgs.Rand.Next(0, OperatorPool.Length)).ToString());
						
			counter = NumberOfOperations;
			while (counter-- > 0)
				terms.Add(TermPool.ElementAt(EquationArgs.Rand.Next(0, TermPool.Length)).ToString());

			counter = 0;
			string result = terms[counter++];

			foreach (string op in operators)
				result += string.Format(" {0} {1}", op, terms[counter++]);

			return result;
		}

		decimal Solve()
		{
			if (_result == null)
			{
				return StaticScriptControl.Evaluate(Equation);
			}
			else
			{
				return -1;
			}
		}

		public override string ToString()
		{
			return Equation + " = " + Result.ToString();
		}
	}
}
