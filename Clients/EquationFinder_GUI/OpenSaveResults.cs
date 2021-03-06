﻿/*
 *
 * Developed by Adam White
 *  https://csharpcodewhisperer.blogspot.com
 * 
 */
using System;
using System.IO;
using System.Windows.Forms;

namespace EquationFinder_GUI
{
	public partial class MainForm : Form
	{
		private DialogResult SaveWork()
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

		private void BtnOpenClick(object sender, EventArgs e)
		{
			OpenWork();
		}

		private DialogResult OpenWork()
		{
			if (PromptToSaveWork() == DialogResult.Cancel)
			{
				return DialogResult.Cancel;
			}

			DialogResult dResult = openFileDialog.ShowDialog();
			if (dResult == DialogResult.OK)
			{
				string fileText = string.Empty;
				using (FileStream fStream = new FileStream(openFileDialog.FileName, FileMode.Open))
				{
					using (TextReader tReader = new StreamReader(fStream))
					{
						fileText = tReader.ReadToEnd();
						tReader.Close();
					}
				}

				if (tbOutput.InvokeRequired)
				{
					tbOutput.Invoke(new MethodInvoker(() => tbOutput.Text = fileText));
				}
				else
				{
					tbOutput.Text = fileText;
				}

				IsDirty = false;
				ResetStats();
				DisplayStats();
			}

			return dResult;
		}

		// Clear found solutions when changing Target or # Operations
		private void OnParametersChanged(object sender, EventArgs e)
		{
			if (!backgroundWorker_ThreadSpawner.IsBusy)
			{
				if (PromptToSaveWork() == DialogResult.OK)
				{
					if (tbOutput.InvokeRequired)
					{
						tbOutput.Invoke(new MethodInvoker(() => tbOutput.Text = string.Empty));
					}
					else
					{
						tbOutput.Text = string.Empty;
					}
					ResetStats();					
					DisplayStats();
				}
			}
		}

		private DialogResult PromptToSaveWork()
		{
			if (!IsDirty || string.IsNullOrEmpty(GetOutputText()))
			{
				return DialogResult.OK;
			}

			DialogResult choice = MessageBox.Show(string.Format(
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
	}
}
