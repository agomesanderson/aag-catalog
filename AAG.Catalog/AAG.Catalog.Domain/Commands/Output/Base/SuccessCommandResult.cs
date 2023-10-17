using AAG.Catalog.Infra.Common;
using System.Text.Json.Serialization;

namespace AAG.Catalog.Domain.Commands.Output.Base;

public class SuccessCommandResult : GenericCommandResult
{
    [JsonIgnore] public new IEnumerable<ErrorItem>? Errors { get; set; }

    public SuccessCommandResult(int statusCode = 200)
    {
        Success = true;
        StatusCode = statusCode;
    }

    public SuccessCommandResult(string message, int statusCode = 200)
    {
        Message = message;
        Success = true;
        StatusCode = statusCode;
    }
}

public class SuccessCommandResult<TData> : GenericCommandResult<TData>
{
    [JsonIgnore] public new IEnumerable<ErrorItem>? Errors { get; set; }


    public SuccessCommandResult(int statusCode = 200)
    {
        Success = true;
        StatusCode = statusCode;
    }

    public SuccessCommandResult(TData data, string message, int statusCode = 200)
    {
        Data = data;
        Message = message;
        Success = true;
        StatusCode = statusCode;
    }
}
