using DataTransformer.Classes;
using System.Text.Json;

namespace DataTransformer
{
  internal class Program
  {
    static void Main(string[] args)
    {
      string dataFile = @"C:\Users\jescox\Downloads\gf_entry_6-export-2026-10-07.json";
      string resultFile = @"C:\Users\jescox\Downloads\test.csv";
      string jsonStr = File.ReadAllText(dataFile);

      var json = JsonSerializer.Deserialize<RequestAQuote[]>(jsonStr) ?? [];

      RequestAQuote.Write(json, resultFile);
    }
  }
}
