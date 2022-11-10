using System;
using System.IO;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace Dynamite_Charts_Tools
{
	public partial class Choose_Mode : ContentPage
	{
		public Choose_Mode()
		{
			InitializeComponent();

		}
		public async void Open_YIXIAODIANCC_S_Space(object s, EventArgs e)
		{
			await Browser.OpenAsync
			(
				/*【YIXIAODIANCC的个人空间-哔哩哔哩】*/
				"https://b23.tv/zGhin14"
			);
		}
		public void GotoSetting(object s, EventArgs e)
		{
			if (Directory.Exists(Mem.appChachePth) == false)
			{
				Directory.CreateDirectory(Mem.appChachePth);
			}
			if(File.Exists(Mem.appChachePth + "Settings.txt") == false)
			{
				File.WriteAllText(Mem.appChachePth + "Settings.txt","CountOfChartsInPage:20");
			}
			Setting st = new Setting();
			st.GetSettings_();
			Ui.LoadMainPage(st);
		}
		public void GotoDownload(object s, EventArgs e)
		{
			if (Directory.Exists(Mem.appChachePth) == false)
			{
				Directory.CreateDirectory(Mem.appChachePth);
			}
			if (File.Exists(Mem.appChachePth + "rena_index_list") == false)
			{
				Chart.GetRena();
			}
			if(File.Exists(Mem.appChachePth + "Settings.txt") == false)
			{
				File.WriteAllText(Mem.appChachePth + "Settings.txt","CountOfChartsInPage:20");
			}
			Mem.SetCountInList();
			Mem.Min = 1;
			Mem.Max = Mem.GetCountOfChartsInpage() + Mem.Min;
			Mem.CountOfChartsInPage = Mem.GetCountOfChartsInpage();
			Ui.LoadMainPage(new Download());
		}
	}
}