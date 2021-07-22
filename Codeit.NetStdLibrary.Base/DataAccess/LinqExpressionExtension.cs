/// <summary>
/// 
/// </summary>
namespace Codeit.NetStdLibrary.Base.DataAccess
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>Http://stackoverflow.com/a/10613631
    /// http://www.albahari.com/nutshell/predicatebuilder.aspx.
    /// </summary>
    public static class LinqExpressionExtension
    {
        #region Methods

        /// <summary>Combina 2 expresiones con el operador logico AND.
        /// Genera una nueva expresión combinando la expresión origen con la otra expresión
        /// con el operador logico AND.</summary>
        /// <param name="source">Expresión origen.</param>
        /// <param name="other">Expresión a combinar.</param>
        /// <typeparam name="TEntity">Tipo de entidad.</typeparam>
        /// <returns>la combinación de las expresiones con el operador logico AND. <see cref="Expression"/>.</returns>
        public static Expression<Func<TEntity, bool>> And<TEntity>(
            this Expression<Func<TEntity, bool>> source,
            Expression<Func<TEntity, bool>> other) where TEntity : class
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            SwapVisitor swap = new SwapVisitor(source.Parameters[0], other.Parameters[0]);
            Expression expression = swap.Visit(source.Body);
            BinaryExpression andExpression = Expression.AndAlso(expression, other.Body);
            Expression<Func<TEntity, bool>> lambda = Expression.Lambda<Func<TEntity, bool>>(
                andExpression,
                other.Parameters);
            return lambda;
        }

        /// <summary>Combina 2 expresiones con el operador logico AND.
        /// Genera una nueva expresión combinando la expresión origen con la otra expresión
        /// con el operador logico AND.</summary>
        /// <typeparam name="TEntity">Tipo de entidad.</typeparam>
        /// <param name="source">Expresión origen.</param>
        /// <param name="other">Expresión a combinar.</param>
        /// <returns>la combinación de las expresiones con el operador logico AND. <see cref="Func{2}"/>.</returns>
        public static Func<TEntity, bool> And<TEntity>(this Func<TEntity, bool> source, Func<TEntity, bool> other)
            where TEntity : class
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            Func<TEntity, bool> ret = t => source(t) && other(t);
            return ret;
        }

        /// <summary>The between.</summary>
        /// <param name="expression">The expression.</param>
        /// <param name="from">The from.</param>
        /// <param name="to">The to.</param>
        /// <param name="strict">The strict.</param>
        /// <typeparam name="TEntity">Tipo de entidad.</typeparam>
        /// <typeparam name="TResult">Parametro generico para el tipo de la propiedad.</typeparam>
        /// <returns>Expresion de comparación entre 2 valores.<see cref="Expression"/>.</returns>
        public static Expression<Func<TEntity, bool>> Between<TEntity, TResult>(
            Expression<Func<TEntity, TResult>> expression,
            object from,
            object to,
            bool strict = false)
            where TEntity : class
        {
            MemberInfo member = DecodeMemberAccessExpression(expression);
            ParameterExpression parameter = Expression.Parameter(typeof(TEntity));
            MemberExpression property = Expression.PropertyOrField(parameter, member.Name);
            Expression<Func<TEntity, bool>> mayorIgual;
            if (strict)
            {
                mayorIgual =
                    Expression.Lambda<Func<TEntity, bool>>(
                        Expression.GreaterThan(property, Expression.Constant(from)),
                        parameter);
            }
            else
            {
                mayorIgual =
                    Expression.Lambda<Func<TEntity, bool>>(
                        Expression.GreaterThanOrEqual(property, Expression.Constant(from)),
                        parameter);
            }

            Expression<Func<TEntity, bool>> menorIgual;
            if (strict)
            {
                menorIgual =
                    Expression.Lambda<Func<TEntity, bool>>(
                        Expression.LessThan(property, Expression.Constant(to)),
                        parameter);
            }
            else
            {
                menorIgual =
                    Expression.Lambda<Func<TEntity, bool>>(
                        Expression.LessThanOrEqual(property, Expression.Constant(to)),
                        parameter);
            }

            Expression<Func<TEntity, bool>> betwennExpression = mayorIgual.And(menorIgual);
            Expression<Func<TEntity, bool>> lambda = Expression.Lambda<Func<TEntity, bool>>(
                betwennExpression.Body,
                parameter);
            return lambda;
        }

        /// <summary>Decodifica una expresión y obtiene la propiedad o field que se esta accediendo.</summary>
        /// <param name="expression">The expression.</param>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <returns>The <see cref="MemberInfo"/>.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static MemberInfo DecodeMemberAccessExpression<TEntity, TResult>(
            Expression<Func<TEntity, TResult>> expression)
        {
            if (expression.Body.NodeType != ExpressionType.MemberAccess)
            {
                if (expression.Body.NodeType == ExpressionType.Convert && expression.Body.Type == typeof(object))
                {
                    return ((MemberExpression)((UnaryExpression)expression.Body).Operand).Member;
                }

                throw new InvalidOperationException(
                    string.Format(
                        "Invalid expression type: Expected ExpressionType.MemberAccess, Found {0}",
                        expression.Body.NodeType));
            }

            return ((MemberExpression)expression.Body).Member;
        }

        /// <summary>Combina 2 expresiones con el operador logico OR.
        /// Genera una nueva expresión combinando la expresión origen con la otra expresión
        /// con el operador logico OR.</summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source">Expresión origen.</param>
        /// <param name="other">Expresión a combinar.</param>
        /// <returns>The <see cref="Expression"/>.</returns>
        public static Expression<Func<TEntity, bool>> Or<TEntity>(
            this Expression<Func<TEntity, bool>> source,
            Expression<Func<TEntity, bool>> other) where TEntity : class
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            SwapVisitor swap = new SwapVisitor(source.Parameters[0], other.Parameters[0]);
            Expression expression = swap.Visit(source.Body);
            BinaryExpression orExpression = Expression.OrElse(expression, other.Body);
            Expression<Func<TEntity, bool>> lambda = Expression.Lambda<Func<TEntity, bool>>(
                orExpression,
                other.Parameters);
            return lambda;
        }

        /// <summary>Combina 2 expresiones con el operador logico OR.
        /// Genera una nueva expresión combinando la expresión origen con la otra expresión
        /// con el operador logico OR.</summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source">Expresión origen.</param>
        /// <param name="other">Expresión a combinar.</param>
        /// <returns>The <see cref="Func"/>.</returns>
        public static Func<TEntity, bool> Or<TEntity>(this Func<TEntity, bool> source, Func<TEntity, bool> other)
            where TEntity : class
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            Func<TEntity, bool> ret = t => source(t) || other(t);
            return ret;
        }

        #endregion
    }

    /// <summary>Clase de ayuda para combinar las expresiones.</summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules",
                     "SA1402:FileMayOnlyContainASingleClass",
                     Justification = "Es una clase de uso interno exclusivo.")]
    internal class SwapVisitor : ExpressionVisitor
    {
        #region Fields

        /// <summary>Expresión origen.</summary>
        private readonly Expression from;

        /// <summary>Expresión destino.</summary>
        private readonly Expression to;

        #endregion

        #region Constructors

        /// <summary>Constructor SwapVisitor.</summary>
        /// <param name="from">Expresión origen.</param>
        /// <param name="to">Expresión destino.</param>
        public SwapVisitor(Expression from, Expression to)
        {
            this.from = from;
            this.to = to;
        }

        #endregion

        #region Methods

        /// <summary>Devuelve la expresión.</summary>
        /// <param name="node">Nodo a visitar.</param>
        /// <returns>La expresión.</returns>
        public override Expression Visit(Expression node)
        {
            return node == @from ? to : base.Visit(node);
        }

        #endregion
    }
}
