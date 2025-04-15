﻿﻿using System.Linq.Expressions;

namespace SmartInventoryBE.Extensions
{
    public static class ExpressionExtensions
    {
        /// <summary>
        ///     Converts the property accessor lambda expression to a textual representation of it's path. <br />
        ///     The textual representation consists of the properties that the expression access flattened and separated by a dot character (".").
        /// </summary>
        /// <param name="expression">The property selector expression.</param>
        /// <returns>The extracted textual representation of the expression's path.</returns>
        public static string AsPath(this LambdaExpression? expression)
        {
            if (expression is null)
            {
                return "";
            }

            TryParsePath(expression.Body, out var path);

            return path ?? "";
        }

        /// <summary>
        ///     Recursively parses an expression tree representing a property accessor to extract a textual representation of it's path. <br />
        ///     The textual representation consists of the properties accessed by the expression tree flattened and separated by a dot character (".").
        /// </summary>
        /// <param name="expression">The expression tree to parse.</param>
        /// <param name="path">The extracted textual representation of the expression's path.</param>
        /// <returns>True if the parse operation succeeds; otherwise, false.</returns>
        private static bool TryParsePath(Expression expression, out string? path)
        {
            var noConvertExp = RemoveConvertOperations(expression);
            path = null;

            if (noConvertExp is MemberExpression memberExpression)
            {
                return TryParseMemberExpression(memberExpression, out path);
            }

            if (noConvertExp is MethodCallExpression methodCallExpression)
            {
                return TryParseMethodCallExpression(methodCallExpression, out path);
            }

            return true;
        }

        /// <summary>
        ///     Parses a MemberExpression to extract the property path.
        /// </summary>
        /// <param name="memberExpression">The MemberExpression to parse.</param>
        /// <param name="path">The extracted textual representation of the member expression's path.</param>
        /// <returns>True if the parse operation succeeds; otherwise, false.</returns>
        private static bool TryParseMemberExpression(MemberExpression memberExpression, out string? path)
        {
            var currentPart = memberExpression.Member.Name;

            if (memberExpression.Expression is null || !TryParsePath(memberExpression.Expression, out var parentPart))
            {
                path = null;
                return false;
            }

            path = string.IsNullOrEmpty(parentPart) ? currentPart : $"{parentPart}.{currentPart}";
            return true;
        }

        /// <summary>
        ///     Parses a MethodCallExpression to extract the property path.
        /// </summary>
        /// <param name="methodCallExpression">The MethodCallExpression to parse.</param>
        /// <param name="path">The extracted textual representation of the method call expression's path.</param>
        /// <returns>True if the parse operation succeeds; otherwise, false.</returns>
        private static bool TryParseMethodCallExpression(MethodCallExpression methodCallExpression, out string? path)
        {
            switch (methodCallExpression.Method.Name)
            {
                case nameof(Queryable.Select) when methodCallExpression.Arguments.Count == 2:
                    return TryParseSelectMethod(methodCallExpression, out path);
                case nameof(Queryable.Where):
                    throw new NotSupportedException("Filtering an Include expression is not supported");
                case nameof(Queryable.OrderBy):
                case nameof(Queryable.OrderByDescending):
                    throw new NotSupportedException("Ordering an Include expression is not supported");
                default:
                    path = null;
                    return false;
            }
        }

        /// <summary>
        ///     Parses a Select MethodCallExpression to extract the property path.
        /// </summary>
        /// <param name="selectExpression">The MethodCallExpression representing a Select method call to parse.</param>
        /// <param name="path">The extracted textual representation of the select method call expression's path.</param>
        /// <returns>True if the parse operation succeeds; otherwise, false.</returns>
        private static bool TryParseSelectMethod(MethodCallExpression selectExpression, out string? path)
        {
            if (!TryParsePath(selectExpression.Arguments[0], out var parentPart))
            {
                path = null;
                return false;
            }

            if (string.IsNullOrEmpty(parentPart))
            {
                path = null;
                return false;
            }

            if (!(selectExpression.Arguments[1] is LambdaExpression subExpression))
            {
                path = null;
                return false;
            }

            if (!TryParsePath(subExpression.Body, out var currentPart))
            {
                path = null;
                return false;
            }

            if (string.IsNullOrEmpty(currentPart))
            {
                path = null;
                return false;
            }

            path = $"{parentPart}.{currentPart}";
            return true;
        }

        /// <summary>
        ///     Removes all casts or conversion operations from the nodes of the provided <see cref="Expression" />.
        ///     Used to prevent type boxing when manipulating expression trees.
        /// </summary>
        /// <param name="expression">The expression to remove the conversion operations.</param>
        /// <returns>The expression without conversion or cast operations.</returns>
        private static Expression RemoveConvertOperations(Expression expression)
        {
            while (expression.NodeType == ExpressionType.Convert
                || expression.NodeType == ExpressionType.ConvertChecked)
            {
                expression = ((UnaryExpression)expression).Operand;
            }

            return expression;
        }
    }
}
