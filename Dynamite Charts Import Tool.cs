using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Net;
namespace Dynamite_Chart
{
    public class Program
    {
    	public static void GetRena()
		{
			if (File.Exists("./rena_index_list") == false && (File.Exists("./rena_index_list") == false))
			{
			//若未找到谱面信息文件，自动下载
	                WebClient wc = new WebClient();
                        wc.DownloadFile
                        (
                        "https://gh.api.99988866.xyz/https://raw.githubusercontent.com/EDTA-gif/dynamite-charts-repository/main/rena_index_list"
                        //以上为github的加速站点地址
                        ,
                         @"rena_index_list"
                         );
                        //空格和缩进混着用。。。：一个字体一个样，凎
			}
		}
		
        static string path = "./";
        public static void Main()
        {
            //新手，不是很熟练，一堆代码没写注释请，见谅
            string[] floders = GetFloder(path).ToArray<string>();//获取程序主目录下的文件夹
            GetRena();//下载谱面信息，若已存在，则跳过，不存在则下载一遍
            Console.WriteLine("正在写入。。。");
            WriteRena(floders);//写入信息
            Console.WriteLine("写入成功！\n按任意键继续:");
            Console.ReadKey();
        }
        public static List<string> GetFloder(string _path)
        {
            string[] _foreach = Directory.GetDirectories(_path);
            List<string> Files = new List<string>();
            foreach
            (
                string name in _foreach
            //将name作为数组 _foreach 的第"循环次数"个
            //比如
            //_foreach={aaa,bbb,ccc}
            //当前循环次数：0
            //name：aaa
            )
            {
                Files.Add(name.Substring(path.Length));
            }
            return Files;
        }
        
        public static List<string> GetFiles(string _path)
        {
            string[] _foreach = Directory.GetFiles(_path);
            List<string> files = new List<string>();
            for (int i = 0; i < _foreach.Length; i++)
            {
                files.Add(_foreach[i].Substring(_path.Length));
            }
            return files;
        }

        public static void WriteRena(string[] Floders)
        {
            //string[] files = Program.ToArray(GetFloder(path));
            Rena[] _rena = new Rena[Floders.Length];
            for (int i = 0; i < Floders.Length; i++)
            {
            	string[] _files = (GetFiles(path + Floders[i] + "/").ToArray<string>());
            	//_files为临时变量
                _rena[i] = GetInfo(_files, path + Floders[i]);
            }
            //以上代码用作获取rena
            
            if (File.Exists(path + "__rena_index_2")){File.Delete(path + "__rena_index_2");}
            
            FileStream _FsWrite = new FileStream(path + "__rena_index_2", FileMode.CreateNew, FileAccess.Write);
			string write = "";//声明一个不为空的string变量
			//write用作写入文件（将要写入的内容都会存储在write里）
			
            for (int i = 0; i < _rena.Length || i < Floders.Length; i++)
            {/*Floders.Length和_rena.Length其实是同一个数字*/
            	start:;
            	if (mirror(File.Exists(_rena[i].S))||mirror(File.Exists(_rena[i].P)))
            	{
            		//如果未检测到_rena内拥有内容，那么直接跳过此次循环
            		if (i+1>=_rena.Length)
            		{
            			continue;
            		}
            		i++;
            		goto start;
            	}
                
                write += "B." + _rena[i].B + "\r\n";
                write += "R?" + 1 + "\r\n";
                write += "N?" + Floders[i] + "\r\n";
                write += "S?" + _rena[i].S + "\r\n";
                write += "C?" + _rena[i].C + "\r\n";
                write += "P?" + _rena[i].P + "\r\n";
                write += "U?" + _rena[i].U + "\r\n";
                write += "W?" + _rena[i].W + "\r\n";
                write += "I?" + _rena[i].I + "\r\n" + "H?";
				write += _rena[i].H;
                write += "\r\n" + "M?";
                if (_rena[i].Casual) { write += _rena[i].CasualPath; }
                if (_rena[i].Normal) { write += _rena[i].NormalPath; }
                if (_rena[i].Hard) { write += _rena[i].HardPath; }
                if (_rena[i].Mega) { write += _rena[i].MegaPath; }
                if (_rena[i].Giga) { write += _rena[i].GigaPath; }
                if (_rena[i].Tera) { write += _rena[i].TeraPath; }
                //byte[] _byte = Encoding.UTF8.GetBytes(write);
            	//_FsWrite.Write(_byte, 0, _byte.Length);
                write+="\r\nE.\r\n\r\n";
            }
            byte[] _byte = Encoding.UTF8.GetBytes(write);
            _FsWrite.Write(_byte, 0, _byte.Length);
            _FsWrite.Close();
        }
        
        public static string GetRandom16(int length)//用于获得指定长度的16进制随机数
        {
            string _return = "";
            int a;
            Random _random = new Random();
            for (int i = 0; i < length; i++)
            {
                a = _random.Next(16);
                switch (a)
                {
                    case 1:
                        _return += "1";
                        break;
                    case 2:
                        _return += "2";
                        break;
                    case 3:
                        _return += "3";
                        break;
                    case 4:
                        _return += "4";
                        break;
                    case 5:
                        _return += "5";
                        break;
                    case 6:
                        _return += "6";
                        break;
                    case 7:
                        _return += "7";
                        break;
                    case 8:
                        _return += "8";
                        break;
                    case 9:
                        _return += "9";
                        break;
                    case 10:
                        _return += "A";
                        break;
                    case 11:
                        _return += "B";
                        break;
                    case 12:
                        _return += "C";
                        break;
                    case 13:
                        _return += "D";
                        break;
                    case 14:
                        _return += "E";
                        break;
                    case 15:
                        _return += "F";
                        break;
                    case 0:
                        _return += "0";
                        break;
                }
            }//也可以用余数将int转为16进制
            return _return;
        }

        public static bool mirror(bool _bool)
        {
            bool mirror = _bool == false;
            return mirror;
        }
        
        public static Rena GetInfo(string[] files, string _path)
        {
            _path = _path.Substring(path.Length);
            Rena _rena = new Rena();
            
                for (int a = 0; a < files.Length; a++)
                {
                    if (files[a].Contains(".jpg"))
                    {
                        _rena.C = _path + "/"  + files[a];
                    }


                    if (files[a].Contains("Casual") || files[a].Contains("CASUAL") || files[a].Contains("casual"))
                    {

                        _rena.CasualPath = _path + "/" + files[a] + ";";
                    }

                    if (files[a].Contains("Normal")||files[a].Contains("NORMAL")||files[a].Contains("normal"))
                    {
                    _rena.Normal = true;
                    _rena.NormalPath = _path + "/" + files[a] + ";";
                    }

                    if (files[a].Contains("Hard")||files[a].Contains("HARD")||files[a].Contains("hard"))
                    {
                        _rena.Hard = true;
                        _rena.HardPath = _path + "/" + files[a] + ";";
                    }
                    if (files[a].Contains("Mega")||files[a].Contains("MEGA")||files[a].Contains("mega"))
                    {
                        _rena.Mega = true;
                        _rena.MegaPath = _path + "/" + files[a] + ";";
                    }
                    if (files[a].Contains("Giga") || files[a].Contains("GIGA") || files[a].Contains("giga"))
                    {   
                    _rena.Giga = true;
                    _rena.GigaPath = _path + "/" + files[a] + ";";
                    }
                    if (files[a].Contains("Tera") || files[a].Contains("TERA") || files[a].Contains("tera"))
                    {   
                    _rena.Tera = true;
                    _rena.TeraPath = _path + "/" + files[a] + ";";
                    }
                    
                    if (files[a].Contains("_preview.mp3"))
                    {
                    _rena.P = _path + "/" + files[a];
                    }
                    if (mirror(files[a].Contains("_preview.mp3")) && files[a].Contains(".mp3"))
                    {
                    _rena.S = _path + "/" + files[a];
                    }
                    
            }
             
             	byte[] temp = File.ReadAllBytes("./rena_index_list");
				string str = Encoding.UTF8.GetString(temp,0,temp.Length);
				int renaP_ = str.IndexOf("P?" + _rena.P);
				string temp_1 = "";
				if (_rena.Casual)
				{
					temp_1="M?"+_rena.CasualPath;
				}
				else
				{
					if (_rena.Normal)
					{	
					temp_1="M?"+_rena.NormalPath;
					}
					else
					{
						if (_rena.Hard)
						{
							temp_1="M?"+_rena.HardPath;
						}
						else
						{
							if (_rena.Mega)
							{
								temp_1="M?"+_rena.MegaPath;
							}
							else
							{
								if (_rena.Giga)
								{
									temp_1="M?"+_rena.GigaPath;
								}
								else
								{
									if (_rena.Tera)
									{
										temp_1="M?"+_rena.TeraPath;
									}
									else
									{
										//throw new Exception();
										//摆烂
									}
								}
							}
						}
					}
				}
				string U_W_I_H="";
				int renaM_ = str.IndexOf(temp_1);
				try
				{
				U_W_I_H = str.Substring(renaP_ + _rena.P.Length + 2, renaM_ - renaP_ - _rena.P.Length - 2 );
				}
				catch
				{
				_rena.B = GetRandom16(24);
                _rena.R = "1";
                _rena.U = "谱师名";
                _rena.W = "曲师名";
                _rena.I = "简介";
                _rena.H = "";
                if (_rena.Casual) { _rena.H+= "Casual,1;"; }
                if (_rena.Normal) { _rena.H += "Normal,5;"; }
                if (_rena.Hard) { _rena.H += "Hard,10;"; }
                if (_rena.Mega) { _rena.H += "Mega,12;"; }
                if (_rena.Giga) { _rena.H += "Giga,15;"; }
                if (_rena.Tera) { _rena.H += "Tera,15;"; }
                return _rena;
        		}
        		
				string[] U_W_I_H_2 = new string[4];
				U_W_I_H_2[0] = U_W_I_H.Split("W?")[0].Substring(3);
				U_W_I_H_2[1] = U_W_I_H.Split("W?")[1].Split("I?")[0];
				U_W_I_H_2[2] = U_W_I_H.Split("I?")[1].Split("H?")[0];
				U_W_I_H_2[3] = U_W_I_H.Split("H?")[1];
				_rena.U = U_W_I_H_2[0].Substring(0,U_W_I_H_2[0].Length-1);
				_rena.W = U_W_I_H_2[1].Substring(0,U_W_I_H_2[1].Length-1);
				_rena.I = U_W_I_H_2[2].Substring(0,U_W_I_H_2[2].Length-1);
				_rena.H = U_W_I_H_2[3].Substring(0,U_W_I_H_2[3].Length-1);

                _rena.B = GetRandom16(24);
                _rena.R = "1";
                /*_rena.U = "谱师名";
                _rena.W = "曲师名";
                _rena.I = "简介";*/

            return _rena;
        }

        public static string[] ToArray(List<string> list)
        {
            string[] _return = new string[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                _return[i] = list[i];
            }
            return _return;
        }
    }

    public struct Rena
    {
        public string B;
        public string R;
        public string N;//歌曲名
        public string S;//歌曲的文件名
        public string C;//谱面背景图片
        public string P;//谱面音乐预览
        public string U;//谱师名称
        public string W;//曲师名称
        public string I;//谱面简介
        
        public bool Casual;
        public string CasualPath;
        
        public bool Normal;
        public string NormalPath;
        
        public bool Hard;
        public string HardPath;
        
        public bool Mega;
        public string MegaPath;
        
        public bool Giga;
        public string GigaPath;
        
        public bool Tera;
        public string TeraPath;
        
        public string E;
        public string H;//谱面定数
    }
}
