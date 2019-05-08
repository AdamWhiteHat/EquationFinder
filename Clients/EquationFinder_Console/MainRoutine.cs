/*
 *
 * Developed by Adam White
 *  https://csharpcodewhisperer.blogspot.com
 * 
 */
using System;
using System.IO;
using System.Collections.Generic;
using EquationFinder;
using EquationFactories;
using EquationFinderCore;

namespace EquationFinder_Console
{
	public class MainRoutine
	{
		public ThreadSpawnerArgs threadArgs { get; set; }
		public IEquationFinderArgs equationArgs { get; set; }
		//BackgroundWorker bgWorker;
		private string outputFilename;
		private List<string> previousfoundResults { get; set; }
		private static string settingsExceptionMessage = "<appSettings> must be configured in App.config file: {0}  key was missing or empty.";
		private static string settingsExceptionArgument = "<appSettings><add key=\"{0}\" value=\"{1}\"/>";

		public MainRoutine(List<string> args)
		{
			if (args.Count > 1)
			{
				// Perhaps accept args?
			}

			LoadSettings();
		}

		private void LoadSettings()
		{
			if (string.IsNullOrWhiteSpace(Settings.Operand_Pool))
			{
				throw new ArgumentException(string.Format(settingsExceptionMessage, "Operand_Pool"), string.Format(settingsExceptionArgument, "Operand_Pool", "+-*"));
			}

			if (string.IsNullOrWhiteSpace(Settings.File_Output))
			{
				throw new ArgumentException(string.Format(settingsExceptionMessage, "File_Output"), string.Format(settingsExceptionArgument, "File_Output", "output.txt"));
			}
			else
			{
				outputFilename = Settings.File_Output;
			}

			previousfoundResults = new List<string>();
			if (File.Exists(outputFilename))
			{
				previousfoundResults.AddRange(File.ReadAllLines(outputFilename));
			}
		}

		public void Find()
		{
			if (string.IsNullOrWhiteSpace(Settings.Term_Pool))
			{
				throw new Exception("Setting TermPool is empty. Check the configuration file, and set the AppSetting value to a comma delimited list of allowed values for the key \"Term.Pool\".");
			}

			int parseOut = 0;
			List<int> termPool = new List<int>();
			foreach (string term in Settings.Term_Pool.Split(','))
			{
				parseOut = 0;
				if (int.TryParse(term, out parseOut))
				{
					termPool.Add(parseOut);
				}
			}

			equationArgs = new EquationFinderArgs(Settings.Equations_Goal, Settings.Operations_Quantity, termPool, Settings.Operand_Pool);
			threadArgs = new ThreadSpawnerArgs(previousfoundResults, LogSolution, Settings.Round_TimeToLive, Settings.Round_Threads, Settings.Round_Quantity, equationArgs);

			ThreadedEquationFinder<AlgebraicTuple> equationFinder = new ThreadedEquationFinder<AlgebraicTuple>(threadArgs);
			if (File.Exists(outputFilename))
			{
				equationFinder.Results.AddRange(File.ReadAllLines(outputFilename));
			}
			equationFinder.Run();
		}

		private void LogSolution(string foundSolution)
		{
			string outputString = foundSolution;

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
