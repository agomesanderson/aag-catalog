namespace AAG.Catalog.Infra.Common;

public class ErrorItem
{
    public string Name { get; set; }
    public string Message { get; set; }

    public ErrorItem(string name, string message)
    {
        Name = name;
        Message = message;
    }
}