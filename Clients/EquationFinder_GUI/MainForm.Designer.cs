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
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Addition");
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Subtraction");
			System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Multiplication");
			System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("Division");
			System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("Exponentiation");
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
			this.lblMaxOrConstantValue = new System.Windows.Forms.Label();
			this.listboxOperators = new System.Windows.Forms.ListView();
			this.tbStats = new System.Windows.Forms.Label();
			this.groupOperators = new System.Windows.Forms.GroupBox();
			this.groupGoal = new System.Windows.Forms.GroupBox();
			this.labelGoalPredicate = new System.Windows.Forms.Label();
			this.radioGoalDivisibleaBy = new System.Windows.Forms.RadioButton();
			this.radioGoalEqual = new System.Windows.Forms.RadioButton();
			this.btnTest = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.groupThreads.SuspendLayout();
			this.groupOperands.SuspendLayout();
			this.groupOperators.SuspendLayout();
			this.groupGoal.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnFindSolution
			// 
			this.btnFindSolution.Location = new System.Drawing.Point(380, 96);
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
			this.tbOutput.Location = new System.Drawing.Point(0, 139);
			this.tbOutput.MaxLength = 3276700;
			this.tbOutput.Multiline = true;
			this.tbOutput.Name = "tbOutput";
			this.tbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbOutput.Size = new System.Drawing.Size(944, 187);
			this.tbOutput.TabIndex = 6;
			this.tbOutput.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbOutput_KeyUp);
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
			this.label2.Location = new System.Drawing.Point(17, 19);
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
			this.btnOpen.Location = new System.Drawing.Point(865, 5);
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
			this.btnSave.Location = new System.Drawing.Point(784, 5);
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
			this.tbGoal.Location = new System.Drawing.Point(66, 62);
			this.tbGoal.MaxLength = 10;
			this.tbGoal.Name = "tbGoal";
			this.tbGoal.Size = new System.Drawing.Size(57, 20);
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
			this.groupThreads.Location = new System.Drawing.Point(380, 3);
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
			this.radioConstant.CheckedChanged += new System.EventHandler(this.radioConstant_CheckedChanged);
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
			this.tbOperandMax.MaxLength = 5;
			this.tbOperandMax.Name = "tbOperandMax";
			this.tbOperandMax.Size = new System.Drawing.Size(40, 20);
			this.tbOperandMax.TabIndex = 1;
			this.tbOperandMax.Text = "20";
			this.tbOperandMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// groupOperands
			// 
			this.groupOperands.Controls.Add(this.cbAllowZero);
			this.groupOperands.Controls.Add(this.lblMaxOrConstantValue);
			this.groupOperands.Controls.Add(this.label2);
			this.groupOperands.Controls.Add(this.tbOperandQuantity);
			this.groupOperands.Controls.Add(this.radioRandom);
			this.groupOperands.Controls.Add(this.tbOperandMax);
			this.groupOperands.Controls.Add(this.radioConstant);
			this.groupOperands.Location = new System.Drawing.Point(112, 3);
			this.groupOperands.Name = "groupOperands";
			this.groupOperands.Size = new System.Drawing.Size(120, 119);
			this.groupOperands.TabIndex = 1;
			this.groupOperands.TabStop = false;
			this.groupOperands.Text = "Operands:";
			this.groupOperands.UseCompatibleTextRendering = true;
			// 
			// cbAllowZero
			// 
			this.cbAllowZero.AutoSize = true;
			this.cbAllowZero.Checked = true;
			this.cbAllowZero.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbAllowZero.Location = new System.Drawing.Point(12, 100);
			this.cbAllowZero.Name = "cbAllowZero";
			this.cbAllowZero.Size = new System.Drawing.Size(74, 17);
			this.cbAllowZero.TabIndex = 4;
			this.cbAllowZero.Text = "Allow zero";
			this.cbAllowZero.UseVisualStyleBackColor = true;
			// 
			// lblMaxOrConstantValue
			// 
			this.lblMaxOrConstantValue.Location = new System.Drawing.Point(2, 39);
			this.lblMaxOrConstantValue.Name = "lblMaxOrConstantValue";
			this.lblMaxOrConstantValue.Size = new System.Drawing.Size(67, 13);
			this.lblMaxOrConstantValue.TabIndex = 11;
			this.lblMaxOrConstantValue.Text = "Max value:";
			this.lblMaxOrConstantValue.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// listboxOperators
			// 
			this.listboxOperators.HideSelection = false;
			listViewItem1.StateImageIndex = 0;
			this.listboxOperators.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5});
			this.listboxOperators.Location = new System.Drawing.Point(10, 18);
			this.listboxOperators.Name = "listboxOperators";
			this.listboxOperators.Scrollable = false;
			this.listboxOperators.Size = new System.Drawing.Size(82, 95);
			this.listboxOperators.TabIndex = 0;
			this.listboxOperators.UseCompatibleStateImageBehavior = false;
			this.listboxOperators.View = System.Windows.Forms.View.SmallIcon;
			// 
			// tbStats
			// 
			this.tbStats.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.tbStats.BackColor = System.Drawing.SystemColors.Control;
			this.tbStats.Location = new System.Drawing.Point(719, 31);
			this.tbStats.Name = "tbStats";
			this.tbStats.Size = new System.Drawing.Size(221, 92);
			this.tbStats.TabIndex = 8;
			this.tbStats.Text = "TOTAL:\r\n  Equations generated: 0\r\n  Solutions found: 0\r\nTHIS ROUND:\r\n  Equations " +
    "generated: 0\r\n  New solutions: 0";
			this.tbStats.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.tbStats.UseCompatibleTextRendering = true;
			// 
			// groupOperators
			// 
			this.groupOperators.Controls.Add(this.listboxOperators);
			this.groupOperators.Location = new System.Drawing.Point(3, 3);
			this.groupOperators.Name = "groupOperators";
			this.groupOperators.Size = new System.Drawing.Size(103, 119);
			this.groupOperators.TabIndex = 2;
			this.groupOperators.TabStop = false;
			this.groupOperators.Text = "Operations:";
			// 
			// groupGoal
			// 
			this.groupGoal.AutoSize = true;
			this.groupGoal.Controls.Add(this.labelGoalPredicate);
			this.groupGoal.Controls.Add(this.radioGoalDivisibleaBy);
			this.groupGoal.Controls.Add(this.radioGoalEqual);
			this.groupGoal.Controls.Add(this.tbGoal);
			this.groupGoal.Location = new System.Drawing.Point(238, 3);
			this.groupGoal.Name = "groupGoal";
			this.groupGoal.Size = new System.Drawing.Size(136, 119);
			this.groupGoal.TabIndex = 0;
			this.groupGoal.TabStop = false;
			this.groupGoal.Text = "Goal:";
			// 
			// labelGoalPredicate
			// 
			this.labelGoalPredicate.Location = new System.Drawing.Point(9, 65);
			this.labelGoalPredicate.Name = "labelGoalPredicate";
			this.labelGoalPredicate.Size = new System.Drawing.Size(53, 13);
			this.labelGoalPredicate.TabIndex = 3;
			this.labelGoalPredicate.Text = "≡ 0,  mod";
			this.labelGoalPredicate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// radioGoalDivisibleaBy
			// 
			this.radioGoalDivisibleaBy.AutoSize = true;
			this.radioGoalDivisibleaBy.Checked = true;
			this.radioGoalDivisibleaBy.Location = new System.Drawing.Point(15, 19);
			this.radioGoalDivisibleaBy.Name = "radioGoalDivisibleaBy";
			this.radioGoalDivisibleaBy.Size = new System.Drawing.Size(86, 17);
			this.radioGoalDivisibleaBy.TabIndex = 2;
			this.radioGoalDivisibleaBy.TabStop = true;
			this.radioGoalDivisibleaBy.Text = "is divisible by";
			this.radioGoalDivisibleaBy.UseVisualStyleBackColor = true;
			this.radioGoalDivisibleaBy.CheckedChanged += new System.EventHandler(this.radioGoalPredicate_CheckedChanged);
			// 
			// radioGoalEqual
			// 
			this.radioGoalEqual.AutoSize = true;
			this.radioGoalEqual.Location = new System.Drawing.Point(15, 39);
			this.radioGoalEqual.Name = "radioGoalEqual";
			this.radioGoalEqual.Size = new System.Drawing.Size(73, 17);
			this.radioGoalEqual.TabIndex = 1;
			this.radioGoalEqual.Text = "is equal to";
			this.radioGoalEqual.UseVisualStyleBackColor = true;
			this.radioGoalEqual.CheckedChanged += new System.EventHandler(this.radioGoalPredicate_CheckedChanged);
			// 
			// btnTest
			// 
			this.btnTest.Location = new System.Drawing.Point(522, 3);
			this.btnTest.Name = "btnTest";
			this.btnTest.Size = new System.Drawing.Size(32, 23);
			this.btnTest.TabIndex = 9;
			this.btnTest.Text = "{T}";
			this.btnTest.UseVisualStyleBackColor = true;
			this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.groupOperators);
			this.panel1.Controls.Add(this.btnTest);
			this.panel1.Controls.Add(this.groupOperands);
			this.panel1.Controls.Add(this.groupGoal);
			this.panel1.Controls.Add(this.groupThreads);
			this.panel1.Controls.Add(this.btnFindSolution);
			this.panel1.Location = new System.Drawing.Point(5, 5);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(564, 128);
			this.panel1.TabIndex = 10;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(944, 322);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.tbStats);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnOpen);
			this.Controls.Add(this.tbOutput);
			this.MinimumSize = new System.Drawing.Size(960, 300);
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
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
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
		private System.Windows.Forms.Label lblMaxOrConstantValue;
		private System.Windows.Forms.GroupBox groupOperators;
		private System.Windows.Forms.GroupBox groupGoal;
		private System.Windows.Forms.Button btnTest;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label labelGoalPredicate;
		private System.Windows.Forms.RadioButton radioGoalDivisibleaBy;
		private System.Windows.Forms.RadioButton radioGoalEqual;
	}
}
