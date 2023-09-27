namespace geradorDeEntidades;
public class Property
{
    public string Name { get; private set; }
    public string Type { get; private set; }

    public Property(string name, string type)
    {
        Name = name;
        Type = type;
    }
}
