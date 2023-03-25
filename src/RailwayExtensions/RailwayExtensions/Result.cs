namespace RailwayExtensions
{
    public class Result
    {
        public bool IsSuccess { get; }
        public string Error { get; private set; }
        public bool IsFailure => !IsSuccess;

        protected Result(bool isSuccess, string error) 
        {
            this.IsSuccess = isSuccess;
            this.Error     = error;
        }

        public static Result Failure(string errorMessage)
        {
            return new Result(false, errorMessage);
        }

        public static Result Ok()
        {
            return new Result(true, string.Empty);
        }

        public static Result<T> Failure<T>(string errorMessage)
        {
            return new Result<T>(default(T), false, errorMessage);
        }

        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(value, true, string.Empty);
        }

        public static Result<T> Create<T>(T result)
        {
            return Result.Ok<T>(result);
        }

        public static Result Combine(params Result[] results)
        {
            foreach (Result result in results)
            {
                if (result.IsFailure)
                {
                    return result;
                }
            }

            return Ok();
        }
    }
}
