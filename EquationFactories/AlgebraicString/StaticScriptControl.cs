/*
 *
 * Developed by Adam Rakaska
 *  http://www.csharpprogramming.tips
 * 
 */
using System;
using System.Linq;
using MSScriptControl;
using System.Threading;
using System.Collections.Generic;

using EquationFinderCore;

namespace EquationFactories
{
	public sealed class StaticScriptControl
	{
		public static MSScriptControl.ScriptControl Instance { get { return Nested.instance; } }

		private StaticScriptControl()
		{
		}

		private class Nested
		{
			internal static readonly MSScriptControl.ScriptControl instance;
			static Nested() // Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
			{ 
				instance = new MSScriptControl.ScriptControl();
				instance.AllowUI = false;
				instance.Language = "VBScript";
			}
		}

		public static decimal Evaluate(string Equation)
		{
			return HelperClass.String2Decimal(EvaluateToString(Equation));
		}

		public static string EvaluateToString(string Equation)
		{
			object result = Instance.Eval(Equation);
			return result.ToString();
		}
	}
}
