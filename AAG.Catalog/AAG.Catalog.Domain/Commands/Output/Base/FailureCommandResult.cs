using AAG.Catalog.Infra.Common;
using System.Text.Json.Serialization;

namespace AAG.Catalog.Domain.Commands.Output.Base;

public class FailureCommandResult : GenericCommandResult
{
    public FailureCommandResult()
    {
        Success = false;
    }

    public FailureCommandResult(string message, IEnumerable<ErrorItem> errors, int statusCode = 422)
    {
        Message = message;
        Success = false;
        Errors = new List<ErrorItem>(errors);
        StatusCode = statusCode;
    }

    public FailureCommandResult(string message, ErrorItem error, int statusCode = 422)
    {
        Message = message;
        Success = false;
        StatusCode = statusCode;

        Errors = new List<ErrorItem>
            {
                error
            };
    }

    public FailureCommandResult(string message, int statusCode = 422)
    {
        Message = message;
        Success = false;
        Errors = new List<ErrorItem>();
        StatusCode = statusCode;
    }
}

public class FailureCommandResult<TData> : GenericCommandResult<TData>
{
    [JsonIgnore] public new TData? Data { get; set; }

    public FailureCommandResult()
    {
        Success = false;
    }

    public FailureCommandResult(string message, IEnumerable<ErrorItem> errors, int statusCode = 422)
    {
        Message = message;
        Success = false;
        Errors = new List<ErrorItem>(errors);
        StatusCode = statusCode;
    }

    public FailureCommandResult(string message, ErrorItem error, int statusCode = 422)
    {
        Message = message;
        Success = false;
        StatusCode = statusCode;

        Errors = new List<ErrorItem>
            {
                error
            };
    }

    public FailureCommandResult(string message, int statusCode = 422)
    {
        Message = message;
        Success = false;
        StatusCode = statusCode;
        Errors = new List<ErrorItem>();
    }
}
