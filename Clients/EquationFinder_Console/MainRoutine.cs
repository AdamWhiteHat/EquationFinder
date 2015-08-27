using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using EquationFinder;
using System.IO;

namespace EquationFinder_Console
{
	public class MainRoutine
	{
		//BackgroundWorker bgWorker;

		private string outputFilename;
		private static string settingsExceptionMessage = "<appSettings> must be configured in App.config file: {0}  key was missing or empty.";
		private static string settingsExceptionArgument = "<appSettings><add key=\"{0}\" value=\"{1}\"/>";

		public MainRoutine(List<string> args)
		{
			if (args.Count > 1)
			{
				// Perhaps accept args?
			}
			
			if (string.IsNullOrWhiteSpace(Settings.File_Output))
			{
				throw new ArgumentException(string.Format(settingsExceptionMessage, "File_Output"), string.Format(settingsExceptionArgument, "File_Output", "output.txt"));
			}
			else
			{
				outputFilename = Settings.File_Output;
			}
		}

		public void Find()
		{
			if (string.IsNullOrWhiteSpace(Settings.Operand_Pool))
			{
				throw new ArgumentException(string.Format(settingsExceptionMessage, "Operand_Pool"), string.Format(settingsExceptionArgument, "Operand_Pool", "+-*"));
			}
			string[] operations = Settings.Operand_Pool.ToArray().Select(c => c.ToString()).ToArray();
			string[] terms		= Settings.Term_Pool.ToArray().Select(c => c.ToString()).ToArray();

			//Func<decimal> termSelector = new Func<decimal>(delegate { return Convert.ToDecimal(operations.ElementAt(StaticRandom.Instance.Next(0, operations.Length))); });
			//Func<string> operatorSelector = new Func<string>(delegate { return operations.ElementAt(StaticRandom.Instance.Next(0, operations.Length)).ToString(); });

			List<string> previousfoundResults = new List<string>();
			if (File.Exists(outputFilename))
			{
				previousfoundResults.AddRange(File.ReadAllLines(outputFilename));
			}
			
			EquationFinderArgs equationArgs = new EquationFinderArgs(Settings.Equations_Goal, Settings.Operations_Quantity, Settings.Term_Pool, Settings.Operand_Pool);
			ThreadSpawnerArgs threadArgs = new ThreadSpawnerArgs(LogOutput, previousfoundResults, Settings.Round_TimeToLive, Settings.Round_Threads, Settings.Round_Quantity, equationArgs);

			ThreadedEquationFinder equationFinder = new ThreadedEquationFinder(threadArgs);
			if (File.Exists(outputFilename))
			{
				equationFinder.Results.AddRange(File.ReadAllLines(outputFilename));
			}
			equationFinder.Run(AlgebraicTuple.TupleExpressionThreadManager);

			// Stats
			long ExpressionsGeneratedThisRound = equationFinder.TotalExpressionsGenerated;
			//TotalExpressionsGenerated += ExpressionsGeneratedThisRound;
			//DisplayStats();
		}

		private void LogOutput(string format, params object[] args)
		{
			string outputString = string.Format(format, args);

			if (!string.IsNullOrWhiteSpace(outputFilename))
			{
				Console.WriteLine(outputString);

				if (!outputString.Contains("Time-to-live expired"))
				{
					File.AppendAllText(outputFilename, outputString + Environment.NewLine);
				}
			}			
		}
	}
}
