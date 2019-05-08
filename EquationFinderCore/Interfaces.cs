/*
 *
 * Developed by Adam White
 *  https://csharpcodewhisperer.blogspot.com
 * 
 */
using System;
using System.Linq;
using System.Numerics;
using System.Collections.Generic;

namespace EquationFinderCore
{
	public delegate void FoundEquationDelegate(string resultsObject);

	public interface IEquation
	{
		void GenerateNewAndEvaluate(IEquationFinderArgs args);
		bool IsSolution { get; }
		BigInteger Result { get; }
		string ToString();
	}

	public interface IEquationFinderArgs
	{
		List<int> TermPool { get; }
		string OperatorPool { get; }
		BigInteger TargetValue { get; }
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
		Exponentiation = 5,
		None = 6
	}
}
