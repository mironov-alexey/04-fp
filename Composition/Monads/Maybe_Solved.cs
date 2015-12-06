//using System;
//using NUnit.Framework;
//
//namespace Composition.Monads
//{
//	public static class Maybe
//	{
//		public static Maybe<T> FromValue<T>(T value)
//		{
//			return new Maybe<T>(null, value);
//		}
//
//		public static Maybe<T> FromError<T>(Exception e)
//		{
//			return new Maybe<T>(e, default(T));
//		}
//
//		public static Maybe<T> Result<T>(Func<T> f)
//		{
//			try
//			{
//				return FromValue(f());
//			}
//			catch (Exception e)
//			{
//				return FromError<T>(e);
//			}
//		}
//		public static Maybe<TOut> SelectMany<TIn, TTemp, TOut>(this Maybe<TIn> m, Func<TIn, TTemp> map, Func<TIn, TTemp, TOut> res)
//		{
//			return m.Success ? Result(() => res(m.Value, map(m.Value))) : FromError<TOut>(m.Error);
//		}
//
//		public static Maybe<TOut> SelectMany<TIn, TOut>(this Maybe<TIn> m, Func<TIn, TOut> map)
//		{
//			return m.Success ? Result(() => map(m.Value)) : FromError<TOut>(m.Error);
//		}
//	}
//
//	public class Maybe<T>
//	{
//		public Maybe(Exception error, T value)
//		{
//			Error = error;
//			Value = value;
//		}
//
//		public Exception Error { get; private set; }
//		public T Value { get; private set; }
//		public bool Success { get { return Error == null; } }
//	}
//
//	[TestFixture]
//	public class Maybe_should
//	{
//		[Test]
//		public void Create_FromValue()
//		{
//			var m = Maybe.FromValue(42);
//
//			Assert.That(m.Success, Is.True);
//			Assert.That(m.Value, Is.EqualTo(42));
//		}
//
//		[Test]
//		public void Create_FromError()
//		{
//			var e = new Exception("123");
//			var m = Maybe.FromError<int>(e);
//
//			Assert.That(m.Success, Is.False);
//			Assert.That(m.Error, Is.EqualTo(e));
//		}
//
//		[Test]
//		public void Support_Linq_Syntax()
//		{
//			var res =
//				from i in Maybe.Result(() => int.Parse("1358571172"))
//				from hex in Convert.ToString(i, 16)
//				from guid in Guid.Parse(hex+hex+hex+hex)
//				select guid;
//
//			Assert.That(res.Success, Is.True);
//			Assert.That(res.Value, Is.EqualTo(Guid.Parse("50FA26A450FA26A450FA26A450FA26A4")));
//		}
//
//		[Test]
//		public void Return_Error_If_Error_On_Final_Stage()
//		{
//			var res =
//				from i in Maybe.Result(() => int.Parse("0"))
//				from hex in Convert.ToString(i, 16)
//				from guid in Guid.Parse(hex + hex + hex + hex)  //Error is here!
//				select guid;
//
//			Assert.That(res.Error, Is.InstanceOf<FormatException>());
//			Console.WriteLine(res.Error.Message);
//		}
//
//		[Test]
//		public void Propagate_Error_Through_All_Stages()
//		{
//			var res =
//				from i in Maybe.Result(() => int.Parse("UNPARSABLE")) //Error is here!
//				from hex in Convert.ToString(i, 16)
//				from guid in Guid.Parse(hex + hex + hex + hex)
//				select guid;
//
//			Assert.That(res.Error, Is.InstanceOf<FormatException>());
//			Console.WriteLine(res.Error.Message);
//		}
//	}
//}
