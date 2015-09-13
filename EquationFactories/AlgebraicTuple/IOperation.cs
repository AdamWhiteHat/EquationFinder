//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

namespace EquationFactories
{
	public enum OperandType
	{
		Equal = 0,
		Add = 1,
		Subtract = 2,
		Multiply = 3,
		Divide = 4,
		Raise = 5,
		None = 6
	}

	public interface IOperation
	{
		//IArithmeticOperation Art { get; set; }
		decimal Calculate(decimal Value1, decimal Value2);
		string ToString();
		bool Equals(object obj);
	}
}
