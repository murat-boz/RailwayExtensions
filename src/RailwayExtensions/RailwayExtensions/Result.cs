using System;
using System.Collections.Generic;
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
        public Exception Exception { get; private set; } = null;

        protected Result(
            bool isSuccess,
            string error)
        {
            this.IsSuccess = isSuccess;
            this.Error = error;
        }

        protected Result(
            bool isSuccess,
            string error,
            Exception exception)
        {
            this.IsSuccess = isSuccess;
            this.Error = error;
            this.Exception = exception;
        }

        public static Result Failure(string errorMessage)
        {
            return new Result(false, errorMessage);
        }

        public static Result Failure(string errorMessage, Exception exception)
        {
            return new Result(false, errorMessage, exception);
        }

        public static Result Ok()
        {
            return new Result(true, string.Empty);
        }

        private static Result Map(
            bool isSuccess,
            string error,
            Exception exception)
        {
            return new Result(isSuccess, error, exception);
        }

        private static Result<T> Map<T>(
            T value,
            bool isSuccess,
            string error,
            Exception exception)
        {
            return new Result<T>(value, isSuccess, error, exception);
        }

        public static Result<T> Failure<T>(string errorMessage)
        {
            return new Result<T>(default(T), false, errorMessage);
        }

        public static Result<T> Failure<T>(T value, string errorMessage)
        {
            return new Result<T>(value, false, errorMessage);
        }

        public static Result<T> Failure<T>(string errorMessage, Exception exception)
        {
            return new Result<T>(default(T), false, errorMessage, exception);
        }

        public static Result<T> Failure<T>(T value, string errorMessage, Exception exception)
        {
            return new Result<T>(value, false, errorMessage, exception);
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
                        : Result.Failure("Failed when creating.", result.Exception);
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
            List<Exception> exceptions = new List<Exception>();

            if (!aggregateErrorMessages)
            {
                return Result.Combine(results);
            }

            foreach (Result result in results)
            {
                if (result.IsFailure)
                {
                    stringBuilder.Append(result.Error);
                    stringBuilder.Append("\r\n");

                    exceptions.Add(result.Exception);
                }
            }

            var errorMessages = stringBuilder.ToString();

            if (string.IsNullOrEmpty(errorMessages) || exceptions.Count > 0)
            {
                return Result.Failure(errorMessages, new AggregateException(errorMessages, exceptions));
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
                    return Result.Failure<T>(result.Error, result.Exception);
                }
            }

            return Result.Ok<T>(results.LastOrDefault().Value);
        }

        public static Result<T> Combine<T>(bool aggregateErrorMessages = false, params Result<T>[] results)
        {
            StringBuilder stringBuilder = new StringBuilder();
            List<Exception> exceptions = new List<Exception>();

            if (!aggregateErrorMessages)
            {
                return Result.Combine(results);
            }

            foreach (Result result in results)
            {
                if (result.IsFailure)
                {
                    stringBuilder.Append(result.Error);
                    stringBuilder.Append("\r\n");

                    exceptions.Add(result.Exception);
                }
            }

            var errorMessages = stringBuilder.ToString();

            if (string.IsNullOrEmpty(errorMessages) || exceptions.Count > 0)
            {
                return Result.Failure<T>(errorMessages, new AggregateException(errorMessages, exceptions));
            }

            return Result.Ok<T>(default(T));
        }

        public static Result<T> Try<T>(Func<T> func, Func<Exception, string> errorHandler = null)
        {
            try
            {
                return Result.Ok(func());
            }
            catch (Exception ex)
            {
                return Result.Failure<T>(errorHandler(ex) == null
                    ? ex.Message
                    : errorHandler(ex),
                    ex);
            }
        }

        public async static Task<Result<T>> TryAsync<T>(Func<Task<T>> func, Func<Exception, string> errorHandler = null)
        {
            try
            {
                return Result.Ok(await func());
            }
            catch (Exception ex)
            {
                return Result.Failure<T>(errorHandler(ex) == null
                    ? ex.Message
                    : errorHandler(ex),
                    ex);
            }
        }

        public static Result Try(Action action, Func<Exception, string> errorHandler = null)
        {
            try
            {
                action();

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Failure(errorHandler(ex) == null
                    ? ex.Message
                    : errorHandler(ex),
                    ex);
            }
        }

        public static async Task<Result> TryAsync(Func<Task> func, Func<Exception, string> errorHandler = null)
        {
            try
            {
                await func();

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Failure(errorHandler(ex) == null
                    ? ex.Message
                    : errorHandler(ex),
                    ex);
            }
        }

        public static Result<T> Map<T>(T value, Result result)
        {
            return Result.Map<T>(value, result.IsSuccess, result.Error, result.Exception);
        }
    }
}