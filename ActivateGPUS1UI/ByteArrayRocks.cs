using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

static class ByteArrayRocks
{

    public static byte[] StringToByteArray(this string hex)
    {
        return Enumerable.Range(0, hex.Length)
                         .Where(x => x % 2 == 0)
                         .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                         .ToArray();
    }


    public static string ReplaceAll(this string src, string search, string repl)
    {
        Debug.WriteLine("replace start for:" + search);
        return src.Replace(search.Replace(" ", ""), repl.Replace(" ", ""));
    }

    public static string ReplaceAll(this string src, Regex search, string repl)
    {
        var match = search.Match(src);
        Debug.WriteLine(match.Value);
        return search.Replace(src, repl.Replace(" ", ""));

    }
}