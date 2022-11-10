using System;
using System.IO;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Dynamite_Charts_Tools
{
	public partial class Setting : ContentPage
	{
		public void GotoChoose(object s, EventArgs e)
		{
			Ui.LoadMainPage(new Choose_Mode());
		}
		
		public Setting()
		{
			InitializeComponent();
			
		}
		public void GetSettings_()
		{
			MaxSongInAPage.Text = Convert.ToString(Mem.GetCountOfChartsInpage());
		}
		public static void SaveMaxSongInAPage(object s,EventArgs e)
		{
			string prefix = "CountOfChartsInPage:";
			Editor sender = (Editor)s;
			string file = File.ReadAllText(Mem.appChachePth + "Settings.txt");
			string temp_0 = file.Split(prefix)[0];
			string temp_1 = file.Split(prefix)[1].Split("\n")[0];
			string temp_2 = 
			file.Split(prefix)[1]
			.Split(temp_1)[1];
			try
			{
				Convert.ToInt32(sender.Text);
			}
			catch
			{
				sender.Text = temp_1;
			}
			File.Delete(Mem.appChachePth + "Settings.txt");
			File.WriteAllText(Mem.appChachePth + "Settings.txt",temp_0 + prefix + sender.Text + temp_2);
		}
	}
}