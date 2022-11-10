using System;
using System.IO;
using Xamarin.Forms;

namespace Dynamite_Charts_Tools
{
	public partial class App : Application
	{
		public App()
		{
			MainPage = new Choose_Mode();
			InitializeComponent();
		}
		
	}
}