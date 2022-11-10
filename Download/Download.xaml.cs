using System;
using System.IO;//文件相关命名空间
using System.Net;//Webclient所在命名空间
using System.Text;//Encoding类所在命名空间
using System.Threading.Tasks;//异步编程命名空间
using System.Collections.Generic;//List所在命名空间
using Xamarin.Forms;

namespace Dynamite_Charts_Tools
{
	public partial class Download : ContentPage
	{
		public static LabelP select;
		public static List<LabelP> DownloadList = new List<LabelP>();
		public static string chart;
		public Download()
		{
			InitializeComponent();
			DrawUi(new object(), new EventArgs());
		}
		
		public void GotoChoose(object s, EventArgs e)
		{
			Ui.LoadMainPage(new Choose_Mode());
		}
		public LabelP GetLabelP(int time)
		{
			string appCachePath = Mem.path + Mem.appChachePth;
			if (Directory.Exists(appCachePath)==false)
			{
					Directory.CreateDirectory(Mem.path +  Mem.appChachePth);
			}
			if (File.Exists(appCachePath + "rena_index_list")==false)
			{
				Chart.GetRena();
			}
			string SongInfomation = 
			File.ReadAllText(appCachePath + "rena_index_list")
			.Split("\nB.")[time];
			
			LabelP lbl = new LabelP();
			lbl.Infomation=SongInfomation;
			
			lbl.Text = 
			Mem.GetString(SongInfomation,"N?");
			TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
			
			tapGestureRecognizer.Tapped += (s, e) =>
			{
				LabelP tempLbl = (LabelP)s;
				chart = tempLbl.Text;
				select=tempLbl;
				this.ACTDownloadListButton.IsEnabled=true;
				this.ACTDownloadListButton.Text="添加 " + chart + " 至下载列表";
    	};
    	
    	lbl.GestureRecognizers.Add(tapGestureRecognizer);
			
			return lbl;
		}
		
		public async void DownloadCharts(object s , EventArgs e)
		{
			if (DownloadList.Count<1)
			{
				goto End;
			}
			string AllSongInfo = await Task.Run(() => File.ReadAllText(Mem.appChachePth + "rena_index_list"));
			for(int i=0;i<DownloadList.Count;i++)
			{
				
				Chart chartFile=new Chart(DownloadList[i].Infomation);
				
				if(Directory.Exists(Mem.chartPath + chartFile.FloderName)==false)
				{
					Directory.CreateDirectory(Mem.chartPath + chartFile.FloderName);
				}
				
				
				WebClient wc = new WebClient();
				
				string tempPath = Mem.chartPath;
				if(false == File.Exists(tempPath+chartFile.SongName))
				{
					byte[] song = await Task.Run(() => wc.DownloadData(Mem.downloadAddress + chartFile.SongName));
					File.WriteAllBytes
					(
						tempPath 
						+ 
						chartFile.SongName
						,
						song
					);
				}
				if(false == File.Exists(tempPath+chartFile.SongPictureName))
				{
					byte[] picture = await Task.Run(() => wc.DownloadData(Mem.downloadAddress + chartFile.SongPictureName));
					File.WriteAllBytes
					(
						tempPath 
						+ 
						chartFile.SongPictureName
						,
						picture
					);
				}
				if(false == File.Exists(tempPath+chartFile.SongPreviewName))
				{
					byte[] preview = await Task.Run(() => wc.DownloadData(Mem.downloadAddress + chartFile.SongPreviewName));
					File.WriteAllBytes
					(
						tempPath 
						+ 
						chartFile.SongPreviewName
						,
						preview
					);
				}
				
				for(int o=0;o<chartFile.ChartDifficulties.Count;o++)
				{
					if(false == File.Exists(tempPath+chartFile.ChartDifficulties[o].ChartPath))
					{
						byte[] chart=await Task.Run( () =>
							wc.DownloadData
							(
								Mem.downloadAddress 
								+
								chartFile.ChartDifficulties[o].ChartPath
							)
						);
						File.WriteAllBytes
						(
							tempPath
							+ 
							chartFile.ChartDifficulties[o].ChartPath
							,
							chart
						);
					}
				}
				wc.Dispose();
				
			}
			End:
			DisplayAlert ("", "下载完成", "是");
		}
		
		public void ACTDownloadList(object s , EventArgs e)
		{
			Button a = s as Button;
			if (a.Text!="已添加")
			{
				DownloadList.Add(select);
				a.Text="已添加";
				if(DownloadList.Count>0)
				{
					DownloadButton.IsEnabled=true;
				}
			}
		}
		public async void DrawUi(object s,EventArgs e)
		{
			for(int i = Mem.Min; i < Mem.Max;i++)
			{
				this.stackLayout.Children.Add(await Task.Run(() => GetLabelP(i)));
				//非异步模式this.stackLayout.Children.Add(GetLabelP(i));
			}
		}
		public void NextChartsPage(object s,EventArgs e)
		{
			if(Mem.Max+Mem.CountOfChartsInPage<File.ReadAllText(Mem.appChachePth + "rena_index_list").Split("\nB.").Length)
			{
			Mem.Max+=Mem.CountOfChartsInPage;
			Mem.Min+=Mem.CountOfChartsInPage;
			}
			else
			{
				return;
			}
			this.stackLayout.Children.Clear();
			DrawUi(new object(),new EventArgs());
		}
		public void LastChartsPage(object s,EventArgs e)
		{
			if(Mem.Min-Mem.CountOfChartsInPage>0)
			{
			Mem.Max-=Mem.CountOfChartsInPage;
			Mem.Min-=Mem.CountOfChartsInPage;
			}
			else
			{
				return;
			}
			stackLayout.Children.Clear();
			DrawUi(new object(),new EventArgs());
		}
	public class Mem
	{
		
		//file为要提取的文件源
		//str为要提取的东西
		//例如：
		//file.txt↓
		//str
		//result
		//abc
		//12#3
		public static string GetString(string file,string str)
		{
			string result = 
			file
			.Split(str)[1]
			.Split("\n")[0];
			return result;
		}
		public static string downloadAddress = "https://gh.api.99988866.xyz/https://raw.githubusercontent.com/EDTA-gif/dynamite-charts-repository/main/Charts/";
		
		public static string rena_DownloadAddress = "https://gh.api.99988866.xyz/https://raw.githubusercontent.com/EDTA-gif/dynamite-charts-repository/main/rena_index_list";
		
		
		
		
		public static int CountOfChartsInPage;
		
		public static int Min = 1;
		
		private static int _max = 0;


		public static int GetCountOfChartsInpage()
		{
			if (File.Exists(appChachePth+ "Settings.txt") == false)
			{
				return 20;
			}
			else
			{
				string settings = File.ReadAllText(Mem.appChachePth + "Settings.txt") + "\n";
				string Count = GetString(settings,"CountOfChartsInPage:");
				return Convert.ToInt32(Count);
			}
		}
		
		public static int Max
		{
			get
			{
				return _max;
			}
			set 
			{
				if(value > ChartsCountInList)
				{
					_max = ChartsCountInList;
				}
				else
				{
					_max = value;
				}
			}
		}
		public static void SetCountInList()
		{
		  ChartsCountInList = File.ReadAllText(Mem.appChachePth + "rena_index_list").Split("\nB.").Length - 1;
		}
		
		private static int ChartsCountInList = 0;
		public static int time = 1;
		public static string path = "./";
    	
   	public static string chartPath = "/sdcard/Android/data/tech.dynami.dynamite/files/";
    	
   	public static string appChachePth = "./Files/";
	}
	public class Difficulty
	{
		public string Number;
		public string ChartPath;
		public string DifficultyName;
	}
	public class Chart
	{
		public Chart(string chartInfomation)
		{
			this.GetChartFile(chartInfomation);
		}
		public string ChartName;
		public string FloderName;
		public string SongName;
		public string SongPreviewName;
		public string SongPictureName;
		public string CharterName;
		public string MusicianName;
		public string Describe;
		
		public List<Difficulty> ChartDifficulties = new List<Difficulty>();
	 
		private void GetChartFile(string songInfomation)
		{
			this.SongName = 
			Mem.GetString(songInfomation,"S?");
			
			this.SongPictureName =
			Mem.GetString(songInfomation,"C?");
			
			this.SongPreviewName = 
			Mem.GetString(songInfomation,"P?");
			
			this.FloderName =
			SongPreviewName.Split('/')[0];
			
			string Temp0=Mem.GetString(songInfomation,"H?");
			string Temp1=Mem.GetString(songInfomation,"M?");
			if(Temp0[Temp0.Length-1]==';')
			{
				Temp0=Temp0.Substring(0,Temp0.Length-1);
			}
			if(Temp1[Temp1.Length-1]==';')
			{
				Temp1=Temp1.Substring(0,Temp1.Length-1);
			}
			string[] DifficultiesTemp=Temp0.Split(';');
			string[] ChartNamesTemp=Temp1.Split(';');

			for
			(
				int i=0;
				i<ChartNamesTemp.Length;
				i++
			)
			{
				Difficulty difficulty=new Difficulty();
				difficulty.Number=DifficultiesTemp[i].Split(",")[1];
				
				difficulty.DifficultyName=DifficultiesTemp[i].Split(",")[0];
				
				difficulty.ChartPath=ChartNamesTemp[i];
				
				
				this.ChartDifficulties.Add(difficulty);
			}
		}
			
		public static void GetRena()
		{
	    WebClient wc = new WebClient();
      wc.DownloadFile
      (
      Mem.rena_DownloadAddress
      ,
      Mem.appChachePth + "rena_index_list"
      );
		}
	}
}
}