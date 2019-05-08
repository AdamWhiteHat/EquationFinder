/*
 *
 * Developed by Adam White
 *  https://csharpcodewhisperer.blogspot.com
 * 
 */
using System.Numerics;

namespace EquationFinder_Console
{
	public static class Settings
	{
		public static string File_Output = SettingsReader.GetSettingString("File.Output");

		public static BigInteger Equations_Goal = SettingsReader.GetSetting<BigInteger>("Equations.Goal");
		public static string Equations_Factory = SettingsReader.GetSettingString("Equations.Factory");

		public static string Term_Pool = SettingsReader.GetSettingString("Term.Pool");
		public static string Operand_Pool = SettingsReader.GetSettingString("Operand.Pool");
		public static int Operations_Quantity = SettingsReader.GetSetting<int>("Operations.Quantity");

		public static int Round_Threads = SettingsReader.GetSetting<int>("Round.Threads");
		public static int Round_Quantity = SettingsReader.GetSetting<int>("Round.Quantity");
		public static int Round_TimeToLive = SettingsReader.GetSetting<int>("Round.TimeToLive");
	}
}
