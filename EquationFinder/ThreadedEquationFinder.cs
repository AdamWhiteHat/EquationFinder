/*
 *
 * Developed by Adam White
 *  https://csharpcodewhisperer.blogspot.com
 * 
 */
using System;
using System.Linq;
using System.Collections.Generic;
using EquationFinderCore;

namespace EquationFinder
{
	public delegate List<EquationResults> EquationThreadManagerDelegate(ThreadState threadState);

	public class ThreadedEquationFinder<TEquation> where TEquation : class, IEquation, new()
	{
		// Public properties
		public List<string> Results { get; set; }
		public long TotalEquationsGenerated { get; private set; }
		// Read only
		public int NumberOfRounds { get { return threadSpawnerArgs.NumberOfRounds; } }
		public FoundEquationDelegate FoundSolutionCallback { get { return threadSpawnerArgs.FoundResultCallback; } }
		public static string ExpirationMessage = "Time-to-live expired.";
		// Private
		private ThreadSpawnerArgs threadSpawnerArgs { get; set; }
		private ThreadState threadState;

		private ThreadedEquationFinder()
		{ }

		public ThreadedEquationFinder(ThreadSpawnerArgs spawnerArgs)
		{
			Results = new List<string>();
			threadSpawnerArgs = spawnerArgs;
			TotalEquationsGenerated = 0;
		}

		public void Cancel()
		{
			if (!threadState.CancelToken.IsCancellationRequested)
			{
				threadState.CancelToken.Cancel();
			}
		}

		public void Run()
		{
			if (threadState != null && threadState.TimeToStop != null && DateTime.Now < threadState.TimeToStop)
			{
				return;
			}

			List<string> results = new List<string>();
			DateTime timeToStop = DateTime.Now.AddSeconds(threadSpawnerArgs.TimeToLive);
			threadState = new ThreadState(threadSpawnerArgs, timeToStop);

			for (int roundCounter = NumberOfRounds; roundCounter > 0; roundCounter--)
			{
				if (threadState.CancelToken.IsCancellationRequested || DateTime.Now > timeToStop)
				{
					break;
				}

				// Im leaving this in for these reasons:
				//  1) For the Console application to work without modification.
				//  2) Testing, time trials between the two methods
				//  3) If you want to see EVERY equation generated, you will likely want to return data this way
				//  4) If this code is extended to span more than one computer, results will need to be aggregated in a more controlled fashion
				results = ThreadSpawner(threadSpawnerArgs, ThreadFunction_FindSatisfiableEquations);
				ReportSolution(string.Join(Environment.NewLine, results.ToArray()));
			}

			threadState = null;
			results = null;
		}

		public List<string> ThreadSpawner(ThreadSpawnerArgs threadArgs, EquationThreadManagerDelegate equationManager)
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
					threadHandletList.Add(thread.BeginInvoke(threadState, null/*new AsyncCallback(OnThreadCompleted)*/, null));
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
				threadDelegateList.RemoveAll(d => true);
				threadHandletList = null;
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

		private List<EquationResults> ThreadFunction_FindSatisfiableEquations(ThreadState threadState)
		{
			List<EquationResults> results = new List<EquationResults>();
			try
			{
				IEquation currentEquation = (IEquation)new TEquation();
				DateTime timeToStop = new DateTime(threadState.TimeToStop.Ticks);

				while (!threadState.CancelToken.IsCancellationRequested && DateTime.Now < timeToStop)
				{
					currentEquation.GenerateNewAndEvaluate(threadState.ThreadArgs.EquationFinderArgs);
					TotalEquationsGenerated += 1;

					if (!currentEquation.IsSolution)
					{
						continue;
					}

					string equationString = currentEquation.ToString();
					if (string.IsNullOrWhiteSpace(equationString))
					{
						continue;
					}
					if (threadState.ThreadArgs.FoundSolutions.Contains(equationString) == false)
					{
						results.Add(new EquationResults(equationString, threadState.ThreadArgs.EquationFinderArgs.TargetValue, currentEquation.Result, currentEquation.IsSolution));
						threadState.ThreadArgs.FoundSolutions.Add(equationString);
					}
				} // End while
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

		private void ReportSolution(string results)
		{
			if (FoundSolutionCallback != null)
			{
				FoundSolutionCallback.Invoke(results);
			}
		}

	} // ThreadedEquationFinder
}
