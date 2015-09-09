/*
 *
 * Developed by Adam Rakaska
 *  http://www.csharpprogramming.tips
 * 
 */
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

using EquationFinder;

namespace EquationFinder_GUI
{
	public partial class MainForm : Form
	{
		public bool IsDirty = false;

		long TotalExpressionsGenerated;
		long ExpressionsGeneratedThisRound;

		public MainForm()
		{
			InitializeComponent();

			TotalExpressionsGenerated = 0;
			ExpressionsGeneratedThisRound = 0;

			int numOps = 9;//StaticRandom.Instance.Next(3, 9);
			//int maxPossible = numOps * (MaxIntValue);
			int targetVal = 27;//StaticRandom.Instance.Next(1, maxPossible + 1);

			tbTargetValue.Text = targetVal.ToString();
			tbNumberOperations.Text = numOps.ToString();
			tbTTL.Text = "6";
			radioRandom.Checked = true;
			tbTerm.Text = "9";

			listOperators.Items[0].Selected = true;
			listOperators.Items[1].Selected = true;
			listOperators.Items[2].Selected = true;
		}

		private void MainForm_Shown(object sender, EventArgs e)
		{
			tbNumberOperations.TextChanged += new EventHandler(this.OnParametersChanged);
			tbTargetValue.TextChanged += new EventHandler(this.OnParametersChanged);
			DisplayStats();
		}

		void BtnFindSolutionClick(object sender, EventArgs e)
		{

			decimal targetValue = StaticClass.String2Decimal(tbTargetValue.Text);
			//int MaxIntValue = 9;
			int numberOfThreads = StaticClass.String2Int(tbThreads.Text);
			int numberOfOperations = StaticClass.String2Int(tbNumberOperations.Text);
			int timeToLive = StaticClass.String2Int(tbTTL.Text);
			int numberOfRounds = StaticClass.String2Int(tbRounds.Text);
			string OperatorPool = GetOperatorPool();
			string TermPool = GetTermPool();

			if (string.IsNullOrWhiteSpace(TermPool))
			{
				MessageBox.Show("Term cannot be empty.", "Input missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (string.IsNullOrWhiteSpace(OperatorPool))
			{
				MessageBox.Show("You must select at least one operation.", "Input missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			EquationFinderArgs equationArgs = new EquationFinderArgs(targetValue, numberOfOperations, TermPool, OperatorPool);
			ThreadSpawnerArgs threadArgs = new ThreadSpawnerArgs(DisplayOutput, timeToLive, numberOfThreads, numberOfRounds, equationArgs);

			if (!backgroundWorker_ThreadSpawner.IsBusy)
			{
				backgroundWorker_ThreadSpawner.RunWorkerAsync(threadArgs);
			}
		}

		private void backgroundWorker_ThreadSpawner_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			if (e != null && e.Argument != null)
			{				
				DisableControls();

				if (e.Argument is ThreadSpawnerArgs)
				{
					ThreadSpawnerArgs args = (ThreadSpawnerArgs)e.Argument;
					ThreadSpawnLauncher(args);
				}
			}
		}

		private void backgroundWorker_ThreadSpawner_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			EnableControls();
		}

		private string GetOperatorPool()
		{
			StringBuilder result = new StringBuilder();
			foreach (ListViewItem item in listOperators.SelectedItems)
			{
				switch (item.Text)
				{
					case "Addition":
						result.Append('+');
						break;

					case "Subtraction":
						result.Append('-');
						break;

					case "Multiplication":
						result.Append('*');
						break;

					case "Division":
						result.Append('/');
						break;
				}
			}
			return result.ToString();
		}

		private string GetTermPool()
		{
			StringBuilder result = new StringBuilder();
			int maxTerm = Convert.ToInt32(tbTerm.Text);

			if (radioRandom.Checked)
			{
				int counter = maxTerm;
				while (counter > 0)
				{
					result.Append(counter);
					counter--;
				}
			}
			else
			{
				result = result.Append(maxTerm);
			}
			
			return result.ToString();
		}

		private void ThreadSpawnLauncher(ThreadSpawnerArgs threadArgs)
		{
			IsDirty = true;

			ThreadedEquationFinder<AlgebraicTuple> equationFinder = new ThreadedEquationFinder<AlgebraicTuple>(threadArgs);
			string[] previousResults = GetOutputLines();
			if (previousResults != null && previousResults.Length > 0)
			{
				equationFinder.Results.AddRange(previousResults);
			}
			equationFinder.Run(ThreadedEquationFinder<AlgebraicTuple>.ThreadManager);

			// Stats
			ExpressionsGeneratedThisRound = equationFinder.TotalExpressionsGenerated;
			TotalExpressionsGenerated += ExpressionsGeneratedThisRound;
			DisplayStats();
		}

		private string[] GetOutputLines()
		{
			return (string[])tbOutput.Invoke(new Func<string[]>(delegate { return (tbOutput.Lines.Length > 0) ? tbOutput.Lines : new string[]{}; } ));
		}

		private string GetOutputText()
		{
			return (string)tbOutput.Invoke(new Func<string>(delegate { return (tbOutput.Text.Length > 0) ? tbOutput.Text : string.Empty; }));
		}
		
		DialogResult PromptToSaveWork()
		{
			if (!IsDirty || string.IsNullOrEmpty(GetOutputText()))
			{
				return DialogResult.OK;
			}

			DialogResult choice =	 MessageBox.Show(string.Format(
									"Results not saved!{0}{0}" +
									"Would you like to save these results now before discarding?", Environment.NewLine),
									"Changing Parameters",
									MessageBoxButtons.YesNoCancel,
									MessageBoxIcon.Question,
									MessageBoxDefaultButton.Button1
							   );

			if (choice == DialogResult.No)
			{
				// The user made the decision to throw the results buffer away.				
				return DialogResult.OK; // Return OK to continue, 
			}
			if (choice == DialogResult.Yes)
			{
				if (SaveWork() == DialogResult.OK)
				{
					return DialogResult.OK;
				}
				// Else, the user cancled the save dialog box. Do not continue.
			}
		#region Mainform Events
			// Cancel, do not continue
			return DialogResult.Cancel;
		}


		DialogResult SaveWork()
		{
			DialogResult dResult = saveFileDialog.ShowDialog();

			if (dResult == DialogResult.OK)
			{
				using (FileStream fStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
				{
					using (TextWriter tWriter = new StreamWriter(fStream))
					{
						tWriter.Write(GetOutputText());
						tWriter.Flush();
						tWriter.Close();
					}					
				}
				IsDirty = false;
			}

			return dResult;
		}

		DialogResult OpenWork()
		{
			if (PromptToSaveWork() == DialogResult.Cancel)
				return DialogResult.Cancel;

			DialogResult dResult = openFileDialog.ShowDialog();
			if (dResult == DialogResult.OK)
			{
				using (FileStream fStream = new FileStream(openFileDialog.FileName, FileMode.Open))
				{
					using (TextReader tReader = new StreamReader(fStream))
					{
						string fileText = tReader.ReadToEnd();						
						tbOutput.Invoke(new MethodInvoker(delegate { tbOutput.Text = fileText; }));
						tReader.Close();
					}
				}
				IsDirty = false;
				ExpressionsGeneratedThisRound = 0;
				TotalExpressionsGenerated = 0;
				DisplayStats();
			}

			return dResult;
		}

		// Clear found solutions when changing Target or # Operations
		void OnParametersChanged(object sender, EventArgs e)
		{
			if (!backgroundWorker_ThreadSpawner.IsBusy)
			{
				if (PromptToSaveWork() == DialogResult.OK)
				{
					tbOutput.Invoke(new MethodInvoker(delegate { tbOutput.Text = string.Empty; }));
					ExpressionsGeneratedThisRound = 0;
					TotalExpressionsGenerated = 0;
					DisplayStats();
				}
			}
		}

		void BtnOpenClick(object sender, EventArgs e)
		{
			OpenWork();
		}

		void BtnSaveClick(object sender, EventArgs e)
		{
			SaveWork();
		}

		void DisplayOutput(string FormatMessage, params object[] FormatArgs)
		{
			tbOutput.Invoke(new MethodInvoker(
				delegate
				{
					string message = string.Format(FormatMessage, FormatArgs);
					if (message.Contains(ThreadedEquationFinder<AlgebraicTuple>.ExpirationMessage))
					{
						List<string> newOutput = new List<string>(tbOutput.Lines);
						newOutput.RemoveAll(line => line.Contains(ThreadedEquationFinder<AlgebraicTuple>.ExpirationMessage));
						tbOutput.Lines = newOutput.ToArray();
					}
					tbOutput.Text = message + Environment.NewLine + tbOutput.Text;
				}				
			));
		}

		void DisplayStats()
		{
			string statsString = string.Format("Expressions generated this round: {1}{0}" +
												"Expressions generated total: {2}", Environment.NewLine,
												ExpressionsGeneratedThisRound, TotalExpressionsGenerated);
			tbStats.Invoke(new MethodInvoker(delegate { tbStats.Text = statsString; }));
		}

		void DisableControls()
		{
			btnFindSolution.Invoke(new MethodInvoker(delegate { btnFindSolution.Enabled = false; }));
		}

		void EnableControls()
		{
			btnFindSolution.Invoke(new MethodInvoker(delegate { btnFindSolution.Enabled = true; }));
		}

		#endregion

	}
}




