namespace RailwayExtensions.Extensions
{
    public static class MaybeExtensions
    {
        public static Result<T> ToResult<T>(this Maybe<T> maybe, string errorMessage)
            where T : class
        {
            if (maybe.HasNoValue)
            {
                return Result.Failure<T>(errorMessage);
            }

            return Result.Ok(maybe.Value);
        }
    }
}
