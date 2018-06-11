using System;
using System.Linq;
using System.Linq.Expressions;

namespace GreenUtil.Linq
{
    /// <summary>
    /// Classe para lógicas relacionadas a <see cref="Expression"/>
    /// </summary>
    public static class ExpressionUtil
    {
        /// <summary>
        /// Boilerplate para iniciar um predicado como verdadeiro
        /// </summary>
        /// <typeparam name="T">Tipo da Expressão do predicado</typeparam>
        /// <returns>Expressão que retorna sempre verdadeiro</returns>
        public static Expression<Func<T, bool>> True<T>() { return f => true; }

        /// <summary>
        /// Boilerplate para iniciar um predicado como falso
        /// </summary>
        /// <typeparam name="T">Tipo da Expressão do predicado</typeparam>
        /// <returns>Expressão que retorna sempre falso</returns>
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        /// <summary>
        /// Método para compor uma expressão utilizando o operador binário Or
        /// </summary>
        /// <typeparam name="T">Tipo da Expressão do predicado</typeparam>
        /// <param name="expr1">Expressão do Tipo <see cref="T"/> a esquerda da condição Or</param>
        /// <param name="expr2">Expressão do Tipo <see cref="T"/> a direita da condição Or</param>
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
        /// Método para compor uma expressão utilizando o operador binário And
        /// </summary>
        /// <typeparam name="T">Tipo da Expressão do predicado</typeparam>
        /// <param name="expr1">Expressão do Tipo <see cref="T"/> a esquerda da condição And</param>
        /// <param name="expr2">Expressão do Tipo <see cref="T"/> a direita da condição And</param>
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
