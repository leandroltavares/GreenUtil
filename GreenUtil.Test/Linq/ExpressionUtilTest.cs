using GreenUtil.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenUtil.Test.Linq
{
    /// <summary>
    /// Classe para testes relacionadas a <see cref="Expression"/>
    /// </summary>
    [TestClass]
    public class ExpressionUtilTest
    {
        [TestMethod]
        public void TrueMethodShouldReturnTrueExpression()
        {
            Assert.IsTrue(ExpressionUtil.True<object>().Compile().Invoke(null));
        }

        [TestMethod]
        public void FalseMethodShouldReturnFalseExpression()
        {
            Assert.IsFalse(ExpressionUtil.False<object>().Compile().Invoke(null));
        }

        [TestMethod]
        public void AndExpressionBetweenTwoTrueExpressionShouldReturnTrue()
        {
            var trueExpression = ExpressionUtil.True<object>();

            Assert.IsTrue(ExpressionUtil.And(trueExpression, trueExpression).Compile().Invoke(null));
        }

        [TestMethod]
        public void AndExpressionBetweenOneTrueExpressionAndOneFalseExpressionShouldReturnFalse()
        {
            var trueExpression = ExpressionUtil.True<object>();
            var falseExpression = ExpressionUtil.False<object>();

            Assert.IsFalse(ExpressionUtil.And(falseExpression, trueExpression).Compile().Invoke(null));
        }

        [TestMethod]
        public void AndExpressionBetweenTwoFalseExpressionShouldReturnFalse()
        {
            var falseExpression = ExpressionUtil.False<object>();

            Assert.IsFalse(ExpressionUtil.And(falseExpression, falseExpression).Compile().Invoke(null));
        }

        [TestMethod]
        public void AndExpressionWithFirstArgumentNullShouldThrowArgumentException()
        {
            var falseExpression = ExpressionUtil.False<object>();

            Assert.ThrowsException<ArgumentNullException>(() => ExpressionUtil.And(null, falseExpression));
        }

        [TestMethod]
        public void AndExpressionWithSecondArgumentNullShouldThrowArgumentException()
        {
            var falseExpression = ExpressionUtil.False<object>();

            Assert.ThrowsException<ArgumentNullException>(() => ExpressionUtil.And(falseExpression, null));
        }


        [TestMethod]
        public void OrExpressionBetweenTwoFalseExpressionShouldReturnFalse()
        {
            var falseExpression = ExpressionUtil.False<object>();

            Assert.IsFalse(ExpressionUtil.Or(falseExpression, falseExpression).Compile().Invoke(null));
        }

        [TestMethod]
        public void OrExpressionBetweenOneTrueExpressionAndOneFalseExpressionShouldReturnTrue()
        {
            var trueExpression = ExpressionUtil.True<object>();
            var falseExpression = ExpressionUtil.False<object>();

            Assert.IsTrue(ExpressionUtil.Or(falseExpression, trueExpression).Compile().Invoke(null));
        }


        [TestMethod]
        public void OrExpressionBetweenTwoTrueExpressionShouldReturnTrue()
        {
            var trueExpression = ExpressionUtil.True<object>();

            Assert.IsTrue(ExpressionUtil.And(trueExpression, trueExpression).Compile().Invoke(null));
        }

        [TestMethod]
        public void OrExpressionWithFirstArgumentNullShouldThrowArgumentException()
        {
            var falseExpression = ExpressionUtil.False<object>();

            Assert.ThrowsException<ArgumentNullException>(() => ExpressionUtil.Or(null, falseExpression));
        }

        [TestMethod]
        public void OrExpressionWithSecondArgumentNullShouldThrowArgumentException()
        {
            var falseExpression = ExpressionUtil.False<object>();

            Assert.ThrowsException<ArgumentNullException>(() => ExpressionUtil.Or(falseExpression, null));
        }
    }
}
