/*
 *
 * Developed by Adam Rakaska
 *  http://www.csharpprogramming.tips
 * 
 */
using System;
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
		private EquationFinderArgs equationArgs { get; set; }
		private ThreadSpawnerArgs threadArgs { get; set; }
		private ThreadedEquationFinder<AlgebraicExpression2> equationFinder { get; set; }

		private bool IsDirty = false;
		private bool isSearching = false;
		private long TotalEquationsGenerated { get; set; }
		private long RoundEquationsGenerated { get; set; }
		private long TotalSolutionsFound { get; set; }
		private long RoundSolutionsFound { get; set; }
		private long lastSolutionCount { get; set; }
		private static string findButtonText = "Find Solution";
		private static string cancelButtonText = "Stop Searching";
		private List<int> TermPool { get { return GetTermPool(cbAllowZero.Checked); } }
		private string OperatorPool { get { return GetOperatorPool(); } }
		private int maxTerm { get { return Convert.ToInt32(tbOperandMax.Text); } }
		private decimal targetValue { get { return HelperClass.String2Decimal(tbGoal.Text); } }
		private int numberOfOperations { get { return HelperClass.String2Int(tbOperandQuantity.Text); } }
		private int numberOfThreads { get { return HelperClass.String2Int(tbThreads.Text); } }
		private int timeToLive { get { return HelperClass.String2Int(tbTTL.Text); } }
		private int numberOfRounds { get { return HelperClass.String2Int(tbRounds.Text); } }

		public MainForm()
		{
			InitializeComponent();						
			listboxOperators.Items[0].Selected = true;		
		}

		private void MainForm_Shown(object sender, EventArgs e)
		{
			tbOperandQuantity.TextChanged += new EventHandler(this.OnParametersChanged);
			tbGoal.TextChanged += new EventHandler(this.OnParametersChanged);
			DisplayStats();
		}


		private void BtnSaveClick(object sender, EventArgs e)
		{
			SaveWork();
		}

		#region Equation search toggling

		///<summary>enabled = make visible</summary>
		private void ToggleControlsVisibility(bool enabled)
		{
				isSearching = !enabled;
				btnFindSolution.Text = isSearching ? cancelButtonText : findButtonText;
				btnFindSolution.BackColor = isSearching ? Color.MistyRose : Color.LightGreen;		
		}

		private void BtnFindSolutionClick(object sender, EventArgs e)
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

		private void CancelSearch()
		{
			if (isSearching && !equationFinder.CancellationPending)
			{
				equationFinder.Cancel();
			}
		}

		private void BeginSearch()
		{		
			if (TermPool == null || TermPool.Count < 1)
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

		private void DisplaySolution(string foundSolution)
		{
			if (tbOutput.InvokeRequired)
			{
				tbOutput.Invoke(new MethodInvoker(
					delegate
					{
						tbOutput.Text = tbOutput.Text.Insert(0, string.IsNullOrWhiteSpace(foundSolution) ? "" : string.Concat(foundSolution,Environment.NewLine));
					}
				));
			}
			else
			{
				tbOutput.Text = tbOutput.Text.Insert(0, string.IsNullOrWhiteSpace(foundSolution) ? "" : string.Concat(foundSolution, Environment.NewLine));
			}
		}

		private string[] GetOutputLines()
		{
			if (tbOutput.InvokeRequired)
			{
				return (string[])tbOutput.Invoke(new Func<string[]>(delegate { return GetOutputLines(); }));
			}
			else
			{
				if (tbOutput.Lines.Length < 1)
				{
					return new string[] { };
				}
				else
				{
					return tbOutput.Lines;
				}
			}
		}

		private string GetOutputText()
		{
			if (tbOutput.InvokeRequired)
			{
				return (string)tbOutput.Invoke(new Func<string>(delegate { return GetOutputText(); }));
			}
			else
			{
				if (tbOutput.Text.Length < 1)
				{
					return "";
				}
				else
				{
					return tbOutput.Text;
				}
			}
		}

		private void ResetStats()
		{
			lastSolutionCount = 0;
			TotalEquationsGenerated = 0;
			TotalSolutionsFound = 0;
			RoundEquationsGenerated = 0;
			RoundSolutionsFound = 0;
		}

		private void DisplayStats()
		{
			if (tbStats.InvokeRequired)
			{
				tbStats.Invoke(new MethodInvoker(delegate { DisplayStats(); }));
			}
			else
			{
				tbOutput.Text = tbOutput.Text.TrimEnd(new char[] { '\r', '\n' });
				TotalSolutionsFound = tbOutput.Lines.Count();
				RoundSolutionsFound = TotalSolutionsFound - lastSolutionCount;
				lastSolutionCount = TotalSolutionsFound;

				if (RoundSolutionsFound < 0)
				{
					RoundSolutionsFound = 0;
				}				

				StringBuilder stats = new StringBuilder();
				stats.AppendLine("TOTAL:");
				stats.Append("  Equations generated: ");
				stats.AppendLine(TotalEquationsGenerated.ToString());
				stats.Append("  Solutions found:     ");
				stats.AppendLine(TotalSolutionsFound.ToString());
				stats.AppendLine("THIS ROUND:");
				stats.Append("  Equations generated: ");
				stats.AppendLine(RoundEquationsGenerated.ToString());
				stats.Append("  New solutions:       ");
				stats.Append(RoundSolutionsFound);

				tbStats.Text = stats.ToString();
			}
		}

		#endregion



		#region Option pools (term & operator)

		private string GetOperatorPool()
		{
			StringBuilder result = new StringBuilder();
			foreach (ListViewItem item in listboxOperators.SelectedItems)
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

		private List<int> GetTermPool(bool IncludeZero = false)
		{
			int minTerm = IncludeZero ? -1 : 0;
			List<int> result = new List<int>();

			if (radioRandom.Checked)
			{
				int counter = maxTerm;
				while (counter > minTerm)
				{
					result.Add(counter--);
				}
			}
			else
			{
				result.Add(maxTerm);
			}

			return result;
		}

		#endregion
		
	}
}




