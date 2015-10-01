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
		//BackgroundWorker bgWorker;
		private string outputFilename;
		private static string settingsExceptionMessage = "<appSettings> must be configured in App.config file: {0}  key was missing or empty.";
		private static string settingsExceptionArgument = "<appSettings><add key=\"{0}\" value=\"{1}\"/>";

		private List<string> previousfoundResults { get; set; }

		public EquationFinderArgs equationArgs { get; set; }
		public ThreadSpawnerArgs threadArgs { get; set; }

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
			equationArgs = new  EquationFinderArgs(Settings.Equations_Goal, Settings.Operations_Quantity, Settings.Term_Pool, Settings.Operand_Pool);
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
