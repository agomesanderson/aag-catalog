namespace AAG.Catalog.Domain.Validation;

internal readonly record struct Response(bool Success, string? Message = "");