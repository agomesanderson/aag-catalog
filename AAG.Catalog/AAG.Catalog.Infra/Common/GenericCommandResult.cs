using AAG.Catalog.Infra.Common.Contracts;

namespace AAG.Catalog.Infra.Common;

public abstract class GenericCommandResult : ICommandResult
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public int StatusCode { get; set; }
}

public abstract class GenericCommandResult<TData> : GenericCommandResult, ICommandResult<TData>
{
    public TData? Data { get; set; }
    public IEnumerable<ErrorItem>? Errors { get; set; }
}

public readonly record struct GenericResult(int StatusCode, string? Message = "");