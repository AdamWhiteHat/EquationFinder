/*
 *
 * Developed by Adam Rakaska
 *  http://www.csharpprogramming.tips
 * 
 */
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using EquationFinderCore;

namespace EquationFinder
{
	public delegate List<EquationResults> EquationThreadManagerDelegate(ThreadSpawnerArgs threadArgs);

	public class ThreadedEquationFinder<T> where T : class, IEquation, new()
	{
		//		
		public List<string> Results { get; set; }
		public long TotalEquationsGenerated { get; private set; }
		public bool CancellationPending { get; private set; }
		// Read only
		public int NumberOfRounds { get { return threadSpawnerArgs.NumberOfRounds; } }
		public FoundEquationDelegate FoundSolutionCallback { get { return threadSpawnerArgs.FoundResultCallback; } }
		public static string ExpirationMessage = "Time-to-live expired.";

		ThreadSpawnerArgs threadSpawnerArgs { get; set; }

		public ThreadedEquationFinder()
		{ }

		public ThreadedEquationFinder(ThreadSpawnerArgs spawnerArgs)
		{
			Results = new List<string>();
			threadSpawnerArgs = spawnerArgs;
			TotalEquationsGenerated = 0;
		}

		public void Cancel()
		{
			CancellationPending = true;
		}

		private List<EquationResults> ThreadFunction_FindSatisfiableEquations(ThreadSpawnerArgs threadArgs)
		{
			List<EquationResults> results = new List<EquationResults>();
			try
			{
				int maxMilliseconds = (threadArgs.TimeToLive * 1000);
				Stopwatch Age = new Stopwatch();
				Age.Start();

				IEquation currentEquation = (IEquation)new T();
				currentEquation.SetArgs(threadArgs.EquationFinderArgs);

				int maxResults = 300;
				while (Age.ElapsedMilliseconds < maxMilliseconds)
				{
					currentEquation.GenerateNewAndEvaluate();
					TotalEquationsGenerated += 1;

					if (currentEquation.IsSolution) //if (currentEquation.Result == (decimal)threadArgs.EquationFinderArgs.TargetValue)
					{
						string equationString = currentEquation.ToString();
						if (string.IsNullOrWhiteSpace(equationString))
						{
							continue; 
						}
						if (threadArgs.FoundSolutions.Contains(equationString) == false)
						{
							results.Add(new EquationResults(equationString, threadArgs.EquationFinderArgs.TargetValue, currentEquation.Result));
							//results.Add(new EquationResults(equationString, currentEquation.Result, currentEquation.Result));
							threadArgs.FoundSolutions.Add(equationString);

							if (--maxResults < 1)
							{
								break;
							}
						}
					}
				} // End while

				Age.Stop();
				Age = null;
			}
			finally
			{
				if (results == null)
				{
					results = new List<EquationResults>();
				}
			}

			return results;
		}

		public void Run()
		{
			int ttlMiliseconds = (threadSpawnerArgs.TimeToLive * 1000);
			List<string> results = new List<string>();

			Stopwatch resultsTimer = new Stopwatch();
			resultsTimer.Start();

			for (int roundCounter = NumberOfRounds; roundCounter > 0; roundCounter--)
			{
				if (CancellationPending)
				{
					return;
				}
				if (resultsTimer.ElapsedMilliseconds > ttlMiliseconds)
				{
					return;
				}

				results = ThreadSpawner(threadSpawnerArgs, ThreadFunction_FindSatisfiableEquations);
				ReportSolution(string.Join(Environment.NewLine, results.ToArray()));
			}

			resultsTimer.Stop();
			resultsTimer = null;
			results = null;
		}

		public static List<string> ThreadSpawner(ThreadSpawnerArgs threadArgs, EquationThreadManagerDelegate equationManager)
		{
			List<string> strResults = new List<string>();
			try
			{
				List<EquationThreadManagerDelegate> threadDelegateList = new List<EquationThreadManagerDelegate>();
				List<IAsyncResult> threadHandletList = new List<IAsyncResult>();
				List<EquationResults> threadResultList = new List<EquationResults>();

				// Make list of delegates that will become threads
				threadDelegateList.AddRange(
						Enumerable.Repeat(equationManager, threadArgs.NumberOfThreads)
					);

				// Invoke all the threads
				foreach (EquationThreadManagerDelegate thread in threadDelegateList)
				{
					threadHandletList.Add(thread.BeginInvoke(threadArgs, null, null));
				}

				// Await the result of each thread 
				int counter = 0;
				foreach (var thread in threadDelegateList)
				{
					threadResultList.AddRange(thread.EndInvoke(threadHandletList[counter]));
					counter++;
				}

				// Free up some resources
				threadHandletList.RemoveAll(d => true);
				threadHandletList = null;
				threadDelegateList.RemoveAll(d => true);
				threadDelegateList = null;


				// Format the results as a List of strings			
				foreach (EquationResults item in threadResultList)
				{
					if (item.IsSolution)
					{
						strResults.Add(item.EquationText);
					}
					else
					{
						strResults.Add(ExpirationMessage);
					}
				}

				// Free up some resources
				threadResultList.RemoveAll(r => true);
				threadResultList = null;
			}
			finally
			{
				if (strResults == null)
				{
					strResults = new List<string>();
				}
			}

			return strResults;
		}

		private void ReportSolution(string results)
		{
			if (FoundSolutionCallback != null)
			{
				FoundSolutionCallback.Invoke(results);
			}
		}

	} // ThreadedEquationFinder
}
