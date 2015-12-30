/*
 *
 * Developed by Adam Rakaska
 *  http://www.csharpprogramming.tips
 * 
 */
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquationFinderCore
{
	public class EquationResults
	{
		public decimal TargetValue { get; private set; }
		public string EquationText { get; private set; }
		public decimal Result { get; private set; }
		public bool IsSolution { get; private set; }

		public EquationResults(string equationText, decimal targetValue, decimal result, bool isSolution)
		{
			EquationText = equationText;
			TargetValue = targetValue;
			Result = result;
			IsSolution = isSolution;
		}
	}

	public class EquationFinderArgs : IEquationFinderArgs
	{
		public decimal TargetValue { get; set; }
		public int NumberOfOperations { get; set; }
		public List<int> TermPool { get; set; }
		public string OperatorPool { get; set; }
		public Random Rand { get; private set; }

		public EquationFinderArgs(decimal targetValue, int numOperations, List<int> termPool, string operatorPool)
		{
			if (termPool == null || termPool.Count < 1)
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

			Rand = StaticRandom.Factory.Random();

			TargetValue = targetValue;
			NumberOfOperations = numOperations;

			TermPool = termPool;
			OperatorPool = operatorPool;
		}
	}

	public class ThreadSpawnerArgs
	{
		public int TimeToLive { get; set; }
		public int NumberOfThreads { get; set; }
		public int NumberOfRounds { get; set; }
		public FoundEquationDelegate FoundResultCallback { get; set; }
		public IEquationFinderArgs EquationFinderArgs { get; set; }
		public BlockingCollection<string> FoundSolutions { get; set; }

		public ThreadSpawnerArgs()
		{
			FoundSolutions = new BlockingCollection<string>();
		}

		public ThreadSpawnerArgs(FoundEquationDelegate foundSolutionCallbackFunction, int timeToLive, int numberOfThreads, int numberOfRounds, IEquationFinderArgs finderArgs)
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
				foundSolutionCallbackFunction = (string e) => { };
			}

			// Thread settings
			FoundResultCallback = foundSolutionCallbackFunction;
			TimeToLive = timeToLive;
			NumberOfThreads = numberOfThreads;
			NumberOfRounds = numberOfRounds;

			// Equation settings
			EquationFinderArgs = finderArgs;
		}

		public ThreadSpawnerArgs(List<string> previouslyFoundSolutons, FoundEquationDelegate displayOutputFunction, int timeToLive, int numberOfThreads, int numberOfRounds, IEquationFinderArgs finderArgs)
			: this(displayOutputFunction, timeToLive, numberOfThreads, numberOfRounds, finderArgs)
		{
			if (previouslyFoundSolutons != null && previouslyFoundSolutons.Count > 0)
			{
				foreach (string prevSolution in previouslyFoundSolutons)
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
