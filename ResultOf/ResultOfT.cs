using System;

namespace ResultOf
{
    /// <summary>
    /// Provides a way to return a value and a boolean success indicator, 
    /// and (in case of an error) error description from a method.
    /// </summary>
    /// The <see cref="Result{T}"/> class overloads the &amp; and | operators to make it easy to use in validatios.
    /// The &amp; operator returns the first failed operand (or the last operand tested),
    /// and the | operator returns the first succeesfull operand (or the last operand tested).
    public class Result<T> : Result
    {
        #region ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="Result{T}"/> class to indicate a success.
        /// </summary>
        /// <param name="value">The value to return from the method.</param>
        /// <returns>A new instance of the <see cref="Result{T}"/> class indicating success.</returns>
        public static Result<T> Success(T value)
            => new Result<T>(value);

        /// <summary>
        /// Initializes a new instance of the <see cref="Result{T}"/> class to indicate a failure.
        /// </summary>
        /// <param name="errorDescription">Description of the error.</param>
        /// <returns>A new instance of the <see cref="Result{T}"/> class indicating a failure.</returns>
        public static new Result<T> Fail(string errorDescription)
            => new Result<T>(errorDescription, default);

        /// <summary>
        /// Initializes a new instance of the <see cref="Result{T}"/> class to indicate a failure,
        /// yet still returning the value.
        /// </summary>
        /// <param name="errorDescription">Description of the error.</param>
        /// <param name="value">The value to return from the method.</param>
        /// <returns>A new instance of the <see cref="Result{T}"/> class indicating a failure, but still have a value.</returns>
        public static Result<T> Fail(string errorDescription, T value)
            => new Result<T>(errorDescription, value);

        /// <summary>
        /// Initializes a new instance of the <see cref="Result{T}"/> class to indicate a success.
        /// </summary>
        /// <param name="value">The value to return from the method.</param>
        protected Result(T value) : base()
            => Value = value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Result{T}"/> class to indicate a failure,
        /// yet still returning the value.
        /// </summary>
        /// <param name="errorDescription">Description of the error.</param>
        /// <param name="value">The value to return from the method.</param>
        protected Result(string errorDescription, T value) : base(errorDescription)
            => Value = value;

        #endregion ctor

        #region properties

        /// <summary>
        /// The value to return from the method.
        /// </summary>
        public T Value { get; }

        #endregion properties

        #region operators

        /// <summary>
        /// Returns the first operand if it's <see cref="Result.Succeeded"/> property is false,
        /// otherwise the second operand.
        /// This can be used to determin the first result that failed in a sequence of results.
        /// </summary>
        /// <example>
        /// The following code will return the first instance that it's <see cref="Result.Succeeded"/> property is false,
        /// or result3 if all succeeded.
        /// <code>
        /// var result = result1 &amp; result2 &amp; result3;
        /// </code>
        /// </example>
        /// <param name="self">An instance of the <see cref="Result{T}"/> class.</param>
        /// <param name="other">An instance of the <see cref="Result{T}"/> class.</param>
        /// <returns><paramref name="self"/> if not succeeded, <paramref name="other"/> otherwise.</returns>
        /// <exception cref="ArgumentNullException">if any of the operands is null.</exception>
        public static Result<T> operator &(Result<T> self, Result<T> other)
        {
            if (self is null) throw new ArgumentNullException(nameof(self));
            if (other is null) throw new ArgumentNullException(nameof(other));

            return self.Succeeded ? other : self;
        }

        /// <summary>
        /// Returns the first operand if it's <see cref="Result.Succeeded"/> property is true,
        /// otherwise the second operand.
        /// This can be used to determin the first result that succeeded in a sequence of results.
        /// </summary>
        /// <example>
        /// The following code will return the first instance that it's <see cref="Result.Succeeded"/> property is true,
        /// or result3 if all succeeded.
        /// <code>
        /// var result = result1 &amp; result2 &amp; result3;
        /// </code>
        /// </example>
        /// <param name="self">An instance of the <see cref="Result{T}"/> class.</param>
        /// <param name="other">An instance of the Result<typeparamref name="T"/> class.</param>
        /// <returns><paramref name="self"/> if succeeded, <paramref name="other"/> otherwise.</returns>
        /// <exception cref="ArgumentNullException">if any of the operands is null.</exception>
        public static Result<T> operator |(Result<T> self, Result<T> other)
        {
            if (self is null) throw new ArgumentNullException(nameof(self));
            if (other is null) throw new ArgumentNullException(nameof(other));
            return self.Succeeded ? self : other;
        }

        #endregion operators
    }
}
