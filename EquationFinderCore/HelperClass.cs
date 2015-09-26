/*
 *
 * Developed by Adam Rakaska
 *  http://www.csharpprogramming.tips
 * 
 */
using System;
using System.Linq;
using System.Text;
//using MSScriptControl;
using System.Threading;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace EquationFinderCore
{
	public static class HelperClass
	{
		public const string DecimalNumbers = "0123456789";
		public const string AlgebraicOperators = "+-*/";

		public static int String2Int(string Input)
		{
			int iReturn = 0;
			if (Int32.TryParse(Input, out iReturn))
			{
				return iReturn;
			}
			else
			{
				return 0;
			}
		}

		public static decimal String2Decimal(string Input)
		{
			decimal lResult = 0;
			decimal.TryParse(Input, out lResult);
			return lResult;
		}
	}	
}
