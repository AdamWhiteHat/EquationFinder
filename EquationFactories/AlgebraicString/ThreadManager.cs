using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationFinder
{
	//public partial class AlgebraicString : IExpression
	//{
	//	public ExpressionResults StringExpressionSpawnerFunc(ThreadSpawnerArgs threadArgs)
	//	{
	//		long TotalExpressionsGenerated = 0;
	//		AlgebraicString expression = new AlgebraicString((EquationFinderArgs)threadArgs.EquationFinderArgs);
	//		TotalExpressionsGenerated++;

	//		int maxAge = (threadArgs.TimeToLive * 1000);
	//		Stopwatch Age = new Stopwatch();
	//		Age.Start();

	//		while (Age.ElapsedMilliseconds < maxAge)
	//		{
	//			expression = new AlgebraicString((EquationFinderArgs)threadArgs.EquationFinderArgs);
	//			TotalExpressionsGenerated++;
	//			if(expression.Evaluate() == threadArgs.EquationFinderArgs.TargetValue)
	//			{
	//				string foundExpression = expression.ToString();
	//				if(!threadArgs.PreviouslyFoundResultsCollection.Contains(foundExpression))
	//				{
	//					break;
	//				}
	//			}
	//		}

	//		Age.Stop();
	//		Age = null;
	//		return expression.GetResults();
	//	}
	//}
}
