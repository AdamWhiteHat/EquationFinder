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
using EquationFactories;
using EquationFinderCore;

namespace EquationFinder_GUI
{
	public partial class MainForm : Form
	{
		public bool IsDirty = false;

		long TotalEquationsGenerated;
		long EquationsGeneratedThisRound;

		public MainForm()
		{
			InitializeComponent();

			TotalEquationsGenerated = 0;
			EquationsGeneratedThisRound = 0;

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

		EquationFinderArgs equationArgs { get; set; }
		ThreadSpawnerArgs threadArgs { get; set; }
		ThreadedEquationFinder<AlgebraicTuple> equationFinder { get; set; }

		int maxTerm { get { return Convert.ToInt32(tbTerm.Text); } }
		decimal targetValue { get { return HelperClass.String2Decimal(tbTargetValue.Text); } }		
		int numberOfThreads { get { return HelperClass.String2Int(tbThreads.Text); } }
		int numberOfOperations { get { return HelperClass.String2Int(tbNumberOperations.Text); } }
		int timeToLive { get { return  HelperClass.String2Int(tbTTL.Text); } }
		int numberOfRounds { get { return HelperClass.String2Int(tbRounds.Text); } }
		string OperatorPool { get { return GetOperatorPool(); } }
		string TermPool { get { return GetTermPool(); } }
		//int MaxIntValue { get { return 9; } }

		void BtnFindSolutionClick(object sender, EventArgs e)
		{
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

			 equationArgs = new EquationFinderArgs(targetValue, numberOfOperations, TermPool, OperatorPool);

			string[] previousResults = GetOutputLines();
			if (previousResults != null && previousResults.Length > 0)
			{
				threadArgs = new ThreadSpawnerArgs(previousResults.ToList(), DisplaySolution, timeToLive, numberOfThreads, numberOfRounds, equationArgs);
			}
			else
			{
				 threadArgs = new ThreadSpawnerArgs(DisplaySolution, timeToLive, numberOfThreads, numberOfRounds, equationArgs);
			}

			if (backgroundWorker_ThreadSpawner.IsBusy == false)
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
					IsDirty = true;

					equationFinder = new ThreadedEquationFinder<AlgebraicTuple>((ThreadSpawnerArgs)e.Argument);
					
					equationFinder.Run();

					// Stats
					EquationsGeneratedThisRound = equationFinder.TotalEquationsGenerated;
					TotalEquationsGenerated += EquationsGeneratedThisRound;
					DisplayStats();
				}
			}
		}

		private void backgroundWorker_ThreadSpawner_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			equationFinder.Dispose();							

	 		equationArgs = null;	
			threadArgs = null;
			equationFinder = null;

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
				// Else, the user canceled the save dialog box. Do not continue.
			}
			// Cancel, do not continue
			return DialogResult.Cancel;
		}

		#region Mainform Events

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
				EquationsGeneratedThisRound = 0;
				TotalEquationsGenerated = 0;
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
					EquationsGeneratedThisRound = 0;
					TotalEquationsGenerated = 0;
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

		void DisplaySolution(EquationResults foundSolution)
		{
			tbOutput.Invoke(new MethodInvoker(
				delegate
				{
					tbOutput.Text = tbOutput.Text.Insert(0, string.Concat(foundSolution.EquationText, Environment.NewLine));
				}				
			));
		}

		void DisplayStats()
		{
			string statsString = string.Format("Equations generated this round: {1}{0}" +
												"Equations generated total: {2}", Environment.NewLine,
												EquationsGeneratedThisRound, TotalEquationsGenerated);
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




