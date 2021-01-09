using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Patcher
{
  class Patcher
  {
    const string verpatch = "verpatch.exe";
    static void Main(string[] args)
    {
      if (args.Length != 3)
      {
        Console.WriteLine("Please provide directory, filename and version. Eg:");
        Console.WriteLine("patcher.exe mydirectory assembly.dll 1.2.3.4");
        return;
      }

      if (!File.Exists(verpatch))
      {
        Console.WriteLine("verpatch.exe not found");
        return;
      }

      var directory = args[0];
      var filename = args[1];
      var version = args[2];

      if (!Directory.Exists(directory))
      {
        Console.WriteLine("Directory does not exist.");
        return;
      }

      var files = Directory.EnumerateFiles(directory, filename, SearchOption.AllDirectories);

      if (!files.Any())
      {
        Console.WriteLine("No matching files found.");
        return;
      }

      foreach (var file in files)
      {
        System.Diagnostics.Process.Start(verpatch, $"{file} {version}");
        Console.WriteLine($"Patched {file} {version}");
      }
    }
  }
}
