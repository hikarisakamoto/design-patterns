using BuilderPatterns.Builder;
using BuilderPatterns.FluentBuilder;
using BuilderPatterns.FluentBuilderInheritance;
using System.Text;

namespace BuilderPatterns
{
    public class Program
    {
        static void Main(string[] args)
        {
            var hello = "Hello";
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("<p>");
            stringBuilder.Append(hello);
            stringBuilder.Append("</p>");
            Console.WriteLine(stringBuilder.ToString());

            var words = new[] { "Hello", "world" };
            stringBuilder.Clear();
            stringBuilder.Append("<ul>");

            foreach (var word in words)
            {
                stringBuilder.AppendFormat("<li>{0}</li>", word);
            }
            stringBuilder.Append("</ul>");
            Console.WriteLine(stringBuilder.ToString());

            BuilderPattern.Run();

            FluentBuilderPattern.Run();

            FluentBuilderPatternInheritance.Run();

        }
    }
}
