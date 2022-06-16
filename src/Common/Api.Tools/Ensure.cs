namespace Api.Tools;

public static class Ensure
{
    public static void IsValidId(int id, string name = "id")
    {
        if (id <= 0)
            throw new ArgumentException("The provided identifier is not valid", name);
    }

    public static void IsNotNull(object parameter, string name)
    {
        if (parameter == null)
            throw new ArgumentNullException(name);
    }

    public static void IsNotNullOrEmpty(string parameter, string name)
    {
        if (string.IsNullOrWhiteSpace(parameter))
            throw new ArgumentNullException(name);
    }
}