namespace PriceList.Contracts;

public class BaseResponse
{
    public bool Success { get; set; }

    public string Message { get; set; }

    public static BaseResponse GetSuccessResponse()
    {
        return new BaseResponse
        {
            Success = true
        };
    }

    public static BaseResponse GetSuccessResponse(string message)
    {
        return new BaseResponse
        {
            Success = true,
            Message = message
        };
    }

    public static BaseResponse GetErrorResponse(string message)
    {
        return new BaseResponse
        {
            Message = message
        };
    }
}