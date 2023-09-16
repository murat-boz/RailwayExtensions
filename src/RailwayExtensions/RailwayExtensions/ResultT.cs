using System;
using System.Diagnostics.CodeAnalysis;

namespace RailwayExtensions
{
    public sealed class Result<T> : Result
    {
        private readonly T value;

        public T Value
        {
            get
            {
                return value == null
                    ? default(T)
                    : value;
            }
        }

        protected internal Result([AllowNull] T value, bool isSuccess, string error)
            : base(isSuccess, error)
        {
            this.value = value;
        }

        protected internal Result([AllowNull] T value, bool isSuccess, string error, Exception exception)
            : base(isSuccess, error, exception)
        {
            this.value = value;
        }
    }
}
