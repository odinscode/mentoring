using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SampleSupport;

namespace SampleQueries
{
    static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			List<SampleHarness> harnesses = new List<SampleHarness>();

            var linqHarness = new LinqSamples();

            harnesses.Add(linqHarness);

            Application.EnableVisualStyles();
				
			using (SampleForm form = new SampleForm("HomeWork - Vyacheslav Odinokov", harnesses))
			{
				form.ShowDialog();
			}
		}
    }
}