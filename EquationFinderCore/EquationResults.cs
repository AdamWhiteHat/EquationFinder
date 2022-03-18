using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace EquationFinderCore
{
	public class EquationResults
	{
		public BigInteger TargetValue { get; private set; }
		public string EquationText { get; private set; }
		public BigInteger Result { get; private set; }
		public bool IsSolution { get; private set; }

		public EquationResults(string equationText, BigInteger targetValue, BigInteger result, bool isSolution)
		{
			EquationText = equationText;
			TargetValue = targetValue;
			Result = result;
			IsSolution = isSolution;
		}
	}
}
