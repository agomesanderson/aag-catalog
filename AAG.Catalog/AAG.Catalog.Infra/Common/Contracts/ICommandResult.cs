﻿namespace AAG.Catalog.Infra.Common.Contracts;

public interface ICommandResult
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public int StatusCode { get; set; }
}

public interface ICommandResult<TData> : ICommandResult
{
    public TData? Data { get; set; }
    public IEnumerable<ErrorItem>? Errors { get; set; }
}
