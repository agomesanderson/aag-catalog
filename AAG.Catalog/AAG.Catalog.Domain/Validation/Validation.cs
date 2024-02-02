namespace AAG.Catalog.Domain.Validation;

internal abstract class Validation
{
    internal abstract Response ValidationInput<T>(T arg);
    internal abstract Response ValidationData<T>(T arg);    
}
