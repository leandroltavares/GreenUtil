using System;
using System.Linq;
using System.Linq.Expressions;

namespace GreenUtil.Linq
{
    /// <summary>
    /// Logic related to <see cref="Expression"/>
    /// </summary>
    public static class ExpressionUtil
    {
        /// <summary>
        /// Boilerplate for initializing a predicate as True
        /// </summary>
        /// <typeparam name="T">Predicate expression type</typeparam>
        /// <returns>Expression that will always return true</returns>
        public static Expression<Func<T, bool>> True<T>() { return f => true; }

        /// <summary>
        /// Boilerplate for initializing a predicate as False
        /// </summary>
        /// <typeparam name="T">Predicate expression type</typeparam>
        /// <returns>Expression that will always return false</returns>
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        /// <summary>
        /// Compose an expression with OR binary operator
        /// </summary>
        /// <typeparam name="T">Predicate expression type</typeparam>
        /// <param name="expr1">Left side expression</param>
        /// <param name="expr2">Right side expression</param>
        /// <returns>Expressão combinada com a condição Or</returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                            Expression<Func<T, bool>> expr2)
        {
            if (expr1 == null)
                throw new ArgumentNullException(nameof(expr1));

            if (expr2 == null)
                throw new ArgumentNullException(nameof(expr2));

            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        /// <summary>
        /// Compose an expression with AND binary operator
        /// </summary>
        /// <typeparam name="T">Predicate expression type</typeparam>
        /// <param name="expr1">Left side expression</param>
        /// <param name="expr2">Right side expression</param>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                             Expression<Func<T, bool>> expr2)
        {
            if (expr1 == null)
                throw new ArgumentNullException(nameof(expr1));

            if (expr2 == null)
                throw new ArgumentNullException(nameof(expr2));

            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }
    }
}
