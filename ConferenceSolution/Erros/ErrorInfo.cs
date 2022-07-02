namespace ConferenceSolution.Erros
{
    public class ErrorInfo : Error
    {
        public string Message { get; private set; }
        public ErrorInfo(string message, Enums.ErrorCodeType errorCodeType) : base(errorCodeType) 
        {
            Message = message;
        }
    }
}
