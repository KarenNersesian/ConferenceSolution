using ConferenceSolution.Erros;

namespace ConferenceSolution.Exceptions
{
    public class DuplicateParticipantEx : System.Exception
    {
        public ErrorInfo ErrorInfo { get; private set; }
        public DuplicateParticipantEx(ErrorInfo errorInfo, string message) : base(message)
        {
            ErrorInfo = errorInfo;
        }
    }
}
