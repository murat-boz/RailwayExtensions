﻿using System;

namespace RailwayExtensions.Extensions
{
    public static partial class ResultExtensions
    {
        public static Result Bind<TIn>(
            this Result<TIn> result,
            Func<TIn, Result> func)
        {
            if (result.IsFailure)
            {
                return Result.Failure(result.Error);
            }

            return func(result.Value);
        }

        public static Result<TOut> Bind<TIn, TOut>(
            this Result<TIn> result,
            Func<TIn, Result<TOut>> func)
        {
            if (result.IsFailure)
            {
                return Result.Failure<TOut>(result.Error);
            }

            return func(result.Value);
        }
    }
}
