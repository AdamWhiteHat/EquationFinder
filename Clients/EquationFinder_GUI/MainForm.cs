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
using System.Drawing;

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


		void BtnSaveClick(object sender, EventArgs e)
		{
			SaveWork();
		}
				
		EquationFinderArgs equationArgs { get; set; }
		ThreadSpawnerArgs threadArgs { get; set; }
		ThreadedEquationFinder<AlgebraicExpression> equationFinder { get; set; }

		bool isSearching = false;
		static string findButtonText = "Find Solution";
		static string cancelButtonText = "Stop Searching";
		string TermPool { get { return GetTermPool(cbAllowZero.Checked); } }
		string OperatorPool { get { return GetOperatorPool(); } }
		int maxTerm { get { return Convert.ToInt32(tbTerm.Text); } }
		decimal targetValue { get { return HelperClass.String2Decimal(tbTargetValue.Text); } }
		int numberOfOperations { get { return HelperClass.String2Int(tbNumberOperations.Text); } }
		int numberOfThreads { get { return HelperClass.String2Int(tbThreads.Text); } }		
		int timeToLive { get { return HelperClass.String2Int(tbTTL.Text); } }
		int numberOfRounds { get { return HelperClass.String2Int(tbRounds.Text); } }

		#region Equation search toggling

		///<summary>enabled = make visible</summary>
		void ToggleControlsVisibility(bool enabled)
		{
				isSearching = !enabled;
				btnFindSolution.Text = isSearching ? cancelButtonText : findButtonText;
				btnFindSolution.BackColor = isSearching ? Color.MistyRose : Color.LightGreen;		
		}

		void BtnFindSolutionClick(object sender, EventArgs e)
		{
			if (isSearching)
			{
				CancelSearch();
			}
			else
			{			
				BeginSearch();
			}
		}

		void CancelSearch()
		{
			if (isSearching && !equationFinder.CancellationPending)
			{
				equationFinder.Cancel();
			}
		}

		void BeginSearch()
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
				ToggleControlsVisibility(false);
				backgroundWorker_ThreadSpawner.RunWorkerAsync(threadArgs);
			}
		}

		#endregion
			

		#region Display Solution Logic

		void DisplaySolution(string foundSolution)
		{
			if (tbOutput.InvokeRequired)
			{
				tbOutput.Invoke(new MethodInvoker(
					delegate
					{
						tbOutput.Text = tbOutput.Text.Insert(0, string.Concat(foundSolution, Environment.NewLine));
					}
				));
			}
			else
			{
				tbOutput.Text = tbOutput.Text.Insert(0, string.Concat(foundSolution, Environment.NewLine));
			}
		}

		private string[] GetOutputLines()
		{
			if (tbOutput.Lines.Length < 1)
			{
				return new string[] { };
			}
			if (tbOutput.InvokeRequired)
			{
				return (string[])tbOutput.Invoke(new Func<string[]>(delegate { return tbOutput.Lines; }));
			}
			else
			{
				return tbOutput.Lines;
			}
		}

		private string GetOutputText()
		{
			if (tbOutput.Text.Length < 1)
			{
				return "";
			}
			if (tbOutput.InvokeRequired)
			{
				return (string)tbOutput.Invoke(new Func<string>(delegate { return tbOutput.Text; }));
			}
			else
			{
				return tbOutput.Text;
			}
		}

		void DisplayStats()
		{
			string statsString = string.Format("Equations generated this round: {1}{0}" +
												"Equations generated total: {2}", Environment.NewLine,
												EquationsGeneratedThisRound, TotalEquationsGenerated);
			if (tbStats.InvokeRequired)
			{
				tbStats.Invoke(new MethodInvoker(delegate { tbStats.Text = statsString; }));
				return;
			}
			tbStats.Text = statsString;
		}

		#endregion



		#region Option pools (term & operator)

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

		private string GetTermPool(bool IncludeZero = false)
		{
			int minTerm = IncludeZero ? -1 : 0;
			StringBuilder result = new StringBuilder();

			if (radioRandom.Checked)
			{
				int counter = maxTerm;
				while (counter > minTerm)
				{
					result.Append(counter--);
				}
			}
			else
			{
				result = result.Append(maxTerm);
			}

			return result.ToString();
		}

		#endregion



	}
}




