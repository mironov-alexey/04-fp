using System;

namespace Composition.Monads
{
	public static class Maybe
	{
		public static Maybe<T> FromValue<T>(T value)
		{
			return new Maybe<T>(null, value);
		}

		public static Maybe<T> FromError<T>(Exception e)
		{
            return new Maybe<T>(e, default(T));
		}

		public static Maybe<T> Result<T>(Func<T> f)
		{
            try
            {
                return FromValue(f());
            }
            catch (Exception e)
            {
                return FromError<T>(e);
            }
		}

        public static Maybe<TRes2> SelectMany<T, TRes, TRes2>(this Maybe<T> m, Func<T, TRes> func, Func<T, TRes, TRes2> func2)
		{
			return m.Success ? Result(() => func2(m.Value, func(m.Value))) : FromError<TRes2>(m.Error);
		}

		public static Maybe<TRes> SelectMany<T, TRes>(this Maybe<T> m, Func<T, TRes> map)
		{
			return m.Success ? Result(() => map(m.Value)) : FromError<TRes>(m.Error);
		}
    }

    public class Maybe<T>
	{
		public Maybe(Exception error, T value)
		{
			Error = error;
			Value = value;
		}

		public Exception Error { get; private set; }
		public T Value { get; private set; }
		public bool Success => Error == null;
//
//	    public Maybe<TRes2> SelectMany<TRes, TRes2>(Func<T, TRes> func, Func<T, TRes, TRes2> func1)
//	    {
////	        var res1 = Maybe.Result<TRes>(() => func(Value));
////	        return Maybe.Result<TRes2>(() => func1(Value, res1.Value));
//        }
//
//	    public Maybe<TRes> SelectMany<TRes>(Func<T, TRes> func)
//	    {
////	        return Maybe.Result(() => func(Value));
//	    }
	}
}
