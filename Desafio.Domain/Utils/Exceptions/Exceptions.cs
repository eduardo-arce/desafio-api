using System.Net;

namespace Desafio.Domain.Utils.Exceptions
{
    public class Exceptions
    {
        public abstract class HttpException : Exception
        {
            public HttpStatusCode StatusCode { get; }

            protected HttpException(HttpStatusCode statusCode, string message) : base(message)
            {
                StatusCode = statusCode;
            }
        }

        public class BadRequestException : HttpException
        {
            public BadRequestException(string message)
                : base(HttpStatusCode.BadRequest, message) { }
        }

        public class UnauthorizedException : HttpException
        {
            public UnauthorizedException(string message)
                : base(HttpStatusCode.Unauthorized, message) { }
        }

        public class ForbiddenException : HttpException
        {
            public ForbiddenException(string message)
                : base(HttpStatusCode.Forbidden, message) { }
        }

        public class NotFoundException : HttpException
        {
            public NotFoundException(string message)
                : base(HttpStatusCode.NotFound, message) { }
        }

        public class InternalServerErrorException : HttpException
        {
            public InternalServerErrorException(string message)
                : base(HttpStatusCode.InternalServerError, message) { }
        }
    }
}
