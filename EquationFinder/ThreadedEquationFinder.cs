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
		public List<string> Results { get; set; }
		ThreadSpawnerArgs threadSpawnerArgs { get; set; }
		public long TotalEquationsGenerated { get; private set; }

		// Read only
		public int NumberOfRounds { get { return threadSpawnerArgs.NumberOfRounds; } }
		public FoundSolutionDelegate FoundSolutionCallback { get { return threadSpawnerArgs.FoundResultCallback; } }
		public static string ExpirationMessage = "Time-to-live expired.";

		public void Dispose()
		{
			threadSpawnerArgs.Dispose();
			threadSpawnerArgs = null;
			Results = null;
		}

		public ThreadedEquationFinder(ThreadSpawnerArgs spawnerArgs)
		{
			Results = new List<string>();
			threadSpawnerArgs = spawnerArgs;
			TotalEquationsGenerated = 0;
		}

		private List<EquationResults> ThreadFunction_FindSatisfiableEquations(ThreadSpawnerArgs threadArgs)
		{
			List<EquationResults> foundSolutions = new List<EquationResults>();
			try
			{
				int maxMilliseconds = (threadArgs.TimeToLive * 1000);
				Stopwatch Age = new Stopwatch();
				Age.Start();

				int maxResults = 3;
				while (Age.ElapsedMilliseconds < maxMilliseconds)
				{
					IEquation currentEquation = (IEquation)new T();
					currentEquation.Initialize((EquationFinderArgs)threadArgs.EquationFinderArgs);
					TotalEquationsGenerated += 1;
					if (currentEquation.Evaluate() == (decimal)threadArgs.EquationFinderArgs.TargetValue)
					{
						string equationString = currentEquation.ToString();
						if (!string.IsNullOrWhiteSpace(equationString))
						{
							if (threadArgs.FoundSolutions.Contains(equationString) == false)
							{
								EquationResults solution = currentEquation.GetResults();
								ReportSolution(solution);
								foundSolutions.Add(solution);
								if (--maxResults < 1)
								{
									break;
								}
							}
						}
						equationString = null;
					}

					currentEquation = null;
				}
				
				Age.Stop();
				Age = null;
			}
			finally
			{
				if (foundSolutions == null)
				{
					foundSolutions = new List<EquationResults>();
				}
			}

			return foundSolutions;
		}

		public void Run()
		{
			int expiredThreadCount = 0;
			bool showExpiredMessage = false;
			int ttlMiliseconds = (threadSpawnerArgs.TimeToLive * 1000);
			List<string> results = new List<string>();

			Stopwatch resultsTimer = new Stopwatch();
			resultsTimer.Start();

			for (int roundCounter = NumberOfRounds; roundCounter > 0; roundCounter--)
			{
				if (resultsTimer.ElapsedMilliseconds > ttlMiliseconds)
				{
					string timeoutMessage = "Round Time To Live expired before new results.";
					Results.Add(timeoutMessage);
					return;
				}

				results = ThreadSpawner(threadSpawnerArgs, ThreadFunction_FindSatisfiableEquations);

				foreach (string s in results)
				{
					if (s.Contains("expired"))
					{
						showExpiredMessage = true;
						expiredThreadCount++;
					}
					else if (!Results.Contains(s))
					{
						Results.Add(s);
						resultsTimer.Reset();
					}
				}

				if (showExpiredMessage)
				{
					if (Results.Count > 0)
					{
						// Prevents more than one message
						//if (Results.Contains(expirationMessage))
						//{
						//	Results.RemoveAll(line => line.Contains(expirationMessage));
						//}
					}

					string expiredMessage = string.Format("{0} ({1} threads)", ExpirationMessage, expiredThreadCount);
					Results.Add(expiredMessage);
				}
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

				// Add Delegates
				int counter = threadArgs.NumberOfThreads;
				while (counter > 0)
				{
					threadDelegateList.Add(equationManager);
					counter--;
				}

				foreach (EquationThreadManagerDelegate thread in threadDelegateList)
				{
					threadHandletList.Add(thread.BeginInvoke(threadArgs, null, null));
				}

				counter = 0;
				foreach (var thread in threadDelegateList)
				{
					threadResultList.AddRange(thread.EndInvoke(threadHandletList[counter]));
					counter++;
				}

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

		private void ReportSolution(EquationResults results)
		{
			if (FoundSolutionCallback != null)
			{
				FoundSolutionCallback.Invoke(results);
			}
		}

	} // ThreadedEquationFinder
}
