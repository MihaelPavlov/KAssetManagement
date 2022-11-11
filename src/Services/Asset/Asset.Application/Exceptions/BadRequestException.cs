namespace Asset.Application.Exceptions
{
    internal class BadRequestException : ApplicationException
    {
        public BadRequestException(string message)
            : base(message)
        {

        }
    }
}
