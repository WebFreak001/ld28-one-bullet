using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ld28_2
{
	static class Program
	{
		/// <summary>
		/// Der Haupteinstiegspunkt für die Anwendung.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Launcher l = new Launcher();
			l.ShowDialog();
			if(l.Game != null)
			l.Game.Start();
		}
	}
}
