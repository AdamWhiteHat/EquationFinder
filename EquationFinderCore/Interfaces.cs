/*
 *
 * Developed by Adam Rakaska
 *  http://www.csharpprogramming.tips
 * 
 */
using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace EquationFinderCore
{
	public delegate void FoundEquationDelegate(string resultsObject);

	public interface IEquation
	{
		void GenerateNewAndEvaluate(IEquationFinderArgs args);
		bool IsSolution { get; }
		decimal Result { get; }
		string ToString();
	}

	public interface IEquationFinderArgs
	{
		List<int> TermPool { get; }
		string OperatorPool { get; }
		decimal TargetValue { get; }
		int NumberOfOperations { get; }
		Random Rand { get; }
	}

	public enum OperationType
	{
		Equal = 0,
		Add = 1,
		Subtract = 2,
		Multiply = 3,
		Divide = 4,
		Raise = 5,
		None = 6
	}


	public class MathSystem
	{


		public interface IExpresionGenerator
		{
			IEquation GenerateExpression(IExpresionGeneratorArgs args);
		}

		public interface IEquationSolver
		{
		}


		//public interface IEquationFinder
		//{
		//	  IExpression CreateEquation(IEquationFinderArgs args);
		//	  IEquation SolveEquation(IEquation equation);
		//	  
		//	  string ToString();
		//}

		public interface IExpresionGeneratorArgs
		{
			  List<int> TermPool { get; }
			  string OperatorPool { get; }
			  int NumberOfOperations { get; }
			  
			  decimal TargetValue { get; }		
		}


	}
}
