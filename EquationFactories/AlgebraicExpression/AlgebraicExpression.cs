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
		public decimal Result { get { return _builder.Result; } }
		public string Equation { get { return _builder.ToString(); } }
		public bool IsSolution { get; private set; }

		private ExpressionBuilder<decimal> _builder = null;
		private IEquationFinderArgs _equationArgs = null;

		public AlgebraicExpression()
		{
		}

		public AlgebraicExpression(IEquationFinderArgs args)
		{
			GenerateNewAndEvaluate(args);
		}		

		public void GenerateNewAndEvaluate(IEquationFinderArgs args)
		{
			if (args == null)
			{
				throw new ArgumentNullException("args");
			}
			if (_equationArgs == null)
			{				
				_equationArgs = args;
			}




			_builder = new ExpressionBuilder<decimal>(args);


			IsSolution = (Result == _equationArgs.TargetValue);
		}
		
		public EquationResults GetResults()
		{
			if (_equationArgs != null)
			{
				return new EquationResults(Equation, _equationArgs.TargetValue, Result, IsSolution);
			}
			throw new Exception("Private property 'equationArgs' should never be null after initialization.");
		}

		public override string ToString()
		{
			return _builder.ToString();
		}
    }
}
