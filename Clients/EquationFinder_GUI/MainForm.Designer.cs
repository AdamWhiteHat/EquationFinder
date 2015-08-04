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
			System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem("Addition");
			System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem("Subtraction");
			System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem("Multiplication");
			System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem("Division");
			this.btnFindSolution = new System.Windows.Forms.Button();
			this.tbOutput = new System.Windows.Forms.TextBox();
			this.tbNumberOperations = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.btnOpen = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.backgroundWorker_ThreadSpawner = new System.ComponentModel.BackgroundWorker();
			this.tbTargetValue = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.tbTTL = new System.Windows.Forms.TextBox();
			this.tbRounds = new System.Windows.Forms.TextBox();
			this.tbThreads = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.groupThreads = new System.Windows.Forms.GroupBox();
			this.radioConstant = new System.Windows.Forms.RadioButton();
			this.radioRandom = new System.Windows.Forms.RadioButton();
			this.tbTerm = new System.Windows.Forms.TextBox();
			this.groupTerm = new System.Windows.Forms.GroupBox();
			this.listOperators = new System.Windows.Forms.ListView();
			this.label8 = new System.Windows.Forms.Label();
			this.tbStats = new System.Windows.Forms.Label();
			this.groupThreads.SuspendLayout();
			this.groupTerm.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnFindSolution
			// 
			this.btnFindSolution.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFindSolution.Location = new System.Drawing.Point(475, 5);
			this.btnFindSolution.Name = "btnFindSolution";
			this.btnFindSolution.Size = new System.Drawing.Size(138, 48);
			this.btnFindSolution.TabIndex = 0;
			this.btnFindSolution.Text = "Find Solution";
			this.btnFindSolution.UseCompatibleTextRendering = true;
			this.btnFindSolution.UseVisualStyleBackColor = true;
			this.btnFindSolution.Click += new System.EventHandler(this.BtnFindSolutionClick);
			// 
			// tbOutput
			// 
			this.tbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbOutput.BackColor = System.Drawing.Color.WhiteSmoke;
			this.tbOutput.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbOutput.Location = new System.Drawing.Point(0, 119);
			this.tbOutput.MaxLength = 3276700;
			this.tbOutput.Multiline = true;
			this.tbOutput.Name = "tbOutput";
			this.tbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbOutput.Size = new System.Drawing.Size(698, 338);
			this.tbOutput.TabIndex = 2;
			// 
			// tbNumberOperations
			// 
			this.tbNumberOperations.Location = new System.Drawing.Point(24, 66);
			this.tbNumberOperations.MaxLength = 4;
			this.tbNumberOperations.Name = "tbNumberOperations";
			this.tbNumberOperations.Size = new System.Drawing.Size(30, 20);
			this.tbNumberOperations.TabIndex = 3;
			this.tbNumberOperations.Text = "9";
			this.tbNumberOperations.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(6, 2);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(75, 17);
			this.label1.TabIndex = 4;
			this.label1.Text = "Goal:";
			this.label1.UseCompatibleTextRendering = true;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(7, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(77, 17);
			this.label2.TabIndex = 5;
			this.label2.Text = "# Operations:";
			this.label2.UseCompatibleTextRendering = true;
			// 
			// btnOpen
			// 
			this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOpen.Location = new System.Drawing.Point(619, 5);
			this.btnOpen.Name = "btnOpen";
			this.btnOpen.Size = new System.Drawing.Size(75, 23);
			this.btnOpen.TabIndex = 12;
			this.btnOpen.Text = "Open...";
			this.btnOpen.UseCompatibleTextRendering = true;
			this.btnOpen.UseVisualStyleBackColor = true;
			this.btnOpen.Click += new System.EventHandler(this.BtnOpenClick);
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.Location = new System.Drawing.Point(619, 30);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 13;
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
			// tbTargetValue
			// 
			this.tbTargetValue.Location = new System.Drawing.Point(24, 19);
			this.tbTargetValue.MaxLength = 10;
			this.tbTargetValue.Name = "tbTargetValue";
			this.tbTargetValue.Size = new System.Drawing.Size(48, 20);
			this.tbTargetValue.TabIndex = 1;
			this.tbTargetValue.Text = "727";
			this.tbTargetValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(3, 99);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(116, 17);
			this.label3.TabIndex = 6;
			this.label3.Text = "Found solutions:";
			this.label3.UseCompatibleTextRendering = true;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(60, 60);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(81, 17);
			this.label4.TabIndex = 9;
			this.label4.Text = "TTL (Seconds)";
			this.label4.UseCompatibleTextRendering = true;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(64, 38);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(83, 17);
			this.label5.TabIndex = 17;
			this.label5.Text = "Rounds";
			this.label5.UseCompatibleTextRendering = true;
			// 
			// tbTTL
			// 
			this.tbTTL.Location = new System.Drawing.Point(18, 57);
			this.tbTTL.MaxLength = 5;
			this.tbTTL.Name = "tbTTL";
			this.tbTTL.Size = new System.Drawing.Size(40, 20);
			this.tbTTL.TabIndex = 8;
			this.tbTTL.Tag = "";
			this.tbTTL.Text = "5";
			this.tbTTL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// tbRounds
			// 
			this.tbRounds.Location = new System.Drawing.Point(18, 35);
			this.tbRounds.MaxLength = 5;
			this.tbRounds.Name = "tbRounds";
			this.tbRounds.Size = new System.Drawing.Size(40, 20);
			this.tbRounds.TabIndex = 16;
			this.tbRounds.Text = "1";
			this.tbRounds.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// tbThreads
			// 
			this.tbThreads.Location = new System.Drawing.Point(18, 14);
			this.tbThreads.MaxLength = 5;
			this.tbThreads.Name = "tbThreads";
			this.tbThreads.Size = new System.Drawing.Size(40, 20);
			this.tbThreads.TabIndex = 18;
			this.tbThreads.Text = "5";
			this.tbThreads.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(60, 17);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(87, 17);
			this.label6.TabIndex = 19;
			this.label6.Text = "Threads/Round";
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
			this.groupThreads.Location = new System.Drawing.Point(202, 5);
			this.groupThreads.Name = "groupThreads";
			this.groupThreads.Size = new System.Drawing.Size(150, 81);
			this.groupThreads.TabIndex = 23;
			this.groupThreads.TabStop = false;
			this.groupThreads.Text = "Threads:";
			this.groupThreads.UseCompatibleTextRendering = true;
			// 
			// radioConstant
			// 
			this.radioConstant.Location = new System.Drawing.Point(3, 59);
			this.radioConstant.Name = "radioConstant";
			this.radioConstant.Size = new System.Drawing.Size(105, 18);
			this.radioConstant.TabIndex = 15;
			this.radioConstant.Text = "Constant value";
			this.radioConstant.UseCompatibleTextRendering = true;
			this.radioConstant.UseVisualStyleBackColor = true;
			// 
			// radioRandom
			// 
			this.radioRandom.Checked = true;
			this.radioRandom.Location = new System.Drawing.Point(4, 41);
			this.radioRandom.Name = "radioRandom";
			this.radioRandom.Size = new System.Drawing.Size(104, 18);
			this.radioRandom.TabIndex = 14;
			this.radioRandom.TabStop = true;
			this.radioRandom.Text = "Max random";
			this.radioRandom.UseCompatibleTextRendering = true;
			this.radioRandom.UseVisualStyleBackColor = true;
			// 
			// tbTerm
			// 
			this.tbTerm.Location = new System.Drawing.Point(32, 14);
			this.tbTerm.MaxLength = 4;
			this.tbTerm.Name = "tbTerm";
			this.tbTerm.Size = new System.Drawing.Size(30, 20);
			this.tbTerm.TabIndex = 10;
			this.tbTerm.Text = "9";
			this.tbTerm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// groupTerm
			// 
			this.groupTerm.Controls.Add(this.tbTerm);
			this.groupTerm.Controls.Add(this.radioRandom);
			this.groupTerm.Controls.Add(this.radioConstant);
			this.groupTerm.Location = new System.Drawing.Point(87, 5);
			this.groupTerm.Name = "groupTerm";
			this.groupTerm.Size = new System.Drawing.Size(109, 81);
			this.groupTerm.TabIndex = 22;
			this.groupTerm.TabStop = false;
			this.groupTerm.Text = "Term:";
			this.groupTerm.UseCompatibleTextRendering = true;
			// 
			// listOperators
			// 
			this.listOperators.HideSelection = false;
			listViewItem9.StateImageIndex = 0;
			this.listOperators.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem9,
            listViewItem10,
            listViewItem11,
            listViewItem12});
			this.listOperators.Location = new System.Drawing.Point(373, 20);
			this.listOperators.Name = "listOperators";
			this.listOperators.Size = new System.Drawing.Size(82, 78);
			this.listOperators.TabIndex = 24;
			this.listOperators.UseCompatibleStateImageBehavior = false;
			this.listOperators.View = System.Windows.Forms.View.SmallIcon;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(355, 1);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(99, 17);
			this.label8.TabIndex = 20;
			this.label8.Text = "Operators:";
			this.label8.UseCompatibleTextRendering = true;
			// 
			// tbStats
			// 
			this.tbStats.Location = new System.Drawing.Point(461, 66);
			this.tbStats.Name = "tbStats";
			this.tbStats.Size = new System.Drawing.Size(233, 50);
			this.tbStats.TabIndex = 25;
			this.tbStats.Text = "Expressions created (this round): 000000\r\nTotal expressions generated: 000000";
			this.tbStats.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.tbStats.UseCompatibleTextRendering = true;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(698, 453);
			this.Controls.Add(this.tbStats);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.listOperators);
			this.Controls.Add(this.groupThreads);
			this.Controls.Add(this.groupTerm);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnOpen);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tbNumberOperations);
			this.Controls.Add(this.tbOutput);
			this.Controls.Add(this.tbTargetValue);
			this.Controls.Add(this.btnFindSolution);
			this.Name = "MainForm";
			this.Text = "Equasion Finder";
			this.Shown += new System.EventHandler(this.MainForm_Shown);
			this.groupThreads.ResumeLayout(false);
			this.groupThreads.PerformLayout();
			this.groupTerm.ResumeLayout(false);
			this.groupTerm.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnOpen;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbNumberOperations;
		private System.Windows.Forms.TextBox tbOutput;
		private System.Windows.Forms.Button btnFindSolution;
		private System.ComponentModel.BackgroundWorker backgroundWorker_ThreadSpawner;
		private System.Windows.Forms.TextBox tbTargetValue;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox tbTTL;
		private System.Windows.Forms.TextBox tbRounds;
		private System.Windows.Forms.TextBox tbThreads;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.GroupBox groupThreads;
		private System.Windows.Forms.RadioButton radioConstant;
		private System.Windows.Forms.RadioButton radioRandom;
		private System.Windows.Forms.TextBox tbTerm;
		private System.Windows.Forms.GroupBox groupTerm;
		private System.Windows.Forms.ListView listOperators;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label tbStats;
	}
}
