/*
 *
 * Developed by Adam Rakaska
 *  http://www.csharpprogramming.tips
 * 
 */
using System;
using System.IO;
using System.Linq;
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

		string OperatorPool = string.Empty;

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
			//EquationExpression.TestExpressionMethod();

			string operators = string.Empty;
			foreach (ListViewItem item in listOperators.SelectedItems)
			{
				switch (item.Text)
				{
					case "Addition":
						operators += "+";
						break;

					case "Subtraction":
						operators += "-";
						break;

					case "Multiplication":
						operators += "*";
						break;

					case "Division":
						operators += "/";
						break;
				}
			}
			OperatorPool = operators;

			backgroundWorker_ThreadSpawner.RunWorkerAsync();
		}

		private void backgroundWorker_ThreadSpawner_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			DisableControls();
			ThreadSpawner();
		}

		private void backgroundWorker_ThreadSpawner_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			EnableControls();
		}

		private void ThreadSpawner()
		{
			IsDirty = true;
			decimal targetValue = StaticClass.String2Decimal(tbTargetValue.Text);
			//int MaxIntValue = 9;
			int numberOfThreads = StaticClass.String2Int(tbThreads.Text);
			int numberOfOperations = StaticClass.String2Int(tbNumberOperations.Text);
			int timeToLive = StaticClass.String2Int(tbTTL.Text);
			int numberOfRounds = StaticClass.String2Int(tbRounds.Text);

			Func<decimal> termSelector;
			int numToUse = StaticClass.String2Int(tbTerm.Text);

			if (radioConstant.Checked)
				termSelector = () => (decimal)(numToUse);
			else
				termSelector = () => (decimal)(StaticRandom.Instance.Next(0, numToUse));

			Func<string> operatorSelector = new Func<string>(delegate { return OperatorPool.ElementAt(StaticRandom.Instance.Next(0, OperatorPool.Length)).ToString(); });

			EquationFinderArgs equationArgs = new EquationFinderArgs(targetValue, numberOfOperations, termSelector, operatorSelector);

			ThreadSpawnerArgs threadArgs = new ThreadSpawnerArgs(DisplayOutput, timeToLive, numberOfThreads, numberOfOperations, equationArgs);

			ThreadedEquationFinder equationFinder = new ThreadedEquationFinder(threadArgs);
			equationFinder.Results.AddRange(tbOutput.Lines);
			equationFinder.Run(AlgebraicTuple.TupleExpressionThreadManager);

			// Stats
			ExpressionsGeneratedThisRound = equationFinder.TotalExpressionsGenerated;
			TotalExpressionsGenerated += ExpressionsGeneratedThisRound;
			DisplayStats();
		}
		
		DialogResult PromptToSaveWork()
		{
			if (!IsDirty || string.IsNullOrEmpty(tbOutput.Text))
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
					TextWriter tWriter = new StreamWriter(fStream);
					tWriter.Write(tbOutput.Text);
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
					TextReader tReader = new StreamReader(fStream);
					tbOutput.Text = tReader.ReadToEnd();
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
			if (PromptToSaveWork() == DialogResult.OK)
			{
				tbOutput.Text = string.Empty;
				ExpressionsGeneratedThisRound = 0;
				TotalExpressionsGenerated = 0;
				DisplayStats();
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
					if (message.Contains(ThreadedEquationFinder.ExpirationMessage))
					{
						List<string> newOutput = new List<string>(tbOutput.Lines);
						newOutput.RemoveAll(line => line.Contains(ThreadedEquationFinder.ExpirationMessage));
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




