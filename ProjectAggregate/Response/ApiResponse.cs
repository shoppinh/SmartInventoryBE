using System.Text.Json.Serialization;
using WeSpace.Core.ProjectAggregate.Constants;

namespace WeSpace.Core.ProjectAggregate.ViewModels.Response;

public class ApiResponse<T> : ApiResponse
{
    [JsonPropertyName(ApiResponsePropertyName.Data)]
    public T Data { get; set; }
}

public class ApiResponse
{
    [JsonPropertyName(ApiResponsePropertyName.IsSuccess)]
    public bool IsSuccess { get; set; }

    [JsonPropertyName(ApiResponsePropertyName.Message)]
    public string Message { get; set; }

    [JsonPropertyName(ApiResponsePropertyName.MessageCode)]
    public string MessageCode { get; set; }
}

public class ApiResponsePagedList<T>: ApiResponse
{
    [JsonPropertyName(ApiResponsePropertyName.Data)]
    public IList<T> Data { get; set; }

    [JsonPropertyName(ApiResponsePropertyName.TotalCount)]
    public int TotalCount { get; set; }

    [JsonPropertyName(ApiResponsePropertyName.PageIndex)]

    public int PageIndex { get; set; }

    [JsonPropertyName(ApiResponsePropertyName.PageSize)]

    public int PageSize { get; set; }
}