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
			Equation = HelperClass.GenerateRandomEquation(EquationArgs);
			return IsSolution;
		}

		public bool IsSolution
		{
			get { return (Result == TargetValue); }
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
