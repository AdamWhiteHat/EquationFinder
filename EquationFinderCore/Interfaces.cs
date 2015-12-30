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
		void SetArgs(IEquationFinderArgs args);
		void GenerateNewAndEvaluate();
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
}
