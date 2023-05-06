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
                if (!base.IsSuccess)
                {
                    throw new InvalidOperationException();
                }

                return value;
            }
        }

        protected internal Result([AllowNull] T value, bool isSuccess, string error)
            : base(isSuccess, error)
        {
            this.value = value;
        }
    }
}
