using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async static Task<Result<T>> CreateAsync<T>(T result)
        {
            return Result.Ok<T>(result);
        }

        public async static Task<Result> CreateAsync()
        {
            return Result.Ok();
        }

        public async static Task<Result> CreateAsync(Result result)
        {
            return result.IsSuccess 
                        ? Result.Ok(result) 
                        : Result.Failure("Failed when creating.");
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

            return Result.Ok();
        }

        public static Result Combine(bool aggregateErrorMessages = false, params Result[] results)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (Result result in results)
            {
                if (result.IsFailure)
                {
                    stringBuilder.Append(result.Error);
                    stringBuilder.Append("\r\n");
                }
            }

            var errorMessages = stringBuilder.ToString();

            if (string.IsNullOrEmpty(errorMessages))
            {
                return Result.Failure(errorMessages);
            }

            return Result.Ok();
        }

        /// <summary>
        /// Check all result value and return the success process of the last <paramref name="results"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="results"></param>
        /// <returns></returns>
        public static Result<T> Combine<T>(params Result<T>[] results)
        {
            foreach (Result result in results)
            {
                if (result.IsFailure)
                {
                    return Result.Failure<T>(result.Error);
                }
            }

            return Result.Ok<T>(results.LastOrDefault().Value);
        }
    }
}
