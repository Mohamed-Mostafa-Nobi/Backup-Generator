using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Data.Text;
using Windows.Devices.Geolocation;
//using Windows.UI.Xaml.Shapes;

namespace Automation_Tool
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var now = DateTime.Now;

            string backupTime = now.ToString("[yyyy-MM-dd_hh-mm-ss tt]");

            string textpath = @"C:\Users\pc\Desktop\Backup Generator.txt";

            string foldernames = "";
            string filenames = "";
            int foldernum = 0;
            int filenum = 0;

            foreach (var item in File.ReadAllLines(textpath))
            {

                if (Path.HasExtension(item))
                {
                    string calcname = Path.GetFileName(item);

                    filenum += 1;  

                    filenames += Path.GetFileName(item) + " &";

                    string path = Path.GetDirectoryName(item);

                    string name = Path.GetFileNameWithoutExtension(item);

                    string ext = Path.GetExtension(item);

                    string backupname = name + backupTime + ext;

                    string backupnamepath = path + "\\" + name + backupTime + ext; 

                   File.Copy(item, backupnamepath, true);

                }
                else
                {
                    var dirctories = Directory.GetDirectories(item,"*.*",SearchOption.AllDirectories);

                    string calcname = Path.GetDirectoryName(item);

                    foldernames += Path.GetFileName(item) + " &";

                    foldernum += 1;

                    string dirPath = item + backupTime;

                    Directory.CreateDirectory(dirPath);

                    
                    foreach (var dir in dirctories)
                    {
                        string path = dir.Replace(item, dirPath);

                        Directory.CreateDirectory(path);

                    }

                    var files = Directory.GetFiles(item, "*.*", SearchOption.AllDirectories);

                    foreach (var file in files)
                    {
                        string path = file.Replace(item, dirPath);

                        FileInfo ss = new FileInfo(file);

                        ss.CopyTo(path);
                    }
                }


            }

            string backupedfolders = "";
            string backupedfiles = "";

            if (foldernum > 0)
            {
                backupedfolders = foldernames.Remove(foldernames.Length - 1);
            }
         
            
            if (filenum > 0)
            {
                 backupedfiles = filenames.Remove(filenames.Length - 1);
            }
            

            ToastContentBuilder notif = new ToastContentBuilder();
            notif.AddText("Backup Generator:");    // Title 
            notif.AddText($"The files in the Backup Generator have neen backed up");
            notif.AddText($"Files : {backupedfiles}\nFolders :{backupedfolders}");
            notif.AddInlineImage(new Uri(Path.GetFullPath(".\\KAITECH Logo.png")));
            notif.Show();


            Console.WriteLine(); 

        }



        #region MyRegion


        /// <summary>
        /// <summary>
        /// <summary>

        static int Letters(string sen, char c)
        {

            string sentence = sen.ToLower();
            var a = sentence.Split(c);
            Console.WriteLine("The Number Of {0} letters is = {1}", c, a.Length - 1);



            return a.Length - 1;

        }



        /// <summary>
        /// 
        /// 
        static int PositiveInt()
        {

            string x = Console.ReadLine();

            while (!int.TryParse(x, out _) || (int.TryParse(x, out _) && int.Parse(x) < 0))
            {
                Console.WriteLine("Please, Enter a Valid Number");
                x = Console.ReadLine();
            }

            return int.Parse(x);

        }

        static int IntInput()
        {

            string x = Console.ReadLine();

            while (!int.TryParse(x, out _))
            {
                Console.WriteLine("Please, Enter a Valid Number");
                x = Console.ReadLine();
            }

            return int.Parse(x);

        }

        #endregion
        
    }

}
