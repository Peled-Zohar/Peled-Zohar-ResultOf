using System;

namespace ResultOf
{
    /// <summary>
    /// Provides a way to return a success indicator 
    /// and (in case of an error) error description from a method.
    /// The Result class overloads the &amp; |, true and false operators to make it easy to use in validatios.
    /// The &amp; operator returns the first failed operand (or the last operand tested),
    /// and the | operator returns the first succeesfull operand (or the last operand tested).
    /// The &amp;&amp; operator and || operators will do the same, but in a short-circuit way.
    /// </summary>
    public class Result
    {
        #region ctor

        /// <summary>
        /// Initializes a new instance of the Result class to indicate a success.
        /// </summary>
        /// <returns>An instance of the Result class indicating success.</returns>
        public static Result Success()
        {
            return new Result();
        }

        /// <summary>
        /// Initializes a new instance of the Result class to indicate a failure.
        /// </summary>
        /// <param name="errorDescription">Description of the error.</param>
        /// <returns>An instance of the Result class indicating failure.</returns>
        public static Result Fail(string errorDescription)
        {
            return new Result(errorDescription);
        }

        /// <summary>
        /// Initializes a new instance of the Result class to indicate a success.
        /// </summary>
        protected Result()
        {
            Succeeded = true;
        }

        /// <summary>
        /// Initializes a new instance of the Result class to indicate a failure.
        /// </summary>
        /// <param name="errorDescription">Description of the error.</param>
        protected Result(string errorDescription)
        {
            Succeeded = false;
            ErrorDescription = errorDescription;
        }

        #endregion ctor

        #region properties

        /// <summary>
        /// Gets a boolean value indicating success or failure of the method.
        /// </summary>
        public bool Succeeded { get; protected set; }

        /// <summary>
        /// Gets the description of the error.
        /// </summary>
        public string ErrorDescription { get; protected set; }

        #endregion properties

        #region operators

        /// <summary>
        /// Returns the first operand if it's <see cref="Succeeded"/> property is false,
        /// otherwise the second operand.
        /// This can be used to determin the first result that failed in a sequence of results.
        /// </summary>
        /// <example>
        /// The following code will return the first instance that it's <see cref="Succeeded"/> property is false,
        /// or result3 if all succeeded.
        /// <code>
        /// var result = result1 &amp; result2 &amp; result3;
        /// </code>
        /// </example>
        /// <param name="self">An instance of the Result class.</param>
        /// <param name="other">An instance of the Result class.</param>
        /// <returns><paramref name="self"/> if not succeeded, <paramref name="other"/> otherwise.</returns>
        /// <exception cref="ArgumentNullException">if any of the operands is null.</exception>
        public static Result operator &(Result self, Result other)
        {
            if (self is null) throw new ArgumentNullException(nameof(self));
            if (other is null) throw new ArgumentNullException(nameof(other));

            return self.Succeeded ? other : self;
        }

        /// <summary>
        /// Returns the first operand if it's <see cref="Succeeded"/> property is true,
        /// otherwise the second operand.
        /// This can be used to determin the first result that succedded in a sequence of results.
        /// </summary>
        /// <example>
        /// The following code will return the first instance that it's <see cref="Succeeded"/> property is true,
        /// or result3 if all succeeded.
        /// <code>
        /// var result = result1 &amp; result2 &amp; result3;
        /// </code>
        /// </example>
        /// <param name="self">An instance of the Result class.</param>
        /// <param name="other">An instance of the Result class.</param>
        /// <returns><paramref name="self"/> if succeeded, <paramref name="other"/> otherwise.</returns>
        /// <exception cref="ArgumentNullException">if any of the operands is null.</exception>
        public static Result operator |(Result self, Result other)
        {
            if (self is null) throw new ArgumentNullException(nameof(self));
            if (other is null) throw new ArgumentNullException(nameof(other));
            return self.Succeeded ? self : other;
        }

        /// <summary>
        /// Returns true when Succedded.
        /// <para>
        /// This operator is needed to allow the usage of the || operator.
        /// </para>
        /// </summary>
        /// <param name="self">The instance of the Result class to test.</param>
        /// <returns>True when succedded, false otherwise.</returns>
        public static bool operator true(Result self)
        {
            return self.Succeeded;
        }

        /// <summary>
        /// Returns false when succedded. (the opposite of the true operator.)
        /// <para>
        /// This operator is needed to allow the usage of the &amp;&amp; operator.
        /// </para>
        /// </summary>
        /// <param name="self">The instance of the Result class to test.</param>
        /// <returns>False when succedded, true otherwise.</returns>
        public static bool operator false(Result self)
        {
            return !self.Succeeded;
        }

        #endregion operators
    }
}
