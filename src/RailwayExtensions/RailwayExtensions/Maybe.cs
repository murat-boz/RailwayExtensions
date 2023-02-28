using System;
using System.Diagnostics.CodeAnalysis;

namespace RailwayExtensions
{
    public struct Maybe<T> : IEquatable<Maybe<T>> 
        where T : class
    {
        private readonly T value;

        public T Value
        {
            get
            {
                if (this.HasNoValue)
                {
                    throw new InvalidOperationException();
                }

                return value; 
            }
        }

        public bool HasValue => this.value != null;
        public bool HasNoValue => !this.HasNoValue;

        private Maybe([AllowNull] T value)
        {
            this.value = value;
        }

        public static implicit operator Maybe<T>([AllowNull] T value)
        {
            return new Maybe<T>(value);
        }

        public static bool operator ==(Maybe<T> maybe, T value)
        {
            if (maybe.HasNoValue)
            {
                return false;
            }

            return maybe.Value.Equals(value);
        }

        public static bool operator !=(Maybe<T> maybe, T value)
        { 
            return !(maybe == value);
        }

        public static bool operator ==(Maybe<T> first, Maybe<T> second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(Maybe<T> first, Maybe<T> second)
        {
            return !(first == second);
        }

        public override bool Equals(object obj)
        {
            if(!(obj is Maybe<T>))
            {
                return false;
            }

            var other = (Maybe<T>)obj;

            return this.Equals(other);
        }

        public bool Equals(Maybe<T> other)
        {
            if(this.HasNoValue && other.HasNoValue)
            {
                return true;
            }

            if(this.HasNoValue || other.HasNoValue)
            {
                return false;
            }

            return this.Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public override string ToString()
        {
            if (this.HasNoValue)
            {
                return "No value";
            }

            return this.Value.ToString();
        }

        public static Maybe<T> From(T obj)
        {
            return new Maybe<T>(obj);
        }
    }
}
