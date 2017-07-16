using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ActivateGPUS1UI
{
  class Program
    {

        private static readonly String _path = @"F:\Tera\S1UI\";
        static void Main(string[] args)
        {
            var countPattern = 0;
            var countFiles = 0;
            var filesWithoutMatchs = new List<String>();
            var allFiles = Directory.GetFiles(_path, "*.gpk", SearchOption.AllDirectories);
            foreach (String file in allFiles)
            {
                countFiles++;
                var baseName = BitConverter.ToString(Encoding.ASCII.GetBytes(file.Replace(".gpk", "").Replace("S1UI_", ""))).Replace("-", "");
                string data = BitConverter.ToString(File.ReadAllBytes(file)).Replace("-", "");
                //var GPUPattern = new Regex("474758.{"+30*2+"}"+baseName+".*2E7467614411)");
                //var GPUPattern = "2E7467614411(.)";*
                var GPUPattern = "474658.{30,400}4411(?<key>.).{7}";

                var listMatch = Regex.Matches(data, GPUPattern);
                if (listMatch.Count == 0) {
                    filesWithoutMatchs.Add(file);
                }
                foreach ( Match match in listMatch)
                {
                    Byte gpuParameters = Convert.ToByte(match.Groups[1].Value);
                    gpuParameters |= 4;
                    gpuParameters |= 2;
                    Debug.WriteLine(gpuParameters.ToString("X1"));
                    data = data.Remove(match.Groups[1].Index, 1).Insert(match.Groups[1].Index, gpuParameters.ToString("X1"));
                    Debug.WriteLine(match.Groups[1].Index);
                   // data.
                   // Debug.WriteLine(match.Groups[0].Value);
                    countPattern++;
                }

                File.WriteAllBytes(file, data.StringToByteArray());


            }

            Debug.WriteLine("total count: files = "+ countFiles+ " ; pattern = " + countPattern);
            Debug.WriteLine(String.Join(", ", filesWithoutMatchs.ToArray()));


        }
    }
}
