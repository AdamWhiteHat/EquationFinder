using System;
using System.Collections.Generic;
using System.Diagnostics;
//using System.Linq;
//using System.Text;

namespace EquationFinder
{
	public delegate void DisplayOutputDelegate(string FormatMessage, params object[] FormatArgs);
	public delegate ExpressionResults ExpressionThreadManagerDelegate(ThreadSpawnerArgs threadArgs);

	public class ThreadedEquationFinder
	{		
		public List<string> Results { get; set; }
		public long TotalExpressionsGenerated { get; private set; }

		ThreadSpawnerArgs FinderThreadArgs { get; set; }
		public int NumberOfRounds { get; set; }

		public DisplayOutputDelegate DisplayOuputFunction { get; set; }
		public static string ExpirationMessage = "Time-to-live expired.";

		public ThreadedEquationFinder(/*string[] previouslyFoundResults,*/ ThreadSpawnerArgs threadArgs)
		{
			Results = new List<string>();
			FinderThreadArgs = threadArgs;
			//if (previouslyFoundResults != null || previouslyFoundResults.Length > 0)
			//	Results.AddRange(previouslyFoundResults);
			DisplayOuputFunction = threadArgs.DisplayOutputFunction;
			NumberOfRounds = threadArgs.NumberOfRounds;
		}

		private void Print(string FormatMessage, params object[] FormatArgs)
		{
			if(DisplayOuputFunction != null)
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
						// Includes expiration message.
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

		public List<string> FindSolutionThread(ThreadSpawnerArgs threadArgs, ExpressionThreadManagerDelegate expressionManager)
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

			foreach (var thread in threadDelegateList)
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

		public ExpressionResults StringExpressionSpawnerFunc(ThreadSpawnerArgs threadArgs)
		{
			IExpression expression = new AlgebraicString(threadArgs.EquationFinderArgs);
			expression.
			TotalExpressionsGenerated++;

			int maxAge = (threadArgs.TimeToLive * 1000);
			Stopwatch Age = new Stopwatch();
			Age.Start();

			while (expression.Evaluate() != (decimal)threadArgs.EquationFinderArgs.TargetValue && Age.ElapsedMilliseconds < maxAge)
			{
				expression = new IExpression(threadArgs.EquationFinderArgs);
				TotalExpressionsGenerated++;
			}

			Age.Stop();
			Age = null;
			return expression.GetResults();
		}
	}
}
