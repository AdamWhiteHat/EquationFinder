/*
 * 
 * Created by BurningBunny
 * Made using SharpDevelop
 *
 * Date.time: 6/7/2013.12:26 AM
 * 
 */
namespace EquationFinder_GUI
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("Addition");
			System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("Subtraction");
			System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("Multiplication");
			System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("Division");
			this.btnFindSolution = new System.Windows.Forms.Button();
			this.tbOutput = new System.Windows.Forms.TextBox();
			this.tbOperandQuantity = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btnOpen = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.backgroundWorker_ThreadSpawner = new System.ComponentModel.BackgroundWorker();
			this.tbGoal = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.tbTTL = new System.Windows.Forms.TextBox();
			this.tbRounds = new System.Windows.Forms.TextBox();
			this.tbThreads = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.groupThreads = new System.Windows.Forms.GroupBox();
			this.radioConstant = new System.Windows.Forms.RadioButton();
			this.radioRandom = new System.Windows.Forms.RadioButton();
			this.tbOperandMax = new System.Windows.Forms.TextBox();
			this.groupOperands = new System.Windows.Forms.GroupBox();
			this.cbAllowZero = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.listboxOperators = new System.Windows.Forms.ListView();
			this.tbStats = new System.Windows.Forms.Label();
			this.groupOperators = new System.Windows.Forms.GroupBox();
			this.groupGoal = new System.Windows.Forms.GroupBox();
			this.groupThreads.SuspendLayout();
			this.groupOperands.SuspendLayout();
			this.groupOperators.SuspendLayout();
			this.groupGoal.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnFindSolution
			// 
			this.btnFindSolution.Location = new System.Drawing.Point(317, 97);
			this.btnFindSolution.Name = "btnFindSolution";
			this.btnFindSolution.Size = new System.Drawing.Size(136, 26);
			this.btnFindSolution.TabIndex = 4;
			this.btnFindSolution.Text = "Find Solution";
			this.btnFindSolution.UseCompatibleTextRendering = true;
			this.btnFindSolution.UseVisualStyleBackColor = true;
			this.btnFindSolution.Click += new System.EventHandler(this.BtnFindSolutionClick);
			// 
			// tbOutput
			// 
			this.tbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbOutput.BackColor = System.Drawing.Color.WhiteSmoke;
			this.tbOutput.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbOutput.Location = new System.Drawing.Point(0, 130);
			this.tbOutput.MaxLength = 3276700;
			this.tbOutput.Multiline = true;
			this.tbOutput.Name = "tbOutput";
			this.tbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbOutput.Size = new System.Drawing.Size(684, 327);
			this.tbOutput.TabIndex = 6;
			// 
			// tbOperandQuantity
			// 
			this.tbOperandQuantity.Location = new System.Drawing.Point(70, 16);
			this.tbOperandQuantity.MaxLength = 4;
			this.tbOperandQuantity.Name = "tbOperandQuantity";
			this.tbOperandQuantity.Size = new System.Drawing.Size(40, 20);
			this.tbOperandQuantity.TabIndex = 0;
			this.tbOperandQuantity.Text = "4";
			this.tbOperandQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(10, 19);
			this.label2.Margin = new System.Windows.Forms.Padding(0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(50, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Quantity:";
			this.label2.UseCompatibleTextRendering = true;
			// 
			// btnOpen
			// 
			this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOpen.Location = new System.Drawing.Point(605, 5);
			this.btnOpen.Name = "btnOpen";
			this.btnOpen.Size = new System.Drawing.Size(75, 23);
			this.btnOpen.TabIndex = 6;
			this.btnOpen.Text = "Open...";
			this.btnOpen.UseCompatibleTextRendering = true;
			this.btnOpen.UseVisualStyleBackColor = true;
			this.btnOpen.Click += new System.EventHandler(this.BtnOpenClick);
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.Location = new System.Drawing.Point(524, 5);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 5;
			this.btnSave.Text = "Save...";
			this.btnSave.UseCompatibleTextRendering = true;
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.BtnSaveClick);
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.DefaultExt = "txt";
			// 
			// backgroundWorker_ThreadSpawner
			// 
			this.backgroundWorker_ThreadSpawner.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_ThreadSpawner_DoWork);
			this.backgroundWorker_ThreadSpawner.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_ThreadSpawner_RunWorkerCompleted);
			// 
			// tbGoal
			// 
			this.tbGoal.Location = new System.Drawing.Point(9, 25);
			this.tbGoal.MaxLength = 10;
			this.tbGoal.Name = "tbGoal";
			this.tbGoal.Size = new System.Drawing.Size(48, 20);
			this.tbGoal.TabIndex = 0;
			this.tbGoal.Text = "20";
			this.tbGoal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(61, 63);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(70, 17);
			this.label4.TabIndex = 9;
			this.label4.Text = "TTL seconds";
			this.label4.UseCompatibleTextRendering = true;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(61, 42);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(43, 17);
			this.label5.TabIndex = 17;
			this.label5.Text = "Rounds";
			this.label5.UseCompatibleTextRendering = true;
			// 
			// tbTTL
			// 
			this.tbTTL.Location = new System.Drawing.Point(18, 60);
			this.tbTTL.MaxLength = 5;
			this.tbTTL.Name = "tbTTL";
			this.tbTTL.Size = new System.Drawing.Size(40, 20);
			this.tbTTL.TabIndex = 2;
			this.tbTTL.Tag = "";
			this.tbTTL.Text = "10";
			this.tbTTL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// tbRounds
			// 
			this.tbRounds.Location = new System.Drawing.Point(18, 39);
			this.tbRounds.MaxLength = 5;
			this.tbRounds.Name = "tbRounds";
			this.tbRounds.Size = new System.Drawing.Size(40, 20);
			this.tbRounds.TabIndex = 1;
			this.tbRounds.Text = "20";
			this.tbRounds.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// tbThreads
			// 
			this.tbThreads.Location = new System.Drawing.Point(18, 18);
			this.tbThreads.MaxLength = 5;
			this.tbThreads.Name = "tbThreads";
			this.tbThreads.Size = new System.Drawing.Size(40, 20);
			this.tbThreads.TabIndex = 0;
			this.tbThreads.Text = "2";
			this.tbThreads.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(61, 21);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(46, 17);
			this.label6.TabIndex = 19;
			this.label6.Text = "Threads";
			this.label6.UseCompatibleTextRendering = true;
			// 
			// groupThreads
			// 
			this.groupThreads.Controls.Add(this.label6);
			this.groupThreads.Controls.Add(this.tbThreads);
			this.groupThreads.Controls.Add(this.tbRounds);
			this.groupThreads.Controls.Add(this.tbTTL);
			this.groupThreads.Controls.Add(this.label5);
			this.groupThreads.Controls.Add(this.label4);
			this.groupThreads.Location = new System.Drawing.Point(317, 4);
			this.groupThreads.Name = "groupThreads";
			this.groupThreads.Size = new System.Drawing.Size(136, 87);
			this.groupThreads.TabIndex = 3;
			this.groupThreads.TabStop = false;
			this.groupThreads.Text = "Threads:";
			this.groupThreads.UseCompatibleTextRendering = true;
			// 
			// radioConstant
			// 
			this.radioConstant.Location = new System.Drawing.Point(12, 77);
			this.radioConstant.Name = "radioConstant";
			this.radioConstant.Size = new System.Drawing.Size(68, 15);
			this.radioConstant.TabIndex = 3;
			this.radioConstant.Text = "Constant";
			this.radioConstant.UseCompatibleTextRendering = true;
			this.radioConstant.UseVisualStyleBackColor = true;
			// 
			// radioRandom
			// 
			this.radioRandom.Checked = true;
			this.radioRandom.Location = new System.Drawing.Point(12, 60);
			this.radioRandom.Name = "radioRandom";
			this.radioRandom.Size = new System.Drawing.Size(65, 15);
			this.radioRandom.TabIndex = 2;
			this.radioRandom.TabStop = true;
			this.radioRandom.Text = "Random";
			this.radioRandom.UseCompatibleTextRendering = true;
			this.radioRandom.UseVisualStyleBackColor = true;
			// 
			// tbOperandMax
			// 
			this.tbOperandMax.Location = new System.Drawing.Point(70, 36);
			this.tbOperandMax.MaxLength = 4;
			this.tbOperandMax.Name = "tbOperandMax";
			this.tbOperandMax.Size = new System.Drawing.Size(40, 20);
			this.tbOperandMax.TabIndex = 1;
			this.tbOperandMax.Text = "20";
			this.tbOperandMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// groupOperands
			// 
			this.groupOperands.Controls.Add(this.cbAllowZero);
			this.groupOperands.Controls.Add(this.label3);
			this.groupOperands.Controls.Add(this.label2);
			this.groupOperands.Controls.Add(this.tbOperandQuantity);
			this.groupOperands.Controls.Add(this.radioRandom);
			this.groupOperands.Controls.Add(this.tbOperandMax);
			this.groupOperands.Controls.Add(this.radioConstant);
			this.groupOperands.Location = new System.Drawing.Point(80, 4);
			this.groupOperands.Name = "groupOperands";
			this.groupOperands.Size = new System.Drawing.Size(120, 119);
			this.groupOperands.TabIndex = 1;
			this.groupOperands.TabStop = false;
			this.groupOperands.Text = "Operands:";
			this.groupOperands.UseCompatibleTextRendering = true;
			// 
			// cbAllowZero
			// 
			this.cbAllowZero.Checked = true;
			this.cbAllowZero.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbAllowZero.Location = new System.Drawing.Point(12, 100);
			this.cbAllowZero.Name = "cbAllowZero";
			this.cbAllowZero.Size = new System.Drawing.Size(74, 15);
			this.cbAllowZero.TabIndex = 4;
			this.cbAllowZero.Text = "Allow zero";
			this.cbAllowZero.UseVisualStyleBackColor = true;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(9, 39);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(59, 13);
			this.label3.TabIndex = 11;
			this.label3.Text = "Max value:";
			// 
			// listboxOperators
			// 
			this.listboxOperators.HideSelection = false;
			listViewItem5.StateImageIndex = 0;
			this.listboxOperators.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8});
			this.listboxOperators.Location = new System.Drawing.Point(10, 18);
			this.listboxOperators.Name = "listboxOperators";
			this.listboxOperators.Size = new System.Drawing.Size(82, 78);
			this.listboxOperators.TabIndex = 0;
			this.listboxOperators.UseCompatibleStateImageBehavior = false;
			this.listboxOperators.View = System.Windows.Forms.View.SmallIcon;
			// 
			// tbStats
			// 
			this.tbStats.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.tbStats.BackColor = System.Drawing.SystemColors.Control;
			this.tbStats.Location = new System.Drawing.Point(459, 33);
			this.tbStats.Name = "tbStats";
			this.tbStats.Size = new System.Drawing.Size(221, 90);
			this.tbStats.TabIndex = 8;
			this.tbStats.Text = "TOTAL:\r\n  Equations generated: 000000\r\n  Solutions found: 000000\r\nTHIS ROUND:\r\n  " +
    "Equations generated: 000000\r\n  New solutions: 000000\r\n";
			this.tbStats.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.tbStats.UseCompatibleTextRendering = true;
			// 
			// groupOperators
			// 
			this.groupOperators.AutoSize = true;
			this.groupOperators.Controls.Add(this.listboxOperators);
			this.groupOperators.Location = new System.Drawing.Point(207, 4);
			this.groupOperators.Name = "groupOperators";
			this.groupOperators.Size = new System.Drawing.Size(103, 119);
			this.groupOperators.TabIndex = 2;
			this.groupOperators.TabStop = false;
			this.groupOperators.Text = "Operations:";
			// 
			// groupGoal
			// 
			this.groupGoal.AutoSize = true;
			this.groupGoal.Controls.Add(this.tbGoal);
			this.groupGoal.Location = new System.Drawing.Point(8, 4);
			this.groupGoal.Name = "groupGoal";
			this.groupGoal.Size = new System.Drawing.Size(65, 64);
			this.groupGoal.TabIndex = 0;
			this.groupGoal.TabStop = false;
			this.groupGoal.Text = "Goal:";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(684, 453);
			this.Controls.Add(this.groupGoal);
			this.Controls.Add(this.groupOperators);
			this.Controls.Add(this.tbStats);
			this.Controls.Add(this.groupThreads);
			this.Controls.Add(this.groupOperands);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnOpen);
			this.Controls.Add(this.tbOutput);
			this.Controls.Add(this.btnFindSolution);
			this.Name = "MainForm";
			this.Text = "Equation Finder";
			this.Shown += new System.EventHandler(this.MainForm_Shown);
			this.groupThreads.ResumeLayout(false);
			this.groupThreads.PerformLayout();
			this.groupOperands.ResumeLayout(false);
			this.groupOperands.PerformLayout();
			this.groupOperators.ResumeLayout(false);
			this.groupGoal.ResumeLayout(false);
			this.groupGoal.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnOpen;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tbOperandQuantity;
		private System.Windows.Forms.TextBox tbOutput;
		private System.Windows.Forms.Button btnFindSolution;
		private System.ComponentModel.BackgroundWorker backgroundWorker_ThreadSpawner;
		private System.Windows.Forms.TextBox tbGoal;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox tbTTL;
		private System.Windows.Forms.TextBox tbRounds;
		private System.Windows.Forms.TextBox tbThreads;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.GroupBox groupThreads;
		private System.Windows.Forms.RadioButton radioConstant;
		private System.Windows.Forms.RadioButton radioRandom;
		private System.Windows.Forms.TextBox tbOperandMax;
		private System.Windows.Forms.GroupBox groupOperands;
		private System.Windows.Forms.ListView listboxOperators;
		private System.Windows.Forms.Label tbStats;
		private System.Windows.Forms.CheckBox cbAllowZero;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupOperators;
		private System.Windows.Forms.GroupBox groupGoal;
	}
}
