using System.Threading.Tasks;
using System;

namespace RailwayExtensions.Extensions
{
    public static partial class ResultExtensions
    {
        public static Result OnBothTry(
            this Result result, 
            Action action,
            Func<Exception, string> errorHandler = null)
        {
            Result.Try(action, errorHandler);

            return result;
        }

        public static Result OnBothTry(
            this Task<Result> resultTask, 
            Action action,
            Func<Exception, string> errorHandler = null)
        {
            var result = resultTask.Result;

            return result.OnBothTry(action, errorHandler);
        }

        public static Result OnBothTry(
            this Result result, 
            Action<string> action, 
            string message,
            Func<Exception, string> errorHandler = null)
        {
            Result.Try(() => action(message), errorHandler);

            return result;
        }

        public static Result OnBothTry(
            this Task<Result> resultTask, 
            Action<string> action, 
            string message,
            Func<Exception, string> errorHandler = null)
        {
            var result = resultTask.Result;

            return result.OnBothTry(action, message, errorHandler);
        }

        public static Result<T> OnBothTry<T>(
            this Result<T> result, 
            Action<T> action,
            Func<Exception, string> errorHandler = null)
        {
            Result.Try(() => action(result.Value), errorHandler);

            return result;
        }

        public static Result<T> OnBothTry<T>(
            this Task<Result<T>> resultTask,
            Action<T> action,
            Func<Exception, string> errorHandler = null)
        {
            var result = resultTask.Result;

            return result.OnBothTry<T>(action, errorHandler);
        }

        public static Result<TOut> OnBothTry<TIn, TOut>(
            this Result<TIn> result, 
            Func<TIn, TOut> func,
            Func<Exception, string> errorHandler = null)
        {
            var resultFunc = Result.Try(() => func(result.Value), errorHandler);

            return Result.Map<TOut>(resultFunc.Value, result);
        }

        public static Result<TOut> OnBothTry<TIn, TOut>(
            this Task<Result<TIn>> resultTask, 
            Func<TIn, TOut> func,
            Func<Exception, string> errorHandler = null)
        {
            var result = resultTask.Result;

            return result.OnBothTry<TIn, TOut>(func, errorHandler);
        }
    }
}
