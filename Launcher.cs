using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ld28_2
{
	partial class Launcher : Form
	{
		public Game Game;

		public Launcher()
		{
			InitializeComponent();
		}

		private void aa_Scroll(object sender, EventArgs e)
		{
			aaInd.Text = aa.Value + "";
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Game = new Game((uint)aa.Value, hq.Checked);
			Close();
		}
	}
}
