using System.Text;

namespace BuilderPatterns.Builder
{
    public class BuilderPattern
    {
        public static void Run()
        {
            var builder = new HtmlBuilder("ul");
            builder.AddChild("li", "Hello");
            builder.AddChild("li", "World");

            Console.WriteLine(builder.ToString());
        }
    }

    internal class HtmlElement
    {
        public string Text { get; set; }
        public string Name { get; set; }
        public List<HtmlElement> Elements { get; set; }
        private const int indentSize = 2;

        public HtmlElement()
        {
            Elements = new List<HtmlElement>();
        }

        public HtmlElement(string name, string text)
        {
            Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
            Text = text ?? throw new ArgumentNullException(paramName: nameof(text));
            Elements = new List<HtmlElement>();
        }

        private string ToStringImpl(int indent)
        {
            var stringBuilder = new StringBuilder();
            var indentation = new string(' ', indentSize * indent);
            stringBuilder.AppendLine($"{indentation}<{Name}>");

            if (!string.IsNullOrWhiteSpace(Text))
            {
                stringBuilder.Append(new string(' ', indentSize * indent + 1));
                stringBuilder.AppendLine(Text);
            }

            foreach (var element in Elements)
            {
                stringBuilder.Append(element.ToStringImpl(indent + 1));
            }

            stringBuilder.AppendLine($"{indentation}</{Name}>");

            return stringBuilder.ToString();
        }

        public override string ToString()
        {
            return ToStringImpl(0);
        }
    }

    internal class HtmlBuilder
    {
        private readonly string rootName;
        HtmlElement root = new HtmlElement();

        public HtmlBuilder(string rootName)
        {
            this.rootName = rootName;
            root.Name = rootName;
        }

        public void AddChild(string childName, string childText)
        {
            var element = new HtmlElement(childName, childText);
            root.Elements.Add(element);
        }

        public override string ToString()
        {
            return root.ToString();
        }

        public void Clear()
        {
            root = new HtmlElement { Name = rootName };
        }
    }
}
