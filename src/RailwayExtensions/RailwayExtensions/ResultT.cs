using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace RailwayExtensions
{
    public class Result<T> : Result
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
