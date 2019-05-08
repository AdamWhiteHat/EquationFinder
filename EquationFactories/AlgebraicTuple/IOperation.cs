/*
 *
 * Developed by Adam White
 *  https://csharpcodewhisperer.blogspot.com
 * 
 */
using System.Numerics;

namespace EquationFactories
{
	public interface IOperation
	{
		//IArithmeticOperation Art { get; set; }
		BigInteger Calculate(BigInteger Value1, BigInteger Value2);
		string ToString();
		bool Equals(object obj);
	}
}
