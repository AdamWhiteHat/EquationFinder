using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace EquationFinder
{
	//public class ThreadManager
	//{
	//	public static ExpressionResults ExpressionThreadManager(ThreadSpawnerArgs threadArgs)
	//	{
	//		long TotalExpressionsGenerated = 0;
	//		IExpression expression = null;
	//
	//		int maxMilliseconds = (threadArgs.TimeToLive * 1000);
	//		Stopwatch Age = new Stopwatch();
	//		Age.Start();
	//
	//		while (Age.ElapsedMilliseconds < maxMilliseconds)
	//		{
	//			expression = expressionClass.NewExpression((EquationFinderArgs)threadArgs.EquationFinderArgs);
	//			TotalExpressionsGenerated++;
	//			if (expression.Evaluate() == (decimal)threadArgs.EquationFinderArgs.TargetValue)
	//			{
	//				string foundExpression = expression.ToString();
	//				if (!threadArgs.PreviouslyFoundResultsCollection.Contains(foundExpression))
	//				{
	//					break;
	//				}
	//			}
	//		}
	//
	//		Age.Stop();
	//		Age = null;
	//		if (expression != null)
	//		{
	//			return expression.GetResults();
	//		}
	//		return ExpressionResults.Empty;
	//	}
	//}	
}
