namespace CurrencyApi.Web.Dtos;

public class ResponseModel<T> where T : class
{
    public ResponseModel() : this(500, false) {}

    public ResponseModel(int statusCode, bool isSuccessful, T? data = null)
    {
        StatusCode = statusCode;
        IsSuccessful = isSuccessful;
        Data = data;
    }

    public int StatusCode { get; set; }
    
    public bool IsSuccessful { get; set; }
    
    public T? Data { get; set; }
}