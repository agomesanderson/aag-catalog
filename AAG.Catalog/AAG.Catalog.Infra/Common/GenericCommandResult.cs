using AAG.Catalog.Infra.Common.Contracts;
using System.Text.Json.Serialization;

namespace AAG.Catalog.Infra.Common;

public abstract class GenericCommandResult : ICommandResult
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public IEnumerable<ErrorItem>? Errors { get; set; }
    [JsonIgnore] public int StatusCode { get; set; }
}

public abstract class GenericCommandResult<TData> : GenericCommandResult, ICommandResult<TData>
{
    public TData? Data { get; set; }
}
