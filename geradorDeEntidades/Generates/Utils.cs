namespace geradorDeEntidades.Generates;
public abstract class Utils
{
    public string FirstCharToUpper(string input)
    {
        if (String.IsNullOrEmpty(input))
            throw new ArgumentException("Insira um nome válido");
        return input.First().ToString().ToUpper() + input.Substring(1);
    }

    public string ClearChar(string input)
    {
        if (String.IsNullOrEmpty(input))
            throw new ArgumentException("Insira um nome válido");
        var split = input.Split("-");
        for (int i = 0; i < split.Length; i++)
        {
            split[i] = FirstCharToUpper(split[i]);
        }
        return string.Join("", split);
    }

    public string NotClearFirstChar(string input)
    {
        if (String.IsNullOrEmpty(input))
            throw new ArgumentException("Insira um nome válido");
        var split = input.Split("-");
        for (int i = 1; i < split.Length; i++)
        {
            split[i] = FirstCharToUpper(split[i]);
        }
        return string.Join("", split);
    }
}
