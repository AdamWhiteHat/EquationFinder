/*
 *
 * Developed by Adam Rakaska
 *  http://www.csharpprogramming.tips
 * 
 */
using System;
using System.Linq;
using System.Text;
using EquationFinderCore;
using System.Globalization;
using System.Collections.Generic;

namespace EquationFactories
{
	public partial class AlgebraicString : IEquation
	{
		public bool IsCorrect { get { return (Result == TargetValue); } }
		public decimal Result { get { return Solve(); } }
		private decimal? _result = null;
		private string Equation { get; set; }
		private IEquationFinderArgs EquationArgs { get; set; }
		private List<int> TermPool { get { return EquationArgs.TermPool; } }
		private string OperatorPool { get { return EquationArgs.OperatorPool; } }
		private decimal TargetValue { get { return EquationArgs.TargetValue; } }
		private int NumberOfOperations { get { return EquationArgs.NumberOfOperations; } }

		public AlgebraicString()
		{ }

		public AlgebraicString(IEquationFinderArgs equationArgs)
		{
			GenerateNewAndEvaluate(equationArgs);
		}

		public void GenerateNewAndEvaluate(IEquationFinderArgs equationArgs)
		{
			_result = null;
			EquationArgs = equationArgs;
			Equation = HelperClass.GenerateRandomEquation(EquationArgs);
			Solve();
		}

		public bool IsSolution
		{
			get { return (Result == TargetValue); }
		}

		private decimal Solve()
		{
			if (_result == null)
			{
				_result = InfixNotation.Evaluate(Equation);
				//_result = StaticScriptControl.Evaluate(Equation);
			}
			return (decimal)_result;
		}

		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0} = {1:0.##}", Equation, Result);
		}
	}
}
