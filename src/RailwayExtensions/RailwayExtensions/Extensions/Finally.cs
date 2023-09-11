using System;
using System.Threading.Tasks;

namespace RailwayExtensions.Extensions
{
    public static partial class ResultExtensions
    {
        public static T Finally<T>(this Result result, Func<Result, T> func)
        {
            return func(result);
        }

        public static async Task<T> Finally<T>(this Task<Result> resultTask, Func<Result, T> func)
        {
            var result = await resultTask;

            return func(result);
        }

        public static TOut Finally<TIn, TOut>(this Result<TIn> result, Func<Result<TIn>, TOut> func)
        {
            return func(result);
        }

        public static async Task<TOut> Finally<TIn, TOut>(this Task<Result<TIn>> resultTask, Func<Result<TIn>, TOut> func)
        {
            var result = await resultTask;

            return func(result);
        }
    }
}
