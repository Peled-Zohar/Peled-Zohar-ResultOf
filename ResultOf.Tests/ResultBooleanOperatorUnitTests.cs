using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResultOf.Tests
{
    class ResultBooleanOperatorUnitTests
    {
        private Result _fail, _success;

        [SetUp]
        public void Setup()
        {
            _fail = Result.Fail("fail");
            _success = Result.Success();
        }

        [Test]
        public void FalseOperator_ReturnsFalseOnFailure()
        {
            if(_fail)
            {
                Assert.Fail("Fail result should have returned false.");
            }
            else
            {
                Assert.Pass();
            }
        }

        [Test]
        public void TrueOperator_ReturnsTrueOnSuccess()
        {
            if (_success)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail("Success result should have returned true."); 
            }
        }
    }
}
