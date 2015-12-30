/*
 *
 * Developed by Adam Rakaska
 *  http://www.csharpprogramming.tips
 * 
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquationFinderCore;

namespace EquationFactories
{
	public partial class AlgebraicString : IEquation
	{
		IEquationFinderArgs EquationArgs { get; set; }
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

		public AlgebraicString(IEquationFinderArgs equationArgs)
		{
			SetArgs(equationArgs);
		}

		public void SetArgs(IEquationFinderArgs args)
		{
			EquationArgs = args;
			GenerateNewAndEvaluate();
		}

		public void GenerateNewAndEvaluate()
		{
			_result = null;
			Equation = HelperClass.GenerateRandomEquation(EquationArgs);
			Solve();
		}

		public bool IsSolution
		{
			get { return (Result == TargetValue); }
		}		

		decimal Solve()
		{
			if (_result == null)
			{
				_result = InfixNotationParser.Parse(Equation);
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
