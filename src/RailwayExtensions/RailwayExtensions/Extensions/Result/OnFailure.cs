using System;

namespace RailwayExtensions.Extensions
{
    public static partial class ResultExtensions
    {
        public static Result OnFailure(
            this Result result, 
            Action action)
        {
            if (result.IsFailure)
            {
                action();
            }

            return result;
        }
    }
}
