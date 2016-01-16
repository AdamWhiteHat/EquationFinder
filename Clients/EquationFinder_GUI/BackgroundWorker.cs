/*
 *
 * Developed by Adam Rakaska
 *  http://www.csharpprogramming.tips
 * 
 */
using System.Windows.Forms;

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
				if (e.Argument is ThreadSpawnerArgs)
				{
					IsDirty = true;

					equationFinder = new ThreadedEquationFinder<AlgebraicExpression2>((ThreadSpawnerArgs)e.Argument);					
					equationFinder.Run();					

					// Stats
					RoundEquationsGenerated = equationFinder.TotalEquationsGenerated;
					TotalEquationsGenerated += RoundEquationsGenerated;
					DisplayStats();
				}
			}
		}

		private void backgroundWorker_ThreadSpawner_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{			
			equationArgs = null;
			threadArgs = null;
			equationFinder = null;
			timerCollectResults.Stop();
			ToggleControlsVisibility(true);			
		}
	}
}
