using System;

namespace PicoBoards
{
    public abstract class Result<TDerived, TValue, TError>
        where TDerived : Result<TDerived, TValue, TError>
    {
        public TValue Value { get; }
        public TError Error { get; }
        public bool IsSuccessful { get; }

        protected Result(TValue value)
            => (Value, IsSuccessful) = (value, true);

        protected Result(TError value)
            => Error = value;

        public TDerived Select(Func<TValue, TDerived> mapper)
            => IsSuccessful
            ? mapper(Value)
            : (TDerived) Activator.CreateInstance(typeof(TDerived), Error);

        public TDerived SelectMany<TIntermediate, TIntermediateDerived>(
            Func<TValue, Result<TIntermediateDerived, TIntermediate, TError>> mapper,
            Func<TValue, TIntermediate, TDerived> resultGetter)
            where TIntermediateDerived : Result<TIntermediateDerived, TIntermediate, TError>
        {
            if (IsSuccessful)
            {
                var intermediate = mapper(Value);
                return intermediate.IsSuccessful
                    ? resultGetter(Value, intermediate.Value)
                    : (TDerived) Activator.CreateInstance(typeof(TDerived), intermediate.Error);
            }

            return (TDerived) Activator.CreateInstance(typeof(TDerived), Error);
        }
    }
}