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
	public static class StaticScriptControl
	{
		private static MSScriptControl.ScriptControl sc = new MSScriptControl.ScriptControl();

		static StaticScriptControl()
		{
			sc.AllowUI = false;
			sc.Language = "VBScript";
		}

		public static decimal Evaluate(string Expression)
		{
			return StaticClass.String2Decimal(EvaluateToString(Expression));
		}

		public static string EvaluateToString(string Expression)
		{
			object result = sc.Eval(Expression);
			return result.ToString();
		}

		public static string Eval(string s)
		{
			return (string)((object)new MSScriptControl.ScriptControl() { Language = "VBScript" }.Eval(s));
		}

	}	
	
}
