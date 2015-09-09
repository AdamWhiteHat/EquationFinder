using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationFinder
{
	//public partial class AlgebraicTuple : IExpression
	//{
	//	public static ExpressionResults TupleExpressionThreadManager(ThreadSpawnerArgs threadArgs)
	//	{
	//		long TotalExpressionsGenerated = 0;
	//		AlgebraicTuple expression = null;
			
	//		int maxMilliseconds = (threadArgs.TimeToLive * 1000);
	//		Stopwatch Age = new Stopwatch();
	//		Age.Start();

	//		while (Age.ElapsedMilliseconds < maxMilliseconds) 
	//		{				
	//			expression = new AlgebraicTuple((EquationFinderArgs)threadArgs.EquationFinderArgs);
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
