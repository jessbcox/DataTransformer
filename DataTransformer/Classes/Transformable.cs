using System.Text;

namespace DataTransformer.Classes
{
  public interface IHasHeaders
  {
    static abstract string GetColumnHeaders();
  }


  public abstract class Transformable<T> where T : Transformable<T>, IHasHeaders
  {
    public abstract string Transform();

    public abstract override string ToString();

    public static void Write(T[] data, string filePath)
    {
      using (var writer = new StreamWriter(filePath, false, new UTF8Encoding(true)))
      {
        writer.WriteLine(T.GetColumnHeaders());

        if (data == null) return;

        foreach (var datum in data)
          writer.WriteLine(datum.Transform());
      }
    }
  }
}
