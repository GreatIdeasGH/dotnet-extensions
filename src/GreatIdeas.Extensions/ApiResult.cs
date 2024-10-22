namespace GreatIdeas.Extensions;

/// <summary>
/// API response with status and message
/// </summary>
public class ApiResult
{
    public bool IsSuccessful { get; set; }
    public string Message { get; set; } = string.Empty;
}

/// <summary>
/// Return list of results from operation.
/// </summary>
/// <typeparam name="T">Type to return</typeparam>
public class ApiResults<T> : ApiResult
{
    public IEnumerable<T> Results { get; set; } = [];
}

/// <summary>
/// Return result from operation.
/// </summary>
public class ApiResult<T> : ApiResult
{
    public T? Result { get; set; }
}
