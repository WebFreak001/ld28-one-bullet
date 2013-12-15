namespace ld28_2
{
	partial class Launcher
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.button1 = new System.Windows.Forms.Button();
			this.hq = new System.Windows.Forms.CheckBox();
			this.aa = new System.Windows.Forms.TrackBar();
			this.label1 = new System.Windows.Forms.Label();
			this.aaInd = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.aa)).BeginInit();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(182, 12);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(163, 84);
			this.button1.TabIndex = 0;
			this.button1.Text = "Start Game";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// hq
			// 
			this.hq.AutoSize = true;
			this.hq.Location = new System.Drawing.Point(12, 79);
			this.hq.Name = "hq";
			this.hq.Size = new System.Drawing.Size(85, 17);
			this.hq.TabIndex = 1;
			this.hq.Text = "High Quallity";
			this.hq.UseVisualStyleBackColor = true;
			// 
			// aa
			// 
			this.aa.Location = new System.Drawing.Point(12, 28);
			this.aa.Maximum = 16;
			this.aa.Name = "aa";
			this.aa.Size = new System.Drawing.Size(144, 45);
			this.aa.TabIndex = 2;
			this.aa.Scroll += new System.EventHandler(this.aa_Scroll);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(93, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Anti-Aliasing Level";
			// 
			// aaInd
			// 
			this.aaInd.Location = new System.Drawing.Point(155, 28);
			this.aaInd.Name = "aaInd";
			this.aaInd.Size = new System.Drawing.Size(21, 29);
			this.aaInd.TabIndex = 4;
			this.aaInd.Text = "0";
			this.aaInd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// Launcher
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(357, 108);
			this.Controls.Add(this.aaInd);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.aa);
			this.Controls.Add(this.hq);
			this.Controls.Add(this.button1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "Launcher";
			this.Text = "One Bullet";
			((System.ComponentModel.ISupportInitialize)(this.aa)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.CheckBox hq;
		private System.Windows.Forms.TrackBar aa;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label aaInd;
	}
}