namespace DataTransformer.Utils
{
  internal static class Extensions
  {
    public static string ToCsvField(this string value)
    {
      if (value == null)
        return "";
      if (value.Contains(",") || value.Contains("\"") || value.Contains("\n"))
        return $"\"{value.Replace("\"", "\"\"")}\"";
      else
        return value;
    }
  }
}
