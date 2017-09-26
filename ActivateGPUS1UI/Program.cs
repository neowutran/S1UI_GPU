using System;
using System.IO;
using System.Text.RegularExpressions;

namespace ActivateGPUS1UI
{
  class Program
    {
        private static readonly byte FLAG_GPU = 2;
        private static readonly byte FLAG_DIRECT_BLIT = 4;

        private static string inputPath; 
        
        static void Main(string[] args)
        {
            inputPath= Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\GpkFiles\";
            if (args.Length == 1)
            {
                if (Directory.Exists(args[0].ToString()))
                {
                    inputPath = args[0].ToString();
                }
            }
            //Check/Create default folders
            Directory.CreateDirectory(inputPath);
            ConsoleOutput.InformationMessage(String.Format("Path for GPK files: {0}", inputPath));
            //Main loop
            try
            {
                var files = Directory.GetFiles(inputPath, "*.gpk", SearchOption.AllDirectories);
                ConsoleOutput.InformationMessage(String.Format("Found {0} gpk files", files.Length));
                ConsoleOutput.InformationMessage("Converting...");
                foreach (var file in files)
                {
                    var data = BitConverter.ToString(File.ReadAllBytes(file)).Replace("-", "");
                    foreach (Match match in Regex.Matches(data, "474658.{30,400}4411(?<key>.).{7}"))
                    {
                        var gpuParameters = Convert.ToByte(match.Groups[1].Value);
                        gpuParameters |= FLAG_DIRECT_BLIT;
                        gpuParameters |= FLAG_GPU;
                        data = data.Remove(match.Groups[1].Index, 1).Insert(match.Groups[1].Index, gpuParameters.ToString("X1"));
                    }
                    File.WriteAllBytes(file, data.StringToByteArray());
                }
                ConsoleOutput.InformationMessage("Done!");
            }
            catch (Exception ex)
            {
                ConsoleOutput.ErrorMessage(ex.Message);

            }
            //Stop it! "Can't see console"
            Console.ReadLine();
        }
    }
}
