using System;
using System.Collections.Generic;
using System.Diagnostics;
//using System.Linq;
//using System.Text;

namespace EquationFinder
{
	public delegate void DisplayOutputDelegate(string FormatMessage, params object[] FormatArgs);
	public delegate ExpressionResults ExpressionThreadManagerDelegate(ThreadSpawnerArgs threadArgs);

	public class ThreadedEquationFinder<T> where T : class, IExpression, new()
	{
		ThreadSpawnerArgs FinderThreadArgs { get; set; }

		public List<string> Results { get; set; }
		public long TotalExpressionsGenerated { get; private set; }

		// Read only
		public int NumberOfRounds { get { return FinderThreadArgs.NumberOfRounds; } }
		public DisplayOutputDelegate DisplayOuputFunction { get { return FinderThreadArgs.DisplayOutputFunction; } }
		public static string ExpirationMessage = "Time-to-live expired.";

		public ThreadedEquationFinder(/*string[] previouslyFoundResults,*/ ThreadSpawnerArgs threadArgs)
		{
			Results = new List<string>();
			FinderThreadArgs = threadArgs;
			//if (previouslyFoundResults != null || previouslyFoundResults.Length > 0)
			//	Results.AddRange(previouslyFoundResults);
		}

		public static ExpressionResults ThreadManager(ThreadSpawnerArgs threadArgs)
		{
			long TotalExpressionsGenerated = 0;
			IExpression expression = (IExpression)new T();

			int maxMilliseconds = (threadArgs.TimeToLive * 1000);
			Stopwatch Age = new Stopwatch();
			Age.Start();

			bool foundSolution = false;
			while (Age.ElapsedMilliseconds < maxMilliseconds)
			{
				foundSolution = false;
				expression = expression.NewExpression((EquationFinderArgs)threadArgs.EquationFinderArgs);
				TotalExpressionsGenerated++;
				if (expression.Evaluate() == (decimal)threadArgs.EquationFinderArgs.TargetValue)
				{
					string foundExpression = expression.ToString();
					if (string.IsNullOrWhiteSpace(foundExpression))
					{
						continue;
					}

					if (threadArgs.PreviouslyFoundResultsCollection == null || threadArgs.PreviouslyFoundResultsCollection.Count < 1)
					{
						foundSolution = true;
						break;
					}
					else if (!threadArgs.PreviouslyFoundResultsCollection.Contains(foundExpression))
					{
						foundSolution = true;
						break;
					}
				}
			}

			Age.Stop();
			Age = null;
			if (expression != null && foundSolution == true)
			{
				return expression.GetResults();
			}
			else
			{
				return ExpressionResults.Empty;
			}
		}

		private void Print(string FormatMessage, params object[] FormatArgs)
		{
			if (DisplayOuputFunction != null)
			{
				DisplayOuputFunction.Invoke(FormatMessage, FormatArgs);
			}
		}

		public void Run(ExpressionThreadManagerDelegate expressionManager)
		{
			int expiredThreadCount = 0;
			bool showExpiredMessage = false;
			int ttlMiliseconds = (FinderThreadArgs.TimeToLive * 1000);
			List<string> results = new List<string>();

			Stopwatch resultsTimer = new Stopwatch();
			resultsTimer.Start();

			for (int roundCounter = NumberOfRounds; roundCounter > 0; roundCounter--)
			{
				if (resultsTimer.ElapsedMilliseconds > ttlMiliseconds)
				{
					string timeoutMessage = "Round Time To Live expired before new results.";
					Results.Add(timeoutMessage);
					Print(timeoutMessage, Environment.NewLine);
					return;
				}

				results = FindSolutionThread(FinderThreadArgs, expressionManager);

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
						Print(s);
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
					Print(expiredMessage);
				}
			}

			resultsTimer.Stop();
			resultsTimer = null;
			results = null;
		}

		public static List<string> FindSolutionThread(ThreadSpawnerArgs threadArgs, ExpressionThreadManagerDelegate expressionManager)
		{
			List<ExpressionThreadManagerDelegate> threadDelegateList = new List<ExpressionThreadManagerDelegate>();
			List<IAsyncResult> threadHandletList = new List<IAsyncResult>();
			List<ExpressionResults> threadResultList = new List<ExpressionResults>();

			// Add Delegates
			int counter = threadArgs.NumberOfThreads;
			while (counter > 0)
			{
				threadDelegateList.Add(expressionManager);
				counter--;
			}

			foreach (ExpressionThreadManagerDelegate thread in threadDelegateList)
			{
				threadHandletList.Add(thread.BeginInvoke(threadArgs, null, null));
			}

			counter = 0;
			foreach (var thread in threadDelegateList)
			{
				threadResultList.Add(thread.EndInvoke(threadHandletList[counter]));
				counter++;
			}

			// Format the results as a List of strings
			List<string> strResults = new List<string>();
			foreach (ExpressionResults item in threadResultList)
			{
				if (item.IsSolution)
				{
					strResults.Add(item.ExpressionText);
				}
				else
				{
					strResults.Add(ExpirationMessage);
				}
			}
			return strResults;
		}

		private void AsyncCompleteCallback(IAsyncResult threadResult)
		{

		}

	} // ThreadedEquationFinder
}
