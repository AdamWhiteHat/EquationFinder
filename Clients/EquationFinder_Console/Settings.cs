using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace EquationFinder_Console
{
	public static class Settings
	{
		public static string File_Output		= SettingsReader.GetSettingString("File.Output");

		public static decimal Equations_Goal	= SettingsReader.GetSetting<decimal>("Equations.Goal");
		public static string  Equations_Factory	= SettingsReader.GetSettingString("Equations.Factory");
		
		public static string Operations			= SettingsReader.GetSettingString("Operations");
		public static int	 Operations_Quantity= SettingsReader.GetSetting<int>("Operations.Quantity");
		
		public static int	 Term_MaxValue		= SettingsReader.GetSetting<int>("Term.MaxValue");
		public static string Term_Varience		= SettingsReader.GetSettingString("Term.Varience");
		
		public static int	 Round_Threads		= SettingsReader.GetSetting<int>("Round.Threads");
		public static int	 Round_Quantity		= SettingsReader.GetSetting<int>("Round.Quantity");
		public static int	 Round_TimeToLive	= SettingsReader.GetSetting<int>("Round.TimeToLive");
	}
}
