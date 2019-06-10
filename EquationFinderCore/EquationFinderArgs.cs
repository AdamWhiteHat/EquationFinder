using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace EquationFinderCore
{
	public class EquationFinderArgs : IEquationFinderArgs
	{
		public BigInteger TargetValue { get; private set; }
		public int NumberOfOperations { get; private set; }
		public List<int> TermPool { get; private set; }
		public string OperatorPool { get; private set; }
		public Random Rand { get; private set; }

		public EquationFinderArgs(BigInteger targetValue, int numOperations, List<int> termPool, string operatorPool)
		{
			if (termPool == null || termPool.Count < 1)
			{
				throw new ArgumentException("termPool may not be empty or null.", "termPool");
			}
			if (string.IsNullOrWhiteSpace(operatorPool))
			{
				throw new ArgumentException("operatorPool may not be empty or null.", "operatorPool");
			}
			if (numOperations < 1)
			{
				throw new ArgumentException("numOperations must be one or greater.", "numOperations");
			}

			Rand = StaticRandom.Factory.Random();

			TargetValue = targetValue;
			NumberOfOperations = numOperations;

			TermPool = termPool;
			OperatorPool = operatorPool;
		}
	}
}
