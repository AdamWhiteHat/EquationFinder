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

namespace EquationFactories
{
	public partial class AlgebraicTuple : IEquation
	{
		public EquationFinderArgs EquationArgs { get; set; }
		public List<Tuple<decimal, TupleOperation>> Equation { get; private set; }

		public string TermPool { get { return EquationArgs.TermPool; } }
		public string OperatorPool { get { return EquationArgs.OperatorPool; } }
		public decimal TargetValue { get { return EquationArgs.TargetValue; } }
		public int NumberOfOperations { get { return EquationArgs.NumberOfOperations; } }

		public void Dispose()
		{
			Equation.RemoveRange(0, Equation.Count);
			Equation = null;
			EquationArgs.Dispose();
			EquationArgs = null;			
		}

		public decimal Evaluate()
		{
			if (_result == null) { _result = Solve(); }
			return (decimal)_result;
		}
		private decimal? _result = null;

		public bool IsCorrect
		{
			get { return (Evaluate() == TargetValue); }
		}

		public AlgebraicTuple()
		{
		}

		public AlgebraicTuple(EquationFinderArgs equationArgs)
		{
			Initialize(equationArgs);
		}

		public void Initialize(EquationFinderArgs equationArgs)
		{
			EquationArgs = equationArgs;
			Equation = GenerateRandomEquation(NumberOfOperations);
			Evaluate();
		}

		public List<Tuple<decimal, TupleOperation>> GenerateRandomEquation(int numberOfOperations)
		{
			List<Tuple<decimal, TupleOperation>> result = new List<Tuple<decimal, TupleOperation>>();

			int Counter = 1;
			decimal term = 0;
			TupleOperation operation = new TupleOperation();
			OperandType lastOperand = OperandType.None;
			while (Counter <= numberOfOperations)
			{
				do
				{
					term = Convert.ToDecimal(TermPool.ElementAt(StaticRandom.Instance.Next(0, TermPool.Length)).ToString());
				}
				while (term == 0 && lastOperand == OperandType.Divide);

				if (Counter == numberOfOperations)
				{
					operation = new TupleOperation(OperandType.Equal);
				}
				else
				{
					//do
					//{
					operation = new TupleOperation(OperatorPool.ElementAt(StaticRandom.Instance.Next(0, OperatorPool.Length)).ToString());
					//}
					//while(term == 0 && operation.Operand == OperandType.Divide);
				}

				result.Add(new Tuple<decimal, TupleOperation>(term, operation));
				lastOperand = operation.Operand;
				Counter++;
			}

			return result;
		}

		private decimal Solve()
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
			return runningTotal;
		}

		public EquationResults GetResults()
		{
			return new EquationResults(this);
		}

		public override string ToString()
		{
			StringBuilder resultText = new StringBuilder();
			foreach (Tuple<decimal, TupleOperation> exp in Equation)
			{
				resultText.AppendFormat("{0} {1} ", exp.Item1, exp.Item2);
			}
			return resultText.Append(Evaluate()).ToString();
		}
	}
}
