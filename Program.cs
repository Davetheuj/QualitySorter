using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMPLib;

namespace QualitySorter
{
    class Program
    {
        static int directoryCount;
        static String[] directories;
        static List<FileInfo> files = new List<FileInfo>();
        static List<FileInfo> fileDump = new List<FileInfo>();
        static List<FileInfo> filesToDelete = new List<FileInfo>();
        static bool showInfo = true;
        static List<QSFile> qsFiles = new List<QSFile>();
        static bool isRunning = true;


        static void Main(string[] args)
        {
            WindowsMediaPlayer wmp = new WindowsMediaPlayer();
            IWMPMedia mediainfo;

            Console.WriteLine("********\nThis application will print a list of files and their size/duration ratio if applicable (v 1.0.1)\n********");
            while (isRunning)
            {
                Console.WriteLine("What directory would you like to query?");

                String directories = Console.ReadLine();

                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("Press Enter to start your search...");
                Console.ReadLine();
                Console.Clear();
                Console.WriteLine($"Searching {directories}...this may take a while depending on the contents of the folders...");


                try { Directory.SetCurrentDirectory(directories); }
                catch (Exception)
                {
                    Console.WriteLine("Can't find " + directories + " folder. Try checking folder path.");

                    return;
                }
                try
                {
                    qsFiles.Clear();
                    foreach (string file in Directory.GetFiles(Environment.CurrentDirectory))
                    {


                        //files.Add(new FileInfo(file));
                        mediainfo = wmp.newMedia(file);
                        //  Console.WriteLine($"{file} - {mediainfo.duration} - {new FileInfo(file).Length} - {mediainfo.imageSourceWidth}");
                        qsFiles.Add(new QSFile(file, mediainfo.duration, new FileInfo(file).Length));



                    }
                    qsFiles = qsFiles.OrderBy(o => o.sizeDurationRatio).ToList();

                    foreach (QSFile file in qsFiles)
                    {
                        Console.WriteLine(file.name + "-" + file.sizeDurationRatio);
                    }
                }

                catch (Exception)
                {
                    return;

                }

                Console.WriteLine("\n\n Would you like to query a different directory? (y/n)");

                String response = Console.ReadLine();
                if(response.ToLower() == "n")
                {
                    isRunning = false;
                }
                else
                {
                    Console.Clear();
                }


            }


            Console.WriteLine("Thank you for using this tool! Press Enter to close this prompt!");
            Console.ReadLine();
        }
    }
}
