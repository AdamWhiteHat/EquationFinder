/*
 *
 * Developed by Adam Rakaska
 *  http://www.csharpprogramming.tips
 * 
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace EquationFinder_GUI
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			//Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(true);
			Application.ThreadException += Application_ThreadException;
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
			Application.Run(new MainForm());
		}

		static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			HandleException((Exception)e.ExceptionObject);
		}

		static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
		{
			HandleException((Exception)e.Exception);
		}


		private static void HandleException(Exception ex)
		{
			string exceptionName = string.Empty;

			if (ex != null && ex.GetType() != null && !string.IsNullOrEmpty(ex.GetType().Name))
			{
				List<string> outputLines = new List<string>();
				exceptionName = ex.GetType().Name;

				
				string exType = string.Format("Exception of type \"{0}\":", exceptionName);
				outputLines.Add(exType);
				//Console.WriteLine(exType);

				if (!string.IsNullOrEmpty(ex.Source) && ex.TargetSite != null && !string.IsNullOrEmpty(ex.TargetSite.Name))
				{
					string exLocation = string.Format("[{0}: Source=\"{1}\", TargetSite=\"{2}\"]", exceptionName, ex.Source, ex.TargetSite.Name);
					outputLines.Add(exLocation);
					outputLines.Add(Environment.NewLine);
					//Console.WriteLine(exLocation);
					//Console.WriteLine();
				}

				if (!string.IsNullOrEmpty(ex.Source))
				{
					string exMessage = string.Format("[Message=\"{0}\"]", ex.Message);
					outputLines.Add(exMessage);
					outputLines.Add(Environment.NewLine);
					//Console.WriteLine(exMessage);
					//Console.WriteLine();
				}

				if (!string.IsNullOrEmpty(ex.StackTrace))
				{
					string exStacktrace = string.Format("[Stacktrace=\"{0}\"]");
					outputLines.Add(exStacktrace);
					outputLines.Add(Environment.NewLine);
					//Console.ForegroundColor = ConsoleColor.Cyan;
					//Console.WriteLine(exStacktrace);					
				}
				
				if (outputLines != null && outputLines.Count > 0)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					foreach (string line in outputLines)
					{
						if (line.Contains("[Message=")) { Console.ForegroundColor = ConsoleColor.Yellow; }
						else if (line.Contains("[Stacktrace=")) { Console.ForegroundColor = ConsoleColor.Cyan; }

						Console.WriteLine(line);
					}
					Console.ResetColor();

					File.WriteAllLines("Exception.log.txt", outputLines.ToArray());
				}
				
			}
			else
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Exception has been caught by the Gloabal Exception Handler. Unfortionatly, there was not exception object or error message available.", exceptionName);
			}
		}

		public static void CountdownTimer(int Seconds = 5)
		{
			Console.ResetColor();
			Console.WriteLine(Environment.NewLine);
			Console.Write("The application will now terminate...   ");

			int top = Console.CursorTop;
			int left = Console.CursorLeft;

			if (Seconds < 1) return;
			if (Seconds > 9) Seconds = 9;

			for (int counter = Seconds * 10; counter > 0; counter--)
			{
				if (Console.KeyAvailable)
				{
					return;
				}

				if (counter % 10 == 0)
				{
					Console.SetCursorPosition(top, left); //Console.Write("\b");
					Console.Write("{0}", (counter / 10));
				}

				System.Threading.Thread.Sleep(100);
			}
		}
	}
}
