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
//using System.Threading.Tasks;

namespace EquationFinder
{
	public partial class AlgebraicTuple : IExpression
	{
		public EquationFinderArgs EquationArgs { get; private set; }
		public List<Tuple<decimal, TupleOperation>> Expression { get; private set; }

		public decimal TargetValue { get { return EquationArgs.TargetValue; } }
		public int NumberOfOperations { get { return EquationArgs.NumberOfOperations; } }
		
		//public Func<string> OperatorSelector { get { return EquationArgs.OperatorSelector; } }
		//public Func<decimal> TermSelector { get { return EquationArgs.TermSelector; } }
		public string OperatorPool { get { return EquationArgs.OperatorPool; } }
		public string TermPool { get { return EquationArgs.TermPool; } }

		public decimal CalculatedValue
		{
			get { return Evaluate(); }
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

		public AlgebraicTuple()
		{
		}

		public AlgebraicTuple(EquationFinderArgs equationArgs)
		{
			EquationArgs = equationArgs;
			BuildExpression();
			Evaluate();
		}

		public IExpression NewExpression(IEquationFinderArgs equationArgs)
		{
			return new AlgebraicTuple((EquationFinderArgs)equationArgs);
		}

		protected void BuildExpression()
		{
			Expression = GenerateRandomExpression(NumberOfOperations);
		}

		public List<Tuple<decimal, TupleOperation>> GenerateRandomExpression(int numberOfOperations)
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

				if(Counter == numberOfOperations)
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

				result.Add(	new Tuple<decimal, TupleOperation>(term,operation) );
				lastOperand = operation.Operand;
				Counter++;
			}

			return result;
		}
		
		private decimal Solve()
		{
			TupleOperation lastOperation = new TupleOperation(OperandType.None);
			decimal runningTotal = 0;
			foreach (Tuple<decimal, TupleOperation> t in Expression)
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

		public ExpressionResults GetResults()
		{
			return new ExpressionResults(TargetValue, this);
		}

		public override string ToString()
		{
			StringBuilder resultText = new StringBuilder();
			foreach (Tuple<decimal, TupleOperation> exp in Expression)
			{
				resultText.AppendFormat("{0} {1} ", exp.Item1, exp.Item2);
			}
			return resultText.Append(CalculatedValue).ToString();
		}

		#region Depricated
		//
		//public Task<IExpression> NewTask(EquationFinderArgs equationArgs)
		//{
		//	return new Task<IExpression>(new Func<IExpression>(delegate { return new AlgebraicTuple(equationArgs); }));
		//}
		//
		//public TupleExpression(uint numOperations,decimal numberToUse)
		//{
		//	this.numberOperations =	(int)numOperations;
		//	this.maxNumericValue =	(int)numberToUse;
		//
		//	_expression = new List<Tuple<decimal,MathOperation>>();
		//
		//	int Counter = 1;
		//	while(Counter<=numOperations)
		//	{
		//		_expression.Add(
		//			new Tuple<decimal,MathOperation>(
		//				numberToUse,
		//				Counter==numOperations?new MathOperation(OperandType.Equal):new MathOperation()
		//			)
		//		);
		//		Counter++;
		//	}
		//	Evaluate();
		//}
		//
		////public TupleExpression(int numOperations,int maxInteger=9)
		////{
		////	this.numberOperations = numOperations;
		////	this.maxNumericValue = maxInteger;
		////	
		////	_expression = new List<Tuple<decimal,MathOperation>>();
		////	
		////	int Counter = 1;
		////	while(Counter<=numOperations)
		////	{
		////		_expression.Add(
		////			new Tuple<decimal,MathOperation>(
		////				StaticRandom.Instance.Next(1,this.maxNumericValue+1),
		////				Counter==numOperations?new MathOperation(OperandType.Equal):new MathOperation()
		////			)
		////		);
		////		Counter++;
		////	}
		////	Evaluate();
		////}
		////
		////
		////
		////
		////OperandType RandomOperation(int operationsBitmask)
		////{
		////				int operatorcount = 0;
		////				if((operationsBitmask & eOperation.Add)==eOperation.Add)
		////				{	operatorcount++;	}
		//// Bitwise AND Operation.Raise
		////	return (OperandType) (int)Math.Pow(2,(double)StaticRandom.Instance.Next(0,3));
		////}
		////
		////public TupleExpression(int numOperations,Func<int> numSelector,Func<MathOperation> operationSelector)
		////{
		////	this.numberOperations =	numOperations;
		////
		////	_expression = new List<Tuple<decimal,MathOperation>>();
		////	
		////	int Counter = 1;
		////	while(Counter<=numOperations)
		////	{
		////		_expression.Add(
		////			new Tuple<decimal,MathOperation>(
		////				(decimal)numSelector(),
		////				Counter==numOperations?new MathOperation(OperandType.Equal):operationSelector()
		////			)
		////		);
		////		Counter++;
		////	}
		////	Evaluate();
		////}
		#endregion
	}
}
