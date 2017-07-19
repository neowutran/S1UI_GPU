using System;
using System.IO;
using System.Text.RegularExpressions;

namespace ActivateGPUS1UI
{
  class Program
    {
        private static readonly byte FLAG_GPU = 2;
        private static readonly byte FLAG_DIRECT_BLIT = 4;
        private static readonly String PATH = @"F:\Tera\S1UI\";

        static void Main(string[] args)
        {
            foreach (var file in Directory.GetFiles(PATH, "*.gpk", SearchOption.AllDirectories))
            {
                var data = BitConverter.ToString(File.ReadAllBytes(file)).Replace("-", "");
                foreach ( Match match in Regex.Matches(data, "474658.{30,400}4411(?<key>.).{7}"))
                {
                    var gpuParameters = Convert.ToByte(match.Groups[1].Value);
                    gpuParameters |= FLAG_DIRECT_BLIT;
                    gpuParameters |= FLAG_GPU;
                    data = data.Remove(match.Groups[1].Index, 1).Insert(match.Groups[1].Index, gpuParameters.ToString("X1"));
                }
                File.WriteAllBytes(file, data.StringToByteArray());
            }
        }
    }
}
