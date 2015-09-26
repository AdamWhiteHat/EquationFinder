using System;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace EquationFactories
{
	public static class ExpressionComposer<T> where T : LambdaExpression
	{
		//Func<In,Out>

		public static Expression<Func<int, T>> LiteralValue = (f) => { return (T)(Expression.Constant(f).Reduce()); };

		public static Expression<Func<T>> Zero = () => { return (T)(Expression.Constant(0).Reduce()); };
		public static Expression<Func<T>> One = () => { return (T)(Expression.Constant(1).Reduce()); };
		public static Expression<Func<T>> Two = () => { return (T)(Expression.Constant(2).Reduce()); };
		public static Expression<Func<T>> Three = () => { return (T)(Expression.Constant(3).Reduce()); };
		public static Expression<Func<T>> Four = () => { return (T)(Expression.Constant(4).Reduce()); };
		public static Expression<Func<T>> Five = () => { return (T)(Expression.Constant(5).Reduce()); };
		public static Expression<Func<T>> Six = () => { return (T)(Expression.Constant(6).Reduce()); };
		public static Expression<Func<T>> Seven = () => { return (T)(Expression.Constant(7).Reduce()); };
		public static Expression<Func<T>> Eight = () => { return (T)(Expression.Constant(8).Reduce()); };
		public static Expression<Func<T>> Nine = () => { return (T)(Expression.Constant(9).Reduce()); };


		public static Expression<Func<int, T>> CreateInitVariable = (i) => { return (T)(Expression.Assign(Expression.Parameter(typeof(int), "Result"), Expression.Constant(i)).Reduce()); };

		public static Expression<Func<T, Expression>> AssignResult = (expr) => { return Expression.Assign(Expression.Parameter(typeof(int), "Result"), expr.Reduce()); };

		
		
		public static Expression<Func<T>> ValueLiteralExpression() { return 0; }

		public static Expression<Func<T>> EmptyExpression = () => { return (T)Expression.Empty().Reduce(); };
		

	}

	public static class PredicateBuilder
	{
		public static Expression<Func<T, bool>> True<T>() { return f => true; }
		public static Expression<Func<T, bool>> False<T>() { return f => false; }

		public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
															Expression<Func<T, bool>> expr2)
		{
			var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
			return Expression.Lambda<Func<T, bool>>
				  (Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
		}

		public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
															 Expression<Func<T, bool>> expr2)
		{
			var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
			return Expression.Lambda<Func<T, bool>>
				  (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
		}
	}
}
