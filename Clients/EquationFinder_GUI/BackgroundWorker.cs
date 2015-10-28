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
		private void backgroundWorker_ThreadSpawner_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			if (e != null && e.Argument != null)
			{
				//DisableControls();

				if (e.Argument is ThreadSpawnerArgs)
				{
					IsDirty = true;

					equationFinder = new ThreadedEquationFinder<AlgebraicExpression>((ThreadSpawnerArgs)e.Argument);

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
			equationArgs = null;
			threadArgs = null;
			equationFinder = null;

			btnFindSolution.Text = findButtonText;
			isSearching = false;
			btnFindSolution.BackColor = Color.LightGreen;
			//EnableControls();
		}
	}
}
