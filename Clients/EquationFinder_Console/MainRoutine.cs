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
			if (string.IsNullOrWhiteSpace(Settings.Operations))
			{
				throw new ArgumentException(string.Format(settingsExceptionMessage, "Operations"), string.Format(settingsExceptionArgument, "Operations", "+-*"));
			}
			string[] operations = Settings.Operations.ToArray().Select(c => c.ToString()).ToArray();

			Func<decimal> termSelector;// = () => (decimal)(0);
			Func<string> operatorSelector = new Func<string>(delegate { return operations.ElementAt(StaticRandom.Instance.Next(0, operations.Length)).ToString(); });

			if (Settings.Term_Varience == "constant")
				termSelector = () => (decimal)(Settings.Term_MaxValue);
			else if (Settings.Term_Varience == "random")
				termSelector = () => (decimal)(StaticRandom.Instance.Next(0, Settings.Term_MaxValue));
			else
			{
				throw new ArgumentException(string.Format(settingsExceptionMessage, "Term.Varience"), string.Format(settingsExceptionArgument, "Term.Varience", "random"));
			}			

			EquationFinderArgs equationArgs = new EquationFinderArgs(Settings.Equations_Goal, Settings.Operations_Quantity, termSelector, operatorSelector);
			ThreadSpawnerArgs threadArgs = new ThreadSpawnerArgs(LogOutput, Settings.Round_TimeToLive, Settings.Round_Threads, Settings.Round_Quantity, equationArgs);

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
