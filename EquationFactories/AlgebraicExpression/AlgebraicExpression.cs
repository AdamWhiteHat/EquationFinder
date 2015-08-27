using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;

namespace EquationFinder
{
	public partial class AlgebraicExpression : IExpression
	{
		//public Expression<Func<T, decimal>> Expression { get; private set; }
		public EquationFinderArgs EquationArgs { get; private set; }

		public decimal TargetValue { get { return EquationArgs.TargetValue; } }
		public int NumberOfOperations { get { return EquationArgs.NumberOfOperations; } }
		public string OperatorPool { get { return EquationArgs.OperatorPool; } }
		public string TermPool { get { return EquationArgs.TermPool; } }

		void test()
		{
			List<int> test = new List<int>();

			test.FindIndex(new Predicate<int>(delegate { return (bool)(1 == 1); }));
		}

		//public static Expression<Func<T, decimal>> Assign<T>(decimal dec) { return d => Convert.ToDecimal(dec); }

		//public static Expression<Func<T, decimal>> Zero<T>() { return d => 0; }
		//public static Expression<Func<T, decimal>> One<T>() { return d => 1; }
		//public static Expression<Func<T, decimal>> Two<T>() { return d => 2; }
		//public static Expression<Func<T, decimal>> Three<T>() { return d => 3; }
		//public static Expression<Func<T, decimal>> Four<T>() { return d => 4; }
		//public static Expression<Func<T, decimal>> Five<T>() { return d => 5; }
		//public static Expression<Func<T, decimal>> Six<T>() { return d => 6; }
		//public static Expression<Func<T, decimal>> Seven<T>() { return d => 7; }
		//public static Expression<Func<T, decimal>> Eight<T>() { return d => 8; }
		//public static Expression<Func<T, decimal>> Nine<T>() { return d => 9; }

		//public static Expression<Func<decimal, decimal, decimal>> AddDecimals<T>(/*decimal lhs, decimal rhs*/) { return (l, r) => l + r; }

		//public static Expression<Func<Expression, Expression, Expression>> MathExpression(Expression lhs, Expression rhs) { return (l, r) => l + r; }

		

		protected void BuildExpression()
		{
			string stringExpression = StaticClass.GenerateRandomStringExpression(NumberOfOperations, OperatorPool, TermPool);

			string[] strArr = stringExpression.Split(' ');

			if (strArr.Length < 1)
			{

			}
			
			

			//Func<decimal, decimal> fun = new Func<T, decimal>(delegate { return (decimal)0; });
			//Expression = new Expression<Func<T, decimal>>(); //GenerateRandomExpression<decimal>(EquationArgs.NumberOfOperations);
		}

		//public Expression<Func<T, decimal>> GenerateRandomExpression<T>(int numberOfOperations)
		//{
		//	return (Expression<Func<T, decimal>>)Expression..New(new Func<T, decimal>()();
		//}

		public decimal CalculatedValue
		{
			get { return Evaluate(); }
		}
		private decimal? _result = null;

		public bool IsCorrect
		{
			get { return (CalculatedValue == TargetValue); }
		}


		public AlgebraicExpression(EquationFinderArgs equationArgs)
		{
			EquationArgs = equationArgs;
			BuildExpression();
			Evaluate();
		}

		//public static decimal Evaluate_9Div3()
		//{
		//	Expression<Func<decimal, decimal>> expresso = Build9Div3Expression();			
		//	decimal result = expresso.Compile().Invoke(0);
		//	return result;
		//}
		//public static Expression<Func<decimal, decimal>> Build9Div3Expression()
		//{
		//	return Nine<decimal>().Divide(One<decimal>().Times(Three<decimal>()));
		//}

		public IExpression NewExpression(IEquationFinderArgs equationArgs)
		{
			return new AlgebraicExpression((EquationFinderArgs)equationArgs);
		}

		

		public decimal Evaluate()
		{
			if (_result == null)
			{
				_result = new Nullable<decimal>(Solve());
			}
			return (decimal)_result;
		}

		public static decimal Solve()
		{
			return 0;// Exp.Compile().Invoke<Expression<Func<T, decimal>>>(new T());
		}

		public ExpressionResults GetResults()
		{
			return new ExpressionResults(TargetValue, this);
		}

		public override string ToString()
		{
			return string.Format("{0} ({1}) = \"{2}\"", this.GetType().Name, this.GetHashCode().ToString(), this.CalculatedValue.ToString());
		}
		

		
		
	}
	
}
