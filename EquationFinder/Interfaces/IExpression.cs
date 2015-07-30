using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace EquationFinder
{
	public interface IExpression
	{
		decimal TargetValue { get; }
		decimal CalculatedValue { get; }
		string ToString();
		decimal Evaluate();
		ExpressionResults GetResults();
		IExpression NewExpression(IEquationFinderArgs equationArgs);
	}

	public interface IEquationFinderArgs
	{
		decimal TargetValue { get; }
		int NumberOfOperations { get; }
	}

	public class ExpressionResults
	{
		public string ExpressionText { get; private set; }
		public decimal TargetValue { get; private set; }
		public bool IsSolution { get; private set; }
		public decimal Result { get; private set; }

		public IExpression Expression { get; private set; }

		public ExpressionResults(decimal targetValue, IExpression expression)
		{
			this.Expression = expression; // Expression
			Result = expression.CalculatedValue; // decimal
			ExpressionText = expression.ToString(); // string

			TargetValue = targetValue; // decimal
			IsSolution = Result.Equals(targetValue); // bool
		}
	}

	public class EquationFinderArgs : IEquationFinderArgs
	{
		public decimal TargetValue { get; set; }
		public int NumberOfOperations { get; set; }

		public Func<decimal> TermSelector { get; set; }
		public Func<string> OperatorSelector { get; set; }
		//public string TermPool { get; set; }
		//public string OperatorPool { get; set; }

		public EquationFinderArgs(decimal targetValue, int numOperations, Func<decimal> termSelector, Func<string> operatorSelector)
		{
			if (termSelector == null)
			{
				throw new ArgumentException("termSelector may not be null.", "termSelector");
			}
			if (operatorSelector == null)
			{
				throw new ArgumentException("operatorSelector may not be null.", "operatorSelector");
			}
			if (numOperations < 1)
			{
				throw new ArgumentException("numOperations must be one or greater.", "numOperations");
			}

			TargetValue = targetValue;
			NumberOfOperations = numOperations;
			TermSelector = termSelector;
			OperatorSelector = operatorSelector;

			//TermPool = termPool;
			//OperatorPool = operatorPool;
			//TermSelector = new Func<decimal>(delegate { return StaticClass.String2Decimal(TermPool.ElementAt(StaticRandom.Instance.Next(0, TermPool.Length)).ToString()); });			
			//OperatorSelector = new Func<string>(delegate { return OperatorPool.ElementAt(StaticRandom.Instance.Next(0, OperatorPool.Length)).ToString(); });
		}
	}

	public class ExpressionFinderArgs : EquationFinderArgs, IEquationFinderArgs
	{
		public ExpressionFinderArgs(decimal targetValue, int numOperations, Func<decimal> termSelector, Func<string> operatorSelector, string termPool, string operatorPool)
			: base(targetValue, numOperations, termSelector, operatorSelector)
		{
			this.TermPool = termPool;
			this.OperatorPool = operatorPool;
		}

		public string TermPool { get; set; }
		public string OperatorPool { get; set; }
	}

	public class ThreadSpawnerArgs
	{
		public int TimeToLive { get; set; }
		public int NumberOfThreads { get; set; }
		public int NumberOfRounds { get; set; }
		public DisplayOutputDelegate DisplayOutputFunction { get; set; }

		public IEquationFinderArgs EquationFinderArgs { get; set; }

		public ThreadSpawnerArgs()
		{
		}

		public ThreadSpawnerArgs(DisplayOutputDelegate displayOutputFunction, int timeToLive, int numberOfThreads, int numberOfRounds, IEquationFinderArgs finderArgs)
		{
			// Thread settings
			DisplayOutputFunction = displayOutputFunction;
			TimeToLive = timeToLive;
			NumberOfThreads = numberOfThreads;
			NumberOfRounds = numberOfRounds;
			
			// Equation settings
			EquationFinderArgs = finderArgs;
		}
	}	
}
