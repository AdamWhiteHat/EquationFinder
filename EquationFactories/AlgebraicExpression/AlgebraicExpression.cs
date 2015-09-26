using System;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;

using EquationFinderCore;

namespace EquationFactories
{
	public class AlgebraicExpression : IEquation
    {
		public decimal TargetValue { get; private set; }
		public EquationFinderArgs EquationArgs { get; set; }

		public void Initialize(EquationFinderArgs equationArgs)
		{ 
		}

		public decimal Evaluate()
		{
			return 0;
		}

		public EquationResults GetResults()
		{
			return new EquationResults(this);
		}
		

		public override string ToString()
		{
			return base.ToString();
		}
    }
}
