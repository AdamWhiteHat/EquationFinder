/*
 *
 * Developed by Adam White
 *  https://csharpcodewhisperer.blogspot.com
 * 
 */
using System;
using System.Text;
using System.Linq;
using System.Numerics;
using System.Globalization;
using System.Collections.Generic;
using EquationFinderCore;

namespace EquationFactories
{
	public partial class AlgebraicTuple : IEquation
	{
		public BigInteger Result
		{
			get
			{
				if (_result != null) { return (BigInteger)_result; }
				else { throw new Exception(); }
			}
		}
		private BigInteger? _result = null;

		public bool IsSolution
		{
			get { return _isSolution; }
		}
		private bool _isSolution = false;

		private List<Tuple<BigInteger, TupleOperation>> Equation { get; set; }

		private IEquationFinderArgs EquationArgs { get; set; }
		private BigInteger TargetValue { get { return EquationArgs.TargetValue; } }
		private int NumberOfOperations { get { return EquationArgs.NumberOfOperations; } }
		private List<int> TermPool { get { return EquationArgs.TermPool; } }

		private List<TupleOperation> IncreasingOperations { get; set; }
		private List<TupleOperation> DecreasingOperations { get; set; }


		public AlgebraicTuple()
		{
		}

		public void GenerateNewAndEvaluate(IEquationFinderArgs args)
		{
			_result = null;
			_isSolution = false;
			EquationArgs = args;

			IncreasingOperations = new List<TupleOperation>();
			DecreasingOperations = new List<TupleOperation>();

			foreach (char op in EquationArgs.OperatorPool)
			{
				TupleOperation tupleOperation = new TupleOperation(op);

				switch (tupleOperation.Operand)
				{
					case OperationType.Add:
					case OperationType.Multiply:
					case OperationType.Exponentiation:
						IncreasingOperations.Add(tupleOperation);
						break;

					case OperationType.Subtract:
					case OperationType.Divide:
						DecreasingOperations.Add(tupleOperation);
						break;
				}
			}

			BuildEquation();
		}

		private void BuildEquation()
		{
			if (_result != null)
			{
				return;
			}

			int termCount = TermPool.Count;
			int increasingOpCount = IncreasingOperations.Count;
			int decreasingOpCount = DecreasingOperations.Count;

			int counter = 1;
			BigInteger currentTerm = 0;
			BigInteger runningTotal = 0;
			TupleOperation currentOperation = new TupleOperation();
			TupleOperation lastOperation = new TupleOperation(OperationType.None);

			List<Tuple<BigInteger, TupleOperation>> result = new List<Tuple<BigInteger, TupleOperation>>();

			while (counter <= NumberOfOperations)
			{
				do
				{
					currentTerm = TermPool[EquationArgs.Rand.Next(0, termCount)];
				}
				while (lastOperation.Operand == OperationType.Divide && currentTerm == 0);


				if (lastOperation.Operand == OperationType.None)
				{
					runningTotal = currentTerm;
				}
				else
				{
					runningTotal = lastOperation.Calculate((BigInteger)runningTotal, currentTerm);
				}


				if (counter == NumberOfOperations)
				{
					currentOperation = new TupleOperation(OperationType.Equal);
				}
				else
				{
					switch (runningTotal.CompareTo(TargetValue))
					{
						case -1:

							currentOperation = IncreasingOperations.ElementAt(EquationArgs.Rand.Next(0, increasingOpCount));

							break;

						case 1:

							currentOperation = DecreasingOperations.ElementAt(EquationArgs.Rand.Next(0, decreasingOpCount));

							break;

						case 0:
							currentOperation = new TupleOperation(OperationType.Equal);
							result.Add(new Tuple<BigInteger, TupleOperation>(currentTerm, currentOperation));
							Equation = result;
							_result = runningTotal;

							if (EquationArgs.TargetValuePredicate == ResultPredicate.IsDivisibleBy)
							{
								_isSolution = (_result % TargetValue == 0);
							}
							else if (EquationArgs.TargetValuePredicate == ResultPredicate.IsEqualTo)
							{
								_isSolution = (_result == TargetValue);
							}

							return;
					}



					//


				}

				result.Add(new Tuple<BigInteger, TupleOperation>(currentTerm, currentOperation));

				lastOperation = currentOperation;
				counter++;
			}


			Equation = result;
			_result = runningTotal;

			if (EquationArgs.TargetValuePredicate == ResultPredicate.IsDivisibleBy)
			{
				_isSolution = (_result % TargetValue == 0);
			}
			else if (EquationArgs.TargetValuePredicate == ResultPredicate.IsEqualTo)
			{
				_isSolution = (_result == TargetValue);
			}
		}

		public override string ToString()
		{
			StringBuilder resultText = new StringBuilder();
			foreach (Tuple<BigInteger, TupleOperation> exp in Equation)
			{
				resultText.AppendFormat(CultureInfo.CurrentCulture, "{0:0.##} {1} ", exp.Item1, exp.Item2);
			}
			resultText.AppendFormat(CultureInfo.CurrentCulture, "{0:0.##}", Result);
			return resultText.ToString();
		}
	}
}
