using Ardalis.ApiEndpoints;
using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;

namespace SkiAnalyze.Util;

public static class ResultExtensions
{
    public static Result<T> ToResult<T, TOther>(this Result<TOther> other)
    {
        return Result<T>.Error(other.Errors.ToArray());
    }
}
