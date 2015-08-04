﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationFinder
{
	public partial class AlgebraicTuple : IExpression
	{
		public static ExpressionResults TupleExpressionThreadManager(ThreadSpawnerArgs threadArgs)
		{
			long TotalExpressionsGenerated = 0;
			AlgebraicTuple expression = new AlgebraicTuple((EquationFinderArgs)threadArgs.EquationFinderArgs);
			TotalExpressionsGenerated++;

			int maxAge = (threadArgs.TimeToLive * 1000);
			Stopwatch Age = new Stopwatch();
			Age.Start();

			while ((expression.Evaluate() != (decimal)threadArgs.EquationFinderArgs.TargetValue) && (Age.ElapsedMilliseconds < maxAge))
			{
				expression = new AlgebraicTuple((EquationFinderArgs)threadArgs.EquationFinderArgs);
				TotalExpressionsGenerated++;
			}

			Age.Stop();
			Age = null;
			return expression.GetResults();
		}
	}
}