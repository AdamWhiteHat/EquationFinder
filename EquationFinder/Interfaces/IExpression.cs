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
		public IExpression xpression { get; private set; }
		public static ExpressionResults Empty = new ExpressionResults(decimal.MinValue, null);
		
		public decimal TargetValue { get; private set; }
		public string ExpressionText { get { return (xpression==null) ? "" : xpression.ToString(); } }
		public decimal Result { get { return (xpression == null) ? 0 : xpression.CalculatedValue; } }
		public bool IsSolution { get { return Result.Equals(TargetValue); } }

		public ExpressionResults(decimal targetValue, IExpression expression)
		{
			xpression = expression; // Expression
			TargetValue = targetValue; // decimal
		}
	}

	public class EquationFinderArgs : IEquationFinderArgs
	{
		public decimal TargetValue { get; set; }
		public int NumberOfOperations { get; set; }
		public string TermPool { get; set; }
		public string OperatorPool { get; set; }

		public EquationFinderArgs(decimal targetValue, int numOperations, string termPool, string operatorPool)
		{
			if (string.IsNullOrWhiteSpace(termPool))
			{
				throw new ArgumentException("termPool may not be empty or null.", "termPool");
			}
			if (string.IsNullOrWhiteSpace(operatorPool))
			{
				throw new ArgumentException("operatorPool may not be empty or null.", "operatorPool");
			}
			if (numOperations < 1)
			{
				throw new ArgumentException("numOperations must be one or greater.", "numOperations");
			}

			TargetValue = targetValue;
			NumberOfOperations = numOperations;
			
			TermPool = termPool;
			OperatorPool = operatorPool;

			//TermSelector = new Func<decimal>(delegate { return StaticClass.String2Decimal(TermPool.ElementAt(StaticRandom.Instance.Next(0, TermPool.Length)).ToString()); });			
			//OperatorSelector = new Func<string>(delegate { return OperatorPool.ElementAt(StaticRandom.Instance.Next(0, OperatorPool.Length)).ToString(); });
		}
	}

	public class ThreadSpawnerArgs
	{
		public int TimeToLive { get; set; }
		public int NumberOfThreads { get; set; }
		public int NumberOfRounds { get; set; }
		public DisplayOutputDelegate DisplayOutputFunction { get; set; }
		public List<string> PreviouslyFoundResultsCollection { get; set; }
		
		public IEquationFinderArgs EquationFinderArgs { get; set; }

		public ThreadSpawnerArgs()
		{
		}

		public ThreadSpawnerArgs(DisplayOutputDelegate displayOutputFunction, int timeToLive, int numberOfThreads, int numberOfRounds, IEquationFinderArgs finderArgs)
		{
			PreviouslyFoundResultsCollection = new List<string>();

			if (finderArgs == null)
			{
				throw new ArgumentNullException("FinderArgs cannot be null.", "finderArgs");
			}
			if (timeToLive <= 0)
			{
				throw new ArgumentException("TimeToLive must be greater than zero.", "timeToLive");
			}
			if (numberOfThreads < 1)
			{
				throw new ArgumentException("NumberOfThreads must be at least one.", "numberOfThreads");
			}
			if (numberOfRounds < 1)
			{
				throw new ArgumentException("NumberOfRounds must be at least one.", "numberOfRounds");
			}
			if (displayOutputFunction == null)
			{
				displayOutputFunction = Console.WriteLine;
			}

			// Thread settings
			DisplayOutputFunction = displayOutputFunction;
			TimeToLive = timeToLive;
			NumberOfThreads = numberOfThreads;
			NumberOfRounds = numberOfRounds;

			// Equation settings
			EquationFinderArgs = finderArgs;
		}

		public ThreadSpawnerArgs(List<string> previouslyFoundResults, DisplayOutputDelegate displayOutputFunction, int timeToLive, int numberOfThreads, int numberOfRounds, IEquationFinderArgs finderArgs)
			: this(displayOutputFunction, timeToLive, numberOfThreads, numberOfRounds, finderArgs)
		{
			if (previouslyFoundResults != null && previouslyFoundResults.Count > 0)
			{
				PreviouslyFoundResultsCollection = previouslyFoundResults;
			}			
		}
	}	
}
