/*
 *
 * Developed by Adam White
 *  https://csharpcodewhisperer.blogspot.com
 * 
 */
using System;
using System.Linq;
using System.Numerics;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace EquationFinderCore
{
	public class ThreadSpawnerArgs
	{
		public int TimeToLive { get; private set; }
		public int NumberOfThreads { get; private set; }
		public int NumberOfRounds { get; private set; }
		public FoundEquationDelegate FoundResultCallback { get; private set; }
		public IEquationFinderArgs EquationFinderArgs { get; private set; }
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
				FoundResultCallback = null;
			}
			else
			{
				FoundResultCallback = foundSolutionCallbackFunction;
			}

			// Thread settings
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
