/*
 *
 * Developed by Adam Rakaska
 *  http://www.csharpprogramming.tips
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EquationFinderCore;
using System.Globalization;

namespace EquationFactories
{
	public partial class AlgebraicTuple : IEquation
	{
		public decimal Result
		{
			get
			{
				if (_result == null) { _result = Solve(); }
				return (decimal)_result;
			}
		}
		private decimal? _result = null;

		IEquationFinderArgs EquationArgs { get; set; }
		List<Tuple<decimal, TupleOperation>> Equation { get; set; }
		string TermPool { get { return EquationArgs.TermPool; } }
		string OperatorPool { get { return EquationArgs.OperatorPool; } }
		decimal TargetValue { get { return EquationArgs.TargetValue; } }
		int NumberOfOperations { get { return EquationArgs.NumberOfOperations; } }

		public AlgebraicTuple()
		{
		}

		public AlgebraicTuple(EquationFinderArgs equationArgs)
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
			Equation = GenerateRandomEquation();
			Solve();
		}

		public bool IsSolution
		{
			get { return (Solve() == TargetValue); }
		}

		List<Tuple<decimal, TupleOperation>> GenerateRandomEquation()
		{
			List<Tuple<decimal, TupleOperation>> result = new List<Tuple<decimal, TupleOperation>>();

			int counter = 1;
			decimal term = 0;
			TupleOperation operation = new TupleOperation();
			OperandType lastOperand = OperandType.None;
			while (counter <= NumberOfOperations)
			{
				do
				{
					term = Convert.ToDecimal(TermPool.ElementAt(EquationArgs.Rand.Next(0, TermPool.Length)).ToString());
				}
				while (lastOperand == OperandType.Divide && term == 0);

				if (counter == NumberOfOperations)
				{
					operation = new TupleOperation(OperandType.Equal);
				}
				else
				{
					operation = new TupleOperation(OperatorPool.ElementAt(EquationArgs.Rand.Next(0, OperatorPool.Length)).ToString());
				}

				result.Add(new Tuple<decimal, TupleOperation>(term, operation));
				lastOperand = operation.Operand;
				counter++;
			}

			return result;
		}

		decimal Solve()
		{
			if (_result == null)
			{
				TupleOperation lastOperation = new TupleOperation(OperandType.None);
				decimal runningTotal = 0;
				foreach (Tuple<decimal, TupleOperation> t in Equation)
				{
					if (lastOperation.Operand == OperandType.None)
					{
						runningTotal = (decimal)t.Item1;
					}
					else
					{
						runningTotal = lastOperation.Calculate((decimal)runningTotal, (decimal)t.Item1);
					}
					lastOperation = t.Item2;
				}
				_result = runningTotal;
			}
		
			return (decimal)_result;			
		}

		public override string ToString()
		{
			StringBuilder resultText = new StringBuilder();
			foreach (Tuple<decimal, TupleOperation> exp in Equation)
			{
				resultText.AppendFormat(CultureInfo.CurrentCulture, "{0:0.##} {1} ", exp.Item1, exp.Item2);
			}
			resultText.AppendFormat(CultureInfo.CurrentCulture, "{0:0.##}", Result);
			return resultText.ToString();
		}
	}
}
