namespace OnlineShoppingApi.Response
{
    public class ApiResponseExtions
    {
        public static ApiResponse Succsess(object? data, string message = "Succsess")
        => new ApiResponse
        {
            Status = true,
            Data = data,
            Message = message
        };
        public static ApiResponse Fail(string message, List<string> errors = null!) =>
        new ApiResponse
        {
            Status = false,
            Errors = errors ?? new List<string>(),
            Message = message
        };
    }
}