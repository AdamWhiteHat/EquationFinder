/*
 *
 * Developed by Adam White
 *  https://csharpcodewhisperer.blogspot.com
 * 
 */
using System;
using System.Linq;
using System.Numerics;
using System.Globalization;
using System.Collections.Generic;
using EquationFinderCore;

namespace EquationFactories
{
	public partial class AlgebraicString : IEquation
	{
		public bool IsCorrect { get { return (Result == TargetValue); } }
		public BigInteger Result { get { return Solve(); } }
		private BigInteger? _result = null;
		private string Equation { get; set; }
		private IEquationFinderArgs EquationArgs { get; set; }
		private List<int> TermPool { get { return EquationArgs.TermPool; } }
		private string OperatorPool { get { return EquationArgs.OperatorPool; } }
		private BigInteger TargetValue { get { return EquationArgs.TargetValue; } }
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

		private BigInteger Solve()
		{
			if (_result == null)
			{
				_result = InfixNotation.Evaluate(Equation);
				//_result = StaticScriptControl.Evaluate(Equation);
			}
			return (BigInteger)_result;
		}

		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0} = {1:0.##}", Equation, Result);
		}
	}
}
