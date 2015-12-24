/*
 *
 * Developed by Adam Rakaska
 *  http://www.csharpprogramming.tips
 * 
 */
namespace EquationFactories
{	
	public sealed class StaticScriptControl
	{
		public static MSScriptControl.ScriptControl Instance { get { return Nested.instance; } }
		private StaticScriptControl() { }

		private sealed class Nested
		{
			internal static readonly MSScriptControl.ScriptControl instance = new MSScriptControl.ScriptControl() { AllowUI = false, Language = "VBScript" };
			static Nested() { }  // Explicit static constructor to tell C# compiler not to mark type as beforefieldinit			
			private Nested() { } // Cannot be instantiated
		}

		public static decimal Evaluate(string Equation)
		{			
			return EquationFinderCore.HelperClass.String2Decimal(Instance.Eval(Equation).ToString());
		}
	}
}
