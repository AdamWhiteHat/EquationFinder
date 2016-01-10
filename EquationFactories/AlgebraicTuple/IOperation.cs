//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

namespace EquationFactories
{	
	public interface IOperation
	{
		//IArithmeticOperation Art { get; set; }
		decimal Calculate(decimal Value1, decimal Value2);
		string ToString();
		bool Equals(object obj);
	}
}
