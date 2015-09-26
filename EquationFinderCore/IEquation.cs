using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace EquationFinderCore
{
	public delegate void FoundSolutionDelegate(EquationResults resultsObject);

	public interface IEquation : IDisposable
	{ 
		string ToString();
		decimal Evaluate();
		decimal TargetValue { get; }		
		EquationResults GetResults();
		EquationFinderArgs EquationArgs { get; set; }
		void Initialize(EquationFinderArgs equationArgs);
	}

	public interface IEquationFinderArgs : IDisposable
	{
		decimal TargetValue { get; }
		int NumberOfOperations { get; }
	}

	public class EquationResults
	{
		public decimal TargetValue { get; private set; }
		public string EquationText { get; private set; }
		public decimal Result { get; private set; }
		public bool IsSolution { get; private set; }

		public EquationResults(IEquation equation)
		{
			EquationText = equation.ToString();
			TargetValue = equation.TargetValue;
			Result = equation.Evaluate();
			IsSolution = Result.Equals(TargetValue);
		}
	}

	public class EquationFinderArgs : IEquationFinderArgs
	{
		public decimal TargetValue { get; set; }
		public int NumberOfOperations { get; set; }
		public string TermPool { get; set; }
		public string OperatorPool { get; set; }

		public void Dispose()
		{
			TermPool = null;
			OperatorPool = null;
		}

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
		}
	}

	public class ThreadSpawnerArgs : IDisposable
	{
		public int TimeToLive { get; set; }
		public int NumberOfThreads { get; set; }
		public int NumberOfRounds { get; set; }
		public FoundSolutionDelegate FoundResultCallback { get; set; }			
		public IEquationFinderArgs EquationFinderArgs { get; set; }
		public BlockingCollection<string> FoundSolutions { get; set; }
		//public List<string> PreviouslyFoundResultsCollection { get; set; }	

		public void Dispose()
		{
			FoundSolutions.Dispose();
			FoundSolutions = null;
		
			EquationFinderArgs.Dispose();
			EquationFinderArgs = null; 
			
			FoundResultCallback = null;			
		}

		public ThreadSpawnerArgs()
		{
			//PreviouslyFoundResultsCollection = new List<string>();
			FoundSolutions = new BlockingCollection<string>();
		}

		public ThreadSpawnerArgs(FoundSolutionDelegate foundSolutionCallbackFunction, int timeToLive, int numberOfThreads, int numberOfRounds, IEquationFinderArgs finderArgs)
			: this()
		{
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
			if (foundSolutionCallbackFunction == null)
			{
				foundSolutionCallbackFunction = (EquationResults e) => { };
			}

			// Thread settings
			FoundResultCallback = foundSolutionCallbackFunction;
			TimeToLive = timeToLive;
			NumberOfThreads = numberOfThreads;
			NumberOfRounds = numberOfRounds;

			// Equation settings
			EquationFinderArgs = finderArgs;
		}

		public ThreadSpawnerArgs(List<string> previouslyFoundSolutons, FoundSolutionDelegate displayOutputFunction, int timeToLive, int numberOfThreads, int numberOfRounds, IEquationFinderArgs finderArgs)
			: this(displayOutputFunction, timeToLive, numberOfThreads, numberOfRounds, finderArgs)
		{
			if (previouslyFoundSolutons != null && previouslyFoundSolutons.Count > 0)
			{
				foreach(string prevSolution in previouslyFoundSolutons)
				{
					if (!string.IsNullOrWhiteSpace(prevSolution))
					{						
						FoundSolutions.Add(prevSolution);
					}
				}				
			}
			
			
		}
	}	
}
