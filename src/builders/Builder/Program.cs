using System.Text;

var builder = new HtmlBuilder("ul");
builder.AddChild("li", "hello");
builder.AddChild("li", "world");

Console.WriteLine(builder.ToString());

public class HtmlElement
{
    public string Name, Text;
    public List<HtmlElement> Elements = [];
    private const int indentSize = 2;

    public HtmlElement()
    {

    }

    public HtmlElement(string name, string text)
    {
        Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
        Text = text ?? throw new ArgumentNullException(paramName: nameof(text));
    }

    private string ToStringImpl(int indent)
    {
        var stringBuilder = new StringBuilder();
        var identation = new string(' ', indentSize * indent);
        stringBuilder.Append($"{identation}<{Name}>");

        if (!string.IsNullOrEmpty(Text))
        {
            stringBuilder.Append(new string(' ', indentSize * (indent + 1)));
            stringBuilder.AppendLine(Text);
        }

        foreach (var element in Elements)
        {
            stringBuilder.Append(element.ToStringImpl(indent + 1));
        }

        stringBuilder.Append($"{identation}</{Name}>");

        return stringBuilder.ToString();
    }

    public override string ToString()
    {
        return ToStringImpl(0);
    }
}

public class HtmlBuilder
{
    private readonly string rootName;
    HtmlElement root = new();

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