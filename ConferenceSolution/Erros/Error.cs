using ConferenceSolution.Enums;

namespace ConferenceSolution.Erros
{
    public class Error
    {
        public ErrorCodeType ErrorCodeType { get; private set; }

        public Error(ErrorCodeType errorCodeType)
        {
            ErrorCodeType = errorCodeType;
        }
    }
}
