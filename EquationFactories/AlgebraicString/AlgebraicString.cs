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
	public partial class AlgebraicString : IEquation
	{
		public EquationFinderArgs EquationArgs { get; set; }
		public string Equation { get; private set; }

		public string TermPool { get { return EquationArgs.TermPool; } }
		public string OperatorPool { get { return EquationArgs.OperatorPool; } }
		public decimal TargetValue { get { return EquationArgs.TargetValue; } }
		public int NumberOfOperations { get { return EquationArgs.NumberOfOperations; } }
		public bool IsCorrect { get { return (Evaluate() == TargetValue); } }

		public decimal Evaluate()
		{
			if (_result == null) { _result = Solve(); }
			return (decimal)_result;
		}
		private decimal? _result = null;

		

		public AlgebraicString()
		{
		}

		public AlgebraicString(EquationFinderArgs equationArgs)
		{
			Initialize(equationArgs);
		}

		public void Initialize(EquationFinderArgs equationArgs)
		{
			EquationArgs = equationArgs;
			Equation = GenerateRandomEquation();
			Evaluate();
		}

		public string GenerateRandomEquation()
		{
			List<string> operators = new List<string>(NumberOfOperations);
			List<string> terms = new List<string>(NumberOfOperations);

			int counter = NumberOfOperations - 1;
			while (counter-- > 0)
				operators.Add(OperatorPool.ElementAt(StaticRandom.Instance.Next(0, OperatorPool.Length)).ToString());
						
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
			return StaticScriptControl.Evaluate(Equation);
		}

		public EquationResults GetResults()
		{
			return new EquationResults(this);
		}

		public override string ToString()
		{
			return Equation + " = " + Evaluate().ToString();
		}
	}
}
