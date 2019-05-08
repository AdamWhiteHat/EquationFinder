/*
 *
 * Developed by Adam White
 *  https://csharpcodewhisperer.blogspot.com
 * 
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Runtime.ExceptionServices;

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
		[STAThread()]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(true);
			Application.ThreadException += Application_ThreadException;
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
			AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;
			Application.Run(new MainForm());
		}

		private static string ExceptionFileName = "Exception.log.txt";

		private static void CurrentDomain_FirstChanceException(object sender, FirstChanceExceptionEventArgs e)
		{
			HandleException((Exception)e.Exception, "FirstChanceException");
		}

		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			HandleException((Exception)e.ExceptionObject, "UnhandledException");
		}

		private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
		{
			HandleException((Exception)e.Exception, "ThreadException");
		}


		private static void HandleException(Exception ex, string CallingEvent)
		{
			File.AppendAllLines(ExceptionFileName, new string[] { "", "", string.Format("{0} occurred @ {1}", CallingEvent, DateTime.Now.ToString("yyyy-MM-ddhh:mm:ss")) });

			string exceptionName = string.Empty;

			if (ex != null && ex.GetType() != null && !string.IsNullOrEmpty(ex.GetType().Name))
			{
				List<string> outputLines = new List<string>();
				exceptionName = ex.GetType().Name;

				string exType = string.Format("Exception of type \"{0}\":", exceptionName);
				outputLines.Add(exType);

				if (!string.IsNullOrEmpty(ex.Message))
				{
					string exMessage = string.Format("\t[Message=\"{0}\"]", ex.Message);
					outputLines.Add(exMessage);
					outputLines.Add(Environment.NewLine);
				}

				if (ex.TargetSite != null && !string.IsNullOrEmpty(ex.TargetSite.Name))
				{
					string exTargetSite = string.Format("\t[TargetSite=\"{0}\"]", ex.TargetSite.Name);
					outputLines.Add(exTargetSite);
				}

				if (!string.IsNullOrEmpty(ex.Source))
				{
					string exSource = string.Format("\t[Source=\"{0}\"]", ex.Source);
					outputLines.Add(exSource);
				}

				if (!string.IsNullOrEmpty(ex.StackTrace))
				{
					string exStacktrace = string.Format("\t[StackTrace=\"{0}\"]", ex.StackTrace);
					outputLines.Add(exStacktrace);
				}

				if (outputLines != null && outputLines.Count > 0)
				{
					File.AppendAllLines(ExceptionFileName, outputLines.ToArray());

					Console.ForegroundColor = ConsoleColor.Red;
					foreach (string line in outputLines)
					{
						if (line.Contains("[Message=")) { Console.ForegroundColor = ConsoleColor.Yellow; }
						else if (line.Contains("[StackTrace=")) { Console.ForegroundColor = ConsoleColor.Cyan; }

						Console.WriteLine(line);
					}
					Console.ResetColor();
				}

			}
			else
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Exception has been caught by the Global Exception Handler. Unfortunately, there was not exception object or error message available.");
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
