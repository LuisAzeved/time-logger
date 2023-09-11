namespace Timelogger.DTOs
{
    public class ResultDTO<T>
    {
       
            public bool Success { get; set; }
            public T Data { get; set; }
            public string ErrorMessage { get; set; }

    }
}