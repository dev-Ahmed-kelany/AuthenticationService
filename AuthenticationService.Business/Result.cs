

namespace AuthenticationService.Business
{
    public static class HttpStatus
    {
        public const int OK = 200;
        public const int Created = 201;
        public const int NoContent = 204;

        public const int BadRequest = 400;
        public const int Unauthorized = 401;
        public const int Forbidden = 403;
        public const int NotFound = 404;
        public const int Conflict = 409;

        public const int InternalServerError = 500;
    }

    public sealed record Error (string Code, string Description, int StatusCode)
    {
        public static readonly Error None = new("", "", 0);
    }

    public class Result
    {
        public bool IsSuccess { get; }
        public Error Error { get; }

        protected Result(bool isSuccess, Error error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        protected Result(Result result)
        {
            IsSuccess = result.IsSuccess;
            Error = result.Error;
        }

        public static Result Success() => new Result(true, Error.None);

        public static Result Failure(Error error) => new Result(false, error);
    }

    public class Result<TData> : Result
    {
        public TData? Data { get; }

        private Result(TData data) : base(true, Error.None)
        {
            Data = data;
        }

        private Result(Error error) : base(false, error)
        {
            Data = default;
        }

        public Result(Result result) : base(result)
        {
            Data = default;
        }

        public static Result<TData> Success(TData data) => new(data);

        public static Result<TData> Failure(Error error) => new(error);
    }
}
