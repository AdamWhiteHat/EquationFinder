/*
 *
 * Developed by Adam White
 *  https://csharpcodewhisperer.blogspot.com
 * 
 */
namespace EquationFactories
{
	using System.Numerics;
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

		public static BigInteger Evaluate(string Equation)
		{			
			return EquationFinderCore.HelperClass.String2Decimal(Instance.Eval(Equation).ToString());
		}
	}
}
