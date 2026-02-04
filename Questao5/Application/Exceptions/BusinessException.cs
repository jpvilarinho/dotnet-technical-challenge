namespace Questao5.Application.Exceptions
{
    public class BusinessException : HttpRequestException
    {
        public string Code { get; }

        public BusinessException(string code, string message) : base(message)
        {
            Code = code;
        }
    }
}
