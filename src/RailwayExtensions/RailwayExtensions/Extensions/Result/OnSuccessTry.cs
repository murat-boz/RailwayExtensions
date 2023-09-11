﻿using System;

namespace RailwayExtensions.Extensions
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Execute <paramref name="func"/> in try catch function only if success
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns>Return new failure <see cref="Result{TOut}"/> if failure, otherwise return new ok <see cref="Result{TOut}"/></returns>
        public static Result<TOut> OnSuccessTry<TIn, TOut>(
            this Result<TIn> result,
            Func<TIn, TOut> func, 
            Func<Exception, string> errorHandler = null)
        {
            return result.IsFailure 
                ? Result.Failure<TOut>(result.Error, result.Exception) 
                : Result.Try(
                    () => func(result.Value), 
                    errorHandler);
        }

        /// <summary>
        /// Execute <paramref name="action"/> in try catch function only if success
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="action"></param>
        /// <returns>Return incoming <see cref="Result{T}"/></returns>
        public static Result<T> OnSuccessTry<T>(
            this Result<T> result,
            Action<T> action,
            Func<Exception, string> errorHandler = null)
        {
            if (result.IsFailure)
            {
                return result;
            }

            var tryResult = Result.Try(() => action(result.Value), errorHandler);

            return tryResult.IsSuccess 
                ? Result.Ok<T>(result.Value) 
                : Result.Failure<T>(tryResult.Error, tryResult.Exception);
        }

        /// <summary>
        /// Execute <paramref name="action"/> in try catch function only if success
        /// </summary>
        /// <param name="result"></param>
        /// <param name="action"></param>
        /// <returns>Return incoming <see cref="Result"/></returns>
        public static Result OnSuccessTry(
            this Result result,
            Action action,
            Func<Exception, string> errorHandler = null)
        {
            if (result.IsFailure)
            {
                return result;
            }

            return Result.Try(() => action(), errorHandler);
        }

        /// <summary>
        /// Execute <paramref name="action"/> in try catch function only if success
        /// </summary>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns>Return <see cref="Result"/> of processed on <paramref name="func"/> if success, otherwise incoming <paramref name="result"/></returns>
        public static Result OnSuccessTry(
            this Result result,
            Func<Result> func,
            Func<Exception, string> errorHandler = null)
        {
            if (result.IsFailure)
            {
                return result;
            }

            var tryResult = Result.Try(() => func(), errorHandler);

            return tryResult.Value;
        }
    }
}